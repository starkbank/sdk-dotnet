using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkBank
{
    /// <summary>
    /// Transfer object
    /// <br/>
    /// When you initialize a Transfer, the entity will not be automatically
    /// created in the Stark Bank API. The 'create' function sends the objects
    /// to the Stark Bank API and returns the list of created objects.
    /// <br/>
    /// Parameters (required):
    /// <list>
    ///     <item>Amount [long integer]: amount in cents to be transferred. ex: 1234 (= R$ 12.34)</item>
    ///     <item>Name [string]: receiver full name. ex: "Anthony Edward Stark"</item>
    ///     <item>TaxID [string]: receiver tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
    ///     <item>BankCode [string]: code of the receiver bank institution in Brazil. If an ISPB (8 digits) is informed, a PIX transfer will be created, else a TED will be issued. ex: "20018183" or "341"</item>
    ///     <item>BranchCode [string]: receiver bank account branch. Use '-' in case there is a verifier digit. ex: "1357-9"</item>
    ///     <item>AccountNumber [string]: Receiver Bank Account number. Use '-' before the verifier digit. ex: "876543-2"</item>
    ///     <item>Scheduled [DateTime, default now]: datetime when the transfer will be processed. May be pushed to next business day if necessary. ex: new DateTime(2020, 3, 11, 8, 0, 0, 0)</item>
    ///     <item>Tags [list of strings]: list of strings for reference when searching for Transfers. ex: ["employees", "monthly"]</item>
    ///     <item>ID [string, default null]: unique id returned when Transfer is created. ex: "5656565656565656"</item>
    ///     <item>Fee [integer, default null]: fee charged when Transfer is created. ex: 200 (= R$ 2.00)</item>
    ///     <item>Status [string, default null]: current Transfer status. ex: "success" or "failed"</item>
    ///     <item>TransactionIds [list of strings, default null]: ledger Transaction ids linked to this Transfer (if there are two, second is the chargeback). ex: ["19827356981273"]</item>
    ///     <item>Created [DateTime, default null]: creation datetime for the Transfer. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime, default null]: latest update datetime for the Transfer. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class Transfer : Utils.Resource
    {
        public long Amount { get; }
        public string Name { get; }
        public string TaxID { get; }
        public string BankCode { get; }
        public string BranchCode { get; }
        public string AccountNumber { get; }
        public DateTime? Scheduled { get; }
        public List<string> TransactionIds { get; }
        public int? Fee { get; }
        public List<string> Tags { get; }
        public string Status { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// Transfer object
        /// <br/>
        /// When you initialize a Transfer, the entity will not be automatically
        /// created in the Stark Bank API. The 'create' function sends the objects
        /// to the Stark Bank API and returns the list of created objects.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>amount [long integer]: amount in cents to be transferred. ex: 1234 (= R$ 12.34)</item>
        ///     <item>name [string]: receiver full name. ex: "Anthony Edward Stark"</item>
        ///     <item>taxID [string]: receiver tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
        ///     <item>bankCode [string]: 1 to 3 digits code of the bank institution in Brazil. ex: "200" or "341"</item>
        ///     <item>branchCode [string]: receiver bank account branch. Use '-' in case there is a verifier digit. ex: "1357-9"</item>
        ///     <item>accountNumber [string]: Receiver Bank Account number. Use '-' before the verifier digit. ex: "876543-2"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>tags [list of strings]: list of strings for reference when searching for Transfers. ex: ["employees", "monthly"]</item>
        ///     <item>scheduled [DateTime, default now]: datetime when the transfer will be processed.May be pushed to next business day if necessary. ex: new DateTime(2020, 3, 11, 8, 0, 0, 0)</item>
        /// </list>
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string, default null]: unique id returned when Transfer is created. ex: "5656565656565656"</item>
        ///     <item>fee [integer, default null]: fee charged when Transfer is created. ex: 200 (= R$ 2.00)</item>
        ///     <item>status [string, default null]: current Transfer status. ex: "success" or "failed"</item>
        ///     <item>transactionIds [list of strings, default null]: ledger transaction ids linked to this Transfer (if there are two, second is the chargeback). ex: ["19827356981273"]</item>
        ///     <item>created [DateTime, default null]: creation datetime for the Transfer. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime, default null]: latest update datetime for the Transfer. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public Transfer(long amount, string name, string taxID, string bankCode, string branchCode, string accountNumber,
            DateTime? scheduled = null, string id = null, List<string> transactionIds = null, int? fee = null,
            List<string> tags = null, string status = null, DateTime? created = null, DateTime? updated = null
            ) : base(id)
        {
            Amount = amount;
            Name = name;
            TaxID = taxID;
            BankCode = bankCode;
            BranchCode = branchCode;
            AccountNumber = accountNumber;
            Scheduled = scheduled;
            TransactionIds = transactionIds;
            Fee = fee;
            Tags = tags;
            Status = status;
            Created = created;
            Updated = updated;
        }

        /// <summary>
        /// Create Transfers
        /// <br/>
        /// Send a list of Transfer objects for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>transfers [list of Transfer objects]: list of Transfer objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of Transfer objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<Transfer> Create(List<Transfer> transfers, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: transfers,
                user: user
            ).ToList().ConvertAll(o => (Transfer)o);
        }

        /// <summary>
        /// Create Transfers
        /// <br/>
        /// Send a list of Transfer objects for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>transfers [list of Dictionaries]: list of Dictionaries representing the Transfers to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of Transfer objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<Transfer> Create(List<Dictionary<string, object>> transfers, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: transfers,
                user: user
            ).ToList().ConvertAll(o => (Transfer)o);
        }

        /// <summary>
        /// Retrieve a specific Transfer
        /// <br/>
        /// Receive a single Transfer object previously created in the Stark Bank API by passing its id
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
        ///     <item>Transfer object with updated attributes</item>
        /// </list>
        /// </summary>
        public static Transfer Get(string id, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as Transfer;
        }

        /// <summary>
        /// Cancel a Transfer entity
        /// <br/>
        /// Cancel a scheduled Transfer entity previously created in the Stark Bank API
        /// <br/>
        /// Parameters(required) :
        /// <list>
        ///     <item>id[string]: Transfer unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters(optional) :
        /// <list>
        ///     <item>user[Project object]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>deleted Transfer object</item>
        /// </list>
        /// </summary>
        public static Transfer Delete(string id, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.DeleteId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as Transfer;
        }

        /// <summary>
        /// Retrieve a specific Transfer pdf file
        /// <br/>
        /// Receive a single Transfer pdf receipt file generated in the Stark Bank API by passing its id.
        /// Only valid for transfers with "processing" and "success" status.
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
        ///     <item>Transfer pdf file</item>
        /// </list>
        /// </summary>
        public static byte[] Pdf(string id, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.GetPdf(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            );
        }

        /// <summary>
        /// Retrieve Transfers
        /// <br/>
        /// Receive an IEnumerable of Transfer objects previously created in the Stark Bank API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null] date filter for objects created or updated only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created or updated only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>transactionIds [list of strings, default null]: list of transaction IDs linked to the desired transfers. ex: ["5656565656565656", "4545454545454545"]</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "paid" or "registered"</item>
        ///     <item>taxID [string, default null]: filter for transfers sent to the specified tax ID. ex: "012.345.678-90"</item>
        ///     <item>sort [string, default "-created"]: sort order considered in the response. Options are: "created", "-created", "updated" and "-updated". "-" means descending order.</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of Transfer objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<Transfer> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
            List<string> transactionIds = null, string status = null, string taxID = null, string sort = null, List<string> tags = null, List<string> ids = null,
            User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "after", new Utils.StarkBankDate(after) },
                    { "before", new Utils.StarkBankDate(before) },
                    { "transactionIds", transactionIds },
                    { "status", status },
                    { "taxID", taxID },
                    { "sort", sort },
                    { "tags", tags },
                    { "ids", ids }
                },
                user: user
            ).Cast<Transfer>();
        }

        internal static (string resourceName, Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "Transfer", resourceMaker: ResourceMaker);
        }

        internal static Utils.Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            long amount = json.amount;
            string name = json.name;
            string taxID = json.taxId;
            string bankCode = json.bankCode;
            string branchCode = json.branchCode;
            string accountNumber = json.accountNumber;
            string scheduledString = json.scheduled;
            DateTime? scheduled = Utils.Checks.CheckNullableDateTime(scheduledString);
            List<string> transactionIds = new List<string>();
            if (json.transactionIds != null) {
                transactionIds = json.transactionIds.ToObject<List<string>>();
            }
            int? fee = json.fee;
            List<string> tags = new List<string>();
            if (json.tags != null) {
                tags = json.tags.ToObject<List<string>>();
            }
            string status = json.status;
            string createdString = json.created;
            DateTime? created = Utils.Checks.CheckNullableDateTime(createdString);
            string updatedString = json.updated;
            DateTime? updated = Utils.Checks.CheckNullableDateTime(updatedString);

            return new Transfer(
                id: id, amount: amount, name: name, taxID: taxID, bankCode: bankCode, branchCode: branchCode,
                accountNumber: accountNumber, scheduled: scheduled, transactionIds: transactionIds, fee: fee, tags: tags, status: status,
                created: created, updated: updated
            );
        }
    }
}
