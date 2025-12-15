using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Produtos.Entities
{
    public class ItemOrcamento
    {
        public string IdItem { get; set; } = new Guid().ToString();
        public Produto Produto { get; set; }
        public  int qtd { get; set; }

        public decimal TotalItem()
        {
            return Produto.CalcularValorProduto() * qtd;
        }
    }
}
