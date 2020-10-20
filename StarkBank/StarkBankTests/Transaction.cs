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
            TestUtils.Log(transaction);
        }

        [Fact]
        public void Query()
        {
            List<Transaction> transactions = Transaction.Query(limit: 101).ToList();
            Assert.Equal(101, transactions.Count);
            Assert.True(transactions.First().ID != transactions.Last().ID);
            foreach (Transaction transaction in transactions)
            {
                TestUtils.Log(transaction);
                Assert.NotNull(transaction.ID);
            }
        }

        [Fact]
        public void QueryIds()
        {
            List<Transaction> transactions = Transaction.Query(limit: 10).ToList();
            List<String> transactionsIdsExpected = new List<string>();
            Assert.Equal(10, transactions.Count);
            Assert.True(transactions.First().ID != transactions.Last().ID);
            foreach (Transaction transaction in transactions)
            {
                Assert.NotNull(transaction.ID);
                transactionsIdsExpected.Add(transaction.ID);
            }

            List<Transaction> transactionsResult = Transaction.Query(limit:10, ids:transactionsIdsExpected).ToList();
            List<String> transactionsIdsResult = new List<string>();
            Assert.Equal(10, transactions.Count);
            Assert.True(transactions.First().ID != transactions.Last().ID);
            foreach (Transaction transaction in transactionsResult)
            {
                Assert.NotNull(transaction.ID);
                transactionsIdsResult.Add(transaction.ID);
            }

            transactionsIdsExpected.Sort();
            transactionsIdsResult.Sort();
            Assert.Equal(transactionsIdsExpected, transactionsIdsResult);
        }

        internal static Transaction Example()
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
