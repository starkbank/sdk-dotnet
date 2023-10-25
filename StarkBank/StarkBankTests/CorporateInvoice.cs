using System;
using StarkBank;
using Xunit;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using StarkCore;


namespace StarkBankTests
{
    public class CorporateInvoiceTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Query()
        {
            List<CorporateInvoice> invoices = CorporateInvoice.Query(limit: 3, status: "paid").ToList();
            Assert.True(invoices.Count <= 3);
            Assert.True(invoices.First().ID != invoices.Last().ID);
            foreach (CorporateInvoice invoice in invoices)
            {
                TestUtils.Log(invoice);
                Assert.NotNull(invoice.ID);
                Assert.Equal("paid", invoice.Status);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<CorporateInvoice> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = CorporateInvoice.Page(limit: 5, cursor: cursor);
                foreach (CorporateInvoice entity in page)
                {
                    TestUtils.Log(entity);
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

        internal static CorporateInvoice Example()
        {
            return new CorporateInvoice(
                amount: 10000
            );
        }
    }
}
