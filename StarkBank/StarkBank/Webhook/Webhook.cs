using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkBank
{
    /// <summary>
    /// # Webhook subscription object
    ///
    /// A Webhook is used to subscribe to notification events on a user-selected endpoint.
    /// Currently available services for subscription are transfer, boleto, boleto-payment,
    /// and utility-payment
    ///
    /// ## Parameters (required):
    /// - url [string]: Url that will be notified when an event occurs.
    /// - subscriptions [list of strings]: list of any non-empty combination of the available services. ex: ["transfer", "boleto-payment"]
    ///
    /// ## Attributes:
    /// - id [string, default nil]: unique id returned when the log is created. ex: "5656565656565656"
    /// </summary>
    public partial class Webhook : Utils.Resource
    {
        public string Url { get; }
        public List<string> Subscriptions { get; }

        public Webhook(string url, List<string> subscriptions = null, string id = null) : base(id)
        {
            Url = url;
            Subscriptions = subscriptions;
        }

        /// <summary>
        /// # Create Webhook subscription
        ///
        /// Send a single Webhook subscription for creation in the Stark Bank API
        ///
        /// ## Parameters (required):
        /// - url [string]: url to which notification events will be sent to. ex: "https://webhook.site/60e9c18e-4b5c-4369-bda1-ab5fcd8e1b29"
        /// - subscriptions [list of strings]: list of any non-empty combination of the available services. ex: ["transfer", "boleto-payment"]
        ///
        /// ## Parameters (optional):
        /// - user [Project object]: Project object. Not necessary if StarkBank.user was set before function call
        ///
        /// ## Return:
        /// - Webhook object with updated attributes
        /// </summary>
        public static Webhook Create(string url, List<string> subscriptions, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.PostSingle(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entity: new Webhook(url: url, subscriptions: subscriptions),
                user: user
            ) as Webhook;
        }

        /// <summary>
        /// # Retrieve a specific Webhook subscription
        ///
        /// Receive a single Webhook subscription object previously created in the Stark Bank API by passing its id
        ///
        /// ## Parameters (required):
        /// - id [string]: object unique id. ex: "5656565656565656"
        ///
        /// ## Parameters (optional):
        /// - user [Project object]: Project object. Not necessary if StarkBank.user was set before function call
        ///
        /// ## Return:
        /// - Webhook object with updated attributes
        /// </summary>
        public static Webhook Get(string id, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as Webhook;
        }

        /// <summary>
        /// # Retrieve Webhook subcriptions
        ///
        /// Receive a generator of Webhook subcription objects previously created in the Stark Bank API
        ///
        /// ## Parameters (optional):
        /// - limit [integer, default nil]: maximum number of objects to be retrieved. Unlimited if nil. ex: 35
        /// - user [Project object, default nil]: Project object. Not necessary if StarkBank.user was set before function call
        ///
        /// ## Return:
        /// - generator of Webhook objects with updated attributes
        /// </summary>
        public static IEnumerable<Webhook> Query(int? limit = null, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit }
                },
                user: user
            ).Cast<Webhook>();
        }

        /// <summary>
        /// # Delete a Webhook entity
        ///
        /// Delete a Webhook entity previously created in the Stark Bank API
        ///
        /// ## Parameters (required):
        /// - id [string]: Webhook unique id. ex: "5656565656565656"
        ///
        /// ## Parameters (optional):
        /// - user [Project object]: Project object. Not necessary if StarkBank.user was set before function call
        ///
        /// ## Return:
        /// - deleted Webhook with updated attributes
        /// </summary>
        public static Webhook Delete(string id, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.DeleteId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as Webhook;
        }

        internal static (string resourceName, Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "Webhook", resourceMaker: ResourceMaker);
        }

        internal static Utils.Resource ResourceMaker(dynamic json)
        {
            string url = json.url;
            List<string> subscriptions = json.subscriptions.ToObject<List<string>>();
            string id = json.id;

            return new Webhook(id: id, url: url, subscriptions: subscriptions);
        }
    }
}
