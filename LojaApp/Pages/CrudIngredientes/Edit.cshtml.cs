using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LojaApp.Data;
using LojaApp.Models.Produtos;

namespace LojaApp.Pages.CrudIngredientes
{
    public class EditModel : PageModel
    {
        private readonly LojaApp.Data.AppDbContext _context;

        public EditModel(LojaApp.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Ingrediente Ingrediente { get; set; } = default!;
        public SelectList SelectSubstancia { get; set; } = default!;
        public string Mensagem { get; private set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            SelectSubstancia = new SelectList(await _context.Substancias.ToListAsync(), "IdSubstancia", "Descricao");
            if (id == null)
            {
                return NotFound();
            }

            var ingrediente =  await _context.Ingredientes.FirstOrDefaultAsync(m => m.IdIngrediente == id);
            if (ingrediente == null)
            {
                return NotFound();
            }
            Ingrediente = ingrediente;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Ingrediente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredienteExists(Ingrediente.IdIngrediente))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool IngredienteExists(string id)
        {
            return _context.Ingredientes.Any(e => e.IdIngrediente == id);
        }
    }
}
