using System;
using System.Linq;
using StarkBank.Utils;
using System.Collections.Generic;

namespace StarkBank
{
    /// <summary>
    /// VerifiedAccount object
    /// <br/>
    /// When you initialize a VerifiedAccount, the entity will not be automatically
    /// created in the Stark Bank API. The 'create' function sends the objects
    /// to the Stark Bank API and returns the list of created objects.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>TaxID [string]: receiver tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
    ///     <item>BankCode [string]: code of the receiver bank institution in Brazil. If an ISPB (8 digits) is informed, a Pix transfer will be created, else a TED will be issued. The BankCode parameter is required if verifying with bank details. ex: "20018183" or "341"</item>
    ///     <item>BranchCode [string]: receiver bank account branch. Use '-' in case there is a verifier digit. ex: "1357-9". The BranchCode parameter is required if verifying with bank details.</item>
    ///     <item>KeyID [string]: pix key identifier. ex: "tony@starkbank.com", "012.345.678-90". The KeyID parameter is required if verifying with Pix key.</item>
    ///     <item>Name [string]: receiver full name. ex: "Anthony Edward Stark". The Name parameter is required if verifying with bank details.</item>
    ///     <item>Number [string]: receiver bank account number. Use '-' before the verifier digit. ex: "876543-2". The Number parameter is required if verifying with bank details.</item>
    ///     <item>Type [string]: verified account type. ex: "checking", "savings", "salary" or "payment". The Type parameter is required if verifying with bank details.</item>
    ///     <item>Tags [list of strings, default []]: list of strings for reference when searching for verified accounts. ex: ["employees", "monthly"]</item>
    ///     <item>ID [string]: unique id returned when the VerifiedAccount is created. ex: "5656565656565656"</item>
    ///     <item>BankName [string]: bank name associated with the verified account. ex: "Stark Bank"</item>
    ///     <item>Status [string]: current verified account status. ex: "creating", "created", "processing", "active", "failed" or "canceled"</item>
    ///     <item>Created [DateTime]: creation datetime for the verified account. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime]: update datetime for the verified account. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class VerifiedAccount : Resource
    {
        public string TaxID { get; }
        public string BankCode { get; }
        public string BranchCode { get; }
        public string KeyID { get; }
        public string Name { get; }
        public string Number { get; }
        public string Type { get; }
        public List<string> Tags { get; }
        public string BankName { get; }
        public string Status { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// VerifiedAccount object
        /// <br/>
        /// When you initialize a VerifiedAccount, the entity will not be automatically
        /// created in the Stark Bank API. The 'create' function sends the objects
        /// to the Stark Bank API and returns the list of created objects.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>taxId [string]: receiver tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
        /// </list>
        /// <br/>
        /// Parameters (conditionally required):
        /// <list>
        ///     <item>bankCode [string]: code of the receiver bank institution in Brazil. If an ISPB (8 digits) is informed, a Pix transfer will be created, else a TED will be issued. The bankCode parameter is required if verifying with bank details. ex: "20018183" or "341"</item>
        ///     <item>branchCode [string]: receiver bank account branch. Use '-' in case there is a verifier digit. ex: "1357-9". The branchCode parameter is required if verifying with bank details.</item>
        ///     <item>keyId [string]: pix key identifier. ex: "tony@starkbank.com", "012.345.678-90". The keyId parameter is required if verifying with Pix key.</item>
        ///     <item>name [string]: receiver full name. ex: "Anthony Edward Stark". The name parameter is required if verifying with bank details.</item>
        ///     <item>number [string]: receiver bank account number. Use '-' before the verifier digit. ex: "876543-2". The number parameter is required if verifying with bank details.</item>
        ///     <item>type [string]: verified account type. ex: "checking", "savings", "salary" or "payment". The type parameter is required if verifying with bank details.</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>tags [list of strings, default []]: list of strings for reference when searching for verified accounts. ex: ["employees", "monthly"]</item>
        /// </list>
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when the VerifiedAccount is created. ex: "5656565656565656"</item>
        ///     <item>bankName [string]: bank name associated with the verified account. ex: "Stark Bank"</item>
        ///     <item>status [string]: current verified account status. ex: "creating", "created", "processing", "active", "failed" or "canceled"</item>
        ///     <item>created [DateTime]: creation datetime for the verified account. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime]: update datetime for the verified account. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public VerifiedAccount(
            string taxId, string bankCode = null, string branchCode = null, string keyId = null,
            string name = null, string number = null, string type = null, List<string> tags = null,
            string id = null, string bankName = null, string status = null, DateTime? created = null,
            DateTime? updated = null
        ) : base(id)
        {
            TaxID = taxId;
            BankCode = bankCode;
            BranchCode = branchCode;
            KeyID = keyId;
            Name = name;
            Number = number;
            Type = type;
            Tags = tags;
            BankName = bankName;
            Status = status;
            Created = created;
            Updated = updated;
        }

        /// <summary>
        /// Create VerifiedAccounts
        /// <br/>
        /// Send a list of VerifiedAccount objects for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>accounts [list of VerifiedAccount objects]: list of VerifiedAccount objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of VerifiedAccount objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<VerifiedAccount> Create(List<VerifiedAccount> accounts, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: accounts,
                user: user
            ).ToList().ConvertAll(o => (VerifiedAccount)o);
        }

        /// <summary>
        /// Retrieve a specific VerifiedAccount
        /// <br/>
        /// Receive a single VerifiedAccount object previously created in the Stark Bank API by passing its id
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
        ///     <item>VerifiedAccount object with updated attributes</item>
        /// </list>
        /// </summary>
        public static VerifiedAccount Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as VerifiedAccount;
        }

        /// <summary>
        /// Cancel a VerifiedAccount entity
        /// <br/>
        /// Cancel a VerifiedAccount entity previously created in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: VerifiedAccount unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>canceled VerifiedAccount object</item>
        /// </list>
        /// </summary>
        public static VerifiedAccount Cancel(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.DeleteId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as VerifiedAccount;
        }

        /// <summary>
        /// Retrieve VerifiedAccounts
        /// <br/>
        /// Receive an IEnumerable of VerifiedAccount objects previously created in the Stark Bank API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created or updated only after specified date. ex: new DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created or updated only before specified date. ex: new DateTime(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "creating", "created", "processing", "active", "failed" or "canceled"</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of VerifiedAccount objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<VerifiedAccount> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
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
            ).Cast<VerifiedAccount>();
        }

        /// <summary>
        /// Retrieve paged VerifiedAccounts
        /// <br/>
        /// Receive a list of up to 100 VerifiedAccount objects previously created in the Stark Bank API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created or updated only after specified date. ex: new DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created or updated only before specified date. ex: new DateTime(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "creating", "created", "processing", "active", "failed" or "canceled"</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of VerifiedAccount objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of VerifiedAccount objects</item>
        /// </list>
        /// </summary>
        public static (List<VerifiedAccount> page, string pageCursor) Page(string cursor = null, int? limit = null, DateTime? after = null,
            DateTime? before = null, string status = null, List<string> tags = null, List<string> ids = null, User user = null)
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
            List<VerifiedAccount> accounts = new List<VerifiedAccount>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                accounts.Add(subResource as VerifiedAccount);
            }
            return (accounts, pageCursor);
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "VerifiedAccount", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            string taxId = json.taxId;
            string bankCode = json.bankCode;
            string branchCode = json.branchCode;
            string keyId = json.keyId;
            string name = json.name;
            string number = json.number;
            string type = json.type;
            List<string> tags = json.tags?.ToObject<List<string>>();
            string bankName = json.bankName;
            string status = json.status;
            string createdString = json.created;
            DateTime? created = StarkCore.Utils.Checks.CheckNullableDateTime(createdString);
            string updatedString = json.updated;
            DateTime? updated = StarkCore.Utils.Checks.CheckNullableDateTime(updatedString);

            return new VerifiedAccount(
                id: id, taxId: taxId, bankCode: bankCode, branchCode: branchCode, keyId: keyId,
                name: name, number: number, type: type, tags: tags, bankName: bankName,
                status: status, created: created, updated: updated
            );
        }
    }
}
