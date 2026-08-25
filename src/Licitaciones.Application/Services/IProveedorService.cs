using Licitaciones.Application.DTOs;

namespace Licitaciones.Application.Services;

public interface IProveedorService
{
    Task<ProveedorResponseDto> CrearProveedorAsync(CrearProveedorDto dto, CancellationToken cancellationToken = default);
    Task<IEnumerable<ProveedorResponseDto>> ObtenerTodosAsync(CancellationToken cancellationToken = default);
    Task<ProveedorResponseDto?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default);
}