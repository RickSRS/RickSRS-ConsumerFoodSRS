using ConsumerFoodSRS.Models;
using ConsumerFoodSRS.Repositories.Interfaces;
using ConsumerFoodSRS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ConsumerFoodSRS.Controllers;

public class HomeController : Controller
{
    private readonly ILancheRepository _lancheRepository;

    public HomeController(ILancheRepository lancheRepository)
    {
        _lancheRepository = lancheRepository;
    }

    public IActionResult Index()
    {
        var model = new HomeViewModel
        {
            LanchesPreferidos = _lancheRepository.LanchesPreferidos
        };

        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}