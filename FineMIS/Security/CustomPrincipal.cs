using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace FineMIS
{
    /// <summary>
    /// Custom IPrincipal class used for authorization.
    /// </summary>
    public class CustomPrincipal : IPrincipal
    {
        private IIdentity _identity;

        /// <summary>
        /// Gets the identity of the current principal.
        /// </summary>
        public IIdentity Identity
        {
            get { return _identity; }
        }

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
            _identity = identity;
        }
    }
}