using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;

namespace StarkBankTests
{
    public class SplitReceiverTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void CreateAndGet()
        {
            List<SplitReceiver> receivers = SplitReceiver.Create(new List<SplitReceiver>() { Example(), Example() });
            SplitReceiver receiver = receivers.First();
            Assert.NotNull(receiver.ID);
            Assert.NotNull(receiver.AccountNumber);
            Assert.NotNull(receiver.BankCode);
            Assert.NotNull(receiver.BranchCode);
            Assert.NotNull(receiver.Name);
            Assert.NotNull(receiver.TaxId);

            SplitReceiver getReceiver = SplitReceiver.Get(id: receiver.ID);
            Assert.Equal(getReceiver.ID, receiver.ID);
            Assert.Equal(getReceiver.AccountNumber, receiver.AccountNumber);
            Assert.Equal(getReceiver.BankCode, receiver.BankCode);
            Assert.Equal(getReceiver.TaxId, receiver.TaxId);
            TestUtils.Log(receiver);
            TestUtils.Log(getReceiver);
        }

        [Fact]
        public void CreateWithDictionary()
        {
            List<Dictionary<string, object>> receiversDict = new List<Dictionary<string, object>>()
            {
                ExampleDictionary(),
                ExampleDictionary()
            };

            List<SplitReceiver> receivers = SplitReceiver.Create(receiversDict);
            Assert.Equal(2, receivers.Count);

            foreach (SplitReceiver receiver in receivers)
            {
                TestUtils.Log(receiver);
                Assert.NotNull(receiver.ID);
                Assert.NotNull(receiver.AccountNumber);
                Assert.NotNull(receiver.BankCode);
                Assert.NotNull(receiver.BranchCode);
                Assert.NotNull(receiver.Name);
                Assert.NotNull(receiver.TaxId);
            }
        }

        [Fact]
        public void Query()
        {
            List<SplitReceiver> receivers = SplitReceiver.Query(limit: 10).ToList();
            Assert.True(receivers.Count <= 10);

            if (receivers.Count > 1)
            {
                Assert.True(receivers.First().ID != receivers.Last().ID);
            }

            foreach (SplitReceiver receiver in receivers)
            {
                TestUtils.Log(receiver);
                Assert.NotNull(receiver.ID);
                Assert.NotNull(receiver.AccountNumber);
                Assert.NotNull(receiver.BankCode);
                Assert.NotNull(receiver.Name);
                Assert.NotNull(receiver.TaxId);
            }
        }

        [Fact]
        public void QueryWithFilters()
        {
            DateTime after = DateTime.Today.AddDays(-30);
            DateTime before = DateTime.Today;

            List<SplitReceiver> receivers = SplitReceiver.Query(
                limit: 5,
                after: after,
                before: before,
                tags: new List<string> { "test" }
            ).ToList();

            Assert.True(receivers.Count <= 5);
            foreach (SplitReceiver receiver in receivers)
            {
                TestUtils.Log(receiver);
                Assert.NotNull(receiver.ID);
                Assert.True(receiver.Created >= after);
                Assert.True(receiver.Created <= before);
            }
        }

        [Fact]
        public void QueryByStatus()
        {
            string[] statuses = { "active", "blocked", "canceled" };

            foreach (string status in statuses)
            {
                List<SplitReceiver> receivers = SplitReceiver.Query(limit: 3, status: status).ToList();
                Assert.True(receivers.Count <= 3);

                foreach (SplitReceiver receiver in receivers)
                {
                    TestUtils.Log(receiver);
                    Assert.NotNull(receiver.ID);
                    Assert.Equal(status, receiver.Status);
                }
            }
        }

        [Fact]
        public void QueryByTaxId()
        {
            List<SplitReceiver> allReceivers = SplitReceiver.Query(limit: 5).ToList();
            if (allReceivers.Count > 0)
            {
                string taxId = allReceivers.First().TaxId;

                List<SplitReceiver> filteredReceivers = SplitReceiver.Query(
                    limit: 10,
                    taxId: taxId
                ).ToList();

                foreach (SplitReceiver receiver in filteredReceivers)
                {
                    TestUtils.Log(receiver);
                    Assert.Equal(taxId, receiver.TaxId);
                }
            }
        }

