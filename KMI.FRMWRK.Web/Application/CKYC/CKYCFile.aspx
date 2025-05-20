<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="CKYCFile.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.CKYC.CKYCFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/bundles/CKYCValidationjs") %>
    <%: Styles.Render("~/bundles/CKYCcss") %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">
    <script type="text/javascript">
        function popup() {
            $("#myModal").modal();

        }
        function funload() {
            document.getElementById('EmptyPagePlaceholder_divloaderqc').style.display = 'block'
        }

    </script>
    <asp:ScriptManager ID="SM1" runat="server">
    </asp:ScriptManager>
    <br />
    <center>
        <div class="container">
            <div class="panel panel-success">
                <div id="Div1"  class="panel-heading" onclick="showHideDiv('divSearch','btnToggle');return false;">
                    <div class="row">
                        <div class="col-sm-10" style="text-align: left">
                            <span class="glyphicon glyphicon-menu-hamburger"></span>
                            <asp:Label ID="lbltitle" runat="server" Height="14px" Font-Size="Small">CKYC SFTP Batch Job</asp:Label>
                        </div>
                        <div class="col-sm-2">
                              <span id="btnToggle" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                        </div>
                    </div>
                </div>
                <div id="divSearch"  class="panel-body">
                    <div class="row" runat="server">
                        <div class="col-md-6" style="text-align: left">
                            <asp:Label ID="Label1" runat="server" Height="14px" Font-Size="Small">Generating file from our system which are to be uploaded in CERSAI</asp:Label>
                        </div>
                        <div class="col-md-3" style="text-align: right">
                            <asp:LinkButton ID="btnGenerate" runat="server" CssClass="btn-animated bg-green" Height="38px" Width="150px"
                                OnClick="btnGenerate_Click"  OnClientClick="funload();">
                         <span class="glyphicon glyphicon-file BtnGlyphicon"></span> Generate File</asp:LinkButton>
                         </div>
                          <div class="col-md-3" style="text-align: left">
                           <asp:Label ID="Label6" runat="server" Height="14px" Font-Size="Small"></asp:Label>
                        </div>
                    </div>
                    <br />
                    <div id="Div2" class="row" runat="server">
                        <div class="col-md-6" style="text-align: left">
                            <asp:Label ID="Label2" runat="server" Height="14px" Font-Size="Small">Dowloaded files are uploaded to CERSAI from our system</asp:Label>
                        </div>
                        <div class="col-md-3" style="text-align: right">
                            <asp:LinkButton ID="btnUpld" runat="server" CssClass="btn-animated bg-green" Width="150px"
                                OnClick="btnUpld_Click" OnClientClick="funload();">
                         <span class="glyphicon glyphicon-cloud-upload BtnGlyphicon"></span> Upload</asp:LinkButton>
                         </div>
                           <div class="col-md-3" style="text-align: left">
                            <asp:Label ID="Label7" runat="server" Height="14px" Font-Size="Small">(To CERSAI)</asp:Label>
                        </div>
                         
                    </div>
                    <br />
                    <div id="Div3" class="row" runat="server">
                        <div class="col-md-6" style="text-align: left">
                            <asp:Label ID="Label4" runat="server" Height="14px" Font-Size="Small">Files are uploaded to our system from CERSAI</asp:Label>
                        </div>
                        <div class="col-md-3" style="text-align: right">
                            <asp:LinkButton ID="btnDwnld" runat="server" CssClass="btn-animated bg-green" Width="150px"
                                OnClick="btnDwnld_Click" OnClientClick="funload();">
                         <span class="glyphicon glyphicon-cloud-download BtnGlyphicon"></span> Download</asp:LinkButton>
                        </div>
                         <div class="col-md-3" style="text-align: left">
                            <asp:Label ID="Label8" runat="server" Height="14px" Font-Size="Small">(From CERSAI)</asp:Label>
                        </div>
                         
                    </div>
                    <br />
                    <div id="Div4" class="row" runat="server">
                        <div class="col-md-6" style="text-align: left">
                            <asp:Label ID="Label5" runat="server" Height="14px" Font-Size="Small">Files are process in our system</asp:Label>
                        </div>
                        <div class="col-md-3" style="text-align: right">
                            <asp:LinkButton ID="btnProcess" runat="server" CssClass="btn-animated bg-green" OnClick="btnProcess_Click"
                                Width="150px" OnClientClick="funload();">
                         <span class="glyphicon glyphicon-play-circle BtnGlyphicon"></span> Process</asp:LinkButton>
                        </div>
                         <div class="col-md-3" style="text-align: left">
                            <asp:Label ID="Label9" runat="server" Height="14px" Font-Size="Small"></asp:Label>
                        </div>
                         
                    </div>
                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div id="divloaderqc" class="col-sm-12" runat="server" style="display: none; position: absolute;">
                        <caption>
                            <img id="Img3" alt="" src="~/images/spinner.gif" runat="server" />
                            Loading...
                        </caption>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="container">
                    <div class="modal fade" id="myModal" role="dialog">
                        <div class="modal-dialog modal-sm">
                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header" style="text-align: center; background-color: #dff0d8;">
                                    <asp:Label ID="Label3" Text="INFORMATION" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <div class="modal-body" style="text-align: center">
                                    <asp:Label ID="lbl" runat="server"></asp:Label>
                                </div>
                                <div class="modal-footer" style="text-align: center">
                                    <button type="button" class="btn btn-primary" data-dismiss="modal" style='margin-top: -6px;'>
                                        <span class="glyphicon glyphicon-ok" style='color: White;'></span>OK
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </center>
</asp:Content>
