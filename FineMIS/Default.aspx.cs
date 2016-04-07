using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;
using FineUI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FineMIS
{
    public partial class Default : System.Web.UI.Page
    {
        #region Page_Init

        protected void Page_Init(object sender, EventArgs e)
        {
            // 用户可见的菜单列表
            var menus = SYS_MENU_Helper.Menus;
            //if (menus.Count == 0)
            //{
            //    // 清除cookie
            //    Security.SignOut();

            //    // 返回未授权页面
            //    Response.Redirect("~/Error/401.html");

            //    return;
            //}

            // 注册客户端脚本，服务器端控件ID和客户端ID的映射关系
            var ids = GetClientIds(regionPanel, regionTop, mainTabStrip, txtUser, btnRefresh);

            ids.Add("userName", Current.UserName);

            if (Settings.MenuStyle == "accordion")
            {
                var accordionMenu = InitAccordionMenu(menus);
                ids.Add("treeMenu", accordionMenu.ClientID);
                ids.Add("menuType", "accordion");
            }
            else
            {
                var treeMenu = InitTreeMenu(menus);
                ids.Add("treeMenu", treeMenu.ClientID);
                ids.Add("menuType", "menu");
            }

            var idsScriptStr = $"window.DATA={ids.ToString(Formatting.None)};";
            PageContext.RegisterStartupScript(idsScriptStr);

            Title = Settings.Title;
        }

        private JObject GetClientIds(params ControlBase[] ctrls)
        {
            var jo = new JObject();
            foreach (var ctrl in ctrls)
            {
                jo.Add(ctrl.ID, ctrl.ClientID);
            }

            return jo;
        }

        //protected override void OnPreRender(EventArgs e)
        //{
        //    base.OnPreRender(e);
        //    // 显示MiniProfiler
        //    Response.Write(
        //        StackExchange.Profiling.MiniProfiler.RenderIncludes(StackExchange.Profiling.RenderPosition.BottomRight));
        //}

        #endregion

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            ThreadInfo.Text = $"RoleIds: {Current.RoleIdsString}, Thread Id: {Thread.CurrentThread.ManagedThreadId}, Session Id: {Session.SessionID}";
        }

        #endregion

        #region InitAccordionMenu

        /// <summary>
        /// 创建手风琴菜单
        /// </summary>
        /// <param name="menus"></param>
        /// <returns></returns>
        private Accordion InitAccordionMenu(List<SYS_MENU> menus)
        {
            var accordionMenu = new Accordion
            {
                ID = "accordionMenu",
                EnableFill = true,
                ShowBorder = false,
                ShowHeader = false
            };

            regionLeft.Items.Add(accordionMenu);

            foreach (var menu in menus.Where(m => m.ParentId == 0))
            {
                var accordionPane = new AccordionPane
                {
                    Title = menu.Name,
                    Layout = Layout.Fit,
                    ShowBorder = false,
                    BodyPadding = "0 0 0 0"
                };

                var innerTree = new Tree
                {
                    EnableArrows = true,
                    ShowBorder = false,
                    ShowHeader = false,
                    EnableIcons = true,
                    AutoScroll = true
                };

                // 生成树
                var nodeCount = ResolveMenuTree(menus, menu, innerTree.Nodes);
                if (nodeCount > 0)
                {
                    accordionPane.Items.Add(innerTree);
                    accordionMenu.Items.Add(accordionPane);
                }
            }

            return accordionMenu;
        }

        #endregion

        #region InitTreeMenu

        /// <summary>
        /// 创建树菜单
        /// </summary>
        /// <param name="menus"></param>
        /// <returns></returns>
        private Tree InitTreeMenu(List<SYS_MENU> menus)
        {
            var treeMenu = new Tree
            {
                ID = "treeMenu",
                EnableArrows = true,
                ShowBorder = false,
                ShowHeader = false,
                EnableIcons = true,
                AutoScroll = true
            };

            regionLeft.Items.Add(treeMenu);

            // 生成树
            ResolveMenuTree(menus, null, treeMenu.Nodes);

            // 展开第一个树节点
            treeMenu.Nodes[0].Expanded = true;

            return treeMenu;
        }

        /// <summary>
        /// 生成菜单树
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="parentMenu"></param>
        /// <param name="nodes"></param>
        private int ResolveMenuTree(List<SYS_MENU> menus, SYS_MENU parentMenu, TreeNodeCollection nodes)
        {
            var count = 0;
            foreach (var menu in menus.Where(m => m.ParentId == (parentMenu?.Id ?? 0)))
            {
                var node = new TreeNode();
                nodes.Add(node);
                count++;

                node.Text = menu.Name;
                node.IconUrl = menu.ImageUrl;
                if (!string.IsNullOrEmpty(menu.NavigateUrl))
                {
                    node.EnableClickEvent = false;
                    node.NavigateUrl = ResolveUrl(menu.NavigateUrl);
                    //node.OnClientClick = String.Format("addTab('{0}','{1}','{2}')", node.NodeID, ResolveUrl(menu.NavigateUrl), node.Text.Replace("'", ""));
                }

                if (menu.ParentId > 0)
                {
                    node.Leaf = true;

                    // 如果是叶子节点，但是不是超链接，则是空目录，删除
                    if (string.IsNullOrEmpty(menu.NavigateUrl))
                    {
                        nodes.Remove(node);
                        count--;
                    }
                }
                else
                {
                    //node.SingleClickExpand = true;

                    int childCount = ResolveMenuTree(menus, menu, node.Nodes);

                    // 如果是目录，但是计算的子节点数为0，可能目录里面的都是空目录，则要删除此父目录
                    if (childCount == 0 && string.IsNullOrEmpty(menu.NavigateUrl))
                    {
                        nodes.Remove(node);
                        count--;
                    }
                }
            }

            return count;
        }

        #endregion

        #region SignOut

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Security.SignOut();

            Response.Redirect(FormsAuthentication.LoginUrl);
        }

        #endregion
    }
}