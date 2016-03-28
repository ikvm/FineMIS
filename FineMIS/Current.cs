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

        /// <summary>
        /// Shortcut to HttpContext.Current.User.Identity.Name.
        /// </summary>
        public static string UserName => Context.User.Identity.Name;

        /// <summary>
        /// Shortcut to HttpContext.Current.User.Identity.RoleIds
        /// </summary>
        public static List<long> RoleIds
            =>
                (Context.User.Identity as CustomIdentity) != null
                    ? ((CustomIdentity)Context.User.Identity).RoleIds
                    : new List<long>();

        /// <summary>
        /// Shortcut to HttpContext.Current.User.Identity.UserId
        /// </summary>
        public static long UserBelongTo => (Context.User.Identity as CustomIdentity)?.UserId ?? 0;

        /// <summary>
        /// Shortcut to HttpContext.Current.User.Identity.CmpyId
        /// </summary>
        public static long CmpyBelongTo => (Context.User.Identity as CustomIdentity)?.CmpyId ?? 0;
    }
}