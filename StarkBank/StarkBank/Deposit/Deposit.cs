using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkBank
{
    /// <summary>
    /// Deposit object
    /// <br/>
    /// Deposits represent passive cash-in received by your account from external transfers
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>ID [string, default null]: unique id associated with a Deposit when it is created. ex: "5656565656565656"</item>
    ///     <item>Amount [long integer]: Deposit value in cents. ex: 1234 (= R$ 12.34)</item>
    ///     <item>Name [string]: payer name. ex: "Iron Bank S.A."</item>
    ///     <item>TaxID [string]: payer tax ID (CPF or CNPJ). ex: "012.345.678-90" or "20.018.183/0001-80"</item>
    ///     <item>BankCode [string]: payer bank code in Brazil. ex: "20018183" or "341"</item>
    ///     <item>BranchCode [string]: payer bank account branch. ex: "1357-9"</item>
    ///     <item>AccountNumber [string]: payer bank account number. ex: "876543-2"</item>
    ///     <item>Type [string]: Type of settlement that originated the deposit.ex: "pix" or "ted"</item>
    ///     <item>Fee [integer, default null]: fee charged for this Deposit. ex: 50 (= R$ 0.50)</item>
    ///     <item>TransactionIds [list of strings, default null]: ledger Transaction ids linked to this Deposit (if there are more than one, all but first are reversals)</item>
    ///     <item>Status [string, default null]: current Deposit status. ex: "created"</item>
    ///     <item>Tags [list of strings, default null]: list of strings for tagging. ex: ["reconciliationId", "txId"]</item>
    ///     <item>Created [DateTime, default null]: creation datetime for the Deposit. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime, default null]: latest update datetime for the Deposit. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class Deposit : Utils.Resource
    {
        public long Amount { get; }
        public string Name { get; }
        public string TaxID { get; }
        public string BankCode { get; }
        public string BranchCode { get; }
        public string AccountNumber { get; }
        public string Type { get; }
        public int? Fee { get; }
        public List<string> TransactionIds { get; }
        public string Status { get; }
        public List<string> Tags { get; }
        public DateTime Created { get; }
        public DateTime Updated { get; }

        /// <summary>
        /// Deposit object
        /// <br/>
        /// When you initialize an Deposit, the entity will not be automatically
        /// sent to the Stark Bank API. The 'create' function sends the objects
        /// to the Stark Bank API and returns the list of created objects.
        /// <br/>
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string, default null]: unique id associated with a Deposit when it is created. ex: "5656565656565656"</item>
        ///     <item>amount [long integer]: Deposit value in cents. ex: 1234 (= R$ 12.34)</item>
        ///     <item>name [string]: payer name. ex: "Iron Bank S.A."</item>
        ///     <item>taxID [string]: payer tax ID (CPF or CNPJ). ex: "012.345.678-90" or "20.018.183/0001-80"</item>
        ///     <item>bankCode [string]: payer bank code in Brazil. ex: "20018183" or "341"</item>
        ///     <item>branchCode [string]: payer bank account branch. ex: "1357-9"</item>
        ///     <item>accountNumber [string]: payer bank account number. ex: "876543-2"</item>
        ///     <item>type [string]: Type of settlement that originated the deposit.ex: "pix" or "ted"</item>
        ///     <item>fee [integer, default null]: fee charged for this Deposit. ex: 50 (= R$ 0.50)</item>
        ///     <item>transactionIds [list of strings, default null]: ledger Transaction ids linked to this Deposit (if there are more than one, all but first are reversals)</item>
        ///     <item>status [string, default null]: current Deposit status. ex: "created"</item>
        ///     <item>tags [list of strings, default null]: list of strings for tagging. ex: ["reconciliationId", "txId"]</item>
        ///     <item>created [DateTime, default null]: creation datetime for the Deposit. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime, default null]: latest update datetime for the Deposit. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public Deposit(string id, long amount, string name, string taxID, string bankCode, string branchCode, string accountNumber,
            string type, int? fee, List<string> transactionIds, string status, List<string> tags, DateTime created, DateTime updated) : base(id)
        {
            Amount = amount;
            Name = name;
            TaxID = taxID;
            BankCode = bankCode;
            BranchCode = branchCode;
            AccountNumber = accountNumber;
            Type = type;
            Fee = fee;
            TransactionIds = transactionIds;
            Status = status;
            Tags = tags;
            Created = created;
            Updated = updated;
        }

        /// <summary>
        /// Create Deposits
        /// <br/>
        /// Send a list of Deposit objects for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>Deposits [list of Deposit objects]: list of Deposit objects to be created in the API</item>
        /// <br/>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of Deposit objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<Deposit> Create(List<Deposit> deposits, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: deposits,
                user: user
            ).ToList().ConvertAll(o => (Deposit)o);
        }

        /// <summary>
        /// Retrieve a specific Deposit
        /// <br/>
        /// Receive a single Deposit object previously created in the Stark Bank API by passing its id
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
        ///     <item>Deposit object with updated attributes</item>
        /// </list>
        /// </summary>
        public static Deposit Get(string id, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as Deposit;
        }

        /// <summary>
        /// Retrieve Deposits
        /// <br/>
        /// Receive an IEnumerable of Deposit objects previously created in the Stark Bank API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "created", "paid", "canceled" or "overdue"</item>
        ///     <item>sort [string, default "-created"]: sort order considered in the response. Options are: "created" and "-created". "-" means descending order.</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of Deposit objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<Deposit> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
            string status = null, string sort = null, List<string> tags = null, List<string> ids = null, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "after", new Utils.StarkBankDate(after) },
                    { "before", new Utils.StarkBankDate(before) },
                    { "status", status },
                    { "sort", sort },
                    { "tags", tags },
                    { "ids", ids }
                },
                user: user
            ).Cast<Deposit>();
        }

        internal static (string resourceName, Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "Deposit", resourceMaker: ResourceMaker);
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
            string type = json.type;
            int? fee = json.fee;
            List<string> transactionIds = json.transactionIds.ToObject<List<string>>();
            string status = json.status;
            List<string> tags = json.tags.ToObject<List<string>>();
            string createdString = json.created;
            string updatedString = json.updated;
            DateTime created = Utils.Checks.CheckDateTime(createdString);
            DateTime updated = Utils.Checks.CheckDateTime(updatedString);

            return new Deposit(
                id: id, amount: amount, name: name, taxID: taxID, bankCode: bankCode, branchCode: branchCode,
                accountNumber: accountNumber, type: type, fee: fee, transactionIds: transactionIds, status: status, tags: tags,
                created: created, updated: updated
            );
        }
    }
}
