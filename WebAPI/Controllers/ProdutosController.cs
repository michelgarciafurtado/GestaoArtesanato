using Microsoft.AspNetCore.Mvc;
using Produtos.Entities;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProdutosController : Controller
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Produto>>> GetAll()
    {
        List<Produto> produtos = new List<Produto>();
        Produto p1 = new Produto() { NomeProduto = "Sabonete Figo"};
        produtos.Add(p1);
        Produto p2 = new Produto() { NomeProduto = "Body Splash" };
        produtos.Add(p2);
        Produto p3 = new Produto() { NomeProduto = "Sachê perfumado" };
        return Ok(produtos);
    }
}