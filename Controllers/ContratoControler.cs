using System.Diagnostics;
using Inmobiliaria2Cuarti.Models;
using Inmobiliaria2Cuatri.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Inmobiliaria2Cuarti.Controllers
{
    public class ContratoController : Controller
    {
        private readonly ILogger<ContratoController> _logger;
        private RepositorioContrato repo;
        private RepositorioInmueble repoInmueble;
        private RepositorioInquilino repoInquilino;

        public ContratoController(ILogger<ContratoController> logger)
        {
            _logger = logger;
            repo = new RepositorioContrato();
            repoInmueble = new RepositorioInmueble();
            repoInquilino = new RepositorioInquilino();
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
                var contrato = repo.Obtener(id);
                return View(contrato);
            }
        }

        // Método para manejar el envío del formulario de edicion
        [HttpPost]
        public IActionResult Edicion(Contrato contrato)
        {
            if (ModelState.IsValid)
            {
                repo.ActualizarContrato(contrato);
                return RedirectToAction(nameof(Index));
            }
            return View(contrato);
        }

        // Método para mostrar el formulario de creación
        public IActionResult Crear()
        {
            ViewBag.Inmuebles = new SelectList(
                repoInmueble.ObtenerTodos(),
                "IdInmueble",
                "Direccion"
            );
            ViewBag.Inquilinos = new SelectList(
                repoInquilino.ObtenerTodos(),
                "IdInquilino",
                "Nombre"
            );
            return View();
        }

        public IActionResult FiltrarPorPlazo(int plazo)
        {
            var lista = repo.ObtenerPorPlazo(plazo);
            ViewBag.PlazoSeleccionado = plazo;
            return View("Index", lista);
        }

        // Método para manejar el envío del formulario de creacion
        [HttpPost]
        public IActionResult Crear(Contrato contrato)
        {
            if (ModelState.IsValid)
            {
                repo.CrearContrato(contrato);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Inmuebles = new SelectList(
                repoInmueble.ObtenerTodos(),
                "IdInmueble",
                "Direccion"
            );
            ViewBag.Inquilinos = new SelectList(
                repoInquilino.ObtenerTodos(),
                "IdInquilino",
                "Nombre"
            );
            return View(contrato);
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
