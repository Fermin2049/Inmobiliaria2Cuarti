using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria2Cuarti.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        public string? Contrasenia { get; set; }

        public string? Avatar { get; set; } // No Required attribute here
        public int Rol { get; set; }
        public bool Estado { get; set; }
    }

    public enum RolUsuario
    {
        Administrador = 1,
        Empleado = 2,
    }
}
