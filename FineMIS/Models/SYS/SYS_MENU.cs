using System;
using System.Collections.Generic;
using System.Linq;
using FineMIS.Controls;
using PetaPoco;

namespace FineMIS
{
    public partial class SYS_MENU : ITree, IKeyId, ICloneable
    {
        [Ignore]
        public int TreeLevel { get; set; }

        [Ignore]
        public bool Enabled { get; set; }

        [Ignore]
        public bool IsTreeLeaf { get; set; }

        public object Clone()
        {
            return new SYS_MENU
            {
                Id = Id,
                ParentId = ParentId,
                Name = Name,
                ViewName = ViewName,
                ImageUrl = ImageUrl,
                NavigateUrl = NavigateUrl,
                SortIndex = SortIndex,
                TreeLevel = TreeLevel,
                Enabled = Enabled,
                IsTreeLeaf = IsTreeLeaf,
                Active = Active,
                UserBelongTo = UserBelongTo,
                CmpyBelongTo = CmpyBelongTo
            };
        }
    }

    /// <summary>
    /// use session to store menus
    /// </summary>
    public class SYS_MENU_Helper
    {
        public static List<SYS_MENU> Menus
        {
            get
            {
                if (!Current.IsAuthenticated) return new List<SYS_MENU>();
                if ((List<SYS_MENU>)Current.Session["__MENUS__"] == null) Current.Session["__MENUS__"] = InitMenus();
                return (List<SYS_MENU>)Current.Session["__MENUS__"];
            }
        }

        public static void Reload()
        {
            Current.Session["__MENUS__"] = null;
        }

        private static List<SYS_MENU> InitMenus()
        {
            var menus = new List<SYS_MENU>();

            var dbMenus = SYS_MENU.Fetch(
                Sql.Builder
                    .LeftJoin("SYS_ROLE_MENU")
                    .On("SYS_MENU.Id = SYS_ROLE_MENU.MenuId")
                    .Where("RoleId IN (@ids)", new { ids = Current.RoleIds.ToArray() })
                    .Where("SYS_MENU.Active = @0", true)
                    .Where("SYS_ROLE_MENU.Active = @0", true)
                    .OrderBy("SortIndex ASC")
                ).Distinct(new SYS_MENU_Comparer()).ToList();

            ResolveMenuCollection(dbMenus, 0, 0, ref menus);

            return menus;
        }

        private static int ResolveMenuCollection(List<SYS_MENU> dbMenus, long parentId, int level, ref List<SYS_MENU> menus)
        {
            var count = 0;

            foreach (var menu in dbMenus.Where(m => m.ParentId == parentId))
            {
                count++;

                menus.Add(menu);
                menu.TreeLevel = level;
                menu.IsTreeLeaf = true;
                menu.Enabled = true;

                level++;
                var childCount = ResolveMenuCollection(dbMenus, menu.Id, level, ref menus);
                if (childCount != 0)
                {
                    menu.IsTreeLeaf = false;
                }
                level--;
            }
            return count;
        }
    }

    /// <summary>
    /// comparer of menus
    /// </summary>
    public class SYS_MENU_Comparer : IEqualityComparer<SYS_MENU>
    {
        public bool Equals(SYS_MENU x, SYS_MENU y)
        {
            return x != null && y != null && x.Id == y.Id;
        }

        public int GetHashCode(SYS_MENU obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}