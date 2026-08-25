using System;

namespace Licitaciones.Application.DTOs;

// Nivel de Aprobaciones
public record CrearNivelAprobacionDto(decimal MontoMinimoCRC, decimal? MontoMaximoCRC, string Aprobador);
public record NivelAprobacionResponseDto(Guid Id, decimal MontoMinimoCRC, decimal? MontoMaximoCRC, string Aprobador);

// Tipo de Cambio
public record CrearTipoCambioDto(decimal CRCPorUSD, DateTimeOffset FechaVigencia);
public record TipoCambioResponseDto(Guid Id, decimal CRCPorUSD, DateTimeOffset FechaVigencia, bool Activo);

// Mejor Oferta y Clasificación de Ahorro
public record MejorOfertaResponseDto(
    Guid LicitacionId,
    string CodigoLicitacion,
    decimal PresupuestoCRC,
    Guid? OfertaId,
    Guid? ProveedorId,
    decimal? MontoOfertaCRC,
    decimal? MontoOfertaUSD,
    decimal? PorcentajeAhorro,
    string ClasificacionAhorro,
    string AprobadorRequerido,
    decimal TipoCambioUtilizado
);