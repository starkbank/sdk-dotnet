using System;
using StarkBank.Utils;

namespace StarkBank
{
    public partial class PaymentPreview
    {
        /// <summary>
        /// BoletoPreview object
        /// <br/>
        /// A BoletoPreview is used to get information from a Boleto payment you received before confirming the payment.
        /// <br/>
        /// Properties:
        /// <list>
        ///     <item>Status [string]: current boleto status. ex: "active", "expired" or "inactive"</item>
        ///     <item>Amount [long]: final amount to be paid. ex: 23456 (= R$ 234.56)</item>
        ///     <item>DiscountAmount [long]: discount amount to be paid. ex: 23456 (= R$ 234.56)</item>
        ///     <item>FineAmount [long]: fine amount to be paid. ex: 23456 (= R$ 234.56)</item>
        ///     <item>InterestAmount [long]: interest amount to be paid. ex: 23456 (= R$ 234.56)</item>
        ///     <item>Due [DateTime]: Boleto due date. ex: 2020-04-30</item>
        ///     <item>Expiration [DateTime]: Boleto expiration date. ex: 2020-04-30</item>
        ///     <item>Name [string]: beneficiary full name. ex: "Anthony Edward Stark"</item>
        ///     <item>TaxID [string]: beneficiary tax ID (CPF or CNPJ). ex: "20.018.183/0001-80"</item>
        ///     <item>ReceiverName [string]: receiver (Sacador Avalista) full name. ex: "Anthony Edward Stark"</item>
        ///     <item>ReceiverTaxID [string]: receiver (Sacador Avalista) tax ID (CPF or CNPJ). ex: "20.018.183/0001-80"</item>
        ///     <item>PayerName [string]: payer full name. ex: "Anthony Edward Stark"</item>
        ///     <item>PayerTaxID [string]: payer tax ID (CPF or CNPJ). ex: "20.018.183/0001-80"</item>
        ///     <item>Line [string]: Number sequence that identifies the payment. ex: "34191.09008 63571.277308 71444.640008 5 81960000000062"</item>
        ///     <item>BarCode [string]: Bar code number that identifies the payment. ex: "34195819600000000621090063571277307144464000"</item>
        /// </list>
        /// </summary>
        public class BoletoPreview : StarkCore.Utils.SubResource
        {
            public string Status { get; }
            public long Amount { get; }
            public long DiscountAmount { get; }
            public long FineAmount { get; }
            public long InterestAmount { get; }
            public DateTime Due { get; }
            public DateTime Expiration { get; }
            public string Name { get; }
            public string TaxID { get; }
            public string ReceiverName { get; }
            public string ReceiverTaxID { get; }
            public string PayerName { get; }
            public string PayerTaxID { get; }
            public string Line { get; }
            public string BarCode { get; }

            /// <summary>
            /// BoletoPreview object
            /// <br/>
            /// A BoletoPreview is used to get information from a Boleto payment you received before confirming the payment.
            /// <br/>
            /// Attributes (return-only):
            /// <list>
            ///     <item>Status [string]: current boleto status. ex: "active", "expired" or "inactive"</item>
            ///     <item>Amount [long]: final amount to be paid. ex: 23456 (= R$ 234.56)</item>
            ///     <item>DiscountAmount [long]: discount amount to be paid. ex: 23456 (= R$ 234.56)</item>
            ///     <item>FineAmount [long]: fine amount to be paid. ex: 23456 (= R$ 234.56)</item>
            ///     <item>InterestAmount [long]: interest amount to be paid. ex: 23456 (= R$ 234.56)</item>
            ///     <item>Due [DateTime]: Boleto due date. ex: 2020-04-30</item>
            ///     <item>Expiration [DateTime]: Boleto expiration date. ex: 2020-04-30</item>
            ///     <item>Name [string]: beneficiary full name. ex: "Anthony Edward Stark"</item>
            ///     <item>TaxID [string]: beneficiary tax ID (CPF or CNPJ). ex: "20.018.183/0001-80"</item>
            ///     <item>ReceiverName [string]: receiver (Sacador Avalista) full name. ex: "Anthony Edward Stark"</item>
            ///     <item>ReceiverTaxID [string]: receiver (Sacador Avalista) tax ID (CPF or CNPJ). ex: "20.018.183/0001-80"</item>
            ///     <item>PayerName [string]: payer full name. ex: "Anthony Edward Stark"</item>
            ///     <item>PayerTaxID [string]: payer tax ID (CPF or CNPJ). ex: "20.018.183/0001-80"</item>
            ///     <item>Line [string]: Number sequence that identifies the payment. ex: "34191.09008 63571.277308 71444.640008 5 81960000000062"</item>
            ///     <item>BarCode [string]: Bar code number that identifies the payment. ex: "34195819600000000621090063571277307144464000"</item>
            /// </list>
            /// </summary>
            public BoletoPreview(string status, long amount, long discountAmount, long fineAmount,
                long interestAmount, DateTime due, DateTime expiration, string name, string taxID,
                string receiverName, string receiverTaxID, string payerName, string payerTaxID,
                string line, string barCode)
            {
                Status = status;
                Amount = amount;
                DiscountAmount = discountAmount;
                FineAmount = fineAmount;
                InterestAmount = interestAmount;
                Due = due;
                Expiration = expiration;
                Name = name;
                TaxID = taxID;
                ReceiverName = receiverName;
                ReceiverTaxID = receiverTaxID;
                PayerName = payerName;
                PayerTaxID = payerTaxID;
                Line = line;
                BarCode = barCode;
            }

            internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) SubResource()
            {
                return (resourceName: "BoletoPreview", resourceMaker: ResourceMaker);
            }

            public static StarkCore.Utils.SubResource ResourceMaker(dynamic json)
            {
                string status = json.status;
                long amount = json.amount;
                long discountAmount = json.discountAmount;
                long fineAmount = json.fineAmount;
                long interestAmount = json.interestAmount;
                string dueString = json.due;
                DateTime due = StarkCore.Utils.Checks.CheckDateTime(dueString);
                string expirationString = json.expiration;
                DateTime expiration = StarkCore.Utils.Checks.CheckDateTime(expirationString);
                string name = json.name;
                string taxID = json.taxId;
                string receiverName = json.receiverName;
                string receiverTaxID = json.receiverTaxId;
                string payerName = json.payerName;
                string payerTaxID = json.payerTaxId;
                string line = json.line;
                string barCode = json.barCode;

                return new BoletoPreview(
                    status: status, amount: amount, discountAmount: discountAmount, fineAmount: fineAmount,
                    interestAmount: interestAmount, due: due, expiration: expiration, name: name, taxID: taxID,
                    receiverName: receiverName, receiverTaxID: receiverTaxID, payerName: payerName,
                    payerTaxID: payerTaxID, line: line, barCode: barCode
                );
            }
        }
    }
}
