namespace Licitaciones.Application.DTOs;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public record CrearProveedorDto(string Nombre);
public record ProveedorResponseDto(Guid Id, string Nombre, DateTimeOffset CreatedAt);