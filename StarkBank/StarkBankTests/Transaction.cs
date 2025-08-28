using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class TransactionTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Create()
        {
            Assert.Throws<Exception>(() => Transaction.Create(new List<Transaction>() { Example() }));
        }

        [Fact]
        public void QueryAndGet()
        {
            List<Transaction> transactions = Transaction.Query(limit: 101).ToList();
            Assert.Equal(101, transactions.Count);
            Assert.True(transactions.First().ID != transactions.Last().ID);
            foreach (Transaction transaction in transactions)
            {
                TestUtils.Log(transaction);
                Assert.NotNull(transaction.ID);
                Transaction deleteTransaction = Transaction.Get(id: transaction.ID);
                Assert.Equal(deleteTransaction.ID, transaction.ID);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<Transaction> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = Transaction.Page(limit: 5, cursor: cursor);
                foreach (Transaction entity in page)
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
