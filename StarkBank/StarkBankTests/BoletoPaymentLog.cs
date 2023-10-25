using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;
using StarkCore;

namespace StarkBankTests
{
    public class BoletoPaymentLogTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void QueryAndGet()
        {
            List<BoletoPayment.Log> logs = BoletoPayment.Log.Query(
                limit: 101,
                types: new List<string> { "failed" }
            ).ToList();
            Assert.True(logs.Count <= 101);
            Assert.True(logs.First().ID != logs.Last().ID);
            foreach (BoletoPayment.Log log in logs)
            {
                TestUtils.Log(log);
                Assert.NotNull(log.ID);
                Assert.Equal("failed", log.Type);
            }
            BoletoPayment.Log getLog = BoletoPayment.Log.Get(id: logs.First().ID);
            Assert.Equal(getLog.ID, logs.First().ID);
            TestUtils.Log(getLog);
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<BoletoPayment.Log> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = BoletoPayment.Log.Page(limit: 5, cursor: cursor);
                foreach (BoletoPayment.Log entity in page)
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
    }
}
