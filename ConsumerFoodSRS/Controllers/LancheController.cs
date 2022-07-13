using ConsumerFoodSRS.Repositories.Interfaces;
using ConsumerFoodSRS.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ConsumerFoodSRS.Controllers;

public class LancheController : Controller
{
    private readonly ILancheRepository _lancheRepository;

    public LancheController(ILancheRepository lancheRepository)
    {
        _lancheRepository = lancheRepository;
    }

    public IActionResult List()
    {
        var model = new LancheListViewModel();
        model.Lanches = _lancheRepository.Lanches;
        model.CategoriaAtual = "Categoria Atual";

        return View(model);
    }
}