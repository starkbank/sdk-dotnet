using System;
using System.Linq;
using StarkBank.Utils;
using System.Collections.Generic;

namespace StarkBank
{
    /// <summary>
    /// Invoice object
    /// <br/>
    /// When you initialize an Invoice, the entity will not be automatically
    /// sent to the Stark Bank API. The 'create' function sends the objects
    /// to the Stark Bank API and returns the list of created objects.
    /// To create scheduled Invoices, which will display the discount, interest, etc. on the final users banking interface,
    /// use dates instead of datetimes on the "due" and "discounts" fields.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Amount [long integer]: Invoice value in cents. Minimum = 0 (any value will be accepted). ex: 1234 (= R$ 12.34)</item>
    ///     <item>TaxID [string]: payer tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
    ///     <item>Name [string]: payer name. ex: "Iron Bank S.A."</item>
    ///     <item>Due [DateTime, default now + 2 days]: Invoice due date in UTC ISO format. ex: DateTime(2020, 3, 10, 10, 30, 0, 0) for immediate invoices and DateTime(2020, 3, 10) for scheduled invoices</item>
    ///     <item>Expiration [long integer, default null]: time interval in seconds between due date and expiration date. ex 123456789</item>
    ///     <item>Fine [float, default 2.0]: Invoice fine for overdue payment in %. ex: 2.5</item>
    ///     <item>Interest [float, default 1.0]: Invoice monthly interest for overdue payment in %. ex: 5.2</item>
    ///     <item>Discounts [list of dictionaries, default null]: list of dictionaries with "percentage":float and "due":string pairs. ex: new List<Dictionary<string,string>>(){new Dictionary<string, string>{{"percentage", 1.5},{"due", "2020-11-25T17:59:26.249976+00:00"}}</item>
    ///     <item>Rules [list of StarkBank.Invoice.Rule objects, default null]: list of Invoice.Rule objects for modifying invoice behavior. ex: [Invoice.Rule(key="allowedTaxIds", value=["012.345.678-90", "45.059.493/0001-73"])]</item>
    ///     <item>Tags [list of strings, default null]: list of strings for tagging</item>
    ///     <item>Descriptions [list of dictionaries, default null]: list of dictionaries with "key":string and (optional) "value":string pairs. ex: new List<Dictionary<string,string>>(){new Dictionary<string, string>{{"key", "Taxes"},{"value", "100"}}</item>
    ///     <item>PdfUrl [string]: public Invoice PDF URL. ex: "https://invoice.starkbank.com/pdf/d454fa4e524441c1b0c1a729457ed9d8"</item>
    ///     <item>Link [string]: public Invoice webpage URL. ex: "https://my-workspace.sandbox.starkbank.com/invoicelink/d454fa4e524441c1b0c1a729457ed9d8"</item>
    ///     <item>NominalAmount [long integer]: Invoice emission value in cents (will change if invoice is updated, but not if it's paid). ex: 400000</item>
    ///     <item>FineAmount [long integer]: Invoice fine value calculated over NominalAmount. ex: 20000</item>
    ///     <item>InterestAmount [long integer]: Invoice interest value calculated over NominalAmount. ex: 10000</item>
    ///     <item>DiscountAmount [long integer]: Invoice discount value calculated over NominalAmount. ex: 3000</item>
    ///     <item>ID [string]: unique id returned when Invoice is created. ex: "5656565656565656"</item>
    ///     <item>Brcode [string]: BR Code for the Invoice payment. ex: "00020101021226800014br.gov.bcb.pix2558invoice.starkbank.com/f5333103-3279-4db2-8389-5efe335ba93d5204000053039865802BR5913Arya Stark6009Sao Paulo6220051656565656565656566304A9A0"</item>
    ///     <item>Status [string]: current Invoice status. ex: "created", "paid", "canceled" or "overdue"</item>
    ///     <item>Fee [integer]: fee charged by this Invoice. ex: 65 (= R$ 0.65)</item>
    ///     <item>TransactionIds [list of strings]: ledger transaction ids linked to this Invoice (if there are more than one, all but the first are reversals or failed reversal chargebacks). ex: ["19827356981273"]</item>
    ///     <item>Created [DateTime]: creation datetime for the Invoice. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime]: latest update datetime for the Invoice. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Splits [list of StarkBank.Split]: Array of Split objects to indicate payment receivers</item>
    /// </list>
    /// </summary>
    public partial class Invoice : Resource
    {
        public string Name { get; }
        public string TaxID { get; }
        public DateTime? Due { get; }
        public long? Expiration { get; }
        public double? Fine { get; }
        public double? Interest { get; }
        public List<Dictionary<string, object>> Descriptions { get; }
        public List<Dictionary<string, object>> Discounts { get; }
        public List<Rule> Rules { get; }
        public List<string> Tags { get; }
        public long Amount { get; }
        public long? NominalAmount { get; }
        public long? FineAmount { get; }
        public long? InterestAmount { get; }
        public long? DiscountAmount { get; }
        public string Brcode { get; }
        public int? Fee { get; }
        public string PdfUrl { get; }
        public string Link { get; }
        public string Status { get; }
        public List<string> TransactionIds { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }
        public List<Split> Splits { get; }

