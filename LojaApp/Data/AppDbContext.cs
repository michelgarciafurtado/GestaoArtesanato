using LojaApp.Models.EntradaMateriais;
using LojaApp.Models.Produtos;
using LojaApp.Models.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LojaApp.Data
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Ingrediente> Ingredientes { get; set; }
        public DbSet<LojaApp.Models.Produtos.Custos> Custos { get; set; } = default!;
        public DbSet<ImagemProduto> ImagensProdutos { get; set; }
        public DbSet<MateriaPrima> MateriasPrimas { get; set; }
        public DbSet<EntradaMaterial> Entradas { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
            configurationBuilder.Properties<string>().HaveMaxLength(150);
            base.ConfigureConventions(configurationBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>()
                .Property(p => p.medidaEnum)
                .HasConversion<string>();

            modelBuilder.Entity<MateriaPrima>()
                .Property(s => s.TpMedida)
                .HasConversion<string>();

            modelBuilder.Entity<Ingrediente>()
                .HasOne(i => i.Produto)
                .WithMany(p => p.ListaIngredientes)
                .HasForeignKey(i => i.IdProduto)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ingrediente>()
                .HasOne(i => i.MateriaPrima)
                .WithMany()
                .HasForeignKey(i => i.IdMateriaPrima)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EntradaMaterial>()
                .Property(e => e.TipoMedida)
                .HasConversion<string>();

            modelBuilder.Entity<EntradaMaterial>()
                .HasOne(e => e.MateriaPrima)
                .WithMany()
                .HasForeignKey(e => e.IdMateriaPrima)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
       
    }
}
