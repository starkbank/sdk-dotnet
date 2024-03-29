﻿using Xunit;
using System;
using StarkBank;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class TransferLogTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void QueryAndGet()
        {
            Settings.Language = "pt-BR";
            List<Transfer.Log> logs = Transfer.Log.Query(
                limit: 101,
                before: DateTime.Now.Date,
                types: new List<string> { "failed" }
            ).ToList();
            Assert.Equal(101, logs.Count);
            Assert.True(logs.First().ID != logs.Last().ID);
            foreach (Transfer.Log log in logs)
            {
                TestUtils.Log(log);
                Assert.NotNull(log.ID);
                Assert.Equal("failed", log.Type);
            }
            Transfer.Log getLog = Transfer.Log.Get(id: logs.First().ID);
            Assert.Equal(getLog.ID, logs.First().ID);
            TestUtils.Log(getLog);
        }


        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<Transfer.Log> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = Transfer.Log.Page(limit: 5, cursor: cursor);
                foreach (Transfer.Log entity in page)
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
