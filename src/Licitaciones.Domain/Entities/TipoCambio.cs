namespace Licitaciones.Domain.Entities;

public class TipoCambio
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public decimal CRCporUSD { get; private set; }
    public DateTimeOffset FechaVigencia { get; private set; }
    public bool Activo { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; private set; }

    private TipoCambio() { } // EF Core

    public TipoCambio(decimal crcPorUsd, DateTimeOffset fechaVigencia, bool activo = false)
    {
        if (crcPorUsd <= 0)
            throw new ArgumentException("El tipo de cambio debe ser mayor que cero.");

        CRCporUSD = crcPorUsd;
        FechaVigencia = fechaVigencia;
        Activo = activo;
    }

    public void Activar() => Activo = true;
    public void Desactivar() => Activo = false;

    public decimal ConvertirCRCaUSD(decimal montoCRC)
    {
        if (CRCporUSD <= 0) return 0;
        return Math.Round(montoCRC / CRCporUSD, 2, MidpointRounding.AwayFromZero);
    }
}