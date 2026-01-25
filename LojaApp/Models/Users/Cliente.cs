using LojaApp.Models.Pedidos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaApp.Models.Users;

public class Cliente
{
    [Key]
    public string IdCliente { get; set; } = Guid.NewGuid().ToString();
    [Required]
    public string UserId { get; set; }
    [ForeignKey(name: "UserId")]
    public ApplicationUser User { get; set; }
    public string Nome { get; set; }
    public string CPF { get; set; }
    public string ClienteId { get; set; }
    [ForeignKey(name: "ClienteId")]
    public ICollection<Endereco> Enderecos { get; set; }
    [Required]
    [RegularExpression(@"^\(\d{2}\) \d{4,5}-\d{4}$", ErrorMessage = "O telefone deve estar no formato (XX) XXXXX-XXXX")]
    public required string Telefone { get; set; }
    [Required]
    public DateTime DataCadastro { get; set; } = DateTime.Now;
    [Required]
    public bool Ativo { get; set; } = true;
    [Required]
    public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
