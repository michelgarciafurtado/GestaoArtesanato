using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LojaApp.Models.Users;

public class ApplicationUser:IdentityUser
{
    //propriedades adicionais para o usuario podem ser adicionadas aqui
    public string NomeCompleto { get; set; }
    public string CPF { get; set; }
    public Cliente Cliente { get; set; }

    public ApplicationUser()
    {
    
    }
}

public record RegisterViewModel { 
    [Required]
    [Display(Name = "Nome completo")] 
    public string NomeCompleto { get; set; }
    [Required]
    [Display(Name = "CPF")]
    public string CPF { get; set; } 
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [DataType(DataType.Password)]
    [Display(Name = "Confirmar senha")]
    [Compare("Password", ErrorMessage = "As senhas não conferem.")]
    public string ConfirmPassword { get; set; }
}
