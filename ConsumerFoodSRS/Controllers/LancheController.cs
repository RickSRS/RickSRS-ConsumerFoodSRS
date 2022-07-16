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
            categoriaAtual = "Cardápio de Lanches";
        }
        else
        {
            lanches = _lancheRepository.Lanches.Where(x => x.Categoria.CategoriaNome.ToUpper().Equals(categoria.ToUpper())).OrderBy(x => x.LancheId);
            //Format string - Caso o usuario tente pesquisa pela propria URL, fazer com que exiba de uma forma bonita
            string categoriaFormatada = categoria.Substring(0, 1).ToUpper() + categoria.Substring(1, (categoria.Length - 1)).ToLower();
            categoriaAtual = lanches.Any() ? $"Cardápio de Lanches - {categoriaFormatada}" : $"Lanches não encontrados.";
        }

        var model = new LancheListViewModel
        {
            Lanches = lanches,
            CategoriaAtual = categoriaAtual
        };

        return View(model);
    }

    public IActionResult Details(int lancheId)
    {
        var lanche = _lancheRepository.Lanches.FirstOrDefault(x => x.LancheId == lancheId);
        return View(lanche);
    }

    public ViewResult Busca(string searchString)
    {
        IEnumerable<Lanche> lanches;
        string categoriaAtual = string.Empty;

        if (string.IsNullOrEmpty(searchString))
        {
            lanches = _lancheRepository.Lanches.OrderBy(x => x.LancheId);
            categoriaAtual = "Cardápio de Lanches";
        }
        else
        {
            lanches = _lancheRepository.Lanches.Where(x => x.Nome.ToUpper().Contains(searchString.ToUpper())).OrderBy(x => x.LancheId);
            string categoriaFormatada = searchString.Substring(0, 1).ToUpper() + searchString.Substring(1, (searchString.Length - 1)).ToLower();
            categoriaAtual = lanches.Any() ? $"Cardápio de Lanches - {categoriaFormatada}" : $"Lanches não encontrados.";
        }

        return View("~/Views/Lanche/List.cshtml", new LancheListViewModel
        {
            Lanches = lanches,
            CategoriaAtual = categoriaAtual
        });
    }
}