using Xunit;
using System;
using StarkBank;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using StarkCore;


namespace StarkBankTests
{
    public class CorporateHolderLogTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void QueryAndGet()
        {
            List<CorporateHolder.Log> logs = CorporateHolder.Log.Query(
                limit: 4,
                types: new List<string> { "created" }
            ).ToList();
            Assert.Equal(4, logs.Count);
            Assert.True(logs.First().ID != logs.Last().ID);
            foreach (CorporateHolder.Log log in logs)
            {
                TestUtils.Log(log);
                Assert.NotNull(log.ID);
                Assert.Equal("created", log.Type);
            }
            CorporateHolder.Log getLog = CorporateHolder.Log.Get(id: logs.First().ID);
            Assert.Equal(getLog.ID, logs.First().ID);
            TestUtils.Log(getLog);
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<CorporateHolder.Log> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = CorporateHolder.Log.Page(limit: 2, cursor: cursor);
                foreach (CorporateHolder.Log entity in page)
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
