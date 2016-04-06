using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using OutOfMemory;
using PetaPoco;

namespace FineMIS
{
    /// <summary>
    /// IIdentity class used for authentication.
    /// </summary>
    public class CustomIdentity : IIdentity
    {
        /// <summary>
        /// Gets the type of authentication used by BlogEngine.NET.
        /// </summary>
        public string AuthenticationType => "FineMIS Custom Identity";

        /// <summary>
        /// Gets a value that indicates whether the user has been authenticated.
        /// </summary>
        public bool IsAuthenticated { get; }

        /// <summary>
        /// Gets the name of the current user.
        /// </summary>
        public string Name { get; }

        public List<long> RoleIds { get; }

        public long UserId { get; }

        public long CmpyId { get; }

        /// <summary>
        /// Creates a new intance using the specified username and isAuthenticated bit.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userId"></param>
        /// <param name="cmpyId"></param>
        /// <param name="isAuthenticated">Whether or not the user is authenticated.</param>
        public CustomIdentity(string userName, long userId, long cmpyId, bool isAuthenticated)
        {
            Name = userName;
            RoleIds = new List<long>();
            UserId = userId;
            CmpyId = cmpyId;
            IsAuthenticated = isAuthenticated;

            var userRoles = SYS_USER_ROLE.Fetch(
                Sql.Builder
                    .Where("UserId = @0", UserId)
                    .Where("Active = @0", true)
                );

            if (userRoles.Count > 0)
            {
                foreach (var userRole in userRoles)
                {
                    RoleIds.Add(userRole.RoleId.ToInt64());
                }
            }
        }
    }
}