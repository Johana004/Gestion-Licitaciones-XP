using Licitaciones.Domain.Entities;
using Licitaciones.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Licitaciones.Infrastructure.Persistence;

namespace Licitaciones.Infrastructure.Persistence.Repositories;

public class OfertaRepository : IOfertaRepository
{
    private readonly LicitacionesDbContext _context;

    public OfertaRepository(LicitacionesDbContext context)
    {
        _context = context;
    }

    public async Task<Oferta?> GetByLicitacionYProveedorAsync(Guid licitacionId, Guid proveedorId, CancellationToken cancellationToken = default)
    {
        return await _context.Ofertas.FirstOrDefaultAsync(o => o.LicitacionId == licitacionId && o.ProveedorId == proveedorId, cancellationToken);
    }

    public async Task<IEnumerable<Oferta>> GetByLicitacionIdAsync(Guid licitacionId, CancellationToken cancellationToken = default)
    {
        return await _context.Ofertas.Where(o => o.LicitacionId == licitacionId).ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Oferta oferta, CancellationToken cancellationToken = default)
    {
        await _context.Ofertas.AddAsync(oferta, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}