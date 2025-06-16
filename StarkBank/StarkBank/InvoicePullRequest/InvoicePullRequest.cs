using System;
using System.Linq;
using StarkBank.Utils;
using System.Collections.Generic;

namespace StarkBank
{
    /// <summary>
    /// Invoice Pull Request object
    /// <br/>
    /// When you initialize an Invoice Pull Request, the entity will not be automatically
    /// sent to the Stark Bank API. The 'create' function sends the objects
    /// to the Stark Bank API and returns the list of created objects.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Amount [long integer]: Invoice Pull Request value in cents. Minimum = 0 (any value will be accepted). ex: 1234 (= R$ 12.34)</item>
    ///     <item>AmountMinLimit [long integer]: </item>
    ///     <item>BacenID [string]: </item>
    ///     <item>Brcode [string]: </item>
    ///     <item>Created [DateTime]: </item>
    ///     <item>Data [list of dictionaries]: List of dictionaries with "amount":long integer, "due":string, "fine": long integer and "interest": long integer fields. ex: new Dictionary<string,object>(){new Dictionary<string, string>{{"amount", 400000},{"due", DateTime(2020, 3, 10, 10, 30, 12, 15)}, {"fine", 2.5}, {"interest", 1.3}}</item></item>
    ///     <item>DisplayDescription [string]: </item>
    ///     <item>Due [DateTime]: </item>
    ///     <item>End [DateTime]: </item>
    ///     <item>ExternalID [string]: </item>
    ///     <item>ID [string]: unique id returned when Invoice Pull Request is created. ex: "5656565656565656"</item>
    ///     <item>Interval [string]: </item>
    ///     <item>Name [string]: </item>
    ///     <item>PullMode [string]: </item>
    ///     <item>PullRetryLimit [float]: </item>
    ///     <item>ReferenceCode [string]: </item>
    ///     <item>Start [DateTime]: </item>
    ///     <item>Status [string]: current Invoice Pull Request status. ex: "created", "active", "denied", "canceled" or "expired"</item>
    ///     <item>Tags [list of strings]: list of strings for tagging</item>
    ///     <item>TaxID [string]: </item>
    ///     <item>Type [string]: </item>
    ///     <item>Updated [DateTime]: latest update datetime for the Invoice Pull Request. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>

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

        /// <summary>
        /// Create InvoicePullRequests
        /// <br/>
        /// Send a list of InvoicePullRequest objects for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>requests [list of InvoicePullRequest objects]: list of InvoicePullRequest objects to be created in the API</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of InvoicePullRequest objects with updated attributes</item>
        /// </list>
        /// </summary>
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
