using System;
using System.Linq;
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

        // Acción para listar pagos
        public IActionResult Index()
        {
            var lista = repo.ObtenerPagossPorContrato(0); // Cambiar 0 por el id del contrato si es necesario
            return View(lista);
        }

        // Acción GET para mostrar la edición de un pago
        [HttpGet]
        public IActionResult Edicion(int id)
        {
            // Obtener los contratos e inquilinos
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

            // Si id es 0, devolver un nuevo pago para crear uno
            if (id == 0)
            {
                return View(new Pagos());
            }
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

        // Acción POST para actualizar un pago existente
        [HttpPost]
        public IActionResult Edicion(Pagos pago)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool actualizado = repo.ActualizarPago(pago);
                    if (actualizado)
                    {
                        TempData["SuccessMessage"] = "Pago actualizado correctamente.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "No se pudo actualizar el pago.";
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Hubo un error al actualizar el pago: " + ex.Message;
                }
            }

            // Si no es válido, recargar los datos para los dropdowns
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

            // Retornar la vista con los errores de validación
            return View(pago);
        }

        // Acción GET para ver los detalles de un pago
        public IActionResult Detalle(int id)
        {
            var pago = repo.ObtenerPagossPorContrato(id).FirstOrDefault();
            if (pago == null)
            {
                return NotFound();
            }
            return View(pago);
        }

        // Acción GET para crear un nuevo pago
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

        // Acción POST para crear un nuevo pago
        [HttpPost]
        public IActionResult Crear(Pagos pago)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    repo.CrearPagos(pago, "UsuarioCreacion"); // Reemplazar "UsuarioCreacion" con el usuario actual
                    TempData["SuccessMessage"] = "Pago creado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Hubo un error al crear el pago: " + ex.Message;
                }
            }

            // Si no es válido, recargar los datos para los dropdowns
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

        // Acción para eliminar un pago (anulación lógica)
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
