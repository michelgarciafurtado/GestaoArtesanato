using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LojaApp.Data;
using LojaApp.Models.EntradaMateriais;

namespace LojaApp.Pages.CrudEntradaMateriais
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
        ViewData["IdMateriaPrima"] = new SelectList(_context.MateriasPrimas, "IdMateriaPrima", "Descricao");
            return Page();
        }

        [BindProperty]
        public EntradaMaterial EntradaMaterial { get; set; } = default!;
        [TempData]
        public string Mensagem { get; set; } = default!;


        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var MateriaPrima = await _context.MateriasPrimas.FindAsync(EntradaMaterial.IdMateriaPrima);
            if (MateriaPrima == null)
            {
                ModelState.AddModelError(string.Empty, "Substância não encontrada.");
                Mensagem = "Substância não encontrada.";
                return Page();
            }

            MateriaPrima.RegistrarEntrada(EntradaMaterial.Quantidade, EntradaMaterial.ValorTotal,EntradaMaterial.PesoTotal);

            _context.Entradas.Add(EntradaMaterial);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
