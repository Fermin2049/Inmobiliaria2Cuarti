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
        public TipoInmueble Tipo { get; set; }

        [Required]
        public int CantAmbiente { get; set; }

        [Required]
        public int Valor { get; set; }

        [Required]
        public bool Estado { get; set; }

        [Required]
        public bool Disponible { get; set; }

        public Propietario? Propietario { get; set; }

        public Inmueble() { }

        public Inmueble(
            int idInmueble,
            int idPropietario,
            string? direccion,
            string? uso,
            TipoInmueble tipo,
            int cantAmbiente,
            int valor,
            bool estado,
            bool disponible
        )
        {
            IdInmueble = idInmueble;
            IdPropietario = idPropietario;
            Direccion = direccion;
            Uso = uso;
            Tipo = tipo;
            CantAmbiente = cantAmbiente;
            Valor = valor;
            Estado = estado;
            Disponible = disponible;
        }
    }

    // Enum para Tipo de Inmueble
    public enum TipoInmueble
    {
        Casa = 1,
        Depto = 2,
        Oficina = 3,
        Almacen = 4,
    }

    // Enum para Disponibilidad
    public enum Disponibilidad
    {
        Disponible = 1,
        NoDisponible = 0,
    }
}
