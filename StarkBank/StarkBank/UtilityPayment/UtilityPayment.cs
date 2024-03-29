﻿using System;
using System.Linq;
using StarkBank.Utils;
using System.Collections.Generic;


namespace StarkBank
{
    /// <summary>
    /// UtilityPayment object
    /// <br/>
    /// When you initialize a UtilityPayment, the entity will not be automatically
    /// created in the Stark Bank API. The 'create' function sends the objects
    /// to the Stark Bank API and returns the list of created objects.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Line [string, default null]: Number sequence that describes the payment. Either 'line' or 'barCode' parameters are required. If both are sent, they must match. ex: "34191.09008 63571.277308 71444.640008 5 81960000000062"</item>
    ///     <item>BarCode [string, default null]: Bar code number that describes the payment. Either 'line' or 'barCode' parameters are required. If both are sent, they must match. ex: "34195819600000000621090063571277307144464000"</item>
    ///     <item>Description [string]: Text to be displayed in your statement (min. 10 characters). ex: "payment ABC"</item>
    ///     <item>Scheduled [DateTime, default today]: payment scheduled date. ex: new DateTime(2020, 3, 10)</item>
    ///     <item>Tags [list of strings]: list of strings for tagging</item>
    ///     <item>ID [string]: unique id returned when payment is created. ex: "5656565656565656"</item>
    ///     <item>Status [string]: current payment status. ex: "success" or "failed"</item>
    ///     <item>Amount [long integer]: amount automatically calculated from line or barCode. ex: 23456 (= R$ 234.56)</item>
    ///     <item>Fee [integer]: fee charged when UtilityPayment is created. ex: 200 (= R$ 2.00)</item>
    ///     <item>Type [string]: payment type. ex: "utility"</item>
    ///     <item>TransactionIds [list of strings]: ledger transaction ids linked to this UtilityPayment. ex: ["19827356981273"]</item>
    ///     <item>Created [DateTime]: creation datetime for the payment. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime]: latest update datetime for the payment. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class UtilityPayment : Resource
    {
        public long? Amount { get; }
        public string Description { get; }
        public string Line { get; }
        public string BarCode { get; }
        public DateTime? Scheduled { get; }
        public List<string> Tags { get; }
        public string Status { get; }
        public int? Fee { get; }
        public string Type { get; }
        public List<string> TransactionIds { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// UtilityPayment object
        /// <br/>
        /// When you initialize a UtilityPayment, the entity will not be automatically
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
        ///     <item>scheduled [DateTime, default today]: payment scheduled date. ex: new DateTime(2020, 3, 10)</item>
        ///     <item>tags [list of strings]: list of strings for tagging</item>
        /// </list>
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when payment is created. ex: "5656565656565656"</item>
        ///     <item>status [string]: current payment status. ex: "success" or "failed"</item>
        ///     <item>amount [long integer]: amount automatically calculated from line or barCode. ex: 23456 (= R$ 234.56)</item>
        ///     <item>fee [integer]: fee charged when UtilityPayment is created. ex: 200 (= R$ 2.00)</item>
        ///     <item>type [string]: payment type. ex: "utility"</item>
        ///     <item>transactionIds [list of strings]: ledger transaction ids linked to this UtilityPayment. ex: ["19827356981273"]</item>
        ///     <item>created [DateTime]: creation datetime for the payment. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime]: latest update datetime for the payment. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public UtilityPayment(string description, string id = null, long? amount = null, string line = null,
            string barCode = null, DateTime? scheduled = null, List<string> tags = null, string status = null,
            int? fee = null, string type = null, List<string> transactionIds = null, DateTime? created = null, 
            DateTime? updated = null) : base(id)
        {
            Amount = amount;
            Description = description;
            Line = line;
            BarCode = barCode;
            Scheduled = scheduled;
            Tags = tags;
            Status = status;
            Fee = fee;
            Type = type;
            TransactionIds = transactionIds;
            Created = created;
            Updated = updated;
        }

        internal new Dictionary<string, object> ToJson()
        {
            Dictionary<string, object> json = base.ToJson();
            json["Scheduled"] = new StarkCore.Utils.StarkDate((DateTime)json["Scheduled"]);
            return json;
        }

        /// <summary>
        /// Create UtilityPayments
        /// <br/>
        /// Send a list of UtilityPayment objects for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>payments [list of UtilityPayment objects]: list of UtilityPayment objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of UtilityPayment objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<UtilityPayment> Create(List<UtilityPayment> payments, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: payments,
                user: user
            ).ToList().ConvertAll(o => (UtilityPayment)o);
        }

        /// <summary>
        /// Create UtilityPayments
        /// <br/>
        /// Send a list of UtilityPayment objects for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>payments [list of Dictionaries]: list of Dictionaries representing the UtilityPayments to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of UtilityPayment objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<UtilityPayment> Create(List<Dictionary<string, object>> payments, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: payments,
                user: user
            ).ToList().ConvertAll(o => (UtilityPayment)o);
        }

        /// <summary>
        /// Retrieve a specific UtilityPayment
        /// <br/>
        /// Receive a single UtilityPayment object previously created by the Stark Bank API by passing its id
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
        ///     <item>UtilityPayment object with updated attributes</item>
        /// </list>
        /// </summary>
        public static UtilityPayment Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as UtilityPayment;
        }

        /// <summary>
        /// Retrieve a specific UtilityPayment pdf file
        /// <br/>
        /// Receive a single UtilityPayment pdf file generated in the Stark Bank API by passing its id.
        /// Only valid for utility payments with "success" status.
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
        ///     <item>UtilityPayment pdf file</item>
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
        /// Retrieve UtilityPayments
        /// <br/>
        /// Receive an IEnumerable of UtilityPayment objects previously created in the Stark Bank API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "paid"</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of UtilityPayment objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<UtilityPayment> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
            List<string> tags = null, List<string> ids = null, string status = null, User user = null)
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
                    { "ids", ids },
                    { "status", status }
                },
                user: user
            ).Cast<UtilityPayment>();
        }

        /// <summary>
        /// Retrieve paged UtilityPayments
        /// <br/>
        /// Receive a list of up to 100 UtilityPayment objects previously created in the Stark Bank API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "paid"</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of UtilityPayment objects with updated attributes and cursor to retrieve the next page of UtilityPayment objects</item>
        /// </list>
        /// </summary>
        public static (List<UtilityPayment> page, string pageCursor) Page(string cursor = null, int? limit = null, DateTime? after = null,
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
                    { "ids", ids },
                    { "status", status }
                },
                user: user
            );
            List<UtilityPayment> utilityPayments = new List<UtilityPayment>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                utilityPayments.Add(subResource as UtilityPayment);
            }
            return (utilityPayments, pageCursor);
        }

        /// <summary>
        /// Delete a UtilityPayment entity
        /// <br/>
        /// Delete a UtilityPayment entity previously created in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: UtilityPayment unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>deleted UtilityPayment object</item>
        /// </list>
        /// </summary>
        public static UtilityPayment Delete(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.DeleteId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as UtilityPayment;
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "UtilityPayment", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            long? amount = json.amount;
            string description = json.description;
            string line = json.line;
            string barCode = json.barCode;
            string scheduledString = json.scheduled;
            DateTime? scheduled = StarkCore.Utils.Checks.CheckNullableDateTime(scheduledString);
            List<string> tags = json.tags?.ToObject<List<string>>();
            string status = json.status;
            int? fee = json.fee;
            string type = json.type;
            List<string> transactionIds = json.transactionIds?.ToObject<List<string>>();
            string createdString = json.created;
            DateTime? created = StarkCore.Utils.Checks.CheckNullableDateTime(createdString);
            string updatedString = json.updated;
            DateTime? updated = StarkCore.Utils.Checks.CheckNullableDateTime(updatedString);

            return new UtilityPayment(
                id: id, amount: amount, description: description, line: line, barCode: barCode,
                scheduled: scheduled, tags: tags, status: status, fee: fee, type: type, 
                transactionIds: transactionIds, created: created, updated: updated
            );
        }
    }
}
