using System;
using StarkBank;
using Xunit;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using StarkCore;


namespace StarkBankTests
{
    public class CardMethodTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Query()
        {
            List<CardMethod> methods = CardMethod.Query(search: "token").ToList();
            foreach (CardMethod method in methods)
            {
                Assert.NotNull(method.Code);
            }
        }
    }
}
