namespace Inmobiliaria2Cuatri.Models
{
    public class DashboardViewModel
    {
        public int TotalPropietarios { get; set; }
        public int TotalInquilinos { get; set; }
        public int TotalInmuebles { get; set; }
        public int TotalUsuarios { get; set; }
        public List<int> InquilinosPorInmueble { get; set; } = new List<int>();
        public List<int> PagosMensuales { get; set; } = new List<int>();
        public List<int> NuevosContratosPorMes { get; set; } = new List<int>();
    }
}
