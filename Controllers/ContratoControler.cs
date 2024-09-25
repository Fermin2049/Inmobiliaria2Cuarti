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
                repoInquilino.ObtenerTodos(),
                "IdInquilino",
                "Nombre"
            );
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Contrato contrato)
        {
            if (ModelState.IsValid)
            {
                contrato.Condiciones = "Nuevo";
                if (
                    repo.ExisteSuperposicionFechas(
                        contrato.IdInmueble,
                        contrato.FechaInicio,
                        contrato.FechaFin
                    )
                )
                {
                    ModelState.AddModelError(
                        "",
                        "Las fechas del contrato se superponen con otro contrato existente."
                    );
                }
                else
                {
                    try
                    {
                        repo.CrearContrato(contrato);
                        TempData["SuccessMessage"] = "Contrato creado exitosamente.";
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Error al crear el contrato: " + ex.Message);
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

        [HttpPost]
        public async Task<IActionResult> Edicion(Contrato contrato, string terminarContrato)
        {
            if (ModelState.IsValid)
            {
                if (
                    repo.ExisteSuperposicionFechas(
                        contrato.IdInmueble,
                        contrato.FechaInicio,
                        contrato.FechaFin,
                        contrato.IdContrato
                    )
                )
                {
                    ModelState.AddModelError(
                        "",
                        "Las fechas del contrato se superponen con otro contrato existente."
                    );
                }
                else
                {
                    try
                    {
                        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                        Console.WriteLine($"User ID: {userId}"); // Punto de impresión para verificar el ID del usuario

                        if (terminarContrato == "si")
                        {
                            contrato.FechaTerminacionTemprana = DateTime.Now;
                            contrato.MultaTerminacionTemprana =
                                contrato.MultaTerminacionTemprana ?? 0;
                            contrato.UsuarioTerminacion = userId;
                        }
                        else
                        {
                            contrato.FechaTerminacionTemprana = null;
                            contrato.MultaTerminacionTemprana = null;
                            contrato.UsuarioTerminacion = null;
                        }

                        if (contrato.IdContrato == 0)
                        {
                            contrato.UsuarioCreacion = userId;
                            repo.CrearContrato(contrato);
                        }
                        else
                        {
                            repo.ActualizarContrato(contrato);
                        }

                        TempData["SuccessMessage"] = "Contrato guardado correctamente.";
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {
                        TempData["ErrorMessage"] =
                            "Hubo un error al guardar el contrato: " + ex.Message;
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

        public IActionResult Renovar(int id)
        {
            var contrato = repo.Obtener(id);
            if (contrato == null)
            {
                return NotFound();
            }
            var nuevoContrato = new Contrato
            {
                IdInmueble = contrato.IdInmueble,
                IdInquilino = contrato.IdInquilino,
                FechaInicio = contrato.FechaFin.AddDays(1),
                FechaFin = contrato.FechaFin.AddYears(1),
                MontoRenta = contrato.MontoRenta,
                Deposito = contrato.Deposito,
                Comision = contrato.Comision,
                Condiciones = contrato.Condiciones,
            };
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
            return View("Crear", nuevoContrato);
        }

        public IActionResult TerminarTemprano(int id)
        {
            var contrato = repo.Obtener(id);
            if (contrato == null)
            {
                return NotFound();
            }
            return View(contrato);
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
