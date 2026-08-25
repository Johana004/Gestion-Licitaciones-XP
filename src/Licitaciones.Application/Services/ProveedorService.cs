using Licitaciones.Application.DTOs;
using Licitaciones.Domain.Entities;
using Licitaciones.Domain.Repositories;

namespace Licitaciones.Application.Services;

public class ProveedorService : IProveedorService
{
    private readonly IProveedorRepository _proveedorRepository;

    public ProveedorService(IProveedorRepository proveedorRepository)
    {
        _proveedorRepository = proveedorRepository;
    }

    public async Task<ProveedorResponseDto> CrearProveedorAsync(CrearProveedorDto dto, CancellationToken cancellationToken = default)
    {
        var nombreNormalizado = Proveedor.NormalizarNombre(dto.Nombre).ToUpperInvariant();
        
        var existe = await _proveedorRepository.GetByNombreNormalizadoAsync(nombreNormalizado, cancellationToken);
        if (existe != null)
        {
            throw new InvalidOperationException($"Ya existe un proveedor registrado con el nombre '{dto.Nombre}'.");
        }

        var proveedor = new Proveedor(dto.Nombre, DateTimeOffset.UtcNow);

        await _proveedorRepository.AddAsync(proveedor, cancellationToken);

        return new ProveedorResponseDto(proveedor.Id, proveedor.Nombre, proveedor.CreatedAt);
    }

    public async Task<IEnumerable<ProveedorResponseDto>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
    {
        var proveedores = await _proveedorRepository.GetAllAsync(cancellationToken);
        return proveedores.Select(p => new ProveedorResponseDto(p.Id, p.Nombre, p.CreatedAt));
    }

    public async Task<ProveedorResponseDto?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var proveedor = await _proveedorRepository.GetByIdAsync(id, cancellationToken);
        if (proveedor == null) return null;

        return new ProveedorResponseDto(proveedor.Id, proveedor.Nombre, proveedor.CreatedAt);
    }
}