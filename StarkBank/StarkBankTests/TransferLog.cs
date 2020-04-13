using Xunit;
using System;
using StarkBank;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class TransferLogTest
    {
        public readonly User user = TestUser.SetDefault();

        [Fact]
        public void QueryAndGet()
        {
            List<Transfer.Log> logs = Transfer.Log.Query(
                limit: 101,
                before: DateTime.Now,
                types: new List<string> { "success" }
            ).ToList();
            Assert.Equal(101, logs.Count);
            Assert.True(logs.First().ID != logs.Last().ID);
            foreach (Transfer.Log log in logs)
            {
                Assert.NotNull(log.ID);
                Assert.Equal("success", log.Type);
            }
            Transfer.Log getLog = Transfer.Log.Get(id: logs.First().ID);
            Assert.Equal(getLog.ID, logs.First().ID);
        }
    }
}
