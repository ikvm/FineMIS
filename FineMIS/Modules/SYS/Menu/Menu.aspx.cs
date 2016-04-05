﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FineMIS.Pages;
using FineUI;
using PetaPoco;

namespace FineMIS.Modules.SYS.Menu
{
    public partial class Menu : SingleGridPage
    {
        protected override Grid MainGrid => Grid1;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///     获取数据
        /// </summary>
        protected override void LoadData()
        {
            var menus = SYS_MENU.Menus; //未缓存的数据查询时需要拼接SQL
            //拼接查询的SQL
            var search = ttbFullTextSearch.Text;
            if (!string.IsNullOrEmpty(search))
            {
                menus = menus.Where(m => m.Name == search).ToList();
            }
            if (!string.IsNullOrEmpty(Grid1.SortField))
            {
                //排序
            }
            Grid1.RecordCount = menus.Count;
            Grid1.DataSource = menus;
            Grid1.DataBind();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        protected override void DeleteData()
        {
            IEnumerable<object> selectIds = GetSelectedIds(MainGrid);
            SYS_MENU.Delete(Sql.Builder.Where("Id IN(@0)", selectIds),true);
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