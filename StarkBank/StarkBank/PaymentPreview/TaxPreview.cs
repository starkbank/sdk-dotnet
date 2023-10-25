using StarkCore;
using StarkCore.Utils;

namespace StarkBank
{
    public partial class PaymentPreview
    {
        /// <summary>
        /// TaxPreview object
        /// <br/>
        /// A TaxPreview is used to get information from a Tax Payment you received before confirming the payment.
        /// <br/>
        /// Properties:
        /// <list>
        ///     <item>Amount [int]: final amount to be paid. ex: 23456 (= R$ 234.56)</item>
        ///     <item>Name [string]: beneficiary full name. ex: "Iron Throne"</item>
        ///     <item>Description [string]: tax payment description. ex: "ISS Payment - Iron Throne"</item>
        ///     <item>Line [string]: Number sequence that identifies the payment. ex: "85660000006 6 67940064007 5 41190025511 7 00010601813 8"</item>
        ///     <item>BarCode [string]: Bar code number that identifies the payment. ex: "85660000006679400640074119002551100010601813"</item>        /// </list>
        /// </summary>
        public class TaxPreview : SubResource
        {
            public long Amount { get; }
            public string Name { get; }
            public string Description { get; }
            public string Line { get; }
            public string BarCode { get; }

            /// <summary>
            /// TaxPreview object
            /// <br/>
            /// A TaxPreview is used to get information from a Tax Payment you received before confirming the payment.
            /// <br/>
            /// Attributes (return-only):
            /// <list>
            ///     <item>Amount [int]: final amount to be paid. ex: 23456 (= R$ 234.56)</item>
            ///     <item>Name [string]: beneficiary full name. ex: "Iron Throne"</item>
            ///     <item>Description [string]: tax payment description. ex: "ISS Payment - Iron Throne"</item>
            ///     <item>Line [string]: Number sequence that identifies the payment. ex: "85660000006 6 67940064007 5 41190025511 7 00010601813 8"</item>
            ///     <item>BarCode [string]: Bar code number that identifies the payment. ex: "85660000006679400640074119002551100010601813"</item>        /// </list>
            /// </list>
            /// </summary>
            public TaxPreview(long amount, string name, string description, string line, string barCode)
            {
                Amount = amount;
                Name = name;
                Description = description;
                Line = line;
                BarCode = barCode;
            }

            internal static (string resourceName, Api.ResourceMaker resourceMaker) SubResource()
            {
                return (resourceName: "TaxPreview", resourceMaker: ResourceMaker);
            }

            public static SubResource ResourceMaker(dynamic json)
            {
                long amount = json.amount;
                string name = json.name;
                string description = json.description;
                string line = json.line;
                string barCode = json.barCode;

                return new TaxPreview(
                    amount: amount, name: name, description: description, line: line, barCode: barCode
                );
            }
        }
    }
}
