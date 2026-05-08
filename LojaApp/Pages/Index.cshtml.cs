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
        [BindProperty]
        public IndexViewModel IndexVM { get; } = new IndexViewModel();
        public async Task<ActionResult> OnGetAsync()
        {
            Produtos = await _context.Produtos
                      .Include(p => p.Categoria) 
                      .ToListAsync();
            if(Produtos.Count <= 0 || Produtos == null)
            {
                Mensagem = "Problemas ao se conectar com a loja";
            }
            return Page();
        }

    }

    public record IndexViewModel(
        string TextoCabecalho = @"<strong>Produto feito à mão, com o coração especialmente para você. ❤️</strong> 
                                 
                                 <em>Obrigado por apoiar o trabalho artesanal e por permitir que a gente leve um pouquinho do nosso carinho para dentro da sua casa!!!</em>.

                                Aproveite seu momento!!!!"
        );
}
