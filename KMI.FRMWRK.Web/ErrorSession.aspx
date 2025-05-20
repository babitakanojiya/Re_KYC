<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorSession.aspx.cs" Inherits="KMI.FRMWRK.Web.ErrorSession1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding: 50px 0; text-align: center;">
            <asp:Label ID="Label1" runat="server" CssClass="msgerror" Text="" Font-Bold="True" Visible="true"></asp:Label><br />
            <br />
            <img src="Content/Images/SessionExpire.png" alt="Session Expire" /><br />
            <br />
            <span style="font-size: 8pt; font-weight: bold; color: #3300ff; font-family: Tahoma,Arial; text-decoration: underline">
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Account/Login.aspx" Target="_top">Click here to Login </asp:HyperLink>
            </span>
        </div>
    </form>
</body>
</html>
