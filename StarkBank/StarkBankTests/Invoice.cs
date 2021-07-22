using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class InvoiceTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void CreateAndGetPdf()
        {
            List<Invoice> invoices = Invoice.Create(new List<Invoice>() { Example(), Example(), Example() });
            Invoice invoice = invoices.First();
            TestUtils.Log(invoice);
            Assert.NotNull(invoices.First().ID);
            Invoice getInvoice = Invoice.Get(id: invoice.ID);
            Assert.Equal(getInvoice.ID, invoice.ID);
            byte[] png = Invoice.Qrcode(id: invoice.ID, size: 30);
            Assert.True(png.Length > 0);
            System.IO.File.WriteAllBytes("qrcode.png", png);
        }

        [Fact]
        public void CreateAndCancel()
        {
            List<Invoice> invoices = Invoice.Create(new List<Invoice>() { Example() });
            Invoice invoice = invoices.First();
            TestUtils.Log(invoice);
            Invoice cancelInvoice = Invoice.Update(id: invoice.ID, status: "canceled");
            Assert.Equal(cancelInvoice.ID, invoice.ID);
            TestUtils.Log(invoice);
        }

        [Fact]
        public void Query()
        {
            List<Invoice> invoices = Invoice.Query(limit: 2, status: "created").ToList();
            Assert.Equal(2, invoices.Count);
            Assert.True(invoices.First().ID != invoices.Last().ID);
            string expected = "created";
            foreach (Invoice invoice in invoices)
            {
                TestUtils.Log(invoice);
                Assert.NotNull(invoice.ID);
                Assert.Equal(expected, invoice.Status);
            }
        }

        [Fact]
        public void Reverse()
        {
            List<Invoice> invoices = Invoice.Query(limit: 1, status: "paid").ToList();
            foreach (Invoice invoice in invoices)
            {
                Invoice updatedInvoice = Invoice.Update(id: invoice.ID, amount: 0);
                Assert.Equal(0, updatedInvoice.Amount);
                TestUtils.Log(updatedInvoice);
            }
        }

        [Fact]
        public void Update()
        {
            List<Invoice> invoices = Invoice.Query(limit: 2, status: "created").ToList();
            Assert.Equal(2, invoices.Count);
            Assert.True(invoices.First().ID != invoices.Last().ID);
            string expected = "created";
            long expectedExpiration = 123456789;
            long expectedAmount = 4321;
            DateTime expectedDue = DateTime.Now.AddDays(10);
            foreach (Invoice invoice in invoices)
            {
                TestUtils.Log(invoice);
                Assert.NotNull(invoice.ID);
                Assert.Equal(expected, invoice.Status);
                Invoice updatedInvoice = Invoice.Update(id: invoice.ID, due: expectedDue, expiration: expectedExpiration, amount: expectedAmount);
                Assert.Equal(expectedDue.Day, ((DateTime)updatedInvoice.Due).Day);
                Assert.Equal(expectedExpiration, updatedInvoice.Expiration);
                Assert.Equal(expectedAmount, updatedInvoice.Amount);
            }
        }

        [Fact]
        public void Payment()
        {
            List<Invoice> invoices = Invoice.Query(limit: 2, status: "paid").ToList();
            Assert.Equal(2, invoices.Count);
            foreach (Invoice invoice in invoices)
            {
                InvoicePayment payment = Invoice.Payment(invoice.ID);
                Assert.NotNull(payment.Name);
            }
        }

        internal static Invoice Example()
        {
            return new Invoice(
                amount: 1000000,
                due: DateTime.Now.AddDays(10),
                name: "Random Company",
                taxID: "012.345.678-90",
                fine: 0.00,
                interest: 0.00,
                tags: new List<string> { "custom", "tags" },
                descriptions: new List<Dictionary<string, object>>() {
                    new Dictionary<string, object> {
                        {"key", "product A"},
                        {"value", "small"}
                    },
                    new Dictionary<string, object> {
                        {"key", "product B"},
                        {"value", "medium"}
                    }
                },
                discounts: new List<Dictionary<string, object>>() {
                    new Dictionary<string, object> {
                        {"percentage", 5},
                        {"due", DateTime.Now.AddDays(1)}
                    },
                    new Dictionary<string, object> {
                        {"percentage", 3.5},
                        {"due", DateTime.Now.AddDays(2)}
                    }
                }
            );
        }
    }
}