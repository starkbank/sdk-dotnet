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
    }
}
