namespace Licitaciones.Domain.Services;

using Licitaciones.Domain.Entities;
using Licitaciones.Domain.Enums;

public class LicitacionDomainService : ILicitacionDomainService
{
    public MejorOfertaResultado CalcularMejorOferta(Licitacion licitacion, IEnumerable<NivelAprobacion> nivelesAprobacion)
    {
        if (!licitacion.Ofertas.Any())
        {
            return new MejorOfertaResultado(null, 0, null, null);
        }

        // Regla: Menor monto CRC. En empate, la primera registrada
        var mejorOferta = licitacion.Ofertas
            .OrderBy(o => o.MontoOfertadoCRC)
            .ThenBy(o => o.FechaPresentacion)
            .First();

        // Cálculo porcentaje de ahorro = ((Presupuesto - Oferta) / Presupuesto) * 100
        decimal presupuesto = licitacion.PresupuestoEstimadoCRC;
        decimal ahorro = ((presupuesto - mejorOferta.MontoOfertadoCRC) / presupuesto) * 100m;

        // Clasificación del ahorro
        ClasificacionAhorro clasificacion = ahorro switch
        {
            >= 10m => ClasificacionAhorro.OfertaConveniente,
            > 0m => ClasificacionAhorro.OfertaAceptable,
            _ => ClasificacionAhorro.OfertaValidaSinAhorro
        };

        // Búsqueda del Aprobador según Rangos
        var aprobador = nivelesAprobacion
            .FirstOrDefault(n => mejorOferta.MontoOfertadoCRC >= n.MontoMinimoCRC &&
                                (!n.MontoMaximoCRC.HasValue || mejorOferta.MontoOfertadoCRC <= n.MontoMaximoCRC.Value))
            ?.Aprobador ?? "Sin aprobador asignado";

        return new MejorOfertaResultado(mejorOferta, ahorro, clasificacion, aprobador);
    }
}