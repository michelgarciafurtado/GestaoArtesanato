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
        public Models.Pedidos.SolicitacaoProduto SolicitacaoProduto { get; set; } = new Models.Pedidos.SolicitacaoProduto();

        private readonly Data.AppDbContext _context;

        public PedidoModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
                return NotFound();

            Produto Produto = await _context.Produtos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.IdProduto == id);
            
            if (Produto == null)
                return NotFound();
            
            SolicitacaoProduto.Produto = Produto;

            return Page();
        }

        public async Task<ActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            SolicitacaoProduto.Produto = await _context.Produtos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.IdProduto == SolicitacaoProduto.IdProduto);
            
            string mensagem = $"Olá, gostaria de fazer um pedido:\n" +
                              $"Nome: {SolicitacaoProduto.Nome}\n" +
                              $"Contato: {SolicitacaoProduto.CelContato}\n" +
                              $"Produto: {SolicitacaoProduto.Produto.NomeProduto}\n" +
                              $"Quantidade: {SolicitacaoProduto.quantidade}\n" +
                              $"Data do Pedido: {SolicitacaoProduto.DataPedido.ToString("dd/MM/yyyy")}";

            string mensagemCodificada = Uri.EscapeDataString(mensagem); 
            
            string linkWhatsApp = $"https://wa.me/5511997206113?text={mensagemCodificada}";
            // Redireciona direto para o WhatsApp
            return Redirect(linkWhatsApp);
        }
    }
}
