using Licitaciones.Application.DTOs;
using Licitaciones.Domain.Repositories;

namespace Licitaciones.Application.Services;

public class MejorOfertaService
{
    private readonly ILicitacionRepository _licitacionRepository;
    private readonly IOfertaRepository _ofertaRepository;
    private readonly INivelAprobacionRepository _nivelAprobacionRepository;
    private readonly ITipoCambioRepository _tipoCambioRepository;

    public MejorOfertaService(
        ILicitacionRepository licitacionRepository,
        IOfertaRepository ofertaRepository,
        INivelAprobacionRepository nivelAprobacionRepository,
        ITipoCambioRepository tipoCambioRepository)
    {
        _licitacionRepository = licitacionRepository;
        _ofertaRepository = ofertaRepository;
        _nivelAprobacionRepository = nivelAprobacionRepository;
        _tipoCambioRepository = tipoCambioRepository;
    }

    public async Task<MejorOfertaResponseDto> ObtenerMejorOfertaAsync(Guid licitacionId, CancellationToken cancellationToken = default)
    {
        var licitacion = await _licitacionRepository.GetByIdAsync(licitacionId, cancellationToken)
            ?? throw new KeyNotFoundException($"No se encontró la licitación con ID {licitacionId}.");

        var tc = await _tipoCambioRepository.GetActivoAsync(cancellationToken);
        
        // Uso de la propiedad 'Monto' de la entidad TipoCambio
        decimal tipoCambio = tc?.CRCporUSD ?? 500.00m;
        var ofertas = (await _ofertaRepository.GetByLicitacionIdAsync(licitacionId, cancellationToken)).ToList();

        if (!ofertas.Any())
        {
            var aprobadorSinOferta = await _nivelAprobacionRepository.GetAprobadorParaMontoAsync(licitacion.PresupuestoEstimadoCRC, cancellationToken);
            return new MejorOfertaResponseDto(
                licitacion.Id,
                licitacion.Codigo,
                licitacion.PresupuestoEstimadoCRC,
                null, null, null, null, null,
                "Sin ofertas válidas",
                aprobadorSinOferta?.Aprobador ?? "Sin asignar",
                tipoCambio
            );
        }

        var mejorOferta = ofertas
            .OrderBy(o => o.MontoOfertadoCRC)
            .ThenBy(o => o.FechaPresentacion)
            .First();

        decimal presupuesto = licitacion.PresupuestoEstimadoCRC;
        decimal montoOferta = mejorOferta.MontoOfertadoCRC;

        // Porcentaje de ahorro = ((Presupuesto CRC - Mejor oferta CRC) / Presupuesto CRC) * 100
        decimal porcentajeAhorro = Math.Round(((presupuesto - montoOferta) / presupuesto) * 100, 2);

        // Clasificación de ahorro
        string clasificacion;
        if (porcentajeAhorro >= 10m)
            clasificacion = "Oferta conveniente";
        else if (porcentajeAhorro > 0m)
            clasificacion = "Oferta aceptable";
        else
            clasificacion = "Oferta válida sin ahorro";

        // Aprobador
        var nivelAprobacion = await _nivelAprobacionRepository.GetAprobadorParaMontoAsync(montoOferta, cancellationToken);

        return new MejorOfertaResponseDto(
            licitacion.Id,
            licitacion.Codigo,
            licitacion.PresupuestoEstimadoCRC,
            mejorOferta.Id,
            mejorOferta.ProveedorId,
            montoOferta,
            Math.Round(montoOferta / tipoCambio, 2),
            porcentajeAhorro,
            clasificacion,
            nivelAprobacion?.Aprobador ?? "Gerencia",
            tipoCambio
        );
    }
}