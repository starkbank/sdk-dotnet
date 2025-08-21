using StarkBank.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StarkBank
{
    /// <summary>
    /// Split object
    /// <br/>
    /// Represents a financial split transaction, detailing the distribution of an amount to a specified receiver.
    /// </summary>
    /// <remarks>The Split resource is used to split an Invoice or Boleto between different receivers.</remarks>
    public partial class Split
    {
        public class Log : Resource
        {

            public DateTime Created { get; }
            public string Type { get; }
            public List<string> Errors { get; }
            public Split Split { get; }

            public Log(string id, DateTime created, string type, List<string> errors, Split split) : base(id)
            {
                Created = created;
                Type = type;
                Errors = errors;
                Split = split;
            }


            /// <summary>
            /// Retrieve a specific Split.Log 
            /// <br/>
            /// Retrieve a single Split.Log object previously created in the Stark Bank API by passing its id
            /// <br/>
            /// Parameters(required):
            /// <list>
            ///     <item>id[string]: object unique id. ex: "5659211052613632"</item>
            /// </list>
            /// <br/>
            /// Parameters(optional):
            /// <list>
            ///     <item>user[Project object]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
            /// </list>
            /// <br/>
            /// Return:
            /// <list>
            ///     <item>Split.Log object with updated attributes</item>
            /// </list>
            /// </summary>
            public static Log Get(string id, User user = null)
            {
                (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
                return Rest.GetId(
                    resourceName: resourceName,
                    resourceMaker: resourceMaker,
                    id: id,
                    user: user
                ) as Log;
            }

            /// <summary>
            /// Retrieve Split Logs
            /// <br/>
            /// Receive an IEnumerable of Split.Log objects previously created in the Stark Bank API
            /// <br/>
            /// Parameters (optional):
            /// <list>
            ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
            ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>types [list of strings, default null]: filter for log event types. ex: "paid" or "registered"</item>
            ///     <item>splitIds [list of strings, default null]: list of Split ids to filter logs. ex: ["5656565656565656", "5745664021495808"]</item>
            ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
            /// </list>
            /// <br/>
            /// Return:
            /// <list>
            ///     <item>list of Split.Log objects with updated attributes</item>
            /// </list>
            /// </summary>
            public static IEnumerable<Log> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
                List<string> types = null, List<string> splitIds = null, User user = null)
            {
                (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
                return Rest.GetList(
                    resourceName: resourceName,
                    resourceMaker: resourceMaker,
                    query: new Dictionary<string, object> {
                        { "limit", limit },
                        { "after", new StarkCore.Utils.StarkDate(after) },
                        { "before", new StarkCore.Utils.StarkDate(before) },
                        { "types", types },
                        { "splitIds ", splitIds  }
                    },
                    user: user
                ).Cast<Log>();
            }

            /// <summary>
            /// Retrieve paged Split.Logs
            /// <br/>
            /// Receive a list of up to 100 Split.Log objects previously created in the Stark Bank API and the cursor to the next page.
            /// Use this function instead of query if you want to manually page your requests.
            /// <br/>
            /// Parameters (optional):
            /// <list>
            ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
            ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
            ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>types [list of strings, default null]: filter for log event types. ex: "paid" or "registered"</item>
            ///     <item>splitIds [list of strings, default null]: list of Split ids to filter logs. ex: ["5656565656565656", "5745664021495808"]</item>
            ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
            /// </list>
            /// <br/>
            /// Return:
            /// <list>
            ///     <item>list of Split.Log objects with updated attributes and cursor to retrieve the next page of Split.Log objects</item>
            /// </list>
            /// </summary>
            public static (List<Log> page, string pageCursor) Page(string cursor = null, int? limit = null, DateTime? after = null,
                DateTime? before = null, List<string> types = null, List<string> splitIds = null, User user = null)
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
                        { "types", types },
                        { "splitIds", splitIds }
                    },
                    user: user
                );
                List<Log> logs = new List<Log>();
                foreach (StarkCore.Utils.SubResource subResource in page)
                {
                    logs.Add(subResource as Log);
                }
                return (logs, pageCursor);
            }

            internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
            {
                return (resourceName: "SplitLog", resourceMaker: ResourceMaker);
            }

            internal static Resource ResourceMaker(dynamic json)
            {
                List<string> errors = json.errors.ToObject<List<string>>();
                string id = json.id;
                string createdString = json.created;
                DateTime created = StarkCore.Utils.Checks.CheckDateTime(createdString);
                string type = json.type;
                Split split = Split.ResourceMaker(json.split);

                return new Log(id: id, created: created, type: type, errors: errors, split: split);
            }
        }
    }
}