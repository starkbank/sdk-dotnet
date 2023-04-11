using System;
using System.Collections.Generic;
using System.Linq;
using StarkBank.Utils;


namespace StarkBank
{
    /// <summary>
    /// CorporateWithdrawal object
    /// <br/>
    /// The CorporateWithdrawal objects created in your Workspace return cash from your Corporate balance to your Banking balance.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Amount [long]: CorporateWithdrawal value in cents. Minimum = 0 (any value will be accepted). ex: 1234 (= R$ 12.34)</item>
    ///     <item>ExternalID [string] CorporateWithdrawal external ID. ex: "12345"</item>
    ///     <item>Tags [list of strings, default null]: list of strings for tagging. ex: new List<string>{ "tony", "stark" }</item>
    ///     <item>ID[string]: unique id returned when CorporateWithdrawal is created. ex: "5656565656565656"</item>
    ///     <item>TransactionID [string]: Stark Bank ledger transaction ids linked to this CorporateWithdrawal</item>
    ///     <item>CorporateTransactionID [string]: corporate ledger transaction ids linked to this CorporateWithdrawal</item>
    ///     <item>Updated [DateTime]: latest update DateTime for the CorporateWithdrawal. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Created [DateTime]: creation DateTime for the CorporateWithdrawal. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class CorporateWithdrawal : Resource
    {
        public long Amount { get; }
        public string TransactionID { get; }
        public string CorporateTransactionID { get; }
        public string ExternalID { get; }
        public List<string> Tags { get; }
        public DateTime? Updated { get; }
        public DateTime? Created { get; }

        /// <summary>
        /// CorporateWithdrawal object
        /// <br/>
        /// The CorporateWithdrawal objects created in your Workspace return cash from your Corporate balance to your Banking balance.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>amount [integer]: CorporateWithdrawal value in cents. Minimum = 0 (any value will be accepted). ex: 1234 (= R$ 12.34)</item>
        ///     <item>externalID [string] CorporateWithdrawal external ID. ex: "12345"</item>
        ///</list>
        /// Parameters (optional):
        /// <list>
        ///     <item>tags [list of strings, default null]: list of strings for tagging. ex: new List<string>{ "tony", "stark" }</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when CorporateWithdrawal is created. ex: "5656565656565656"</item>
        ///     <item>transactionID [string]: Stark Bank ledger transaction ids linked to this CorporateWithdrawal</item>
        ///     <item>corporateTransactionID [string]: corporate ledger transaction ids linked to this CorporateWithdrawal</item>
        ///     <item>updated [DateTime]: latest update DateTime for the CorporateWithdrawal. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>created [DateTime]: creation DateTime for the CorporateWithdrawal. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public CorporateWithdrawal(long amount, string externalID, List<string> tags = null, string id = null,
            string transactionID = null, string corporateTransactionID = null, DateTime? updated = null, DateTime? created = null
        ) : base(id)
        {
            Amount = amount;
            TransactionID = transactionID;
            CorporateTransactionID = corporateTransactionID;
            ExternalID = externalID;
            Tags = tags;
            Updated = updated;
            Created = created;
        }

        /// <summary>
        /// Create a CorporateWithdrawal
        /// <br/>
        /// Send a CorporateWithdrawal object for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>withdrawal [CorporateWithdrawal object]: CorporateWithdrawal object to be created in the API.</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>CorporateWithdrawal object with updated attributes</item>
        /// </list>
        /// </summary>
        public static CorporateWithdrawal Create(CorporateWithdrawal withdrawal, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PostSingle(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entity: withdrawal,
                user: user
            ) as CorporateWithdrawal;
        }
        
        /// <summary>
        /// Create CorporateWithdrawal
        /// <br/>
        /// Send a CorporateWithdrawal dictionary for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>withdrawal [dictionary]: Dictionary representing the CorporateWithdrawal to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>CorporateWithdrawal object with updated attributes</item>
        /// </list>
        /// </summary>
        public static CorporateWithdrawal Create(Dictionary<string, object> withdrawal, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PostSingle(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entity: withdrawal,
                user: user
            ) as CorporateWithdrawal;
        }

        /// <summary>
        /// Retrieve a specific CorporateWithdrawal by its id
        /// <br/>
        /// Receive a single CorporateWithdrawal object previously created in the Stark Bank API by passing its id
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: object unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>CorporateWithdrawal object that corresponds to the given id.</item>
        /// </list>
        /// </summary>
        public static CorporateWithdrawal Get(string id, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as CorporateWithdrawal;
        }

        /// <summary>
        /// Retrieve CorporateWithdrawal objects
        /// <br/>
        /// Receive an IEnumerable of CorporateWithdrawal objects previously created in the Stark Bank API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>externalIds [list of strings, default null]: external IDs. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkBank.Settings. User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of CorporateWithdrawal objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<CorporateWithdrawal> Query(int? limit = null, string externalID = null, DateTime? after = null, DateTime? before = null,
            List<string> tags = null, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit" , limit },
                    { "externalID" , externalID },
                    { "after" , after },
                    { "before" , before },
                    { "tags" , tags }
                },
                user: user
            ).Cast<CorporateWithdrawal>();
        }

        /// <summary>
        /// Retrieve paged CorporateWithdrawal objects
        /// <br/>
        /// Receive a list of up to 100 CorporateWithdrawal objects previously created in the Stark Bank API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35.</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>externalIds [list of strings, default null]: external IDs. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of CorporateWithdrawal objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of CorporateWithdrawal objects</item>
        /// </list>
        /// </summary>
        public static (List<CorporateWithdrawal> page, string pageCursor) Page(string cursor = null, int? limit = null, string externalID = null,
            DateTime? after = null, DateTime? before = null, List<string> tags = null, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            (List<SubResource> page, string pageCursor) = Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "limit" , limit },
                    { "externalID" , externalID },
                    { "after" , after },
                    { "before" , before },
                    { "tags" , tags }
                },
                user: user
            );
            List<CorporateWithdrawal> withdrawals = new List<CorporateWithdrawal>();
            foreach (SubResource subResource in page)
            {
                withdrawals.Add(subResource as CorporateWithdrawal);
            }
            return (withdrawals, pageCursor);
        }

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "CorporateWithdrawal", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            long amount = json.amount;
            string transactionID = json.transactionId;
            string corporateTransactionID = json.corporateTransactionId;
            string externalID = json.externalId;
            List<string> tags = json.tags?.ToObject<List<string>>();
            string createdString = json.created;
            DateTime created = Checks.CheckDateTime(createdString);
            string updatedString = json.updated;
            DateTime updated = Checks.CheckDateTime(updatedString);

            return new CorporateWithdrawal(
                id: id, amount: amount, transactionID: transactionID, tags: tags, externalID: externalID,
                corporateTransactionID: corporateTransactionID, updated: updated, created: created
            );
        }
    }
}
