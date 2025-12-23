using Produtos.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaApp.Models.Produtos;

public class Substancia
{
    public  string IdSubstancia { get; set; } = Guid.NewGuid().ToString();
    public string Descricao { get; set; }
    public decimal VlUn { get; set; }
    public TpMedidaEnum TpMedida { get; set; }

    public Substancia(string descricao, TpMedidaEnum tpmedida)
    {
        Descricao = descricao;
        TpMedida = tpmedida;
        VlUn = 0;
    }


}
