namespace Inmobiliaria2Cuatri.Models;
using System.ComponentModel.DataAnnotations;
    public class Propietario
    {
        [Key]
        public int IdPropietario { get; set; }
        [Required]
        public string? Nombre { get; set; }
        [Required]
        public string? Apellido { get; set; }
        [Required]
        public int  Dni { get; set; }
        [Required]
        public string? Telefono { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public bool Estado { get; set; }
   
    }

