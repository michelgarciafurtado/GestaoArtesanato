using LojaApp.Models.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaApp.Models.Produtos;

public class Ingrediente
{
    [Key]
    public string IdIngrediente { get; set; } = Guid.NewGuid().ToString();  
    public string? IdSubstancia { get; set; }
    [ForeignKey(nameof(IdSubstancia))]
    public Substancia? Substancia { get; set; }
    public string? IdProduto { get; set; }
    [ForeignKey(nameof(IdProduto))]
    public Produto? Produto { get; set; }
    [Display(Name = "Qtd. Ingr.")]
    public int QtdIngrediente { get; set; }
    public decimal CalcularCustoIngrediente()
    {
        return QtdIngrediente * Substancia.VlUn;
    }
}
