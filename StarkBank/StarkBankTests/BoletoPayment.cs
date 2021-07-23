using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class BoletoPaymentTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void CreateGetGetPdfAndDelete()
        {
            List<BoletoPayment> payments = BoletoPayment.Create(new List<BoletoPayment>() { Example() });
            BoletoPayment payment = payments.First();
            Assert.NotNull(payment.ID);
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
            TestUtils.Log(payment);
        }

        [Fact]
        public void Query()
        {
            List<BoletoPayment> payments = BoletoPayment.Query(limit: 101, status: "failed").ToList();
            Assert.True(payments.Count <= 101);
            Assert.True(payments.First().ID != payments.Last().ID);
            foreach (BoletoPayment payment in payments)
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
            List<BoletoPayment> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = BoletoPayment.Page(limit: 5, cursor: cursor);
                foreach (BoletoPayment entity in page)
                {
                    Assert.DoesNotContain(entity.ID, ids);
                    ids.Add(entity.ID);
                }
                if (cursor == null)
                {
                    break;
                }
            }
            Console.WriteLine(ids);
            Assert.True(ids.Count == 10);
        }

        internal static BoletoPayment Example(bool schedule = true)
        {
            DateTime? scheduled = null;
            if (schedule) {
                scheduled = DateTime.Today.Date.AddDays(1);
            }
            Boleto boleto = BoletoTest.Example();
            boleto = Boleto.Create(new List<Boleto>() { boleto }).First();
            return new BoletoPayment(
                line: boleto.Line,
                scheduled: scheduled,
                description: "loading a random account",
                taxID: boleto.TaxID
            );
        }
    }
}
