using Microsoft.AspNetCore.Mvc;
using Inmobiliaria2Cuatri.Models;
using System.Diagnostics;

namespace Inmobiliaria2Cuatri.Controllers
{
    public class InquilinoController : Controller
    {
        private readonly ILogger<InquilinoController> _logger;
        private RepositorioInquilino repo;

        public InquilinoController(ILogger<InquilinoController> logger)
        {
            _logger = logger;
            repo = new RepositorioInquilino();
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
                var inquilino = repo.Obtener(id);
                return View(inquilino);
             }
        }

        // Método para manejar el envío del formulario de edicion
        [HttpPost]
        public IActionResult Edicion(Inquilino inquilino)
        {
            if (ModelState.IsValid)
            {
                repo.ActualizarInquilino(inquilino);
                return RedirectToAction(nameof(Index));
            }
            return View(inquilino);
        }

        // Método para mostrar el formulario de creación
        public IActionResult Crear()
        {
            return View();
        }

        // Método para manejar el envío del formulario de creacion
        [HttpPost]
        public IActionResult Crear(Inquilino inquilino)
        {
            if (ModelState.IsValid)
            {
                repo.CrearInquilino(inquilino);
                return RedirectToAction(nameof(Index));
            }
            return View(inquilino);
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
            var inquilino = repo.Obtener(id);
            if (inquilino == null)
            {
                return NotFound();
            }
            return View(inquilino);
        }
    }
}


