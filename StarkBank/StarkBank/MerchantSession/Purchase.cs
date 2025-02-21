using System;
using System.Linq;
using StarkBank.Utils;
using System.Collections.Generic;

namespace StarkBank
{
	public partial class MerchantSession
	{
		public partial class Purchase : Resource
        {
			public int Amount { get; }
			public int? InstallmentCount { get; }
            public string CardId { get; }
            public string CardExpiration { get; }
			public string CardNumber { get; }
			public string CardSecurityCode { get; }
			public string HolderName { get; }
			public string HolderEmail { get; }
			public string HolderPhone { get; }
			public string FundingType { get; }
			public string BillingCountryCode { get; }
			public string BillingCity { get; }
			public string BillingStateCode { get; }
			public string BillingStreetLine1 { get; }
			public string BillingStreetLine2 { get; }
			public string BillingZipCode { get; }
			public Dictionary<string, object> Metadata { get; }
			public string CardEnding { get; }
			public string ChallengeMode { get; }
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

			public Purchase(int amount, string cardExpiration, string cardNumber, string cardSecurityCode,
			string holderName, string fundingType, int? installmentCount = null, string cardId = null, string holderEmail = null,
			string holderPhone = null, string billingCountryCode = null, string billingCity = null, string billingStateCode = null,
			string billingStreetLine1 = null, string billingStreetLine2 = null, string billingZipCode = null, Dictionary<string, object> metadata = null,
			string cardEnding = null, string challengeMode = null, string challengeUrl = null, DateTime? created = null, string currencyCode = null,
			string endToEndId = null, int? fee = null, string network = null, string source = null, 
			string status = null, List<string> tags = null, DateTime? updated = null, string id = null) : base(id) 
			{
				Amount = amount;
				CardId = cardId;
				InstallmentCount = installmentCount;
				CardExpiration = cardExpiration;
				CardNumber = cardNumber;
				CardSecurityCode = cardSecurityCode;
				HolderName = holderName;
				HolderEmail = holderEmail;
				HolderPhone = holderPhone;
				FundingType = fundingType;
				BillingCountryCode = billingCountryCode;
				BillingCity = billingCity;
				BillingStateCode = billingStateCode;
				BillingStreetLine1 = billingStreetLine1;
				BillingStreetLine2 = billingStreetLine2;
				BillingZipCode = billingZipCode;
				Metadata = metadata;
				CardEnding = cardEnding;
				ChallengeMode = challengeMode;
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

            internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
            {
                return (resourceName: "Purchase", resourceMaker: ResourceMaker);
            }

            internal static Resource ResourceMaker(dynamic json)
			{

				int amount = json.amount;
				int? installmentCount = json.installmentCount;
				string cardId = json.cardId;
				string cardExpiration = json.cardExpiration;
				string cardNumber = json.cardNumber;
				string cardSecurityCode = json.cardSecurityCode;
				string holderName = json.holderName;
				string holderEmail = json.holderEmail;
				string holderPhone = json.holderPhone;
				string fundingType = json.fundingType;
				string billingCountryCode = json.billingCountryCode;
				string billingCity = json.billingCity;
				string billingStateCode = json.billingStateCode;
				string billingStreetLine1 = json.billingStreetLine1;
				string billingStreetLine2 = json.billingStreetLine2;
				string billingZipCode = json.billingZipCode;
				string cardEnding = json.cardEnding;
				string challengeMode = json.challengeMode;
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
				string id = json.id;
				Dictionary<string, object> metadata = json.metadata.ToObject<Dictionary<string, object>>();

				return new Purchase(
					amount: amount, cardExpiration: cardExpiration, cardNumber: cardNumber, cardSecurityCode: cardSecurityCode,
					holderName: holderName, fundingType: fundingType, installmentCount: installmentCount, cardId: cardId, holderEmail: holderEmail,
					holderPhone: holderPhone, billingCountryCode: billingCountryCode, billingCity: billingCity, billingStateCode: billingStateCode,
					billingStreetLine1: billingStreetLine1, billingStreetLine2: billingStreetLine2, billingZipCode: billingZipCode, metadata: metadata, 
					cardEnding: cardEnding, challengeMode: challengeMode, challengeUrl: challengeUrl, created: created, currencyCode: currencyCode, 
					endToEndId: endToEndId, fee: fee, network: network, source: source, status: status, tags: tags, updated: updated, id: id
				);
			}
        }
	}
}

