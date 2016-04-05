using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using FineMIS.Controls;
using FineUI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PetaPoco;

namespace FineMIS.Pages
{
    public class PageBase : System.Web.UI.Page
    {
        #region 页面初始化

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // 此用户是否有访问此页面的权限
            if (!CheckPowerView())
            {
                CheckPowerFailWithPage();
                return;
            }

            // 设置主题
            if (PageManager.Instance != null)
            {
                PageManager.Instance.Theme = (Theme)Enum.Parse(typeof(Theme), Settings.Theme, true);
            }

            // 设置页面标题
            Page.Title = Settings.Title;
        }

        #endregion

        #region 产品版本

        public string GetProductVersion()
        {
            var v = Assembly.GetExecutingAssembly().GetName().Version;
            return $"{v.Major}.{v.Minor}.{v.Build}";
        }

        #endregion

        #region 只读静态变量

        private static readonly string CHECK_POWER_FAIL_PAGE_MESSAGE = "您无权访问此页面！";
        private static readonly string CHECK_POWER_FAIL_ACTION_MESSAGE = "您无权进行此操作！";

        public static readonly string FORCE_REFRESH = "FORCE_REFRESH";
        public static readonly string DEFAULT_ORDER_BY = "Id DESC";

        #endregion

        #region 请求参数

        /// <summary>
        ///     获取查询字符串中的参数值
        /// </summary>
        protected string GetQueryValue(string queryKey)
        {
            return Request.QueryString[queryKey];
        }

        /// <summary>
        ///     获取查询字符串中的参数值
        /// </summary>
        protected int GetQueryIntValue(string queryKey)
        {
            var queryIntValue = -1;
            try
            {
                queryIntValue = Convert.ToInt32(Request.QueryString[queryKey]);
            }
            catch (Exception)
            {
                // TODO
            }

            return queryIntValue;
        }

        #endregion

        #region 当前登录用户信息

        // http://blog.163.com/zjlovety@126/blog/static/224186242010070024282/
        // http://www.cnblogs.com/gaoshuai/articles/1863231.html
        /// <summary>
        ///     当前登录用户的角色列表
        /// </summary>
        /// <returns></returns>
        protected List<int> GetIdentityRoleIDs()
        {
            var roleIDs = new List<int>();

            if (!User.Identity.IsAuthenticated) return roleIDs;
            var ticket = ((FormsIdentity)User.Identity).Ticket;
            var userData = ticket.UserData;

            roleIDs.AddRange(from roleId in userData.Split(',')
                             where !string.IsNullOrEmpty(roleId)
                             select Convert.ToInt32(roleId));

            return roleIDs;
        }

        /// <summary>
        ///     当前登录用户名
        /// </summary>
        /// <returns></returns>
        protected string GetIdentityName()
        {
            return User.Identity.IsAuthenticated ? User.Identity.Name : string.Empty;
        }

        /// <summary>
        ///     创建表单验证的票证并存储在客户端Cookie中
        /// </summary>
        /// <param name="userName">当前登录用户名</param>
        /// <param name="roleIDs">当前登录用户的角色ID列表</param>
        /// <param name="isPersistent">是否跨浏览器会话保存票证</param>
        /// <param name="expiration">过期时间</param>
        protected void CreateFormsAuthenticationTicket(string userName, string roleIDs, bool isPersistent,
            DateTime expiration)
        {
            // 创建Forms身份验证票据
            var ticket = new FormsAuthenticationTicket(1,
                userName, // 与票证关联的用户名
                DateTime.Now, // 票证发出时间
                expiration, // 票证过期时间
                isPersistent, // 如果票证将存储在持久性 Cookie 中（跨浏览器会话保存），则为 true；否则为 false。
                roleIDs // 存储在票证中的用户特定的数据
                );

            // 对Forms身份验证票据进行加密，然后保存到客户端Cookie中
            var hashTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hashTicket)
            {
                HttpOnly = true,

                // 1. 关闭浏览器即删除（Session Cookie）：DateTime.MinValue
                // 2. 指定时间后删除：大于 DateTime.Now 的某个值
                // 3. 删除Cookie：小于 DateTime.Now 的某个值
                Expires = isPersistent ? expiration : DateTime.MinValue
            };

