using LojaApp.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace LojaApp.Data;

public static class Seeder
{
    public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    { // Criar roles
      if (!await roleManager.RoleExistsAsync("Admin")) 
        { 
            await roleManager.CreateAsync(new IdentityRole("Admin")); 
        }
        if (!await roleManager.RoleExistsAsync("User"))
        { 
            await roleManager.CreateAsync(new IdentityRole("User")); 
        }
        // Criar usuário admin
        var adminUser = await userManager.FindByEmailAsync("admin@teste.com"); 
        if (adminUser == null) {
            adminUser = new ApplicationUser { UserName = "admin@teste.com",
                                              Email = "admin@teste.com",
                                              CPF= "21554514150",
                                              EmailConfirmed = true,
                                              NomeCompleto="Key Master"
            }; 
            await userManager.CreateAsync(adminUser, "01MasterKey#"); 
            await userManager.AddToRoleAsync(adminUser, "Admin");
        } 
    } 
}
    
