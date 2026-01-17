using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LojaApp.Models.Produtos;

namespace LojaApp.Pages.CrudMateriaPrima
{
    public class DeleteModel : PageModel
    {
        private readonly LojaApp.Data.AppDbContext _context;

        public DeleteModel(LojaApp.Data.AppDbContext context)
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

            var matprima = await _context.MateriasPrimas.FirstOrDefaultAsync(m => m.IdMateriaPrima == id);

            if (matprima is not null)
            {
                MateriaPrima = matprima;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matprima = await _context.MateriasPrimas.FindAsync(id);
            if (matprima != null)
            {
                MateriaPrima = matprima;
                _context.MateriasPrimas.Remove(MateriaPrima);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
