namespace Licitaciones.Domain.Entities;

using Licitaciones.Domain.Enums;

public class Licitacion
{
    public Guid Id { get; private set; }
    public string Codigo { get; private set; } = string.Empty;
    public string CodigoNormalizado { get; private set; } = string.Empty;
    public string Titulo { get; private set; } = string.Empty;
    public EstadoLicitacion Estado { get; private set; }
    public DateTimeOffset FechaCierre { get; private set; }
    public decimal PresupuestoEstimadoCRC { get; private set; }
    public Guid? OfertaGanadoraId { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }
    public uint VersionConcurrencia { get; private set; }

    // Propiedad de navegación y backing field para EF Core
    private readonly List<Oferta> _ofertas = new();
    public IReadOnlyCollection<Oferta> Ofertas => _ofertas.AsReadOnly();

    private Licitacion() { } // EF Core

    public Licitacion(string codigo, string titulo, decimal presupuestoEstimadoCRC, DateTimeOffset fechaCierre, DateTimeOffset fechaActual)
    {
        if (string.IsNullOrWhiteSpace(codigo))
            throw new ArgumentException("El código no puede estar vacío.");

        if (string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentException("El título no puede estar vacío.");

        if (presupuestoEstimadoCRC <= 0)
            throw new ArgumentException("El presupuesto debe ser mayor a cero.");

        if (fechaCierre <= fechaActual)
            throw new ArgumentException("La fecha de cierre debe ser futura.");

        Id = Guid.NewGuid();
        SetCodigo(codigo);
        Titulo = titulo.Trim();
        PresupuestoEstimadoCRC = presupuestoEstimadoCRC;
        FechaCierre = fechaCierre;
        Estado = EstadoLicitacion.Borrador;
        CreatedAt = fechaActual;
        UpdatedAt = fechaActual;
    }

    public void SetCodigo(string codigo)
    {
        if (string.IsNullOrWhiteSpace(codigo))
            throw new ArgumentException("El código no puede estar vacío.");
        
        Codigo = codigo.Trim();
        CodigoNormalizado = Codigo.ToUpperInvariant();
    }

    public void Publicar(DateTimeOffset fechaActual)
    {
        if (Estado != EstadoLicitacion.Borrador)
            throw new InvalidOperationException("Solo se puede publicar una licitación en borrador.");

        if (FechaCierre <= fechaActual)
            throw new InvalidOperationException("No se puede publicar una licitación con fecha de cierre vencida.");

        Estado = EstadoLicitacion.Publicada;
        UpdatedAt = fechaActual;
    }

    public void Adjudicar(Guid ofertaGanadoraId, DateTimeOffset fechaActual)
    {
        if (Estado != EstadoLicitacion.Publicada)
            throw new InvalidOperationException("Solo se puede adjudicar una licitación en estado 'Publicada'.");

        OfertaGanadoraId = ofertaGanadoraId;
        Estado = EstadoLicitacion.Adjudicada;
        UpdatedAt = fechaActual;
    }

    public void Cerrar(DateTimeOffset fechaActual)
    {
        if (Estado == EstadoLicitacion.Cerrada)
            throw new InvalidOperationException("La licitación ya se encuentra cerrada.");

        Estado = EstadoLicitacion.Cerrada;
        UpdatedAt = fechaActual;
    }

    public bool EstaVencida(DateTimeOffset fechaActual)
    {
        return Estado == EstadoLicitacion.Cerrada || fechaActual >= FechaCierre;
    }
}