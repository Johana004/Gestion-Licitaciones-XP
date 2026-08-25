namespace Licitaciones.Domain.Entities;

public class TipoCambio
{
    public Guid Id { get; private set; }
    public decimal CRCPorUSD { get; private set; }
    public DateTimeOffset FechaVigencia { get; private set; }
    public bool Activo { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }

    private TipoCambio() { } // Constructor para EF Core

    public TipoCambio(decimal crcPorUsd, DateTimeOffset fechaVigencia, bool activo, DateTimeOffset fechaActual)
    {
        if (crcPorUsd <= 0)
            throw new ArgumentException("El tipo de cambio debe ser mayor a cero.");

        Id = Guid.NewGuid();
        CRCPorUSD = crcPorUsd;
        FechaVigencia = fechaVigencia;
        Activo = activo;
        CreatedAt = fechaActual;
        UpdatedAt = fechaActual;
    }

    public void Activar() => Activo = true;
    public void Desactivar() => Activo = false;
}