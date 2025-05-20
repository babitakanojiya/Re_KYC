<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="CkycPMSVerify.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.CKYC.CkycPMSVerify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/bundles/CKYCValidationjs") %>
    <%: Styles.Render("~/bundles/CKYCcss") %>
    <style type="text/css">
        .GridViewtr {
            padding: 3px;
            height: 40px;
            background-color: #DCE9F9;
            font-weight: bold;
        }



        table tbody tr td {
            font-family: Verdana;
            font-weight: normal;
            font-size: 12px;
            padding: 3px 3px 3px 3px; /* color: #0076B7;*/
            background-color: #fff;
            text-align: left;
            border: 1px solid;
            border-color: Gray !important;
            white-space: normal;
            width: 25%;
        }

        .image {
            margin-left: 35%;
        }

        .chk {
            margin-left: 25%;
        }

        .gridview th {
            padding: 3px;
            height: 40px;
            background-color: #d6d6c2;
        }


        .disablepage {
            display: none;
        }
    </style>
    <script type="text/javascript">
        function popup() {
            $("#myModal").modal();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">

    <asp:ScriptManager ID="PMS" runat="server">
    </asp:ScriptManager>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="page-container" style="margin-top: 0px;">
                <div id="trDgViewDtl" runat="server" class="page-container" style="margin-top: 0px;">
                    <%-- visible=""--%>
                    <div class="panel panel-success" style='margin-right: 26px; margin-left: 26px;'>
                        <div id="Div1" runat="server" class="panel-heading" style='margin-left: 0px; margin-right: 0px;'
                            onclick="showHideDiv('trgridsponsorship','Span4');return false;">
                            <div class="row" id="Div2" runat="server">
                                <div class="col-sm-10" style="text-align: left">
                                    <span class="glyphicon glyphicon-menu-hamburger"></span>
                                    <asp:Label ID="Label1" runat="server" Text="Probable Match Based on Detail Data (Download API)"></asp:Label>
                                </div>
                                <div class="col-sm-2">
                                    <span id="Span4" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                                </div>
                            </div>
                        </div>
                        <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                              <ContentTemplate>--%>
                        <div id="trgridsponsorship" class="panel-body">
                            <asp:GridView ID="dgView" runat="server" CssClass="footable"
                                AutoGenerateColumns="true" PageSize="10" CellPadding="1">
                                <RowStyle CssClass="GridViewProbableRow"></RowStyle>
                                <PagerStyle CssClass="disablepage" />
                                <HeaderStyle CssClass="gridview th" />

                            </asp:GridView>
                            <%--  </ContentTemplate>
                                        </asp:UpdatePanel>--%>
                            <asp:HiddenField ID="hdnRemark" runat="server" />
                        </div>
                        <br />
                        <div id="divBindData" runat="server" visible="false" style="padding: 5px;">
                            <div class="row">
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblCKYC" CssClass="control-label" Text="Select CKYC Number for Match Details:"
                                        Font-Bold="true" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                </div>
                                <div class="col-sm-3" style="text-align: left">
                                </div>
                                <div class="col-sm-3">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblRefNo" CssClass="control-label" Text="CKYC Reference Number:" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtRefNo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblName" CssClass="control-label" Text="Name:" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>



                    <div class="col-sm-3" style="text-align: left" style="display: none">
                        <asp:Label ID="lblPageInfo" runat="server" Visible="false"></asp:Label>
                    </div>
                    <div id="divloadergrid" class="col-sm-12" runat="server" style="display: none;">
                        <caption>
                            <img id="Img2" alt="" src="~/images/spinner.gif" runat="server" />
                            Loading...
                        </caption>
                    </div>
                </div>

                <div class="row" style="margin-top: 12px;" id="divButtons" runat="server">
                    <div class="col-sm-12" align="center">
                        <asp:Button ID="btnNoMatch" runat="server" CssClass="btn-animated bg-green" OnClick="btnNoMatch_Click" CausesValidation="false" Text="No Match"></asp:Button>
                        <asp:Button ID="btnComMatch" runat="server" OnClick="btnComMatch_Click" CssClass="btn-animated bg-green" CausesValidation="false" Text="Complete Match" Enabled="false"></asp:Button>
                        <asp:Button ID="btnCancel" TabIndex="43" runat="server" CssClass="btn-animated bg-horrible" Text="Cancel" OnClick="btnCancel_Click"></asp:Button>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="myModal" role="dialog">
                <div class="modal-dialog modal-sm">

                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header" style="text-align: center; background-color: #dff0d8;">
                            <asp:Label ID="Label2" Text="INFORMATION" runat="server" Font-Bold="true"></asp:Label>

                        </div>
                        <div class="modal-body" style="text-align: center">
                            <asp:Label ID="lbl3" runat="server"></asp:Label><br />
                            <asp:Label ID="lbl2" runat="server"></asp:Label><br />

                        </div>
                        <div class="modal-footer" style="text-align: center">


                            <asp:LinkButton ID="lnkOk" runat="server" OnClick="btnOk_Click" CssClass="btn btn-primary">
                                  <span class="glyphicon glyphicon-ok" style="color:White"> </span> OK
                            </asp:LinkButton>

                        </div>
                    </div>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
