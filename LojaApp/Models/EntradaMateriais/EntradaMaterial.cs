using LojaApp.Models.Enums;
using LojaApp.Models.Produtos;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace LojaApp.Models.EntradaMateriais
{
    public class EntradaMaterial
    {
        [Key]
        public Guid IdEntrada { get; set; } = Guid.NewGuid();
        public DateOnly DataEntrada { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public required string IdSubstancia { get; set; }
        [ForeignKey(nameof(IdSubstancia))]
        public Substancia? Substancia { get; set; }
        public int Quantidade { get; set; } = 1;
        [Display(Name = "Peso Un")]
        [Precision(18, 3)]
        public decimal PesoUn { get; set; }
        public TpMedidaEnum TipoMedida { get; set; }
        [Precision(18, 2)]
        [Display(Name = "Valor Un. R$")]
        public  decimal ValorUn { get; set; }
        [Precision(18, 2)]
        [Display(Name = "Valor Total R$")]
        public  decimal ValorTotal => Quantidade * ValorUn;
        [Precision(18, 3)]
        [Display(Name = "Peso Total gr/ml")]
        public  decimal PesoTotal => Quantidade * PesoUn;
    }
}
