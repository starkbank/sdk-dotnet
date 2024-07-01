using System;
using System.Linq;
using StarkBank.Utils;
using System.Collections.Generic;

namespace StarkBank
{
    /// <summary>
    /// CorporateCard object
    /// <br/>
    /// The CorporateCard object displays the information of the cards created in your Workspace.
    /// Sensitive information will only be returned when the "expand" parameter is used, to avoid security concerns.
    /// <br/>
    /// When you initialize a CorporateCard, the entity will not be automatically
    /// created in the Stark Bank API. The 'create' function sends the objects
    /// to the Stark Bank API and returns the created object.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>HolderID [string]: card holder unique id. ex: "5656565656565656"</item>
    ///     <item>ID[string]: unique id returned when CorporateCard is created. ex: "5656565656565656"</item>
    ///     <item>HolderName [string]: card holder name. ex: "Tony Stark"</item>
    ///     <item>DisplayName [string]: card displayed name. ex: "ANTHONY STARK"</item>
    ///     <item>Rules [list of CorporateRule objects]: [EXPANDABLE] list of card spending rules. ex: new List<CorporateRule>{ new CorporateRule() }</item>
    ///     <item>Tags [list of strings]: list of strings for tagging. ex: new List<string>{ "travel", "food" }</item>
    ///     <item>StreetLine1 [string, default sub-issuer street line 1]: card holder main address. ex: "Av. Paulista, 200"</item>
    ///     <item>StreetLine2 [string, default sub-issuer street line 2]: card holder address complement. ex: "Apto. 123"</item>
    ///     <item>District [string, default sub-issuer district]: card holder address district/neighbourhood. ex: "Bela Vista"</item>
    ///     <item>City [string, default sub-issuer city]: card holder address city. ex: "Rio de Janeiro"</item>
    ///     <item>StateCode [string, default sub-issuer state code]: card holder address state. ex: "GO"</item>
    ///     <item>ZipCode [string, default sub-issuer zip code]: card holder address zip code. ex: "01311-200"</item>
    ///     <item>Type [string]: card type. ex: "virtual"</item>
    ///     <item>Status [string]: current CorporateCard status. ex: "active", "blocked", "canceled" or "expired"</item>
    ///     <item>Number [string]: [EXPANDABLE] masked card number. Expand to unmask the value. ex: "123".</item>
    ///     <item>SecurityCode [string]: [EXPANDABLE] masked card verification value (cvv). Expand to unmask the value. ex: "123".</item>
    ///     <item>Expiration [DateTime]: [EXPANDABLE] masked card expiration DateTime. Expand to unmask the value. ex: DateTime(2020, 3, 10, 10, 30, 0, 0).</item>
    ///     <item>Updated [DateTime]: latest update DateTime for the CorporateCard. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Created [DateTime]: creation DateTime for the CorporateCard. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class CorporateCard : Resource
    {
        public string HolderID {  get; }
        public string HolderName { get; }
        public string DisplayName { get; }
        public List<CorporateRule> Rules { get; }
        public List<string> Tags { get; }
        public string StreetLine1 { get; }
        public string StreetLine2 { get; }
        public string District { get; }
        public string City { get; }
        public string StateCode { get; }
        public string ZipCode { get; }
        public string Type { get; }
        public string Status { get; }
        public string Number { get; }
        public string SecurityCode { get; }
        public DateTime? Expiration { get; }
        public DateTime? Updated { get;  }
        public DateTime? Created { get; }

        /// <summary>
        /// CorporateCard object
        /// <br/>
        /// The CorporateCard object displays the information of the cards created in your Workspace.
        /// Sensitive information will only be returned when the "expand" parameter is used, to avoid security concerns.
        /// <br/>
        /// When you initialize a CorporateCard, the entity will not be automatically
        /// created in the Stark Bank API. The 'create' function sends the objects
        /// to the Stark Bank API and returns the created object.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>holderID [string]: card holder unique id. ex: "5656565656565656"</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when CorporateCard is created. ex: "5656565656565656"</item>
        ///     <item>holderName [string]: card holder name. ex: "Tony Stark"</item>
        ///     <item>displayName [string]: card displayed name. ex: "ANTHONY STARK"</item>
        ///     <item>rules [list of CorporateRule objects]: [EXPANDABLE] list of card spending rules. ex: new List<CorporateRule>{ new CorporateRule() }</item>
        ///     <item>tags [list of strings]: list of strings for tagging. ex: new List<string>{ "travel", "food" }</item>
        ///     <item>streetLine1 [string, default sub-issuer street line 1]: card holder main address. ex: "Av. Paulista, 200"</item>
        ///     <item>streetLine2 [string, default sub-issuer street line 2]: card holder address complement. ex: "Apto. 123"</item>
        ///     <item>district [string, default sub-issuer district]: card holder address district / neighbourhood. ex: "Bela Vista"</item>
        ///     <item>city [string, default sub-issuer city]: card holder address city. ex: "Rio de Janeiro"</item>
        ///     <item>stateCode [string, default sub-issuer state code]: card holder address state. ex: "GO"</item>
        ///     <item>zipCode [string, default sub-issuer zip code]: card holder address zip code. ex: "01311-200"</item>
        ///     <item>type [string]: card type. ex: "virtual"</item>
        ///     <item>status [string]: current CorporateCard status. ex: “active”, “blocked”, “canceled” or “expired"</item>
        ///     <item>number [string]: [EXPANDABLE] masked card number. Expand to unmask the value. ex: "123".</item>
        ///     <item>securityCode [string]: [EXPANDABLE] masked card verification value (cvv). Expand to unmask the value. ex: "123".</item>
        ///     <item>expiration [DateTime]: [EXPANDABLE] masked card expiration DateTime. Expand to unmask the value. ex: DateTime(2020, 3, 10, 10, 30, 0, 0).</item>
        ///     <item>updated [DateTime]: latest update DateTime for the CorporateCard. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>created [DateTime]: creation DateTime for the CorporateCard. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public CorporateCard(
            string holderID, string id = null, string holderName = null, string displayName = null, List<CorporateRule> rules = null, 
            List<string> tags = null, string streetLine1 = null, string streetLine2 = null, string district = null, string city = null, 
            string stateCode = null, string zipCode = null, string type = null, string status = null, string number = null, 
            string securityCode = null, DateTime? expiration = null, DateTime? updated = null, DateTime? created = null
        ) : base(id)
        {
            HolderID = holderID;
            HolderName = holderName;
            DisplayName = displayName;
            Rules = rules;
            Tags = tags;
            StreetLine1 = streetLine1;
            StreetLine2 = streetLine2;
            District = district;
            City = city;
            StateCode = stateCode;
            ZipCode = zipCode;
            Type = type;
            Status = status;
            Number = number;
            SecurityCode = securityCode;
            Expiration = expiration;
            Updated = updated;
            Created = created;
        }

