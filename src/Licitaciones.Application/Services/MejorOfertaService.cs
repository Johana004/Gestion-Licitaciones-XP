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
        decimal tipoCambio = tc?.CRCPorUSD ?? 500.00m; // Tasa por defecto si no hay activa

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

        // Regla: Menor monto CRC. En empate, la registrada primero (CreatedAt más antiguo)
        // Regla: Menor monto CRC. En empate, la registrada primero (FechaRegistro más antiguo)
        var mejorOferta = ofertas
    .OrderBy(o => o.MontoOfertaCRC)
    .ThenBy(o => o.FechaPresentacion) // <-- Cambiado de CreatedAt a FechaRegistro
    .First();

        decimal presupuesto = licitacion.PresupuestoEstimadoCRC;
        decimal montoOferta = mejorOferta.MontoOfertaCRC;

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