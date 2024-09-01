using System;
using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria2Cuarti.Models
{
    public class Pagos
    {
        public int IdPago { get; set; }
        public int IdContrato { get; set; }
        public int NroPago { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaPago { get; set; }

        public string? Detalle { get; set; }
        public decimal? Importe { get; set; }
        public string? Estado { get; set; }
    }
}
