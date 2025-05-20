<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="ApplicationConfig.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.Admin.ApplicationConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Styles.Render("~/bundles/CKYCcss") %>
    <script src="../../Content/Bootstrap/AutoCompleteTextBox/jquery.min.js"></script>
    <script src="../../Content/Bootstrap/AutoCompleteTextBox/jquery-ui.min.js"></script>
    <link href="../../Content/Bootstrap/AutoCompleteTextBox/jquery-ui.css" rel="stylesheet" />
    <link href="../../assets/css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../assets/js/bootstrap-multiselect.js"></script>
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
    </style>

    <asp:ScriptManager ID="scrusdtls" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="page-container">
                <div class="panel panel-success" style="margin-left: 2%; margin-right: 2%; margin-top: 0.5%">
                    <div class="panel-heading" onclick="showHideDiv('divCustomerAdd','btnToggleNew');return false;">
                        <div class="row" style="margin: 0px">
                            <div class="col-sm-10" style="text-align: left">
                                <img src="../../Image/add_customer_icon.png" style="width: 35px" />
                                <%--<span class="glyphicon glyphicon-menu-hamburger"></span>--%>
                                <asp:Label ID="Label1" runat="server" Font-Bold="true" Font-Size="18px" Text="Add Customer"></asp:Label>
                            </div>
                            <div class="col-sm-2">
                                <span id="btnToggleNew" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div>
                    <div class="panel-body" id="divCustomerAdd">
                        <div class="container-fluid">
                            <div class="row custom-map">
                                <div class="col-sm-6">
                                    <span class="control-label">Entity Name
                                        <span style="color: red; vertical-align: baseline">*</span>
                                    </span>
                                    <asp:TextBox runat="server" ID="txtEntityName" CssClass="form-control" />
                                </div>
                                <div class="col-sm-3">
                                    <span class="control-label">Institution Code
                                        <span style="color: red; vertical-align: baseline">*</span>
                                    </span>
                                    <asp:TextBox runat="server" ID="txtInsCode" CssClass="form-control" />
                                </div>
                                <div class="col-sm-3">
                                    <span class="control-label">Constitution Type
                                        <span style="color: red; vertical-align: baseline">*</span>
                                    </span>
                                    <asp:DropDownList runat="server" ID="ddlConstType" CssClass="form-control">
                                        <%--<asp:ListItem Text="Select" Value="" />
                                        <asp:ListItem Text="Individual Entity" Value="1" />
                                        <asp:ListItem Text="Legal Entity" Value="2" />--%>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="row custom-map">
                                <div class="col-sm-3">
                                    <span class="control-label">Core System Integration for Data
                                        <span style="color: red; vertical-align: baseline">*</span>
                                    </span>
                                    <asp:ListBox runat="server" ID="ListIntTypeData" SelectionMode="Multiple" AutoPostBack="true"
                                        OnSelectedIndexChanged="ListIntTypeData_SelectedIndexChanged" CssClass="form-control"></asp:ListBox>
                                </div>
                                <div class="col-sm-3">
                                    <span class="control-label">Core System Integration for Document
                                        <span style="color: red; vertical-align: baseline">*</span>
                                    </span>
                                    <asp:ListBox runat="server" ID="ListIntTypeDoc" SelectionMode="Multiple" CssClass="form-control"></asp:ListBox>
                                </div>
                                <div class="col-sm-6" runat="server" id="filePath">
                                    <span class="control-label">File Path</span>
                                    <asp:TextBox runat="server" ID="txtFilePath" CssClass="form-control" />
                                </div>
                            </div>
                            <div class="row custom-map">
                                <div class="col-sm-3">
                                    <asp:CheckBox ID="cbNew" runat="server" CssClass="standardcheckbox" Text="FI Reference Number" AutoPostBack="true"
                                    Enabled="false" TabIndex="20" />
                                    <asp:TextBox runat="server" ID="txtPrefix" CssClass="form-control" />
                                    <asp:TextBox runat="server" ID="txtNo" CssClass="form-control" />
                                </div>
                                <div class="col-sm-3">
                                     <asp:CheckBox ID="CheckBox1" runat="server" CssClass="standardcheckbox" Text="Relative Reference Number" AutoPostBack="true"
                                    Enabled="false" TabIndex="20" />
                                    <asp:TextBox runat="server" ID="txtRelRefNo" CssClass="form-control" />
                                </div>
                                <hr />
                                <div class="row custom-map">
                                    <div class="col-sm-12">
                                        <span class="control-label text-uppercase" style="font-weight: bold">CERSAI INTEGRATION
                                        <span style="color: red; vertical-align: baseline">*</span>
                                        </span>

                                        <asp:RadioButtonList runat="server" ID="rdoCERSAIIntList" AutoPostBack="true" RepeatDirection="Horizontal"
                                            CssClass="radio-list" OnSelectedIndexChanged="rdoCERSAIIntList_SelectedIndexChanged">
                                            <asp:ListItem Text="SFTP" Value="1" />
                                            <asp:ListItem Text="BULK" Value="2" />
                                        </asp:RadioButtonList>
                                    </div>
                                    <div id="SFTPDetails" runat="server">
                                        <div class="col-sm-3">
                                            <span class="control-label">CERSAI SFTP Path</span>
                                            <asp:TextBox runat="server" ID="txtCERSAISFTPPath" CssClass="form-control" />
                                        </div>
                                        <div class="col-sm-3">
                                            <span class="control-label">CERSAI Username</span>
                                            <asp:TextBox runat="server" ID="txtCERSAIUserNameKey" autocomplete="off" CssClass="form-control" />
                                        </div>
                                        <div class="col-sm-3">
                                            <span class="control-label">CERSAI Password</span>
                                            <div class="input-group">
                                                <asp:TextBox runat="server" ID="txtCERSAIPassKey" autocomplete="off" ClientIDMode="Static" TextMode="Password" CssClass="form-control" />
                                                <span class="input-group-addon">
                                                    <i class="glyphicon glyphicon-eye-open" onclick="changeMode(this, 'txtCERSAIPassKey')"></i>
                                                </span>
                                            </div>
                                        </div>
                                        <br />
                                        <hr />
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
                                                <asp:TextBox runat="server" ID="txtCKYCPassKey" autocomplete="off" ClientIDMode="Static" TextMode="Password" CssClass="form-control" />
                                                <span class="input-group-addon">
                                                    <i class="glyphicon glyphicon-eye-open" onclick="changeMode(this, 'txtCKYCPassKey' )"></i>
                                                </span>
                                            </div>
                                        </div>
                                        <hr />
                                    </div>
                                </div>
                                <div class="row custom-map">
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
                                <div class="row custom-map">
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
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        $(document).ready(function () {
            BindMultiSelect();
        });

        function BindMultiSelect() {
            $('#<%= ListIntTypeData.ClientID %>').multiselect({
                numberDisplayed: 1,
                includeSelectAllOption: true,
                buttonWidth: ($("#EmptyPagePlaceholder_ListIntTypeData").width()) + 'px'
            });
            $('#<%= ListIntTypeDoc.ClientID %>').multiselect({
                numberDisplayed: 1,
                includeSelectAllOption: true,
                buttonWidth: $("#EmptyPagePlaceholder_ListIntTypeDoc").width() + 'px'
            });
        }

        // Async postback handling
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            BindMultiSelect();
        });
    </script>
</asp:Content>
