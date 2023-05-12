using System;
using StarkBank;
using Xunit;
using System.Collections.Generic;
using System.Linq;


namespace StarkBankTests
{
    public class CorporateHolderTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Query()
        {
            List<CorporateHolder> holders = CorporateHolder.Query(limit: 3, status: "active").ToList();
            Assert.True(holders.Count <= 3);
            Assert.True(holders.First().ID != holders.Last().ID);
            foreach (CorporateHolder holder in holders)
            {
                TestUtils.Log(holder);
                Assert.NotNull(holder.ID);
                Assert.Equal("active", holder.Status);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<CorporateHolder> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = CorporateHolder.Page(limit: 2, cursor: cursor);
                foreach (CorporateHolder entity in page)
                {
                    TestUtils.Log(entity);
                    Assert.DoesNotContain(entity.ID, ids);
                    ids.Add(entity.ID);
                }
                if (cursor == null)
                {
                    break;
                }
            }
            Assert.True(ids.Count == 4);
        }

        [Fact]
        public void Update()
        {
            List<CorporateHolder> holders = CorporateHolder.Query(limit: 2, status: "active").ToList();
            Assert.Equal(2, holders.Count);
            Assert.True(holders.First().ID != holders.Last().ID);
            Dictionary<string, object> patchData = new Dictionary<string, object> {
                { "status", "blocked" },
                { "rules", new List<StarkBank.CorporateRule>{
                        new StarkBank.CorporateRule(
                            name: "Patch Rule",
                            amount: 989898,
                            interval: "day",
                            currencyCode: "USD"
                        )
                    }
                },
                { "permissions", new StarkBank.CorporateHolder.Permission(Environment.GetEnvironmentVariable("SANDBOX_ID"), "project") }
            };
            foreach (CorporateHolder holder in holders)
            {
                TestUtils.Log(holder);
                Assert.NotNull(holder.ID);
                Assert.Equal("active", holder.Status);
                CorporateHolder updatedHolder = CorporateHolder.Update(id: holder.ID, patchData: patchData);
                TestUtils.Log(updatedHolder);
                Assert.Equal("blocked", updatedHolder.Status);
            }
        }

        [Fact]
        public void CreateGetAndCancel()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"expand", new List<string> {"rules"}}
            };
            List<CorporateHolder> holders = CorporateHolder.Create(new List<CorporateHolder>() { Example() }, parameters: parameters);
            CorporateHolder holder = holders.First();
            TestUtils.Log(holder);
            Assert.NotNull(holder.ID);
            CorporateHolder getHolder = CorporateHolder.Get(id: holder.ID, parameters: parameters);
            Assert.Equal(getHolder.ID, holder.ID);
            CorporateHolder canceledgHolder = CorporateHolder.Cancel(id: holder.ID);
            Assert.Equal(canceledgHolder.ID, holder.ID);
            Assert.Equal("canceled", canceledgHolder.Status);
            TestUtils.Log(holder);
        }

        internal static CorporateHolder Example()
        {
            Random random = new Random();
            return new CorporateHolder(
                name: "Iron Bank S.A." + random.Next(100, 100000).ToString("D11"),
                tags: new List<string> { "Traveler Employee" },
                permissions: new List<StarkBank.CorporateHolder.Permission> {
                    new StarkBank.CorporateHolder.Permission(
                        ownerId: Environment.GetEnvironmentVariable("SANDBOX_ID"),
                        ownerType: "project"
                    )
                    ),
                },
                rules: new List<CorporateRule> {
                    new CorporateRule(
                        name: "rule_name",
                        amount: 10,
                        purposes: new List<string> {"purchase"}
                    ),
                }
            );
        }
    }
}
