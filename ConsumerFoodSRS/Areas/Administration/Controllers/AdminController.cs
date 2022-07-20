using Microsoft.AspNetCore.Mvc;

namespace ConsumerFoodSRS.Areas.Administration.Controllers;

[Area("Administration")]
public class AdminController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}