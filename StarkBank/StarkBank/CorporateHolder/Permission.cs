using System;
using System.Linq;
using StarkBank.Utils;
using System.Collections.Generic;

namespace StarkBank
{
    public partial class CorporateHolder {
        /// <summary>
        /// Permission object
        /// <br/>
        /// Permission object represents access granted to an user for a particular cardholder
        /// <br/>
        /// Properties:
        /// <list>
        ///     <item>OwnerId [string, default null]: owner unique id. ex: "5656565656565656"</item>
        ///     <item>OwnerType [string, default null]: owner type. ex: "project"</item>
        ///     <item>OwnerEmail [string]: email address of the owner. ex: "tony@starkbank.com</item>
        ///     <item>OwnerName [string]: name of the owner. ex: "Tony Stark"</item>
        ///     <item>OwnerPictureUrl [string]: Profile picture Url of the owner. ex: "https://storage.googleapis.com/api-ms-workspace-sbx.appspot.com/pictures/member/6227829385592832?20230404164942"</item>
        ///     <item>OwnerStatus [string]: current owner status. ex: "active", "blocked", "canceled"</item>
        ///     <item>Created [DateTime]: creation DateTime for the Permission. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public partial class Permission : StarkCore.Utils.SubResource
        {
            public string OwnerId { get; }
            public string OwnerType { get; }
            public string OwnerEmail { get; }
            public string OwnerName { get; }
            public string OwnerPictureUrl { get; }
            public string OwnerStatus { get; }
            public DateTime? Created { get; }

            /// <summary>
            /// Permission object
            /// <br/>
            /// Permission object represents access granted to an user for a particular cardholder
            /// <br/>
            /// Parameters (optional):
            /// <list>
            ///     <item>OwnerId [string, default null]: owner unique id. ex: "5656565656565656"</item>
            ///     <item>OwnerType [string, default null]: owner type. ex: "project"</item>
            /// </list>
            /// Attributes (return-only):
            /// <list>
            ///     <item>OwnerEmail [string]: email address of the owner. ex: "tony@starkbank.com</item>
            ///     <item>OwnerName [string]: name of the owner. ex: "Tony Stark"</item>
            ///     <item>OwnerPictureUrl [string]: Profile picture Url of the owner. ex: "https://storage.googleapis.com/api-ms-workspace-sbx.appspot.com/pictures/member/6227829385592832?20230404164942"</item>
            ///     <item>OwnerStatus [string]: current owner status. ex: "active", "blocked", "canceled"</item>
            ///     <item>Created [DateTime]: creation DateTime for the CorporateHolder. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
            /// </list>
            /// </summary>
            public Permission( 
                string ownerId, string ownerType, string ownerEmail = null, string ownerName = null, string ownerPictureUrl = null, 
                string ownerStatus = null, DateTime? created = null
            )
            {
                OwnerId = ownerId;
                OwnerType = ownerType;
                OwnerEmail = ownerEmail;
                OwnerName = ownerName;
                OwnerPictureUrl = ownerPictureUrl;
                OwnerStatus = ownerStatus;
                Created = created;
            }

            public static List<Permission> ParsePermissions(dynamic json)
            {
                if (json is null) return null;

                List<Permission> permissions = new List<Permission>();

                foreach (dynamic permission in json)
                {
                    permissions.Add(Permission.ResourceMaker(permission));
                }
                return permissions;
            }

            internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
            {
                return (resourceName: "Permission", resourceMaker: ResourceMaker);
            }

            internal static Permission ResourceMaker(dynamic json)
            {
                string ownerId = json.ownerId;
                string ownerType = json.ownerType;
                string ownerEmail = json.ownerEmail;
                string ownerName = json.ownerName;
                string ownerPictureUrl = json.ownerPictureUrl;
                string ownerStatus = json.ownerStatus;
                string createdString = json.created;
                DateTime created = StarkCore.Utils.Checks.CheckDateTime(createdString);

                return new Permission(
                    ownerId: ownerId, ownerType: ownerType, ownerEmail: ownerEmail, ownerName: ownerName, ownerPictureUrl: ownerPictureUrl,
                    ownerStatus: ownerStatus, created: created
                );
            }
        }
    }
}
