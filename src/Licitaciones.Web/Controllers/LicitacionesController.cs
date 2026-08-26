using Licitaciones.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Licitaciones.Web.Controllers;

public class LicitacionesController : Controller
{
    // Datos de prueba iniciales para evitar errores 404 al reiniciar el servidor
    public static readonly List<Licitacion> _licitaciones = new()
    {
        new Licitacion(
            "LIC-2026-001",
            "Adquisición de Licencias de Software y Soporte Técnico",
            15000000m,
            DateTimeOffset.Now.AddDays(15),
            DateTimeOffset.Now
        )
    };

    [HttpGet]
    public IActionResult Index(string? buscar, string? estado)
    {
        var consulta = _licitaciones.AsQueryable();

        if (!string.IsNullOrWhiteSpace(buscar))
        {
            consulta = consulta.Where(l => 
                (l.Codigo != null && l.Codigo.Contains(buscar, StringComparison.OrdinalIgnoreCase)) || 
                (l.Titulo != null && l.Titulo.Contains(buscar, StringComparison.OrdinalIgnoreCase)));
        }

        ViewBag.Buscar = buscar;
        ViewBag.Estado = estado;

        return View(consulta.ToList());
    }

    [HttpGet]
    public IActionResult Detalle(Guid id)
    {
        var licitacion = _licitaciones.FirstOrDefault(l => l.Id == id);
        if (licitacion == null) 
        {
            return NotFound();
        }

        return View(licitacion);
    }

    [HttpGet]
    public IActionResult Crear()
    {
        return View(new CrearLicitacionModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Crear(CrearLicitacionModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var nuevaLicitacion = new Licitacion(
            model.Codigo,
            model.Titulo,
            model.PresupuestoEstimadoCRC,
            model.FechaCierre,
            DateTimeOffset.UtcNow
        );

        _licitaciones.Add(nuevaLicitacion);

        return RedirectToAction(nameof(Index));
    }
}

public class CrearLicitacionModel
{
    [Required(ErrorMessage = "El código es obligatorio.")]
    public string Codigo { get; set; } = string.Empty;

    [Required(ErrorMessage = "El título es obligatorio.")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "El presupuesto es obligatorio.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El presupuesto debe ser mayor a 0.")]
    public decimal PresupuestoEstimadoCRC { get; set; }

    [Required(ErrorMessage = "La fecha de cierre es obligatoria.")]
    public DateTimeOffset FechaCierre { get; set; } = DateTimeOffset.Now.AddDays(7);
}