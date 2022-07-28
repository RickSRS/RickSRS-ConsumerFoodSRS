using ConsumerFoodSRS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ConsumerFoodSRS.Areas.Administration.Controllers;

[Area("Administration")]
[Authorize(Roles = "Admin")]
public class AdminImagensController : Controller
{
    private readonly ConfigurationImagens _configurationImagens;
    private readonly IWebHostEnvironment _hostEnvironment;

    public AdminImagensController(IOptions<ConfigurationImagens> configurationImagens, IWebHostEnvironment webHostEnvironment)
    {
        _configurationImagens = configurationImagens.Value;
        _hostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
        return View();
    }
}