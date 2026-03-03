using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class SplitProfileTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Query()
        {
            List<SplitProfile> splitProfiles = SplitProfile.Query(
                limit: 1
            ).ToList();
            Assert.Equal(1, splitProfiles.Count);
        }

        [Fact]
        public void QueryWithParameters()
        {
            try
            {
                List<SplitProfile> splitProfiles = SplitProfile.Query(
                    limit: 1,
                    before: DateTime.Now.Date,
                    after: DateTime.Now.Date.AddDays(-30),
                    tags: new List<string>() { "sdk-test" },
                    ids: new List<string>() { "5656565656565656", "4545454545454545" },
                    status: "created"
                ).ToList();
                Console.WriteLine(splitProfiles.Count);
            } catch (Exception e)
            {
                throw new Exception("Test failed", e);
            }
        }

        [Fact]
        public void QueryWithParametersFail()
        {
            try
            {
                List<SplitProfile> splitProfiles = SplitProfile.Query(
                    status: "invalidStatus"
                ).ToList();
            } catch (Exception e) {
                Assert.IsType<StarkCore.Error.InputErrors>(e);
            }
        }

        [Fact]
        public void Page()
        {
            List<SplitProfile> page;
            string cursor = null;

            (page, cursor) = SplitProfile.Page(
                limit: 5,
                cursor: cursor
            );

            Assert.True(cursor == null);
            Assert.True(page.Count == 1);
        }

        [Fact]
        public void Put()
        {
            List<SplitProfile> splitProfiles = SplitProfile.Put(
                new List<SplitProfile>() {
                    new SplitProfile("day", 0)
                }
            );
            Assert.NotNull(splitProfiles.First().ID);
            Assert.Equal("day", splitProfiles.First().Interval);
            Assert.Equal(0, splitProfiles.First().Delay);
        }
    }
}