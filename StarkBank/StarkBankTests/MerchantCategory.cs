using System;
using StarkBank;
using Xunit;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using StarkCore;

namespace StarkBankTests
{
    public class MerchantCategoryTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Query()
        {
            List<MerchantCategory> categories = MerchantCategory.Query(search: "token").ToList();
            foreach (MerchantCategory category in categories)
            {
                Assert.NotNull(category.Code);
            }
        }
    }
}
