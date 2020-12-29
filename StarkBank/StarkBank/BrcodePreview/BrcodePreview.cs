using System.Linq;
using System.Collections.Generic;


namespace StarkBank
{
    /// <summary>
    /// BrcodePreview object
    /// <br/>
    /// A BrcodePreview is used to get information from a BR Code you received to check the informations before paying it.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Status [string]: Payment status. ex: "active", "paid", "canceled" or "unknown"</item>
    ///     <item>Name [string]: Payment receiver name.ex: "Tony Stark"</item>
    ///     <item>TaxID [string]: Payment receiver tax ID.ex: "012.345.678-90"</item>
    ///     <item>BankCode [string]: Payment receiver bank code. ex: "20018183"</item>
    ///     <item>BranchCode [string]: Payment receiver branch code. ex: "0001"</item>
    ///     <item>AccountNumber [string]: Payment receiver account number. ex: "1234567"</item>
    ///     <item>AccountType [string]: Payment receiver account type. ex: "checking"</item>
    ///     <item>AllowChange [string]: If True, the payment is able to receive amounts that are diferent from the nominal one. ex: true or false</item>
    ///     <item>Amount [integer]: Value in cents that this payment is expecting to receive.If 0, any value is accepted.ex: 123 (= R$1,23)</item>
    ///     <item>ReconciliationID [string]: Reconciliation ID linked to this payment.ex: "txId", "payment-123"</item>
    /// </list>
    /// </summary>
    public partial class BrcodePreview : Utils.Resource
    {
        public string Status { get; }
        public string Name { get; }
        public string TaxID { get; }
        public string BankCode { get; }
        public string BranchCode { get; }
        public string AccountNumber { get; }
        public string AccountType { get; }
        public string AllowChange { get; }
        public long? Amount { get; }
        public string ReconciliationID { get; }

        /// <summary>
        /// BrcodePreview object
        /// <br/>
        /// A BrcodePreview is used to get information from a BR Code you received to check the informations before paying it.
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>status [string]: Payment status. ex: "active", "paid", "canceled" or "unknown"</item>
        ///     <item>name [string]: Payment receiver name.ex: "Tony Stark"</item>
        ///     <item>taxID [string]: Payment receiver tax ID.ex: "012.345.678-90"</item>
        ///     <item>bankCode [string]: Payment receiver bank code. ex: "20018183"</item>
        ///     <item>branchCode [string]: Payment receiver branch code. ex: "0001"</item>
        ///     <item>accountNumber [string]: Payment receiver account number. ex: "1234567"</item>
        ///     <item>accountType [string]: Payment receiver account type. ex: "checking"</item>
        ///     <item>allowChange [string]: If True, the payment is able to receive amounts that are diferent from the nominal one. ex: true or false</item>
        ///     <item>amount [integer]: Value in cents that this payment is expecting to receive.If 0, any value is accepted.ex: 123 (= R$1,23)</item>
        ///     <item>reconciliationID [string]: Reconciliation ID linked to this payment.ex: "txId", "payment-123"</item>
        /// </list>
        /// </summary>
        public BrcodePreview(string status, string name, string taxID, string bankCode, string branchCode,
            string accountNumber, string accountType, string allowChange, long amount, string reconciliationID, string id
            ) : base(id)
        {
            Status = status;
            Name = name;
            TaxID = taxID;
            BankCode = bankCode;
            BranchCode = branchCode;
            AccountNumber = accountNumber;
            AccountType = accountType;
            AllowChange = allowChange;
            Amount = amount;
            ReconciliationID = reconciliationID;
        }

    /// <summary>
    /// Retrieve BrcodePreviews
    /// <br/>
    /// Receive an IEnumerable of BrcodePreview objects previously created in the Stark Bank API
    /// <br/>
    /// Parameters (optional):
    /// <list>
    ///     <item>brcodes [list of strings]: List of brcodes to preview. ex: ["00020126580014br.gov.bcb.pix0136a629532e-7693-4846-852d-1bbff817b5a8520400005303986540510.005802BR5908T'Challa6009Sao Paulo62090505123456304B14A"]</item>
    ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
    /// </list>
    /// <br/>
    /// Return:
    /// <list>
    ///     <item>IEnumerable of BrcodePreview objects with updated attributes</item>
    /// </list>
    /// </summary>
    public static IEnumerable<BrcodePreview> Query(List<string> brcodes = null, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", null },
                    { "brcodes", brcodes }
                },
                user: user
            ).Cast<BrcodePreview>();
        }

        internal static (string resourceName, Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "BrcodePreview", resourceMaker: ResourceMaker);
        }

        internal static Utils.Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            string status = json.status;
            string name = json.name;
            string taxID = json.taxId;
            string bankCode = json.bankCode;
            string branchCode = json.branchCode;
            string accountNumber = json.accountNumber;
            string accountType = json.accountType;
            string allowChange = json.allowChange;
            long amount = json.amount;
            string reconciliationID = json.reconciliationId;

            return new BrcodePreview(
                id: id, status: status, name: name, taxID: taxID, bankCode: bankCode, branchCode: branchCode,
                accountNumber: accountNumber, accountType: accountType, allowChange: allowChange,
                amount: amount, reconciliationID: reconciliationID
            );
        }
    }
}
