using System;
using StarkBank;
using Xunit;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace StarkBankTests
{
	public class MerchantPurchaseTest
	{
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void ChallengeModeDisabledCreate()
        {
            MerchantSession sessionExample = MerchantSessionTest.Example();
            MerchantSession session = MerchantSession.Create(sessionExample);

            MerchantSession.Purchase purchaseJson = new MerchantSession.Purchase(
                    amount: 1000,
                    cardExpiration: "2035-01",
                    cardNumber: "36490101441625",
                    cardSecurityCode: "123",
                    holderName: "Margaery Tyrell",
                    fundingType: "credit"
                );

            MerchantSession.Purchase sessionPurchase = MerchantSession.PostPurchase(session.Uuid, purchaseJson);

            MerchantPurchase purchase = new MerchantPurchase(
                amount: 1000,
                cardId: sessionPurchase.CardId,
                challengeMode: "disabled",
                fundingType: "credit"
            );

            MerchantPurchase createdPurchase = MerchantPurchase.Create(purchase);

            Debug.WriteLine(createdPurchase);
        }

        [Fact]
        public void ChallengeModeEnabledCreate()
        {
            MerchantSession sessionExample = MerchantSessionTest.Example();
            MerchantSession session = MerchantSession.Create(sessionExample);

            MerchantSession.Purchase purchaseExample = new MerchantSession.Purchase(
                    amount: 1000,
                    cardExpiration: "2035-01",
                    cardNumber: "36490101441625",
                    cardSecurityCode: "123",
                    holderName: "Margaery Tyrell",
                    fundingType: "credit"
                ); 

            MerchantSession.Purchase sessionPurchase = MerchantSession.PostPurchase(session.Uuid, purchaseExample);

            MerchantPurchase purchase = Example(cardId: sessionPurchase.CardId);
            MerchantPurchase createdPurchase = MerchantPurchase.Create(purchase);

            Debug.WriteLine(createdPurchase);
        }

        [Fact]
        public void Get()
        {
            List<MerchantPurchase> purchases = MerchantPurchase.Query(limit: 1).ToList();
            string purchaseId = purchases.First().ID;
            MerchantPurchase purchase = MerchantPurchase.Get(purchaseId);
            Assert.Equal(purchase.ID, purchaseId);
        }

        [Fact]
        public void Query()
        {
            List<MerchantPurchase> purchases = MerchantPurchase.Query(limit: 2).ToList();
            Assert.Equal(2, purchases.Count);
            Assert.True(purchases.First().ID != purchases.Last().ID);
            foreach (MerchantPurchase purchase in purchases)
            {
                TestUtils.Log(purchase);
                Assert.NotNull(purchase.ID);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<MerchantPurchase> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = MerchantPurchase.Page(limit: 5, cursor: cursor);
                foreach (MerchantPurchase entity in page)
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
        public void Update()
        {
            MerchantSession sessionExample = MerchantSessionTest.Example();
            string sessionId = MerchantSession.Create(sessionExample).Uuid;

            MerchantSession.Purchase purchaseExample = new MerchantSession.Purchase(
                    amount: 1000,
                    installmentCount: 1,
                    cardExpiration: "2035-01",
                    cardNumber: "5102589999999954",
                    cardSecurityCode: "123",
                    holderName: "Holder Name",
                    holderEmail: "holdeName@email.com",
                    holderPhone: "11111111111",
                    fundingType: "credit",
                    billingCountryCode: "BRA",
                    billingCity: "São Paulo",
                    billingStateCode: "SP",
                    billingStreetLine1: "Rua do Holder Name, 123",
                    billingStreetLine2: "",
                    billingZipCode: "11111-111",
                    metadata: new Dictionary<string, object>
                    {
                        { "userAgent", "Postman" },
                        { "userIp", "255.255.255.255" },
                        { "language", "pt-BR" },
                        { "timezoneOffset", 3 },
                        { "extraData", "extraData" }
                    }
                );

            string purchaseId = MerchantSession.PostPurchase(id: sessionId, purchaseExample).ID;

            Debug.WriteLine(purchaseId);

            MerchantPurchase purchase = MerchantPurchase.Update(id: purchaseId, status: "canceled", amount: 0);

            Debug.WriteLine(purchase);
        }

        [Fact]
        public void ParseMerchantPurchaseEvent()
        {
            string content = "{\"event\": {\"created\": \"2025-10-14T20:46:00.300285+00:00\", \"id\": \"5454126009810944\", \"log\": {\"created\": \"2025-10-14T20:45:58.347434+00:00\", \"description\": \"Purchase approved.\", \"errors\": [], \"id\": \"5669171517980672\", \"purchase\": {\"amount\": 1000, \"billingCity\": \"\", \"billingCountryCode\": \"\", \"billingStateCode\": \"\", \"billingStreetLine1\": \"\", \"billingStreetLine2\": \"\", \"billingZipCode\": \"\", \"cardEnding\": \"1625\", \"cardId\": \"5113758527520768\", \"challengeMode\": \"disabled\", \"challengeUrl\": \"\", \"created\": \"2025-10-14T20:45:56.936238+00:00\", \"currencyCode\": \"BRL\", \"endToEndId\": \"f02b36d6-7872-4a81-b2ce-cd1a0b89da69\", \"fee\": 0, \"fundingType\": \"credit\", \"holderEmail\": \"\", \"holderName\": \"Margaery Tyrell\", \"holderPhone\": \"\", \"id\": \"5903823029665792\", \"installmentCount\": 1, \"metadata\": {}, \"network\": \"diners\", \"softDescriptor\": \"\", \"source\": \"merchant-session/5047053356892160\", \"status\": \"approved\", \"tags\": [\"yourtags\"], \"transactionIds\": [], \"updated\": \"2025-10-14T20:45:58.347478+00:00\"}, \"transactionId\": \"\", \"type\": \"approved\"}, \"subscription\": \"merchant-purchase\", \"workspaceId\": \"6341320293482496\"}}";
            string validSignature = "MEYCIQCQ7cDmcaRxVpEwTbmGCiTKE6RiWHgtXvVs3sSITgF4wwIhAInXEEeRKoi3UuOm4BexLAG05RQiSG5iSZJW3UZ3jBE3";
            Event parsedEvent = Event.Parse(content, validSignature);

            Assert.NotNull(parsedEvent.ID);
            Assert.Equal(typeof(MerchantPurchase.Log), parsedEvent.Log.GetType());
        }

        public static MerchantPurchase Example(string cardId)
        {
            return new MerchantPurchase(
                amount: 1000,
                cardId: cardId,
                installmentCount: 1,
                challengeMode: "enabled",
                holderEmail: "holdeName@email.com",
                holderPhone: "11111111111",
                fundingType: "credit",
                billingCountryCode: "BRA",
                billingCity: "São Paulo",
                billingStateCode: "SP",
                billingStreetLine1: "Rua do Holder Name, 123",
                billingStreetLine2: "",
                billingZipCode: "11111-111",
                metadata: new Dictionary<string, object>
                {
                    { "userAgent", "Postman" },
                    { "userIp", "255.255.255.255" },
                    { "language", "pt-BR" },
                    { "timezoneOffset", 3 },
                    { "extraData", "extraData" }
                }
            );
        }
    }
}

