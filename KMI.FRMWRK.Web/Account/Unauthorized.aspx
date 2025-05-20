<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Unauthorized.aspx.cs" Inherits="KMI.FRMWRK.Web.Account.Unauthorized" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Unauthorized User</title>
    <link href="../Content/Bootstrap/css/bootstrap.css" rel="stylesheet" />
    <script src="../Content/Bootstrap/js/jquery-1.11.1.js"></script>
    <script src="../Content/Bootstrap/js/bootstrap.min.js"></script>
    <link href="../Content/Bootstrap/css/animate.css" rel="stylesheet" />
    <link href="../Content/kmi.framework.css" rel="stylesheet" />
    <style>
        body {
            background: rgba(49, 112, 143, 0.06);
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="panel panel-warning pnl-custom">
                <div class="panel-heading">
                        Unauthorized User
                </div>
                <div class="panel-body">
                    <img src="../Content/Images/unauthorize.jpg" />
                    <span>You are not authorized user to access this page.</span>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
