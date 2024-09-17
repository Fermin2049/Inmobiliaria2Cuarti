using System.Security.Claims;
using BCrypt.Net;
using Inmobiliaria2Cuarti.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Inmobiliaria2Cuarti.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private RepositorioUsuario repo;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
            repo = new RepositorioUsuario();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string contrasenia)
        {
            _logger.LogInformation("Intentando iniciar sesión con el email: {Email}", email);
            var usuario = repo.ObtenerPorEmail(email);
            if (usuario != null)
            {
                _logger.LogInformation("Usuario encontrado: {Email}", usuario.Email);
                try
                {
                    _logger.LogInformation("Hash almacenado: {Hash}", usuario.Contrasenia);
                    _logger.LogInformation("Contraseña ingresada: {Password}", contrasenia);
                    if (BCrypt.Net.BCrypt.Verify(contrasenia, usuario.Contrasenia))
                    {
                        _logger.LogInformation(
                            "Contraseña verificada correctamente para el usuario: {Email}",
                            usuario.Email
                        );
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, usuario.Email),
                            new Claim(ClaimTypes.Role, ((RolUsuario)usuario.Rol).ToString()),
                            new Claim("Avatar", usuario.Avatar ?? string.Empty),
                        };

                        var claimsIdentity = new ClaimsIdentity(
                            claims,
                            CookieAuthenticationDefaults.AuthenticationScheme
                        );
                        var authProperties = new AuthenticationProperties { IsPersistent = true };

                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            authProperties
                        );
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        _logger.LogWarning(
                            "Contraseña incorrecta para el usuario: {Email}",
                            usuario.Email
                        );
                    }
                }
                catch (SaltParseException ex)
                {
                    _logger.LogError(
                        ex,
                        "Error al verificar la contraseña para el usuario: {Email}",
                        usuario.Email
                    );
                }
            }
            else
            {
                _logger.LogWarning("Usuario no encontrado con el email: {Email}", email);
            }

            ModelState.AddModelError(string.Empty, "Email o contraseña incorrectos.");
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
