using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Diagnostics.CodeAnalysis;

namespace Licitaciones.Infrastructure.Persistence;

[ExcludeFromCodeCoverage]
public class LicitacionesDbContextFactory : IDesignTimeDbContextFactory<LicitacionesDbContext>
{
    public LicitacionesDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<LicitacionesDbContext>();
        
        // Cadena de conexión ficticia para tiempo de diseño / generación de código
        optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=LicitacionesDb;Username=postgres;Password=postgres");

        return new LicitacionesDbContext(optionsBuilder.Options);
    }
}