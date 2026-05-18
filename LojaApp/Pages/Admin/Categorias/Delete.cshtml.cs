using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LojaApp.Data;
using LojaApp.Models.Produtos;

namespace LojaApp.Pages.Categorias
{
    public class DeleteModel : PageModel
    {
        private readonly LojaApp.Data.AppDbContext _context;

        public DeleteModel(LojaApp.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Categoria Categoria { get; set; } = default!;
        public string MensagemErro { get; set; } = string.Empty;
        public string MensagemSucesso { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias.FirstOrDefaultAsync(m => m.IdCategoria == id);

            if (categoria is not null)
            {
                Categoria = categoria;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                MensagemErro = "Categoria não encontrada.";
                return NotFound();
            }

            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria != null)
            {
                Categoria = categoria;
                _context.Categorias.Remove(Categoria);
                await _context.SaveChangesAsync();
                MensagemSucesso = "Categoria excluída com sucesso.";
            }

            return RedirectToPage("./Index");
        }
    }
}
