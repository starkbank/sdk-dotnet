using System;
using System.Linq;
using System.Collections.Generic;
using StarkBank.Utils;


namespace StarkBank
{
    public partial class Transfer
    {
        /// <summary>
        /// Transfer.Rule object
        /// <br/>
        /// The Transfer.Rule object modifies the behavior of Transfer objects when passed as an argument upon their creation.
        /// <br/>
        /// Properties:
        /// <list>
        ///     <item>key [string]: Rule to be customized, describes what Transfer behavior will be altered. ex: "resendingLimit"</item>
        ///     <item>value [integer]: Value of the rule. ex: 5</item>
        /// </list>
        /// </summary>
        public partial class Rule : SubResource
        {
            public string Key { get; }
            public int Value { get; }

            /// <summary>
            /// Transfer.Rule object
            /// <br/>
            /// The Transfer.Rule object modifies the behavior of Transfer objects when passed as an argument upon their creation.
            /// <br/>
            /// Parameters (required):
            /// <list>
            ///     <item>key [string]: Rule to be customized, describes what Transfer behavior will be altered. ex: "resendingLimit"</item>
            ///     <item>value [integer]: Value of the rule. ex: 5</item>
            /// </list>
            /// </summary>
            ///
            public Rule(String key, int value)
            {
                Key = key;
                Value = value;
            }

            internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
            {
                return (resourceName: "Rule", resourceMaker: ResourceMaker);
            }

            internal static SubResource ResourceMaker(dynamic json)
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
