using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkBank
{
    /// <summary>
    /// Invoice object
    /// <br/>
    /// When you initialize an Invoice, the entity will not be automatically
    /// sent to the Stark Bank API. The 'create' function sends the objects
    /// to the Stark Bank API and returns the list of created objects.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Amount [long integer]: Invoice value in cents. Minimum = 0 (any value will be accepted). ex: 1234 (= R$ 12.34)</item>
    ///     <item>Name [string]: payer name. ex: "Iron Bank S.A."</item>
    ///     <item>TaxID [string]: payer tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>

    ///     <item>Due [string, default today + 2 days]: Invoice due date in UTC ISO format. ex: "2020-10-28T17:59:26.249976+00:00"</item>
    ///     <item>Expiration [long integer, default null]: time interval in seconds between due date and expiration date. ex 123456789</item>
    ///     <item>Fine [float, default 2.0]: Invoice fine for overdue payment in %. ex: 2.5</item>
    ///     <item>Interest [float, default 1.0]: Invoice monthly interest for overdue payment in %. ex: 5.2</item>
    ///     <item>Descriptions [list of dictionaries, default null]: list of dictionaries with "key":string and (optional) "value":string pairs. ex: new List<Dictionary<string,string>>(){new Dictionary<string, string>{{"key", "Taxes"},{"value", "100"}}</item>
    ///     <item>Discounts [list of dictionaries, default null]: list of dictionaries with "percentage":float and "due":string pairs. ex: new List<Dictionary<string,string>>(){new Dictionary<string, string>{{"percentage", 1.5},{"due", "2020-11-25T17:59:26.249976+00:00"}}</item>
    ///     <item>Tags [list of strings, default null]: list of strings for tagging</item>

    ///     <item>NominalAmount [long integer, default null]: Invoice emission value in cents (will change if invoice is updated, but not if it's paid). ex: 400000</item>
    ///     <item>FineAmount [long integer, default null]: Invoice fine value calculated over NominalAmount. ex: 20000</item>
    ///     <item>InterestAmount [long integer, default null]: Invoice interest value calculated over NominalAmount. ex: 10000</item>
    ///     <item>DiscountAmount [long integer, default null]: Invoice discount value calculated over NominalAmount. ex: 3000</item>
    ///     <item>ID [string, default null]: unique id returned when Invoice is created. ex: "5656565656565656"</item>
    ///     <item>BrCode [string, default null]: BR Code for the Invoice payment. ex: "00020101021226800014br.gov.bcb.pix2558invoice.starkbank.com/f5333103-3279-4db2-8389-5efe335ba93d5204000053039865802BR5913Arya Stark6009Sao Paulo6220051656565656565656566304A9A0"</item>
    ///     <item>Status [string, default null]: current Invoice status. ex: "created", "paid", "canceled" or "overdue"</item>
    ///     <item>Created [DateTime, default null]: creation datetime for the Invoice. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime, default null]: latest update datetime for the Invoice. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class Invoice : Utils.Resource
    {
        public long Amount { get; }
        public string Name { get; }
        public string TaxID { get; }
        public string Due { get; }
        public long? Expiration { get; }
        public double? Fine { get; }
        public double? Interest { get; }
        public List<Dictionary<string, object>> Descriptions { get; }
        public List<Dictionary<string, object>> Discounts { get; }
        public List<string> Tags { get; }

        public long? NominalAmount { get; }
        public long? FineAmount { get; }
        public long? InterestAmount { get; }
        public long? DiscountAmount { get; }
        public string BrCode { get; }
        public string Status { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// Invoice object
        /// <br/>
        /// When you initialize an Invoice, the entity will not be automatically
        /// sent to the Stark Bank API. The 'create' function sends the objects
        /// to the Stark Bank API and returns the list of created objects.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>amount [long integer]: Invoice value in cents. Minimum = 0 (any value will be accepted). ex: 1234 (= R$ 12.34)</item>
        ///     <item>due [string, default today + 2 days]: Invoice due date in UTC ISO format. ex: "2020-10-28T17:59:26.249976+00:00"</item>
        ///     <item>name [string]: payer name. ex: "Iron Bank S.A."</item>
        ///     <item>taxID [string]: payer tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>expiration [long integer, default null]: time interval in seconds between due date and expiration date. ex 123456789</item>
        ///     <item>fine [float, default 2.0]: Invoice fine for overdue payment in %. ex: 2.5</item>
        ///     <item>interest [float, default 1.0]: Invoice monthly interest for overdue payment in %. ex: 5.2</item>
        ///     <item>descriptions [list of dictionaries, default null]: list of dictionaries with "key":string and (optional) "value":string pairs. ex: new List<Dictionary<string,string>>(){new Dictionary<string, string>{{"key", "Taxes"},{"value", "100"}}</item>
        ///     <item>discounts [list of dictionaries, default null]: list of dictionaries with "percentage":float and "due":string pairs. ex: new List<Dictionary<string,string>>(){new Dictionary<string, string>{{"percentage", 1.5},{"due", "2020-11-25T17:59:26.249976+00:00"}}</item>
        ///     <item>tags [list of strings, default null]: list of strings for tagging</item>
        /// </list>
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>nominalAmount [long integer, default null]: Invoice emission value in cents (will change if invoice is updated, but not if it's paid). ex: 400000</item>
        ///     <item>fineAmount [long integer, default null]: Invoice fine value calculated over nominalAmount. ex: 20000</item>
        ///     <item>interestAmount [long integer, default null]: Invoice interest value calculated over nominalAmount. ex: 10000</item>
        ///     <item>discountAmount [long integer, default null]: Invoice discount value calculated over nominalAmount. ex: 3000</item>
        ///     <item>id [string, default null]: unique id returned when Invoice is created. ex: "5656565656565656"</item>
        ///     <item>brCode [string, default null]: BR Code for the Invoice payment. ex: "00020101021226800014br.gov.bcb.pix2558invoice.starkbank.com/f5333103-3279-4db2-8389-5efe335ba93d5204000053039865802BR5913Arya Stark6009Sao Paulo6220051656565656565656566304A9A0"</item>
        ///     <item>status [string, default null]: current Invoice status. ex: "created", "paid", "canceled" or "overdue"</item>
        ///     <item>created [DateTime, default null]: creation datetime for the Invoice. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime, default null]: latest update datetime for the Invoice. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public Invoice(long amount, string name, string taxID, string due, long? expiration = null, double? fine = null, double? interest = null,
            List<string> tags = null, List<Dictionary<string, object>> descriptions = null, List<Dictionary<string, object>> discounts = null,
            long? nominalAmount = null, long? fineAmount = null, long? interestAmount = null, long? discountAmount = null,
            string id = null, string brCode = null, string status = null, DateTime? created = null, DateTime? updated = null) : base(id)
        {
            Amount = amount;
            Name = name;
            TaxID = taxID;
            Expiration = expiration;
            Due = due;

            Fine = fine;
            Interest = interest;
            Tags = tags;
            Descriptions = descriptions;
            Discounts = discounts;
            NominalAmount = nominalAmount;
            FineAmount = fineAmount;
            InterestAmount = interestAmount;
            DiscountAmount = discountAmount;
            BrCode = brCode;
            Status = status;
            Created = created;
            Updated = updated;
        }

        /// <summary>
        /// Create Invoices
        /// <br/>
        /// Send a list of Invoice objects for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>Invoices [list of Invoice objects]: list of Invoice objects to be created in the API</item>
        /// <br/>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Project object]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of Invoice objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<Invoice> Create(List<Invoice> invoices, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: invoices,
                user: user
            ).ToList().ConvertAll(o => (Invoice)o);
        }

        /// <summary>
        /// Create Invoices
        /// <br/>
        /// Send a list of Invoice objects for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>invoices [list of Dictionaries]: list of Dictionaries representing the Invoices to be created in the API</item>
        /// <br/>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Project object]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of Invoice objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<Invoice> Create(List<Dictionary<string, object>> invoices, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: invoices,
                user: user
            ).ToList().ConvertAll(o => (Invoice)o);
        }

        /// <summary>
        /// Retrieve a specific Invoice
        /// <br/>
        /// Receive a single Invoice object previously created in the Stark Bank API by passing its id
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
        ///     <item>Invoice object with updated attributes</item>
        /// </list>
        /// </summary>
        public static Invoice Get(string id, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as Invoice;
        }

        /// <summary>
        /// Retrieve Invoices
        /// <br/>
        /// Receive an IEnumerable of Invoice objects previously created in the Stark Bank API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "created", "paid", "canceled" or "overdue"</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of Invoice objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<Invoice> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
            string status = null, List<string> tags = null, List<string> ids = null, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "after", after },
                    { "before", before },
                    { "status", status },
                    { "tags", tags },
                    { "ids", ids }
                },
                user: user
            ).Cast<Invoice>();
        }

        /// <summary>
        /// Update Invoice entity
        /// <br/>
        /// Update an Invoice by passing id, if it hasn't been paid yet.
        /// <br/>
        /// Parameters(required):
        /// <list>
        ///     <item>id[string]: object unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>status [string]: filter for status of retrieved objects. ex: "paid" or "registered"</item>
        ///     <item>amount [long integer]: Nominal amount charge by the invoice. ex: 100 (R$1.00)</item>
        ///     <item>due [string, default today + 2 days]: Invoice due date in UTC ISO format. ex: "2020-10-28T17:59:26.249976+00:00"</item>
        ///     <item>expiration [long integer, default null]: time interval in seconds between the due date and the expiration date. ex 123456789</item>
        ///     <item>user [Project object]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>target Invoice with updated attributes</item>
        /// </list>
        /// </summary>
        public static Invoice Update(string id, string status = null, long? amount = null, string due = null, long? expiration = null, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();

            var payload = new Dictionary<string, object> {
                { "amount", amount },
                { "expiration", expiration },
                { "status", status },
                { "due", due },
            };

            if (status == null) {
                payload.Remove("status");
            }

            if (amount == null) {
                payload.Remove("amount");
            }

            if (due == null) {
                payload.Remove("due");
            }

            if (expiration == null) {
                payload.Remove("expiration");
            }

            return Utils.Rest.PatchId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                payload: payload,
                user: user
            ) as Invoice;
        }


        internal static (string resourceName, Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "Invoice", resourceMaker: ResourceMaker);
        }

        internal static Utils.Resource ResourceMaker(dynamic json)
        {
            long amount = json.amount;
            string name = json.name;
            string taxID = json.taxId;
            string dueString = json.due;
            string due = dueString;
            long? expiration = json.expiration;
            double? fine = json.fine;
            double? interest = json.interest;
            List<string> tags = json.tags.ToObject<List<string>>();
            List<Dictionary<string, object>> descriptions = json.descriptions.ToObject<List<Dictionary<string, object>>>();
            List<Dictionary<string, object>> discounts = json.discounts.ToObject<List<Dictionary<string, object>>>();

            long? nominalAmount = json.nominalAmount;
            long? fineAmount = json.fineAmount;
            long? interestAmount = json.interestAmount;
            long? discountAmount = json.discountAmount;
            string id = json.id;
            string brCode = json.brCode;
            string status = json.status;
            string createdString = json.created;
            string updatedString = json.updated;
            DateTime? created = Utils.Checks.CheckDateTime(createdString);
            DateTime? updated = Utils.Checks.CheckDateTime(updatedString);

            return new Invoice(
                amount: amount, name: name, taxID: taxID, due: due, expiration: expiration, fine: fine,
                interest: interest, tags: tags, descriptions: descriptions, discounts: discounts, nominalAmount: nominalAmount,
                fineAmount: fineAmount, interestAmount: interestAmount, discountAmount: discountAmount,
                id: id, brCode: brCode, status: status, created: created, updated: updated
            );
        }
    }
}