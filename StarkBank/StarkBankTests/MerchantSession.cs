using System;
using StarkBank;
using Xunit;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;


namespace StarkBankTests
{
    public class MerchantSessionTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Create()
        {
            MerchantSession sessionExample = Example();
            MerchantSession session = MerchantSession.Create(sessionExample);
            Assert.NotNull(session.ID);
        }

        [Fact]
        public void Get()
        {
            List<MerchantSession> sessions = MerchantSession.Query(limit: 1).ToList();
            string merchantSessionId = sessions.First().ID;
            MerchantSession session = MerchantSession.Get(merchantSessionId);
            Assert.Equal(merchantSessionId, session.ID);
        }

        [Fact]
        public void Query()
        {
            List<MerchantSession> sessions = MerchantSession.Query(limit: 2).ToList();
            Assert.Equal(2, sessions.Count);
            Assert.True(sessions.First().ID != sessions.Last().ID);
            foreach (MerchantSession session in sessions)
            {
                TestUtils.Log(session);
                Assert.NotNull(session.ID);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<MerchantSession> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = MerchantSession.Page(limit: 5, cursor: cursor);
                foreach (MerchantSession entity in page)
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
        public void Purchase()
        {

            MerchantSession sessionExample = Example();
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

            MerchantSession.Purchase purchase = MerchantSession.PostPurchase(id: sessionId, purchaseExample);
            Assert.NotNull(purchase);
        }

        internal static MerchantSession Example()
        {
            MerchantSession.AllowedInstallment installement = new MerchantSession.AllowedInstallment(
                totalAmount: 1000,
                count: 1
            );
            return new MerchantSession(
                allowedFundingTypes: new List<string> { "credit" },
                allowedInstallments: new List<MerchantSession.AllowedInstallment> { installement },
                challengeMode: "disabled",
                expiration: 3600,
                tags: new List<string> { "yourTags" }
            );
        }

    }
}