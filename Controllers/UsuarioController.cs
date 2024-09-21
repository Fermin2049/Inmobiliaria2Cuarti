using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BCrypt.Net;
using Firebase.Storage;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Inmobiliaria2Cuarti.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Inmobiliaria2Cuarti.Controllers
{
    [Authorize(Roles = "Administrador,Empleado")]
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;
        private RepositorioUsuario repo;
        private readonly StorageClient storageClient;

        public UsuarioController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
            repo = new RepositorioUsuario();
            storageClient = StorageClient.Create();
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Index()
        {
            var lista = repo.ObtenerTodos();
            return View(lista);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult Edicion(int id)
        {
            if (id == 0)
                return View();
            else
            {
                var usuario = repo.Obtener(id);
                return View(usuario);
            }
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> Edicion(Usuario usuario, IFormFile Avatar)
        {
            if (ModelState.IsValid)
            {
                if (Avatar != null && Avatar.Length > 0)
                {
                    try
                    {
                        var stream = Avatar.OpenReadStream();
                        var task = new FirebaseStorage("inmobilirianet.appspot.com")
                            .Child("avatars")
                            .Child(usuario.Email)
                            .Child(Avatar.FileName)
                            .PutAsync(stream);

                        usuario.Avatar = await task;
                    }
                    catch (FirebaseStorageException ex)
                    {
                        ModelState.AddModelError(
                            string.Empty,
                            "Error al subir el archivo: " + ex.Message
                        );
                        return View(usuario);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, "Ocurrió un error: " + ex.Message);
                        return View(usuario);
                    }
                }

                usuario.Contrasenia = BCrypt.Net.BCrypt.HashPassword(usuario.Contrasenia);
                repo.ActualizarUsuario(usuario);
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> Crear(Usuario usuario, IFormFile Avatar)
        {
            if (Avatar != null && Avatar.Length > 0)
            {
                try
                {
                    var stream = Avatar.OpenReadStream();
                    var task = new FirebaseStorage("inmobilirianet.appspot.com")
                        .Child("avatars")
                        .Child(usuario.Email)
                        .Child(Avatar.FileName)
                        .PutAsync(stream);

                    usuario.Avatar = await task;
                }
                catch (FirebaseStorageException ex)
                {
                    ModelState.AddModelError(
                        string.Empty,
                        "Error al subir el archivo: " + ex.Message
                    );
                    return View(usuario);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Ocurrió un error: " + ex.Message);
                    return View(usuario);
                }
            }

            if (ModelState.IsValid)
            {
                usuario.Contrasenia = BCrypt.Net.BCrypt.HashPassword(usuario.Contrasenia);
                _logger.LogInformation(
                    $"Hash generado para el usuario {usuario.Email}: {usuario.Contrasenia}"
                );

                repo.CrearUsuario(usuario);
                _logger.LogInformation($"Usuario {usuario.Email} creado correctamente.");

                return RedirectToAction(nameof(Index));
            }
            else
            {
                _logger.LogWarning(
                    $"Error al crear el usuario {usuario.Email}: {string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))}"
                );
            }

            return View(usuario);
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Detalle(int id)
        {
            var usuario = repo.Obtener(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Eliminar(int id)
        {
            if (id != 0)
            {
                repo.EliminarLogico(id);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ModificarAvatar()
        {
            var email = User.FindFirst(ClaimTypes.Name)?.Value;
            if (email == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var bucketName = "inmobilirianet.appspot.com";
            var prefix = $"avatars/{email}/";
            var storageClient = StorageClient.Create();
            var objects = storageClient.ListObjects(bucketName, prefix);

            var urls = objects
                .Select(obj =>
                    $"https://storage.googleapis.com/inmobilirianet.appspot.com/{obj.Name}"
                )
                .ToList();

            return View(urls); // Devuelve las URLs de las imágenes a la vista
        }

        [HttpPost]
        public async Task<IActionResult> SubirAvatar(IFormFile Avatar)
        {
            var email = User.FindFirst(ClaimTypes.Name)?.Value;
            if (email == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (Avatar != null && Avatar.Length > 0)
            {
                try
                {
                    var stream = Avatar.OpenReadStream();
                    var task = new FirebaseStorage("inmobilirianet.appspot.com")
                        .Child("avatars")
                        .Child(email)
                        .Child(Avatar.FileName)
                        .PutAsync(stream);

                    await task;

                    var usuario = repo.ObtenerPorEmail(email);
                    if (usuario != null)
                    {
                        usuario.Avatar =
                            $"https://storage.googleapis.com/inmobilirianet.appspot.com/avatars/{email}/{Avatar.FileName}";
                        repo.ActualizarUsuario(usuario);
                    }
                }
                catch (FirebaseStorageException ex)
                {
                    _logger.LogError(
                        ex,
                        "Error al subir el avatar para el usuario: {Email}",
                        email
                    );
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        ex,
                        "Error inesperado al subir el avatar para el usuario: {Email}",
                        email
                    );
                }
            }

            return RedirectToAction(nameof(ModificarAvatar));
        }

        [HttpPost]
        public async Task<IActionResult> EliminarAvatar()
        {
            var email = User.FindFirst(ClaimTypes.Name)?.Value;
            if (email == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var usuario = repo.ObtenerPorEmail(email);
            if (usuario != null)
            {
                usuario.Avatar = null;
                repo.ActualizarUsuario(usuario);
            }

            return RedirectToAction(nameof(ModificarAvatar));
        }

        [HttpPost]
        public async Task<IActionResult> SeleccionarAvatar(string avatarUrl)
        {
            var email = User.FindFirst(ClaimTypes.Name)?.Value;
            if (email == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var usuario = repo.ObtenerPorEmail(email);
            if (usuario != null)
            {
                usuario.Avatar = avatarUrl;
                repo.ActualizarUsuario(usuario);
            }

            return RedirectToAction(nameof(ModificarAvatar));
        }

        [HttpPost]
        public async Task<IActionResult> EliminarFoto(string avatarUrl)
        {
            var email = User.FindFirst(ClaimTypes.Name)?.Value;
            if (email == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var bucketName = "inmobilirianet.appspot.com";
            var objectName = avatarUrl.Replace(
                "https://storage.googleapis.com/inmobilirianet.appspot.com/",
                ""
            );

            try
            {
                await storageClient.DeleteObjectAsync(bucketName, objectName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la foto: {AvatarUrl}", avatarUrl);
            }

            return RedirectToAction(nameof(ModificarAvatar));
        }

        [HttpGet]
        public IActionResult ConfigurarPerfil()
        {
            var email = User.FindFirst(ClaimTypes.Name)?.Value;
            if (email == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var usuario = repo.ObtenerPorEmail(email);
            if (usuario == null)
            {
                return NotFound();
            }

            // Crea el ViewModel y lo llena con los datos del usuario
            var model = new ConfigurarPerfilViewModel
            {
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult ConfigurarPerfil(ConfigurarPerfilViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                var email = User.FindFirst(ClaimTypes.Name)?.Value;
                if (email == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var usuarioExistente = repo.ObtenerPorEmail(email);
                if (usuarioExistente == null)
                {
                    return NotFound();
                }

                usuarioExistente.Nombre = modelo.Nombre;
                usuarioExistente.Apellido = modelo.Apellido;

                // Verificar si se quiere cambiar la contraseña
                if (!string.IsNullOrEmpty(modelo.ContraseniaNueva))
                {
                    if (
                        BCrypt.Net.BCrypt.Verify(
                            modelo.ContraseniaAnterior,
                            usuarioExistente.Contrasenia
                        )
                    )
                    {
                        usuarioExistente.Contrasenia = BCrypt.Net.BCrypt.HashPassword(
                            modelo.ContraseniaNueva
                        );
                    }
                    else
                    {
                        ModelState.AddModelError(
                            "ContraseniaAnterior",
                            "La contraseña anterior es incorrecta."
                        );
                        return View(modelo);
                    }
                }

                repo.ActualizarUsuario(usuarioExistente);
                return RedirectToAction("Index", "Home");
            }

            return View(modelo);
        }
    }
}
