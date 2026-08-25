namespace Licitaciones.Application.DTOs;

public record CrearOfertaDto(
    Guid LicitacionId,
    Guid ProveedorId,
    decimal MontoOfertaCRC
);

public record OfertaResponseDto(
    Guid Id,
    Guid LicitacionId,
    Guid ProveedorId,
    decimal MontoOfertaCRC,
    DateTimeOffset FechaPresentacion
);