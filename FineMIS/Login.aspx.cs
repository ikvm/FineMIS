using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineMIS.Pages;

namespace FineMIS
{
    public partial class Login : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            ClearTips();

            // 如果用户已经登录
            // 则重定向到首页
            if (User.Identity.IsAuthenticated)
            {
                Response.Redirect(FormsAuthentication.DefaultUrl);
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbxPassword.Text))
            {
                SetTips("请输入用户名");
            }
            else if (string.IsNullOrEmpty(tbxUserName.Text))
            {
                SetTips("请输入密码");
            }
            else if (Security.AuthenticateUser(tbxUserName.Text, tbxPassword.Text, true))
            {
                // 必须使用自定义跳转
                Response.Redirect(FormsAuthentication.DefaultUrl);
            }
            else
            {
                SetTips("输入的用户名或密码有误");
                tbxUserName.Focus();
            }
        }

        protected void tbxPassword_TextChanged(object sender, EventArgs e)
        {
            ClearTips();
        }

        protected void tbxUserName_TextChanged(object sender, EventArgs e)
        {
            ClearTips();
        }

        private void SetTips(string tips)
        {
            lblTips.Text = tips;
        }

        private void ClearTips()
        {
            SetTips(string.Empty);
        }
    }
}