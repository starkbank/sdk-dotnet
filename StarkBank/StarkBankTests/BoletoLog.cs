using Xunit;
using System;
using StarkBank;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class BoletoLogTest
    {
        public readonly User user = TestUser.SetDefault();

        [Fact]
        public void QueryAndGet()
        {
            List<Boleto.Log> logs = Boleto.Log.Query(limit: 101, before: DateTime.Now, types: new List<string> { "paid" }).ToList();
            Assert.Equal(101, logs.Count);
            Assert.True(logs.First().ID != logs.Last().ID);
            foreach (Boleto.Log log in logs)
            {
                Assert.NotNull(log.ID);
                Assert.Equal("paid", log.Type);
            }
            Boleto.Log getBoleto = Boleto.Log.Get(id: logs.First().ID);
            Assert.Equal(getBoleto.ID, logs.First().ID);
        }
    }
}
