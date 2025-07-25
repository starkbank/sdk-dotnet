using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;


namespace StarkBankTests
{
    public class InvoicePullSubscriptionTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void CreatePush()
        {
            string type = "push";
            List<InvoicePullSubscription> subscriptions = InvoicePullSubscription.Create(new List<InvoicePullSubscription> { Example(type) }, user);
            InvoicePullSubscription subscription = subscriptions.First();
            Assert.NotNull(subscription.ID);
            Assert.NotNull(subscription.Name);
            Assert.NotNull(subscription.TaxID);
            Assert.Equal(type, subscription.Type);
            Console.WriteLine("Test: Create Push Subscription - OK");
        }

        [Fact]
        public void CreateQrCode()
        {
            string type = "qrcode";
            List<InvoicePullSubscription> subscriptions = InvoicePullSubscription.Create(new List<InvoicePullSubscription> { Example(type) }, user);
            InvoicePullSubscription subscription = subscriptions.First();
            Assert.NotNull(subscription.ID);
            Assert.NotNull(subscription.Name);
            Assert.NotNull(subscription.TaxID);
            Console.WriteLine("Test: Create QR Code Subscription - OK");
        }

        [Fact]
        public void CreateQrCodeAndPayment()
        {
            string type = "qrcodeAndPayment";
            List<InvoicePullSubscription> subscriptions = InvoicePullSubscription.Create(new List<InvoicePullSubscription> { Example(type) }, user);
            InvoicePullSubscription subscription = subscriptions.First();
            Assert.NotNull(subscription.ID);
            Assert.NotNull(subscription.Name);
            Assert.NotNull(subscription.TaxID);
            Console.WriteLine("Test: Created QR Code and Payment Subscription - OK");
        }

        [Fact]
        public void CreatePaymentAndOrQrCode()
        {
            string type = "paymentAndOrQrcode";
            List<InvoicePullSubscription> subscriptions = InvoicePullSubscription.Create(new List<InvoicePullSubscription> { Example(type) }, user);
            InvoicePullSubscription subscription = subscriptions.First();
            Assert.NotNull(subscription.ID);
            Assert.NotNull(subscription.Name);
            Assert.NotNull(subscription.TaxID);
            Console.WriteLine("Test: Create Payment and/or QR Code Subscription - OK");
        }

        [Fact]
        public void Query()
        {
            List<InvoicePullSubscription> subscriptions = InvoicePullSubscription.Query(limit: 5).ToList();
            foreach (InvoicePullSubscription subscription in subscriptions)
            {
                Assert.NotNull(subscription.ID);
                Assert.NotNull(subscription.Name);
                Assert.NotNull(subscription.TaxID);
            }
            Console.WriteLine("Test: Query Subscriptions - OK");
        }

        [Fact]
        public void QueryAndGet()
        {
            List<InvoicePullSubscription> subscriptions = InvoicePullSubscription.Query(limit: 1).ToList();
            InvoicePullSubscription subscription = InvoicePullSubscription.Get(subscriptions.First().ID, user);
            Assert.Equal(subscription.ID, subscriptions.First().ID);
            Assert.Equal(subscription.Name, subscriptions.First().Name);
            Assert.Equal(subscription.TaxID, subscriptions.First().TaxID);
            Console.WriteLine("Test: Query and Get Subscription - OK");
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<InvoicePullSubscription> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = InvoicePullSubscription.Page(limit: 5, cursor: cursor);
                foreach (InvoicePullSubscription entity in page)
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
            Console.WriteLine("Test: Page Subscriptions - OK");
        }

        [Fact]
        public void CreateAndDelete()
        {
            List<InvoicePullSubscription> subscriptions = InvoicePullSubscription.Create(new List<InvoicePullSubscription> { Example("push") }, user);
            InvoicePullSubscription subscription = subscriptions.First();
            // Change Subscription Status to "active"
            InvoicePullSubscription cancelSubscription = InvoicePullSubscription.Cancel(subscription.ID);
            Assert.Equal(subscription.ID, cancelSubscription.ID);
            Console.WriteLine("Test: Create and Delete Subscription - OK");
        }

