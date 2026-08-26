using Licitaciones.Domain.Entities;
using Xunit;

namespace Licitaciones.UnitTests.Domain;

public class TipoCambioTests
{
    [Fact]
    public void CrearTipoCambio_ConValorValido_DebeCrearInactivo()
    {
        var tipoCambio = new TipoCambio(500m, DateTimeOffset.UtcNow);

        Assert.False(tipoCambio.Activo);
        Assert.Equal(500m, tipoCambio.CRCporUSD);
    }

    [Fact]
    public void CrearTipoCambio_ConValorCeroONegativo_DebeLanzarExcepcion()
    {
        Assert.Throws<ArgumentException>(() => new TipoCambio(0m, DateTimeOffset.UtcNow));
    }
}