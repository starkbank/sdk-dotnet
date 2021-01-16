using System.Linq;
using System.Collections.Generic;


namespace StarkBank
{
    /// <summary>
    /// Workspace object
    /// <br/>
    /// Workspaces are bank accounts. They have independent balances, statements, operations and permissions.
    /// The only property that is shared between your workspaces is that they are linked to your organization,
    /// which carries your basic informations, such as tax ID, name, etc..
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Username [string]: Simplified name to define the workspace URL. This name must be unique across all Stark Bank Workspaces. Ex: "starkbank-workspace".</item>
    ///     <item>Name [string]: Full name that identifies the Workspace. This name will appear when people access the Workspace on our platform, for example. Ex: "Stark Bank Workspace"</item>
    ///     <item>ID [string, default null]: unique id returned when the Workspace is created. ex: "5656565656565656"</item>
    /// </list>
    /// </summary>
    public partial class Workspace : Utils.Resource
    {
        public string Username { get; }
        public string Name { get; }

        /// <summary>
        /// Workspace object
        /// <br/>
        /// Workspaces are bank accounts. They have independent balances, statements, operations and permissions.
        /// The only property that is shared between your workspaces is that they are linked to your organization,
        /// which carries your basic informations, such as tax ID, name, etc..
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>username [string]: Simplified name to define the workspace URL. This name must be unique across all Stark Bank Workspaces. Ex: "starkbank-workspace".</item>
        ///     <item>name [string]: Full name that identifies the Workspace. This name will appear when people access the Workspace on our platform, for example. Ex: "Stark Bank Workspace"</item>
        /// </list>
        /// <br/>
        /// Attributes:
        /// <list>
        ///     <item>id [string, default null]: unique id returned when the Workspace is created. ex: "5656565656565656"</item>
        /// </list>
        /// </summary>
        public Workspace(string username, string name = null, string id = null) : base(id)
        {
            Username = username;
            Name = name;
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
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>Workspace object with updated attributes</item>
        /// </list>
        /// </summary>
        public static Workspace Create(string username, string name, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.PostSingle(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entity: new Workspace(username: username, name: name),
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
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>Workspace object with updated attributes</item>
        /// </list>
        /// </summary>
        public static Workspace Get(string id, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
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
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of Workspace objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<Workspace> Query(int? limit = null, string username = null, List<string> ids = null, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
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

        internal static (string resourceName, Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "Workspace", resourceMaker: ResourceMaker);
        }

        internal static Utils.Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            string username = json.username;
            string name = json.name;

            return new Workspace(id: id, username: username, name: name);
        }
    }
}
