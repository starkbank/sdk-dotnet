using Xunit;
using System;
using StarkBank;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class InvoicePullRequestLogTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void QueryAndGet()
        {
            List<InvoicePullRequest.Log> logs = InvoicePullRequest.Log.Query(
                limit: 101,
                before: DateTime.Now.Date,
                types: new List<string> { "pending" }
            ).ToList();
            Assert.True(logs.First().ID != logs.Last().ID);
            foreach (InvoicePullRequest.Log log in logs)
            {
                TestUtils.Log(log);
                Assert.NotNull(log.ID);
                Assert.Equal("pending", log.Type);
            }
            InvoicePullRequest.Log getLog = InvoicePullRequest.Log.Get(id: logs.First().ID);
            Assert.Equal(getLog.ID, logs.First().ID);
            Console.WriteLine("Test: Query And Get Log - OK");
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<InvoicePullRequest.Log> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = InvoicePullRequest.Log.Page(limit: 5, cursor: cursor);
                foreach (InvoicePullRequest.Log entity in page)
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
