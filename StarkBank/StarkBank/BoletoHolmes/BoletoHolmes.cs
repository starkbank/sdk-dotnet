﻿using System;
using System.Linq;
using StarkBank.Utils;
using System.Collections.Generic;

namespace StarkBank
{
    /// <summary>
    /// BoletoHolmes object
    /// <br/>
    /// When you initialize a BoletoHolmes, the entity will not be automatically
    /// created in the Stark Bank API. The 'create' function sends the objects
    /// to the Stark Bank API and returns the list of created objects.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>BoletoID [string]: Investigated boleto entity ID. ex: "5656565656565656"</item>
    ///     <item>Tags [list of strings, default null]: list of strings for tagging</item>
    ///     <item>ID [string]: unique id returned when holmes is created. ex: "5656565656565656"</item>
    ///     <item>Status [string]: current holmes status. ex: "solving" or "solved"</item>
    ///     <item>Result [string]: Result of boleto status investigation. ex: "paid" or "canceled"</item>
    ///     <item>Created [DateTime]: creation datetime for the holmes. ex: DateTime.new(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime]: latest update datetime for the holmes. ex: DateTime.new(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class BoletoHolmes : Resource
    {
        public string BoletoID { get; }
        public List<string> Tags { get; }
        public string Status { get; }
        public string Result { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// BoletoHolmes object
        /// <br/>
        /// When you initialize a BoletoHolmes, the entity will not be automatically
        /// created in the Stark Bank API. The 'create' function sends the objects
        /// to the Stark Bank API and returns the list of created objects.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>boletoID [string]: Investigated boleto entity ID. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>tags [list of strings, default null]: list of strings for tagging</item>
        /// </list>
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when holmes is created. ex: "5656565656565656"</item>
        ///     <item>status [string]: current holmes status. ex: "solving" or "solved"</item>
        ///     <item>result [string]: Result of boleto status investigation. ex: "paid" or "canceled"</item>
        ///     <item>created [DateTime]: creation datetime for the holmes. ex: DateTime.new(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime]: latest update datetime for the holmes. ex: DateTime.new(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public BoletoHolmes(string boletoID, string id = null, List<string> tags = null, string status = null, string result = null,
            DateTime? created = null, DateTime? updated = null) : base(id)
        {
            BoletoID = boletoID;
            Tags = tags;
            Status = status;
            Result = result;
            Created = created;
            Updated = updated;
        }

        /// <summary>
        /// Create BoletoHolmes
        /// <br/>
        /// Send a list of BoletoHolmes objects for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>holmes [list of BoletoHolmes objects]: list of BoletoHolmes objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of BoletoHolmes objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<BoletoHolmes> Create(List<BoletoHolmes> holmes, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: holmes,
                user: user
            ).ToList().ConvertAll(o => (BoletoHolmes)o);
        }

        /// <summary>
        /// Create BoletoHolmes
        /// <br/>
        /// Send a list of BoletoHolmes objects for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>holmes [list of Dictionaries]: list of Dictionaries representing the BoletoHolmes to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of BoletoHolmes objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<BoletoHolmes> Create(List<Dictionary<string, object>> holmes, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: holmes,
                user: user
            ).ToList().ConvertAll(o => (BoletoHolmes)o);
        }

        /// <summary>
        /// Retrieve a specific BoletoHolmes
        /// <br/>
        /// Receive a single BoletoHolmes object previously created by the Stark Bank API by passing its id
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: object unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>BoletoHolmes object with updated attributes</item>
        /// </list>
        /// </summary>
        public static BoletoHolmes Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as BoletoHolmes;
        }

        /// <summary>
        /// Retrieve BoletoHolmes
        /// <br/>
        /// Receive an IEnumerable of BoletoHolmes objects previously created in the Stark Bank API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime.new(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime.new(2020, 3, 10)</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>ids [list of strings, default null]: list of strings to get specific entities by ids. ex: ["12376517623", "1928367198236"]</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "solved"</item>
        ///     <item>boletoID [string, default null]: filter for holmes that investigate a specific boleto by its ID. ex: "5656565656565656"</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of BoletoHolmes objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<BoletoHolmes> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
            List<string> tags = null, List<string> ids = null, string status = null, string boletoID = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "after", new StarkCore.Utils.StarkDate(after) },
                    { "before", new StarkCore.Utils.StarkDate(before) },
                    { "tags", tags },
                    { "ids", ids},
                    { "status", status },
                    { "boletoID", boletoID }
                },
                user: user
            ).Cast<BoletoHolmes>();
        }

        /// <summary>
        /// Retrieve paged BoletoHolmes
        /// <br/>
        /// Receive a list of up to 100 BoletoHolmes objects previously created in the Stark Bank API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime.new(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime.new(2020, 3, 10)</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>ids [list of strings, default null]: list of strings to get specific entities by ids. ex: ["12376517623", "1928367198236"]</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "solved"</item>
        ///     <item>boletoID [string, default null]: filter for holmes that investigate a specific boleto by its ID. ex: "5656565656565656"</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of BoletoHolmes objects with updated attributes and cursor to retrieve the next page of BoletoHolmes objects</item>
        /// </list>
        /// </summary>
        public static (List<BoletoHolmes> page, string pageCursor) Page(string cursor = null, int? limit = null, DateTime? after = null,
            DateTime? before = null, List<string> tags = null, List<string> ids = null, string status = null, string boletoID = null, User user = null)
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
                    { "tags", tags },
                    { "ids", ids},
                    { "status", status },
                    { "boletoID", boletoID }
                },
                user: user
            );
            List<BoletoHolmes> boletoHolmes = new List<BoletoHolmes>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                boletoHolmes.Add(subResource as BoletoHolmes);
            }
            return (boletoHolmes, pageCursor);
        }


        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "BoletoHolmes", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;

            string boletoID = json.boletoId;
            List<string> tags = json.tags.ToObject<List<string>>();
            string status = json.status;
            string result = json.result;
            string createdString = json.created;
            DateTime? created = StarkCore.Utils.Checks.CheckDateTime(createdString);
            string updatedString = json.updated;
            DateTime? updated = StarkCore.Utils.Checks.CheckDateTime(updatedString);

            return new BoletoHolmes(
                id: id, boletoID: boletoID, tags: tags, status: status, result: result, created: created, updated: updated
            );
        }
    }
}
