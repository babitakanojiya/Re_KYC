<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="ZipFileDetail.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.CKYC.ZipFileDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/bundles/CKYCValidationjs") %>
    <%: Styles.Render("~/bundles/CKYCcss") %>
    <link rel="stylesheet" href="../../Content/Bootstrap/css/bootstrap.css">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <%--<meta http-equiv="refresh" content="30">--%>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">
    <style type="text/css">
          .HeaderText {
            text-align: center !important;
        }
          .center{
              text-align:center!important;
          }
        .modal-content {
            position: relative;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #999;
            border: 1px solid rgba(0,0,0,.2);
            border-radius: 6px;
            -webkit-box-shadow: 0 3px 9px rgb(0 0 0 / 50%);
            box-shadow: 0 3px 9px rgb(0 0 0 / 50%);
            outline: 0;
        }


        .HeaderText {
            white-space: nowrap;
            padding: 10px;
        }

        .modal-dialog {
            position: relative;
            width: auto;
            margin: 10px;
        }
    </style>
    <style>
        .gridViewHeader {
            background-color: #0066CC;
            color: #FFFFFF;
            padding: 4px 50px 4px 4px;
            text-align: left;
            border-width: 0px;
            border-collapse: collapse;
        }

        .HeaderText {
            padding-left: 10px !important;
            width: 30px;
        }

        .standardcheckbox {
            padding-right: 9px !important;
        }

        .chkClass label {
            margin-left: 3px !important;
        }

        input[type=checkbox], input[type=radio] {
            margin-right: 7px !important;
        }

            input[type=checkbox] + label, input[type=radio] + label {
                vertical-align: middle !important;
            }

        .mGrid {
            text-align: right !important;
        }
                .mGridL {
            text-align: left !important;
        }
            .mGrid td + td + td {
                text-align: right !important;
            }

        .username {
            background: #FFFFFF url(../../assets/images/dashboard-icon/search_icon.png) no-repeat right;
            padding: 5px;
            padding-right: 18px;
            border: 1px solid #ccc;
        }

        .center {
            text-align: center !important;
        }
    </style>

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
        .HeaderText {
            text-align: center !important;
        }

        .pad {
            text-align: center !important;
        }
    </style>

    
    <script type="text/javascript">
        //added by rutuja 
        function AlertMsg(msg) {
            debugger;
            var varFooter = '<center><button type="button" class="btn btn-success" data-dismiss="modal" style="margin-top: -6px;"><span class="glyphicon glyphicon-ok" style="color: White;"></span> OK </button></center>';
            showModal('#myModal', 'Alert', 'alert-warning', varFooter, '', msg);
        }

        function ShowHideModal23() {
            debugger;
            var div = document.getElementById("myModal23");
            // var div2
            if (div.style.display == "none") {
                div.style.display = "block";
                document.getElementById("myModal").style.display = "block";
            }
            else {
                div.style.display = "none";
            }
        }
        function ModalbtnClose() {
            debugger;
            ShowHideModal();
            MstShowHide("myModal23", "none");

        }
        function MstShowHide(Id, Action) {

            document.getElementById(Id).style.display = Action;
        }
        //ended by rutuja
        $(document).ready(function () {
            //var idleInterval = setInterval("reloadPage()", 50000);
        })

        function reloadPage() {

            location.reload();
        }

        //Added By Shubham
        function ShowHideModal() {
            debugger;
            var div = document.getElementById("myModal");
            if (div.style.display == "none") {
                div.style.display = "block";
            }
            else {
                div.style.display = "none";
            }
        }
        //Ended By Shubham
    </script>

    <%--<script type="text/javascript">
        function funfordefautenterkey2(btn2, event2) {
            debugger;
            if (document.all) {
                if (event2.keyCode == 13) {
                    event2.returnValue = false;
                    event2.cancel = true;
                    btn2.click();
                }
            }
        }
    </script>--%>

    <script type="text/javascript">
        function funfordefautenterkey(btn, event) {
            debugger;
            if (document.all) {
                if (event.keyCode == 13) {
                    event.returnValue = false;
                    event.cancel = true;
                    btn.click();
                }
            }
        }


        function ViewDocument(FileName) {
            debugger;

            window.open(FileName, 'DCTM', 'width=650,height=500,toolbar=no,scrollbars=yes,location=no,resizable=no,directories=no,status=no, menubar=no,addressbar=no,left=430,top=150,bottom=100');


        }

    </script>


    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <div style="margin-left: 84%;">
        <asp:Label ID="lblTime" runat="server"></asp:Label>
        <i class="fa fa-refresh" onclick="reloadPage();" style="font-size: 24px"></i>
    </div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>

            <div class="panel  panel-success" runat="server" style="margin-left: 2%; margin-right: 2%; margin-top: 0.5%">
                <%--<div class="panel panel-success" style='margin-right: 26px; margin-left: 26px;'>--%>
                <div id="trtitle" class="panel-heading" style="background-color: #00C5CC; color: white;" onclick="showHideDiv('trgridsponsorship','span1');return false;">
                    <div class="row" id="trDetails" runat="server">
                        <div class="col-sm-10" style="text-align: left">
                            <span class="glyphicon glyphicon-menu-hamburger"></span>
                            <asp:Label ID="lblprospectsearch" runat="server" Text="ANVIL FOR DATA PROCESSING & DATA FLOW"></asp:Label>
                        </div>
                        <div class="col-sm-2">
                            <span id="span1" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                        </div>
                    </div>
                </div>
                <div id="trgridsponsorship" class="panel-body">
                    <%--Added By Shubham--%>
                    <div style="border-collapse: collapse; display: block" runat="server" id="div1">
                        <%--LISTBOX SHOWING A LIST OF FILE TYPES.--%>
                        <%--ADD A GRIDVIEW WITH FEW COLUMNS--%>
                        <%--Input Folder(FI to Middleware)--%>
                        <div>
                            <img src="../../assets/images/dashboard-icon/input_Folder_icon.png" />
                            <asp:Image runat="server" ImageUrl="../../assets/images/dashboard-icon/input_Folder_icon.png" Style="padding-top: -1px; display: none" ToolTip="Zip" Width="20px" Height="20px" />
                            <label>Input Folder (FIS to MW)</label>
                            <%--<asp:TextBox ID="TextBox1" float="right" Style="float: right;" CssClass="username" ispostback="false" placeholder="Search Archive" runat="server"> 
                            </asp:TextBox>
                            <asp:Button ID="Button2" runat="server" ispostback="false" OnClick="lnkdelete_Click" Style="display: none;" ForeColor="Black"></asp:Button>&nbsp;--%>
                        </div>
                        <br />
                        <asp:GridView ID="GridView3" CssClass="footable" GridLines="Vertical" ShowFooter="false"
                            AllowPaging="true" PageSize="30"
                            AutoGenerateColumns="false" runat="server">

                            <EditRowStyle BackColor="#7C6F57" />
                            <%--<FooterStyle BackColor="#1C5E55" ForeColor="White" />--%>
                            <HeaderStyle BackColor="#00c5cc" ForeColor="White" Height="35px" CssClass="gridViewHeader" />
                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#E3EAEB" />
                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="false" ForeColor="#333333" />
                            <Columns>
                                <asp:TemplateField HeaderText="Name" HeaderStyle-CssClass="HeaderText" ItemStyle-Width="40%">
                                    <ItemTemplate>
                                        <%--<span class="glyphicon glyphicon-list-alt" style="color: #337ab7;"></span>--%>
                                        <img src="../../assets/images/CKYC_Table.png" />
                                        <asp:Image runat="server" ImageUrl="../../assets/images/CKYC_Table.png" Style="padding-top: -1px; display: none" ToolTip="Zip" Width="20px" Height="20px" />
                                        <asp:Label ID="lblTName" runat="server" Text='<%#Eval("TName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No. of Records" HeaderStyle-CssClass="HeaderText" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRecords" runat="server" Text='<%#Eval("Records") %> '></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="File Size" Visible="false" HeaderStyle-CssClass="HeaderText" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <%-- <%# string.Format("{0:N1}", (decimal)Item.Length / 1024) %> KB--%>
                                        <asp:Label ID="lblLen" runat="server" Text=''>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="File Extension" Visible="false" HeaderStyle-CssClass="HeaderText" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFileType" runat="server" Text=''>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Creation Date & Time" HeaderStyle-CssClass="HeaderText" ItemStyle-Width="30%" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDateTime" runat="server" Text='<%#Eval("Creation")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="File Mode" HeaderStyle-CssClass="HeaderText" ItemStyle-Width="10%" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMode" runat="server" Text='<%#Eval("Mode")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="View Records" HeaderStyle-CssClass="HeaderText" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnViewMiddltoFI" runat="server" class="glyphicon glyphicon-eye-open" OnClick="btnViewMiddltoFI_Click"
                                            Style="color: #337ab7;">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <p>
                            <asp:Label Text="" ID="Label1" runat="server"></asp:Label>
                        </p>
                        <br />
                        <%--Input Folder(FI to Middleware)--%>
                        <%--Input folder(CERSAI to Middleware)--%>
                        <div style="display: none;">
                            <img src="../../assets/images/dashboard-icon/input_Folder_icon.png" />
                            <asp:Image runat="server" ImageUrl="../../assets/images/dashboard-icon/input_Folder_icon.png" Style="padding-top: -1px; display: none" ToolTip="Zip" Width="20px" Height="20px" />
                            <label>Input Folder (CRS to MW)</label>
                        </div>
                        <%--<br />--%>
                        <div style="display: none;">
                            <asp:GridView ID="GridView4" CssClass="footable" GridLines="Vertical" ShowFooter="false"
                                AllowPaging="true" PageSize="15" EmptyDataText="No Record Found"
                                AutoGenerateColumns="false" runat="server">
                                <EditRowStyle BackColor="#7C6F57" />
                                <%--<FooterStyle BackColor="#1C5E55" ForeColor="White" />--%>
                                <HeaderStyle BackColor="#00c5cc" ForeColor="White" Height="35px" CssClass="gridViewHeader" />
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#E3EAEB" />
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="false" ForeColor="#333333" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Name" HeaderStyle-CssClass="HeaderText" ItemStyle-Width="40%">
                                        <ItemTemplate>
                                            <%--<span class="glyphicon glyphicon-list-alt" style="color: #337ab7;"></span>--%>
                                            <img src="../../assets/images/CKYC_Table.png" />
                                            <asp:Image runat="server" ImageUrl="../../assets/images/CKYC_Table.png" Style="padding-top: -1px; display: none" ToolTip="Zip" Width="20px" Height="20px" />
                                            <asp:Label ID="lblTName" runat="server" Text='<%#Eval("TName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="No. of Records" HeaderStyle-CssClass="HeaderText" ItemStyle-Width="10%" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRecords" runat="server" Text='<%#Eval("Records") %> '></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View Records" HeaderStyle-CssClass="HeaderText" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnViewMiddltoFI" runat="server" class="glyphicon glyphicon-eye-open" OnClick="btnViewMiddltoFI_Click"
                                                Style="color: #337ab7;">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="25%" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <p>
                            <asp:Label Text="" ID="Label2" runat="server"></asp:Label>
                        </p>
                        <%--<br />--%>
                        <%--Input folder(CERSAI to Middleware)--%>
                        <!-- Modal -->
                        <div class="modal fade in" id="myModal" style="display: none">
                            <div class="modal-dialog">

                                <!-- Modal content-->
                                <div class="modal-content" style="">
                                    <div class="modal-header" style="height: 40px;">
                                        <button type="button" class="close" onclick="ShowHideModal();">&times;</button>
                                        <h4 class="modal-title">Search Result</h4>
                                    </div>
                                    <div class="modal-body" style="height: 450px; overflow-x: scroll; overflow-y: scroll;">
                                        <asp:HiddenField ID="hdnFlagFIStoMW" ClientIDMode="Static" runat="server" Value="" />
                                        <asp:TextBox ID="txtSrchFIStoMW" float="right" Style="float: right; margin-bottom: 6px;" CssClass="username" placeholder="Search"
                                            OnTextChanged="txtSrchFIStoMW_TextChanged" runat="server"> </asp:TextBox>
                                        <%--Gridview BulkSearch--%>
                                        <asp:GridView ID="gvBulkSearch" CssClass="footable" ShowFooter="false" EmptyDataText="No Record Found"
                                            AllowPaging="false" PageSize="50" AutoGenerateColumns="false" runat="server">
                                            <EditRowStyle BackColor="#7C6F57" />
                                            <HeaderStyle BackColor="#00c5cc" ForeColor="White" Width="100%" Height="35px" CssClass="gridViewHeader" />
                                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#E3EAEB" />
                                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="false" ForeColor="#333333" />
                                            <Columns>
                                                <%-- <asp:TemplateField HeaderText="Name" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTName" runat="server" Text='<%#Eval("RecId")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <%--<asp:TemplateField HeaderText="Record Type" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRecord_Type" runat="server" Text='<%#Eval("Record_Type") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>--%>
                                                <%-- <asp:TemplateField HeaderText="Line Number" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLine_Number" runat="server" Text='<%#Eval("Line_Number") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Identity Type" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRecords" runat="server" Text='<%#Eval("Identity_Type") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Identity No." HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIdentity_Number" runat="server" Text='<%#Eval("Identity_Number") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <%-- <asp:TemplateField HeaderText="Name" HeaderStyle-CssClass="HeaderText"Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Gender" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGender" runat="server" Text='<%#Eval("Gender") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="File Mode" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMODE" runat="server" Text='<%#Eval("MODE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="center" HorizontalAlign="center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Creation Date & Time" HeaderStyle-CssClass="HeaderText" ItemStyle-Width="30%" >
                                                    <ItemTemplate>
                                                <asp:Label ID="lblDateTime" runat="server" Text='<%#Eval("CREATEDDATE")%>'>
                                                </asp:Label>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <%--Gridview BulkSearch--%>

                                        <%--Gridview BulkDownload--%>
                                        <asp:GridView ID="gvBulkDownload" CssClass="footable" ShowFooter="false" Width="100%" EmptyDataText="No Record Found"
                                            AllowPaging="true" PageSize="15" AutoGenerateColumns="false" runat="server">
                                            <EditRowStyle BackColor="#7C6F57" />
                                            <HeaderStyle BackColor="#00c5cc" ForeColor="White" Height="35px" CssClass="gridViewHeader" />
                                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#E3EAEB" />
                                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="false" ForeColor="#333333" />
                                            <Columns>
                                                <%-- <asp:TemplateField HeaderText="RecId" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTName" runat="server" Text='<%#Eval("RecId")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Record Type" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRecord_Type" runat="server" Text='<%#Eval("Bulk_download_record_type") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="CKYC Number" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLine_Number" runat="server" Text='<%#Eval("CKYC_No") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Authentication Factor" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRecords" runat="server" Text='<%#Eval("Authentication_Factor") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Authentication Factor Type" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRecord1" runat="server" Text='<%#Eval("Authentication_Factor_Type") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Filler" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIdentity_Number" runat="server" Text='<%#Eval("Filler_1") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="MODE" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMODE" runat="server" Text='<%#Eval("MODE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Creation Date & Time" HeaderStyle-CssClass="HeaderText" ItemStyle-Width="30%" >
                                                    <ItemTemplate>
                                                <asp:Label ID="lblDateTime" runat="server" Text='<%#Eval("CREATEDDATE")%>'>
                                                </asp:Label>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <%--Gridview BulkDownload--%>

                                        <%--Gridview BulkUpload rutujak--%>
                                        <asp:GridView ID="gvBulkUpload" CssClass="footable" ShowFooter="false" EmptyDataText="No Record Found"
                                            AllowPaging="true" PageSize="15" AutoGenerateColumns="false" runat="server" Width="100%" OnRowDataBound="gvBulkUpload_RowDataBound">
                                            <EditRowStyle BackColor="#7C6F57" />
                                            <HeaderStyle BackColor="#00c5cc" ForeColor="White" Height="35px" CssClass="gridViewHeader" />
                                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#E3EAEB" />
                                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="false" ForeColor="#333333" />
                                            <Columns>
                                                <%--<asp:TemplateField HeaderText="RecId" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSRNo" runat="server" Text='<%#Eval("SRNo")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BATCH NO" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBATCH_NO" runat="server" Text='<%#Eval("BATCH_NO") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>--%>
                                                <%--<asp:TemplateField HeaderText="FICode" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFI_Code" runat="server" Text='<%#Eval("FI_Code") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>--%>
                                                <%-- <asp:TemplateField HeaderText="Customer Type" HeaderStyle-CssClass="HeaderText"Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCUST_Type" runat="server" Text='<%#Eval("CUST_Type") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>--%>
                                                <%-- <asp:TemplateField HeaderText="App TYPE" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAPP_TYPE" runat="server" Text='<%#Eval("APP_TYPE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="BRANCH CODE" HeaderStyle-CssClass="HeaderText" HeaderStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBRANCH_CODE" runat="server" Text='<%#Eval("BRANCH_CODE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="HeaderText" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="APPLICANT NAME UPDATEFLAG" HeaderStyle-CssClass="HeaderText" ItemStyle-Width="40%" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAPPLICANT_NAME_UPDATE_FLAG" runat="server" Text='<%#Eval("APPLICANT_NAME_UPDATE_FLAG") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PERSONAL UPDATEFLAG" HeaderStyle-CssClass="HeaderText" ItemStyle-Width="40%" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPERSONAL_UPDATE_FLAG" runat="server" Text='<%#Eval("PERSONAL_UPDATE_FLAG") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ADDRESS UPDATEFLAG" HeaderStyle-CssClass="HeaderText" ItemStyle-Width="40%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAddress_Update_Flag" runat="server" Text='<%#Eval("ADDRESS_UPDATE_FLAG") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CONTACT UPDATEFLAG" HeaderStyle-CssClass="HeaderText" ItemStyle-Width="40%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblContact_Update_Flag" runat="server" Text='<%#Eval("CONTACT_UPDATE_FLAG") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="REMARK UPDATEFLAG" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblREMARK_UPDATE_FLAG" runat="server" Text='<%#Eval("REMARK_UPDATE_FLAG") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="KYC UPDATEFLAG" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblKYC_UPDATE_FLAG" runat="server" Text='<%#Eval("KYC_UPDATE_FLAG") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="IDENTITY UPDATEFLAG" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIDENTITY_UPDATE_FLAG" runat="server" Text='<%#Eval("IDENTITY_UPDATE_FLAG") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="RELATED PERSON UPDATEFLAG" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRELPERSON_UPDATE_FLAG" runat="server" Text='<%#Eval("RELPERSON_UPDATE_FLAG") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ACCOUNTTYPE UPDATEFLAG" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblACCOUNTTYPE_UPDATE_FLAG" runat="server" Text='<%#Eval("ACCOUNTTYPE_UPDATE_FLAG") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="IMAGE UPDATEFLAG" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIMAGE_UPDATE_FLAG" runat="server" Text='<%#Eval("IMAGE_UPDATE_FLAG") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CONSTTYPE" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCONSTTYPE" runat="server" Text='<%#Eval("CONSTTYPE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="OTHER CONSTTYPE" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOTHERCONSTTYPE" runat="server" Text='<%#Eval("OTHERCONSTTYPE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="A/C HOLDER TYPE" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblACC_HOLDER_TYPE" runat="server" Text='<%#Eval("ACC_HOLDER_TYPE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="A/C TYPE" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblACC_TYPE" runat="server" Text='<%#Eval("ACC_TYPE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FIREFNO CKYC" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFIREFNO_CKYC" runat="server" Text='<%#Eval("FIREFNO_CKYC") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PREFIX" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPREFIX" runat="server" Text='<%#Eval("PREFIX") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FIRST NAME" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFNAME" runat="server" Text='<%#Eval("FNAME") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MIDDLE NAME" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMNAME" runat="server" Text='<%#Eval("MNAME") %> ' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="LAST NAME" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLNAME" runat="server" Text='<%#Eval("LNAME") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FULLNAME" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFULLNAME" runat="server" Text='<%#Eval("FULLNAME") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ENTITY NAME" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNAME_OF_THE_ENTITY" runat="server" Text='<%#Eval("NAME_OF_THE_ENTITY") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PREFIX" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMAIDEN_PREFIX" runat="server" Text='<%#Eval("MAIDEN_PREFIX") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MAIDEN FIRST NAME" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMAIDEN_FNAME" runat="server" Text='<%#Eval("MAIDEN_FNAME") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MAIDEN MIDDLE NAME" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMAIDEN_MNAME" runat="server" Text='<%#Eval("MAIDEN_MNAME") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MAIDEN LAST NAME" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMAIDEN_LNAME" runat="server" Text='<%#Eval("MAIDEN_LNAME") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MAIDEN FULLNAME" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMAIDEN_FULLNAME" runat="server" Text='<%#Eval("MAIDEN_FULLNAME") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FATHERSPOUSE FLAG" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFATHERSPOUSE_FLAG" runat="server" Text='<%#Eval("FATHERSPOUSE_FLAG") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FATHER PREFIX" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFATHER_PREFIX" runat="server" Text='<%#Eval("FATHER_PREFIX") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FATHER FIRST NAME" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFATHER_FNAME" runat="server" Text='<%#Eval("FATHER_FNAME") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FATHER MIDLE NAME" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFATHER_MNAME" runat="server" Text='<%#Eval("FATHER_MNAME") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FATHER_LAST NAME" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFATHER_LNAME" runat="server" Text='<%#Eval("FATHER_LNAME") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FATHER FULLNAME" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblACC_TYPE1" runat="server" Text='<%#Eval("FATHER_FULLNAME") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MOTHER PREFIX" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMOTHER_PREFIX" runat="server" Text='<%#Eval("MOTHER_PREFIX") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MOTHER FIRST NAME" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMOTHER_FNAME" runat="server" Text='<%#Eval("MOTHER_FNAME") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MOTHER MIDDLE NAME" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMOTHER_MNAME" runat="server" Text='<%#Eval("MOTHER_MNAME") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MOTHER LAST NAME" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMOTHER_LNAME" runat="server" Text='<%#Eval("MOTHER_LNAME") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MOTHER FULLNAME" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMOTHER_FULLNAME" runat="server" Text='<%#Eval("MOTHER_FULLNAME") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="GENDER" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGENDER" runat="server" Text='<%#Eval("GENDER") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NATIONALITY" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNATIONALITY" runat="server" Text='<%#Eval("NATIONALITY") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DOB DATE OF INCORPORATION" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDATE_OF_BIRTH_DATE_OF_INCORPORATION" runat="server" Text='<%#Eval("DATE_OF_BIRTH_DATE_OF_INCORPORATION") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PLACE OF INCORPORATION" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPLACE_OF_INCORPORATION" runat="server" Text='<%#Eval("PLACE_OF_INCORPORATION") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DATE OF COMMENCEMENT OF BUSINESS" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDATE_OF_COMMENCEMENT_OF_BUSINESS" runat="server" Text='<%#Eval("DATE_OF_COMMENCEMENT_OF_BUSINESS") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="COUNTRY OF INCORPORATION" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCOUNTRY_OF_INCORPORATION" runat="server" Text='<%#Eval("COUNTRY_OF_INCORPORATION") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="COUNTRY OF RESIDANCE AS PER TAX LAWS" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCOUNTRY_OF_RESIDANCE_AS_PER_TAX_LAWS" runat="server" Text='<%#Eval("COUNTRY_OF_RESIDANCE_AS_PER_TAX_LAWS") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="IDENTIFICATION TYPE" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIDENTIFICATION_TYPE" runat="server" Text='<%#Eval("IDENTIFICATION_TYPE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TIN GST REGISTRATION No" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTIN_GST_REGISTRATION_NO" runat="server" Text='<%#Eval("TIN_GST_REGISTRATION_NO") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TIN ISSUING COUNTRY" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTIN_ISSUING_COUNTRY" runat="server" Text='<%#Eval("TIN_ISSUING_COUNTRY") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PAN" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPAN" runat="server" Text='<%#Eval("PAN") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FORM60" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFORM_60" runat="server" Text='<%#Eval("FORM_60") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="HeaderText" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="RESIDENTIAL STATUS" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRESIDENTIAL_STATUS" runat="server" Text='<%#Eval("RESIDENTIAL_STATUS") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FLAG APPLICANT RESIDENT TAX JURISDICTION OUTSIDE INDIA" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFLAG_APPLICANT_RESIDENT_TAX_JURISDICTION_OUTSIDE_INDIA" runat="server" Text='<%#Eval("FLAG_APPLICANT_RESIDENT_TAX_JURISDICTION_OUTSIDE_INDIA") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JURISDICTION OF RESIDENCE" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJURISDICTION_OF_RESIDENCE" runat="server" Text='<%#Eval("JURISDICTION_OF_RESIDENCE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TIN GST REGNO." HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTIN_GST_REG_NO" runat="server" Text='<%#Eval("TIN_GST_REG_NO") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="COUNTRY OF BIRTH" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCOUNTRY_OF_BIRTH" runat="server" Text='<%#Eval("COUNTRY_OF_BIRTH") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CITY PLANCE OF BIRTH" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCITY_PLANCE_OF_BIRTH" runat="server" Text='<%#Eval("CITY_PLANCE_OF_BIRTH") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CPO ADDRESS TYPE" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCPO_ADDRESSTYPE" runat="server" Text='<%#Eval("CPO_ADDRESSTYPE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PERMANENT LINE1" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPERM_LINE1" runat="server" Text='<%#Eval("PERM_LINE1") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="HeaderText" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PERMANENT LINE2" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPERM_LINE2" runat="server" Text='<%#Eval("PERM_LINE2") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="HeaderText" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PERMANENT LINE3" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPERM_LINE3" runat="server" Text='<%#Eval("PERM_LINE3") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="HeaderText" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PERMANENT CITY" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPERM_CITY" runat="server" Text='<%#Eval("PERM_CITY") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="HeaderText" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PERMANENT DISTRICT" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPERM_DIST" runat="server" Text='<%#Eval("PERM_DIST") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="HeaderText" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PERMANENT STATE" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPERM_STATE" runat="server" Text='<%#Eval("PERM_STATE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="HeaderText" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PERMANENT COUNTRY" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPERM_COUNTRY" runat="server" Text='<%#Eval("PERM_COUNTRY") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="HeaderText" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PERMANENT PIN" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPERM_PIN" runat="server" Text='<%#Eval("PERM_PIN") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PERMANENT POA" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPERM_POA" runat="server" Text='<%#Eval("PERM_POA") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PERMANENT POAOTHERS" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPERM_POAOTHERS" runat="server" Text='<%#Eval("PERM_POAOTHERS") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PERM CORRESPONDENCE SAMEFLAG" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPERM_CORRES_SAMEFLAG" runat="server" Text='<%#Eval("PERM_CORRES_SAMEFLAG") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="HeaderText" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CORRESPONDENCE LOCAL ADDRESSTYPE" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCORRES_LOCAL_ADDRESSTYPE" runat="server" Text='<%#Eval("CORRES_LOCAL_ADDRESSTYPE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CORRESPONDENCE LINE1" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCORRES_LINE1" runat="server" Text='<%#Eval("CORRES_LINE1") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CORRESPONDENCE LINE2" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCORRES_LINE2" runat="server" Text='<%#Eval("CORRES_LINE2") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CORRESPONDENCE LINE3" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCORRES_LINE3" runat="server" Text='<%#Eval("CORRES_LINE3") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CORRESPONDENCE CITY" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCORRES_CITY" runat="server" Text='<%#Eval("CORRES_CITY") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CORRESPONDENCE DIST" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCORRES_DIST" runat="server" Text='<%#Eval("CORRES_DIST") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CORRESPONDENCE STATE" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCORRES_STATE" runat="server" Text='<%#Eval("CORRES_STATE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CORRESPONDENCE COUNTRY" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCORRES_COUNTRY" runat="server" Text='<%#Eval("CORRES_COUNTRY") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CORRESPONDENCE PIN" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCORRES_PIN" runat="server" Text='<%#Eval("CORRES_PIN") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CORRESPONDENCE POA" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCORRES_POA" runat="server" Text='<%#Eval("CORRES_POA") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JURISDICTION PERM  CORRES OVERSEASE SAMEFLAG" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJURISDICTION_PERM_CORRES_OVERSEASE_SAMEFLAG" runat="server" Text='<%#Eval("JURISDICTION_PERM_CORRES_OVERSEASE_SAMEFLAG") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JURISDICTION TYPE" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJURISDICTION_TYPE" runat="server" Text='<%#Eval("JURISDICTION_TYPE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JURISDICTION LINE1" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJURISDICTION_LINE1" runat="server" Text='<%#Eval("JURISDICTION_LINE1") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JURISDICTION LINE2" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJURISDICTION_LINE2" runat="server" Text='<%#Eval("JURISDICTION_LINE2") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JURISDICTION LINE3" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJURISDICTION_LINE3" runat="server" Text='<%#Eval("JURISDICTION_LINE3") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JURISDICTION CITY" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJURISDICTION_CITY" runat="server" Text='<%#Eval("JURISDICTION_CITY") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JURISDICTION DISTRICT" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJURISDICTION_DIST" runat="server" Text='<%#Eval("JURISDICTION_DIST") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JURISDICTION STATE" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJURISDICTION_STATE" runat="server" Text='<%#Eval("JURISDICTION_STATE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JURISDICTION COUNTRY" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJURISDICTION_COUNTRY" runat="server" Text='<%#Eval("JURISDICTION_COUNTRY") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JURISDICTION PIN" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJURISDICTION_PIN" runat="server" Text='<%#Eval("JURISDICTION_PIN") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JURISDICTION POA" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJURISDICTION_POA" runat="server" Text='<%#Eval("JURISDICTION_POA") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="RESI STD CODE" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRESI_STD_CODE" runat="server" Text='<%#Eval("RESI_STD_CODE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="RESI TEL NUM" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRESI_TEL_NUM" runat="server" Text='<%#Eval("RESI_TEL_NUM") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="OFF STD CODE" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOFF_STD_CODE" runat="server" Text='<%#Eval("OFF_STD_CODE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="OFFICE TELEPHONE NUMBER" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOFF_TEL_NUM" runat="server" Text='<%#Eval("OFF_TEL_NUM") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MOBILE CODE" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMOB_CODE" runat="server" Text='<%#Eval("MOB_CODE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MOBILE NUMBER2" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMOB_NUM" runat="server" Text='<%#Eval("MOB_NUM") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FAX CODE" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFAX_CODE" runat="server" Text='<%#Eval("FAX_CODE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FAX No" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFAX_NO" runat="server" Text='<%#Eval("FAX_NO") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EMAIL" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEMAIL" runat="server" Text='<%#Eval("EMAIL") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="REMARKS" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblREMARKS" runat="server" Text='<%#Eval("REMARKS") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="HeaderText" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DATE" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDEC_DATE" runat="server" Text='<%#Eval("DEC_DATE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="HeaderText" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PLACE" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDEC_PLACE" runat="server" Text='<%#Eval("DEC_PLACE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="KYC DATE" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblKYC_DATE" runat="server" Text='<%#Eval("KYC_DATE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="HeaderText" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DOCUMENT SUB" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDOC_SUB" runat="server" Text='<%#Eval("DOC_SUB") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NAME" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblKYC_NAME" runat="server" Text='<%#Eval("KYC_NAME") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="HeaderText" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DESIGNATION" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblKYC_DESIGNATION" runat="server" Text='<%#Eval("KYC_DESIGNATION") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="HeaderText" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BRANCH" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblKYC_BRANCH" runat="server" Text='<%#Eval("KYC_BRANCH") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EMPLOYEE CODE" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblKYC_EMPCODE" runat="server" Text='<%#Eval("KYC_EMPCODE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ORGANIZATION NAME" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblORG_NAME" runat="server" Text='<%#Eval("ORG_NAME") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="HeaderText" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ORGANIZATION CODE" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblORG_CODE" runat="server" Text='<%#Eval("ORG_CODE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="HeaderText" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NUM IDENTITY" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNUM_IDENTITY" runat="server" Text='<%#Eval("NUM_IDENTITY") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NUM RELATED" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNUM_RELATED" runat="server" Text='<%#Eval("NUM_RELATED") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="IDENTITY VER FLAG" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIDENTITY_VER_FLAG" runat="server" Text='<%#Eval("IDENTITY_VER_FLAG") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No. OF LOCAL ADDRESSDETAILS" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNUMBER_OF_LOCAL_ADDRESSDETAILS" runat="server" Text='<%#Eval("NUMBER_OF_LOCAL_ADDRESSDETAILS") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No. OF IMAGES" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNUM_IMAGES" runat="server" Text='<%#Eval("NUM_IMAGES") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MOBILE CODE2" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMOB_CODE2" runat="server" Text='<%#Eval("MOB_CODE2") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MOBILENo.2" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMOB_NUM2" runat="server" Text='<%#Eval("MOB_NUM2") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EMAIL2" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEMAIL2" runat="server" Text='<%#Eval("EMAIL2") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="REG FILLER1" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblREG_FILLER1" runat="server" Text='<%#Eval("REG_FILLER1") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Other IDNT DESC" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOther_IDNT_DESC" runat="server" Text='<%#Eval("Other_IDNT_DESC") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EXPIRY DATE" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEXPIRY_DATE" runat="server" Text='<%#Eval("EXPIRY_DATE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="IDENTITY PROOF SUBMITTED" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIDENTITY_PROOF_SUBMITTED" runat="server" Text='<%#Eval("IDENTITY_PROOF_SUBMITTED") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="IDVER STATUS ID1" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIDVER_STATUS_ID1" runat="server" Text='<%#Eval("IDVER_STATUS_ID1") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ID FILLER1" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID_FILLER1" runat="server" Text='<%#Eval("ID_FILLER1") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ID FILLER2" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID_FILLER2" runat="server" Text='<%#Eval("ID_FILLER2") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ID FILLER3" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID_FILLER3" runat="server" Text='<%#Eval("ID_FILLER3") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ID FILLER4" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID_FILLER4" runat="server" Text='<%#Eval("ID_FILLER4") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <%-- <asp:TemplateField HeaderText="RELATED FIREFNO" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRELATED_FIREFNO" runat="server" Text='<%#Eval("RELATED_FIREFNO") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Error Msg" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblErrorMesg" runat="server" Text='<%#Eval("ErrorMesg") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="BATCH ID" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBatchid" runat="server" Text='<%#Eval("Batchid") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MODE" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMODE" runat="server" Text='<%#Eval("MODE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Creation Date & Time" HeaderStyle-CssClass="HeaderText" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                <asp:Label ID="lblDateTime" runat="server" Text='<%#Eval("CREATEDDATE")%>'>
                                                </asp:Label>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="STATUS" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblStatus" runat="server" Text='<%#Eval("Status") %> ' OnClick="lblStatus_Click" ForeColor="Blue"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <%--added by rutuja --%>
                                                <%-- <asp:TemplateField HeaderText="User Id" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUserId" runat="server" Text='<%#Eval("UserId") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Process Status" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProcessStatus" runat="server" Text='<%#Eval("ProcessStatus") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                        <%--Gridview BulkUpload--%>

                                        <%--Gridview UnsolitatedNotification--%>
                                        <asp:GridView ID="gvUnsolitatedNotification" CssClass="footable" ShowFooter="false" Width="100%" EmptyDataText="No Record Found"
                                            AllowPaging="true" PageSize="15" AutoGenerateColumns="false" runat="server" Visible="false">
                                            <EditRowStyle BackColor="#7C6F57" />
                                            <HeaderStyle BackColor="#00c5cc" ForeColor="White" Height="35px" CssClass="gridViewHeader" />
                                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#E3EAEB" />
                                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="false" ForeColor="#333333" />
                                            <Columns>

                                                <%-- <asp:TemplateField HeaderText="RecId" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTName" runat="server" Text='<%#Eval("RecId")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FICode" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFI_Code" runat="server" Text='<%#Eval("FI_Code") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="KYC Number" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblKYC_No" runat="server" Text='<%#Eval("KYC_No") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ACCOUNT TYPE" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRecords" runat="server" Text='<%#Eval("Account_Type") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NAME UPDATEFLAG" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblName_update_Flag" runat="server" Text='<%#Eval("Name_update_Flag","{0:dd MMM yyyy}") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" Width="45px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PERSONAL ENTITY DETAILS UPDATEFLAG" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPersonal_Entity_Details_Update_Flag" runat="server" Text='<%#Eval("Personal_Entity_Details_Update_Flag","{0:dd MMM yyyy}") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ADDRESS DETAILS UPDATEFLAG" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAddress_Details_Update_Flag" runat="server" Text='<%#Eval("Address_Details_Update_Flag","{0:dd MMM yyyy}") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CONTACT DETAILS UPDATEFLAG" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblContact_Details_Update_Flag" runat="server" Text='<%#Eval("Contact_Details_Update_Flag","{0:dd MMM yyyy}") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="OTHER DETAILS UPDATEFLAG" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOther_Details_Update_Flag" runat="server" Text='<%#Eval("Other_Details_Update_Flag","{0:dd MMM yyyy}") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="IDENTITY DETAILS UPDATEFLAG" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIdentity_Details_Update_Flag" runat="server" Text='<%#Eval("Identity_Details_Update_Flag","{0:dd MMM yyyy}") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="RELATED PERSON DETAILS UPDATEFLAG" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRelated_Person_Details_Update_Flag" runat="server" Text='<%#Eval("Related_Person_Details_Update_Flag","{0:dd MMM yyyy}") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="IMAGE UPDATEFLAG" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblImage_Update_Flag" runat="server" Text='<%#Eval("Image_Update_Flag","{0:dd MMM yyyy}") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="KYC DEACTIVATIONFLAG" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblKYC_Deactivation_Flag" runat="server" Text='<%#Eval("KYC_Deactivation_Flag") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="KYC DEACTIVATION REMARK" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblKYC_Deactivation_Remarks" runat="server" Text='<%#Eval("KYC_Deactivation_Remarks") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PASSPORTNo. EXPIRY" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPassport_Number_Expiry" runat="server" Text='<%#Eval("Passport_Number_Expiry","{0:dd MMM yyyy}") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DRIVINGNo. Expiry" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIdentity_Number" runat="server" Text='<%#Eval("Driving_Number_Expiry","{0:dd MMM yyyy}") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <%--Gridview UnsolitatedNotification--%>
                                         <%--added by babita--%>
                                           <asp:GridView ID="Gridunsolitateddata" CssClass="footable" ShowFooter="false" Width="100%" EmptyDataText="No Record Found"
                                            AllowPaging="true" PageSize="15" AutoGenerateColumns="false" runat="server" OnRowDataBound="Gridunsolitateddata_RowDataBound">
                                            <EditRowStyle BackColor="#7C6F57" />
                                            <HeaderStyle BackColor="#00c5cc" ForeColor="White" Height="35px" CssClass="gridViewHeader" />
                                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#E3EAEB" />
                                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="false" ForeColor="#333333" />
                                            <Columns>

                                            
                                                <asp:TemplateField HeaderText="KYC Number" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Kycno" runat="server" Text='<%#Eval("Kyc_no") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                               
                                              
                                                <asp:TemplateField HeaderText="Update Category" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcalltype" runat="server" Text='<%#Eval("Categorydesc") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Create Date Time" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcreateddatetime" runat="server" Text='<%#Eval("Createdtim","{0:dd MMM yyyy}") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" Width="45px" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("Status") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="CurrentStatus" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                       <asp:Image ID="loadingImage" runat="server"   Height="25px" Width="25px" style="vertical-align: middle;" />
                                                        <%--<asp:Label ID="lblcurrentStatus" Text="" runat="server"  Style="text-decoration-color:green" />--%>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                
                                            </Columns>
                                        </asp:GridView>

                                         <%--ended by babita--%>
                                        <%--Gridview FIUpdate--%>
                                        <asp:GridView ID="gvFIUpdate" CssClass="footable" ShowFooter="false" EmptyDataText="No record found"
                                            AllowPaging="true" PageSize="15" AutoGenerateColumns="false" runat="server">
                                            <EditRowStyle BackColor="#7C6F57" />
                                            <HeaderStyle BackColor="#00c5cc" ForeColor="White" Height="35px" CssClass="gridViewHeader" />
                                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#E3EAEB" />
                                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="false" ForeColor="#333333" />
                                            <Columns>
                                                <%-- <asp:TemplateField HeaderText="RecId" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSRNo" runat="server" Text='<%#Eval("SRNo")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BATCH NO" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBATCH_NO" runat="server" Text='<%#Eval("BATCH_NO") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FICode" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFI_Code" runat="server" Text='<%#Eval("FI_Code") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Customer Type" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCUST_Type" runat="server" Text='<%#Eval("CUST_Type") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="App TYPE" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAPP_TYPE" runat="server" Text='<%#Eval("APP_TYPE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="BRANCH CODE" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBRANCH_CODE" runat="server" Text='<%#Eval("BRANCH_CODE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="HeaderText" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="APPLICANT NAME UPDATE" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAPPLICANT_NAME_UPDATE_FLAG" runat="server" Text='<%#Eval("APPLICANT_NAME_UPDATE_FLAG") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PERSONAL UPDATE" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPERSONAL_UPDATE_FLAG" runat="server" Text='<%#Eval("PERSONAL_UPDATE_FLAG") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ADDRESS UPDATE" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAddress_Update_Flag" runat="server" Text='<%#Eval("ADDRESS_UPDATE_FLAG") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CONTACT UPDATE" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblContact_Update_Flag" runat="server" Text='<%#Eval("CONTACT_UPDATE_FLAG") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="REMARK UPDATE" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblREMARK_UPDATE_FLAG" runat="server" Text='<%#Eval("REMARK_UPDATE_FLAG") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="KYC UPDATE" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblKYC_UPDATE_FLAG" runat="server" Text='<%#Eval("KYC_UPDATE_FLAG") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="IDENTITY UPDATE" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIDENTITY_UPDATE_FLAG" runat="server" Text='<%#Eval("IDENTITY_UPDATE_FLAG") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="RELATED PERSON UPDATE" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRELPERSON_UPDATE_FLAG" runat="server" Text='<%#Eval("RELPERSON_UPDATE_FLAG") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ACCOUNTTYPE UPDATE" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblACCOUNTTYPE_UPDATE_FLAG" runat="server" Text='<%#Eval("ACCOUNTTYPE_UPDATE_FLAG") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="IMAGE UPDATE" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIMAGE_UPDATE_FLAG" runat="server" Text='<%#Eval("IMAGE_UPDATE_FLAG") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CONSTTYPE" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCONSTTYPE" runat="server" Text='<%#Eval("CONSTTYPE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="OTHER CONSTTYPE" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOTHERCONSTTYPE" runat="server" Text='<%#Eval("OTHERCONSTTYPE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="A/C HOLDER TYPE" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblACC_HOLDER_TYPE" runat="server" Text='<%#Eval("ACC_HOLDER_TYPE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="A/C TYPE" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblACC_TYPE2" runat="server" Text='<%#Eval("ACC_TYPE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FIREFNO CKYC" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFIREFNO_CKYC" runat="server" Text='<%#Eval("FIREFNO_CKYC") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PREFIX" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPREFIX" runat="server" Text='<%#Eval("PREFIX") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FIRST NAME" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFNAME" runat="server" Text='<%#Eval("FNAME") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MIDDLE MNAME" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMNAME" runat="server" Text='<%#Eval("MNAME") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="LAST NAME" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLNAME" runat="server" Text='<%#Eval("LNAME") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FULLNAME" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFULLNAME" runat="server" Text='<%#Eval("FULLNAME") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ENTITY NAME" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNAME_OF_THE_ENTITY" runat="server" Text='<%#Eval("NAME_OF_THE_ENTITY") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="HeaderText" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PREFIX" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMAIDEN_PREFIX" runat="server" Text='<%#Eval("MAIDEN_PREFIX") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MAIDEN FIRST NAME" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMAIDEN_FNAME" runat="server" Text='<%#Eval("MAIDEN_FNAME") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MAIDEN MIDDLE NAME" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMAIDEN_MNAME" runat="server" Text='<%#Eval("MAIDEN_MNAME") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MAIDEN LAST NAME" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMAIDEN_LNAME" runat="server" Text='<%#Eval("MAIDEN_LNAME") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MAIDEN FULLNAME" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMAIDEN_FULLNAME" runat="server" Text='<%#Eval("MAIDEN_FULLNAME") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FATHERSPOUSE FLAG" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFATHERSPOUSE_FLAG" runat="server" Text='<%#Eval("FATHERSPOUSE_FLAG") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FATHER PREFIX" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFATHER_PREFIX" runat="server" Text='<%#Eval("FATHER_PREFIX") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FATHER FIRST NAME" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFATHER_FNAME" runat="server" Text='<%#Eval("FATHER_FNAME") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FATHER MIDDLE NAME" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFATHER_MNAME" runat="server" Text='<%#Eval("FATHER_MNAME") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FATHER LAST NAME" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFATHER_LNAME" runat="server" Text='<%#Eval("FATHER_LNAME") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FATHER FULLNAME" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblACC_TYPE3" runat="server" Text='<%#Eval("FATHER_FULLNAME") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MOTHER PREFIX" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMOTHER_PREFIX" runat="server" Text='<%#Eval("MOTHER_PREFIX") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MOTHER FIRST NAME" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMOTHER_FNAME" runat="server" Text='<%#Eval("MOTHER_FNAME") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MOTHER MIDDLE NAME" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMOTHER_MNAME" runat="server" Text='<%#Eval("MOTHER_MNAME") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MOTHER LAST NAME" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMOTHER_LNAME" runat="server" Text='<%#Eval("MOTHER_LNAME") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MOTHER FULLNAME" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMOTHER_FULLNAME" runat="server" Text='<%#Eval("MOTHER_FULLNAME") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="GENDER" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGENDER" runat="server" Text='<%#Eval("GENDER") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NATIONALITY" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNATIONALITY" runat="server" Text='<%#Eval("NATIONALITY") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DOB DATE OF INCORPORATION" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDATE_OF_BIRTH_DATE_OF_INCORPORATION" runat="server" Text='<%#Eval("DATE_OF_BIRTH_DATE_OF_INCORPORATION") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PLACE OF INCORPORATION" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPLACE_OF_INCORPORATION" runat="server" Text='<%#Eval("PLACE_OF_INCORPORATION") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DATE OF COMMENCEMENT OF BUSINESS" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDATE_OF_COMMENCEMENT_OF_BUSINESS" runat="server" Text='<%#Eval("DATE_OF_COMMENCEMENT_OF_BUSINESS") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="COUNTRY OF INCORPORATION" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCOUNTRY_OF_INCORPORATION" runat="server" Text='<%#Eval("COUNTRY_OF_INCORPORATION") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="COUNTRY OF RESIDANCE AS PER TAX LAWS" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCOUNTRY_OF_RESIDANCE_AS_PER_TAX_LAWS" runat="server" Text='<%#Eval("COUNTRY_OF_RESIDANCE_AS_PER_TAX_LAWS") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="IDENTIFICATION TYPE" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIDENTIFICATION_TYPE" runat="server" Text='<%#Eval("IDENTIFICATION_TYPE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TIN GST REGISTRATION No" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTIN_GST_REGISTRATION_NO" runat="server" Text='<%#Eval("TIN_GST_REGISTRATION_NO") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TIN ISSUING COUNTRY" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTIN_ISSUING_COUNTRY" runat="server" Text='<%#Eval("TIN_ISSUING_COUNTRY") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PAN" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPAN" runat="server" Text='<%#Eval("PAN") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FORM60" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFORM_60" runat="server" Text='<%#Eval("FORM_60") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="RESIDENTIAL STATUS" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRESIDENTIAL_STATUS" runat="server" Text='<%#Eval("RESIDENTIAL_STATUS") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FLAG APPLICANT RESIDENT TAX JURISDICTION OUTSIDE INDIA" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFLAG_APPLICANT_RESIDENT_TAX_JURISDICTION_OUTSIDE_INDIA" runat="server" Text='<%#Eval("FLAG_APPLICANT_RESIDENT_TAX_JURISDICTION_OUTSIDE_INDIA") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JURISDICTION OF RESIDENCE" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJURISDICTION_OF_RESIDENCE" runat="server" Text='<%#Eval("JURISDICTION_OF_RESIDENCE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TIN GST REGNo." HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTIN_GST_REG_NO" runat="server" Text='<%#Eval("TIN_GST_REG_NO") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="COUNTRY OF BIRTH" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCOUNTRY_OF_BIRTH" runat="server" Text='<%#Eval("COUNTRY_OF_BIRTH") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CITY PLANCE OF BIRTH" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCITY_PLANCE_OF_BIRTH" runat="server" Text='<%#Eval("CITY_PLANCE_OF_BIRTH") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CPO ADDRESS TYPE" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCPO_ADDRESSTYPE" runat="server" Text='<%#Eval("CPO_ADDRESSTYPE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PERMANENT LINE1" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPERM_LINE1" runat="server" Text='<%#Eval("PERM_LINE1") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="HeaderText" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PERMANENT LINE2" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPERM_LINE2" runat="server" Text='<%#Eval("PERM_LINE2") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="HeaderText" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PERMANENT LINE3" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPERM_LINE3" runat="server" Text='<%#Eval("PERM_LINE3") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="HeaderText" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PERMANENT CITY" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPERM_CITY" runat="server" Text='<%#Eval("PERM_CITY") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="HeaderText" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PERMANENT DIST" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPERM_DIST" runat="server" Text='<%#Eval("PERM_DIST") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="HeaderText" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PERMANENT STATE" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPERM_STATE" runat="server" Text='<%#Eval("PERM_STATE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="HeaderText" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PERMANENT COUNTRY" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPERM_COUNTRY" runat="server" Text='<%#Eval("PERM_COUNTRY") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="HeaderText" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PERMANENT PIN" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPERM_PIN" runat="server" Text='<%#Eval("PERM_PIN") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PERMANENT POA" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPERM_POA" runat="server" Text='<%#Eval("PERM_POA") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PERMANENT POAOTHERS" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPERM_POAOTHERS" runat="server" Text='<%#Eval("PERM_POAOTHERS") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PERMANENT CORRESPONDENCE SAMEFLAG" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPERM_CORRES_SAMEFLAG" runat="server" Text='<%#Eval("PERM_CORRES_SAMEFLAG") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CORRESPONDENCE LOCAL ADDRESSTYPE" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCORRES_LOCAL_ADDRESSTYPE" runat="server" Text='<%#Eval("CORRES_LOCAL_ADDRESSTYPE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CORRESPONDENCE LINE1" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCORRES_LINE1" runat="server" Text='<%#Eval("CORRES_LINE1") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CORRESPONDENCE LINE2" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCORRES_LINE2" runat="server" Text='<%#Eval("CORRES_LINE2") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CORRESPONDENCE LINE3" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCORRES_LINE3" runat="server" Text='<%#Eval("CORRES_LINE3") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CORRESPONDENCE CITY" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCORRES_CITY" runat="server" Text='<%#Eval("CORRES_CITY") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CORRESPONDENCE DIST" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCORRES_DIST" runat="server" Text='<%#Eval("CORRES_DIST") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CORRESPONDENCE STATE" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCORRES_STATE" runat="server" Text='<%#Eval("CORRES_STATE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CORRESPONDENCE COUNTRY" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCORRES_COUNTRY" runat="server" Text='<%#Eval("CORRES_COUNTRY") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CORRESPONDENCE PIN" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCORRES_PIN" runat="server" Text='<%#Eval("CORRES_PIN") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CORRESPONDENCE POA" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCORRES_POA" runat="server" Text='<%#Eval("CORRES_POA") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JURISDICTION PERM CORRES OVERSEASE SAMEFLAG" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJURISDICTION_PERM_CORRES_OVERSEASE_SAMEFLAG" runat="server" Text='<%#Eval("JURISDICTION_PERM_CORRES_OVERSEASE_SAMEFLAG") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JURISDICTION TYPE" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJURISDICTION_TYPE" runat="server" Text='<%#Eval("JURISDICTION_TYPE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JURISDICTION LINE1" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJURISDICTION_LINE1" runat="server" Text='<%#Eval("JURISDICTION_LINE1") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JURISDICTION LINE2" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJURISDICTION_LINE2" runat="server" Text='<%#Eval("JURISDICTION_LINE2") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JURISDICTION LINE3" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJURISDICTION_LINE3" runat="server" Text='<%#Eval("JURISDICTION_LINE3") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JURISDICTION CITY" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJURISDICTION_CITY" runat="server" Text='<%#Eval("JURISDICTION_CITY") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JURISDICTION DISTRICT" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJURISDICTION_DIST" runat="server" Text='<%#Eval("JURISDICTION_DIST") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JURISDICTION STATE" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJURISDICTION_STATE" runat="server" Text='<%#Eval("JURISDICTION_STATE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JURISDICTION COUNTRY" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJURISDICTION_COUNTRY" runat="server" Text='<%#Eval("JURISDICTION_COUNTRY") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGridL" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JURISDICTION PIN" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJURISDICTION_PIN" runat="server" Text='<%#Eval("JURISDICTION_PIN") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JURISDICTION POA" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJURISDICTION_POA" runat="server" Text='<%#Eval("JURISDICTION_POA") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="RESI_STD_CODE" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRESI_STD_CODE" runat="server" Text='<%#Eval("RESI_STD_CODE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="RESI_TEL_NUM" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRESI_TEL_NUM" runat="server" Text='<%#Eval("RESI_TEL_NUM") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="OFF_STD_CODE" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOFF_STD_CODE" runat="server" Text='<%#Eval("OFF_STD_CODE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="OFFICE TELEPHONE NUMBER" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOFF_TEL_NUM" runat="server" Text='<%#Eval("OFF_TEL_NUM") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MOBILE CODE" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMOB_CODE" runat="server" Text='<%#Eval("MOB_CODE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MOBILE NUMBER" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMOB_NUM" runat="server" Text='<%#Eval("MOB_NUM") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FAX CODE" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFAX_CODE" runat="server" Text='<%#Eval("FAX_CODE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FAX No" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFAX_NO" runat="server" Text='<%#Eval("FAX_NO") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EMAIL" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEMAIL" runat="server" Text='<%#Eval("EMAIL") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="REMARKS" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblREMARKS" runat="server" Text='<%#Eval("REMARKS") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="HeaderText" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DATE" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDEC_DATE" runat="server" Text='<%#Eval("DEC_DATE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="HeaderText" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PLACE" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDEC_PLACE" runat="server" Text='<%#Eval("DEC_PLACE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="KYC DATE" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblKYC_DATE" runat="server" Text='<%#Eval("KYC_DATE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="HeaderText" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DOCUMENT SUB" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDOC_SUB" runat="server" Text='<%#Eval("DOC_SUB") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NAME" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblKYC_NAME" runat="server" Text='<%#Eval("KYC_NAME") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="HeaderText" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DESIGNATION" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblKYC_DESIGNATION" runat="server" Text='<%#Eval("KYC_DESIGNATION") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="HeaderText" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BRANCH" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblKYC_BRANCH" runat="server" Text='<%#Eval("KYC_BRANCH") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EMPLOYEE CODE" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblKYC_EMPCODE" runat="server" Text='<%#Eval("KYC_EMPCODE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ORGANIZATION NAME" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblORG_NAME" runat="server" Text='<%#Eval("ORG_NAME") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="HeaderText" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ORGANIZATION CODE" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblORG_CODE" runat="server" Text='<%#Eval("ORG_CODE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NUM IDENTITY" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNUM_IDENTITY" runat="server" Text='<%#Eval("NUM_IDENTITY") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NUM RELATED" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNUM_RELATED" runat="server" Text='<%#Eval("NUM_RELATED") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="IDENTITY VER FLAG" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIDENTITY_VER_FLAG" runat="server" Text='<%#Eval("IDENTITY_VER_FLAG") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No. OF LOCAL ADDRESSDETAILS" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNUMBER_OF_LOCAL_ADDRESSDETAILS" runat="server" Text='<%#Eval("NUMBER_OF_LOCAL_ADDRESSDETAILS") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No. of IMAGES" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNUM_IMAGES" runat="server" Text='<%#Eval("NUM_IMAGES") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MOBIILE CODE2" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMOB_CODE2" runat="server" Text='<%#Eval("MOB_CODE2") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MOBILENo.2" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMOB_NUM2" runat="server" Text='<%#Eval("MOB_NUM2") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EMAIL2" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEMAIL2" runat="server" Text='<%#Eval("EMAIL2") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="REG FILLER1" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblREG_FILLER1" runat="server" Text='<%#Eval("REG_FILLER1") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Other IDNT DESC" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOther_IDNT_DESC" runat="server" Text='<%#Eval("Other_IDNT_DESC") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EXPIRY DATE" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEXPIRY_DATE" runat="server" Text='<%#Eval("EXPIRY_DATE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="IDENTITY PROOF SUBMITTED" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIDENTITY_PROOF_SUBMITTED" runat="server" Text='<%#Eval("IDENTITY_PROOF_SUBMITTED") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="IDVER STATUS ID1" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIDVER_STATUS_ID1" runat="server" Text='<%#Eval("IDVER_STATUS_ID1") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ID FILLER1" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID_FILLER1" runat="server" Text='<%#Eval("ID_FILLER1") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ID FILLER2" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID_FILLER2" runat="server" Text='<%#Eval("ID_FILLER2") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ID FILLER3" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID_FILLER3" runat="server" Text='<%#Eval("ID_FILLER3") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ID FILLER4" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID_FILLER4" runat="server" Text='<%#Eval("ID_FILLER4") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="RELATED FIREFNO" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRELATED_FIREFNO" runat="server" Text='<%#Eval("RELATED_FIREFNO") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>--%>
                                                
                                                <%--<asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="BATCH ID" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBatchid" runat="server" Text='<%#Eval("Batchid") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MODE" HeaderStyle-CssClass="HeaderText" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMODE" runat="server" Text='<%#Eval("MODE") %> '></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Creation Date & Time" HeaderStyle-CssClass="HeaderText" ItemStyle-Width="30%" >
                                                    <ItemTemplate>
                                                <asp:Label ID="lblDateTime" runat="server" Text='<%#Eval("CREATEDDATE")%>'>
                                                </asp:Label>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="STATUS" HeaderStyle-CssClass="HeaderText">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblStatus2" runat="server" Text='<%#Eval("Status") %> ' OnClick="lblStatus2_Click" ForeColor="Blue"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <%--added by rutuja --%>
                                            </Columns>
                                        </asp:GridView>
                                        <%--Gridview FIUpdate--%>
                                    </div>
                                    <%--<div class="modal-footer">
                                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                    </div>--%>
                                </div>

                            </div>
                        </div>

                        <!-- Modal -->

                    </div>
                    <%--Ended By Shubham--%>
                    <div style="border-collapse: collapse; display: block" runat="server" id="divOutput">
                        <%--LISTBOX SHOWING A LIST OF FILE TYPES.--%>
                        <%--ADD A GRIDVIEW WITH FEW COLUMNS--%>
                        <div>
                            <img src="../../assets/images/dashboard-icon/input_Folder_icon.png" />
                            <asp:Image runat="server" ImageUrl="../../assets/images/dashboard-icon/input_Folder_icon.png" Style="padding-top: -1px; display: none" ToolTip="Zip" Width="20px" Height="20px" />
                            <label>Input Folder (CRS to MW)</label>
                            <asp:TextBox ID="searchTextBox" float="right" Style="float: right;" CssClass="username" ispostback="false" placeholder="Enter File Name" runat="server"> 
                            </asp:TextBox>
                            <asp:Button ID="lnkdelete" runat="server" ispostback="true" OnClick="lnkdelete_Click" Style="display: none;" ForeColor="Black"></asp:Button>&nbsp;
                        </div>
                        <br />
                        <asp:GridView ID="GridView1" CssClass="footable" GridLines="Vertical" ShowFooter="false"
                            AllowPaging="true" PageSize="10" EmptyDataText="No Record Found"
                            AutoGenerateColumns="false" OnPageIndexChanging="GridView1_PageIndexChanging" runat="server" OnRowDataBound="GridView1_RowDataBound">

                            <EditRowStyle BackColor="#7C6F57" />
                            <%--<FooterStyle BackColor="#1C5E55" ForeColor="White" />--%>
                            <HeaderStyle BackColor="#00c5cc" ForeColor="White" Height="35px" CssClass="gridViewHeader" />
                            <PagerStyle ForeColor="#666666" BackColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#E3EAEB" />
                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="false" ForeColor="#333333" />

                            <Columns>
                                <asp:TemplateField HeaderText="Name" HeaderStyle-CssClass="HeaderText" ItemStyle-Width="40%">
                                    <ItemTemplate>
                                        <asp:Image ID="ImgFiletyp" runat="server" ImageUrl='<%#Eval("Img")%>' />
                                        <asp:Image ID="ImgFiletyp1" runat="server" ImageUrl='<%#Eval("Img")%>' Style="padding-top: -1px; display: none" ToolTip="Zip" Width="20px" Height="20px" />
                                     <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                        <%--<%# Item.Name %>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No. of Records" HeaderStyle-CssClass="HeaderText" ItemStyle-Width="10%" Visible="false">
                                    <ItemTemplate>
                                        <%--<asp:Label ID="lblRecords" runat="server" Text='<%#Eval("Records") %> '></asp:Label>--%>
                                        <asp:Label ID="lblRecords" runat="server" Text=''></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="File Size" HeaderStyle-CssClass="HeaderText" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <%-- <%# string.Format("{0:N1}", (decimal)Item.Length / 1024) %> KB--%>
                                        <asp:Label ID="lblLen" runat="server" Text='<%#Eval("Length").ToString()==""?"NA" :Eval("Length") %> '>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="File Extension" HeaderStyle-CssClass="HeaderText" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFileType" runat="server" Text='<%#Eval("Extension").ToString()==""?"NA" :Eval("Extension")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Creation Date & Time" HeaderStyle-CssClass="HeaderText" ItemStyle-Width="30%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDateTime" runat="server" Text='<%#Eval("CreationTime").ToString()==""?"NA" :Eval("CreationTime","{0:dd/MM/yyyy hh:mm:ss tt}")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="File Mode" HeaderStyle-CssClass="HeaderText" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMode" runat="server" Text='<%#Eval("Mode").ToString()==""?"NA" :Eval("Mode")%>'>
                                        </asp:Label>
                                    </ItemTemplate>              
                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="File Location" Visible="false" HeaderStyle-CssClass="HeaderText" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLoc" runat="server" Text='<%#Eval("Location").ToString()==""?"NA" :Eval("Location")%>'>
                                        </asp:Label>
                                    </ItemTemplate>              
                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="View Records" HeaderStyle-CssClass="HeaderText" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnViewMiddltoCERSAI" runat="server" class="glyphicon glyphicon-eye-open" OnClick="btnViewMiddltoCERSAI_Click" runat="server" Style="color: #337ab7;">
                                        </asp:LinkButton>
                                         <%--<img id="loadingImage" src="loading.gif" alt="Loading..." style="display:none;" />--%>
                                        <%--<img id="loadingImage" src="Images/spinner.gif" Height="15px" Width="15px">--%>
                                         <asp:Image ID="loadingImage" runat="server" AlternateText="Fetching.."  Height="25px" Width="25px"  />
                                         <asp:Label ID="lblStatus" Text="" runat="server"  Style="text-decoration-color:green" />
                                    </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="25%" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <p>
                            <asp:Label Text="" ID="lblMsg" runat="server" Visible="false"></asp:Label>
                        </p>
                        <br />


                    </div>
                    <div style="border-collapse: collapse; display: block;" runat="server" id="divInput">
                        <%-- Added byh Rutuja--%>
                        <div>
                            <img src="../../assets/images/dashboard-icon/output_Folder_icon.png" />
                            <asp:Image runat="server" ImageUrl="../../assets/images/dashboard-icon/output_Folder_icon.png" Style="padding-top: -1px; display: none" ToolTip="Text" Width="20px" Height="20px" />
                            <label>Output Folder (MW to CRS)</label>
                            <asp:TextBox ID="searchtextbox2" float="right" Style="float: right;" ispostback="false" CssClass="username" placeholder="Enter File Name" runat="server"> 
                            </asp:TextBox>
                            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click1" ispostback="true" Style="display: none;" ForeColor="Black"></asp:Button>
                        </div>
                        <br />
                        <asp:GridView ID="GridView2" CssClass="footable" GridLines="Vertical" ShowFooter="false"
                            AllowPaging="true" PageSize="10" EmptyDataText="No Record Found"
                            AutoGenerateColumns="false" 
                            OnPageIndexChanging="GridView2_PageIndexChanging" runat="server">

                            <EditRowStyle BackColor="#7C6F57" />
                            <%--<FooterStyle BackColor="#1C5E55" ForeColor="White" />--%>
                            <HeaderStyle BackColor="#00c5cc" ForeColor="White" Height="35px" CssClass="gridViewHeader" />
                            <PagerStyle ForeColor="#666666" BackColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#E3EAEB" />
                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="false" ForeColor="#333333" />

                            <Columns>
                                <asp:TemplateField HeaderText="Name" HeaderStyle-CssClass="HeaderText" ItemStyle-Width="40%">
                                    <ItemTemplate>
                                        
                                        <asp:Image ID="ImgFiletyp" runat="server" ImageUrl='<%#Eval("Img")%>' />
                                        <asp:Image ID="ImgFiletyp1" runat="server" ImageUrl='<%#Eval("Img")%>' Style="padding-top: -1px; display: none" ToolTip="Zip" Width="20px" Height="20px" />
                                        <%--  <asp:Image ID="ImgFiletyp" runat="server" ImageUrl="../../assets/images/dashboard-icon/txt_file_icon.png" />
                                        <asp:Image runat="server" ImageUrl="../../assets/images/dashboard-icon/txt_file_icon.png" Style="padding-top: -1px; display: none" ToolTip="Zip" Width="20px" Height="20px" />--%>
                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                        <%--<%# Item.Name %>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="No. of Records" HeaderStyle-CssClass="HeaderText" Visible="false" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRecords" runat="server" Text=''></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="File Size" HeaderStyle-CssClass="HeaderText" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <%--<%# string.Format("{0:N1}", (decimal)Item.Length / 1024) %> KB--%>
                                        <asp:Label ID="lblLen" runat="server" Text='<%#Eval("Length").ToString()==""?"NA" :Eval("Length") %> '></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="File Extension" HeaderStyle-CssClass="HeaderText" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFileType" runat="server" Text='<%#Eval("Extension").ToString()==""?"NA" :Eval("Extension")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Creation Date & Time" HeaderStyle-CssClass="HeaderText" ItemStyle-Width="30%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDateTime1" runat="server" Text='<%#Eval("CreationTime").ToString()==""?"NA" :Eval("CreationTime","{0:dd/MM/yyyy hh:mm:ss tt}")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="File Mode" HeaderStyle-CssClass="HeaderText" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMode" runat="server" Text='<%#Eval("Mode").ToString()==""?"NA" :Eval("Mode") %> '></asp:Label>
                                        </ItemTemplate>
                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                <asp:TemplateField Visible="false" HeaderText="File Flag" HeaderStyle-CssClass="HeaderText" ItemStyle-Width="30%">
                                    <ItemTemplate>
                                        <asp:Label ID="FileFlags" runat="server">
                                            <%--Text='<%#Eval("FileFlag")%>'--%>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="View Records" HeaderStyle-CssClass="HeaderText" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnViewCERSAItoMiddl" runat="server" class="glyphicon glyphicon-eye-open" OnClick="btnViewCERSAItoMiddl_Click" Style="color: #337ab7;">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="25%" />
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>
                        <%-- Ended by Rutuja--%>
                        <%--A LABEL SHOWING NUMBER OF FILES FOUND IN THE FOLDER.--%>
                        <p>
                            <asp:Label Text="" ID="lblMsg2" runat="server" Visible="false"></asp:Label>
                        </p>
                    </div>
                    <div class="modal fade in" id="myModal23" style="display: none">
                        <div class="modal-dialog">
                            <div class="modal-content" style="">
                                <div class="modal-header" style="height: 40px;">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="ShowHideModal23();">&times;</button>
                                    <h4 class="modal-title">Error Result</h4>
                                </div>
                                <div class="modal-body" style="height: 450px; overflow-x: scroll; overflow-y: scroll;">
                                    <asp:GridView ID="gvErrorSearch" CssClass="footable" ShowFooter="false"
                                        AllowPaging="true" PageSize="15" AutoGenerateColumns="false" runat="server">
                                        <EditRowStyle BackColor="#7C6F57" />
                                        <HeaderStyle BackColor="#00c5cc" ForeColor="White" Width="100%" Height="35px" CssClass="gridViewHeader" />
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#E3EAEB" />
                                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="false" ForeColor="#333333" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="FIRefNo" HeaderStyle-CssClass="HeaderText">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblfirefno" runat="server" Text='<%#Eval("firefno") %> '></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="mGridL" HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Error Description" HeaderStyle-CssClass="HeaderText">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblerrordesc" runat="server" Text='<%#Eval("errordesc") %> '></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="mGridL" HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Error Code" HeaderStyle-CssClass="HeaderText">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblerrorcode" runat="server" Text='<%#Eval("errorcode") %> '></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="mGridL" HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Batch Id" HeaderStyle-CssClass="HeaderText">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbatchid" runat="server" Text='<%#Eval("batchid") %> '></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="mGridL" HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <%--</div>--%>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
