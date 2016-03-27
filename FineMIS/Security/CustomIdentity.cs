using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

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

        public long RoleId { get; }

        public long UserId { get; }

        public long CmpyId { get; }

        /// <summary>
        /// Creates a new intance using the specified username and isAuthenticated bit.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <param name="cmpyId"></param>
        /// <param name="isAuthenticated">Whether or not the user is authenticated.</param>
        public CustomIdentity(string userName, long roleId, long userId, long cmpyId, bool isAuthenticated)
        {
            Name = userName;
            RoleId = roleId;
            UserId = userId;
            CmpyId = cmpyId;
            IsAuthenticated = isAuthenticated;
        }
    }
}