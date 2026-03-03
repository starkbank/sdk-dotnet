using Xunit;
using System;
using StarkBank;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class SplitReceiverLogTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void QueryAndGet()
        {
            List<SplitReceiver.Log> logs = SplitReceiver.Log.Query(
                limit: 101,
                before: DateTime.Now.Date
            ).ToList();
            Assert.Equal(101, logs.Count);
            Assert.True(logs.First().ID != logs.Last().ID);
            foreach (SplitReceiver.Log log in logs)
            {
                TestUtils.Log(log);
                Assert.NotNull(log.ID);
            }
            SplitReceiver.Log getLog = SplitReceiver.Log.Get(id: logs.First().ID);
            Assert.Equal(getLog.ID, logs.First().ID);
        }

        [Fact]
        public void QueryWithParameters()
        {
            try
            {
                List<SplitReceiver.Log> logs = SplitReceiver.Log.Query(
                    limit: 101,
                    before: DateTime.Now.Date,
                    receiverIds: new List<string>() { "5656565656565656", "4545454545454545" },
                    after: DateTime.Now.Date.AddDays(-30),
                    types: new List<string>() { "created", "updated"}
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
                List<SplitReceiver.Log> logs = SplitReceiver.Log.Query(
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
            List<SplitReceiver.Log> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = SplitReceiver.Log.Page(limit: 5, cursor: cursor);
                foreach (SplitReceiver.Log entity in page)
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
