using HoneywellApp.Domain.Entities.Maestros;
using HoneywellApp.Domain.Entities.Transaccionales;
using Microsoft.EntityFrameworkCore;

namespace HoneywellApp.Persistence.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Maestros
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<ClienteDatos> ClientesDatos => Set<ClienteDatos>();
    public DbSet<ClienteDireccion> ClientesDirecciones => Set<ClienteDireccion>();
    public DbSet<ClienteImpuesto> ClientesImpuestos => Set<ClienteImpuesto>();
    public DbSet<ClienteTipo> ClientesTipos => Set<ClienteTipo>();
    public DbSet<ClienteCuentaBancaria> ClientesCuentasBancarias => Set<ClienteCuentaBancaria>();
    public DbSet<Articulo> Articulos => Set<Articulo>();
    public DbSet<ArticuloImpuesto> ArticulosImpuestos => Set<ArticuloImpuesto>();
    public DbSet<MedidaUnidad> MedidasUnidades => Set<MedidaUnidad>();
    public DbSet<GrupoArticulo> GruposArticulos => Set<GrupoArticulo>();
    public DbSet<Ruta> Rutas => Set<Ruta>();
    public DbSet<RutaBodega> RutasBodegas => Set<RutaBodega>();
    public DbSet<RutaCliente> RutasClientes => Set<RutaCliente>();
    public DbSet<RutaLibro> RutasLibros => Set<RutaLibro>();
    public DbSet<RutaImei> RutasImei => Set<RutaImei>();
    public DbSet<RutaRegistroTipo> RutasRegistrosTipos => Set<RutaRegistroTipo>();
    public DbSet<BodegaArticulo> BodegasArticulos => Set<BodegaArticulo>();
    public DbSet<BodegaExistencia> BodegasExistencias => Set<BodegaExistencia>();
    public DbSet<ListaPrecio> ListasPrecios => Set<ListaPrecio>();
    public DbSet<ListaPrecioNivel> ListasPreciosNiveles => Set<ListaPrecioNivel>();
    public DbSet<ListaPrecioNivelDetalle> ListasPreciosNivelesDetalles => Set<ListaPrecioNivelDetalle>();
    public DbSet<ListaPrecioRuta> ListasPreciosRutas => Set<ListaPrecioRuta>();
    public DbSet<ListaPrecioCliente> ListasPreciosClientes => Set<ListaPrecioCliente>();
    public DbSet<PrecioEspecial> PreciosEspeciales => Set<PrecioEspecial>();
    public DbSet<Promocion> Promociones => Set<Promocion>();
    public DbSet<PromocionDetalle> PromocionesDetalles => Set<PromocionDetalle>();
    public DbSet<PromocionCanal> PromocionesCanales => Set<PromocionCanal>();
    public DbSet<PromocionCliente> PromocionesClientes => Set<PromocionCliente>();
    public DbSet<PromocionRegion> PromocionesRegiones => Set<PromocionRegion>();
    public DbSet<PromocionRuta> PromocionesRutas => Set<PromocionRuta>();
    public DbSet<PromocionGrupo> PromocionesGrupos => Set<PromocionGrupo>();
    public DbSet<PromocionGrupoDetalle> PromocionesGruposDetalles => Set<PromocionGrupoDetalle>();
    public DbSet<PromocionGrupoPromocion> PromocionesGruposPromociones => Set<PromocionGrupoPromocion>();
    public DbSet<TipoPromocion> TiposPromociones => Set<TipoPromocion>();
    public DbSet<Impuesto> Impuestos => Set<Impuesto>();
    public DbSet<ImpuestoDetalle> ImpuestosDetalles => Set<ImpuestoDetalle>();
    public DbSet<Negociacion> Negociaciones => Set<Negociacion>();
    public DbSet<Pais> Paises => Set<Pais>();
    public DbSet<Departamento> Departamentos => Set<Departamento>();
    public DbSet<Municipio> Municipios => Set<Municipio>();
    public DbSet<Banco> Bancos => Set<Banco>();
    public DbSet<CuentaBancaria> CuentasBancarias => Set<CuentaBancaria>();
    public DbSet<Serie> Series => Set<Serie>();
    public DbSet<Contingencia> Contingencias => Set<Contingencia>();
    public DbSet<Folio> Folios => Set<Folio>();
    public DbSet<Membrete> Membretes => Set<Membrete>();
    public DbSet<MembreteEstablecimiento> MembretesEstablecimientos => Set<MembreteEstablecimiento>();
    public DbSet<ControlSinc> ControlSincs => Set<ControlSinc>();
    public DbSet<Destinatario> Destinatarios => Set<Destinatario>();
    public DbSet<Cuota> Cuotas => Set<Cuota>();

    // Transaccionales
    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<PedidoDetalle> PedidosDetalles => Set<PedidoDetalle>();
    public DbSet<PedidoDetalleImpuesto> PedidosDetallesImpuestos => Set<PedidoDetalleImpuesto>();
    public DbSet<PedidoBeneficioDetalle> PedidosBeneficiosDetalles => Set<PedidoBeneficioDetalle>();
    public DbSet<PedidoNegociacionDetalle> PedidosNegociacionesDetalles => Set<PedidoNegociacionDetalle>();
    public DbSet<Factura> Facturas => Set<Factura>();
    public DbSet<FacturaDetalle> FacturasDetalles => Set<FacturaDetalle>();
    public DbSet<CambioDevolucion> CambiosDevolucion => Set<CambioDevolucion>();
    public DbSet<CambioDevolucionDetalle> CambiosDevolucionDetalles => Set<CambioDevolucionDetalle>();
    public DbSet<Recibo> Recibos => Set<Recibo>();
    public DbSet<ReciboDetalleCheque> RecibosDetallesCheques => Set<ReciboDetalleCheque>();
    public DbSet<ReciboDetalleFactura> RecibosDetallesFacturas => Set<ReciboDetalleFactura>();
    public DbSet<ReciboTalonario> RecibosTalonarios => Set<ReciboTalonario>();
    public DbSet<Deposito> Depositos => Set<Deposito>();
    public DbSet<RutaRegistro> RutasRegistros => Set<RutaRegistro>();
    public DbSet<RutaRegistroPedido> RutasRegistrosPedidos => Set<RutaRegistroPedido>();
    public DbSet<RutaRegistroCobro> RutasRegistrosCobros => Set<RutaRegistroCobro>();
    public DbSet<ClienteGestion> ClientesGestion => Set<ClienteGestion>();
    public DbSet<BackOrder> BackOrders => Set<BackOrder>();
    public DbSet<Documento> Documentos => Set<Documento>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Unique constraints equivalentes al CONFLICT REPLACE del Android
        modelBuilder.Entity<Usuario>().HasIndex(u => u.CodigoUsuario).IsUnique();
        modelBuilder.Entity<Usuario>().HasIndex(u => u.CodigoVendedor).IsUnique();

        modelBuilder.Entity<Cliente>().HasIndex(c => c.CodigoCliente).IsUnique();

        modelBuilder.Entity<ClienteDatos>().HasIndex(c => c.CodigoCliente).IsUnique();

        modelBuilder.Entity<ClienteDireccion>().HasIndex(c => c.CodigoClienteDireccionId).IsUnique();

        modelBuilder.Entity<ClienteImpuesto>()
            .HasIndex(c => new { c.CodigoCliente, c.CodigoImpuesto }).IsUnique();

        modelBuilder.Entity<ClienteTipo>().HasIndex(c => c.CodigoCliente).IsUnique();

        modelBuilder.Entity<ClienteCuentaBancaria>()
            .HasIndex(c => c.ClienteCuentaBancariaId).IsUnique();

        modelBuilder.Entity<Articulo>().HasIndex(a => a.CodigoArticulo).IsUnique();

        modelBuilder.Entity<ArticuloImpuesto>()
            .HasIndex(a => new { a.CodigoArticulo, a.CodigoImpuesto }).IsUnique();

        modelBuilder.Entity<MedidaUnidad>().HasIndex(m => m.CodigoMedidaUnidad).IsUnique();

        modelBuilder.Entity<GrupoArticulo>().HasIndex(g => g.Codigo).IsUnique();

        modelBuilder.Entity<Ruta>().HasIndex(r => r.CodigoRuta).IsUnique();

        modelBuilder.Entity<RutaBodega>()
            .HasIndex(r => new { r.CodigoBodega, r.CodigoRuta }).IsUnique();

        modelBuilder.Entity<RutaCliente>()
            .HasIndex(r => new { r.CodigoRuta, r.CodigoCliente }).IsUnique();

        modelBuilder.Entity<RutaLibro>()
            .HasIndex(r => new { r.CodigoRuta, r.CodigoCliente, r.Semana, r.Dia }).IsUnique();

        modelBuilder.Entity<RutaImei>()
            .HasIndex(r => new { r.CodigoRuta, r.CodigoUsuario }).IsUnique();

        modelBuilder.Entity<RutaRegistroTipo>()
            .HasIndex(r => r.CodigoRutaRegistroTipo).IsUnique();

        modelBuilder.Entity<BodegaArticulo>()
            .HasIndex(b => new { b.CodigoBodega, b.CodigoArticulo }).IsUnique();

        modelBuilder.Entity<BodegaExistencia>()
            .HasIndex(b => new { b.CodigoBodega, b.CodigoArticulo }).IsUnique();

        modelBuilder.Entity<ListaPrecio>().HasIndex(l => l.CodigoListaPrecio).IsUnique();

        modelBuilder.Entity<ListaPrecioNivel>()
            .HasIndex(l => new { l.CodigoListaPrecio, l.CodigoListaPrecioNivel }).IsUnique();

        modelBuilder.Entity<ListaPrecioNivelDetalle>()
            .HasIndex(l => new { l.CodigoListaPrecio, l.CodigoListaPrecioNivel, l.CodigoArticulo }).IsUnique();

        modelBuilder.Entity<ListaPrecioRuta>()
            .HasIndex(l => new { l.CodigoListaPrecio, l.CodigoListaPrecioNivel, l.CodigoRuta }).IsUnique();

        modelBuilder.Entity<ListaPrecioCliente>()
            .HasIndex(l => new { l.CodigoCliente, l.CodigoListaPrecio, l.CodigoListaPrecioNivel }).IsUnique();

        modelBuilder.Entity<PrecioEspecial>()
            .HasIndex(p => new { p.CodigoCliente, p.CodigoArticulo }).IsUnique();

        modelBuilder.Entity<Promocion>().HasIndex(p => p.CodigoPromocion).IsUnique();

        modelBuilder.Entity<PromocionDetalle>()
            .HasIndex(p => new { p.CodigoPromocion, p.CodigoArticulo }).IsUnique();

        modelBuilder.Entity<PromocionCanal>()
            .HasIndex(p => new { p.CodigoPromocion, p.CodigoCanal }).IsUnique();

        modelBuilder.Entity<PromocionCliente>()
            .HasIndex(p => new { p.CodigoPromocion, p.CodigoCliente }).IsUnique();

        modelBuilder.Entity<PromocionRegion>()
            .HasIndex(p => new { p.CodigoPromocion, p.CodigoRegion }).IsUnique();

        modelBuilder.Entity<PromocionRuta>()
            .HasIndex(p => new { p.CodigoPromocion, p.CodigoRuta }).IsUnique();

        modelBuilder.Entity<PromocionGrupo>().HasIndex(p => p.CodigoPromocionGrupo).IsUnique();

        modelBuilder.Entity<PromocionGrupoDetalle>()
            .HasIndex(p => new { p.CodigoPromocionGrupo, p.CodigoArticulo }).IsUnique();

        modelBuilder.Entity<PromocionGrupoPromocion>()
            .HasIndex(p => new { p.CodigoPromocionGrupo, p.CodigoPromocion }).IsUnique();

        modelBuilder.Entity<TipoPromocion>()
            .HasIndex(t => new { t.CodigoTipo, t.CodigoPromocion }).IsUnique();

        modelBuilder.Entity<Impuesto>().HasIndex(i => i.CodigoImpuesto).IsUnique();

        modelBuilder.Entity<ImpuestoDetalle>()
            .HasIndex(i => new { i.CodigoImpuesto, i.ImpuestoDetalleId }).IsUnique();

        modelBuilder.Entity<Pais>().HasIndex(p => p.CodigoPais).IsUnique();

        modelBuilder.Entity<Departamento>().HasIndex(d => d.CodigoDepartamento).IsUnique();

        modelBuilder.Entity<Municipio>().HasIndex(m => m.CodigoMunicipio).IsUnique();

        modelBuilder.Entity<Banco>().HasIndex(b => b.CodigoBanco).IsUnique();

        modelBuilder.Entity<CuentaBancaria>().HasIndex(c => c.CuentaBancariaId).IsUnique();

        modelBuilder.Entity<Folio>().HasKey(f => f.CodigoRuta);

        modelBuilder.Entity<ControlSinc>()
            .HasIndex(c => new { c.NombreTabla, c.CodigoUsuario }).IsUnique();

        modelBuilder.Entity<Destinatario>().HasIndex(d => d.CodigoVendedor).IsUnique();

        modelBuilder.Entity<Cuota>().HasIndex(c => c.Categoria).IsUnique();

        modelBuilder.Entity<BackOrder>()
            .HasIndex(b => new { b.PedidoId, b.Producto }).IsUnique();

        modelBuilder.Entity<Documento>().HasIndex(d => d.DocumentoId).IsUnique();

        modelBuilder.Entity<Negociacion>()
            .HasIndex(n => new { n.Llave, n.CodigoCliente, n.CodigoArticulo, n.FechaVigencia }).IsUnique();
    }

    public static string GetDbPath()
    {
        var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        return Path.Combine(folder, "HoneywellApp", "app.db");
    }
}
