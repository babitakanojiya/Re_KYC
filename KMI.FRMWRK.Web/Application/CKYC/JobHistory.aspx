<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="JobHistory.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.CKYC.JobHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/bundles/CKYCValidationjs") %>
    <%: Styles.Render("~/bundles/CKYCcss") %>


    <style type="text/css">
        .container {
            width: 1300px !important;
        }

        .pad {
            text-align: center !important;
            padding: 10px;
        }

        .left {
            text-align: left !important;
        }

        .standardcheckbox {
            /*display: inline-flex !important;*/
        }

        input[type=checkbox], input[type=radio] {
            /*margin: 4px 0 0 !important;*/
            /*margin-top: 1px\9;    */
            line-height: normal !important;
            margin-right: 4px !important;
        }

        .custom-table tbody tr:first-child th {
            padding: 10px;
            color: #000;
            background-color: #f2d18a;
            text-align: left;
        }

        .custom-table tbody td {
            padding: 20px 10px;
            color: #000;
            text-align: left;
        }

        .custom-table tbody tr:not(:last-child):nth-child(2n) {
            background-color: #f6f6f6;
        }

        .pagination .current {
            background: #26B;
            color: #fff;
            border: solid 1px #AAE;
        }

        .custom-pagination {
            border-top: 0.5px solid #eee;
        }

            .custom-pagination > td {
                border: none;
                padding: 10px !important;
            }

            .custom-pagination table {
                width: auto;
                margin: 1px auto;
                border-collapse: separate;
                border-spacing: 10px;
            }

                .custom-pagination table td {
                    text-align: center;
                    padding: 5px;
                }

                    .custom-pagination table td span {
                        padding: 7px 11px;
                        border: none;
                        text-decoration: none;
                        background-color: #054da2;
                        color: #fff;
                    }

                    .custom-pagination table td a {
                        padding: 7px 11px;
                        border: 0.1px solid #ccc;
                        text-decoration: none;
                        background-color: black;
                        color: #000;
                    }

        a {
            color: black;
        }
        /*Added byn Rutuja content 27jul2021*/


        .form-control, .btn-group > .btn {
            border-radius: 0px !important;
        }

        .control-label {
            font-weight: normal;
            font-size: 14px;
            color: #969595;
        }

        .custom-radio > tbody > tr > td {
            padding: 0px 10px 0px 2px;
        }

            .custom-radio > tbody > tr > td > label {
                vertical-align: inherit;
                font-size: 15px;
                font-weight: normal;
            }

        .input-group-addon {
            background: #fff;
            border-radius: 0px;
        }
    </style>

    <script>

        function AlertMsg(msg) {
            showModal('#myModal', 'Alert', 'alert-warning', '', '', msg);
        }

        function OpenImgWindow(Flag1) {
            debugger;
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", "window.open('../../Application/CKyc_Image.aspx?','_blank', 'toolbar=yes,scrollbars=yes,resizable=yes,top=10,left=10,width=200,height=300')", true);
            window.open("CKyc_Image.aspx?", '', 'width=640,height=354,toolbar=no,scrollbars=yes,resizable=yes,left=300,top=230,location=0;');
        }


        function StateCity(input) {
            debugger;
            var displayIcon = "img" + input;
            if ($("#" + displayIcon).hasClass("glyphicon-plus")) {
                $("#" + displayIcon).closest("tr")
                    .after("<tr><td></td><td colspan = '100%'>" + $("#" + input)
                        .html() + "</td></tr>");
                $("#" + displayIcon).removeClass("glyphicon-plus").addClass("glyphicon-minus");
            } else {
                $("#" + displayIcon).closest("tr").next().remove();
                $("#" + displayIcon).removeClass("glyphicon-minus").addClass("glyphicon-plus");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">

    <asp:ScriptManager ID="ExecutionHst" runat="server">
    </asp:ScriptManager>

    <div class="container" style="margin-top: 0px; width: 100%;">
        <div class="page-container" style="margin-top: 0px;">
            <div class="panel  panel-success" style="margin-left: 2%; margin-right: 2%; margin-top: 0.5%">

                <div class="panel-heading" onclick="showHideDiv('trSearchDetails','btnToggle');return false;">
                    <div class="row">
                        <div class="col-sm-10" style="text-align: left">
                            <span class="glyphicon glyphicon-menu-hamburger"></span>
                            <asp:Label ID="lblTitle" runat="server" Font-Bold="False" Text="JOB EXECUTION HISTORY"></asp:Label>
                        </div>
                        <div class="col-sm-2">
                            <span id="btnToggle" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                        </div>
                    </div>
                </div>

                <div id="trSearchDetails" class="panel-body">
                    <div id="divSrvcReqReport1" style="display: block;">
                        <%--<div class="row">
                            <div class="col-sm-8"></div>
                            <div class="col-sm-4" style="display: flex; justify-content: flex-end">
                                <span class="control-label" style="margin-right: 10px; font-weight: 600;">Mode : </span>

                            </div>
                        </div>--%>
                        <div class="row">
                            <div class="col-sm-2">
                                <span class="control-label">Job Name</span>
                                <br />
                                <asp:DropDownList runat="server" ID="ddlJobType" AutoPostBack="false" CssClass="form-control" Width="125%">
                                    <asp:ListItem Text="Select" Selected="False" />
                                    <asp:ListItem Text="Periodic Response" Value="1" />
                                    <asp:ListItem Text="Processing Response" Value="2" />
                                    <asp:ListItem Text="ZIP Generation" Value="3" />
                                    <asp:ListItem Text="Data Validation" Value="4" />
                                    <asp:ListItem Text="FTP/SFTP" Value="6" />
                                    <asp:ListItem Text="All Jobs" Value="5" />

                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="row">
                            <center>
                            <div class="col-sm-12" style='margin-top: 15px;'>
                                <asp:Button ID="btnSearch" Text="Search" CssClass="btn-animated bg-green" OnClick="btnSearch_Click" runat="server">
                                </asp:Button>
                                <asp:Button ID="btnClear" CssClass="btn-animated bg-horrible" Text="Clear" OnClick="btnClear_Click" runat="server">    
                                </asp:Button>
                            </div>
                        </center>
                        </div>
                    </div>
                </div>
            </div>
            <div id="JobDiv" runat="server" class="page-container" style="margin-top: 0px; display: none">
                <div class="panel  panel-success" runat="server" style="margin-left: 2%; margin-right: 2%; margin-top: 0.5%;">
                    <div class="panel-heading" onclick="showHideDiv('trSearchDetailsGrid','btnToggleGrid');return false;">
                        <div class="row">
                            <div class="col-sm-10" style="text-align: left">
                                <span class="glyphicon glyphicon-menu-hamburger"></span>
                                <asp:Label ID="Label1" runat="server" Font-Bold="False" Text="JOB EXECUTION HISTORY DETAILS"></asp:Label>
                            </div>
                            <div class="col-sm-2">
                                <span id="btnToggleGrid" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div>
                    <div id="trSearchDetailsGrid" class="panel-body">

                        <asp:GridView ID="dgView" runat="server" Width="100%" CssClass="footable" EmptyDataText="No record found"
                            AutoGenerateColumns="False" PageSize="10" AllowPaging="true" CellPadding="1" OnPageIndexChanging="dgView_PageIndexChanging">

                            <HeaderStyle HorizontalAlign="Center" BackColor="#dce9f9" />
                            <FooterStyle CssClass="GridViewFooter" />
                            <RowStyle CssClass="GridViewRow" />

                            <SelectedRowStyle CssClass="GridViewSelectedRow" />
                            <AlternatingRowStyle CssClass="GridViewAlternateRow"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="Job Name" HeaderStyle-CssClass="pad" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblJobName" runat="server" Text='<%# Eval("JobName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="left" HorizontalAlign="Center" Width="15%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Job Start DateTime" SortExpression="FIRefNo" ItemStyle-Width="20%" HeaderStyle-CssClass="pad">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStartDate" runat="server" Text='<%# Eval("StartDate","{0:dd/MM/yyyy hh:mm:ss tt}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="15%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Job End DateTime" HeaderStyle-CssClass="pad" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEndDate" runat="server" Text='<%# Eval("EndDate","{0:dd/MM/yyyy hh:mm:ss tt}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="15%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Job Run Duration (Sec)" HeaderStyle-CssClass="pad" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDuration" runat="server" Text='<%# Eval("Duration") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="15%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Start Count" HeaderStyle-CssClass="pad" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStrtCount" runat="server" Text='<%# Eval("StartCount") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="15%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="End Count" HeaderStyle-CssClass="pad" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEndCount" runat="server" Text='<%# Eval("EndCount") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="15%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="pad" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="15%" />
                                </asp:TemplateField>

                            </Columns>

                        </asp:GridView>
                        <div class="col-sm-3" style="text-align: left" style="display: none">
                            <asp:Label ID="lblPageInfo" runat="server" Visible="false"></asp:Label>
                        </div>
                        <div id="trRecord" runat="server" visible="false" colspan="6" style="height: 18px; text-align: center;">
                            <asp:Label ID="lblMessage" runat="server" CssClass="standardlabelErr"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        function BindDatePicker(id) {
            $('#' + id).datepicker()
        }
    </script>
</asp:Content>
