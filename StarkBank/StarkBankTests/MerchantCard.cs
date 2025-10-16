using System;
using StarkBank;
using Xunit;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;


namespace StarkBankTests
{
    public class MerchantCardTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Get()
        {
            List<MerchantCard> cards = MerchantCard.Query(limit: 1).ToList();
            string merchantCardId = cards.First().ID;
            MerchantCard card = MerchantCard.Get(merchantCardId);
            Assert.Equal(merchantCardId, card.ID);
        }

        [Fact]
        public void Query()
        {
            List<MerchantCard> cards = MerchantCard.Query(limit: 2).ToList();
            Assert.Equal(2, cards.Count);
            Assert.True(cards.First().ID != cards.Last().ID);
            foreach (MerchantCard card in cards)
            {
                TestUtils.Log(card);
                Assert.NotNull(card.ID);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<MerchantCard> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = MerchantCard.Page(limit: 5, cursor: cursor);
                foreach (MerchantCard entity in page)
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

        [Fact]
        public void ParseMerchantCardEvent()
        {
            string content = "{\"event\": {\"created\": \"2025-10-16T19:33:33.957137+00:00\", \"id\": \"4824539051589632\", \"log\": {\"card\": {\"created\": \"2025-10-16T19:33:33.667314+00:00\", \"ending\": \"9733\", \"expiration\": \"2035-02-01T02:59:59.999999+00:00\", \"fundingType\": \"debit\", \"holderName\": \"Kaladin Stormblessed\", \"id\": \"5596404638547968\", \"network\": \"mastercard\", \"status\": \"active\", \"tags\": [], \"updated\": \"2025-10-16T19:33:33.672774+00:00\"}, \"created\": \"2025-10-16T19:33:33.672780+00:00\", \"errors\": [], \"id\": \"6722304545390592\", \"type\": \"active\"}, \"subscription\": \"merchant-card\", \"workspaceId\": \"6314371953197056\"}}";
            string validSignature = "MEQCIFj9Vg+QkC+oXYXirS0j2ZoLFChRw7khSWrfpOud7/q7AiBPD7aPWYbpT6t3qSfyj2ol8b0cFQwtUHXu0iBkp4zGTQ==";
            Event parsedEvent = Event.Parse(content, validSignature);

            Assert.NotNull(parsedEvent.ID);
            Assert.Equal(typeof(MerchantCard.Log), parsedEvent.Log.GetType());
        }
    }
}
