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

        public int NroPago { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaPago { get; set; }

        [Required]
        public string? Detalle { get; set; }

        [Required]
        public decimal? Importe { get; set; }

        [Required]
        public bool Estado { get; set; }

        public string? UsuarioCreacion { get; set; }

        public string? UsuarioAnulacion { get; set; }
    }
}
