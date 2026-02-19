using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaApp.Models.Produtos
{
    public class ImagemProduto
    {
        [Key]
        public Guid IdImagem { get; set; } = Guid.NewGuid();
        public required string ImgUrl { get; set; }
        [ForeignKey("IdProduto")]
         public Produto Produto { get; set; } = default!;
        public required string IdProduto { get; set; }
    }
}
