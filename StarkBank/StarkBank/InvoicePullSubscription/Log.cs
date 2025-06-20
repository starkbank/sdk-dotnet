using System;
using System.Linq;
using StarkBank.Utils;
using System.Collections.Generic;

namespace StarkBank
{
    /// Check out our API Documentation at https://starkbank.com/docs/api#invoice-pull-subscription
    
    public partial class InvoicePullSubscription
    {
        public class Log : Resource
        {
            public DateTime Created { get; }
            public string Type { get; }
            public List<string> Errors { get; }
            public InvoicePullSubscription InvoicePullSubscription { get; }

            public Log(string id, DateTime created, string type, List<string> errors, InvoicePullSubscription subscription) : base(id)
            {
                Created = created;
                Type = type;
                Errors = errors;
                InvoicePullSubscription = subscription;
            }

            public static Log Get(string id, User user = null)
            {
                (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
                return Rest.GetId(
                    resourceName: resourceName,
                    resourceMaker: resourceMaker,
                    id: id,
                    user: user
                ) as Log;
            }

            public static IEnumerable<Log> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
                List<string> types = null, List<string> subscriptionIds = null, User user = null)
            {
                (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
                return Rest.GetList(
                    resourceName: resourceName,
                    resourceMaker: resourceMaker,
                    query: new Dictionary<string, object> {
                        { "limit", limit },
                        { "after", new StarkCore.Utils.StarkDate(after) },
                        { "before", new StarkCore.Utils.StarkDate(before) },
                        { "types", types },
                        { "subscriptionIds", subscriptionIds }
                    },
                    user: user
                ).Cast<Log>();
            }

            public static (List<Log> page, string pageCursor) Page(string cursor = null, int? limit = null, DateTime? after = null,
                DateTime? before = null, List<string> types = null, List<string> subscriptionIds = null, User user = null)
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
                        { "types", types },
                        { "subscriptionIds", subscriptionIds }
                    },
                    user: user
                );
                List<Log> logs = new List<Log>();
                foreach (StarkCore.Utils.SubResource subResource in page)
                {
                    logs.Add(subResource as Log);
                }
                return (logs, pageCursor);
            }

            internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
            {
                return (resourceName: "InvoicePullSubscriptionLog", resourceMaker: ResourceMaker);
            }

            internal static Resource ResourceMaker(dynamic json)
            {
                List<string> errors = json.errors.ToObject<List<string>>();
                string id = json.id;
                string createdString = json.created;
                DateTime created = StarkCore.Utils.Checks.CheckDateTime(createdString);
                string type = json.type;
                InvoicePullSubscription subscription = InvoicePullSubscription.ResourceMaker(json.subscription);

                return new Log(id: id, created: created, type: type, errors: errors, subscription: subscription);
            }
        }
    }
}
