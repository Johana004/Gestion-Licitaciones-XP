using Licitaciones.Domain.Repositories;
using Licitaciones.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Licitaciones.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IProveedorRepository, ProveedorRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>(); // Ambas 'W' en mayúscula

        return services;
    }
}