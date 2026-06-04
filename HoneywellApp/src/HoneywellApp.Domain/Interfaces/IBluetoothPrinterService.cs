namespace HoneywellApp.Domain.Interfaces;

public class BluetoothDevice
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Rssi { get; set; }
    public bool IsConnected { get; set; }
}

public interface IBluetoothPrinterService
{
    bool IsScanning { get; }
    bool IsConnected { get; }
    string? ConnectedDeviceName { get; }

    Task<IEnumerable<BluetoothDevice>> ScanForPrintersAsync(TimeSpan timeout, CancellationToken cancellationToken = default);
    Task<bool> ConnectAsync(string deviceId, CancellationToken cancellationToken = default);
    Task DisconnectAsync(CancellationToken cancellationToken = default);
    Task<bool> PrintZplAsync(string zplContent, CancellationToken cancellationToken = default);
    Task<bool> PrintProductLabelAsync(string productName, string sku, string barcode, decimal price, CancellationToken cancellationToken = default);
    event EventHandler<BluetoothDevice>? DeviceDiscovered;
    event EventHandler<bool>? ConnectionStateChanged;
}
