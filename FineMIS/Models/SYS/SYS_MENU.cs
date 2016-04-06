using System;
using System.Collections.Generic;
using System.Linq;
using FineMIS.Controls;
using OutOfMemory;
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
    /// thread safe instance of menus
    /// </summary>
    public class SYS_MENU_Helper
    {
        private SYS_MENU_Helper()
        {
        }

        private static List<SYS_MENU> _menus;
        private static readonly object Lock = new object();

        public static List<SYS_MENU> Menus
        {
            get
            {
                lock (Lock)
                {
                    if (_menus == null)
                    {
                        InitMenus();
                    }

                    return _menus;
                }
            }
        }

        public static void Reload()
        {
            lock (Lock)
            {
                _menus = null;
            }
        }

        private static void InitMenus()
        {
            _menus = new List<SYS_MENU>();

            var dbMenus = SYS_MENU.Fetch(
                Sql.Builder
                    .LeftJoin("SYS_ROLE_MENU_ACTION")
                    .On("SYS_MENU.Id = SYS_ROLE_MENU_ACTION.MenuId")
                    // at least have one action is ok to view
                    // or specify the 'view' action
                    //.LeftJoin("SYS_ACTION")
                    //.On("SYS_ACTION.Id = SYS_ROLE_MENU_ACTION.ActionId")
                    //.Where("SYS_ACTION.Name = @0", "View")
                    .Where("RoleId IN (@ids)", new { ids = Current.RoleIds.ToArray() })
                    .Where("SYS_MENU.Active = @0", true)
                    .Where("SYS_ROLE_MENU_ACTION.Active = @0", true)
                );

            dbMenus = dbMenus.Distinct(new SYS_MENU_Comparer()).ToList();

            ResolveMenuCollection(dbMenus, 0, 0);
        }


        private static int ResolveMenuCollection(List<SYS_MENU> dbMenus, long parentId, int level)
        {
            var count = 0;

            foreach (var menu in dbMenus.Where(m => m.ParentId == parentId))
            {
                count++;

                _menus.Add(menu);
                menu.TreeLevel = level;
                menu.IsTreeLeaf = true;
                menu.Enabled = true;

                level++;
                var childCount = ResolveMenuCollection(dbMenus, menu.Id, level);
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