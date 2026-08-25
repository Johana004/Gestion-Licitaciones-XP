using Licitaciones.Application;
using Licitaciones.Infrastructure;
using Scalar.AspNetCore; // <--- Agregar este using

var builder = WebApplication.CreateBuilder(args);

// 1. Registrar servicios de las capas
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

// 2. Registrar controladores y la API nativa de OpenAPI
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// 3. Configurar OpenAPI y la interfaz visual en desarrollo
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(); // <--- Expone la UI gráfica en /scalar/v1
}

app.UseHttpsRedirection();

// 4. Mapear las rutas de los controladores
app.MapControllers();

app.Run();