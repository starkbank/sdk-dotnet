using System;
using System.Linq;
using StarkBank.Utils;
using System.Collections.Generic;

namespace StarkBank
{
    public partial class DynamicBrcode
    {
        /// <summary>
        /// DynamicBrcode.Rule object
        /// <br/>
        /// The DynamicBrcode.Rule object modifies the behavior of DynamicBrcode objects when passed as an argument upon their creation.
        /// <br/>
        /// Properties:
        /// <list>
        ///     <item>key [string]: Rule to be customized, describes what DynamicBrcode behavior will be altered. ex: "allowedTaxIds"</item>
        ///     <item>value [list of strings]: Value of the rule. ex: {"012.345.678-90", "45.059.493/0001-73"}</item>
        /// </list>
        /// </summary>
        public partial class Rule : StarkCore.Utils.SubResource
        {
            public string Key { get; }
            public List<string> Value { get; }

            /// <summary>
            /// DynamicBrcode.Rule object
            /// <br/>
            /// The DynamicBrcode.Rule object modifies the behavior of DynamicBrcode objects when passed as an argument upon their creation.
            /// <br/>
            /// Parameters (required):
            /// <list>
            ///     <item>key [string]: Rule to be customized, describes what DynamicBrcode behavior will be altered. ex: "allowedTaxIds"</item>
            ///     <item>value [list of string]: Value of the rule. ex: {"012.345.678", "45.059.493/0001-73"} </item>
            /// </list>
            /// </summary>
            ///
            public Rule(String key, List<string> value)
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
                List<string> value = json.value?.ToObject<List<string>>();

                return new Rule(
                    key: key,
                    value: value
                );
            }
        }
    }
}
