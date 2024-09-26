using System.Diagnostics;
using Inmobiliaria2Cuatri.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Inmobiliaria2Cuatri.Controllers
{
    [Authorize]
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

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Propietario propietario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    repo.CrearPropietario(propietario);
                    TempData["SuccessMessage"] = "Propietario guardado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (MySqlException ex) when (ex.Number == 1062) // Código de error para duplicados
                {
                    TempData["ErrorMessage"] =
                        "Ya existe un propietario con el mismo DNI, Email o Teléfono.";
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] =
                        "Hubo un error al guardar el propietario: " + ex.Message;
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Hubo un error en la validación del formulario.";
            }
            return View(propietario);
        }

        public IActionResult Edicion(int id)
        {
            if (id == 0)
                return View();
            else
            {
                var propietario = repo.Obtener(id);
                return View(propietario);
            }
        }

        [HttpPost]
        public IActionResult Edicion(Propietario propietario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    repo.ActualizarPropietario(propietario);
                    TempData["SuccessMessage"] = "Propietario actualizado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] =
                        "Hubo un error al actualizar el propietario: " + ex.Message;
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Hubo un error en la validación del formulario.";
            }
            return View(propietario);
        }

        [Authorize(Roles = "Administrador")]
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
            var propietario = repo.Obtener(id);
            if (propietario == null)
            {
                return NotFound();
            }
            return View(propietario);
        }
    }
}