        [Fact]
        public void QueryByIds()
        {
            List<SplitReceiver> allReceivers = SplitReceiver.Query(limit: 5).ToList();
            if (allReceivers.Count > 0)
            {
                List<string> receiverIds = allReceivers.Take(3).Select(r => r.ID).ToList();

                List<SplitReceiver> queriedReceivers = SplitReceiver.Query(ids: receiverIds).ToList();

                Assert.True(queriedReceivers.Count <= receiverIds.Count);
                foreach (SplitReceiver receiver in queriedReceivers)
                {
                    TestUtils.Log(receiver);
                    Assert.Contains(receiver.ID, receiverIds);
                }
            }
        }

        [Fact]
        public void QueryByReceiverIds()
        {
            List<SplitReceiver> allReceivers = SplitReceiver.Query(limit: 5).ToList();
            if (allReceivers.Count > 0)
            {
                List<string> receiverIds = allReceivers.Take(2).Select(r => r.ID).ToList();

                List<SplitReceiver> filteredReceivers = SplitReceiver.Query(
                    limit: 10,
                    receiverIds: receiverIds
                ).ToList();

                foreach (SplitReceiver receiver in filteredReceivers)
                {
                    TestUtils.Log(receiver);
                    Assert.Contains(receiver.ID, receiverIds);
                }
            }
        }

        [Fact]
        public void QueryByTransactionIds()
        {
            List<string> transactionIds = new List<string> { "1234567890123456", "9876543210987654" };

            List<SplitReceiver> receivers = SplitReceiver.Query(
                limit: 5,
                transactionIds: transactionIds
            ).ToList();

            Assert.True(receivers.Count <= 5);
            foreach (SplitReceiver receiver in receivers)
            {
                TestUtils.Log(receiver);
                Assert.NotNull(receiver.ID);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<SplitReceiver> page;
            string cursor = null;

            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = SplitReceiver.Page(limit: 5, cursor: cursor);
                foreach (SplitReceiver entity in page)
                {
                    TestUtils.Log(entity);
                    Assert.DoesNotContain(entity.ID, ids);
                    ids.Add(entity.ID);
                    Assert.NotNull(entity.AccountNumber);
                    Assert.NotNull(entity.BankCode);
                    Assert.NotNull(entity.Name);
                    Assert.NotNull(entity.TaxId);
                }
                if (cursor == null)
                {
                    break;
                }
            }
            Assert.True(ids.Count <= 10);
        }

