using Xunit;
using System;
using StarkBank;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class SplitProfileLogTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void QueryAndGet()
        {
            List<SplitProfile.Log> logs = SplitProfile.Log.Query(
                limit: 101,
                before: DateTime.Now.Date
            ).ToList();
            Assert.Equal(101, logs.Count);
            Assert.True(logs.First().ID != logs.Last().ID);
            foreach (SplitProfile.Log log in logs)
            {
                TestUtils.Log(log);
                Assert.NotNull(log.ID);
            }
            SplitProfile.Log getLog = SplitProfile.Log.Get(id: logs.First().ID);
            Assert.Equal(getLog.ID, logs.First().ID);
            TestUtils.Log(getLog);
        }

        [Fact]
        public void QueryWithParameters()
        {
            try
            {
                List<SplitProfile.Log> logs = SplitProfile.Log.Query(
                    limit: 101,
                    before: DateTime.Now.Date,
                    profileIds: new List<string>() { "5656565656565656", "4545454545454545" },
                    after: DateTime.Now.Date.AddDays(-30),
                    types: new List<string>() { "created", "updated" }
                ).ToList();
            } catch (Exception e)
            {
                throw new Exception("Test failed", e);
            }
        }

        [Fact]
        public void QueryWithParametersFail()
        {
            try
            {
                List<SplitProfile.Log> logs = SplitProfile.Log.Query(
                    types: new List<string>() { "invalidType" }
                ).ToList();
            } catch (Exception e)
            {
                Assert.IsType<StarkCore.Error.InputErrors>(e);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<SplitProfile.Log> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = SplitProfile.Log.Page(limit: 5, cursor: cursor);
                foreach (SplitProfile.Log entity in page)
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
