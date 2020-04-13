using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkBank
{
    /// <summary>
    /// Boleto object
    ///  
    /// When you initialize a Boleto, the entity will not be automatically
    /// sent to the Stark Bank API. The 'create' function sends the objects
    /// to the Stark Bank API and returns the list of created objects.
    ///  
    /// Parameters (required):
    ///     amount [integer]: Boleto value in cents. Minimum = 200 (R$2,00). ex: 1234 (= R$ 12.34)
    ///     name [string]: payer full name. ex: "Anthony Edward Stark"
    ///     tax_id [string]: payer tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"
    ///     street_line_1 [string]: payer main address. ex: Av. Paulista, 200
    ///     street_line_2 [string]: payer address complement. ex: Apto. 123
    ///     district [string]: payer address district / neighbourhood. ex: Bela Vista
    ///     city [string]: payer address city. ex: Rio de Janeiro
    ///     state_code [string]: payer address state. ex: GO
    ///     zip_code [string]: payer address zip code. ex: 01311-200
    ///     due [datetime.date, default today + 2 days]: Boleto due date in ISO format. ex: 2020-04-30
    /// Parameters (optional):
    ///     fine [float, default 0.0]: Boleto fine for overdue payment in %. ex: 2.5
    ///     interest [float, default 0.0]: Boleto monthly interest for overdue payment in %. ex: 5.2
    ///     overdue_limit [integer, default 59]: limit in days for automatic Boleto cancellation after due date. ex: 7 (max: 59)
    ///     descriptions [list of dictionaries, default None]: list of dictionaries with "text":string and (optional) "amount":int pairs
    ///     tags [list of strings]: list of strings for tagging
    /// Attributes (return-only):
    ///     id [string, default None]: unique id returned when Boleto is created. ex: "5656565656565656"
    ///     fee [integer, default None]: fee charged when Boleto is paid. ex: 200 (= R$ 2.00)
    ///     line [string, default None]: generated Boleto line for payment. ex: "34191.09008 63571.277308 71444.640008 5 81960000000062"
    ///     bar_code [string, default None]: generated Boleto bar-code for payment. ex: "34195819600000000621090063571277307144464000"
    ///     status [string, default None]: current Boleto status. ex: "registered" or "paid"
    ///     created [datetime.datetime, default None]: creation datetime for the Boleto. ex: datetime.datetime(2020, 3, 10, 10, 30, 0, 0)
    /// </summary>
    public class Boleto : Utils.IResource
    {
        public string ID { get; }
        public int Amount { get; }
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
        public List<string> Tags { get; }
        public List<Dictionary<string, object>> Descriptions { get; }
        public int? Fee { get; }
        public string Line { get; }
        public string BarCode { get; }
        public string Status { get; }
        public DateTime? Created { get; }
        public Boleto(int amount, string name, string taxID, string streetLine1, string streetLine2, string district,
            string city, string stateCode, string zipCode, DateTime? due = null, double? fine = null, double? interest = null,
            int? overdueLimit = null, List<string> tags = null, List<Dictionary<string, object>> descriptions = null,
            string id = null, int? fee = null, string line = null, string barCode = null, string status = null,
            DateTime? created = null)
        {
            ID = id;
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
            Tags = tags;
            Descriptions = descriptions;
            Fee = fee;
            Line = line;
            BarCode = barCode;
            Status = status;
            Created = created;
        }

        /// <summary>
        /// Create Boletos
        ///
        /// Send a list of Boleto objects for creation in the Stark Bank API
        ///
        /// Parameters (required):
        ///     boletos [list of Boleto objects]: list of Boleto objects to be created in the API
        /// Parameters (optional):
        ///     user [Project object]: Project object. Not necessary if starkbank.user was set before function call
        /// Return:
        ///     list of Boleto objects with updated attributes
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
        /// Retrieve a specific Boleto
        /// 
        /// Receive a single Boleto object previously created in the Stark Bank API by passing its id
        /// 
        /// Parameters(required) :
        ///     id[string]: object unique id.ex: "5656565656565656"
        /// Parameters(optional) :
        ///     user[Project object]: Project object. Not necessary if starkbank.user was set before function call
        /// Return:
        ///     Boleto object with updated attributes
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
        /// 
        /// Receive a single Boleto pdf file generated in the Stark Bank API by passing its id.
        /// 
        /// Parameters(required):
        ///     id[string]: object unique id.ex: "5656565656565656"
        /// Parameters(optional) :
        ///     user[Project object]: Project object. Not necessary if starkbank.user was set before function call
        /// Return:
        ///     Boleto pdf file
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
        /// Retrieve Boletos
        /// 
        /// Receive a generator of Boleto objects previously created in the Stark Bank API
        /// 
        /// Parameters (optional):
        ///     limit [integer, default None]: maximum number of objects to be retrieved. Unlimited if None. ex: 35
        ///     status [string, default None]: filter for status of retrieved objects. ex: "paid" or "registered"
        ///     tags [list of strings, default None]: tags to filter retrieved objects. ex: ["tony", "stark"]
        ///     ids [list of strings, default None]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]
        ///     after [datetime.date, default None] date filter for objects created only after specified date. ex: datetime.date(2020, 3, 10)
        ///     before [datetime.date, default None] date filter for objects only before specified date. ex: datetime.date(2020, 3, 10)
        ///     user [Project object, default None]: Project object. Not necessary if starkbank.user was set before function call
        /// Return:
        ///     generator of Boleto objects with updated attributes
        /// </summary>
        public static IEnumerable<Boleto> Query(int? limit = null, string status = null, List<string> tags = null, List<string> ids = null,
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
                    { "ids", ids },
                    { "after", after },
                    { "before", before }
                },
                user: user
            ).Cast<Boleto>();
        }

        /// <summary>
        /// Delete a Boleto entity
        /// 
        /// Delete a Boleto entity previously created in the Stark Bank API
        /// 
        /// Parameters(required) :
        ///     id[string]: Boleto unique id.ex: "5656565656565656"
        /// Parameters(optional) :
        ///     user[Project object]: Project object. Not necessary if starkbank.user was set before function call
        /// Return:
        ///     deleted Boleto with updated attributes
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

        internal static Utils.IResource ResourceMaker(dynamic json)
        {
            int amount = json.amount;
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
            List<string> tags = json.tags.ToObject<List<string>>();
            List<Dictionary<string, object>> descriptions = json.descriptions.ToObject<List<Dictionary<string, object>>>();
            string id = json.id;
            int fee = json.fee;
            string line = json.line;
            string barCode = json.barCode;
            string status = json.status;
            string createdString = json.created;
            DateTime? created = Utils.Checks.CheckDateTime(createdString);

            return new Boleto(amount: amount, name: name, taxID: taxID, streetLine1: streetLine1, streetLine2: streetLine2,
                district: district, city: city, stateCode: stateCode, zipCode: zipCode, due: due, fine: fine,
                interest: interest, overdueLimit: overdueLimit, tags: tags, descriptions: descriptions, id: id, fee: fee,
                line: line, barCode: barCode, status: status, created: created
            );
        }
    }
}
