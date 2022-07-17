using ConsumerFoodSRS.Models;
using ConsumerFoodSRS.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConsumerFoodSRS.Controllers;

public class PedidoController : Controller
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly CarrinhoCompra _carrinhoCompra;

    public PedidoController(IPedidoRepository pedidoRepository, CarrinhoCompra carrinhoCompra)
    {
        _pedidoRepository = pedidoRepository;
        _carrinhoCompra = carrinhoCompra;
    }

    public IActionResult Checkout()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Checkout(Pedido pedido)
    {
        int totalItensPedido = 0;
        decimal precoTotalPedido = 0.0m;

        List<CarrinhoCompraItem> itens = _carrinhoCompra.GetCarrinhoCompraItens();
        _carrinhoCompra.CarrinhoCompraItens = itens;

        if (!_carrinhoCompra.CarrinhoCompraItens.Any())
        {
            ModelState.AddModelError("", "Carrinho vazio, adicione ao menos um produto nele.");
        }

        foreach (var item in itens)
        {
            totalItensPedido += item.Quantidade;
            precoTotalPedido += (item.Lanche.Preco * item.Quantidade);
        }

        pedido.TotalItensPedido = totalItensPedido;
        pedido.PedidoTotal = precoTotalPedido;

        if (ModelState.IsValid)
        {
            _pedidoRepository.CriarPedido(pedido);
            ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido.";
            ViewBag.TotalPedido = _carrinhoCompra.GetCarrinhoTotal();

            _carrinhoCompra.LimparCarrinho();

            return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedido);
        }

        return View(pedido);
    }
}