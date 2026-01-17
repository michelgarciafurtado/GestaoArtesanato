using LojaApp.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LojaApp.Models.Produtos;

public class MateriaPrima
{
    [Key]
    public  string IdMateriaPrima { get; set; } = Guid.NewGuid().ToString();
    public string Descricao { get; set; }
    [Precision(18,2)]
    public decimal VlUn { get; set; }
    public TpMedidaEnum TpMedida { get; set; }
    [Precision(18,2)]
    [DisplayName("Valor R$ Estoque")]
    public decimal ValorTotalEstoque { get; private set; }
    [Precision(18,3)]
    [DisplayName("Peso gr/ml Estoque")]
    public decimal PesoTotalEstoque { get; private set; }
    [DisplayName("Quantidade Estoque Un")]
    public decimal QtdEstoque { get; private set; }
    public MateriaPrima()
    {
        
    }
    public MateriaPrima(string descricao, TpMedidaEnum tpmedida)
    {
        Descricao = descricao;
        TpMedida = tpmedida;
        VlUn = 0;
    }
    public void RegistrarEntrada(decimal quantidade, decimal valorTotal, decimal peso)
    {
        QtdEstoque += quantidade;
        ValorTotalEstoque += valorTotal;
        PesoTotalEstoque += peso;
        VlUn = ValorTotalEstoque /  PesoTotalEstoque;
    }


}
