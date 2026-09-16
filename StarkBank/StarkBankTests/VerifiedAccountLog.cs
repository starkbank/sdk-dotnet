using Xunit;
using System;
using StarkBank;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class VerifiedAccountLogTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void QueryAndGet()
        {
            // The API caps VerifiedAccount creation at three per tax id per 24h, and the
            // fixtures use a fixed tax id, so reuse an account that already exists.
            VerifiedAccount account = VerifiedAccount.Query(limit: 1).FirstOrDefault()
                ?? VerifiedAccount.Create(new List<VerifiedAccount>() { VerifiedAccountTest.Example() }).First();
            string accountId = account.ID;

            List<VerifiedAccount.Log> logs = VerifiedAccount.Log.Query(limit: 5, accountIds: new List<string> { accountId }).ToList();
            Assert.NotEmpty(logs);
            foreach (VerifiedAccount.Log log in logs)
            {
                TestUtils.Log(log);
                Assert.NotNull(log.ID);
                Assert.Equal(accountId, log.VerifiedAccount.ID);
            }

            VerifiedAccount.Log getLog = VerifiedAccount.Log.Get(id: logs.First().ID);
            Assert.Equal(getLog.ID, logs.First().ID);
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<VerifiedAccount.Log> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = VerifiedAccount.Log.Page(limit: 5, cursor: cursor);
                foreach (VerifiedAccount.Log entity in page)
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
