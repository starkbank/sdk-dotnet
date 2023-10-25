using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;
using StarkCore;


namespace StarkBankTests
{
    public class WebhookTest
    {
        public readonly User user = TestUser.SetDefaultProject();

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

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<Webhook> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = Webhook.Page(limit: 2, cursor: cursor);
                foreach (Webhook entity in page)
                {
                    Assert.DoesNotContain(entity.ID, ids);
                    ids.Add(entity.ID);
                }
                if (cursor == null)
                {
                    break;
                }
            }
            Assert.True(ids.Count <= 4);
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
