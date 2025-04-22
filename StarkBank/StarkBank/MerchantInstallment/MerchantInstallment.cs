using System;
using System.Linq;
using StarkBank.Utils;
using System.Collections.Generic;

namespace StarkBank
{
	public partial class MerchantInstallment : Resource
	{

        /// Check out our API Documentation at https://starkbank.com/docs/api#merchant-installment

        public int Amount { get; }
		public DateTime Created { get; }
		public DateTime Due { get; }
		public int Fee { get; }
		public string FundingType { get; }
		public string Network { get; }
		public string PurchaseId { get; }
		public string Status { get; }
		public List<string> Tags { get; }
		public List<string> TransactionIds { get; }
		public DateTime Updated { get; }

		public MerchantInstallment(int amount, DateTime created, DateTime due, int fee, string fundingType, string id,
		string network, string purchaseId, string status, List<string> tags, List<string> transactionIds, DateTime updated) : base(id)
		{
			Amount = amount;
			Created = created;
			Due = due;
			Fee = fee;
			FundingType = fundingType;
			Network = network;
			PurchaseId = purchaseId;
			Status = status;
			Tags = tags;
			TransactionIds = transactionIds;
			Updated = updated;
		}

		public static MerchantInstallment Get(string id, User user = null)
		{
			(string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
			return Rest.GetId(
				resourceName: resourceName,
				resourceMaker: resourceMaker,
				id: id,
				user: user
			) as MerchantInstallment;
		}

		public static IEnumerable<MerchantInstallment> Query(int? limit = null, string status = null, List<string> ids = null, DateTime? after = null, DateTime? before = null, List<string> tags = null, User user = null)
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
			).Cast<MerchantInstallment>();
		}

		public static (List<MerchantInstallment> page, string pageCursor) Page(string cursor = null, int? limit = null, string status = null, List<string> ids = null, DateTime? after = null, DateTime? before = null, List<string> tags = null, User user = null)
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
            List<MerchantInstallment> installments = new List<MerchantInstallment>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                installments.Add(subResource as MerchantInstallment);
            }
            return (installments, pageCursor);

        }

		internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
		{
			return (resourceName: "MerchantInstallment", resourceMaker: ResourceMaker);
		}

		internal static Resource ResourceMaker(dynamic json)
		{
			int amount = json.amount;
			DateTime created = json.created;
			DateTime due = json.due;
			int fee = json.fee;
			string fundingType = json.fundingType;
			string id = json.id;
			string network = json.network;
			string purchaseId = json.purchaseId;
			string status = json.status;
			List<string> tags = json.tags.ToObject<List<string>>();
			List<string> transactionIds = json.transactionIds.ToObject<List<string>>();
			DateTime updated = json.updated;
			return new MerchantInstallment(amount, created, due, fee, fundingType, id, network, purchaseId, status, tags, transactionIds, updated);
		}
	}
}

