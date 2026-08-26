namespace Licitaciones.Domain.Services;

using Licitaciones.Domain.Entities;
using Licitaciones.Domain.Enums;

public record MejorOfertaResultado(
    Oferta? Oferta,
    decimal PorcentajeAhorro,
    ClasificacionAhorro? Clasificacion,
    string? AprobadorRequerido
);

public interface ILicitacionDomainService
{
    MejorOfertaResultado CalcularMejorOferta(Licitacion licitacion, IEnumerable<NivelAprobacion> nivelesAprobacion);
}