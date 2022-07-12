using ConsumerFoodSRS.Context;
using ConsumerFoodSRS.Models;
using ConsumerFoodSRS.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConsumerFoodSRS.Repositories;

public class LancheRepository : ILancheRepository
{
    private readonly AppDbContext _context;

    public LancheRepository(AppDbContext context)
    {
        _context = context;
    }
    public IEnumerable<Lanche> Lanches => _context.Lanches.Include(x => x.Categoria);

    public IEnumerable<Lanche> LanchesPreferidos => _context.Lanches.Where(c => c.IsLanchePreferido).Include(x => x.Categoria);

    public Lanche GetLancheById(int lancheId)
    {
        return _context.Lanches.FirstOrDefault(x => x.LancheId == lancheId);
    }
}