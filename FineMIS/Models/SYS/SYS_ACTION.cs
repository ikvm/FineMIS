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
    public static class ActionHelper
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
                    .Where("RoleId IN (@0)", Current.RoleIds.ToArray())
                    .Where("SYS_ACION.Active = @0", true)
                    .Where("SYS_ROLE_MENU_ACTION.Active = @0", true)
                ).Distinct(new ActionComparer()).ToList();

            return actions;
        }
    }

    /// <summary>
    /// comparer of menus
    /// </summary>
    public class ActionComparer : IEqualityComparer<SYS_ACTION>
    {
        public bool Equals(SYS_ACTION x, SYS_ACTION y)
        {
            return x != null && y != null && x.Id == y.Id;
        }

        public int GetHashCode(SYS_ACTION obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}