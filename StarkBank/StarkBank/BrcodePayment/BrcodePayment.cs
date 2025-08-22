using System;
using System.Linq;
using StarkBank.Utils;
using System.Collections.Generic;


namespace StarkBank
{
    /// <summary>
    /// BrcodePayment object
    /// <br/>
    /// When you initialize a BrcodePayment, the entity will not be automatically
    /// created in the Stark Bank API. The 'create' function sends the objects
    /// to the Stark Bank API and returns the list of created objects.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Brcode [string]: String loaded directly from the QRCode or copied from the invoice.ex: "00020126580014br.gov.bcb.pix0136a629532e-7693-4846-852d-1bbff817b5a8520400005303986540510.005802BR5908T'Challa6009Sao Paulo62090505123456304B14A"</item>
    ///     <item>TaxID [string]: receiver tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
    ///     <item>Description [string]: Text to be displayed in your statement (min. 10 characters). ex: "payment ABC"</item>
    ///     <item>Amount [long integer]: If the BR Code does not provide an amount, this parameter is mandatory, else it is optional, but when it is informed, it must be a match. ex: 23456 (= R$ 234.56)</item>
    ///     <item>Scheduled [DateTime]: payment scheduled datetime. ex: new DateTime(2020, 3, 10)</item>
    ///     <item>Tags [list of strings]: list of strings for tagging</item>
    ///     <item>Rules [list of BrcodePayment.Rule objects, default null]: list of rules to overwrite default behavior. ex: new List<BrcodePayment.Rule>() {new BrcodePayment.Rule("resendingLimit", 5)}</item>
    ///     <item>ID [string]: unique id returned when payment is created. ex: "5656565656565656"</item>
    ///     <item>Name [string]: receiver name. ex: "Jon Snow"</item>
    ///     <item>Status [string]: current payment status. ex: "success" or "failed"</item>
    ///     <item>Type [string]: brcode type. ex: "static" or "dynamic"</item>
    ///     <item>TransactionIds [list of strings, default null]: ledger transaction ids linked to this payment. ex: ["19827356981273"]</item>
    ///     <item>Fee [integer]: fee charged when BrcodePayment is created. ex: 200 (= R$ 2.00)</item>
    ///     <item>Created [DateTime]: creation datetime for the payment. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime]: latest udpate datetime for the payment. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class BrcodePayment : Resource
    {
        public string Brcode { get; }
        public string TaxID { get; }
        public string Description { get; }
        public long? Amount { get; }
        public DateTime? Scheduled { get; }
        public List<string> Tags { get; }
        public List<Rule> Rules { get; }
        public string Name { get; }
        public string Status { get; }
        public string Type { get; }
        public List<string> TransactionIds { get; }
        public int? Fee { get; }
        public Dictionary<string, object> Metadata { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// BrcodePayment object
        /// <br/>
        /// When you initialize a BrcodePayment, the entity will not be automatically
        /// created in the Stark Bank API. The 'create' function sends the objects
        /// to the Stark Bank API and returns the list of created objects.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>brcode [string]: : String loaded directly from the QRCode or copied from the invoice.ex: "00020126580014br.gov.bcb.pix0136a629532e-7693-4846-852d-1bbff817b5a8520400005303986540510.005802BR5908T'Challa6009Sao Paulo62090505123456304B14A"</item>
        ///     <item>taxID [string]: receiver tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
        ///     <item>description [string]: Text to be displayed in your statement (min. 10 characters). ex: "payment ABC"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>amount [long integer, default null]: If the BRCode does not provide an amount, this parameter is mandatory, else it is optional, but if informed must be a match. ex: 23456 (= R$ 234.56)</item>
        ///     <item>scheduled [DateTime, default now]: payment scheduled datetime. ex: new DateTime(2020, 3, 10)</item>
        ///     <item>tags [list of strings, default null]: list of strings for tagging</item>
        ///     <item>rules [list of BrcodePayment.Rule objects, default null]: list of rules to overwrite default behavior. ex: new List<BrcodePayment.Rule>() {new BrcodePayment.Rule("resendingLimit", 5)}</item>
        /// </list>
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when payment is created. ex: "5656565656565656"</item>
        ///     <item>name [string]: receiver name. ex: "Jon Snow"</item>
        ///     <item>status [string]: current payment status. ex: "success" or "failed"</item>
        ///     <item>type [string]: brcode type. ex: "static" or "dynamic"</item>
        ///     <item>fee [integer]: fee charged when BrcodePayment is created. ex: 200 (= R$ 2.00)</item>
        ///     <item>transactionIds [list of strings]: ledger transaction ids linked to this payment. ex: ["19827356981273"]</item>
        ///     <item>created [DateTime]: creation datetime for the payment. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime]: latest udpate datetime for the payment. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public BrcodePayment(string brcode, string taxID, string description, string id = null, long? amount = null,
            DateTime? scheduled = null, List<string> tags = null, List<Rule> rules = null, string name = null, 
            string status = null, string type = null, List<string> transactionIds = null, int? fee = null, Dictionary<string, object> metadata = null,
            DateTime? created = null, DateTime? updated = null
        ) : base(id)
        {
            Brcode = brcode;
            TaxID = taxID;
            Description = description;
            Amount = amount;
            Scheduled = scheduled;
            Tags = tags;
            Rules = rules;
            Name = name;
            Status = status;
            Type = type;
            TransactionIds = transactionIds;
            Fee = fee;
            Metadata = metadata;
            Created = created;
            Updated = updated;
        }

        /// <summary>
        /// Create BrcodePayments
        /// <br/>
        /// Send a list of BrcodePayment objects for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>payments [list of BrcodePayment objects]: list of BrcodePayment objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of BrcodePayment objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<BrcodePayment> Create(List<BrcodePayment> payments, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: payments,
                user: user
            ).ToList().ConvertAll(o => (BrcodePayment)o);
        }

        /// <summary>
        /// Create BrcodePayments
        /// <br/>
        /// Send a list of BrcodePayment objects for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>payments [list of Dictionaries]: list of Dictionaries representing the BrcodePayments to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of BrcodePayment objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<BrcodePayment> Create(List<Dictionary<string, object>> payments, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: payments,
                user: user
            ).ToList().ConvertAll(o => (BrcodePayment)o);
        }

        /// <summary>
        /// Retrieve a specific BrcodePayment
        /// <br/>
        /// Receive a single BrcodePayment object previously created by the Stark Bank API by passing its id
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
        ///     <item>BrcodePayment object with updated attributes</item>
        /// </list>
        /// </summary>
        public static BrcodePayment Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as BrcodePayment;
        }

        /// <summary>
        /// Retrieve a specific BrcodePayment pdf file
        /// <br/>
        /// Receive a single BrcodePayment pdf receipt file generated in the Stark Bank API by passing its id.
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
        ///     <item>BrcodePayment pdf file</item>
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
        /// Update BrcodePayment entity
        /// <br/>
        /// Update an BrcodePayment by passing id, if it hasn't been paid yet.
        /// <br/>
        /// Parameters(required):
        /// <list>
        ///     <item>id[string]: object unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>status [string]: you may cancel the payment by passing "canceled" in the status. ex: "canceled"</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>target BrcodePayment with updated attributes</item>
        /// </list>
        /// </summary>
        public static BrcodePayment Update(string id, string status = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PatchId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                payload: new Dictionary<string, object> {
                    { "status", status },
                },
                user: user
            ) as BrcodePayment;
        }

        /// <summary>
        /// Retrieve BrcodePayments
        /// <br/>
        /// Receive an IEnumerable of BrcodePayment objects previously created in the Stark Bank API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: new DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: new DateTime(2020, 3, 10)</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>ids [list of strings, default null]: list of strings to get specific entities by ids. ex: ["12376517623", "1928367198236"]</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "success" or "failed"</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of BrcodePayment objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<BrcodePayment> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
            List<string> tags = null, List<string> ids= null, string status = null, User user = null)
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
                    { "ids", ids},
                    { "status", status }
                },
                user: user
            ).Cast<BrcodePayment>();
        }

        /// <summary>
        /// Retrieve paged BrcodePayments
        /// <br/>
        /// Receive a list of up to 100 BrcodePayment objects previously created in the Stark Bank API and the cursor to the next page.
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
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "success" or "failed"</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of BrcodePayment objects with updated attributes and cursor to retrieve the next page of Boleto objects</item>
        /// </list>
        /// </summary>

        public static (List<BrcodePayment> page, string pageCursor) Page(string cursor = null, int? limit = null, DateTime? after = null,
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
                    { "ids", ids},
                    { "status", status }
                },
                user: user
            );
            List<BrcodePayment> brcodePayments = new List<BrcodePayment>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                brcodePayments.Add(subResource as BrcodePayment);
            }
            return (brcodePayments, pageCursor);
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "BrcodePayment", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            string brcode = json.brcode;
            string taxID = json.taxId;
            string description = json.description;
            long? amount = json.amount;
            string scheduledString = json.scheduled;
            DateTime? scheduled = StarkCore.Utils.Checks.CheckNullableDateTime(scheduledString);
            List<string> tags = new List<string>();
            List<Rule> rules = ParseRule(json.rules);
            string name = json.name;
            tags = json.tags?.ToObject<List<string>>();
            string status = json.status;
            string type = json.type;
            List<string> transactionIds = new List<string>();
            transactionIds = json.transactionIds?.ToObject<List<string>>();
            int? fee = json.fee;
            Dictionary<string, object> metadata = json.metadata.ToObject<Dictionary<string, object>>();
            string createdString = json.created;
            DateTime? created = StarkCore.Utils.Checks.CheckNullableDateTime(createdString);
            string updatedString = json.updated;
            DateTime? updated = StarkCore.Utils.Checks.CheckNullableDateTime(updatedString);

            return new BrcodePayment(
                id: id, brcode: brcode, taxID: taxID, description: description, amount: amount, scheduled: scheduled, tags: tags,
                rules: rules, name: name, status: status, type: type, transactionIds: transactionIds, fee: fee, metadata: metadata, created: created,
                updated: updated
            );
        }

        private static List<Rule> ParseRule(dynamic json)
        {
            if(json is null) return null;

            List<Rule> rules = new List<Rule>();

            foreach (dynamic rule in json)
            {
                rules.Add(Rule.ResourceMaker(rule));
            }
            return rules;
        }
    }
}
