using System.Diagnostics;
using Inmobiliaria2Cuatri.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria2Cuatri.Controllers
{
    [Authorize]
    public class PropietarioController : Controller
    {
        private readonly ILogger<PropietarioController> _logger;
        private RepositorioPropietario repo;

        public PropietarioController(ILogger<PropietarioController> logger)
        {
            _logger = logger;
            repo = new RepositorioPropietario();
        }

        public IActionResult Index()
        {
            var lista = repo.ObtenerTodos();
            return View(lista);
        }

        // Método para mostrar el formulario de edición
        public IActionResult Edicion(int id)
        {
            if (id == 0)
                return View();
            else
            {
                var persona = repo.Obtener(id);
                return View(persona);
            }
        }

        // Método para manejar el envío del formulario de edicion
        [HttpPost]
        public IActionResult Edicion(Propietario propietario)
        {
            if (ModelState.IsValid)
            {
                repo.ActualizarPropietario(propietario);
                return RedirectToAction(nameof(Index));
            }
            return View(propietario);
        }

        // Método para mostrar el formulario de creación
        public IActionResult Crear()
        {
            return View();
        }

        // Método para manejar el envío del formulario de creacion
        [HttpPost]
        public IActionResult Crear(Propietario propietario)
        {
            if (ModelState.IsValid)
            {
                repo.CrearPropietario(propietario);
                return RedirectToAction(nameof(Index));
            }
            return View(propietario);
        }

        public IActionResult Eliminar(int id)
        {
            if (id != 0)
            {
                repo.EliminarLogico(id);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detalle(int id)
        {
            var propietario = repo.Obtener(id);
            if (propietario == null)
            {
                return NotFound();
            }
            return View(propietario);
        }
    }
}
