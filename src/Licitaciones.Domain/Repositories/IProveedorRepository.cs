using Licitaciones.Domain.Entities;

namespace Licitaciones.Domain.Repositories;

public interface IProveedorRepository
{
    Task<Proveedor?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Proveedor?> GetByNombreNormalizadoAsync(string nombreNormalizado, CancellationToken cancellationToken = default);
    Task<IEnumerable<Proveedor>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Proveedor proveedor, CancellationToken cancellationToken = default);
    Task UpdateAsync(Proveedor proveedor, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}