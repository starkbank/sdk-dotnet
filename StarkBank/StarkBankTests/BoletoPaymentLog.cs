﻿using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;

namespace StarkBankTests
{
    public class BoletoPaymentLogTest
    {
        public readonly User user = TestUser.SetDefault();

        [Fact]
        public void QueryAndGet()
        {
            List<BoletoPayment.Log> logs = BoletoPayment.Log.Query(
                limit: 101,
                types: new List<string> { "success" }
            ).ToList();
            Assert.True(logs.Count <= 101);
            Assert.True(logs.First().ID != logs.Last().ID);
            foreach (BoletoPayment.Log log in logs)
            {
                Console.WriteLine(log);
                Assert.NotNull(log.ID);
                Assert.Equal("success", log.Type);
            }
            BoletoPayment.Log getLog = BoletoPayment.Log.Get(id: logs.First().ID);
            Assert.Equal(getLog.ID, logs.First().ID);
            Console.WriteLine(getLog);
        }
    }
}
