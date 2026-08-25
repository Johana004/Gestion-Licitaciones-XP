using Licitaciones.Application.DTOs;
using Licitaciones.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Licitaciones.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OfertasController : ControllerBase
{
    private readonly OfertaService _service;

    public OfertasController(OfertaService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> PresentarOferta([FromBody] CrearOfertaDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.PresentarOfertaAsync(dto, cancellationToken);
            return StatusCode(201, result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensaje = ex.Message });
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
}