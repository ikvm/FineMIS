using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

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
        /// Shortcut to HttpContext.Current.Session.
        /// </summary>
        public static HttpSessionState Session
        {
            get
            {
                // if not authenticated, clear session
                if (!IsAuthenticated) HttpContext.Current.Session.Clear();
                return HttpContext.Current.Session;
            }
        }

        /// <summary>
        /// Shortcut to HttpContext.Current.User.Identity.Name.
        /// </summary>
        public static string UserName => Context.User.Identity.Name;

        public static bool IsAuthenticated => Context.User.Identity.IsAuthenticated;

        /// <summary>
        /// Shortcut to HttpContext.Current.User.Identity.RoleIds
        /// </summary>
        public static List<long> RoleIds
            =>
                (Context.User.Identity as CustomIdentity) != null
                    ? ((CustomIdentity)Context.User.Identity).RoleIds
                    : new List<long>();

        /// <summary>
        /// RoleIds split by comma
        /// </summary>
        public static string RoleIdsString
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var id in RoleIds)
                {
                    sb.Append(id);
                    sb.Append(",");
                }
                return sb.ToString().TrimEnd(',');
            }
        }

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