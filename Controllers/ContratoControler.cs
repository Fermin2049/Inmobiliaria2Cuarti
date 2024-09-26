using System.Diagnostics;
using System.Security.Claims;
using Inmobiliaria2Cuarti.Models;
using Inmobiliaria2Cuatri.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Inmobiliaria2Cuarti.Controllers
{
    [Authorize]
    public class ContratoController : Controller
    {
        private readonly ILogger<ContratoController> _logger;
        private RepositorioContrato repo;
        private RepositorioInmueble repoInmueble;
        private RepositorioInquilino repoInquilino;
        private RepositorioPagos repoPagos;

        public ContratoController(ILogger<ContratoController> logger)
        {
            _logger = logger;
            repo = new RepositorioContrato();
            repoInmueble = new RepositorioInmueble();
            repoInquilino = new RepositorioInquilino();
            repoPagos = new RepositorioPagos();
        }

        public IActionResult Index()
        {
            var lista = repo.ObtenerTodos();
            return View(lista);
        }

        // Método para mostrar el formulario de edición
        public IActionResult Edicion(int id)
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
            if (id == 0)
            {
                return View(new Contrato());
            }
            else
            {
                var contrato = repo.Obtener(id);
                return View(contrato);
            }
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
                repoInquilino
                    .ObtenerTodos()
                    .Select(i => new
                    {
                        i.IdInquilino,
                        NombreCompleto = $"{i.Dni} - {i.Apellido}, {i.Nombre}",
                    }),
                "IdInquilino",
                "NombreCompleto"
            );
            return View();
        }

        [HttpPost]
        [HttpPost]
        public IActionResult Crear(Contrato contrato)
        {
            if (ModelState.IsValid)
            {
                var idUsuario = User.FindFirstValue(ClaimTypes.NameIdentifier);
                contrato.Condiciones = "Nuevo";
                contrato.UsuarioCreacion = idUsuario;
                var contratosSuperpuestos = repo.ObtenerContratosSuperpuestos(
                    contrato.IdInmueble,
                    contrato.FechaInicio,
                    contrato.FechaFin
                );

                if (contratosSuperpuestos.Any())
                {
                    var fechasSuperpuestas = string.Join(
                        ", ",
                        contratosSuperpuestos.Select(c =>
                            $"{c.FechaInicio.ToShortDateString()} - {c.FechaFin.ToShortDateString()}"
                        )
                    );
                    ModelState.AddModelError(
                        "",
                        $"Las fechas del contrato se superponen con otros contratos existentes: {fechasSuperpuestas}."
                    );
                }
                else
                {
                    try
                    {
                        repo.CrearContrato(contrato);
                        TempData["SuccessMessage"] = "Contrato creado exitosamente.";
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error al crear el contrato.");
                        ModelState.AddModelError("", "Ocurrió un error al crear el contrato.");
                    }
                }
            }

            ViewBag.Inmuebles = new SelectList(
                repoInmueble.ObtenerTodos(),
                "IdInmueble",
                "Direccion"
            );
            ViewBag.Inquilinos = new SelectList(
                repoInquilino
                    .ObtenerTodos()
                    .Select(i => new
                    {
                        i.IdInquilino,
                        NombreCompleto = $"{i.Dni} - {i.Apellido}, {i.Nombre}",
                    }),
                "IdInquilino",
                "NombreCompleto"
            );
            return View(contrato);
        }

        [HttpPost]
        public async Task<IActionResult> Edicion(Contrato contrato)
        {
            if (ModelState.IsValid)
            {
                var contratosSuperpuestos = repo.ObtenerContratosSuperpuestos(
                    contrato.IdInmueble,
                    contrato.FechaInicio,
                    contrato.FechaFin
                );

                if (contratosSuperpuestos.Any())
                {
                    var fechasSuperpuestas = string.Join(
                        ", ",
                        contratosSuperpuestos.Select(c =>
                            $"{c.FechaInicio.ToShortDateString()} - {c.FechaFin.ToShortDateString()}"
                        )
                    );
                    ModelState.AddModelError(
                        "",
                        $"Las fechas del contrato se superponen con otros contratos existentes: {fechasSuperpuestas}."
                    );
                }
                else
                {
                    try
                    {
                        if (contrato.IdContrato == 0)
                        {
                            contrato.Condiciones = "Nuevo";
                            repo.CrearContrato(contrato);
                        }
                        else
                        {
                            contrato.Condiciones = "Renovado";
                            repo.ActualizarContrato(contrato);
                        }
                        TempData["SuccessMessage"] = "Contrato guardado exitosamente.";
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error al guardar el contrato.");
                        ModelState.AddModelError("", "Ocurrió un error al guardar el contrato.");
                    }
                }
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

        public IActionResult FiltrarPorPlazo(int plazo)
        {
            var lista = repo.ObtenerPorPlazo(plazo);
            ViewBag.PlazoSeleccionado = plazo;
            return View("Index", lista);
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
            var contrato = repo.Obtener(id);
            if (contrato == null)
            {
                return NotFound();
            }
            return View(contrato);
        }

        public IActionResult InmueblesNoOcupados(DateTime fechaInicio, DateTime fechaFin)
        {
            var inmuebles = repo.ObtenerInmueblesNoOcupados(fechaInicio, fechaFin);
            return View(inmuebles);
        }

        [HttpPost]
        public IActionResult TerminarTemprano(int id, decimal multa)
        {
            var contrato = repo.Obtener(id);
            if (contrato == null)
            {
                return NotFound();
            }
            contrato.MultaTerminacionTemprana = multa;
            contrato.FechaTerminacionTemprana = DateTime.Now;
            contrato.UsuarioTerminacion = User.Identity.Name;
            repo.ActualizarContrato(contrato);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult FiltrarInmueblesNoOcupados(DateTime fechaInicio, DateTime fechaFin)
        {
            var inmuebles = repo.ObtenerInmueblesNoOcupados(fechaInicio, fechaFin);
            return View("InmueblesNoOcupados", inmuebles);
        }

        public IActionResult CalcularMulta(int id, bool conAviso)
        {
            var contrato = repo.Obtener(id);
            if (contrato == null)
            {
                return Json(new { success = false, message = "El contrato no fue encontrado." });
            }

            // Verificación 1: Si el contrato ya está cancelado
            if (contrato.Condiciones == "Cancelado")
            {
                return Json(new { success = false, message = "El contrato ya está cancelado." });
            }

            // Verificación 2: Si el contrato ya no está vigente
            if (contrato.FechaFin < DateTime.Now)
            {
                return Json(
                    new
                    {
                        success = false,
                        message = "El contrato no está vigente, su fecha de fin ya pasó.",
                    }
                );
            }

            // Cálculo de la multa
            var mesesVigencia = (DateTime.Now - contrato.FechaInicio).Days / 30;
            decimal multa = 0;
            string message = "";

            // Escenario 1: Rescindir antes de los primeros seis meses
            if (mesesVigencia < 6)
            {
                int mesesRestantes = 6 - mesesVigencia;
                multa = contrato.MontoRenta * mesesRestantes;
                message =
                    $"No puedes cancelar el contrato antes de los 6 meses. Debes pagar los alquileres restantes: {multa:C}.";
            }
            // Escenario 2: Rescindir entre los seis y doce meses
            else if (mesesVigencia >= 6 && mesesVigencia <= 12)
            {
                if (!conAviso || (contrato.FechaFin - DateTime.Now).Days < 90)
                {
                    multa = contrato.MontoRenta * 1.5m; // Multa de 1.5 meses de alquiler
                    message = $"Multa de 1.5 meses de alquiler: {multa:C}.";
                }
                else
                {
                    multa = 0; // Sin multa si se avisa con 3 meses de anticipación
                    message = "No hay multa ya que diste aviso con antelación.";
                }
            }
            // Escenario 3: Rescindir después del primer año
            else if (mesesVigencia > 12)
            {
                if (!conAviso || (contrato.FechaFin - DateTime.Now).Days < 90)
                {
                    multa = contrato.MontoRenta; // Multa de 1 mes de alquiler
                    message = $"Multa de 1 mes de alquiler: {multa:C}.";
                }
                else
                {
                    multa = 0; // Sin multa si se avisa con 3 meses de anticipación
                    message = "No hay multa ya que diste aviso con antelación.";
                }
            }

            return Json(
                new
                {
                    success = true,
                    multa,
                    message,
                }
            );
        }

        [HttpPost]
        public IActionResult ProcesarCancelacion(
            int id,
            string metodoPago,
            bool conAviso,
            decimal multa
        )
        {
            var contrato = repo.Obtener(id);
            if (contrato == null)
            {
                return Json(new { success = false, message = "El contrato no fue encontrado." });
            }

            // Obtenemos el id del usuario actual desde los claims
            string usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var pago = new Pagos
            {
                IdContrato = contrato.IdContrato,
                FechaPago = DateTime.Now,
                Importe = multa, // Utilizamos el valor de la multa calculada
                Detalle = metodoPago ?? "Método no especificado",
                Estado = true,
                UsuarioCreacion = usuarioId,
            };

            try
            {
                // Crear el pago con la multa como importe
                repoPagos.CrearPago2(pago);

                // Actualización del contrato
                contrato.Condiciones = "Cancelado";
                contrato.MultaTerminacionTemprana = multa; // La multa también se guarda en el contrato
                contrato.FechaTerminacionTemprana = DateTime.Now;
                contrato.UsuarioTerminacion = usuarioId;

                repo.ActualizarContrato(contrato);

                return Json(
                    new
                    {
                        success = true,
                        message = "Contrato cancelado y pago registrado correctamente.",
                    }
                );
            }
            catch (Exception ex)
            {
                return Json(
                    new
                    {
                        success = false,
                        message = $"Error al procesar la cancelación: {ex.Message}",
                    }
                );
            }
        }
    }
}
