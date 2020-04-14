﻿using System;
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
    ///     <item>BankCode [string]: receiver 1 to 3 digits of the bank institution in Brazil. ex: "200" or "341"</item>
    ///     <item>BranchCode [string]: receiver bank account branch. Use '-' in case there is a verifier digit. ex: "1357-9"</item>
    ///     <item>AccountNumber [string]: Receiver Bank Account number. Use '-' before the verifier digit. ex: "876543-2"</item>
    ///     <item>Tags [list of strings]: list of strings for reference when searching for transfers. ex: ["employees", "monthly"]</item>
    ///     <item>ID [string, default nil]: unique id returned when Transfer is created. ex: "5656565656565656"</item>
    ///     <item>Fee [integer, default nil]: fee charged when transfer is created. ex: 200 (= R$ 2.00)</item>
    ///     <item>Status [string, default nil]: current boleto status. ex: "registered" or "paid"</item>
    ///     <item>TransactionIds [list of strings, default nil]: ledger transaction ids linked to this transfer (if there are two, second is the chargeback). ex: ["19827356981273"]</item>
    ///     <item>Created [DateTime, default nil]: creation datetime for the transfer. ex: DateTime.new(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime, default nil]: latest update datetime for the transfer. ex: DateTime.new(2020, 3, 10, 10, 30, 0, 0)</item>
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
        ///     <item>bankCode [string]: receiver 1 to 3 digits of the bank institution in Brazil. ex: "200" or "341"</item>
        ///     <item>branchCode [string]: receiver bank account branch. Use '-' in case there is a verifier digit. ex: "1357-9"</item>
        ///     <item>accountNumber [string]: Receiver Bank Account number. Use '-' before the verifier digit. ex: "876543-2"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>tags [list of strings]: list of strings for reference when searching for transfers. ex: ["employees", "monthly"]</item>
        /// </list>
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string, default nil]: unique id returned when Transfer is created. ex: "5656565656565656"</item>
        ///     <item>fee [integer, default nil]: fee charged when transfer is created. ex: 200 (= R$ 2.00)</item>
        ///     <item>status [string, default nil]: current boleto status. ex: "registered" or "paid"</item>
        ///     <item>transactionIds [list of strings, default nil]: ledger transaction ids linked to this transfer (if there are two, second is the chargeback). ex: ["19827356981273"]</item>
        ///     <item>created [DateTime, default nil]: creation datetime for the transfer. ex: DateTime.new(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime, default nil]: latest update datetime for the transfer. ex: DateTime.new(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public Transfer(long amount, string name, string taxID, string bankCode, string branchCode, string accountNumber,
            string id = null, List<string> transactionIds = null, int? fee = null, List<string> tags = null,
            string status = null, DateTime? created = null, DateTime? updated = null) : base(id)
        {
            Amount = amount;
            Name = name;
            TaxID = taxID;
            BankCode = bankCode;
            BranchCode = branchCode;
            AccountNumber = accountNumber;
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
        ///     <item>user [Project object]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
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
        ///     <item>user [Project object]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
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
        ///     <item>user [Project object]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
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
        ///     <item>limit [integer, default nil]: maximum number of objects to be retrieved. Unlimited if nil. ex: 35</item>
        ///     <item>status [string, default nil]: filter for status of retrieved objects. ex: "paid" or "registered"</item>
        ///     <item>tags [list of strings, default nil]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>ids [list of strings, default nil]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
        ///     <item>after [Date, default nil] date filter for objects created only after specified date. ex: Date.new(2020, 3, 10)</item>
        ///     <item>before [Date, default nil] date filter for objects only before specified date. ex: Date.new(2020, 3, 10)</item>
        ///     <item>user [Project object, default nil]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of Transfer objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<Transfer> Query(int? limit = null, string status = null, List<string> tags = null,
            List<string> ids = null, DateTime? after = null, DateTime? before = null, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "status", status },
                    { "tags", tags },
                    { "ids", ids },
                    { "after", after },
                    { "before", before }
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
            List<string> transactionIds = json.transactionIds.ToObject<List<string>>();
            int? fee = json.fee;
            List<string> tags = json.tags.ToObject<List<string>>();
            string status = json.status;
            string createdString = json.created;
            DateTime? created = Utils.Checks.CheckDateTime(createdString);
            string updatedString = json.updated;
            DateTime? updated = Utils.Checks.CheckDateTime(updatedString);

            return new Transfer(
                id: id, amount: amount, name: name, taxID: taxID, bankCode: bankCode, branchCode: branchCode,
                accountNumber: accountNumber, transactionIds: transactionIds, fee: fee, tags: tags, status: status,
                created: created, updated: updated
            );
        }
    }
}
