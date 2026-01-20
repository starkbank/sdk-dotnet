using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;

namespace StarkBankTests
{
    public class SplitTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void QueryAndGet()
        {
            List<Split> splits = Split.Query(limit: 5).ToList();
            Assert.True(splits.Count <= 5);
            foreach (Split split in splits)
            {
                TestUtils.Log(split);
                Assert.NotNull(split.ID);
                Assert.True(split.Amount > 0);
                Assert.NotNull(split.ReceiverId);
                Assert.NotNull(split.Status);
                
                Split getSplit = Split.Get(id: split.ID);
                Assert.Equal(getSplit.ID, split.ID);
                Assert.Equal(getSplit.Amount, split.Amount);
                Assert.Equal(getSplit.ReceiverId, split.ReceiverId);
                TestUtils.Log(getSplit);
            }
        }

        [Fact]
        public void Query()
        {
            List<Split> splits = Split.Query(limit: 10).ToList();
            Assert.True(splits.Count <= 10);
            
            if (splits.Count > 1)
            {
                Assert.True(splits.First().ID != splits.Last().ID);
            }
            
            foreach (Split split in splits)
            {
                TestUtils.Log(split);
                Assert.NotNull(split.ID);
                Assert.NotNull(split.ReceiverId);
                Assert.True(split.Amount > 0);
            }
        }

        [Fact]
        public void QueryWithFilters()
        {
            DateTime after = DateTime.Today.AddDays(-30);
            DateTime before = DateTime.Today;
            
            List<Split> splits = Split.Query(
                limit: 5,
                after: after,
                before: before,
                tags: new List<string> { "test" }
            ).ToList();
            
            Assert.True(splits.Count <= 5);
            foreach (Split split in splits)
            {
                TestUtils.Log(split);
                Assert.NotNull(split.ID);
                if (split.Created.HasValue)
                {
                    Assert.True(split.Created.Value >= after);
                    Assert.True(split.Created.Value <= before);
                }
            }
        }

        [Fact]
        public void QueryByStatus()
        {
            string[] statuses = { "created", "processing", "success", "failed" };
            
            foreach (string status in statuses)
            {
                List<Split> splits = Split.Query(limit: 3, status: status).ToList();
                Assert.True(splits.Count <= 3);
                
                foreach (Split split in splits)
                {
                    TestUtils.Log(split);
                    Assert.NotNull(split.ID);
                    Assert.Equal(status, split.Status);
                }
            }
        }

