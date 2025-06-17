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
        public void CreateAndRetry()
        {
            List<InvoicePullSubscription> subscriptions = InvoicePullSubscription.Query(limit: 1, status: "active").ToList();
            InvoicePullSubscription subscription = InvoicePullSubscription.Get(subscriptions.First().ID, user);
            
            string invoiceId = subscriptions.First().Data["invoiceId"]?.ToString();
            string subscriptionId = subscriptions.First().ID;

            List<InvoicePullRequest> requests = InvoicePullRequest.Create(new List<InvoicePullRequest> { Example("default", invoiceId, subscriptionId) }, user);
            List<InvoicePullRequest> requests = InvoicePullRequest.Create(new List<InvoicePullRequest> { Example("retry", invoiceId, subscriptionId) }, user);
            InvoicePullRequest request = requests.First();

            TestUtils.Log(request);
            Assert.NotNull(request.ID);
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
        }

        [Fact]
        public void QueryAndGet()
        {
            List<InvoicePullRequest> requests = InvoicePullRequest.Query(limit: 1).ToList();
            InvoicePullRequest request = InvoicePullRequest.Get(requests.First().ID, user);
            Assert.Equal(request.ID, requests.First().ID);
        }

        [Fact]
        public void CreateAndDelete()
        {
            List<InvoicePullSubscription> subscriptions = InvoicePullSubscription.Query(limit: 1, status: "active").ToList();
            InvoicePullSubscription subscription = InvoicePullSubscription.Get(subscriptions.First().ID, user);
            
            string invoiceId = subscriptions.First().Data["invoiceId"]?.ToString();
            string subscriptionId = subscriptions.First().ID;

            List<InvoicePullRequest> requests = InvoicePullRequest.Create(new List<InvoicePullRequest> { Example("default", invoiceId, subscriptionId) }, user);
            List<InvoicePullRequest> requests = InvoicePullRequest.Create(new List<InvoicePullRequest> { Example("retry", invoiceId, subscriptionId) }, user);
            InvoicePullRequest request = requests.First();

            TestUtils.Log(request);
            Assert.NotNull(request.ID);
        }

        internal static InvoicePullRequest Example(string attemptType, string invoiceId = null, string subscriptionId = null)
        {
            InvoicePullRequest example = null;
            if (attemptType == "default")
            {
                example = new InvoicePullRequest(
                    attemptType: attemptType,
                    due: DateTime.Today.Date.AddDays(10),
                    invoiceId: invoiceId,
                    subscriptionId: subscriptionId,
                    tags: new List<string>()
                );
            }
            if (attemptType == "retry")
            {
                example = new InvoicePullRequest(
                    attemptType: attemptType,
                    due: DateTime.Today.Date.AddDays(10),
                    invoiceId: invoiceId,
                    subscriptionId: subscriptionId,
                    tags: new List<string>()
                );
            }
            return example;
        }
    }
}