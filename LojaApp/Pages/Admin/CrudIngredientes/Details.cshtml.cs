using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LojaApp.Data;
using LojaApp.Models.Produtos;

namespace LojaApp.Pages.CrudIngredientes
{
    public class DetailsModel : PageModel
    {
        private readonly LojaApp.Data.AppDbContext _context;

        public DetailsModel(LojaApp.Data.AppDbContext context)
        {
            _context = context;
        }

        public Ingrediente Ingrediente { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingrediente = await _context.Ingredientes.FirstOrDefaultAsync(m => m.IdIngrediente == id);

            if (ingrediente is not null)
            {
                Ingrediente = ingrediente;

                return Page();
            }

            return NotFound();
        }
    }
}
