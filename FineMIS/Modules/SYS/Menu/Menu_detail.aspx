<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Page.Master" AutoEventWireup="true" CodeBehind="Menu_detail.aspx.cs" Inherits="FineMIS.Modules.SYS.Menu.Menu_detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <f:Form ID="Form1" BoxFlex="1" BodyPadding="10px" runat="server">
        <Toolbars>
            <f:Toolbar ID="Toolbar1" runat="server" Position="Bottom" ToolbarAlign="Right">
                <Items>
                    <f:Button ID="btnClose" Icon="SystemClose" runat="server" Text="关闭" OnClick="btnClose_Click">
                    </f:Button>
                    <f:Button ID="btnReset" Icon="ArrowUndo" runat="server" Text="重置" OnClick="btnReset_Click">
                    </f:Button>
                    <f:Button ID="btnSaveAndContinue" Icon="SystemSaveNew" runat="server" Text="保存并继续" OnClick="btnSaveAndContinue_Click">
                    </f:Button>
                    <f:Button ID="btnSaveAndClose" Icon="SystemSaveClose" runat="server" Text="保存并关闭" OnClick="btnSaveAndClose_Click">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Items>
            <f:GroupPanel runat="server" ID="GroupPanel1" Layout="Anchor" Title="基础信息" BodyPadding="10px">
                <Items>
                    <f:TextBox ID="Name" runat="server" Label="菜单标题" Required="true" ShowRedStar="true">
                    </f:TextBox>
                    <f:DropDownList ID="ParentId" Label="上级菜单" Required="true" ShowRedStar="true"
                        runat="server">
                    </f:DropDownList>
                </Items>
            </f:GroupPanel>
            <f:GroupPanel runat="server" ID="GroupPanel2" Layout="Anchor" Title="详细信息" BodyPadding="10px" EnableCollapse="true">
                <Items>
                    <f:NumberBox ID="SortIndex" Label="排序" runat="server">
                    </f:NumberBox>
                    <f:TextBox ID="NavigateUrl" Label="链接" runat="server">
                    </f:TextBox>
                    <f:TextBox ID="ImageUrl" Label="图片链接" runat="server">
                    </f:TextBox>
                    <f:RadioButtonList ID="iconList" ColumnNumber="4" runat="server" AutoPostBack="true" OnSelectedIndexChanged="iconList_SelectedIndexChanged">
                    </f:RadioButtonList>
                </Items>
            </f:GroupPanel>
        </Items>
    </f:Form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Foot" runat="server">
</asp:Content>
