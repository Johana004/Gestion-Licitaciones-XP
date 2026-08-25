using Licitaciones.Domain.Entities;

namespace Licitaciones.Domain.Repositories;

public interface IOfertaRepository
{
    Task<Oferta?> GetByLicitacionYProveedorAsync(Guid licitacionId, Guid proveedorId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Oferta>> GetByLicitacionIdAsync(Guid licitacionId, CancellationToken cancellationToken = default);
    Task AddAsync(Oferta oferta, CancellationToken cancellationToken = default);
}