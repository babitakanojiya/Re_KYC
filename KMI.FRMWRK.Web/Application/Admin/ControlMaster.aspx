<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="ControlMaster.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.Admin.ControlMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Styles.Render("~/bundles/CKYCcss") %>
    <link href="../../assets/css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../assets/js/bootstrap-multiselect.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">
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

        .ui-menu {
            position: fixed !important;
        }

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

        .custom-map {
            margin-bottom: 11px !important;
        }

        .control-label {
            margin-bottom: 8px !important;
        }

        .radio-list [type='radio'] {
            margin: 10px !important;
        }

        .check-list [type='checkbox'] {
            margin: 10px !important;
        }

        .form-control, .input-group-addon, .panel, .panel-header, .panel-body {
            border-radius: 0px !important;
        }

        .btn {
            border-radius: 0px !important;
        }

        .glyphicon-eye-open, .glyphicon-eye-close {
            cursor: pointer;
        }

        .AlignCenter {
            text-align: center !important;
            word-break: break-word;
        }
    </style>
    <asp:ScriptManager ID="scrusdtls" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="page-container">
                <div class="panel panel-success" style="margin-left: 2%; margin-right: 2%; margin-top: 0.5%">
                    <div class="panel-heading" onclick="showHideDiv('divCustomerAdd','btnToggleNew');return false;">
                        <div class="row" style="margin: 0px">
                            <div class="col-sm-10" style="text-align: left">
                                <asp:Label ID="Label1" runat="server" Font-Bold="true" Font-Size="18px" Text="Control Setup Search"></asp:Label>
                            </div>
                            <div class="col-sm-2">
                                <span id="btnToggleNew" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div>
                    <div class="panel-body" id="divCustomerAdd">
                        <div class="container-fluid">
                            <div class="row custom-map">
                                <div class="col-sm-3">
                                    <span class="control-label">Segment
                                        <span style="color: red; vertical-align: baseline">*</span>
                                    </span>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlSegment">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-3">
                                    <span class="control-label">Control Name
                                        <span style="color: red; vertical-align: baseline">*</span>
                                    </span>
                                    <asp:TextBox runat="server" ID="txtControlName" CssClass="form-control" />
                                </div>
                                <div class="col-sm-3">
                                    <span class="control-label">Control ID
                                        <span style="color: red; vertical-align: baseline">*</span>
                                    </span>
                                    <asp:TextBox runat="server" ID="txtControlID" ClientIDMode="Static" CssClass="form-control" />
                                </div>
                                <div class="col-sm-3">
                                    <span class="control-label">Control Type
                                        <span style="color: red; vertical-align: baseline">*</span>
                                    </span>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlControlType">
                                        <asp:ListItem Text="text1" />
                                        <asp:ListItem Text="text2" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row custom-map">
                                <div class="col-sm-3">
                                    <span class="control-label">Is Visible
                                        <span style="color: red; vertical-align: baseline">*</span>
                                    </span>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlIsVisible">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-3">
                                    <span class="control-label">Is Master
                                        <span style="color: red; vertical-align: baseline">*</span>
                                    </span>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlIsMaster">
                                        <asp:ListItem Text="text1" />
                                        <asp:ListItem Text="text2" />
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-3">
                                    <span class="control-label">Is Active
                                        <span style="color: red; vertical-align: baseline">*</span>
                                    </span>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlActive">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row" style="padding: 10px 15px">
                                <center>
                                   <div>
                                        <asp:Button Text="Search" runat="server" CssClass="btn btn-primary" ID="btnSearch" OnClick="btnSearch_Click" />
                                        <asp:Button Text="Clear" runat="server" CssClass="btn btn-danger" ID="btnClear" OnClick="btnClear_Click" />
                                         <asp:Button Text="Add New" runat="server" CssClass="btn btn-primary" ID="btnAdd" OnClick="btnAdd_Click" />
                                    </div>
                                </center>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="page-container">
                <div class="panel panel-success" style="margin-left: 2%; margin-right: 2%; margin-top: 0.5%">
                    <div class="panel-heading" onclick="showHideDiv('divGrid','btnToggleGrid');return false;">
                        <div class="row" style="margin: 0px">
                            <div class="col-sm-10" style="text-align: left">
                                <asp:Label ID="Label2" runat="server" Font-Bold="true" Font-Size="18px" Text="Search Result"></asp:Label>
                            </div>
                            <div class="col-sm-2">
                                <span id="btnToggleGrid" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div>
                    <div class="panel-body" id="divGrid" style="display: block; overflow: auto;">
                        <asp:GridView runat="server" ID="gvControl" PageSize="10" CssClass="footable"
                            AllowPaging="True" AutoGenerateColumns="false" AllowSorting="True" EmptyDataText="No records has been added."
                            OnRowCreated="gvControl_RowCreated" OnPageIndexChanging="gvControl_PageIndexChanging" OnSorting="gvControl_Sorting">
                            <HeaderStyle ForeColor="Black" />
                            <Columns>
                                <asp:TemplateField HeaderText="SeqNo" HeaderStyle-CssClass="AlignCenter" SortExpression="SeqNo">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSeqNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" CssClass="AlignCenter" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Segment" HeaderStyle-CssClass="AlignCenter" SortExpression="SegmentName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSegment" runat="server" Text='<%# Eval("SegmentName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" CssClass="AlignCenter" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Control Name" HeaderStyle-CssClass="AlignCenter"  SortExpression="SegmentName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblControlName" runat="server" Text='<%# Eval("ControlName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" CssClass="AlignCenter" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Control Id" HeaderStyle-CssClass="AlignCenter"  SortExpression="ControlId">
                                    <ItemTemplate>
                                        <asp:Label ID="lblControlId" runat="server" Text='<%# Eval("ControlId") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" CssClass="AlignCenter" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Constitution Type" HeaderStyle-CssClass="AlignCenter"  SortExpression="ConstitutionType">
                                    <ItemTemplate>
                                        <asp:Label ID="lblConstitutionType" runat="server" Text='<%# Eval("ConstitutionType") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" CssClass="AlignCenter" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Is Mandatory" HeaderStyle-CssClass="AlignCenter"  SortExpression="IsMandate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIsMandate" runat="server" Text='<%# Eval("IsMandate") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" CssClass="AlignCenter" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Is Master" HeaderStyle-CssClass="AlignCenter"  SortExpression="IsMaster">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIsMaster" runat="server" Text='<%# Eval("IsMaster") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" CssClass="AlignCenter" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Is Visible" HeaderStyle-CssClass="AlignCenter"  SortExpression="IsVisible">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIsVisible" runat="server" Text='<%# Eval("IsVisible") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" CssClass="AlignCenter" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="AlignCenter">
                                    <ItemTemplate>
                                        <asp:LinkButton Text="Edit" ForeColor="Black" CommandArgument='<%# Eval("RecId") %>' ID="lnkEdit" OnCommand="lnkEdit_Command" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" CssClass="AlignCenter" />
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
                            </PagerTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
