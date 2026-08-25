using Licitaciones.Domain.Repositories;
using Licitaciones.Infrastructure.Persistence;
using Licitaciones.Infrastructure.Persistence.Repositories;
using Licitaciones.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Licitaciones.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<LicitacionesDbContext>(options =>
            options.UseNpgsql(connectionString));

        // Registrar Repositorios y UnitOfWork
        services.AddScoped<IProveedorRepository, ProveedorRepository>();
        services.AddScoped<ILicitacionRepository, LicitacionRepository>();
        services.AddScoped<IOfertaRepository, OfertaRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    
}