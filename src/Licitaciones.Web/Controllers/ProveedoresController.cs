using Licitaciones.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Licitaciones.Web.Controllers;

public class ProveedoresController : Controller
{
    // Lista estática en memoria con un proveedor por defecto para pruebas
    public static readonly List<ProveedorViewModel> _proveedores = new()
    {
        new ProveedorViewModel 
        { 
            Id = Guid.NewGuid(), 
            CedulaJuridica = "3-101-654321", 
            NombreRazonSocial = "Soluciones Tecnológicas del Norte S.A.", 
            EmailContacto = "contacto@solucionestech.cr",
            Telefono = "2460-1234"
        }
    };

    [HttpGet]
    public IActionResult Index()
    {
        return View(_proveedores);
    }

    [HttpGet]
    public IActionResult Crear()
    {
        return View(new ProveedorViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Crear(ProveedorViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        _proveedores.Add(model);

        return RedirectToAction(nameof(Index));
    }
}