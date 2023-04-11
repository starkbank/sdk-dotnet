using System;
using System.Collections.Generic;
using System.Linq;
using StarkBank.Utils;


namespace StarkBank
{
    /// <summary>
    /// CorporateHolder object
    /// <br/>
    /// The CorporateHolder describes a card holder that may group several cards.
    /// <br/>
    /// When you initialize a CorporateHolder, the entity will not be automatically
    /// created in the Stark Bank API. The 'create' function sends the objects
    /// to the Stark Bank API and returns the created object.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Name [string]: card holder's name. ex: "Jaime Lannister"</item>
    ///     <item>CenterID [string, default null]: target cost center ID. ex: "5656565656565656"</item>
    ///     <item>Permissions [list of Permission object, default null]: list of Permission object representing access granted to an user for a particular cardholder</item>
    ///     <item>Rules [list of CorporateRule objects, default null]: [EXPANDABLE] list of holder spending rules</item>
    ///     <item>Tags [list of strings, default null]: list of strings for tagging. ex: new List<string>{ "travel", "food" }</item>
    ///     <item>ID [string]: unique id returned when CorporateHolder is created. ex: "5656565656565656"</item>
    ///     <item>Status [string]: current CorporateHolder status. ex: "active", "blocked" or "canceled"</item>
    ///     <item>Updated [DateTime]: latest update DateTime for the CorporateHolder. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Created [DateTime]: creation DateTime for the CorporateHolder. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class CorporateHolder : Resource
    {
        public string Name { get; }
        public string CenterID { get; }
        public List<CorporateHolder.Permission> Permissions { get; }
        public List<CorporateRule> Rules { get; }
        public List<string> Tags { get; }
        public string Status { get; }
        public DateTime? Updated { get; }
        public DateTime? Created { get; }

        /// <summary>
        /// CorporateHolder object
        /// <br/>
        /// The CorporateHolder describes a card holder that may group several cards.
        /// <br/>
        /// When you initialize a CorporateHolder, the entity will not be automatically
        /// created in the Stark Bank API. The 'create' function sends the objects
        /// to the Stark Bank API and returns the created object.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>name [string]: card holder's name. ex: "Jaime Lannister</item>
        ///</list>
        /// Parameters (optional):
        /// <list>
        ///     <item>centerID [string, default null]: target cost center ID. ex: "5656565656565656"</item>
        ///     <item>permissions [list of Permission object, default null]: list of Permission object representing access granted to an user for a particular cardholder</item>
        ///     <item>rules [list of Rules, default null]: [EXPANDABLE] list of holder spending rules</item>
        ///     <item>tags [list of strings, default null]: list of strings for tagging. ex: new List<string>{ "travel", "food" }</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when CorporateHolder is created. ex: "5656565656565656"</item>
        ///     <item>status [string]: current CorporateHolder status. ex: "active", "blocked" or "canceled"</item>
        ///     <item>updated [DateTime]: latest update DateTime for the CorporateHolder. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>created [DateTime]: creation DateTime for the CorporateHolder. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public CorporateHolder(string name, string centerID = null, List<CorporateHolder.Permission> permissions = null, List<CorporateRule> rules = null, 
            List<string> tags = null, string id = null, string status = null, DateTime? updated = null, DateTime? created = null
        ) : base(id)
        {
            Name = name;
            CenterID = centerID;
            Permissions = permissions;
            Rules = rules;
            Tags = tags;
            Status = status;
            Updated = updated;
            Created = created;
        }

