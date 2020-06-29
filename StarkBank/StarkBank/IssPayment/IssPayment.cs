﻿using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkBank
{
    /// <summary>
    /// IssPayment object
    /// <br/>
    /// When you initialize a IssPayment, the entity will not be automatically
    /// created in the Stark Bank API. The 'create' function sends the objects
    /// to the Stark Bank API and returns the list of created objects.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Line [string, default null]: Number sequence that describes the payment. Either 'line' or 'barCode' parameters are required. If both are sent, they must match. ex: "34191.09008 63571.277308 71444.640008 5 81960000000062"</item>
    ///     <item>BarCode [string, default null]: Bar code number that describes the payment. Either 'line' or 'barCode' parameters are required. If both are sent, they must match. ex: "34195819600000000621090063571277307144464000"</item>
    ///     <item>Description [string]: Text to be displayed in your statement (min. 10 characters). ex: "payment ABC"</item>
    ///     <item>Scheduled [DateTime, default today]: payment scheduled date. ex: DateTime.new(2020, 3, 10)</item>
    ///     <item>Tags [list of strings]: list of strings for tagging</item>
    ///     <item>ID [string, default null]: unique id returned when payment is created. ex: "5656565656565656"</item>
    ///     <item>Status [string, default null]: current payment status. ex: "success" or "failed"</item>
    ///     <item>Amount [long integer, default null]: amount automatically calculated from line or barCode. ex: 23456 (= R$ 234.56)</item>
    ///     <item>Fee [integer, default null]: fee charged when IssPayment is created. ex: 200 (= R$ 2.00)</item>
    ///     <item>Created [DateTime, default null]: creation datetime for the payment. ex: DateTime.new(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class IssPayment : Utils.Resource
    {
        public long? Amount { get; }
        public string Description { get; }
        public string Line { get; }
        public string BarCode { get; }
        public DateTime? Scheduled { get; }
        public List<string> Tags { get; }
        public string Status { get; }
        public int? Fee { get; }
        public DateTime? Created { get; }

        /// <summary>
        /// IssPayment object
        /// <br/>
        /// When you initialize a IssPayment, the entity will not be automatically
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
        ///     <item>description [string]: Text to be displayed in your statement (min. 10 characters). ex: "payment ABC"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>scheduled [DateTime, default today]: payment scheduled date. ex: DateTime.new(2020, 3, 10)</item>
        ///     <item>tags [list of strings]: list of strings for tagging</item>
        /// </list>
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string, default null]: unique id returned when payment is created. ex: "5656565656565656"</item>
        ///     <item>status [string, default null]: current payment status. ex: "success" or "failed"</item>
        ///     <item>amount [long integer, default null]: amount automatically calculated from line or barCode. ex: 23456 (= R$ 234.56)</item>
        ///     <item>fee [integer, default null]: fee charged when IssPayment is created. ex: 200 (= R$ 2.00)</item>
        ///     <item>created [DateTime, default null]: creation datetime for the payment. ex: DateTime.new(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public IssPayment(string description, string id = null, long? amount = null, string line = null,
            string barCode = null, DateTime? scheduled = null, List<string> tags = null, string status = null,
            int? fee = null, DateTime? created = null) : base(id)
        {
            Amount = amount;
            Description = description;
            Line = line;
            BarCode = barCode;
            Scheduled = scheduled;
            Tags = tags;
            Status = status;
            Fee = fee;
            Created = created;
        }

        /// <summary>
        /// Create IssPayments
        /// <br/>
        /// Send a list of IssPayment objects for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>payments [list of IssPayment objects]: list of IssPayment objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Project object]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IssPayment objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<IssPayment> Create(List<IssPayment> payments, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: payments,
                user: user
            ).ToList().ConvertAll(o => (IssPayment)o);
        }

        /// <summary>
        /// Create IssPayments
        /// <br/>
        /// Send a list of IssPayment objects for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>payments [list of Dictionaries]: list of Dictionaries representing the IssPayments to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Project object]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IssPayment objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<IssPayment> Create(List<Dictionary<string, object>> payments, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: payments,
                user: user
            ).ToList().ConvertAll(o => (IssPayment)o);
        }

        /// <summary>
        /// Retrieve a specific IssPayment
        /// <br/>
        /// Receive a single IssPayment object previously created by the Stark Bank API by passing its id
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
        ///     <item>IssPayment object with updated attributes</item>
        /// </list>
        /// </summary>
        public static IssPayment Get(string id, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as IssPayment;
        }

        /// <summary>
        /// Retrieve a specific IssPayment pdf file
        /// <br/>
        /// Receive a single IssPayment pdf file generated in the Stark Bank API by passing its id.
        /// Only valid for ISS payments with "success" status.
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
        ///     <item>IssPayment pdf file</item>
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
        /// Retrieve IssPayments
        /// <br/>
        /// Receive an IEnumerable of IssPayment objects previously created in the Stark Bank API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "paid"</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of IssPayment objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<IssPayment> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
            List<string> tags = null, List<string> ids = null, string status = null, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "after", after },
                    { "before", before },
                    { "tags", tags },
                    { "ids", ids },
                    { "status", status }
                },
                user: user
            ).Cast<IssPayment>();
        }

        /// <summary>
        /// Delete a IssPayment entity
        /// <br/>
        /// Delete a IssPayment entity previously created in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: IssPayment unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Project object]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>deleted IssPayment with updated attributes</item>
        /// </list>
        /// </summary>
        public static IssPayment Delete(string id, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.DeleteId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as IssPayment;
        }

        internal static (string resourceName, Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "IssPayment", resourceMaker: ResourceMaker);
        }

        internal static Utils.Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            long? amount = json.amount;
            string description = json.description;
            string line = json.line;
            string barCode = json.barCode;
            string scheduledString = json.scheduled;
            DateTime? scheduled = Utils.Checks.CheckDateTime(scheduledString);
            List<string> tags = json.tags.ToObject<List<string>>();
            string status = json.status;
            int fee = json.fee;
            string createdString = json.created;
            DateTime? created = Utils.Checks.CheckDateTime(createdString);

            return new IssPayment(
                id: id, amount: amount, description: description, line: line, barCode: barCode,
                scheduled: scheduled, tags: tags, status: status, fee: fee, created: created
            );
        }
    }
}
