using System;
using System.Collections.Generic;
using System.Linq;
using StarkBank;
using Xunit;

namespace StarkBankTests
{
    public class PaymentPreviewTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Create()
        {
            BrcodePayment brcodePayment = BrcodePaymentTest.Example(false);
            BoletoPayment boletoPayment = BoletoPaymentTest.Example(false);
            TaxPayment taxPayment = TaxPaymentTest.Example(false);
            UtilityPayment utilityPayment = UtilityPaymentTest.Example(false);
            List<PaymentPreview> previews = PaymentPreview.Create(new List<PaymentPreview>
            {
                new PaymentPreview(id: brcodePayment.Brcode),
                new PaymentPreview(id: boletoPayment.Line),
                new PaymentPreview(id: taxPayment.BarCode),
                new PaymentPreview(id: utilityPayment.BarCode)
            });

            foreach (PaymentPreview preview in previews)
            {
                Assert.NotNull(preview);
                Assert.IsType(new PaymentPreview().GetType(), preview);
            }
        }
    }
}
