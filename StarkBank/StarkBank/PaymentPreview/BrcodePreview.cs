using StarkCore;
using StarkBank.Utils;

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
        ///     <item>AccountType [string]: Payment receiver account type. ex: "checking"</item>
        ///     <item>AllowChange [bool]: If True, the payment is able to receive amounts that are different from the nominal one. ex: True or False</item>
        ///     <item>Amount [long]: Value in cents that this payment is expecting to receive. If 0, any value is accepted. ex: 123 (= R$1,23)</item>
        ///     <item>NominalAmount [long]: Original value in cents that this payment was expecting to receive without the discounts, fines, etc.. If 0, any value is accepted. ex: 123 (= R$1,23)</item>
        ///     <item>InterestAmount [long]: Current interest value in cents that this payment is charging. If 0, any value is accepted. ex: 123 (= R$1,23)</item>
        ///     <item>FineAmount [long]: Current fine value in cents that this payment is charging. ex: 123 (= R$1,23)</item>
        ///     <item>ReductionAmount [long]: Current value reduction value in cents that this payment is expecting. ex: 123 (= R$1,23)</item>
        ///     <item>DiscountAmount [long]: Current discount value in cents that this payment is expecting. ex: 123 (= R$1,23)</item>
        ///     <item>ReconciliationID [string]: Reconciliation ID linked to this payment. ex: "txId", "payment-123"</item>
        ///     <item>CashAmount [long]: Amount to be withdrawn from the cashier in cents. Example: 1000 (= R$ 10.00)</item>
        ///     <item>CashierBankCode [string]: Cashier's bank code. Example: "20018183"</item>
        ///     <item>CashierType [string]: Cashier's type. Options: "merchant", "participant", and "other"</item>
        ///     <item>Description [string]: Additional information presented in statement.</item>
        ///     <item>KeyId [string]: Receiver's Pix Key id. Can be a taxId (CPF/CNPJ), a phone number, an email or an alphanumeric sequence (EVP). ex: "+5511989898989"</item>
        /// </list>
        /// </summary>
        public class BrcodePreview : StarkCore.Utils.SubResource
        {
            public string Status { get; }
            public string Name { get; }
            public string TaxID { get; }
            public string BankCode { get; }
            public string AccountType { get; }
            public bool AllowChange { get; }
            public long Amount { get; }
            public long NominalAmount { get; }
            public long InterestAmount { get; }
            public long FineAmount { get; }
            public long ReductionAmount { get; }
            public long DiscountAmount { get; }
            public string ReconciliationID { get; }
            public long CashAmount { get; }
            public string CashierBankCode { get; }
            public string CashierType { get; }
            public string Description { get; }
            public string KeyId { get; }

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
            ///     <item>AccountType [string]: Payment receiver account type. ex: "checking"</item>
            ///     <item>AllowChange [bool]: If True, the payment is able to receive amounts that are different from the nominal one. ex: True or False</item>
            ///     <item>Amount [long]: Value in cents that this payment is expecting to receive. If 0, any value is accepted. ex: 123 (= R$1,23)</item>
            ///     <item>NominalAmount [long]: Original value in cents that this payment was expecting to receive without the discounts, fines, etc.. If 0, any value is accepted. ex: 123 (= R$1,23)</item>
            ///     <item>InterestAmount [long]: Current interest value in cents that this payment is charging. If 0, any value is accepted. ex: 123 (= R$1,23)</item>
            ///     <item>FineAmount [long]: Current fine value in cents that this payment is charging. ex: 123 (= R$1,23)</item>
            ///     <item>ReductionAmount [long]: Current value reduction value in cents that this payment is expecting. ex: 123 (= R$1,23)</item>
            ///     <item>DiscountAmount [long]: Current discount value in cents that this payment is expecting. ex: 123 (= R$1,23)</item>
            ///     <item>ReconciliationID [string]: Reconciliation ID linked to this payment. ex: "txId", "payment-123"</item>
            ///     <item>CashAmount [long]: Amount to be withdrawn from the cashier in cents. Example: 1000 (= R$ 10.00)</item>
            ///     <item>CashierBankCode [string]: Cashier's bank code. Example: "20018183"</item>
            ///     <item>CashierType [string]: Cashier's type. Options: "merchant", "participant", and "other"</item>
            ///     <item>Description [string]: Additional information presented in statement.</item>
            ///     <item>KeyId [string]: Receiver's Pix Key id. Can be a taxId (CPF/CNPJ), a phone number, an email or an alphanumeric sequence (EVP). ex: "+5511989898989"</item>
            /// </list>
            /// </summary>
            public BrcodePreview(string status, string name, string taxID, string bankCode, string accountType,
                bool allowChange, long amount, long nominalAmount, long interestAmount, long fineAmount,
                long reductionAmount, long discountAmount, string reconciliationID, long cashAmount,
                string cashierBankCode, string cashierType, string description, string keyId
            )
            {
                Status = status;
                Name = name;
                TaxID = taxID;
                BankCode = bankCode;
                AccountType = accountType;
                AllowChange = allowChange;
                Amount = amount;
                NominalAmount = nominalAmount;
                InterestAmount = interestAmount;
                FineAmount = fineAmount;
                ReductionAmount = reductionAmount;
                DiscountAmount = discountAmount;
                ReconciliationID = reconciliationID;
                CashAmount = cashAmount;
                CashierBankCode = cashierBankCode;
                CashierType = cashierType;
                Description = description;
                KeyId = keyId;
            }

            internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) SubResource()
            {
                return (resourceName: "BrcodePreview", resourceMaker: ResourceMaker);
            }

            public static StarkCore.Utils.SubResource ResourceMaker(dynamic json)
            {
                string status = json.status;
                string name = json.name;
                string taxID = json.taxId;
                string bankCode = json.bankCode;
                string accountType = json.accountType;
                bool allowChange = json.allowChange;
                long amount = json.amount;
                long nominalAmount = json.nominalAmount;
                long interestAmount = json.interestAmount;
                long fineAmount = json.fineAmount;
                long reductionAmount = json.reductionAmount;
                long discountAmount = json.discountAmount;
                string reconciliationID = json.reconciliationId;
                long cashAmount = json.cashAmount;
                string cashierBankCode = json.cashierBankCode;
                string cashierType = json.cashierType;
                string description = json.description;
                string keyId = json.keyId;

                return new BrcodePreview(
                    status: status, name: name, taxID: taxID, bankCode: bankCode, accountType: accountType,
                    allowChange: allowChange, amount: amount, nominalAmount: nominalAmount, interestAmount: interestAmount,
                    fineAmount: fineAmount, reductionAmount: reductionAmount, discountAmount: discountAmount,
                    reconciliationID: reconciliationID, cashAmount: cashAmount, cashierBankCode: cashierBankCode,
                    cashierType: cashierType, description: description, keyId: keyId
                );
            }
        }
    }
}
