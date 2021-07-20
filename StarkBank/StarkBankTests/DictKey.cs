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
    }
}
