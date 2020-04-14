using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using EllipticCurve;


namespace StarkBank
{
    /// <summary>
    /// # Webhook Event object
    ///
    /// An Event is the notification received from the subscription to the Webhook.
    /// Events cannot be created, but may be retrieved from the Stark Bank API to
    /// list all generated updates on entities.
    ///
    /// ## Attributes:
    /// - id [string]: unique id returned when the log is created. ex: "5656565656565656"
    /// - log [Log]: a Log object from one the subscription services (TransferLog, BoletoLog, BoletoPaymentlog or UtilityPaymentLog)
    /// - created [DateTime]: creation datetime for the notification event. ex: DateTime.new(2020, 3, 10, 10, 30, 0, 0)
    /// - is_delivered [bool]: true if the event has been successfully delivered to the user url. ex: False
    /// - subscription [string]: service that triggered this event. ex: "transfer", "utility-payment"
    /// </summary>
    public partial class Event : Utils.Resource
    {
        public Utils.Resource Log { get; }
        public bool? IsDelivered { get; }
        public string Subscription { get; }
        public DateTime? Created { get; }

        public Event(string id, Utils.Resource log, bool? isDelivered, string subscription, DateTime? created = null) : base(id)
        {
            Log = log;
            IsDelivered = isDelivered;
            Subscription = subscription;
            Created = created;
        }

        /// <summary>
        /// # Retrieve a specific notification Event
        ///
        /// Receive a single notification Event object previously created in the Stark Bank API by passing its id
        ///
        /// ## Parameters (required):
        /// - id [string]: object unique id. ex: "5656565656565656"
        ///
        /// ## Parameters (optional):
        /// - user [Project object]: Project object. Not necessary if StarkBank.user was set before function call
        ///
        /// ## Return:
        /// - Event object with updated attributes
        /// </summary>
        public static Event Get(string id, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as Event;
        }

        /// <summary>
        /// # Retrieve notification Events
        ///
        /// Receive a generator of notification Event objects previously created in the Stark Bank API
        ///
        /// ## Parameters (optional):
        /// - limit [integer, default nil]: maximum number of objects to be retrieved. Unlimited if nil. ex: 35
        /// - is_delivered [bool, default nil]: bool to filter successfully delivered events. ex: True or False
        /// - after [Date, default nil]: date filter for objects created only after specified date. ex: Date.new(2020, 3, 10)
        /// - before [Date, default nil]: date filter for objects only before specified date. ex: Date.new(2020, 3, 10)
        /// - user [Project object, default nil]: Project object. Not necessary if StarkBank.user was set before function call
        ///
        /// ## Return:
        /// - generator of Event objects with updated attributes
        /// </summary>
        public static IEnumerable<Event> Query(int? limit = null, bool? isDelivered = null,
            DateTime? after = null, DateTime? before = null, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "isDelivered", isDelivered },
                    { "after", after },
                    { "before", before }
                },
                user: user
            ).Cast<Event>();
        }

        /// <summary>
        /// # Delete a notification Event
        ///
        /// Delete a of notification Event entity previously created in the Stark Bank API by its ID
        ///
        /// ## Parameters (required):
        /// - id [string]: Event unique id. ex: "5656565656565656"
        ///
        /// ## Parameters (optional):
        /// - user [Project object]: Project object. Not necessary if StarkBank.user was set before function call
        ///
        /// ## Return:
        /// - deleted Event with updated attributes
        /// </summary>
        public static Event Delete(string id, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.DeleteId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as Event;
        }

        /// <summary>
        /// # Update notification Event entity
        ///
        /// Update notification Event by passing id.
        /// If is_delivered is True, the event will no longer be returned on queries with is_delivered=False.
        ///
        /// ## Parameters (required):
        /// - id [list of strings]: Event unique ids. ex: "5656565656565656"
        /// - is_delivered [bool]: If True and event hasn't been delivered already, event will be set as delivered. ex: True
        ///
        /// ## Parameters (optional):
        /// - user [Project object]: Project object. Not necessary if StarkBank.user was set before function call
        ///
        /// ## Return:
        /// - target Event with updated attributes
        /// </summary>
        public static Event Update(string id, bool isDelivered, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.PatchId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                payload: new Dictionary<string, object> {
                    { "isDelivered", isDelivered }
                },
                user: user
            ) as Event;
        }

        /// # Create single notification Event from a content string
        ///
        /// Create a single Event object received from event listening at subscribed user endpoint.
        /// If the provided digital signature does not check out with the StarkBank public key, a
        /// starkbank.exception.InvalidSignatureException will be raised.
        ///
        /// ## Parameters (required):
        /// - content [string]: response content from request received at user endpoint (not parsed)
        /// - signature [string]: base-64 digital signature received at response header "Digital-Signature"
        ///
        /// ## Parameters (optional):
        /// - user [Project object]: Project object. Not necessary if StarkBank.user was set before function call
        ///
        /// ## Return:
        /// - Parsed Event object
        public static Event Parse(string content, string signature, User user = null)
        {
            dynamic json = Utils.Json.Decode(content);
            Event parsedEvent = ResourceMaker(json["event"]);

            if (verifySignature(content, signature, user)) {
                return parsedEvent;
            }
            if (verifySignature(content, signature, user, true)) {
                return parsedEvent;
            }

            throw new Error.InvalidSignatureError("The provided signature and content do not match the Stark Bank public key");
        }

        private static bool verifySignature(string content, string signature, User user, bool refresh = false)
        {
            Signature signatureObject = Signature.fromBase64(signature);
            PublicKey publicKey = Utils.Cache.StarkBankPublicKey;

            if (publicKey is null || refresh)
            {
                publicKey = GetPublicKeyPem(user);
            }

            return Ecdsa.verify(content, signatureObject, publicKey);
        }

        private static PublicKey GetPublicKeyPem(User user)
        {
            dynamic json = Utils.Request.Fetch(
                method: Utils.Request.Get,
                path: "public-key",
                query: new Dictionary<string, object> { { "limit", 1 } },
                user: user
            ).Json();
            List<JObject> publicKeys = json.publicKeys.ToObject<List<JObject>>();
            dynamic publicKey = publicKeys.First();
            string content = publicKey.content;
            PublicKey publicKeyObject = PublicKey.fromPem(content);
            Utils.Cache.StarkBankPublicKey = publicKeyObject;
            return publicKeyObject;
        }

        internal static (string resourceName, Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "Event", resourceMaker: ResourceMaker);
        }

        internal static Utils.Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            bool? isDelivered = json.isDelivered;
            string subscription = json.subscription;
            string createdString = json.created;
            DateTime? created = Utils.Checks.CheckDateTime(createdString);

            Utils.Resource log = null;
            if (subscription == "transfer") {
                log = Transfer.Log.ResourceMaker(json.log);
            } else if (subscription == "boleto") {
                log = Boleto.Log.ResourceMaker(json.log);
            } else if (subscription == "boleto-payment") {
                log = BoletoPayment.Log.ResourceMaker(json.log);
            } else if (subscription == "utility-payment") {
                log = UtilityPayment.Log.ResourceMaker(json.log);
            }

            return new Event(
                id: id, isDelivered: isDelivered, subscription: subscription, created: created, log: log
            );
        }
    }
}
