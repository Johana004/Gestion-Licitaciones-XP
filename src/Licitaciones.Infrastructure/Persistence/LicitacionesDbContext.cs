using Licitaciones.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Licitaciones.Infrastructure.Persistence;

public class LicitacionesDbContext : DbContext
{
    public DbSet<Proveedor> Proveedores => Set<Proveedor>();
    public DbSet<TipoCambio> TiposCambio => Set<TipoCambio>();

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
    }
}



