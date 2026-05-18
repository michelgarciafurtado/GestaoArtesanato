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
        public string? Mensagem { get; set; }
        [BindProperty]
        public List<Produto> Produtos { get; set; }
        [BindProperty]
        public List<Categoria> Categorias { get; set; } = new List<Categoria>();
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
            Categorias = await _context.Categorias.ToListAsync();
            return Page();
        }

        public async Task<ActionResult> OnGetFilterCategAsync(string IdCateg)
        {
            Produtos = await _context.Produtos
                            .Include(p => p.Categoria)
                            .Where(p => p.Categoria.IdCategoria == IdCateg)
                            .ToListAsync();
            if (Produtos.Count <= 0 || Produtos == null)
            {
                Mensagem = "Não ha itens nesta categoria";
            }
            Categorias = await _context.Categorias.ToListAsync();
            return Page();
        }

    }

    public record IndexViewModel(
        string TextoCabecalho = @"<strong>Produto feito à mão, com o coração especialmente para você. ❤️</strong> 
                                 
                                 <em>Obrigado por apoiar o trabalho artesanal e por permitir que a gente leve um pouquinho do nosso carinho para dentro da sua casa!!!</em>.

                                Aproveite seu momento!!!!"
        );
}
