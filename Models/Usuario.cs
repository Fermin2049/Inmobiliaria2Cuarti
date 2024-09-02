using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria2Cuarti.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string? Contrasenia { get; set; }

        public string? Avatar { get; set; }
        public string? Rol { get; set; }
        public bool Estado { get; set; }
    }
}
