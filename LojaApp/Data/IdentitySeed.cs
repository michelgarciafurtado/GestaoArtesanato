using LojaApp.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace LojaApp.Data;

public static class IdentitySeed
{
    public static async Task CriarPerfis(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        string [] perfis = { "Admin", "User" };

        foreach (var perfil in perfis)
        {
            if (!await roleManager.RoleExistsAsync(perfil))
            {
                await roleManager.CreateAsync(new IdentityRole(perfil));
            }
        }

        var adminEmail = "email@email.com";
        var adminPassword = "KeyMaster";

        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if(adminUser == null)
        {
            adminUser = new ApplicationUser()
            {
                UserName = "01MasterKey#",
                Email = adminEmail,
                NomeCompleto = "Administrador Padrão",
                EmailConfirmed = true
            };

            var resultado = await userManager.CreateAsync(adminUser, adminPassword);

            if(resultado.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

        }


    }
}