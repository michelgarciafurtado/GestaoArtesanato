using Produtos.Entities.Enums;

namespace LojaApp.Models.Produtos;

public class Produto
{
    public string IdProduto { get; set; } =  Guid.NewGuid().ToString();
    public required string NomeProduto { get; set; }
    public required Categoria Categoria { get; set; }
    public decimal PesoProduto { get; set; }
    public TpMedidaEnum medidaEnum { get; set; }
    public string? UrlImg { get; set; }
    public decimal Preco { get; set; }
    public List<Ingrediente>? ListaIngredientes { get; set; }

    public decimal CalcularCustoMateriaPrima()
    {
        decimal valor = 0;
        try
        {
            if(ListaIngredientes?.Count > 0)
            {
                foreach (var item in ListaIngredientes)
                {
                    valor = valor + item.CalcularCustoIngrediente();
                }
                return valor;
            }
        }catch(Exception )
        {
            throw;
        }
        return 0;   
    }
}
