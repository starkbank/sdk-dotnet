using Xunit;
using System;
using StarkBank;
using System.Linq;
using System.Collections.Generic;
using StarkCore;


namespace StarkBankTests
{
    public class CorporateCardLogTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void QueryAndGet()
        {
            List<CorporateCard.Log> logs = CorporateCard.Log.Query(
                limit: 2,
                types: new List<string> { "blocked" }
            ).ToList();
            Assert.Equal(2, logs.Count);
            Assert.True(logs.First().ID != logs.Last().ID);
            foreach (CorporateCard.Log log in logs)
            {
                TestUtils.Log(log);
                Assert.NotNull(log.ID);
                Assert.Equal("blocked", log.Type);
            }
            CorporateCard.Log getLog = CorporateCard.Log.Get(id: logs.First().ID);
            Assert.Equal(getLog.ID, logs.First().ID);
            TestUtils.Log(getLog);
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<CorporateCard.Log> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = CorporateCard.Log.Page(limit: 2, cursor: cursor);
                foreach (CorporateCard.Log entity in page)
                {
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
