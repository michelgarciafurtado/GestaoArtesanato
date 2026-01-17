using LojaApp.Data;
using LojaApp.Models.Produtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LojaApp.Pages.CrudProdutos
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        [BindProperty]
        public List<Produto> Produtos { get; set; } = default!;
        [TempData]
        public string Mensagem { get; set; } = default!;
        public IndexModel(AppDbContext context)
        {
            _context = context;
        }
        public async Task  OnGetAsync()
        {
            Produtos = await _context.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.ListaIngredientes)
                .ThenInclude(i => i.MateriaPrima)
                .Include(p => p.ListaCustos)
                .ToListAsync();

            if(Produtos.Count <= 0)
            {
                Mensagem = "Erro ao buscar produtos";
            }

            
        }
        public async Task<IActionResult> OnPostDeleteProdutoAsync(string IdProduto)
        {
            var produtoDb = await _context.Produtos
                                   .Include(p => p.ListaIngredientes)
                                   .Include(p => p.ListaCustos)
                                   .FirstOrDefaultAsync(p => p.IdProduto.Equals(IdProduto));
            if (produtoDb is null)
            {
                Mensagem = "Produto não encontrado.";
                return RedirectToPage("/CrudProdutos/Index");
            }
            //Remover ingredientes associados
            if(produtoDb.ListaIngredientes is not null && produtoDb.ListaIngredientes.Count > 0)
            {
                _context.Ingredientes.RemoveRange(produtoDb.ListaIngredientes);
            }
            //Remover custos associados
            if(produtoDb.ListaCustos is not null && produtoDb.ListaCustos.Count > 0)
            {
                _context.Custos.RemoveRange(produtoDb.ListaCustos);
            }
            _context.Produtos.Remove(produtoDb);
            await _context.SaveChangesAsync();
            Mensagem = "Produto removido com sucesso.";
            return RedirectToPage("/Admin/CrudProdutos/Index");
        }
    }
}
