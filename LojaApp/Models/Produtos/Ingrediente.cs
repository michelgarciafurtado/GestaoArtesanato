using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaApp.Models.Produtos;

public class Ingrediente
{
    public string IdIngrediente { get; set; } = Guid.NewGuid().ToString();  
    public required Substancia Substancia { get; set; }
    public required Produto Produto { get; set; }
    public decimal QtdIngrediente { get; set; }
    public decimal CalcularCustoIngrediente()
    {
        return QtdIngrediente * Substancia.VlUn;
    }
}