        /// <summary>
        /// Create CorporateCard
        /// <br/>
        /// Send a CorporateCard object for creation in the Stark Bank API
        /// If the CorporateCard was not used in the last purchase, this resource will return it.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>card [CorporateCard object]: CorporateCard object to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>parameters [dictionary]: dictionary of optional parameters</item>
        ///     <list>
        ///         <item>expand [list of strings, default null]: fields to expand information. ex: new List<string>{ "rules", "securityCode", "number", "expiration" }</item>
        ///     </list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>CorporateCard object with updated attributes</item>
        /// </list>
        /// </summary>
        public static CorporateCard Create(CorporateCard card, Dictionary<string, object> parameters = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();

            string path = $"{StarkCore.Utils.Api.Endpoint(resourceName)}/token";
            dynamic json = Rest.PostRaw(
                path: path,
                payload: StarkCore.Utils.Api.ApiJson(card),
                query: parameters,
                user: user
            ).Json()[StarkCore.Utils.Api.LastName(resourceName)];
            return StarkCore.Utils.Api.FromApiJson(resourceMaker, json);
        }

        /// <summary>
        /// Create CorporateCard
        /// <br/>
        /// Send a CorporateCard dictionary for creation in the Stark Bank API
        /// If the CorporateCard was not used in the last purchase, this resource will return it.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>card [Dictionary]: dictionary representing the CorporateCard object to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>parameters [dictionary]: dictionary of optional parameters</item>
        ///     <list>
        ///         <item>expand [list of strings, default null]: fields to expand information. ex: new List<string>{ "rules", "securityCode", "number", "expiration" }</item>
        ///     </list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>CorporateCard object with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<CorporateCard> Create(Dictionary<string, object> card, Dictionary<string, object> parameters = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();

