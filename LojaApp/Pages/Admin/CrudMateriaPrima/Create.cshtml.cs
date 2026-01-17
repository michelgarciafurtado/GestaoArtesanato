using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LojaApp.Models.Produtos;

namespace LojaApp.Pages.CrudMateriaPrima
{
    public class CreateModel : PageModel
    {
        private readonly LojaApp.Data.AppDbContext _context;

        public CreateModel(LojaApp.Data.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }
        [BindProperty]
        public MateriaPrima MateriaPrima { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.MateriasPrimas.Add(MateriaPrima);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
