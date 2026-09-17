using System;
using System.Linq;
using StarkBank.Utils;
using System.Collections.Generic;

namespace StarkBank
{
    /// <summary>
    /// VerifiedTransfer object
    /// <br/>
    /// When you initialize a VerifiedTransfer, the entity will not be automatically
    /// created in the Stark Bank API. The 'create' function sends the objects
    /// to the Stark Bank API and returns the list of created objects.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Amount [long integer]: transfer value in cents. ex: 1234 (= R$ 12.34)</item>
    ///     <item>AccountID [string]: receiver's VerifiedAccount ID. ex: "5656565656565656"</item>
    ///     <item>AccountType [string, default "checking"]: receiver bank account type. This parameter only has effect on Pix Transfers. ex: "checking", "savings", "salary" or "payment"</item>
    ///     <item>ExternalID [string, default null]: url safe string that must be unique among all your transfers. Duplicated externalIds will cause failures. By default, this parameter will block any transfer that repeats amount and receiver information on the same date. ex: "my-internal-id-123456"</item>
    ///     <item>Scheduled [DateTime, default now]: date or datetime when the transfer will be processed. May be pushed to next business day if necessary. ex: new DateTime(2020, 11, 12, 0, 14, 22, 0)</item>
    ///     <item>Description [string, default null]: optional description to override default description to be shown in the bank statement. ex: "Payment for service #1234"</item>
    ///     <item>DisplayDescription [string, default null]: optional description to be shown in the receiver bank interface. ex: "Payment for service #1234"</item>
    ///     <item>Tags [list of strings, default []]: list of strings for reference when searching for verified transfers. ex: ["employees", "monthly"]</item>
    ///     <item>Rules [list of StarkBank.Transfer.Rule objects, default null]: list of Transfer.Rule objects for modifying transfer behaviour. ex: [new StarkBank.Transfer.Rule("resendingLimit", 5)]</item>
    ///     <item>ID [string]: unique id returned when the VerifiedTransfer is created. ex: "5656565656565656"</item>
    ///     <item>Fee [integer]: fee charged when the transfer is created. ex: 200 (= R$ 2.00)</item>
    ///     <item>Status [string]: current verified transfer status. ex: "created", "processing", "success" or "failed"</item>
    ///     <item>TransactionIds [list of strings]: ledger transaction ids linked to this transfer (if there are two, second is the chargeback). ex: ["19827356981273"]</item>
    ///     <item>Metadata [Dictionary object]: dictionary object used to store additional information about the VerifiedTransfer object.</item>
    ///     <item>Created [DateTime]: creation datetime for the verified transfer. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime]: update datetime for the verified transfer. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class VerifiedTransfer : Resource
    {
        public long Amount { get; }
        public string AccountID { get; }
        public string AccountType { get; }
        public string ExternalID { get; }
        public DateTime? Scheduled { get; }
        public string Description { get; }
        public string DisplayDescription { get; }
        public List<string> Tags { get; }
        public List<Transfer.Rule> Rules { get; }
        public int? Fee { get; }
        public string Status { get; }
        public List<string> TransactionIds { get; }
        public Dictionary<string, object> Metadata { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// VerifiedTransfer object
        /// <br/>
        /// When you initialize a VerifiedTransfer, the entity will not be automatically
        /// created in the Stark Bank API. The 'create' function sends the objects
        /// to the Stark Bank API and returns the list of created objects.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>amount [long integer]: transfer value in cents. ex: 1234 (= R$ 12.34)</item>
        ///     <item>accountId [string]: receiver's VerifiedAccount ID. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>accountType [string, default "checking"]: receiver bank account type. This parameter only has effect on Pix Transfers. ex: "checking", "savings", "salary" or "payment"</item>
        ///     <item>externalId [string, default null]: url safe string that must be unique among all your transfers. Duplicated externalIds will cause failures. By default, this parameter will block any transfer that repeats amount and receiver information on the same date. ex: "my-internal-id-123456"</item>
        ///     <item>scheduled [DateTime, default now]: date or datetime when the transfer will be processed. May be pushed to next business day if necessary. ex: new DateTime(2020, 11, 12, 0, 14, 22, 0)</item>
        ///     <item>description [string, default null]: optional description to override default description to be shown in the bank statement. ex: "Payment for service #1234"</item>
        ///     <item>displayDescription [string, default null]: optional description to be shown in the receiver bank interface. ex: "Payment for service #1234"</item>
        ///     <item>tags [list of strings, default []]: list of strings for reference when searching for verified transfers. ex: ["employees", "monthly"]</item>
        ///     <item>rules [list of StarkBank.Transfer.Rule objects, default null]: list of Transfer.Rule objects for modifying transfer behaviour. ex: [new StarkBank.Transfer.Rule("resendingLimit", 5)]</item>
        /// </list>
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when the VerifiedTransfer is created. ex: "5656565656565656"</item>
        ///     <item>fee [integer]: fee charged when the transfer is created. ex: 200 (= R$ 2.00)</item>
        ///     <item>status [string]: current verified transfer status. ex: "created", "processing", "success" or "failed"</item>
        ///     <item>transactionIds [list of strings]: ledger transaction ids linked to this transfer (if there are two, second is the chargeback). ex: ["19827356981273"]</item>
        ///     <item>metadata [Dictionary object]: dictionary object used to store additional information about the VerifiedTransfer object.</item>
        ///     <item>created [DateTime]: creation datetime for the verified transfer. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime]: update datetime for the verified transfer. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public VerifiedTransfer(
            long amount, string accountId, string accountType = null, string externalId = null,
            DateTime? scheduled = null, string description = null, string displayDescription = null,
            List<string> tags = null, List<Transfer.Rule> rules = null, string id = null, int? fee = null,
            string status = null, List<string> transactionIds = null, Dictionary<string, object> metadata = null,
            DateTime? created = null, DateTime? updated = null
        ) : base(id)
        {
            Amount = amount;
            AccountID = accountId;
            AccountType = accountType;
            ExternalID = externalId;
            Scheduled = scheduled;
            Description = description;
            DisplayDescription = displayDescription;
            Tags = tags;
            Rules = rules;
            Fee = fee;
            Status = status;
            TransactionIds = transactionIds;
            Metadata = metadata;
            Created = created;
            Updated = updated;
        }

        /// <summary>
        /// Create VerifiedTransfers
        /// <br/>
        /// Send a list of VerifiedTransfer objects for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>transfers [list of VerifiedTransfer objects]: list of VerifiedTransfer objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of VerifiedTransfer objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<VerifiedTransfer> Create(List<VerifiedTransfer> transfers, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: transfers,
                user: user
            ).ToList().ConvertAll(o => (VerifiedTransfer)o);
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "VerifiedTransfer", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            long amount = json.amount;
            string accountId = json.accountId;
            string accountType = json.accountType;
            string externalId = json.externalId;
            string scheduledString = json.scheduled;
            DateTime? scheduled = StarkCore.Utils.Checks.CheckNullableDateTime(scheduledString);
            string description = json.description;
            string displayDescription = json.displayDescription;
            List<string> tags = json.tags?.ToObject<List<string>>();
            List<Transfer.Rule> rules = ParseRule(json.rules);
            int? fee = json.fee;
            string status = json.status;
            List<string> transactionIds = json.transactionIds?.ToObject<List<string>>();
            Dictionary<string, object> metadata = json.metadata?.ToObject<Dictionary<string, object>>();
            string createdString = json.created;
            DateTime? created = StarkCore.Utils.Checks.CheckNullableDateTime(createdString);
            string updatedString = json.updated;
            DateTime? updated = StarkCore.Utils.Checks.CheckNullableDateTime(updatedString);

            return new VerifiedTransfer(
                id: id, amount: amount, accountId: accountId, accountType: accountType, externalId: externalId,
                scheduled: scheduled, description: description, displayDescription: displayDescription, tags: tags,
                rules: rules, fee: fee, status: status, transactionIds: transactionIds, metadata: metadata,
                created: created, updated: updated
            );
        }

        private static List<Transfer.Rule> ParseRule(dynamic json)
        {
            if (json is null) return null;

            List<Transfer.Rule> rules = new List<Transfer.Rule>();

            foreach (dynamic rule in json)
            {
                rules.Add((Transfer.Rule)Transfer.Rule.ResourceMaker(rule));
            }
            return rules;
        }
    }
}
