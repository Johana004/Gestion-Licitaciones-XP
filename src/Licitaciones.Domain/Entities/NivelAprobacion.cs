namespace Licitaciones.Domain.Entities;

public class NivelAprobacion
{
    public Guid Id { get; private set; }
    public decimal MontoMinimoCRC { get; private set; }
    public decimal? MontoMaximoCRC { get; private set; }
    public string Aprobador { get; private set; } = null!;
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }

    private NivelAprobacion() { }

    public NivelAprobacion(decimal montoMinimoCRC, decimal? montoMaximoCRC, string aprobador, DateTimeOffset fechaActual)
    {
        if (montoMinimoCRC <= 0)
            throw new ArgumentException("El monto mínimo debe ser mayor a cero.");

        if (montoMaximoCRC.HasValue && montoMaximoCRC.Value <= montoMinimoCRC)
            throw new ArgumentException("El monto máximo debe ser mayor al monto mínimo.");

        if (string.IsNullOrWhiteSpace(aprobador))
            throw new ArgumentException("El aprobador es obligatorio.");

        Id = Guid.NewGuid();
        MontoMinimoCRC = montoMinimoCRC;
        MontoMaximoCRC = montoMaximoCRC;
        Aprobador = aprobador.Trim();
        CreatedAt = fechaActual;
        UpdatedAt = fechaActual;
    }
}