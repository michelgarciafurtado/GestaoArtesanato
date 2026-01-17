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

        public MateriaPrima MateriaPrima { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matprima = await _context.MateriasPrimas.FirstOrDefaultAsync(m => m.IdMateriaPrima == id);

            if (matprima is not null)
            {
                MateriaPrima = matprima;

                return Page();
            }

            return NotFound();
        }
    }
}
