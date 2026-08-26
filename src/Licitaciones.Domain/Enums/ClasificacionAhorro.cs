namespace Licitaciones.Domain.Enums;

public enum ClasificacionAhorro
{
    OfertaConveniente,  // Ahorro >= 10%
    OfertaAceptable,    // Ahorro > 0% y < 10%
    OfertaValidaSinAhorro // Ahorro == 0%
}