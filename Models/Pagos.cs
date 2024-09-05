using System;
using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria2Cuarti.Models
{
    public class Pagos
    {
        [Key]
        public int IdPago { get; set; }
        [Required]
        public int IdContrato { get; set; }
        [Required]
        public int NroPago { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaPago { get; set; }
        [Required]
        public string? Detalle { get; set; }
        [Required]
        public decimal? Importe { get; set; }
        [Required]
        public string? Estado { get; set; }
    }
}
