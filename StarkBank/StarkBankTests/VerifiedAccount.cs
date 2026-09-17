using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class VerifiedAccountTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void CreateAndCancel()
        {
            List<VerifiedAccount> accounts = VerifiedAccount.Create(new List<VerifiedAccount>() { Example() });
            VerifiedAccount account = accounts.First();
            Assert.NotNull(account.ID);
            TestUtils.Log(account);

            VerifiedAccount getAccount = VerifiedAccount.Get(account.ID);
            Assert.Equal(getAccount.ID, account.ID);

            VerifiedAccount canceledAccount = VerifiedAccount.Cancel(account.ID);
            Assert.NotNull(canceledAccount.ID);
        }

        [Fact]
        public void CreateWithPixKey()
        {
            List<VerifiedAccount> accounts = VerifiedAccount.Create(new List<VerifiedAccount>() { PixKeyExample() });
            VerifiedAccount account = accounts.First();
            Assert.NotNull(account.ID);
            TestUtils.Log(account);
        }

        [Fact]
        public void Query()
        {
            List<VerifiedAccount> accounts = VerifiedAccount.Query(limit: 5).ToList();
            foreach (VerifiedAccount account in accounts)
            {
                TestUtils.Log(account);
                Assert.NotNull(account.ID);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<VerifiedAccount> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = VerifiedAccount.Page(limit: 5, cursor: cursor);
                foreach (VerifiedAccount entity in page)
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

        // The API caps VerifiedAccount creation at three per tax id per 24h, so the
        // bank-details example needs a fresh CPF on every run. The Pix-key example
        // keeps its fixed tax id, since the key has to belong to it.
        internal static string RandomCpf()
        {
            Random random = new Random();
            List<int> digits = new List<int>();
            for (int i = 0; i < 9; i++) digits.Add(random.Next(0, 10));
            for (int round = 0; round < 2; round++)
            {
                int sum = 0;
                int weight = digits.Count + 1;
                foreach (int digit in digits) sum += digit * weight--;
                int check = sum * 10 % 11;
                digits.Add(check == 10 ? 0 : check);
            }
            return string.Format("{0}{1}{2}.{3}{4}{5}.{6}{7}{8}-{9}{10}", digits.Cast<object>().ToArray());
        }

        internal static VerifiedAccount Example()
        {
            return new VerifiedAccount(
                taxId: RandomCpf(),
                name: "Joao",
                bankCode: "18236120",
                branchCode: "0001",
                number: "10000-0",
                type: "checking",
                tags: new List<string> { "iron", "suit" }
            );
        }

        internal static VerifiedAccount PixKeyExample()
        {
            return new VerifiedAccount(
                taxId: "039.946.040-36",
                keyId: "arya.stark@starkbank.com",
                tags: new List<string> { "verified-account-test" }
            );
        }
    }
}
