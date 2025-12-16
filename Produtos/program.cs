using System;
using Produtos.Entities;


namespace Produtos.ConsoleApp;


public class Program
{
	static void main(string[] args)
	{
		Produto p = new Produto() { NomeProduto = "Sabonete GLicerinado Figo"};

		Console.WriteLine($"Produto: {p.NomeProduto}");
	}
}
