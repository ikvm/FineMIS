<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Page.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="FineMIS.Modules.SYS.Menu.Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
        EnableCheckBoxSelect="true"
        DataKeyNames="Id,UserName" AllowSorting="true" OnSort="grid_Sort" SortField="Name"
        SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true"
        OnRowCommand="grid_RowCommand" OnPageIndexChange="grid_PageIndexChange">
        <Toolbars>
            <f:Toolbar runat="server">
                <Items>
                    <f:Button ID="btnRefresh" Icon="ArrowRefresh" runat="server" Text="刷新" OnClick="btnRefresh_Click">
                    </f:Button>
                    <f:Button ID="btnInsert" Icon="Add" runat="server" Text="新增" OnClick="btnInsert_Click">
                    </f:Button>
                    <f:Button ID="btnDelete" Icon="Delete" runat="server" Text="删除" OnClick="btnDelete_Click">
                    </f:Button>
                    <f:TwinTriggerBox ID="ttbFullTextSearch" runat="server" EmptyText="请输入菜单名称" Trigger1Icon="Clear" Trigger2Icon="Search" Width="300">
                    </f:TwinTriggerBox>
                    <%--其它按钮--%>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Columns>
            <f:LinkButtonField ColumnID="detailField" CommandName="DETAIL" TextAlign="Center" Icon="Information" ToolTip="查看" HeaderText="查看" Width="50px" />
            <f:LinkButtonField ColumnID="editField" CommandName="UPDATE" TextAlign="Center" Icon="Pencil" ToolTip="编辑" HeaderText="编辑" Width="50px" />
            <f:RowNumberField runat="server" HeaderText="序号" Width="60px" />
            <f:BoundField DataField="Name" HeaderText="菜单标题" DataSimulateTreeLevelField="TreeLevel" Width="150px" SortField="Name" />
            <f:BoundField DataField="NavigateUrl" HeaderText="链接" ExpandUnusedSpace="true" />
            <f:BoundField DataField="SortIndex" HeaderText="排序" Width="80px" />
        </Columns>
        <PageItems>
            <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
            </f:ToolbarSeparator>
            <f:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
            </f:ToolbarText>
            <f:DropDownList ID="ddlGridPageSize" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="ddlGridPageSize_SelectedIndexChanged"
                runat="server">
                <f:ListItem Text="10" Value="10" />
                <f:ListItem Text="20" Value="20" />
                <f:ListItem Text="50" Value="50" />
                <f:ListItem Text="100" Value="100" />
            </f:DropDownList>
        </PageItems>
    </f:Grid>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Foot" runat="server">
</asp:Content>
