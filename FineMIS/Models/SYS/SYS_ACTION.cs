using System;
using System.Collections.Generic;
using System.Linq;
using PetaPoco;

namespace FineMIS
{
    public partial class SYS_ACTION
    {

    }

    /// <summary>
    /// use session to store menus
    /// </summary>
    public class SYS_ACTION_Helper
    {
        public static List<SYS_ACTION> Actions
        {
            get
            {
                if ((List<SYS_ACTION>)Current.Session["__ACTIONS__"] == null)
                {
                    Current.Session["__ACTIONS__"] = InitActions();
                }
                return (List<SYS_ACTION>)Current.Session["__ACTIONS__"];
            }
        }

        public static void Reload()
        {
            Current.Session["__ACTIONS__"] = null;
        }

        private static List<SYS_ACTION> InitActions()
        {
            var actions = SYS_ACTION.Fetch(
                Sql.Builder
                    .LeftJoin("SYS_ROLE_MENU_ACTION")
                    .On("SYS_ACION.Id = SYS_ROLE_MENU_ACION.ActionId")
                    .Where("RoleId IN (@ids)", new { ids = Current.RoleIds.ToArray() })
                    .Where("SYS_MENU.Active = @0", true)
                    .Where("SYS_ROLE_MENU_ACTION.Active = @0", true)
                ).Distinct(new SYS_ACTION_Comparer()).ToList();

            return actions;
        }
    }

    /// <summary>
    /// comparer of actions
    /// </summary>
    public class SYS_ACTION_Comparer : IEqualityComparer<SYS_ACTION>
    {
        public bool Equals(SYS_ACTION x, SYS_ACTION y)
        {
            return x != null && y != null && x.Id == y.Id && x.MenuId == y.MenuId;
        }

        public int GetHashCode(SYS_ACTION obj)
        {
            return obj.Id.GetHashCode() ^ obj.MenuId.GetHashCode();
        }
    }
}