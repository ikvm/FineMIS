using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FineMIS
{
    public class Settings
    {
        static Settings()
        {
            Refresh();
        }

        // todo...
        private static void Refresh()
        {
            MenuStyle = "accordion";
            Title = "通用管理系统";
            Theme = "Neptune";
        }

        public static string MenuStyle { get; private set; }

        public static string Title { get; private set; }

        public static string Theme { get; private set; }
    }
}