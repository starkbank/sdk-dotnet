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
            Console.WriteLine(deleteWebhook);
        }

        [Fact]
        public void Query()
        {
            List<Webhook> webhooks = Webhook.Query().ToList();
            foreach (Webhook webhook in webhooks)
            {
                Console.WriteLine(webhook);
                Assert.NotNull(webhook.ID);
                Assert.NotNull(webhook.Url);
                Assert.NotNull(webhook.Subscriptions);
                Assert.NotEmpty(webhook.Subscriptions);
            }
        }

        private Webhook Example()
        {
            return new Webhook(
                url: "https://webhook.site/60e9c18e-4b5c-4369-bda1-ab5fcd8e1b29",
                subscriptions: new List<string> { "transfer", "boleto", "boleto-payment", "utility-payment" }
            );
        }
    }
}
