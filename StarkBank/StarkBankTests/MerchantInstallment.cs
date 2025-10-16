using System;
using StarkBank;
using Xunit;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;


namespace StarkBankTests
{
    public class MerchantInstallmentTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Get()
        {
            List<MerchantInstallment> installments = MerchantInstallment.Query(limit: 1).ToList();
            string merchantInstallmentId = installments.First().ID;
            MerchantInstallment installment = MerchantInstallment.Get(merchantInstallmentId);
            Assert.Equal(merchantInstallmentId, installment.ID);
        }

        [Fact]
        public void Query()
        {
            List<MerchantInstallment> installments = MerchantInstallment.Query(limit: 2).ToList();
            Assert.Equal(2, installments.Count);
            Assert.True(installments.First().ID != installments.Last().ID);
            foreach (MerchantInstallment installment in installments)
            {
                TestUtils.Log(installment);
                Assert.NotNull(installment.ID);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<MerchantInstallment> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = MerchantInstallment.Page(limit: 5, cursor: cursor);
                foreach (MerchantInstallment entity in page)
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
        public void ParseMerchantInstallmentEvent()
        {
            string content = "{\"event\": {\"created\": \"2025-10-14T20:45:55.357314+00:00\", \"id\": \"6007986671583232\", \"log\": {\"created\": \"2025-10-14T20:45:53.697574+00:00\", \"description\": \"Installment created with a nominal amount of R$ 10,00.\", \"errors\": [], \"id\": \"6655369694674944\", \"installment\": {\"amount\": 1000, \"created\": \"2025-10-14T20:45:53.610204+00:00\", \"due\": \"2025-11-17T03:00:00+00:00\", \"fee\": 24, \"fundingType\": \"credit\", \"id\": \"5529469787832320\", \"isProtected\": false, \"network\": \"diners\", \"nominalDue\": \"2025-11-17T03:00:00+00:00\", \"purchaseId\": \"5022074565296128\", \"status\": \"created\", \"tags\": [\"yourtags\"], \"transactionIds\": [], \"updated\": \"2025-10-14T20:45:53.697608+00:00\"}, \"type\": \"created\"}, \"subscription\": \"merchant-installment\", \"workspaceId\": \"6341320293482496\"}}";
            string validSignature = "MEUCIQD8azmNmlsG+baoqAh4xmX9G538cGrDhTT0VvU85rz8bwIgBEdIr6SSW/7vfxOv4ZET+LsHU0TpNpTrGmBxjs8Y5o0=";
            Event parsedEvent = Event.Parse(content, validSignature);

            Assert.NotNull(parsedEvent.ID);
            Assert.Equal(typeof(MerchantInstallment.Log), parsedEvent.Log.GetType());
        }
    }
}
