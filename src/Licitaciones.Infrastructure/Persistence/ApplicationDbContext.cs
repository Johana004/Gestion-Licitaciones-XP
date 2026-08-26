using Licitaciones.Domain.Entities; // <-- Este using quita el error de las entidades
using Microsoft.EntityFrameworkCore;

namespace Licitaciones.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    // Faltaban estas propiedades DbSet
    public DbSet<Licitacion> Licitaciones => Set<Licitacion>();
    public DbSet<Oferta> Ofertas => Set<Oferta>();
    public DbSet<Proveedor> Proveedores => Set<Proveedor>();
    public DbSet<NivelAprobacion> NivelesAprobacion => Set<NivelAprobacion>();
    public DbSet<TipoCambio> TiposCambio => Set<TipoCambio>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Licitacion
        modelBuilder.Entity<Licitacion>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.CodigoNormalizado).IsUnique();
            entity.Property(e => e.PresupuestoEstimadoCRC).HasColumnType("numeric(18,2)");
            entity.Property(e => e.VersionConcurrencia).IsRowVersion();
        });

        // Oferta
        modelBuilder.Entity<Oferta>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.LicitacionId, e.ProveedorId }).IsUnique();
            entity.Property(e => e.MontoOfertadoCRC).HasColumnType("numeric(18,2)");
        });

        // Proveedor
        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.NombreNormalizado).IsUnique();
            entity.Property(e => e.VersionConcurrencia).IsRowVersion();
        });

        // NivelAprobacion
        modelBuilder.Entity<NivelAprobacion>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.MontoMinimoCRC).HasColumnType("numeric(18,2)");
            entity.Property(e => e.MontoMaximoCRC).HasColumnType("numeric(18,2)");
        });

        // TipoCambio
        modelBuilder.Entity<TipoCambio>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CRCporUSD).HasColumnType("numeric(18,2)");
        });
    }
}