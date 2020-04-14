﻿using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkBank
{
    /// <summary>
    /// BoletoPayment object
    /// <br/>
    /// When you initialize a BoletoPayment, the entity will not be automatically
    /// created in the Stark Bank API. The 'create' function sends the objects
    /// to the Stark Bank API and returns the list of created objects.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Line [string, default nil]: Number sequence that describes the payment. Either 'line' or 'barCode' parameters are required. If both are sent, they must match. ex: "34191.09008 63571.277308 71444.640008 5 81960000000062"</item>
    ///     <item>BarCode [string, default nil]: Bar code number that describes the payment. Either 'line' or 'barCode' parameters are required. If both are sent, they must match. ex: "34195819600000000621090063571277307144464000"</item>
    ///     <item>TaxID [string]: receiver tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
    ///     <item>Description [string]: Text to be displayed in your statement (min. 10 characters). ex: "payment ABC"</item>
    ///     <item>Scheduled [Date, default today]: payment scheduled date. ex: Date.new(2020, 3, 10)</item>
    ///     <item>Tags [list of strings]: list of strings for tagging</item>
    ///     <item>ID [string, default nil]: unique id returned when payment is created. ex: "5656565656565656"</item>
    ///     <item>Status [string, default nil]: current payment status. ex: "registered" or "paid"</item>
    ///     <item>Amount [long integer, default nil]: amount automatically calculated from line or barCode. ex: 23456 (= R$ 234.56)</item>
    ///     <item>Fee [integer, default nil]: fee charged when boleto payment is created. ex: 200 (= R$ 2.00)</item>
    ///     <item>Created [DateTime, default nil]: creation datetime for the payment. ex: DateTime.new(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class BoletoPayment : Utils.Resource
    {
        public long? Amount { get; }
        public string Description { get; }
        public string TaxID { get; }
        public string Line { get; }
        public string BarCode { get; }
        public DateTime? Scheduled { get; }
        public List<string> Tags { get; }
        public string Status { get; }
        public int? Fee { get; }
        public DateTime? Created { get; }

        /// <summary>
        /// BoletoPayment object
        /// <br/>
        /// When you initialize a BoletoPayment, the entity will not be automatically
        /// created in the Stark Bank API. The 'create' function sends the objects
        /// to the Stark Bank API and returns the list of created objects.
        /// <br/>
        /// Parameters (conditionally required):
        /// <list>
        ///     <item>line [string, default nil]: Number sequence that describes the payment. Either 'line' or 'barCode' parameters are required. If both are sent, they must match. ex: "34191.09008 63571.277308 71444.640008 5 81960000000062"</item>
        ///     <item>barCode [string, default nil]: Bar code number that describes the payment. Either 'line' or 'barCode' parameters are required. If both are sent, they must match. ex: "34195819600000000621090063571277307144464000"</item>
        /// </list>
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>taxID [string]: receiver tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
        ///     <item>description [string]: Text to be displayed in your statement (min. 10 characters). ex: "payment ABC"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>scheduled [Date, default today]: payment scheduled date. ex: Date.new(2020, 3, 10)</item>
        ///     <item>tags [list of strings]: list of strings for tagging</item>
        /// </list>
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string, default nil]: unique id returned when payment is created. ex: "5656565656565656"</item>
        ///     <item>status [string, default nil]: current payment status. ex: "registered" or "paid"</item>
        ///     <item>amount [long integer, default nil]: amount automatically calculated from line or barCode. ex: 23456 (= R$ 234.56)</item>
        ///     <item>fee [integer, default nil]: fee charged when boleto payment is created. ex: 200 (= R$ 2.00)</item>
        ///     <item>created [DateTime, default nil]: creation datetime for the payment. ex: DateTime.new(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public BoletoPayment(string taxID, string description, string id = null, long? amount = null, string line = null,
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
        /// Create BoletoPayments
        /// <br/>
        /// Send a list of BoletoPayment objects for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>payments [list of BoletoPayment objects]: list of BoletoPayment objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Project object]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of BoletoPayment objects with updated attributes</item>
        /// </list>
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
        /// Retrieve a specific BoletoPayment
        /// <br/>
        /// Receive a single BoletoPayment object previously created by the Stark Bank API by passing its id
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: object unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Project object]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>BoletoPayment object with updated attributes</item>
        /// </list>
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
        /// Retrieve a specific BoletoPayment pdf file
        /// <br/>
        /// Receive a single BoletoPayment pdf file generated in the Stark Bank API by passing its id.
        /// Only valid for boleto payments with "success" status.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: object unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Project object]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>BoletoPayment pdf file</item>
        /// </list>
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
        /// Retrieve BoletoPayments
        /// <br/>
        /// Receive an IEnumerable of BoletoPayment objects previously created in the Stark Bank API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default nil]: maximum number of objects to be retrieved. Unlimited if nil. ex: 35</item>
        ///     <item>status [string, default nil]: filter for status of retrieved objects. ex: "paid"</item>
        ///     <item>tags [list of strings, default nil]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>after [Date, default nil] date filter for objects created only after specified date. ex: Date.new(2020, 3, 10)</item>
        ///     <item>before [Date, default nil] date filter for objects only before specified date. ex: Date.new(2020, 3, 10)</item>
        ///     <item>user [Project object, default nil]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of BoletoPayment objects with updated attributes</item>
        /// </list>
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
        /// Delete a BoletoPayment entity
        /// <br/>
        /// Delete a BoletoPayment entity previously created in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: BoletoPayment unique id. ex: "5656565656565656"</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Project object]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// Return:
        /// <list>
        ///     <item>deleted BoletoPayment with updated attributes</item>
        /// </list>
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
            long? amount = json.amount;
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
