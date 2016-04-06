using System;
using System.Drawing;
using System.Linq;
using FineUI;
using OutOfMemory;

namespace FineMIS.Pages
{
    public abstract class SingleGridPage : PageBase, ISinglePageBase
    {
        #region 属性

        /// <summary>
        /// 主表格实例
        /// </summary>
        protected abstract Grid MainGrid { get; }

        #endregion

        #region 页面初始化

        protected override void OnInit(EventArgs e)
        {
            InitGrid();
            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                BindGrid();
            }

            InitControls();
        }


        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            if (MainGrid.Toolbars.Count > 0 && MainGrid.Toolbars.First() != null)
            {
                Button btnPostBack = new Button
                {
                    ID = "btnPostBack",
                    Visible = false
                };
                btnPostBack.Click += btnPostBack_Click;
                MainGrid.Toolbars.First().Items.Add(btnPostBack);
            }
        }

        /// <summary>
        ///     设置表格属性
        /// </summary>
        private void InitGrid()
        {
            MainGrid.PageSize = 20;
        }


        #endregion

        #region 页面事件

        #region 1.搜索栏相关

        /// <summary>
        ///     搜索栏清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ttbFullTextSearch_Trigger1Click(object sender, EventArgs e)
        {
            try
            {
                var ttb = sender as TwinTriggerBox;

                if (ttb == null) return;

                ttb.Text = string.Empty;
                ttb.Reset();
                ttb.ShowTrigger1 = false;

                ResetGrid();
            }
            catch (Exception ex)
            {
                Alert.ShowInTop(ex.Message, "刷新失败", MessageBoxIcon.Error);
            }
        }

        /// <summary>
        ///     搜索栏搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ttbFullTextSearch_Trigger2Click(object sender, EventArgs e)
        {
            try
            {
                var ttb = sender as TwinTriggerBox;

                if (ttb == null) return;

                if (string.IsNullOrWhiteSpace(ttb.Text))
                {
                    // 没有输入文字时等同与刷新
                    ResetGrid();
                }
                else
                {
                    // 启用搜索标识
                    ttb.ShowTrigger1 = true;
                    // 从第一页开始显示
                    MainGrid.PageIndex = 0;
                    // 绑定表格
                    BindGrid();
                }
            }
            catch (Exception ex)
            {
                Alert.ShowInTop(ex.Message, "搜索失败", MessageBoxIcon.Error);
            }
        }

        #endregion

        #region 2.Grid相关

        /// <summary>
        ///     表格行内点击事件（编辑/查看等）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grid_RowCommand(object sender, GridCommandEventArgs e)
        {
            ProcessCommand(e.CommandName);
        }

        /// <summary>
        ///     表格排序事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grid_Sort(object sender, GridSortEventArgs e)
        {
            try
            {
                MainGrid.SortDirection = e.SortDirection;
                MainGrid.SortField = e.SortField;

                BindGrid();
            }
            catch (Exception ex)
            {
                Alert.ShowInTop(ex.Message, "刷新失败", MessageBoxIcon.Error);
            }
        }

        /// <summary>
        ///     表格每页显示项数改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var ddl = sender as DropDownList;

                if (ddl == null) return;

                MainGrid.PageSize = ddl.SelectedValue.ToInt32();

                BindGrid();
            }
            catch (Exception ex)
            {
                Alert.ShowInTop(ex.Message, "刷新失败", MessageBoxIcon.Error);
            }
        }

        /// <summary>
        ///     表格分页改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grid_PageIndexChange(object sender, GridPageEventArgs e)
        {
            try
            {
                MainGrid.PageIndex = e.NewPageIndex;

                BindGrid();
            }
            catch (Exception ex)
            {
                Alert.ShowInTop(ex.Message, "刷新失败", MessageBoxIcon.Error);
            }
        }

        #endregion

        #region 3.按钮事件

        /// <summary>
        ///     刷新按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                // 重置表格
                ResetGrid();

                Notify.Show("已刷新数据", null, NotifyIcon.Success);
            }
            catch (Exception ex)
            {
                Alert.ShowInTop(ex.Message, "刷新失败", MessageBoxIcon.Error);
            }
        }

        /// <summary>
        ///     回调按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPostBack_Click(object sender, EventArgs e)
        {
            // 获得回发参数
            var argument = Request.Params["__EVENTARGUMENT"];
            ProcessArgument(argument);
        }

        /// <summary>
        ///     新增按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnInsert_Click(object sender, EventArgs e)
        {
            var page = Master as Page;
            if (page != null)
                PageContext.RegisterStartupScript(page.Window.GetShowReference(
                    GetWindowUrl(ACTION.INSERT),
                    EnumHelper.GetDescName(ACTION.INSERT),
                    GetWindowSize(ACTION.INSERT).Width,
                    GetWindowSize(ACTION.INSERT).Height));
        }

        /// <summary>
        ///     删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnDelete_Click(object sender, EventArgs e)
        {
            // 至少选中一行
            if (MainGrid.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选中一行!", "删除失败", MessageBoxIcon.Error);
            }
            //else if (IsSelectedDataVerified(MainGrid))
            //{
            //    Alert.ShowInTop("不能删除已审核的数据!", "删除失败", MessageBoxIcon.Error);
            //    return;
            //}
            else
            {
                var btnPostBack = FindControlDeep(MainGrid, "btnPostBack") as Button;
                if (btnPostBack != null)
                    MessageBox.Show($"已选中{MainGrid.SelectedRowIndexArray.Length}行,是否确认删除选中数据?", "确认删除",
                        buttons: MessageBoxButtons.OKCANCEL,
                        icon: MessageBoxIcon.Question,
                        okScript: btnPostBack.GetPostBackEventReference(ACTION.DELETE.ToString()));
            }
        }

        /// <summary>
        ///     审核按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnAudit_Click(object sender, EventArgs e)
        {
            if (MainGrid.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选中一行!", "审核失败", MessageBoxIcon.Error);
            }
            else
            {
                var btnPostBack = FindControlDeep(MainGrid, "btnPostBack") as Button;
                if (btnPostBack != null)
                    MessageBox.Show($"已选中{MainGrid.SelectedRowIndexArray.Length}行,审核或拒绝选中数据?", "确认审核",
                        buttons: MessageBoxButtons.YESNOCANCEL,
                        yesText: "审核",
                        noText: "拒绝",
                        icon: MessageBoxIcon.Question,
                        yesScript: btnPostBack.GetPostBackEventReference(ACTION.PERMIT.ToString()),
                        noScript: btnPostBack.GetPostBackEventReference(ACTION.REFUSE.ToString()));
            }
        }

        /// <summary>
        ///     反审按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnUnaudit_Click(object sender, EventArgs e)
        {
            if (MainGrid.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选中一行!", "反审失败", MessageBoxIcon.Error);
            }
            else
            {
                var btnPostBack = FindControlDeep(MainGrid, "btnPostBack") as Button;
                if (btnPostBack != null)
                    MessageBox.Show($"已选中{MainGrid.SelectedRowIndexArray.Length}行,是否确认反审选中数据?", "确认反审",
                        buttons: MessageBoxButtons.OKCANCEL,
                        icon: MessageBoxIcon.Question,
                        okScript: btnPostBack.GetPostBackEventReference(ACTION.UNAUDIT.ToString()));
            }
        }

        /// <summary>
        ///     导入按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnImport_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnExport_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     打印按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnPrint_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 4.窗口事件

        #endregion

        #endregion

        #region 方法

        /// <summary>
        ///     重置Grid
        /// </summary>
        protected void ResetGrid()
        {
            if (MainGrid.AllowPaging)
            {
                // 返回第一页
                MainGrid.PageIndex = 0;
            }

            if (MainGrid.AllowSorting)
            {
                // 重置默认排序
                MainGrid.SortField = "Id";
                MainGrid.SortDirection = "DESC";

                // 清除排序的箭头，重置排序选项
                PageContext.RegisterStartupScript($"Ext.getCmp('{MainGrid.ClientID}').f_setSortIcon();");
            }

            MainGrid.Reset();

            LoadData();
        }

        /// <summary>
        ///     绑定表格
        /// </summary>
        protected void BindGrid()
        {
            // 保持选中状态
            var selectedRowIndex = MainGrid.SelectedRowIndexArray.Length == 0 ? 0 : MainGrid.SelectedRowIndex;

            LoadData();

            // 保持选中状态
            MainGrid.SelectedRowIndex = selectedRowIndex;
        }

        /// <summary>
        ///     获得弹出窗体尺寸
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        protected virtual Size GetWindowSize(ACTION action = ACTION.NONE)
        {
            return new Size(600, 350);
        }

        #endregion

        #region 需要每个页面实现的方法

        /// <summary>
        ///     获得弹出窗体地址
        /// </summary>
        /// <param name="action">操作枚举类型</param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        protected virtual string GetWindowUrl(ACTION action, int rowIndex = 0)
        {
            throw new NotImplementedException();
        }

        protected virtual void LoadData()
        {
            throw new NotImplementedException();
        }

        public virtual void ProcessArgument(string argument)
        {
            if (string.IsNullOrEmpty(argument))
            {
                return;
            }
            // 修改成功
            else if (argument.Contains(ACTION.UPDATE.ToString()))
            {
                Notify.Show("保存成功!", null, NotifyIcon.Success);
                BindGrid();
            }
            // 新增成功
            else if (argument.Contains(ACTION.INSERT.ToString()))
            {
                Notify.Show("保存成功!", null, NotifyIcon.Success);
                BindGrid();
            }
            // 添加成功
            else if (argument.Contains(ACTION.APPEND.ToString()))
            {
                Notify.Show("保存成功!", null, NotifyIcon.Success);
                // 强制刷新
                BindGrid();
            }
            //删除操作
            else if (argument.Contains(ACTION.DELETE.ToString()))
            {
                DeleteData();
                Notify.Show("删除成功!", null, NotifyIcon.Success);
                // 强制刷新
                BindGrid();
            }
            // 表格通过保存并继续添加按钮被编辑过后应启动一个刷新效果
            else if (argument.Contains(FORCE_REFRESH))
            {
                // 强制刷新
                BindGrid();
            }
            else if (Session[FORCE_REFRESH].ToBoolean())
            {
                // 强制刷新
                BindGrid();
            }
        }

        protected virtual void ProcessCommand(string command)
        {
            if (command == ACTION.UPDATE.ToString())
            {
                //TODO:验证
                //if (IsDataVerified(MainGrid, e.RowIndex))
                //{
                //    Alert.ShowInTop("不能编辑已审核的数据!", "编辑失败", MessageBoxIcon.Error);
                //    return;
                //}
                var page = Master as Page;
                if (page != null)
                    PageContext.RegisterStartupScript(page.Window.GetShowReference(
                        GetWindowUrl(ACTION.UPDATE, MainGrid.SelectedRowIndex),
                        EnumHelper.GetDescName(ACTION.UPDATE),
                        GetWindowSize(ACTION.UPDATE).Width,
                        GetWindowSize(ACTION.UPDATE).Height));
            }
            else if (command == ACTION.DETAIL.ToString())
            {
                var page = Master as Page;
                if (page != null)
                    PageContext.RegisterStartupScript(page.Window.GetShowReference(
                        GetWindowUrl(ACTION.DETAIL, MainGrid.SelectedRowIndex),
                        EnumHelper.GetDescName(ACTION.DETAIL),
                        GetWindowSize(ACTION.DETAIL).Width,
                        GetWindowSize(ACTION.DETAIL).Height));
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        protected virtual void DeleteData()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}