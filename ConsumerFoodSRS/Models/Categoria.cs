using System.ComponentModel.DataAnnotations;

namespace ConsumerFoodSRS.Models;

public class Categoria
{
    public int CategoriaId { get; set; }

    [Required(ErrorMessage = "Informe o nome da categoria")]
    [StringLength(100, ErrorMessage = "O tamanho máximo é de {1} caracteres")]
    [Display(Name = "Nome")]
    public string CategoriaNome { get; set; }

    [Required(ErrorMessage = "Informe a descrição da categoria")]
    [StringLength(200, ErrorMessage = "O tamanho máximo é de {1} caracteres")]
    [Display(Name = "Descricão")]
    public string Descricao { get; set; }

    public List<Lanche> Lanches { get; set; }
}
