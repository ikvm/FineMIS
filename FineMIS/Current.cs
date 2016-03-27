using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FineMIS
{
    public static class Current
    {
        /// <summary>
        /// Shortcut to HttpContext.Current.
        /// </summary>
        public static HttpContext Context => HttpContext.Current;

        /// <summary>
        /// Shortcut to HttpContext.Current.Request.
        /// </summary>
        public static HttpRequest Request => Context.Request;

        public static string UserName
        {
            get
            {
                var identity = Context.User.Identity as CustomIdentity;
                return identity != null ? identity.Name : string.Empty;
            }
        }

        public static long RoleId
        {
            get
            {
                var identity = Context.User.Identity as CustomIdentity;
                return identity?.RoleId ?? 0;
            }
        }

        public static long UserBelongTo
        {
            get
            {
                var identity = Context.User.Identity as CustomIdentity;
                return identity?.UserId ?? 0;
            }
        }

        public static long CmpyBelongTo
        {
            get
            {
                var identity = Context.User.Identity as CustomIdentity;
                return identity?.CmpyId ?? 0;
            }
        }
    }
}