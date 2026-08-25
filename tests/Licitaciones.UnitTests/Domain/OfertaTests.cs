using Licitaciones.Domain.Entities;
using Xunit;

namespace Licitaciones.UnitTests.Domain;

public class OfertaTests
{
    private readonly DateTimeOffset _fechaBase = new DateTimeOffset(2026, 8, 24, 10, 0, 0, TimeSpan.FromHours(-6));

    [Fact]
    public void CrearOferta_ConDatosValidos_DebeCrearExitosamente()
    {
        // Arrange
        var licitacionId = Guid.NewGuid();
        var proveedorId = Guid.NewGuid();
        var monto = 12000000m;

        // Act
        var oferta = new Oferta(licitacionId, proveedorId, monto, _fechaBase);

        // Assert
        Assert.NotEqual(Guid.Empty, oferta.Id);
        Assert.Equal(licitacionId, oferta.LicitacionId);
        Assert.Equal(proveedorId, oferta.ProveedorId);
        Assert.Equal(monto, oferta.MontoOfertaCRC);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-500)]
    public void CrearOferta_ConMontoInvalido_DebeLanzarExcepcion(decimal montoInvalido)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            new Oferta(Guid.NewGuid(), Guid.NewGuid(), montoInvalido, _fechaBase));
    }
}