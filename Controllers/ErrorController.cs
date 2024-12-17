using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

public class ErrorController : Controller
{
    [Route("Error/404")]
    public IActionResult Error404()
    {
        Response.StatusCode = 404;
        return View("Error404");
    }

    [Route("Error/500")]
    public IActionResult Error500()
    {
        Response.StatusCode = 500;
        return View("Error500");
    }

}
