using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConsumerFoodSRS.Areas.Administration.Controllers;

[Area("Administration")]
[Authorize]
public class AdminController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}