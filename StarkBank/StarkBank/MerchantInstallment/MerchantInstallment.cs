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

		/// <summary>
		/// MerchantInstallment object
		/// <br/>
		/// Represents one installment of a MerchantPurchase.
		/// <br/>
		/// Attributes (return-only):
		/// <list>
		///     <item>id [string]: unique id returned when the MerchantInstallment is created. ex: "5656565656565656"</item>
		///     <item>amount [integer]: installment value in cents. ex: 100 (= R$1.00)</item>
		///     <item>due [DateTime]: installment due date.</item>
		///     <item>fee [integer]: fee charged for this installment. ex: 200 (= R$ 2.00)</item>
		///     <item>fundingType [string]: funding type of the installment. ex: "credit", "debit"</item>
		///     <item>network [string]: card network.</item>
		///     <item>purchaseId [string]: id of the MerchantPurchase to which this installment belongs.</item>
		///     <item>status [string]: current MerchantInstallment status.</item>
		///     <item>tags [list of strings]: list of strings for tagging.</item>
		///     <item>transactionIds [list of strings]: ledger transaction ids linked to this installment.</item>
		///     <item>created [DateTime]: creation datetime for the installment.</item>
		///     <item>updated [DateTime]: latest update datetime for the installment.</item>
		/// </list>
		/// </summary>
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

		/// <summary>
		/// Retrieve a specific MerchantInstallment
		/// <br/>
		/// Receive a single MerchantInstallment object previously created in the Stark Bank API by passing its id.
		/// <br/>
		/// Parameters (required):
		/// <list>
		///     <item>id [string]: object unique id.</item>
		/// </list>
		/// <br/>
		/// Return:
		/// <list>
		///     <item>MerchantInstallment object with updated attributes</item>
		/// </list>
		/// </summary>
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

		/// <summary>
		/// Retrieve MerchantInstallments
		/// <br/>
		/// Receive an IEnumerable of MerchantInstallment objects previously created in the Stark Bank API.
		/// <br/>
		/// Parameters (optional):
		/// <list>
		///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
		///     <item>status [string, default null]: filter for status of retrieved objects.</item>
		///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
		///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
		///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
		///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
		///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
		/// </list>
		/// <br/>
		/// Return:
		/// <list>
		///     <item>IEnumerable of MerchantInstallment objects with updated attributes</item>
		/// </list>
		/// </summary>
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

		/// <summary>
		/// Retrieve paged MerchantInstallments
		/// <br/>
		/// Receive a list of up to 100 MerchantInstallment objects previously created in the Stark Bank API and the cursor to the next page.
		/// Use this function instead of query if you want to manually page your requests.
		/// <br/>
		/// Parameters (optional):
		/// <list>
		///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
		///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
		///     <item>status [string, default null]: filter for status of retrieved objects.</item>
		///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
		///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
		///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
		///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
		///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
		/// </list>
		/// <br/>
		/// Return:
		/// <list>
		///     <item>list of MerchantInstallment objects with updated attributes and cursor to retrieve the next page of MerchantInstallment objects</item>
		/// </list>
		/// </summary>
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

