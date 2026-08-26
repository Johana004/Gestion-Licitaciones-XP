using Licitaciones.Domain.Entities;
using Xunit;
using Licitaciones.Domain.Enums;

namespace Licitaciones.UnitTests.Domain;

public class LicitacionTests
{
    private readonly DateTimeOffset _fechaBase = new DateTimeOffset(2026, 8, 24, 10, 0, 0, TimeSpan.FromHours(-6));

    [Fact]
    public void CrearLicitacion_ConDatosValidos_DebeCrearEnEstadoBorrador()
    {
        // Arrange
        var fechaCierre = _fechaBase.AddDays(5);

        // Act
        var licitacion = new Licitacion("LIC-001", "Licitación de Equipos", 5000000m, fechaCierre, _fechaBase);

        // Assert
        Assert.Equal(EstadoLicitacion.Borrador, licitacion.Estado);
        Assert.Equal("LIC-001", licitacion.Codigo);
        Assert.Equal("LIC-001", licitacion.CodigoNormalizado);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    public void CrearLicitacion_ConPresupuestoInvalido_DebeLanzarExcepcion(decimal presupuestoInvalido)
    {
        // Arrange
        var fechaCierre = _fechaBase.AddDays(5);

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            new Licitacion("LIC-001", "Licitación Invalida", presupuestoInvalido, fechaCierre, _fechaBase));
    }

    [Fact]
    public void Publicar_EstadoBorradorYFechaFutura_DebeCambiarAEstadoPublicada()
    {
        // Arrange
        var licitacion = new Licitacion("LIC-001", "Licitación", 1000000m, _fechaBase.AddDays(2), _fechaBase);

        // Act
        licitacion.Publicar(_fechaBase);

        // Assert
        Assert.Equal(EstadoLicitacion.Publicada, licitacion.Estado);
    }

    [Fact]
    public void Transicion_PublicadaABorrador_NoDebeSerPermitida()
    {
        // Arrange
        var licitacion = new Licitacion("LIC-001", "Licitación", 1000000m, _fechaBase.AddDays(2), _fechaBase);
        licitacion.Publicar(_fechaBase);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => licitacion.Publicar(_fechaBase));
    }
}