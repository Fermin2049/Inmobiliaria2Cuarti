namespace Inmobiliaria2Cuatri.Models;
using System.ComponentModel.DataAnnotations;
using Inmobiliaria2Cuarti.Models;

public class Inmueble
    {
        [Key]
        public int IdInmueble { get; set; }

        [Required]
        public int IdPropietario { get; set; }
        [Required]
        public int IdTipoInmueble { get; set; }

        [Required]
        public string? Direccion { get; set; }

        [Required]
        public Uso Uso { get; set; }

        [Required]
        public int CantAmbiente { get; set; }

        [Required]
        public int Valor { get; set; }

        [Required]
        public bool Estado { get; set; }

        [Required]
        public bool Disponible { get; set; }

        public Propietario? Propietario { get; set; }
        public TipoInmueble? TipoInmueble { get; set; }  
        
    }

    public enum Disponibilidad
    {
        Disponible = 1,
        NoDisponible = 0,
    }

    public enum Uso
    {
        Residencial = 1,
        Comercial = 2,
    }

