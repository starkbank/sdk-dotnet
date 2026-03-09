using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class SplitReceiverTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void CreateAndGet()
        {
            List<SplitReceiver> receivers = SplitReceiver.Create(new List<SplitReceiver>() { Example(), Example(), Example() });
            Assert.Equal(3, receivers.Count);
            foreach (SplitReceiver receiver in receivers)
            {
                Assert.NotNull(receiver.ID);
            }
        }

        [Fact]
        public void QueryAndGet()
        {
            List<SplitReceiver> receivers = SplitReceiver.Query(limit: 101).ToList();
            Assert.Equal(101, receivers.Count);
            Assert.True(receivers.First().ID != receivers.Last().ID);
            foreach (SplitReceiver receiver in receivers)
            {
                Assert.NotNull(receiver.ID);
            }
            SplitReceiver getReceiver = SplitReceiver.Get(id: receivers.First().ID);
            Assert.Equal(getReceiver.ID, receivers.First().ID);
        }

        [Fact]
        public void QueryWithParameters()
        {
            try
            {
                List<SplitReceiver> receivers = SplitReceiver.Query(
                    limit: 101,
                    taxIds: new List<string>() { "012.345.678-90" },
                    before: DateTime.Now.Date,
                    after: DateTime.Now.Date.AddDays(-30),
                    tags: new List<string>() { "sdk-test" },
                    ids: new List<string>() { "5656565656565656", "4545454545454545" }
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
                List<SplitReceiver> receivers = SplitReceiver.Query(    
                    taxIds: new List<string>() { "invalidTaxId" }
                ).ToList();
            } catch (Exception e) {
                Assert.IsType<StarkCore.Error.InputErrors>(e);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<SplitReceiver> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = SplitReceiver.Page(limit: 5, cursor: cursor);
                foreach (SplitReceiver entity in page)
                {
                    Assert.DoesNotContain(entity.ID, ids);
                    ids.Add(entity.ID);
                }
            }
            Assert.True(ids.Count == 10);
        }

        internal static SplitReceiver Example()
        {
            return new SplitReceiver(
                name: "John Doe",
                taxID: "012.345.678-90",
                bankCode: "341",
                branchCode: "1234",
                accountNumber: "12345-2",
                accountType: "checking",
                tags: new List<string> { "sdk-test" }
            );
        }
    }
}