using Microsoft.AspNetCore.Mvc;

namespace ConsumerFoodSRS.Controllers;

public class ContatoController : Controller
{
    public IActionResult Contato()
    {
        return View();
    }
}