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

        [HttpPost]
        public IActionResult Crear(int id, Propietario propietario)
        {
            if(id == 0)
            {
                repo.CrearPropietario(propietario);  
            }
            else
             {
                repo.ActualizarPropietario(propietario);
             }

            return RedirectToAction(nameof(Index));
        }
    }
}
