using Xunit;
using System;
using StarkBank;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class InvoiceLogTest
    {
        public readonly User user = TestUser.SetDefault();

        [Fact]
        public void QueryAndGet()
        {
            List<Invoice.Log> logs = Invoice.Log.Query(
                limit: 101,
                before: DateTime.Now.Date,
                types: new List<string> { "paid" }
            ).ToList();
            Assert.Equal(101, logs.Count);
            Assert.True(logs.First().ID != logs.Last().ID);
            foreach (Invoice.Log log in logs)
            {
                TestUtils.Log(log);
                Assert.NotNull(log.ID);
                Assert.Equal("paid", log.Type);
            }
            Invoice.Log getLog = Invoice.Log.Get(id: logs.First().ID);
            Assert.Equal(getLog.ID, logs.First().ID);
            TestUtils.Log(getLog);
        }
    }
}
