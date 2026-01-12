using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LojaApp.Data;
using LojaApp.Models.EntradaMateriais;

namespace LojaApp.Pages.CrudEntradaMateriais;

public class IndexModel : PageModel
{
    private readonly LojaApp.Data.AppDbContext _context;

    public IndexModel(LojaApp.Data.AppDbContext context)
    {
        _context = context;
    }

    public IList<EntradaMaterial> EntradaMaterial { get;set; } = default!;

    [TempData]
    public string Mensagem { get; set; } = string.Empty;

    public async Task OnGetAsync()
    {
        EntradaMaterial = await _context.Entradas
            .Include(e => e.Substancia).ToListAsync();
    }

    public async Task<ActionResult> OnPostDeleteEntradaAsync(string IdEntrada)
    {
        if (!Guid.TryParse(IdEntrada, out var guidId))
        {
            ModelState.AddModelError(string.Empty, "Id inválido.");
            return Page();
        }

        var entrada = await _context.Entradas.FindAsync(guidId);

        if (entrada != null)
        {
            _context.Entradas.Remove(entrada);
            await _context.SaveChangesAsync();
            Mensagem = "Entrada de material deletada com sucesso!";
            return RedirectToPage("./Index");
        }
        return Page();
    }
}