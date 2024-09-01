using System;
using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria2Cuarti.Models
{
    public class Contrato
    {
        public int IdContrato { get; set; }
        public int IdInmueble { get; set; }
        public int IdInquilino { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaInicio { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaFin { get; set; }

        [DataType(DataType.Currency)]
        public decimal MontoRenta { get; set; }

        [DataType(DataType.Currency)]
        public decimal Deposito { get; set; }

        [DataType(DataType.Currency)]
        public decimal Comision { get; set; }

        public string? Condiciones { get; set; }
    }
}
