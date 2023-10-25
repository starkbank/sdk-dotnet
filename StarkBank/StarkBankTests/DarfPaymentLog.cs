using System;
using System.Collections.Generic;
using System.Linq;
using StarkBank;
using Xunit;
using StarkCore;

namespace StarkBankTests
{
    public class DarfPaymentLogTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void QueryAndGet()
        {
            List<DarfPayment.Log> logs = DarfPayment.Log.Query(
                limit: 5
            ).ToList();
            Assert.True(logs.First().ID != logs.Last().ID);
            foreach (DarfPayment.Log log in logs)
            {
                TestUtils.Log(log);
                Assert.NotNull(log.ID);
            }
            DarfPayment.Log getLog = DarfPayment.Log.Get(id: logs.First().ID);
            Assert.Equal(getLog.ID, logs.First().ID);
            TestUtils.Log(getLog);
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<DarfPayment.Log> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = DarfPayment.Log.Page(limit: 5, cursor: cursor);
                foreach (DarfPayment.Log entity in page)
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
