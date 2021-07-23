using Xunit;
using System;
using StarkBank;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class InvoiceLogTest
    {
        public readonly User user = TestUser.SetDefaultProject();

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

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<Invoice.Log> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = Invoice.Log.Page(limit: 5, cursor: cursor);
                foreach (Invoice.Log entity in page)
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

        [Fact]
        public void Pdf()
        {
            List<Invoice.Log> logs = Invoice.Log.Query(
                limit: 1,
                before: DateTime.Now.Date,
                types: new List<string> { "reversed" }
            ).ToList();
            foreach (Invoice.Log log in logs)
            {
                byte[] pdf = Invoice.Log.Pdf(id: log.ID);
                Assert.True(pdf.Length > 0);
                System.IO.File.WriteAllBytes("invoice-log.pdf", pdf);
            }
        }
    }
}
