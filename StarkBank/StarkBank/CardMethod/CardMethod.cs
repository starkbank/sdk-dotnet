using System;
using System.Linq;
using StarkBank.Utils;
using System.Collections.Generic;

namespace StarkBank
{
    /// <summary>
    /// CardMethod object
    /// <br/>
    /// CardMethod's codes are used to define methods filters in CorporateRules.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Code [string]: method's code. Options: "chip", "token", "server", "manual", "magstripe", "contactless"</item>
    ///     <item>Name [string]: method's name. ex: "token"</item>
    ///     <item>Number [string]: method's number. ex: "81"</item>
    /// </list>
    /// </summary>
    public partial class CardMethod : StarkCore.Utils.SubResource
    {
        public string Code { get; }
        public string Name { get; }
        public string Number { get; }

        /// <summary>
        /// CardMethod object
        /// <br/>
        /// CardMethod's codes are used to define methods filters in CorporateRules.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>code [string]: method's code. Options: "chip", "token", "server", "manual", "magstripe", "contactless"</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>name [string]: method's name. ex: "token"</item>
        ///     <item>number [string]: method's number. ex: "81"</item>
        /// </list>
        /// </summary>
        public CardMethod(string code, string name = null, string number = null)
        {
            Code = code;
            Name = name;
            Number = number;
        }
        
        /// <summary>
        /// Retrieve CardMethod Objects
        /// <br/>
        /// Receive an IEnumerable of CardMethod objects in the Stark Bank API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>search [string, default null]: keyword to search for code, name or number</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of CardMethod objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<CardMethod> Query(string search = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "search" , search }
                },
                user: user
            ).Cast<CardMethod>();
        }

        public static List<CardMethod> ParseMethods(dynamic json)
        {
            if (json is null) return null;

            List<CardMethod> methods = new List<CardMethod>();

            foreach (dynamic method in json)
            {
                methods.Add(CardMethod.ResourceMaker(method));
            }
            return methods;
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "CardMethod", resourceMaker: ResourceMaker);
        }

        internal static StarkCore.Utils.SubResource ResourceMaker(dynamic json)
        {
            string code = json.code;
            string name = json.name;
            string number = json.number;

            return new CardMethod(code: code, name: name, number: number);
        }
    }
}
