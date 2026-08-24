namespace Licitaciones.Application.DTOs;

public record ProveedorDto(Guid Id, string Nombre);

public record CrearProveedorDto(string Nombre);