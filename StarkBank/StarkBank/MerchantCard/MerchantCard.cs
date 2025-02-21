using System;
using System.Linq;
using StarkBank.Utils;
using System.Collections.Generic;

namespace StarkBank
{
	public partial class MerchantCard : Resource
	{

        /// Check out our API Documentation at https://starkbank.com/docs/api#merchant-card

        public string Created { get; }
		public string Ending { get; }
		public string Expiration { get; }
		public string FundingType { get; }
		public string HolderName { get; }
		public string Network { get; }
		public string Status { get; }
		public List<string> Tags { get; }
		public string Updated { get; }

		public MerchantCard(string created, string ending, string expiration, string fundingType,
		string holderName, string network, string status, List<string> tags, string updated, string id) : base(id)
		{
			Created = created;
			Ending = ending;
			Expiration = expiration;
			FundingType = fundingType;
			HolderName = holderName;
			Network = network;
			Status = status;
			Tags = tags;
			Updated = updated;
		}
		public static MerchantCard Get(string id, User user = null)
		{
			(string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
			return Rest.GetId(
				resourceName: resourceName,
				resourceMaker: resourceMaker,
				id: id,
				user: user
			) as MerchantCard;
		}

		public static IEnumerable<MerchantCard> Query(int? limit = null, string status = null, List<string> ids = null, List<string> tags = null, DateTime? after = null, DateTime? before = null, User user = null)
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
			).Cast<MerchantCard>();
		}

        public static (List<MerchantCard> page, string pageCursor) Page(string cursor = null, int? limit = null, DateTime? after = null,
            DateTime? before = null, string status = null, List<string> ids = null, List<string> tags = null, User user = null)
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
            List<MerchantCard> sessions = new List<MerchantCard>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                sessions.Add(subResource as MerchantCard);
            }
            return (sessions, pageCursor);
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
		{
			return (resourceName: "MerchantCard", resourceMaker: ResourceMaker);
		}

		internal static Resource ResourceMaker(dynamic json)
		{
			string id = json.id;
			string created = json.created;
			string ending = json.ending;
			string expiration = json.expiration;
			string fundingType = json.fundingType;
			string holderName = json.holderName;
			string network = json.network;
			string status = json.status;
			List<string> tags = json.tags.ToObject<List<string>>();
			string updated = json.updated;
			
			return new MerchantCard(
				created: created, ending: ending, expiration: expiration, fundingType: fundingType,
				holderName: holderName, network: network, status: status, tags: tags, updated: updated, id: id
			);
		}
	}
}

