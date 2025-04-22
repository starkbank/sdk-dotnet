using System;
using System.Linq;
using StarkBank.Utils;
using System.Collections.Generic;

namespace StarkBank
{
	public partial class MerchantSession : Resource
	{

        /// Check out our API Documentation at https://starkbank.com/docs/api#merchant-session

        public List<string> AllowedFundingTypes { get; }
        public List<AllowedInstallment> AllowedInstallments { get; }
		public List<string> AllowedIps { get; }
		public string ChallengeMode { get; }
		public int? Expiration { get; }
		public string Status { get; }
		public List<string> Tags { get; }
		public string Uuid { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        public MerchantSession(List<string> allowedFundingTypes, List<AllowedInstallment> allowedInstallments, int expiration,
        List<string> allowedIps = null, string challengeMode = null, string status = null, List<string> tags = null, string uuid = null, DateTime? created = null, DateTime? updated = null, string id = null) : base(id)
		{
			AllowedFundingTypes = allowedFundingTypes;
			AllowedInstallments = allowedInstallments;
			Expiration = expiration;
			AllowedIps = allowedIps;
			ChallengeMode = challengeMode;
			Status = status;
			Tags = tags;
			Uuid = uuid;
			Created = created;
			Updated = updated;
		}

        public static MerchantSession Create(MerchantSession merchantSession, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PostSingle(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entity: merchantSession,
                user: user
            ) as MerchantSession;
        }

        public static MerchantSession Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as MerchantSession;
        }

		public static IEnumerable<MerchantSession> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
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
            ).Cast<MerchantSession>();
        }

        public static (List<MerchantSession> page, string pageCursor) Page(string cursor = null, int? limit = null, DateTime? after = null,
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
            List<MerchantSession> sessions = new List<MerchantSession>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                sessions.Add(subResource as MerchantSession);
            }
            return (sessions, pageCursor);
        }

        public static Purchase PostPurchase(string id, Purchase purchase, User user = null)
		{
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            (string subResourceName, StarkCore.Utils.Api.ResourceMaker subResourceMaker) = Purchase.Resource();

            return Rest.PostSubResource(
				resourceName: resourceName,
                subResourceName: subResourceName,
                subResourceMaker: subResourceMaker,
				entity: purchase,
				id: id,
				user: user
			) as Purchase;
		}

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "MerchantSession", resourceMaker: ResourceMaker);
        }

		internal static Resource ResourceMaker(dynamic json)
        {
			List<string> allowedFundingTypes = json.allowedFundingTypes.ToObject<List<string>>();
            List<AllowedInstallment> allowedInstallments = ParseAllowedInstallment(json.allowedInstallments);
			List<string> allowedIps = json.allowedIps.ToObject<List<string>>();
			string challengeMode = json.challengeMode;
			int expiration = json.expiration;
			string status = json.status;
			List<string> tags = json.tags.ToObject<List<string>>();
			string uuid = json.uuid;
            string createdString = json.created;
            string updatedString = json.updated;
            DateTime? created = StarkCore.Utils.Checks.CheckDateTime(createdString);
			DateTime? updated = StarkCore.Utils.Checks.CheckDateTime(updatedString);
            string id = json.id;

            return new MerchantSession(
				allowedFundingTypes: allowedFundingTypes, allowedInstallments: allowedInstallments, expiration: expiration,
				allowedIps: allowedIps, challengeMode: challengeMode, status: status, tags: tags, uuid: uuid, created: created, updated: updated, id: id
			);
		}

        private static List<AllowedInstallment> ParseAllowedInstallment(dynamic json)
        {
            if (json is null) return null;

            List<AllowedInstallment> installments = new List<AllowedInstallment>();

            foreach (dynamic installment in json)
            {
                installments.Add(AllowedInstallment.ResourceMaker(installment));
            }
            return installments;
        }
    }
}

