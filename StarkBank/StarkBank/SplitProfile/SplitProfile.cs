using System;
using System.Linq;
using StarkBank.Utils;
using System.Collections.Generic;

namespace StarkBank
{
    /// <summary>
    /// SplitProfile object
    /// <br/>
    /// When you create a Split, the entity SplitProfile will be automatically created, if you haven't create a Split yet, you can use the put method to create your SplitProfile.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>interval [string]: frequency of transfer, default "week". Options: "day", "week", "month"</item>
    ///     <item>delay [long integer]: how long the amount will stay at the workspace in milliseconds, ex: 604800</item>
    ///     <item>tags [list of strings, default []]: list of strings for tagging</item>
    ///     <item>id [string]: unique id returned when the splitProfile is created. ex: "5656565656565656"</item>
    ///     <item>status [string]: current splitProfile status. ex: "created"</item>
    ///     <item>created [DateTime]: creation datetime for the splitProfile. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>updated [DateTime]: latest update datetime for the splitProfile. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class SplitProfile : Resource
    {
        public string Interval { get; }
        public long Delay { get; }
        public List<string> Tags { get; }
        public string Status { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// SplitProfile object
        /// <br/>
        /// When you create a Split, the entity SplitProfile will be automatically created, if you haven't create a Split yet, you can use the put method to create your SplitProfile.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>interval [string]: frequency of transfer, default "week". Options: "day", "week", "month"</item>
        ///     <item>delay [long integer]: how long the amount will stay at the workspace in milliseconds, ex: 604800</item>
        ///     <item>tags [list of strings, default []]: list of strings for tagging</item>
        /// </list>
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when the splitProfile is created. ex: "5656565656565656"</item>
        ///     <item>status [string]: current splitProfile status. ex: "created"</item>
        ///     <item>created [DateTime]: creation datetime for the splitProfile. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime]: latest update datetime for the splitProfile. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>

        public SplitProfile(string interval, long delay, List<string> tags = null, string id = null, string status = null,
            DateTime? created = null, DateTime? updated = null) : base(id)
        {
            Interval = interval;
            Delay = delay;
            Tags = tags;
            Status = status;
            Created = created;
            Updated = updated;
        }

        internal new Dictionary<string, object> ToJson()
        {
            Dictionary<string, object> json = base.ToJson();
            return json;
        }

        /// <summary>
        /// Put SplitProfiles
        /// <br/>
        /// Create SplitProfile or update it if you already have it created 
        /// Send a list of SplitProfile objects for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>SplitProfiles [list of SplitProfile objects]: list of SplitProfile objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of SplitProfile objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<SplitProfile> Put(List<SplitProfile> splitProfiles, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Put(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: splitProfiles,
                user: user
            ).ToList().ConvertAll(o => (SplitProfile)o);
        }

        /// <summary>
        /// Put SplitProfiles
        /// <br/>
        /// Create SplitProfile or update it if you already have it created 
        /// Send a list of SplitProfile objects for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>SplitProfiles [list of SplitProfile objects]: list of SplitProfile objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of SplitProfile objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<SplitProfile> Put(List<Dictionary<string, object>> splitProfiles, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Put(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: splitProfiles,
                user: user
            ).ToList().ConvertAll(o => (SplitProfile)o);
        }

        /// <summary>
        /// Retrieve a specific SplitProfile
        /// <br/>
        /// Receive a single SplitProfile object previously created in the Stark Bank API by passing its id
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: unique id returned when the splitProfile is created. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>SplitProfile object with updated attributes</item>
        /// </list>
        /// </summary>
        public static SplitProfile Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as SplitProfile;
        }

        /// <summary>
        /// Retrieve SplitProfiles
        /// <br/>
        /// Receive an IEnumerable of SplitProfile objects previously created in the Stark Bank API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "created", "paid", "canceled" or "overdue"</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of SplitProfile objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<SplitProfile> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
            string status = null, List<string> tags = null, List<string> ids = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "after", new StarkCore.Utils.StarkDate(after) },
                    { "before", new StarkCore.Utils.StarkDate(before) },
                    { "status", status },
                    { "tags", tags },
                    { "ids", ids }
                },
                user: user
            ).Cast<SplitProfile>();
        }

        /// <summary>
        /// Retrieve paged SplitProfiles
        /// <br/>
        /// Receive a list of up to 100 SplitProfile objects previously created in the Stark Bank API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// 
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "created", "paid", "canceled" or "overdue"</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of SplitProfile objects with updated attributes and cursor to retrieve the next page of SplitProfile objects</item>
        /// </list>
        /// </summary>
        public static (List<SplitProfile> page, string pageCursor) Page(string cursor = null, int? limit = null, DateTime? after = null,
            DateTime? before = null, string status = null, List<string> tags = null, List<string> ids = null, User user = null)
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
                    { "status", status },
                    { "tags", tags },
                    { "ids", ids }
                },
                user: user
            );
            List<SplitProfile> splitProfiles = new List<SplitProfile>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                splitProfiles.Add(subResource as SplitProfile);
            }
            return (splitProfiles, pageCursor);
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return ("SplitProfile", ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string interval = json.interval;
            long delay = json.delay;
            List<string> tags = json.tags?.ToObject<List<string>>();
            string id = json.id;
            string status = json.status;
            string createdString = json.created;
            string updatedString = json.updated;
            DateTime? created = StarkCore.Utils.Checks.CheckDateTime(createdString);
            DateTime? updated = StarkCore.Utils.Checks.CheckDateTime(updatedString);

            return new SplitProfile(interval: interval, delay: delay, tags: tags, id: id, status: status, created: created, updated: updated);
        }
    }
}