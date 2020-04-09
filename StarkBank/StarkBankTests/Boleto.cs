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
        public void Create()
        {
            List<Boleto> boletos = Boleto.Create(new List<Boleto>() {Example()});
            Assert.NotNull(boletos.First().ID);
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
