﻿using System;
using System.Linq;
using StarkBank.Utils;
using System.Collections.Generic;


namespace StarkBank
{
    /// <summary>
    /// Webhook subscription object
    /// <br/>
    /// A Webhook is used to subscribe to notification events on a user-selected endpoint.
    /// Currently available services for subscription are transfer, invoice, deposit, brcode-payment, boleto, boleto-payment,
    /// and utility-payment
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Url [string]: Url that will be notified when an event occurs.</item>
    ///     <item>Subscriptions [list of strings]: list of any non-empty combination of the available services. ex: ["transfer", "invoice"]</item>
    ///     <item>ID [string]: unique id returned when the Webhook is created. ex: "5656565656565656"</item>
    /// </list>
    /// </summary>
    public partial class Webhook : Resource
    {
        public string Url { get; }
        public List<string> Subscriptions { get; }

        /// <summary>
        /// Webhook subscription object
        /// <br/>
        /// A Webhook is used to subscribe to notification events on a user-selected endpoint.
        /// Currently available services for subscription are transfer, invoice, deposit, brcode-payment, boleto, boleto-payment,
        /// and utility-payment
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>url [string]: Url that will be notified when an event occurs.</item>
        ///     <item>subscriptions [list of strings]: list of any non-empty combination of the available services. ex: ["transfer", "boleto-payment"]</item>
        /// </list>
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when the Webhook is created. ex: "5656565656565656"</item>
        /// </list>
        /// </summary>
        public Webhook(string url, List<string> subscriptions = null, string id = null) : base(id)
        {
            Url = url;
            Subscriptions = subscriptions;
        }

        /// <summary>
        /// Create Webhook subscription
        /// <br/>
        /// Send a single Webhook subscription for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>url [string]: url to which notification events will be sent to. ex: "https://webhook.site/60e9c18e-4b5c-4369-bda1-ab5fcd8e1b29"</item>
        ///     <item>subscriptions [list of strings]: list of any non-empty combination of the available services. ex: ["transfer", "boleto-payment"]</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>Webhook object with updated attributes</item>
        /// </list>
        /// </summary>
        public static Webhook Create(string url, List<string> subscriptions, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PostSingle(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entity: new Webhook(url: url, subscriptions: subscriptions),
                user: user
            ) as Webhook;
        }

        /// <summary>
        /// Retrieve a specific Webhook subscription
        /// <br/>
        /// Receive a single Webhook subscription object previously created in the Stark Bank API by passing its id
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
        ///     <item>Webhook object with updated attributes</item>
        /// </list>
        /// </summary>
        public static Webhook Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as Webhook;
        }

        /// <summary>
        /// Retrieve Webhook subcriptions
        /// <br/>
        /// Receive an IEnumerable of Webhook subcription objects previously created in the Stark Bank API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of Webhook objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<Webhook> Query(int? limit = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit }
                },
                user: user
            ).Cast<Webhook>();
        }

        /// <summary>
        /// Retrieve paged Webhook subcriptions
        /// <br/>
        /// Receive a list of up to 100 Webhook objects previously created in the Stark Bank API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of Webhook objects with updated attributes and cursor to retrieve the next page of Transfer objects</item>
        /// </list>
        /// </summary>
        public static (List<Webhook> page, string pageCursor) Page(string cursor = null, int? limit = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            (List<StarkCore.Utils.SubResource> page, string pageCursor) = Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "limit", limit }
                },
                user: user
            );
            List<Webhook> webhooks = new List<Webhook>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                webhooks.Add(subResource as Webhook);
            }
            return (webhooks, pageCursor);
        }

        /// <summary>
        /// Delete a Webhook entity
        /// <br/>
        /// Delete a Webhook entity previously created in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: Webhook unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>deleted Webhook object</item>
        /// </list>
        /// </summary>
        public static Webhook Delete(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.DeleteId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as Webhook;
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "Webhook", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string url = json.url;
            List<string> subscriptions = json.subscriptions.ToObject<List<string>>();
            string id = json.id;

            return new Webhook(id: id, url: url, subscriptions: subscriptions);
        }
    }
}
