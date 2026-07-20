using Licitaciones.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Licitaciones.Infrastructure.Persistence;

public class LicitacionesDbContext : DbContext
{
    public DbSet<Proveedor> Proveedores => Set<Proveedor>();
    public DbSet<TipoCambio> TiposCambio => Set<TipoCambio>();

    public LicitacionesDbContext(DbContextOptions<LicitacionesDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Aplica automáticamente todas las configuraciones que implementen IEntityTypeConfiguration en este proyecto
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}