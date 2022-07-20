using Microsoft.AspNetCore.Mvc;

namespace ConsumerFoodSRS.Controllers;

public class ContatoController : Controller
{
    public IActionResult Contato()
    {
        if (User.Identity.IsAuthenticated)
        {
            return View();
        }
        return RedirectToAction("Login", "Account");
    }
}