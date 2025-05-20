<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="KMI.FRMWRK.Web.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >
     <asp:ScriptManager ID="SM1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" >
        <ContentTemplate>
            <div class="row">
                <div class="col-sm-3">
                    <asp:DropDownList ID="cboTitle" runat="server" CssClass="form-control" AutoPostBack="true">
                        <asp:ListItem Value="">Select</asp:ListItem>
                        <asp:ListItem Value="1">Akash</asp:ListItem>
                        <asp:ListItem Value="2">Sunny</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
   <%-- <script type="text/javascript">
        //<![CDATA[
        Sys.WebForms.PageRequestManager._initialize('ScriptManager1', 'form1', ['tUpdatePanel2', 'UpdatePanel2', 'tUpdatePaneldayMart', 'UpdatePaneldayMart'], [], [], 90, '');
        //]]>
    </script>
    <script src="Scripts/MicrosoftAjaxWebForms.js"></script>
    <script src="Scripts/MsAjaxBundle.js"></script>--%>

</asp:Content>
