using Licitaciones.Domain.Entities;
using Xunit;

namespace Licitaciones.UnitTests.Domain;

public class OfertaTests
{
    private readonly DateTimeOffset _fechaBase =
        new(2026, 8, 24, 10, 0, 0, TimeSpan.FromHours(-6));

    private Licitacion CrearLicitacionPublicada()
    {
        var licitacion = new Licitacion(
            "LIC-001",
            "Licitación de prueba",
            12_000_000m,
            _fechaBase.AddDays(1),
            _fechaBase);

        licitacion.Publicar(_fechaBase);
        return licitacion;
    }

    [Fact]
    public void CrearOferta_ConDatosValidos_DebeCrearExitosamente()
    {
        var licitacion = CrearLicitacionPublicada();
        var proveedorId = Guid.NewGuid();
        var monto = 12_000_000m;

        var oferta = new Oferta(licitacion, proveedorId, monto, _fechaBase);

        Assert.NotEqual(Guid.Empty, oferta.Id);
        Assert.Equal(licitacion.Id, oferta.LicitacionId);
        Assert.Equal(proveedorId, oferta.ProveedorId);
        Assert.Equal(monto, oferta.MontoOfertadoCRC);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-500)]
    public void CrearOferta_ConMontoInvalido_DebeLanzarExcepcion(decimal montoInvalido)
    {
        var licitacion = CrearLicitacionPublicada();

        Assert.Throws<ArgumentException>(() =>
            new Oferta(licitacion, Guid.NewGuid(), montoInvalido, _fechaBase));
    }
}