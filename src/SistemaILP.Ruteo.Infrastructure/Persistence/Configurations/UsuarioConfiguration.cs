using SistemaILP.Ruteo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SistemaILP.Ruteo.Infrastructure.Persistence.Configurations;

/// <summary>
/// Mapea exactamente la tabla "usuario" de la app Android existente
/// (DataBaseContract.UsuarioEntry) - mismos nombres de tabla y columnas,
/// para que la base de datos migrada sea compatible con la real.
/// </summary>
public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("usuario");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnName("_id");

        builder.Property(u => u.AsCodigoUsuario).HasColumnName("asCodigoUsuario").IsRequired();
        builder.Property(u => u.Vendedor).HasColumnName("vendedor").IsRequired();
        builder.Property(u => u.Nombre).HasColumnName("nombre").IsRequired();
        builder.Property(u => u.PassW).HasColumnName("passW").IsRequired();
        builder.Property(u => u.SesionActiva).HasColumnName("sesionActiva").IsRequired();
        builder.Property(u => u.UltimaSincronizacion).HasColumnName("ultimaSincronizacion").IsRequired();

        builder.HasIndex(u => u.AsCodigoUsuario).IsUnique();
        builder.HasIndex(u => u.Vendedor).IsUnique();
    }
}
