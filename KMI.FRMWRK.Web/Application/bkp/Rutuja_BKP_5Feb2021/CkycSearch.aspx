<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="CkycSearch.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.CKYC.CkycSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/bundles/CKYCValidationjs") %>
    <%: Styles.Render("~/bundles/CKYCcss") %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">

    <style type="text/css">
        /*AP*/
        a {
            color: rgba(21, 62, 60, 0.93);
        }
        /*AP*/
        ul#menu {
            padding: 0;
        }

            ul#menu li {
                display: inline;
            }

                ul#menu li a {
                    background-color: Silver;
                    color: black;
                    cursor: pointer;
                    padding: 10px 20px;
                    text-decoration: none;
                    border-radius: 4px 4px 0 0;
                }

                    ul#menu li a:active {
                        background: white;
                    }

                    ul#menu li a:hover {
                        background-color: #F55856;
                    }
    </style>

    <style type="text/css">
        .pad {
            text-align: center !important;
        }
    </style>

    <script type="text/javascript">

        function funRedirect() {
            document.getElementById('EmptyPagePlaceholder_divloaderqc').style.display = 'block'
            document.getElementById('divloaderqc').style.top = '264px';
        }

        function OpenZipFilePage() {
            debugger;
            var modal = document.getElementById('myModalRaise_NEw');
            var modaliframe = document.getElementById("iframeCFR_New");
            modaliframe.src = "../../Application/CKYC/ZipFileDetail.aspx?Status=Zip";
            $('#myModalRaise_NEw').modal();
        }

        function OpenQCPage(refno) {
            debugger;
            var modal = document.getElementById('myModalRaise');
            var modaliframe = document.getElementById("iframeCFR");
            modaliframe.src = "../../Application/CKYC/CKYCQC.aspx?Status=QC&refno=" + refno;
            $('#myModalRaise').modal();
        }

        function OpenConstTypeQCPage(refno) {
            debugger;
            var modal = document.getElementById('myModalRaise');
            var modaliframe = document.getElementById("iframeCFR");
            modaliframe.src = "../../Application/CKYC/CKYCLegalEntityQC.aspx?Status=QC&refno=" + refno;
            $('#myModalRaise').modal();
        }

        function AlertMsg(msg) {
            showModal('#myModal', 'Alert', 'alert-warning', '', '', msg);
        }

    </script>

    <asp:ScriptManager ID="CKYCSearch" runat="server">
    </asp:ScriptManager>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <%--Added by Tushar for sync Zip File--%>
     <%--       <div class="row">
                <div class="col-sm-12" style="margin-left: 86.5%;">
                    <asp:Button ID="btnsyncFile" Text="Sync Zip File" OnClick="btnsyncFile_Click" CssClass="btn-animated bg-green" runat="server"></asp:Button>
                </div>
            </div>--%>
            <%--Added by Tushar for sync Zip File--%>
            <div class="page-container" style="margin-top: 0px;">


                <div class="panel  panel-success" style="margin-left: 2%; margin-right: 2%; margin-top: 0.5%">

                    <div class="panel-heading" onclick="showHideDiv('trSearchDetails','btnToggle');return false;">
                        <div class="row">
                            <div class="col-sm-10" style="text-align: left">
                                <span class="glyphicon glyphicon-menu-hamburger"></span>
                                <asp:Label ID="lblTitle" runat="server" Font-Bold="False" Text="CKYC Search"></asp:Label>
                            </div>
                            <div class="col-sm-2">
                                <span id="btnToggle" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div>

                    <div id="trSearchDetails" class="panel-body">
                        <div id="divSrvcReqReport1" style="display: block;" class="panel-body panel-collapse collapse in">
                            <div class="row" style="margin-bottom: 5px">
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblCASId" CssClass="control-label" runat="server" Text="FI Reference Number"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtRefNo" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                </div>
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblKycNo" CssClass="control-label" runat="server" Text="CKYC Number"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtKycNo" runat="server" CssClass="form-control" MaxLength="14"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row" style="margin-bottom: 5px">
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblGivenName" runat="server" CssClass="control-label" Text="Entity Name"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" MaxLength="60" onchange="javascript:this.value=this.value.toUpperCase();"></asp:TextBox>

                                </div>
                                <div id="tdPan" class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblPan" runat="server" CssClass="control-label" Text="PAN Number"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtPan" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"></asp:TextBox>
                                </div>
                                <div class="col-sm-3" style="display: none; text-align: left">
                                    <asp:Label ID="lblSurName" runat="server" CssClass="control-label" Text="Surname"></asp:Label>
                                </div>
                                <div class="col-sm-3" style="display: none;">
                                    <asp:TextBox ID="txtSurname" runat="server" CssClass="form-control" MaxLength="60" onchange="javascript:this.value=this.value.toUpperCase();"></asp:TextBox>

                                </div>
                            </div>
                            <div id="trregstrtndt" runat="server" class="row" style="margin-bottom: 5px">

                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblDTRegFrom" runat="server" CssClass="control-label" Text="Registration Date From"> </asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtDTRegFrom" runat="server" CssClass="form-control"
                                        MaxLength="15" onmousedown="$('#EmptyPagePlaceholder_txtDTRegFrom').datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd-mm-yy', yearRange: '1945:'+(new Date).getFullYear()  });"></asp:TextBox>
                                </div>
                                <div id="Div3" class="col-sm-3" style="text-align: left" runat="server">
                                    <asp:Label ID="lblDTRegTO" runat="server" Font-Bold="False" Text="Registration Date To"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtDTRegTo" runat="server" CssClass="form-control" MaxLength="15" onmousedown="$('#EmptyPagePlaceholder_txtDTRegTo').datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd-mm-yy', yearRange: '1945:'+(new Date).getFullYear()  });"></asp:TextBox>
                                </div>
                            </div>
                            <div id="TrForSpon" runat="server" class="row" visible="false" style="margin-bottom: 5px">
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblIdType" runat="server" CssClass="control-label" Text="ID Type"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlIdType" AutoPostBack="true" runat="server" CssClass="form-control"
                                        OnSelectedIndexChanged="ddlIdType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div id="Div2" class="col-sm-3" style="text-align: left" runat="server">
                                    <asp:Label ID="lblIdNo" runat="server" CssClass="control-label" Text="ID No"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtIdno" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div id="trShw" runat="server" class="row" style="margin-bottom: 5px">

                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblShwRecords" runat="server" CssClass="control-label" Text="Show Records"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlShwRecrds" runat="server" AutoPostBack="true" CssClass="form-control"
                                        OnSelectedIndexChanged="ddlShwRecrds_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <center>
                            <div class="col-sm-12" style='margin-top: 15px;'>
                                <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click" CssClass="btn-animated bg-green" runat="server">   <%--OnClientClick="return validtab();"--%>
                                   
                                </asp:Button>
                                <asp:Button ID="btnClear" OnClick="btnClear_Click" CssClass="btn-animated bg-horrible" Text="Clear" runat="server">    </asp:Button>
                            
                                <asp:Button ID="btnAddnew" runat="server"  CssClass="btn-animated bg-green"  OnClick="btnAddnew_Click" Text="Add New" Visible="false" TabIndex="12">
                            
                                </asp:Button>
                                <asp:Button ID="btnReFresh" runat="server" CssClass="btn btn-primary" Style="display: none;"
                                    ClientIDMode="Static" />
                                <div id="divloader" runat="server" style="display: none;">
                                    <caption>
                                        <img id="Img1" alt="" src="~/images/spinner.gif" runat="server" />
                                        Loading...
                                    </caption>
                                </div>
                            </div>
                        </center>
                            </div>
                            <br />
                            <div id="trnote" runat="server" class="col-sm-12" style="margin-bottom: 5px; text-align: center;">
                                <asp:Label ID="Label2" runat="server" Text="Note: All dates are in (dd-mm-yyyy)"
                                    ForeColor="Red"></asp:Label>
                            </div>
                            <div id="trRecord" runat="server" visible="false" colspan="6" style="height: 18px; text-align: center;">
                                <asp:Label ID="lblMessage" runat="server" CssClass="standardlabelErr"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="trDgViewDtl" runat="server" class="page-container" style="margin-top: 0px;">
                    <div class="panel panel-success" style='margin-right: 26px; margin-left: 26px;'>
                        <div runat="server" id="trtitle" class="panel-heading" onclick="showHideDiv('trgridsponsorship','span1');return false;">
                            <div class="row" id="trDetails" runat="server">
                                <div class="col-sm-10" style="text-align: left">
                                    <span class="glyphicon glyphicon-menu-hamburger"></span>
                                    <asp:Label ID="lblprospectsearch" runat="server" Text="CKYC Search Results"></asp:Label>
                                </div>
                                <div class="col-sm-2">
                                    <span id="span1" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                                </div>
                            </div>
                        </div>
                        <div id="trgridsponsorship" class="panel-body">

                            <asp:GridView ID="dgView" runat="server" AllowSorting="True" CssClass="footable" Width="100%"
                                AutoGenerateColumns="False" PageSize="10" AllowPaging="true" CellPadding="1" OnPageIndexChanging="dgView_PageIndexChanging"
                                OnSorting="dgView_Sorting" DataKeyNames="ConstitutionType" OnRowDataBound="dgView_RowDataBound" OnRowCreated="dgView_RowCreated" OnRowCommand="dgView_RowCommand">
                                <%-- --%>
                                <HeaderStyle HorizontalAlign="Center" BackColor="#dce9f9" />
                                <FooterStyle CssClass="GridViewFooter" />
                                <RowStyle CssClass="GridViewRow" />

                                <SelectedRowStyle CssClass="GridViewSelectedRow" />
                                <AlternatingRowStyle CssClass="GridViewAlternateRow"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr. No." ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="pad">
                                        <ItemTemplate><%#Container.DataItemIndex+1%></ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CKYC Reference No." HeaderStyle-CssClass="pad" ItemStyle-Width="20%" SortExpression="RegRefNo">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("RegRefNo") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>


                                    <%-- add new code CAS ID 02-01-2018--%>
                                    <asp:TemplateField HeaderText="FI Reference No." ItemStyle-Width="20%" SortExpression="FIRefNo" HeaderStyle-CssClass="pad">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFIRefNo" runat="server" Text='<%# Eval("FIRefNo") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="8%" />
                                    </asp:TemplateField>

                                    <%--End  CAS ID 02-01-2018--%>

                                    <asp:TemplateField HeaderText="Entity Name" ItemStyle-Width="20%" SortExpression="NAME" HeaderStyle-CssClass="pad">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNAME" runat="server" Text='<%# Eval("NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CKYC No." ItemStyle-Width="20%" SortExpression="KYC_NO" HeaderStyle-CssClass="pad">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKYCNo" runat="server" Text='<%# Eval("KYC_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CKYC Status" ItemStyle-Width="20%" SortExpression="cndStatus" HeaderStyle-CssClass="pad">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKYCSTATUS" runat="server" Text='<%# Eval("cndStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="12%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reg Date" ItemStyle-Width="20%" SortExpression="DATECREATED" HeaderStyle-CssClass="pad">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRegdt" runat="server" Text='<%# Eval("DATECREATED") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="15%" SortExpression="Request" Visible="False" HeaderStyle-CssClass="pad">
                                        <ItemTemplate>
                                            <div style="width: 20%; white-space: nowrap;">
                                                <span class="glyphicon glyphicon-flag"></span>
                                                <asp:LinkButton ID="lblshortview" runat="server" Text='Short Search' CommandName="Short"
                                                    CommandArgument='<%# Eval("RegRefNo") %>'></asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="15%" HeaderText="Action" HeaderStyle-CssClass="pad">
                                        <ItemTemplate>
                                            <div style="width: 20%; white-space: nowrap;">
                                                <span class="glyphicon glyphicon-flag"></span>
                                                <asp:LinkButton ID="lblview" runat="server" Text='Update Details' CommandName="View"
                                                    CommandArgument='<%# Eval("RegRefNo") %>'></asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                </Columns>
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
                                                <asp:Label ID="lblpageindx" CssClass="standardlabelCRM" Text="Page : " runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    </table>
                                </PagerTemplate>
                            </asp:GridView>
                            <br />

                            <br />
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
                    </div>
                </div>

                <table>
                    <tr>
                        <td>
                            <asp:HiddenField ID="hdnRefNo" runat="server" />
                        </td>
                        <td>
                            <asp:HiddenField ID="hdnKycNo" runat="server" />
                        </td>
                        <td></td>
                        <td>
                            <asp:HiddenField ID="hdnTrnReqDt" runat="server" />
                        </td>
                        <td></td>
                        <td>
                            <asp:HiddenField ID="hdnName" runat="server" />
                        </td>
                        <td>
                            <asp:HiddenField ID="hdnCandCode" runat="server" />
                        </td>
                        <td>
                            <asp:HiddenField ID="hdnRecruiterCode" runat="server" />
                        </td>
                        <td>
                            <asp:HiddenField ID="hdnMenuName" runat="server" />
                            <input type="hidden" id="hdnEmpName" runat="server" />
                        </td>
                        <%--Added by rachana on 22-08-2013 for branched user end--%>
                    </tr>
                </table>
            </div>
            <!-- Display Modal popup window in division -->
            <div class="modal fade" id="myModal" role="dialog">
            </div>
            <div class="modal" id="myModalRaise" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="padding-top: 0px;">
                <div class="modal-dialog" style="padding-top: 0px; margin-top: 2px; width: 95%;">
                    <div class="modal-content">
                        <div class="modal-header" style="margin-top: -10px; margin-bottom: -20px; padding-bottom: 6px ! important;">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="myModalLabel">CKYC QC</h4>
                        </div>
                        <div class="modal-body">

                            <iframe id="iframeCFR" src="" width="100%" height="300" frameborder="0" allowtransparency="true"></iframe>
                        </div>
                        <div class="modal-footer" style="display: none">
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>
            <!-- End Display Modal popup window in division -->

             <!-- Display Modal popup window in division -->
            <div class="modal fade" id="myModal_NEw" role="dialog">
            </div>
            <div class="modal" id="myModalRaise_NEw" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="padding-top: 0px;">
                <div class="modal-dialog" style="padding-top: 43px;margin-top: 2px;width: 95%;padding-left: 22px;padding-right: 2%;">
                    <div class="modal-content">
                        <div class="modal-header" style="margin-top: -10px; margin-bottom: -20px; padding-bottom: 6px ! important;">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="myModalLabel_New">CKYC Pending File Details </h4>
                        </div>
                        <div class="modal-body">
                            <iframe id="iframeCFR_New" src="" width="100%" height="300" frameborder="0" allowtransparency="true"></iframe>
                        </div>
                        <div class="modal-footer" style="display: none">
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>
            <!-- End Display Modal popup window in division -->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
