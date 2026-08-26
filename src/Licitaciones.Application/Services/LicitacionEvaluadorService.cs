using Licitaciones.Domain.Entities;

namespace Licitaciones.Application.Services;

public static class LicitacionEvaluadorService
{
    public static (Oferta? mejorOferta, string clasificacion, decimal ahorroPorcentaje) EvaluarMejorOferta(
        Licitacion licitacion, 
        IEnumerable<Oferta> ofertas)
    {
        var ofertaGanadora = ofertas
            .Where(o => o.LicitacionId == licitacion.Id)
            .OrderBy(o => o.MontoOfertadoCRC)
            .ThenBy(o => o.FechaPresentacion)
            .FirstOrDefault();

        if (ofertaGanadora == null)
        {
            return (null, "Sin ofertas válidas", 0m);
        }

        if (licitacion.PresupuestoEstimadoCRC <= 0)
        {
            return (ofertaGanadora, "Oferta válida sin ahorro", 0m);
        }

        decimal ahorro = licitacion.PresupuestoEstimadoCRC - ofertaGanadora.MontoOfertadoCRC;
        decimal porcentajeAhorro = (ahorro / licitacion.PresupuestoEstimadoCRC) * 100m;

        string clasificacion = porcentajeAhorro switch
        {
            >= 10m => "Oferta conveniente",
            > 0m => "Oferta aceptable",
            _ => "Oferta válida sin ahorro"
        };

        return (ofertaGanadora, clasificacion, porcentajeAhorro);
    }
}