            Response.Cookies.Add(cookie);
        }

        #endregion

        #region 权限检查

        /// <summary>
        ///     检查当前用户是否拥有当前页面的浏览权限
        ///     页面需要先定义ViewPower属性，以确定页面与某个浏览权限的对应关系
        /// </summary>
        /// <returns></returns>
        protected bool CheckPowerView()
        {
            return true;
        }

        /// <summary>
        ///     检查当前用户是否拥有某个权限
        /// </summary>
        /// <returns></returns>
        protected bool CheckPower(string powerName)
        {
            // 如果权限名为空，则放行
            if (string.IsNullOrEmpty(powerName))
            {
                return true;
            }

            // 当前登陆用户的权限列表
            var rolePowerNames = GetRolePowerNames();
            if (rolePowerNames.Contains(powerName))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     获取当前登录用户拥有的全部权限列表
        /// </summary>
        /// <returns></returns>
        protected List<string> GetRolePowerNames()
        {
            //// 将用户拥有的权限列表保存在Session中，这样就避免每个请求多次查询数据库
            //if (Session["UserPowerList"] == null)
            //{
            //    List<string> rolePowerNames = new List<string>();

            //    // 超级管理员拥有所有权限
            //    if (GetIdentityName() == "admin")
            //    {
            //        rolePowerNames = DB.Powers.Select(p => p.Name).ToList();
            //    }
            //    else
            //    {
            //        List<int> roleIDs = GetIdentityRoleIDs();

            //        foreach (var role in DB.Roles.Include(r => r.Powers).Where(r => roleIDs.Contains(r.ID)))
            //        {
            //            foreach (var power in role.Powers)
            //            {
            //                if (!rolePowerNames.Contains(power.Name))
            //                {
            //                    rolePowerNames.Add(power.Name);
            //                }
            //            }
            //        }
            //    }

            //    Session["UserPowerList"] = rolePowerNames;
            //}
            //return (List<string>)Session["UserPowerList"];

            return null;
        }

        #endregion

        #region 权限相关

        protected void CheckPowerFailWithPage()
        {
            Response.Write(CHECK_POWER_FAIL_PAGE_MESSAGE);
            Response.End();
        }

        protected void CheckPowerFailWithButton(Button btn)
        {
            btn.Enabled = false;
            btn.ToolTip = CHECK_POWER_FAIL_ACTION_MESSAGE;
        }

        protected void CheckPowerFailWithLinkButtonField(Grid grid, string columnId)
        {
            var btn = grid.FindColumn(columnId) as LinkButtonField;
            if (btn == null) return;
            btn.Enabled = false;
            btn.ToolTip = CHECK_POWER_FAIL_ACTION_MESSAGE;
        }

        protected void CheckPowerFailWithWindowField(Grid grid, string columnId)
        {
            var btn = grid.FindColumn(columnId) as WindowField;
            if (btn == null) return;
            btn.Enabled = false;
            btn.ToolTip = CHECK_POWER_FAIL_ACTION_MESSAGE;
        }

        protected void CheckPowerFailWithAlert()
        {
            PageContext.RegisterStartupScript(Alert.GetShowInTopReference(CHECK_POWER_FAIL_ACTION_MESSAGE));
        }

        protected void CheckPowerWithButton(string powerName, Button btn)
        {
            if (!CheckPower(powerName))
            {
                CheckPowerFailWithButton(btn);
            }
        }

        protected void CheckPowerWithLinkButtonField(string powerName, Grid grid, string columnId)
        {
            if (!CheckPower(powerName))
            {
                CheckPowerFailWithLinkButtonField(grid, columnId);
            }
        }

        protected void CheckPowerWithWindowField(string powerName, Grid grid, string columnId)
        {
            if (!CheckPower(powerName))
            {
                CheckPowerFailWithWindowField(grid, columnId);
            }
        }

        /// <summary>
        ///     为删除Grid中选中项的按钮添加提示信息
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="grid"></param>
        protected void ResolveDeleteButtonForGrid(Button btn, Grid grid)
        {
            ResolveDeleteButtonForGrid(btn, grid, "确定要删除选中的{0}项记录吗？");
        }

        protected void ResolveDeleteButtonForGrid(Button btn, Grid grid, string confirmTemplate)
        {
            ResolveDeleteButtonForGrid(btn, grid, "请至少应该选择一项记录！", confirmTemplate);
        }

        protected void ResolveDeleteButtonForGrid(Button btn, Grid grid, string noSelectionMessage,
            string confirmTemplate)
        {
            // 点击删除按钮时，至少选中一项
            btn.OnClientClick = grid.GetNoSelectionAlertInParentReference(noSelectionMessage);
            btn.ConfirmText = string.Format(confirmTemplate,
                "&nbsp;<span class=\"highlight\"><script>" + grid.GetSelectedCountReference() + "</script></span>&nbsp;");
            btn.ConfirmTarget = Target.Top;
        }

        #endregion

        #region 隐藏字段相关

        /// <summary>
        ///     从隐藏字段中获取选择的全部ID列表
        /// </summary>
        /// <param name="hfSelectedIds"></param>
        /// <returns></returns>
        public List<int> GetSelectedIDsFromHiddenField(HiddenField hfSelectedIds)
        {
            var currentIds = hfSelectedIds.Text.Trim();
            var idsArray = !string.IsNullOrEmpty(currentIds) ? JArray.Parse(currentIds) : new JArray();
            return new List<int>(idsArray.ToObject<int[]>());
        }

        /// <summary>
        ///     跨页保持选中项 - 将表格当前页面选中行对应的数据同步到隐藏字段中
        /// </summary>
        /// <param name="hfSelectedIds"></param>
        /// <param name="grid"></param>
        public void SyncSelectedRowIndexArrayToHiddenField(HiddenField hfSelectedIds, Grid grid)
        {
            var ids = GetSelectedIDsFromHiddenField(hfSelectedIds);

            var selectedRows = new List<int>();
            if (grid.SelectedRowIndexArray != null && grid.SelectedRowIndexArray.Length > 0)
            {
                selectedRows = new List<int>(grid.SelectedRowIndexArray);
            }

            if (grid.IsDatabasePaging)
            {
                for (int i = 0, count = Math.Min(grid.PageSize, grid.RecordCount - grid.PageIndex * grid.PageSize);
                    i < count;
                    i++)
                {
                    var id = Convert.ToInt32(grid.DataKeys[i][0]);
                    if (selectedRows.Contains(i))
                    {
                        if (!ids.Contains(id))
                        {
                            ids.Add(id);
                        }
                    }
                    else
                    {
                        if (ids.Contains(id))
                        {
                            ids.Remove(id);
                        }
                    }
                }
            }
            else
            {
                var startPageIndex = grid.PageIndex * grid.PageSize;
                for (int i = startPageIndex, count = Math.Min(startPageIndex + grid.PageSize, grid.RecordCount);
                    i < count;
                    i++)
                {
                    var id = Convert.ToInt32(grid.DataKeys[i][0]);
                    if (selectedRows.Contains(i - startPageIndex))
                    {
                        if (!ids.Contains(id))
                        {
                            ids.Add(id);
                        }
                    }
                    else
                    {
                        if (ids.Contains(id))
                        {
                            ids.Remove(id);
                        }
                    }
                }
            }

            hfSelectedIds.Text = new JArray(ids).ToString(Formatting.None);
        }

        /// <summary>
        ///     跨页保持选中项 - 根据隐藏字段的数据更新表格当前页面的选中行
        /// </summary>
        /// <param name="hfSelectedIds"></param>
        /// <param name="grid"></param>
        public void UpdateSelectedRowIndexArray(HiddenField hfSelectedIds, Grid grid)
        {
            var ids = GetSelectedIDsFromHiddenField(hfSelectedIds);

            var nextSelectedRowIndexArray = new List<int>();
            if (grid.IsDatabasePaging)
            {
                for (int i = 0, count = Math.Min(grid.PageSize, grid.RecordCount - grid.PageIndex * grid.PageSize);
                    i < count;
                    i++)
                {
                    var id = Convert.ToInt32(grid.DataKeys[i][0]);
                    if (ids.Contains(id))
                    {
                        nextSelectedRowIndexArray.Add(i);
                    }
                }
            }
            else
            {
                var nextStartPageIndex = grid.PageIndex * grid.PageSize;
                for (int i = nextStartPageIndex, count = Math.Min(nextStartPageIndex + grid.PageSize, grid.RecordCount);
                    i < count;
                    i++)
                {
                    var id = Convert.ToInt32(grid.DataKeys[i][0]);
                    if (ids.Contains(id))
                    {
                        nextSelectedRowIndexArray.Add(i - nextStartPageIndex);
                    }
                }
            }
            grid.SelectedRowIndexArray = nextSelectedRowIndexArray.ToArray();
        }

        #endregion

        #region 模拟树的下拉列表

        protected List<T> ResolveDdl<T>(List<T> mys) where T : class, ITree, ICloneable, IKeyId, new()
        {
            return ResolveDdl(mys, -1, true);
        }

        protected List<T> ResolveDdl<T>(List<T> mys, int currentId) where T : class, ITree, ICloneable, IKeyId, new()
        {
            return ResolveDdl(mys, currentId, true);
        }

        // 将一个树型结构放在一个下列列表中可供选择
        protected List<T> ResolveDdl<T>(List<T> source, int currentId, bool addRootNode)
            where T : class, ITree, ICloneable, IKeyId, new()
        {
            var result = new List<T>();

            if (addRootNode)
            {
                // 添加根节点
                var root = new T
                {
                    Name = "--根节点--",
                    Id = -1,
                    TreeLevel = 0,
                    Enabled = true
                };
                result.Add(root);
            }

            foreach (var item in source)
            {
                var newT = (T)item.Clone();
                result.Add(newT);

                // 所有节点的TreeLevel加一
                if (addRootNode)
                {
                    newT.TreeLevel++;
                }
            }

            // currentId==-1表示当前节点不存在
            if (currentId != -1)
            {
                // 本节点不可点击（也就是说当前节点不可能是当前节点的父节点）
                // 并且本节点的所有子节点也不可点击，你想如果当前节点跑到子节点的子节点，那么这些子节点就从树上消失了
                var startChileNode = false;
                var startTreeLevel = 0;
                foreach (var my in result)
                {
                    if (my.Id == currentId)
                    {
                        startTreeLevel = my.TreeLevel;
                        my.Enabled = false;
                        startChileNode = true;
                    }
                    else
                    {
                        if (startChileNode)
                        {
                            if (my.TreeLevel > startTreeLevel)
                            {
                                my.Enabled = false;
                            }
                            else
                            {
                                startChileNode = false;
                            }
                        }
                    }
                }
            }

            return result;
        }

        #endregion

        #region 日志记录

        #endregion

        #region 表格相关

        /// <summary>
        ///     根据列序号获得指定行的DataKey
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="rowIndex"></param>
        /// <param name="dataIndex"></param>
        /// <returns></returns>
        protected object GetDataKey(Grid grid, int rowIndex, int dataIndex)
        {
            try
            {
                return grid.DataKeys[rowIndex][dataIndex];
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        ///     根据名称获得指定行的DataKey
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="rowIndex"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        protected object GetDataKey(Grid grid, int rowIndex, string name)
        {
            try
            {
                for (var i = 0; i < grid.DataKeyNames.Length; i++)
                {
                    if (grid.DataKeyNames[i] == name) return grid.DataKeys[rowIndex][i];
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        ///     获取当前选中行（一行）的Id
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        protected long GetSelectedId(Grid grid)
        {
            return Convert.ToInt64(GetDataKey(grid, grid.SelectedRowIndex, "Id"));
        }

        /// <summary>
        ///     获取当前选中行（一行）的指定datakey
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="dataIndex"></param>
        /// <returns></returns>
        protected object GetSelectedDataKey(Grid grid, int dataIndex)
        {
            return GetDataKey(grid, grid.SelectedRowIndex, dataIndex);
        }

        /// <summary>
        ///     获取当前选中行（一行）的指定datakey
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        protected object GetSelectedDataKey(Grid grid, string name)
        {
            return GetDataKey(grid, grid.SelectedRowIndex, name);
        }

        /// <summary>
        ///     获取当前选中行的Id列表
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        protected IEnumerable<object> GetSelectedIds(Grid grid)
        {
            return grid.SelectedRowIndexArray.Select(rowIndex => GetDataKey(grid, rowIndex, "Id"));
        }

        /// <summary>
        ///     获取当前选中行的指定datakey列表
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="dataIndex"></param>
        /// <returns></returns>
        protected IEnumerable<object> GetSelectedDataKeys(Grid grid, int dataIndex)
        {
            return grid.SelectedRowIndexArray.Select(rowIndex => GetDataKey(grid, rowIndex, dataIndex));
        }

        /// <summary>
        ///     获取当前选中行的指定datakey列表
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        protected IEnumerable<object> GetSelectedDataKeys(Grid grid, string name)
        {
            return grid.SelectedRowIndexArray.Select(rowIndex => GetDataKey(grid, rowIndex, name));
        }

        /// <summary>
        ///     根据类型获得指定格式的DataKeyNames
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<string> GetDataKeyNames<T>()
        {
            return from p in typeof(T).GetProperties()
                   where p.GetCustomAttributes(typeof(ColumnAttribute)).Any()
                   select p.Name;
        }

        /// <summary>
        ///     根据类型设置指定Grid的DataKeyNames
        /// </summary>
        /// <param name="grid"></param>
        protected void SetDataKeyNames<T>(Grid grid)
        {
            grid.DataKeyNames = GetDataKeyNames<T>().ToArray();
        }

        ///// <summary>
        ///// 选中行是否被审核
        ///// </summary>
        ///// <param name="grid"></param>
        ///// <returns></returns>
        //protected bool IsSelectedDataVerified(Grid grid)
        //{
        //    return grid.SelectedRowIndexArray.Any(rowIndex => Convert.ToInt32(Verified.Permit) == Convert.ToInt32(GetDataKey(grid, rowIndex, "Verified")));
        //}

        ///// <summary>
        ///// 行是否被审核
        ///// </summary>
        ///// <param name="grid"></param>
        ///// <param name="rowIndex"></param>
        /// <returns></returns>
        /// <summary>
        ///     清空所有表单字段内容
        /// </summary>
        /// <param name="form"></param>
        /// <param name="ignoreFields"></param>
        public virtual void ResetControls(Form form, string[] ignoreFields = null)
        {
            //设置默认值
            if (ignoreFields == null)
            {
                ignoreFields = new[] { "Name" };
            }

            foreach (var c in FindControlsDeep(form))
            {
                try
                {
                    var field = c as TextField;
                    if (field != null && !ignoreFields.Contains(field.ID))
                    {
                        (c as TextField).Reset();
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }

        /// <summary>
        ///     将表单所有字段置为只读
        /// </summary>
        /// <param name="form"></param>
        public void SetReadOnlyControls(Form form)
        {
            foreach (var c in FindControlsDeep(form))
            {
                try
                {
                    c.GetType().GetProperty("ShowRedStar").SetValue(c, false);
                }
                catch (Exception)
                {
                    // ignored
                }
                try
                {
                    c.GetType().GetProperty("Required").SetValue(c, false);
                }
                catch (Exception)
                {
                    // ignored
                }
                try
                {
                    c.GetType().GetProperty("Readonly").SetValue(c, true);
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }

        #endregion

        #region 控件相关

        /// <summary>
        ///     查找所有子控件(所有层级)
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public List<Control> FindControlsDeep(Control control)
        {
            var controls = new List<Control>();

            foreach (Control c in control.Controls)
            {
                controls.Add(c);
                controls.AddRange(FindControlsDeep(c));
            }

            return controls;
        }

        /// <summary>
        ///     查找唯一子控件(所有层级)
        /// </summary>
        /// <param name="control"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Control FindControlDeep(Control control, string id)
        {
            try
            {
                return FindControlsDeep(control).SingleOrDefault(c => c.ID == id);
            }
            catch (Exception)
            {
                // 没找到或ID不唯一
                return null;
            }
        }

        /// <summary>
        ///     表单控件只读
        /// </summary>
        /// <param name="form"></param>
        public void ReadOnlyForm(Form form)
        {
            foreach (var c in FindControlsDeep(form))
            {
                try
                {
                    c.GetType().GetProperty("Readonly").SetValue(c, true);
                    c.GetType().GetProperty("Required").SetValue(c, false);
                }
                catch (Exception)
                {
                }
            }
        }

        //重置除Code外所有表单字段
        public virtual void ResetControl(Control control, string[] ignoreFields = null)
        {
            foreach (var c in FindControlsDeep(control))
            {
                try
                {
                    if (c is TextBox)
                    {
                        if ((ignoreFields != null && !ignoreFields.Contains(c.ID)) || ignoreFields == null)
                        {
                            (c as TextBox).Reset();
                        }
                    }
                    else if (c is TextArea)
                    {
                        if ((ignoreFields != null && !ignoreFields.Contains(c.ID)) || ignoreFields == null)
                        {
                            (c as TextArea).Reset();
                        }

                    }
                    else if (c is NumberBox)
                    {
                        if ((ignoreFields != null && !ignoreFields.Contains(c.ID)) || ignoreFields == null)
                        {
                            (c as NumberBox).Reset();
                        }
                    }
                    else if (c is DatePicker)
                    {
                        if ((ignoreFields != null && !ignoreFields.Contains(c.ID)) || ignoreFields == null)
                        {
                            (c as DatePicker).Reset();
                        }
                    }
                    else if (c is DropDownList)
                    {
                        if ((ignoreFields != null && !ignoreFields.Contains(c.ID)) || ignoreFields == null)
                        {
                            (c as DropDownList).Reset();
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        #endregion
    }
}