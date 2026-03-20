using LojaApp.Data;
using LojaApp.Models.Users;
using LojaApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization(options => 
    { 
        options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    }
);

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Admin","Admin");
}
);

builder.Services.AddDbContextFactory<AppDbContext>(options =>
                     options.UseSqlServer(builder.Configuration.GetConnectionString("WorkConnection")
                     ?? throw new InvalidOperationException("Nao encontrou a string de conexao")
                      )
);

// Configura Identity com suporte a Roles
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    }) 
    .AddEntityFrameworkStores<AppDbContext>() //usa minha classe de contexto para acesso aos dados
    .AddDefaultTokenProviders();

var app = builder.Build();

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

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
