using Xunit;
using StarkBank;
using System.Linq;
using System.Collections.Generic;
using System;

namespace StarkBankTests
{
    public class BoletoTest
    {
        public readonly User user = TestUser.SetDefault();

        [Fact]
        public void CreateGetGetPdfAndDelete()
        {
            List<Boleto> boletos = Boleto.Create(new List<Boleto>() {Example()});
            Boleto boleto = boletos.First();
            Assert.NotNull(boletos.First().ID);
            Boleto getBoleto = Boleto.Get(id: boleto.ID);
            Assert.Equal(getBoleto.ID, boleto.ID);
            string pdf = Boleto.Pdf(id: boleto.ID);
            Assert.True(pdf.Length > 0);
            System.IO.File.WriteAllText("boleto.pdf", pdf);
            Boleto deleteBoleto = Boleto.Get(id: boleto.ID);
            Assert.Equal(deleteBoleto.ID, boleto.ID);
        }

        [Fact]
        public void Query()
        {
            List<Boleto> boletos = Boleto.Query(limit: 101, status: "paid").ToList();
            Assert.Equal(101, boletos.Count);
            Assert.True(boletos.First().ID != boletos.Last().ID);
            foreach (Boleto boleto in boletos)
            {
                Assert.NotNull(boleto.ID);
                Assert.Equal("paid", boleto.Status);
            }
        }

        private Boleto Example()
        {
            return new Boleto(
                amount: 1000000,
                due: DateTime.UtcNow.Date.AddDays(1),
                name: "Random Company",
                streetLine1: "Rua ABC",
                streetLine2: "Ap 123",
                district: "Jardim Paulista",
                city: "São Paulo",
                stateCode: "SP",
                zipCode: "01234-567",
                taxID: "012.345.678-90",
                overdueLimit: 10,
                fine: 0.00,
                interest: 0.00,
                descriptions: new List<Dictionary<string, object>>() {
                    new Dictionary<string, object> {
                        {"text", "product A"},
                        {"amount", 123}
                    },
                    new Dictionary<string, object> {
                        {"text", "product B"},
                        {"amount", 456}
                    },
                    new Dictionary<string, object> {
                        { "text", "product C"},
                        { "amount", 789}
                    }
                }
            );
        }
    }
}
