using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaApp.Models.Produtos;

public class Categoria
{
    public string IdCategoria { get; set; } = Guid.NewGuid().ToString();
    public string Descricao { get; set; }

    
    public Categoria(string descricao)
    {
        Descricao = descricao;
    }
    public Categoria(string idCategoria, string descricao)
    {
        IdCategoria = idCategoria;
        Descricao = descricao;
    }
}
