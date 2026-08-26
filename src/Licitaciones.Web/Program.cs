using Scalar.AspNetCore;
using Microsoft.AspNetCore.OpenApi;


var builder = WebApplication.CreateBuilder(args);

// 1. Agregar servicios MVC / Controladores
builder.Services.AddControllersWithViews();

// 2. Agregar soporte para OpenAPI / Swagger
builder.Services.AddOpenApi();

var app = builder.Build();

// Configuración del pipeline de solicitudes HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// 3. Mapear los endpoints de OpenAPI y la interfaz de Scalar (solo en desarrollo o global)
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // Expone /openapi/v1.json
    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("Gestión Licitaciones API")
            .WithTheme(ScalarTheme.Purple)
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    }); // Expone /scalar/v1
}

// 4. Ruta por defecto para el portal MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Licitaciones}/{action=Index}/{id?}");

app.Run();

public partial class Program { }