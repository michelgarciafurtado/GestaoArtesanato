using LojaApp.Data;
using LojaApp.Models.Produtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaApp.Pages.CrudCustosProdutos
{
    public class CreateModel : PageModel
    {
        private readonly LojaApp.Data.AppDbContext _context;
        [BindProperty]
        public Custos Custos { get; set; } = default!;
        [TempData]
        public string Message { get; set; } = string.Empty;

        public CreateModel(LojaApp.Data.AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(string IdProduto)
        {
            var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.IdProduto == IdProduto);
            if (produto == null)
            {
                Message = "Produto não encontrado.";
                return RedirectToPage("/CrudProdutos/Index");
            }
            Custos custo = new Custos()
            {
                IdProduto = produto.IdProduto,
                Produto = produto
            };
            return Page();
        }

       

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string IdProduto)
        {
           
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Custos.Add(Custos);
            await _context.SaveChangesAsync();

            return RedirectToPage("/CrudProdutos/Editar", new { IdProduto });
        }
    }
}
