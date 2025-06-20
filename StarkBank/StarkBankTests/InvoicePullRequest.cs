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
            List<Invoice> invoices = Invoice.Create(new List<Invoice>() { Example() });
            string invoiceId = invoices.First().ID;
            Console.WriteLine("===========================================================");
            Console.WriteLine($"Created invoice with ID: {invoiceId}");
            Console.WriteLine("===========================================================");

            List<InvoicePullSubscription> subscriptions = InvoicePullSubscription.Create(new List<InvoicePullSubscription> { Example("push") }, user);
            string subscriptionId = subscriptions.First().ID;
            Console.WriteLine("===========================================================");
            Console.WriteLine($"Created subscription with ID: {subscriptionId}");
            Console.WriteLine("===========================================================");

            List<InvoicePullRequest> requests = InvoicePullRequest.Create(new List<InvoicePullRequest> { Example("default", invoiceId, subscriptionId) }, user);
            InvoicePullRequest request = requests.First();
            TestUtils.Log(request);
            Assert.NotNull(request.ID);

            List<InvoicePullRequest> retries = InvoicePullRequest.Create(new List<InvoicePullRequest> { Example("retry", invoiceId, subscriptionId) }, user);
            InvoicePullRequest retry = retries.First();
            TestUtils.Log(retry);
            Assert.NotNull(retry.ID);
        }

        // [Fact]
        // public void Query()
        // {
        //     List<InvoicePullRequest> requests = InvoicePullRequest.Query(limit: 5).ToList();
        //     foreach (InvoicePullRequest request in requests)
        //     {
        //         TestUtils.Log(request);
        //         Assert.NotNull(request.ID);
        //     }
        // }

        // [Fact]
        // public void QueryAndGet()
        // {
        //     List<InvoicePullRequest> requests = InvoicePullRequest.Query(limit: 1).ToList();
        //     InvoicePullRequest request = InvoicePullRequest.Get(requests.First().ID, user);
        //     Assert.Equal(request.ID, requests.First().ID);
        // }

        // [Fact]
        // public void CreateAndDelete()
        // {
        //     List<InvoicePullSubscription> subscriptions = InvoicePullSubscription.Query(limit: 1, status: "active").ToList();
        //     InvoicePullSubscription subscription = InvoicePullSubscription.Get(subscriptions.First().ID, user);

        //     string invoiceId = subscriptions.First().Data["invoiceId"]?.ToString();
        //     string subscriptionId = subscriptions.First().ID;

        //     List<InvoicePullRequest> requests = InvoicePullRequest.Create(new List<InvoicePullRequest> { Example("default", invoiceId, subscriptionId) }, user);
        //     InvoicePullRequest request = InvoicePullRequest.Delete(requests.First().ID, user);
        //     Assert.Equal(requests.First().ID, request.ID);
        //     TestUtils.Log(request);
        // }

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

        internal static Invoice Example()
        {
            return new Invoice(
                amount: 0,
                due: DateTime.Now.AddDays(10),
                name: "Random Company",
                taxID: "012.345.678-90",
                fine: 0.00,
                interest: 0.00,
                tags: new List<string> { "custom", "tags" },
                descriptions: new List<Dictionary<string, object>>() {
                    new Dictionary<string, object> {
                        {"key", "product A"},
                        {"value", "small"}
                    },
                    new Dictionary<string, object> {
                        {"key", "product B"},
                        {"value", "medium"}
                    }
                },
                discounts: new List<Dictionary<string, object>>() {
                    new Dictionary<string, object> {
                        {"percentage", 5},
                        {"due", DateTime.Now.AddDays(1)}
                    },
                    new Dictionary<string, object> {
                        {"percentage", 3.5},
                        {"due", DateTime.Now.AddDays(2)}
                    }
                },
                rules: new List<Invoice.Rule>() {
                    new Invoice.Rule(
                        key: "allowedTaxIds",
                        value: new List<string> {"012.345.678-90", "45.059.493/0001-73"}
                    )
                }
            );
        }

        internal static InvoicePullSubscription Example(string type)
        {
            return new InvoicePullSubscription(
                amount: 0,
                amountMinLimit: 5000,
                data: new Dictionary<string, object> {
                {"accountNumber", "9123900000"},
                {"bankCode", "05097757"},
                {"branchCode", "1126"},
                {"taxId", "20.018.183/0001-80"}
                },
                displayDescription: "Dragon Travel Fare",
                externalID: Guid.NewGuid().ToString(),
                interval: "month",
                name: "John Snow",
                pullMode: "manual",
                pullRetryLimit: 3,
                start: DateTime.Today.Date.AddDays(1),
                end: DateTime.Today.Date.AddDays(31),
                referenceCode: "contract-12345",
                tags: new List<string>(),
                taxID: "012.345.678-90",
                type: type
            );
        }
    }
}