namespace Licitaciones.Application.DTOs;

public record CrearLicitacionDto(
    string Codigo,
    string Titulo,
    decimal PresupuestoEstimadoCRC,
    DateTimeOffset FechaCierre
);

public record LicitacionResponseDto(
    Guid Id,
    string Codigo,
    string Titulo,
    string Estado,
    decimal PresupuestoEstimadoCRC,
    DateTimeOffset FechaCierre,
    DateTimeOffset CreatedAt
);