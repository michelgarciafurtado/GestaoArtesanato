using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LojaApp.Data;
using LojaApp.Models.Produtos;
using Microsoft.AspNetCore.Authorization;

namespace LojaApp.Pages.Categorias;
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
    public Categoria Categoria { get; set; } = default!;

    // For more information, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Categorias.Add(Categoria);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
