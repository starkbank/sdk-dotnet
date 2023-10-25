using System;
using StarkBank;
using Xunit;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using StarkCore;


namespace StarkBankTests
{
    public class CorporateTransactionTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Get()
        {
            List<CorporateTransaction> transactions = CorporateTransaction.Query(limit: 1).ToList();
            CorporateTransaction transaction = transactions.First();
            CorporateTransaction getTransaction = CorporateTransaction.Get(transaction.ID);
            Assert.NotNull(getTransaction);
            TestUtils.Log(getTransaction);
        }

        [Fact]
        public void Query()
        {
            List<CorporateTransaction> transactions = CorporateTransaction.Query(limit: 2).ToList();
            Assert.True(transactions.Count <= 2);
            Assert.True(transactions.First().ID != transactions.Last().ID);
            foreach (CorporateTransaction transaction in transactions)
            {
                TestUtils.Log(transaction);
                Assert.NotNull(transaction.ID);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<CorporateTransaction> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = CorporateTransaction.Page(limit: 2, cursor: cursor);
                foreach (CorporateTransaction entity in page)
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
    }
}
