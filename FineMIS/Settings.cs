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

        private static void Refresh()
        {
            MenuStyle = "accordion";
            Title = "电子政务平台";
            Theme = "Neptune";
        }

        public static string MenuStyle { get; private set; }

        public static string Title { get; private set; }

        public static string Theme { get; private set; }
    }
}