        /// <summary>
        /// Invoice object
        /// <br/>
        /// When you initialize an Invoice, the entity will not be automatically
        /// sent to the Stark Bank API. The 'create' function sends the objects
        /// to the Stark Bank API and returns the list of created objects.
        /// To create scheduled Invoices, which will display the discount, interest, etc. on the final users banking interface,
        /// use dates instead of datetimes on the "due" and "discounts" fields.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>amount [long integer]: Invoice value in cents. Minimum = 0 (any value will be accepted). ex: 1234 (= R$ 12.34)</item>
        ///     <item>taxID [string]: payer tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
        ///     <item>name [string]: payer name. ex: "Iron Bank S.A."</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>due [DateTime, default now + 2 days]: Invoice due date in UTC ISO format. ex: DateTime(2020, 3, 10, 10, 30, 0, 0) for immediate invoices and DateTime(2020, 3, 10) for scheduled invoices</item>
        ///     <item>expiration [long integer, default null]: time interval in seconds between due date and expiration date. ex 123456789</item>
        ///     <item>fine [float, default 2.0]: Invoice fine for overdue payment in %. ex: 2.5</item>
        ///     <item>interest [float, default 1.0]: Invoice monthly interest for overdue payment in %. ex: 5.2</item>
        ///     <item>discounts [list of dictionaries, default null]: list of dictionaries with "percentage":float and "due":string pairs. ex: new List<Dictionary<string,object>>(){new Dictionary<string, string>{{"percentage", 1.5},{"due", DateTime(2020, 3, 10, 10, 30, 12, 15)}}</item>
        ///     <item>rules [list of StarkBank.Invoice.Rule objects, default null]: list of Invoice.Rule objects for modifying invoice behavior. ex: [Invoice.Rule(key="allowedTaxIds", value=["012.345.678-90", "45.059.493/0001-73"])]</item>
        ///     <item>tags [list of strings, default null]: list of strings for tagging</item>
        ///     <item>descriptions [list of dictionaries, default null]: list of dictionaries with "key":string and (optional) "value":string pairs. ex: new List<Dictionary<string,string>>(){new Dictionary<string, string>{{"key", "Taxes"},{"value", "100"}}</item>
        ///     <item>splits [list of Split]: array of Split objects to indicate payment receivers</item>
        /// </list>
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>pdfUrl [string]: public Invoice PDF URL. ex: "https://invoice.starkbank.com/pdf/d454fa4e524441c1b0c1a729457ed9d8"</item>
        ///     <item>link [string]: public Invoice webpage URL. ex: "https://my-workspace.sandbox.starkbank.com/invoicelink/d454fa4e524441c1b0c1a729457ed9d8"</item>
        ///     <item>nominalAmount [long integer]: Invoice emission value in cents (will change if invoice is updated, but not if it's paid). ex: 400000</item>
        ///     <item>fineAmount [long integer]: Invoice fine value calculated over nominalAmount. ex: 20000</item>
        ///     <item>interestAmount [long integer]: Invoice interest value calculated over nominalAmount. ex: 10000</item>
        ///     <item>discountAmount [long integer]: Invoice discount value calculated over nominalAmount. ex: 3000</item>
        ///     <item>id [string]: unique id returned when Invoice is created. ex: "5656565656565656"</item>
        ///     <item>brcode [string]: BR Code for the Invoice payment. ex: "00020101021226800014br.gov.bcb.pix2558invoice.starkbank.com/f5333103-3279-4db2-8389-5efe335ba93d5204000053039865802BR5913Arya Stark6009Sao Paulo6220051656565656565656566304A9A0"</item>
        ///     <item>status [string]: current Invoice status. ex: "created", "paid", "canceled" or "overdue"</item>
        ///     <item>fee [integer]: fee charged by this Invoice. ex: 65 (= R$ 0.65)</item>
        ///     <item>transactionIds [list of strings]: ledger transaction ids linked to this Invoice (if there are more than one, all but the first are reversals or failed reversal chargebacks). ex: ["19827356981273"]</item>
        ///     <item>created [DateTime]: creation datetime for the Invoice. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime]: latest update datetime for the Invoice. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public Invoice(long amount, string name, string taxID, DateTime? due = null, long? expiration = null, double? fine = null, double? interest = null,
            List<string> tags = null, List<Dictionary<string, object>> descriptions = null, List<Dictionary<string, object>> discounts = null, List<Rule> rules = null,
            long? nominalAmount = null, long? fineAmount = null, long? interestAmount = null, long? discountAmount = null,
            string id = null, string brcode = null, string pdfUrl = null, string link = null, int? fee = null, string status = null, List<string> transactionIds = null, DateTime? created = null, DateTime? updated = null, List<Split> splits = null) : base(id)
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
            Rules = rules;
            NominalAmount = nominalAmount;
            FineAmount = fineAmount;
            InterestAmount = interestAmount;
            DiscountAmount = discountAmount;
            Brcode = brcode;
            Fee = fee;
            PdfUrl = pdfUrl;
            Link = link;
            Status = status;
            TransactionIds = transactionIds;
            Created = created;
            Updated = updated;
            Splits = splits;
        }

        internal new Dictionary<string, object> ToJson()
        {
            Dictionary<string, object> json = base.ToJson();
            json["Due"] = new StarkCore.Utils.StarkDateTime((DateTime) json["Due"]);
            return json;
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
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of Invoice objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<Invoice> Create(List<Invoice> invoices, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
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
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of Invoice objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<Invoice> Create(List<Dictionary<string, object>> invoices, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
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
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as Invoice;
        }

        /// <summary>
        /// Retrieve a specific Invoice pdf file
        /// <br/>
        /// Receive a single Invoice pdf receipt file generated in the Stark Bank API by passing its id.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: object unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>Invoice pdf file</item>
        /// </list>
        /// </summary>
        public static byte[] Pdf(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetContent(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                subResourceName: "pdf",
                id: id,
                user: user
            );
        }

        /// <summary>
        /// Retrieve Invoices
        /// <br/>
        /// Receive an IEnumerable of Invoice objects previously created in the Stark Bank API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
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
            ).Cast<Invoice>();
        }

        /// <summary>
        /// Retrieve paged Invoices
        /// <br/>
        /// Receive a list of up to 100 Invoice objects previously created in the Stark Bank API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "created", "paid", "canceled" or "overdue"</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of Invoice objects with updated attributes and cursor to retrieve the next page of Invoice objects</item>
        /// </list>
        /// </summary>
        public static (List<Invoice> page, string pageCursor) Page(string cursor = null, int? limit = null, DateTime? after = null,
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
            List<Invoice> invoices = new List<Invoice>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                invoices.Add(subResource as Invoice);
            }
            return (invoices, pageCursor);
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
        ///     <item>status [string]: you may cancel the Invoice by passing "canceled" in the status. ex: "canceled"</item>
        ///     <item>amount [long integer]: nominal amount charged by the Invoice. ex: 100 (R$1.00)</item>
        ///     <item>due [DateTime, default today + 2 days]: Invoice due date in UTC ISO format. ex: DateTime(2020, 3, 10, 10, 30, 12, 15)</item>
        ///     <item>expiration [long integer, default null]: time interval in seconds between the due date and the expiration date. ex 123456789</item>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>target Invoice with updated attributes</item>
        /// </list>
        /// </summary>
        public static Invoice Update(string id, string status = null, long? amount = null, DateTime? due = null, long? expiration = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PatchId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                payload: new Dictionary<string, object> {
                    { "amount", amount },
                    { "expiration", expiration },
                    { "status", status },
                    { "due", new StarkCore.Utils.StarkDateTime(due) },
                },
                user: user
            ) as Invoice;
        }

        /// <summary>
        /// Retrieve a specific Invoice QRCode png
        /// <br/>
        /// Receive a single Invoice QRCode in png format generated in the Stark Bank API by the invoice ID.
        /// <br/>
        /// <list>
        /// Parameters(required):
        ///     <item>id[string]: object unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters(optional) :
        /// <list>
        ///     <item>size [integer, default 7.0]: number of pixels in each "box" of the QR code. Minimum = 1, maximum = 50. ex: 12</item>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>Boleto pdf file</item>
        /// </list>
        /// </summary>
        public static byte[] Qrcode(string id, int? size = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();

            return Rest.GetContent(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                subResourceName: "qrcode",
                id: id,
                options: new Dictionary<string, object> {
                    { "size", size }
                },
                user: user
            );
        }

        /// <summary>
        /// Retrieve a specific Invoice payment information
        /// <br/>
        /// Receive the InvoicePayment sub-resource associated with a paid Invoice.
        /// <br/>
        /// <list>
        /// Parameters(required):
        ///     <item>id[string]: object unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters(optional) :
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>Invoice Payment sub-resource</item>
        /// </list>
        /// </summary>
        public static InvoicePayment Payment(string id, Dictionary<string, object> payload = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            (string subResourceName, StarkCore.Utils.Api.ResourceMaker subResourceMaker) = InvoicePayment.SubResource();

            return Rest.GetSubResource(
                resourceName: resourceName,
                subResourceMaker: subResourceMaker,
                subResourceName: subResourceName,
                id: id,
                payload: payload,
                user: user
            ) as InvoicePayment;
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "Invoice", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            long amount = json.amount;
            string name = json.name;
            string taxID = json.taxId;
            string dueString = json.due;
            DateTime? due = StarkCore.Utils.Checks.CheckDateTime(dueString);
            long? expiration = json.expiration;
            double? fine = json.fine;
            double? interest = json.interest;
            List<string> tags = json.tags.ToObject<List<string>>();
            List<Dictionary<string, object>> descriptions = json.descriptions.ToObject<List<Dictionary<string, object>>>();
            List<Dictionary<string, object>> discounts = json.discounts.ToObject<List<Dictionary<string, object>>>();
            List<Rule> rules = ParseRule(json.rules);
            foreach (Dictionary<string, object> discount in discounts) {
                discount["due"] = StarkCore.Utils.Checks.CheckDateTime((string)discount["due"]);
            }
            long? nominalAmount = json.nominalAmount;
            long? fineAmount = json.fineAmount;
            long? interestAmount = json.interestAmount;
            long? discountAmount = json.discountAmount;
            string id = json.id;
            string brcode = json.brcode;
            string pdf = json.pdf;
            string link = json.link;
            int fee = json.fee;
            string status = json.status;
            string createdString = json.created;
            string updatedString = json.updated;
            List<string> transactionIds = json.transactionIds.ToObject<List<string>>();
            DateTime? created = StarkCore.Utils.Checks.CheckDateTime(createdString);
            DateTime? updated = StarkCore.Utils.Checks.CheckDateTime(updatedString);
            List<Split> splits = ParseSplits(json.splits);

            return new Invoice(
                amount: amount, name: name, taxID: taxID, due: due, expiration: expiration, fine: fine, interest: interest,
                tags: tags, descriptions: descriptions, discounts: discounts, nominalAmount: nominalAmount, fineAmount: fineAmount,
                interestAmount: interestAmount, discountAmount: discountAmount, id: id, brcode: brcode, pdfUrl: pdf, link: link,
                rules: rules, fee: fee, status: status, transactionIds: transactionIds, created: created, updated: updated, splits: splits
            );

        }

        private static List<Split> ParseSplits(dynamic json)
        {
            if (json is null) return null;

            List<Split> splits = new List<Split>();

            foreach (dynamic split in json)
            {
                splits.Add(Split.ResourceMaker(split));
            }
            return splits;
        }
        private static List<Rule> ParseRule(dynamic json)
        {
            if(json is null) return null;

            List<Rule> rules = new List<Rule>();

            foreach (dynamic rule in json)
            {
                rules.Add(Rule.ResourceMaker(rule));
            }
            return rules;
        }
    }
}
