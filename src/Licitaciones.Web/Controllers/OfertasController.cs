using Licitaciones.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Licitaciones.Web.Controllers;

public class OfertasController : Controller
{
    private static readonly List<Oferta> _ofertas = new();

    [HttpGet]
    public IActionResult Index()
    {
        return View(_ofertas);
    }

    [HttpGet]
    public IActionResult Crear(Guid licitacionId)
    {
        var model = new CrearOfertaViewModel
        {
            LicitacionId = licitacionId
        };
        return View(model);
    }

    [HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Crear(CrearOfertaViewModel model)
{
    if (!ModelState.IsValid)
    {
        return View(model);
    }

    // 1. Obtener la licitación correspondiente desde la lista estática
    var licitacion = LicitacionesController._licitaciones
        .FirstOrDefault(l => l.Id == model.LicitacionId);

    if (licitacion == null)
    {
        return NotFound("No se encontró la licitación asociada.");
    }

    // 2. Definir un ProveedorId ficticio para la prueba
    Guid proveedorId = Guid.NewGuid();

    // 3. Instanciar Oferta pasando exactamente los 4 parámetros requeridos en su orden
    var nuevaOferta = new Oferta(
        licitacion,              // Parameter 1: Licitacion
        proveedorId,             // Parameter 2: Guid
        model.MontoOfertadoCRC,  // Parameter 3: decimal
        DateTimeOffset.UtcNow    // Parameter 4: DateTimeOffset (fechaActual)
    );

    _ofertas.Add(nuevaOferta);

    return RedirectToAction(nameof(Index));
}
}

public class CrearOfertaViewModel
{
    [Required]
    public Guid LicitacionId { get; set; }

    [Required(ErrorMessage = "El monto es obligatorio.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0.")]
    public decimal MontoOfertadoCRC { get; set; }
}