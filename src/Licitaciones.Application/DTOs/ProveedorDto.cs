namespace Licitaciones.Application.DTOs;

public record CrearProveedorDto(string Nombre);
public record ProveedorResponseDto(Guid Id, string Nombre, DateTimeOffset CreatedAt);