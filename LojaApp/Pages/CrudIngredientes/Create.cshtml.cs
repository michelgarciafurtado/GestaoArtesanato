using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LojaApp.Data;
using LojaApp.Models.Produtos;
using Microsoft.EntityFrameworkCore;

namespace LojaApp.Pages.CrudIngredientes
{
    public class CreateModel : PageModel
    {
        private readonly LojaApp.Data.AppDbContext _context;

        public CreateModel(LojaApp.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Ingrediente Ingrediente { get; set; } = default!;
        public SelectList SelectSubstancia { get; set; } = default!;
        public string Mensagem { get; private set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(string IdProduto)
        {
            SelectSubstancia = new SelectList(await _context.Substancias.ToListAsync(), "IdSubstancia", "Descricao");
            var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.IdProduto == IdProduto);
            
            if (produto == null)
            {
                Mensagem = "Produto não encontrado.";
                return RedirectToPage("/CrudProdutos/Index");
            }

            Ingrediente = new Ingrediente
            { 
                IdProduto = IdProduto, 
                Produto = produto
            };
            return Page();
        }

       

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string IdProduto)
        {
            if (!ModelState.IsValid)
            {
                SelectSubstancia = new SelectList(await _context.Substancias.ToListAsync(), "IdSubstancia", "Descricao");
                return Page();
            }
            Ingrediente.IdProduto = IdProduto;
            Ingrediente.Produto = await _context.Produtos.FirstOrDefaultAsync(p => p.IdProduto == IdProduto);

            _context.Ingredientes.Add(Ingrediente);
            
            await _context.SaveChangesAsync();

            return RedirectToPage("/CrudProdutos/Editar", new { IdProduto });
        }
    }
}