        /// <summary>
        /// Create CorporateHolder objects
        /// <br/>
        /// Send a list of CorporateHolder objects for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>holders [list of CorporateHolder objects]: list of CorporateHolder objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>parameters [dictionary]: dictionary of optional parameters</item>
        ///     <list>
        ///         <item>expand [list of strings, default null]: fields to expand information. ex: new List<string>{ "rules" }</item>
        ///     </list>
        ///     <item>user [Organization/Project object default null]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of CorporateHolder objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<CorporateHolder> Create(List<CorporateHolder> holders, Dictionary<string, object> parameters = null, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: holders,
                query: parameters,
                user: user
            ).ToList().ConvertAll(o => (CorporateHolder)o);
        }

        /// <summary>
        /// Create CorporateHolder objects
        /// <br/>
        /// Send a list of CorporateHolder objects for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>holders [list of Dictionaries]: list of dictionaries representing the CorporateHolder objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>parameters [dictionary]: dictionary of optional parameters</item>
        ///     <list>
        ///         <item>expand [list of strings, default null]: fields to expand information. ex: new List<string>{ "rules" }</item>
        ///     </list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of CorporateHolder objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<CorporateHolder> Create(List<Dictionary<string, object>> holders, Dictionary<string, object> parameters = null, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: holders,
                query: parameters,
                user: user
            ).ToList().ConvertAll(o => (CorporateHolder)o);
        }

        /// <summary>
        /// Retrieve a specific CorporateHolder by its id
        /// <br/>
        /// Receive a single CorporateHolder object previously created in the Stark Bank API by passing its id
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
        ///         <item>expand [list of strings, default null]: fields to expand information. ex: new List<string>{ "rules" }</item>
        ///     </list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>CorporateHolder object that corresponds to the given id.</item>
        /// </list>
        /// </summary>
        public static CorporateHolder Get(string id, Dictionary<string, object> parameters = null, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                query: parameters,
                user: user
            ) as CorporateHolder;
        }

        /// <summary>
        /// Retrieve CorporateHolder objects
        /// <br/>
        /// Receive an IEnumerable of CorporateHolder objects previously created in the Stark Bank API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "active", "blocked", "canceled".</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>expand [list of strings, default null]: fields to expand information. ex: new List<string>{ "rules" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: List<string>{ "5656565656565656", "4545454545454545"}</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of CorporateHolder objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<CorporateHolder> Query(int? limit = null, List<string> ids = null, DateTime? after = null, 
            DateTime? before = null, string status = null, List<string> tags = null,List<string> expand = null, 
            User user = null
        ) {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "ids", ids },
                    { "after", after },
                    { "before", before },
                    { "status", status },
                    { "tags", tags },
                    { "expand", expand }
                },
                user: user
            ).Cast<CorporateHolder>();
        }

        /// <summary>
        /// Retrieve paged CorporateHolder objects
        /// <br/>
        /// Receive a list of up to 100 CorporateHolder objects previously created in the Stark Bank API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35.</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "active", "blocked", "canceled"</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{"tony", "stark" }</item>
        ///     <item>expand [list of strings, default null]: fields to expand information. ex: new List<string>{"rules"}</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of CorporateHolder objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of CorporateHolder objects</item>
        /// </list>
        /// </summary>
        public static (List<CorporateHolder> page, string pageCursor) Page(string cursor = null, int? limit = null, 
            List<string> ids = null, DateTime? after = null, DateTime? before = null, string status = null, 
            List<string> tags = null, List<string> expand = null, User user = null
        ) {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            (List<SubResource> page, string pageCursor) = Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "limit", limit },
                    { "ids", ids },
                    { "after", after },
                    { "before", before },
                    { "status", status },
                    { "tags", tags },
                    { "expand", expand }
                },
                user: user
            );
            List<CorporateHolder> holders = new List<CorporateHolder>();
            foreach (SubResource subResource in page)
            {
                holders.Add(subResource as CorporateHolder);
            }
            return (holders, pageCursor);
        }

        /// <summary>
        /// Update CorporateHolder entity
        /// <br/>
        /// Update a CorporateHolder by passing its id, if it hasn't been paid yet.
        /// <br/>
        /// Parameters(required):
        /// <list>
        ///     <item>id[string]: CorporateHolder id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>centerID [string, default null]: target cost center ID. ex: "5656565656565656"</item>
        ///     <item>permissions [list of Permission object, default None]: list of Permission object representing access granted to an user for a particular cardholder.</item>
        ///     <item>status [string, default null]: You may block the CorporateHolder by passing 'blocked' in the status. ex: "blocked"</item>
        ///     <item>name [string, default null]: card holder name. ex: "Jaime Lannister"</item>
        ///     <item>tags [list of strings, default null]: list of strings for tagging</item>
        ///     <item>rules [list of dictionaries, default null]: list of dictionaries with "amount": int, "currencyCode": string, "id": string, "interval": string, "name": string pairs.</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>target CorporateHolder with updated attributes</item>
        /// </list>
        /// </summary>
        public static CorporateHolder Update(string id, Dictionary<string, object> patchData, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PatchId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                payload: patchData,
                user: user
            ) as CorporateHolder;
        }

        /// <summary>
        /// Cancel a CorporateHolder entity
        /// <br/>
        /// Cancel a CorporateHolder entity previously created in the Stark Bank API
        /// <br/>
        /// Parameters(required):
        /// <list>
        ///     <item>id[string]: CorporateHolder unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters(optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>canceled CorporateHolder object</item>
        /// </list>
        /// </summary>
        public static CorporateHolder Cancel(string id, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.DeleteId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as CorporateHolder;
        }

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "CorporateHolder", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string name = json.name;
            string centerID = json.centerId;
            List<Permission> permissions = CorporateHolder.Permission.ParsePermissions(json.permissions);
            List<CorporateRule> rules = CorporateRule.ParseRules(json.rules);
            List<string> tags = json.tags?.ToObject<List<string>>();
            string id = json.id;
            string status = json.status;
            string updatedString = json.updated;
            DateTime updated = Checks.CheckDateTime(updatedString);
            string createdString = json.created;
            DateTime created = Checks.CheckDateTime(createdString);

            return new CorporateHolder(
                name: name, centerID: centerID, permissions: permissions, rules: rules, 
                tags: tags, id: id, status: status, updated: updated, created: created             
            );
        }
    }
}
