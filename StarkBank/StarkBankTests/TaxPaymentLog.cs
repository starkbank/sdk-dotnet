using System;
using System.Collections.Generic;
using System.Linq;
using StarkBank;
using Xunit;

namespace StarkBankTests
{
    public class TaxPaymentLogTest
    {

        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void QueryAndGet()
        {
            List<TaxPayment.Log> logs = TaxPayment.Log.Query(
                limit: 5
            ).ToList();
            Assert.True(logs.First().ID != logs.Last().ID);
            foreach (TaxPayment.Log log in logs)
            {
                TestUtils.Log(log);
                Assert.NotNull(log.ID);
            }
            TaxPayment.Log getLog = TaxPayment.Log.Get(id: logs.First().ID);
            Assert.Equal(getLog.ID, logs.First().ID);
            TestUtils.Log(getLog);
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<TaxPayment.Log> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = TaxPayment.Log.Page(limit: 5, cursor: cursor);
                foreach (TaxPayment.Log entity in page)
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
