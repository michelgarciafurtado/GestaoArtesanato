using LojaApp.Models.Produtos;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace LojaApp.Models.Pedidos
{
    public class SolicitacaoProduto
    {
        public DateOnly DataPedido { get; set; } = DateOnly.FromDateTime(DateTime.Today);
        public string Nome { get; set; }
        public string CelContato { get; set; }
        public string IdProduto { get; set; }
        [ValidateNever]
        public Produto Produto { get; set; }
        public int quantidade { get; set; }
    }
}
