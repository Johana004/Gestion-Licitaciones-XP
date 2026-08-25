using Licitaciones.Application.DTOs;
using Licitaciones.Domain.Entities;
using Licitaciones.Domain.Repositories;

namespace Licitaciones.Application.Services;

public class LicitacionService
{
    private readonly ILicitacionRepository _repository;

    public LicitacionService(ILicitacionRepository repository)
    {
        _repository = repository;
    }

    public async Task<LicitacionResponseDto> CrearAsync(CrearLicitacionDto dto, CancellationToken cancellationToken = default)
    {
        var codigoNormalizado = dto.Codigo.Trim().ToUpperInvariant();
        var existente = await _repository.GetByCodigoNormalizadoAsync(codigoNormalizado, cancellationToken);
        
        if (existente != null)
            throw new InvalidOperationException($"Ya existe una licitación con el código '{dto.Codigo}'.");

        var licitacion = new Licitacion(
            dto.Codigo,
            dto.Titulo,
            dto.PresupuestoEstimadoCRC,
            dto.FechaCierre,
            DateTimeOffset.UtcNow
        );

        await _repository.AddAsync(licitacion, cancellationToken);

        return MapToDto(licitacion);
    }

    public async Task<IEnumerable<LicitacionResponseDto>> ObtenerTodasAsync(CancellationToken cancellationToken = default)
    {
        var licitaciones = await _repository.GetAllAsync(cancellationToken);
        return licitaciones.Select(MapToDto);
    }

    private static LicitacionResponseDto MapToDto(Licitacion l) =>
        new(l.Id, l.Codigo, l.Titulo, l.Estado.ToString(), l.PresupuestoEstimadoCRC, l.FechaCierre, l.CreatedAt);

    public async Task<LicitacionResponseDto> PublicarAsync(Guid id, CancellationToken cancellationToken = default)
{
    var licitacion = await _repository.GetByIdAsync(id, cancellationToken)
        ?? throw new KeyNotFoundException($"No se encontró la licitación con ID {id}.");

    licitacion.Publicar(DateTimeOffset.UtcNow);
    await _repository.UpdateAsync(licitacion, cancellationToken);

    return MapToDto(licitacion);
}

public async Task<LicitacionResponseDto> CerrarAsync(Guid id, CancellationToken cancellationToken = default)
{
    var licitacion = await _repository.GetByIdAsync(id, cancellationToken)
        ?? throw new KeyNotFoundException($"No se encontró la licitación con ID {id}.");

    licitacion.Cerrar(DateTimeOffset.UtcNow);
    await _repository.UpdateAsync(licitacion, cancellationToken);

    return MapToDto(licitacion);
}





}