using System.Linq;
using System.Collections.Generic;
using System;
using StarkCore.Utils;
using StarkCore;


namespace StarkBank
{
    /// <summary>
    /// Workspace object
    /// <br/>
    /// Workspaces are bank accounts. They have independent balances, statements, operations and permissions.
    /// The only property that is shared between your workspaces is that they are linked to your organization,
    /// which carries your basic information, such as tax ID, name, etc..
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Username [string]: Simplified name to define the workspace URL. This name must be unique across all Stark Bank Workspaces. Ex: "starkbank-workspace".</item>
    ///     <item>Name [string]: Full name that identifies the Workspace. This name will appear when people access the Workspace on our platform, for example. Ex: "Stark Bank Workspace"</item>
    ///     <item>AllowedTaxIds [list of strings]: list of tax IDs that will be allowed to send Deposits to this Workspace. ex: ["012.345.678-90", "20.018.183/0001-80"]</item>
    ///     <item>ID [string, default null]: unique id returned when the Workspace is created. ex: "5656565656565656"</item>
    ///     <item>Status [string]: current Workspace status. Options: "active", "closed", "frozen" or "blocked"</item>
    ///     <item>OrganizationId [string]: unique organization id returned when the organization is created. ex: "5656565656565656"</item>
    ///     <item>PictureUrl [string]: public workspace image (png) URL. ex: "https://storage.googleapis.com/api-ms-workspace-sbx.appspot.com/pictures/workspace/6284441752174592.png?20230208220551"</item>
    ///     <item>Created [DateTime]: creation datetime for the workspace. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class Workspace : Resource
    {
        public string Username { get; }
        public string Name { get; }
        public List<string> AllowedTaxIds { get; }
        public string Status { get; }
        public string OrganizationId { get; }
        public string PictureUrl { get; }
        public DateTime? Created { get; }

        /// <summary>
        /// Workspace object
        /// <br/>
        /// Workspaces are bank accounts. They have independent balances, statements, operations and permissions.
        /// The only property that is shared between your workspaces is that they are linked to your organization,
        /// which carries your basic information, such as tax ID, name, etc..
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>username [string]: Simplified name to define the workspace URL. This name must be unique across all Stark Bank Workspaces. Ex: "starkbank-workspace".</item>
        ///     <item>name [string]: Full name that identifies the Workspace. This name will appear when people access the Workspace on our platform, for example. Ex: "Stark Bank Workspace"</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>allowedTaxIds [list of strings, default null]: list of tax IDs that will be allowed to send Deposits to this Workspace. ex: ["012.345.678-90", "20.018.183/0001-80"]</item>
        /// </list>
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when the Workspace is created. ex: "5656565656565656"</item>
        ///     <item>status [string]: current Workspace status. Options: "active", "closed", "frozen" or "blocked"</item>
        ///     <item>organizationId [string]: unique organization id returned when the organization is created. ex: "5656565656565656"</item>
        ///     <item>pictureUrl [string]: public workspace image (png) URL. ex: "https://storage.googleapis.com/api-ms-workspace-sbx.appspot.com/pictures/workspace/6284441752174592.png?20230208220551"</item>
        ///     <item>created [DateTime]: creation datetime for the workspace. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public Workspace(string username, string name = null, List<string> allowedTaxIds = null, string id = null, string status = null,
        string organizationId = null, string pictureUrl = null, DateTime? created = null) : base(id)
        {
            Username = username;
            Name = name;        
            AllowedTaxIds = allowedTaxIds;
            Status = status;
            OrganizationId = organizationId;
            PictureUrl = pictureUrl;
            Created = created;
        }

        /// <summary>
        /// Create Workspace
        /// <br/>
        /// Send a single Workspace for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>username [string]: Simplified name to define the workspace URL. This name must be unique across all Stark Bank Workspaces. Ex: "starkbank-workspace".</item>
        ///     <item>name [string]: Full name that identifies the Workspace. This name will appear when people access the Workspace on our platform, for example. Ex: "Stark Bank Workspace"</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>allowedTaxIds [list of strings, default null]: list of tax IDs that will be allowed to send Deposits to this Workspace. ex: ["012.345.678-90", "20.018.183/0001-80"]</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>Workspace object with updated attributes</item>
        /// </list>
        /// </summary>
        public static Workspace Create(string username, string name, List<string> allowedTaxIds = null, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.PostSingle(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entity: new Workspace(username: username, name: name, allowedTaxIds: allowedTaxIds),
                user: user
            ) as Workspace;
        }

