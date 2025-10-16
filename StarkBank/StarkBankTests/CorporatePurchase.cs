using System;
using StarkBank;
using StarkCore;
using Xunit;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;


namespace StarkBankTests
{
    public class CorporatePurchaseTest
    {
        public readonly StarkBank.User user = TestUser.SetDefaultProject();

        [Fact]
        public void Get()
        {
            List<CorporatePurchase> purchases = CorporatePurchase.Query(limit: 1).ToList();
            CorporatePurchase purchase = purchases.First();
            CorporatePurchase getPurchase = CorporatePurchase.Get(purchase.ID);
            Assert.NotNull(getPurchase);
            TestUtils.Log(getPurchase);
        }

        [Fact]
        public void Query()
        {
            List<CorporatePurchase> purchases = CorporatePurchase.Query(limit: 3, status: "canceled").ToList();
            Assert.True(purchases.Count <= 3);
            foreach (CorporatePurchase purchase in purchases)
            {
                TestUtils.Log(purchase);
                Assert.NotNull(purchase.ID);
                Assert.Equal("canceled", purchase.Status);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<CorporatePurchase> page;
            string cursor = null;
            for (int i = 0; i < 3; i++)
            {
                (page, cursor) = CorporatePurchase.Page(limit: 1, cursor: cursor);
                foreach (CorporatePurchase entity in page)
                {
                    TestUtils.Log(entity);
                    ids.Add(entity.ID);
                }
                if (cursor == null)
                {
                    break;
                }
            }
            Assert.True(ids.Count == 3);
        }

        public readonly string Content = "{\"acquirerId\": \"236090\", \"amount\": 100, \"cardId\": \"5671893688385536\", \"cardTags\": [], \"endToEndId\": \"2fa7ef9f-b889-4bae-ac02-16749c04a3b6\", \"holderId\": \"5917814565109760\", \"holderTags\": [], \"isPartialAllowed\": false, \"issuerAmount\": 100, \"issuerCurrencyCode\": \"BRL\", \"merchantAmount\": 100, \"merchantCategoryCode\": \"bookStores\", \"merchantCountryCode\": \"BRA\", \"merchantCurrencyCode\": \"BRL\", \"merchantFee\": 0, \"merchantId\": \"204933612653639\", \"merchantName\": \"COMPANY 123\", \"methodCode\": \"token\", \"purpose\": \"purchase\", \"score\": null, \"tax\": 0, \"walletId\": \"\"}";
        public readonly string GoodSignature = "MEUCIBxymWEpit50lDqFKFHYOgyyqvE5kiHERi0ZM6cJpcvmAiEA2wwIkxcsuexh9BjcyAbZxprpRUyjcZJ2vBAjdd7o28Q=";
        public readonly string BadSignature = "MEUCIQDOpo1j+V40DNZK2URL2786UQK/8mDXon9ayEd8U0/l7AIgYXtIZJBTs8zCRR3vmted6Ehz/qfw1GRut/eYyvf1yOk=";

        [Fact]
        public void ParseWithRightSignature()
        {
            CorporatePurchase parsedCorporatePurchase = CorporatePurchase.Parse(Content, GoodSignature);
            TestUtils.Log(parsedCorporatePurchase);
        }

        [Fact]
        public void ParseWithWrongSignature()
        {
            try {
                CorporatePurchase parsedCorporatePurchase = CorporatePurchase.Parse(Content, BadSignature);
            } catch (StarkCore.Error.InvalidSignatureError e) {
                TestUtils.Log(e);
                return;
            }
            throw new Exception("failed to raise InvalidSignatureError");
        }

        [Fact]
        public void ParseWithMalformedSignature()
        {
            try
            {
                CorporatePurchase parsedCorporatePurchase = CorporatePurchase.Parse(Content, "Something is definitely wrong");
            }
            catch (StarkCore.Error.InvalidSignatureError e)
            {
                TestUtils.Log(e);
                return;
            }
            throw new Exception("failed to raise InvalidSignatureError");
        }

        [Fact]
        public void SendResponse()
        {
            string response = CorporatePurchase.Response(status: "accepted");
            TestUtils.Log(response);
        }

        [Fact]
        public void ParseCorporatePurchaseEvent()
        {
            string content = "{\"event\": {\"created\": \"2025-10-15T15:00:23.409774+00:00\", \"id\": \"5894409012903936\", \"log\": {\"corporateTransactionId\": \"\", \"created\": \"2025-10-15T15:00:21.705391+00:00\", \"description\": \"Purchase denied: This card was already linked to COMIDA NICE. Create a new virtual card to complete your purchase or keep on this card temporarily.\", \"errors\": [{\"code\": \"invalidMerchant\", \"message\": \"This card was already linked to COMIDA NICE. Create a new virtual card to complete your purchase or keep on this card temporarily.\"}], \"id\": \"4820678880526336\", \"installment\": null, \"purchase\": {\"amount\": 1000000, \"attachments\": [], \"cardEnding\": null, \"cardId\": \"5740780031311872\", \"centerId\": \"4830905268961280\", \"corporateTransactionIds\": [], \"created\": \"2025-10-15T15:00:21.684087+00:00\", \"description\": null, \"holderId\": \"6026968331976704\", \"holderName\": null, \"id\": \"5662785937604608\", \"installmentCount\": 1, \"issuerAmount\": null, \"issuerCurrencyCode\": null, \"issuerCurrencySymbol\": null, \"merchantAmount\": null, \"merchantCategoryCode\": null, \"merchantCategoryNumber\": null, \"merchantCategoryType\": null, \"merchantCountryCode\": null, \"merchantCurrencyCode\": null, \"merchantCurrencySymbol\": null, \"merchantDisplayName\": \"China in Box\", \"merchantDisplayUrl\": \"https://sandbox.api.starkbank.com/v2/corporate-icon/type/food.png\", \"merchantFee\": null, \"merchantName\": \"China in Box\", \"methodCode\": null, \"status\": \"denied\", \"tags\": [], \"tax\": null, \"updated\": \"2025-10-15T15:00:21.705427+00:00\"}, \"type\": \"denied\"}, \"subscription\": \"corporate-purchase\", \"workspaceId\": \"6341320293482496\"}}";
            string validSignature = "MEYCIQCaqA7LOTVIfqYod1pdm6Fxgfw8n6db9+2pqRHOhF8FcgIhAI8Up+yubHFDx3sCwTEioEflnHyiAI/v4Vlrh4ycN8jd";
            Event parsedEvent = Event.Parse(content, validSignature);

            Assert.NotNull(parsedEvent.ID);
            Assert.Equal(typeof(CorporatePurchase.Log), parsedEvent.Log.GetType());
        }
    }
}
