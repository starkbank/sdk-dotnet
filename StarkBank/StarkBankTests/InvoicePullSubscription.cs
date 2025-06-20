using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;


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
            Console.WriteLine($"Created (type = \"{type}\")");
        }

        [Fact]
        public void CreateQrCode()
        {
            string type = "qrcode";
            List<InvoicePullSubscription> subscriptions = InvoicePullSubscription.Create(new List<InvoicePullSubscription> { Example("qrcode") }, user);
            InvoicePullSubscription subscription = subscriptions.First();
            Assert.NotNull(subscription.ID);
            Assert.NotNull(subscription.Name);
            Assert.NotNull(subscription.TaxID);
            Console.WriteLine($"Created (type = \"{type}\")");
        }

        [Fact]
        public void CreateQrCodeAndPayment()
        {
            string type = "qrcodeAndPayment";
            List<InvoicePullSubscription> subscriptions = InvoicePullSubscription.Create(new List<InvoicePullSubscription> { Example("qrcodeAndPayment") }, user);
            InvoicePullSubscription subscription = subscriptions.First();
            Assert.NotNull(subscription.ID);
            Assert.NotNull(subscription.Name);
            Assert.NotNull(subscription.TaxID);
            Console.WriteLine($"Created (type = \"{type}\")");
        }

        [Fact]
        public void CreatePaymentAndOrQrCode()
        {
            string type = "paymentAndOrQrcode";
            List<InvoicePullSubscription> subscriptions = InvoicePullSubscription.Create(new List<InvoicePullSubscription> { Example("paymentAndOrQrcode") }, user);
            InvoicePullSubscription subscription = subscriptions.First();
            Assert.NotNull(subscription.ID);
            Assert.NotNull(subscription.Name);
            Assert.NotNull(subscription.TaxID);
            Console.WriteLine($"Created (type = \"{type}\")");
        }

        [Fact]
        public void Query()
        {
            List<InvoicePullSubscription> subscriptions = InvoicePullSubscription.Query(limit: 5).ToList();
            Console.WriteLine("List of IDs:");
            foreach (InvoicePullSubscription subscription in subscriptions)
            {
                Assert.NotNull(subscription.ID);
                Assert.NotNull(subscription.Name);
                Assert.NotNull(subscription.TaxID);
                Console.WriteLine($"- " + subscription.ID);
            }
        }

        [Fact]
        public void QueryAndGet()
        {
            List<InvoicePullSubscription> subscriptions = InvoicePullSubscription.Query(limit: 1).ToList();
            InvoicePullSubscription subscription = InvoicePullSubscription.Get(subscriptions.First().ID, user);
            Assert.Equal(subscription.ID, subscriptions.First().ID);
            Assert.Equal(subscription.Name, subscriptions.First().Name);
            Assert.Equal(subscription.TaxID, subscriptions.First().TaxID);
            Console.WriteLine("ID: " + subscription.ID);
        }

        internal static InvoicePullSubscription Example(string type)
        {
            InvoicePullSubscription example = null;
            if (type == "push")
            {
                example = new InvoicePullSubscription(
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
            if (type == "qrcode")
            {
                example = new InvoicePullSubscription(
                    amount: 0,
                    amountMinLimit: 5000,
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
            if (type == "qrcodeAndPayment")
            {
                example = new InvoicePullSubscription(
                    amount: 0,
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
                    start: DateTime.Today.Date.AddDays(1),
                    end: DateTime.Today.Date.AddDays(31),
                    referenceCode: "contract-12345",
                    tags: new List<string>(),
                    taxID: "012.345.678-90",
                    type: type
                );
            }
            if (type == "paymentAndOrQrcode")
            {
                example = new InvoicePullSubscription(
                    amount: 0,
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
                    start: DateTime.Today.Date.AddDays(1),
                    end: DateTime.Today.Date.AddDays(31),
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