        /// <summary>
        /// Retrieve a specific Workspace
        /// <br/>
        /// Receive a single Workspace object previously created in the Stark Bank API by passing its id
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: object unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>Workspace object with updated attributes</item>
        /// </list>
        /// </summary>
        public static Workspace Get(string id, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as Workspace;
        }

        /// <summary>
        /// Retrieve Workspaces
        /// <br/>
        /// Receive an IEnumerable of Workspace objects previously created in the Stark Bank API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>username [string, default null]: Simplified name to define the workspace URL. This name must be unique across all Stark Bank Workspaces. Ex: "starkbank-workspace".</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of Workspace objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<Workspace> Query(int? limit = null, string username = null, List<string> ids = null, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "username", username },
                    { "ids", ids},
                },
                user: user
            ).Cast<Workspace>();
        }

        /// <summary>
        /// Retrieve paged Workspaces
        /// <br/>
        /// Receive a list of up to 100 Workspace objects previously created in the Stark Bank API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>username [string, default null]: Simplified name to define the workspace URL. This name must be unique across all Stark Bank Workspaces. Ex: "starkbank-workspace".</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of Workspace objects with updated attributes and cursor to retrieve the next page of Workspace objects</item>
        /// </list>
        /// </summary>
        public static (List<Workspace> page, string pageCursor) Page(string cursor = null, int? limit = null,
            string username = null, List<string> ids = null, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            (List<SubResource> page, string pageCursor) = Utils.Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "limit", limit },
                    { "username", username },
                    { "ids", ids }
                },
                user: user
            );
            List<Workspace> workspaces = new List<Workspace>();
            foreach (SubResource subResource in page)
            {
                workspaces.Add(subResource as Workspace);
            }
            return (workspaces, pageCursor);
        }

        /// <summary>
        /// Update Workspace
        /// <br/>
        /// Update a Workspace by passing its ID.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: Workspace ID. ex: '5656565656565656'</item>
        /// </list>
        /// Parameters (conditionally-required):
        /// <list>
        ///     <item>pictureType [string]: picture MIME type. This parameter will be required if the picture parameter is informed ex: "image/png" or "image/jpeg"</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>name [string, default null]: Full name that identifies the Workspace. This name will appear when people access the Workspace on our platform, for example. Ex: "Stark Bank Workspace"</item>
        ///     <item>username [string, default null]: Simplified name to define the workspace URL. This name must be unique across all Stark Bank Workspaces. Ex: "starkbank-workspace".</item>
        ///     <item>allowedTaxIds [list of strings, default null]: list of tax IDs that will be allowed to send Deposits to this Workspace. If empty, all are allowed. ex: ["012.345.678-90", "20.018.183/0001-80"]</item>
        ///     <item>status [string, default null]: current Workspace status. Options: "active" or "blocked"</item>
        ///     <item>picture [byte[], default null]: Binary buffer of the picture. ex: open("/path/to/file.png", "rb").read()</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>target Workspace with updated attributes</item>
        /// </list>
        /// </summary>
        public static Workspace Update(
            string id, string name = null, string username = null, List<string> allowedTaxIds = null, string status = null, 
            byte[] picture = null, string pictureType = null, User user = null
        ) {
            Dictionary<string, object> patchData = new Dictionary<string, object> {
                { "name", name },
                { "username", username },
                { "allowedTaxIds", allowedTaxIds },
                { "status", status }
            };

            if (picture != null)
            {
                patchData.Add("picture", "data:" + pictureType  + ";base64," + Convert.ToBase64String(picture));
            }

            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.PatchId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                payload: patchData,
                user: user
            ) as Workspace;
        }

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "Workspace", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            string username = json.username;
            string name = json.name;
            List<string> allowedTaxIds = json.allowedTaxIds.ToObject<List<string>>();
            string status = json.status;
            string organizationId = json.organizationId;
            string pictureUrl = json.pictureUrl;
            string createdString = json.created;
            DateTime? created = Checks.CheckNullableDateTime(createdString);

            return new Workspace(
                id: id, username: username, name: name, allowedTaxIds: allowedTaxIds, 
                status: status, organizationId: organizationId, pictureUrl: pictureUrl, created: created
            );
        }
    }
}
