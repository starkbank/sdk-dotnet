using Xunit;
using System;
using StarkBank;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class InvoicePullSubscriptionLogTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void QueryAndGet()
        {
            List<InvoicePullSubscription.Log> logs = InvoicePullSubscription.Log.Query(
                limit: 10
            ).ToList();
            foreach (InvoicePullSubscription.Log log in logs)
            {
                TestUtils.Log(log);
                Assert.NotNull(log.ID);
            }
            InvoicePullSubscription.Log getLog = InvoicePullSubscription.Log.Get(id: logs.First().ID);
            Assert.Equal(logs.First().ID, getLog.ID);
            Console.WriteLine("Test: Query And Get Log - OK");
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<InvoicePullSubscription.Log> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = InvoicePullSubscription.Log.Page(limit: 5, cursor: cursor);
                foreach (InvoicePullSubscription.Log entity in page)
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
            Console.WriteLine("Test: Page Log - OK");
        }
    }
}
