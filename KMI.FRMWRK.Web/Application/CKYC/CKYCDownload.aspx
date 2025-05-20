<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="CKYCDownload.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.CKYC.CKYCDownload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <%: Scripts.Render("~/bundles/CKYCValidationjs") %>
    <%: Styles.Render("~/bundles/CKYCcss") %>
    <style type="text/css">
        table > thead > tr > th {
            background-image: linear-gradient(to bottom,#d6d6c2,#d6d6c2) !important;
            text-align: center !Important;
        }

        .AlignCenter {
            text-align: center !Important;
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
    <center>
        <asp:ScriptManager ID="SM1" runat="server">
        </asp:ScriptManager>
        <br />
        <center>
            <div class="container">
                <div class="panel panel-success">
                    <div class="panel-heading" onclick="showHideDiv('EmptyPagePlaceholder_divSearch','btnToggle');return false;">
                <div class="row">
                    <div class="col-sm-10" style="text-align:left">
                       <span class="glyphicon glyphicon-menu-hamburger" ></span>
                                <asp:Label ID="lbltitle" runat="server"   CssClass="control-label"></asp:Label>
                    </div>
                    <div class="col-sm-2">
                        <span id="btnToggle" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                    </div>
                </div>
            </div>
                    
                    <div id="divSearch" runat="server" style="display: block;" class="panel-body">
                        <div class="row" style="padding-bottom: 6px ! important;">
                            <div class="col-md-3" style="text-align: left">
                                <asp:Label ID="lblUpload" runat="server" CssClass="control-label" ></asp:Label><span
                                    style="color: #ff0000">*</span>
                            </div>
                            <div class="col-md-6" style="text-align: left">
                                <asp:DropDownList ID="ddldwnload" DataTextField="ParamDesc" DataValueField="ParamValue"
                                    runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddldwnload_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3" style="text-align: left">
                                <asp:Button ID="btndwnld" runat="server" CssClass="btn-animated bg-green" OnClick="btndwnld_Click" Text="Search">
                        </asp:Button>
                            </div>
                        </div>

                         <div class="row">
                              <div class="col-md-12" style="text-align: left">
                                  <center>
                                  <asp:Label runat="server" ID="lblerror" Visible="false" ></asp:Label></center>
                              </div>
                         </div>
                        
                        <div class="row" id="divSearchDetails" runat="server" style="display: none">
                            <div class="panel  panel-success">
                                <div class="panel-heading subheader" onclick="ShowReqDtl('ctl00_ContentPlaceHolder1_trdg','btnDeptMstGrd');return false;"
                                    style="background-color: #EDF1cc !important;">
                                    <div class="row" id="trdgHdr" runat="server" visible="false">
                                        <div class="col-sm-10" style="text-align: left">
                                            <span class="glyphicon glyphicon-menu-hamburger" style="color: Orange;"></span>
                                            <asp:Label ID="lblSearch" runat="server"  CssClass="control-label"></asp:Label>
                                        </div>
                                        <div class="col-sm-2">
                                            <span id="btnDeptMstGrd" class="glyphicon glyphicon-collapse-down" style="float: right;
                                                color: Orange; padding: 1px 10px ! important; font-size: 18px;"></span>
                                        </div>
                                    </div>
                                </div>
                                <div id="trdg" runat="server" visible="false">
                                    <div>
                                        <div style="overflow-x: scroll;">
                                            <asp:GridView ID="dgDownload" runat="server" AutoGenerateColumns="true" EmptyDataText="No records has been added."
                                             Width="2000px"   PageSize="10" AllowSorting="False" AllowPaging="True" OnPageIndexChanging="dgDownload_PageIndexChanging"
                                                OnRowCreated="dgDownload_RowCreated" CssClass="footable">
                                               <HeaderStyle ForeColor="Black" />

                                                <PagerTemplate>
                            <table class="tablePager" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td class="tablePagerDataSmall" align="left">
                                        <asp:Label ID="lblpageindx2" CssClass="standardlabelCRM" Text="Page : " runat="server"></asp:Label>
                                    </td>
                                    <td align="center" class="tablePagerData" style="display: none;">
                                        <table cellspacing="2">
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ToolTip="First Page" CommandName="Page" CommandArgument="First"
                                                        runat="server" ID="ImgbtnFirst" ImageUrl="../../Content/Images/ImgArrFirst.gif" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ToolTip="Previous Page" CommandName="Page" CommandArgument="Prev"
                                                        runat="server" ID="ImgbtnPrevious" ImageUrl="../../Content/Images/ImgArrPrevious.gif" />
                                                </td>
                                                <td>
                                                    <asp:DropDownList ToolTip="Goto Page" ID="ddlPageSelectorL" runat="server" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlPageSelectorL_SelectedIndexChanged" CssClass="standardPagerDropdown">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ToolTip="Next Page" CommandName="Page" CommandArgument="Next" runat="server"
                                                        ID="ImgbtnNext" ImageUrl="../../Content/Images/ImgArrNext.gif" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ToolTip="Last Page" CommandName="Page" CommandArgument="Last" runat="server"
                                                        ID="ImgbtnLast" ImageUrl="../../Content/Images/ImgArrLast.gif" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td align="right" class="tablePagerData">
                                        <table cellspacing="2">
                                            <tr>
                                                <td>
                                                    <span style="padding-left: 5px;"></span>
                                                    <asp:ImageButton ToolTip="First Page" CommandName="Page" CommandArgument="First"
                                                        runat="server" ID="ImgbtnFirst1" ImageUrl="../../Content/Images/ImgArrFirst.gif" />
                                                </td>
                                                <td>
                                                    <span style="padding-left: 5px;"></span>
                                                    <asp:ImageButton ToolTip="Previous Page" CommandName="Page" CommandArgument="Prev"
                                                        runat="server" ID="ImgbtnPrevious1" ImageUrl="../../Content/Images/ImgArrPrevious.gif" />
                                                </td>
                                                <td>
                                                    <span style="padding-left: 5px;"></span>
                                                    <asp:DropDownList ToolTip="Goto Page" ID="ddlPageSelectorR" runat="server" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlPageSelectorR_SelectedIndexChanged" CssClass="standardPagerDropdown">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <span style="padding-left: 5px;"></span>
                                                    <asp:ImageButton ToolTip="Next Page" CommandName="Page" CommandArgument="Next" runat="server"
                                                        ID="ImgbtnNext1" ImageUrl="../../Content/Images/ImgArrNext.gif" />
                                                </td>
                                                <td>
                                                    <span style="padding-left: 5px;"></span>
                                                    <asp:ImageButton ToolTip="Last Page" CommandName="Page" CommandArgument="Last" runat="server"
                                                        ID="ImgbtnLast1" ImageUrl="../../Content/Images/ImgArrLast.gif" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="tablePagerDataSmall" align="right" style="display: none">
                                        <asp:Label ID="lblpageindx" CssClass="standardlabelCRM" Text="Page : " runat="server" ></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            </table>
                        </PagerTemplate>
                                            </asp:GridView>
                                        </div>
                                        <br />
                                        
                                        <div class="row">
                                            <div width="100%" align="center">
                                                <asp:CheckBox ID="chkfiledwnld" CellPadding="0" CellSpacing="0" RepeatLayout="Table"
                                                    TextAlign="Right" runat="server" RepeatDirection="Horizontal" Width="100%" CssClass="standardCheckBox"
                                                    AutoPostBack="true" OnCheckedChanged="chkfiledwnld_CheckedChanged" Text="Confirm Successfull File Download"
                                                    Font-Size="120%" />
                                            </div>
                                        </div>
                                        <asp:HiddenField ID="hdnEnbl" runat="server" />
                                        <asp:HiddenField ID="hdncheck" runat="server" />
                                        <div class="row">
                                            <div align="center">
                                              <asp:Button runat="server" ID="btnExport" OnClick="btnExport_Click" Text="Download" CssClass="btn-animated bg-green"></asp:Button>
                                              <asp:Button runat="server" ID="btncnfrm" OnClick="btncnfrm_Click" Text="Confirm" CssClass="btn-animated bg-green"></asp:Button>
                                              <asp:Button ID="btnfail" CssClass="btn-animated bg-horrible" runat="server" Visible="true" OnClick="btnFail_Click" Text="Cancel"></asp:Button>
                                            </div>
                                        </div>
                                        <br />
                                    </div>
                                </div>
                                
                            </div>
                        </div>
                        <div id="btncan" runat="server" visible="false" class="row">
                            <div align="center">
                                <asp:LinkButton ID="btnpopcancel" runat="server" CssClass="btn btn-danger">
                         <span class="glyphicon glyphicon-remove BtnGlyphicon"></span> Cancel</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            
        </center>
       
    </center>
    <!-- Display Modal popup window in division -->
    <div class="modal fade" id="myModal" role="dialog">
    </div>
    <!-- End Display Modal popup window in division -->
    <asp:HiddenField ID="hdnBatchId" runat="server" />
    <asp:HiddenField ID="hdnpath" runat="server" />
</asp:Content>
