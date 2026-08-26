namespace Licitaciones.Domain.Entities;

public class NivelAprobacion
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public decimal MontoMinimoCRC { get; private set; }
    public decimal? MontoMaximoCRC { get; private set; }
    public string Aprobador { get; private set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; private set; }

    private NivelAprobacion() { } // EF Core

    public NivelAprobacion(decimal montoMinimo, decimal? montoMaximo, string aprobador)
    {
        if (montoMinimo < 0)
            throw new ArgumentException("El monto mínimo no puede ser negativo.");

        if (montoMaximo.HasValue && montoMaximo.Value <= montoMinimo)
            throw new ArgumentException("El monto máximo debe ser estrictamente mayor que el monto mínimo.");

        if (string.IsNullOrWhiteSpace(aprobador))
            throw new ArgumentException("El nombre del aprobador es requerido.");

        MontoMinimoCRC = montoMinimo;
        MontoMaximoCRC = montoMaximo;
        Aprobador = aprobador.Trim();
    }
}