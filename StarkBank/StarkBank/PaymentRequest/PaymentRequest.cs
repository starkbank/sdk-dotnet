using System;
using System.Linq;
using System.Collections.Generic;
using StarkBank.Utils;


namespace StarkBank
{
    /// <summary>
    /// PaymentRequest object
    /// <br/>
    /// A PaymentRequest is an indirect request to access a specific cash-out service
    /// (such as Transfers, BrcodePayments, etc.) which goes through the cost center
    /// approval flow on our website. To emit a PaymentRequest, you must direct it to
    /// a specific cost center by its ID, which can be retrieved on our website at the
    /// cost center page.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>CenterID [string]: target cost center ID. ex: "5656565656565656"</item>
    ///     <item>Payment [Transfer, BrcodePayment, BoletoPayment, UtilityPayment, Transaction or dictionary]: payment entity that should be approved and executed.</item>
    ///     <item>Type [string]: payment type, inferred from the payment parameter if it is not a dictionary. ex: "transfer", "brcode-payment"</item>
    ///     <item>Due [DateTime]: Payment target date in ISO format. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Tags [list of strings]: list of strings for tagging</item>
    ///     <item>ID [string, default null]: unique id returned when PaymentRequest is created. ex: "5656565656565656"</item>
    ///     <item>Amount [integer, default null]: PaymentRequest amount. ex: 100000 = R$1.000,00</item>
    ///     <item>Status [string, default null]: current PaymentRequest status. ex: "pending" or "approved"</item>
    ///     <item>Actions [list of dictionaries, default null]: list of actions that are affecting this PaymentRequest. ex: new List<Dictionary<string,string>>(){new Dictionary<string, string>{{"type", "member"},{"id", "56565656565656"},{"action", "requested"}}</item>
    ///     <item>Updated [DateTime, default null]: latest update datetime for the PaymentRequest. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Created [DateTime, default null]: creation datetime for the PaymentRequest. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class PaymentRequest : Utils.Resource
    {
        public string CenterID { get; }
        public Utils.Resource Payment { get; }
        public string Type { get; }
        public DateTime? Due { get; }
        public List<string> Tags { get; }
        public long? Amount { get; }
        public string Status { get; }
        public List<Dictionary<string, string>> Actions { get; }
        public DateTime? Updated { get; }
        public DateTime? Created { get; }

        /// <summary>
        /// PaymentRequest object
        /// <br/>
        /// A PaymentRequest is an indirect request to access a specific cash-out service
        /// (such as Transfers, BrcodePayments, etc.) which goes through the cost center
        /// approval flow on our website. To emit a PaymentRequest, you must direct it to
        /// a specific cost center by its ID, which can be retrieved on our website at the
        /// cost center page.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>centerID [string]: target cost center ID. ex: "5656565656565656"</item>
        ///     <item>payment [Transfer, BrcodePayment, BoletoPayment, UtilityPayment, Transaction or dictionary]: payment entity that should be approved and executed.</item>
        /// </list>
        /// <br/>
        /// Parameters (conditionally required):
        /// <list>
        ///     <item>type [string]: payment type, inferred from the payment parameter if it is not a dictionary. ex: "transfer", "brcode-payment"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        ///     <item>due [DateTime, default today]: Payment target date in ISO format. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>tags [list of strings]: list of strings for tagging</item>
        /// </list>
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string, default null]: unique id returned when PaymentRequest is created. ex: "5656565656565656"</item>
        ///     <item>amount [integer, default null]: PaymentRequest amount. ex: 100000 = R$1.000,00</item>
        ///     <item>status [string, default null]: current PaymentRequest status. ex: "pending" or "approved"</item>
        ///     <item>actions [list of dictionaries, default null]: list of actions that are affecting this PaymentRequest. ex: new List<Dictionary<string,string>>(){new Dictionary<string, string>{{"type", "member"},{"id", "56565656565656"},{"action", "requested"}}</item>
        ///     <item>updated [DateTime, default null]: latest update datetime for the PaymentRequest. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>created [DateTime, default null]: creation datetime for the PaymentRequest. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public PaymentRequest(Utils.Resource payment, string centerID, string id = null, string type = null, DateTime? due = null,
            List<string> tags = null, long? amount = null, string status = null,
            List<Dictionary<string, string>> actions = null, DateTime? updated = null, DateTime? created = null) : base(id)
        {
            CenterID = centerID;
            Payment = payment;
            Type = type;
            Due = due;
            Tags = tags;
            Amount = amount;
            Status = status;
            Actions = actions;
            Updated = updated;
            Created = created;

            if (type == null) {
                Type = GetType(payment);
            }
        }

        private static string GetType(Utils.Resource payment)
        {
            if (payment.GetType() == typeof(Transfer)) {
                return "transfer";
            }
            if (payment.GetType() == typeof(Transaction)) {
                return "transaction";
            }
            if (payment.GetType() == typeof(BrcodePayment)) {
                return "brcode-payment";
            }
            if (payment.GetType() == typeof(BoletoPayment)) {
                return "boleto-payment";
            }
            if (payment.GetType() == typeof(UtilityPayment)) {
                return "utility-payment";
            }
            throw new Exception("if no type is specified, payment must be either a Transfer, a Transaction, a BoletoPayment or a UtilityPayment");
        }

        internal new Dictionary<string, object> ToJson()
        {
            Dictionary<string, object> json = base.ToJson();
            json["Due"] = new Utils.StarkBankDate((DateTime)json["Due"]);
            return json;
        }

        /// <summary>
        /// Create PaymentRequests
        /// <br/>
        /// Send a list of PaymentRequest objects for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>requests [list of PaymentRequest objects]: list of PaymentRequest objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of PaymentRequest objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<PaymentRequest> Create(List<PaymentRequest> requests, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: requests,
                user: user
            ).ToList().ConvertAll(o => (PaymentRequest)o);
        }

        /// <summary>
        /// Retrieve PaymentRequests
        /// <br/>
        /// Receive an IEnumerable of PaymentRequest objects previously created by this user in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>centerID [string]: target cost center ID. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "success", "failed"</item>
        ///     <item>type [string, default null]: payment type, inferred from the payment parameter if it is not a dictionary. ex: "transfer", "brcode-payment"</item>
        ///     <item>sort [string, default "-created"]: sort order considered in response. Valid options are "-created" or "-due".
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        ///     <item>IEnumerable of PaymentRequest objects with updated attributes</item>
        /// </summary>
        public static IEnumerable<PaymentRequest> Query(string centerID, int? limit = null, DateTime? after = null, DateTime? before = null,
            string status = null, string type = null, string sort = null, List<string> tags = null, List<string> ids = null, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "centerID", centerID },
                    { "limit", limit },
                    { "after", new Utils.StarkBankDate(after) },
                    { "before", new Utils.StarkBankDate(before) },
                    { "status", status },
                    { "type", type },
                    { "sort", sort },
                    { "tags", tags },
                    { "ids", ids }
                },
                user: user
            ).Cast<PaymentRequest>();
        }

        /// <summary>
        /// Retrieve paged PaymentRequests
        /// <br/>
        /// Receive a list of up to 100 PaymentRequest objects previously created in the Stark Bank API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>centerID [string]: target cost center ID. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "success", "failed"</item>
        ///     <item>type [string, default null]: payment type, inferred from the payment parameter if it is not a dictionary. ex: "transfer", "brcode-payment"</item>
        ///     <item>sort [string, default "-created"]: sort order considered in response. Valid options are "-created" or "-due".
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        ///     <item>list of PaymentRequest objects with updated attributes and cursor to retrieve the next page of PaymentRequest objects</item>
        /// </summary>
        public static (List<PaymentRequest> page, string pageCursor) Page(string centerID, string cursor = null, int? limit = null, DateTime? after = null,
            DateTime? before = null, string status = null, string type = null, string sort = null, List<string> tags = null, List<string> ids = null, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            (List<SubResource> page, string pageCursor) = Utils.Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "centerID", centerID },
                    { "limit", limit },
                    { "after", new Utils.StarkBankDate(after) },
                    { "before", new Utils.StarkBankDate(before) },
                    { "status", status },
                    { "type", type },
                    { "sort", sort },
                    { "tags", tags },
                    { "ids", ids }
                },
                user: user
            );
            List<PaymentRequest> paymentRequests = new List<PaymentRequest>();
            foreach (SubResource subResource in page)
            {
                paymentRequests.Add(subResource as PaymentRequest);
            }
            return (paymentRequests, pageCursor);
        }

        internal static (string resourceName, Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "PaymentRequest", resourceMaker: ResourceMaker);
        }

        internal static Utils.Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            string centerID = json.centerId;
            string type = json.type;
            Utils.Resource payment = ParsePaymentJson(json: json.payment, type: type);
            string dueString = json.due;
            DateTime? due = Utils.Checks.CheckNullableDateTime(dueString);
            List<string> tags = json.tags.ToObject<List<string>>();
            long? amount = json.amount;
            string status = json.status;
            List<Dictionary<string, string>> actions = json.actions.ToObject<List<Dictionary<string, string>>>();
            string createdString = json.created;
            DateTime? created = Utils.Checks.CheckDateTime(createdString);
            string updatedString = json.updated;
            DateTime? updated = Utils.Checks.CheckDateTime(updatedString);

            return new PaymentRequest(
                id: id, centerID: centerID, type: type, payment: payment, due: due, tags: tags,
                amount: amount, status: status, actions: actions, created: created, updated: updated
            );
        }

        private static Utils.Resource ParsePaymentJson(dynamic json, string type)
        {
            if (type == "transfer") {
                return Transfer.ResourceMaker(json);
            }
            if (type == "transaction") {
                return Transaction.ResourceMaker(json);
            }
            if (type == "brcode-payment") {
                return BrcodePayment.ResourceMaker(json);
            }
            if (type == "boleto-payment") {
                return BoletoPayment.ResourceMaker(json);
            }
            if (type == "utility-payment") {
                return UtilityPayment.ResourceMaker(json);
            }
            return null;
        }
    }
}