            string path = $"{StarkCore.Utils.Api.Endpoint(resourceName)}/token";
            dynamic json = Rest.PostRaw(
                path: path,
                payload: StarkCore.Utils.Api.ApiJson(resourceMaker(card)),
                query: parameters,
                user: user
            ).Json()[StarkCore.Utils.Api.LastName(resourceName)];
            return StarkCore.Utils.Api.FromApiJson(resourceMaker, json);
        }

        /// <summary>
        /// Retrieve a specific CorporateCard
        /// <br/>
        /// Receive a single CorporateCard object previously created in the Stark Bank API by passing its id
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: object unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>parameters [dictionary]: dictionary of optional parameters</item>
        ///     <list>
        ///         <item>expand [list of strings, default null]: fields to expand information. ex: new List<string>{ "rules", "securityCode", "number", "expiration" }</item>
        ///     </list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>CorporateCard object that corresponds to the given id.</item>
        /// </list>
        /// </summary>
        public static CorporateCard Get(string id, Dictionary<string, object> parameters = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                query: parameters,
                user: user
            ) as CorporateCard;
        }

        /// <summary>
        /// Retrieve CorporateCard objects
        /// <br/>
        /// Receive an IEnumerable of CorporateCard objects previously created in the Stark Bank API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of string, default null]: filter for status of retrieved objects. ex: "active", "blocked", "expired" or "canceled"</item>
        ///     <item>types [list of strings, default null]: card type. ex: new List<string>{ "virtual" }</item>
        ///     <item>holderIds [list of strings, default null]: card holder IDs. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>expand [list of strings, default null]: fields to expand information. ex: new List<string>{ "rules", "securityCode", "number", "expiration" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of CorporateCard objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<CorporateCard> Query(int? limit = null, List<string> ids = null,  
            DateTime? after = null, DateTime? before = null, List<string> status = null, 
            List<string> types = null, List<string> holderIds = null, List<string> tags = null,
            List<string> expand = null, User user = null
        )
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "ids", ids },
                    { "after", after },
                    { "before", before },
                    { "status", status },
                    { "types", types },
                    { "holderIds", holderIds },
                    { "tags", tags },
                    { "expand", expand }
                },
                user: user
            ).Cast<CorporateCard>();
        }

        /// <summary>
        /// Retrieve paged CorporateCard objects
        /// <br/>
        /// Receive a list of up to 100 CorporateCard objects previously created in the Stark Bank API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35.</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of string, default null]: filter for status of retrieved objects. ex: "active", "blocked", "expired" or "canceled"</item>
        ///     <item>types [list of strings, default null]: card type. ex: new List<string>{ "virtual" }</item>
        ///     <item>holderIds [list of strings, default null]: card holder IDs. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>expand [list of strings, default null]: fields to expand information. ex: new List<string>{ "rules", "securityCode", "number", "expiration" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of CorporateCard objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of CorporateCard objects</item>
        /// </list>
        /// </summary>
        public static (List<CorporateCard> page, string pageCursor) Page(string cursor = null, 
            int? limit = null, List<string> ids = null, DateTime? after = null, 
            DateTime? before = null, List<string> status = null,List<string> types = null, 
            List<string> holderIds = null,List<string> tags = null, List<string> expand = null, 
            User user = null
        ) {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            (List<StarkCore.Utils.SubResource> page, string pageCursor) = Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "limit", limit },
                    { "after", after },
                    { "before", before },
                    { "types", types },
                    { "holderIds", holderIds },
                    { "tags", tags },
                    { "expand", expand },
                    { "ids", ids }
                },
                user: user
            );
            List<CorporateCard> cards = new List<CorporateCard>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                cards.Add(subResource as CorporateCard);
            }
            return (cards, pageCursor);
        }

        /// <summary>
        /// Update a CorporateCard entity
        /// <br/>
        /// Update a CorporateCard by passing its id.
        /// <br/>
        /// Parameters(required):
        /// <list>
        ///     <item>id[string]: CorporateCard id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>status [string, default null]: You may block the CorporateCard by passing "blocked" or activate by passing "active" in the status</item>
        ///     <item>displayName [string, default null]: card displayed name. ex: "ANTHONY EDWARD"</item>
        ///     <item>pin [string, default null]: You may unlock your physical card by passing its PIN. This is also the PIN you use to authorize a purhcase. ex: "1234"</item>
        ///     <item>rules [list of CorporateRule, default null]: list of CorporateRules with "amount": int, "currencyCode": string, "id": string, "interval": string, "name": string pairs.</item>
        ///     <item>tags [list of strings, default]: list of strings for tagging. ex: ["tony", "stark"]</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>target CorporateCard with updated attributes</item>
        /// </list>
        /// </summary>
        public static CorporateCard Update(string id, Dictionary<string, object> patchData, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PatchId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                payload: patchData,
                user: user
            ) as CorporateCard;
        }

        /// <summary>
        /// Cancel a CorporateCard entity
        /// <br/>
        /// Cancel a CorporateCard entity previously created in the Stark Bank API
        /// <br/>
        /// Parameters(required):
        /// <list>
        ///     <item>id[string]: CorporateCard unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters(optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>canceled CorporateCard object</item>
        /// </list>
        /// </summary>
        public static CorporateCard Cancel(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.DeleteId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as CorporateCard;
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "CorporateCard", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string holderID = json.holderId;
            string displayName = json.displayName;
            List<CorporateRule> rules = CorporateRule.ParseRules(json.rules);
            List<string> tags = json.tags?.ToObject<List<string>>();
            string streetLine1 = json.streetLine1;
            string streetLine2 = json.streetLine2;
            string district = json.district;
            string city = json.city;
            string stateCode = json.stateCode;
            string zipCode = json.zipCode;
            string id = json.id;
            string type = json.type;
            string status = json.status;
            string number = json.number;
            string securityCode = json.securityCode;
            string expirationString = json.expiration;
            DateTime? expiration = null;
            if (expirationString[0] != '*') expiration = StarkCore.Utils.Checks.CheckDateTime(expirationString);
            string updatedString = json.updated;
            DateTime updated = StarkCore.Utils.Checks.CheckDateTime(updatedString);
            string createdString = json.created;
            DateTime created = StarkCore.Utils.Checks.CheckDateTime(createdString);

            return new CorporateCard(
                holderID: holderID, displayName: displayName, rules: rules, tags: tags, 
                streetLine1: streetLine1, streetLine2: streetLine2, district: district, 
                city: city, stateCode: stateCode, zipCode: zipCode, id: id, 
                type: type, status: status, number: number, securityCode: securityCode, 
                expiration: expiration, updated: updated, created: created
            );
        }
    }
}
