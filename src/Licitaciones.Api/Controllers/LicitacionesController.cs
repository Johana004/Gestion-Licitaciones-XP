using Licitaciones.Application.DTOs;
using Licitaciones.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Licitaciones.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LicitacionesController : ControllerBase
{
    private readonly LicitacionService _service;

    public LicitacionesController(LicitacionService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _service.ObtenerTodasAsync(cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CrearLicitacionDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.CrearAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { mensaje = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpPut("{id:guid}/publicar")]
    public async Task<IActionResult> Publicar(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.PublicarAsync(id, cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensaje = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpPut("{id:guid}/cerrar")]
    public async Task<IActionResult> Cerrar(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.CerrarAsync(id, cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensaje = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpPost("{id:guid}/adjudicar")]
    public async Task<IActionResult> Adjudicar(Guid id, [FromBody] AdjudicarLicitacionDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.AdjudicarLicitacionAsync(id, dto.OfertaGanadoraId, cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensaje = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { mensaje = ex.Message });
        }
    }
}