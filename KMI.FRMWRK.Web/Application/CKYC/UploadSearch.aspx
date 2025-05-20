<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="UploadSearch.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.CKYC.UploadSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/bundles/CKYCValidationjs") %>
    <%: Styles.Render("~/bundles/CKYCcss") %>
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

        function AllowOnlyNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
        }

    </script>
    <script type="text/javascript">
        function AlertMsg(msg) {
            debugger;
            showModal('#myModal', 'Information', 'alert-warning', '', '', msg);
        }

        function ValidateDate() {
            debugger;
            //if (Datetime.Parse(txtUploadFrm.text) > Datetime.Parse(txtUploadTo.text)) {

            //    AlertMsg('Please enter correct upload date to, Kindly contact to service provider.');
            //    return false;
            //}
            //else {
            //    return true;
            //}


            var fromDate = $('#<%=txtUploadFrm.ClientID %>').val();
            var toDate = $('#<%= txtUploadTo.ClientID %>').val();
            if ((fromDate != "" && toDate != "") && Date.parse(fromDate) > Date.parse(toDate)) {
                // alert("To date should be greater than From date.");
                AlertMsg('Please enter correct upload date to, Kindly contact to service provider.');
                return false;
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">

    <asp:ScriptManager ID="ScriptManager" runat="server">
    </asp:ScriptManager>

    <div class="page-container" style="margin-top: 0px;">
        <div class="panel  panel-success" style="margin-left: 2%; margin-right: 2%; margin-top: 0.5%">
            <div class="panel-heading" onclick="showHideDiv('trSearchDetails','btnToggle');return false;">
                <div class="row">
                    <div class="col-sm-10" style="text-align: left">
                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                        <asp:Label ID="lblTitle" runat="server" Font-Bold="False" Text="Bulk Upload Search"></asp:Label>
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
                            <asp:Label ID="lblBatchId" CssClass="control-label" runat="server" Text="Batch ID"></asp:Label>
                        </div>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtBatchid" runat="server" CssClass="form-control" onkeypress="return AllowOnlyNumber(event)" MaxLength="6"></asp:TextBox>
                        </div>
                        <div class="col-sm-3" style="text-align: left">
                            <asp:Label ID="lblUploadedBy" CssClass="control-label" runat="server" Text="Uploaded By"></asp:Label>
                        </div>
                        <div class="col-sm-3">
                             <asp:TextBox ID="txtUploadedBy" runat="server" CssClass="form-control" Text="Maker" Enabled="false" MaxLength="50"></asp:TextBox>

                            <%--<asp:DropDownList ID="ddlUserName" runat="server" AutoPostBack="true"
                                Enabled="false" CssClass="form-control">
                                <asp:ListItem Text="Checker" Value="checker" Selected="True"></asp:ListItem>
                            </asp:DropDownList>--%>
                        </div>
                    </div>

                    <div class="row" style="margin-bottom: 5px">
                        <div class="col-sm-3" style="text-align: left">
                            <asp:Label ID="lblUploadFrm" runat="server" CssClass="control-label" Text="Upload From"></asp:Label>
                        </div>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtUploadFrm" runat="server" CssClass="form-control" MaxLength="10" TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="col-sm-3" style="text-align: left">
                            <asp:Label ID="lblUploadTo" runat="server" CssClass="control-label" Text="Upload To"></asp:Label>
                        </div>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtUploadTo" runat="server" CssClass="form-control" MaxLength="10" TextMode="Date"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <center>
                            <div class="col-sm-12" style='margin-top: 15px;'>
                                <asp:Button ID="btnSearch" Text="Search" CssClass="btn-animated bg-green" runat="server" OnClick="btnSearch_Click"
                                     OnClientClick=" return ValidateDate(); return true;"> 
                                    <%-- OnClick="btnSearch_Click" --%>
                                </asp:Button>
                                <asp:Button ID="btnClear" CssClass="btn-animated bg-horrible" Text="Clear" OnClick="btnClear_Click" runat="server"> </asp:Button> 
                            
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
                        <asp:Label ID="Label2" runat="server" Text="Note: All dates are in (dd/mm/yyyy)"
                            ForeColor="Red"></asp:Label>
                    </div>
                    <div id="trRecord" runat="server" visible="false" colspan="6" style="height: 18px; text-align: center;">
                        <asp:Label ID="lblMessage" runat="server" CssClass="standardlabelErr"></asp:Label>
                    </div>
                </div>
            </div>
        </div>

        <div id="trDgViewDtl" runat="server" class="page-container" style="margin-top: 0px;" visible="false">
            <div class="panel panel-success" style='margin-right: 26px; margin-left: 26px;'>
                <div runat="server" id="trtitle" class="panel-heading" onclick="showHideDiv('trgridsponsorship','span1');return false;">
                    <div class="row" id="trDetails" runat="server">
                        <div class="col-sm-10" style="text-align: left">
                            <span class="glyphicon glyphicon-menu-hamburger"></span>
                            <asp:Label ID="lblprospectsearch" runat="server" Text="Bulk Upload Search Results"></asp:Label>
                        </div>
                        <div class="col-sm-2">
                            <span id="span1" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                        </div>
                    </div>
                </div>
                <div id="trgridsponsorship" class="panel-body">

                    <asp:GridView ID="dgUpldSearch" runat="server" AllowSorting="True" CssClass="footable" Width="100%"
                        AutoGenerateColumns="False" PageSize="10" AllowPaging="true" CellPadding="1" OnRowCreated="dgUpldSearch_RowCreated"
                        OnPageIndexChanging="dgUpldSearch_PageIndexChanging" OnRowCommand="dgUpldSearch_RowCommand" OnRowDataBound="dgUpldSearch_RowDataBound">
                        <HeaderStyle HorizontalAlign="Center" BackColor="#00c5cc" ForeColor="White" />
                        <FooterStyle CssClass="GridViewFooter" />
                        <RowStyle CssClass="GridViewRow" />
                        <SelectedRowStyle CssClass="GridViewSelectedRow" />
                        <AlternatingRowStyle CssClass="GridViewAlternateRow"></AlternatingRowStyle>
                        <Columns>

                            <asp:TemplateField HeaderText="Batch ID" ItemStyle-Width="20%" HeaderStyle-ForeColor="Black" SortExpression="BatchID">
                                <ItemTemplate>
                                    <asp:Label ID="lblBatchid" runat="server" Text='<%# Eval("BatchID") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="File Name" ItemStyle-Width="20%" HeaderStyle-ForeColor="Black" SortExpression="DocDesc">
                                <ItemTemplate>
                                    <asp:Label ID="lblFileName" runat="server" Text='<%# Eval("FileName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>

                              <%--Added by Prathamesh on 20180208 start--%>
                            <asp:TemplateField HeaderText="Created Date" ItemStyle-Width="20%" HeaderStyle-ForeColor="Black" SortExpression="Create Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblCreateDate" runat="server" Text='<%# Eval("Create Date") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>
                            <%--Added by Prathamesh on 20180208 end--%>

                            <asp:TemplateField HeaderText="No of Records Uploaded" ItemStyle-Width="20%" HeaderStyle-ForeColor="Black" SortExpression="NoofRecords">
                                <ItemTemplate>
                                    <asp:Label ID="lblNoofRecord" runat="server" Text='<%# Eval("NoofRecords") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Status" ItemStyle-Width="20%" HeaderStyle-ForeColor="Black" SortExpression="UpdStatus">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("UpdStatus") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Process Status" ItemStyle-Width="20%" HeaderStyle-ForeColor="Black" SortExpression="PrcType">
                                <ItemTemplate>
                                    <asp:Label ID="lblProcessStatus" runat="server" Text='<%# Eval("PrcType") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Success Count" ItemStyle-Width="20%" HeaderStyle-ForeColor="Black" SortExpression="SuccessCount">
                                <ItemTemplate>
                                    <asp:Label ID="lblSuccessCount" runat="server" Text='<%# Eval("SuccessCount") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Fail Count" ItemStyle-Width="20%" HeaderStyle-ForeColor="Black" SortExpression="FailCount">
                                <ItemTemplate>
                                    <asp:Label ID="lblFailCount" runat="server" Text='<%# Eval("FailCount") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-Width="15%" SortExpression="Request" HeaderStyle-ForeColor="Black" HeaderText="Action">
                                <ItemTemplate>
                                    <div style="width: 20%; white-space: nowrap;">
                                        <%-- <span class="glyphicon glyphicon-flag"></span>--%>
                                        <asp:LinkButton ID="lnkbtnerrorDWN" runat="server" Text='Download Error' ForeColor="Red" CommandName="DwnldError"
                                            CommandArgument='<%# Eval("Batchid")+","+ Eval("DocDesc") %>'></asp:LinkButton>
                                    </div>
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
                                                        CssClass="standardPagerDropdown">

                                                        <%--  OnSelectedIndexChanged="ddlPageSelectorL_SelectedIndexChanged"--%>
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
                                                        CssClass="standardPagerDropdown">
                                                    </asp:DropDownList><%-- OnSelectedIndexChanged="ddlPageSelectorR_SelectedIndexChanged"--%>
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

    <asp:HiddenField ID="hdnBatchid" runat="server" />
</asp:Content>
