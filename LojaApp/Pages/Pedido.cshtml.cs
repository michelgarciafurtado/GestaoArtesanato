using LojaApp.Data;
using LojaApp.Models.Produtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LojaApp.Pages
{
    public class PedidoModel : PageModel
    {
        [BindProperty]
        public Models.Pedidos.SolicitacaoProduto SolicitacaoProduto { get; set; }
        [BindProperty]

        public Produto Produto { get; set; }

        private readonly Data.AppDbContext _context;

        public PedidoModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
                return NotFound();
            Produto = await _context.Produtos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.IdProduto == id);
            if (Produto == null)
                return NotFound();
            return Page();
        }

        public async Task<ActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            string mensagem = $"Olá, gostaria de fazer um pedido:%0A" +
                              $"Nome: {SolicitacaoProduto.Nome}%0A" +
                              $"Contato: {SolicitacaoProduto.CelContato}%0A" +
                              $"Produto: {SolicitacaoProduto.NomeProduto}%0A" +
                              $"Quantidade: {SolicitacaoProduto.quantidade}%0A" +
                              $"Data do Pedido: {SolicitacaoProduto.DataPedido.ToString("dd/MM/yyyy")}";
            string mensagemCodificada = Uri.EscapeDataString(mensagem);
            string linkWhatsApp = $"https://wa.me/15981230946?text={mensagemCodificada}";
            return Page();
        }
    }
}
