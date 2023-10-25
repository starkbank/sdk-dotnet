using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;
using StarkCore;
using StarkCore.Utils;


namespace StarkBankTests
{
    public class PaymentRequestTest
    {
        public readonly User user = TestUser.SetDefaultProject();

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

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<PaymentRequest> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = PaymentRequest.Page(limit: 5, cursor: cursor, centerID: Environment.GetEnvironmentVariable("SANDBOX_CENTER_ID"));
                foreach (PaymentRequest entity in page)
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

        internal static PaymentRequest Example()
        {
            Random random = new Random();
            Resource payment = CreatePayment();
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

        private static Resource CreatePayment()
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
            if (choice == 4) {
                return BrcodePaymentTest.Example(schedule: false);
            }
            throw new Exception("bad switch");
        }
    }
}
