using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria2Cuatri.Models
{
    public class TipoInmueble
    {
        [Key]
        public int IdTipoInmueble { get; set; }
        [Required]
        public string? Nombre { get; set; }
        [Required]
        public bool Activo { get; set; } = true;


        public TipoInmueble(string nombre)
        {
            Nombre = nombre;
            Activo = true;
        }

        public TipoInmueble() { }
    }
}

