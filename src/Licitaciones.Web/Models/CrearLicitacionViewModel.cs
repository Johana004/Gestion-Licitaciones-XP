using System.ComponentModel.DataAnnotations;

namespace Licitaciones.Web.Models;

public class CrearLicitacionViewModel
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