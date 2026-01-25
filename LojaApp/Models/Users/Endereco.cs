using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace LojaApp.Models.Users
{
    public class Endereco
    {
        [Key]
        public string IdEndereco { get; set; } = Guid.NewGuid().ToString();
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string IdCliente { get; set; }
        [ForeignKey("IdCliente")]
        public required Cliente Cliente { get; set; }
        public required string Rua { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O campo {0} deve ser maior que 0.")]
        public required int Numero { get; set; }
        [StringLength(80, ErrorMessage = "Não pode exceder 80 caracteres.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public required string Cidade { get; set; }
        [StringLength(100, ErrorMessage = "Não pode exceder 100 caracteres.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public required string Bairro { get; set; }
        public string Compl { get; set; }
        [StringLength(8, ErrorMessage = "O campo {0} deve 8 digitos.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public required string CEP { get; set; }
        public string? Referencia { get; set; }
        public TipoEnderecoEnum TipoEndereco { get; set; }

    }
}
