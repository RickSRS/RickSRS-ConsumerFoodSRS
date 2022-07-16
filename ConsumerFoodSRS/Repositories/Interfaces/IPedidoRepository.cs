using ConsumerFoodSRS.Models;

namespace ConsumerFoodSRS.Repositories.Interfaces;

public interface IPedidoRepository
{
    public void CriarPedido(Pedido pedido);
}