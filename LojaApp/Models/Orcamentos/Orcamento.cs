using System;

namespace LojaApp.Models.Orcamentos;

    public class Orcamento
{
    public string IdOrcamento { get; set; } = new Guid().ToString();

    public Orcamento()
	{
	}
}
