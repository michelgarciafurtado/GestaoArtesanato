using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaApp.Models.Produtos
{
    public class Custos {
        [Key] public string IdCusto { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string DescricaoCusto { get; set; } = string.Empty;
        [Precision(18, 2)]
        public decimal ValorCusto { get; set; }
        public string IdProduto { get; set; }
        [ForeignKey(nameof(IdProduto))] 
        public Produto Produto { get; set; }
    }
}
