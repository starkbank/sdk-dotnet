using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class VerifiedTransferTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Create()
        {
            // The API caps VerifiedAccount creation at three per tax id per 24h, and the
            // fixtures use a fixed tax id, so reuse an account that is already active.
            VerifiedAccount account = VerifiedAccount.Query(limit: 1, status: "active").FirstOrDefault()
                ?? VerifiedAccount.Create(new List<VerifiedAccount>() { VerifiedAccountTest.Example() }).First();
            string accountId = account.ID;

            VerifiedTransfer example = Example(accountId);
            List<VerifiedTransfer> transfers = VerifiedTransfer.Create(new List<VerifiedTransfer>() { example });
            VerifiedTransfer transfer = transfers.First();
            Assert.NotNull(transfer.ID);
            Assert.Equal(example.Amount, transfer.Amount);
            Assert.NotNull(transfer.Status);
            TestUtils.Log(transfer);

            Assert.NotEmpty(transfer.Rules);
            Transfer.Rule sentRule = transfer.Rules.FirstOrDefault(rule => rule.Key == "resendingLimit");
            Assert.NotNull(sentRule);
            Assert.Equal(5, sentRule.Value);
            foreach (Transfer.Rule rule in transfer.Rules)
            {
                TestUtils.Log(rule);
            }

            Transfer getTransfer = Transfer.Get(transfer.ID);
            Assert.Equal(transfer.ID, getTransfer.ID);
        }

        internal static VerifiedTransfer Example(string accountId)
        {
            return new VerifiedTransfer(
                amount: new Random().Next(1, 1000),
                accountId: accountId,
                externalId: Guid.NewGuid().ToString(),
                scheduled: DateTime.Now.AddDays(1),
                description: "Good description",
                displayDescription: "Good display description",
                tags: new List<string> { "iron", "suit" },
                rules: new List<Transfer.Rule>() {
                    new Transfer.Rule(
                        key: "resendingLimit",
                        value: 5
                    )
                }
            );
        }
    }
}
