using ConsumerFoodSRS.Models;

namespace ConsumerFoodSRS.Repositories.Interfaces;

public interface ICategoriaRepository
{
    IEnumerable<Categoria> Categorias { get; }
}