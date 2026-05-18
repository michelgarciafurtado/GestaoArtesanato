using LojaApp.Data;
using LojaApp.Models.Produtos;
using LojaApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LojaApp.Pages.CrudProdutos
{
    public class EditarModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly GenImagensService _genImagensService;
        [BindProperty]
        public Produto Produto { get; set; } = default!;
        [BindProperty]
        public IFormFile? ImagemUpload { get; set; } = default!;
        public string MensagemErro { get; set; } = string.Empty;
        public string MensagemSucesso { get; set; } = string.Empty;
        public SelectList SelectCategoria { get; set; } = default!;

        public EditarModel(AppDbContext context, GenImagensService genImagensService)
        {
            _context = context;
            _genImagensService = genImagensService;
        }

        public async Task<IActionResult> OnGetAsync(string IdProduto)
        {
            var resultado  = await _context.Produtos
                                   .Include(p => p.Categoria)
                                   .Include(p => p.ListaIngredientes)
                                   .ThenInclude(i => i.MateriaPrima)
                                   .Include(p => p.ListaCustos)
                                   .FirstOrDefaultAsync(p => p.IdProduto.Equals(IdProduto));

            if(resultado is null)
            {
                MensagemErro = "Produto não encontrado.";
                return RedirectToPage("/Admin/CrudProdutos/Index");
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
                MensagemErro = "Modelo enviado e invalido";
                SelectCategoria = new SelectList(_context.Categorias.ToList(), "IdCategoria", "Descricao");
                return Page();
            }
            var produtoDb = await _context.Produtos
                                   .Include(p => p.Categoria)
                                   .Include(p => p.ListaIngredientes)
                                   .ThenInclude(i => i.MateriaPrima)
                                   .FirstOrDefaultAsync(p => p.IdProduto.Equals(Produto.IdProduto));
            if (produtoDb is null)
            {
                MensagemErro = "Produto não encontrado.";
                return RedirectToPage("/Admin/CrudProdutos/Index");
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
                try
                {
                    var imagem = _genImagensService.UploadImagem(ImagemUpload, produtoDb.NomeProduto, "ImgPathProduto");

                    produtoDb.UrlImg = imagem;
                }
                catch(Exception ex)
                {
                    MensagemErro = "Erro ao Atualizar a imagem";
                    throw ex;
                }
            }
            else
            {
                produtoDb.UrlImg = Produto.UrlImg;
            }
                await _context.SaveChangesAsync();
            MensagemSucesso = "Produto atualizado com sucesso!";
            return RedirectToPage("/Admin/CrudProdutos/Index");
        }

        public async Task<IActionResult> OnPostDeleteIngredienteAsync(string IdIngrediente)
        {
            var ingrediente = await _context.Ingredientes.FirstOrDefaultAsync(x => x.IdIngrediente == IdIngrediente);
            if (ingrediente != null)
            {
                _context.Ingredientes.Remove(ingrediente);
                await _context.SaveChangesAsync();
                await OnPostAddProductAsync();
                MensagemSucesso = "Ingrediente excluído com sucesso!";
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
                MensagemSucesso = "Custo Excluido com sucesso!";
            }
            return RedirectToPage();
        }
    }
}
