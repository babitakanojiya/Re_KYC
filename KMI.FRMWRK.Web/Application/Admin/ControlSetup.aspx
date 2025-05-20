<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="ControlSetup.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.Admin.ControlSetup" %>

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
            text-align: center !Important;
        }
    </style>
    <script>
        function removeSpecialChar(str) {
            return str.replace(/[^\w\s]/gi, '');
        }

        function SetControlID(value) {
            document.getElementById("txtControlID").value = removeSpecialChar(value).split(' ').join('');
        }

        function AlertMsg(msg) {
            debugger;
            showModal('#myModal', 'Information', 'alert-warning', '', '', msg);
        }

    </script>
    <asp:ScriptManager ID="scrusdtls" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="page-container">
                <div class="panel panel-success" style="margin-left: 2%; margin-right: 2%; margin-top: 0.5%">
                    <div class="panel-heading" onclick="showHideDiv('divCustomerAdd','btnToggleNew');return false;">
                        <div class="row" style="margin: 0px">
                            <div class="col-sm-10" style="text-align: left">
                                <asp:Label ID="Label1" runat="server" Font-Bold="true" Font-Size="18px" Text="Control Setup"></asp:Label>
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
                                    <span class="control-label">Constitution Type
                                        <span style="color: red; vertical-align: baseline">*</span>
                                    </span>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlConstitution">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-3">
                                    <span class="control-label">Control Name
                                        <span style="color: red; vertical-align: baseline">*</span>
                                    </span>
                                    <asp:TextBox runat="server" ID="txtControlName" onblur="SetControlID(this.value)" CssClass="form-control" />
                                </div>
                                <div class="col-sm-3">
                                    <span class="control-label">Control ID
                                        <span style="color: red; vertical-align: baseline">*</span>
                                    </span>
                                    <asp:TextBox runat="server" ID="txtControlID" ClientIDMode="Static" CssClass="form-control" />
                                </div>
                            </div>
                            <hr />
                            <div class="row custom-map">
                                <div class="col-sm-3">
                                    <span class="control-label">Control Type
                                        <span style="color: red; vertical-align: baseline">*</span>
                                    </span>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlControlType">
                                        <asp:ListItem Text="text1" />
                                        <asp:ListItem Text="text2" />
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-3">
                                    <span class="control-label">Data Size
                                        <span style="color: red; vertical-align: baseline">*</span>
                                    </span>
                                    <asp:TextBox runat="server" ID="txtDataSize" MaxLength="4" onkeypress="fncInputNumericValuesOnly()" CssClass="form-control" />
                                </div>
                                <div class="col-sm-3">
                                    <span class="control-label">Is Mandatory
                                        <span style="color: red; vertical-align: baseline">*</span>
                                    </span>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlIsMandatory">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-3">
                                    <span class="control-label">Is Master
                                        <span style="color: red; vertical-align: baseline">*</span>
                                    </span>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlIsMaster">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <hr />
                            <div class="row custom-map">
                                <div class="col-sm-3">
                                    <span class="control-label">Table Name
                                        <span style="color: red; vertical-align: baseline">*</span>
                                    </span>
                                    <asp:TextBox runat="server" ID="txtTableName" CssClass="form-control" />
                                </div>
                                <div class="col-sm-3">
                                    <span class="control-label">Column Name
                                        <span style="color: red; vertical-align: baseline">*</span>
                                    </span>
                                    <asp:TextBox runat="server" ID="txtColumnName" CssClass="form-control" />
                                </div>
                                <div class="col-sm-3">
                                    <span class="control-label">Column Value
                                        <span style="color: red; vertical-align: baseline">*</span>
                                    </span>
                                    <asp:TextBox runat="server" ID="txtColumnValue" CssClass="form-control" />
                                </div>
                            </div>
                            <hr />
                            <div class="row custom-map">
                                <div class="col-sm-3">
                                    <span class="control-label">Is Visible
                                        <span style="color: red; vertical-align: baseline">*</span>
                                    </span>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlIsVisible">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-3">
                                    <span class="control-label">Container
                                        <span style="color: red; vertical-align: baseline">*</span>
                                    </span>
                                    <asp:TextBox runat="server" ID="txtContainer" CssClass="form-control" />
                                </div>
                                <div class="col-sm-3">
                                    <span class="control-label">Order
                                        <span style="color: red; vertical-align: baseline">*</span>
                                    </span>
                                    <asp:TextBox runat="server" ID="txtOrder" onkeypress="fncInputNumericValuesOnly()" CssClass="form-control" />
                                </div>
                                <div class="col-sm-3">
                                    <span class="control-label">Is Active
                                        <span style="color: red; vertical-align: baseline">*</span>
                                    </span>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlActive">
                                        <asp:ListItem Text="text1" />
                                        <asp:ListItem Text="text2" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <hr />
                            <div class="row" style="padding: 10px 15px">
                                <center>
                                    <div id="DivSave" runat="server">
                                        <asp:Button Text="Save" runat="server" CssClass="btn btn-primary" ID="btnSave" OnClick="btnSave_Click" />
                                        <asp:Button Text="Clear" runat="server" CssClass="btn btn-danger" ID="btnClear" OnClick ="btnClear_Click" />
                                    </div>
                                    <div id="DivUpdate" runat="server">
                                        <asp:Button Text="Update" runat="server" CssClass="btn btn-primary" ID="btnUpdate" OnClick="btnUpdate_Click" />
                                        <asp:Button Text="Cancel" runat="server" CssClass="btn btn-danger" ID="btnCancel" OnClick="btnCancel_Click" />
                                    </div>
                                </center>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
