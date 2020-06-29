using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class IssPaymentTest
    {
        public readonly User user = TestUser.SetDefault();

        [Fact]
        public void CreateGetGetPdfAndDelete()
        {
            List<IssPayment> payments = IssPayment.Create(new List<IssPayment>() { Example() });
            IssPayment payment = payments.First();
            Assert.NotNull(payments.First().ID);
            IssPayment getIssPayment = IssPayment.Get(id: payment.ID);
            Assert.Equal(getIssPayment.ID, payment.ID);
            byte[] pdf = IssPayment.Pdf(id: payment.ID);
            Assert.True(pdf.Length > 0);
            System.IO.File.WriteAllBytes("issPayment.pdf", pdf);
            IssPayment deleteIssPayment = IssPayment.Delete(id: payment.ID);
            Assert.Equal(deleteIssPayment.ID, payment.ID);
            Console.WriteLine(payment);
        }

        [Fact]
        public void Query()
        {
            List<IssPayment> payments = IssPayment.Query(limit: 101, status: "success").ToList();
            Assert.True(payments.Count <= 101);
            Assert.True(payments.First().ID != payments.Last().ID);
            foreach (IssPayment payment in payments)
            {
                Console.WriteLine(payment);
                Assert.NotNull(payment.ID);
                Assert.Equal("success", payment.Status);
            }
        }

        private IssPayment Example()
        {
            Random random = new Random();
            return new IssPayment(
                barCode: "8366" + random.Next(100, 100000).ToString("D11") + "02380074119002551100010601813",
                scheduled: DateTime.Today.Date.AddDays(2),
                description: "pagando a conta"
            );
        }
    }
}
