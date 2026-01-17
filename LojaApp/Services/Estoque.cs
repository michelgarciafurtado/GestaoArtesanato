using LojaApp.Data;

namespace LojaApp.Services;

public class Estoque
{
    private readonly Data.AppDbContext _context;
    public Estoque(AppDbContext context) 
    {
        _context = context;
    }
    public async Task ProcessarEntrada()
    {
            
    }

    public decimal ConsultarQuantidadeEstoque(string IdMateriaPrima)
    {
        // Lógica para consultar o estoque de uma substância específica
        return 0m;
    }

    public decimal ConsultarValorEstoque(string IdMateriaPrima)
    {
        // Lógica para consultar o valor total do estoque de uma substância específica
        return 0m;
    }
}
