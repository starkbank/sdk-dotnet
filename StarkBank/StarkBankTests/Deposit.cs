using Xunit;
using StarkBank;
using System.Linq;
using System.Collections.Generic;
using StarkCore;


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

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<Deposit> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = Deposit.Page(limit: 5, cursor: cursor);
                foreach (Deposit entity in page)
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
    }
}
