namespace Inmobiliaria2Cuarti.Models;
using System.ComponentModel.DataAnnotations;

public class Contrato
{
    [Key]
    public int IdContrato { get; set; }

    [Required]
    public int IdInmueble { get; set; }

    [Required]
    public int IdInquilino { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime FechaInicio { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime FechaFin { get; set; }

    [Required]
    [DataType(DataType.Currency)]
    public decimal MontoRenta { get; set; }

    [DataType(DataType.Currency)]
    public decimal Deposito { get; set; }

    [DataType(DataType.Currency)]
    public decimal Comision { get; set; }
    public string? Condiciones { get; set; }

    // propiedades para mostrar datos
    public string? PropietarioNombre { get; set; }
    public string? PropietarioApellido { get; set; }
    public string? InmuebleDireccion { get; set; }
    public string? InquilinoNombre { get; set; }
    public string? InquilinoApellido { get; set; }
}
