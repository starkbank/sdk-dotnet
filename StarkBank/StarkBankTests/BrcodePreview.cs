using Xunit;
using StarkBank;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class BrcodePreviewTest
    {
        public readonly User user = TestUser.SetDefault();

        [Fact]
        public void Query()
        {
            List<BrcodePreview> payments = BrcodePreview.Query(brcodes: new List<string>(){ "00020126580014br.gov.bcb.pix0136a629532e-7693-4846-852d-1bbff817b5a8520400005303986540510.005802BR5908T'Challa6009Sao Paulo62090505123456304B14A" }).ToList();
            Assert.True(payments.Count >= 1);
            foreach (BrcodePreview payment in payments) {
                TestUtils.Log(payment);
                Assert.NotNull(payment.Amount);
            }
        }
    }
}
