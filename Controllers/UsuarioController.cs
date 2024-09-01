using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using BCrypt.Net;
using Firebase.Storage; // Importa el namespace correcto para Firebase Storage
using Inmobiliaria2Cuarti.Models;
using Microsoft.AspNetCore.Mvc;

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
                    var stream = Avatar.OpenReadStream();
                    // Usa FirebaseStorage.net para subir la imagen
                    var task = new FirebaseStorage("gs://inmobilirianet.appspot.com")
                        .Child("avatars")
                        .Child(Avatar.FileName)
                        .PutAsync(stream);

                    usuario.Avatar = await task;
                }

                usuario.Contrasenia = BCrypt.Net.BCrypt.HashPassword(usuario.Contrasenia);
                repo.ActualizarUsuario(usuario);
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Usuario usuario, IFormFile Avatar)
        {
            if (ModelState.IsValid)
            {
                if (Avatar != null && Avatar.Length > 0)
                {
                    var stream = Avatar.OpenReadStream();
                    var task = new FirebaseStorage("gs://inmobilirianet.appspot.com")
                        .Child("avatars")
                        .Child(Avatar.FileName)
                        .PutAsync(stream);

                    usuario.Avatar = await task;
                }

                usuario.Contrasenia = BCrypt.Net.BCrypt.HashPassword(usuario.Contrasenia);
                repo.CrearUsuario(usuario);
                return RedirectToAction(nameof(Index));
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
