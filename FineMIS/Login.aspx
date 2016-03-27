<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FineMIS.Login" %>

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
        html {
            overflow: auto;
        }

        html, body {
            height: 100%;
            width: 100%;
            margin: 0;
            padding: 0;
            border: 0;
            text-align: center;
        }

        #bgDiv {
            position: absolute;
            top: 0;
            overflow: hidden;
            width: 100%;
            height: 100%;
            background-image: url(res/images/bg.jpg);
            background-repeat: no-repeat;
            background-size: cover;
        }

        #login {
            width: 280px;
            height: 340px;
            background-color: #F7F7F7;
            position: absolute;
            left: 50%;
            top: 50%;
            margin: -170px 0 0 -140px;
            opacity: 0.8;
            filter: alpha(opacity=80);
            border-radius: 5px;
            box-shadow: 0 5px 5px rgba(0, 0, 0, 0.5);
        }

        #img {
            margin-top: 30px;
        }

        #btn {
            margin-top: 10px;
        }

        .profile-image {
            width: 96px;
            height: 96px;
            margin: 0 auto 10px;
            display: block;
            border-radius: 50%;
        }

        .profile-input {
            width: 220px;
        }

            .profile-input input {
                height: 40px;
                line-height: 40px;
                font-size: 16px;
            }

        .profile-button {
            background: #4387F5 !important;
            width: 220px;
            height: 40px;
        }

        .profile-error span {
            color: #FC4343;
        }

        #footer {
            text-align: right;
            position: absolute;
            bottom: 0;
            right: 20px;
        }

        .footer-text span {
            font-size: 13px;
            font-weight: bold;
            color: #FFFFFF;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" />
        <div id="bgDiv">
            <div id="login">
                <div id="img">
                    <f:Image ID="Image1" ImageUrl="res/images/avatar_2x.png" ImageCssClass="profile-image" runat="server"></f:Image>
                </div>
                <div>
                    <f:Label Text="" CssClass="profile-error" runat="server" ID="lblTips"></f:Label>
                </div>
                <div>
                    <f:TextBox EmptyText="用户名" CssClass="profile-input" runat="server" ID="tbxUserName"
                        OnTextChanged="tbxUserName_TextChanged" AutoPostBack="true" NextFocusControl="tbxPassword">
                    </f:TextBox>
                </div>
                <div>
                    <f:TextBox EmptyText="密　码" CssClass="profile-input" runat="server" ID="tbxPassword"
                        OnTextChanged="tbxPassword_TextChanged" AutoPostBack="true" NextFocusControl="btnLogin" TextMode="Password">
                    </f:TextBox>
                </div>
                <div id="btn">
                    <f:Button ID="btnLogin" Text="登录" CssClass="profile-button" Size="Large" runat="server" OnClick="btnLogin_Click"></f:Button>
                </div>
            </div>
            <div id="footer">
                <p>
                    <f:Label ID="Label1" Text="预拌混凝土（砂浆）行业信息化解决方案" CssClass="footer-text" runat="server"></f:Label>
                </p>
                <p>
                    <f:Label ID="Label2" Text="Copyright &copy;2011 - 2016 湖北鄂恒科技有限公司" CssClass="footer-text" runat="server"></f:Label>
                </p>
            </div>
        </div>
    </form>
</body>
</html>
