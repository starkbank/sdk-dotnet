using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class InvoiceTest
    {
        public readonly User user = TestUser.SetDefault();

        [Fact]
        public void CreateGet()
        {
            List<Invoice> invoices = Invoice.Create(new List<Invoice>() {Example()});
            Invoice invoice = invoices.First();
            TestUtils.Log(invoice);
            Assert.NotNull(invoices.First().ID);
            Invoice getInvoice = Invoice.Get(id: invoice.ID);
            Assert.Equal(getInvoice.ID, invoice.ID);
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
        public void UpdateStatus()
        {
            List<Invoice> invoices = Invoice.Query(limit: 2, status: "created").ToList();
            Assert.Equal(2, invoices.Count);
            Assert.True(invoices.First().ID != invoices.Last().ID);
            string expected = "created";
            string expectedStatus = "canceled";
            foreach (Invoice invoice in invoices)
            {
                TestUtils.Log(invoice);
                Assert.NotNull(invoice.ID);
                Assert.Equal(expected, invoice.Status);
                Invoice updatedInvoice = Invoice.Update(id: invoice.ID, status: expectedStatus);
                Assert.Equal(expectedStatus, updatedInvoice.Status);
            }
        }

        [Fact]
        public void UpdateAmount()
        {
            List<Invoice> invoices = Invoice.Query(limit: 2, status: "created").ToList();
            Assert.Equal(2, invoices.Count);
            Assert.True(invoices.First().ID != invoices.Last().ID);
            string expected = "created";
            long expectedAmount = 4321;
            foreach (Invoice invoice in invoices)
            {
                TestUtils.Log(invoice);
                Assert.NotNull(invoice.ID);
                Assert.Equal(expected, invoice.Status);
                Invoice updatedInvoice = Invoice.Update(id: invoice.ID, amount: expectedAmount);
                Assert.Equal(expectedAmount, updatedInvoice.Amount);
            }
        }


        [Fact]
        public void UpdateDue()
        {
            List<Invoice> invoices = Invoice.Query(limit: 101, status: "created").ToList();
            Assert.Equal(101, invoices.Count);
            Assert.True(invoices.First().ID != invoices.Last().ID);
            string expected = "created";
            string expectedDue = DateTime.Now.AddDays(10).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.ffffffzzz");
            foreach (Invoice invoice in invoices)
            {
                TestUtils.Log(invoice);
                Assert.NotNull(invoice.ID);
                Assert.Equal(expected, invoice.Status);
                Invoice updatedInvoice = Invoice.Update(id: invoice.ID, due: expectedDue);
                Assert.Equal(expectedDue, updatedInvoice.Due);
            }
        }

        [Fact]
        public void UpdateExpiration()
        {
            List<Invoice> invoices = Invoice.Query(limit: 2, status: "created").ToList();
            Assert.Equal(2, invoices.Count);
            Assert.True(invoices.First().ID != invoices.Last().ID);
            string expected = "created";
            long expectedExpiration = 123456789;
            foreach (Invoice invoice in invoices)
            {
                TestUtils.Log(invoice);
                Assert.NotNull(invoice.ID);
                Assert.Equal(expected, invoice.Status);
                Invoice updatedInvoice = Invoice.Update(id: invoice.ID, expiration: expectedExpiration);
                Assert.Equal(expectedExpiration, updatedInvoice.Expiration);
            }
        }

        internal static Invoice Example()
        {
            return new Invoice(
                amount: 1000000,
                due: DateTime.Now.AddDays(10).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.ffffffzzz"),
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
                        {"due", DateTime.Now.AddDays(1).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.ffffffzzz")}
                    },
                    new Dictionary<string, object> {
                        {"percentage", 3.5},
                        {"due", DateTime.Now.AddDays(2).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.ffffffzzz")}
                    }
                }
            );
        }
    }
}