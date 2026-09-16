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

		/// <summary>
		/// MerchantCard object
		/// <br/>
		/// Stores information about cards used in approved purchases; these cards can be reused in new purchases without creating a new session.
		/// <br/>
		/// Attributes (return-only):
		/// <list>
		///     <item>id [string]: unique id returned when the MerchantCard is created. ex: "5656565656565656"</item>
		///     <item>ending [string]: last 4 digits of the card number.</item>
		///     <item>expiration [string]: card expiration date. ex: "2025-06"</item>
		///     <item>fundingType [string]: card funding type. ex: "credit", "debit"</item>
		///     <item>holderName [string]: name of the card holder.</item>
		///     <item>network [string]: card network.</item>
		///     <item>status [string]: current MerchantCard status. Options: "active", "expired", "canceled", "blocked"</item>
		///     <item>tags [list of strings]: list of strings for tagging.</item>
		///     <item>created [string]: creation datetime for the MerchantCard.</item>
		///     <item>updated [string]: latest update datetime for the MerchantCard.</item>
		/// </list>
		/// </summary>
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
		/// <summary>
		/// Retrieve a specific MerchantCard
		/// <br/>
		/// Receive a single MerchantCard object previously created in the Stark Bank API by passing its id.
		/// <br/>
		/// Parameters (required):
		/// <list>
		///     <item>id [string]: object unique id.</item>
		/// </list>
		/// <br/>
		/// Return:
		/// <list>
		///     <item>MerchantCard object with updated attributes</item>
		/// </list>
		/// </summary>
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

		/// <summary>
		/// Retrieve MerchantCards
		/// <br/>
		/// Receive an IEnumerable of MerchantCard objects previously created in the Stark Bank API.
		/// <br/>
		/// Parameters (optional):
		/// <list>
		///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
		///     <item>status [string, default null]: filter for status of retrieved objects. Options: "active", "expired", "canceled", "blocked"</item>
		///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
		///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
		///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
		///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
		///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
		/// </list>
		/// <br/>
		/// Return:
		/// <list>
		///     <item>IEnumerable of MerchantCard objects with updated attributes</item>
		/// </list>
		/// </summary>
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

        /// <summary>
        /// Retrieve paged MerchantCards
        /// <br/>
        /// Receive a list of up to 100 MerchantCard objects previously created in the Stark Bank API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. Options: "active", "expired", "canceled", "blocked"</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of MerchantCard objects with updated attributes and cursor to retrieve the next page of MerchantCard objects</item>
        /// </list>
        /// </summary>
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

