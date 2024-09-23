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
                        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                        contrato.UsuarioCreacion = userId;

                        repo.CrearContrato(contrato);
                        TempData["SuccessMessage"] = "Contrato creado correctamente.";
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {
                        TempData["ErrorMessage"] =
                            "Hubo un error al crear el contrato: " + ex.Message;
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
    }
}
