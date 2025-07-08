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
        public void CreateAndCancel()
        {
            List<Invoice> invoices = Invoice.Create(new List<Invoice>() { InvoiceTest.Example() });
            string invoiceId = invoices.First().ID;
            List<InvoicePullSubscription> subscriptions = InvoicePullSubscription.Create(new List<InvoicePullSubscription> { InvoicePullSubscriptionTest.Example("qrcodeAndPayment") });
            string subscriptionId = subscriptions.First().ID;

            List<InvoicePullRequest> requests = InvoicePullRequest.Create(new List<InvoicePullRequest> { Example(invoiceId, subscriptionId) });
            InvoicePullRequest request = requests.First();
            Assert.NotNull(request.ID);

            InvoicePullRequest cancelRequest = InvoicePullRequest.Cancel(request.ID);
            Assert.NotNull(cancelRequest.ID);
            Console.WriteLine("Test: Create And Cancel Request - OK");
        }

        [Fact]
        public void Query()
        {
            List<InvoicePullRequest> requests = InvoicePullRequest.Query(limit: 5).ToList();
            foreach (InvoicePullRequest request in requests)
            {
                TestUtils.Log(request);
                Assert.NotNull(request.ID);
            }
            Console.WriteLine("Test: Query Request - OK");
        }

        [Fact]
        public void QueryAndGet()
        {
            List<InvoicePullRequest> requests = InvoicePullRequest.Query(limit: 1).ToList();
            InvoicePullRequest request = InvoicePullRequest.Get(requests.First().ID, user);
            Assert.Equal(request.ID, requests.First().ID);
            Console.WriteLine("Test: Query And Get Request - OK");
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
            Console.WriteLine("Test: Page Request - OK");
        }

        internal static InvoicePullRequest Example(string invoiceId, string subscriptionId)
        {
            return new InvoicePullRequest(
                attemptType: "default",
                due: DateTime.Today.Date.AddDays(4),
                invoiceId: invoiceId,
                subscriptionId: subscriptionId,
                tags: new List<string> { }
            );
        }
    }
}
