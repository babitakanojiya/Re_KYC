<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="ApplicationConfig.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.Admin.ApplicationConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Styles.Render("~/bundles/CKYCcss") %>


    <script src="../../Content/Bootstrap/AutoCompleteTextBox/jquery.min.js"></script>
    <script src="../../Content/Bootstrap/AutoCompleteTextBox/jquery-ui.min.js"></script>
    <link href="../../Content/Bootstrap/AutoCompleteTextBox/jquery-ui.css" rel="stylesheet" />
    <link href="../../assets/css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../assets/js/bootstrap-multiselect.js"></script>
    <script src="../../Scripts/ApplicationConfig.js?ver=102"></script>


    <style type="text/css">
        hr {
            margin-right: 30px;
        }

        .container {
            width: 1300px !important;
        }

        .pointer {
            cursor: pointer;
        }

        .panel-heading {
            padding: 10px 10px 10px 10px;
        }

        .columvalueCenter {
            text-align: center !important;
        }

        .columvalueLeft {
            text-align: left !important;
        }

        .columvalueRight {
            text-align: right !important;
        }

        /*Adde by arjun 17-03-2021*/

        .gridview th {
            padding: 3px;
            height: 40px;
            background-color: #00b4bf;
            color: #FFFFFF;
            border-color: gray;
            text-align: center;
            /* font-family: Helvetica Neue Helvetica Arial sans-serif;*/
            font-size: 15px;
            white-space: nowrap;
        }

            .gridview th a {
                color: White !important;
            }

        .GridViewSelectedRow {
            font-weight: bold;
            color: #333333;
        }

        .tab {
            overflow: hidden;
            border: 1px solid #ccc;
            background-color: #f1f1f1;
        }

            /* Style the buttons inside the tab */
            .tab button {
                background-color: inherit;
                float: left;
                border: none;
                outline: none;
                cursor: pointer;
                padding: 14px 16px;
                transition: 0.3s;
                font-size: 17px;
            }

                /* Change background color of buttons on hover */
                .tab button:hover {
                    background-color: #ddd;
                }

                /* Create an active/current tablink class */
                .tab button.active {
                    background-color: #ccc;
                }

        /* Style the tab content */
        .tabcontent2 {
            padding: 6px 12px;
            border: 1px solid #ccc;
            border-top: none;
        }

        /*ended by arjun 17-03-2021*/

        /* Small even padding on each side */
        input[type=checkbox], input[type=radio] {
            /*margin: 0 0 0 0 !important;*/
            margin: auto;
        }

        .radio_NEw {
            padding: 0px 6px;
        }
        /* Large amount of padding just on the right side */
        .radio_NEw {
            padding-right: 30px;
        }

        iti .radio-list {
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">

    <script>
        function changeMode(obj, textbox) {
            if ($(obj).hasClass('glyphicon-eye-open')) {
                $(obj).removeClass('glyphicon-eye-open').addClass('glyphicon-eye-close')
                document.getElementById(textbox).type = "text";
            }
            else {
                $(obj).addClass('glyphicon-eye-open').removeClass('glyphicon-eye-close')
                document.getElementById(textbox).type = "password";
            }
        }
    </script>

    <script type="text/javascript">
        function LoadAPI() { window.open("http://kmidev.centralus.cloudapp.azure.com/CkycWebApi/Help"); }

        function NotificationDivSlide(DivId, IconId) {
            //debugger;
            var ID = "#" + DivId;
            var Icon = "#" + IconId;
            $(document).ready(function () {
                $(Icon).hover(function () {
                    $(ID).show(1000);


                },
                    function () {
                        //This is onMouseOut event

                        $(ID).hide(1000);
                    });
            });
        }

        function getHeaderbyID(Code) {
            //queryString("MstrModuleCode")
            debugger;
            var getParam = "{'id':'" + Code + "'}";
            //var strDesc = 'testt'
            //if (strDesc != "N") {
            //    document.getElementById(idTo).innerHTML = strDesc;
            //}

            $.ajax({
                type: "POST",
                url: "ApplicationConfig.aspx/getHeaderbyIDMethod",
                contentType: "application/json; charset=utf-8",
                data: getParam,
                dataType: "json",
                success: function (data) {
                    successBindata(data, Code)
                },
                failure: function (data) {
                    alert(response.d);
                },
            });
        }
        function successBindata(data, Code) {
            debugger;

            document.getElementById(Code).innerHTML = data.d;
        }

    </script>


    <asp:ScriptManager ID="scrusdtls" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="container" style="margin-top: 0px; width: 100%;">
                <div class="page-container">
                    <div class="panel panel-success" style="margin-left: 2%; margin-right: 2%; margin-top: 0.5%">
                        <div class="panel-heading" onclick="showHideDiv('divCustomerAdd','btnToggleNew');return false;">
                            <div class="row" style="margin: 0px">
                                <div class="col-sm-10" style="text-align: left; margin-bottom: -7px;">
                                    <img src="../../Image/add_customer_icon.png" style="width: 35px" />
                                    <%--<span class="glyphicon glyphicon-menu-hamburger"></span>--%>
                                    <asp:Label ID="Label1" runat="server" Font-Bold="true" Font-Size="18px" Text="Initial Configuration for Financial Institution"></asp:Label>
                                </div>
                                <div class="col-sm-2">
                                    <span id="ReqDtlsInfoIcon" class="glyphicon glyphicon-question-sign" onmouseover="NotificationDivSlide('DivSrcReqDtlsNote','ReqDtlsInfoIcon');
                                    getHeaderbyID('IntlCnfgDtls');"
                                        style="float: left;  font-size: 18px; color: red; margin-left: 102px;"></span>
                                    <span id="btnToggleNew" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 0px 5px ! important; font-size: 18px; margin-right: 6%"></span>
                                </div>
                            </div>
                        </div>
                        <div style="background-color: #d5d5c3; display: none;" id="DivSrcReqDtlsNote">
                            <div class="row">
                                <div class="col-sm-12" style="margin-left: 17px; margin-top: 10px;">
                                    <span class="glyphicon glyphicon-alert" style="color: yellow"></span>&#160;&#160;
          <span id="IntlCnfgDtls" style="text-align: justify; font-size: 11px;"></span>
                                </div>
                            </div>
                        </div>
                        <div class="panel-body" id="divCustomerAdd">
                            <div class="container-fluid">
                                <div class="row custom-map">
                                    <div class="col-sm-6">
                                        <span class="control-label">Financial Institution (FI) Name
                                        <span style="color: red; vertical-align: baseline">*</span>
                                        </span>
                                        <asp:TextBox runat="server" ID="txtEntityName" CssClass="form-control" />
                                    </div>
                                    <div class="col-sm-3">
                                        <span class="control-label">Financial Institution (FI) Code
                                        <span style="color: red; vertical-align: baseline">*</span>
                                        </span>
                                        <asp:TextBox runat="server" ID="txtInsCode" CssClass="form-control" />
                                    </div>
                                    <div class="col-sm-3">
                                        <span class="control-label">Customer Type
                                        <span style="color: red; vertical-align: baseline">*</span>
                                        </span>
                                        <asp:DropDownList runat="server" ID="ddlConstType" Visible="false" CssClass="form-control">
                                            <%--<asp:ListItem Text="Select" Value="" />
                                        <asp:ListItem Text="Individual Entity" Value="1" />
                                        <asp:ListItem Text="Legal Entity" Value="2" />--%>
                                        </asp:DropDownList>

                                        <asp:ListBox runat="server" ID="ListConstType" SelectionMode="Multiple" CssClass="form-control"></asp:ListBox>

                                    </div>
                                </div>
                                <div class="row custom-map">
                                    <div class="col-sm-3">
                                        <span class="control-label">Branch Code
                                        <span style="color: red; vertical-align: baseline">*</span>
                                        </span>
                                        <asp:TextBox runat="server" ID="txtbranch" MaxLength="10" CssClass="form-control" />
                                    </div>
                                    <div class="col-sm-3">
                                        <span class="control-label">Region Code
                                        <span style="color: red; vertical-align: baseline">*</span>
                                        </span>
                                        <asp:TextBox runat="server" MaxLength="10" ID="txtregion" CssClass="form-control" />
                                    </div>
                                </div>

                                <hr />

                                <div class="row custom-map" style="margin-bottom: 22px; margin-top: 17px;">
                                    <div class="col-sm-12" style="padding-bottom: 6px;">
                                        <span class="control-label text-uppercase" style="font-weight: bold">FIS to MW INTEGRATION
                                        <span style="color: red; vertical-align: baseline">*</span>
                                        </span>
                                        <hr style="border-top: 4px solid #111; margin: 0; margin-right: 30px;" />
                                    </div>
                                    <div class="col-sm-3">
                                        <span class="control-label">Integration for Data
                                        <span style="color: red; vertical-align: baseline">*</span>
                                        </span>
                                        <div class="input-group">
                                            <div class="input-group-addon input-group-addon-tel" style="background-color: white;">
                                                <asp:ListBox runat="server" ID="ListIntTypeData" SelectionMode="Multiple" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ListIntTypeData_SelectedIndexChanged" CssClass="form-control"></asp:ListBox>
                                            </div>
                                            <asp:LinkButton ID="btnview" runat="server" Visible="false" ForeColor="Blue" Style="margin-left: -60px;" OnClientClick="LoadAPI();">View API
                                            </asp:LinkButton>
                                            <img src="../../assets/images/API_Icon_sm.png" onclick="LoadAPI();" class="pointer" style="position: absolute; margin-top: -3px; margin-left: -50px;" />
                                        </div>
                                    </div>
                                    <%-- <div class="col-sm-1" style="padding-top: 2%">
                                </div>--%>
                                    <div class="col-sm-3">
                                        <span class="control-label">Integration for Document
                                        <span style="color: red; vertical-align: baseline">*</span>
                                        </span>
                                        <asp:ListBox runat="server" ID="ListIntTypeDoc" SelectionMode="Multiple" OnSelectedIndexChanged="ListIntTypeDoc_SelectedIndexChanged" CssClass="form-control width" AutoPostBack="true"></asp:ListBox>
                                    </div>
                                    <div class="col-sm-6" runat="server" id="filePath">
                                        <span class="control-label">Input File Path</span>
                                        <asp:TextBox runat="server" ID="txtFilePath" CssClass="form-control" Style="width: 569px;" />
                                    </div>
                                </div>
                                <div class="row custom-map">

                                    <div class="col-sm-3" style="display: none">
                                        <asp:CheckBox ID="cbFIRef" runat="server" CssClass="control-label" AutoPostBack="true" OnCheckedChanged="cbFIRef_CheckedChanged"
                                            Style="font-weight: 100; margin-bottom: 0;" />
                                        <span class="control-label">FI Reference Number</span>
                                        <div class="input-group">
                                            <%--<span class="input-group-addon" style="width: 20% !important; border-top-left-radius: 7% !important; padding:0px !important; border:0px !important;">--%>
                                            <span class="input-group-addon input-group-addon-tel">
                                                <asp:TextBox ID="txtPrefix" runat="server" CssClass="form-control" MaxLength="2" Style="border-top-left-radius: 4px; border-bottom-left-radius: 4px;" Enabled="false"></asp:TextBox>
                                            </span>
                                            <asp:TextBox ID="txtFIRef" runat="server" CssClass="form-control" onkeypress="fncInputNumericValuesOnly();" Style="width: 83%;"
                                                MaxLength="10" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3" style="display: none">
                                        <asp:CheckBox ID="cbRelRef" runat="server" CssClass="control-label" AutoPostBack="true" OnCheckedChanged="cbRelRef_CheckedChanged"
                                            Style="font-weight: 100; margin-bottom: 0;" />
                                        <span class="control-label">Related Person Reference Number</span>
                                        <asp:TextBox runat="server" ID="txtRelRef" CssClass="form-control" Style="width: 100%;" Enabled="false" />
                                    </div>
                                    <div class="col-sm-3" style="display: none" id="divemail" runat="server">
                                        <span class="control-label">Output Email Id</span>
                                        <span style="color: red; vertical-align: baseline">*</span>
                                        <asp:TextBox runat="server" ID="txtEmailID" autocomplete="off" CssClass="form-control" />
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                </div>
                                <hr />


                                <div class="row custom-map" id="div22" style="margin-bottom: 22px; margin-top: 17px;" runat="server">
                                    <div class="col-sm-12" style="padding-bottom: 6px;">
                                        <span class="control-label text-uppercase" style="font-weight: bold">MW to CRS INTEGRATION
                                        <span style="color: red; vertical-align: baseline">*</span>
                                        </span>
                                        <hr style="border-top: 4px solid #111; margin: 0; margin-right: 30px;" />

                                    </div>

                                    <div class="col-sm-3">
                                        <span class="control-label">Integration for Data
                                        <span style="color: red; vertical-align: baseline">*</span>
                                        </span>
                                        <asp:ListBox runat="server" ID="ListFISMW1" SelectionMode="Multiple" AutoPostBack="true" OnSelectedIndexChanged="ListFISMW1_SelectedIndexChanged"
                                            CssClass="form-control">
                                            <asp:ListItem Value="1">    SFTP </asp:ListItem>


                                        </asp:ListBox>
                                    </div>
                                    <div class="col-sm-3">
                                        <span class="control-label">Integration for Document
                                        <span style="color: red; vertical-align: baseline">*</span>
                                        </span>
                                        <asp:ListBox runat="server" ID="ListFISMW2" SelectionMode="Multiple" CssClass="form-control">
                                            <asp:ListItem Value="1">SFTP</asp:ListItem>

                                        </asp:ListBox>
                                    </div>

                                </div>

                                <div class="row custom-map" id="div23" runat="server">
                                    <div class="col-sm-12" style="padding-bottom: 6px;">

                                        <asp:RadioButtonList Visible="false" runat="server" ID="rdoCERSAIIntList" AutoPostBack="true" RepeatDirection="Horizontal"
                                            CssClass="radio-list" OnSelectedIndexChanged="rdoCERSAIIntList_SelectedIndexChanged" Style="width: 183px; margin-top: 6px;">
                                            <asp:ListItem class="radio_NEw" Text="&nbsp;SFTP" Value="1" />
                                            <asp:ListItem class="radio_NEw" Text="&nbsp;BULK" Value="2" />
                                        </asp:RadioButtonList>
                                    </div>
                                    <div id="SFTPDetails" runat="server">
                                        <div class="col-sm-3">
                                            <span class="control-label">CRS SFTP Path</span>
                                            <asp:TextBox runat="server" ID="txtCERSAISFTPPath" CssClass="form-control" />
                                        </div>
                                        <div class="col-sm-3">
                                            <span class="control-label">CRS Username</span>
                                            <asp:TextBox runat="server" ID="txtCERSAIUserNameKey" autocomplete="off" CssClass="form-control" />
                                        </div>
                                        <div class="col-sm-3">
                                            <span class="control-label">CRS Password</span>
                                            <div class="input-group">
                                                <asp:TextBox runat="server" ID="txtCERSAIPassKey" autocomplete="off" ClientIDMode="Static" CssClass="form-control" />
                                                <span class="input-group-addon">
                                                    <i class="glyphicon glyphicon-eye-close" onclick="changeMode(this, 'txtCERSAIPassKey')"></i>
                                                </span>
                                            </div>
                                        </div>
                                        <br />

                                    </div>

                                    <div id="BULKDetails" runat="server">
                                        <div class="col-sm-3">
                                            <span class="control-label">FI SFTP Path</span>
                                            <asp:TextBox runat="server" ID="txtCKYCSFTPPath" CssClass="form-control" />
                                        </div>
                                        <div class="col-sm-3">
                                            <span class="control-label">FI Username</span>
                                            <asp:TextBox runat="server" ID="txtCKYCUserNameKey" autocomplete="off" CssClass="form-control" />
                                        </div>
                                        <div class="col-sm-3">
                                            <span class="control-label">FI Password</span>
                                            <div class="input-group">
                                                <asp:TextBox runat="server" ID="txtCKYCPassKey" autocomplete="off" ClientIDMode="Static" CssClass="form-control" />
                                                <span class="input-group-addon">
                                                    <i class="glyphicon glyphicon-eye-open" onclick="changeMode(this, 'txtCKYCPassKey' )"></i>
                                                </span>
                                            </div>
                                        </div>
                                        <hr />
                                    </div>
                                </div>
                                <div class="row custom-map" id="div24" runat="server">
                                    <div class="col-sm-3">
                                        <span class="control-label">Probable Match
                                        <span style="color: red; vertical-align: baseline">*</span>
                                        </span>
                                        <asp:DropDownList runat="server" ID="ddlMatch" CssClass="form-control">
                                            <%--<asp:ListItem Text="Yes (Default)" Value="Y" />
                                        <asp:ListItem Text="No (Default)" Value="N" />
                                        <asp:ListItem Text="Yes-Image Assisted" Value="N" />--%>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3">
                                        <span class="control-label">Search
                                        <span style="color: red; vertical-align: baseline">*</span>
                                        </span>
                                        <asp:DropDownList runat="server" ID="ddlSearch" CssClass="form-control">
                                            <%--<asp:ListItem Text="Select" Value="" />
                                        <asp:ListItem Text="Single API Call" Value="1" />
                                        <asp:ListItem Text="Excel Bulk Upload" Value="2"></asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3">
                                        <span class="control-label">Download
                                        <span style="color: red; vertical-align: baseline">*</span>
                                        </span>
                                        <asp:DropDownList runat="server" ID="ddlDownload" CssClass="form-control">
                                            <%--<asp:ListItem Text="Select" Value="" />
                                        <asp:ListItem Text="Single API Call" Value="1" />
                                        <asp:ListItem Text="Excel Bulk Download" Value="2"></asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row custom-map" id="div25" runat="server">
                                    <div class="col-sm-3">
                                        <span class="control-label">Digital Signature Path</span>
                                        <asp:TextBox runat="server" ID="txtDSPath" CssClass="form-control" />
                                    </div>
                                    <div class="col-sm-3">
                                        <span class="control-label">DS Public Key</span>
                                        <asp:TextBox runat="server" ID="txtPubKey" CssClass="form-control" />
                                    </div>
                                    <div class="col-sm-3">
                                        <span class="control-label">DS Private Key</span>
                                        <asp:TextBox runat="server" ID="txtPrvKey" CssClass="form-control" />
                                    </div>
                                </div>


                                <div class="row">
                                    <div style="padding: 10px; margin: 0 auto">
                                        <center>
                                    <asp:Button Text="Save" runat="server" CssClass="btn btn-primary" ID="btnSave" OnClick="btnSave_Click" />
                                    <asp:Button Text="Update" runat="server" CssClass="btn btn-primary" ID="btnUpdate" OnClick="btnUpdate_Click" />
                                    <asp:Button Text="Clear" CssClass="btn btn-danger" runat="server" ID="btnClear" OnClick="btnClear_Click" />
                                        </center>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="panel-heading" onclick="showHideDiv('divOfficeandUSer','btnToggleNew2');return false;" id="div26" runat="server">
                            <div class="row" style="margin: 0px">
                                <div class="col-sm-10" style="text-align: left; margin-bottom: -7px;">
                                    <img src="../../Image/add_customer_icon.png" style="width: 35px" />
                                    <%--<span class="glyphicon glyphicon-menu-hamburger"></span>--%>
                                    <asp:Label ID="Label2" runat="server" Font-Bold="true" Font-Size="18px" Text="Office & User Setup"></asp:Label>
                                </div>
                                <div class="col-sm-2">
                                    <span id="btnToggleNew2" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                                </div>
                            </div>
                        </div>
                        <div class="panel-body" id="divOfficeandUSer" runat="server">


                            <%--  <button  id="test1" onclick="openCity('London','Paris')">London</button>
                            <button id="test2" onclick="openCity('Paris','London')">Paris</button>--%>
                            <div class="tab">

                                <%--                             <a class="tablinks" onclick="openCity('London','Paris')" >London</a>
                              <a class="tablinks" onclick="openCity('Paris','London')" >Paris</a>--%>


                                <button class="tablinks" id="1" style="background-color: #ccc" onclick="openCity('London','Paris',1);return false">Office Organization List</button>
                                <button class="tablinks" id="2" onclick="openCity('Paris','London',2);return false">Office Organization Chart</button>

                                <input type="hidden" id="tabid" value="London" />
                            </div>

                            <div id="London" class="tabcontent" style="display: block">
                                <asp:GridView ID="dgCmp" runat="server" AutoGenerateColumns="false" Width="100%"
                                    PageSize="10" AllowSorting="True" AllowPaging="true"
                                    CssClass="footable" OnRowDataBound="dgCmp_RowDataBound" DataKeyNames="OFFICE_CODE">
                                    <RowStyle CssClass="GridViewRowNew"></RowStyle>
                                    <PagerStyle CssClass="disablepage" />
                                    <HeaderStyle CssClass="gridview th" />
                                    <Columns>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <img alt="test" id="imgnext" runat="server" name="btnexp"
                                                    style="cursor: pointer" src="../../images/btnexp.png" />



                                                <div id="divChild" runat="server" style="display: none; width: auto; margin: 0px 0 !important;"
                                                    class="table-scrollable,divBorder1">
                                                    <asp:UpdatePanel ID="UpdatePanel61" runat="server">
                                                        <ContentTemplate>
                                                            <%--                                                            <asp:GridView ID="dgCntst" runat="server" AutoGenerateColumns="false" Width="100%"
                                                                PageSize="10" AllowSorting="False" AllowPaging="true" CssClass="footable" OnRowDataBound="dgCntst_RowDataBound"
                                                                DataKeyNames="CNTSTNT_CODE">--%>
                                                            <asp:GridView ID="dgCntst" runat="server" AutoGenerateColumns="false" Width="100%"
                                                                AllowSorting="False" AllowPaging="false" CssClass="footable">

                                                                <RowStyle CssClass="GridViewRowNew"></RowStyle>
                                                                <PagerStyle CssClass="disablepage" />
                                                                <HeaderStyle CssClass="gridview th" />
                                                                <Columns>


                                                                    <asp:TemplateField HeaderText="User ID" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                                        SortExpression="[User ID]">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lnkUserID" Text='<%# Bind("[User ID]")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="User Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                                        SortExpression="[User Name]">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lnkUserName" Text='<%# Bind("[User Name]")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Member Type" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                                        SortExpression="[Member Type]">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lnkMemberType" Text='<%# Bind("[Member Type]")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Office Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                                        SortExpression="[Office Name]">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lnkOfficeName" Text='<%# Bind("[Office Name]")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Office Type" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                                        SortExpression="[Office Type]">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lnkOfficeType" Text='<%# Bind("[Office Type]")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="CRS User" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                                        SortExpression="[CRS User]">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lnkCRSUser" Text='<%# Bind("[CRS User]")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" CssClass="columvalueCenter" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="CRS User Type" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                                        SortExpression="[CRS User Type]">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lnkCRSUserType" Text='<%# Bind("[CRS User Type]")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" CssClass="columvalueCenter" />
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="CRS User Code" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                                        SortExpression="[CRS User Code]">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lnkCRSUserCode" Text='<%# Bind("[CRS User Code]")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" CssClass="columvalueCenter" />
                                                                    </asp:TemplateField>

                                                                </Columns>

                                                            </asp:GridView>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Office Code" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                            SortExpression="OFFICE_CODE">
                                            <ItemTemplate>
                                                <asp:Label ID="lnkCmpCode" Text='<%# Bind("OFFICE_CODE")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" CssClass="columvalueCenter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Office Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                            SortExpression="OfficeName">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="OfficeName" Text='<%# Bind("OfficeName")%>'></asp:Label>


                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Office Type" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                            SortExpression="OfficeType">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="OfficeType" Text='<%# Bind("OfficeType")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" CssClass="columvalueCenter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reporting Office" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                            SortExpression="ReportingOffice">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="ReportingOffice" Text='<%# Bind("ReportingOffice")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Effective Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                            SortExpression="effectivedate">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="effectivedate" Text='<%# Bind("effectivedate")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" CssClass="columvalueCenter" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                            </div>

                            <div id="Paris" class="tabcontent" style="display: none">

                                <iframe src="HTMLPage_new_html.html" width="100%" height="1000px;"></iframe>
                            </div>





                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">



        $(document).ready(function () {

            $("[src*=btnexp]").on("click", function () {

                debugger;

                if (document.getElementById(this.id).name == 'btnexp') {
                    $(this).closest("tr").after("<tr><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
                    $(this).attr("src", "../../images/col.png");
                    //$(this).attr("alt", "test2");
                    //alert("The paragraph was clicked.");

                    document.getElementById(this.id).name = 'col'
                } else {

                    $(this).attr("src", "../../images/btnexp.png");
                    $(this).attr("alt", "test");
                    $(this).closest("tr").next().remove();

                    document.getElementById(this.id).name = 'btnexp'

                }
            });
            //$("[src*=col]").live("click", function () {
            //    debugger;

            //    //alert("The paragraph  clicked.");
            //});

            BindMultiSelect();

        });

        function BindMultiSelect() {
            debugger;
            $('#<%= ListIntTypeDoc.ClientID %>').multiselect({
                numberDisplayed: 1,
                includeSelectAllOption: true,
                //buttonWidth: $('#<%= ListIntTypeDoc.ClientID %>').width() + 'px'
                //buttonWidth: $('#<%= ListIntTypeDoc.ClientID %>').width() + 'px'
                buttonWidth: 354.562
            });

            $('#<%= ListIntTypeData.ClientID %>').multiselect({
                numberDisplayed: 1,
                includeSelectAllOption: true,
                //buttonWidth: ($('#<%= ListIntTypeData.ClientID %>').width()) + 'px'
                buttonWidth: 313.562
            });

            $('#<%= ListConstType.ClientID %>').multiselect({
                numberDisplayed: 1,
                includeSelectAllOption: true,
                <%-- buttonWidth: ($('#<%= ListConstType.ClientID %>').width()) + 'px'--%>
                buttonWidth: 319
            });

            $('#<%= ListFISMW1.ClientID %>').multiselect({
                numberDisplayed: 1,
                includeSelectAllOption: true,
                 <%--buttonWidth: ($('#<%= ListFISMW1.ClientID %>').width()) + 'px'--%>
                buttonWidth: 351,

            });

            $('#<%= ListFISMW2.ClientID %>').multiselect({
                numberDisplayed: 1,
                includeSelectAllOption: true,
                buttonWidth: 351

            });

            //var id = document.getElementById("tabid").value
            //document.getElementById(id).style.display = "block";

            //if (id = 'London') {
            //    document.getElementById('Paris').style.display = "none";
            //}
            //else {
            //    document.getElementById('London').style.display = "none";

            //}
        }

        // Async postback handling
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            BindMultiSelect();


        });


        function SuccessAlert(msg) {
            alert(msg);
            window.location.reload();
        }


    </script>
</asp:Content>
