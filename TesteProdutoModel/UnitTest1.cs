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
                PesoProduto = PersonalFormatter.DecimalFormatter("30")
            };
            List<Ingrediente> ListaIngredientes = new List<Ingrediente>()
                {
                    new Ingrediente(){Produto = sabonete,
                         Substancia = new Substancia("Base Sabonete GLicerinada",LojaApp.Models.Enums.TpMedidaEnum.gr)
                         {
                             VlUn = gli
                         },
                         QtdIngrediente = 100m
                    },
                     new Ingrediente(){Produto = sabonete,
                         Substancia = new Substancia("Essencia",LojaApp.Models.Enums.TpMedidaEnum.ml)
                         {
                             VlUn = esse
                         },
                         QtdIngrediente = 6m
                    },
                      new Ingrediente(){Produto = sabonete,
                         Substancia = new Substancia("Extrato glicolico",LojaApp.Models.Enums.TpMedidaEnum.ml)
                         {
                             VlUn = glic
                         },
                         QtdIngrediente = 4m
                    },
                       new Ingrediente(){Produto = sabonete,
                         Substancia = new Substancia("Lauril",LojaApp.Models.Enums.TpMedidaEnum.ml)
                         {
                             VlUn = lau
                         },
                         QtdIngrediente = 8m
                    },
                     new Ingrediente(){Produto = sabonete,
                         Substancia = new Substancia("corante",LojaApp.Models.Enums.TpMedidaEnum.ml)
                         {
                             VlUn = cor
                         },
                         QtdIngrediente  = 1m
                    }
                };
            sabonete.ListaIngredientes = ListaIngredientes;
            

            Assert.Equal(esperado, sabonete.CalcularCustoMateriaPrima());

        }
    }
}
