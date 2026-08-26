namespace Licitaciones.Domain.Entities;

public class Oferta
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid LicitacionId { get; private set; }
    public Guid ProveedorId { get; private set; }
    public decimal MontoOfertadoCRC { get; private set; }
    public DateTimeOffset FechaPresentacion { get; private set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; private set; }
    public byte[] Version { get; private set; } = [];

    public Licitacion? Licitacion { get; private set; }
    public Proveedor? Proveedor { get; private set; }

    private Oferta() { } // EF Core

    public Oferta(Licitacion licitacion, Guid proveedorId, decimal montoCRC, DateTimeOffset fechaActual)
    {
        if (licitacion.EstaVencida(fechaActual))
            throw new InvalidOperationException("No se pueden registrar ofertas en licitaciones cerradas o vencidas.");

        if (montoCRC <= 0)
            throw new ArgumentException("El monto ofertado debe ser mayor que cero.");

        if (montoCRC > licitacion.PresupuestoEstimadoCRC)
            throw new InvalidOperationException("El monto ofertado no puede superar el presupuesto de la licitación.");

        LicitacionId = licitacion.Id;
        ProveedorId = proveedorId;
        MontoOfertadoCRC = montoCRC;
        FechaPresentacion = fechaActual;
    }
}