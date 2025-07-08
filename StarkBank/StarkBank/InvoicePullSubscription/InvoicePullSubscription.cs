using System;
using System.Linq;
using StarkBank.Utils;
using System.Collections.Generic;

namespace StarkBank
{
    /// <summary>
    /// InvoicePullSubscription object
    /// <br/>
    /// When you initialize an InvoicePullSubscription, the entity will not be automatically
    /// sent to the Stark Bank API. The 'create' function sends the objects
    /// to the Stark Bank API and returns the list of created objects.
    /// <br/>
    /// Properties:
    ///     <item>Start [DateTime or string]: subscription start date. ex: "2022-04-01"</item>
    ///     <item>Interval [string]: subscription installment interval. Options: "week", "month", "quarter", "semester", "year"</item>
    ///     <item>PullMode [string]: subscription pull mode. Options: "manual", "automatic". Automatic mode will create the Invoice Pull Requests automatically</item>
    ///     <item>PullRetryLimit [integer]: subscription pull retry limit. Options: 0, 3</item>
    ///     <item>Type [string]: subscription type. Options: "push", "qrcode", "qrcodeAndPayment", "paymentAndOrQrcode"</item>
    ///     <item>Amount [integer, default 0]: subscription amount in cents. Required if an amount_min_limit is not informed. Minimum = 1 (R$ 0.01). ex: 100 (= R$ 1.00)</item>
    ///     <item>AmountMinLimit [integer, 0 None]: subscription minimum amount in cents. Required if an amount is not informed. Minimum = 1 (R$ 0.01). ex: 100 (= R$ 1.00)</item>
    ///     <item>DisplayDescription [string, default None]: Invoice description to be shown to the payer. ex: "Subscription payment"</item>
    ///     <item>Due [DateTime or integer, default None]: subscription invoice due offset. Available only for type "push". ex: timedelta(days=7)</item>
    ///     <item>ExternalID [string, default None]: string that must be unique among all your subscriptions. Duplicated external_ids will cause failures. ex: "my-external-id"</item>
    ///     <item>ReferenceCode [string, default None]: reference code for reconciliation. ex: "REF123456"</item>
    ///     <item>End [DateTime or string, default None]: subscription end date. ex: "2023-04-01"</item>
    ///     <item>Data [dictionary, default None]: additional data for the subscription based on type</item>
    ///     <item>Name [string, default None]: subscription debtor name. ex: "Iron Bank S.A."</item>
    ///     <item>TaxID [string, default None]: subscription debtor tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
    ///     <item>Tags [list of strings, default []]: list of strings for tagging</item>
    ///     <item>ID [string]: unique id returned when a subscription is created. ex: "5656565656565656"</item>
    ///     <item>Status [string]: current subscription status. ex: "pending", "scheduled", "canceled", "success", failed </item>
    ///     <item>BacenID [string]: unique authentication id at the Central Bank. ex: "ccf9bd9c-e99d-999e-bab9-b999ca999f99"</item>
    ///     <item>Brcode [string]: Bacen brcode for the subscription. ex: "RR3990842720250702ws3mC6J0DHh"</item>
    ///     <item>Created [DateTime]: creation DateTime for the subscription. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime]: latest update DateTime for the subscription. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class InvoicePullSubscription : Resource
    {
        public DateTime? Start { get; }
        public string Interval { get; }
        public string PullMode { get; }
        public long? PullRetryLimit { get; }
        public string Type { get; }
        public long? Amount { get; }
        public long? AmountMinLimit { get; }
        public string DisplayDescription { get; }
        public DateTime? Due { get; }
        public string ExternalID { get; }
        public string ReferenceCode { get; }
        public DateTime? End { get; }
        public Dictionary<string, object> Data { get; }
        public string Name { get; }
        public string TaxID { get; }
        public List<string> Tags { get; }
        public string Status { get; }
        public string BacenID { get; }
        public string Brcode { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// InvoicePullSubscription object
        /// <br/>
        /// When you initialize an InvoicePullSubscription, the entity will not be automatically
        /// sent to the Stark Bank API. The 'create' function sends the objects
        /// to the Stark Bank API and returns the list of created objects.
        /// Parameters (required):
        ///     <item>Start [DateTime or string]: subscription start date. ex: "2022-04-01"</item>
        ///     <item>Interval [string]: subscription installment interval. Options: "week", "month", "quarter", "semester", "year"</item>
        ///     <item>PullMode [string]: subscription pull mode. Options: "manual", "automatic". Automatic mode will create the Invoice Pull Requests automatically</item>
        ///     <item>PullRetryLimit [integer]: subscription pull retry limit. Options: 0, 3</item>
        ///     <item>Type [string]: subscription type. Options: "push", "qrcode", "qrcodeAndPayment", "paymentAndOrQrcode"</item>
        /// </list>
        /// <br/>
        /// Parameters (conditionally required):
        ///     <item>Amount [integer, default 0]: subscription amount in cents. Required if an amount_min_limit is not informed. Minimum = 1 (R$ 0.01). ex: 100 (= R$ 1.00)</item>
        ///     <item>AmountMinLimit [integer, 0 None]: subscription minimum amount in cents. Required if an amount is not informed. Minimum = 1 (R$ 0.01). ex: 100 (= R$ 1.00)</item>
        /// <br/>
        /// Parameters (optional):
        ///     <item>DisplayDescription [string, default None]: Invoice description to be shown to the payer. ex: "Subscription payment"</item>
        ///     <item>Due [DateTime or integer, default None]: subscription invoice due offset. Available only for type "push". ex: timedelta(days=7)</item>
        ///     <item>ExternalID [string, default None]: string that must be unique among all your subscriptions. Duplicated external_ids will cause failures. ex: "my-external-id"</item>
        ///     <item>ReferenceCode [string, default None]: reference code for reconciliation. ex: "REF123456"</item>
        ///     <item>End [DateTime or string, default None]: subscription end date. ex: "2023-04-01"</item>
        ///     <item>Data [dictionary, default None]: additional data for the subscription based on type</item>
        ///     <item>Name [string, default None]: subscription debtor name. ex: "Iron Bank S.A."</item>
        ///     <item>TaxID [string, default None]: subscription debtor tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
        ///     <item>Tags [list of strings, default []]: list of strings for tagging</item>
        /// <br/>
        /// Attributes (return-only):
        ///     <item>ID [string]: unique id returned when subscription is created. ex: "5656565656565656"</item>
        ///     <item>Status [string]: current subscription status. ex: "active", "canceled"</item>
        ///     <item>BacenID [string]: unique authentication id at the Central Bank. ex: "RR2001818320250616dtsPkBVaBYs"</item>
        ///     <item>Brcode [string]: Bacen brcode for the subscription. ex: "00020101021126580014br.gov.bcb.pix0114+5599999999990210starkbank.com.br520400005303986540410000000000005802BR5913Stark Bank S.A.6009SAO PAULO62070503***6304D2B1"</item>
        ///     <item>Created [DateTime]: creation DateTime for the subscription. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>Updated [DateTime]: latest update DateTime for the subscription. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
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

        /// <summary>
        /// Create InvoicePullSubscriptions
        /// <br/>
        /// Send a list of InvoicePullSubscription objects for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>payments [list of InvoicePullSubscription objects]: list of InvoicePullSubscription objects to be created in the API</item>
        /// </list>
        /// <br/>
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
        /// Retrieve a specific InvoicePullSubscription
        /// <br/>
        /// Receive a single InvoicePullSubscription object previously created by the Stark Bank API by passing its id
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
        ///     <item>InvoicePullSubscription object with updated attributes</item>
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
        /// Retrieve InvoicePullSubscriptions
        /// <br/>
        /// Receive an IEnumerable of InvoicePullSubscription objects previously created in the Stark Bank API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: new DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: new DateTime(2020, 3, 10)</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>ids [list of strings, default null]: list of strings to get specific entities by ids. ex: ["12376517623", "1928367198236"]</item>
        ///     <item>invoiceIds [list of strings, default null]: list of strings to get specific entities by invoice ids. ex: ["12376517623", "1928367198236"]</item>
        ///     <item>externalIds [list of strings, default null]: list of strings to get specific entities by external ids. ex: ["my-external-id-1", "my-external-id-2"]</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "success" or "failed"</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of InvoicePullSubscription objects with updated attributes</item>
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

        /// <summary>
        /// Retrieve paged InvoicePullSubscriptions
        /// <br/>
        /// Receive a list of up to 100 InvoicePullSubscription objects previously created in the Stark Bank API and the cursor to the next page.
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
        ///     <item>externalIds [list of strings, default null]: list of strings to get specific entities by external ids. ex: ["my-external-id-1", "my-external-id-2"]</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "success" or "failed"</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of InvoicePullSubscription objects with updated attributes and cursor to retrieve the next page of InvoicePullSubscription objects</item>
        ///     <item>cursor to retrieve the next page of InvoicePullSubscription objects</item>
        /// </list>
        /// </summary>
        public static (List<InvoicePullSubscription> page, string pageCursor) Page(string cursor = null, int? limit = null, DateTime? after = null, DateTime? before = null,
                    string status = null, List<string> tags = null, List<string> ids = null, User user = null)
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
            List<InvoicePullSubscription> invoicePullSubscriptions = new List<InvoicePullSubscription>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                invoicePullSubscriptions.Add(subResource as InvoicePullSubscription);
            }
            return (invoicePullSubscriptions, pageCursor);
        }

        /// <summary>
        /// Cancel a InvoicePullSubscription entity
        /// <br/>
        /// Cancel a InvoicePullSubscription entity previously created in the Stark Bank API
        /// <br/>
        /// Parameters(required):
        /// <list>
        ///     <item>id[string]: subscription unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters(optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>canceled InvoicePullSubscription object</item>
        /// </list>
        /// </summary>
        public static InvoicePullSubscription Cancel(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.DeleteId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as InvoicePullSubscription;
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
