namespace Inmobiliaria2Cuarti.Models;

public class PagosViewModel
{
    public List<Pagos> PagosRealizados { get; set; } = new List<Pagos>();
    public List<string> MesesNoPagados { get; set; } = new List<string>();
    public decimal? MultaPendiente { get; set; }
}
