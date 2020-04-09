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

            Console.WriteLine(privateKey);
            Console.WriteLine(publicKey);
        }

        [Fact]
        public void CreateAndSave()
        {
            (string privateKey, string publicKey) = Key.Create("keys");

            Console.WriteLine(privateKey);
            Console.WriteLine(publicKey);
        }
    }
}
