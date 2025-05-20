<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="KMI.FRMWRK.Web.Account.ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Forgot Password</title>
    <%: Styles.Render("~/bundles/FRMWRKcss") %>
    <link href="../Content/jquery-ui.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="panel panel-success">
                <div class="panel-heading">Please enter your registered email Id</div>
                <div class="panel-body">
                    <div class="row">

                        <div class="col-sm-10">
                            <asp:TextBox ID="txtEmailID" CssClass="form-control" runat="server" placeholder="Email ID"></asp:TextBox>
                        </div>
                        <div class="col-sm-2">
                            <asp:Button ID="btnSendLink" CssClass="btn-animated bg-green pull-right" runat="server" OnClick="btnSendLink_Click" Text="Send Link"></asp:Button>
                        </div>

                    </div>

                </div>
            </div>

            <div class="row">
                <div class="col-sm-12">
                    <asp:Label ID="lblMessage" CssClass="" runat="server"></asp:Label>
                </div>
            </div>
        </div>

    </form>

    <%: Scripts.Render("~/bundles/FRMWRKjs") %>
</body>
</html>
