using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;
using StarkCore;


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

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<BoletoHolmes> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = BoletoHolmes.Page(limit: 5, cursor: cursor);
                foreach (BoletoHolmes entity in page)
                {
                    Assert.DoesNotContain(entity.ID, ids);
                    ids.Add(entity.ID);
                }
                if (cursor == null)
                {
                    break;
                }
            }
            Assert.True(ids.Count == 10);
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
