namespace Licitaciones.Domain.Entities;

public class TipoCambio
{
    public Guid Id { get; private set; }
    public decimal Valor { get; private set; }
    public DateTime FechaVigencia { get; private set; }
    public bool Activo { get; private set; }

    private TipoCambio() { } // Para EF Core

    public TipoCambio(decimal valor, DateTime fechaVigencia)
    {
        if (valor <= 0)
        {
            throw new ArgumentException("El valor del tipo de cambio debe ser mayor a cero.");
        }

        Id = Guid.NewGuid();
        Valor = valor;
        FechaVigencia = fechaVigencia;
        Activo = false;
    }

    public void Activar()
    {
        Activo = true;
    }
}