using LojaApp.Models.Produtos;
using LojaApp.Models.Utils;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.Serialization;

namespace TesteProdutoModel
{
    public class UnitTest1
    {
        private const decimal V = 3.98m;

        [Fact]
        public void Gerou_Guid_CadastroCategoria_ReturnTrue()
        {
            Categoria c = new Categoria("Sabonete em Barra");

            Assert.True(!string.IsNullOrEmpty(c.IdCategoria));
        }

        [Theory]
        [InlineData(0.0299, 0.065, 0.057, 0.033, 0.109, 3.981)]
        public void CalcularCustoMateriaPrima_DeveRetornarSomaCorreta(decimal gli, decimal esse, decimal glic, decimal lau, decimal cor, decimal esperado)
        {
            Produto sabonete = new Produto()
            {
                NomeProduto = "Sabonete de Aveia",
                Categoria = new Categoria("Sabonete em Barra"),
                medidaEnum = LojaApp.Models.Enums.TpMedidaEnum.gr,
                PesoProduto = 30
            };
            List<Ingrediente> ListaIngredientes = new List<Ingrediente>()
                {
                    new Ingrediente(){Produto = sabonete,
                         MateriaPrima = new MateriaPrima("Base Sabonete GLicerinada",LojaApp.Models.Enums.TpMedidaEnum.gr)
                         {
                             VlUn = gli
                         },
                         QtdIngrediente = 100
                    },
                     new Ingrediente(){Produto = sabonete,
                         MateriaPrima = new MateriaPrima("Essencia",LojaApp.Models.Enums.TpMedidaEnum.ml)
                         {
                             VlUn = esse
                         },
                         QtdIngrediente = 6                    },
                      new Ingrediente(){Produto = sabonete,
                         MateriaPrima = new MateriaPrima("Extrato glicolico",LojaApp.Models.Enums.TpMedidaEnum.ml)
                         {
                             VlUn = glic
                         },
                         QtdIngrediente = 4
                    },
                       new Ingrediente(){Produto = sabonete,
                         MateriaPrima = new MateriaPrima("Lauril",LojaApp.Models.Enums.TpMedidaEnum.ml)
                         {
                             VlUn = lau
                         },
                         QtdIngrediente = 8
                    },
                     new Ingrediente(){Produto = sabonete,
                         MateriaPrima = new MateriaPrima("corante",LojaApp.Models.Enums.TpMedidaEnum.ml)
                         {
                             VlUn = cor
                         },
                         QtdIngrediente  = 1
                    }
                };
            sabonete.ListaIngredientes = ListaIngredientes;
            

            Assert.Equal(esperado, sabonete.CalcularPrecoProduto());

        }
    }
}
