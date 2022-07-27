using ConsumerFoodSRS.Models;

namespace ConsumerFoodSRS.ViewModels;

public class PedidoLancheViewModel
{
    public Pedido Pedido { get; set; }
    public IEnumerable<PedidoDetalhe> PedidoDetalhes { get; set; }
}