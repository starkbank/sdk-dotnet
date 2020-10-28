using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;

namespace StarkBankTests
{
    public class BoletoHolmesLogTest
    {
        public readonly User user = TestUser.SetDefault();

        [Fact]
        public void QueryAndGet()
        {
            List<BoletoHolmes.Log> logs = BoletoHolmes.Log.Query(
                limit: 101,
                types: new List<string> { "solved" }
            ).ToList();
            Assert.True(logs.Count <= 101);
            Assert.True(logs.First().ID != logs.Last().ID);
            foreach (BoletoHolmes.Log log in logs)
            {
                Console.WriteLine(log);
                Assert.NotNull(log.ID);
                Assert.Equal("solved", log.Type);
            }
            BoletoHolmes.Log getLog = BoletoHolmes.Log.Get(id: logs.First().ID);
            Assert.Equal(getLog.ID, logs.First().ID);
            TestUtils.Log(getLog);
        }
    }
}
