using Xunit;
using StarkBank;


namespace StarkBankTests
{
    public class KeyTest
    {
        [Fact]
        public void Create()
        {
            (string privateKey, string publicKey) = Key.Create();

            System.Console.WriteLine(privateKey);
            System.Console.WriteLine(publicKey);
        }

        [Fact]
        public void CreateAndSave()
        {
            (string privateKey, string publicKey) = Key.Create("keys");

            System.Console.WriteLine(privateKey);
            System.Console.WriteLine(publicKey);
        }
    }
}
