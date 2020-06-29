using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class DasPaymentLogTest
    {
        public readonly User user = TestUser.SetDefault();

        [Fact]
        public void QueryAndGet()
        {
            List<DasPayment.Log> logs = DasPayment.Log.Query(
                limit: 101,
                types: new List<string> { "success" }
            ).ToList();
            Assert.True(logs.Count <= 101);
            Assert.True(logs.First().ID != logs.Last().ID);
            foreach (DasPayment.Log log in logs)
            {
                Console.WriteLine(log);
                Assert.NotNull(log.ID);
                Assert.Equal("success", log.Type);
            }
            DasPayment.Log getLog = DasPayment.Log.Get(id: logs.First().ID);
            Assert.Equal(getLog.ID, logs.First().ID);
            Console.WriteLine(getLog);
        }
    }
}
