using System;
using StarkCore;
using StarkCore.Utils;
using System.Collections.Generic;
using System.Linq;

namespace StarkBank
{
    /// <summary>
    /// CorporateInvoice object
    /// <br/>
    /// The CorporateInvoice objects created in your Workspace load your Corporate balance when paid.
    /// <br/>
    /// When you initialize a CorporateInvoice, the entity will not be automatically
    /// created in the Stark Bank API. The 'create' function sends the objects
    /// to the Stark Bank API and returns the created object.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Amount [long]: CorporateInvoice value in cents. ex: 1234 (= R$ 12.34)</item>
    ///     <item>Tags [list of strings, default null]: list of strings for tagging. ex: new List<string>{ "travel", "food" }</item>
    ///     <item>ID[string]: unique id returned when CorporateInvoice is created. ex: "5656565656565656"</item>
    ///     <item>Name [string, default sub-issuer name]: payer name. ex: "Iron Bank S.A."</item>
    ///     <item>TaxID [string, default sub-issuer tax ID]: payer tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
    ///     <item>Brcode [string]: BR Code for the Invoice payment. ex: "00020101021226930014br.gov.bcb.pix2571brcode-h.development.starkinfra.com/v2/d7f6546e194d4c64a153e8f79f1c41ac5204000053039865802BR5925Stark Bank S.A. - Institu6009Sao Paulo62070503***63042109"</item>
    ///     <item>Due [DateTime]: Invoice due and expiration date in UTC ISO format. ex: DateTime(2020, 10, 28)</item>
    ///     <item>Link [string]: public Invoice webpage URL. ex: "https://starkbank-card-issuer.development.starkbank.com/invoicelink/d7f6546e194d4c64a153e8f79f1c41ac"</item>
    ///     <item>Status [string]: current CorporateInvoice status status. Options: "created", "expired", "overdue" and "paid"</item>
    ///     <item>CorporateTransactionID [string]: ledger transaction ids linked to this CorporateInvoice. ex: "corporate-invoice/5656565656565656"</item>
    ///     <item>Updated [DateTime]: latest update DateTime for the CorporateInvoice. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Created [DateTime]: creation DateTime for the CorporateInvoice. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class CorporateInvoice : Resource
    {
        public long Amount { get; }
        public string Name { get; }
        public string TaxID { get; }
        public List<string> Tags { get; }
        public string Brcode { get; }
        public DateTime? Due { get; }
        public string Link { get; }
        public string Status { get; }
        public string CorporateTransactionID { get; }
        public DateTime? Updated { get; }
        public DateTime? Created { get; }

        /// <summary>
        /// CorporateInvoice object
        /// <br/>
        /// The CorporateInvoice objects created in your Workspace load your Corporate balance when paid.
        /// <br/>
        /// When you initialize a CorporateInvoice, the entity will not be automatically
        /// created in the Stark Bank API. The 'create' function sends the objects
        /// to the Stark Bank API and returns the created object.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>amount [long]: CorporateInvoice value in cents. ex: 1234 (= R$ 12.34)</item>
        ///</list>
        /// Parameters (optional):
        /// <list>
        ///     <item>tags [list of strings, default null]: list of strings for tagging. ex: new List<string>{ "travel", "food" }</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when CorporateInvoice is created. ex: "5656565656565656"</item>
        ///     <item>name [string]: payer name. ex: "Iron Bank S.A."</item>
        ///     <item>taxId [string]: payer tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
        ///     <item>brcode [string]: BR Code for the Invoice payment. ex: "00020101021226930014br.gov.bcb.pix2571brcode-h.development.starkinfra.com/v2/d7f6546e194d4c64a153e8f79f1c41ac5204000053039865802BR5925Stark Bank S.A. - Institu6009Sao Paulo62070503***63042109"</item>
        ///     <item>due [DateTime]: Invoice due and expiration date in UTC ISO format. ex: DateTime(2020, 10, 28)</item>
        ///     <item>link [string]: public Invoice webpage URL. ex: "https://starkbank-card-issuer.development.starkbank.com/invoicelink/d7f6546e194d4c64a153e8f79f1c41ac"</item>
        ///     <item>status [string]: current CorporateInvoice status status. ex: "created", "expired", "overdue" and "paid"</item>
        ///     <item>corporateTransactionID [string]: ledger transaction ids linked to this CorporateInvoice. ex: "corporate-invoice/5656565656565656"</item>
        ///     <item>updated [DateTime]: latest update DateTime for the CorporateInvoice. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>created [DateTime]: creation DateTime for the CorporateInvoice. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public CorporateInvoice(long amount, string taxID = null, string name = null, 
            List<string> tags = null, string id = null, string brcode = null, 
            DateTime? due = null, string link = null, string status = null, string corporateTransactionID = null,
            DateTime? updated = null, DateTime? created = null
        ) : base(id)
        {
            Amount = amount;
            TaxID = taxID;
            Name = name;
            Tags = tags;
            Brcode = brcode;
            Due = due;
            Link = link;
            Status = status;
            CorporateTransactionID = corporateTransactionID;
            Updated = updated;
            Created = created;
        }

