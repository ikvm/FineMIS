<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FineMIS.Default" %>

<!doctype html>
<html class="no-js">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="description" content="">
    <meta name="keywords" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">
    <title>FineMIS</title>

    <!-- Set render engine for 360 browser -->
    <meta name="renderer" content="webkit">

    <!-- No Baidu Siteapp-->
    <meta http-equiv="Cache-Control" content="no-siteapp" />

    <link rel="icon" type="image/png" href="res/images/favicon.png">

    <style>
        .topbar {
            background: url(res/images/logo.png) no-repeat 10px;
            background-size: 100px;
        }

        /*.topbar.f-toolbar {
                border-top-color: inherit;
                padding: 3px 12px;
            }*/

        /* menu items */
        div.x-grid-cell-inner.x-grid-cell-inner-treecolumn {
            margin-top: 2px;
            margin-bottom: 2px;
        }

        .bottombar .x-toolbar-text a {
            color: #000;
            text-decoration: none;
        }

        .f-theme-neptune .region-top .x-panel-body,
        .f-theme-neptune .region-top .x-toolbar {
            background-color: #005999 !important;
        }

        .f-theme-neptune .region-top .x-toolbar {
            border: none;
            /*border-color: #014A7E !important;*/
        }

        .f-theme-neptune .region-top span,
        .f-theme-neptune .region-top .x-toolbar-text,
        .f-theme-neptune .region-top a {
            color: #fff;
        }

        .f-theme-neptune .region-top .x-toolbar-separator {
            border-color: #aaa;
        }

        .f-theme-neptune .region-top .x-btn-default-toolbar-small {
            background-color: transparent;
            background-image: none;
            border-width: 0;
        }

        .f-theme-neptune .region-top .x-btn-default-toolbar-small-over,
        .f-theme-neptune .region-top .x-btn-default-toolbar-small-menu-active {
            background-color: #3386c2;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="regionPanel" runat="server" />
        <f:RegionPanel ID="regionPanel" ShowBorder="false" runat="server">
            <Regions>
                <f:Region ID="regionTop" CssClass="region-top" ShowBorder="false" ShowHeader="false" Position="Top"
                    Layout="Fit" runat="server">
                    <Toolbars>
                        <f:Toolbar ID="topRegionToolbar" Position="Bottom" CssClass="topbar" runat="server">
                            <Items>
                                <f:ToolbarFill ID="ToolbarFill" runat="server" />
                                <f:ToolbarText ID="txtUser" runat="server"></f:ToolbarText>
                                <f:Label runat="server" Width="4px" Text=""></f:Label>
                                <f:Button ID="btnRefresh" runat="server" Icon="Reload" Text="刷新" ToolTip="刷新主区域内容" EnablePostBack="false"></f:Button>
                                <f:Button ID="btnHelp" EnablePostBack="false" Icon="Help" Text="帮助" runat="server"></f:Button>
                                <f:Button ID="btnExit" runat="server" Icon="UserRed" Text="退出" ConfirmText="确定退出系统?" OnClick="btnExit_Click"></f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                </f:Region>
                <f:Region ID="regionLeft" EnableCollapse="false" RegionSplit="false" Margin="0 5 0 0" Width="200px" ShowHeader="true" Title="菜单" Layout="Fit" Position="Left" runat="server">
                </f:Region>
                <f:Region ID="regionBottom" Position="Bottom" Layout="Fit" ShowHeader="false" Margin="5 0 0 0" runat="server">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" Position="Bottom" CssClass="bottombar" runat="server">
                            <Items>
                                <f:ToolbarText runat="server" ID="StatusInfo" Text="就绪"></f:ToolbarText>
                                <f:ToolbarText runat="server" ID="ThreadInfo" Text=""></f:ToolbarText>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server" />
                                <f:ToolbarText runat="server" Text="Copyright &copy; 2011 - 2015 <a href='http://www.eheng.net.cn' target='_blank'>湖北鄂恒科技有限公司</a>"></f:ToolbarText>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                </f:Region>
                <f:Region ID="mainRegion" ShowHeader="false" Layout="Fit" Position="Center"
                    runat="server">
                    <Items>
                        <f:TabStrip ID="mainTabStrip" EnableTabCloseMenu="true" ShowBorder="false" runat="server" AutoPostBack="true">
                            <Tabs>
                                <f:Tab ID="Tab1" Title="首页" EnableIFrame="true" IFrameUrl="http://www.eheng.net.cn" Icon="House" runat="server"></f:Tab>
                            </Tabs>
                        </f:TabStrip>
                    </Items>
                </f:Region>
            </Regions>
        </f:RegionPanel>
        <f:Window ID="Window1" runat="server" IsModal="true" Hidden="true" EnableIFrame="true"
            EnableResize="true" EnableMaximize="true" IFrameUrl="about:blank" Width="800px"
            Height="500px">
        </f:Window>
        <a id="toggleheader" href="javascript:;"></a>
    </form>
    <script>
        F.ready(function () {
            var treeMenu = F(DATA.treeMenu),
                regionPanel = F(DATA.regionPanel),
                regionTop = F(DATA.regionTop),
                mainTabStrip = F(DATA.mainTabStrip),
                txtUser = F(DATA.txtUser),
                btnRefresh = F(DATA.btnRefresh);

            // 欢迎信息和用户信息
            txtUser.setText('<span>' + DATA.userName + '</span><span class="label">, 你好!</span>');

            // 点击刷新按钮
            btnRefresh.on('click', function () {
                var iframe = Ext.DomQuery.selectNode('iframe', mainTabStrip.getActiveTab().body.dom);
                iframe.contentWindow.location.reload(false);
            });

            // 初始化主框架中的树(或者Accordion+Tree)和选项卡互动，以及地址栏的更新
            // treeMenu： 主框架中的树控件实例，或者内嵌树控件的手风琴控件实例
            // mainTabStrip： 选项卡实例
            // addTabCallback： 创建选项卡前的回调函数（接受tabConfig参数）
            // updateLocationHash: 切换Tab时，是否更新地址栏Hash值
            // refreshWhenExist： 添加选项卡时，如果选项卡已经存在，是否刷新内部IFrame
            // refreshWhenTabChange: 切换选项卡时，是否刷新内部IFrame
            F.util.initTreeTabStrip(treeMenu, mainTabStrip, null, true, false, false);

            // 公开添加示例标签页的方法
            window.addExampleTab = function (id, url, text, icon, refreshWhenExist) {
                // 动态添加一个标签页
                // mainTabStrip： 选项卡实例
                // id： 选项卡ID
                // url: 选项卡IFrame地址 
                // text： 选项卡标题
                // icon： 选项卡图标
                // addTabCallback： 创建选项卡前的回调函数（接受tabConfig参数）
                // refreshWhenExist： 添加选项卡时，如果选项卡已经存在，是否刷新内部IFrame
                F.util.addMainTab(mainTabStrip, id, url, text, icon, null, refreshWhenExist);

                // todo...
                // 当点击切换Tab页时增加一个IFrame内事件
                // 注意这里只会在IFrame页面已经加载到Tab列表中并重新通过addMainTab方法加载时才会有效
                // 页面之间的切换并不会触发这个事件
                // 未加载过的页面即新建的页面也不会触发这个事件，但是新建页面时会触发页面自己的Page_Load事件
                // 因此需要在两个不同的地方进行处理
                // 同时，在所有需要触发事件的IFrame页面中必须定义一个名为onTabReload的方法
                // 可以利用母板页或者PageBase来统一增加该脚本
                //var activeTab = mainTabStrip.getActiveTab();
                //if (activeTab) {
                //    var iframe = activeTab.body.query('iframe')[0];
                //    if (iframe && iframe.contentWindow.onTabReload) {
                //        iframe.contentWindow.onTabReload();
                //    }
                //}
            };

            window.removeActiveTab = function () {
                var activeTab = mainTabStrip.getActiveTab();
                mainTabStrip.removeTab(activeTab.id);
            };
        });
    </script>
</body>
</html>
