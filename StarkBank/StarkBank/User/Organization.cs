

namespace StarkBank
{
    /// <summary>
    /// Organization object
    /// <br/>
    /// The Organization object is an authentication entity for the SDK that
    /// represents your entire Organization, being able to access any Workspace
    /// underneath it and even create new Workspaces.Only a legal representative
    /// of your organization can register or change the Organization credentials.
    /// All requests to the Stark Bank API must be authenticated via an SDK user,
    /// which must have been previously created at the Stark Bank website
    /// [https://sandbox.web.starkbank.com] or [https://web.starkbank.com]
    /// before you can use it in this SDK.Organizations may be passed as the user parameter on
    /// each request or may be defined as the default user at the start(See README).
    /// If you are accessing a specific Workspace using Organization credentials, you should
    /// specify the workspace ID when building the Organization object or by request, using
    /// the organization.set_workspace(workspace_id) method, which creates a copy of the organization
    /// object with the altered workspace ID. If you are listing or creating new Workspaces, the
    /// workspace_id should be None.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>ID [string]: unique id required to identify organization. ex: "5656565656565656"</item>
    ///     <item>Environment [string]: environment where the project is being used. ex: "sandbox" or "production"</item>
    ///     <item>WorkspaceID [string]: unique id of the accessed Workspace, if any. ex: null or "4848484848484848"
    ///     <item>Pem [string]: private key in pem format.ex: "-----BEGIN PUBLIC KEY-----\nMFYwEAYHKoZIzj0CAQYFK4EEAAoDQgAEyTIHK6jYuik6ktM9FIF3yCEYzpLjO5X/\ntqDioGM+R2RyW0QEo+1DG8BrUf4UXHSvCjtQ0yLppygz23z0yPZYfw==\n-----END PUBLIC KEY-----"</item>
    /// </list>
    /// </summary>
    public class Organization : User
    {
        public string WorkspaceID { get; }

        /// <summary>
        /// Organization object
        /// <br/>
        /// The Organization object is an authentication entity for the SDK that
        /// represents your entire Organization, being able to access any Workspace
        /// underneath it and even create new Workspaces.Only a legal representative
        /// of your organization can register or change the Organization credentials.
        /// All requests to the Stark Bank API must be authenticated via an SDK user,
        /// which must have been previously created at the Stark Bank website
        /// [https://sandbox.web.starkbank.com] or [https://web.starkbank.com]
        /// before you can use it in this SDK.Organizations may be passed as the user parameter on
        /// each request or may be defined as the default user at the start(See README).
        /// If you are accessing a specific Workspace using Organization credentials, you should
        /// specify the workspace ID when building the Organization object or by request, using
        /// the organization.set_workspace(workspace_id) method, which creates a copy of the organization
        /// object with the altered workspace ID. If you are listing or creating new Workspaces, the
        /// workspace_id should be None.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: unique id required to identify organization. ex: "5656565656565656"</item>
        ///     <item>environment [string]: environment where the project is being used. ex: "sandbox" or "production"</item>
        ///     <item>privateKey [string]: private key in pem format.ex: "-----BEGIN PUBLIC KEY-----\nMFYwEAYHKoZIzj0CAQYFK4EEAAoDQgAEyTIHK6jYuik6ktM9FIF3yCEYzpLjO5X/\ntqDioGM+R2RyW0QEo+1DG8BrUf4UXHSvCjtQ0yLppygz23z0yPZYfw==\n-----END PUBLIC KEY-----"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>workspaceID [string, default null]: unique id of the accessed Workspace, if any. ex: null or "4848484848484848"
        /// </list>
        /// </summary>
        public Organization(string environment, string id, string privateKey, string workspaceID = null) : base(environment, id, privateKey)
        {
            WorkspaceID = workspaceID;
        }

        internal override string AccessId()
        {
            if (WorkspaceID == null)
            {
                return "organization/" + ID;
            }
            return "organization/" + ID + "/workspace/" + WorkspaceID;
        }

        public Organization WithWorkspace(string workspaceID)
        {
            return new Organization(
                id: ID,
                environment: Environment,
                privateKey: Pem,
                workspaceID: workspaceID
            );
        }
    }
}
