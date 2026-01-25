namespace LojaApp.Models.Pedidos
{
    public class SolicitacaoProduto
    {
        public DateOnly DataPedido { get; set; } = DateOnly.FromDateTime(DateTime.Today);
        public string Nome { get; set; }
        public  string CelContato { get; set; }

        public string NomeProduto { get; set; }
        public int quantidade { get; set; }
    }
}
