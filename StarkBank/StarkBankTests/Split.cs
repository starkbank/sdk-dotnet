using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class SplitTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void QueryAndGet()
        {
            List<Split> splits = Split.Query(limit: 10).ToList();
            foreach (Split split in splits) {
                Assert.NotNull(split.ID);
                Split getSplit = Split.Get(split.ID);
                Assert.Equal(split.ID, getSplit.ID);
            }
            Assert.Equal(10, splits.Count);
        }

        [Fact]
        public void QueryWithParameters()
        {
            try
            {
                List<Split> splits = Split.Query(
                    limit: 10,
                    after: DateTime.Now.Date.AddDays(-30),
                    before: DateTime.Now.Date,
                    receiverIds: new List<string>() { "5656565656565656" },
                    tags: new List<string>() { "sdk-test" },
                    ids: new List<string>() { "5656565656565656", "4545454545454545" },
                    status: new List<string>() { "created", "success" }
                ).ToList();
            } catch (Exception e)            {
                throw new Exception("Test failed", e);
            }
        }

        [Fact]
        public void QueryWithParametersFail()
        {
            try
            {
                List<Split> splits = Split.Query(
                    status: new List<string>() { "invalidStatus" }
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
            List<Split> page;
            string cursor = null;
            for (int i = 0; i < 2; i++) {
                (page, cursor) = Split.Page(limit: 5, cursor: cursor);
                foreach (Split entity in page) {
                    Assert.DoesNotContain(entity.ID, ids);
                    ids.Add(entity.ID);
                }
                if (cursor == null) {
                    break;
                }
            }
            Assert.True(ids.Count == 10);
        }
    }
}