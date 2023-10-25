using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;
using StarkCore;


namespace StarkBankTests
{
    public class UtilityPaymentLogTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void QueryAndGet()
        {
            List<UtilityPayment.Log> logs = UtilityPayment.Log.Query(
                limit: 101,
                types: new List<string> { "success" }
            ).ToList();
            Assert.True(logs.Count <= 101);
            Assert.True(logs.First().ID != logs.Last().ID);
            foreach (UtilityPayment.Log log in logs)
            {
                TestUtils.Log(log);
                Assert.NotNull(log.ID);
                Assert.Equal("success", log.Type);
            }
            UtilityPayment.Log getLog = UtilityPayment.Log.Get(id: logs.First().ID);
            Assert.Equal(getLog.ID, logs.First().ID);
            TestUtils.Log(getLog);
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<UtilityPayment.Log> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = UtilityPayment.Log.Page(limit: 5, cursor: cursor);
                foreach (UtilityPayment.Log entity in page)
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
