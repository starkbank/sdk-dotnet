using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class BoletoHolmesTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void CreateAndGet()
        {
            List<BoletoHolmes> holmes = BoletoHolmes.Create(new List<BoletoHolmes>() { Example() });
            BoletoHolmes sherlock = holmes.First();
            Assert.NotNull(sherlock.ID);
            BoletoHolmes getBoletoHolmes = BoletoHolmes.Get(id: sherlock.ID);
            Assert.Equal(getBoletoHolmes.ID, sherlock.ID);
            TestUtils.Log(holmes);
        }

        [Fact]
        public void Query()
        {
            List<BoletoHolmes> holmes = BoletoHolmes.Query(limit: 101, status: "solved").ToList();
            Assert.True(holmes.Count <= 101);
            Assert.True(holmes.First().ID != holmes.Last().ID);
            foreach (BoletoHolmes sherlock in holmes)
            {
                TestUtils.Log(sherlock);
                Assert.NotNull(sherlock.ID);
                Assert.Equal("solved", sherlock.Status);
            }
        }

        private static BoletoHolmes Example()
        {
            Boleto boleto = BoletoTest.Example();
            List<Boleto> boletos = Boleto.Create(new List<Boleto> { boleto });
            boleto = boletos.First();

            return new BoletoHolmes(
                boletoID: boleto.ID,
                tags: new List<string> { "sherlock" }
            );
        }
    }
}
