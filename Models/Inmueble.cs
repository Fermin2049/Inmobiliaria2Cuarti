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
    public TipoInmueble Tipo { get; set; }
    [Required]
    public int CantAmbiente { get; set; }
    [Required]
    public decimal Valor { get; set; }
    [Required]
    public bool Estado { get; set; }
    public Propietario? Propietario { get; set; }
    
}

public enum TipoInmueble
{
    Casa =1,
    Depto = 2,
    Local = 3,
    Oficina = 4 
}