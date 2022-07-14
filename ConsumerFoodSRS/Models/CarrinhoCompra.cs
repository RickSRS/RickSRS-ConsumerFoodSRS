using ConsumerFoodSRS.Context;
using Microsoft.EntityFrameworkCore;

namespace ConsumerFoodSRS.Models;

public class CarrinhoCompra
{
    private readonly AppDbContext _context;

    public CarrinhoCompra(AppDbContext context)
    {
        _context = context;
    }

    public string CarrinhoCompraId { get; set; }
    public List<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }

    public static CarrinhoCompra GetCarrinho(IServiceProvider services)
    {
        //Define uma session
        ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
        //Obtem um serviço do tipo nosso contexto
        var context = services.GetService<AppDbContext>();
        //Obtem ou gera um novo id do carrinho
        string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();
        //Atribui o id co carrinho na session
        session.SetString("CarrinhoId", carrinhoId);

        return new CarrinhoCompra(context)
        {
            CarrinhoCompraId = carrinhoId
        };
    }

    public void AdicionarAoCarrinho(Lanche lanche)
    {
        var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault
                                                              (
                                                                x => x.Lanche.LancheId == lanche.LancheId &&
                                                                x.CarrinhoCompraId == CarrinhoCompraId
                                                              );
        if (carrinhoCompraItem == null)
        {
            carrinhoCompraItem = new CarrinhoCompraItem
            {
                CarrinhoCompraId = CarrinhoCompraId,
                Lanche = lanche,
                Quantidade = 1
            };
            _context.CarrinhoCompraItens.Add(carrinhoCompraItem);
        }
        else
        {
            carrinhoCompraItem.Quantidade++;
        }

        _context.SaveChanges();
    }

    public int RemoverDoCarrinho(Lanche lanche)
    {
        var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault
                                                              (
                                                                x => x.Lanche.LancheId == lanche.LancheId &&
                                                                x.CarrinhoCompraId == CarrinhoCompraId
                                                              );
        int qtdeLocal = 0;
        if(carrinhoCompraItem != null)
        {
            if(carrinhoCompraItem.Quantidade > 1)
            {
                carrinhoCompraItem.Quantidade--;
                qtdeLocal = carrinhoCompraItem.Quantidade;
            }
            else
            {
                _context.CarrinhoCompraItens.Remove(carrinhoCompraItem);
            }
        }
        _context.SaveChanges();
        return qtdeLocal;
    }

    public List<CarrinhoCompraItem> GetCarrinhoCompraItens()
    {
        return CarrinhoCompraItens ?? (CarrinhoCompraItens = _context.CarrinhoCompraItens.Where(x => x.CarrinhoCompraId == CarrinhoCompraId)
                                                                                         .Include(c => c.Lanche)
                                                                                         .ToList());
    }

    public void LimparCarrinho()
    {
        var carrinhoItens = _context.CarrinhoCompraItens.Where(x => x.CarrinhoCompraId == CarrinhoCompraId);

        _context.CarrinhoCompraItens.RemoveRange(carrinhoItens);
        _context.SaveChanges();
    }

    public decimal GetCarrinhoTotal()
    {
        var total = _context.CarrinhoCompraItens.Where(x => x.CarrinhoCompraId == CarrinhoCompraId)
                                                .Select(c => c.Lanche.Preco * c.Quantidade).Sum();
        return total;
    }
}