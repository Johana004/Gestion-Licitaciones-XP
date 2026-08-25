using Licitaciones.Domain.Entities;

namespace Licitaciones.Domain.Repositories;

public interface INivelAprobacionRepository
{
    Task<IEnumerable<NivelAprobacion>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<NivelAprobacion?> GetAprobadorParaMontoAsync(decimal montoCRC, CancellationToken cancellationToken = default);
    Task AddAsync(NivelAprobacion nivel, CancellationToken cancellationToken = default);
}