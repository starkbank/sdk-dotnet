using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class InvoicePullRequestTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Query()
        {
            List<InvoicePullRequest> requests = InvoicePullRequest.Query(limit: 5).ToList();
            foreach (InvoicePullRequest request in requests)
            {
                TestUtils.Log(request);
                Assert.NotNull(request.ID);
            }
        }

        [Fact]
        public void QueryAndGet()
        {
            List<InvoicePullRequest> requests = InvoicePullRequest.Query(limit: 1).ToList();
            InvoicePullRequest request = InvoicePullRequest.Get(requests.First().ID, user);
            Assert.Equal(request.ID, requests.First().ID);
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<InvoicePullRequest> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = InvoicePullRequest.Page(limit: 5, cursor: cursor);
                foreach (InvoicePullRequest entity in page)
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