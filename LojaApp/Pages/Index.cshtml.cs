using LojaApp.Data;
using LojaApp.Models.Produtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LojaApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }
        [TempData]
        public string Mensagem { get; set; }
        [BindProperty]
        public List<Produto> Produtos { get; set; }
        public async Task<ActionResult> OnGetAsync()
        {
            Produtos = await _context.Produtos.ToListAsync();
            if(Produtos.Count <= 0 || Produtos == null)
            {
                Mensagem = "Problemas ao se conectar com a loja";
            }
            return Page();
        }
    }
}
