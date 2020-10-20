using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class PaymentRequestTest
    {
        public readonly User user = TestUser.SetDefault();

        [Fact]
        public void Create()
        {
            List<PaymentRequest> requests = new List<PaymentRequest>();
            for (int i = 0; i < 10; i++) {
                requests.Add(Example());
            }
            requests = PaymentRequest.Create(requests);
            foreach(PaymentRequest payment in requests) {
                TestUtils.Log(payment);
            }
        }

        [Fact]
        public void Query()
        {
            string centerID = Environment.GetEnvironmentVariable("SANDBOX_CENTER_ID");
            List<PaymentRequest> requests = PaymentRequest.Query(centerID: centerID, limit: 101).ToList();
            Assert.Equal(101, requests.Count);
            Assert.True(requests.First().ID != requests.Last().ID);
            foreach (PaymentRequest request in requests)
            {
                TestUtils.Log(request);
                Assert.NotNull(request.ID);
            }
        }

        internal static PaymentRequest Example()
        {
            Random random = new Random();
            StarkBank.Utils.Resource payment = CreatePayment();
            DateTime? due = null;
            if (payment.GetType() != typeof(Transaction)) {
                due = DateTime.Today.Date.AddDays(random.Next(0, 7));
            }
            return new PaymentRequest(
                centerID: Environment.GetEnvironmentVariable("SANDBOX_CENTER_ID"),
                payment: payment,
                due: due
            );
        }

        private static StarkBank.Utils.Resource CreatePayment()
        {
            Random random = new Random();
            int choice = random.Next(0, 3);
            if (choice == 0) {
                return TransferTest.Example(schedule: false);
            }
            if (choice == 1) {
                return TransactionTest.Example();
            }
            if (choice == 2) {
                return BoletoPaymentTest.Example(schedule: false);
            }
            if (choice == 3) {
                return UtilityPaymentTest.Example(schedule: false);
            }
            throw new Exception("bad switch");
        }
    }
}
