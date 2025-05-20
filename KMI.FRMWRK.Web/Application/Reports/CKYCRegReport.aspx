<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="CKYCRegReport.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.Reports.CKYCRegReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/bundles/CKYCValidationjs") %>
    <%: Styles.Render("~/bundles/CKYCcss") %>

    <script>
        function callCalender() {
            debugger;
            var dateArr = $("#<%=txtDateFrom.ClientID%>").val().split('-');
            $("#<%= txtDateFrom.ClientID%>").datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd-mm-yy' });  //maxDate: new Date(),
            $.datepicker.initialized = true;
            $("#<%= txtDateFrom.ClientID%>").focus();

        }

        function callCalender1() {
            debugger;
            var dateArr = $("#<%=txtDateTo.ClientID%>").val().split('-');
            $("#<%= txtDateTo.ClientID%>").datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd-mm-yy' });    //minDate: new Date(),
            $.datepicker.initialized = true;
            $("#<%= txtDateTo.ClientID%>").focus();

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">
    <div class="panel panel-success" style="margin-left: 2%;margin-right: 2%;margin-top: 0.5%;">
        <div id="Div18" runat="server" class="panel-heading" onclick="showHideDiv('divCKYCdtls','btnCKYCdtls');return false;">
            <div class="row">
                <div class="col-sm-10" style="text-align: left">
                    <span class="glyphicon glyphicon-menu-hamburger"></span>
                    <asp:Label ID="Label6" Text="Registration Report Search Criteria" runat="server" CssClass="control-label">
                    </asp:Label>
                    <%--<span style="color: red">*</span>--%>
                </div>
                <div class="col-sm-2" style="text-align: right">
                    <asp:Label ID="Label3" Text="Version 1.6" runat="server" CssClass="control-label"></asp:Label>
                    <span id="btnCKYCdtls" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                </div>
            </div>
        </div>
        <div id="divCKYCdtls" style="display: block;" class="panel-body">
            <div class="row">
                <div class="col-sm-3" style="text-align: left">
                    <asp:Label ID="lblRegFrom" Text="Registration Date From" runat="server" CssClass="control-label"></asp:Label>
                    <span style="color: red">*</span>
                </div>

                <div class="col-sm-3">
                    <div class="input-group">
                        <asp:TextBox ID="txtDateFrom" placeholder="dd-mm-yyyy" runat="server" CssClass="form-control" MaxLength="10" TabIndex="2"></asp:TextBox>
                        <div class="input-group-btn">
                            <div class="btn btn-primary btn-lg-kmi" onclick="callCalender()">
                                <span class="glyphicon glyphicon-calendar BtnGlyphicon"></span>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-sm-3">
                    <asp:Label ID="lblRegTo" Text="Registration Date To" runat="server"  CssClass="control-label"></asp:Label>
                    <span style="color: red">*</span>
                </div>
                <div class="col-sm-3">
                    <div class="input-group">
                        <asp:TextBox ID="txtDateTo" placeholder="dd-mm-yyyy" runat="server" CssClass="form-control" MaxLength="10" TabIndex="2"></asp:TextBox>
                        <div class="input-group-btn">
                            <div class="btn btn-primary btn-lg-kmi" onclick="callCalender1()">
                                <span class="glyphicon glyphicon-calendar BtnGlyphicon"></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

             <div id="trShw" runat="server" class="row" style="margin-bottom: 1%;margin-top:1%;">
                       
                        <div class="col-sm-3" style="text-align: left">
                            <asp:Label ID="lblShwRecords" runat="server" CssClass="control-label" Text="Records shown per page"></asp:Label>
                        </div>
                        <div class="col-sm-3">
                            <asp:DropDownList ID="ddlShwRecrds" runat="server" AutoPostBack="true" CssClass="form-control"
                                OnSelectedIndexChanged="ddlShwRecrds_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <center>
                            <div class="col-sm-12">
                                <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click" CssClass="btn-animated bg-green" runat="server">   <%--OnClientClick="return validtab();"--%>
                                </asp:Button>
                                <asp:Button ID="btnClear" OnClick="btnClear_Click" CssClass="btn-animated bg-horrible" Text="Clear" runat="server">    </asp:Button>
                                <asp:Button ID="btnReFresh" runat="server" CssClass="btn btn-primary" Style="display: none;"
                                    ClientIDMode="Static" />
                                <asp:Button ID="btnExport" Text="Export To Excel" OnClick="btnExport_Click" CssClass="btn-animated bg-green" runat="server" Visible="false">   <%--OnClientClick="return validtab();"--%>
                                </asp:Button>
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
                    <div id="trRecord" runat="server" visible="false" colspan="6" style="text-align: center;">  <%--height: 18px; --%>
                        <asp:Label ID="lblMessage" runat="server" CssClass="standardlabelErr"></asp:Label>
                    </div>
        </div>
    </div>


     <div id="trDgViewDtl" runat="server">
            <div class="panel panel-success" style="margin-left: 2%;margin-right: 2%;margin-top: 0.5%;">
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
                        OnSorting="dgView_Sorting" OnRowDataBound="dgView_RowDataBound" OnRowCreated="dgView_RowCreated" >  <%--OnRowCommand="dgView_RowCommand"--%>
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
                            <asp:TemplateField HeaderText="Reference No" HeaderStyle-CssClass="pad" ItemStyle-Width="20%" SortExpression="RegRefNo">
                                <ItemTemplate>
                                    <asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("RegRefNo") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>


                            <%-- add new code CAS ID 02-01-2018--%>
                            <asp:TemplateField HeaderText="FI Reference No" ItemStyle-Width="20%" SortExpression="FIRefNo" HeaderStyle-CssClass="pad">
                                <ItemTemplate>
                                    <asp:Label ID="lblFIRefNo" runat="server" Text='<%# Eval("FIRefNo") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="8%" />
                            </asp:TemplateField>

                            <%--End  CAS ID 02-01-2018--%>

                            <asp:TemplateField HeaderText="Entity Name" ItemStyle-Width="20%" SortExpression="EntityName" HeaderStyle-CssClass="pad">
                                <ItemTemplate>
                                    <asp:Label ID="lblNAME" runat="server" Text='<%# Eval("EntityName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="15%" />
                            </asp:TemplateField>
                           <%-- <asp:TemplateField HeaderText="KYC No" ItemStyle-Width="20%" SortExpression="KYC_NO" HeaderStyle-CssClass="pad">
                                <ItemTemplate>
                                    <asp:Label ID="lblKYCNo" runat="server" Text='<%# Eval("KYC_NO") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="KYC Status" ItemStyle-Width="20%" SortExpression="cndStatus" HeaderStyle-CssClass="pad">
                                <ItemTemplate>
                                    <asp:Label ID="lblKYCSTATUS" runat="server" Text='<%# Eval("cndStatus") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="12%"/>
                            </asp:TemplateField>
                           <%-- <asp:TemplateField HeaderText="Reg Date" ItemStyle-Width="20%" SortExpression="DATECREATED" HeaderStyle-CssClass="pad">
                                <ItemTemplate>
                                    <asp:Label ID="lblRegdt" runat="server" Text='<%# Eval("DATECREATED") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>--%>
                            <%--<asp:TemplateField ItemStyle-Width="15%" SortExpression="Request" Visible="False" HeaderStyle-CssClass="pad">
                                <ItemTemplate>
                                    <div style="width: 20%; white-space: nowrap;">
                                        <span class="glyphicon glyphicon-flag"></span>
                                        <asp:LinkButton ID="lblshortview" runat="server" Text='Short Search' CommandName="Short"
                                            CommandArgument='<%# Eval("RegRefNo") %>'></asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>--%>
                            <%--<asp:TemplateField ItemStyle-Width="15%" HeaderText="Action" HeaderStyle-CssClass="pad">
                                <ItemTemplate>
                                    <div style="width: 20%; white-space: nowrap;">
                                        <span class="glyphicon glyphicon-flag"></span>
                                        <asp:LinkButton ID="lblview" runat="server" Text='View Details' CommandName="View"
                                            CommandArgument='<%# Eval("RegRefNo") %>'></asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>--%>
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

</asp:Content>
