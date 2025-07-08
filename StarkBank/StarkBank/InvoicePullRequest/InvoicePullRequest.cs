using System;
using System.Linq;
using StarkBank.Utils;
using System.Collections.Generic;

namespace StarkBank
{
    /// <summary>
    /// InvoicePullRequest object
    /// <br/>
    /// When you initialize an InvoicePullRequest, the entity will not be automatically
    /// sent to the Stark Bank API. The 'create' function sends the objects
    /// to the Stark Bank API and returns the list of created objects.
    /// <br/>
    /// Properties:
    ///     <item>SubscriptionID [string]: Unique of the InvoicePullSubscription related to the invoice. ex: "5656565656565656"
    ///     <item>InvoiceID [string]: Id of the invoice previously created to be sent for payment. ex: "5656565656565656"
    ///     <item>Due [DateTime or string]: payment scheduled date in UTC ISO format. ex: "2023-10-28T17:59:26.249976+00:00"
    ///     <item>AttemptType [string, default "default"]: attempt type for the payment. Options: "default", "retry". ex: "retry"
    ///     <item>Tags [list of strings, default []]: list of strings for tagging
    ///     <item>ExternalID [string, default None]: a string that must be unique among all your InvoicePullRequests. Duplicated external_ids will cause failures. ex: "my-external-id"
    ///     <item>DisplayDescription [string, default None]: Description to be shown to the payer. ex: "Payment for services"
    ///     <item>ID [string]: unique id returned when InvoicePullRequest is created. ex: "5656565656565656"
    ///     <item>Status [string]: current InvoicePullRequest status. ex: "created", "pending", "scheduled", "success", "failed", "canceled"
    ///     <item>InstallmentID [string]: unique id of the installment related to this request. ex: "5656565656565656"
    ///     <item>Created [DateTime]: creation datetime for the InvoicePullRequest. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)
    ///     <item>Updated [DateTime]: latest update datetime for the InvoicePullRequest. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)
    /// </list>
    /// </summary>
    public partial class InvoicePullRequest : Resource
    {
        public string SubscriptionID { get; }
        public string InvoiceID { get; }
        public DateTime? Due { get; }
        public string AttemptType { get; }
        public List<string> Tags { get; }
        public string ExternalID { get; }
        public string DisplayDescription { get; }
        public string Status { get; }
        public string InstallmentID { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// InvoicePullRequest object
        /// <br/>
        /// When you initialize an InvoicePullRequest, the entity will not be automatically
        /// sent to the Stark Bank API. The 'create' function sends the objects
        /// to the Stark Bank API and returns the list of created objects.
        /// Parameters (required):
        ///     <item>SubscriptionID [string]: Unique of the InvoicePullSubscription related to the invoice. ex: "5656565656565656"
        ///     <item>InvoiceID [string]: Id of the invoice previously created to be sent for payment. ex: "5656565656565656"
        ///     <item>Due [DateTime or string]: payment scheduled date in UTC ISO format. ex: "2023-10-28T17:59:26.249976+00:00"
        /// </list>
        /// Parameters (optional):
        ///     <item>AttemptType [string, default "default"]: attempt type for the payment. Options: "default", "retry". ex: "retry"
        ///     <item>Tags [list of strings, default []]: list of strings for tagging
        ///     <item>ExternalID [string, default None]: a string that must be unique among all your InvoicePullRequests. Duplicated external_ids will cause failures. ex: "my-external-id"
        ///     <item>DisplayDescription [string, default None]: Description to be shown to the payer. ex: "Payment for services"
        /// </list>
        /// Attributes (return-only):
        ///     <item>ID [string]: unique id returned when InvoicePullRequest is created. ex: "5656565656565656"
        ///     <item>Status [string]: current InvoicePullRequest status. ex: "created", "pending", "scheduled", "success", "failed", "canceled"
        ///     <item>InstallmentID [string]: unique id of the installment related to this request. ex: "5656565656565656"
        ///     <item>Created [DateTime]: creation datetime for the InvoicePullRequest. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)
        ///     <item>Updated [DateTime]: latest update datetime for the InvoicePullRequest. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)
        /// </list>
        /// </summary>
        public InvoicePullRequest(string id = null, string subscriptionId = null, string invoiceId = null, DateTime? due = null, string attemptType = null, List<string> tags = null, string externalId = null, string displayDescription = null, string status = null, string installmentId = null, DateTime? created = null, DateTime? updated = null)
            : base(id)
        {
            SubscriptionID = subscriptionId;
            InvoiceID = invoiceId;
            Due = due;
            Created = created;
            Updated = updated;
            AttemptType = attemptType;
            Tags = tags;
            ExternalID = externalId;
            DisplayDescription = displayDescription;
            Status = status;
            InstallmentID = installmentId;
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
        /// <br/>
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
        /// Retrieve a specific InvoicePullRequest
        /// <br/>
        /// Receive a single InvoicePullRequest object previously created by the Stark Bank API by passing its id
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
        ///     <item>InvoicePullRequest object with updated attributes</item>
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
        /// Retrieve InvoicePullRequests
        /// <br/>
        /// Receive an IEnumerable of InvoicePullRequest objects previously created in the Stark Bank API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: new DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: new DateTime(2020, 3, 10)</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>ids [list of strings, default null]: list of strings to get specific entities by ids. ex: ["12376517623", "1928367198236"]</item>
        ///     <item>invoiceIds [list of strings, default null]: list of strings to get specific entities by invoice ids. ex: ["12376517623", "1928367198236"]</item>
        ///     <item>subscriptionIds [list of strings, default null]: list of strings to get specific entities by subscription ids. ex: ["12376517623", "1928367198236"]</item>
        ///     <item>externalIds [list of strings, default null]: list of strings to get specific entities by external ids. ex: ["my-external-id-1", "my-external-id-2"]</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "success" or "failed"</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of InvoicePullRequest objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<InvoicePullRequest> Query(int? limit = null, DateTime? after = null, DateTime? before = null, string status = null, List<string> tags = null, List<string> ids = null, User user = null)
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

        /// <summary>
        /// Retrieve paged InvoicePullRequests
        /// <br/>
        /// Receive a list of up to 100 InvoicePullRequest objects previously created in the Stark Bank API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: new DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: new DateTime(2020, 3, 10)</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>ids [list of strings, default null]: list of strings to get specific entities by ids. ex: ["12376517623", "1928367198236"]</item>
        ///     <item>invoiceIds [list of strings, default null]: list of strings to get specific entities by invoice ids. ex: ["12376517623", "1928367198236"]</item>
        ///     <item>subscriptionIds [list of strings, default null]: list of strings to get specific entities by subscription ids. ex: ["12376517623", "1928367198236"]</item>
        ///     <item>externalIds [list of strings, default null]: list of strings to get specific entities by external ids. ex: ["my-external-id-1", "my-external-id-2"]</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "success" or "failed"</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of InvoicePullRequest objects with updated attributes and cursor to retrieve the next page of InvoicePullRequest objects</item>
        ///     <item>cursor to retrieve the next page of InvoicePullRequest objects</item>
        /// </list>
        /// </summary>
        public static (List<InvoicePullRequest> page, string pageCursor) Page(string cursor = null, int? limit = null, DateTime? after = null, DateTime? before = null, string status = null, List<string> tags = null, List<string> ids = null, User user = null)
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
                    { "ids", ids }
                },
                user: user
            );
            List<InvoicePullRequest> invoicePullRequests = new List<InvoicePullRequest>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                invoicePullRequests.Add(subResource as InvoicePullRequest);
            }
            return (invoicePullRequests, pageCursor);
        }

        /// <summary>
        /// Cancel a InvoicePullRequest entity
        /// <br/>
        /// Cancel a InvoicePullRequest entity previously created in the Stark Bank API
        /// <br/>
        /// Parameters(required):
        /// <list>
        ///     <item>id[string]: InvoicePullRequest unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters(optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>canceled InvoicePullRequest object</item>
        /// </list>
        /// </summary>
        public static InvoicePullRequest Cancel(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.DeleteId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as InvoicePullRequest;
        }

        internal new Dictionary<string, object> ToJson()
        {
            Dictionary<string, object> json = base.ToJson();
            if (Due != null) json["due"] = new StarkCore.Utils.StarkDateTime(Due);
            if (Created != null) json["created"] = new StarkCore.Utils.StarkDateTime(Created);
            if (Updated != null) json["updated"] = new StarkCore.Utils.StarkDateTime(Updated);
            return json;
        }
        
        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "InvoicePullRequest", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            string subscriptionId = json.subscriptionId;
            string invoiceId = json.invoiceId;
            DateTime? due = null;
            string attemptType = json.attemptType;
            List<string> tags = json.tags != null ? json.tags.ToObject<List<string>>() : null;
            string externalId = json.externalId;
            string displayDescription = json.displayDescription;
            string status = json.status;
            string installmentId = json.installmentId;

            if (json.due != "")
            {
                due = StarkCore.Utils.Checks.CheckDateTime((string)json.due);
            }
            DateTime? created = StarkCore.Utils.Checks.CheckDateTime((string)json.created);
            DateTime? updated = StarkCore.Utils.Checks.CheckDateTime((string)json.updated);

            return new InvoicePullRequest(
                id: id,
                subscriptionId: subscriptionId,
                invoiceId: invoiceId,
                due: due,
                attemptType: attemptType,
                tags: tags,
                externalId: externalId,
                displayDescription: displayDescription,
                status: status,
                installmentId: installmentId,
                created: created,
                updated: updated
            );
        }
    }
}
