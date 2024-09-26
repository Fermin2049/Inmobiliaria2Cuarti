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

            var contratosInquilinos = contratos
                .Join(
                    inquilinos,
                    contrato => contrato.IdInquilino,
                    inquilino => inquilino.IdInquilino,
                    (contrato, inquilino) =>
                        new
                        {
                            contrato.IdContrato,
                            DatosContrato = $"{contrato.InmuebleDireccion} - {inquilino.Apellido}, {inquilino.Dni}",
                            contrato.FechaInicio,
                            contrato.FechaFin,
                            contrato.MultaTerminacionTemprana,
                        }
                )
                .ToList();

            var viewModel = new PagosViewModel();

            foreach (var contrato in contratosInquilinos)
            {
                var pagosRealizados = repo.ObtenerPagossPorContrato(contrato.IdContrato);
                var mesesNoPagados = repo.ObtenerMesesNoPagados(
                    contrato.IdContrato,
                    contrato.FechaInicio,
                    contrato.FechaFin
                );

                viewModel.PagosRealizados.AddRange(pagosRealizados);
                viewModel.MesesNoPagados.AddRange(mesesNoPagados);
                if (contrato.MultaTerminacionTemprana.HasValue)
                {
                    viewModel.MultaPendiente = contrato.MultaTerminacionTemprana;
                }
            }

            ViewBag.ContratosInquilinos = new SelectList(
                contratosInquilinos,
                "IdContrato",
                "DatosContrato"
            );

            ViewBag.PagosViewModel = viewModel;

            return View();
        }

        [HttpPost]
        public IActionResult Crear(Pagos pago)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioId = User.FindFirst(
                        System.Security.Claims.ClaimTypes.NameIdentifier
                    )?.Value;

                    if (string.IsNullOrEmpty(usuarioId))
                    {
                        TempData["ErrorMessage"] = "No se pudo obtener el ID del usuario.";
                        return View(pago);
                    }

                    repo.CrearPagos(pago, usuarioId);
                    TempData["SuccessMessage"] = "Pago creado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Hubo un error al crear el pago: " + ex.Message;
                }
            }

            var contratos = repoContrato
                .ObtenerTodos()
                .Where(c => c.Condiciones == "Nuevo" || c.Condiciones == "Renovado");
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
            var usuarioId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (id != 0)
            {
                if (!string.IsNullOrEmpty(usuarioId))
                {
                    repo.AnularPagos(id, usuarioId); // Reemplazar "UsuarioAnulacion" con el usuario actual
                }
                else
                {
                    TempData["ErrorMessage"] = "No se pudo obtener el ID del usuario.";
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [HttpGet]
        public IActionResult ObtenerDatosContrato(int idContrato)
        {
            var contrato = repoContrato.Obtener(idContrato);
            if (contrato == null)
            {
                return NotFound();
            }

            var pagosRealizados = repo.ObtenerPagossPorContrato(idContrato);
            var mesesNoPagados = repo.ObtenerMesesNoPagados(
                idContrato,
                contrato.FechaInicio,
                contrato.FechaFin
            );

            var viewModel = new PagosViewModel
            {
                PagosRealizados = pagosRealizados,
                MesesNoPagados = mesesNoPagados,
                MultaPendiente = contrato.MultaTerminacionTemprana,
            };

            return Json(viewModel);
        }

        public List<string> ObtenerMesesNoPagados(
            int idContrato,
            DateTime fechaInicioContrato,
            DateTime fechaFinContrato
        )
        {
            List<string> mesesNoPagados = new List<string>();
            List<Pagos> pagosRealizados = repo.ObtenerPagossPorContrato(idContrato);

            DateTime fechaActual = DateTime.Now;
            DateTime fechaInicio = fechaInicioContrato;

            while (fechaInicio <= fechaFinContrato)
            {
                var pago = pagosRealizados.FirstOrDefault(p =>
                    p.FechaPago.Month == fechaInicio.Month && p.FechaPago.Year == fechaInicio.Year
                );
                if (pago == null)
                {
                    mesesNoPagados.Add(fechaInicio.ToString("MMMM yyyy"));
                }
                fechaInicio = fechaInicio.AddMonths(1);
            }

            return mesesNoPagados;
        }
    }
}
