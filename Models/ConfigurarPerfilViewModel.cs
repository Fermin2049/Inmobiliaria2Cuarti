namespace Inmobiliaria2Cuarti.Models
{
    public class ConfigurarPerfilViewModel
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Email { get; set; }

        // Campos adicionales para cambio de contraseña
        public string? ContraseniaAnterior { get; set; }
        public string? ContraseniaNueva { get; set; }
    }
}
