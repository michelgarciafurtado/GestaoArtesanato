using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Produtos.Entities
{
    public class Ingrediente
    {
        public string IdIngrediente { get; set; }
        public Substancia Substancia { get; set; }
        public Produto Produto { get; set; }
        public decimal QtdIngrediente { get; set; }
        public decimal CalcularValorIngrediente()
        {
            return QtdIngrediente * Substancia.VlUn;
        }
    }
}
