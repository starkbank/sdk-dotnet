using Xunit;
using System;
using StarkBank;


namespace StarkBankTests
{
    public class BalanceTest
    {
        public readonly User user = TestUser.SetDefault();

        [Fact]
        public void Get()
        {
            Balance balance = Balance.Get();
            Assert.NotNull(balance.ID);
            Assert.True(balance.Amount >= 0);
            Assert.Equal(3, balance.Currency.Length);
            Assert.True(balance.Updated <= DateTime.UtcNow);
            TestUtils.Log(balance);
        }
    }
}
