using Microsoft.AspNetCore.Identity;

namespace LojaApp.Models.Users
{
    public class ApplicationUser:IdentityUser
    {
        //propriedades adicionais para o usuario podem ser adicionadas aqui
        public string NomeCompleto { get; set; }
        public string CPF { get; set; }

        public ApplicationUser()
        {
            
        }
    }
}
