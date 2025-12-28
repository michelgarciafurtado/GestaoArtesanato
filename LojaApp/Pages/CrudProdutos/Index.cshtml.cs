using LojaApp.Data;
using LojaApp.Models.Produtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                .ToListAsync();

            if(Produtos.Count <= 0)
            {
                Mensagem = "Erro ao buscar produtos";
            }

            
        }
    }
}
