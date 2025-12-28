using LojaApp.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaApp.Models.Produtos;

public class Substancia
{
    [Key]
    public  string IdSubstancia { get; set; } = Guid.NewGuid().ToString();
    public string Descricao { get; set; }
    [Precision(18,2)]
    public decimal VlUn { get; set; }
    public TpMedidaEnum TpMedida { get; set; }

    public Substancia()
    {
        
    }
    public Substancia(string descricao, TpMedidaEnum tpmedida)
    {
        Descricao = descricao;
        TpMedida = tpmedida;
        VlUn = 0;
    }


}
