using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Licitaciones.FunctionalTests;

public class ProveedoresControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ProveedoresControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Index_DevuelveRespuestaExitosaYVistaHtml()
    {
        // Act
        var response = await _client.GetAsync("/Proveedores");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType?.ToString());
    }

    [Fact]
    public async Task Crear_VistaGet_DevuelveFormularioExitosamente()
    {
        // Act
        var response = await _client.GetAsync("/Proveedores/Crear");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
public async Task Crear_PostValido_RedireccionaAIndex()
{
    // 1. Datos codificados como formulario URL-encoded
    var formData = new Dictionary<string, string>
    {
        { "CedulaJuridica", "3-101-123456" },
        { "NombreRazonSocial", "Empresa Ejemplo S.A." },
        { "EmailContacto", "contacto@ejemplo.com" },
        { "Telefono", "2460-1234" }
    };

    var content = new FormUrlEncodedContent(formData);

    // 2. Ejecutar la petición POST
    var response = await _client.PostAsync("/Proveedores/Crear", content);

    // 3. Verificación
    Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
    Assert.Equal("/Proveedores", response.Headers.Location?.OriginalString);
}
}