using StarkBank.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarkBank
{
	public partial class SplitReceiver : Resource
	{
		public string AccountNumber { get; }
		public string AccountType { get; }
		public string BankCode { get; }
		public string BranchCode { get; }
		public DateTime Created { get; }
		public string Name { get; }
		public string Status { get; }
		public List<string> Tags { get; }
		public string TaxId { get; }
		public DateTime Updated { get; }


		public SplitReceiver(string id, string accountNumber, string accountType, string bankCode, string branchCode, DateTime created, string name, string status, List<string> tags, string taxId, DateTime updated)
			: base(id)
		{
			AccountNumber = accountNumber;
			AccountType = accountType;
			BankCode = bankCode;
			BranchCode = branchCode;
			Created = created;
			Name = name;
			Status = status;
			Tags = tags;
			TaxId = taxId;
			Updated = updated;
		}

        /// <summary>
        /// Create SplitReceivers
        /// <br/>
        /// Send a list of SplitReceiver objects for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>splitReceiver [list of Boleto objects]: list of SplitReceiver objects to be created in the API</item>
        /// <br/>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of SplitReceiver objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<SplitReceiver> Create(List<SplitReceiver> splitReceiver, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: splitReceiver,
                user: user
            ).ToList().ConvertAll(o => (SplitReceiver)o);
        }

        /// <summary>
        /// Create SplitReceivers
        /// <br/>
        /// Send a list of SplitReceiver objects for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>splitReceiver [list of Dictionaries]: list of Dictionaries representing the SplitReceivers to be created in the API</item>
        /// <br/>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of SplitReceiver objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<SplitReceiver> Create(List<Dictionary<string, object>> splitReceiver, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: splitReceiver,
                user: user
            ).ToList().ConvertAll(o => (SplitReceiver)o);
        }

        /// <summary>
        /// Retrieve a specific SplitReceiver
        /// <br/>
        /// Receive a single SplitReceiver object previously created in the Stark Bank API by passing its id
        /// <br/>
        /// Parameters(required):
        /// <list>
        ///     <item>id[string]: object unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters(optional):
        /// <list>
        ///     <item>user[Project object]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>SplitReceiver object with updated attributes</item>
        /// </list>
        /// </summary>
        public static SplitReceiver Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as SplitReceiver;
        }

        /// <summary>
        /// Retrieve SplitReceivers
        /// <br/>
        /// Receive an IEnumerable of SplitReceiver objects previously created in the Stark Bank API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "paid" or "registered"</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>taxId [list of strings, default null]: filter for splitReceivers sent to the specified tax ID. ex: '012.345.678-90'</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
        ///     <item>receiverIds [list of strings, default null]: list of receiver ids to filter retrieved objects. ex: ['5656565656565656', '4545454545454545'].</item>
        ///     <item>transactionIds [list of strings, default null]: list of transaction IDs linked to the desired splitReceivers. ex: ['5656565656565656', '4545454545454545']</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of SplitReceivers objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<SplitReceiver> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
            string status = null, List<string> tags = null, string taxId = null, List<string> ids = null, List<string> receiverIds = null, List<string> transactionIds = null, User user = null)
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
                    { "taxId", taxId },
                    { "receiverIds", receiverIds },
                    { "transactionIds", transactionIds },
                    { "ids", ids },
                },
                user: user
            ).Cast<SplitReceiver>();
        }

        /// 
        /// <summary>
        /// Retrieve paged SplitReceivers
        /// <br/>
        /// Receive a list of up to 100 SplitReceiver objects previously created in the Stark Bank API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "paid" or "registered"</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>taxId [list of strings, default null]: filter for splitReceivers sent to the specified tax ID. ex: '012.345.678-90'</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
        ///     <item>receiverIds [list of strings, default null]: list of receiver ids to filter retrieved objects. ex: ['5656565656565656', '4545454545454545'].</item>
        ///     <item>transactionIds [list of strings, default null]: list of transaction IDs linked to the desired splitReceivers. ex: ['5656565656565656', '4545454545454545']</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>List of SplitReceiver objects with updated attributes and cursor to retrieve the next page of SplitReceiver objects</item>
        /// </list>
        /// </summary>
        public static (List<SplitReceiver> page, string pageCursor) Page(string cursor = null, int ? limit = null, DateTime? after = null, DateTime? before = null, string status = null,
            List<string> tags = null, string taxId = null, List<string> ids = null, List<string> receiverIds = null, List<string> transactionIds = null, User user = null)
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
                    { "taxId", taxId },
                    { "receiverIds", receiverIds },
                    { "transactionIds", transactionIds },
                    { "ids", ids },
                },
                user: user
            );
            List<SplitReceiver> splitReceivers = new List<SplitReceiver>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                splitReceivers.Add(subResource as SplitReceiver);
            }
            return (splitReceivers, pageCursor);
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "split-receiver", resourceMaker: ResourceMaker);
        }

        internal static StarkCore.Utils.Resource ResourceMaker(dynamic json)
        {
            string accountNumber = json.accountNumber;
            string accountType = json.accountType;
            string bankCode = json.bankCode;
            string id = json.id;
            string branchCode = json.branchCode;
            string name = json.name;
            DateTime created = StarkCore.Utils.Checks.CheckDateTime((string)json.created);
            List<string> tags = json.tags.ToObject<List<string>>();
            string taxId = json.taxId;
            string status = json.status;
            DateTime updated = StarkCore.Utils.Checks.CheckDateTime((string)json.updated);

            return new SplitReceiver(
                id: id, accountNumber: accountNumber, accountType: accountType, bankCode: bankCode, branchCode: branchCode, created: created, name: name, status: status, tags: tags, taxId: taxId, updated: updated
                );
        }
    }
}
