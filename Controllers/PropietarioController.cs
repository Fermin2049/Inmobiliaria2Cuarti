using Microsoft.AspNetCore.Mvc;
using Inmobiliaria2Cuatri.Models;
using System.Diagnostics;

namespace Inmobiliaria2Cuatri.Controllers
{
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

        public IActionResult Crear()
        {
            return View();
        }

        // Método para manejar el envío del formulario
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
    }
}