        [Fact]
        public void QueryByReceiverIds()
        {
            List<Split> allSplits = Split.Query(limit: 10).ToList();
            if (allSplits.Count > 0)
            {
                List<string> receiverIds = allSplits.Take(3).Select(s => s.ReceiverId).ToList();
                
                List<Split> filteredSplits = Split.Query(
                    limit: 10,
                    receiverIds: receiverIds
                ).ToList();
                
                foreach (Split split in filteredSplits)
                {
                    TestUtils.Log(split);
                    Assert.Contains(split.ReceiverId, receiverIds);
                }
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<Split> page;
            string cursor = null;
            
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = Split.Page(limit: 5, cursor: cursor);
                foreach (Split entity in page)
                {
                    TestUtils.Log(entity);
                    Assert.DoesNotContain(entity.ID, ids);
                    ids.Add(entity.ID);
                    Assert.NotNull(entity.ReceiverId);
                    Assert.True(entity.Amount > 0);
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
            List<Split> page;
            string cursor = null;
            DateTime after = DateTime.Today.AddDays(-30);
            
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = Split.Page(
                    limit: 3,
                    cursor: cursor,
                    after: after,
                    status: "success"
                );
                
                foreach (Split entity in page)
                {
                    TestUtils.Log(entity);
                    Assert.DoesNotContain(entity.ID, ids);
                    ids.Add(entity.ID);
                    Assert.Equal("success", entity.Status);
                    if (entity.Created.HasValue)
                    {
                        Assert.True(entity.Created.Value >= after);
                    }
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
            List<Split> splits = Split.Query(limit: 1).ToList();
            if (splits.Count > 0)
            {
                Split split = splits.First();
                Split getSplit = Split.Get(id: split.ID);
                
                Assert.Equal(split.ID, getSplit.ID);
                Assert.Equal(split.Amount, getSplit.Amount);
                Assert.Equal(split.ReceiverId, getSplit.ReceiverId);
                Assert.Equal(split.Status, getSplit.Status);
                Assert.Equal(split.Source, getSplit.Source);
                Assert.Equal(split.ExternalId, getSplit.ExternalId);
                
                if (split.Tags != null && getSplit.Tags != null)
                {
                    Assert.Equal(split.Tags.Count, getSplit.Tags.Count);
                }
                
                TestUtils.Log(getSplit);
            }
        }

        [Fact]
        public void QueryByIds()
        {
            List<Split> allSplits = Split.Query(limit: 5).ToList();
            if (allSplits.Count > 0)
            {
                List<string> splitIds = allSplits.Take(2).Select(s => s.ID).ToList();
                
                List<Split> queriedSplits = Split.Query(ids: splitIds).ToList();
                
                Assert.True(queriedSplits.Count <= splitIds.Count);
                foreach (Split split in queriedSplits)
                {
                    TestUtils.Log(split);
                    Assert.Contains(split.ID, splitIds);
                }
            }
        }

        [Fact]
        public void ValidateProperties()
        {
            List<Split> splits = Split.Query(limit: 5).ToList();
            foreach (Split split in splits)
            {
                TestUtils.Log(split);
                
                // Required properties
                Assert.NotNull(split.ID);
                Assert.NotNull(split.ReceiverId);
                Assert.True(split.Amount >= 0);
                
                // Optional properties validation
                if (split.ExternalId != null)
                {
                    Assert.True(split.ExternalId.Length > 0);
                }
                
                if (split.Status != null)
                {
                    string[] validStatuses = { "created", "processing", "success", "failed" };
                    Assert.Contains(split.Status, validStatuses);
                }
                
                if (split.Source != null)
                {
                    Assert.True(split.Source.Length > 0);
                }
                
                if (split.Tags != null)
                {
                    Assert.True(split.Tags.Count >= 0);
                }
                
                // Date validation
                if (split.Created.HasValue)
                {
                    Assert.True(split.Created.Value <= DateTime.UtcNow);
                }
                
                if (split.Updated.HasValue)
                {
                    Assert.True(split.Updated.Value <= DateTime.UtcNow);
                }
                
                if (split.Scheduled.HasValue)
                {
                    // Scheduled can be in the future
                    Assert.True(split.Scheduled.Value > DateTime.MinValue);
                }
            }
        }
    }

    public class SplitLogTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void QueryAndGet()
        {
            List<Split.Log> logs = Split.Log.Query(limit: 10).ToList();
            Assert.True(logs.Count <= 10);
            
            if (logs.Count > 1)
            {
                Assert.True(logs.First().ID != logs.Last().ID);
            }
            
            foreach (Split.Log log in logs)
            {
                TestUtils.Log(log);
                Assert.NotNull(log.ID);
                Assert.NotNull(log.Type);
                Assert.True(log.Created <= DateTime.UtcNow);
                Assert.NotNull(log.Split);
                
                Split.Log getLog = Split.Log.Get(id: log.ID);
                Assert.Equal(getLog.ID, log.ID);
                Assert.Equal(getLog.Type, log.Type);
                TestUtils.Log(getLog);
            }
        }

        [Fact]
        public void QueryByTypes()
        {
            string[] logTypes = { "created", "processing", "success", "failed" };
            
            foreach (string logType in logTypes)
            {
                List<Split.Log> logs = Split.Log.Query(
                    limit: 5,
                    types: new List<string> { logType }
                ).ToList();
                
                Assert.True(logs.Count <= 5);
                foreach (Split.Log log in logs)
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
            
            List<Split.Log> logs = Split.Log.Query(
                limit: 5,
                after: after,
                before: before
            ).ToList();
            
            Assert.True(logs.Count <= 5);
            foreach (Split.Log log in logs)
            {
                TestUtils.Log(log);
                Assert.True(log.Created >= after);
                Assert.True(log.Created <= before);
            }
        }

        [Fact]
        public void QueryBySplitIds()
        {
            List<Split> splits = Split.Query(limit: 3).ToList();
            if (splits.Count > 0)
            {
                List<string> splitIds = splits.Select(s => s.ID).ToList();
                
                List<Split.Log> logs = Split.Log.Query(
                    limit: 10,
                    splitIds: splitIds
                ).ToList();
                
                foreach (Split.Log log in logs)
                {
                    TestUtils.Log(log);
                    Assert.Contains(log.Split.ID, splitIds);
                }
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<Split.Log> page;
            string cursor = null;
            
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = Split.Log.Page(limit: 5, cursor: cursor);
                foreach (Split.Log entity in page)
                {
                    TestUtils.Log(entity);
                    Assert.DoesNotContain(entity.ID, ids);
                    ids.Add(entity.ID);
                    Assert.NotNull(entity.Type);
                    Assert.NotNull(entity.Split);
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
            List<Split.Log> page;
            string cursor = null;
            DateTime after = DateTime.Today.AddDays(-30);
            
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = Split.Log.Page(
                    limit: 3,
                    cursor: cursor,
                    after: after,
                    types: new List<string> { "success", "failed" }
                );
                
                foreach (Split.Log entity in page)
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
            List<Split.Log> logs = Split.Log.Query(limit: 5).ToList();
            foreach (Split.Log log in logs)
            {
                TestUtils.Log(log);
                
                // Required properties
                Assert.NotNull(log.ID);
                Assert.NotNull(log.Type);
                Assert.True(log.Created <= DateTime.UtcNow);
                Assert.NotNull(log.Split);
                
                // Validate nested Split object
                Assert.NotNull(log.Split.ID);
                Assert.NotNull(log.Split.ReceiverId);
                Assert.True(log.Split.Amount >= 0);
                
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
                string[] validTypes = { "created", "processing", "success", "failed" };
                Assert.Contains(log.Type, validTypes);
            }
        }

        [Fact]
        public void GetValidation()
        {
            List<Split.Log> logs = Split.Log.Query(limit: 1).ToList();
            if (logs.Count > 0)
            {
                Split.Log log = logs.First();
                Split.Log getLog = Split.Log.Get(id: log.ID);
                
                Assert.Equal(log.ID, getLog.ID);
                Assert.Equal(log.Type, getLog.Type);
                Assert.Equal(log.Created, getLog.Created);
                Assert.Equal(log.Split.ID, getLog.Split.ID);
                
                if (log.Errors != null && getLog.Errors != null)
                {
                    Assert.Equal(log.Errors.Count, getLog.Errors.Count);
                }
                
                TestUtils.Log(getLog);
            }
        }
    }
}