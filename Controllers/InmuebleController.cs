using System.Diagnostics;
using System.Linq;
using Inmobiliaria2Cuatri.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Inmobiliaria2Cuatri.Controllers
{
    [Authorize]
    public class InmuebleController : Controller
    {
        private readonly ILogger<InmuebleController> _logger;
        private RepositorioInmueble repo;
        private RepositorioPropietario repoPropietario;
        private RepositorioTipoInmueble repoTipoInmueble;

        public InmuebleController(ILogger<InmuebleController> logger)
        {
            _logger = logger;
            repo = new RepositorioInmueble();
            repoPropietario = new RepositorioPropietario();
            repoTipoInmueble = new RepositorioTipoInmueble();
        }

        public IActionResult Index()
        {
            var lista = repo.ObtenerTodos();
            return View(lista);
        }

        // Método para mostrar el formulario de edición
        [HttpGet]
        public IActionResult Edicion(int id)
        {
            if (id == 0)
                return RedirectToAction(nameof(Index)); // Redirige si no se proporciona un ID válido

            var inmueble = repo.Obtener(id);
            if (inmueble == null)
            {
                _logger.LogWarning($"No se encontró un inmueble con ID: {id}");
                return RedirectToAction(nameof(Index)); // Redirige si no se encuentra el inmueble
            }

            // Cargar la lista de propietarios para el dropdown
            var listaPropietarios = repoPropietario.ObtenerTodos();
            ViewBag.PropietariosLista = listaPropietarios
                .Select(p => new SelectListItem
                {
                    Value = p.IdPropietario.ToString(),
                    Text = $"{p.Nombre} {p.Apellido}",
                })
                .ToList();

            var tiposInmueble = repoTipoInmueble?.ObtenerTodos();
            ViewBag.TiposInmueble = tiposInmueble
                ?.Select(t => new SelectListItem
                {
                    Value = t.IdTipoInmueble.ToString(),
                    Text = t.Nombre,
                    Selected = t.IdTipoInmueble == inmueble.IdTipoInmueble // Seleccionar el tipo actual del inmueble
                })
                .ToList();

                return View(inmueble); // Pasar el inmueble a la vista para ser editado
        }

        // Método para manejar el envío del formulario de edición
        [HttpPost]
        public IActionResult Edicion(Inmueble inmueble)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    repo.ActualizarInmueble(inmueble);
                    TempData["SuccessMessage"] = "Inmueble actualizado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error al actualizar el inmueble: {ex.Message}");
                    TempData["ErrorMessage"] =
                        "No se pudo actualizar el inmueble. Por favor, intente de nuevo.";
                }
            }

            // Recarga los ViewBag en caso de error de validación
            var listaPropietarios = repoPropietario.ObtenerTodos();
            ViewBag.PropietariosLista = listaPropietarios
                .Select(p => new SelectListItem
                {
                    Value = p.IdPropietario.ToString(),
                    Text = $"{p.Nombre} {p.Apellido}",
                })
                .ToList();
                
                var tiposinmueble = repoTipoInmueble?.ObtenerTodos();
            ViewBag.TiposInmueble = tiposinmueble
                ?.Select(p => new SelectListItem
                {
                    Value = p.IdTipoInmueble.ToString(),
                    Text = $"{p.Nombre}",
                })
                .ToList();

            return View(inmueble); // Devolver la vista de edición con los datos actuales y el mensaje de error
        }

        // Método para mostrar el formulario de creación
        [HttpGet]
        public IActionResult Crear()
        {
            var listaPropietarios = repoPropietario.ObtenerTodos();
            ViewBag.PropietariosLista = listaPropietarios
                .Select(p => new SelectListItem
                {
                    Value = p.IdPropietario.ToString(),
                    Text = $"{p.Apellido} {p.Dni}",
                })
                .ToList();

            var tiposinmueble = repoTipoInmueble?.ObtenerTodos();
            ViewBag.TiposInmueble = tiposinmueble
                ?.Select(p => new SelectListItem
                {
                    Value = p.IdTipoInmueble.ToString(),
                    Text = $"{p.Nombre}",
                })
                .ToList();

            return View();
        }

        // Método para manejar el envío del formulario de creación
        [HttpPost]
        public IActionResult Crear(Inmueble inmueble)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    repo.CrearInmueble(inmueble);
                    TempData["SuccessMessage"] = "Inmueble creado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Hubo un error al crear el inmueble: " + ex.Message;
                }
            }

            // Recarga los ViewBag en caso de error de validación
            var listaPropietarios = repoPropietario.ObtenerTodos();
            ViewBag.PropietariosLista = listaPropietarios
                .Select(p => new SelectListItem
                {
                    Value = p.IdPropietario.ToString(),
                    Text = $"{p.Apellido} {p.Dni}",
                })
                .ToList();

            return View(inmueble);
        }

        public IActionResult Detalle(int id)
        {
            var inmueble = repo.Obtener(id);
            if (inmueble == null)
            {
                return NotFound();
            }
            return View(inmueble);
        }
    }
}
