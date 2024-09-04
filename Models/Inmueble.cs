namespace Inmobiliaria2Cuatri.Models;
public class Inmueble
{
    public int idInmueble { get; set; }
    public int idPropietario { get; set; }
    public string? Direccion { get; set; }
    public string? Uso { get; set; }
    public string? Tipo { get; set; }
    public int CantAmbiente { get; set; }
    public decimal Valor { get; set; }
    public bool Estado { get; set; }
}
