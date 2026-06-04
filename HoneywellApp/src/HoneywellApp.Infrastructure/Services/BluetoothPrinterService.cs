using HoneywellApp.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using System.Text;

namespace HoneywellApp.Infrastructure.Services;

public class BluetoothPrinterService : IBluetoothPrinterService, IDisposable
{
    private readonly IBluetoothLE _ble;
    private readonly IAdapter _adapter;
    private readonly ILogger<BluetoothPrinterService> _logger;

    private IDevice? _connectedDevice;
    private ICharacteristic? _writeCharacteristic;

    private static readonly string[] HoneywellKeywords = ["Honeywell", "PC42", "PD43", "PM45", "PC45", "CT45"];

    public bool IsScanning { get; private set; }
    public bool IsConnected => _connectedDevice?.State == Plugin.BLE.Abstractions.DeviceState.Connected;
    public string? ConnectedDeviceName => _connectedDevice?.Name;

    public event EventHandler<BluetoothDevice>? DeviceDiscovered;
    public event EventHandler<bool>? ConnectionStateChanged;

    public BluetoothPrinterService(ILogger<BluetoothPrinterService> logger)
    {
        _logger = logger;
        _ble = CrossBluetoothLE.Current;
        _adapter = CrossBluetoothLE.Current.Adapter;
        _adapter.DeviceDiscovered += OnDeviceDiscovered;
        _adapter.DeviceConnected += OnDeviceConnected;
        _adapter.DeviceDisconnected += OnDeviceDisconnected;
    }

    public async Task<IEnumerable<BluetoothDevice>> ScanForPrintersAsync(TimeSpan timeout, CancellationToken ct = default)
    {
        var found = new List<BluetoothDevice>();

        if (_ble.State != BluetoothState.On)
        {
            _logger.LogWarning("Bluetooth is not enabled");
            return found;
        }

        IsScanning = true;
        _adapter.ScanTimeout = (int)timeout.TotalMilliseconds;

        var tcs = new TaskCompletionSource<bool>();
        ct.Register(() => tcs.TrySetCanceled());
        _adapter.ScanTimeoutElapsed += (_, _) => tcs.TrySetResult(true);

        await _adapter.StartScanningForDevicesAsync(cancellationToken: ct);

        try { await tcs.Task; } catch (OperationCanceledException) { }

        IsScanning = false;
        await _adapter.StopScanningForDevicesAsync();

        foreach (var device in _adapter.DiscoveredDevices)
        {
            if (IsHoneywellPrinter(device.Name))
                found.Add(MapDevice(device));
        }

        return found;
    }

    public async Task<bool> ConnectAsync(string deviceId, CancellationToken ct = default)
    {
        try
        {
            var device = _adapter.DiscoveredDevices
                .FirstOrDefault(d => d.Id.ToString() == deviceId);

            if (device == null)
            {
                _logger.LogWarning("Device {Id} not found in discovered devices", deviceId);
                return false;
            }

            await _adapter.ConnectToDeviceAsync(device, cancellationToken: ct);
            _connectedDevice = device;

            var services = await device.GetServicesAsync(ct);

            // Try known Honeywell BLE service UUIDs
            var printerService = services.FirstOrDefault(s =>
                s.Id.ToString().ToLower().Contains("18f0") ||
                s.Id.ToString().ToLower().Contains("e7810a71"));

            if (printerService != null)
            {
                var characteristics = await printerService.GetCharacteristicsAsync();
                _writeCharacteristic = characteristics.FirstOrDefault(c => c.CanWrite);
            }

            ConnectionStateChanged?.Invoke(this, true);
            _logger.LogInformation("Connected to {Name}", device.Name);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to connect to device {Id}", deviceId);
            return false;
        }
    }

    public async Task DisconnectAsync(CancellationToken ct = default)
    {
        if (_connectedDevice == null) return;
        try
        {
            await _adapter.DisconnectDeviceAsync(_connectedDevice);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Error during disconnect");
        }
        finally
        {
            _connectedDevice = null;
            _writeCharacteristic = null;
            ConnectionStateChanged?.Invoke(this, false);
        }
    }

    public async Task<bool> PrintZplAsync(string zplContent, CancellationToken ct = default)
    {
        if (!IsConnected || _writeCharacteristic == null)
        {
            _logger.LogWarning("Not connected to a printer");
            return false;
        }

        try
        {
            var bytes = Encoding.UTF8.GetBytes(zplContent);
            const int chunkSize = 512;
            for (int i = 0; i < bytes.Length; i += chunkSize)
            {
                var chunk = bytes.Skip(i).Take(chunkSize).ToArray();
                await _writeCharacteristic.WriteAsync(chunk, ct);
            }
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ZPL print failed");
            return false;
        }
    }

    public async Task<bool> PrintProductLabelAsync(string productName, string sku, string barcode, decimal price, CancellationToken ct = default)
    {
        var zpl = $"^XA\n^FO20,20^A0N,28,28^FD{productName}^FS\n^FO20,60^A0N,22,22^FDSKU: {sku}^FS\n^FO20,90^A0N,22,22^FDPrecio: ${price:F2}^FS\n^FO20,120^BY2^BCN,60,Y,N,N^FD{barcode}^FS\n^XZ";
        return await PrintZplAsync(zpl, ct);
    }

    private static bool IsHoneywellPrinter(string? name) =>
        name != null && HoneywellKeywords.Any(kw => name.Contains(kw, StringComparison.OrdinalIgnoreCase));

    private static BluetoothDevice MapDevice(IDevice d) => new()
    {
        Id = d.Id.ToString(),
        Name = d.Name ?? "Unknown",
        Rssi = d.Rssi,
        IsConnected = d.State == Plugin.BLE.Abstractions.DeviceState.Connected
    };

    private void OnDeviceDiscovered(object? sender, DeviceEventArgs e)
    {
        if (IsHoneywellPrinter(e.Device.Name))
            DeviceDiscovered?.Invoke(this, MapDevice(e.Device));
    }

    private void OnDeviceConnected(object? sender, DeviceEventArgs e) =>
        ConnectionStateChanged?.Invoke(this, true);

    private void OnDeviceDisconnected(object? sender, DeviceEventArgs e)
    {
        _connectedDevice = null;
        _writeCharacteristic = null;
        ConnectionStateChanged?.Invoke(this, false);
    }

    public void Dispose()
    {
        _adapter.DeviceDiscovered -= OnDeviceDiscovered;
        _adapter.DeviceConnected -= OnDeviceConnected;
        _adapter.DeviceDisconnected -= OnDeviceDisconnected;
    }
}
