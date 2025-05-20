<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="ExecutionHistory.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.CKYC.ExecutionHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/bundles/CKYCValidationjs") %>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <%: Styles.Render("~/bundles/CKYCcss") %>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">

    <style type="text/css">

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
                        background-color: #fff;
                        color: #000;
                    }

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
    <style type="text/css">
        .loader {
            position: fixed;
            width: 100%;
            left: 0;
            right: 0;
            top: 0;
            bottom: 0;
            background-color: rgba(255,255,255,0.7);
            z-index: 9999;
            margin: auto;
            padding: 10px;
            /*display:none;*/
        }

            .loader::after {
                /*content:'';*/
                /*display:block;*/
                position: absolute;
                left: 0%;
                top: 0%;
                width: 100vw;
                height: 50vh;
                border-style: solid;
                border-color: black;
                border-top-color: transparent;
                border-width: 4px;
                border-radius: 50%;
                /*-webkit-animation: spin .8s linear infinite;
    animation: spin .8s linear infinite;*/
            }
        /*  .imglder
        {
            margin-bottom:10%;
        }*/
    </style>

    <script>

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

    <asp:ScriptManager ID="ExecutionHst" runat="server">
    </asp:ScriptManager>
<%--    <asp:UpdatePanel runat="server">
        <ContentTemplate>--%>
    <div class="page-container" style="margin-top: 0px;">
                                        <%-- Added By Megha Bhave 25.03.2021 --%>
            <div id="dvProgressBar" style="display: none; text-align: center" class="loader">
                <center>
                         <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /> <br /><br /><br /><br /><br />
                          <asp:Image id="ldr" src="../../Images/horizonal_loader.gif"   height="50px" alt="" runat="server" ImageAlign="Middle"/>
                         <br />
                      <asp:Label ID="lblMsg" Text="" runat="server" ForeColor="Blue" style="font-size: medium; font-weight:bold" > </asp:Label>
                
            </center>
            </div>
            <%-- Ended By Megha Bhave 25.03.2021 --%>
            <div class="panel  panel-success" style="margin-left: 2%; margin-right: 2%; margin-top: 0.5%">

                <div class="panel-heading" onclick="showHideDiv('trSearchDetails','btnToggle');return false;">
                    <div class="row">
                        <div class="col-sm-10" style="text-align: left">
                            <span class="glyphicon glyphicon-menu-hamburger"></span>
                            <asp:Label ID="lblTitle" runat="server" Font-Bold="False" Text="EXECUTION HISTORY"></asp:Label>
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
                                <span class="control-label">Search Type</span>
                                <asp:DropDownList runat="server" ID="ddlActType" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlActType_SelectedIndexChanged">
                                    <%--<asp:ListItem Text="Select" Value="0" />--%>
                                    <asp:ListItem Text="Based on FI Reference No" Value="1" />
                                    <asp:ListItem Text="Based on Batch ID" Value="2" />
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-2">
                                <span class="control-label" id="lblSearchText" runat="server"></span>
                                <asp:TextBox runat="server" ID="txtSearchNo" CssClass="form-control" />
                            </div>
                            <div class="col-sm-2">
                                <span class="control-label">Duration</span>
                                <br />
                                <div class="btn-group" role="group">
                                    <asp:Button Text="1D" runat="server" class="btn btn-default" OnClick="btnDay_Click" ID="btnDay"  OnClientClick="ShowProgressBar('Searching..Please wait')"  />
                                    <asp:Button Text="1W" runat="server" class="btn btn-default" OnClick="btnWeek_Click" ID="btnWeek"  OnClientClick="ShowProgressBar('Searching..Please wait')" />
                                    <asp:Button Text="1M" runat="server" class="btn btn-default" OnClick="btnMonth_Click" ID="btnMonth"  OnClientClick="ShowProgressBar('Searching..Please wait')"  />
                                    <asp:Button Text="1Q" runat="server" class="btn btn-default" OnClick="btnQuarter_Click" ID="btnQuarter"   OnClientClick="ShowProgressBar('Searching..Please wait')" />
                                </div>
                            </div>
                            <div class="col-sm-2">
                                <span class="control-label">Start Date</span>
                                <div class="input-group">
                                    <asp:TextBox runat="server" ID="txtStartDate" onmousedown="BindDatePicker(this.id)" CssClass="form-control" />
                                    <span class="input-group-addon" id="basic-addon1">
                                        <i class="glyphicon glyphicon-calendar"></i>
                                    </span>
                                </div>
                            </div>
                            <div class="col-sm-2">
                                <span class="control-label">End Date</span>
                                <div class="input-group">
                                    <asp:TextBox runat="server" ID="txtEndDate" onmousedown="BindDatePicker(this.id)" CssClass="form-control" />
                                    <span class="input-group-addon" id="basic-addon2">
                                        <i class="glyphicon glyphicon-calendar"></i>
                                    </span>
                                </div>
                            </div>
                            <div class="col-sm-2">
                                <div style="margin-top: 25px">
                                    <asp:RadioButtonList runat="server" ID="rdoState" CssClass="custom-radio" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="Active" Value="1" />
                                        <asp:ListItem Text="Archive" Value="2" />
                                    </asp:RadioButtonList>
                                </div>
                            </div>

                        </div>
                        <br />
                        <div id="trnote" runat="server" class="col-sm-12" style="margin-bottom: 5px; text-align: center;">
                                <asp:Label ID="Label2" runat="server" Text="Note: If data is not available please check in Archive"
                                    ForeColor="Red"></asp:Label>
                            </div>
                        <div class="row">
                            <center>
                            <div class="col-sm-12" style='margin-top: 15px;'>
                                <asp:Button ID="btnSearch" Text="Search" CssClass="btn-animated bg-green" OnClick="btnSearch_Click" OnClientClick="ShowProgressBar('Searching..Please wait')"  runat="server">
                                </asp:Button>
                                <asp:Button ID="btnClear" CssClass="btn-animated bg-horrible" Text="Clear" runat="server" OnClick="btnClear_Click">    
                                </asp:Button>
                            </div>
                        </center>
                        </div>
                    </div>
                </div>
            </div>
            <div id="trDgViewDtl" runat="server" class="page-container" style="margin-top: 0px;">
            <div class="panel  panel-success" style="margin-left: 2%; margin-right: 2%; margin-top: 0.5%;" runat="server">
                <div class="panel-heading" onclick="showHideDiv('trSearchDetailsGrid','btnToggleGrid');return false;" runat="server">
                    <div class="row" runat="server">
                        <div class="col-sm-10" style="text-align: left">
                            <span class="glyphicon glyphicon-menu-hamburger"></span>
                            <asp:Label ID="Label1" runat="server" Font-Bold="False" Text="SEARCH RESULT"></asp:Label>
                        </div>
                        <div class="col-sm-2">
                            <span id="btnToggleGrid" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                        </div>
                    </div>
                </div>
                <div id="trSearchDetailsGrid" class="panel-body" style="padding: 0px" runat="server">

                    <asp:GridView ID="dgView" runat="server" Width="100%" CssClass="custom-table"
                        AutoGenerateColumns="False" PageSize="10" AllowPaging="true" CellPadding="1"
                        BorderStyle="None" GridLines="None" DataKeyNames="FiRefNo"
                        OnPageIndexChanging="dgView_PageIndexChanging"
                        OnRowDataBound="dgView_RowDataBound">
                        <PagerStyle CssClass="custom-pagination" />
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="20px">
                                <ItemTemplate>
                                    <a href="JavaScript:StateCity('div<%# Eval("FiRefNo") %>');">
                                        <i class="glyphicon glyphicon-plus" id="imgdiv<%# Eval("FiRefNo") %>" style="color: black"></i>
                                    </a>
                                    <div id="div<%# Eval("FiRefNo") %>" style="display: none;">
                                        <asp:GridView ID="dgInnerDiv" runat="server" Width="100%" CssClass="custom-table"
                                            AutoGenerateColumns="False" AllowPaging="false" CellPadding="1" BorderStyle="None" GridLines="None">
                                            <PagerStyle CssClass="custom-pagination" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr. No." ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate><%#Container.DataItemIndex+1%></ItemTemplate>
                                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FI Ref No." ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFiRefNo" runat="server" Text='<%# Eval("FIRefNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDate" runat="server" Text='<%# Eval("LogDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Activity Type" SortExpression="FIRefNo" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblActionType" runat="server" Text='<%# Eval("ActivityType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Activity Description" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblActionDesc" runat="server" Text='<%# Eval("ActivityDesc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Integration Mode" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIntgMode" runat="server" Text='<%# Eval("IntgMode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <center>
                                                  <asp:LinkButton runat="server" ID="btnViewReg" ForeColor="BlueViolet">
                                                  <img src="../../Image/view_icon.png" />
                                                     <br />
                                                       View Details
                                                      </asp:LinkButton>
                                                         </center>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Source Destination" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                <asp:Label ID="SrcDes" runat="server" Text='<%# Eval("SourceDestination") %>'></asp:Label>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="CommandBtn" runat="server" ForeColor="Blue" Text="View Photo" OnClick="CommandBtn_Click1" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sr. No." ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate><%#Container.DataItemIndex+1%></ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" />
                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FI Ref No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFiRefNo" runat="server" Text='<%# Eval("FIRefNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text='<%# Eval("LogDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Activity Type" SortExpression="FIRefNo">
                                <ItemTemplate>
                                    <asp:Label ID="lblActionType" runat="server" Text='<%# Eval("ActivityType") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Activity Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblActionDesc" runat="server" Text='<%# Eval("ActivityDesc") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Integration Mode">
                                <ItemTemplate>
                                    <asp:Label ID="lblIntgMode" runat="server" Text='<%# Eval("IntgMode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Source Destination">
                                <ItemTemplate>
                                    <asp:Label ID="lblSrcDesti" runat="server" Text='<%# Eval("SourceDestination") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblError" runat="server" Text='<%# Eval("ErrorStatus") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Action" Visible="false">
                                <ItemTemplate>
                                    <center>
                                    <asp:LinkButton runat="server" ID="btnViewReg" ForeColor="BlueViolet" OnClick="btnViewReg_Click1" >
                                        <img src="../../Image/view_icon.png" />
                                        <br />
                                        View Details
                                    </asp:LinkButton>
                                        </center>
                                </ItemTemplate>
                            </asp:TemplateField>   

                        </Columns>
                    </asp:GridView>

                    <asp:GridView ID="dgBatchView" runat="server" Width="100%" CssClass="custom-table"
                        AutoGenerateColumns="False" PageSize="10" AllowPaging="true" CellPadding="1"
                        BorderStyle="None" GridLines="None" DataKeyNames="BatchId, FiRefNo, ActivityType, ErrorStatus"
                        OnPageIndexChanging="dgBatchView_PageIndexChanging" OnRowCommand="dgBatchView_RowCommand"
                        OnRowDataBound="dgBatchView_RowDataBound">
                        <PagerStyle CssClass="custom-pagination" />
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="20px">
                                <ItemTemplate>
                                    <div runat="server" id="divContainer">

                                        <%--<asp:LinkButton runat="server" ID="lnkToggle" href="JavaScript:void(0)" OnClientClick="StateCity('div<%# Eval("BatchId") %>'); return false;">
                                        <i class="glyphicon glyphicon-plus" id="imgdiv<%# Eval("BatchId") %>" style="color: black"></i>
                                    </asp:LinkButton>--%>
                                        <a href="JavaScript:StateCity('div<%# Eval("BatchId") %>');">
                                            <i class="glyphicon glyphicon-plus" id="imgdiv<%# Eval("BatchId") %>" style="color: black"></i>
                                        </a>
                                    </div>

                                    <div id="div<%# Eval("BatchId") %>" style="display: none;">
                                        <asp:GridView ID="dgLevel1View" runat="server" Width="100%" CssClass="custom-table" DataKeyNames="BatchId, FiRefNo"
                                            AutoGenerateColumns="False" AllowPaging="false" CellPadding="1" BorderStyle="None" GridLines="None"
                                            OnRowDataBound="dgLevel1View_RowDataBound">
                                            <PagerStyle CssClass="custom-pagination" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr. No." ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate><%#Container.DataItemIndex+1%></ItemTemplate>
                                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BatchId">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBatchId" runat="server" Text='<%# Eval("BatchId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDate" runat="server" Text='<%# Eval("LogDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Activity Type" SortExpression="FIRefNo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblActionType" runat="server" Text='<%# Eval("ActivityType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Activity Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblActionDesc" runat="server" Text='<%# Eval("ActivityDesc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Integration Mode">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIntgMode" runat="server" Text='<%# Eval("IntgMode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sr. No." ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate><%#Container.DataItemIndex+1%></ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" />
                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BatchId">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBatchId" runat="server" Text='<%# Eval("BatchId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text='<%# Eval("LogDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Activity Type" SortExpression="FIRefNo">
                                <ItemTemplate>
                                    <asp:Label ID="lblActionType" runat="server" Text='<%# Eval("ActivityType") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Activity Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblActionDesc" runat="server" Text='<%# Eval("ActivityDesc") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Integration Mode">
                                <ItemTemplate>
                                    <asp:Label ID="lblIntgMode" runat="server" Text='<%# Eval("IntgMode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action" >
                                <ItemTemplate>
                                    <center>
                                    <asp:LinkButton runat="server" ID="btnViewReg" ForeColor="Red"  CommandName="DwnldError"
                                            CommandArgument='<%# Eval("Batchid") %>'>
                                        <img src="../../Image/download_error_icon.png" />
                                        <br />
                                        Download Error
                                    </asp:LinkButton>
                                        </center>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                            <div class="col-sm-3" style="text-align: left" style="display: none">
                                <asp:Label ID="lblPageInfo" runat="server" Visible="false"></asp:Label>
                            </div>

                </div>
            </div>
            </div>

    </div>
                                    <div id="trRecord" runat="server" visible="false" colspan="6" style="height: 18px; text-align: center; ">
                                <asp:Label ID="lblMessage" runat="server" CssClass="standardlabelErr"></asp:Label>
                            </div>

    <script>
        function BindDatePicker(id) {
            $('#' + id).datepicker()
        }
    </script>
    <script type="text/javascript">
        function ShowProgressBar(Msg) {
            debugger;
            var Msg = Msg
            document.getElementById('dvProgressBar').style.display = "block";
            document.getElementById('EmptyPagePlaceholder_lblMsg').innerHTML = Msg;
            setTimeout(function () { HideProgressBar(); }, 5000);
        }

        function HideProgressBar() {
            debugger;
            document.getElementById('dvProgressBar').style.display = "none";
        }
    </script>


<%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
