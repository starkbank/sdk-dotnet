﻿using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkBank
{
    public partial class Boleto
    {
        /// <summary>
        /// Boleto::Log object
        ///
        /// Every time a Boleto entity is updated, a corresponding Boleto::Log
        /// is generated for the entity. This log is never generated by the
        /// user, but it can be retrieved to check additional information
        /// on the Boleto.
        ///
        /// ## Attributes:
        /// - id [string]: unique id returned when the log is created. ex: "5656565656565656"
        /// - boleto [Boleto]: Boleto entity to which the log refers to.
        /// - errors [list of strings]: list of errors linked to this Boleto event
        /// - type [string]: type of the Boleto event which triggered the log creation. ex: "registered" or "paid"
        /// - created [DateTime]: creation datetime for the boleto. ex: DateTime.new(2020, 3, 10, 10, 30, 0, 0)
        /// </summary>
        public class Log : Utils.IResource
        {
            public string ID { get; }
            public DateTime Created { get; }
            public string Type { get; }
            public List<string> Errors { get; }
            public Boleto Boleto { get; }

            public Log(string id, DateTime created, string type, List<string> errors, Boleto boleto)
            {
                ID = id;
                Created = created;
                Type = type;
                Errors = errors;
                Boleto = boleto;
            }

            /// <summary>
            /// Retrieve a specific Log
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
            /// - boleto_ids [list of strings, default nil]: list of Boleto ids to filter logs. ex: ["5656565656565656", "4545454545454545"]
            /// - types [list of strings, default nil]: filter for log event types. ex: "paid" or "registered"
            /// - after [Date, default nil] date filter for objects created only after specified date. ex: Date.new(2020, 3, 10)
            /// - before [Date, default nil] date filter for objects only before specified date. ex: Date.new(2020, 3, 10)
            /// - user [Project object, default nil]: Project object. Not necessary if StarkBank.user was set before function call
            /// 
            /// ## Return:
            /// - list of Log objects with updated attributes
            /// </summary>
            public static IEnumerable<Log> Query(int? limit = null, List<string> boletoIds = null, List<string> types = null, List<string> ids = null,
                DateTime? after = null, DateTime? before = null, User user = null)
            {
                (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
                return Utils.Rest.GetList(
                    resourceName: resourceName,
                    resourceMaker: resourceMaker,
                    query: new Dictionary<string, object> {
                        { "limit", limit },
                        { "boletoIds", boletoIds },
                        { "types", types },
                        { "after", after },
                        { "before", before }
                    },
                    user: user
                ).Cast<Log>();
            }

            internal static (string resourceName, Utils.Api.ResourceMaker resourceMaker) Resource()
            {
                return (resourceName: "BoletoLog", resourceMaker: ResourceMaker);
            }

            internal static Utils.IResource ResourceMaker(dynamic json)
            {
                List<string> errors = json.errors.ToObject<List<string>>();
                string id = json.id;
                string createdString = json.created;
                DateTime created = Utils.Checks.CheckDateTime(createdString);
                string type = json.type;
                Boleto boleto = Boleto.ResourceMaker(json.boleto);

                return new Log(id: id, created: created, type: type, errors: errors, boleto: boleto);
            }
        }
    }
}
