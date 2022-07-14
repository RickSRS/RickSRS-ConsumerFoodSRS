using ConsumerFoodSRS.Models;

namespace ConsumerFoodSRS.ViewModels;

public class HomeViewModel
{
    public IEnumerable<Lanche> LanchesPreferidos { get; set; }
}