using LojaApp.Models.Enums;
using LojaApp.Models.Utils;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace LojaApp.Models.Produtos;

public class Produto
{
    [Key]
    public string IdProduto { get; set; } =  Guid.NewGuid().ToString();
    [Display(Name = "Nome do Produto")]
    public required string NomeProduto { get; set; }
    public string? Descricao { get; set; }
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
    [Display(Name = "Preço. R$")]
    public decimal Preco { get; set; }
    public List<ImagemProduto>? ListaImgs { get; set; } = new List<ImagemProduto>();
    public List<Ingrediente>? ListaIngredientes { get; set; } = new List<Ingrediente>();
    public List<Custos>? ListaCustos { get; set; } = new List<Custos>();
    [Precision(18, 2)]
    [Display(Name = "Margem de Lucro (%)")]
    public decimal MargemLucro { get; set; }
  

    public decimal CalcularPrecoProduto()
    {
        decimal valor = 0;
        try
        {
            if(ListaIngredientes?.Count > 0)
            {
                foreach (var item in ListaIngredientes)
                {
                    valor += item.CalcularCustoIngrediente();
                }
                
            }
            if(ListaCustos?.Count > 0)
            {
                foreach (var item in ListaCustos)
                {
                    valor += item.ValorCusto;
                }
            }
            valor += ((valor*MargemLucro)/100);
            return PersonalFormatter.DecimalFormatter(Convert.ToString(valor));
        }
        catch(Exception )
        {
            throw;
        }  
    }

    public List<string> GetUrlImgs()
    {
        if (ListaImgs != null && ListaImgs.Count > 0)
        {
            return ListaImgs.Select(i => i.ImgUrl).ToList();
        }
        else if (!string.IsNullOrEmpty(UrlImg))
        {
            return new List<string> { UrlImg };
        }
        else
        {
            return new List<string>();
        }
    }

    public List<string> GetIngredientes()
    {
        if (ListaIngredientes != null && ListaIngredientes.Count > 0)
        {
            return ListaIngredientes.Select(i => i.MateriaPrima.Descricao).ToList();
        }
        else
        {
            return new List<string>();
        }
    }
}
