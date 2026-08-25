using System.Text.RegularExpressions;

namespace Licitaciones.Domain.Entities;

public class Proveedor
{
    private static readonly Regex RegexNombreValido = new(@"^[\p{L}\p{N}\s.,()]+$", RegexOptions.Compiled);

    public Guid Id { get; private set; }
    public string Nombre { get; private set; } = null!;
    public string NombreNormalizado { get; private set; } = null!;
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }
    public uint VersionConcurrencia { get; private set; }

    private Proveedor() { } // Constructor privado para EF Core

    public Proveedor(string nombre, DateTimeOffset fechaActual)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            throw new ArgumentException("El nombre del proveedor no puede estar vacío.");
        }

        var nombreLimpio = NormalizarNombre(nombre);

        if (!RegexNombreValido.IsMatch(nombreLimpio))
        {
            throw new ArgumentException("El nombre contiene caracteres especiales no permitidos. Solo se admiten letras, números, espacios, puntos, comas y paréntesis.");
        }

        Id = Guid.NewGuid();
        Nombre = nombreLimpio;
        NombreNormalizado = nombreLimpio.ToUpperInvariant();
        CreatedAt = fechaActual;
        UpdatedAt = fechaActual;
    }

    public static string NormalizarNombre(string texto)
    {
        if (string.IsNullOrWhiteSpace(texto)) return string.Empty;
        
        string resultado = texto.Trim();
        resultado = Regex.Replace(resultado, @"\s+", " ");
        return resultado.Normalize();
    }
}