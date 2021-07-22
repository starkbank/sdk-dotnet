using System;

namespace StarkBank
{
    /// <summary>
    /// InvoicePayment object
    /// <br/>
    /// When an Invoice is paid, its InvoicePayment sub-resource will become available.
    /// It carries all the available information about the invoice payment.
    /// <br/>
    /// Attributes:
    /// <list>
    ///     <item>Amount [long]: amount in cents that was paid. ex: 1234 (= R$ 12.34)</item>
    ///     <item>Name [string]: payer full name. ex: "Anthony Edward Stark"</item>
    ///     <item>TaxID [string]: payer tax ID (CPF or CNPJ). ex: "20.018.183/0001-80"</item>
    ///     <item>BankCode [string]: code of the payer bank institution in Brazil. ex: "20018183"</item>
    ///     <item>BranchCode [string]: payer bank account branch. ex: "1357-9"</item>
    ///     <item>AccountNumber [string]: payer bank account number. ex: "876543-2"</item>
    ///     <item>AccountType [string]: payer bank account type. ex: "checking", "savings", "salary" or "payment"</item>
    ///     <item>EndToEndId [string]: central bank's unique transaction ID. ex: "E79457883202101262140HHX553UPqeq"</item>
    ///     <item>Method [string]: payment method that was used. ex: "pix"</item>    
    /// </list>
    /// </summary>
    public class InvoicePayment : Utils.SubResource
    {
        public long Amount { get; }
        public string Name { get; }
        public string TaxID { get; }
        public string BankCode { get; }
        public string BranchCode { get; }
        public string AccountNumber { get; }
        public string AccountType { get; }
        public string EndToEndId { get; }
        public string Method { get; }

        /// <summary>
        /// Invoice.Payment object
        /// <br/>
        /// When an Invoice is paid, its Payment sub-resource will become available.
        /// It carries all the available information about the invoice payment.
        /// <br/>
        /// Attributes:
        /// <list>
        ///     <item>Amount [long]: amount in cents that was paid. ex: 1234 (= R$ 12.34)</item>
        ///     <item>Name [string]: payer full name. ex: "Anthony Edward Stark"</item>
        ///     <item>TaxID [string]: payer tax ID (CPF or CNPJ). ex: "20.018.183/0001-80"</item>
        ///     <item>BankCode [string]: code of the payer bank institution in Brazil. ex: "20018183"</item>
        ///     <item>BranchCode [string]: payer bank account branch. ex: "1357-9"</item>
        ///     <item>AccountNumber [string]: payer bank account number. ex: "876543-2"</item>
        ///     <item>AccountType [string]: payer bank account type. ex: "checking", "savings", "salary" or "payment"</item>
        ///     <item>EndToEndId [string]: central bank's unique transaction ID. ex: "E79457883202101262140HHX553UPqeq"</item>
        ///     <item>Method [string]: payment method that was used. ex: "pix"</item>    /// </list>
        /// </summary>
        public InvoicePayment(long amount, string name, string taxID, string bankCode, string branchCode,
            string accountNumber, string accountType, string endToEndId, string method)
        {
            Amount = amount;
            Name = name;
            TaxID = taxID;
            BankCode = bankCode;
            BranchCode = branchCode;
            AccountNumber = accountNumber;
            AccountType = accountType;
            EndToEndId = endToEndId;
            Method = method;
        }

        internal static (string resourceName, Utils.Api.ResourceMaker resourceMaker) SubResource()
        {
            return (resourceName: "Payment", resourceMaker: ResourceMaker);
        }

        public static Utils.SubResource ResourceMaker(dynamic json)
        {
            long amount = json.amount;
            string name = json.name;
            string taxID = json.taxId;
            string bankCode = json.bankCode;
            string branchCode = json.branchCode;
            string accountNumber = json.accountNumber;
            string accountType = json.accountType;
            string endToEndId = json.endToEndId;
            string method = json.method;

            return new InvoicePayment(
                amount: amount, name: name, taxID: taxID, bankCode: bankCode, branchCode: branchCode,
                accountNumber: accountNumber, accountType: accountType, endToEndId: endToEndId, method: method
            );
        }
    }
}
