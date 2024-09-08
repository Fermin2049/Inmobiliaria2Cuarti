using Microsoft.AspNetCore.Mvc;
using Inmobiliaria2Cuatri.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Linq;

namespace Inmobiliaria2Cuatri.Controllers
{
    public class InmuebleController : Controller
    {
        private readonly ILogger<InmuebleController> _logger;
        private RepositorioInmueble repo;
        private RepositorioPropietario repoPropietario;

        public InmuebleController(ILogger<InmuebleController> logger)
        {
            _logger = logger;
            repo = new RepositorioInmueble();
            repoPropietario = new RepositorioPropietario();
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
            ViewBag.PropietariosLista = listaPropietarios.Select(p => new SelectListItem
            {
                Value = p.IdPropietario.ToString(),
                Text = $"{p.Nombre} {p.Apellido}"
            }).ToList();

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
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error al actualizar el inmueble: {ex.Message}");
                    ModelState.AddModelError(string.Empty, "No se pudo actualizar el inmueble. Por favor, intente de nuevo.");
                }
            }

            // Recarga los ViewBag en caso de error de validación
            var listaPropietarios = repoPropietario.ObtenerTodos();
            ViewBag.PropietariosLista = listaPropietarios.Select(p => new SelectListItem
            {
                Value = p.IdPropietario.ToString(),
                Text = $"{p.Nombre} {p.Apellido}"
            }).ToList();

            return View(inmueble); // Devolver la vista de edición con los datos actuales y el mensaje de error
        }

        // Método para mostrar el formulario de creación
        [HttpGet]
        public IActionResult Crear()
        {
            var listaPropietarios = repoPropietario.ObtenerTodos();
            ViewBag.PropietariosLista = listaPropietarios.Select(p => new SelectListItem
            {
                Value = p.IdPropietario.ToString(),
                Text = $"{p.Nombre} {p.Apellido}"
            }).ToList();

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
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error al crear el inmueble: {ex.Message}");
                    ModelState.AddModelError(string.Empty, "No se pudo crear el inmueble. Por favor, intente de nuevo.");
                }
            }

            // Recarga los ViewBag en caso de error de validación
            var listaPropietarios = repoPropietario.ObtenerTodos();
            ViewBag.PropietariosLista = listaPropietarios.Select(p => new SelectListItem
            {
                Value = p.IdPropietario.ToString(),
                Text = $"{p.Nombre} {p.Apellido}"
            }).ToList();

            return View(inmueble);
        }

        public IActionResult Eliminar(int id)
        {
            if (id != 0)
            {
                try
                {
                    repo.EliminarLogico(id);
                    _logger.LogInformation($"Inmueble con ID: {id} eliminado lógicamente.");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error al eliminar el inmueble: {ex.Message}");
                    ModelState.AddModelError(string.Empty, "No se pudo eliminar el inmueble. Por favor, intente de nuevo.");
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
