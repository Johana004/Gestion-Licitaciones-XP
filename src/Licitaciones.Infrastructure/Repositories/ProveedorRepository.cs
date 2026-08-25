using Licitaciones.Domain.Entities;
using Licitaciones.Domain.Repositories;
using Licitaciones.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Licitaciones.Infrastructure.Repositories;

public class ProveedorRepository : IProveedorRepository
{
    private readonly LicitacionesDbContext _context;

    public ProveedorRepository(LicitacionesDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Proveedor proveedor, CancellationToken cancellationToken = default)
    {
        await _context.Proveedores.AddAsync(proveedor, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Proveedor?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Proveedores
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<Proveedor?> GetByNombreNormalizadoAsync(string nombreNormalizado, CancellationToken cancellationToken = default)
    {
        return await _context.Proveedores
            .FirstOrDefaultAsync(p => p.NombreNormalizado == nombreNormalizado, cancellationToken);
    }

    public async Task<IEnumerable<Proveedor>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Proveedores
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Proveedor proveedor, CancellationToken cancellationToken = default)
    {
        _context.Proveedores.Update(proveedor);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var proveedor = await GetByIdAsync(id, cancellationToken);
        if (proveedor != null)
        {
            _context.Proveedores.Remove(proveedor);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}