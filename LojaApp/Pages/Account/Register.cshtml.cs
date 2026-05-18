using LojaApp.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace LojaApp.Pages.Account;

[Authorize(Policy = "Admin")]
public class RegisterModel : PageModel
{
    [BindProperty]
    public RegisterViewModel Input { get; set; }
    public string Mensagem { get; set; }
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    public RegisterModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }
    public IActionResult OnGet()
    {
        if (!_signInManager.IsSignedIn(User))
        {
            return RedirectToPage("/Index");
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser { 
                UserName = Input.UserName,
                NomeCompleto = Input.Name,
                Email = Input.Email,
                CPF = Input.CPF,
            };
            var result = await _userManager.CreateAsync(user, Input.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Input.Role);
                return RedirectToPage("/Admin/Dashboard/Index");
            }
        }
        return Page();
    }
}

public class RegisterViewModel
{
    [Required]
    [Display(Name = "Nome do Usuário")]
    public string UserName { get; set; }
    [Required]
    [Display(Name = "Nome Completo")]
    public string Name { get; set; }
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }
    [Required]
    [Display(Name = "CPF")]
    public required string CPF { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Senha")]
    public string Password { get; set; }
    [DataType(DataType.Password)]
    [Display(Name = "Confirmar Senha")]
    [Compare("Password", ErrorMessage = "As senhas não conferem.")]
    public string ConfirmPassword { get; set; }
    // Role do usuário (Admin ou Client)
    [Required]
    [Display(Name = "Perfil")]
    public string Role { get; set; }
}
