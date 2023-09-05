using System;
using System.Linq;
using System.Collections.Generic;
using StarkBank.Utils;


namespace StarkBank
{
    /// <summary>
    /// DictKey object
    /// <br/>
    /// DictKey represents a Pix key registered in Bacens DICT system.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item> ID [string]: DictKey object unique id and DICT key itself. ex: "tony@starkbank.com", "722.461.430-04", "20.018.183/0001-80", "+5511988887777", "b6295ee1-f054-47d1-9e90-ee57b74f60d9"</item>
    ///     <item> Type [string]: DICT key type. ex: "email", "cpf", "cnpj", "phone" or "evp"</item>
    ///     <item> Name [string]: account owner full name. ex: "Tony Stark"</item>
    ///     <item> TaxId [string]: tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
    ///     <item> OwnerType [string]: DICT key owner type. ex "naturalPerson" or "legalPerson"</item>
    ///     <item> BankName [string]: bank name associated with the DICT key. ex: "Stark Bank"</item>
    ///     <item> Ispb [string]: ISPB code used for transactions. ex: "20018183"</item>
    ///     <item> BranchCode [string]: bank account branch code associated with the DICT key. ex: "9585"</item>
    ///     <item> AccountNumber [string]: bank account number associated with the DICT key. ex: "9828282578010513"</item>
    ///     <item> AccountType [string]: bank account type associated with the DICT key. ex: "checking", "saving", "salary" or "payment"</item>
    ///     <item> Status [string]: current DICT key status. ex: "created", "registered", "canceled" or "failed"</item>
    /// </list>
    /// </summary>
    public partial class DictKey : Utils.Resource
    {
        public string Type { get; }
        public string Name { get; }
        public string TaxId { get; }
        public string OwnerType { get; }
        public string BankName { get; }
        public string Ispb { get; }
        public string BranchCode { get; }
        public string AccountNumber { get; }
        public string AccountType { get; }
        public string Status { get; }

        /// <summary>
        /// DictKey object
        /// <br/>
        /// DictKey represents a Pix key registered in Bacen"s DICT system.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item> id [string]: DictKey object unique id and Pix key itself. ex: "tony@starkbank.com", "722.461.430-04", "20.018.183/0001-80", "+5511988887777", "b6295ee1-f054-47d1-9e90-ee57b74f60d9"</item>
        /// Attributes (return-only):
        /// <list>
        ///     <item> type [string]: Pix key type. ex: "email", "cpf", "cnpj", "phone" or "evp"</item>
        ///     <item> name [string]: account owner full name. ex: "Tony Stark"</item>
        ///     <item> taxId [string]: tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
        ///     <item> ownerType [string]: Pix key owner type. ex "naturalPerson" or "legalPerson"</item>
        ///     <item> bankName [string]: bank name associated with the DICT key. ex: "Stark Bank"</item>
        ///     <item> ispb [string]: ISPB code used for transactions. ex: "20018183"</item>
        ///     <item> branchCode [string]: bank account branch code associated with the Pix key. ex: "9585"</item>
        ///     <item> accountNumber [string]: bank account number associated with the Pix key. ex: "9828282578010513"</item>
        ///     <item> accountType [string]: bank account type associated with the Pix key. ex: "checking", "saving" e "salary"</item>
        ///     <item> status [string]: current Pix key status. ex: "created", "registered", "canceled" or "failed"</item>
        /// </list>
        /// </summary>
        public DictKey(string id = null, string type = null, string name = null, string taxId = null, string ownerType = null, string bankName = null,
                    string ispb = null, string branchCode = null, string accountNumber = null, string accountType = null, string status = null,
                    string accountCreated = null, string owned = null, string created = null) : base(id)
        {
            Type = type;
            Name = name;
            TaxId = taxId;
            OwnerType = ownerType;
            BankName = bankName;
            Ispb = ispb;
            BranchCode = branchCode;
            AccountNumber = accountNumber;
            AccountType = accountType;
            Status = status;
        }

        /// <summary>
        /// Retrieve a specific DictKey
        /// <br/>
        /// Receive a single DictKey object by passing its id
        /// <br/>
        /// Parameters(required):
        /// <list>
        ///     <item> id [string]: DictKey object unique id and Pix key itself. ex: "tony@starkbank.com", "722.461.430-04", "20.018.183/0001-80", "+5511988887777", "b6295ee1-f054-47d1-9e90-ee57b74f60d9"
        /// </list>
        /// <br/>
        /// Parameters(optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>DictKey object with updated attributes</item>
        /// </list>
        /// </summary>
        public static DictKey Get(string id, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as DictKey;
        }

        /// <summary>
        /// Retrieve DictKeys
        /// <br/>
        /// Receive an IEnumerable of DictKey objects associated with your Stark Bank Workspace
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>type [string, default null]: DictKey type.ex: "cpf", "cnpj", "phone", "email" or "evp"<item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "created", "paid", "canceled" or "overdue"</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of DictKey objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<DictKey> Query(int? limit = null, string type = null, DateTime? after = null,
            DateTime? before = null, List<string> ids = null, string status = null, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "type", type },
                    { "after", new Utils.StarkDate(after) },
                    { "before", new Utils.StarkDate(before) },
                    { "ids", ids },
                    { "status", status }
                },
                user: user
            ).Cast<DictKey>();
        }

        /// <summary>
        /// Retrieve paged DictKeys
        /// <br/>
        /// Receive a list of up to 100 DictKeys objects previously created in the Stark Bank API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>type [string, default null]: DictKey type.ex: "cpf", "cnpj", "phone", "email" or "evp"<item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "created", "paid", "canceled" or "overdue"</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of DictKey objects with updated attributes and cursor to retrieve the next page of DictKey objects</item>
        /// </list>
        /// </summary>
        public static (List<DictKey> page, string pageCursor) Page(string cursor = null, int? limit = null, string type = null,
            DateTime? after = null, DateTime? before = null, List<string> ids = null, string status = null, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            (List<SubResource> page, string pageCursor) = Utils.Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "limit", limit },
                    { "type", type },
                    { "after", new Utils.StarkDate(after) },
                    { "before", new Utils.StarkDate(before) },
                    { "ids", ids },
                    { "status", status }
                },
                user: user
            );
            List<DictKey> dictKeys = new List<DictKey>();
            foreach (SubResource subResource in page)
            {
                dictKeys.Add(subResource as DictKey);
            }
            return (dictKeys, pageCursor);
        }

        internal static (string resourceName, Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "DictKey", resourceMaker: ResourceMaker);
        }

        internal static Utils.Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            string type = json.type;
            string name = json.name;
            string taxId = json.taxId;
            string ownerType = json.ownerType;
            string bankName = json.bankName;
            string ispb = json.ispb;
            string branchCode = json.branchCode;
            string accountNumber = json.accountNumber;
            string accountType = json.accountType;
            string status = json.status;

            return new DictKey(
                id: id, type: type, accountCreated: accountCreated, accountType: accountType, name: name, taxId: taxId,
                ownerType: ownerType, bankName: bankName, ispb: ispb, branchCode: branchCode, status: status
            );
        }
    }
}
