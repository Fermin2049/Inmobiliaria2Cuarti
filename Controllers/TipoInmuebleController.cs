using Inmobiliaria2Cuatri.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Inmobiliaria2Cuatri.Controllers
{
    [Authorize] // Aseguramos que solo usuarios autenticados puedan acceder
    public class TipoInmuebleController : Controller
    {
        private readonly RepositorioTipoInmueble _repoTipoInmueble;

        public TipoInmuebleController(RepositorioTipoInmueble repoTipoInmueble)
        {
            _repoTipoInmueble = repoTipoInmueble;
        }

        // Acción para mostrar el formulario de creación
        [HttpGet]
        public IActionResult CrearTipoInmueble()
        {
            return View();
        }

        // Acción para procesar el formulario de creación
        [HttpPost]
        public IActionResult CrearTipoInmueble(TipoInmueble tipoInmueble)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repoTipoInmueble.Crear(tipoInmueble);
                    TempData["SuccessMessage"] = "Tipo de Inmueble creado correctamente.";
                    return RedirectToAction("Crear", "Inmueble"); // Redirige al formulario de creación de Inmueble
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Error al crear el tipo de inmueble: {ex.Message}";
                }
            }

            return View(tipoInmueble); // Si hay errores, vuelve a mostrar el formulario
        }

        // Acción para mostrar el formulario de edición
        [HttpGet]
        public IActionResult EditarTipoInmueble(int id)
        {
            var tipoInmueble = _repoTipoInmueble.ObtenerPorId(id);
            if (tipoInmueble == null)
            {
                TempData["ErrorMessage"] = "No se encontró el tipo de inmueble.";
                return RedirectToAction("Index", "Inmueble"); // Redirige a la lista de inmuebles
            }

            return View(tipoInmueble);
        }

        // Acción para procesar el formulario de edición
        [HttpPost]
        public IActionResult EditarTipoInmueble(TipoInmueble tipoInmueble)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repoTipoInmueble.Actualizar(tipoInmueble);
                    TempData["SuccessMessage"] = "Tipo de Inmueble actualizado correctamente.";
                    return RedirectToAction("Index", "Inmueble"); // Redirige a la lista de inmuebles
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Error al actualizar el tipo de inmueble: {ex.Message}";
                }
            }

            return View(tipoInmueble);
        }

        // Acción para eliminar lógicamente un tipo de inmueble
        [HttpPost]
        public IActionResult EliminarTipoInmueble(int id)
        {
            try
            {
                bool resultado = _repoTipoInmueble.EliminarLogico(id);
                if (resultado)
                {
                    TempData["SuccessMessage"] = "Tipo de Inmueble eliminado correctamente.";
                }
                else
                {
                    TempData["ErrorMessage"] = "No se pudo eliminar el tipo de inmueble.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al eliminar el tipo de inmueble: {ex.Message}";
            }

            return RedirectToAction("Index", "Inmueble");
        }

        // Acción para listar todos los tipos de inmuebles
        [HttpGet]
        public IActionResult ListarTiposInmueble()
        {
            var lista = _repoTipoInmueble.ObtenerTodos();
            return View(lista);
        }
    }
}
