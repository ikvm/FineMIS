using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

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

        public List<SYS_MENU> Menus { get; }

        /// <summary>
        /// Creates a new intance using the specified username and isAuthenticated bit.
        /// </summary>
        public CustomIdentity(string userName, long userId, long cmpyId, bool isAuthenticated, IEnumerable<long> roleIds)
        {
            Name = userName;
            RoleIds = new List<long>();
            UserId = userId;
            CmpyId = cmpyId;
            IsAuthenticated = isAuthenticated;
            RoleIds = roleIds?.ToList() ?? new List<long>();
        }
    }
}