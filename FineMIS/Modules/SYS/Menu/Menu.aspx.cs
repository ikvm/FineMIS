using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using FineMIS.Pages;
using FineUI;
using PetaPoco;

namespace FineMIS.Modules.SYS.Menu
{
    [Description("Menu")]
    public partial class Menu : SingleGridPage
    {
        /// <summary>
        /// if the grid is the only control in page
        /// then the grid ID must be MainPanel
        /// </summary>
        protected override Grid MainGrid => MainPanel;

        protected void Page_Init(object sender, EventArgs e)
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 获取数据
        /// </summary>
        protected override void LoadData()
        {
            var menus = SYS_MENU.Fetch(Sql.Builder.Where("Active = @0", true));
            //拼接查询的SQL
            var search = ttbFullTextSearch.Text;
            if (!string.IsNullOrEmpty(search))
            {
                menus = menus.Where(m => m.Name == search).ToList();
            }
            if (!string.IsNullOrEmpty(MainPanel.SortField))
            {
                //排序
            }
            MainPanel.RecordCount = menus.Count;
            MainPanel.DataSource = menus;
            MainPanel.DataBind();
        }

        /// <summary>
        ///     获取打开页面地址
        /// </summary>
        /// <param name="action"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        protected override string GetWindowUrl(ACTION action, int rowIndex = 0)
        {
            switch (action)
            {
                case ACTION.INSERT:
                    return $"~/Modules/SYS/Menu/Menu_detail.aspx?action={(int)ACTION.INSERT}";
                case ACTION.UPDATE:
                    return
                        $"~/Modules/SYS/Menu/Menu_detail.aspx?id={GetDataKey(MainGrid, rowIndex, "Id")}&action={(int)ACTION.UPDATE}";
                case ACTION.DETAIL:
                    return
                        $"~/Modules/SYS/Menu/Menu_detail.aspx?id={GetDataKey(MainGrid, rowIndex, "Id")}&action={(int)ACTION.DETAIL}";
                default:
                    break;
            }
            return string.Empty;
        }
    }
}