using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class TransactionTest
    {
        public readonly User user = TestUser.SetDefault();

        [Fact]
        public void CreateAndGet()
        {
            List<Transaction> transactions = Transaction.Create(new List<Transaction>() { Example() });
            Transaction transaction = transactions.First();
            Assert.NotNull(transactions.First().ID);
            Transaction deleteTransaction = Transaction.Get(id: transaction.ID);
            Assert.Equal(deleteTransaction.ID, transaction.ID);
        }

        [Fact]
        public void Query()
        {
            List<Transaction> transactions = Transaction.Query(limit: 101).ToList();
            Assert.Equal(101, transactions.Count);
            Assert.True(transactions.First().ID != transactions.Last().ID);
            foreach (Transaction transaction in transactions)
            {
                Assert.NotNull(transaction.ID);
            }
        }

        private Transaction Example()
        {
            return new Transaction(
                amount: 50,
                receiverID: "5768064935133184",
                externalID: Guid.NewGuid().ToString(),
                description: "Transferência para Workspace aleatório",
                tags: new List<string> { "custom", "tags" }
            );
        }
    }
}
