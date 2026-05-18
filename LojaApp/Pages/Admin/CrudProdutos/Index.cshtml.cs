using LojaApp.Data;
using LojaApp.Models.Produtos;
using LojaApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LojaApp.Pages.CrudProdutos
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly GenImagensService _genImagensService;
        [BindProperty]
        public List<Produto> Produtos { get; set; } = default!;
        public string MensagemErro { get; set; } = string.Empty;
        public string MensagemSucesso { get; set; } = string.Empty;

        public IndexModel(AppDbContext context, GenImagensService genImagensService)
        {
            _context = context;
            _genImagensService = genImagensService;
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
                MensagemErro = "Erro ao buscar produtos";
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
                MensagemErro = "Produto não encontrado.";
                return RedirectToPage("./Index");
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
            // Remover imagem associada
            if (!string.IsNullOrEmpty(produtoDb.UrlImg))
            {
                _genImagensService.ExcluirImagem(produtoDb.UrlImg);
            }

            _context.Produtos.Remove(produtoDb);
            await _context.SaveChangesAsync();
            MensagemSucesso = "Produto removido com sucesso.";
            return RedirectToPage("/Admin/CrudProdutos/Index");
        }
    }
}
