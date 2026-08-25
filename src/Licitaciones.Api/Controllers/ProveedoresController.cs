using Licitaciones.Application.DTOs;
using Licitaciones.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Licitaciones.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProveedoresController : ControllerBase
{
    private readonly IProveedorService _proveedorService;

    public ProveedoresController(IProveedorService proveedorService)
    {
        _proveedorService = proveedorService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var proveedores = await _proveedorService.ObtenerTodosAsync(cancellationToken);
        return Ok(proveedores);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var proveedor = await _proveedorService.ObtenerPorIdAsync(id, cancellationToken);
        if (proveedor == null) return NotFound();

        return Ok(proveedor);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CrearProveedorDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var nuevoProveedor = await _proveedorService.CrearAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = nuevoProveedor.Id }, nuevoProveedor);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { mensaje = ex.Message });
        }
    }
}