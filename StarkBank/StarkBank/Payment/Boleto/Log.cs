﻿using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkBank
{
    public partial class BoletoPayment
    {
        /// <summary>
        /// # BoletoPayment::Log object
        ///
        /// Every time a BoletoPayment entity is modified, a corresponding BoletoPayment::Log
        /// is generated for the entity. This log is never generated by the
        /// user, but it can be retrieved to check additional information
        /// on the BoletoPayment.
        ///
        /// ## Attributes:
        /// - id [string]: unique id returned when the log is created. ex: "5656565656565656"
        /// - payment [BoletoPayment]: BoletoPayment entity to which the log refers to.
        /// - errors [list of strings]: list of errors linked to this BoletoPayment event.
        /// - type [string]: type of the BoletoPayment event which triggered the log creation. ex: "registered" or "paid"
        /// - created [DateTime]: creation datetime for the payment. ex: DateTime.new(2020, 3, 10, 10, 30, 0, 0)
        /// </summary>
        public class Log : Utils.Resource
        {
            public DateTime Created { get; }
            public string Type { get; }
            public List<string> Errors { get; }
            public BoletoPayment Payment { get; }

            public Log(string id, DateTime created, string type, List<string> errors, BoletoPayment payment) : base(id)
            {
                Created = created;
                Type = type;
                Errors = errors;
                Payment = payment;
            }

            /// <summary>
            /// # Retrieve a specific Log
            ///
            /// Receive a single Log object previously created by the Stark Bank API by passing its id
            ///
            /// ## Parameters (required):
            /// - id [string]: object unique id. ex: "5656565656565656"
            ///
            /// ## Parameters (optional):
            /// - user [Project object]: Project object. Not necessary if StarkBank.user was set before function call
            ///
            /// ## Return:
            /// - Log object with updated attributes
            /// </summary>
            public static Log Get(string id, User user = null)
            {
                (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
                return Utils.Rest.GetId(
                    resourceName: resourceName,
                    resourceMaker: resourceMaker,
                    id: id,
                    user: user
                ) as Log;
            }

            /// <summary>
            /// # Retrieve Logs
            ///
            /// Receive a generator of Log objects previously created in the Stark Bank API
            ///
            /// ## Parameters (optional):
            /// - limit [integer, default nil]: maximum number of objects to be retrieved. Unlimited if nil. ex: 35
            /// - payment_ids [list of strings, default nil]: list of BoletoPayment ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]
            /// - types [list of strings, default nil]: filter retrieved objects by event types. ex: "paid" or "registered"
            /// - user [Project object, default nil]: Project object. Not necessary if StarkBank.user was set before function call
            ///
            /// ## Return:
            /// - list of Log objects with updated attributes
            /// </summary>
            public static IEnumerable<Log> Query(int? limit = null, List<string> paymentIds = null, List<string> types = null, List<string> ids = null,
                User user = null)
            {
                (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
                return Utils.Rest.GetList(
                    resourceName: resourceName,
                    resourceMaker: resourceMaker,
                    query: new Dictionary<string, object> {
                        { "limit", limit },
                        { "paymentIds", paymentIds },
                        { "types", types }
                    },
                    user: user
                ).Cast<Log>();
            }

            internal static (string resourceName, Utils.Api.ResourceMaker resourceMaker) Resource()
            {
                return (resourceName: "BoletoPaymentLog", resourceMaker: ResourceMaker);
            }

            internal static Utils.Resource ResourceMaker(dynamic json)
            {
                List<string> errors = json.errors.ToObject<List<string>>();
                string id = json.id;
                string createdString = json.created;
                DateTime created = Utils.Checks.CheckDateTime(createdString);
                string type = json.type;
                BoletoPayment payment = BoletoPayment.ResourceMaker(json.payment);

                return new Log(id: id, created: created, type: type, errors: errors, payment: payment);
            }
        }
    }
}