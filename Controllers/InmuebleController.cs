using Microsoft.AspNetCore.Mvc;
using Inmobiliaria2Cuatri.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

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
        public IActionResult Edicion(int id)
        {
            if(id == 0)
                return View();
            else
             {
                var persona = repo.Obtener(id);
                return View(persona);
             }
        }

        // Método para manejar el envío del formulario de edicion
        [HttpPost]
        public IActionResult Edicion(Inmueble inmueble)
        {
            if (ModelState.IsValid)
            {
                repo.ActualizarInmueble(inmueble);
                return RedirectToAction(nameof(Index));
            }
            return View(inmueble);
        }

        // Método para mostrar el formulario de creación
        public IActionResult Crear()
        {
            ViewBag.Propietario = new SelectList(
                repoPropietario.ObtenerTodos(),
                "IdPropietario",
                "Direccion"
            );
          
            return View();
        }

        // Método para manejar el envío del formulario de creacion
        [HttpPost]
        public IActionResult Crear(Inmueble inmueble)
        {
            if (ModelState.IsValid)
            {
                repo.CrearInmueble(inmueble);
                return RedirectToAction(nameof(Index));
            }
            return View(inmueble);
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


