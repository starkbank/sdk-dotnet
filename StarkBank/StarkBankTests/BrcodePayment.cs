using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class BrcodePaymentTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void CreateAndGet()
        {
            List<BrcodePayment> payments = BrcodePayment.Create(new List<BrcodePayment>() { Example() });
            BrcodePayment payment = payments.First();
            Assert.NotNull(payment.ID);
            BrcodePayment getBrcodePayment = BrcodePayment.Get(id: payment.ID);
            Assert.Equal(getBrcodePayment.ID, payment.ID);
            TestUtils.Log(payment);
        }

        [Fact]
        public void CreateAndCancel()
        {
            BrcodePayment payment = BrcodePayment.Query(status: "created", limit: 1).ToList().First();
            TestUtils.Log(payment);
            BrcodePayment cancelPayment = BrcodePayment.Update(id: payment.ID, status: "canceled");
            Assert.Equal(cancelPayment.ID, payment.ID);
            TestUtils.Log(payment);
        }

        [Fact]
        public void Query()
        {
            List<BrcodePayment> payments = BrcodePayment.Query(limit: 101, status: "failed").ToList();
            Assert.True(payments.Count <= 101);
            Assert.True(payments.First().ID != payments.Last().ID);
            foreach (BrcodePayment payment in payments)
            {
                TestUtils.Log(payment);
                Assert.NotNull(payment.ID);
                Assert.Equal("failed", payment.Status);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<BrcodePayment> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = BrcodePayment.Page(limit: 5, cursor: cursor);
                foreach (BrcodePayment entity in page)
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

        internal static BrcodePayment Example(bool schedule = true)
        {
            DateTime? scheduled = null;
            if (schedule) {
                scheduled = DateTime.Today.Date.AddDays(1);
            }
            Invoice invoice= InvoiceTest.Example();
            invoice = Invoice.Create(new List<Invoice>() { invoice }).First();
            return new BrcodePayment(
                brcode: invoice.Brcode,
                scheduled: scheduled,
                description: "loading a random account",
                taxID: "20.018.183/0001-80"
            );
        }
    }
}
