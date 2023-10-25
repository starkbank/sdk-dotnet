using System;
using System.Collections.Generic;
using System.Linq;
using StarkBank;
using Xunit;
using StarkCore;

namespace StarkBankTests
{
    public class TaxPaymentTest
    {

        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void CreateGetGetPdfAndDelete()
        {
            List<TaxPayment> payments = TaxPayment.Create(new List<TaxPayment>() { Example() });
            TaxPayment payment = payments.First();
            Assert.NotNull(payments.First().ID);
            TaxPayment getTaxPayment = TaxPayment.Get(id: payment.ID);
            Assert.Equal(getTaxPayment.ID, payment.ID);
            byte[] pdf = TaxPayment.Pdf(id: payment.ID);
            Assert.True(pdf.Length > 0);
            System.IO.File.WriteAllBytes("taxPayment.pdf", pdf);
            TaxPayment deleteTaxPayment = TaxPayment.Delete(id: payment.ID);
            Assert.Equal(deleteTaxPayment.ID, payment.ID);
            TestUtils.Log(payment);
        }

        [Fact]
        public void Query()
        {
            List<TaxPayment> payments = TaxPayment.Query(limit: 5).ToList();
            Assert.True(payments.Count <= 5);
            Assert.True(payments.First().ID != payments.Last().ID);
            foreach (TaxPayment payment in payments)
            {
                TestUtils.Log(payment);
                Assert.NotNull(payment.ID);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<TaxPayment> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = TaxPayment.Page(limit: 5, cursor: cursor);
                foreach (TaxPayment entity in page)
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

        internal static TaxPayment Example(bool schedule = true)
        {
            DateTime? scheduled = null;
            if (schedule)
            {
                scheduled = DateTime.Today.Date.AddDays(1);
            }
            return new TaxPayment(
                barCode: "8566000" + new Random().Next(1, 100000000).ToString().PadLeft(8, '0') + "00640074119002551100010601813",
                description: "loading a random account",
                tags: new List<string> { "test1", "test2", "test3" },
                scheduled: scheduled
            );
        }
    }
}
