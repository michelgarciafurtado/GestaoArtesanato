using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Produtos.Entities
{
    public class Categoria
    {
        public string IdCategoria { get; set; } = new Guid().ToString();
    }
}
