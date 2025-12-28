using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LojaApp.Data;
using LojaApp.Models.Produtos;

namespace LojaApp.Pages.CrudSubstancias
{
    public class DetailsModel : PageModel
    {
        private readonly LojaApp.Data.AppDbContext _context;

        public DetailsModel(LojaApp.Data.AppDbContext context)
        {
            _context = context;
        }

        public Substancia Substancia { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var substancia = await _context.Substancias.FirstOrDefaultAsync(m => m.IdSubstancia == id);

            if (substancia is not null)
            {
                Substancia = substancia;

                return Page();
            }

            return NotFound();
        }
    }
}
