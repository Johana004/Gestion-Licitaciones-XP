using Licitaciones.Domain.Entities;

namespace Licitaciones.Domain.Repositories;

public interface IProveedorRepository
{
    Task<Proveedor?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Proveedor?> GetByNombreAsync(string nombre, CancellationToken cancellationToken = default);
    Task<IEnumerable<Proveedor>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Proveedor proveedor, CancellationToken cancellationToken = default);
    void Update(Proveedor proveedor);
    void Delete(Proveedor proveedor);
}