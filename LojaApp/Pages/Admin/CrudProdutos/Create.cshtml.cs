using LojaApp.Data;
using LojaApp.Models.Produtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace LojaApp.Pages.CrudProdutos;

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

    public readonly IWebHostEnvironment _env;
    public CreateModel(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
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

            if (ext != ".jpg" && ext != ".jpeg" && ext != ".png" && ext != ".webp")
            {
                ModelState.AddModelError("ImagemUpload", "Apenas arquivos .jpg, .jpeg, .png e .webp são permitidos.");
                return Page();
            }
            var fileName = ParaUrlAmigavel(Produto.NomeProduto.Trim() + ext);

            var filePath = Path.Combine(_env.WebRootPath, "Imagens/Produtos", fileName.Trim());

            if (!Directory.Exists(_env.WebRootPath + "Imagens/Produtos"))
            {
                Directory.CreateDirectory(_env.WebRootPath + "Imagens/Produtos");
            }

            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImagemUpload.CopyToAsync(stream);
                }

                Produto.UrlImg = $"/Imagens/Produtos/{fileName}";
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

    private string ParaUrlAmigavel(string name)
    {
        // Remove acentos
        var bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(name);
        string cleanName = System.Text.Encoding.ASCII.GetString(bytes);

        // Remove espaços e deixa minúsculo
        cleanName = cleanName.Replace(" ", "-").ToLower();

        // Remove qualquer coisa que não seja letra, número ou traço
        return System.Text.RegularExpressions.Regex.Replace(cleanName, @"[^a-z0-9\-\.]", "");
    }
}
