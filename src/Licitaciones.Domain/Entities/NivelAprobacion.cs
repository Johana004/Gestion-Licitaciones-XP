namespace Licitaciones.Domain.Entities;

public class NivelAprobacion
{
    public Guid Id { get; private set; }
    public decimal MontoMinimoCRC { get; private set; }
    public decimal? MontoMaximoCRC { get; private set; }
    public string Aprobador { get; private set; } = string.Empty;

    private NivelAprobacion() { } // Para EF Core

    public NivelAprobacion(decimal montoMinimoCRC, decimal? montoMaximoCRC, string aprobador)
    {
        Id = Guid.NewGuid();
        MontoMinimoCRC = montoMinimoCRC;
        MontoMaximoCRC = montoMaximoCRC;
        Aprobador = aprobador;
    }
}