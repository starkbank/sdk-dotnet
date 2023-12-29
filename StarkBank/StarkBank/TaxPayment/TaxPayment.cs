using System;
using System.Linq;
using StarkBank.Utils;
using System.Collections.Generic;

namespace StarkBank
{
    /// <summary>
    /// TaxPayment object
    /// <br/>
    /// When you initialize a TaxPayment, the entity will not be automatically
    /// created in the Stark Bank API. The 'create' function sends the objects
    /// to the Stark Bank API and returns the list of created objects.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Line [string, default null]: Number sequence that describes the payment. Either 'line' or 'barCode' parameters are required. If both are sent, they must match. ex: "85800000003 0 28960328203 1 56072020190 5 22109674804 0"</item>
    ///     <item>BarCode [string, default null]: Bar code number that describes the payment. Either 'line' or 'barCode' parameters are required. If both are sent, they must match. ex: "83660000001084301380074119002551100010601813"</item>
    ///     <item>Description [string]: Text to be displayed in your statement (min. 10 characters). ex: "payment ABC"</item>
    ///     <item>Scheduled [DateTime or string, default today]: payment scheduled date. ex: DateTime(2020, 3, 10)</item>
    ///     <item>Tags [list of strings, default null]: list of strings for tagging</item>
    ///     <item>Id [string]: unique id returned when payment is created. ex: "5656565656565656"</item>
    ///     <item>Type [string]: tax type. ex: "das"</item>
    ///     <item>Status [string]: current payment status. ex: "success" or "failed"</item>
    ///     <item>Amount [int]: amount automatically calculated from line or barCode. ex: 23456 (= R$ 234.56)</item>
    ///     <item>Fee [integer]: fee charged when tax payment is created. ex: 200 (= R$ 2.00)</item>
    ///     <item>TransactionIds [list of strings]: ledger transaction ids linked to this TaxPayment. ex: ["19827356981273"]</item>
    ///     <item>Updated [DateTime]: latest update datetime for the payment. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Created [DateTime]: creation datetime for the payment. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class TaxPayment : Resource
    {
        public string Line { get; }
        public string BarCode { get; }
        public string Description { get; }
        public List<string> Tags { get; }
        public DateTime? Scheduled { get; }
        public string Status { get; }
        public long? Amount { get; }
        public string Type { get; }
        public int? Fee { get; }
        public List<string> TransactionIds { get; }
        public DateTime? Updated { get; }
        public DateTime? Created { get; }

        /// <summary>
        /// TaxPayment object
        /// <br/>
        /// When you initialize a TaxPayment, the entity will not be automatically
        /// created in the Stark Bank API. The 'create' function sends the objects
        /// to the Stark Bank API and returns the list of created objects.
        /// <br/>
        /// Parameters (conditionally required):
        /// <list>
        ///     <item>line [string, default null]: Number sequence that describes the payment. Either 'line' or 'barCode' parameters are required. If both are sent, they must match. ex: "85800000003 0 28960328203 1 56072020190 5 22109674804 0"</item>
        ///     <item>barCode [string, default null]: Bar code number that describes the payment. Either 'line' or 'barCode' parameters are required. If both are sent, they must match. ex: "83660000001084301380074119002551100010601813"</item>
        /// </list>
        /// Parameters (required):
        /// <list>
        ///     <item>description [string]: Text to be displayed in your statement (min. 10 characters). ex: "payment ABC"</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>scheduled [DateTime or string, default today]: payment scheduled date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>tags [list of strings, default null]: list of strings for tagging</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when payment is created. ex: "5656565656565656"</item>
        ///     <item>type [string]: tax type. ex: "das"</item>
        ///     <item>status [string]: current payment status. ex: "success" or "failed"</item>
        ///     <item>amount [int]: amount automatically calculated from line or barCode. ex: 23456 (= R$ 234.56)</item>
        ///     <item>fee [integer]: fee charged when tax payment is created. ex: 200 (= R$ 2.00)</item>
        ///     <item>transactionIds [list of strings]: ledger transaction ids linked to this TaxPayment. ex: ["19827356981273"]</item>
        ///     <item>updated [DateTime]: latest update datetime for the payment. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>created [DateTime]: creation datetime for the payment. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public TaxPayment(string description, string line = null, string barCode = null, string id = null, List<string> tags = null,
            DateTime? scheduled = null, string status = null, long? amount = null, int? fee = null, string type = null,
            List<string> transactionIds = null, DateTime? updated = null, DateTime? created = null
        ) : base(id)
        {
            Description = description;
            Line = line;
            BarCode = barCode;
            Tags = tags;
            Scheduled = scheduled;
            Status = status;
            Amount = amount;
            Type = type;
            Fee = fee;
            TransactionIds = transactionIds;
            Updated = updated;
            Created = created;
        }

