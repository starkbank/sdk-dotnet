using Xunit;
using StarkBank;
using System.Linq;
using System.Collections.Generic;

namespace StarkBankTests
{
    public class BrcodePaymentLogTest
    {
        public readonly User user = TestUser.SetDefault();

        [Fact]
        public void QueryAndGet()
        {
            List<BrcodePayment.Log> logs = BrcodePayment.Log.Query(
                limit: 101,
                types: new List<string> { "created" }
            ).ToList();
            Assert.True(logs.Count <= 101);
            Assert.True(logs.First().ID != logs.Last().ID);
            foreach (BrcodePayment.Log log in logs)
            {
                TestUtils.Log(log);
                Assert.NotNull(log.ID);
                Assert.Equal("created", log.Type);
            }
            BrcodePayment.Log getLog = BrcodePayment.Log.Get(id: logs.First().ID);
            Assert.Equal(getLog.ID, logs.First().ID);
            TestUtils.Log(getLog);
        }
    }
}
