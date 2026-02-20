using LojaApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LojaApp.Pages;

public class DetalhesProdutoModel : PageModel
{
    private readonly AppDbContext _context;
    public ProductViewModel Product { get; set; }
    public DetalhesProdutoModel(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> OnGetAsync(string IdProduto)
    {
        var produto = await _context.Produtos
            .Include(p => p.Categoria)
            .Include(p => p.ListaIngredientes)
            .ThenInclude(i => i.MateriaPrima)
            .Include(p => p.ListaImgs)
            .FirstOrDefaultAsync(p => p.IdProduto == IdProduto);
        if (produto == null)
        {
            return NotFound();
        }
        Product = new ProductViewModel
        {
            IdProduto = produto.IdProduto,
            NomeProduto = produto.NomeProduto,
            UrlImgPrincipal = produto.UrlImg,
            Preco = produto.Preco,
            Peso = produto.PesoProduto,
            Medida = produto.medidaEnum.ToString(),
            Descricao = produto.Descricao,
            Categoria = produto.Categoria != null ? produto.Categoria.Descricao : string.Empty,
            Imagens = produto.ListaImgs != null ? produto.GetUrlImgs() : new List<string>(),
            Ingredientes = produto.ListaIngredientes != null ? produto.GetIngredientes() : new List<string>()
        };
        return Page();
    }
}
public class ProductViewModel
{
    public string IdProduto { get; set; }
    public string NomeProduto { get; set; }
    public string UrlImgPrincipal { get; set; }
    public int Peso { get; set; }
    public string Medida { get; set; }
    public decimal Preco { get; set; }
    public string Descricao { get; set; }
    public string Categoria { get; set; }
    public List<string> Imagens { get; set; }
    public List<string> Ingredientes { get; set; }
}
