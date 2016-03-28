using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FineMIS.Models.Comparer
{
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