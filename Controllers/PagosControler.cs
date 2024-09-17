using System;
using System.Threading.Tasks;
using Inmobiliaria2Cuarti.Models;
using Inmobiliaria2Cuatri.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace Inmobiliaria2Cuarti.Controllers
{
    [Authorize]
    public class PagosController : Controller
    {
        private readonly ILogger<PagosController> _logger;
        private RepositorioPagos repo;
        private RepositorioContrato repoContrato;
        private RepositorioInquilino repoInquilino;

        public PagosController(ILogger<PagosController> logger)
        {
            _logger = logger;
            repo = new RepositorioPagos();
            repoContrato = new RepositorioContrato();
            repoInquilino = new RepositorioInquilino();
        }

        public IActionResult Index()
        {
            var lista = repo.ObtenerPagossPorContrato(0); // Cambiar 0 por el id del contrato si es necesario
            return View(lista);
        }

        [HttpGet]
        public IActionResult Edicion(int id)
        {
            if (id == 0)
                return View(new Pagos());
            else
            {
                var pago = repo.ObtenerPagossPorContrato(id).FirstOrDefault();
                if (pago == null)
                {
                    return NotFound();
                }
                return View(pago);
            }
        }

        [HttpPost]
        public IActionResult Edicion(Pagos pago)
        {
            if (ModelState.IsValid)
            {
                repo.ActualizarPago(pago);
                return RedirectToAction(nameof(Index));
            }
            return View(pago);
        }

        public IActionResult Detalle(int id)
        {
            var pago = repo.ObtenerPagossPorContrato(id).FirstOrDefault();
            if (pago == null)
            {
                return NotFound();
            }
            return View(pago);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            var contratos = repoContrato.ObtenerTodos();
            var inquilinos = repoInquilino.ObtenerTodos();

            var contratosInquilinos = contratos.Join(
                inquilinos,
                contrato => contrato.IdInquilino,
                inquilino => inquilino.IdInquilino,
                (contrato, inquilino) => new { contrato.IdContrato, InquilinoDni = inquilino.Dni }
            );

            ViewBag.ContratosInquilinos = new SelectList(
                contratosInquilinos,
                "IdContrato",
                "InquilinoDni"
            );

            return View();
        }

        [HttpPost]
        public IActionResult Crear(Pagos pago)
        {
            if (ModelState.IsValid)
            {
                repo.CrearPagos(pago, "UsuarioCreacion"); // Reemplazar "UsuarioCreacion" con el usuario actual
                return RedirectToAction(nameof(Index));
            }

            var contratos = repoContrato.ObtenerTodos();
            var inquilinos = repoInquilino.ObtenerTodos();

            var contratosInquilinos = contratos.Join(
                inquilinos,
                contrato => contrato.IdInquilino,
                inquilino => inquilino.IdInquilino,
                (contrato, inquilino) => new { contrato.IdContrato, InquilinoDni = inquilino.Dni }
            );

            ViewBag.ContratosInquilinos = new SelectList(
                contratosInquilinos,
                "IdContrato",
                "InquilinoDni"
            );

            return View(pago);
        }

        public IActionResult Eliminar(int id)
        {
            if (id != 0)
            {
                repo.AnularPagos(id, "UsuarioAnulacion"); // Reemplazar "UsuarioAnulacion" con el usuario actual
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
