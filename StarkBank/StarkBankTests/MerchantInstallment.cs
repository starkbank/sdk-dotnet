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
    }
}
