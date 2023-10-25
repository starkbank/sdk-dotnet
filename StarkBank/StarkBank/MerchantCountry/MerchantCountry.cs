using System;
using StarkCore;
using System.Linq;
using StarkCore.Utils;
using System.Collections.Generic;

namespace StarkBank
{
    /// <summary>
    /// MerchantCountry object
    /// <br/>
    /// MerchantCountry's codes are used to define country filters in CorporateRules.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Code [string]: country's code. ex: "BRA"</item>
    ///     <item>Name [string]: country's name. ex: "Brazil"</item>
    ///     <item>Number [string]: country's number. ex: "076"</item>
    ///     <item>ShortCode [string]: country's short code. ex: "BR"</item>
    /// </list>
    /// </summary>
    public partial class MerchantCountry : SubResource
    {
        public string Code { get; }
        public string Name { get; }
        public string Number { get; }
        public string ShortCode { get; }

        /// <summary>
        /// MerchantCountry object
        /// <br/>
        /// MerchantCountry's codes are used to define country filters in CorporateRules.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>code [string]: country's code. ex: "BRA"</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>name [string]: country's name. ex: "Brazil"</item>
        ///     <item>number [string]: country's number. ex: "076"</item>
        ///     <item>shortCode [string]: country's short code. ex: "BR"</item>
        /// </list>
        /// </summary>
        public MerchantCountry(string code, string name = null, string number = null, string shortCode = null)
        {
            Code = code;
            Name = name;
            Number = number;
            ShortCode = shortCode;
        }

        /// <summary>
        /// Retrieve MerchantCountries objects
        /// <br/>
        /// Receive an IEnumerable of MerchantCountry objects available in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>search [string, default null]: keyword to search for code, name, number or shortCode</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of MerchantCountry objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<MerchantCountry> Query(string search = null, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "search" , search }
                },
                user: user
            ).Cast<MerchantCountry>();
        }

        public static List<MerchantCountry> ParseCountries(dynamic json)
        {
            if (json is null) return null;

            List<MerchantCountry> countries = new List<MerchantCountry>();

            foreach (dynamic country in json)
            {
                countries.Add(MerchantCountry.ResourceMaker(country));
            }
            return countries;
        }

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "MerchantCountry", resourceMaker: ResourceMaker);
        }

        internal static SubResource ResourceMaker(dynamic json)
        {
            string code = json.code;
            string name = json.name;
            string number = json.number;
            string shortCode = json.shortCode;

            return new MerchantCountry(code: code, name: name, number: number, shortCode: shortCode);
        }
    }
}
