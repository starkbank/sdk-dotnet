using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using EllipticCurve;


namespace StarkBank
{
    /// <summary>
    /// Webhook Event object
    /// <br/>
    /// An Event is the notification received from the subscription to the Webhook.
    /// Events cannot be created, but may be retrieved from the Stark Bank API to
    /// list all generated updates on entities.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>ID [string]: unique id returned when the Event is created. ex: "5656565656565656"</item>
    ///     <item>Log [Log]: a Log object from one the subscription services (TransferLog, BoletoLog, BoletoPaymentlog or UtilityPaymentLog)</item>
    ///     <item>Created [DateTime]: creation datetime for the notification event. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>IsDelivered [bool]: true if the Event has been successfully delivered to the user url. ex: False</item>
    ///     <item>Subscription [string]: service that triggered this event. ex: "transfer", "utility-payment"</item>
    /// </list>
    /// </summary>
    public partial class Event : Utils.Resource
    {
        public Utils.Resource Log { get; }
        public bool? IsDelivered { get; }
        public string Subscription { get; }
        public DateTime? Created { get; }

        /// <summary>
        /// Webhook Event object
        /// <br/>
        /// An Event is the notification received from the subscription to the Webhook.
        /// Events cannot be created, but may be retrieved from the Stark Bank API to
        /// list all generated updates on entities.
        /// <br/>
        /// Attributes:
        /// <list>
        ///     <item>id [string]: unique id returned when the Event is created. ex: "5656565656565656"</item>
        ///     <item>log [Log]: a Log object from one the subscription services (TransferLog, BoletoLog, BoletoPaymentlog or UtilityPaymentLog)</item>
        ///     <item>created [DateTime]: creation datetime for the notification event. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>isDelivered [bool]: true if the Event has been successfully delivered to the user url. ex: False</item>
        ///     <item>subscription [string]: service that triggered this event. ex: "transfer", "utility-payment"</item>
        /// </list>
        /// </summary>
        public Event(string id, Utils.Resource log, bool? isDelivered, string subscription, DateTime? created = null) : base(id)
        {
            Log = log;
            IsDelivered = isDelivered;
            Subscription = subscription;
            Created = created;
        }

        /// <summary>
        /// Retrieve a specific notification Event
        /// <br/>
        /// Receive a single notification Event object previously created in the Stark Bank API by passing its id
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
        ///     <item>Event object with updated attributes</item>
        /// </list>
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
        /// Retrieve notification Events
        /// <br/>
        /// Receive an IEnumerable of notification Event objects previously created in the Stark Bank API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>isDelivered [bool, default null]: bool to filter successfully delivered events. ex: True or False</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of Event objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<Event> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
            bool? isDelivered = null, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "after", new Utils.StarkBankDate(after) },
                    { "before", new Utils.StarkBankDate(before) },
                    { "isDelivered", isDelivered }
                },
                user: user
            ).Cast<Event>();
        }

        /// <summary>
        /// Delete a notification Event
        /// <br/>
        /// Delete a of notification Event entity previously created in the Stark Bank API by its ID
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: Event unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>deleted Event object</item>
        /// </list>
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
        /// Update notification Event entity
        /// <br/>
        /// Update notification Event by passing id.
        /// If isDelivered is True, the event will no longer be returned on queries with isDelivered=False.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: Event unique ids. ex: "5656565656565656"</item>
        ///     <item>isDelivered [bool]: If True and event hasn't been delivered already, event will be set as delivered. ex: True</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>target Event with updated attributes</item>
        /// </list>
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

        /// <summary>
        /// Create single notification Event from a content string
        /// <br/>
        /// Create a single Event object received from event listening at subscribed user endpoint.
        /// If the provided digital signature does not check out with the StarkBank public key, a
        /// starkbank.exception.InvalidSignatureException will be raised.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>content [string]: response content from request received at user endpoint (not parsed)</item>
        ///     <item>signature [string]: base-64 digital signature received at response header "Digital-Signature"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>Parsed Event object</item>
        /// </list>
        /// </summary>
        public static Event Parse(string content, string signature, User user = null)
        {
            dynamic json = Utils.Json.Decode(content);
            Event parsedEvent = ResourceMaker(json["event"]);

            Signature signatureObject;
            try
            {
                signatureObject = Signature.fromBase64(signature);
            } catch
            {
                throw new Error.InvalidSignatureError("The provided signature is not valid");
            }

            if (verifySignature(content, signatureObject, user)) {
                return parsedEvent;
            }
            if (verifySignature(content, signatureObject, user, true)) {
                return parsedEvent;
            }

            throw new Error.InvalidSignatureError("The provided signature and content do not match the Stark Bank public key");
        }

        private static bool verifySignature(string content, Signature signature, User user, bool refresh = false)
        {
            
            PublicKey publicKey = Utils.Cache.StarkBankPublicKey;

            if (publicKey is null || refresh)
            {
                publicKey = GetPublicKeyPem(user);
            }

            return Ecdsa.verify(content, signature, publicKey);
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
            } else if (subscription == "invoice") {
                log = Invoice.Log.ResourceMaker(json.log);
            } else if (subscription == "deposit") {
                log = Deposit.Log.ResourceMaker(json.log);
            } else if (subscription == "brcode-payment") {
                log = BrcodePayment.Log.ResourceMaker(json.log);
            } else if (subscription == "boleto") {
                log = Boleto.Log.ResourceMaker(json.log);
            } else if (subscription == "boleto-payment") {
                log = BoletoPayment.Log.ResourceMaker(json.log);
            } else if (subscription == "utility-payment") {
                log = UtilityPayment.Log.ResourceMaker(json.log);
            } else if (subscription == "boleto-holmes") {
                log = BoletoHolmes.Log.ResourceMaker(json.log);
            }

            return new Event(
                id: id, isDelivered: isDelivered, subscription: subscription, created: created, log: log
            );
        }
    }
}
