using System;
using System.Collections.Generic;
using System.Linq;
using FineUI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Menu = FineMIS.SYS_MENU;

namespace FineMIS
{
    public partial class Default : System.Web.UI.Page
    {
        #region Page_Init

        protected void Page_Init(object sender, EventArgs e)
        {
            // 用户可见的菜单列表
            var menus = ResolveUserMenuList();
            if (menus.Count == 0)
            {
                // todo
                // 返回未授权页面
                Response.Redirect("401.aspx");
                Response.Write("系统管理员尚未给你配置菜单！");
                Response.End();

                return;
            }

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

        #endregion

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #endregion

        #region InitAccordionMenu

        /// <summary>
        /// 创建手风琴菜单
        /// </summary>
        /// <param name="menus"></param>
        /// <returns></returns>
        private Accordion InitAccordionMenu(List<Menu> menus)
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
        private Tree InitTreeMenu(List<Menu> menus)
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
        private int ResolveMenuTree(List<Menu> menus, Menu parentMenu, TreeNodeCollection nodes)
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

        #region ResolveUserMenuList

        // 获取用户可用的菜单列表
        private List<Menu> ResolveUserMenuList()
        {
            // 当前登陆用户的权限列表
            var roles = Current.Roles.Split(',').ToList();

            // 当前用户所属角色可用的菜单列表
            var menus = new List<Menu>();

            foreach (var menu in Menu.Menus)
            {
                // 如果此菜单不属于任何模块
                if (string.IsNullOrEmpty(menu.Roles))
                {
                    menus.Add(menu);
                }
                // 或者此用户所属角色拥有对此模块的权限
                else if (menu.Roles.Split(',').Any(role => roles.Contains(role)))
                {
                    menus.Add(menu);
                }
            }

            return menus;
        }

        #endregion
    }
}