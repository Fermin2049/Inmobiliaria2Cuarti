using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using BCrypt.Net;
using Firebase.Storage;
using Inmobiliaria2Cuarti.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Inmobiliaria2Cuarti.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;
        private RepositorioUsuario repo;

        public UsuarioController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
            repo = new RepositorioUsuario();
        }

        public IActionResult Index()
        {
            var lista = repo.ObtenerTodos();
            return View(lista);
        }

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

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Usuario usuario, IFormFile Avatar)
        {
            if (Avatar != null && Avatar.Length > 0)
            {
                try
                {
                    var stream = Avatar.OpenReadStream();

                    // Actualizar la URL para la carga
                    var task = new FirebaseStorage("inmobilirianet.appspot.com") // Quitar el prefijo gs://
                        .Child("avatars")
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
                repo.CrearUsuario(usuario);
                return RedirectToAction(nameof(Index));
            }

            return View(usuario);
        }

        public IActionResult Detalle(int id)
        {
            var usuario = repo.Obtener(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        public IActionResult Eliminar(int id)
        {
            if (id != 0)
            {
                repo.EliminarLogico(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
