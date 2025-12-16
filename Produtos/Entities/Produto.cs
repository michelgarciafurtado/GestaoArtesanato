using Produtos.Entities.Enums;

namespace Produtos.Entities;

public class Produto
{
    public string IdProduto { get; set; } = new Guid().ToString();
    public required string NomeProduto { get; set; }
    public Categoria Categoria { get; set; }
    public decimal PesoProduto { get; set; }
    public TpMedidaEnum medidaEnum { get; set; }
    public string UrlImg { get; set; }
    public List<Ingrediente> ListaIngredientes { get; set; }

    public decimal CalcularValorProduto()
    {
        decimal valor = 0;
        foreach (var item in ListaIngredientes)
        {
            valor = valor + item.CalcularValorIngrediente();
        }
        return valor;
    }
}
