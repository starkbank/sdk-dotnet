using Xunit;
using System;
using StarkBank;


namespace StarkBankTests
{
    public class KeyTest
    {
        [Fact]
        public void Create()
        {
            (string privateKey, string publicKey) = Key.Create();

            Assert.True(privateKey.Length > 0);
            Assert.True(publicKey.Length > 0);
        }

        [Fact]
        public void CreateAndSave()
        {
            (string privateKey, string publicKey) = Key.Create("keys");

            Assert.True(privateKey.Length > 0);
            Assert.True(publicKey.Length > 0);
        }
    }
}
