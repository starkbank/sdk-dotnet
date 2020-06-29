using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class DasPaymentTest
    {
        public readonly User user = TestUser.SetDefault();

        [Fact]
        public void CreateGetGetPdfAndDelete()
        {
            List<DasPayment> payments = DasPayment.Create(new List<DasPayment>() { Example() });
            DasPayment payment = payments.First();
            Assert.NotNull(payments.First().ID);
            DasPayment getDasPayment = DasPayment.Get(id: payment.ID);
            Assert.Equal(getDasPayment.ID, payment.ID);
            byte[] pdf = DasPayment.Pdf(id: payment.ID);
            Assert.True(pdf.Length > 0);
            System.IO.File.WriteAllBytes("dasPayment.pdf", pdf);
            DasPayment deleteDasPayment = DasPayment.Delete(id: payment.ID);
            Assert.Equal(deleteDasPayment.ID, payment.ID);
            Console.WriteLine(payment);
        }

        [Fact]
        public void Query()
        {
            List<DasPayment> payments = DasPayment.Query(limit: 101, status: "success").ToList();
            Assert.True(payments.Count <= 101);
            Assert.True(payments.First().ID != payments.Last().ID);
            foreach (DasPayment payment in payments)
            {
                Console.WriteLine(payment);
                Assert.NotNull(payment.ID);
                Assert.Equal("success", payment.Status);
            }
        }

        private DasPayment Example()
        {
            Random random = new Random();
            return new DasPayment(
                barCode: "8366" + random.Next(100, 100000).ToString("D11") + "02380074119002551100010601813",
                scheduled: DateTime.Today.Date.AddDays(2),
                description: "pagando a conta"
            );
        }
    }
}
