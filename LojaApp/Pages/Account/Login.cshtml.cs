using LojaApp.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace LojaApp.Pages.Admin.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        [TempData]
        public string Mensagem { get; set; }
        public class InputModel
        {
            [Required]
            public string Email { get; set; }
            [Required]
            public string Password { get; set; }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, $"Usuário com email {Input.Email}.");
                Mensagem = "Email nao encontrado!";
                return Page();
            }
                var result = await _signInManager.PasswordSignInAsync(
                             user.UserName, 
                             Input.Password,
                             false,
                             lockoutOnFailure: false
                          );
            if (result.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(user);
                return RedirectToPage("/Admin/CrudProdutos/Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Tentativa de login inválida.");
                return Page();
            }
        }
    }
}
