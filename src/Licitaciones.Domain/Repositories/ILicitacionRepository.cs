using Licitaciones.Domain.Entities;

namespace Licitaciones.Domain.Repositories;

public interface ILicitacionRepository
{
    Task<Licitacion?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Licitacion?> GetByCodigoNormalizadoAsync(string codigoNormalizado, CancellationToken cancellationToken = default);
    Task<IEnumerable<Licitacion>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Licitacion licitacion, CancellationToken cancellationToken = default);
    Task UpdateAsync(Licitacion licitacion, CancellationToken cancellationToken = default);
}