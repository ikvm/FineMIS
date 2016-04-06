using System;
using FineMIS.Pages;
using FineUI;
using OutOfMemory;
using PetaPoco;

namespace FineMIS.Modules.SYS.Menu
{
    public partial class Menu_detail : SingleFormPage
    {
        public override Form MainForm => Form1;

        protected void Page_Init(object sender, EventArgs e)
        {
            BindParentId();
            // 预置图标列表
            InitIconList(iconList);
        }

        public void InitIconList(RadioButtonList iconsList)
        {
            string[] icons = { "tag_yellow", "tag_red", "tag_purple", "tag_pink", "tag_orange", "tag_green", "tag_blue" };
            foreach (var icon in icons)
            {
                string value = $"~/res/icon/{icon}.png";
                string text = $"<img style=\"vertical-align:bottom;\" src=\"{ResolveUrl(value)}\" />&nbsp;{icon}";
                iconsList.Items.Add(new RadioItem(text, value));
            }
        }


        /// <summary>
        ///     绑定下拉菜单
        /// </summary>
        private void BindParentId()
        {
            var mys = ResolveDdl(SYS_MENU_Helper.Menus, (int)Id);
            // 绑定到下拉列表（启用模拟树功能和不可选择项功能）
            ParentId.EnableSimulateTree = true;
            ParentId.DataTextField = "Name";
            ParentId.DataValueField = "Id";
            ParentId.DataSimulateTreeLevelField = "TreeLevel";
            ParentId.DataEnableSelectField = "Enabled";
            ParentId.DataSource = mys;
            ParentId.DataBind();
        }

        /// <summary>
        ///     初始化表单
        /// </summary>
        protected override void InitForm()
        {
            switch (Action)
            {
                case ACTION.INSERT:
                    break;
                case ACTION.UPDATE:
                    FillData();
                    break;
                case ACTION.DETAIL:
                    FillData();
                    SetReadOnlyControls(MainForm);
                    btnReset.Visible = false;
                    btnSaveAndClose.Visible = false;
                    btnSaveAndContinue.Visible = false;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        ///     填充数据
        /// </summary>
        private void FillData()
        {
            var menu = SYS_MENU.SingleOrDefault(Sql.Builder.Where("Id=@0", Id));
            if (menu != null)
            {
                Name.Text = menu.Name;
                SortIndex.Text = menu.SortIndex.ToString();
                NavigateUrl.Text = menu.NavigateUrl;
                ParentId.SelectedValue = menu.ParentId.ToString();
                ImageUrl.Text = menu.ImageUrl;
                iconList.SelectedValue = menu.ImageUrl;
            }
        }

        /// <summary>
        /// 保存表单数据
        /// </summary>
        protected override void SaveForm()
        {
            SYS_MENU menu;
            switch (Action)
            {
                case ACTION.INSERT:
                    menu = new SYS_MENU
                    {
                        Name = Name.Text,
                        ImageUrl = ImageUrl.Text,
                        NavigateUrl = NavigateUrl.Text,
                        ParentId = ParentId.Text.ToInt64(),
                        SortIndex = SortIndex.Text.ToInt32()
                    };
                    menu.Insert();
                    SYS_MENU_Helper.Reload();
                    break;
                case ACTION.UPDATE:
                    menu = SYS_MENU.SingleOrDefault(Sql.Builder.Where("Id=@0", Id));
                    if (menu != null)
                    {
                        menu.Name = Name.Text;
                        menu.ImageUrl = ImageUrl.Text;
                        menu.NavigateUrl = NavigateUrl.Text;
                        menu.ParentId = ParentId.Text.ToInt64();
                        menu.SortIndex = SortIndex.Text.ToInt32();
                        menu.Update();
                        SYS_MENU_Helper.Reload();
                    }
                    break;
                case ACTION.DETAIL:
                    break;
                default:
                    break;
            }
        }


        protected void iconList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ImageUrl.Text = iconList.SelectedValue;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}