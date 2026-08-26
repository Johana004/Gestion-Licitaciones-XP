using System.Net;
using System.Net.Http.Json;
using Licitaciones.Domain.Entities;
using Licitaciones.IntegrationTests.Infrastructure;
using Xunit;

namespace Licitaciones.IntegrationTests.Controllers;

public class ProveedoresControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ProveedoresControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ObtenerProveedores_DebeRetornarCodigo200OK()
    {
        // Act
        var response = await _client.GetAsync("/api/proveedores");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}