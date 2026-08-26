using Licitaciones.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;
namespace Licitaciones.Infrastructure.Persistence;

[ExcludeFromCodeCoverage]
public class LicitacionesDbContext : DbContext
{
    public DbSet<Proveedor> Proveedores => Set<Proveedor>();
    public DbSet<TipoCambio> TiposCambio => Set<TipoCambio>();
    public DbSet<Licitacion> Licitaciones => Set<Licitacion>();
    public DbSet<Oferta> Ofertas => Set<Oferta>();
    public DbSet<NivelAprobacion> NivelesAprobacion => Set<NivelAprobacion>();

    public LicitacionesDbContext(DbContextOptions<LicitacionesDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        
        // Ignora la advertencia de cambios pendientes para permitir el update fluido en .NET 9
        optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
    }   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Aplica automáticamente todas las configuraciones que implementen IEntityTypeConfiguration en este proyecto
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Mapeo explícito para la entidad Licitación
        modelBuilder.Entity<Licitacion>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Codigo).IsRequired().HasMaxLength(50);
            entity.Property(e => e.CodigoNormalizado).IsRequired().HasMaxLength(50);
            entity.HasIndex(e => e.CodigoNormalizado).IsUnique(); // Unicidad por código normalizado
            entity.Property(e => e.Titulo).IsRequired().HasMaxLength(200);
            entity.Property(e => e.PresupuestoEstimadoCRC).HasColumnType("decimal(18,2)");
            
            // Concurrencia optimista para PostgreSQL
            entity.Property(e => e.VersionConcurrencia).IsConcurrencyToken();
        });

        // Mapeo explícito para la entidad Oferta
        modelBuilder.Entity<Oferta>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.LicitacionId, e.ProveedorId }).IsUnique(); // Unicidad por proveedor/licitación
            entity.Property(e => e.MontoOfertadoCRC).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Version).IsConcurrencyToken();
        });
    }
}