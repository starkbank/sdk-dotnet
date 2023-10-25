using System;
using System.Collections.Generic;
using System.Linq;
using StarkBank;
using Xunit;
using StarkCore;

namespace StarkBankTests
{
    public class DarfPaymentTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void CreateGetGetPdfAndDelete()
        {
            List<DarfPayment> payments = DarfPayment.Create(new List<DarfPayment>() { Example() });
            DarfPayment payment = payments.First();
            Assert.NotNull(payments.First().ID);
            DarfPayment getDarfPayment = DarfPayment.Get(id: payment.ID);
            Assert.Equal(getDarfPayment.ID, payment.ID);
            byte[] pdf = DarfPayment.Pdf(id: payment.ID);
            Assert.True(pdf.Length > 0);
            System.IO.File.WriteAllBytes("DarfPayment.pdf", pdf);
            DarfPayment deleteDarfPayment = DarfPayment.Delete(id: payment.ID);
            Assert.Equal(deleteDarfPayment.ID, payment.ID);
            TestUtils.Log(payment);
        }

        [Fact]
        public void Query()
        {
            List<DarfPayment> payments = DarfPayment.Query(limit: 5).ToList();
            Assert.True(payments.Count <= 5);
            Assert.True(payments.First().ID != payments.Last().ID);
            foreach (DarfPayment payment in payments)
            {
                TestUtils.Log(payment);
                Assert.NotNull(payment.ID);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<DarfPayment> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = DarfPayment.Page(limit: 5, cursor: cursor);
                foreach (DarfPayment entity in page)
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

        internal static DarfPayment Example(bool schedule = true)
        {
            DateTime? scheduled = null;
            if (schedule)
            {
                scheduled = DateTime.Today.Date.AddDays(1);
            }
            return new DarfPayment(
                revenueCode: "1240",
                taxID: "012.345.678-90",
                competence: DateTime.Now.AddDays(-5).Date,
                referenceNumber: "2340978970",
                nominalAmount: 1234,
                fineAmount: 12,
                interestAmount: 34,
                due: DateTime.Now.AddDays(10).Date,
                scheduled: scheduled,
                tags: new List<string> { "test1", "test2" },
                description: "Darf Payment test"
            );
        }
    }
}
