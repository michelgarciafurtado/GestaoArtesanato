using LojaApp.Models.Enums;
using LojaApp.Models.Utils;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaApp.Models.Produtos;

public class Produto
{
    [Key]
    public string IdProduto { get; set; } =  Guid.NewGuid().ToString();
    [Display(Name = "Nome do Produto")]
    public required string NomeProduto { get; set; }
    public string? IdCategoria { get; set; }

    [ForeignKey(nameof(IdCategoria))]
    [ValidateNever]
    public Categoria Categoria { get; set; }
    [Display(Name = "Peso")]
    public int PesoProduto { get; set; }
    [Display(Name = "Medida")]
    public TpMedidaEnum medidaEnum { get; set; }
    public string? UrlImg { get; set; }
    [Precision(18, 2)]
    public decimal Preco { get; set; }
    public List<Ingrediente>? ListaIngredientes { get; set; } = new List<Ingrediente>();

    public decimal CalcularCustoMateriaPrima()
    {
        decimal valor = 0;
        try
        {
            if(ListaIngredientes?.Count > 0)
            {
                foreach (var item in ListaIngredientes)
                {
                    valor = valor + item.CalcularCustoIngrediente();
                }
                return PersonalFormatter.DecimalFormatter(Convert.ToString(valor));
            }
        }catch(Exception )
        {
            throw;
        }
        return 0;   
    }
}
