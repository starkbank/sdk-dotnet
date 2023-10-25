using System;
using StarkCore;
using System.Linq;
using StarkCore.Utils;
using System.Collections.Generic;

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
    ///     <item>WorkspaceId [string]: ID of the Workspace that generated this event. Mostly used when multiple Workspaces have Webhooks registered to the same endpoint. ex: "4545454545454545"</item>
    /// </list>
    /// </summary>
    public partial class Event : Resource
    {
        public Resource Log { get; }
        public bool? IsDelivered { get; }
        public string Subscription { get; }
        public DateTime? Created { get; }
        public string WorkspaceId { get; }

        /// <summary>
        /// Webhook Event object
        /// <br/>
        /// An Event is the notification received from the subscription to the Webhook.
        /// Events cannot be created, but may be retrieved from the Stark Bank API to
        /// list all generated updates on entities.
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when the Event is created. ex: "5656565656565656"</item>
        ///     <item>log [Log]: a Log object from one the subscription services (TransferLog, BoletoLog, BoletoPaymentlog or UtilityPaymentLog)</item>
        ///     <item>created [DateTime]: creation datetime for the notification event. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>isDelivered [bool]: true if the Event has been successfully delivered to the user url. ex: False</item>
        ///     <item>subscription [string]: service that triggered this event. ex: "transfer", "utility-payment"</item>
        ///     <item>workspaceId [string]: ID of the Workspace that generated this event. Mostly used when multiple Workspaces have Webhooks registered to the same endpoint. ex: "4545454545454545"</item>
        /// </list>
        /// </summary>
        public Event(string id, Resource log, bool? isDelivered, string subscription, string workspaceId, DateTime? created = null) : base(id)
        {
            Log = log;
            IsDelivered = isDelivered;
            Subscription = subscription;
            WorkspaceId = workspaceId;
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
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
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
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
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
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "after", new StarkDate(after) },
                    { "before", new StarkDate(before) },
                    { "isDelivered", isDelivered }
                },
                user: user
            ).Cast<Event>();
        }

        /// <summary>
        /// Retrieve paged notification Events
        /// <br/>
        /// Receive a list of up to 100 Event objects previously created in the Stark Bank API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>isDelivered [bool, default null]: bool to filter successfully delivered events. ex: True or False</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of Event objects with updated attributes and cursor to retrieve the next page of Event objects</item>
        /// </list>
        /// </summary>
        public static (List<Event> page, string pageCursor) Page(string cursor = null, int? limit = null, DateTime? after = null,
            DateTime? before = null, bool? isDelivered = null, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            (List<SubResource> page, string pageCursor) = Utils.Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "limit", limit },
                    { "after", new StarkDate(after) },
                    { "before", new StarkDate(before) },
                    { "isDelivered", isDelivered }
                },
                user: user
            );
            List<Event> events = new List<Event>();
            foreach (SubResource subResource in page)
            {
                events.Add(subResource as Event);
            }
            return (events, pageCursor);
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
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
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
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
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
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return (Event)Utils.Parse.ParseAndVerify(content, signature, resourceName, resourceMaker, user, "event");
        }

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "Event", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            bool? isDelivered = json.isDelivered;
            string subscription = json.subscription;
            string createdString = json.created;
            string workspaceId = json.workspaceId;
            DateTime? created = Checks.CheckDateTime(createdString);

            Resource log = null;
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
            } else if (subscription == "tax-payment") {
                log = TaxPayment.Log.ResourceMaker(json.log);
            } else if (subscription == "darf-payment") {
                log = DarfPayment.Log.ResourceMaker(json.log);
            }

            return new Event(
                id: id, isDelivered: isDelivered, subscription: subscription, created: created, log: log, workspaceId: workspaceId
            );
        }
    }
}
