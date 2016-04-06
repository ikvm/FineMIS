using System.Security.Principal;

namespace FineMIS
{
    /// <summary>
    /// Custom IPrincipal class used for authorization.
    /// </summary>
    public class CustomPrincipal : IPrincipal
    {
        /// <summary>
        /// Gets the identity of the current principal.
        /// </summary>
        public IIdentity Identity { get; }

        public bool IsInRole(string roleName)
        {
            return Identity != null && Identity.IsAuthenticated && !string.IsNullOrEmpty(Identity.Name);
        }

        /// <summary>
        /// Creates a new instance using the given IIdentity object.
        /// </summary>
        /// <param name="identity"></param>
        public CustomPrincipal(IIdentity identity)
        {
            Identity = identity;
        }
    }
}