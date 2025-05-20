<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Empty.Master" CodeBehind="ExecutionHistoImage.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.CKYC.ExecutionHistoImage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/bundles/CKYCValidationjs") %>
    <%: Styles.Render("~/bundles/CKYCcss") %>
    <style>
        .standardcheckbox {
            padding-right: 9px !important;
        }

        .chkClass label {
            margin-left: 3px !important;
        }

        input[type=checkbox], input[type=radio] {
            margin-right: 7px !important;
        }

            input[type=checkbox] + label, input[type=radio] + label {
                vertical-align: middle !important;
            }
    </style>
    <style>
        .gridview th {
            padding: 3px;
            height: 40px;
            background-color: #d6d6c2;
            color: #337ab7;
            text-align: center;
        }

        .left_padding {
            margin-left: 35%;
        }

    </style>
    <script type="text/javascript">
        function AlertMsg(msg) {
            debugger;
            showModal('#myModal', 'Information', 'alert-warning', '', '', msg);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">
    <asp:ScriptManager ID="SM1" runat="server">
    </asp:ScriptManager>
    <div class="container" style="margin-top: 31px; width: 100%;">
        <div id="divImg" runat="server" class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
            <div id="Div19" runat="server" class="panel-heading" style="padding: 0;">
                <div class="row">
                    <div class="col-sm-10" style="text-align: left; padding-top: 7px; margin-bottom: -5px;">
                        <%--<span class="glyphicon glyphicon-menu-hamburger"></span>--%>
                        <asp:Label ID="lbluploadDoc" Text="&nbsp;&nbsp;UPLOADED DOCUMENTS" runat="server" CssClass="control-label"></asp:Label>
                    </div>
                    <div class="col-sm-2">
                        <span id="btnnavigate" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                    </div>
                </div>
            </div>
            <div id="divnavigate" style="display: block; padding-top: 0px;" runat="server" class="panel-body">
                <div>
                    <div class="col-sm-12" style="text-align: right">
                        <asp:UpdatePanel runat="server" ID="upnlPrev">
                            <ContentTemplate>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div>
                    <div class="col-sm-3" style="text-align: left" style="display: none">
                        <asp:Label ID="lblPageInfo" runat="server" Visible="false"></asp:Label>
                    </div>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div id="divloaderqc" class="col-sm-12" runat="server" style="display: none; position: absolute;">
                                <caption>
                                    <img id="Img3" alt="" src="~/images/spinner.gif" runat="server" />
                                    Loading...
                                </caption>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel runat="server" ID="upnlHeader">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-sm-12" align="center">
                                    <asp:Label ID="lblpanelheader" runat="server" CssClass="control-label" />
                                    <asp:HiddenField ID="hdnDocId" runat="server" />
                                </div>
                            </div>
                            <div>
                                <asp:GridView ID="GridImage" runat="server" AllowSorting="True" CssClass="footable"
                                    Width="100%" AutoGenerateColumns="False" PageSize="1" AllowPaging="true" CellPadding="1"
                                    OnRowDataBound="GridImage_RowDataBound">
                                    <RowStyle CssClass="GridViewRow"></RowStyle>
                                    <PagerStyle CssClass="disablepage" />
                                    <HeaderStyle CssClass="gridview th" />
                                    <Columns>
                                        <asp:TemplateField SortExpression="SR_NO" HeaderText="SR_NO" Visible="false">
                                            <ItemTemplate>
                                                <%-- <asp:LinkButton ID="lblCndNo1" runat="server" Text='<%# Eval("SR_NO") %>'></asp:LinkButton>
                                                <asp:HiddenField ID="hdnid" runat="server" Value='<%# Eval("SR_NO") %>'></asp:HiddenField>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:ImageField DataImageUrlField="SR_NO" DataImageUrlFormatString="ImageCSharp.aspx?ImageID=ckyc+{0}"
                                            HeaderText="Preview Image">
                                            <ControlStyle CssClass="left_padding" Width="30%" />
                                            <%--Height="100%"--%>
                                        </asp:ImageField>
                                    </Columns>
                                </asp:GridView>
                                <center>                                                  
                                 <table>
                                     <tr>
                                         <td>
                                             <asp:Button ID="btnprev" Text="<" CssClass="form-submit-button" runat="server"
                                                 Width="40px" Enabled="false" Style="border-style: solid; border-width: 1px; background-repeat: no-repeat;
                                                 background-color: transparent; float: left; margin: 0; height: 30px;" OnClick="btnprev_Click" />
                                             <asp:Button ID="btnnext" Text=">" CssClass="form-submit-button" runat="server" Style="border-style: solid;
                                                 border-width: 1px; background-repeat: no-repeat; background-color: transparent;
                                                 float: left; margin: 0; height: 30px;" Width="40px" OnClick="btnnext_Click" OnClientClick="funload();" />
                                         </td>
                                     </tr>
                                 </table>
                                </center>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnnext" EventName="Click"></asp:AsyncPostBackTrigger>
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
