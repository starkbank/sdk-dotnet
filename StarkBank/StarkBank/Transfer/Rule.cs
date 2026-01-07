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
        ///     <item>value [integer or boolean]: Value of the rule. ex: 5</item>
        /// </list>
        /// </summary>
        public partial class Rule : StarkCore.Utils.SubResource
        {
            public string Key { get; }
            public object Value { get; }

            /// <summary>
            /// Transfer.Rule object
            /// <br/>
            /// The Transfer.Rule object modifies the behavior of Transfer objects when passed as an argument upon their creation.
            /// <br/>
            /// Parameters (required):
            /// <list>
            ///     <item>key [string]: Rule to be customized, describes what Transfer behavior will be altered. ex: "resendingLimit"</item>
            ///     <item>value [integer or boolean]: Value of the rule. ex: 5</item>
            /// </list>
            /// </summary>
            ///
            public Rule(String key, object value)
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
                object value = ParseRules(json.value);

                return new Rule(
                    key: key, value: value
                );
            }

            internal static object ParseRules(object jsonValue)
            {
                var value = Newtonsoft.Json.Linq.JToken.FromObject(jsonValue);
                object parsedValue;
                switch (value.Type)
                {
                    case Newtonsoft.Json.Linq.JTokenType.Integer:
                        parsedValue = value.ToObject<int>();
                        break;
                    case Newtonsoft.Json.Linq.JTokenType.Boolean:
                        parsedValue = value.ToObject<bool>();
                        break;
                    default:
                        parsedValue = null;
                        break;
                }

                return parsedValue;
            }
        }
    }
}
