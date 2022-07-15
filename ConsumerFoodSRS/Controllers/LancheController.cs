using ConsumerFoodSRS.Models;
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

    public IActionResult List(string categoria)
    {
        IEnumerable<Lanche> lanches;
        string categoriaAtual = string.Empty;

        if (string.IsNullOrEmpty(categoria))
        {
            lanches = _lancheRepository.Lanches.OrderBy(x => x.LancheId);
        }
        else
        {
            lanches = _lancheRepository.Lanches.Where(x => x.Categoria.CategoriaNome.ToUpper().Equals(categoria.ToUpper())).OrderBy(x => x.LancheId);
            categoriaAtual = categoria;
        }

        var model = new LancheListViewModel
        {
            Lanches = lanches,
            CategoriaAtual = categoriaAtual
        };

        return View(model);
    }
}