using StarkBank;
using Xunit;
using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class CorporateCardTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Query()
        {
            List<CorporateCard> cards = CorporateCard.Query(limit: 4, status: new List<string> { "active" }, expand: new List<string> { "rules" } ).ToList();
            Assert.True(cards.Count <= 4);
            foreach (CorporateCard card in cards)
            {
                TestUtils.Log(card);
                Assert.NotNull(card.ID);
                Assert.Equal("active", card.Status);

                foreach(CorporateRule rule in card.Rules)
                {
                    TestUtils.Log(rule);
                }
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<CorporateCard> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = CorporateCard.Page(limit: 2, cursor: cursor);
                foreach (CorporateCard entity in page)
                {
                    TestUtils.Log(entity);
                    Assert.DoesNotContain(entity.ID, ids);
                    ids.Add(entity.ID);
                }
                if (cursor == null)
                {
                    break;
                }
            }
            Assert.Equal(4, ids.Count);
        }

        [Fact]
        public void Update()
        {
            List<CorporateCard> cards = CorporateCard.Query(limit: 2, status: new List<string> { "active" }).ToList();
            Assert.True(2 >= cards.Count);
            Dictionary<string, object> patchData = new Dictionary<string, object>
            {
                {"status", "blocked"}
            };
            foreach (CorporateCard card in cards)
            {
                TestUtils.Log(card);
                Assert.NotNull(card.ID);
                Assert.Equal("active", card.Status);
                CorporateCard updatedCard = CorporateCard.Update(id: card.ID, patchData: patchData);
                TestUtils.Log(updatedCard);
                Assert.Equal("blocked", updatedCard.Status);
            }
        }

        [Fact]
        public void CreateGetAndCancel()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"expand", new List<string> {"rules"}}
            };
            CorporateCard card = CorporateCard.Create(Example(), parameters: parameters);
            TestUtils.Log(card);
            Assert.NotNull(card.ID);
            CorporateCard getCard = CorporateCard.Get(card.ID, parameters: parameters);
            Assert.Equal(getCard.ID, card.ID);
            TestUtils.Log(getCard);
            CorporateCard canceledCard = CorporateCard.Cancel(id: card.ID);
            Assert.Equal(canceledCard.ID, card.ID);
            Assert.Equal("canceled", canceledCard.Status);
            TestUtils.Log(canceledCard);
        }

        [Fact]
        public void ParseCorporateCardEvent()
        {
            string content = "{\"event\": {\"created\": \"2025-10-14T19:23:03.316779+00:00\", \"id\": \"5655030621274112\", \"log\": {\"card\": {\"city\": \"Sao Paulo\", \"created\": \"2025-10-14T19:22:59.153498+00:00\", \"displayName\": \"\", \"displayUrl\": \"https://sandbox.api.starkbank.com/v2/corporate-icon/default.png\", \"district\": \"Jardim Paulista\", \"expiration\": \"****-**-**T**:**:**.******+00:00\", \"holderId\": \"5418684893888512\", \"holderName\": \"Iron Bank S.A.00000007864\", \"id\": \"5788921409568768\", \"number\": \"**** **** **** 7125\", \"rules\": [], \"securityCode\": \"***\", \"stateCode\": \"SP\", \"status\": \"active\", \"streetLine1\": \"Rua Pamplona, 145\", \"streetLine2\": \"\", \"tags\": [], \"type\": \"virtual\", \"updated\": \"2025-10-14T19:23:02.242959+00:00\", \"zipCode\": \"01405-900\"}, \"created\": \"2025-10-14T19:23:00.794443+00:00\", \"id\": \"5225971456147456\", \"type\": \"created\"}, \"subscription\": \"corporate-card\", \"workspaceId\": \"6341320293482496\"}}";
            string validSignature = "MEUCIQCpY2MR4ZdP7VAEEOaoIVg5RheA/8kNef8wIA62BEZvQgIgIqXbusyEnwY3CyB4AXoVa9T9Zhibrp2AscodmTviHnI=";
            Event parsedEvent = Event.Parse(content, validSignature);

            Assert.NotNull(parsedEvent.ID);
            Assert.Equal(typeof(CorporateCard.Log), parsedEvent.Log.GetType());
        }
        
        internal static CorporateCard Example()
        {
            List<CorporateHolder> holders = CorporateHolder.Create(new List<CorporateHolder>() { HolderExample() });
            CorporateHolder holder = holders.First();

            return new CorporateCard(
                holderID: holder.ID
            );
        }

        internal static CorporateHolder HolderExample()
        {
            Random random = new Random();
            return new CorporateHolder(
                name: "Iron Bank S.A." + random.Next(100, 100000).ToString("D11"),
                tags: new List<string> { "Traveler Employee" },
                permissions: new List<StarkBank.CorporateHolder.Permission> {
                    new StarkBank.CorporateHolder.Permission(
                        ownerId: Environment.GetEnvironmentVariable("SANDBOX_ID"),
                        ownerType: "project"
                    )
                }
            );
        }
    }
}
