using Licitaciones.Application.DTOs;
using Licitaciones.Domain.Entities;
using Licitaciones.Domain.Repositories;

namespace Licitaciones.Application.Services;

public class ProveedorService : IProveedorService
{
    private readonly IProveedorRepository _proveedorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProveedorService(IProveedorRepository proveedorRepository, IUnitOfWork unitOfWork)
    {
        _proveedorRepository = proveedorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ProveedorDto>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
    {
        var proveedores = await _proveedorRepository.GetAllAsync(cancellationToken);
        return proveedores.Select(p => new ProveedorDto(p.Id, p.Nombre));
    }

    public async Task<ProveedorDto?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var proveedor = await _proveedorRepository.GetByIdAsync(id, cancellationToken);
        if (proveedor == null) return null;

        return new ProveedorDto(proveedor.Id, proveedor.Nombre);
    }

    public async Task<ProveedorDto> CrearAsync(CrearProveedorDto dto, CancellationToken cancellationToken = default)
    {
        // Validar si ya existe un proveedor con el mismo nombre
        var existente = await _proveedorRepository.GetByNombreAsync(dto.Nombre, cancellationToken);
        if (existente != null)
        {
            throw new InvalidOperationException($"Ya existe un proveedor registrado con el nombre '{dto.Nombre}'.");
        }

        // Instanciar entidad (aplica las reglas de negocio y normalización creadas en la entidad)
        var nuevoProveedor = new Proveedor(dto.Nombre);

        await _proveedorRepository.AddAsync(nuevoProveedor, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ProveedorDto(nuevoProveedor.Id, nuevoProveedor.Nombre);
    }
}