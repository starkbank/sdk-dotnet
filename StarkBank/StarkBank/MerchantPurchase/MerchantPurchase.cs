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
		public string HolderEmail { get; }
		public string HolderPhone { get; }
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

        public MerchantPurchase(int amount, string fundingType, string cardId, string challengeMode = null, int? installmentCount = null, string holderEmail = null,
        string holderPhone = null, string billingCountryCode = null, string billingCity = null, string billingStateCode = null, string billingStreetLine1 = null,
            string billingStreetLine2 = null, string billingZipCode = null, Dictionary<string, object> metadata = null, string id = null, string cardEnding = null,
        string challengeUrl = null, DateTime? created = null, string currencyCode = null, string endToEndId = null, int? fee = null, string network = null,
        string source = null, string status = null, List<string> tags = null, DateTime? updated = null) : base(id)
            {
                Amount = amount;
                FundingType = fundingType;
                CardId = cardId;
                ChallengeMode = challengeMode;
                InstallmentCount = installmentCount;
                HolderEmail = holderEmail;
                HolderPhone = holderPhone;
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

        public static IEnumerable<MerchantPurchase> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
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
            ).Cast<MerchantPurchase>();
        }

        public static (List<MerchantPurchase> page, string pageCursor) Page(string cursor = null, int? limit = null, DateTime? after = null,
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
            List<MerchantPurchase> sessions = new List<MerchantPurchase>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                sessions.Add(subResource as MerchantPurchase);
            }
            return (sessions, pageCursor);
        }

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
                string holderEmail = json.holderEmail;
                string holderPhone = json.holderPhone;
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
                holderEmail: holderEmail, holderPhone: holderPhone, billingCountryCode: billingCountryCode,
                billingCity: billingCity, billingStateCode: billingStateCode, billingStreetLine1: billingStreetLine1,
                billingStreetLine2: billingStreetLine2, billingZipCode: billingZipCode, metadata: metadata, challengeMode: challengeMode, id: id,
                cardEnding: cardEnding, challengeUrl: challengeUrl, created: created, currencyCode: currencyCode, endToEndId: endToEndId,
                fee: fee, network: network, source: source, status: status, tags: tags, updated: updated);
            }
    }
}