        [Fact]
        public void TestParseInvoicePullSubscriptionEvent()
        {
            string content = "{\"event\": {\"created\": \"2025-07-25T17:03:49.207194+00:00\", \"id\": \"5339088045473792\", \"log\": {\"created\": \"2025-07-25T17:03:47.305348+00:00\", \"description\": \"\", \"errors\": [], \"id\": \"4814349822590976\", \"reason\": \"\", \"subscription\": {\"amount\": 1500, \"amountMinLimit\": 0, \"bacenId\": \"RR3990842720250725fwJYIfdOGeF\", \"brcode\": \"00020101021226180014br.gov.bcb.pix5204000053039865802BR5925Stark Sociedade de Credit6009Sao Paulo62070503***80930014br.gov.bcb.pix2571brcode-h.sandbox.starkinfra.com/v2/rec/d2766b29d5184e90853405a9720439a16304686F\", \"created\": \"2025-07-25T17:03:47.280303+00:00\", \"data\": {}, \"displayDescription\": \"fist test - lucas4\", \"due\": \"2025-07-27T17:03:46.709858+00:00\", \"end\": \"2055-06-23T03:00:00+00:00\", \"externalId\": \"3581163bfe96436794a5284d5eb7a5b9\", \"id\": \"4786208928432128\", \"interval\": \"week\", \"name\": \"jaojao\", \"pullMode\": \"manual\", \"pullRetryLimit\": 3, \"referenceCode\": \"ricandalarrapateumanual\", \"start\": \"2055-06-16T03:00:00+00:00\", \"status\": \"created\", \"tags\": [], \"taxId\": \"457.965.518-41\", \"type\": \"qrcode\", \"updated\": \"2025-07-25T17:03:47.305366+00:00\"}, \"type\": \"created\"}, \"subscription\": \"invoice-pull-subscription\", \"workspaceId\": \"6235001133727744\"}}";
            string validSignature = "MEYCIQDPoI8o1N1qbtq24wY2cvQ4reAxHv/AZs901L6WKU8ylwIhAJ4+ARRrgNARFu/1SbHnuDoHX4EtvkZDCmxTjP9WsT1b";
            Event parsedEvent = Event.Parse(content, validSignature);
            Assert.NotNull(parsedEvent.ID);
            Assert.Equal(typeof(InvoicePullSubscription.Log), parsedEvent.Log.GetType());

            Console.WriteLine("Test: Parse Invoice Pull Subscription Event - OK");
        }

        internal static InvoicePullSubscription Example(string type)
        {
            InvoicePullSubscription example = null;
            if (type == "push")
            {
                example = new InvoicePullSubscription(
                    amount: 1000000,
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
                    start: DateTime.Today.Date.AddDays(5),
                    end: DateTime.Today.Date.AddDays(35),
                    referenceCode: "contract-12345",
                    tags: new List<string>(),
                    taxID: "012.345.678-90",
                    type: type
                );
            }
            if (type == "qrcode")
            {
                example = new InvoicePullSubscription(
                    amount: 1000000,
                    amountMinLimit: 5000,
                    displayDescription: "Dragon Travel Fare",
                    externalID: Guid.NewGuid().ToString(),
                    interval: "month",
                    name: "John Snow",
                    pullMode: "manual",
                    pullRetryLimit: 3,
                    start: DateTime.Today.Date.AddDays(5),
                    end: DateTime.Today.Date.AddDays(35),
                    referenceCode: "contract-12345",
                    tags: new List<string>(),
                    taxID: "012.345.678-90",
                    type: type
                );
            }
            if (type == "qrcodeAndPayment")
            {
                example = new InvoicePullSubscription(
                    amount: 1000000,
                    amountMinLimit: 5000,
                    data: new Dictionary<string, object> {
                    {"amount", 400000}
                    },
                    displayDescription: "Dragon Travel Fare",
                    externalID: Guid.NewGuid().ToString(),
                    interval: "month",
                    name: "John Snow",
                    pullMode: "manual",
                    pullRetryLimit: 3,
                    start: DateTime.Today.Date.AddDays(5),
                    end: DateTime.Today.Date.AddDays(35),
                    referenceCode: "contract-12345",
                    tags: new List<string>(),
                    taxID: "012.345.678-90",
                    type: type
                );
            }
            if (type == "paymentAndOrQrcode")
            {
                example = new InvoicePullSubscription(
                    amount: 1000000,
                    amountMinLimit: 5000,
                    data: new Dictionary<string, object> {
                    {"amount", 400000},
                    {"due", "2026-06-26T17:59:26.000000+00:00"},
                    {"fine", 2.5},
                    {"interest", 1.3}
                    },
                    displayDescription: "Dragon Travel Fare",
                    externalID: Guid.NewGuid().ToString(),
                    interval: "month",
                    name: "John Snow",
                    pullMode: "manual",
                    pullRetryLimit: 3,
                    start: DateTime.Today.Date.AddDays(5),
                    end: DateTime.Today.Date.AddDays(35),
                    referenceCode: "contract-12345",
                    tags: new List<string>(),
                    taxID: "012.345.678-90",
                    type: type
                );
            }
            return example;
        }
    }
}
