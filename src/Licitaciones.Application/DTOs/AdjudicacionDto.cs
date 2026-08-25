namespace Licitaciones.Application.DTOs;

public record AdjudicarLicitacionDto(
    Guid OfertaGanadoraId
);

public record LicitacionAdjudicadaResponseDto(
    Guid LicitacionId,
    string Codigo,
    string Estado,
    Guid OfertaGanadoraId,
    decimal MontoGanadorCRC
);