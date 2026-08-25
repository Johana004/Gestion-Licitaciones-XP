using Licitaciones.Domain.Entities;

namespace Licitaciones.Domain.Repositories;

public interface ITipoCambioRepository
{
    Task<TipoCambio?> GetActivoAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<TipoCambio>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(TipoCambio tipoCambio, CancellationToken cancellationToken = default);
    Task ActivarAsync(Guid id, CancellationToken cancellationToken = default);
}