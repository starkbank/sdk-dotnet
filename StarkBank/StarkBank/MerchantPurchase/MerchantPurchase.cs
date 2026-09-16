using System;
using System.Linq;
using StarkBank.Utils;
using System.Collections.Generic;
using static StarkCore.Utils.Api;

namespace StarkBank
{
	public partial class MerchantPurchase : Resource
	{

        /// Check out our API Documentation at https://starkbank.com/docs/api#merchant-purchase

        public int Amount { get; }
        public string FundingType { get; }
		public string CardId { get; }
        public int? InstallmentCount { get; }
        public string HolderName { get; }
		public string HolderEmail { get; }
		public string HolderPhone { get; }
        public string HolderId { get; }
		public string BillingCountryCode { get; }
		public string BillingCity { get; }
		public string BillingStateCode { get; }
		public string BillingStreetLine1 { get; }
		public string BillingStreetLine2 { get; }
		public string BillingZipCode { get; }
        public string ChallengeMode { get; }
        public Dictionary<string, object> Metadata { get; }
        public string CardEnding { get; }
        public string ChallengeUrl { get; }
        public DateTime? Created { get; }
        public string CurrencyCode { get; }
        public string EndToEndId { get; }
        public int? Fee { get; }
        public string Network { get; }
        public string Source { get; }
        public string Status { get; }
        public List<string> Tags { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// MerchantPurchase object
        /// <br/>
        /// Represents a purchase made with a card previously approved through a Merchant Session Purchase; a card cannot be used directly in a Merchant Purchase until it has completed that approval flow.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>amount [integer]: purchase value in cents. ex: 100 (= R$1.00)</item>
        ///     <item>fundingType [string]: card funding type. ex: "credit", "debit"</item>
        ///     <item>cardId [string]: id of a previously approved MerchantCard. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>challengeMode [string, default "enabled"]: whether 3DS holder verification is used. Options: "enabled", "disabled"</item>
        ///     <item>installmentCount [integer, default 1]: number of purchase installments.</item>
        /// </list>
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when MerchantPurchase is created. ex: "5656565656565656"</item>
        ///     <item>status [string]: current MerchantPurchase status. ex: "created", "approved", "denied", "confirmed", "paid", "pending", "canceled", "voided" or "failed"</item>
        ///     <item>fee [integer]: fee charged when the MerchantPurchase is created. ex: 200 (= R$ 2.00)</item>
        /// </list>
        /// </summary>
        public MerchantPurchase(int amount, string fundingType, string cardId, string challengeMode = null, int? installmentCount = null,
        string holderName = null, string holderEmail = null, string holderPhone = null, string holderId = null, string billingCountryCode = null,
        string billingCity = null, string billingStateCode = null, string billingStreetLine1 = null, string billingStreetLine2 = null, string billingZipCode = null,
        Dictionary<string, object> metadata = null, string id = null, string cardEnding = null, string challengeUrl = null, DateTime? created = null, string currencyCode = null,
        string endToEndId = null, int? fee = null, string network = null, string source = null, string status = null, List<string> tags = null, DateTime? updated = null) : base(id)
            {
                Amount = amount;
                FundingType = fundingType;
                CardId = cardId;
                ChallengeMode = challengeMode;
                InstallmentCount = installmentCount;
                HolderName = holderName;
                HolderEmail = holderEmail;
                HolderPhone = holderPhone;
                HolderId = holderId;
                BillingCountryCode = billingCountryCode;
                BillingCity = billingCity;
                BillingStateCode = billingStateCode;
                BillingStreetLine1 = billingStreetLine1;
                BillingStreetLine2 = billingStreetLine2;
                BillingZipCode = billingZipCode;
                Metadata = metadata;
                CardEnding = cardEnding;
                ChallengeUrl = challengeUrl;
                Created = created;
                CurrencyCode = currencyCode;
                EndToEndId = endToEndId;
                Fee = fee;
                Network = network;
                Source = source;
                Status = status;
                Tags = tags;
                Updated = updated;
            }

		/// <summary>
		/// Create a MerchantPurchase
		/// <br/>
		/// Send a MerchantPurchase object for creation in the Stark Bank API. The card must already have been approved through a Merchant Session Purchase before it can be charged here.
		/// <br/>
		/// Parameters (required):
		/// <list>
		///     <item>purchase [MerchantPurchase object]: MerchantPurchase to be created in the API</item>
		/// </list>
		/// <br/>
		/// Parameters (optional):
		/// <list>
		///     <item>user [Organization/Project object]: not necessary if StarkBank.Settings.User was set before the call</item>
		/// </list>
		/// <br/>
		/// Return:
		/// <list>
		///     <item>MerchantPurchase object with updated attributes</item>
		/// </list>
		/// </summary>
		public static MerchantPurchase Create(MerchantPurchase purchase, User user = null)
		{
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PostSingle(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entity: purchase,
                user: user
            ) as MerchantPurchase;
		}

		/// <summary>
		/// Retrieve a specific MerchantPurchase
		/// <br/>
		/// Receive a single MerchantPurchase object previously created in the Stark Bank API by passing its id.
		/// <br/>
		/// Parameters (required):
		/// <list>
		///     <item>id [string]: object unique id.</item>
		/// </list>
		/// <br/>
		/// Return:
		/// <list>
		///     <item>MerchantPurchase object with updated attributes</item>
		/// </list>
		/// </summary>
		public static MerchantPurchase Get(string id, User user = null)
		{
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();

            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as MerchantPurchase;
		}

        /// <summary>
        /// Retrieve MerchantPurchases
        /// <br/>
        /// Receive an IEnumerable of MerchantPurchase objects previously created in the Stark Bank API.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "created", "approved", "denied", "confirmed", "paid", "pending", "canceled", "voided" or "failed"</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
        ///     <item>holderId [string, default null]: filter for the MerchantPurchases created by a specific Merchant Session holder. ex: "5656565656565656"</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of MerchantPurchase objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<MerchantPurchase> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
            string status = null, List<string> tags = null, List<string> ids = null, string holderId = null, User user = null)
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
                    { "holderId", holderId },
                },
                user: user
            ).Cast<MerchantPurchase>();
        }

        /// <summary>
        /// Retrieve paged MerchantPurchases
        /// <br/>
        /// Receive a list of up to 100 MerchantPurchase objects previously created in the Stark Bank API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "created", "approved", "denied", "confirmed", "paid", "pending", "canceled", "voided" or "failed"</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
        ///     <item>holderId [string, default null]: filter for the MerchantPurchases created by a specific Merchant Session holder. ex: "5656565656565656"</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of MerchantPurchase objects with updated attributes and cursor to retrieve the next page of MerchantPurchase objects</item>
        /// </list>
        /// </summary>
        public static (List<MerchantPurchase> page, string pageCursor) Page(string cursor = null, int? limit = null, DateTime? after = null,
            DateTime? before = null, string status = null, List<string> tags = null, List<string> ids = null, string holderId = null, User user = null)
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
                    { "holderId", holderId },
                },
                user: user
            );
            List<MerchantPurchase> sessions = new List<MerchantPurchase>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                sessions.Add(subResource as MerchantPurchase);
            }
            return (sessions, pageCursor);
        }

		/// <summary>
		/// Update MerchantPurchase entity
		/// <br/>
		/// Update a MerchantPurchase by passing its id. If the purchase is "approved", only canceling it (status="canceled", amount=0) is allowed, which cancels the authorization. If it is "confirmed", set status="reversed" with a lower amount to debit and reverse the difference, partially or fully; a partial reversal keeps status "confirmed", while a full reversal sets status to "voided".
		/// <br/>
		/// Parameters (required):
		/// <list>
		///     <item>id [string]: MerchantPurchase unique id.</item>
		///     <item>status [string]: "canceled" or "reversed".</item>
		///     <item>amount [integer]: new amount; 0 to cancel an approved purchase, or a lower value to reverse a confirmed one.</item>
		/// </list>
		/// <br/>
		/// Parameters (optional):
		/// <list>
		///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
		/// </list>
		/// <br/>
		/// Return:
		/// <list>
		///     <item>target MerchantPurchase with updated attributes</item>
		/// </list>
		/// </summary>
		public static MerchantPurchase Update(string id, string status, int amount, User user = null)
		{
			(string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
			return Rest.PatchId(
				resourceName: resourceName,
				resourceMaker: resourceMaker,
				id: id,
				payload: new Dictionary<string, object>
                {
                    { "status", status},
                    { "amount", amount}
                },
				user: user
			) as MerchantPurchase;
		}

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "MerchantPurchase", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
            {
            string id = json.id;
            int amount = json.amount;
                string fundingType = json.fundingType;
                string cardId = json.cardId;
                int? installmentCount = json.installmentCount;
                string holderName = json.holderName;
                string holderEmail = json.holderEmail;
                string holderPhone = json.holderPhone;
                string holderId = json.holderId;
                string billingCountryCode = json.billingCountryCode;
                string billingCity = json.billingCity;
                string billingStateCode = json.billingStateCode;
                string billingStreetLine1 = json.billingStreetLine1;
                string billingStreetLine2 = json.billingStreetLine2;
                string billingZipCode = json.billingZipCode;
                string challengeMode = json.challengeMode;
                Dictionary<string, object> metadata = json.metadata.ToObject<Dictionary<string, object>>();
                string cardEnding = json.cardEnding;
                string challengeUrl = json.challengeUrl;
                DateTime? created = json.created;
                string currencyCode = json.currencyCode;
                string endToEndId = json.endToEndId;
                int? fee = json.fee;
                string network = json.network;
                string source = json.source;
                string status = json.status;
                List<string> tags = json.tags.ToObject<List<string>>();
                DateTime? updated = json.updated;

            return new MerchantPurchase(amount: amount, fundingType: fundingType, cardId: cardId, installmentCount: installmentCount,
                holderName: holderName, holderEmail: holderEmail, holderPhone: holderPhone, holderId: holderId, billingCountryCode: billingCountryCode,
                billingCity: billingCity, billingStateCode: billingStateCode, billingStreetLine1: billingStreetLine1,
                billingStreetLine2: billingStreetLine2, billingZipCode: billingZipCode, metadata: metadata, challengeMode: challengeMode, id: id,
                cardEnding: cardEnding, challengeUrl: challengeUrl, created: created, currencyCode: currencyCode, endToEndId: endToEndId,
                fee: fee, network: network, source: source, status: status, tags: tags, updated: updated);
            }
    }
}

