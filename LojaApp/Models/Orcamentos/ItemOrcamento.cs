using LojaApp.Models.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaApp.Models.Orcamentos;

public class ItemOrcamento
{
    public string IdItem { get; set; } = new Guid().ToString();
    public required Produto Produto { get; set; }
    public  int qtd { get; set; }

    public decimal TotalItem()
    {
        return Produto.CalcularCustoMateriaPrima() * qtd;
    }
}
