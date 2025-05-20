<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ErrorSession.aspx.cs" Inherits="Error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Error Profile</title>
    <link rel="stylesheet" type="text/css" href="Styles/Site.css" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="width: 100%; height: 400px;">
            <tr>

                <td style="width: 100%; height: 100%" valign="middle" align="center">
                    <asp:Label ID="lblMessage" runat="server" CssClass="msgerror" Text="" Font-Bold="True" Visible="true"></asp:Label><br />
                    <br />
                    <img src="Content/Images/SessionExpire.png" alt="Session Expire" /><br />
                    <br />
                    <span style="font-size: 8pt; font-weight: bold; color: #3300ff; font-family: Tahoma,Arial; text-decoration: underline">
                        <asp:HyperLink ID="hlAction" runat="server" NavigateUrl="~/Account/Login.aspx" Target="_top">Click here to Login </asp:HyperLink>
                    </span>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
