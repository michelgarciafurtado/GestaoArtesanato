using LojaApp.Models.Produtos;
using Microsoft.EntityFrameworkCore;

namespace LojaApp.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        {
            
        }

        DbSet<Produto> Produtos { get; set; }
        DbSet<Categoria> Categorias { get; set; }
        DbSet<Ingrediente> Ingredientes { get; set; }
        DbSet<Substancia> Substancias { get; set; }
    }
}
