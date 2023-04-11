using System;
using System.Collections.Generic;
using System.Linq;
using StarkBank.Utils;


namespace StarkBank
{
    /// <summary>
    /// MerchantCategory object
    /// <br/>
    /// MerchantCategory's codes and types are used to define categories filters in CorporateRules.
    /// A MerchantCategory filter must define exactly one parameter between code and type.
    /// A type, such as "food", "services", etc. Defines an entire group of merchant codes,
    /// whereas a code only specifies a specific MCC.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Code [string, default null]: category's code. ex: "veterinaryServices", "fastFoodRestaurants"</item>
    ///     <item>Type [string, default null]: category's type. ex: "pets", "food"</item>
    ///     <item>Name [string]: category's name. ex: "Veterinary services", "Fast food restaurants"</item>
    ///     <item>Number [string]: category's number. ex: "742", "5814"</item>
    /// </list>
    /// </summary>
    public partial class MerchantCategory : SubResource
    {
        public string Code { get; }
        public string Type { get; }
        public string Name { get; }
        public string Number { get; }

        /// <summary>
        /// MerchantCategory object
        /// <br/>
        /// MerchantCategory's codes and types are used to define categories filters in CorporateRules.
        /// A MerchantCategory filter must define exactly one parameter between code and type.
        /// A type, such as "food", "services", etc. Defines an entire group of merchant codes,
        /// whereas a code only specifies a specific MCC.
        /// <br/>
        /// Parameters (conditionally required):
        /// <list>
        ///     <item>code [string, default null]: category's code. ex: "veterinaryServices", "fastFoodRestaurants"</item>
        ///     <item>type [string, default null]: category's type. ex: "pets", "food"</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>name [string]: category's name. ex: "Veterinary services", "Fast food restaurants"</item>
        ///     <item>number [string]: category's number. ex: "742", "5814"</item>
        /// </list>
        /// </summary>
        public MerchantCategory(string code = null, string type = null, string name = null, string number = null)
        {
            Code = code;
            Type = type;
            Name = name;
            Number = number;
        }
        
        /// <summary>
        /// Retrieve MerchantCategories objects
        /// <br/>
        /// Receive an IEnumerable of MerchantCategory objects available in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>search [string, default null]: keyword to search for code, type, name or number</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of MerchantCategory objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<MerchantCategory> Query(string search = null, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "search" , search }
                },
                user: user
            ).Cast<MerchantCategory>();
        }

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "MerchantCategory", resourceMaker: ResourceMaker);
        }

        public static List<MerchantCategory> ParseCategories(dynamic json)
        {
            if (json is null) return null;

            List<MerchantCategory> categories = new List<MerchantCategory>();

            foreach (dynamic category in json)
            {
                categories.Add(MerchantCategory.ResourceMaker(category));
            }
            return categories;
        }

        internal static SubResource ResourceMaker(dynamic json)
        {
            string code = json.code;
            string type = json.type;
            string name = json.name;
            string number = json.number;

            return new MerchantCategory(code: code, type: type, name: name, number: number);
        }
    }
}
