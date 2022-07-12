using ConsumerFoodSRS.Context;
using ConsumerFoodSRS.Models;
using ConsumerFoodSRS.Repositories.Interfaces;

namespace ConsumerFoodSRS.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext _context;

    public CategoriaRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Categoria> Categorias => _context.Categorias;
}
