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

        [Fact]
        public void TestParseInvoicePullRequestEvent()
        {
            string content = "{\"event\": {\"created\": \"2025-07-25T17:36:41.040267+00:00\", \"id\": \"4805265536843776\", \"log\": {\"created\": \"2025-07-25T17:36:39.571648+00:00\", \"description\": \"\", \"errors\": [], \"id\": \"5789040171286528\", \"reason\": \"\", \"request\": {\"attemptType\": \"default\", \"created\": \"2025-07-25T17:36:37.201258+00:00\", \"displayDescription\": \"\", \"due\": \"2025-07-30T07:00:00+00:00\", \"externalId\": \"a15c4821d1c2413a82a4f3cfeee1315e\", \"id\": \"5397390693498880\", \"installmentId\": \"5424937942646784\", \"invoiceId\": \"5118508564217856\", \"status\": \"pending\", \"subscriptionId\": \"5181739848695808\", \"tags\": [], \"updated\": \"2025-07-25T17:36:39.571665+00:00\"}, \"type\": \"pending\"}, \"subscription\": \"invoice-pull-request\", \"workspaceId\": \"6235001133727744\"}}";
            string validSignature = "MEUCIQCvbPc+mWLLL5nwvOBy/3MVJ3JU9fG/rNmyqmHtaeJA9wIgOR8Tw75MSj7lR9DPqhM62tlq+cFkbw14T4KmDBeC5rM=";
            Event parsedEvent = Event.Parse(content, validSignature);
            Assert.NotNull(parsedEvent.ID);
            Assert.Equal(typeof(InvoicePullRequest.Log), parsedEvent.Log.GetType());

            Console.WriteLine("Test: Parse Invoice Pull Request Event - OK");
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
