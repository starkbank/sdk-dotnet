using Xunit;
using StarkBank;


namespace StarkBankTests
{
    public class DictKeyTest
    {
        public readonly User use = TestUser.SetDefault();
        
        [Fact]
        public void Get()
        {
            string pixKey = "tony@starkbank.com";
            DictKey dictKey = DictKey.Get(pixKey);
            Assert.NotNull(dictKey);
            Assert.Equal(dictKey.ID, pixKey);
            TestUtils.Log(dictKey);
        }
    }
}