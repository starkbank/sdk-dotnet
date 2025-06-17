using System;
using System.Linq;
using StarkBank.Utils;
using System.Collections.Generic;

namespace StarkBank
{
    /// Check out our API Documentation at https://starkbank.com/docs/api#invoice-pull-subscription
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
