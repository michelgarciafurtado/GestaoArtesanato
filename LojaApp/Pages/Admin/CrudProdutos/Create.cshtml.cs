using LojaApp.Data;
using LojaApp.Models.Produtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace LojaApp.Pages.CrudProdutos
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;
        [BindProperty]
        public Produto Produto { get; set; } = default!;
        [BindProperty]
        public IFormFile ImagemUpload { get; set; } = default!;
        [TempData]
        public string Mensagem { get; set; } = default!;
        public SelectList SelectCategoria { get; set; } = default!;
        public CreateModel(AppDbContext context)
        {
            _context = context;
        }
        public async Task  OnGetAsync()
        {
            var categorias = await _context.Categorias.ToListAsync();
            SelectCategoria = new SelectList(categorias, "IdCategoria", "Descricao");

            // Garantir que Produto não seja nulo quando a página for renderizada
            if (Produto is null)
            {
                Produto = new Produto
                {
                    NomeProduto = string.Empty,
                    Categoria = new Categoria()
                };
            }
        }
        public async Task<ActionResult> OnPostAsync(IFormFile ImagemUpload) 
        {
            if (ImagemUpload != null)
            {
                var ext = Path.GetExtension(ImagemUpload.FileName);
                var fileName = Path.GetFileName(Produto.NomeProduto.Trim() + ext);

                var filePath = Path.Combine(Directory.GetCurrentDirectory(),
                                            "wwwroot/Imagens/Produtos",
                                            fileName.Trim());

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    var caminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filePath.TrimStart('/'));
                    //Se o arquivo físico já existir, deleta para evitar duplicidade
                    //if (System.IO.File.Exists(caminhoArquivo))
                    //{
                    //    //deleta o arquivo fisico no servidor
                    //    System.IO.File.Delete(caminhoArquivo);
                    //}
                    await ImagemUpload.CopyToAsync(stream);
                }

                Produto.UrlImg = "/Imagens/Produtos/" + fileName;
            }
            if (!ModelState.IsValid)
            {
                Mensagem = "Erro ao cadastrar produto. Verifique os dados informados.";
                return Page();
            }

            await _context.Produtos.AddAsync(Produto);
            await _context.SaveChangesAsync();
            Mensagem = "Produto cadastrado com sucesso!";
            return RedirectToPage("./Index");
        }
    }
}
