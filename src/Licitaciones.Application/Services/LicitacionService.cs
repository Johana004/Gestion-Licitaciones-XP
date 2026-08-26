using Licitaciones.Application.DTOs;
using Licitaciones.Domain.Entities;
using Licitaciones.Domain.Repositories;

namespace Licitaciones.Application.Services;

public class LicitacionService
{
    private readonly ILicitacionRepository _licitacionRepository;
    private readonly IOfertaRepository _ofertaRepository;

    public LicitacionService(ILicitacionRepository licitacionRepository, IOfertaRepository ofertaRepository)
    {
        _licitacionRepository = licitacionRepository;
        _ofertaRepository = ofertaRepository;
    }

    public async Task<LicitacionResponseDto> CrearAsync(CrearLicitacionDto dto, CancellationToken cancellationToken = default)
    {
        var codigoNormalizado = dto.Codigo.Trim().ToUpperInvariant();
        var existente = await _licitacionRepository.GetByCodigoNormalizadoAsync(codigoNormalizado, cancellationToken);
        
        if (existente != null)
            throw new InvalidOperationException($"Ya existe una licitación con el código '{dto.Codigo}'.");

        var licitacion = new Licitacion(
            dto.Codigo,
            dto.Titulo,
            dto.PresupuestoEstimadoCRC,
            dto.FechaCierre,
            DateTimeOffset.UtcNow
        );

        await _licitacionRepository.AddAsync(licitacion, cancellationToken);

        return MapToDto(licitacion);
    }

    public async Task<IEnumerable<LicitacionResponseDto>> ObtenerTodasAsync(CancellationToken cancellationToken = default)
    {
        var licitaciones = await _licitacionRepository.GetAllAsync(cancellationToken);
        return licitaciones.Select(MapToDto);
    }

    public async Task<LicitacionResponseDto> PublicarAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var licitacion = await _licitacionRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new KeyNotFoundException($"No se encontró la licitación con ID {id}.");

        licitacion.Publicar(DateTimeOffset.UtcNow);
        await _licitacionRepository.UpdateAsync(licitacion, cancellationToken);

        return MapToDto(licitacion);
    }

    public async Task<LicitacionResponseDto> CerrarAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var licitacion = await _licitacionRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new KeyNotFoundException($"No se encontró la licitación con ID {id}.");

        licitacion.Cerrar(DateTimeOffset.UtcNow);
        await _licitacionRepository.UpdateAsync(licitacion, cancellationToken);

        return MapToDto(licitacion);
    }

    public async Task<LicitacionAdjudicadaResponseDto> AdjudicarLicitacionAsync(Guid licitacionId, Guid ofertaGanadoraId, CancellationToken cancellationToken = default)
    {
        var licitacion = await _licitacionRepository.GetByIdAsync(licitacionId, cancellationToken)
            ?? throw new KeyNotFoundException("La licitación no existe.");

        var ofertas = await _ofertaRepository.GetByLicitacionIdAsync(licitacionId, cancellationToken);
        var ofertaGanadora = ofertas.FirstOrDefault(o => o.Id == ofertaGanadoraId)
            ?? throw new KeyNotFoundException("La oferta seleccionada no pertenece a esta licitación o no existe.");

        licitacion.Adjudicar(ofertaGanadora.Id, DateTimeOffset.UtcNow);

        await _licitacionRepository.UpdateAsync(licitacion, cancellationToken);

        return new LicitacionAdjudicadaResponseDto(
            licitacion.Id,
            licitacion.Codigo,
            licitacion.Estado.ToString(),
            ofertaGanadora.Id,
            ofertaGanadora.MontoOfertadoCRC
        );
    }

    private static LicitacionResponseDto MapToDto(Licitacion l) =>
        new(l.Id, l.Codigo, l.Titulo, l.Estado.ToString(), l.PresupuestoEstimadoCRC, l.FechaCierre, l.CreatedAt);
}