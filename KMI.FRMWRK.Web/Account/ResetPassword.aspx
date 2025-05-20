<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="KMI.FRMWRK.Web.Account.ResetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reset Password</title>
    <%: Styles.Render("~/bundles/FRMWRKcss") %>
    <%: Scripts.Render("~/bundles/FRMWRKjs") %>
    <script>
        $(document).ready(function () {
            debugger;
            if (queryString("tockenid") != 'false' && queryString("tockenid") != '')
                $('#divOldPassword').hide();
            else
                $('#divOldPassword').show();

        })


        function AlertMsg(msg) {
            var headerCSS = '';
            if (msg == 'SUCCESS') {
                headerCSS = 'alert-success';
                $('#btnLogin').removeClass('disabled');
                msg = 'Password reseted successfully, Please use Login to continue.'
            }
            else
                headerCSS = 'alert-warning';

            showModal('#myModal', 'Alert', headerCSS, '', '', msg);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="panel panel-success">
                <div class="panel-heading">Reset password</div>
                <div class="panel-body">
                    <div class="form-horizontal">

                        <div id="divOldPassword" class="form-group">
                            <label class="control-label col-sm-2" for="email">Old Password :</label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtOldPassword" CssClass="form-control" runat="server" placeholder="Old Password" TextMode="Password"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-sm-2" for="email">New Password :</label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtNewPassword" CssClass="form-control" runat="server" placeholder="New Password" TextMode="Password"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-sm-2" for="email">Confirm Password :</label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtConfirmNewPassword" CssClass="form-control" runat="server" placeholder="Confirm New Password" TextMode="Password"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-12">
                            <asp:Button ID="btnLogin" runat="server" CssClass="btn-animated bg-green pull-right disabled" Text="Login" OnClick="btnLogin_Click" />
                            <asp:Button ID="btnResetPassword" runat="server" CssClass="btn-animated bg-green pull-right" Text="Reset Password" OnClick="btnResetPassword_Click" />
                        </div>

                    </div>
                </div>
            </div>

        </div>

        <!-- Display Modal popup window in division -->
        <div class="modal fade" id="myModal" role="dialog">
        </div>
        <!-- End Display Modal popup window in division -->

    </form>
</body>
</html>
