using System;
using System.Linq;
using StarkBank.Utils;
using System.Collections.Generic;

namespace StarkBank
{
    /// <summary>
    /// Invoice Pull Subscription object
    /// <br/>
    /// When you initialize an Invoice Pull Subscription, the entity will not be automatically
    /// sent to the Stark Bank API. The 'create' function sends the objects
    /// to the Stark Bank API and returns the list of created objects.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Amount [long integer]: Invoice Pull Subscription value in cents. Minimum = 0 (any value will be accepted). ex: 1234 (= R$ 12.34)</item>
    ///     <item>AmountMinLimit [long integer]: </item>
    ///     <item>BacenID [string]: </item>
    ///     <item>Brcode [string]: </item>
    ///     <item>Created [DateTime]: </item>
    ///     <item>Data [list of dictionaries]: List of dictionaries with "amount":long integer, "due":string, "fine": long integer and "interest": long integer fields. ex: new Dictionary<string,object>(){new Dictionary<string, string>{{"amount", 400000},{"due", DateTime(2020, 3, 10, 10, 30, 12, 15)}, {"fine", 2.5}, {"interest", 1.3}}</item></item>
    ///     <item>DisplayDescription [string]: </item>
    ///     <item>Due [DateTime]: </item>
    ///     <item>End [DateTime]: </item>
    ///     <item>ExternalID [string]: </item>
    ///     <item>ID [string]: unique id returned when Invoice Pull Subscription is created. ex: "5656565656565656"</item>
    ///     <item>Interval [string]: </item>
    ///     <item>Name [string]: </item>
    ///     <item>PullMode [string]: </item>
    ///     <item>PullRetryLimit [float]: </item>
    ///     <item>ReferenceCode [string]: </item>
    ///     <item>Start [DateTime]: </item>
    ///     <item>Status [string]: current Invoice Pull Subscription status. ex: "created", "active", "denied", "canceled" or "expired"</item>
    ///     <item>Tags [list of strings]: list of strings for tagging</item>
    ///     <item>TaxID [string]: </item>
    ///     <item>Type [string]: </item>
    ///     <item>Updated [DateTime]: latest update datetime for the Invoice Pull Subscription. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class InvoicePullSubscription : Resource
    {
        public long? Amount { get; }
        public long? AmountMinLimit { get; }
        public string BacenID { get; }
        public string Brcode { get; }
        public DateTime? Created { get; }
        public Dictionary<string, object> Data { get; }
        public string DisplayDescription { get; }
        public DateTime? Due { get; }
        public DateTime? End { get; }
        public string ExternalID { get; }
        public string Interval { get; }
        public string Name { get; }
        public string PullMode { get; }
        public long? PullRetryLimit { get; }
        public string ReferenceCode { get; }
        public DateTime? Start { get; }
        public string Status { get; }
        public List<string> Tags { get; }
        public string TaxID { get; }
        public string Type { get; }
        public DateTime? Updated { get; }

        public InvoicePullSubscription(long? amount = null, long? amountMinLimit = null, Dictionary<string, object> data = null, string displayDescription = null, DateTime? due = null, DateTime? end = null, string externalID = null, string id = null, string interval = null, string name = null, string pullMode = null, long? pullRetryLimit = null, string referenceCode = null, DateTime? start = null, string status = null, List<string> tags = null, string taxID = null, string type = null, DateTime? updated = null, string bacenID = null, string brcode = null, DateTime? created = null)
            : base(id)
        {
            Amount = amount;
            AmountMinLimit = amountMinLimit;
            Data = data;
            DisplayDescription = displayDescription;
            Due = due;
            End = end;
            ExternalID = externalID;
            Interval = interval;
            Name = name;
            PullMode = pullMode;
            PullRetryLimit = pullRetryLimit;
            ReferenceCode = referenceCode;
            Start = start;
            Status = status;
            Tags = tags;
            TaxID = taxID;
            Type = type;
            Updated = updated;
            BacenID = bacenID;
            Brcode = brcode;
            Created = created;
        }

        internal new Dictionary<string, object> ToJson()
        {
            Dictionary<string, object> json = base.ToJson();
            if (Due != null) json["due"] = new StarkCore.Utils.StarkDateTime(Due);
            if (End != null) json["end"] = new StarkCore.Utils.StarkDateTime(End);
            if (Start != null) json["start"] = new StarkCore.Utils.StarkDateTime(Start);
            if (Updated != null) json["updated"] = new StarkCore.Utils.StarkDateTime(Updated);
            if (Created != null) json["created"] = new StarkCore.Utils.StarkDateTime(Created);
            if (Data != null) json["data"] = Data;
            return json;
        }

        /// <summary>
        /// Create InvoicePullSubscriptions
        /// <br/>
        /// Send a list of InvoicePullSubscription objects for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>subscriptions [list of InvoicePullSubscription objects]: list of InvoicePullSubscription objects to be created in the API</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of InvoicePullSubscription objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<InvoicePullSubscription> Create(List<InvoicePullSubscription> subscriptions, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = ("InvoicePullSubscription", ResourceMaker);
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: subscriptions,
                user: user
            ).ToList().ConvertAll(o => (InvoicePullSubscription)o);
        }

        /// <summary>
        /// Retrieve a specific Invoice
        /// <br/>
        /// Receive a single Invoice object previously created in the Stark Bank API by passing its id
        /// <br/>
        /// Parameters(required):
        /// <list>
        ///     <item>id[string]: object unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters(optional):
        /// <list>
        ///     <item>user[Project object]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>Invoice object with updated attributes</item>
        /// </list>
        /// </summary>
        public static InvoicePullSubscription Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as InvoicePullSubscription;
        }

        /// <summary>
        /// Retrieve Invoices
        /// <br/>
        /// Receive an IEnumerable of Invoice objects previously created in the Stark Bank API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "created", "paid", "canceled" or "overdue"</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of Invoice objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<InvoicePullSubscription> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
            string status = null, List<string> tags = null, List<string> ids = null, User user = null)
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
                    { "ids", ids }
                },
                user: user
            ).Cast<InvoicePullSubscription>();
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "InvoicePullSubscription", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            long? amount = json.amount;
            long? amountMinLimit = json.amountMinLimit;
            string bacenID = json.bacenId;
            string brcode = json.brcode;
            string displayDescription = json.displayDescription;
            string externalID = json.externalId;
            string id = json.id;
            string interval = json.interval;
            string name = json.name;
            string pullMode = json.pullMode;
            long? pullRetryLimit = json.pullRetryLimit;
            string referenceCode = json.referenceCode;
            string status = json.status;
            List<string> tags = json.tags != null ? json.tags.ToObject<List<string>>() : null;
            string taxID = json.taxId;
            string type = json.type;
            DateTime? due = null;

            if (json.due != "")
            {
                due = StarkCore.Utils.Checks.CheckDateTime((string)json.due);
            }
            DateTime? end = StarkCore.Utils.Checks.CheckDateTime((string)json.end);
            DateTime? start = StarkCore.Utils.Checks.CheckDateTime((string)json.start);
            DateTime? updated = StarkCore.Utils.Checks.CheckDateTime((string)json.updated);
            DateTime? created = StarkCore.Utils.Checks.CheckDateTime((string)json.created);

            Dictionary<string, object> data = json.data != null ? json.data.ToObject<Dictionary<string, object>>() : null;

            return new InvoicePullSubscription(
                amount: amount,
                amountMinLimit: amountMinLimit,
                data: data,
                displayDescription: displayDescription,
                due: due,
                end: end,
                externalID: externalID,
                id: id,
                interval: interval,
                name: name,
                pullMode: pullMode,
                pullRetryLimit: pullRetryLimit,
                referenceCode: referenceCode,
                start: start,
                status: status,
                tags: tags,
                taxID: taxID,
                type: type,
                updated: updated,
                bacenID: bacenID,
                brcode: brcode,
                created: created
            );
        }
    }
}
