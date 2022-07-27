using ConsumerFoodSRS.Context;
using ConsumerFoodSRS.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsumerFoodSRS.Areas.Administration.Services;

public class RelatorioVendasService
{
    private readonly AppDbContext _context;

    public RelatorioVendasService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Pedido>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
    {
        var result = _context.Pedidos.Select(x => x);

        if (minDate.HasValue)
        {
            result = result.Where(x => x.PedidoEnviado >= minDate.Value);
        }
        if (maxDate.HasValue)
        {
            result = result.Where(x => x.PedidoEnviado <= maxDate.Value);
        }

        return await result.Include(l => l.PedidoItens)
                           .ThenInclude(x => x.Lanche)
                           .OrderByDescending(c => c.PedidoEnviado)
                           .ToListAsync();
    }
}