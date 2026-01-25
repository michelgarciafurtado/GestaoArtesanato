using LojaApp.Models.Produtos;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaApp.Models.Pedidos
{
    public class ItemPedido
    {
        [Key]
        public string IdItemPedido { get; set; } = Guid.NewGuid().ToString();
        public required string IdPedido { get; set; }
        [ForeignKey("IdPedido")]
        public required Pedido Pedido { get; set; }
        public required string IdProduto { get; set; }
        [ForeignKey("IdProduto")]
        public required Produto Produto { get; set; }
        [Required(ErrorMessage = "Informe a quantidade")]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser pelo menos 1")]
        public required int Quantidade { get; set; } = 1;
        [Precision(18,2)]
        public decimal PrecoUnitario { get; private set; }
        public decimal PrecoItem { get; private set; }

        public ItemPedido()
        {
            if(Produto != null)
            {
                AdicionarItemPedido();
            }
        }

        public void AdicionarItemPedido()
        {
            this.PrecoUnitario = Produto.Preco;
            this.PrecoItem = this.PrecoUnitario * this.Quantidade;
        }
    }
}
