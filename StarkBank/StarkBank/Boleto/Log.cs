﻿using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkBank
{
    public partial class Boleto
    {
        /// <summary>
        /// Boleto.Log object
        /// <br/>
        /// Every time a Boleto entity is updated, a corresponding Boleto.Log
        /// is generated for the entity. This log is never generated by the
        /// user, but it can be retrieved to check additional information
        /// on the Boleto.
        /// <br/>
        /// Properties:
        /// <list>
        ///     <item>ID [string]: unique id returned when the log is created. ex: "5656565656565656"</item>
        ///     <item>Boleto [Boleto]: Boleto entity to which the log refers to.</item>
        ///     <item>Errors [list of strings]: list of errors linked to this Boleto event</item>
        ///     <item>Type [string]: type of the Boleto event which triggered the log creation. ex: "registered" or "paid"</item>
        ///     <item>Created [DateTime]: creation datetime for the log. ex: DateTime.new(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public class Log : Utils.Resource
        {
            public DateTime Created { get; }
            public string Type { get; }
            public List<string> Errors { get; }
            public Boleto Boleto { get; }

            /// <summary>
            /// Boleto.Log object
            /// <br/>
            /// Every time a Boleto entity is updated, a corresponding Boleto.Log
            /// is generated for the entity. This log is never generated by the
            /// user, but it can be retrieved to check additional information
            /// on the Boleto.
            /// <br/>
            /// Attributes:
            /// <list>
            ///     <item>id [string]: unique id returned when the log is created. ex: "5656565656565656"</item>
            ///     <item>boleto [Boleto]: Boleto entity to which the log refers to.</item>
            ///     <item>errors [list of strings]: list of errors linked to this Boleto event</item>
            ///     <item>type [string]: type of the Boleto event which triggered the log creation. ex: "registered" or "paid"</item>
            ///     <item>created [DateTime]: creation datetime for the log. ex: DateTime.new(2020, 3, 10, 10, 30, 0, 0)</item>
            /// </list>
            /// </summary>
            public Log(string id, DateTime created, string type, List<string> errors, Boleto boleto) : base(id)
            {
                Created = created;
                Type = type;
                Errors = errors;
                Boleto = boleto;
            }

            /// <summary>
            /// Retrieve a specific Log
            /// <br/>
            /// Receive a single Log object previously created by the Stark Bank API by passing its id
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
            ///     <item>Log object with updated attributes</item>
            /// </list>
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
            /// Retrieve Logs
            /// <br/>
            /// Receive an IEnumerable of Log objects previously created in the Stark Bank API
            /// <br/>
            /// Parameters (optional):
            /// <list>
            ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
            ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>types [list of strings, default null]: filter for log event types. ex: "paid" or "registered"</item>
            ///     <item>boletoIds [list of strings, default null]: list of Boleto ids to filter logs. ex: ["5656565656565656", "4545454545454545"]</item>
            ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
            /// </list>
            /// <br/>
            /// Return:
            /// <list>
            ///     <item>list of Log objects with updated attributes</item>
            /// </list>
            /// </summary>
            public static IEnumerable<Log> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
                List<string> types = null, List<string> boletoIds = null, User user = null)
            {
                (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
                return Utils.Rest.GetList(
                    resourceName: resourceName,
                    resourceMaker: resourceMaker,
                    query: new Dictionary<string, object> {
                        { "limit", limit },
                        { "after", after },
                        { "before", before },
                        { "types", types },
                        { "boletoIds", boletoIds }
                    },
                    user: user
                ).Cast<Log>();
            }

            internal static (string resourceName, Utils.Api.ResourceMaker resourceMaker) Resource()
            {
                return (resourceName: "BoletoLog", resourceMaker: ResourceMaker);
            }

            internal static Utils.Resource ResourceMaker(dynamic json)
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
