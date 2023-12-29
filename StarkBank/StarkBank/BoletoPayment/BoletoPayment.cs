﻿using System;
using System.Linq;
using StarkBank.Utils;
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
    ///     <item>Line [string, default null]: Number sequence that describes the payment. Either 'line' or 'barCode' parameters are required. If both are sent, they must match. ex: "34191.09008 63571.277308 71444.640008 5 81960000000062"</item>
    ///     <item>BarCode [string, default null]: Bar code number that describes the payment. Either 'line' or 'barCode' parameters are required. If both are sent, they must match. ex: "34195819600000000621090063571277307144464000"</item>
    ///     <item>TaxID [string]: receiver tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
    ///     <item>Description [string]: Text to be displayed in your statement (min. 10 characters). ex: "payment ABC"</item>
    ///     <item>Amount [long integer, default null]: amount automatically calculated from line or barCode. ex: 23456 (= R$ 234.56)</item>
    ///     <item>Scheduled [DateTime, default today]: payment scheduled date. ex: new DateTime(2020, 3, 10)</item>
    ///     <item>Tags [list of strings]: list of strings for tagging</item>
    ///     <item>ID [string]: unique id returned when payment is created. ex: "5656565656565656"</item>
    ///     <item>Status [string]: current payment status. ex: "success" or "failed"</item>
    ///     <item>Fee [integer]: fee charged when BoletoPayment is created. ex: 200 (= R$ 2.00)</item>
    ///     <item>TransactionIds [list of strings]: ledger transaction ids linked to this BoletoPayment. ex: ["19827356981273"]</item>
    ///     <item>Created [DateTime]: creation datetime for the payment. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class BoletoPayment : Resource
    {
        public string Line { get; }
        public string BarCode { get; }
        public string TaxID { get; }
        public string Description { get; }
        public long? Amount { get; }
        public DateTime? Scheduled { get; }
        public List<string> Tags { get; }
        public string Status { get; }
        public int? Fee { get; }
        public List<string> TransactionIds { get; }
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
        ///     <item>line [string, default null]: Number sequence that describes the payment. Either 'line' or 'barCode' parameters are required. If both are sent, they must match. ex: "34191.09008 63571.277308 71444.640008 5 81960000000062"</item>
        ///     <item>barCode [string, default null]: Bar code number that describes the payment. Either 'line' or 'barCode' parameters are required. If both are sent, they must match. ex: "34195819600000000621090063571277307144464000"</item>
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
        ///     <item>amount [long integer, default null]: amount automatically calculated from line or barCode. ex: 23456 (= R$ 234.56)</item>
        ///     <item>scheduled [DateTime, default today]: payment scheduled date. ex: new DateTime(2020, 3, 10)</item>
        ///     <item>tags [list of strings, default null]: list of strings for tagging</item>
        /// </list>
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when payment is created. ex: "5656565656565656"</item>
        ///     <item>status [string]: current payment status. ex: "success" or "failed"</item>
        ///     <item>fee [integer]: fee charged when BoletoPayment is created. ex: 200 (= R$ 2.00)</item>
        ///     <item>transactionIds [list of strings]: ledger transaction ids linked to this BoletoPayment. ex: ["19827356981273"]</item>
        ///     <item>created [DateTime]: creation datetime for the payment. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public BoletoPayment(string taxID, string description, string id = null, long? amount = null, string line = null,
            string barCode = null, DateTime? scheduled = null, List<string> tags = null, string status = null,
            int? fee = null, List<string> transactionIds = null, DateTime? created = null) : base(id)
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
            TransactionIds = transactionIds;
            Created = created;
        }

        internal new Dictionary<string, object> ToJson()
        {
            Dictionary<string, object> json = base.ToJson();
            json["Scheduled"] = new StarkCore.Utils.StarkDate((DateTime)json["Scheduled"]);
            return json;
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
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of BoletoPayment objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<BoletoPayment> Create(List<BoletoPayment> payments, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: payments,
                user: user
            ).ToList().ConvertAll(o => (BoletoPayment)o);
        }

        /// <summary>
        /// Create BoletoPayments
        /// <br/>
        /// Send a list of BoletoPayment objects for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>payments [list of Dictionaries]: list of Dictionaries representing the BoletoPayments to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of BoletoPayment objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<BoletoPayment> Create(List<Dictionary<string, object>> payments, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: payments,
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
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>BoletoPayment object with updated attributes</item>
        /// </list>
        /// </summary>
        public static BoletoPayment Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
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
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>BoletoPayment pdf file</item>
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
        /// Retrieve BoletoPayments
        /// <br/>
        /// Receive an IEnumerable of BoletoPayment objects previously created in the Stark Bank API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: new DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: new DateTime(2020, 3, 10)</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>ids [list of strings, default null]: list of strings to get specific entities by ids. ex: ["12376517623", "1928367198236"]</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "paid"</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of BoletoPayment objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<BoletoPayment> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
            List<string> tags = null, List<string> ids= null, string status = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "after", new StarkCore.Utils.StarkDate(after) },
                    { "before", new StarkCore.Utils.StarkDate(before) },
                    { "tags", tags },
                    { "ids", ids},
                    { "status", status }
                },
                user: user
            ).Cast<BoletoPayment>();
        }

        /// <summary>
        /// Retrieve paged BoletoPayments
        /// <br/>
        /// Receive a list of up to 100 BoletoPayment objects previously created in the Stark Bank API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: new DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: new DateTime(2020, 3, 10)</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>ids [list of strings, default null]: list of strings to get specific entities by ids. ex: ["12376517623", "1928367198236"]</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "paid"</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of BoletoPayment objects with updated attributes and cursor to retrieve the next page of BoletoPayment objects</item>
        /// </list>
        /// </summary>
        public static (List<BoletoPayment> page, string pageCursor) Page(string cursor = null, int? limit = null, DateTime? after = null,
            DateTime? before = null, List<string> tags = null, List<string> ids = null, string status = null, User user = null)
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
                    { "tags", tags },
                    { "ids", ids},
                    { "status", status }
                },
                user: user
            );
            List<BoletoPayment> boletoPayments = new List<BoletoPayment>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                boletoPayments.Add(subResource as BoletoPayment);
            }
            return (boletoPayments, pageCursor);
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
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// Return:
        /// <list>
        ///     <item>deleted BoletoPayment object</item>
        /// </list>
        /// </summary>
        public static BoletoPayment Delete(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.DeleteId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as BoletoPayment;
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "BoletoPayment", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            string line = json.line;
            string barCode = json.barCode;
            string taxID = json.taxId;
            string description = json.description;
            long? amount = json.amount;
            string scheduledString = json.scheduled;
            DateTime? scheduled = StarkCore.Utils.Checks.CheckNullableDateTime(scheduledString);
            List<string> tags = json.tags?.ToObject<List<string>>();
            string status = json.status;
            int? fee = json.fee;
            List<string> transactionIds = json.transactionIds?.ToObject<List<string>>();
            string createdString = json.created;
            DateTime? created = StarkCore.Utils.Checks.CheckNullableDateTime(createdString);

            return new BoletoPayment(
                id: id, amount: amount, description: description, taxID: taxID, line: line, barCode: barCode,
                scheduled: scheduled, tags: tags, status: status, fee: fee, transactionIds: transactionIds,
                created: created
            );
        }
    }
}
