using Xunit;
using StarkBank;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class DepositTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void QueryAndGet()
        {
            List<Deposit> deposits = Deposit.Query(limit: 2).ToList();
            Assert.Equal(2, deposits.Count);
            Assert.True(deposits.First().ID != deposits.Last().ID);
            foreach (Deposit deposit in deposits)
            {
                TestUtils.Log(deposit);
                Assert.NotNull(deposit.ID);
                TestUtils.Log(deposit);
            }
        }
    }
}