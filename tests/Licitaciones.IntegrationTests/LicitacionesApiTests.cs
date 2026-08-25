using System.Net;
using System.Net.Http.Json;
using Licitaciones.Application.DTOs;
using Xunit;

namespace Licitaciones.IntegrationTests;

public class LicitacionesApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public LicitacionesApiTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ObtenerLicitaciones_DeberiaRetornar200OK()
    {
        // Act
        var response = await _client.GetAsync("/api/Licitaciones");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task CrearLicitacion_ConDatosValidos_DeberiaRetornar201Created()
    {
        // Arrange
        var codigoUnico = $"LIC-TEST-{Guid.NewGuid().ToString()[..5]}";
        var dto = new CrearLicitacionDto(
            Codigo: codigoUnico,
            Titulo: "Licitación de Prueba Integración TDD",
            PresupuestoEstimadoCRC: 25000000.00m,
            FechaCierre: DateTimeOffset.UtcNow.AddDays(15)
        );

        // Act
        var response = await _client.PostAsJsonAsync("/api/Licitaciones", dto);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}