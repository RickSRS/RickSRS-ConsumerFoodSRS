using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsumerFoodSRS.Models;

public class Lanche
{
    public int LancheId { get; set; }

    [Required(ErrorMessage = "Informe o nome do produto")]
    [StringLength(100, ErrorMessage = "O tamanho máximo é de {1} caracteres")]
    [Display(Name = "Nome")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Informe uma descrição para o produto")]
    [StringLength(150, ErrorMessage = "O tamanho máximo é de {1} caracteres")]
    [Display(Name = "Descrição")]
    public string DescricaoCurta { get; set; }

    [StringLength(300, ErrorMessage = "O tamanho máximo é de {1} caracteres")]
    [Display(Name = "Descrição Detalhada")]
    public string DescricaoDetalhada { get; set; }

    [Required(ErrorMessage = "Informe um preço ao produto")]
    [Column(TypeName = "decimal(10,2)")]
    [Display(Name = "Preço")]
    [Range(1,999.99, ErrorMessage = "Preço deve estar entre 1 e 999.99")]
    public decimal Preco { get; set; }

    [StringLength(200, ErrorMessage = "O tamanho máximo é de {1} caracteres")]
    [Display(Name = "Caminho da Imagem")]
    public string ImagemUrl { get; set; }

    [StringLength(200, ErrorMessage = "O tamanho máximo é de {1} caracteres")]
    [Display(Name = "Caminho da Thumbnail")]
    public string ImagemThumbnailUrl { get; set; }

    [Display(Name = "Preferido")]
    public bool IsLanchePreferido { get; set; }

    [Display(Name = "Estoque")]
    public bool EmEstoque { get; set; }

    public int CategoriaId { get; set; }
    public virtual Categoria Categoria { get; set; }
}

