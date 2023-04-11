using System;
using StarkBank;
using Xunit;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;


namespace StarkBankTests
{
    public class MerchantCountryTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Query()
        {
            List<MerchantCountry> countries = MerchantCountry.Query(search: "token").ToList();
            foreach (MerchantCountry country in countries)
            {
                Assert.NotNull(country.Code);
            }
        }
    }
}
