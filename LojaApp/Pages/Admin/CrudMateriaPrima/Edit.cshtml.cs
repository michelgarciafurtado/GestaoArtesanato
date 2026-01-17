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

namespace LojaApp.Pages.CrudSubstancias
{
    public class EditModel : PageModel
    {
        private readonly LojaApp.Data.AppDbContext _context;

        public EditModel(LojaApp.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MateriaPrima MateriaPrima { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matprima =  await _context.MateriasPrimas.FirstOrDefaultAsync(m => m.IdMateriaPrima == id);
            if (matprima == null)
            {
                return NotFound();
            }
            MateriaPrima = matprima;
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

            _context.Attach(MateriaPrima).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!materiaprimaExists(MateriaPrima.IdMateriaPrima))
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

        private bool materiaprimaExists(string id)
        {
            return _context.MateriasPrimas.Any(e => e.IdMateriaPrima == id);
        }
    }
}
