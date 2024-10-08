using System.Diagnostics;
using Inmobiliaria2Cuarti.Models;
using Inmobiliaria2Cuatri.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria2Cuatri.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RepositorioPropietario _repoPropietario;
        private readonly RepositorioInquilino _repoInquilino;
        private readonly RepositorioUsuario _repoUsuario;
        private readonly RepositorioInmueble _repoInmueble;
        private readonly RepositorioPagos _repoPagos;
        private readonly RepositorioContrato _repoContrato;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _repoPropietario = new RepositorioPropietario();
            _repoInquilino = new RepositorioInquilino();
            _repoInmueble = new RepositorioInmueble();
            _repoUsuario = new RepositorioUsuario();
            _repoInmueble = new RepositorioInmueble();
            _repoPagos = new RepositorioPagos();
            _repoContrato = new RepositorioContrato();
        }

        public IActionResult Index()
        {
            var model = new DashboardViewModel
            {
                TotalPropietarios = _repoPropietario.ObtenerTodos().Count(),
                TotalInquilinos = _repoInquilino.ObtenerTodos().Count(),
                TotalInmuebles = _repoInmueble.ObtenerTodos().Count(),
                TotalUsuarios = _repoUsuario.ObtenerTodos().Count(),
                InquilinosPorInmueble = _repoInmueble.ObtenerInquilinosPorInmueble(),
                PagosMensuales = _repoPagos.ObtenerPagosMensuales(),
                NuevosContratosPorMes = _repoContrato.ObtenerNuevosContratosPorMes(),
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(
                new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                }
            );
        }
    }
}
