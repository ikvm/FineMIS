using System;
using System.Web.UI;
using FineUI;

namespace FineMIS.Pages
{
    public partial class Page : MasterPage
    {
        public ISinglePageBase PageBase => (ISinglePageBase)Page;

        public Window Window => MainWindow;

        protected void Page_Init(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void MainWindow_Close(object sender, WindowCloseEventArgs e)
        {
            var argument = Request.Params["__EVENTARGUMENT"];
            PageBase.ProcessArgument(argument);
        }
    }
}