using System;
using System.Collections.Generic;
using System.Linq;
using StarkBank.Utils;


namespace StarkBank
{
    /// <summary>
    /// CorporateTransaction object
    /// <br/>
    /// The CorporateTransaction objects created in your Workspace to represent each balance shift.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>ID [string]: unique id returned when CorporateTransaction is created. ex: "5656565656565656"</item>
    ///     <item>Amount [long]: CorporateTransaction value in cents. ex: 1234 (= R$ 12.34)</item>
    ///     <item>Balance [integer]: balance amount of the Workspace at the instant of the Transaction in cents. ex: 200 (= R$ 2.00)</item>
    ///     <item>Description [string]: CorporateTransaction description. ex: "Buying food"</item>
    ///     <item>Source [string]: source of the transaction. ex: "corporate-purchase/5656565656565656"</item>
    ///     <item>Tags [list of strings]: list of strings inherited from the source resource. ex: new List<string>{ "tony", "stark" }</item>
    ///     <item>Created [DateTime]: creation DateTime for the CorporateTransaction. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class CorporateTransaction : Resource
    {
        public long Amount { get; }
        public long Balance { get; }
        public string Description { get; }
        public string Source { get; }
        public List<string> Tags { get; }
        public DateTime? Created { get; }

        /// <summary>
        /// CorporateTransaction object
        /// <br/>
        /// The CorporateTransaction objects created in your Workspace to represent each balance shift.
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when CorporateTransaction is created. ex: "5656565656565656"</item>
        ///     <item>amount [long]: CorporateTransaction value in cents. ex: 1234 (= R$ 12.34)</item>
        ///     <item>balance [integer]: balance amount of the Workspace at the instant of the Transaction in cents. ex: 200 (= R$ 2.00)</item>
        ///     <item>description [string]: CorporateTransaction description. ex: "Buying food"</item>
        ///     <item>source [string]: source of the transaction. ex: "corporate-purchase/5656565656565656"</item>
        ///     <item>tags [list of strings]: list of strings inherited from the source resource. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>created [DateTime]: creation DateTime for the CorporateTransaction. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public CorporateTransaction(long amount, long balance, string description, string source, List<string> tags, DateTime? created = null, string id = null) : base(id)
        {
            Amount = amount;
            Balance = balance;
            Description = description;
            Source = source;
            Tags = tags;
            Created = created;
        }

        /// <summary>
        /// Retrieve a specific CorporateTransaction by its id
        /// <br/>
        /// Receive a single CorporateTransaction object previously created in the Stark Bank API by passing its id
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
        ///     <item>CorporateTransaction object that corresponds to the given id.</item>
        /// </list>
        /// </summary>
        public static CorporateTransaction Get(string id, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as CorporateTransaction;
        }

        /// <summary>
        /// Retrieve CorporateTransaction objects
        /// <br/>
        /// Receive an IEnumerable of CorporateTransaction objects previously created in the Stark Bank API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "approved", "canceled", "denied", "confirmed" or "voided"</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>externalIds [list of strings, default null]: external IDs. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>ids [list of strings, default null]: purchase IDs. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of CorporateTransaction objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<CorporateTransaction> Query(List<string> tags = null, List<string> externalIds = null,
            DateTime? after = null, DateTime? before = null, string status = null, List<string> ids = null, int? limit = 1, 
            User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "tags" , tags },
                    { "externalIds" , externalIds },
                    { "after" , after },
                    { "before" , before },
                    { "status" , status },
                    { "ids" , ids },
                    { "limit" , limit }
                },
                user: user
            ).Cast<CorporateTransaction>();
        }

        /// <summary>
        /// Retrieve paged CorporateTransaction objects
        /// <br/>
        /// Receive a list of up to 100 CorporateTransaction objects previously created in the Stark Bank API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>externalIds [list of strings, default null]: external IDs. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "approved", "canceled", "denied", "confirmed" or "voided"</item>
        ///     <item>ids [list of strings, default null]: purchase IDs. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35.</item>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of CorporateTransaction objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of CorporateTransaction objects</item>
        /// </list>
        /// </summary>
        public static (List<CorporateTransaction> page, string pageCursor) Page(List<string> tags = null, 
            List<string> externalIds = null, DateTime? after = null, DateTime? before = null, string status = null,
            List<string> ids = null, string cursor = null, int? limit = 1, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            (List<SubResource> page, string pageCursor) = Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "tags" , tags },
                    { "externalIds" , externalIds },
                    { "after" , after },
                    { "before" , before },
                    { "status" , status },
                    { "ids" , ids },
                    { "limit" , limit },
                    { "cursor", cursor }
                },
                user: user
            );
            List<CorporateTransaction> transactions = new List<CorporateTransaction>();
            foreach (SubResource subResource in page)
            {
                transactions.Add(subResource as CorporateTransaction);
            }
            return (transactions, pageCursor);
        }

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "CorporateTransaction", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            long amount = json.amount;
            long balance = json.balance;
            string description = json.description;
            string source = json.source;
            List<string> tags = json.tags?.ToObject<List<string>>();
            string createdString = json.created;
            DateTime created = Checks.CheckDateTime(createdString);

            return new CorporateTransaction(
                id: id, amount: amount, balance: balance, description: description, source: source, 
                tags: tags, created: created
            );
        }
    }
}
