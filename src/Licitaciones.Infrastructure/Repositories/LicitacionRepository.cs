using Licitaciones.Domain.Entities;
using Licitaciones.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Licitaciones.Infrastructure.Persistence.Repositories;

public class LicitacionRepository : ILicitacionRepository
{
    private readonly LicitacionesDbContext _context;

    public LicitacionRepository(LicitacionesDbContext context)
    {
        _context = context;
    }

    public async Task<Licitacion?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Licitaciones.FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
    }

    public async Task<Licitacion?> GetByCodigoNormalizadoAsync(string codigoNormalizado, CancellationToken cancellationToken = default)
    {
        return await _context.Licitaciones.FirstOrDefaultAsync(l => l.CodigoNormalizado == codigoNormalizado, cancellationToken);
    }

    public async Task<IEnumerable<Licitacion>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Licitaciones.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Licitacion licitacion, CancellationToken cancellationToken = default)
    {
        await _context.Licitaciones.AddAsync(licitacion, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Licitacion licitacion, CancellationToken cancellationToken = default)
    {
        _context.Licitaciones.Update(licitacion);
        await _context.SaveChangesAsync(cancellationToken);
    }
}