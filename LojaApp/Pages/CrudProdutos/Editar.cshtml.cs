using LojaApp.Data;
using LojaApp.Models.Produtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LojaApp.Pages.CrudProdutos
{
    public class EditarModel : PageModel
    {
        private readonly AppDbContext _context;
        [BindProperty]
        public Produto Produto { get; set; } = default!;
        [BindProperty]
        public IFormFile? ImagemUpload { get; set; } = default!;
        [TempData]
        public string Mensagem { get; set; } = default!;
        public SelectList SelectCategoria { get; set; } = default!;

        public EditarModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(string IdProduto)
        {
            var resultado  = await _context.Produtos
                                   .Include(p => p.Categoria)
                                   .Include(p => p.ListaIngredientes)
                                   .ThenInclude(i => i.Substancia)
                                   .Include(p => p.ListaCustos)
                                   .FirstOrDefaultAsync(p => p.IdProduto.Equals(IdProduto));

            if(resultado is null)
            {
                Mensagem = "Produto não encontrado.";
                return RedirectToPage("/CrudProdutos/Index");
            }
            Produto = resultado;

            Produto.Preco = Produto.CalcularPrecoProduto();
            //Se estiver nulo, inicializa as listas para evitar erros na view
            Produto.ListaIngredientes ??= [];
            Produto.ListaCustos ??= [];

            SelectCategoria = new SelectList(_context.Categorias.ToList(), "IdCategoria", "Descricao");

            return Page();
        }

        public async Task<IActionResult> OnPostAddProductAsync()
        {
            if (!ModelState.IsValid)
            {
                Mensagem = "Modelo enviado e invalido";
                SelectCategoria = new SelectList(_context.Categorias.ToList(), "IdCategoria", "Descricao");
                return Page();
            }
            var produtoDb = await _context.Produtos
                                   .Include(p => p.Categoria)
                                   .Include(p => p.ListaIngredientes)
                                   .ThenInclude(i => i.Substancia)
                                   .FirstOrDefaultAsync(p => p.IdProduto.Equals(Produto.IdProduto));
            if (produtoDb is null)
            {
                Mensagem = "Produto não encontrado.";
                return RedirectToPage("/CrudProdutos/Index");
            }
            produtoDb.NomeProduto = Produto.NomeProduto;
            produtoDb.Descricao = Produto.Descricao;
            produtoDb.IdCategoria = Produto.IdCategoria;
            produtoDb.PesoProduto = Produto.PesoProduto;
            produtoDb.medidaEnum = Produto.medidaEnum;
            produtoDb.MargemLucro = Produto.MargemLucro;
            produtoDb.Preco = Produto.Preco;
            if (ImagemUpload != null)
            {
                var ext = Path.GetExtension(ImagemUpload.FileName);
                var fileName = Path.GetFileName(Produto.NomeProduto.Trim() + ext);

                var filePath = Path.Combine(Directory.GetCurrentDirectory(),
                                            "wwwroot/Imagens/Produtos",
                                            fileName.Trim());

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImagemUpload.CopyToAsync(stream);
                }

                produtoDb.UrlImg = "/Imagens/Produtos/" + fileName;
            }
            else
            {
                produtoDb.UrlImg = Produto.UrlImg;
            }
                await _context.SaveChangesAsync();
            Mensagem = "Produto atualizado com sucesso!";
            return RedirectToPage("/CrudProdutos/Index");
        }

        public async Task<IActionResult> OnPostDeleteIngredienteAsync(string IdIngrediente)
        {
            var ingrediente = await _context.Ingredientes.FirstOrDefaultAsync(x => x.IdIngrediente == IdIngrediente);
            if (ingrediente != null)
            {
                _context.Ingredientes.Remove(ingrediente);
                await _context.SaveChangesAsync();
                await OnPostAddProductAsync();
                Mensagem = "Ingrediente excluído com sucesso!";
            }
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostDeleteCustoAsync(string IdCusto)
        {
            var custo = await _context.Custos.FirstOrDefaultAsync(x => x.IdCusto == IdCusto);
            if (custo != null)
            {
                _context.Custos.Remove(custo);
                await _context.SaveChangesAsync();
                await OnPostAddProductAsync();
                Mensagem = "Custo Excluido com sucesso!";
            }
            return RedirectToPage();
        }
    }
}
