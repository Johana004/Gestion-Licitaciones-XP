using Licitaciones.Application.DTOs;

namespace Licitaciones.Application.Services;

public interface IProveedorService
{
    Task<IEnumerable<ProveedorDto>> ObtenerTodosAsync(CancellationToken cancellationToken = default);
    Task<ProveedorDto?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ProveedorDto> CrearAsync(CrearProveedorDto dto, CancellationToken cancellationToken = default);
}