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
    public class DetailsModel : PageModel
    {
        private readonly LojaApp.Data.AppDbContext _context;

        public DetailsModel(LojaApp.Data.AppDbContext context)
        {
            _context = context;
        }

        public Categoria Categoria { get; set; } = default!;

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
    }
}
