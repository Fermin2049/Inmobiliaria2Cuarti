namespace Inmobiliaria2Cuatri.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Inmueble
    {
        [Key]
        public int IdInmueble { get; set; }
        [Required]
        public int IdPropietario { get; set; }
        [Required]
        public string? Direccion { get; set; }
        [Required]
        public string? Uso { get; set; }
        [Required]
        public TipoInmueble Tipo { get; set; } // Asegúrate de que TipoInmueble esté en el mismo espacio de nombres
        [Required]
        public int CantAmbiente { get; set; }
        [Required]
        public int Valor { get; set; }
        [Required]
        public bool Estado { get; set; }
        public Propietario? Propietario { get; set; }

        public Inmueble()
        {

        }

        public Inmueble(int idInmueble, int idPropietario, string? direccion, string? uso, TipoInmueble tipo, int cantAmbiente, int valor, bool estado)
        {
            IdInmueble = idInmueble;
            IdPropietario = idPropietario;
            Direccion = direccion;
            Uso = uso;
            Tipo = tipo;
            CantAmbiente = cantAmbiente;
            Valor = valor;
            Estado = estado;
        }
    }

    // Enum para Tipo de Inmueble
    public enum TipoInmueble
    {
        Casa = 1,
        Oficina = 2,
        Departamento = 3,
        Almacen = 4
    }
}
