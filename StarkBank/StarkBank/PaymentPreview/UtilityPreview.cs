namespace StarkBank
{
    public partial class PaymentPreview
    {
        /// <summary>
        /// UtilityPreview object
        /// <br/>
        /// A UtilityPreview is used to get information from a Utility Payment you received before confirming the payment.
        /// <br/>
        /// Properties:
        /// <list>
        ///     <item>Amount [integer]: final amount to be paid. ex: 23456 (= R$ 234.56)</item>
        ///     <item>Name [string]: beneficiary full name. ex: "Iron Throne"</item>
        ///     <item>Description [string]: utility payment description. ex: "Utility Payment - Light Company"</item>
        ///     <item>Line [string]: Number sequence that identifies the payment. ex: "82660000002 8 44361143007 7 41190025511 7 00010601813 8"</item>
        ///     <item>BarCode [string]: Bar code number that identifies the payment. ex: "82660000002443611430074119002551100010601813"</item>        /// </list>
        /// </summary>
        public class UtilityPreview : Utils.SubResource
        {
            public long Amount { get; }
            public string Name { get; }
            public string Description { get; }
            public string Line { get; }
            public string BarCode { get; }

            /// <summary>
            /// UtilityPreview object
            /// <br/>
            /// A UtilityPreview is used to get information from a Utility Payment you received before confirming the payment.
            /// <br/>
            /// Attributes (return-only):
            /// <list>
            ///     <item>Amount [integer]: final amount to be paid. ex: 23456 (= R$ 234.56)</item>
            ///     <item>Name [string]: beneficiary full name. ex: "Iron Throne"</item>
            ///     <item>Description [string]: utility payment description. ex: "Utility Payment - Light Company"</item>
            ///     <item>Line [string]: Number sequence that identifies the payment. ex: "82660000002 8 44361143007 7 41190025511 7 00010601813 8"</item>
            ///     <item>BarCode [string]: Bar code number that identifies the payment. ex: "82660000002443611430074119002551100010601813"</item>        /// </list>
            /// </list>
            /// </summary>
            public UtilityPreview(long amount, string name, string description, string line, string barCode)
            {
                Amount = amount;
                Name = name;
                Description = description;
                Line = line;
                BarCode = barCode;
            }

            internal static (string resourceName, Utils.Api.ResourceMaker resourceMaker) SubResource()
            {
                return (resourceName: "UtilityPreview", resourceMaker: ResourceMaker);
            }

            public static Utils.SubResource ResourceMaker(dynamic json)
            {
                long amount = json.amount;
                string name = json.name;
                string description = json.description;
                string line = json.line;
                string barCode = json.barCode;

                return new UtilityPreview(
                    amount: amount, name: name, description: description, line: line, barCode: barCode
                );
            }
        }
    }
}
