using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkBank
{
    /// <summary>
    /// Boleto object
    /// <br/>
    /// When you initialize a Boleto, the entity will not be automatically
    /// sent to the Stark Bank API. The 'create' function sends the objects
    /// to the Stark Bank API and returns the list of created objects.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Amount [long integer]: Boleto value in cents. Minimum = 200 (R$2,00). ex: 1234 (= R$ 12.34)</item>
    ///     <item>Name [string]: payer full name. ex: "Anthony Edward Stark"</item>
    ///     <item>TaxID [string]: payer tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
    ///     <item>StreetLine1 [string]: payer main address. ex: Av. Paulista, 200</item>
    ///     <item>StreetLine2 [string]: payer address complement. ex: Apto. 123</item>
    ///     <item>District [string]: payer address district / neighbourhood. ex: Bela Vista</item>
    ///     <item>City [string]: payer address city. ex: Rio de Janeiro</item>
    ///     <item>StateCode [string]: payer address state. ex: GO</item>
    ///     <item>ZipCode [string]: payer address zip code. ex: 01311-200</item>
    ///     <item>Due [DateTime, default today + 2 days]: Boleto due date in ISO format. ex: new DateTime(2020, 3, 10)</item>
    ///     <item>Fine [float, default 0.0]: Boleto fine for overdue payment in %. ex: 2.5</item>
    ///     <item>Interest [float, default 0.0]: Boleto monthly interest for overdue payment in %. ex: 5.2</item>
    ///     <item>OverdueLimit [integer, default 59]: limit in days for payment after due date. ex: 7 (max: 59)</item>
    ///     <item>ReceiverName [string]: receiver (Sacador Avalista) full name. ex: "Anthony Edward Stark"</item>
    ///     <item>ReceiverTaxID [string]: receiver(Sacador Avalista) tax ID(CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
    ///     <item>Descriptions [list of dictionaries, default null]: list of dictionaries with "text":string and (optional) "amount":int pairs. ex: new List<Dictionary<string,string>>(){new Dictionary<string, string>{{"amount", 1000},{"text", "Taxes"}}</item>
    ///     <item>Discounts [list of dictionaries, default null]: list of dictionaries with "percentage":float and "date":DateTime pairs. ex: new List<Dictionary<string,string>>(){new Dictionary<string, string>{{"percentage", 1.5},{"date", new DateTime(2020, 3, 8)}}</item>
    ///     <item>Tags [list of strings]: list of strings for tagging</item>
    ///     <item>ID [string, default null]: unique id returned when Boleto is created. ex: "5656565656565656"</item>
    ///     <item>Fee [integer, default null]: fee charged when Boleto is paid. ex: 200 (= R$ 2.00)</item>
    ///     <item>Line [string, default null]: generated Boleto line for payment. ex: "34191.09008 63571.277308 71444.640008 5 81960000000062"</item>
    ///     <item>BarCode [string, default null]: generated Boleto bar-code for payment. ex: "34195819600000000621090063571277307144464000"</item>
    ///     <item>Status [string, default null]: current Boleto status. ex: "registered" or "paid"</item>
    ///     <item>Created [DateTime, default null]: creation datetime for the Boleto. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class Boleto : Utils.Resource
    {
        public long Amount { get; }
        public string Name { get; }
        public string TaxID { get; }
        public string StreetLine1 { get; }
        public string StreetLine2 { get; }
        public string District { get; }
        public string City { get; }
        public string StateCode { get; }
        public string ZipCode { get; }
        public DateTime? Due { get; }
        public double? Fine { get; }
        public double? Interest { get; }
        public int? OverdueLimit { get; }
        public string ReceiverName { get; }
        public string ReceiverTaxID { get; }
        public List<string> Tags { get; }
        public List<Dictionary<string, object>> Descriptions { get; }
        public List<Dictionary<string, object>> Discounts { get; }
        public int? Fee { get; }
        public string Line { get; }
        public string BarCode { get; }
        public string Status { get; }
        public DateTime? Created { get; }

        /// <summary>
        /// Boleto object
        /// <br/>
        /// When you initialize a Boleto, the entity will not be automatically
        /// sent to the Stark Bank API. The 'create' function sends the objects
        /// to the Stark Bank API and returns the list of created objects.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>amount [long integer]: Boleto value in cents. Minimum = 200 (R$2,00). ex: 1234 (= R$ 12.34)</item>
        ///     <item>name [string]: payer full name. ex: "Anthony Edward Stark"</item>
        ///     <item>taxID [string]: payer tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
        ///     <item>streetLine1 [string]: payer main address. ex: Av. Paulista, 200</item>
        ///     <item>streetLine2 [string]: payer address complement. ex: Apto. 123</item>
        ///     <item>district [string]: payer address district / neighbourhood. ex: Bela Vista</item>
        ///     <item>city [string]: payer address city. ex: Rio de Janeiro</item>
        ///     <item>stateCode [string]: payer address state. ex: GO</item>
        ///     <item>zipCode [string]: payer address zip code. ex: 01311-200</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>due [DateTime, default today + 2 days]: Boleto due date in ISO format. ex: new DateTime(2020, 3, 10)</item>
        ///     <item>fine [float, default 0.0]: Boleto fine for overdue payment in %. ex: 2.5</item>
        ///     <item>interest [float, default 0.0]: Boleto monthly interest for overdue payment in %. ex: 5.2</item>
        ///     <item>overdueLimit [integer, default 59]: limit in days for payment after due date. ex: 7 (max: 59)</item>
        ///     <item>receiverName [string]: receiver (Sacador Avalista) full name. ex: "Anthony Edward Stark"</item>
        ///     <item>receiverTaxID [string]: receiver(Sacador Avalista) tax ID(CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
        ///     <item>descriptions [list of dictionaries, default null]: list of dictionaries with "text":string and (optional) "amount":int pairs. ex: new List<Dictionary<string,string>>(){new Dictionary<string, string>{{"amount", 1000},{"text", "Taxes"}}</item>
        ///     <item>discounts [list of dictionaries, default null]: list of dictionaries with "percentage":float and "date":DateTime pairs. ex: new List<Dictionary<string,string>>(){new Dictionary<string, string>{{"percentage", 1.5},{"date", new DateTime(2020, 3, 8)}}</item>
        ///     <item>tags [list of strings]: list of strings for tagging</item>
        /// </list>
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string, default null]: unique id returned when Boleto is created. ex: "5656565656565656"</item>
        ///     <item>fee [integer, default null]: fee charged when Boleto is paid. ex: 200 (= R$ 2.00)</item>
        ///     <item>line [string, default null]: generated Boleto line for payment. ex: "34191.09008 63571.277308 71444.640008 5 81960000000062"</item>
        ///     <item>barCode [string, default null]: generated Boleto bar-code for payment. ex: "34195819600000000621090063571277307144464000"</item>
        ///     <item>status [string, default null]: current Boleto status. ex: "registered" or "paid"</item>
        ///     <item>created [DateTime, default null]: creation datetime for the Boleto. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public Boleto(long amount, string name, string taxID, string streetLine1, string streetLine2, string district,
            string city, string stateCode, string zipCode, DateTime? due = null, double? fine = null, double? interest = null,
            int? overdueLimit = null, string receiverName = null, string receiverTaxID = null, List<string> tags = null, List<Dictionary<string, object>> descriptions = null,
            List<Dictionary<string, object>> discounts = null, string id = null, int? fee = null, string line = null,
            string barCode = null, string status = null, DateTime? created = null) : base(id)
        {
            Amount = amount;
            Name = name;
            TaxID = taxID;
            StreetLine1 = streetLine1;
            StreetLine2 = streetLine2;
            District = district;
            City = city;
            StateCode = stateCode;
            ZipCode = zipCode;
            Due = due;
            Fine = fine;
            Interest = interest;
            OverdueLimit = overdueLimit;
            ReceiverName = receiverName;
            ReceiverTaxID = receiverTaxID;
            Tags = tags;
            Descriptions = descriptions;
            Discounts = discounts;
            Fee = fee;
            Line = line;
            BarCode = barCode;
            Status = status;
            Created = created;
        }

        internal new Dictionary<string, object> ToJson()
        {
            Dictionary<string, object> json = base.ToJson();
            json["Due"] = new Utils.StarkBankDate((DateTime)json["Due"]);
            return json;
        }

        /// <summary>
        /// Create Boletos
        /// <br/>
        /// Send a list of Boleto objects for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>boletos [list of Boleto objects]: list of Boleto objects to be created in the API</item>
        /// <br/>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of Boleto objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<Boleto> Create(List<Boleto> boletos, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: boletos,
                user: user
            ).ToList().ConvertAll(o => (Boleto)o);
        }

        /// <summary>
        /// Create Boletos
        /// <br/>
        /// Send a list of Boleto objects for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>boletos [list of Dictionaries]: list of Dictionaries representing the Boletos to be created in the API</item>
        /// <br/>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of Boleto objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<Boleto> Create(List<Dictionary<string, object>> boletos, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: boletos,
                user: user
            ).ToList().ConvertAll(o => (Boleto)o);
        }

        /// <summary>
        /// Retrieve a specific Boleto
        /// <br/>
        /// Receive a single Boleto object previously created in the Stark Bank API by passing its id
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
        ///     <item>Boleto object with updated attributes</item>
        /// </list>
        /// </summary>
        public static Boleto Get(string id, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as Boleto;
        }

        /// <summary>
        /// Retrieve a specific Boleto pdf file
        /// <br/>
        /// Receive a single Boleto pdf file generated in the Stark Bank API by passing its id.
        /// <br/>
        /// <list>
        /// Parameters(required):
        ///     <item>id[string]: object unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters(optional) :
        /// <list>
        ///     <item>layout[string]: Layout specification. Available options are "default" and "booklet"</item>
        ///     <item>user[Project object]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>Boleto pdf file</item>
        /// </list>
        /// </summary>
        public static byte[] Pdf(string id, string layout = null, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();

            return Utils.Rest.GetPdf(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                options: new Dictionary<string, object> {
                    { "layout", layout }
                },
                user: user
            );
        }

        /// <summary>
        /// Retrieve Boletos
        /// <br/>
        /// Receive an IEnumerable of Boleto objects previously created in the Stark Bank API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "paid" or "registered"</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of Boleto objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<Boleto> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
            string status = null, List<string> tags = null, List<string> ids = null, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "after", new Utils.StarkBankDate(after) },
                    { "before", new Utils.StarkBankDate(before) },
                    { "status", status },
                    { "tags", tags },
                    { "ids", ids }
                },
                user: user
            ).Cast<Boleto>();
        }

        /// <summary>
        /// Delete a Boleto entity
        /// <br/>
        /// Delete a Boleto entity previously created in the Stark Bank API
        /// <br/>
        /// Parameters(required) :
        /// <list>
        ///     <item>id[string]: Boleto unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters(optional) :
        /// <list>
        ///     <item>user[Project object]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>deleted Boleto object</item>
        /// </list>
        /// </summary>
        public static Boleto Delete(string id, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.DeleteId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as Boleto;
        }

        internal static (string resourceName, Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "Boleto", resourceMaker: ResourceMaker);
        }

        internal static Utils.Resource ResourceMaker(dynamic json)
        {
            long amount = json.amount;
            string name = json.name;
            string taxID = json.taxId;
            string streetLine1 = json.streetLine1;
            string streetLine2 = json.streetLine2;
            string district = json.district;
            string city = json.city;
            string stateCode = json.stateCode;
            string zipCode = json.zipCode;
            string dueString = json.due;
            DateTime? due = Utils.Checks.CheckNullableDateTime(dueString);
            double fine = json.fine;
            double interest = json.interest;
            int overdueLimit = json.overdueLimit;
            string receiverName = json.receiverName;
            string receiverTaxID = json.receiverTaxId;
            List<string> tags = json.tags.ToObject<List<string>>();
            List<Dictionary<string, object>> descriptions = json.descriptions.ToObject<List<Dictionary<string, object>>>();
            List<Dictionary<string, object>> discounts = json.discounts.ToObject<List<Dictionary<string, object>>>();
            foreach(Dictionary<string, object> discount in discounts) {
                discount["date"] = Utils.Checks.CheckDateTime((string)discount["date"]);
            }
            string id = json.id;
            int fee = json.fee;
            string line = json.line;
            string barCode = json.barCode;
            string status = json.status;
            string createdString = json.created;
            DateTime? created = Utils.Checks.CheckDateTime(createdString);

            return new Boleto(
                amount: amount, name: name, taxID: taxID, streetLine1: streetLine1, streetLine2: streetLine2,
                district: district, city: city, stateCode: stateCode, zipCode: zipCode, due: due, fine: fine,
                interest: interest, overdueLimit: overdueLimit, receiverName: receiverName, receiverTaxID: receiverTaxID,
                tags: tags, descriptions: descriptions, discounts: discounts, id: id, fee: fee, line: line,
                barCode: barCode, status: status, created: created
            );
        }
    }
}
