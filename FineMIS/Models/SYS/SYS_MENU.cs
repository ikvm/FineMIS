using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace FineMIS
{
    public partial class SYS_MENU
    {
        [ThreadStatic]
        private static List<SYS_MENU> _menus;

        public static List<SYS_MENU> Menus
        {
            get
            {
                if (_menus == null)
                {
                    Init();
                }
                return _menus;
            }
        }

        public static void Reload()
        {
            _menus = null;
        }

        private static void Init()
        {
            _menus = SYS_MENU.Fetch(Sql.Builder.Where("Active = @0", true).OrderBy("SortIndex"));
        }
    }
}