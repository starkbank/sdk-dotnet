using System;
using StarkBank;
using Xunit;
using System.Diagnostics;
using StarkCore;


namespace StarkBankTests
{
    public class CorporateBalanceTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Get()
        {
            CorporateBalance balance = CorporateBalance.Get();
            Assert.NotNull(balance.ID);
            Assert.True(balance.Amount >= 0);
            Assert.Equal(3, balance.Currency.Length);
            Assert.True(balance.Updated <= DateTime.UtcNow);
            TestUtils.Log(balance);
        }
    }
}
