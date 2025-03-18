using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class DynamicBrcodeTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void CreateAndGet()
        {
            List<DynamicBrcode> brcodes = DynamicBrcode.Create(new List<DynamicBrcode>() { Example(), Example(), Example() });
            DynamicBrcode brcode = brcodes.First();
            TestUtils.Log(brcode);
            Assert.NotNull(brcodes.First().ID);
            DynamicBrcode getDynamicBrcode = DynamicBrcode.Get(uuid: brcode.Uuid);
            Assert.Equal(getDynamicBrcode.ID, brcode.ID);
        }

        [Fact]
        public void Query()
        {
            List<DynamicBrcode> brcodes = DynamicBrcode.Query(limit: 2).ToList();
            Assert.Equal(2, brcodes.Count);
            Assert.True(brcodes.First().ID != brcodes.Last().ID);
            foreach (DynamicBrcode brcode in brcodes)
            {
                TestUtils.Log(brcode);
                Assert.NotNull(brcode.ID);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<DynamicBrcode> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = DynamicBrcode.Page(limit: 5, cursor: cursor);
                foreach (DynamicBrcode entity in page)
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

        internal static DynamicBrcode Example()
        {
            return new DynamicBrcode(
                amount: 1000000,
                displayDescription: "Payment for service #1234",
                rules: new List<DynamicBrcode.Rule>() {
                    new DynamicBrcode.Rule(
                        key: "allowedTaxIds",
                        value: new List<string> {"012.345.678-90"}
                    )
                },
                tags: new List<string> { "custom", "tags" }  
            );
        }
    }
}
