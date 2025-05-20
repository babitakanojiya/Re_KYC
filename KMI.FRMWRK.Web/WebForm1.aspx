<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="KMI.FRMWRK.Web.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">
    <asp:ScriptManager ID="SM1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
    <asp:UpdatePanel ID="Updatepanel3" runat="server" UpdateMode="Conditional" >
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
</asp:Content>
