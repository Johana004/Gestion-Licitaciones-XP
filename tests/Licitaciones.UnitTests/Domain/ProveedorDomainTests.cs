using Licitaciones.Domain.Entities;
using Xunit;

namespace Licitaciones.UnitTests.Domain;

public class ProveedorDomainTests
{
    [Theory]
    [InlineData("Empresa Constructora S.A.")]
    [InlineData("Tech Solutions CR")]
    [InlineData("Proveedor 123, Ltda.")]
    public void Proveedor_NombreValido_DebeCrearseCorrectamente(string nombreValido)
    {
        // Arrange
        var fechaActual = DateTimeOffset.UtcNow;

        // Act
        var proveedor = new Proveedor(nombreValido, fechaActual);

        // Assert
        Assert.NotNull(proveedor);
        Assert.Equal(nombreValido, proveedor.Nombre);
    }

    [Theory]
    [InlineData("Empresa <Script>")]
    [InlineData("Proveedor #1 -- DROP TABLE")]
    public void Proveedor_NombreConCaracteresInvalidos_DebeLanzarExcepcion(string nombreInvalido)
    {
        // Arrange
        var fechaActual = DateTimeOffset.UtcNow;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
            new Proveedor(nombreInvalido, fechaActual));
    }
}