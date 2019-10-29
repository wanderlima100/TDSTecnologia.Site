

using Microsoft.AspNetCore.Mvc;

public class UsuarioController : Controller
{

    [HttpGet]
    public IActionResult Contato()
    {
        return View();
    }
}