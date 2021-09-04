namespace StarkBank
{
    public partial class PaymentPreview
    {
        /// <summary>
        /// BrcodePreview object
        /// <br/>
        /// A BrcodePreview is used to get information from a BR Code you received before confirming the payment.
        /// <br/>
        /// Properties:
        /// <list>
        ///     <item>Status [string]: Payment status. ex: "active", "paid", "canceled" or "unknown"</item>
        ///     <item>Name [string]: Payment receiver name. ex: "Tony Stark"</item>
        ///     <item>TaxID [string]: Payment receiver tax ID. ex: "012.345.678-90"</item>
        ///     <item>BankCode [string]: Payment receiver bank code. ex: "20018183"</item>
        ///     <item>BranchCode [string]: Payment receiver branch code. ex: "0001"</item>
        ///     <item>AccountNumber [string]: Payment receiver account number. ex: "1234567"</item>
        ///     <item>AccountType [string]: Payment receiver account type. ex: "checking"</item>
        ///     <item>AllowChange [bool]: If True, the payment is able to receive amounts that are different from the nominal one. ex: True or False</item>
        ///     <item>Amount [long]: Value in cents that this payment is expecting to receive. If 0, any value is accepted. ex: 123 (= R$1,23)</item>
        ///     <item>NominalAmount [long]: Original value in cents that this payment was expecting to receive without the discounts, fines, etc.. If 0, any value is accepted. ex: 123 (= R$1,23)</item>
        ///     <item>InterestAmount [long]: Current interest value in cents that this payment is charging. If 0, any value is accepted. ex: 123 (= R$1,23)</item>
        ///     <item>FineAmount [long]: Current fine value in cents that this payment is charging. ex: 123 (= R$1,23)</item>
        ///     <item>ReductionAmount [long]: Current value reduction value in cents that this payment is expecting. ex: 123 (= R$1,23)</item>
        ///     <item>DiscountAmount [long]: Current discount value in cents that this payment is expecting. ex: 123 (= R$1,23)</item>
        ///     <item>ReconciliationID [string]: Reconciliation ID linked to this payment. ex: "txId", "payment-123"</item>
        /// </list>
        /// </summary>
        public class BrcodePreview : Utils.SubResource
        {
            public string Status { get; }
            public string Name { get; }
            public string TaxID { get; }
            public string BankCode { get; }
            public string BranchCode { get; }
            public string AccountNumber { get; }
            public string AccountType { get; }
            public bool AllowChange { get; }
            public long Amount { get; }
            public long NominalAmount { get; }
            public long InterestAmount { get; }
            public long FineAmount { get; }
            public long ReductionAmount { get; }
            public long DiscountAmount { get; }
            public string ReconciliationID { get; }

            /// <summary>
            /// BrcodePreview object
            /// <br/>
            /// A BrcodePreview is used to get information from a BR Code you received before confirming the payment.
            /// <br/>
            /// Attributes (return-only):
            /// <list>
            ///     <item>Status [string]: Payment status. ex: "active", "paid", "canceled" or "unknown"</item>
            ///     <item>Name [string]: Payment receiver name. ex: "Tony Stark"</item>
            ///     <item>TaxID [string]: Payment receiver tax ID. ex: "012.345.678-90"</item>
            ///     <item>BankCode [string]: Payment receiver bank code. ex: "20018183"</item>
            ///     <item>BranchCode [string]: Payment receiver branch code. ex: "0001"</item>
            ///     <item>AccountNumber [string]: Payment receiver account number. ex: "1234567"</item>
            ///     <item>AccountType [string]: Payment receiver account type. ex: "checking"</item>
            ///     <item>AllowChange [bool]: If True, the payment is able to receive amounts that are different from the nominal one. ex: True or False</item>
            ///     <item>Amount [long]: Value in cents that this payment is expecting to receive. If 0, any value is accepted. ex: 123 (= R$1,23)</item>
            ///     <item>NominalAmount [long]: Original value in cents that this payment was expecting to receive without the discounts, fines, etc.. If 0, any value is accepted. ex: 123 (= R$1,23)</item>
            ///     <item>InterestAmount [long]: Current interest value in cents that this payment is charging. If 0, any value is accepted. ex: 123 (= R$1,23)</item>
            ///     <item>FineAmount [long]: Current fine value in cents that this payment is charging. ex: 123 (= R$1,23)</item>
            ///     <item>ReductionAmount [long]: Current value reduction value in cents that this payment is expecting. ex: 123 (= R$1,23)</item>
            ///     <item>DiscountAmount [long]: Current discount value in cents that this payment is expecting. ex: 123 (= R$1,23)</item>
            ///     <item>ReconciliationID [string]: Reconciliation ID linked to this payment. ex: "txId", "payment-123"</item>
            /// </list>
            /// </summary>
            public BrcodePreview(string status, string name, string taxID, string bankCode,
                string branchCode, string accountNumber, string accountType, bool allowChange,
                long amount, long nominalAmount, long interestAmount, long fineAmount,
                long reductionAmount, long discountAmount, string reconciliationID)
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
                NominalAmount = nominalAmount;
                InterestAmount = interestAmount;
                FineAmount = fineAmount;
                ReductionAmount = reductionAmount;
                DiscountAmount = discountAmount;
                ReconciliationID = reconciliationID;
            }

            internal static (string resourceName, Utils.Api.ResourceMaker resourceMaker) SubResource()
            {
                return (resourceName: "BrcodePreview", resourceMaker: ResourceMaker);
            }

            public static Utils.SubResource ResourceMaker(dynamic json)
            {
                string status = json.status;
                string name = json.name;
                string taxID = json.taxId;
                string bankCode = json.bankCode;
                string branchCode = json.branchCode;
                string accountNumber = json.accountNumber;
                string accountType = json.accountType;
                bool allowChange = json.allowChange;
                long amount = json.amount;
                long nominalAmount = json.nominalAmount;
                long interestAmount = json.interestAmount;
                long fineAmount = json.fineAmount;
                long reductionAmount = json.reductionAmount;
                long discountAmount = json.discountAmount;
                string reconciliationID = json.reconciliationId;

                return new BrcodePreview(
                    status: status, name: name, taxID: taxID, bankCode: bankCode, branchCode: branchCode,
                    accountNumber: accountNumber, accountType: accountType, allowChange: allowChange, amount: amount,
                    nominalAmount: nominalAmount, interestAmount: interestAmount, fineAmount: fineAmount,
                    reductionAmount: reductionAmount, discountAmount: discountAmount, reconciliationID: reconciliationID
                );
            }
        }
    }
}
