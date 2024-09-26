using System.Diagnostics;
using Inmobiliaria2Cuatri.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Inmobiliaria2Cuatri.Controllers
{
    [Authorize]
    public class InquilinoController : Controller
    {
        private readonly ILogger<InquilinoController> _logger;
        private RepositorioInquilino repo;

        public InquilinoController(ILogger<InquilinoController> logger)
        {
            _logger = logger;
            repo = new RepositorioInquilino();
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
                var inquilino = repo.Obtener(id);
                return View(inquilino);
            }
        }

        // Método para manejar el envío del formulario de edición
        [HttpPost]
        public IActionResult Edicion(Inquilino inquilino)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    repo.ActualizarInquilino(inquilino);
                    TempData["SuccessMessage"] = "Inquilino actualizado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] =
                        "Hubo un error al actualizar el inquilino: " + ex.Message;
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Hubo un error en la validación del formulario.";
            }
            return View(inquilino);
        }

        // Método para mostrar el formulario de creación
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Inquilino inquilino)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    repo.CrearInquilino(inquilino);
                    TempData["SuccessMessage"] = "Inquilino guardado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] =
                        "Hubo un error al guardar el inquilino: " + ex.Message;
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Hubo un error en la validación del formulario.";
            }
            return View(inquilino);
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
            var inquilino = repo.Obtener(id);
            if (inquilino == null)
            {
                return NotFound();
            }
            return View(inquilino);
        }
    }
}
