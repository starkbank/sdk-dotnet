using System;
using System.Collections.Generic;
using System.Linq;

using StarkBank.Utils;

namespace StarkBank
{
    /// <summary>
    /// Split object
    /// <br/>
    /// Represents a financial split transaction, detailing the distribution of an amount to a specified receiver.
    /// </summary>
    /// <remarks>The Split resource is used to split an Invoice or Boleto between different receivers.</remarks>
    public partial class Split : Resource
    {
        public Split(long amount, string receiverId, DateTime? created = null, string externalId = null, DateTime? scheduled = null, string source = null, string status = null, List<string> tags = null, DateTime? updated = null, string id = null)
            : base(id)
        {
            Amount = amount;
            Created = created;
            ExternalId = externalId;
            ReceiverId = receiverId;
            Scheduled = scheduled;
            Source = source;
            Status = status;
            Tags = tags;
            Updated = updated;
        }

        public long Amount { get; }
        public string ReceiverId { get; }
        public string ExternalId { get; }
        public DateTime? Created { get; }
        public DateTime? Scheduled { get; }
        public string Source { get; }
        public string Status { get; }
        public List<string> Tags { get; }
        public DateTime? Updated { get; }


        /// <summary>
        /// Retrieve a specific Split
        /// <br/>
        /// Receive a single Split object previously created in the Stark Bank API by passing its id
        /// <br/>
        /// Parameters(required):
        /// <list>
        ///     <item>id[string]: object unique id. ex: "5155165527080960"</item>
        /// </list>
        /// <br/>
        /// Parameters(optional):
        /// <list>
        ///     <item>user[Project object]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>Split object with updated attributes</item>
        /// </list>
        /// </summary>
        public static Split Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as Split;
        }


        /// <summary>
        /// Retrieve Splits
        /// <br/>
        /// Receive an IEnumerable of Splits objects previously created in the Stark Bank API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter transfers by the specified status.</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5745664021495808"]</item>
        ///     <item>receiverIds [list of strings, default null]: list of receiver ids to filter retrieved objects. ex: ['5656565656565656', '4545454545454545'].</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of Split objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<Split> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
        string status = null, List<string> tags = null, List<string> ids = null, List<string> receiverIds = null, User user = null)
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
                    { "ids", ids },
                    { "receiverIds", receiverIds }
                },
                user: user
            ).Cast<Split>();
        }

        /// <summary>
        /// Retrieve paged Slits
        /// <br/>
        /// Receive a list of up to 100 Split objects previously created in the Stark Bank API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>tags [list of strings, default null]: Filter entities that contain the specified tags.</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5745664021495808"]</item>
        ///     <item>receiverIds [list of strings, default null]: list of receiver ids to filter retrieved objects. ex: ['5656565656565656', '4545454545454545'].</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of Split objects with updated attributes and cursor to retrieve the next page of Split objects</item>
        /// </list>
        /// </summary>
        public static (List<Split> page, string pageCursor) Page(string cursor = null, int? limit = null, DateTime? after = null,
            DateTime? before = null, string status = null, List<string> tags = null, List<string> ids = null, List<string> receiverIds = null, User user = null)
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
                        { "tags ", tags  },
                        { "ids ", ids  },
                        { "receiverIds ", receiverIds  }
                },
                user: user
            );
            List<Split> logs = new List<Split>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                logs.Add(subResource as Split);
            }
            return (logs, pageCursor);
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "Split", resourceMaker: ResourceMaker);
        }

        internal static StarkCore.Utils.Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            long amount = json.amount;
            string receiverId = json.receiverId;
            DateTime? created = StarkCore.Utils.Checks.CheckNullableDateTime((string)json.created);
            string externalId = json.externalId;
            DateTime? scheduled = StarkCore.Utils.Checks.CheckNullableDateTime((string)json.scheduled);
            string source = json.source;
            string status = json.status;
            List<string> tags = json.tags.ToObject<List<string>>();
            DateTime? updated = StarkCore.Utils.Checks.CheckNullableDateTime((string)json.updated);

            return new Split(
                amount: amount, receiverId: receiverId, created: created, externalId: externalId, scheduled: scheduled, source: source, status: status, tags: tags, updated: updated, id: id
            );
        }
    }
}
