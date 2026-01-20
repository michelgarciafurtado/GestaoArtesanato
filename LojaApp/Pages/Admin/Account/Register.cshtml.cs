using LojaApp.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LojaApp.Pages.Admin.Account
{
    public class RegisterModel : PageModel
    {
       private readonly SignInManager<ApplicationUser> _signInManager;
        public RegisterModel(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public IActionResult OnGet()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToPage("/Admin/Dashboard/Index");
            }
            return Page();
        }
    }
}
