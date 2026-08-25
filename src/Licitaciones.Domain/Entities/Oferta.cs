namespace Licitaciones.Domain.Entities;

public class Oferta
{
    public Guid Id { get; private set; }
    public Guid LicitacionId { get; private set; }
    public Guid ProveedorId { get; private set; }
    public decimal MontoOfertaCRC { get; private set; }
    public DateTimeOffset FechaPresentacion { get; private set; }
    public uint VersionConcurrencia { get; private set; }

    private Oferta() { } // EF Core

    public Oferta(Guid licitacionId, Guid proveedorId, decimal montoOfertaCRC, DateTimeOffset fechaPresentacion)
    {
        if (licitacionId == Guid.Empty)
            throw new ArgumentException("El ID de la licitación es obligatorio.");

        if (proveedorId == Guid.Empty)
            throw new ArgumentException("El ID del proveedor es obligatorio.");

        if (montoOfertaCRC <= 0)
            throw new ArgumentException("El monto de la oferta debe ser mayor a cero.");

        Id = Guid.NewGuid();
        LicitacionId = licitacionId;
        ProveedorId = proveedorId;
        MontoOfertaCRC = montoOfertaCRC;
        FechaPresentacion = fechaPresentacion;
    }
}