using Produtos.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Produtos.Entities
{
    public class Substancia
    {
        public  string IdSubstancia { get; set; }
        public string Descricao { get; set; }
        public decimal VlUn { get; set; }
        public TpMedidaEnum TpMedida { get; set; }
    }
}
