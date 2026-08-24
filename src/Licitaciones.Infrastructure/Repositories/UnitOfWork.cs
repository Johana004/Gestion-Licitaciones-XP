using Licitaciones.Domain.Repositories;
using Licitaciones.Infrastructure.Persistence;

namespace Licitaciones.Infrastructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly LicitacionesDbContext _context;

    public UnitOfWork(LicitacionesDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}