        /// <summary>
        /// Create a CorporateInvoice
        /// <br/>
        /// Send a CorporateInvoice object for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>invoice [CorporateInvoice object]: CorporateInvoice object to be created in the API.</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>CorporateInvoice object with updated attributes</item>
        /// </list>
        /// </summary>
        public static CorporateInvoice Create(CorporateInvoice invoice, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.PostSingle(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entity: invoice,
                user: user
            ) as CorporateInvoice;
        }

        /// <summary>
        /// Create a CorporateInvoice
        /// <br/>
        /// Send a CorporateInvoice object for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>invoice [Dictionaries]: Dictionary representing the CorporateInvoice objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>CorporateInvoice object with updated attributes</item>
        /// </list>
        /// </summary>
        public static CorporateInvoice Create(Dictionary<string, object> invoice, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.PostSingle(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entity: invoice,
                user: user
            ) as CorporateInvoice;
        }

        /// <summary>
        /// Retrieve CorporateInvoice objects
        /// <br/>
        /// Receive an IEnumerable of CorporateInvoice objects previously created in the Stark Bank API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "created", "expired", "overdue" and "paid"</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of CorporateInvoice objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<CorporateInvoice> Query(int? limit = null, DateTime? after = null, 
            DateTime? before = null, string status = null, List<string> tags = null, 
            User user = null
        ) {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "after", after },
                    { "before", before },
                    { "status", status },
                    { "tags", tags }
                },
                user: user
            ).Cast<CorporateInvoice>();
        }

        /// <summary>
        /// Retrieve paged CorporateInvoice objects
        /// <br/>
        /// Receive a list of up to 100 CorporateInvoice objects previously created in the Stark Bank API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35.</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "created", "expired", "overdue" and "paid"</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of CorporateInvoice objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of CorporateInvoice objects</item>
        /// </list>
        /// </summary>
        public static (List<CorporateInvoice> page, string pageCursor) Page(string cursor = null, int? limit = null, 
            DateTime? after = null, DateTime? before = null, string status = null, List<string> tags = null, 
            User user = null
        ) {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            (List<SubResource> page, string pageCursor) = Utils.Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "limit", limit },
                    { "after", after },
                    { "before", before },
                    { "status", status },
                    { "tags", tags }
                },
                user: user
            );
            List<CorporateInvoice> corporateInvoices = new List<CorporateInvoice>();
            foreach (SubResource subResource in page)
            {
                corporateInvoices.Add(subResource as CorporateInvoice);
            }
            return (corporateInvoices, pageCursor);
        }

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "CorporateInvoice", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            long amount = json.amount;
            string taxID = json.taxId;
            string name = json.name;
            List<string> tags = json.tags.ToObject<List<string>>();
            string id = json.id;
            string brcode = json.brcode;
            string dueString = json.due;
            DateTime? due = Checks.CheckNullableDateTime(dueString);
            string link = json.link;
            string status = json.status;
            string corporateTransactionID = json.corporateTransactionId;
            string updatedString = json.updated;
            DateTime? updated = Checks.CheckNullableDateTime(updatedString);
            string createdString = json.created;
            DateTime? created = Checks.CheckNullableDateTime(createdString);

            return new CorporateInvoice(
                amount: amount, taxID: taxID, name: name, tags: tags, id: id, brcode: brcode, 
                due: due, link: link, status: status, corporateTransactionID: corporateTransactionID, 
                updated: updated, created: created           
            );
        }
    }
}
