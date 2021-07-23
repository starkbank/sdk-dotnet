using Xunit;
using StarkBank;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class DictKeyTest
    {
        public readonly User use = TestUser.SetDefaultProject();
        
        [Fact]
        public void Get()
        {
            string pixKey = "valid@sandbox.com";
            DictKey dictKey = DictKey.Get(pixKey);
            Assert.NotNull(dictKey);
            Assert.Equal(dictKey.ID, pixKey);
            TestUtils.Log(dictKey);
        }

        [Fact]
        public void Query()
        {
            List<DictKey> dictKeys = DictKey.Query(limit: 1, status: "registered", type: "evp").ToList();
            Assert.Single(dictKeys);
            foreach (DictKey dictKey in dictKeys)
            {
                TestUtils.Log(dictKey);
                Assert.NotNull(dictKey.ID);
                TestUtils.Log(dictKey);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<DictKey> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = DictKey.Page(limit: 2, cursor: cursor);
                foreach (DictKey entity in page)
                {
                    Assert.DoesNotContain(entity.ID, ids);
                    ids.Add(entity.ID);
                }
                if (cursor == null)
                {
                    break;
                }
            }
            Assert.True(ids.Count <= 4);
        }
    }
}
