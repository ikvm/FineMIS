using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using OutOfMemory;
using PetaPoco;

namespace FineMIS
{
    public sealed class Security
    {
        /// <summary>
        /// Handles the AuthenticateRequest event of the context control.
        /// </summary>
        public static void AuthenticateRequest()
        {
            // default to an empty/unauthenticated user to assign to context.User.
            var identity = new CustomIdentity(string.Empty, 0, 0, false, null);
            var principal = new CustomPrincipal(identity);

            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var ticket = (HttpContext.Current.User.Identity as FormsIdentity)?.Ticket;
                if (!string.IsNullOrWhiteSpace(ticket?.UserData))
                {
                    var datas = ticket.UserData.Split('|');
                    if (datas.Length == 4)
                    {
                        identity = new CustomIdentity(datas[0], datas[1].ToInt64(), datas[2].ToInt64(), true,
                            datas[3].Split(',').ToInt64Array());
                        principal = new CustomPrincipal(identity);
                    }
                }
            }

            HttpContext.Current.User = principal;
        }

        /// <summary>
        /// Signs out user out of the current blog instance.
        /// </summary>
        public static void SignOut()
        {
            // clear session
            Current.Session?.Clear();

            var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddYears(-3);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        /// <summary>
        /// Attempts to sign the user into the current blog instance.
        /// </summary>
        /// <param name="username">The user's username.</param>
        /// <param name="password">The user's password.</param>
        /// <param name="rememberMe">Whether or not to persist the user's sign-in state.</param>
        /// <returns>True if the user is successfully authenticated and signed in; false otherwise.</returns>
        public static bool AuthenticateUser(string username, string password, bool rememberMe)
        {
            // clear session
            Current.Session?.Clear();

            var un = (username ?? string.Empty).Trim();
            var pw = (password ?? string.Empty).Trim();

            if (!string.IsNullOrWhiteSpace(un) && !string.IsNullOrWhiteSpace(pw))
            {
                var user = SYS_USER.SingleOrDefault(Sql.Builder.Where("UserName = @0", un).Where("Active = @0", true));

                // todo hash password
                if (user != null && user.Password == pw)
                {
                    var roles = SYS_USER_ROLE.Fetch(Sql.Builder.Where("UserId = @0", user.Id));
                    var result = new StringBuilder();
                    foreach (var role in roles)
                    {
                        result.Append(role.RoleId);
                        result.Append(",");
                    }
                    var roleIds = result.ToString().TrimEnd(',');

                    var context = HttpContext.Current;
                    var expirationDate = DateTime.Now.Add(FormsAuthentication.Timeout);

                    var ticket = new FormsAuthenticationTicket(
                        1,
                        un,
                        DateTime.Now,
                        expirationDate,
                        rememberMe,
                        $"{user.Name}|{user.Id}|{user.CmpyBelongTo}|{roleIds}",
                        FormsAuthentication.FormsCookiePath
                        );

                    var encryptedTicket = FormsAuthentication.Encrypt(ticket);

                    // setting a custom cookie name based on the current blog instance.
                    // if !rememberMe, set expires to DateTime.MinValue which makes the
                    // cookie a browser-session cookie expiring when the browser is closed.
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
                    {
                        Expires = rememberMe ? expirationDate : DateTime.MinValue,
                        HttpOnly = true
                    };

                    context.Response.Cookies.Add(cookie);

                    return true;
                }
            }

            return false;
        }

        #region Utilities

        /// <summary>
        /// Encrypts a string using the SHA256 algorithm.
        /// </summary>
        /// <param name="plainMessage">
        /// The plain Message.
        /// </param>
        /// <returns>
        /// The hash password.
        /// </returns>
        public static string HashPassword(string plainMessage)
        {
            var data = Encoding.UTF8.GetBytes(plainMessage);
            using (HashAlgorithm sha = new SHA256Managed())
            {
                sha.TransformFinalBlock(data, 0, data.Length);
                return Convert.ToBase64String(sha.Hash);
            }
        }

        /// <summary>
        /// Generates random password for password reset
        /// </summary>
        /// <returns>
        /// Random password
        /// </returns>
        public static string RandomPassword()
        {
            var chars = "abcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            var password = string.Empty;
            var random = new Random();

            for (var i = 0; i < 8; i++)
            {
                var x = random.Next(1, chars.Length);
                if (!password.Contains(chars.GetValue(x).ToString()))
                {
                    password += chars.GetValue(x);
                }
                else
                {
                    i--;
                }
            }

            return password;
        }

        #endregion
    }
}