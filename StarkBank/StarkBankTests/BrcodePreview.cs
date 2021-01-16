using Xunit;
using StarkBank;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class BrcodePreviewTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Query()
        {
            List<BrcodePreview> payments = BrcodePreview.Query(brcodes: new List<string>(){ "00020126580014br.gov.bcb.pix013635719950-ac93-4bab-8ad6-56d7fb63afd252040000530398654040.005802BR5915Stark Bank S.A.6009Sao Paulo62070503***6304AA26" }).ToList();
            Assert.True(payments.Count >= 1);
            foreach (BrcodePreview payment in payments) {
                TestUtils.Log(payment);
                Assert.NotNull(payment.Amount);
            }
        }
    }
}
