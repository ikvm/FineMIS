using System;
using System.Collections.Generic;
using System.Linq;
using PetaPoco;
using FineMIS.Controls;

namespace FineMIS
{
    public partial class SYS_MENU
    {
        public static List<SYS_MENU> Menus
        {
            get
            {
                var menus = new List<SYS_MENU>();

                foreach (var id in Current.RoleIds)
                {
                    menus.AddRange(Fetch(
                        Sql.Builder
                            .LeftJoin("SYS_ROLE_MENU_ACTION")
                            .On("SYS_MENU.Id = SYS_ROLE_MENU_ACTION.MenuId")
                            .Where("RoleId = @0", id)
                            .Where("SYS_MENU.Active = @0", true)
                            .Where("SYS_ROLE_MENU_ACTION.Active = @0", true)
                        ));
                }

                return menus.Distinct(new SYS_MENU_Comparer()).ToList();
            }
        }
    }
}