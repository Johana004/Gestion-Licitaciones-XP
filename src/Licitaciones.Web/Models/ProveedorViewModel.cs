using System.ComponentModel.DataAnnotations;

namespace Licitaciones.Web.Models;

public class ProveedorViewModel
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "La cédula jurídica es obligatoria.")]
    [Display(Name = "Cédula Jurídica")]
    public string CedulaJuridica { get; set; } = string.Empty;

    [Required(ErrorMessage = "La razón social es obligatoria.")]
    [Display(Name = "Nombre / Razón Social")]
    public string NombreRazonSocial { get; set; } = string.Empty;

    [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
    [EmailAddress(ErrorMessage = "El correo no tiene un formato válido.")]
    [Display(Name = "Correo Electrónico")]
    public string EmailContacto { get; set; } = string.Empty;

    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [Display(Name = "Teléfono")]
    public string Telefono { get; set; } = string.Empty;
}