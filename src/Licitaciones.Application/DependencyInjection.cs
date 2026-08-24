using Licitaciones.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Licitaciones.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IProveedorService, ProveedorService>();
        return services;
    }
}