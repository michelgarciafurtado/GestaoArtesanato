using LojaApp.Data;
using LojaApp.Models.Produtos;
using LojaApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace LojaApp.Pages.CrudProdutos;

public class CreateModel : PageModel
{
    private readonly AppDbContext _context;
    private readonly GenImagensService _genImagensService;
    [BindProperty]
    public Produto Produto { get; set; } = default!;
    [BindProperty]
    public IFormFile ImagemUpload { get; set; } = default!;
    [TempData]
    public string Mensagem { get; set; } = default!;
    public SelectList SelectCategoria { get; set; } = default!;



    
    public CreateModel(AppDbContext context, GenImagensService genImagensService)
    {
        _context = context;
        _genImagensService = genImagensService;
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
            try
            {
                var imagem = _genImagensService.UploadImagem(ImagemUpload, Produto.NomeProduto, "ImgPathProduto");

                Produto.UrlImg = imagem;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ocorreu um erro ao fazer o upload da imagem: " + ex.Message);
                return Page();
            }
        }
        await _context.Produtos.AddAsync(Produto);
        await _context.SaveChangesAsync();
        Mensagem = "Produto cadastrado com sucesso!";
        return RedirectToPage("./Index");
    }

    
}
