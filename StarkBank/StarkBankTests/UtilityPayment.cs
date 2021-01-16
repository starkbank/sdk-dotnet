﻿using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class UtilityPaymentTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void CreateGetGetPdfAndDelete()
        {
            List<UtilityPayment> payments = UtilityPayment.Create(new List<UtilityPayment>() { Example() });
            UtilityPayment payment = payments.First();
            Assert.NotNull(payments.First().ID);
            UtilityPayment getUtilityPayment = UtilityPayment.Get(id: payment.ID);
            Assert.Equal(getUtilityPayment.ID, payment.ID);
            byte[] pdf = UtilityPayment.Pdf(id: payment.ID);
            Assert.True(pdf.Length > 0);
            System.IO.File.WriteAllBytes("utilityPayment.pdf", pdf);
            UtilityPayment deleteUtilityPayment = UtilityPayment.Delete(id: payment.ID);
            Assert.Equal(deleteUtilityPayment.ID, payment.ID);
            TestUtils.Log(payment);
        }

        [Fact]
        public void Query()
        {
            List<UtilityPayment> payments = UtilityPayment.Query(limit: 101, status: "success").ToList();
            Assert.True(payments.Count <= 101);
            Assert.True(payments.First().ID != payments.Last().ID);
            foreach (UtilityPayment payment in payments)
            {
                TestUtils.Log(payment);
                Assert.NotNull(payment.ID);
                Assert.Equal("success", payment.Status);
            }
        }

        internal static UtilityPayment Example(bool schedule = true)
        {
            DateTime? scheduled = null;
            if (schedule)
            {
                scheduled = DateTime.Today.Date.AddDays(2);
            }
            Random random = new Random();
            return new UtilityPayment(
                barCode: "8366" + random.Next(100, 100000).ToString("D11") + "01380074119002551100010601813",
                scheduled: scheduled,
                description: "pagando a conta"
            );
        }
    }
}
