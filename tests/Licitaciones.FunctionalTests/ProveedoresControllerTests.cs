using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Licitaciones.Web;

namespace Licitaciones.FunctionalTests;

public class ProveedoresControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ProveedoresControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Index_DevuelveRespuestaExitosaYVistaHtml()
    {
        // Act
        var response = await _client.GetAsync("/Proveedores");

        // Assert
        response.EnsureSuccessStatusCode(); // Código HTTP 200 OK
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
    // Arrange
    var postData = new Dictionary<string, string>
    {
        { "Nombre", "Proveedor Test S.A." }
    };

    var content = new FormUrlEncodedContent(postData);

    // Act
    var response = await _client.PostAsync("/Proveedores/Crear", content);

    // Si no fue exitoso, lee el HTML/JSON devuelto para ver los errores de validación
    if (response.StatusCode != HttpStatusCode.Redirect)
    {
        var errorContent = await response.Content.ReadAsStringAsync();
        throw new Exception($"Falló con estado {response.StatusCode}. Contenido de respuesta:\n{errorContent}");
    }

    // Assert
    Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
    Assert.Equal("/Proveedores", response.Headers.Location?.OriginalString);
}
}