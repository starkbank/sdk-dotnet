using System;
using System.Linq;
using StarkBank.Utils;
using System.Collections.Generic;

namespace StarkBank
{
    public partial class Invoice
    {
        /// <summary>
        /// Invoice.Rule object
        /// <br/>
        /// The Invoice.Rule object modifies the behavior of Invoice objects when passed as an argument upon their creation.
        /// <br/>
        /// Properties:
        /// <list>
        ///     <item>key [string]: Rule to be customized, describes what Invoice behavior will be altered. ex: "allowedTaxIds"</item>
        ///     <item>value [list of strings]: Value of the rule. ex: {"012.345.678-90", "45.059.493/0001-73"}</item>
        /// </list>
        /// </summary>
        public partial class Rule : StarkCore.Utils.SubResource
        {
            public string Key { get; }
            public object Value { get; }

            /// <summary>
            /// Invoice.Rule object
            /// <br/>
            /// The Invoice.Rule object modifies the behavior of Invoice objects when passed as an argument upon their creation.
            /// <br/>
            /// Parameters (required):
            /// <list>
            ///     <item>key [string]: Rule to be customized, describes what Invoice behavior will be altered. ex: "allowedTaxIds"</item>
            ///     <item>value [list of string]: Value of the rule. ex: {"012.345.678", "45.059.493/0001-73"} </item>
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
                    key: key,
                    value: value
                );
            }

            internal static object ParseRules(object jsonValue)
            {
                var value = Newtonsoft.Json.Linq.JToken.FromObject(jsonValue);
                object parsedValue = (value.Type == Newtonsoft.Json.Linq.JTokenType.String)
                    ? (object)value.ToString()
                    : (object)value.ToObject<List<string>>();

                return parsedValue;
            }
        }
    }
}
