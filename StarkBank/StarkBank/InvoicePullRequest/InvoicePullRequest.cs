using System;
using System.Linq;
using StarkBank.Utils;
using System.Collections.Generic;

namespace StarkBank
{
    /// Check out our API Documentation at https://starkbank.com/docs/api#invoice-pull-request

    public partial class InvoicePullRequest : Resource
    {
        public string AttemptType { get; }
        public DateTime? Created { get; }
        public DateTime? Due { get; }
        public string InvoiceID { get; }
        public string Status { get; }
        public string SubscriptionID { get; }
        public List<string> Tags { get; }

        public InvoicePullRequest(string id = null, string attemptType = null, DateTime? created = null, DateTime? due = null, string invoiceId = null, string status = null, string subscriptionId = null, List<string> tags = null)
            : base(id)
        {
            AttemptType = attemptType;
            Created = created;
            Due = due;
            InvoiceID = invoiceId;
            SubscriptionID = subscriptionId;
            Status = status;
            Tags = tags;
        }

        internal new Dictionary<string, object> ToJson()
        {
            Dictionary<string, object> json = base.ToJson();
            if (Due != null) json["due"] = new StarkCore.Utils.StarkDateTime(Due);
            if (Created != null) json["created"] = new StarkCore.Utils.StarkDateTime(Created);
            return json;
        }

        public static List<InvoicePullRequest> Create(List<InvoicePullRequest> requests, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = ("InvoicePullRequest", ResourceMaker);
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: requests,
                user: user
            ).ToList().ConvertAll(o => (InvoicePullRequest)o);
        }

        public static InvoicePullRequest Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as InvoicePullRequest;
        }

        public static InvoicePullRequest Delete(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.DeleteId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as InvoicePullRequest;
        }
        
        public static IEnumerable<InvoicePullRequest> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
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
            ).Cast<InvoicePullRequest>();
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "InvoicePullRequest", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            string attemptType = json.attemptType;
            DateTime? due = null;
            string status = json.status;
            string invoiceId = json.invoiceId;
            string subscriptionId = json.subscriptionId;
            List<string> tags = json.tags != null ? json.tags.ToObject<List<string>>() : null;

            if (json.due != "")
            {
                due = StarkCore.Utils.Checks.CheckDateTime((string)json.due);
            }
            DateTime? created = StarkCore.Utils.Checks.CheckDateTime((string)json.created);

            return new InvoicePullRequest(
                id: id,
                attemptType: attemptType,
                created: created,
                due: due,
                invoiceId: invoiceId,
                status: status,
                subscriptionId: subscriptionId,
                tags: tags
            );
        }
    }
}
