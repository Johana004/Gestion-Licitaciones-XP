using System.Text.RegularExpressions;

namespace Licitaciones.Domain.Entities;

public class Proveedor
{
    public Guid Id { get; private set; }
    public string Nombre { get; private set; }

    public Proveedor(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            throw new ArgumentException("El nombre del proveedor no puede estar vacío.");
        }

        // 1. Validar caracteres permitidos: letras, números, espacios, puntos, comas y paréntesis.
        if (!Regex.IsMatch(nombre, @"^[a-zA-Z0-9 áéíóúÁÉÍÓÚñÑ.,()]+$"))
        {
            throw new ArgumentException("El nombre contiene caracteres especiales no permitidos.");
        }

        // 2. Normalizar el nombre (Eliminar espacios al inicio/final y reducir espacios internos repetidos)
        Nombre = NormalizarNombre(nombre);
        Id = Guid.NewGuid();
    }

    private string NormalizarNombre(string texto)
    {
        // Elimina espacios extras en los bordes
        string resultado = texto.Trim();
        
        // Reemplaza múltiples espacios internos por uno solo
        resultado = Regex.Replace(resultado, @"\s+", " ");
        
        return resultado;
    }
}