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
    }
}
