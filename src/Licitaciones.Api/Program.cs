using Licitaciones.Application;
using Licitaciones.Domain.Entities;
using Licitaciones.Infrastructure;
using Licitaciones.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Registrar servicios de las capas
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

// Registrar servicio de Health Checks para Kubernetes
builder.Services.AddHealthChecks();

// 2. Registrar controladores y la API nativa de OpenAPI
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// 3. Semilla de Niveles de Aprobación y Tipo de Cambio
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<LicitacionesDbContext>(); 
    await context.Database.MigrateAsync();

    if (!await context.NivelesAprobacion.AnyAsync())
    {
        var now = DateTimeOffset.UtcNow;
        context.NivelesAprobacion.AddRange(
            new NivelAprobacion(0m, 5000000m, "Jefatura"),
            new NivelAprobacion(5000000.01m, 20000000m, "Gerencia"),
            new NivelAprobacion(20000000.01m, decimal.MaxValue, "Junta Directiva")
        );
        await context.SaveChangesAsync();
    }

    if (!await context.TiposCambio.AnyAsync())
    {
        var now = DateTimeOffset.UtcNow;
        context.TiposCambio.Add(new TipoCambio(500.00m, now, true));
        await context.SaveChangesAsync();
    }
}

// 4. Configurar OpenAPI y la interfaz visual en desarrollo
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(); // Expone la UI gráfica en /scalar/v1
}

// Endpoint de salud para las probes de Kubernetes (startup, liveness, readiness)
app.MapHealthChecks("/health");

app.UseHttpsRedirection();

// 5. Mapear las rutas de los controladores
app.MapControllers();

app.Run();

public partial class Program { }