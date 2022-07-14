using ConsumerFoodSRS.Models;
using ConsumerFoodSRS.Repositories.Interfaces;
using ConsumerFoodSRS.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ConsumerFoodSRS.Controllers;

public class CarrinhoCompraController : Controller
{
    private readonly ILancheRepository _lancheRepository;
    private readonly CarrinhoCompra _carrinhoCompra;

    public CarrinhoCompraController(ILancheRepository lancheRepository, CarrinhoCompra carrinhoCompra)
    {
        _lancheRepository = lancheRepository;
        _carrinhoCompra = carrinhoCompra;
    }

    public IActionResult Carrinho()
    {
        var itens = _carrinhoCompra.GetCarrinhoCompraItens();
        _carrinhoCompra.CarrinhoCompraItens = itens;

        var carrinhoCompraVM = new CarrinhoCompraViewModel
        {
            CarrinhoCompra = _carrinhoCompra,
            CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoTotal()
        };

        return View(carrinhoCompraVM);
    }

    public RedirectToActionResult AdicionarCarrinhoCompra(int lancheId)
    {
        var lancheSelecionado = _lancheRepository.Lanches.FirstOrDefault(x => x.LancheId == lancheId);

        if(lancheSelecionado != null)
        {
            _carrinhoCompra.AdicionarAoCarrinho(lancheSelecionado);
        }

        return RedirectToAction("Carrinho");
    }

    public RedirectToActionResult RemoverItemCarrinhoCompra(int lancheId)
    {
        var lancheSelecionado = _lancheRepository.Lanches.FirstOrDefault(x => x.LancheId == lancheId);

        if (lancheSelecionado != null)
        {
            _carrinhoCompra.RemoverDoCarrinho(lancheSelecionado);
        }

        return RedirectToAction("Carrinho");
    }
}