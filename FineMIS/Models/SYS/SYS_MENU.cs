using System;
using System.Collections.Generic;
using System.Linq;
using FineMIS.Controls;
using PetaPoco;

namespace FineMIS
{
    public partial class SYS_MENU : ITree, ICloneable, IKeyId
    {
        private static List<SYS_MENU> _menus;

        public static List<SYS_MENU> Menus
        {
            get
            {
                InitMenus();
                return _menus;
            }
        }

        [Ignore]
        public int TreeLevel { get; set; }

        [Ignore]
        public bool Enabled { get; set; }

        [Ignore]
        public bool IsTreeLeaf { get; set; }

        public static void Reload()
        {
            _menus = null;
        }

        private static void InitMenus()
        {
            _menus = new List<SYS_MENU>();

            var dbMenus = Fetch("WHERE Active=@0", true).OrderBy(m => m.SortIndex).ToList();

            ResolveMenuCollection(dbMenus, 0, 0);
        }


        private static int ResolveMenuCollection(List<SYS_MENU> dbMenus, long? parentId, int level)
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

        public object Clone()
        {
            var menu = new SYS_MENU
            {
                Id = Id,
                ParentId = ParentId,
                Name = Name,
                ImageUrl = ImageUrl,
                NavigateUrl = NavigateUrl,
                SortIndex = SortIndex,
                TreeLevel = TreeLevel,
                Enabled = Enabled,
                IsTreeLeaf = IsTreeLeaf
            };
            return menu;
        }
    }
}