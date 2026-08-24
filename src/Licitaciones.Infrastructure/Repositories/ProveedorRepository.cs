using Licitaciones.Domain.Entities;
using Licitaciones.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Licitaciones.Infrastructure.Persistence.Repositories;

public class ProveedorRepository : IProveedorRepository
{
    private readonly LicitacionesDbContext _context;

    public ProveedorRepository(LicitacionesDbContext context)
    {
        _context = context;
    }

    public async Task<Proveedor?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Proveedores.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<Proveedor?> GetByNombreAsync(string nombre, CancellationToken cancellationToken = default)
    {
        return await _context.Proveedores.FirstOrDefaultAsync(p => p.Nombre == nombre, cancellationToken);
    }

    public async Task<IEnumerable<Proveedor>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Proveedores.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Proveedor proveedor, CancellationToken cancellationToken = default)
    {
        await _context.Proveedores.AddAsync(proveedor, cancellationToken);
    }

    public void Update(Proveedor proveedor)
    {
        _context.Proveedores.Update(proveedor);
    }

    public void Delete(Proveedor proveedor)
    {
        _context.Proveedores.Remove(proveedor);
    }
}