using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkBank
{
    /// <summary>
    /// # BoletoPayment object
    ///
    /// When you initialize a BoletoPayment, the entity will not be automatically
    /// created in the Stark Bank API. The 'create' function sends the objects
    /// to the Stark Bank API and returns the list of created objects.
    ///
    /// ## Parameters (conditionally required):
    /// - line [string, default nil]: Number sequence that describes the payment. Either 'line' or 'bar_code' parameters are required. If both are sent, they must match. ex: "34191.09008 63571.277308 71444.640008 5 81960000000062"
    /// - bar_code [string, default nil]: Bar code number that describes the payment. Either 'line' or 'barCode' parameters are required. If both are sent, they must match. ex: "34195819600000000621090063571277307144464000"
    ///
    /// ## Parameters (required):
    /// - tax_id [string]: receiver tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"
    /// - description [string]: Text to be displayed in your statement (min. 10 characters). ex: "payment ABC"
    ///
    /// ## Parameters (optional):
    /// - scheduled [Date, default today]: payment scheduled date. ex: Date.new(2020, 3, 10)
    /// - tags [list of strings]: list of strings for tagging
    ///
    /// ## Attributes (return-only):
    /// - id [string, default nil]: unique id returned when payment is created. ex: "5656565656565656"
    /// - status [string, default nil]: current payment status. ex: "registered" or "paid"
    /// - amount [int, default nil]: amount automatically calculated from line or bar_code. ex: 23456 (= R$ 234.56)
    /// - fee [integer, default nil]: fee charged when boleto payment is created. ex: 200 (= R$ 2.00)
    /// - created [DateTime, default nil]: creation datetime for the payment. ex: DateTime.new(2020, 3, 10, 10, 30, 0, 0)
    /// </summary>
    public partial class BoletoPayment : Utils.Resource
    {
        public int? Amount { get; }
        public string Description { get; }
        public string TaxID { get; }
        public string Line { get; }
        public string BarCode { get; }
        public DateTime? Scheduled { get; }
        public List<string> Tags { get; }
        public string Status { get; }
        public int? Fee { get; }
        public DateTime? Created { get; }

        public BoletoPayment(string taxID, string description, string id = null, int? amount = null, string line = null,
            string barCode = null, DateTime? scheduled = null, List<string> tags = null, string status = null,
            int? fee = null, DateTime? created = null) : base(id)
        {
            Amount = amount;
            Description = description;
            TaxID = taxID;
            Line = line;
            BarCode = barCode;
            Scheduled = scheduled;
            Tags = tags;
            Status = status;
            Fee = fee;
            Created = created;
        }

        /// <summary>
        /// # Create BoletoPayments
        ///
        /// Send a list of BoletoPayment objects for creation in the Stark Bank API
        ///
        /// ## Parameters (required):
        /// - payments [list of BoletoPayment objects]: list of BoletoPayment objects to be created in the API
        ///
        /// ## Parameters (optional):
        /// - user [Project object]: Project object. Not necessary if StarkBank.user was set before function call
        ///
        /// ## Return:
        /// - list of BoletoPayment objects with updated attributes
        /// </summary>
        public static List<BoletoPayment> Create(List<BoletoPayment> boletos, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: boletos,
                user: user
            ).ToList().ConvertAll(o => (BoletoPayment)o);
        }

        /// <summary>
        /// # Retrieve a specific BoletoPayment
        ///
        /// Receive a single BoletoPayment object previously created by the Stark Bank API by passing its id
        ///
        /// ## Parameters (required):
        /// - id [string]: object unique id. ex: "5656565656565656"
        ///
        /// ## Parameters (optional):
        /// - user [Project object]: Project object. Not necessary if StarkBank.user was set before function call
        ///
        /// ## Return:
        /// - BoletoPayment object with updated attributes
        /// </summary>
        public static BoletoPayment Get(string id, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as BoletoPayment;
        }

        /// <summary>
        /// # Retrieve a specific BoletoPayment pdf file
        ///
        /// Receive a single BoletoPayment pdf file generated in the Stark Bank API by passing its id.
        /// Only valid for boleto payments with "success" status.
        ///
        /// ## Parameters (required):
        /// - id [string]: object unique id. ex: "5656565656565656"
        ///
        /// ## Parameters (optional):
        /// - user [Project object]: Project object. Not necessary if StarkBank.user was set before function call
        ///
        /// ## Return:
        /// - BoletoPayment pdf file
        /// </summary>
        public static byte[] Pdf(string id, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.GetPdf(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            );
        }

        /// <summary>
        /// # Retrieve BoletoPayments
        ///
        /// Receive a generator of BoletoPayment objects previously created in the Stark Bank API
        ///
        /// ## Parameters (optional):
        /// - limit [integer, default nil]: maximum number of objects to be retrieved. Unlimited if nil. ex: 35
        /// - status [string, default nil]: filter for status of retrieved objects. ex: "paid"
        /// - tags [list of strings, default nil]: tags to filter retrieved objects. ex: ["tony", "stark"]
        /// - after [Date, default nil] date filter for objects created only after specified date. ex: Date.new(2020, 3, 10)
        /// - before [Date, default nil] date filter for objects only before specified date. ex: Date.new(2020, 3, 10)
        /// - user [Project object, default nil]: Project object. Not necessary if StarkBank.user was set before function call
        ///
        /// ## Return:
        /// - generator of BoletoPayment objects with updated attributes
        /// </summary>
        public static IEnumerable<BoletoPayment> Query(int? limit = null, string status = null, List<string> tags = null,
            DateTime? after = null, DateTime? before = null, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "status", status },
                    { "tags", tags },
                    { "after", after },
                    { "before", before }
                },
                user: user
            ).Cast<BoletoPayment>();
        }

        /// <summary>
        /// # Delete a BoletoPayment entity
        ///
        /// Delete a BoletoPayment entity previously created in the Stark Bank API
        ///
        /// Parameters (required):
        /// - id [string]: BoletoPayment unique id. ex: "5656565656565656"
        /// Parameters (optional):
        /// - user [Project object]: Project object. Not necessary if StarkBank.user was set before function call
        /// Return:
        /// - deleted BoletoPayment with updated attributes
        /// </summary>
        public static BoletoPayment Delete(string id, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.DeleteId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as BoletoPayment;
        }

        internal static (string resourceName, Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "BoletoPayment", resourceMaker: ResourceMaker);
        }

        internal static Utils.Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            int? amount = json.amount;
            string description = json.description;
            string taxID = json.taxId;
            string line = json.line;
            string barCode = json.barCode;
            string scheduledString = json.scheduled;
            DateTime? scheduled = Utils.Checks.CheckDateTime(scheduledString);
            List<string> tags = json.tags.ToObject<List<string>>();
            string status = json.status;
            int fee = json.fee;
            string createdString = json.created;
            DateTime? created = Utils.Checks.CheckDateTime(createdString);

            return new BoletoPayment(
                id: id, amount: amount, description: description, taxID: taxID, line: line, barCode: barCode,
                scheduled: scheduled, tags: tags, status: status, fee: fee, created: created
            );
        }
    }
}
