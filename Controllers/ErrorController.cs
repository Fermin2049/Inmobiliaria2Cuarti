using Microsoft.AspNetCore.Mvc;

public class ErrorController : Controller
{
    [Route("Error/401")]
    public IActionResult Error401()
    {
        return View("401");
    }

    [Route("Error/404")]
    public IActionResult Error404()
    {
        return View("404");
    }

    [Route("Error/500")]
    public IActionResult Error500()
    {
        return View("500");
    }
}
