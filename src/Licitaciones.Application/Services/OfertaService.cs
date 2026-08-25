using Licitaciones.Application.DTOs;
using Licitaciones.Domain.Entities;
using Licitaciones.Domain.Repositories;

namespace Licitaciones.Application.Services;

public class OfertaService
{
    private readonly IOfertaRepository _ofertaRepository;
    private readonly ILicitacionRepository _licitacionRepository;
    private readonly IProveedorRepository _proveedorRepository;

    public OfertaService(
        IOfertaRepository ofertaRepository,
        ILicitacionRepository licitacionRepository,
        IProveedorRepository proveedorRepository)
    {
        _ofertaRepository = ofertaRepository;
        _licitacionRepository = licitacionRepository;
        _proveedorRepository = proveedorRepository;
    }

    public async Task<OfertaResponseDto> PresentarOfertaAsync(CrearOfertaDto dto, CancellationToken cancellationToken = default)
    {
        var licitacion = await _licitacionRepository.GetByIdAsync(dto.LicitacionId, cancellationToken)
            ?? throw new KeyNotFoundException("La licitación especificada no existe.");

        if (licitacion.Estado != EstadoLicitacion.Publicada)
            throw new InvalidOperationException("Solo se pueden presentar ofertas a licitaciones en estado 'Publicada'.");

        var proveedor = await _proveedorRepository.GetByIdAsync(dto.ProveedorId, cancellationToken)
            ?? throw new KeyNotFoundException("El proveedor especificado no existe.");

        var ofertaExistente = await _ofertaRepository.GetByLicitacionYProveedorAsync(dto.LicitacionId, dto.ProveedorId, cancellationToken);
        if (ofertaExistente != null)
            throw new InvalidOperationException("El proveedor ya ha presentado una oferta para esta licitación.");

        var oferta = new Oferta(dto.LicitacionId, dto.ProveedorId, dto.MontoOfertaCRC, DateTimeOffset.UtcNow);
        await _ofertaRepository.AddAsync(oferta, cancellationToken);

        return new OfertaResponseDto(oferta.Id, oferta.LicitacionId, oferta.ProveedorId, oferta.MontoOfertaCRC, oferta.FechaPresentacion);
    }
}