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
            new NivelAprobacion(0.01m, 999999.99m, "Encargado de área", now),
            new NivelAprobacion(1000000.00m, 9999999.99m, "Gerencia", now),
            new NivelAprobacion(10000000.00m, null, "Junta Directiva", now)
        );
        await context.SaveChangesAsync();
    }

    if (!await context.TiposCambio.AnyAsync())
    {
        var now = DateTimeOffset.UtcNow;
        context.TiposCambio.Add(new TipoCambio(505.50m, now, true, now));
        await context.SaveChangesAsync();
    }
}

// 4. Configurar OpenAPI y la interfaz visual en desarrollo
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(); // Expone la UI gráfica en /scalar/v1
}

app.UseHttpsRedirection();

// 5. Mapear las rutas de los controladores
app.MapControllers();

app.Run();

public partial class Program { }