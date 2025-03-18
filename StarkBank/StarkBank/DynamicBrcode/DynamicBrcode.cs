using System;
using System.Linq;
using StarkBank.Utils;
using System.Collections.Generic;

namespace StarkBank
{
    /// <summary>
    /// DynamicBrcode object
    /// <br/>
    /// When you initialize an DynamicBrcode, the entity will not be automatically
    /// sent to the Stark Bank API. The 'create' function sends the objects
    /// to the Stark Bank API and returns the list of created objects.
    /// <br/>
    /// DynamicBrcodes are conciliated BR Codes that can be used to receive Pix transactions in a convenient way.
    /// When a DynamicBrcode is paid, a Deposit is created with the tags parameter containing the character “dynamic-brcode/” followed by the DynamicBrcode’s uuid "dynamic-brcode/{uuid}" for conciliation.
    /// Additionally, all tags passed on the DynamicBrcode will be transferred to the respective Deposit resource.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Id [string]: id returned on creation, this is the BR code. ex: "00020126360014br.gov.bcb.pix0114+552840092118152040000530398654040.095802BR5915Jamie Lannister6009Sao Paulo620705038566304FC6C"</item>
    ///     <item>Amount [integer]: DynamicBrcode value in cents. Minimum = 0 (any value will be accepted). ex: 1234 (= R$ 12.34)</item>
    ///     <item>Expiration [integer, default 3600 (1 hour)]: time interval in seconds between due date and expiration date. ex 123456789</item>
    ///     <item>Tags [list of strings, default []]: list of strings for tagging, these will be passed to the respective Deposit resource when paid</item>
    ///     <item>DisplayDescription [string, default null]: optional description to be shown in the receiver bank interface. ex: "Payment for service #1234"</item>
    ///     <item>Rules [List of Rules, default []]: list of dynamic brcode rules to be applied to this brcode. ex: new List<DynamicBrCode.Rule>(){new DynamicBrCode.Rule(Key: "allowedTaxIds", Value: new List<string>(){"012.345.678-90", "20.018.183/0001-80"})}</item>
    ///     <item>Uuid [string]: unique uuid returned when the DynamicBrcode is created. ex: "4e2eab725ddd495f9c98ffd97440702d"</item>
    ///     <item>PictureUrl [string]: public QR Code (png image) URL. "https://sandbox.api.starkbank.com/v2/dynamic-brcode/d3ebb1bd92024df1ab6e5a353ee799a4.png"</item>
    ///     <item>Created [DateTime]: creation datetime for the DynamicBrcode. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime]: latest update datetime for the DynamicBrcode. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class DynamicBrcode : Resource
    {
        public long Amount { get; }
        public long? Expiration { get; }
        public List<string> Tags { get; }
        public string Uuid { get; }
        public string PictureUrl { get; }
        public string DisplayDescription { get; }
        public List<Rule> Rules { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// DynamicBrcode object
        /// <br/>
        /// When you initialize an DynamicBrcode, the entity will not be automatically
        /// sent to the Stark Bank API. The 'create' function sends the objects
        /// to the Stark Bank API and returns the list of created objects.
        /// <br/>
        /// DynamicBrcodes are conciliated BR Codes that can be used to receive Pix transactions in a convenient way.
        /// When a DynamicBrcode is paid, a Deposit is created with the tags parameter containing the character “dynamic-brcode/” followed by the DynamicBrcode’s uuid "dynamic-brcode/{uuid}" for conciliation.
        /// Additionally, all tags passed on the DynamicBrcode will be transferred to the respective Deposit resource.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>amount [integer]: DynamicBrcode value in cents. Minimum = 0 (any value will be accepted). ex: 1234 (= R$ 12.34)</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>expiration [integer, default 3600 (1 hour)]: time interval in seconds between due date and expiration date. ex 123456789</item>
        ///     <item>tags [list of strings, default []]: list of strings for tagging, these will be passed to the respective Deposit resource when paid</item>
        ///     <item>displayDescription [string, default null]: optional description to be shown in the receiver bank interface. ex: "Payment for service #1234"</item>
        ///     <item>rules [List of Rules, default []]: list of dynamic brcode rules to be applied to this brcode. ex: new List<DynamicBrCode.Rule>(){new DynamicBrCode.Rule(Key: "allowedTaxIds", Value: new List<string>(){"012.345.678-90", "20.018.183/0001-80"})}</item>
        /// </list>
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: id returned on creation, this is the BR code. ex: "00020126360014br.gov.bcb.pix0114+552840092118152040000530398654040.095802BR5915Jamie Lannister6009Sao Paulo620705038566304FC6C"</item>
        ///     <item>uuid [string]: unique uuid returned when the DynamicBrcode is created. ex: "4e2eab725ddd495f9c98ffd97440702d"</item>
        ///     <item>pictureUrl [string]: public QR Code (png image) URL. "https://sandbox.api.starkbank.com/v2/dynamic-brcode/d3ebb1bd92024df1ab6e5a353ee799a4.png"</item>
        ///     <item>created [DateTime]: creation datetime for the DynamicBrcode. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime]: latest update datetime for the DynamicBrcode. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public DynamicBrcode(
            long amount, long? expiration = null, List<string> tags = null, string id = null, string uuid = null, 
            string displayDescription = null, List<Rule> rules = null, string pictureUrl = null, 
            DateTime? created = null, DateTime? updated = null
        ) : base(id)
        {
            Amount = amount;
            Expiration = expiration;
            Tags = tags;
            Uuid = uuid;
            PictureUrl = pictureUrl;
            DisplayDescription = displayDescription;
            Rules = rules;
            Created = created;
            Updated = updated;
        }

        /// <summary>
        /// Create DynamicBrcodes
        /// <br/>
        /// Send a list of DynamicBrcode objects for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>DynamicBrcodes [list of DynamicBrcode objects]: list of DynamicBrcode objects to be created in the API</item>
        /// <br/>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of DynamicBrcode objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<DynamicBrcode> Create(List<DynamicBrcode> DynamicBrcodes, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: DynamicBrcodes,
                user: user
            ).ToList().ConvertAll(o => (DynamicBrcode)o);
        }

        /// <summary>
        /// Create DynamicBrcodes
        /// <br/>
        /// Send a list of DynamicBrcode objects for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>DynamicBrcodes [list of Dictionaries]: list of Dictionaries representing the DynamicBrcodes to be created in the API</item>
        /// <br/>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of DynamicBrcode objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<DynamicBrcode> Create(List<Dictionary<string, object>> DynamicBrcodes, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: DynamicBrcodes,
                user: user
            ).ToList().ConvertAll(o => (DynamicBrcode)o);
        }

        /// <summary>
        /// Retrieve a specific DynamicBrcode
        /// <br/>
        /// Receive a single DynamicBrcode object previously created in the Stark Bank API by passing its uuid
        /// <br/>
        /// Parameters(required):
        /// <list>
        ///     <item>Uuid [string]: object's unique uuid. ex: "901e71f2447c43c886f58366a5432c4b"</item>
        /// </list>
        /// <br/>
        /// Parameters(optional):
        /// <list>
        ///     <item>user[Project object]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>DynamicBrcode object with updated attributes</item>
        /// </list>
        /// </summary>
        public static DynamicBrcode Get(string uuid, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: uuid,
                user: user
            ) as DynamicBrcode;
        }

        /// <summary>
        /// Retrieve DynamicBrcodes
        /// <br/>
        /// Receive an IEnumerable of DynamicBrcode objects previously created in the Stark Bank API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "created", "paid", "canceled" or "overdue"</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>uuids [list of strings, default null]: list of uuids to filter retrieved objects. ex: ["901e71f2447c43c886f58366a5432c4b", "4e2eab725ddd495f9c98ffd97440702d"]</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of DynamicBrcode objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<DynamicBrcode> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
            string status = null, List<string> tags = null, List<string> uuids = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "after", new StarkCore.Utils.StarkDate(after) },
                    { "before", new StarkCore.Utils.StarkDate(before) },
                    { "status", status },
                    { "tags", tags },
                    { "uuids", uuids }
                },
                user: user
            ).Cast<DynamicBrcode>();
        }

        /// <summary>
        /// Retrieve paged DynamicBrcodes
        /// <br/>
        /// Receive a list of up to 100 DynamicBrcode objects previously created in the Stark Bank API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "created", "paid", "canceled" or "overdue"</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>uuids [list of strings, default null]: list of uuids to filter retrieved objects. ex: ["901e71f2447c43c886f58366a5432c4b", "4e2eab725ddd495f9c98ffd97440702d"]</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of DynamicBrcode objects with updated attributes and cursor to retrieve the next page of DynamicBrcode objects</item>
        /// </list>
        /// </summary>
        public static (List<DynamicBrcode> page, string pageCursor) Page(string cursor = null, int? limit = null, DateTime? after = null,
            DateTime? before = null, string status = null, List<string> tags = null, List<string> uuids = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            (List<StarkCore.Utils.SubResource> page, string pageCursor) = Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "limit", limit },
                    { "after", new StarkCore.Utils.StarkDate(after) },
                    { "before", new StarkCore.Utils.StarkDate(before) },
                    { "status", status },
                    { "tags", tags },
                    { "uuids", uuids }
                },
                user: user
            );
            List<DynamicBrcode> DynamicBrcodes = new List<DynamicBrcode>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                DynamicBrcodes.Add(subResource as DynamicBrcode);
            }
            return (DynamicBrcodes, pageCursor);
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "DynamicBrcode", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            long amount = json.amount;
            long? expiration = json.expiration;
            List<string> tags = json.tags.ToObject<List<string>>();
            string id = json.id;
            string uuid = json.uuid;
            string pictureUrl = json.pictureUrl;
            string displayDescription = json.displayDescription;
            List<Rule> rules = ParseRule(json.rules);
            string createdString = json.created;
            string updatedString = json.updated;
            DateTime? created = StarkCore.Utils.Checks.CheckDateTime(createdString);
            DateTime? updated = StarkCore.Utils.Checks.CheckDateTime(updatedString);

            return new DynamicBrcode( 
                amount: amount, expiration: expiration, tags: tags, id: id, uuid: uuid, pictureUrl: pictureUrl, 
                displayDescription: displayDescription, rules: rules, created: created, updated: updated
            );
        }
        
        private static List<Rule> ParseRule(dynamic json)
        {
            if(json is null) return null;

            List<Rule> rules = new List<Rule>();

            foreach (dynamic rule in json)
            {
                rules.Add(Rule.ResourceMaker(rule));
            }
            return rules;
        }
    }
}
