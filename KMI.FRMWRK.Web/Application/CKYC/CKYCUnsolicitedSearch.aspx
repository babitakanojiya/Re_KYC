<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="CKYCUnsolicitedSearch.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.CKYC.CKYCUnsolicitedSearch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <%: Scripts.Render("~/bundles/CKYCValidationjs") %>
    <%: Styles.Render("~/bundles/CKYCcss") %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">
    <asp:ScriptManager ID="CKYCSearch" runat="server">
    </asp:ScriptManager>
    
     <div class="page-container" style="margin-top: 0px;">
        <div class="panel  panel-success" style="margin-left: 2%; margin-right: 2%; margin-top: 0.5%">
            <%-- <table class="container" width="80%">--%>
                <div class="panel-heading" onclick="showHideDiv('trSearchDetails','btnToggle');return false;"> <div class="row">
                    <div class="col-sm-10" style="text-align: left">
                    <span class="glyphicon glyphicon-menu-hamburger"></span>
                        <asp:Label ID="lblTitle" runat="server" Font-Bold="False" Text="CKYC Unsolicited File Search"></asp:Label>
                    </div>
                    <div class="col-sm-2">
                          <span id="btnToggle" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                    </div>
                </div>
            </div>
            <div id="trSearchDetails" class="panel-body">
                <div id="divSrvcReqReport1" style="display: block;" class="panel-body panel-collapse collapse in">
                    <div class="row">
                        <div class="col-sm-3" style="text-align: left">
                            <asp:Label ID="lblBatchId" CssClass="control-label" runat="server" Text="Batch ID"></asp:Label>
                        </div>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtBatchId" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                         
                        </div>
                        <div class="col-sm-3" style="text-align: left">
                            <asp:Label ID="lblKycNo" CssClass="control-label" runat="server" Text="KYC No"></asp:Label>
                        </div>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtKycNo" runat="server" CssClass="form-control" MaxLength="14"></asp:TextBox>
                        </div>
                    </div>
                    
                   
                       
                    <div id="trRecord" runat="server" visible="false" colspan="6" style="height: 18px;
                        text-align: center;">
                        <asp:Label ID="lblMessage" runat="server" CssClass="standardlabelErr"></asp:Label>
                    </div> </div>
                    
                    <div class="row">
                        <center>
                            <div class="col-sm-12" style='margin-top: 15px;'>
                                <asp:Button ID="btnSearch" OnClick="btnSearch_Click" Text="Search"
                                    CssClass="btn-animated bg-green" runat="server"> 
                                </asp:Button>
                                <asp:Button ID="btnClear" OnClick="btnClear_Click" Text="Clear"
                                    CssClass="btn-animated bg-horrible" runat="server">
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
             
            </div>
            </div>
       
        <div id="trDgViewDtl" runat="server" class="page-container" style="margin-top: 0px;">
            <div class="panel panel-success" style='margin-right: 26px; margin-left: 26px;'>
                <div id="trtitle" runat="server" class="panel-heading" style='margin-left: 0px; margin-right: 0px;'
                    onclick="showHideDiv('trgridUnsolicited','span1');return false;">
                    <div class="row" id="trDetails" runat="server">
                          <div class="col-sm-10" style="text-align: left">
                            <span class="glyphicon glyphicon-menu-hamburger"></span>
                            <asp:Label ID="lblprospectsearch" runat="server" Text="CKYC Unsolicited File Search Results"></asp:Label>
                        </div>
                           <div class="col-sm-2">
                            <span id="span1" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                        </div>
                    </div>
                </div>
                <div id="trgridUnsolicited"  class="panel panel-success" style='margin-right: 0px; 
                    margin-left: 0px; display: block; overflow:auto;width:100%;'>
                    
                    <asp:GridView ID="dgView" runat="server" AllowSorting="True" CssClass="footable" Width="100%"
                        AutoGenerateColumns="False" PageSize="10" AllowPaging="true" CellPadding="1" OnRowCreated="dgView_RowCreated" OnPageIndexChanging="dgView_PageIndexChanging"
                        OnSorting="dgView_Sorting"  >
                   <HeaderStyle HorizontalAlign="Center" BackColor="#dce9f9" />
                        <FooterStyle CssClass="GridViewFooter" />
                        <RowStyle CssClass="GridViewRow" />

                        <SelectedRowStyle CssClass="GridViewSelectedRow" />
                        <AlternatingRowStyle CssClass="GridViewAlternateRow"></AlternatingRowStyle>
                        <Columns>
                   
                              <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate><%#Container.DataItemIndex+1%></ItemTemplate>
                             <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FI Code" ItemStyle-Width="20%" SortExpression="FICode">
                                <ItemTemplate>
                                    <asp:Label ID="lblFICode" runat="server" Text='<%# Eval("FICode") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="KYC No" ItemStyle-Width="20%" SortExpression="KYCNo">
                                <ItemTemplate>
                                    <asp:Label ID="lblKYCNo" runat="server" Text='<%# Eval("KYCNo") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Batch Id" ItemStyle-Width="20%" SortExpression="BatchId">
                                <ItemTemplate>
                                    <asp:Label ID="lblbatchid" runat="server" Text='<%# Eval("BatchId") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name" ItemStyle-Width="20%" SortExpression="Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Account Type" ItemStyle-Width="20%" SortExpression="AccType">
                                <ItemTemplate>
                                    <asp:Label ID="lblAccountType" runat="server" Text='<%# Eval("AccType") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Name Updated Flag" ItemStyle-Width="20%" SortExpression="NameUpdFlag">
                                <ItemTemplate>
                                    <asp:Label ID="lblNameUpdatedFlag" runat="server" Text='<%# Eval("NameUpdFlag") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Personal Updated Flag" ItemStyle-Width="20%" SortExpression="PerDtlsUpdFlag">
                                <ItemTemplate>
                                    <asp:Label ID="lblPersonalUpdatedFlag" runat="server" Text='<%# Eval("PerDtlsUpdFlag") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Address Updated Flag" ItemStyle-Width="20%" SortExpression="AddrDtlsUpdFlag">
                                <ItemTemplate>
                                    <asp:Label ID="lblAddressUpdatedFlag" runat="server" Text='<%# Eval("AddrDtlsUpdFlag") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Contact Updated Flag" ItemStyle-Width="20%" SortExpression="CnctDtlsUpdFlag">
                                <ItemTemplate>
                                    <asp:Label ID="lblContactUpdatedFlag" runat="server" Text='<%# Eval("CnctDtlsUpdFlag") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="KYC Updated Flag" ItemStyle-Width="20%" SortExpression="OthrDtlsUpdFlag">
                                <ItemTemplate>
                                    <asp:Label ID="lblKYCUpdatedFlag" runat="server" Text='<%# Eval("OthrDtlsUpdFlag") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Identity Updated Flag" ItemStyle-Width="20%" SortExpression="IdDtlsUpdFlag">
                                <ItemTemplate>
                                    <asp:Label ID="lblIdentityUpdatedFlag" runat="server" Text='<%# Eval("IdDtlsUpdFlag") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Related Person Updated Flag" ItemStyle-Width="20%" SortExpression="RltdDtlsUpdFlag">
                                <ItemTemplate>
                                    <asp:Label ID="lblRelPersonUpdatedFlag" runat="server" Text='<%# Eval("RltdDtlsUpdFlag") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Image Updated Flag" ItemStyle-Width="20%" SortExpression="ImgUpdFlag">
                                <ItemTemplate>
                                    <asp:Label ID="lblImageUpdatedFlag" runat="server" Text='<%# Eval("ImgUpdFlag") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="KYC Deactivation Flag" ItemStyle-Width="20%" SortExpression="KYCDeactivationFlag">
                                <ItemTemplate>
                                    <asp:Label ID="lblKYCDeactivationFlag" runat="server" Text='<%# Eval("KYCDeactivationFlag") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="KYC Deactivation Remarks" ItemStyle-Width="20%" SortExpression="KYCDeactivationRemarks">
                                <ItemTemplate>
                                    <asp:Label ID="lblKYCDeactivationRemarks" runat="server" Text='<%# Eval("KYCDeactivationRemarks") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Passport Number Expiry" ItemStyle-Width="20%" SortExpression="PassportNoExpiry">
                                <ItemTemplate>
                                    <asp:Label ID="lblPassportNumberExpiry" runat="server" Text='<%# Eval("PassportNoExpiry") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Driving License Expiry" ItemStyle-Width="20%" SortExpression="DrivingNoExpiry">
                                <ItemTemplate>
                                    <asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("DrivingNoExpiry") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
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
        
         
    </div>
</asp:Content>
