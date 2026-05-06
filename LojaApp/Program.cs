using LojaApp.Data;
using LojaApp.Models.Users;
using LojaApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
//Adiciona serviços de autenticação e autorização
builder.Services.AddAuthorization(options => 
    { 
        options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    }
);

// Restringe o acesso a todas as páginas dentro da pasta "Admin" para usuários com a role "Admin"
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Admin","Admin");
}
);

builder.Services.AddDbContextFactory<AppDbContext>(options =>
                     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
                     ?? throw new InvalidOperationException("Nao encontrou a string de conexao")
                      )
);

// Configura Identity com suporte a Roles
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    }) 
    .AddEntityFrameworkStores<AppDbContext>() //usa minha classe de contexto para acesso aos dados
    .AddDefaultTokenProviders();

builder.Services.AddServicosUtilitarios();

var app = builder.Build();

app.UseStaticFiles();

using (var scope = app.Services.CreateScope()) 
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>(); 
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    await Seeder.SeedAsync(userManager, roleManager);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var supportedCultures = new[] { "pt-BR" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Configura o caminho para a pasta "Imagens" dentro de wwwroot
var pastaImagens = Path.Combine(builder.Environment.WebRootPath, "Imagens");

if (!Directory.Exists(pastaImagens))
{
    Directory.CreateDirectory(pastaImagens);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(pastaImagens),
    RequestPath = "/Imagens"
});

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
