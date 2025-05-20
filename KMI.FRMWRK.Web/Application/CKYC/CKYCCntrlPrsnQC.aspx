<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="CKYCCntrlPrsnQC.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.CKYC.CKYCCntrlPrsnQC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">


    <script type="text/javascript">

        function popup() {
            $("#myModal").modal();
        }

        function openModal() {
            $('#demoModal').modal('show');
        }

        function OpenStateWindow(Flag1) {
            debugger;

            var flag = Flag1
            var e = document.getElementById("<%= ddlState.ClientID %>");
            var strUser = e.options[e.selectedIndex].value;
            //var strUser = e.options[e.selectedIndex].innerText;
            // var StCode = document.getElementById("<%= ddlState.ClientID %>").value;
            window.open("PinCodeDtls.aspx?StateCode=" + strUser + "&flag=" + flag + "", '', 'width=640,height=354,toolbar=no,scrollbars=yes,resizable=yes,left=300,top=230,location=0;');
        }

        function Closepopup() {
            debugger;
            $('#modalConfirmYesNo').hide();
            $('#myModalRaise').hide();

        }

        function AsyncConfirmYesNo(title, msg, yesFn, noFn) {
            debugger;
            var $confirm = $("#modalConfirmYesNo");
            $confirm.modal('show');
            $("#lblTitleConfirmYesNo").html(title);
            $("#lblMsgConfirmYesNo").html(msg);
            $("#btnYesConfirmYesNo").off('click').click(function () {
                yesFn();
                $confirm.modal("hide");
            });
            $("#btnNoConfirmYesNo").off('click').click(function () {
                noFn();
                $confirm.modal("hide");
            });
        }

        function datePicker() {

            if (document.getElementById(strContent + "ddlProofIdentity").selectedIndex == 1) {


            }

        }

    </script>

    <script type="text/javascript">

        $(function () {

            <%--$("#EmptyPagePlaceholder_txtPassExpDate").datepicker({ dateFormat: 'dd/mm/yy' });
            $("#EmptyPagePlaceholder_txtPassExpDate1").datepicker({ dateFormat: 'dd/mm/yy' });
            $("#<%= txtDOB.ClientID  %>").datepicker({ dateFormat: 'dd/mm/yy' });
            $("#<%= txtDate.ClientID  %>").datepicker({ dateFormat: 'dd/mm/yy' });
            $("#<%= txtDateKYCver.ClientID  %>").datepicker({ dateFormat: 'dd/mm/yy' });--%>


        });

        function funddlProofRelPerson() {
            //  $("#ct100_ctl00_ContentPlaceHolder1_A3_ddlProofRelPerson").click(function () {
            debugger;
            $('#menu1').attr("class", "tab-pane fade");
            $('#EmptyPagePlaceholder_personal').attr("aria-expanded", false);
            $('#EmptyPagePlaceholder_m1').removeAttr("class");
            $('#EmptyPagePlaceholder_m3').attr("class", "active");
            $('#EmptyPagePlaceholder_A3').attr("aria-expanded", true);
            $('#menu4').attr("class", "tab-pane fade in active");
        }
    </script>
    <script type="text/javascript">

        function StartProgressBar() {

            var myExtender = $find('ProgressBarModalPopupExtender');
            myExtender.show();
            //document.getElementById("btnSubmit").click();
            return true;
        }


        //function PAN format
        function CheckPANFormat(strPANNo) {

            var result = true;
            var pan = strPANNo.split(",");
            var Char;

            var pan2 = pan[0]
            if (pan2 != "") {
                if (pan2.length == 10) {
                    for (j = 0; j < pan2.length && result == true; j++) {
                        Char = pan2.substring(j, j + 1);

                        if (j == 0 || j == 1 || j == 2 || j == 3 || j == 4 || j == 9) {
                            if (!isAlphabet(Char)) {
                                return 0;
                            }
                            if (j == 3) {
                                //var pan4char = pan2.substring(j,j+1);
                                if (pan2.substring(j, j + 1) != 'P')
                                    //if( pan4char != 'P' && pan4char != 'C')
                                {
                                    return 0;
                                }
                            }
                        }
                        if (j == 5 || j == 6 || j == 7 || j == 8) {
                            if (!IsNumeric(Char)) {
                                return 0;
                            }
                        }
                    }
                }
            }
            else {
                return 0;
            }

            return 1;

        }

        function validateEmail1(sEmail1) {
            //debugger;
            var reEmail = /^(?:[\w\!\#\$\%\&\'\*\+\-\/\=\?\^\`\{\|\}\~]+\.)*[\w\!\#\$\%\&\'\*\+\-\/\=\?\^\`\{\|\}\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\-](?!\.)){0,61}[a-zA-Z0-9]?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\[(?:(?:[01]?\d{1,2}|2[0-4]\d|25[0-5])\.){3}(?:[01]?\d{1,2}|2[0-4]\d|25[0-5])\]))$/;

            if (!sEmail1.match(reEmail)) {
                alert("Please Enter Valid Email-1 Address");
                document.getElementById("EmptyPagePlaceholder_txtemail").focus();
                var myExtender = $find('ProgressBarModalPopupExtender'); myExtender.hide();
                return 0;
            }

            return 1;

        }

    </script>

    <style type="text/css">
        .nav-tabs > li.active > a, .nav-tabs > li.active > a:hover, .nav-tabs > li.active > a:focus {
            color: #555555;
            background-color: #dff0d8;
        }

        .modal-dialog {
            position: relative;
            display: table;
            overflow-y: auto;
            overflow-x: auto;
            width: auto;
            min-width: 50px;
        }

        .gridview th {
            padding: 3px;
            height: 40px;
            background-color: #d6d6c2;
        }

        .disablepage {
            display: none;
        }

        ul#menu {
            padding: 0;
            margin-right: 69%;
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
                        background-color: red;
                    }
    </style>
    <asp:ScriptManager ID="SM1" runat="server">
        <%--<scripts>
            <asp:ScriptReference Path="../../../Application/Common/Lookup.js" /> 
        </scripts>
        <services>
            <asp:ServiceReference  Path="../../../Application/Common/Lookup.asmx" />
        </services>--%>
    </asp:ScriptManager>

    <div class="container" style="margin-top: 0px; width: 100%;">
        <%-- Added for CKYC Details header start--%>
        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
            <div id="Div18" runat="server" class="panel-heading" onclick="showHideDiv('divCKYCdtls','btnCKYCdtls');return false;">
                <div class="row">
                    <div class="col-sm-10" style="text-align: left">
                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                        <asp:Label ID="Label6" Text="DETAILS OF CONTROLLING PERSON" runat="server" CssClass="control-label">
                        </asp:Label>
                        <span style="color: red">*</span>
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
                        <asp:Label ID="lblRelRefNumber" Text="Reference Number" runat="server" Font-Bold="false"></asp:Label>
                    </div>

                    <div class="col-sm-3" style="text-align: left">
                        <asp:TextBox ID="txtRelRefNumber" runat="server" CssClass="form-control" Enabled="false"
                            Font-Bold="false" TabIndex="2">
                        </asp:TextBox>
                    </div>
                    <div class="col-sm-3">
                        <asp:Label ID="lblKYCNum" Text="KYC Number of Controlling Person (if available)" placeholder="KYC Number"
                            runat="server" CssClass="control-label"></asp:Label>

                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox CssClass="form-control" Enabled="false" runat="server" ID="txtKYCNum"
                            MaxLength="15" TabIndex="87" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblControlType" Text="Type of control" runat="server" CssClass="control-label">
                        </asp:Label>
                        <span style="color: red">*</span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblRelType" Text="In case of Legal Person" runat="server" CssClass="control-label">
                        </asp:Label>
                        <%--<span style="color: red">*</span>--%>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlRelType" runat="server" Enabled="false" CssClass="form-control" TabIndex="86">
                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblTrust" Text="In case of Trust" runat="server" CssClass="control-label">
                        </asp:Label>
                        <%--<span style="color: red">*</span>--%>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlTrust" runat="server" Enabled="false" CssClass="form-control" TabIndex="86">
                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblOthrLglArrange" Text="In case of Other Legal arrangement" runat="server" CssClass="control-label">
                        </asp:Label>
                        <%--<span style="color: red">*</span>--%>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlOthrLglArrange" Enabled="false" runat="server" CssClass="form-control" TabIndex="86">
                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>

        <asp:UpdatePanel ID="Updatepanel4" runat="server">
            <ContentTemplate>

                <asp:UpdatePanel ID="Updatepanel3" runat="server">
                    <ContentTemplate>
                        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">

                            <div id="Div19" runat="server" class="panel-heading">
                                <div class="row">
                                    <div class="col-sm-10" style="text-align: left">
                                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                                        <%--  <asp:checkbox id="ChkUpdPersonal" runat="server" cssclass="standardcheckbox" text="" autopostback="true"
                             tabindex="1" OnCheckedChanged="ChkUpdPersonal_Checked" />--%>
                                        <asp:Label ID="lblpfPersonal1" Text="PERSONAL DETAILS" runat="server" CssClass="control-label">
                                        </asp:Label>
                                    </div>
                                    <div class="col-sm-2" onclick="showHideDiv('menu1','Span8');return false;">
                                        <span id="Span8" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                                    </div>
                                </div>
                            </div>
                            <div id="menu1" style="display: block;" class="panel-body">

                                <%--  Added for Personal Details start --%>
                                <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                                    <div id="Div2" runat="server" class="panel-heading subheader" style="background-color: #EDF1cc !important" onclick="ShowReqDtl1('divPersonal','btnpersnl');return false;">
                                        <div class="row">
                                            <div class="col-sm-10" style="text-align: left">
                                                <span class="glyphicon glyphicon-menu-hamburger"></span>

                                                <asp:Label ID="lblpfPersonal" Text="PERSONAL DETAILS" runat="server" CssClass="control-label">
                                                </asp:Label>
                                            </div>
                                            <div class="col-sm-2">
                                                <span id="btnpersnl" class="glyphicon glyphicon-resize-full" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="divPersonal" style="display: block;" class="form-group panel-body">
                                        <div class="row">
                                            <div class="col-sm-3" style="text-align: left">
                                            </div>
                                            <div class="col-sm-9" style="padding-left: 0">
                                                <div class="col-sm-2" style="padding-left: 3%">
                                                    <asp:Label ID="Label7" Text="Prefix" runat="server" CssClass="control-label">
                                                    </asp:Label>
                                                </div>
                                                <div class="col-sm-10" style="padding-left: 0">
                                                    <div class="col-sm-4">
                                                        <asp:Label ID="Label8"
                                                            Text="First Name" runat="server" CssClass="control-label">
                                                        </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4" style="padding-left: 3%">
                                                        <asp:Label ID="Label10" Text="Middle Name" runat="server" CssClass="control-label">
                                                        </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4" style="padding-left: 4%">
                                                        <asp:Label ID="Label11" Text="Last Name" runat="server" CssClass="control-label">
                                                        </asp:Label>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblName" Text="Name" runat="server" CssClass="control-label">
                                                </asp:Label>
                                                <span style="color: red">*</span>
                                            </div>
                                            <div class="col-sm-9" style="padding: 0">
                                                <div class="col-sm-2">
                                                    <asp:UpdatePanel ID="upcboTitle" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="cboTitle" runat="server" Enabled="false" CssClass="form-control" DataTextField="ParamDesc"
                                                                DataValueField="ParamValue" AppendDataBoundItems="True" TabIndex="6">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-sm-10" style="padding: 0">
                                                    <div class="col-sm-4" style="padding-left: 0">
                                                        <asp:TextBox ID="txtGivenName" runat="server" Enabled="false" CssClass="form-control" onkeypress="funIsAlphaNumericWithSpace();" onchange="javascript:this.value=this.value.toUpperCase();"
                                                            MaxLength="50" TabIndex="7" placeholder="First Name">
                                                        </asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-4" style="padding-left: 0">
                                                        <asp:TextBox ID="txtMiddleName" runat="server" Enabled="false" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="funIsAlphaNumericWithSpace();"
                                                            MaxLength="50" TabIndex="8" onblur="CheckSpaces();return false;" placeholder="Middle Name">
                                                        </asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-4" style="padding-left: 0">
                                                        <asp:TextBox ID="txtLastName" runat="server" Enabled="false" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="funIsAlphaNumericWithSpace();"
                                                            MaxLength="50" TabIndex="9" onblur="CheckSpaces();return false;" placeholder="Last Name">
                                                        </asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblMaidenName" Text="Maiden Name" CssClass="control-label" runat="server">
                                                </asp:Label>
                                            </div>
                                            <div class="col-sm-9" style="padding: 0">
                                                <div class="col-sm-2">
                                                    <asp:UpdatePanel ID="ipcboTitle1" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="cboTitle1" Enabled="false" runat="server" CssClass="form-control" DataTextField="ParamDesc"
                                                                DataValueField="ParamValue" AppendDataBoundItems="True" TabIndex="10">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-sm-10" style="padding: 0">
                                                    <div class="col-sm-4" style="padding-left: 0">
                                                        <asp:TextBox ID="txtGivenName1" Enabled="false" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="funIsAlphaNumericWithSpace();"
                                                            MaxLength="50" TabIndex="11" onblur="CheckSpaces();return false;" placeholder="First Name">
                                                        </asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-4" style="padding-left: 0">
                                                        <asp:TextBox ID="txtMiddleName1" Enabled="false" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="funIsAlphaNumericWithSpace();"
                                                            MaxLength="50" TabIndex="12" onblur="CheckSpaces();return false;" placeholder="Middle Name">
                                                        </asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-4" style="padding-left: 0">
                                                        <asp:TextBox ID="txtLastName1" Enabled="false" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="funIsAlphaNumericWithSpace();"
                                                            MaxLength="50" TabIndex="13" onblur="CheckSpaces();return false;" placeholder="Last Name">
                                                        </asp:TextBox>
                                                    </div>
                                                    <asp:HiddenField ID="hdnGenderTitle1" runat="server"></asp:HiddenField>
                                                    <asp:HiddenField ID="hdnGenderTitle2" runat="server"></asp:HiddenField>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="col-sm-6" style="padding: 0">
                                                    <asp:Label ID="lblFatherName" Text="Father/Spouse Name" CssClass="control-label"
                                                        runat="server"></asp:Label>
                                                    <span style="color: red">*</span>
                                                </div>
                                                <div class="col-sm-6" style="padding: 0">
                                                    <asp:UpdatePanel ID="UpdFSFlag" runat="server">
                                                        <ContentTemplate>
                                                            <asp:RadioButtonList ID="rbtFS" Enabled="false" runat="server" CssClass="radiobtn" RepeatDirection="Horizontal"
                                                                Visible="true" TabIndex="14" AutoPostBack="true">
                                                                <asp:ListItem Value="F">Father</asp:ListItem>
                                                                <asp:ListItem Value="S">Spouse</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="col-sm-9" style="padding: 0">
                                                <div class="col-sm-2">
                                                    <asp:UpdatePanel ID="upcboTitle2" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="cboTitle2" Enabled="false" runat="server" CssClass="form-control" DataTextField="ParamDesc"
                                                                DataValueField="ParamValue" AppendDataBoundItems="True" TabIndex="15">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-sm-10" style="padding: 0">
                                                    <div class="col-sm-4" style="padding-left: 0">
                                                        <asp:TextBox ID="txtGivenName2" Enabled="false" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="funIsAlphaNumericWithSpace();"
                                                            MaxLength="50" TabIndex="16" onblur="CheckSpaces();return false;" placeholder="First Name">
                                                        </asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-4" style="padding-left: 0">
                                                        <asp:TextBox ID="txtMiddleName2" Enabled="false" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="funIsAlphaNumericWithSpace();"
                                                            MaxLength="50" TabIndex="17" onblur="CheckSpaces();return false;" placeholder="Middle Name">
                                                        </asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-4" style="padding-left: 0">
                                                        <asp:TextBox ID="txtLastName2" Enabled="false" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="funIsAlphaNumericWithSpace();"
                                                            MaxLength="50" TabIndex="18" onblur="CheckSpaces();return false;" placeholder="Last Name">
                                                        </asp:TextBox>
                                                    </div>

                                                    <%--  <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConn %>">
                                                         </asp:SqlDataSource>--%>
                                                    <asp:HiddenField ID="HiddenField1" runat="server"></asp:HiddenField>
                                                    <asp:HiddenField ID="HiddenField2" runat="server"></asp:HiddenField>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblMotherName" Text="Mother Name" CssClass="control-label" runat="server">
                                                </asp:Label>
                                                <span style="color: red">*</span>
                                            </div>
                                            <div class="col-sm-9" style="padding: 0">
                                                <div class="col-sm-2">
                                                    <asp:DropDownList ID="cboTitle3" Enabled="false" runat="server" CssClass="form-control" DataTextField="ParamDesc"
                                                        DataValueField="ParamValue" AppendDataBoundItems="True" TabIndex="19">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-10" style="padding: 0">
                                                    <div class="col-sm-4" style="padding-left: 0">
                                                        <asp:TextBox ID="txtGivenName3" Enabled="false" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="funIsAlphaNumericWithSpace();"
                                                            MaxLength="50" TabIndex="20" onblur="CheckSpaces();return false;" placeholder="First Name">
                                                        </asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-4" style="padding-left: 0">
                                                        <asp:TextBox ID="txtMiddleName3" Enabled="false" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="funIsAlphaNumericWithSpace();"
                                                            MaxLength="50" TabIndex="21" onblur="CheckSpaces();return false;" placeholder="Middle Name">
                                                        </asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-4" style="padding-left: 0">
                                                        <asp:TextBox ID="txtLastName3" Enabled="false" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="funIsAlphaNumericWithSpace();"
                                                            MaxLength="50" TabIndex="22" onblur="CheckSpaces();return false;" placeholder="Last Name">
                                                        </asp:TextBox>
                                                    </div>
                                                    <asp:HiddenField ID="HiddenField3" runat="server"></asp:HiddenField>
                                                    <asp:HiddenField ID="HiddenField4" runat="server"></asp:HiddenField>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lbldob" Text="DOB (dd/mm/yyyy) " runat="server" CssClass="control-label">
                                                </asp:Label>
                                                <span style="color: red">*</span>

                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox CssClass="form-control" Enabled="false"
                                                    onmousedown="$('#EmptyPagePlaceholder_txtDOB').datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy', yearRange: '1945:'+(new Date).getFullYear()});"
                                                    runat="server" ID="txtDOB" MaxLength="15"
                                                    TabIndex="23" />
                                                <%-- onchange="setDateFormat('txtDob')"--%>
                                            </div>
                                            <div class="col-sm-3" style="text-align: left">
                                                <%--Added by shreela on 6/03/14 to remove space--%>
                                                <asp:Label ID="lblGender" Text="Gender" runat="server" CssClass="control-label"></asp:Label>
                                                <span style="color: red">*</span>

                                                <%-- <span style="color: #ff0000">*</span>--%>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:UpdatePanel ID="upcboGender" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="cboGender" Enabled="false" runat="server" CssClass="form-control" TabIndex="24">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <%--<div class="row">
                                           
                                        </div>--%>
                                        <div class="row">
                                            <div class="col-sm-3" style="text-align: left">
                                                <%--Added by shreela on 6/03/14 to remove space--%>
                                                <asp:Label ID="lblMarStatus" Text="Marital Status" runat="server" CssClass="control-label">
                                                </asp:Label>
                                                <span style="color: red">*</span>

                                                <%-- <span style="color: #ff0000">*</span>--%>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:UpdatePanel ID="upMaritalStatus" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlMaritalStatus" Enabled="false" runat="server" CssClass="form-control" TabIndex="27">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblCitizenship" Text="Nationality" runat="server" CssClass="control-label">
                                                </asp:Label>
                                                <span style="color: red">*</span>

                                            </div>
                                            <div class="col-sm-3">
                                                <asp:UpdatePanel ID="upCitizenship" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlCitizenship" Enabled="false" runat="server" CssClass="form-control" TabIndex="28" AutoPostBack="true">
                                                            <%--OnSelectedIndexChanged="ddlCitizenship_SelectedIndexChanged"--%>
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row">

                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:UpdatePanel ID="uplblIsoCountryCodeOthr" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lblIsoCountryCodeOthr" Text="ISO 3166 Country Code" Visible="false"
                                                            runat="server" CssClass="control-label"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-sm-3">

                                                <asp:UpdatePanel ID="upIsoCountryCodeOthr" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlIsoCountryCodeOthr" Enabled="false" runat="server" CssClass="form-control" AutoPostBack="true" TabIndex="30" Visible="false"></asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblResStatus" Text="Residential Status" runat="server" CssClass="control-label">
                                                </asp:Label>

                                            </div>
                                            <div class="col-sm-3">
                                                <asp:UpdatePanel ID="upResStatus" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlResStatus" Enabled="false" runat="server" CssClass="form-control" TabIndex="29">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <asp:UpdatePanel ID="upOccuSubType" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-sm-3" style="text-align: left">
                                                        <asp:Label ID="lblOccupation" Text="Occupation Type" runat="server" CssClass="control-label">
                                                        </asp:Label>
                                                        <span style="color: red">*</span>

                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="ddlOccupation" Enabled="false" AutoPostBack="true" runat="server" CssClass="form-control"
                                                            TabIndex="25">
                                                            <%--OnSelectedIndexChanged="ddlOccupation_SelectedIndexChanged"--%>
                                                        </asp:DropDownList><%--  --%>
                                                    </div>
                                                    <%-- <div class="col-sm-3" style="text-align: left" id="divOccuSubType" runat="server">
                                <asp:label id="lblOccuSubType" text="Occupation Sub Type" runat="server" cssclass="control-label">
                                </asp:label>
                                
                            </div>
                            <div class="col-sm-3">
                             
                                                        <asp:DropDownList ID="ddlOccuSubType" runat="server" CssClass="form-control" TabIndex="26">
                                                        </asp:DropDownList>
                                                   
                            </div>--%>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                        <div class="row">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-sm-3" style="text-align: left" id="divOccuSubType" runat="server">
                                                        <asp:Label ID="lblOccuSubType" Text="Occupation Sub Type" runat="server" CssClass="control-label">
                                                        </asp:Label>
                                                        <span style="color: red">*</span>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="ddlOccuSubType" Enabled="false" runat="server" CssClass="form-control" TabIndex="26">
                                                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblIsoCountryCode2" Text="ISO 3166 Country Code of Jurisdiction of Residence"
                                                    runat="server" CssClass="control-label"></asp:Label>
                                                <span id="spnISO3166" runat="server" style="color: red">*</span>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlIsoCountryCode2" Enabled="false" runat="server" CssClass="form-control" AutoPostBack="true" TabIndex="32"></asp:DropDownList>

                                            </div>
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblTaxIden" Text="Tax Identification Number or equivalent (if issued by jurisdiction)"
                                                    runat="server" CssClass="control-label"></asp:Label>
                                                <span id="spnTINNo" runat="server" style="color: red">*</span>

                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox CssClass="form-control" Enabled="false"
                                                    runat="server" ID="txtIDResTax" MaxLength="15"
                                                    TabIndex="33" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblPlace" Text="Place / City of Birth" runat="server" CssClass="control-label">
                                                </asp:Label>
                                                <span id="spnPlaceOfBirth" runat="server" style="color: red">*</span>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox CssClass="form-control" Enabled="false"
                                                    runat="server" ID="txtDOBRes" MaxLength="15"
                                                    TabIndex="34" />
                                            </div>
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblIsoContry" Text="ISO 3166 Country Code of Birth" runat="server"
                                                    CssClass="control-label"></asp:Label>
                                                <span id="spnISOCntryCodeBrth" runat="server" style="color: red">*</span>
                                            </div>
                                            <div class="col-sm-3">

                                                <asp:DropDownList ID="ddlIsoCountry" Enabled="false" runat="server" CssClass="form-control" AutoPostBack="true" TabIndex="35"></asp:DropDownList>

                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <%--  Added for Personal Details end --%>
                                <%--  Added for Tick If Applicable start --%>
                                <%--<div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                                    <div id="Div1" runat="server" class="panel-heading subheader" style="background-color: #EDF1cc !important" onclick="ShowReqDtl1('div3','Span1');return false;">
                                        <div class="row">
                                            <div class="col-sm-10" style="text-align: left">
                                                <span class="glyphicon glyphicon-menu-hamburger"></span>
                                                <asp:Label ID="Label1" Text="TICK IF APPLICABLE" runat="server" CssClass="control-label">
                                                </asp:Label>&nbsp;&nbsp;
                                            </div>
                                            <div class="col-sm-2">
                                                <span id="Span1" class="glyphicon glyphicon-resize-full" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="div3" style="display: block;" class="panel-body">
                                        ADDITIONAL DETAILS REQUIRED (Mandatory only if section 2 is ticked)
                                        <span id="spnAddDtls" runat="server" visible="false" style="color:red">*</span>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-12" style="text-align: left">
                                                <asp:CheckBox ID="chkTick" Text="RESIDENCE FOR TAX PURPOSES IN JURISDICTION(S) OUTSIDE INDIA" AutoPostBack="true" OnCheckedChanged="chkTick_Checked"
                                                    CssClass="standardcheckbox" runat="server" TabIndex="31" />
                                            </div>
                                        </div>
                                      
                                    </div>
                                </div>--%>
                                <%--  Added for Tick If Applicable end --%>
                            </div>

                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>


                <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                    <div id="Div4" runat="server" class="panel-heading">
                        <div class="row">
                            <div class="col-sm-10" style="text-align: left">
                                <span class="glyphicon glyphicon-menu-hamburger"></span>
                                <%-- <asp:checkbox id="ChkUpdID" runat="server" cssclass="standardcheckbox" text="" autopostback="true"
                             tabindex="1" OnCheckedChanged="ChkUpdID_Checked" />--%>
                                <asp:Label ID="lblProofOfIdentity11" Text="PROOF OF IDENTITY (PoI)" runat="server"
                                    CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-2" onclick="showHideDiv('menu2','btnProofIdentity');return false;">
                                <span id="btnProofIdentity" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div>
                    <div id="menu2" style="display: block;" class="panel-body">


                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblProof" Text="(Certified copy of any one of the following Proof of Identity [PoI] needs to be submitted)"
                                    runat="server" CssClass="control-label"></asp:Label>
                                <span><font color="red">*</font></span>

                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddlProofIdentity" Enabled="false" runat="server" CssClass="form-control" AutoPostBack="true"
                                    TabIndex="36">
                                    <%--OnSelectedIndexChanged="ddlProofIdentity_SelectedIndexChanged" --%>
                                </asp:DropDownList>
                            </div>

                        </div>
                        <div id="divIdProof" runat="server" class="row">
                            <div id="divPassNo" runat="server" class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblPassportNo" Placeholder="Passport Number" runat="server" CssClass="control-label"></asp:Label>
                                <span><font color="red">*</font></span>
                            </div>
                            <div id="divPassNotxt" runat="server" class="col-sm-3">
                                <asp:TextBox CssClass="form-control" runat="server" Enabled="false"
                                    onChange="javascript:this.value=this.value.toUpperCase();" ID="txtPassNo" MaxLength="15" TabIndex="37" />
                            </div>
                            <div id="divPass" runat="server" class="col-sm-3" style="text-align: left">
                                <asp:Label ID="llPassExpDate" runat="server" CssClass="control-label"></asp:Label>
                                <span><font color="red">*</font></span>
                            </div>
                            <div id="divPassDate" runat="server" class="col-sm-3">


                                <asp:TextBox CssClass="form-control" Enabled="false" onmousedown="$('#EmptyPagePlaceholder_txtPassExpDate').datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' });"
                                    runat="server" ID="txtPassExpDate" MaxLength="15" TabIndex="38" />


                                <asp:TextBox CssClass="form-control" Enabled="false" runat="server"
                                    ID="txtPassOthr" MaxLength="15" TabIndex="39" />
                            </div>
                        </div>
                        <%--   </div>
                                </div>--%>
                    </div>
                </div>

                <%-- Added for Proof of Identity end--%>
                <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                    <div id="Div20" runat="server" class="panel-heading">
                        <div class="row">
                            <div class="col-sm-10" style="text-align: left">
                                <span class="glyphicon glyphicon-menu-hamburger"></span>
                                <asp:Label ID="lblpfofAddr1" Text="PROOF OF ADDRESS" runat="server" CssClass="control-label">
                                </asp:Label>
                            </div>
                            <div class="col-sm-2" onclick="showHideDiv('menu3','btnpersnl');return false;">
                                <span id="Span9" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div>
                    <div id="menu3" style="display: block;" class="panel-body">
                        <asp:UpdatePanel ID="upMenu3" runat="server">
                            <ContentTemplate>
                                <%--  Added for Proof of Address start--%>
                                <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                                    <div id="Div6" runat="server" class="panel-heading subheader" style="background-color: #EDF1cc !important">
                                        <div class="row">
                                            <div class="col-sm-10" style="text-align: left">
                                                <span class="glyphicon glyphicon-menu-hamburger"></span>
                                                <%--<asp:checkbox id="ChkUpdAddr" runat="server" cssclass="standardcheckbox" text="" OnCheckedChanged="ChkUpdAddr_Checked" autopostback="true"
                             tabindex="1" />--%>
                                                <asp:Label ID="Label2" Text="PROOF OF ADDRESS (PoA)" runat="server" CssClass="control-label"></asp:Label>
                                                <span style="color: red">*</span>
                                            </div>
                                            <div class="col-sm-2" onclick="ShowReqDtl1('div7','Span2');return false;">
                                                <span id="Span2" class="glyphicon glyphicon-resize-full" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="div7" style="display: block;" class="panel-body">
                                        <div class="row">
                                            <div class="col-sm-12" style="text-align: left">
                                                <asp:CheckBox ID="chkPerAddress" Enabled="false" Text="CURRENT / PERMANENT / OVERSEAS ADDRESS DETAILS" AutoPostBack="true"
                                                    runat="server" CssClass="control-label" TabIndex="40" />
                                                <%--OnCheckedChanged="chkPerAddress_Checked"--%>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblAddressType" Text="Address Type" runat="server" CssClass="control-label"></asp:Label>
                                                <span style="color: red">*</span>

                                            </div>
                                            <div class="col-sm-3">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlAddressType" Enabled="false" runat="server" CssClass="form-control" AutoPostBack="true"
                                                            TabIndex="41">
                                                            <%--OnSelectedIndexChanged="ddlAddressType_SelectedIndexChanged"--%>
                                                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblProofOfAddress" Text="Proof of Address" runat="server" CssClass="control-label"></asp:Label>
                                                <span style="color: red">*</span>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlProofOfAddress" Enabled="false" runat="server" CssClass="form-control" AutoPostBack="true"
                                                    TabIndex="42">
                                                    <%--OnSelectedIndexChanged="ddlProofOfAddress_SelectedIndexChanged"--%>
                                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div id="divAddProof" runat="server" class="row">
                                            <div id="divPassNoAdd" runat="server" class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblPassportNoAdd" Placeholder="Passport Number" runat="server" CssClass="control-label"></asp:Label>

                                            </div>
                                            <div id="divPassNotxtAdd" runat="server" class="col-sm-3">
                                                <asp:TextBox CssClass="form-control" Enabled="false" runat="server"
                                                    onChange="javascript:this.value=this.value.toUpperCase();" ID="txtPassNoAdd" MaxLength="15" TabIndex="43" />
                                                <%--<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                                    FilterType="Numbers,UppercaseLetters,LowercaseLetters" TargetControlID="txtPassNoAdd">
                                                </ajaxToolkit:FilteredTextBoxExtender>--%>
                                            </div>
                                            <div id="divPassAdd" runat="server" class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="llPassExpDateAdd" runat="server" CssClass="control-label"></asp:Label>

                                            </div>
                                            <div id="divPassDateAdd" runat="server" class="col-sm-3">


                                                <asp:TextBox CssClass="form-control" Enabled="false" onmousedown="$('#EmptyPagePlaceholder_txtPassExpDateAdd').datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' });"
                                                    runat="server" ID="txtPassExpDateAdd" MaxLength="15" TabIndex="44" />


                                                <asp:TextBox CssClass="form-control" Enabled="false" runat="server"
                                                    ID="txtPassOthrAdd" MaxLength="15" TabIndex="45" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblAddressLine1" Text="Address Line1" runat="server" CssClass="control-label"></asp:Label>
                                                <span style="color: red">*</span>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox CssClass="form-control" Enabled="false" onchange="setDateFormat('txtDrivLicNum')" runat="server"
                                                    ID="txtAddressLine1" MaxLength="300" TabIndex="46" />
                                            </div>
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblAddressLine2" Text="Address Line2" runat="server" CssClass="control-label"></asp:Label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox CssClass="form-control" Enabled="false" onchange="setDateFormat('txtAddressLine2')"
                                                    runat="server" ID="txtAddressLine2" MaxLength="300" TabIndex="47" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblAddressLine3" Text="Address Line3" runat="server" CssClass="control-label"></asp:Label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox CssClass="form-control" Enabled="false" onchange="setDateFormat('txtDrivLicNum')" runat="server"
                                                    ID="txtAddressLine3" MaxLength="300" TabIndex="48" />
                                            </div>
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblCity" Text="City / Town / Village" runat="server" CssClass="control-label"></asp:Label>
                                                <span style="color: red">*</span>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtCity" Enabled="false" runat="server" CssClass="form-control" TabIndex="49">
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblDistrict" Text="District" runat="server" CssClass="control-label"></asp:Label>
                                                <span style="color: red">*</span>
                                            </div>
                                            <div class="col-sm-3">
                                                <%--   <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" Enabled="false"
                                                    TabIndex="50">
                                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                </asp:DropDownList>--%>
                                                <asp:TextBox runat="server" ID="txtDistrictname" CssClass="form-control" Enabled="false"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblPinCode" Text="Pin / Post Code" runat="server" CssClass="control-label"></asp:Label>
                                                <span style="color: red">*</span>
                                            </div>
                                            <div class="col-sm-3">
                                                <%--  <asp:DropDownList ID="ddlPinCode" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPinCode_SelectedIndexChanged"
                                                    AutoPostBack="True" TabIndex="51">
                                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                </asp:DropDownList>--%>
                                                <asp:TextBox ID="txtPinCode" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblState" Text="State / U.T Code" runat="server" CssClass="control-label"></asp:Label>
                                                <span style="color: red">*</span>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlState" Enabled="false" runat="server" CssClass="form-control" TabIndex="52">
                                                        <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:HiddenField runat="server" ID="hdnddlPinCode" />
                                                    <div class="input-group-btn">
                                                        <asp:LinkButton runat="server" Enabled="false" ID="btnShow" CssClass="btn btn-primary btn-lg-kmi" title="Search" data-toggle="tooltip"><%--OnClick="GetModelData"--%>
                                                        <span class="glyphicon glyphicon-search BtnGlyphicon"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblIsoCountryCode" Text="ISO 3166 Country Code" runat="server" CssClass="control-label"></asp:Label>
                                                <span style="color: red">*</span>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlCountryCode" Enabled="false" runat="server" CssClass="form-control" AutoPostBack="true" TabIndex="53"></asp:DropDownList>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <%-- Added for Proof of Address end--%>

                                <%--  Added for Contact Details start--%>
                                <%--<div class="panel panel-success" style="margin-left: 5px; margin-right: 5px">
                    <div id="Div1" runat="server" class="panel-heading subheader" style="background-color: #EDF1cc !important"
                        onclick="ShowReqDtl1('div9','Span3');return false;">
                        <div class="row">
                            <div class="col-sm-10" style="text-align: left">
                                <span class="glyphicon glyphicon-menu-hamburger"></span>CONTACT DETAILS
                            </div>
                            <div class="col-sm-2" onclick="ShowReqDtl1('div9','Span3');return false;">
                                <span id="Span3" class="glyphicon glyphicon-resize-full" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div>
                    <div id="div9" style="display: block;" class="panel-body">
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblTelOff1" runat="server" Text="" CssClass="control-label"></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <div class="input-group">
                                    <span class="input-group-addon input-group-addon-tel" style="width: 20% !important; border-top-left-radius: 7% !important; padding: 0px !important; border: 0px !important;">
                                        <asp:TextBox ID="txtTelOff" runat="server" CssClass="form-control" MaxLength="4"></asp:TextBox>
                                    </span>
                                    <asp:TextBox ID="txtTelOff2" runat="server" CssClass="form-control" onkeypress="fncInputNumericValuesOnly();"
                                        MaxLength="10"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblTelRes" runat="server" CssClass="control-label" Text=""></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <div class="input-group">
                                    <span class="input-group-addon input-group-addon-tel" style="width: 20% !important; border-top-left-radius: 7% !important; padding: 0px !important; border: 0px !important;">
                                        <asp:TextBox ID="txtTelRes" runat="server" CssClass="form-control" TabIndex="77"
                                            onkeypress="fncInputNumericValuesOnly();" MaxLength="4" Style="border-top-left-radius: 4px; border-bottom-left-radius: 4px; top: 0px; left: 0px;"></asp:TextBox>
                                    </span>
                                    <asp:TextBox ID="txtTelRes2" runat="server" CssClass="form-control" MaxLength="10"
                                        onkeypress="fncInputNumericValuesOnly();" TabIndex="78"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblMobile" runat="server" Text="" CssClass="control-label"></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <div class="input-group">
                                    <span class="input-group-addon " style="width: 20% !important; border-top-left-radius: 7% !important; padding: 0px !important; border: 0px !important;">
                                        <asp:TextBox ID="TextBox21" runat="server" CssClass="form-control" onkeypress="fncInputNumericValuesOnly();"
                                            MaxLength="2" Style="border-top-left-radius: 4px; border-bottom-left-radius: 4px;"></asp:TextBox>
                                    </span>
                                    <asp:TextBox ID="TextBox22" runat="server" CssClass="form-control" onkeypress="fncInputNumericValuesOnly();"
                                        MaxLength="10"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblFax" runat="server" Text="" CssClass="control-label"></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <div class="input-group">
                                    <span class="input-group-addon input-group-addon-tel" style="width: 20% !important; border-top-left-radius: 7% !important; padding: 0px !important; border: 0px !important;">
                                        <asp:TextBox ID="txtFax1" runat="server" MaxLength="4" CssClass="form-control" TabIndex="81"></asp:TextBox>
                                    </span>
                                    <asp:TextBox ID="txtFax2" runat="server" CssClass="form-control" MaxLength="10" onkeypress="fncInputNumericValuesOnly();"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                Email ID
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtemail" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>--%>

                                <div class="panel panel-success" style="margin-left: 5px; margin-right: 5px">
                                    <div id="Div5" runat="server" class="panel-heading subheader" style="background-color: #EDF1cc !important"
                                        onclick="ShowReqDtl1('div9','Span3');return false;">
                                        <div class="row">
                                            <div class="col-sm-10" style="text-align: left">
                                                <span class="glyphicon glyphicon-menu-hamburger"></span>CONTACT DETAILS
                                            </div>
                                            <div class="col-sm-2" onclick="ShowReqDtl1('div9','Span3');return false;">
                                                <span id="Span3" class="glyphicon glyphicon-resize-full" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="div9" style="display: block;" class="panel-body">
                                        <div class="row">
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblTelOff1" runat="server" Text="Tel. (Off)" CssClass="control-label"></asp:Label>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="input-group">
                                                    <span class="input-group-addon input-group-addon-tel" style="width: 20% !important; border-top-left-radius: 7% !important; padding: 0px !important; border: 0px !important;">
                                                        <asp:TextBox ID="txtTelOff" Enabled="false" runat="server" CssClass="form-control" MaxLength="4"></asp:TextBox>
                                                    </span>
                                                    <asp:TextBox ID="txtTelOff2" runat="server" Enabled="false" CssClass="form-control" onkeypress="fncInputNumericValuesOnly();"
                                                        MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblTelRes" runat="server" CssClass="control-label" Text="Tel. (Res)"></asp:Label>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="input-group">
                                                    <span class="input-group-addon input-group-addon-tel" style="width: 20% !important; border-top-left-radius: 7% !important; padding: 0px !important; border: 0px !important;">
                                                        <asp:TextBox ID="txtTelRes" Enabled="false" runat="server" CssClass="form-control" TabIndex="77"
                                                            onkeypress="fncInputNumericValuesOnly();" MaxLength="4" Style="border-top-left-radius: 4px; border-bottom-left-radius: 4px; top: 0px; left: 0px;"></asp:TextBox>
                                                    </span>
                                                    <asp:TextBox ID="txtTelRes2" runat="server" Enabled="false" CssClass="form-control" MaxLength="10"
                                                        onkeypress="fncInputNumericValuesOnly();" TabIndex="78"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblMobile" runat="server" Text="Mobile" CssClass="control-label"></asp:Label>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="input-group">
                                                    <span class="input-group-addon " style="width: 20% !important; border-top-left-radius: 7% !important; padding: 0px !important; border: 0px !important;">
                                                        <asp:TextBox ID="TextBox21" Enabled="false" runat="server" CssClass="form-control" onkeypress="fncInputNumericValuesOnly();"
                                                            MaxLength="2" Style="border-top-left-radius: 4px; border-bottom-left-radius: 4px;"></asp:TextBox>
                                                    </span>
                                                    <asp:TextBox ID="TextBox22" Enabled="false" runat="server" CssClass="form-control" onkeypress="fncInputNumericValuesOnly();"
                                                        MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblFax" runat="server" Text="FAX" CssClass="control-label"></asp:Label>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="input-group">
                                                    <span class="input-group-addon input-group-addon-tel" style="width: 20% !important; border-top-left-radius: 7% !important; padding: 0px !important; border: 0px !important;">
                                                        <asp:TextBox ID="txtFax1" Enabled="false" runat="server" MaxLength="4" CssClass="form-control" TabIndex="81"></asp:TextBox>
                                                    </span>
                                                    <asp:TextBox ID="txtFax2" Enabled="false" runat="server" CssClass="form-control" MaxLength="10" onkeypress="fncInputNumericValuesOnly();"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3" style="text-align: left">
                                                Email ID
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtemail" Enabled="false" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--  Added for Contact Details end--%>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                    <div id="Div23" runat="server" class="panel-heading">
                        <div class="row">
                            <div class="col-sm-10" style="text-align: left">
                                <span class="glyphicon glyphicon-menu-hamburger"></span>
                                <%--  <asp:checkbox id="ChkUpdKYCVrfy" runat="server" cssclass="standardcheckbox" text="" OnCheckedChanged="ChkUpdControlPrsn_Checked" autopostback="true"
                             tabindex="1" />--%>
                                <asp:Label ID="lblattstn" Text="ATTESTATION" runat="server" CssClass="control-label">
                                </asp:Label>
                            </div>
                            <div class="col-sm-2" onclick="showHideDiv('menu6','Span12');return false;">
                                <span id="Span12" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div>

                    <div id="menu6" style="display: block;" class="panel-body">
                        <%--  Added for Applicant Declaration start--%>
                        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                            <div id="Div14" runat="server" class="panel-heading subheader" style="background-color: #EDF1cc !important" onclick="ShowReqDtl1('div15','Span6');return false;">
                                <div class="row">
                                    <div class="col-sm-10" style="text-align: left">
                                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                                        <asp:Label ID="Label5" Text="APPLICANT DECLARATION" runat="server" CssClass="control-label">
                                        </asp:Label>
                                    </div>
                                    <div class="col-sm-2">
                                        <span id="Span6" class="glyphicon glyphicon-resize-full" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                                    </div>
                                </div>
                            </div>
                            <%--<div id="div15" style="display: block;" class="panel-body">
                                <div class="row">
                                    <div class="col-sm-12" style="text-align: left; display: flex;">
                                        <asp:CheckBox ID="chkAppDeclare1" Text="I/We hereby declare that the details furnished above are true and correct to the best of my/our knowledge and belief and I/we undertake to inform you of any changes " CssClass="control-label"
                                            AutoPostBack="true" runat="server" onchange="setDateFormat('txtRemarks')" TabIndex="97" />

                                    </div>
                                    <div class="col-sm-12" style="text-align: left; display: flex; font-weight: bold;">
                                        <asp:Label CssClass="control-label" Text="therein, immediately. In case any of the above information is found to be false or untrue or misleading or misrepresenting. I/we am/are aware that I/we may be held liable for it."
                                            onchange="setDateFormat('txtRemarks')" runat="server" ID="lblAppDeclare1" maxlength="15" />
                                    </div>
                                    <br />
                                    <br />
                                    <br />
                                    <div class="col-sm-12" style="text-align: left; display: flex;">
                                        <asp:CheckBox ID="chkAppDeclare2" Text="I hereby consent to receiving information from Central KYC Registry through SMS/Email on the above registered number/email address" CssClass="control-label"
                                            AutoPostBack="true" runat="server" onchange="setDateFormat('txtRemarks')" TabIndex="98" />
                                    </div>
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblDate" Text="Date " runat="server" CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" onmousedown="$('#EmptyPagePlaceholder_txtDate').datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' });"
                                            runat="server" ID="txtDate" MaxLength="15"
                                            TabIndex="99" />
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblPlace1" Text="Place " runat="server" CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtPlace')" runat="server"
                                            ID="txtPlace" MaxLength="15" TabIndex="100" />
                                    </div>
                                </div>
                            </div>--%>
                            <div id="div15" style="display: block;" class="panel-body">
                                <div class="row">
                                    <div class="col-sm-12" style="text-align: left; display: flex;">
                                        <asp:CheckBox Style="margin-bottom: -0.5%;" ID="chkAppDeclare1" Enabled="false" Text="I/We hereby declare that the details furnished above are true and correct to the best of my/our knowledge and belief and I/we undertake to inform you of "
                                            CssClass="control-label" AutoPostBack="true" runat="server" onchange="setDateFormat('txtRemarks')"
                                            TabIndex="97" />
                                    </div>
                                    <div class="col-sm-12" style="text-align: left; display: flex; font-weight: bold; padding-left: 2.5%;">
                                        <asp:Label CssClass="control-label" Text="any changes therein, immediately.In case any of the above information is found to be false or untrue or misleading or misrepresenting, I/we am/are aware that I/we may be held liable for it."
                                            runat="server" ID="lblAppDeclare1" maxlength="15" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12" style="text-align: left; display: flex;">
                                        <asp:CheckBox ID="chkAppDeclare2" Enabled="false" Text="My/Our personal KYC details may be shared with Central KYC Registry."
                                            CssClass="control-label" AutoPostBack="true" runat="server" onchange="setDateFormat('txtRemarks')"
                                            TabIndex="98" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12" style="text-align: left; display: flex;">
                                        <asp:CheckBox ID="chkAppDeclare3" Enabled="false" Text="I/We hereby consent to receiving information from Central KYC Registry through SMS/Email on the above registered number/email address."
                                            CssClass="control-label" AutoPostBack="true" runat="server" onchange="setDateFormat('txtRemarks')"
                                            TabIndex="98" />
                                    </div>
                                </div>
                                <br />
                                <br />
                                <%-- <br />
                            <br />--%>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblDate" Text="Date" runat="server" CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left; top: 0px; left: 0px;">
                                        <asp:TextBox ID="txtDate" Enabled="false" runat="server" CssClass="form-control" MaxLength="10"
                                            TextMode="Date"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblPlace1" Text="Place" runat="server" CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" Enabled="false" onchange="setDateFormat('txtPlace')" runat="server"
                                            ID="txtPlace" MaxLength="15" TabIndex="100" />
                                    </div>
                                </div>
                            </div>

                        </div>
                        <%--  Added for Applicant Declaration end--%>
                        <%--  Added for Attestation/For Office Use Only start--%>
                        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                            <div id="Div16" runat="server" class="panel-heading subheader" style="background-color: #EDF1cc !important" onclick="ShowReqDtl1('div17','Span7');return false;">
                                <div class="row">
                                    <div class="col-sm-10" style="text-align: left">
                                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                                        <asp:Label ID="Label9" Text="ATTESTATION / FOR OFFICE USE ONLY" runat="server" CssClass="control-label">
                                        </asp:Label>
                                    </div>
                                    <div class="col-sm-2">
                                        <span id="Span7" class="glyphicon glyphicon-resize-full" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                                    </div>
                                </div>
                            </div>
                            <%--<div id="div17" style="display: block;" class="panel-body">
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblDocRec" Text="Documents Received" runat="server" Font-Bold="true"
                                            CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:CheckBox ID="chkCertifyCopy" Text="Self-Certified" CssClass="standardcheckbox"
                                            Enabled="false" AutoPostBack="true" runat="server" TabIndex="101" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12" style="text-align: center">
                                        <asp:Label ID="lblKYCVerify" Style='text-align: center' CssClass="control-label"
                                            Font-Bold="true" Text="KYC VERIFICATION CARRIED OUT BY" runat="server" />

                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblDate3" Text="Date " runat="server" CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" onmousedown="$('#EmptyPagePlaceholder_txtDateKYCver').datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' });"
                                            onchange="setDateFormat('txtDateKYCver')" runat="server" ID="txtDateKYCver" MaxLength="15"
                                            TabIndex="102" />
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblEmpName" Text="Employee Name" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtEmpName" MaxLength="15" Enabled="true"
                                            TabIndex="103" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblEmpCode" Text="Employee Code" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <span style="color: red">*</span>

                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtEmpCode" MaxLength="15" Enabled="true"
                                            TabIndex="104" />
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblEmpDesignation" Text="Employee Designation" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtEmpDesignation" MaxLength="15" Enabled="true"
                                            TabIndex="105" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblEmpBranch" Text="Employee Branch" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtEmpBranch" MaxLength="15" Enabled="true"
                                            TabIndex="106" />
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-sm-12" style="text-align: center">
                                        <asp:Label ID="lblInsDtls" Style='text-align: center' CssClass="control-label" Font-Bold="true"
                                            Text="Institution Details" runat="server" />
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblInsName" Text="Name" runat="server" CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control"
                                            runat="server" ID="txtInsName" MaxLength="15" Enabled="true"
                                            TabIndex="107" />
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblInsCode" Text="Code" runat="server" CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtInsCode" MaxLength="15" Enabled="true"
                                            TabIndex="108" />
                                    </div>
                                </div>
                            </div>--%>

                            <div id="div17" style="display: block;" class="panel-body">
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblDocRec" Text="Documents Received" runat="server" Font-Bold="true"
                                            CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:CheckBox ID="chkSelfCerti" Enabled="false" Text="" CssClass="standardcheckbox"
                                            AutoPostBack="true" runat="server" TabIndex="101" />
                                        <span>Self-Certified</span>
                                        <asp:CheckBox ID="chkTrueCopies" Enabled="false" Text="" CssClass="standardcheckbox"
                                            AutoPostBack="true" runat="server" TabIndex="101" />
                                        <span>True Copies</span>
                                        <asp:CheckBox ID="chkNotary" Enabled="false" Text="" CssClass="standardcheckbox"
                                            AutoPostBack="true" runat="server" TabIndex="101" />
                                        <span>Notary</span>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblRiskCategory" Text="Risk Category" runat="server" Font-Bold="true"
                                            CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:CheckBox ID="chkHigh" Enabled="false" Text="" CssClass="standardcheckbox"
                                            AutoPostBack="true" runat="server" TabIndex="101" />
                                        <span>High</span>
                                        <asp:CheckBox ID="chkMedium" Enabled="false" Text="" CssClass="standardcheckbox"
                                            AutoPostBack="true" runat="server" TabIndex="101" />
                                        <span>Medium</span>
                                        <asp:CheckBox ID="chkLow" Enabled="false" Text="" CssClass="standardcheckbox"
                                            AutoPostBack="true" runat="server" TabIndex="101" />
                                        <span>Low</span>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-sm-12" style="text-align: center">
                                        <asp:Label ID="lblKYCVerify" Style='text-align: center' CssClass="control-label"
                                            Font-Bold="true" Text="KYC VERIFICATION CARRIED OUT BY" runat="server" />
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblIdVerif" Text="Identity Verification" runat="server" CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left; top: 0px; left: 0px;">
                                        <asp:CheckBox ID="chkDone" Enabled="false" Text="" CssClass="standardcheckbox"
                                            AutoPostBack="true" runat="server" TabIndex="101" />
                                        <span>Done</span>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblDate3" Text="Date" runat="server" CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left; top: 0px; left: 0px;">
                                        <asp:TextBox ID="txtDate3" Enabled="false" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblEmpName" Text="Employee Name" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" Enabled="false" runat="server" ID="txtEmpName" MaxLength="15"
                                            TabIndex="103" />
                                        <br />
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblEmpCode" Text="Employee Code" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" Enabled="false" runat="server" ID="txtEmpCode" MaxLength="15"
                                            TabIndex="104" />
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblEmpDesignation" Text="Employee Designation" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtEmpDesignation" MaxLength="15"
                                            Enabled="false" TabIndex="105" />
                                        <%--<br/>--%>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblEmpBranch" Text="Employee Branch" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtEmpBranch" MaxLength="15"
                                            Enabled="false" TabIndex="106" />
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-sm-12" style="text-align: center">
                                        <asp:Label ID="lblInsDtls" Style='text-align: center' CssClass="control-label" Font-Bold="true"
                                            Text="INSTITUTION DETAILS" runat="server" />
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblInsName" Text="Name" runat="server" CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtDate3')" runat="server"
                                            ID="txtInsName" MaxLength="15" Enabled="false" TabIndex="107" />
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblInsCode" Text="Code" runat="server" CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtInsCode" MaxLength="15"
                                            Enabled="false" TabIndex="108" />
                                    </div>
                                </div>
                            </div>

                        </div>
                        <%--  Added for Attestation/For Office Use Only  end--%>
                    </div>
                </div>
                <%--  </div>--%>
                <%-- <div  id="Document" class="tab-content">
               </div>--%>
            </ContentTemplate>
        </asp:UpdatePanel>

      <%--  <div class="row" style="margin-top: 12px;">
            <div class="col-sm-12" align="center">
                <asp:LinkButton ID="btnAdd" CssClass="btn-animated bg-green" runat="server" TabIndex="109">
                                               <span class="glyphicon glyphicon-floppy-disk BtnGlyphicon"></span> Add</asp:LinkButton>
                <asp:LinkButton ID="btnPartialAdd" CssClass="btn-animated bg-green"
                    runat="server" TabIndex="110">
                             <span class="glyphicon glyphicon-floppy-disk BtnGlyphicon"> </span> Partial Add </asp:LinkButton>

                <div id="divloader" runat="server" style="display: none;">
                    <img id="Img1" alt="" src="Common/Images/spinner.gif" runat="server" />
                    Loading...
                </div>
            </div>
        </div>--%>
    </div>


    <input id="hdnUpdate" type="hidden" runat="server" />
    <asp:HiddenField ID="hdnUserId" runat="server" />

    <asp:Label runat="server" ID="lbl1" Style="display: none" />
    <div class="modal fade" id="myModal" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header" style="text-align: center; background-color: #dff0d8;">
                    <asp:Label ID="Label16" Text="INFORMATION" runat="server" Font-Bold="true"></asp:Label>
                </div>
                <div class="modal-body" style="text-align: center">
                    <asp:Label ID="lbl" runat="server"></asp:Label>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <button type="button" class="btn btn-primary" data-dismiss="modal">
                        <span class="glyphicon glyphicon-ok  BtnGlyphicon"></span>OK
                    </button>
                </div>
            </div>
        </div>
    </div>


    <div id="modalConfirmYesNo" class="modal fade" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header" style="text-align: center; background-color: #dff0d8;">
                    <%--<button type="button" class="close" data-dismiss="modal" aria-label="Close">--%>
                    <%--  <span aria-hidden="true">&times;</span>
                </button>--%>
                    <asp:Label ID="Label4" Text="Confirmation" runat="server" Font-Bold="true"></asp:Label>
                </div>
                <div class="modal-body" style="text-align: center">
                    <asp:Label ID="lblMsgConfirmYesNo" runat="server"></asp:Label><%--OnClientClick="return YesConfirm();"--%>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="btnYesConfirmYesNo" type="button" class="btn-animated bg-green" runat="server"><span class="glyphicon glyphicon-ok  BtnGlyphicon"></span> Yes</asp:LinkButton><%--OnClick="ConfirmYes"--%>
                    <asp:LinkButton ID="btnNoConfirmYesNo" type="button" class="btn-animated bg-horrible" runat="server"><span class="glyphicon glyphicon-remove BtnGlyphicon"></span> No</asp:LinkButton>
                    <%--OnClick="ConfirmNo"--%>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
