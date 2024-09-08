namespace Inmobiliaria2Cuatri.Models;
using System.ComponentModel.DataAnnotations;
public class Inmueble
{
    [Key]
    public int IdInmueble { get; set; }
    [Required]
    public int IdPropietario { get; set; }
    [Required]
    public string? Direccion { get; set; }
    [Required]
    public string? Uso { get; set; }
    [Required]
    public string? Tipo { get; set; }
    [Required]
    public int CantAmbiente { get; set; }
    [Required]
    public int Valor { get; set; }
    [Required]
    public bool Estado { get; set; }
    public string? PropietarioNombre { get; set; }
    public string? PropietarioApellido { get; set; }
    public Propietario? Propietario { get; set; }
    
}

