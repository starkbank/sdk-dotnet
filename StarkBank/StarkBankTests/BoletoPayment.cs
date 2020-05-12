using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class BoletoPaymentTest
    {
        public readonly User user = TestUser.SetDefault();

        [Fact]
        public void CreateGetGetPdfAndDelete()
        {
            List<BoletoPayment> payments = BoletoPayment.Create(new List<BoletoPayment>() { Example() });
            BoletoPayment payment = payments.First();
            Assert.NotNull(payments.First().ID);
            BoletoPayment getBoletoPayment = BoletoPayment.Get(id: payment.ID);
            Assert.Equal(getBoletoPayment.ID, payment.ID);
            byte[] pdf = BoletoPayment.Pdf(id: payment.ID);
            Assert.True(pdf.Length > 0);
            System.IO.File.WriteAllBytes("boletoPayment.pdf", pdf);
            try {
                BoletoPayment deleteBoletoPayment = BoletoPayment.Delete(id: payment.ID);
                Assert.Equal(deleteBoletoPayment.ID, payment.ID);
            } catch (StarkBank.Error.InputErrors e)
            {
                foreach(StarkBank.Error.ErrorElement error in e.Errors) {
                    Assert.Equal("invalidAction", error.Code);
                }
            }
            Console.WriteLine(payment);
        }

        [Fact]
        public void Query()
        {
            List<BoletoPayment> payments = BoletoPayment.Query(limit: 101, status: "success").ToList();
            Assert.True(payments.Count <= 101);
            Assert.True(payments.First().ID != payments.Last().ID);
            foreach (BoletoPayment payment in payments)
            {
                Console.WriteLine(payment);
                Assert.NotNull(payment.ID);
                Assert.Equal("success", payment.Status);
            }
        }

        private BoletoPayment Example()
        {
            Random random = new Random();
            return new BoletoPayment(
                line: "34191.09008 61713.957208 71444.640008 2 834300" + random.Next(0, 99999999).ToString("D8"),
                scheduled: DateTime.Today.Date.AddDays(1),
                description: "loading a random account",
                taxID: "20.018.183/0001-80"
            );
        }
    }
}
