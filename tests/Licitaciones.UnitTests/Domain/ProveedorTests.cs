using Xunit;
using Licitaciones.Domain.Entities; // Nota: Esto va a dar error de compilación porque la entidad no existe aún.

namespace Licitaciones.UnitTests.Domain;

public class ProveedorTests
{
    [Fact]
    public void CrearProveedor_DebeNormalizarNombre_EliminandoEspaciosExtremosYRepetidos()
    {
        // Arrange (Preparar)
        string nombreConEspacios = "   Empresa    Central   ";
        string nombreEsperadoNormalizado = "Empresa Central";

        // Act (Actuar)
        var proveedor = new Proveedor(nombreConEspacios);

        // Assert (Afirmar)
        Assert.Equal(nombreEsperadoNormalizado, proveedor.Nombre);
    }

    [Fact]
    public void CrearProveedor_ConCaracteresEspecialesNoPermitidos_DebeLanzarArgumentException()
    {
        // Arrange
        string nombreInvalido = "Empresa@Central#";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Proveedor(nombreInvalido));
    }
}