using System;
using System.Linq;
using StarkBank.Utils;
using System.Collections.Generic;

namespace StarkBank
{
    public partial class BrcodePayment
    {
        /// <summary>
        /// BrcodePayment.Rule object
        /// <br/>
        /// The BrcodePayment.Rule object modifies the behavior of BrcodePayment objects when passed as an argument upon their creation.
        /// <br/>
        /// Properties:
        /// <list>
        ///     <item>key [string]: Rule to be customized, describes what BrcodePayment behavior will be altered. ex: "resendingLimit"</item>
        ///     <item>value [integer]: Value of the rule. ex: 5</item>
        /// </list>
        /// </summary>
        public partial class Rule : StarkCore.Utils.SubResource
        {
            public string Key { get; }
            public int Value { get; }

            /// <summary>
            /// BrcodePayment.Rule object
            /// <br/>
            /// The BrcodePayment.Rule object modifies the behavior of BrcodePayment objects when passed as an argument upon their creation.
            /// <br/>
            /// Parameters (required):
            /// <list>
            ///     <item>key [string]: Rule to be customized, describes what BrcodePayment behavior will be altered. ex: "resendingLimit"</item>
            ///     <item>value [integer]: Value of the rule. ex: 5</item>
            /// </list>
            /// </summary>
            ///
            public Rule(String key, int value)
            {
                Key = key;
                Value = value;
            }

            internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
            {
                return (resourceName: "Rule", resourceMaker: ResourceMaker);
            }

            internal static StarkCore.Utils.SubResource ResourceMaker(dynamic json)
            {
                string key = json.key;
                int value = json.value;

                return new Rule(
                    key: key, value: value
                );
            }
        }
    }
}
