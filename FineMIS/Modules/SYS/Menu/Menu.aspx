<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Page.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="FineMIS.Modules.SYS.Menu.Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <f:Grid ID="MainPanel" runat="server" BoxFlex="1" ShowBorder="false" ShowHeader="false" EnableCheckBoxSelect="true" EnableColumnLines="True">
        <Toolbars>
            <f:Toolbar runat="server">
                <Items>
                    <f:Button ID="btnRefresh" Icon="ArrowRefresh" runat="server" Text="刷新" OnClick="btnRefresh_Click">
                    </f:Button>
                    <f:Button ID="btnInsert" Icon="Add" runat="server" Text="新增" OnClick="btnInsert_Click">
                    </f:Button>
                    <f:Button ID="btnDelete" Icon="Delete" runat="server" Text="删除" OnClick="btnDelete_Click">
                    </f:Button>
                    <f:Button ID="btnAudit" Icon="Lock" runat="server" Text="审核" OnClick="btnDelete_Click">
                    </f:Button>
                    <f:Button ID="btnUnaudit" Icon="LockBreak" runat="server" Text="反审" OnClick="btnDelete_Click">
                    </f:Button>
                    <f:Button ID="btnExport" Icon="SystemSave" runat="server" Text="导出" OnClick="btnDelete_Click">
                    </f:Button>
                    <f:Button ID="btnPrint" Icon="Printer" runat="server" Text="打印" OnClick="btnDelete_Click">
                    </f:Button>
                    <f:TwinTriggerBox ID="ttbFullTextSearch" runat="server" EmptyText="请输入关键字"
                        Trigger1Icon="Clear" Trigger2Icon="Search" Width="300" ShowTrigger1="False">
                    </f:TwinTriggerBox>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Columns>
            <f:RowNumberField runat="server" HeaderText="序号" Width="60px" />
            <f:LinkButtonField ColumnID="detailField" CommandName="DETAIL" TextAlign="Center" Icon="Information" ToolTip="查看" HeaderText="查看" Width="50px" />
            <f:LinkButtonField ColumnID="editField" CommandName="UPDATE" TextAlign="Center" Icon="Pencil" ToolTip="编辑" HeaderText="编辑" Width="50px" />
            <f:BoundField DataField="Name" HeaderText="标题" DataSimulateTreeLevelField="TreeLevel" Width="150px" />
            <f:BoundField DataField="ViewName" HeaderText="视图" />
            <f:BoundField DataField="SortIndex" HeaderText="排序" />
            <f:BoundField DataField="ImageUrl" HeaderText="图标" ExpandUnusedSpace="true" />
            <f:BoundField DataField="NavigateUrl" HeaderText="链接" ExpandUnusedSpace="true" />
        </Columns>
    </f:Grid>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Foot" runat="server">
</asp:Content>
