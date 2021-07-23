using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;

namespace StarkBankTests
{
    public class BoletoHolmesLogTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void QueryAndGet()
        {
            List<BoletoHolmes.Log> logs = BoletoHolmes.Log.Query(
                limit: 101,
                types: new List<string> { "solved" }
            ).ToList();
            Assert.True(logs.Count <= 101);
            Assert.True(logs.First().ID != logs.Last().ID);
            foreach (BoletoHolmes.Log log in logs)
            {
                TestUtils.Log(log);
                Assert.NotNull(log.ID);
                Assert.Equal("solved", log.Type);
            }
            BoletoHolmes.Log getLog = BoletoHolmes.Log.Get(id: logs.First().ID);
            Assert.Equal(getLog.ID, logs.First().ID);
            TestUtils.Log(getLog);
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<BoletoHolmes.Log> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = BoletoHolmes.Log.Page(limit: 5, cursor: cursor);
                foreach (BoletoHolmes.Log entity in page)
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
