namespace Inmobiliaria2Cuatri.Models;
    public class Inquilino
    {
        public int idInquilino { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public int  Dni { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public bool Estado { get; set; }
   
    }