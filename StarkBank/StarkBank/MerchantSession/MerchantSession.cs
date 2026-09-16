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
        public string HolderId { get; }
        public string SoftDescriptor { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// MerchantSession object
        /// <br/>
        /// A MerchantSession authorizes card purchase attempts for a limited time window.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>allowedFundingTypes [list of strings]: list of funding types allowed for the session. ex: ["credit", "debit"]</item>
        ///     <item>allowedInstallments [list of AllowedInstallment objects]: list of AllowedInstallment objects allowed for the session</item>
        ///     <item>expiration [integer]: seconds from creation until the session expires; after expiration, a purchase can no longer be created using the session. ex: 3600</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>allowedIps [list of strings, default null]: list of ips that are allowed to make requests with this session</item>
        ///     <item>challengeMode [string, default "enabled"]: whether 3DS holder verification is used. Options: "enabled", "disabled"</item>
        ///     <item>tags [list of strings, default null]: list of strings for tagging. All tags will be converted to lowercase.</item>
        /// </list>
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>uuid [string]: unique id returned when the MerchantSession is created. ex: "5656565656565656"</item>
        ///     <item>status [string]: current MerchantSession status. ex: "created", "expired"</item>
        /// </list>
        /// </summary>
        public MerchantSession(List<string> allowedFundingTypes, List<AllowedInstallment> allowedInstallments, int expiration,
        List<string> allowedIps = null, string challengeMode = null, string status = null, List<string> tags = null, string uuid = null,
        string holderId = null, string softDescriptor = null, DateTime? created = null, DateTime? updated = null, string id = null) : base(id)
		{
			AllowedFundingTypes = allowedFundingTypes;
			AllowedInstallments = allowedInstallments;
			Expiration = expiration;
			AllowedIps = allowedIps;
			ChallengeMode = challengeMode;
			Status = status;
			Tags = tags;
			Uuid = uuid;
			HolderId = holderId;
			SoftDescriptor = softDescriptor;
			Created = created;
			Updated = updated;
		}

        /// <summary>
        /// Create a MerchantSession
        /// <br/>
        /// Send a MerchantSession object for creation in the Stark Bank API.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>merchantSession [MerchantSession object]: MerchantSession to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: not necessary if StarkBank.Settings.User was set before the call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>MerchantSession object with updated attributes</item>
        /// </list>
        /// </summary>
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

        /// <summary>
        /// Retrieve a specific MerchantSession
        /// <br/>
        /// Receive a single MerchantSession object previously created in the Stark Bank API by passing its id.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: object unique id.</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>MerchantSession object with updated attributes</item>
        /// </list>
        /// </summary>
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

		/// <summary>
		/// Retrieve MerchantSessions
		/// <br/>
		/// Receive an IEnumerable of MerchantSession objects previously created in the Stark Bank API.
		/// <br/>
		/// Parameters (optional):
		/// <list>
		///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
		///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
		///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
		///     <item>status [string, default null]: filter for status of retrieved objects. ex: "created", "expired"</item>
		///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
		///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
		///     <item>holderId [string, default null]: filter for the MerchantSessions created by a specific Merchant Session holder. ex: "5656565656565656"</item>
		///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
		/// </list>
		/// <br/>
		/// Return:
		/// <list>
		///     <item>IEnumerable of MerchantSession objects with updated attributes</item>
		/// </list>
		/// </summary>
		public static IEnumerable<MerchantSession> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
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
            ).Cast<MerchantSession>();
        }

        /// <summary>
        /// Retrieve paged MerchantSessions
        /// <br/>
        /// Receive a list of up to 100 MerchantSession objects previously created in the Stark Bank API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "created", "expired"</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
        ///     <item>holderId [string, default null]: filter for the MerchantSessions created by a specific Merchant Session holder. ex: "5656565656565656"</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of MerchantSession objects with updated attributes and cursor to retrieve the next page of MerchantSession objects</item>
        /// </list>
        /// </summary>
        public static (List<MerchantSession> page, string pageCursor) Page(string cursor = null, int? limit = null, DateTime? after = null,
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
            List<MerchantSession> sessions = new List<MerchantSession>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                sessions.Add(subResource as MerchantSession);
            }
            return (sessions, pageCursor);
        }

        /// <summary>
        /// Create a Merchant Session Purchase
        /// <br/>
        /// Attempt a purchase using an existing MerchantSession, approving the card for later direct use in MerchantPurchase.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: MerchantSession uuid.</item>
        ///     <item>purchase [MerchantSession.Purchase object]: card and purchase data, including installmentCount [integer, default 1].</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>MerchantSession.Purchase object with updated attributes</item>
        /// </list>
        /// </summary>
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
			string holderId = json.holderId;
            string softDescriptor = json.softDescriptor;
            string createdString = json.created;
            string updatedString = json.updated;
            DateTime? created = StarkCore.Utils.Checks.CheckDateTime(createdString);
			DateTime? updated = StarkCore.Utils.Checks.CheckDateTime(updatedString);
            string id = json.id;

            return new MerchantSession(
				allowedFundingTypes: allowedFundingTypes, allowedInstallments: allowedInstallments, expiration: expiration, allowedIps: allowedIps,
                challengeMode: challengeMode, status: status, tags: tags, uuid: uuid, holderId: holderId, softDescriptor: softDescriptor, created: created, updated: updated, id: id
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

