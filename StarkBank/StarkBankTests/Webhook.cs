using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class WebhookTest
    {
        public readonly User user = TestUser.SetDefault();

        [Fact]
        public void CreateGetAndDelete()
        {
            Webhook webhook = Example();
            webhook = Webhook.Create(url: webhook.Url, subscriptions: webhook.Subscriptions);
            Assert.NotNull(webhook.ID);
            Webhook getWebhook = Webhook.Get(id: webhook.ID);
            Assert.Equal(getWebhook.ID, webhook.ID);
            Webhook deleteWebhook = Webhook.Delete(id: webhook.ID);
            Assert.Equal(deleteWebhook.ID, webhook.ID);
            TestUtils.Log(deleteWebhook);
        }

        [Fact]
        public void Query()
        {
            List<Webhook> webhooks = Webhook.Query().ToList();
            foreach (Webhook webhook in webhooks)
            {
                TestUtils.Log(webhook);
                Assert.NotNull(webhook.ID);
                Assert.NotNull(webhook.Url);
                Assert.NotNull(webhook.Subscriptions);
                Assert.NotEmpty(webhook.Subscriptions);
            }
        }

        internal static Webhook Example()
        {
            return new Webhook(
                url: "https://webhook.site/" + Guid.NewGuid(),
                subscriptions: new List<string> { "transfer", "boleto", "boleto-payment", "utility-payment", "boleto-holmes" }
            );
        }
    }
}
