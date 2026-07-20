using Licitaciones.Domain.Entities;
using Xunit;

namespace Licitaciones.UnitTests.Domain;

public class TipoCambioTests
{
    [Fact]
    public void CrearTipoCambio_ConValorValido_DebeCrearInactivo()
    {
        var tipoCambio = new TipoCambio(500m, DateTime.Now);

        Assert.False(tipoCambio.Activo);
        Assert.Equal(500m, tipoCambio.Valor);
    }

    [Fact]
    public void CrearTipoCambio_ConValorCeroONegativo_DebeLanzarExcepcion()
    {
        Assert.Throws<ArgumentException>(() => new TipoCambio(0, DateTime.Now));
    }
}