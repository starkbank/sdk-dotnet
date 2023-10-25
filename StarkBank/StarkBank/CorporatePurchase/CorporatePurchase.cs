using System;
using StarkCore;
using System.Linq;
using StarkCore.Utils;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace StarkBank
{
    /// <summary>
    /// CorporatePurchase object
    /// <br/>
    /// Displays the CorporatePurchase objects created in your Workspace.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>ID [string]: unique id returned when CorporatePurchase is created. ex: "5656565656565656"</item>
    ///     <item>HolderID [string]: card holder unique id. ex: "5656565656565656"</item>
    ///     <item>HolderName [string]: card holder name. ex: "Tony Stark"</item>
    ///     <item>CenterID [string]: target cost center ID. ex: "5656565656565656"</item>
    ///     <item>CardID [string]: unique id returned when CorporateCard is created. ex: "5656565656565656"</item>
    ///     <item>CardEnding [string]: last 4 digits of the card number. ex: "1234"</item>
    ///     <item>Description [string]: purchase descriptions. ex: "my_description"</item>
    ///     <item>Amount [long]: CorporatePurchase value in cents. Minimum = 0. ex: 1234 (= R$ 12.34)</item>
    ///     <item>Tax [integer]: IOF amount taxed for international purchases. ex: 1234 (= R$ 12.34)</item>
    ///     <item>IssuerAmount [long]: issuer amount. ex: 1234 (= R$ 12.34)</item>
    ///     <item>IssuerCurrencyCode [string]: issuer currency code. ex: "USD"</item>
    ///     <item>IssuerCurrencySymbol [string]: issuer currency symbol. ex: "$"</item>
    ///     <item>MerchantAmount [long]: merchant amount. ex: 1234 (= R$ 12.34)</item>
    ///     <item>MerchantCurrencyCode [string]: merchant currency code. ex: "USD"</item>
    ///     <item>MerchantCurrencySymbol [string]: merchant currency symbol. ex: "$"</item>
    ///     <item>MerchantCategoryCode [string]: merchant category code. ex: "fastFoodRestaurants"</item>
    ///     <item>MerchantCategoryType [string]: merchant category type. ex: "health"</item>
    ///     <item>MerchantCountryCode [string]: merchant country code. ex: "USA"</item>
    ///     <item>MerchantName [string]: merchant name. ex: "Google Cloud Platform"</item>
    ///     <item>MerchantDisplayName [string]: merchant name. ex: "Google Cloud Platform"</item>
    ///     <item>MerchantDisplayUrl [string]: public merchant icon (png image). ex: "https://sandbox.api.starkbank.com/v2/corporate-icon/merchant/ifood.png"</item>
    ///     <item>MerchantFee [integer]: fee charged by the merchant to cover specific costs, such as ATM withdrawal logistics, etc. ex: 200 (= R$ 2.00)</item>
    ///     <item>MethodCode [string]: method code. Options: "chip", "token", "server", "manual", "magstripe" or "contactless"</item>
    ///     <item>Tags [list of strings]: list of strings for tagging returned by the sub-issuer during the authorization. ex: new List<string>{ "travel", "food" }</item>
    ///     <item>CorporateTransactionIds [string]: ledger transaction ids linked to this Purchase</item>
    ///     <item>Status [string]: current CorporateCard status. Options: "approved", "canceled", "denied", "confirmed" or "voided"</item>
    ///     <item>Updated [DateTime]: latest update DateTime for the CorporatePurchase. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Created [DateTime]: creation DateTime for the CorporatePurchase. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class CorporatePurchase : Resource
    {
        public string HolderID { get; }
        public string HolderName { get; }
        public string CenterID { get; }
        public string CardID { get; }
        public string CardEnding { get; }
        public string Description { get; }
        public long? Amount { get; }
        public int? Tax { get; }
        public long? IssuerAmount { get; }
        public string IssuerCurrencyCode { get; }
        public string IssuerCurrencySymbol { get; }
        public long? MerchantAmount { get; }
        public string MerchantCurrencyCode { get; }
        public string MerchantCurrencySymbol { get; }
        public string MerchantCategoryCode { get; }
        public string MerchantCategoryType { get; }
        public string MerchantCountryCode { get; }
        public string MerchantName { get; }
        public string MerchantDisplayName { get; }
        public string MerchantDisplayUrl { get; }
        public int? MerchantFee { get; }
        public string MethodCode { get; }
        public List<string> Tags { get; }
        public List<string> CorporateTransactionIds { get; }
        public string Status { get; }
        public DateTime? Updated { get; }
        public DateTime? Created { get; }

        /// <summary>
        /// CorporatePurchase object
        /// <br/>
        /// Displays the CorporatePurchase objects created in your Workspace.
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when CorporatePurchase is created. ex: "5656565656565656"</item>
        ///     <item>holderID [string]: card holder unique id. ex: "5656565656565656"</item>
        ///     <item>holderName [string]: card holder name. ex: "Tony Stark"</item>
        ///     <item>centerID [string]: target cost center ID. ex: "5656565656565656"</item>
        ///     <item>cardID [string]: unique id returned when CorporateCard is created. ex: "5656565656565656"</item>
        ///     <item>cardEnding [string]: last 4 digits of the card number. ex: "1234"</item>
        ///     <item>description [string]: purchase descriptions. ex: "my_description"</item>
        ///     <item>amount [long]: CorporatePurchase value in cents. Minimum = 0. ex: 1234 (= R$ 12.34)</item>
        ///     <item>tax [integer]: IOF amount taxed for international purchases. ex: 1234 (= R$ 12.34)</item>
        ///     <item>issuerAmount [long]: issuer amount. ex: 1234 (= R$ 12.34)</item>
        ///     <item>issuerCurrencyCode [string]: issuer currency code. ex: "USD"</item>
        ///     <item>issuerCurrencySymbol [string]: issuer currency symbol. ex: "$"</item>
        ///     <item>merchantAmount [long]: merchant amount. ex: 1234 (= R$ 12.34)</item>
        ///     <item>merchantCurrencyCode [string]: merchant currency code. ex: "USD"</item>
        ///     <item>merchantCurrencySymbol [string]: merchant currency symbol. ex: "$"</item>
        ///     <item>merchantCategoryCode [string]: merchant category code. ex: "fastFoodRestaurants"</item>
        ///     <item>merchantCategoryType [string]: merchant category type. ex: "health"</item>
        ///     <item>merchantCountryCode [string]: merchant country code. ex: "USA"</item>
        ///     <item>merchantName [string]: merchant name. ex: "Google Cloud Platform"</item>
        ///     <item>merchantDisplayName [string]: merchant name. ex: "Google Cloud Platform"</item>
        ///     <item>merchantDisplayUrl [string]: public merchant icon (png image). ex: "https://sandbox.api.starkbank.com/v2/corporate-icon/merchant/ifood.png"</item>
        ///     <item>merchantFee [integer]: fee charged by the merchant to cover specific costs, such as ATM withdrawal logistics, etc. ex: 200 (= R$ 2.00)</item>
        ///     <item>methodCode [string]: method code. Options: "chip", "token", "server", "manual", "magstripe" or "contactless"</item>
        ///     <item>tags [list of strings]: list of strings for tagging returned by the sub-issuer during the authorization. ex: new List<string>{ "travel", "food" }</item>
        ///     <item>corporateTransactionIds [string]: ledger transaction ids linked to this Purchase</item>
        ///     <item>status [string]: current CorporateCard status. Options: "approved", "canceled", "denied", "confirmed" or "voided"</item>
        ///     <item>updated [DateTime]: latest update DateTime for the CorporatePurchase. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>created [DateTime]: creation DateTime for the CorporatePurchase. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public CorporatePurchase(
            string id = null, string holderID = null, string holderName = null, string centerID = null, string cardID = null, 
            string cardEnding = null, string description = null, long? amount = null, int? tax = null, long? issuerAmount = null, 
            string issuerCurrencyCode = null, string issuerCurrencySymbol = null, long? merchantAmount = null, string merchantCurrencyCode = null, 
            string merchantCurrencySymbol = null, string merchantCategoryCode = null, string merchantCategoryType = null, 
            string merchantCountryCode = null, string merchantName = null, string merchantDisplayName = null, string merchantDisplayUrl = null, 
            int? merchantFee = null, string methodCode = null, List<string> tags = null, List<string> corporateTransactionIds = null, 
            string status = null, DateTime? updated = null, DateTime? created = null        
        ) : base(id)
        {
            HolderID = holderID;
            HolderName = holderName;
            CenterID = centerID;
            CardID = cardID;
            CardEnding = cardEnding;
            Description = description;
            Amount = amount;
            Tax = tax;
            IssuerAmount = issuerAmount;
            IssuerCurrencyCode = issuerCurrencyCode;
            IssuerCurrencySymbol = issuerCurrencySymbol;
            MerchantAmount = merchantAmount;
            MerchantCurrencyCode = merchantCurrencyCode;
            MerchantCurrencySymbol = merchantCurrencySymbol;
            MerchantCategoryCode = merchantCategoryCode;
            MerchantCategoryType = merchantCategoryType;
            MerchantCountryCode = merchantCountryCode;
            MerchantName = merchantName;
            MerchantDisplayName = merchantDisplayName;
            MerchantDisplayUrl = merchantDisplayUrl;
            MerchantFee = merchantFee;
            MethodCode = methodCode;
            Tags = tags;
            CorporateTransactionIds = corporateTransactionIds;
            Status = status;
            Updated = updated;
            Created = created;
        }

        /// <summary>
        /// Retrieve a specific CorporatePurchase by its id
        /// <br/>
        /// Receive a single CorporatePurchase object previously created in the Stark Bank API by passing its id
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: object unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>CorporatePurchase object that corresponds to the given id.</item>
        /// </list>
        /// </summary>
        public static CorporatePurchase Get(string id, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as CorporatePurchase;
        }

        /// <summary>
        /// Retrieve CorporatePurchase objects
        /// <br/>
        /// Receive an IEnumerable of CorporatePurchase objects previously created in the Stark Bank API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>merchantCategoryTypes [list of strings, default null]: merchant category type. ex: new List<string>{ "health" }</item>
        ///     <item>holderIds [list of strings, default null]: card holder IDs. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>cardIds [list of strings, default null]: card  IDs. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "approved", "canceled", "denied", "confirmed" or "voided"</item>
        ///     <item>ids [list of strings, default null]: purchase IDs</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of CorporatePurchase objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<CorporatePurchase> Query(List<string> ids = null, int? limit = 1, DateTime? after = null, 
            DateTime? before = null, List<string> merchantCategoryTypes = null, List<string> holderIds = null,List<string> cardIds = null, 
            string status = null, User user = null
        ) {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "ids" , ids },
                    { "limit" , limit },
                    { "after" , after },
                    { "before" , before },
                    { "merchantCategoryTypes" , merchantCategoryTypes },
                    { "holderIds" , holderIds },
                    { "cardIds" , cardIds },
                    { "status" , status }
                },
                user: user
            ).Cast<CorporatePurchase>();
        }

        /// <summary>
        /// Retrieve paged CorporatePurchase objects
        /// <br/>
        /// Receive a list of up to 100 CorporatePurchase objects previously created in the Stark Bank API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35.</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>merchantCategoryTypes [list of strings, default null]: merchant category type. ex: new List<string>{ "health" }</item>
        ///     <item>holderIds [list of strings, default null]: card holder IDs. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>cardIds [list of strings, default null]: card  IDs. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "approved", "canceled", "denied", "confirmed" or "voided"</item>
        ///     <item>ids [list of strings, default null]: purchase IDs</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of CorporatePurchase objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of CorporatePurchase objects</item>
        /// </list>
        /// </summary>
        public static (List<CorporatePurchase> page, string pageCursor) Page(
            string cursor = null, List<string> ids = null, int? limit = 1, DateTime? after = null, 
            DateTime? before = null, List<string> merchantCategoryTypes = null, List<string> holderIds = null,List<string> cardIds = null, 
            string status = null, User user = null
        ) {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            (List<SubResource> page, string pageCursor) = Utils.Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "ids" , ids },
                    { "limit" , limit },
                    { "after" , after },
                    { "before" , before },
                    { "merchantCategoryTypes" , merchantCategoryTypes },
                    { "holderIds" , holderIds },
                    { "cardIds" , cardIds },
                    { "status" , status }
                },
                user: user
            );
            List<CorporatePurchase> purchases = new List<CorporatePurchase>();
            foreach (SubResource subResource in page)
            {
                purchases.Add(subResource as CorporatePurchase);
            }
            return (purchases, pageCursor);
        }

        /// <summary>
        /// Create a single verified CorporatePurchase authorization request from a content string
        /// <br/>
        /// Use this method to parse and verify the authenticity of the authorization request received at the informed endpoint.
        /// Authorization requests are posted to your registered endpoint whenever CorporatePurchases are received.
        /// They present CorporatePurchase data that must be analyzed and answered with approval or declination.
        /// If the provided digital signature does not check out with the StarkBank public key, a Error.InvalidSignatureException will be raised.
        /// If the authorization request is not answered within 2 seconds or is not answered with an HTTP status code 200 the CorporatePurchase will go through the pre-configured stand-in validation.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>content [string]: response content from request received at user endpoint (not parsed)</item>
        ///     <item>signature [string]: base-64 digital signature received at response header "Digital-Signature"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>Parsed CorporatePurchase object</item>
        /// </list>
        /// </summary>
        public static CorporatePurchase Parse(string content, string signature, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Parse.ParseAndVerify(
                content: content,
                signature: signature,
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                user: user
            ) as CorporatePurchase;
        }

        /// <summary>
        /// Helps you respond to a CorporatePurchase authorization request
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>status [string]: sub-issuer response to the authorization. ex: "approved" or "denied"</item>
        ///</list>
        /// Parameters (conditionally required):
        /// <list>
        ///     <item>reason [string]: denial reason. Options: "other", "blocked", "lostCard", "stolenCard", "invalidPin", "invalidCard", "cardExpired", "issuerError", "concurrency", "standInDenial", "subIssuerError", "invalidPurpose", "invalidZipCode", "invalidWalletID", "inconsistentCard", "settlementFailed", "cardRuleMismatch", "invalidExpiration", "prepaidInstallment", "holderRuleMismatch", "insufficientBalance", "tooManyTransactions", "invalidSecurityCode", "invalidPaymentMethod", "confirmationDeadline", "withdrawalAmountLimit", "insufficientCardLimit", "insufficientHolderLimit"</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>amount [integer, default null]: amount in cents that was authorized. ex: 1234 (= R$ 12.34)</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved object. ex: new List<string>{ "tony", "stark" }</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>Dumped JSON string that must be returned to us on the CorporatePurchase request</item>
        /// </list>
        /// </summary>
        public static string Response(string status, long? amount = null, string reason = null, List<string> tags = null)
        {
            Dictionary<string, object> rawResponse = new Dictionary<string, object>
            {
                {"authorization", new Dictionary<string, object>
                    {
                        {"status", status},
                        {"amount", amount},
                        {"reason", reason},
                        {"tags", tags}
                    }
                }
            };
            Dictionary<string, object> response = Api.CastJsonToApiFormat(rawResponse);
            return JsonConvert.SerializeObject(response);
        }

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "CorporatePurchase", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            string holderID = json.holderId;
            string holderName = json.holderName;
            string centerID = json.centerId;
            string cardID = json.cardId;
            string cardEnding = json.cardEnding;
            string description = json.description;
            long amount = json.amount;
            int? tax = json.tax;
            long issuerAmount = json.issuerAmount;
            string issuerCurrencyCode = json.issuerCurrencyCode;
            string issuerCurrencySymbol = json.issuerCurrencySymbol;
            long? merchantAmount = json.merchantAmount;
            string merchantCurrencyCode = json.merchantCurrencyCode;
            string merchantCurrencySymbol = json.merchantCurrencySymbol;
            string merchantCategoryCode = json.merchantCategoryCode;
            string merchantCountryCode = json.merchantCountryCode;
            string merchantName = json.merchantName;
            string merchantDisplayName = json.merchantDisplayName;
            string merchantDisplayUrl = json.merchantDisplayUrl;
            int? merchantFee = json.merchantFee;
            string methodCode = json.methodCode;
            List<string> tags = json.tags?.ToObject<List<string>>();
            List<string> corporateTransactionIds = json.corporateTransactionIds?.ToObject<List<string>>();
            string status = json.status;
            string createdString = json.created;
            DateTime? created = Checks.CheckNullableDateTime(createdString);
            string updatedString = json.updated;
            DateTime? updated = Checks.CheckNullableDateTime(updatedString);

            return new CorporatePurchase(
                id: id, holderID: holderID, holderName: holderName, centerID: centerID, cardID: cardID, cardEnding: cardEnding, 
                description: description, amount: amount, tax: tax, issuerAmount: issuerAmount, issuerCurrencyCode: issuerCurrencyCode, 
                issuerCurrencySymbol: issuerCurrencySymbol, merchantAmount: merchantAmount, merchantCurrencyCode: merchantCurrencyCode, 
                merchantCurrencySymbol: merchantCurrencySymbol, merchantCategoryCode: merchantCategoryCode, merchantCountryCode: merchantCountryCode, 
                merchantName: merchantName, merchantDisplayName: merchantDisplayName, merchantDisplayUrl: merchantDisplayUrl, 
                merchantFee: merchantFee, methodCode: methodCode, tags: tags, corporateTransactionIds: corporateTransactionIds, 
                status: status, created: created, updated: updated
            );
        }
    }
}
