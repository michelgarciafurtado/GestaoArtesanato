using LojaApp.Data;
using LojaApp.Models.Produtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace LojaApp.Pages.Admin.CrudProdutos;

public class GenImagensModel : PageModel
{
    [BindProperty]
    public AdicionarImagemCmd AdicionarImagem { get; set;}
    [TempData]
    public string Mensagem { get; set; } = string.Empty;
    private readonly AppDbContext _context;

    public GenImagensModel(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> OnGetAsync(string IdProduto)
    {
        var produto = await _context.Produtos
            .Include(p => p.ListaImgs)
            .FirstOrDefaultAsync(p => p.IdProduto == IdProduto);
        if (produto == null) 
            return RedirectToPage("Admin/CrudProdutos/Index");
        AdicionarImagem = new AdicionarImagemCmd { Id = IdProduto ,
                                                Nome = produto.NomeProduto,
        Imagens = produto.ListaImgs != null ? produto.ListaImgs : new List<ImagemProduto>()
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAddImagemAsync()
    {
        var produto = await _context.Produtos
            .Include(p => p.ListaImgs)
            .FirstOrDefaultAsync(p => p.IdProduto == AdicionarImagem.Id);
        
        if (produto == null)
        {
            return RedirectToPage("Admin/CrudProdutos/Index", new  {Mensagem ="Produto nao encontrado" });
        } 
            
        
        // Garantir que a lista de imagens do produto não seja nula antes de adicionar
        produto.ListaImgs ??= new List<ImagemProduto>();

        // Garantir que o diretório exista
        var imagensDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Imagens", "Produtos");
        if(!Directory.Exists(imagensDir))
            Directory.CreateDirectory(imagensDir);

        // Instancia a classe Imagem e adiciona ao 
        if(AdicionarImagem.ImagemFile != null)
        {
            var ext = Path.GetExtension(AdicionarImagem.ImagemFile.FileName);
            var fileName = Path.GetFileName(AdicionarImagem.Nome.Trim() + "_" + GetNewToken() + ext);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(),
                                            "wwwroot/Imagens/Produtos",
                                            fileName.Trim());//monta o urlImg
            var imagemProduto = new ImagemProduto
            {
                IdProduto = AdicionarImagem.Id,
                ImgUrl = "/Imagens/Produtos/" + fileName,
            };

            _context.ImagensProdutos.Add(imagemProduto);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await AdicionarImagem.ImagemFile.CopyToAsync(stream);//grava o arquivo na pasta
            }

        }
        else
        {
            Mensagem = "Não foi possível salvar a Imagem!";
        }
        var produtoAtualizado = await _context.Produtos.
            Include(p => p.ListaImgs).
            FirstOrDefaultAsync(p => p.IdProduto == AdicionarImagem.Id);

        AdicionarImagem = new AdicionarImagemCmd
        {
            Id = produtoAtualizado.IdProduto,
            Nome = produtoAtualizado.NomeProduto,
            Imagens = produtoAtualizado.ListaImgs
        };

        await _context.SaveChangesAsync();
        return Page();


    }

    public async Task<IActionResult> OnPostDeleteImagemAsync(string IdProduto, string IdImagem)
    {
        var imagem = await _context.ImagensProdutos.FirstOrDefaultAsync(i =>  i.IdImagem == Guid.Parse(IdImagem));
        if(imagem != null)
        {
            var caminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagem.ImgUrl.TrimStart('/'));
            if(System.IO.File.Exists(caminhoArquivo))
            {
                //deleta o arquivo fisico no servidor
                System.IO.File.Delete(caminhoArquivo);

                //deleta o registro da imagem no banco de dados
                _context.ImagensProdutos.Remove(imagem);
                await _context.SaveChangesAsync();
                Mensagem = "Imagem deletada com sucesso!";
            }
        }
        else
        {
            Mensagem = "Imagem nao encontrada!";
        }
        return RedirectToPage(new { IdProduto });
    }

    private string GetNewToken()
    {
        return Guid.NewGuid().ToString("n").Substring(0, 4);
    }

}

public class AdicionarImagemCmd
{
    public string Id { get; set; }
    public string Nome { get; set; }
    public string ImgUrl { get; set; }
    public IFormFile ImagemFile { get; set; } = default!;
    public List<ImagemProduto> Imagens { get; set; } = new List<ImagemProduto>();
}
