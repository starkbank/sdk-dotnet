using System;
using System.Linq;
using StarkBank.Utils;
using System.Collections.Generic;

namespace StarkBank
{
    /// <summary>
    /// SplitReceiver object
    /// <br/>
    /// When you initialize a SplitReceiver, the entity will not be automatically
    /// created in the Stark Bank API. The 'create' function sends the objects
    /// to the Stark Bank API and returns the list of created objects.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Name [string]: receiver full name. ex: "Anthony Edward Stark"</item>
    ///     <item>TaxID [string]: receiver account tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
    ///     <item>BankCode [string]: code of the receiver bank institution in Brazil. If an ISPB (8 digits) is informed, a PIX splitReceiver will be created, else a TED will be issued. ex: "20018183" or "341"</item>
    ///     <item>BranchCode [string]: receiver bank account branch. Use '-' in case there is a verifier digit. ex: "1357-9"</item>
    ///     <item>AccountNumber [string]: receiver bank account number. Use '-' before the verifier digit. ex: "876543-2"</item>
    ///     <item>AccountType [string]: Receiver bank account type. This parameter only has effect on Pix SplitReceivers. ex: "checking", "savings", "salary" or "payment"</item>
    ///     <item>Tags [list of strings, default []]: list of strings for reference when searching for receivers. ex: ["seller/123456"]</item>
    ///     <item>ID [string]: unique id returned when the SplitReceiver is created. ex: "5656565656565656"</item>
    ///     <item>Status [string]: current SplitReceiver status. ex: "success" or "failed"</item>
    ///     <item>Created [DateTime]: creation datetime for the SplitReceiver. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime]: latest update datetime for the SplitReceiver. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class SplitReceiver : Resource
    {
        public string Name { get; }
        public string TaxID { get; }
        public string BankCode { get; }
        public string BranchCode { get; }
        public string AccountNumber { get; }
        public string AccountType { get; }
        public List<string> Tags { get; }
        public string Status { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// SplitReceiver object
        /// <br/>
        /// When you initialize a SplitReceiver, the entity will not be automatically
        /// created in the Stark Bank API. The 'create' function sends the objects
        /// to the Stark Bank API and returns the list of created objects.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>Name [string]: receiver full name. ex: "Anthony Edward Stark"</item>
        ///     <item>TaxID [string]: receiver account tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
        ///     <item>BankCode [string]: code of the receiver bank institution in Brazil. If an ISPB (8 digits) is informed, a PIX splitReceiver will be created, else a TED will be issued. ex: "20018183" or "341"</item>
        ///     <item>BranchCode [string]: receiver bank account branch. Use '-' in case there is a verifier digit. ex: "1357-9"</item>
        ///     <item>AccountNumber [string]: receiver bank account number. Use '-' before the verifier digit. ex: "876543-2"</item>
        ///     <item>AccountType [string]: Receiver bank account type. This parameter only has effect on Pix SplitReceivers. ex: "checking", "savings", "salary" or "payment"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>Tags [list of strings, default []]: list of strings for reference when searching for receivers. ex: ["seller/123456"]</item>
        /// </list>
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>ID [string]: unique id returned when the SplitReceiver is created. ex: "5656565656565656"</item>
        ///     <item>Status [string]: current SplitReceiver status. ex: "success" or "failed"</item>
        ///     <item>Created [DateTime]: creation datetime for the SplitReceiver. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>Updated [DateTime]: latest update datetime for the SplitReceiver. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public SplitReceiver(string name, string taxID, string bankCode, string branchCode, string accountNumber, string accountType,
            List<string> tags = null, string id = null, string status = null, DateTime? created = null, DateTime? updated = null) : base(id)
        {
            Name = name;
            TaxID = taxID;
            BankCode = bankCode;
            BranchCode = branchCode;
            AccountNumber = accountNumber;
            AccountType = accountType;
            Tags = tags;
            Status = status;
            Created = created;
            Updated = updated;
        }

        internal new Dictionary<string, object> ToJson()
        {
            Dictionary<string, object> json = base.ToJson();
            return json;
        }

        /// <summary>
        /// Create SplitReceivers
        /// <br/>
        /// Send a list of SplitReceiver objects for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>SplitReceivers [list of SplitReceiver objects]: list of SplitReceiver objects to be created in the API</item>
        /// <br/>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of SplitReceiver objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<SplitReceiver> Create(List<SplitReceiver> splitReceivers, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: splitReceivers,
                user: user
            ).ToList().ConvertAll(o => (SplitReceiver)o);
        }

        /// <summary>
        /// Create SplitReceivers
        /// <br/>
        /// Send a list of SplitReceiver objects for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>splitReceivers [list of Dictionaries]: list of Dictionaries representing the SplitReceivers to be created in the API</item>
        /// <br/>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of SplitReceiver objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<SplitReceiver> Create(List<Dictionary<string, object>> splitReceivers, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: splitReceivers,
                user: user
            ).ToList().ConvertAll(o => (SplitReceiver)o);
        }

        /// <summary>
        /// Retrieve a specific SplitReceiver
        /// <br/>
        /// Receive a single SplitReceiver object previously created in the Stark Bank API by passing its id
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
        ///     <item>SplitReceiver object with updated attributes</item>
        /// </list>
        /// </summary>
        public static SplitReceiver Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as SplitReceiver;
        }

        /// <summary>
        /// Retrieve SplitReceivers
        /// <br/>
        /// Receive an IEnumerable of SplitReceiver objects previously created in the Stark Bank API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "created", "paid", "canceled" or "overdue"</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
        ///     <item>taxIds [list of strings, default null]: filter for splitReceivers sent to the specified tax IDs. ex: ["012.345.678-90", "987.654.321-00"]</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of SplitReceiver objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<SplitReceiver> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
            string status = null, List<string> tags = null, List<string> ids = null, List<string> taxIds = null, User user = null)
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
                    { "ids", ids },
                    { "taxIds", taxIds }
                },
                user: user
            ).Cast<SplitReceiver>();
        }

        /// <summary>
        /// Retrieve paged SplitReceivers
        /// <br/>
        /// Receive a list of up to 100 SplitReceiver objects previously created in the Stark Bank API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "created", "paid", "canceled" or "overdue"</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
        ///     <item>taxIds [list of strings, default null]: filter for splitReceivers sent to the specified tax IDs. ex: ["012.345.678-90", "987.654.321-00"]</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of SplitReceiver objects with updated attributes and cursor to retrieve the next page of SplitReceiver objects</item>
        /// </list>
        /// </summary>
        public static (List<SplitReceiver> page, string pageCursor) Page(string cursor = null, int? limit = null, DateTime? after = null,
            DateTime? before = null, string status = null, List<string> tags = null, List<string> ids = null,List<string> taxIds = null, 
            User user = null)
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
                    { "ids", ids },
                    { "taxIds", taxIds }
                },
                user: user
            );
            List<SplitReceiver> splitReceivers = new List<SplitReceiver>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                splitReceivers.Add(subResource as SplitReceiver);
            }
            return (splitReceivers, pageCursor);
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "SplitReceiver", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string name = json.name;
            string taxID = json.taxId;
            string bankCode = json.bankCode;
            string branchCode = json.branchCode;
            string accountNumber = json.accountNumber;
            string accountType = json.accountType;
            List<string> tags = json.tags.ToObject<List<string>>();
            string id = json.id;
            string status = json.status;
            string createdString = json.created;
            string updatedString = json.updated;
            DateTime? created = StarkCore.Utils.Checks.CheckDateTime(createdString);
            DateTime? updated = StarkCore.Utils.Checks.CheckDateTime(updatedString);
            return new SplitReceiver(name: name, taxID: taxID, bankCode: bankCode, branchCode: branchCode,
                accountNumber: accountNumber, accountType: accountType, tags: tags, id: id, status: status,
                created: created, updated: updated
            );
        }
    }
}