        /// <summary>
        /// Create TaxPayments
        /// <br/>
        /// Send a list of TaxPayment objects for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>payments [list of TaxPayment objects]: list of TaxPayment objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of TaxPayment objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<TaxPayment> Create(List<TaxPayment> payments, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: payments,
                user: user
            ).ToList().ConvertAll(o => (TaxPayment)o);
        }

        /// <summary>
        /// Create TaxPayments
        /// <br/>
        /// Send a list of TaxPayment objects for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>payments [list of Dictionaries]: list of Dictionaries representing the TaxPayments to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of TaxPayment objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<TaxPayment> Create(List<Dictionary<string, object>> payments, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: payments,
                user: user
            ).ToList().ConvertAll(o => (TaxPayment)o);
        }

        /// <summary>
        /// Retrieve a specific TaxPayment
        /// <br/>
        /// Receive a single TaxPayment object previously created by the Stark Bank API by passing its id
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
        ///     <item>TaxPayment object with updated attributes</item>
        /// </list>
        /// </summary>
        public static TaxPayment Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as TaxPayment;
        }

        /// <summary>
        /// Retrieve a specific TaxPayment pdf file
        /// <br/>
        // Receive a single TaxPayment pdf file generated in the Stark Bank API by passing its id.
        // Only valid for tax payments with "success" status.
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
        ///     <item>TaxPayment pdf file</item>
        /// </list>
        /// </summary>
        public static byte[] Pdf(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetContent(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                subResourceName: "pdf",
                id: id,
                user: user
            );
        }

        /// <summary>
        /// Retrieve TaxPayments
        /// <br/>
        /// Receive an IEnumerable of TaxPayment objects previously created in the Stark Bank API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "paid"</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of TaxPayment objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<TaxPayment> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
            List<string> tags = null, List<string> ids = null, string status = null, User user = null)
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
                    { "ids", ids },
                    { "status", status }
                },
                user: user
            ).Cast<TaxPayment>();
        }

        /// <summary>
        /// Retrieve paged TaxPayments
        /// <br/>
        /// Receive a list of up to 100 TaxPayment objects previously created in the Stark Bank API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "paid"</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of TaxPayment objects with updated attributes and cursor to retrieve the next page of TaxPayment objects</item>
        /// </list>
        /// </summary>
        public static (List<TaxPayment> page, string pageCursor) Page(string cursor = null, int? limit = null, DateTime? after = null,
            DateTime? before = null, List<string> tags = null, List<string> ids = null, string status = null, User user = null)
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
                    { "ids", ids },
                    { "status", status }
                },
                user: user
            );
            List<TaxPayment> taxPayments = new List<TaxPayment>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                taxPayments.Add(subResource as TaxPayment);
            }
            return (taxPayments, pageCursor);
        }

        /// <summary>
        /// Delete a TaxPayment entity
        /// <br/>
        /// Delete a TaxPayment entity previously created in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: TaxPayment unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>deleted TaxPayment object</item>
        /// </list>
        /// </summary>
        public static TaxPayment Delete(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.DeleteId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as TaxPayment;
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "TaxPayment", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string line = json.line;
            string barCode = json.barCode;
            string description = json.description;
            string scheduledString = json.scheduled;
            DateTime? scheduled = StarkCore.Utils.Checks.CheckNullableDateTime(scheduledString);
            List<string> tags = json.tags?.ToObject<List<string>>();
            string id = json.id;
            string status = json.status;
            string type = json.type;
            long? amount = json.amount;
            int? fee = json.fee;
            List<string> transactionIds = json.transactionIds?.ToObject<List<string>>();
            string createdString = json.created;
            DateTime? created = StarkCore.Utils.Checks.CheckNullableDateTime(createdString);
            string updatedString = json.updated;
            DateTime? updated = StarkCore.Utils.Checks.CheckNullableDateTime(updatedString);

            return new TaxPayment(
                line: line, barCode: barCode, description: description, id: id, tags: tags,
                scheduled: scheduled, status: status, amount: amount, fee: fee, 
                transactionIds: transactionIds, type: type, updated: updated, 
                created: created
            );
        }
    }
}
