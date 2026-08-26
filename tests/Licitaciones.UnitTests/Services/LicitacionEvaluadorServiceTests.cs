using Licitaciones.Application.Services;
using Licitaciones.Domain.Entities;
using Xunit;

namespace Licitaciones.UnitTests.Services;

public class LicitacionEvaluadorServiceTests
{
    [Fact]
    public void EvaluarMejorOferta_DebeSeleccionarMenorMonto()
    {
        // Arrange
        var fechaBase = DateTimeOffset.UtcNow;

        var licitacion = new Licitacion(
            "LIC-2026-001",
            "Licitación de Prueba",
            10_000_000m,
            fechaBase.AddDays(5),
            fechaBase);

        licitacion.Publicar(fechaBase);

        var oferta1 = new Oferta(
            licitacion,
            Guid.NewGuid(),
            9_000_000m,
            fechaBase);

        var oferta2 = new Oferta(
            licitacion,
            Guid.NewGuid(),
            8_500_000m,
            fechaBase.AddMinutes(5));

        var ofertas = new List<Oferta> { oferta1, oferta2 };

        // Act
        var (mejorOferta, clasificacion, porcentajeAhorro) = 
            LicitacionEvaluadorService.EvaluarMejorOferta(licitacion, ofertas);

        // Assert
        Assert.NotNull(mejorOferta);
        Assert.Equal(oferta2.Id, mejorOferta!.Id);
        Assert.Equal("Oferta conveniente", clasificacion); // 15% de ahorro (>= 10%)
        Assert.Equal(15m, porcentajeAhorro);
    }

    [Fact]
    public void EvaluarMejorOferta_AnteEmpateDeMonto_DebeSeleccionarLaPrimeraPresentada()
    {
        // Arrange
        var fechaBase = DateTimeOffset.UtcNow;

        var licitacion = new Licitacion(
            "LIC-2026-002",
            "Licitación Desempate",
            10_000_000m,
            fechaBase.AddDays(5),
            fechaBase);

        licitacion.Publicar(fechaBase);

        var primeraOferta = new Oferta(
            licitacion,
            Guid.NewGuid(),
            9_500_000m,
            fechaBase.AddHours(-2));

        var segundaOferta = new Oferta(
            licitacion,
            Guid.NewGuid(),
            9_500_000m,
            fechaBase.AddHours(-1));

        var ofertas = new List<Oferta> { segundaOferta, primeraOferta };

        // Act
        var (mejorOferta, clasificacion, porcentajeAhorro) = 
            LicitacionEvaluadorService.EvaluarMejorOferta(licitacion, ofertas);

        // Assert
        Assert.NotNull(mejorOferta);
        Assert.Equal(primeraOferta.Id, mejorOferta!.Id);
        Assert.Equal("Oferta aceptable", clasificacion); // 5% de ahorro (>0% y <10%)
        Assert.Equal(5m, porcentajeAhorro);
    }
}