        [Fact]
        public void PageWithFilters()
        {
            List<string> ids = new List<string>();
            List<SplitReceiver> page;
            string cursor = null;
            DateTime after = DateTime.Today.AddDays(-30);

            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = SplitReceiver.Page(
                    limit: 3,
                    cursor: cursor,
                    after: after,
                    status: "active"
                );

                foreach (SplitReceiver entity in page)
                {
                    TestUtils.Log(entity);
                    Assert.DoesNotContain(entity.ID, ids);
                    ids.Add(entity.ID);
                    Assert.Equal("active", entity.Status);
                    Assert.True(entity.Created >= after);
                }
                if (cursor == null)
                {
                    break;
                }
            }
        }

        [Fact]
        public void GetValidation()
        {
            List<SplitReceiver> receivers = SplitReceiver.Query(limit: 1).ToList();
            if (receivers.Count > 0)
            {
                SplitReceiver receiver = receivers.First();
                SplitReceiver getReceiver = SplitReceiver.Get(id: receiver.ID);

                Assert.Equal(receiver.ID, getReceiver.ID);
                Assert.Equal(receiver.AccountNumber, getReceiver.AccountNumber);
                Assert.Equal(receiver.AccountType, getReceiver.AccountType);
                Assert.Equal(receiver.BankCode, getReceiver.BankCode);
                Assert.Equal(receiver.BranchCode, getReceiver.BranchCode);
                Assert.Equal(receiver.Name, getReceiver.Name);
                Assert.Equal(receiver.Status, getReceiver.Status);
                Assert.Equal(receiver.TaxId, getReceiver.TaxId);
                Assert.Equal(receiver.Created, getReceiver.Created);
                Assert.Equal(receiver.Updated, getReceiver.Updated);

                if (receiver.Tags != null && getReceiver.Tags != null)
                {
                    Assert.Equal(receiver.Tags.Count, getReceiver.Tags.Count);
                }

                TestUtils.Log(getReceiver);
            }
        }

        [Fact]
        public void ValidateProperties()
        {
            List<SplitReceiver> receivers = SplitReceiver.Query(limit: 5).ToList();
            foreach (SplitReceiver receiver in receivers)
            {
                TestUtils.Log(receiver);

                // Required properties
                Assert.NotNull(receiver.ID);
                Assert.NotNull(receiver.AccountNumber);
                Assert.NotNull(receiver.BankCode);
                Assert.NotNull(receiver.BranchCode);
                Assert.NotNull(receiver.Name);
                Assert.NotNull(receiver.TaxId);

                // Validate account number format
                Assert.True(receiver.AccountNumber.Length > 0);

                // Validate bank code format (Brazilian bank codes are typically 3 digits)
                Assert.True(receiver.BankCode.Length >= 3);

                // Validate branch code format
                Assert.True(receiver.BranchCode.Length > 0);

                // Validate account type
                if (receiver.AccountType != null)
                {
                    string[] validTypes = { "checking", "savings", "salary", "payment" };
                    Assert.Contains(receiver.AccountType, validTypes);
                }

                // Validate status
                if (receiver.Status != null)
                {
                    string[] validStatuses = { "active", "blocked", "canceled" };
                    Assert.Contains(receiver.Status, validStatuses);
                }

                // Validate tax ID format (Brazilian CPF or CNPJ)
                Assert.True(receiver.TaxId.Length >= 11); // CPF has 11 digits, CNPJ has 14

                // Tags validation
                if (receiver.Tags != null)
                {
                    Assert.True(receiver.Tags.Count >= 0);
                    foreach (string tag in receiver.Tags)
                    {
                        Assert.NotNull(tag);
                        Assert.True(tag.Length > 0);
                    }
                }

                // Date validation
                Assert.True(receiver.Created <= DateTime.UtcNow);
                Assert.True(receiver.Updated <= DateTime.UtcNow);
                Assert.True(receiver.Updated >= receiver.Created);
            }
        }

        internal static SplitReceiver Example()
        {
            Random random = new Random();
            string randomNumber = random.Next(100000, 999999).ToString();

            return new SplitReceiver(
                id: null, // Will be set by API
                accountNumber: $"1234{randomNumber}",
                accountType: "checking",
                bankCode: "341", // Itaú bank code
                branchCode: $"12{random.Next(10, 99)}",
                created: DateTime.UtcNow,
                name: $"Test Split Receiver {randomNumber}",
                status: "active",
                tags: new List<string> { "test", "split-receiver", randomNumber },
                taxId: "012.345.678-90", // Valid test CPF
                updated: DateTime.UtcNow
            );
        }

        internal static Dictionary<string, object> ExampleDictionary()
        {
            Random random = new Random();
            string randomNumber = random.Next(100000, 999999).ToString();

            return new Dictionary<string, object>
            {
                { "accountNumber", $"5678{randomNumber}" },
                { "accountType", "savings" },
                { "bankCode", "237" }, // Bradesco bank code
                { "branchCode", $"34{random.Next(10, 99)}" },
                { "name", $"Test Dict Receiver {randomNumber}" },
                { "taxId", "20.018.183/0001-80" }, // Valid test CNPJ
                { "tags", new List<string> { "test", "dictionary", randomNumber } }
            };
        }
    }

    public class SplitReceiverLogTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void QueryAndGet()
        {
            List<SplitReceiver.Log> logs = SplitReceiver.Log.Query(limit: 10).ToList();
            Assert.True(logs.Count <= 10);

            if (logs.Count > 1)
            {
                Assert.True(logs.First().ID != logs.Last().ID);
            }

            foreach (SplitReceiver.Log log in logs)
            {
                TestUtils.Log(log);
                Assert.NotNull(log.ID);
                Assert.NotNull(log.Type);
                Assert.True(log.Created <= DateTime.UtcNow);
                Assert.NotNull(log.Receiver);

                SplitReceiver.Log getLog = SplitReceiver.Log.Get(id: log.ID);
                Assert.Equal(getLog.ID, log.ID);
                Assert.Equal(getLog.Type, log.Type);
                TestUtils.Log(getLog);
            }
        }

        [Fact]
        public void QueryByTypes()
        {
            string[] logTypes = { "created", "success", "failed" };

            foreach (string logType in logTypes)
            {
                List<SplitReceiver.Log> logs = SplitReceiver.Log.Query(
                    limit: 5,
                    types: new List<string> { logType }
                ).ToList();

                Assert.True(logs.Count <= 5);
                foreach (SplitReceiver.Log log in logs)
                {
                    TestUtils.Log(log);
                    Assert.Equal(logType, log.Type);
                }
            }
        }

        [Fact]
        public void QueryWithDateFilters()
        {
            DateTime after = DateTime.Today.AddDays(-30);
            DateTime before = DateTime.Today;

            List<SplitReceiver.Log> logs = SplitReceiver.Log.Query(
                limit: 5,
                after: after,
                before: before
            ).ToList();

            Assert.True(logs.Count <= 5);
            foreach (SplitReceiver.Log log in logs)
            {
                TestUtils.Log(log);
                Assert.True(log.Created >= after);
                Assert.True(log.Created <= before);
            }
        }

        [Fact]
        public void QueryByReceiverIds()
        {
            List<SplitReceiver> receivers = SplitReceiver.Query(limit: 3).ToList();
            if (receivers.Count > 0)
            {
                List<string> receiverIds = receivers.Select(r => r.ID).ToList();

                List<SplitReceiver.Log> logs = SplitReceiver.Log.Query(
                    limit: 10,
                    receiverIds: receiverIds
                ).ToList();

                foreach (SplitReceiver.Log log in logs)
                {
                    TestUtils.Log(log);
                    Assert.Contains(log.Receiver.ID, receiverIds);
                }
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<SplitReceiver.Log> page;
            string cursor = null;

            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = SplitReceiver.Log.Page(limit: 5, cursor: cursor);
                foreach (SplitReceiver.Log entity in page)
                {
                    TestUtils.Log(entity);
                    Assert.DoesNotContain(entity.ID, ids);
                    ids.Add(entity.ID);
                    Assert.NotNull(entity.Type);
                    Assert.NotNull(entity.Receiver);
                }
                if (cursor == null)
                {
                    break;
                }
            }
            Assert.True(ids.Count <= 10);
        }

        [Fact]
        public void PageWithFilters()
        {
            List<string> ids = new List<string>();
            List<SplitReceiver.Log> page;
            string cursor = null;
            DateTime after = DateTime.Today.AddDays(-30);

            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = SplitReceiver.Log.Page(
                    limit: 3,
                    cursor: cursor,
                    after: after,
                    types: new List<string> { "success", "failed" }
                );

                foreach (SplitReceiver.Log entity in page)
                {
                    TestUtils.Log(entity);
                    Assert.DoesNotContain(entity.ID, ids);
                    ids.Add(entity.ID);
                    Assert.Contains(entity.Type, new[] { "success", "failed" });
                    Assert.True(entity.Created >= after);
                }
                if (cursor == null)
                {
                    break;
                }
            }
        }

        [Fact]
        public void ValidateLogProperties()
        {
            List<SplitReceiver.Log> logs = SplitReceiver.Log.Query(limit: 5).ToList();
            foreach (SplitReceiver.Log log in logs)
            {
                TestUtils.Log(log);

                // Required properties
                Assert.NotNull(log.ID);
                Assert.NotNull(log.Type);
                Assert.True(log.Created <= DateTime.UtcNow);
                Assert.NotNull(log.Receiver);

                // Validate nested SplitReceiver object
                Assert.NotNull(log.Receiver.ID);
                Assert.NotNull(log.Receiver.AccountNumber);
                Assert.NotNull(log.Receiver.BankCode);
                Assert.NotNull(log.Receiver.Name);
                Assert.NotNull(log.Receiver.TaxId);

                // Errors list validation
                if (log.Errors != null)
                {
                    Assert.True(log.Errors.Count >= 0);
                    foreach (string error in log.Errors)
                    {
                        Assert.NotNull(error);
                        Assert.True(error.Length > 0);
                    }
                }

                // Validate log type
                string[] validTypes = { "created", "success", "failed" };
                Assert.Contains(log.Type, validTypes);
            }
        }

        [Fact]
        public void GetValidation()
        {
            List<SplitReceiver.Log> logs = SplitReceiver.Log.Query(limit: 1).ToList();
            if (logs.Count > 0)
            {
                SplitReceiver.Log log = logs.First();
                SplitReceiver.Log getLog = SplitReceiver.Log.Get(id: log.ID);

                Assert.Equal(log.ID, getLog.ID);
                Assert.Equal(log.Type, getLog.Type);
                Assert.Equal(log.Created, getLog.Created);
                Assert.Equal(log.Receiver.ID, getLog.Receiver.ID);

                if (log.Errors != null && getLog.Errors != null)
                {
                    Assert.Equal(log.Errors.Count, getLog.Errors.Count);
                }

                TestUtils.Log(getLog);
            }
        }
    }
}