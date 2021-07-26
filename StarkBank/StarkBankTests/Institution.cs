using System.Collections.Generic;
using StarkBank;
using StarkBank.Institution;
using Xunit;


namespace StarkBankTests
{
    public class InstitutionTest
    {
        public readonly User user = TestUser.SetDefaultProject();


        [Fact]
        public void Query()
        {
            List<Institution> institutions = Institution.Query(limit: 1, search: "stark");
            Assert.Single(institutions);

            institutions = Institution.Query(limit: 1, spiCodes: new List<string> { "20018183" });
            Assert.Single(institutions);

            institutions = Institution.Query(limit: 1, strCodes: new List<string> { "341" });
            Assert.Single(institutions);
        }
    }
}
