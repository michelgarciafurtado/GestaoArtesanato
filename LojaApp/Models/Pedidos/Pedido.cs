using LojaApp.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaApp.Models.Pedidos
{
    public class Pedido
    {
        [Key]
        public string IdPedido { get; set; } = Guid.NewGuid().ToString();
        public DateTime DataPedido { get; set; } = DateTime.Now;
        public required string IdCliente { get; set; }
        [ForeignKey("IdCliente")]
        public required Cliente Cliente { get; set; }
        public ICollection<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();
        public decimal TotalPedido 
        { 
            get 
            {
                return ItensPedido.Sum(item => item.PrecoUnitario * item.Quantidade);
            }
        }
    }
}
