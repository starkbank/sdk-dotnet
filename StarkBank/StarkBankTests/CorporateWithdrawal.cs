using System;
using StarkBank;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using StarkCore;


namespace StarkBankTests
{
    public class CorporateWithdrawalTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void CreateGet()
        {
            CorporateWithdrawal withdrawal = CorporateWithdrawal.Create(Example());
            Assert.NotNull(withdrawal.ID);
            CorporateWithdrawal getCorporateWithdrawal = CorporateWithdrawal.Get(id: withdrawal.ID);
            Assert.Equal(getCorporateWithdrawal.ID, withdrawal.ID);
            TestUtils.Log(withdrawal);
        }

        [Fact]
        public void Query()
        {
            List<CorporateWithdrawal> withdrawals = CorporateWithdrawal.Query(limit: 3).ToList();
            Assert.True(withdrawals.Count <= 3);
            Assert.True(withdrawals.First().ID != withdrawals.Last().ID);
            foreach (CorporateWithdrawal withdrawal in withdrawals)
            {
                TestUtils.Log(withdrawal);
                Assert.NotNull(withdrawal.ID);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<CorporateWithdrawal> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = CorporateWithdrawal.Page(limit: 2, cursor: cursor);
                foreach (CorporateWithdrawal entity in page)
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
            Assert.True(ids.Count == 4);
        }

        internal static CorporateWithdrawal Example()
        {
            return new CorporateWithdrawal(
                amount: 10000,
                externalID : Guid.NewGuid().ToString()
            );
        }
    }
}
