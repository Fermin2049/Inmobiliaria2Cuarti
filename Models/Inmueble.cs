public class Inmueble
{
    public int IdInmueble { get; set; }
    public int IdPropietario { get; set; }
    public string? Direccion { get; set; }
    public string? Uso { get; set; }
    public string? Tipo { get; set; }
    public int CantAmbiente { get; set; }
    public decimal Valor { get; set; }
    public bool Estado { get; set; }
}
