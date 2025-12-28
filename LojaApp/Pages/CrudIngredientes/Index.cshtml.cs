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
    public class IndexModel : PageModel
    {
        private readonly LojaApp.Data.AppDbContext _context;

        public IndexModel(LojaApp.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Ingrediente> Ingrediente { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Ingrediente = await _context.Ingredientes.ToListAsync();
        }
    }
}
