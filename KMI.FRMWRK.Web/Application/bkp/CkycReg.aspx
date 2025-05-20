<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="CkycReg.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.CKYC.CkycReg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/bundles/CKYCValidationjs") %>
    <%: Styles.Render("~/bundles/CKYCcss") %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">
    <style>
         /* Small even padding on each side */
        .radio_NEw {
            padding: 0px 6px;
        }
        /* Large amount of padding just on the right side */
        .radio_NEw {
            padding-right: 30px;
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
        .radiobtn {}
    </style>
    <script type="text/javascript">

        function lettersOnly() {
            var charCode = event.keyCode;
            if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8 || charCode == 32)
                return true;
            else
                return false;
        }

        function CheckMaritalStatus(Source) {
            debugger;
            var mStatus = $("#ddlMaritalStatus").val();
            var prefix = $("#cboTitle").val();
            if (prefix == "MS" && (mStatus == "01")) {
                popup("Marital status is invalid");
                if (Source == "MStatus") {
                    $("#ddlMaritalStatus").val('');
                }
                else if (Source == "prefix") {
                    $("#cboTitle").val('');
                }
            }
        }

        function CheckGenderPrefix(Sources) {
            debugger;
            var prefix = $("#cboTitle").val();
            var gender = $("#cboGender").val();
            if (prefix == 'MR') {
                if (gender == "F") {

                    if (Sources == 'prefix') {
                        popup("Prefix cannot be Mr. when gender is female");
                        $("#cboTitle").val('');
                    }
                    else {
                        popup("Gender cannot be female when prefix is Mr.");
                        $("#cboGender").val('');
                    }
                }
            }
            else if (prefix == 'MS' || prefix == 'MRS') {
                if (gender == "M") {
                    if (Sources == 'prefix') {
                        popup("Prefix cannot be Ms. or Mrs. when gender is male");
                        $("#cboTitle").val('');
                    }
                    else {
                        popup("Gender cannot be male when prefix is  Ms. or Mrs.");
                        $("#cboGender").val('');
                    }
                }
            }
            if (Sources == 'prefix') {
                CheckMaritalStatus(Sources);
            }
        }

        function CheckFatherSpouce(Sources) {
            debugger;
            var FatherSelected = $("#rbtFS_0").is(':checked');
            if (FatherSelected) {
                var FatherPrefix = $("#cboTitle2").val();
                if (FatherPrefix == "") return;
                if (!(FatherPrefix == "MR" || FatherPrefix == "DR")) {
                    if (Sources == "rdoFather") {
                        popup("Father prefix cannot be Mrs. or Ms.");
                    }
                    else {
                        popup("Father prefix cannot be Mrs. or Ms.");
                        $("#cboTitle2").val("");
                    }
                }
            }
        }

        function validatePAN(obj) {
            var val = obj.value;
            if (obj.value == "")
                return;

            var reg = /^[A-Z]{3}P[A-Z]{1}[0-9]{4}[A-Z]{1}$/
            if (!(reg.test(val))) {
                popup("Invalid PAN format.");
                obj.value = "";
            }
        }


        function validatePOIDoc() {

        }

        function checkContactNumber(prefix, number, source) {
            var p = $(prefix).val().trim();
            var n = $(number).val().trim();
            if ((p == "" && n != "") || (p != "" && n == "")) {
                d
                if (source == "tele_home") {
                    popup("Resident STD code and Telephone number is mandatory");
                }
                else if (source == "tele_off") {
                    popup("Office STD code and Telephone number is mandatory");
                }
                else if (source == "mobile") {
                    popup("Mobile ISD code and mobile number is mandatory");
                }
                else if (source == "fax") {
                    popup("Fax STD code and fax number is mandatory");
                }
                return false;
            }
            return true;
        }

        function validateEmail(email) {
            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (email.value == "") return;
            if (!filter.test(email.value)) {
                email.value = "";
                popup("Invalid email address");
            }
        }

        function validateMobileNumber(obj) {
            debugger;
            var startWith = ["6", "7", "8", "9"]
            var first = obj.value.split('');

            if (obj.value == "") return;
            if (startWith.indexOf(first[0]) == -1 || obj.value.length != 10) {
                obj.value = "";
                popup("Mobile number should start with  6, 7, 8, 9 and 10 digit long");
            }
        }

        function popup(msg) {
            showModal('#myModal', 'Alert', 'alert-warning', '', '', msg);
        }

        function OpenRelatedPersonPage() {
            debugger;
            var modal = document.getElementById('myModalRaise');
            var modaliframe = document.getElementById("iframeCFR");
            modaliframe.src = "CKYCRelatedPrsn.aspx?Status=reg&mdl=myModalRaise";
            $('#myModalRaise').modal();
        }

        function OpenRelatedPersonPageView(RelRefnNo, refno) {
            debugger;
            var modal = document.getElementById('myModalRaise');
            var modaliframe = document.getElementById("iframeCFR");

            modaliframe.src = "CKYCRelatedPrsn.aspx?Status=Mod&refno=" + refno + "&relrefno=" + RelRefnNo;
            $('#myModalRaise').modal('show');
        }

        function OpenRelatedPersonPageEdit(RelRefnNo, refno) {
            debugger;
            var modal = document.getElementById('myModalRaise');
            var modaliframe = document.getElementById("iframeCFR");

            modaliframe.src = "CKYCRelatedPrsn.aspx?Status=Mod&refno=" + refno + "&relrefno=" + RelRefnNo;
            $('#myModalRaise').modal('show');
        }


        function OpenPartialRelatedPersonPageView(RelRefnNo, refno) {
            debugger;
            var modal = document.getElementById('myModalRaise');
            var modaliframe = document.getElementById("iframeCFR");

            modaliframe.src = "CKYCRelatedPrsn.aspx?Status=PMod&Action=View&refno=" + refno + "&relrefno=" + RelRefnNo;
            $('#myModalRaise').modal('show');
        }


        function OpenPartialRelatedPersonPageEdit(RelRefnNo, refno) {
            debugger;
            var modal = document.getElementById('myModalRaise');
            var modaliframe = document.getElementById("iframeCFR");

            modaliframe.src = "CKYCRelatedPrsn.aspx?Status=PMod&Action=Edit&refno=" + refno + "&relrefno=" + RelRefnNo;
            $('#myModalRaise').modal('show');
        }

        function funddlProofRelPerson() {
            $('#menu1').attr("class", "tab-pane fade");
            $('#EmptyPagePlaceholder_personal').attr("aria-expanded", false);
            $('#EmptyPagePlaceholder_m1').removeAttr("class");
            $('#EmptyPagePlaceholder_m3').attr("class", "active");
            $('#EmptyPagePlaceholder_A3').attr("aria-expanded", true);
            $('#menu4').attr("class", "tab-pane fade in active");
        }
    </script>

    <%--<asp:ScriptManager ID="SM1" runat="server">--%>
    <%--<Scripts>
            <asp:ScriptReference Path="../../../Application/Common/Lookup.js" />
        </Scripts>
        <Services>
            <asp:ServiceReference Path="../../../Application/Common/Lookup.asmx" />
        </Services>--%>
    <%-- </asp:ScriptManager>--%>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="container" style="margin-top: 0px; width: 100%;">
                <%-- Added for CKYC Details header start--%>
                <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                    <div id="Div18" runat="server" class="panel-heading" onclick="showHideDiv('divCKYCdtls','btnCKYCdtls');return false;">
                        <div class="row">
                            <div class="col-sm-10" style="text-align: left">
                                <span class="glyphicon glyphicon-menu-hamburger"></span>
                                <asp:Label ID="lblOfcuseOnly" Text="" runat="server" CssClass="control-label">
                                </asp:Label>
                            </div>
                            <div class="col-sm-2" style="text-align: right">
                                <asp:Label ID="Label3" runat="server" CssClass="control-label"></asp:Label>
                                <span id="btnCKYCdtls" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div>
                    <div id="divCKYCdtls" style="display: block;" class="panel-body">
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblAppType" Text="" runat="server" Font-Bold="false">
                                </asp:Label>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:CheckBox ID="cbNew" runat="server" CssClass="standardcheckbox" Text="New" AutoPostBack="true"
                                    Enabled="false" TabIndex="20" />
                                <asp:CheckBox ID="cbUpdate" runat="server" CssClass="standardcheckbox" Text="Update"
                                    AutoPostBack="true" Visible="false" TabIndex="1" />
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblRefNumber" Text="" runat="server" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:TextBox ID="txtRefNumber" runat="server" CssClass="form-control" Enabled="false"
                                    Font-Bold="false" TabIndex="2">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblAccountType" Text="" runat="server" Font-Bold="false">
                                </asp:Label>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:CheckBox ID="chkNormal" runat="server" CssClass="standardcheckbox chkClass" Text="Normal"
                                    AutoPostBack="true" TabIndex="3" name="cb1" value="value1" />
                                <asp:CheckBox ID="chkSimplified" runat="server" CssClass="standardcheckbox chkClass" Text="Simplified"
                                    AutoPostBack="true" TabIndex="3" name="cb2" value="value1" />
                                <asp:CheckBox ID="Chksmall" runat="server" CssClass="standardcheckbox chkClass" Text="Small"
                                    AutoPostBack="true" TabIndex="5"
                                    name="cb3" value="value1" />
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblKYCNumber" Text="" runat="server" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:TextBox ID="txtKYCNumber" runat="server" CssClass="form-control" Enabled="false"
                                    Font-Bold="false" TabIndex="2">
                                </asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <%-- Added for CKYC Details header end--%>



                <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                    <div id="Div19" runat="server" class="panel-heading">
                        <div class="row">
                            <div class="col-sm-10" style="text-align: left">
                                <span class="glyphicon glyphicon-menu-hamburger"></span>
                                <asp:CheckBox ID="ChkUpdPersonal" runat="server" CssClass="standardcheckbox" Text=""
                                    AutoPostBack="true" TabIndex="1" OnCheckedChanged="ChkUpdPersonal_Checked" />
                                <asp:Label ID="lblpfPersonal1" Text="" runat="server" CssClass="control-label">
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
                            <div id="Div2" runat="server" class="panel-heading subheader"
                                onclick="ShowReqDtl1('divPersonal','btnpersnl');return false;">
                                <div class="row">
                                    <div class="col-sm-10" style="text-align: left">
                                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                                        <asp:Label ID="lblpfPersonal" Text="" runat="server" CssClass="control-label">
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
                                        <asp:Label ID="lblName" Text="" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <span style="color: red">*</span>
                                        <asp:CheckBox ID="ChkUpdName" runat="server" CssClass="standardcheckbox" Text=""
                                            OnCheckedChanged="ChkUpdName_Checked" AutoPostBack="true" TabIndex="1" />
                                    </div>
                                    <div class="col-sm-9" style="padding: 0">
                                        <div class="col-sm-2">
                                            <%-- <asp:UpdatePanel ID="upcboTitle" runat="server">
                                                <ContentTemplate>--%>
                                            <asp:DropDownList ID="cboTitle" runat="server" CssClass="form-control" DataTextField="ParamDesc"
                                                DataValueField="ParamValue" AppendDataBoundItems="True" TabIndex="6" ClientIDMode="Static" onchange="CheckGenderPrefix('prefix')">
                                            </asp:DropDownList>
                                            <%-- </ContentTemplate>
                                            </asp:UpdatePanel>--%>
                                        </div>
                                        <div class="col-sm-10" style="padding: 0">
                                            <div class="col-sm-4" style="padding-left: 0">
                                                <asp:TextBox ID="txtGivenName" runat="server" CssClass="form-control" onkeypress="return lettersOnly();" onchange="javascript:this.value=this.value.toUpperCase();"
                                                    MaxLength="50" TabIndex="7" placeholder="First Name">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-sm-4" style="padding-left: 0">
                                                <asp:TextBox ID="txtMiddleName" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="return lettersOnly();"
                                                    MaxLength="50" TabIndex="8" onblur="CheckSpaces();return false;" placeholder="Middle Name">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-sm-4" style="padding-left: 0">
                                                <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="return lettersOnly()"
                                                    MaxLength="50" TabIndex="9" onblur="CheckSpaces();return false;" placeholder="Last Name">
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblMaidenName" Text="" CssClass="control-label" runat="server">
                                        </asp:Label>
                                    </div>
                                    <div class="col-sm-9" style="padding: 0">
                                        <div class="col-sm-2">
                                            <%--  <asp:UpdatePanel ID="ipcboTitle1" runat="server">
                                                <ContentTemplate>--%>
                                            <asp:DropDownList ID="cboTitle1" runat="server" CssClass="form-control" DataTextField="ParamDesc"
                                                DataValueField="ParamValue" AppendDataBoundItems="True" TabIndex="10">
                                            </asp:DropDownList>
                                            <%-- </ContentTemplate>
                                            </asp:UpdatePanel>--%>
                                        </div>
                                        <div class="col-sm-10" style="padding: 0">
                                            <div class="col-sm-4" style="padding-left: 0">
                                                <asp:TextBox ID="txtGivenName1" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="return lettersOnly()"
                                                    MaxLength="50" TabIndex="11" onblur="CheckSpaces();return false;" placeholder="First Name">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-sm-4" style="padding-left: 0">
                                                <asp:TextBox ID="txtMiddleName1" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="return lettersOnly()"
                                                    MaxLength="50" TabIndex="12" onblur="CheckSpaces();return false;" placeholder="Middle Name">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-sm-4" style="padding-left: 0">
                                                <asp:TextBox ID="txtLastName1" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="return lettersOnly()"
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
                                            <asp:Label ID="lblFatherName" Text="" CssClass="control-label"
                                                runat="server"></asp:Label>
                                            <span style="color: red">*</span>
                                        </div>
                                        <div class="col-sm-6" style="padding-right:14px">
                                            <%--   <asp:UpdatePanel ID="UpdFSFlag" runat="server">
                                                <ContentTemplate>--%>
                                            <asp:RadioButtonList ID="rbtFS" runat="server" CssClass="radiobtn" ClientIDMode="Static" RepeatDirection="Horizontal"
                                                Visible="true" TabIndex="14" AutoPostBack="false" Width="216px">
                                                <asp:ListItem class="radio_NEw" Value="F">Father</asp:ListItem>
                                                <asp:ListItem class="radio_NEw" Value="S">Spouse</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <%--</ContentTemplate>
                                            </asp:UpdatePanel>--%>
                                        </div>
                                    </div>
                                    <div class="col-sm-9" style="padding: 0">
                                        <div class="col-sm-2">
                                            <%--  <asp:UpdatePanel ID="upcboTitle2" runat="server">
                                                    <ContentTemplate>--%>
                                            <asp:DropDownList ID="cboTitle2" runat="server" CssClass="form-control" DataTextField="ParamDesc" onchange="CheckFatherSpouce('FatherPrefix')"
                                                DataValueField="ParamValue" AppendDataBoundItems="True" TabIndex="15" AutoPostBack="false" ClientIDMode="Static">
                                            </asp:DropDownList>
                                            <%--</ContentTemplate>
                                                </asp:UpdatePanel>--%>
                                        </div>
                                        <div class="col-sm-10" style="padding: 0">
                                            <div class="col-sm-4" style="padding-left: 0">
                                                <asp:TextBox ID="txtGivenName2" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="return lettersOnly()"
                                                    MaxLength="50" TabIndex="16" onblur="CheckSpaces();return false;" placeholder="First Name">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-sm-4" style="padding-left: 0">
                                                <asp:TextBox ID="txtMiddleName2" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="return lettersOnly()"
                                                    MaxLength="50" TabIndex="17" onblur="CheckSpaces();return false;" placeholder="Middle Name">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-sm-4" style="padding-left: 0">
                                                <asp:TextBox ID="txtLastName2" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="return lettersOnly()"
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
                                        <asp:Label ID="lblMotherName" Text="" CssClass="control-label" runat="server">
                                        </asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-9" style="padding: 0">
                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="cboTitle3" runat="server" CssClass="form-control" DataTextField="ParamDesc"
                                                DataValueField="ParamValue" AppendDataBoundItems="True" TabIndex="19">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-10" style="padding: 0">
                                            <div class="col-sm-4" style="padding-left: 0">
                                                <asp:TextBox ID="txtGivenName3" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="return lettersOnly()"
                                                    MaxLength="50" TabIndex="20" onblur="CheckSpaces();return false;" placeholder="First Name">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-sm-4" style="padding-left: 0">
                                                <asp:TextBox ID="txtMiddleName3" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="return lettersOnly()"
                                                    MaxLength="50" TabIndex="21" onblur="CheckSpaces();return false;" placeholder="Middle Name">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-sm-4" style="padding-left: 0">
                                                <asp:TextBox ID="txtLastName3" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="return lettersOnly()"
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
                                        <asp:Label ID="lbldob" Text=" " runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control"
                                            runat="server" ID="txtDOB" MaxLength="15" onmousedown="$('#EmptyPagePlaceholder_txtDOB').datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy', yearRange: '1945:'+(new Date).getFullYear()  });"
                                            TabIndex="23" onblur="setDateFormat('EmptyPagePlaceholder_txtDOB');" />
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <%--Added by shreela on 6/03/14 to remove space--%>
                                        <asp:Label ID="lblGender" Text="" runat="server" CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                        <%-- <span style="color: #ff0000">*</span>--%>
                                    </div>
                                    <div class="col-sm-3">
                                        <%--<asp:UpdatePanel ID="upcboGender" runat="server">
                                            <ContentTemplate>--%>
                                        <asp:DropDownList ID="cboGender" runat="server" CssClass="form-control" TabIndex="24" ClientIDMode="Static" onchange="CheckGenderPrefix('gender')">
                                        </asp:DropDownList>
                                        <%-- </ContentTemplate>
                                        </asp:UpdatePanel>--%>
                                    </div>
                                </div>
                                <div class="row">
                                    <%--<asp:UpdatePanel ID="upOccuSubType" runat="server">
                                        <ContentTemplate>--%>
                                    <%-- <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>--%>
                                    <%--<div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblOccupation" Text="" runat="server" CssClass="control-label">
                                        </asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlOccupation" AutoPostBack="true" runat="server" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlOccupation_SelectedIndexChanged" TabIndex="25">
                                        </asp:DropDownList>
                                    </div>--%>
                                    <%--  <div class="col-sm-3" style="text-align: left" id="divOccuSubType" runat="server">
                                        <asp:Label ID="lblOccuSubType" Text="" runat="server" CssClass="control-label">
                                        </asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlOccuSubType" runat="server" CssClass="form-control" TabIndex="26">
                                            <asp:ListItem Text="Select" />
                                        </asp:DropDownList>
                                    </div>--%>
                                    <%-- </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlOccupation" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>--%>
                                    <%-- </ContentTemplate>
                                    </asp:UpdatePanel>--%>
                                </div>
                                <div class="row">
                                    <%--<div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblMarStatus" Text="" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        
                                    </div>
                                    <div class="col-sm-3">        
                                        <asp:DropDownList ID="ddlMaritalStatus" runat="server" CssClass="form-control" ClientIDMode="Static" onchange="CheckMaritalStatus('MStatus')" TabIndex="27">
                                        </asp:DropDownList>>
                                    </div>--%>
                                    <%--<div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblCitizenship" Text="" runat="server" CssClass="control-label">
                                        </asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlCitizenship" runat="server" CssClass="form-control" TabIndex="28"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlCitizenship_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>--%>
                                </div>
                                <div class="row">
                                    <%--<div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblResStatus" Text="" runat="server" CssClass="control-label">
                                        </asp:Label>
                                    </div>
                                    <div class="col-sm-3">                                     
                                        <asp:DropDownList ID="ddlResStatus" runat="server" CssClass="form-control" TabIndex="29">
                                        </asp:DropDownList>
                                        
                                    </div>--%>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="Label1" Text="PAN" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:TextBox runat="server" ID="txtPanNo" CssClass="form-control" ClientIDMode="Static" onblur="return validatePAN(this)" onkeyup="javascript: this.value = this.value.toUpperCase()" TabIndex="29" />
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <%--<asp:UpdatePanel ID="uplblIsoCountryCodeOthr" runat="server">
                                    <ContentTemplate>--%>
                                        <asp:Label ID="lblIsoCountryCodeOthr" Text="" Visible="false"
                                            runat="server" CssClass="control-label"></asp:Label>
                                        <span id="asteriskIsoCountryCodeOthr" style="color: red" runat="server" visible="false">*</span>
                                        <%--  </ContentTemplate>
                                </asp:UpdatePanel>--%>
                                    </div>
                                    <div class="col-sm-3">
                                        <%--<asp:UpdatePanel ID="upIsoCountryCodeOthr" runat="server">
                                    <ContentTemplate>--%>
                                        <asp:DropDownList ID="ddlIsoCountryCodeOthr" runat="server" CssClass="form-control"
                                            AutoPostBack="true" TabIndex="30" Visible="false">
                                        </asp:DropDownList>
                                        <%--</ContentTemplate>
                                </asp:UpdatePanel>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--  Added for Personal Details end --%>
                        <%--  Added for Tick If Applicable start --%>
                        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px; display: none">
                            <div id="Div1" runat="server" class="panel-heading subheader" style="background-color: #EDF1cc !important"
                                onclick="ShowReqDtl1('div3','Span1');return false;">
                                <div class="row">
                                    <div class="col-sm-10" style="text-align: left">
                                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                                        <asp:Label ID="lbltick" Text="" runat="server" CssClass="control-label">
                                        </asp:Label>&nbsp;&nbsp;
                                        <%-- <asp:CheckBox ID="chkTick" Text=" RESIDENCE FOR TAX PURPOSES IN JURISDICTIONS(S) OUTSIDE INDIA" OnCheckedChanged="chkTick_Checked"
                                            CssClass="standardcheckbox"  runat="server" />--%>
                                    </div>
                                    <div class="col-sm-2">
                                        <span id="Span1" class="glyphicon glyphicon-resize-full" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                                    </div>
                                </div>
                            </div>
                            <div id="div3" style="display: block;" class="panel-body">
                                ADIITIONAL DETAILS REQUIRED*(Mandatory only if section 2 is ticked)
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-sm-12" style="text-align: left">
                                        <asp:CheckBox ID="chkTick" Text=" RESIDENCE FOR TAX PURPOSES IN JURISDICTIONS(S) OUTSIDE INDIA"
                                            OnCheckedChanged="chkTick_Checked" CssClass="standardcheckbox" runat="server"
                                            TabIndex="31" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblIsoCountryCode2" Text=""
                                            runat="server" CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <%-- <asp:textbox cssclass="form-control" onmousedown="$('#txtDob').datepicker({ changeMonth: true, changeYear: true });"
                                    onchange="setDateFormat('txtDob')" runat="server" id="txtIsoCountryCode2" maxlength="15"
                                    tabindex="12" enabled="true" />--%>
                                        <asp:DropDownList ID="ddlIsoCountryCode2" runat="server" CssClass="form-control"
                                            AutoPostBack="true" TabIndex="32">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblTaxIden" Text=""
                                            runat="server" CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control"
                                            runat="server" ID="txtIDResTax" MaxLength="20" onkeypress="funIsAlphaNumeric();"
                                            TabIndex="33" />
                                        <%--onmousedown="$('#EmptyPagePlaceholder_txtIDResTax').datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' });"--%>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblPlace" Text="" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control"
                                            runat="server" ID="txtDOBRes" MaxLength="15"
                                            TabIndex="34" />
                                        <%--onmousedown="$('#EmptyPagePlaceholder_txtDOBRes').datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' });"--%>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblIsoContry" Text="" runat="server"
                                            CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <%--<asp:textbox cssclass="form-control" onmousedown="$('#txtDob').datepicker({ changeMonth: true, changeYear: true });"
                                    onchange="setDateFormat('txtDob')" runat="server" id="txtIsoCountry" maxlength="15"
                                    tabindex="12" />--%>
                                        <asp:DropDownList ID="ddlIsoCountry" runat="server" CssClass="form-control" AutoPostBack="true"
                                            TabIndex="35">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--  Added for Tick If Applicable end --%>
                    </div>
                </div>

                <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                    <div id="Div4" runat="server" class="panel-heading">
                        <div class="row">
                            <div class="col-sm-10" style="text-align: left">
                                <span class="glyphicon glyphicon-menu-hamburger"></span>
                                <asp:CheckBox ID="ChkUpdID" runat="server" CssClass="standardcheckbox" Text="" AutoPostBack="true"
                                    TabIndex="1" OnCheckedChanged="ChkUpdID_Checked" />
                                <asp:Label ID="lblProofOfIdentity11" Text="" runat="server"
                                    CssClass="control-label"></asp:Label>
                            </div>
                            <div class="col-sm-2" onclick="showHideDiv('menu2','btnProofIdentity');return false;">
                                <span id="btnProofIdentity" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div>
                    <div id="menu2" style="display: block;" class="panel-body">
                        <%--  Added for Proof of Identity start--%>

                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblProof" Text=""
                                    runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddlProofIdentity" runat="server" CssClass="form-control" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlProofIdentity_SelectedIndexChanged" TabIndex="36">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div id="divIdProof" runat="server" class="row">
                            <div id="divPassNo" runat="server" class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblPassportNo" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div id="divPassNotxt" runat="server" class="col-sm-3">
                                <asp:TextBox CssClass="form-control" runat="server" onChange="javascript:this.value=this.value.toUpperCase();"
                                    ID="txtPassNo" MaxLength="20" TabIndex="37" onkeypress="funIsAlphaNumeric()" />

                            </div>
                            <div id="divPass" runat="server" class="col-sm-3" style="text-align: left">
                                <asp:Label ID="llPassExpDate" runat="server" CssClass="control-label"></asp:Label>
                                <%--<span style="color: red">*</span>--%>
                            </div>
                            <div id="divPassDate" runat="server" class="col-sm-3">
                                <asp:TextBox CssClass="form-control" onmousedown="$('#EmptyPagePlaceholder_txtPassExpDate').datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' });"
                                    runat="server"
                                    ID="txtPassExpDate" MaxLength="15" TabIndex="38" />
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtPassOthr" MaxLength="15"
                                    TabIndex="39" />
                            </div>
                        </div>

                    </div>
                </div>

                <%-- Added for Proof of Identity end--%>
                <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                    <div id="Div20" runat="server" class="panel-heading">
                        <div class="row">
                            <div class="col-sm-10" style="text-align: left">
                                <span class="glyphicon glyphicon-menu-hamburger"></span>
                                <asp:Label ID="lblpfofAddr1" Text="" runat="server" CssClass="control-label">
                                </asp:Label>
                            </div>
                            <div class="col-sm-2" onclick="showHideDiv('menu3','btnpersnl');return false;">
                                <span id="Span9" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div>
                    <div id="menu3" style="display: block;" class="panel-body">
                        <%--<asp:UpdatePanel ID="upMenu3" runat="server">
                    <ContentTemplate>--%>
                        <%--  Added for Proof of Address start--%>
                        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                            <div id="Div6" runat="server" class="panel-heading subheader">
                                <div class="row">
                                    <div class="col-sm-10" style="text-align: left">
                                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                                        <asp:CheckBox ID="ChkUpdAddr" runat="server" CssClass="standardcheckbox" Text=""
                                            OnCheckedChanged="ChkUpdAddr_Checked" AutoPostBack="true" TabIndex="1" />
                                        <asp:Label ID="lblpfofAddr2" Text="" runat="server" CssClass="control-label"></asp:Label>
                                    </div>
                                    <div class="col-sm-2" onclick="ShowReqDtl1('div7','Span2');return false;">
                                        <span id="Span2" class="glyphicon glyphicon-resize-full" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                                    </div>
                                </div>
                            </div>
                            <div id="div7" style="display: block;" class="panel-body">
                                <div class="row">
                                    <div class="col-sm-12" style="text-align: left">
                                        <asp:CheckBox ID="chkPerAddress" Text="CURRENT/PERMANENT/OVERSEAS ADDRESS DETAILS"
                                            AutoPostBack="true" OnCheckedChanged="chkPerAddress_Checked" runat="server" CssClass="control-label"
                                            TabIndex="40" />
                                        <span style="color: red">*</span>
                                    </div>
                                </div>
                                <div class="row">
                                    <%--<div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblAddressType" Text="" runat="server" CssClass="control-label"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlAddressType" runat="server" CssClass="form-control" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlAddressType_SelectedIndexChanged" TabIndex="41">
                                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>--%>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblProofOfAddress" Text="" runat="server" CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlProofOfAddress" runat="server" CssClass="form-control" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlProofOfAddress_SelectedIndexChanged" TabIndex="42">
                                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div id="divAddProof" runat="server" class="row">
                                    <div id="divPassNoAdd" runat="server" class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblPassportNoAdd" runat="server" CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div id="divPassNotxtAdd" runat="server" class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" runat="server" onChange="javascript:this.value=this.value.toUpperCase();"
                                            ID="txtPassNoAdd" MaxLength="15" TabIndex="43" />

                                    </div>
                                    <div id="divPassAdd" runat="server" class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="llPassExpDateAdd" runat="server" CssClass="control-label"></asp:Label>
                                        <%--<span style="color: red">*</span>--%>
                                    </div>
                                    <div id="divPassDateAdd" runat="server" class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" onmousedown="$('#EmptyPagePlaceholder_txtPassExpDateAdd').datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' });"
                                            runat="server"
                                            ID="txtPassExpDateAdd" MaxLength="15" TabIndex="44" />
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtPassOthrAdd" MaxLength="15"
                                            TabIndex="45" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblAddressLine1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" runat="server"
                                            ID="txtAddressLine1" MaxLength="55" TabIndex="46" />
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblAddressLine2" Text="" runat="server" CssClass="control-label"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control"
                                            runat="server" ID="txtAddressLine2" MaxLength="55" TabIndex="47" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblAddressLine3" Text="" runat="server" CssClass="control-label"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtDrivLicNum')" runat="server"
                                            ID="txtAddressLine3" MaxLength="55" TabIndex="48" />
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblCity" Text="" runat="server" CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" TabIndex="49" MaxLength="50">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblDistrict" Text="" runat="server" CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" Enabled="false"
                                            TabIndex="50">
                                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblPinCode" Text="" runat="server" CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlPinCode" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPinCode_SelectedIndexChanged"
                                            AutoPostBack="True" TabIndex="51">
                                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblState" Text="" runat="server" CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" Enabled="false"
                                            TabIndex="52">
                                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblIsoCountryCode" Text="" runat="server" CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <%--       <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtState')" runat="server"
                                                    ID="txtCountryCode" MaxLength="15"  TabIndex="12" Enabled="false" />--%>
                                        <asp:DropDownList ID="ddlCountryCode" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCountryCode_SelectedIndexChanged"
                                            TabIndex="53">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div style="margin-top: 25px; margin-bottom: 25px">
                                    <div class="row">
                                        <div class="col-sm-12" style="text-align: left">
                                            <asp:CheckBox ID="chkLocalAddress" Text="CORRESPONDENCE/LOCAL ADDRESS DETAILS" runat="server"
                                                CssClass="control-label" TabIndex="54" />
                                            <span style="color: red">*</span>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12" style="text-align: left">
                                            <asp:CheckBox ID="chkCuurentAddress" Text="Same as Current/Permanent/Overseas Address details"
                                                OnCheckedChanged="chkCuurentAddress_Checked" AutoPostBack="true" runat="server"
                                                CssClass="control-label" TabIndex="55" />
                                            <%--<span style="color: red">*</span>--%>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3" style="text-align: left">
                                            <asp:Label ID="lblLocAddLine1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                            <span style="color: red">*</span>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox CssClass="form-control" runat="server"
                                                ID="txtLocAddLine1" MaxLength="55" TabIndex="56" />
                                        </div>
                                        <div class="col-sm-3" style="text-align: left">
                                            <asp:Label ID="lblLocAddLine2" Text="" runat="server" CssClass="control-label"></asp:Label>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox CssClass="form-control" runat="server"
                                                ID="txtLocAddLine2" MaxLength="55" TabIndex="57" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3" style="text-align: left">
                                            <asp:Label ID="lblLocAddLine3" Text="" runat="server" CssClass="control-label"></asp:Label>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox CssClass="form-control" runat="server"
                                                ID="txtLocAddLine3" MaxLength="55" TabIndex="58" />
                                        </div>
                                        <div class="col-sm-3" style="text-align: left">
                                            <asp:Label ID="lblCity1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                            <span style="color: red">*</span>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtCity1" runat="server" CssClass="form-control" TabIndex="59" MaxLength="50"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3" style="text-align: left">
                                            <asp:Label ID="lblDistrict1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                            <span style="color: red">*</span>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlDistrict1" runat="server" CssClass="form-control" Enabled="false"
                                                TabIndex="60">
                                                <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3" style="text-align: left">
                                            <asp:Label ID="lblPin1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                            <span style="color: red">*</span>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlPinCode1" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPinCode1_SelectedIndexChanged"
                                                AutoPostBack="True" TabIndex="61">
                                                <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3" style="text-align: left">
                                            <asp:Label ID="lblState1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                            <span style="color: red">*</span>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlState1" runat="server" CssClass="form-control" Enabled="false"
                                                TabIndex="62">
                                                <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3" style="text-align: left">
                                            <asp:Label ID="lblCountryCode1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                            <span style="color: red">*</span>
                                        </div>
                                        <div class="col-sm-3">
                                            <%--  <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtState')" runat="server"
                                                    ID="txtCountryCode1" MaxLength="15" TabIndex="12"  Enabled="false"/>--%>
                                            <asp:DropDownList ID="ddlCountryCode1" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCountryCode1_SelectedIndexChanged"
                                                TabIndex="63">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <%--                                <div class="row">
                                    <div class="col-sm-12" style="text-align: left">
                                        <asp:CheckBox ID="chkAddResident" Text="ADDRESS IN THE JURISDICTION DETAILS WHERE APPLICANT IS RESIDENT OUTSIDE INDIA FOR TAX PURPOSES"
                                            runat="server" CssClass="control-label" TabIndex="64" />

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6" style="text-align: left">
                                        <asp:CheckBox ID="chkCurrentAdd" Text="Same as Current/Permanent/Overseas Address details"
                                            TabIndex="65" OnCheckedChanged="chkCurrentAdd_Checked" AutoPostBack="true" runat="server"
                                            CssClass="control-label" />

                                    </div>
                                    <div class="col-sm-6" style="text-align: left">
                                        <asp:CheckBox ID="chkCorresAdd" Text="Same as Correspondance/Local Address details"
                                            TabIndex="66" OnCheckedChanged="chkCorresAdd_Checked" AutoPostBack="true" runat="server"
                                            CssClass="control-label" />

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblAddLine1" Text="" runat="server" CssClass="control-label"></asp:Label>

                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" runat="server"
                                            ID="txtAddLine1" MaxLength="55" TabIndex="67" />
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblAddLine2" Text="" runat="server" CssClass="control-label"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" runat="server"
                                            ID="txtAddLine2" MaxLength="55" TabIndex="68" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblAddLine3" Text="" runat="server" CssClass="control-label"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" runat="server"
                                            ID="txtAddLine3" MaxLength="55" TabIndex="69" />
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblCity2" Text="" runat="server" CssClass="control-label"></asp:Label>

                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtCity2" runat="server" CssClass="form-control" TabIndex="70" MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblDistrict2" Text="" runat="server" CssClass="control-label"></asp:Label>

                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlDistrict2" runat="server" CssClass="form-control" AutoPostBack="true"
                                            Enabled="false" TabIndex="71">
                                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblPin2" Text="" runat="server" CssClass="control-label"></asp:Label>

                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlPinCode2" runat="server" CssClass="form-control" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlPinCode2_SelectedIndexChanged" TabIndex="72">
                                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblState2" Text="" runat="server" CssClass="control-label"></asp:Label>

                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlState2" runat="server" CssClass="form-control" TabIndex="73"
                                            Enabled="false">
                                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblIsoCountry2" Text="" runat="server" CssClass="control-label"></asp:Label>

                                    </div>
                                    <div class="col-sm-3">
                                        <%-- <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtIsoCountryCode')"
                                        <asp:DropDownList ID="ddlIsoCountryCode" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlIsoCountryCode_SelectedIndexChanged"
                                            TabIndex="74">
                                        </asp:DropDownList>
                                    </div>
                                </div>--%>
                            </div>
                        </div>
                        <%-- Added for Proof of Address end--%>
                        <%--  Added for Contact Details start--%>
                        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                            <div id="Div8" runat="server" class="panel-heading subheader">
                                <div class="row">
                                    <div class="col-sm-10" style="text-align: left">
                                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                                        <asp:CheckBox ID="ChkUpdContact" runat="server" CssClass="standardcheckbox" Text=""
                                            OnCheckedChanged="ChkUpdContact_Checked" AutoPostBack="true" TabIndex="1" />
                                        <asp:Label ID="lblContactDetails" Text=""
                                            runat="server" CssClass="control-label"></asp:Label>
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
                                            <%--<span class="input-group-addon" style="width: 20% !important; border-top-left-radius: 7% !important; padding:0px !important; border:0px !important;">--%>
                                            <span class="input-group-addon input-group-addon-tel">
                                                <asp:TextBox ID="txtTelOff" runat="server" CssClass="form-control" TabIndex="75" onkeypress="fncInputNumericValuesOnly();" MaxLength="4" Style="border-top-left-radius: 4px; border-bottom-left-radius: 4px;"></asp:TextBox>
                                            </span>
                                            <asp:TextBox ID="txtTelOff2" runat="server" CssClass="form-control" onkeypress="fncInputNumericValuesOnly();"
                                                MaxLength="10" TabIndex="76"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblTelRes" runat="server" CssClass="control-label" Text="Tel.(Res)"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="input-group">
                                            <span class="input-group-addon input-group-addon-tel">
                                                <asp:TextBox ID="txtTelRes" runat="server" CssClass="form-control" TabIndex="77" onkeypress="fncInputNumericValuesOnly();" MaxLength="4" Style="border-top-left-radius: 4px; border-bottom-left-radius: 4px;"></asp:TextBox>
                                            </span>
                                            <asp:TextBox ID="txtTelRes2" runat="server" CssClass="form-control" MaxLength="10" onkeypress="fncInputNumericValuesOnly();"
                                                TabIndex="78"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblMobile" runat="server" Text="" CssClass="control-label"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="input-group">
                                            <span class="input-group-addon input-group-addon-tel">
                                                <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" TabIndex="79" onkeypress="fncInputNumericValuesOnly();" MaxLength="3" Style="border-top-left-radius: 4px; border-bottom-left-radius: 4px;"></asp:TextBox>
                                            </span>
                                            <asp:TextBox ID="txtMobile2" runat="server" CssClass="form-control" onblur="validateMobileNumber(this)" onkeypress="fncInputNumericValuesOnly();"
                                                MaxLength="10" TabIndex="80"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblFax" runat="server" Text="" CssClass="control-label"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="input-group">
                                            <span class="input-group-addon input-group-addon-tel">
                                                <asp:TextBox ID="txtFax1" runat="server" CssClass="form-control" TabIndex="81" onkeypress="fncInputNumericValuesOnly();" MaxLength="4" Style="border-top-left-radius: 4px; border-bottom-left-radius: 4px;"></asp:TextBox>
                                            </span>
                                            <asp:TextBox ID="txtFax2" runat="server" CssClass="form-control" MaxLength="10" TabIndex="82" onkeypress="fncInputNumericValuesOnly();"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblpfemail" runat="server" Text="" CssClass="control-label"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtemail" runat="server" CssClass="form-control" MaxLength="100" onblur="validateEmail(this)"
                                            TabIndex="83"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                    <%-- </ContentTemplate>
                </asp:UpdatePanel>--%>
                    <%--  Added for Contact Details end--%>
                </div>


                <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                    <div id="Div21" runat="server" class="panel-heading">
                        <div class="row">
                            <div class="col-sm-10" style="text-align: left">
                                <span class="glyphicon glyphicon-menu-hamburger"></span>
                                <asp:CheckBox ID="ChkUpdRelated" runat="server" CssClass="standardcheckbox" Text=""
                                    OnCheckedChanged="ChkUpdRelated_Checked" AutoPostBack="true" TabIndex="1" />
                                <asp:Label ID="lblDtlOfRtltpr" Text=""
                                    runat="server" CssClass="control-label">
                                </asp:Label>
                            </div>
                            <div class="col-sm-2" onclick="showHideDiv('menu4','Span10');return false;">
                                <span id="Span10" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div>
                    <div id="menu4" style="display: block;" class="panel-body">

                        <%--  Added for Details of Related Person start--%>
                        <div class="row">
                            <div id="divchkAddRel" class="col-sm-3" style="text-align: left" runat="server">
                                <asp:CheckBox ID="chkAddRel" Text=" Addition of Related Person" TabIndex="84" AutoPostBack="true"
                                    runat="server" CssClass="control-label" OnCheckedChanged="chkAddRel_Checked" />
                                <%--<span style="color: red">*</span>--%>
                            </div>
                            <div id="divchkDelRel" class="col-sm-6" style="text-align: left" runat="server" visible="false">
                                <asp:CheckBox ID="chkDelRel" OnCheckedChanged="chkAddRel_Checked" Text=" Deletion of Related Person" TabIndex="85" runat="server"
                                    CssClass="control-label" />
                                <span style="color: red">*</span>
                            </div>
                            <div id="div10" class="col-sm-3" style="text-align: left" runat="server">
                            </div>
                            <div id="div11" class="col-sm-3" style="text-align: left" runat="server">
                            </div>
                            <div id="div5" class="col-sm-3" style="text-align: left" runat="server">

                                <asp:LinkButton ID="lnkViewRel" runat="server" Text="View Related Person Detail" FontBold="true" OnClick="lnkViewRel_Click"></asp:LinkButton>
                                <%-- --%>
                            </div>
                        </div>
                        <div class="row">
                            <div id="div12" class="col-sm-12" style="text-align: center" runat="server">
                                <asp:Label ID="lblRelRecordShow" Style="text-align: center" Text="  No Records Found" Visible="false" runat="server" />
                            </div>
                            <asp:GridView ID="gvMemDtls" Width="100%" runat="server" AllowSorting="True" CssClass="footable"
                                PageSize="10" AllowPaging="true" CellPadding="1"
                                AutoGenerateColumns="False" OnRowDataBound="gvMemDtls_RowDataBound">
                                <RowStyle CssClass="GridViewRow"></RowStyle>
                                <%--<PagerStyle CssClass="disablepage" />--%>
                                <%--OnPageIndexChanging="gvMemDtls_PageIndexChanging" OnRowCreated="gvMemDtls_RowCreated"--%>
                                <FooterStyle CssClass="GridViewFooter" />
                                <HeaderStyle HorizontalAlign="Center" BackColor="#dce9f9" />
                                <SelectedRowStyle CssClass="GridViewSelectedRow" />
                                <AlternatingRowStyle CssClass="GridViewAlternateRow"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr. No." ItemStyle-Width="10%" SortExpression="Reference No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Relative Reference No." ItemStyle-Width="20%" SortExpression="Reference No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("RelRefNo") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Relative Type" ItemStyle-Width="20%" SortExpression="Reference No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRelTypVal" runat="server" Text='<%# Eval("RelationTypetxt") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Relative Name" ItemStyle-Width="20%" SortExpression="Candidate Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNameVal" runat="server" Text='<%# Eval("FNameRel") + " " + Eval("LNameRel")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date Of Birth" ItemStyle-Width="20%" SortExpression="KYC No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMemDOBVal" runat="server" Text='<%# Eval("DOBRel") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gender" ItemStyle-Width="20%" SortExpression="KYC No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMemGenVal" runat="server" Text='<%# Eval("GenderReltxt") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Marital Status " ItemStyle-Width="20%" SortExpression="KYC No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMemMrtVal" runat="server" Text='<%# Eval("MaritalStatusReltxt") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Citizenship" ItemStyle-Width="20%" SortExpression="KYC No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMemCizVal" runat="server" Text='<%# Eval("CitizenshipReltxt") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Residential Status " ItemStyle-Width="20%" SortExpression="KYC No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMemResiVal" runat="server" Text='<%# Eval("ResiStatusReltxt") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Occupation Type" ItemStyle-Width="20%" SortExpression="KYC No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMemOccVal" runat="server" Text='<%# Eval("OccuTypeReltxt") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="15%" SortExpression="Request" HeaderText="Action">
                                        <ItemTemplate>
                                            <div style="width: 20%; white-space: nowrap;">
                                                <span class="glyphicon glyphicon-flag"></span>
                                                <asp:LinkButton ID="lnkdelete" runat="server" OnClick="lnkdelete_Click" Text="Delete"></asp:LinkButton>&nbsp;
                                              <asp:LinkButton ID="lnkView" runat="server" OnClick="lnkView_Click" Text="View" CommandArgument='<%# Eval("RelRefNo") %>'></asp:LinkButton>
                                                <asp:LinkButton ID="lnkEdit" runat="server" OnClick="lnkEdit_Click" Text="Edit" CommandArgument='<%# Eval("RelRefNo") %>'></asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Left" Width="10%" />
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
                        </div>



                        <%--<div class="row">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblRelType" Text="Related Person Type" runat="server" CssClass="control-label">
                        </asp:Label>
                       <span style="color:red">*</span>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlRelType" runat="server" CssClass="form-control" TabIndex="86">
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-3">
                        <asp:Label ID="lblKYCNum" Text="KYC Number of Related Person(if available)" placeholder="Passport Number"
                            runat="server" CssClass="control-label"></asp:Label>
                       <span style="color:red">*</span>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox CssClass="form-control" Enabled="false" runat="server" ID="txtKYCNum"
                            MaxLength="15" TabIndex="87" />
                    </div>
                </div>--%>
                        <%--<div class="row">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="txtName1" Text="Name" runat="server" CssClass="control-label"></asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-9">
                        <div style="display: flex;">
                            <asp:DropDownList ID="ddlPrefix" runat="server" CssClass="form-control" DataTextField="ParamDesc"
                                DataValueField="ParamValue" AppendDataBoundItems="True" Width="70px" TabIndex="88">
                            </asp:DropDownList>
                            &nbsp;
                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"
                                placeholder="First Name" MaxLength="30" TabIndex="89" onblur="CheckSpaces();return false;">
                            </asp:TextBox>&nbsp;
                            <asp:TextBox ID="txtMidddleName" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"
                                placeholder="Middle Name" MaxLength="30" TabIndex="90" onblur="CheckSpaces();return false;">
                            </asp:TextBox>&nbsp;
                            <asp:TextBox ID="txtSurName" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"
                                placeholder="Last Name" MaxLength="30" TabIndex="91" onblur="CheckSpaces();return false;">
                            </asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                FilterType="LowercaseLetters, UppercaseLetters,Custom" TargetControlID="txtFirstName"
                                ValidChars=" " FilterMode="ValidChars">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </div>
                    </div>
                </div>--%>
                        <br />
                        <%-- <div class="row">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblProofOfIdentity1" Text="PROOF OF IDENTITY[Pol] OF RELATED PERSON"
                            runat="server" CssClass="control-label"></asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlProofRelPerson" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlProofRelPerson_SelectedIndexChanged"
                            AutoPostBack="true" TabIndex="92">
                        </asp:DropDownList>
                    </div>
                </div>
                <div id="divRelIdProof" runat="server" class="row">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblPassNo2" runat="server" CssClass="control-label"></asp:Label>
                        <%--<span style="color:red">*</span>--%>
                        <%--</div>
                    <div class="col-sm-3">
                        <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtPassNo2')" runat="server"
                            ID="txtPassNo2" MaxLength="15" TabIndex="93" />
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblPassExpDate1" runat="server" CssClass="control-label"></asp:Label>
                        <%--<span style="color:red">*</span>--%>
                        <%--  </div>
                    <div class="col-sm-3">
                        <asp:TextBox CssClass="form-control" onmousedown="$('#txtPassExpDate1').datepicker({ changeMonth: true, changeYear: true });"
                            onchange="setDateFormat('txtPassExpDate1')" runat="server" ID="txtPassExpDate1"
                            MaxLength="15" TabIndex="94" />
                        <asp:TextBox CssClass="form-control" runat="server" ID="txtPassOthr2" MaxLength="15"
                            TabIndex="95" />
                    </div>
                </div>--%>
                        <div id="divRelAdd" runat="server" class="row">
                            <div class="col-sm-3" style="text-align: left">
                            </div>
                            <div class="col-sm-3">
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                            </div>
                            <div class="col-sm-3" style="text-align: right">
                            </div>
                        </div>
                        <%--  Added for Details of Related Person end--%>
                    </div>

                </div>

                <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                    <div id="Div22" runat="server" class="panel-heading">
                        <div class="row">
                            <div class="col-sm-10" style="text-align: left">
                                <span class="glyphicon glyphicon-menu-hamburger"></span>
                                <asp:CheckBox ID="ChkUpdRemark" runat="server" CssClass="standardcheckbox" Text=""
                                    OnCheckedChanged="ChkUpdRemark_Checked" AutoPostBack="true" TabIndex="1" />
                                <asp:Label ID="lblRemarks" Text="" runat="server" CssClass="control-label">
                                </asp:Label>
                            </div>
                            <div class="col-sm-2" onclick="showHideDiv('menu5','btnpersnl');return false;">
                                <span id="Span11" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div>
                    <div id="menu5" style="display: block;" class="panel-body">
                        <%--  Added for Details of Remarks start--%>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtRemarks')" runat="server"
                                    ID="txtRemarks" TextMode="MultiLine" MaxLength="15" TabIndex="96" />
                            </div>
                        </div>
                        <%--  Added for Details of Remarks end--%>
                    </div>
                </div>

                <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                    <div id="Div23" runat="server" class="panel-heading">
                        <div class="row">
                            <div class="col-sm-10" style="text-align: left">
                                <span class="glyphicon glyphicon-menu-hamburger"></span>
                                <asp:CheckBox ID="ChkUpdKYCVrfy" runat="server" CssClass="standardcheckbox" Text=""
                                    OnCheckedChanged="ChkUpdControlPrsn_Checked" AutoPostBack="true" TabIndex="1" />
                                <asp:Label ID="lblattstn" Text="" runat="server" CssClass="control-label">
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
                            <div id="Div14" runat="server" class="panel-heading subheader"
                                onclick="ShowReqDtl1('div15','Span6');return false;">
                                <div class="row">
                                    <div class="col-sm-10" style="text-align: left">
                                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                                        <asp:Label ID="lbldec" Text="" runat="server" CssClass="control-label">
                                        </asp:Label>
                                    </div>
                                    <div class="col-sm-2">
                                        <span id="Span6" class="glyphicon glyphicon-resize-full" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                                    </div>
                                </div>
                            </div>
                            <div id="div15" style="display: block;" class="panel-body">
                                <div class="row">
                                    <div class="col-sm-12" style="text-align: left; display: flex;">
                                        <%--  <asp:label cssclass="control-label" text="I hereby declare that the details furnished above are true and correct to the best of my knowledge and belief and I undertake to inform you of any changes therein immediately.In case any of the above information is found to be false or untrue or misleading or misrepresenting. I am aware that I may be held liable for it."
                                    onchange="setDateFormat('txtRemarks')" runat="server" id="lblAppDeclare1" maxlength="15"
                                    tabindex="12" />--%>
                                        <asp:CheckBox ID="chkAppDeclare1" Text="I hereby declare that the details furnished above are true and correct to the best of my knowledge and belief and I undertake  to inform you of any changes "
                                            CssClass="control-label" AutoPostBack="false" runat="server"
                                            TabIndex="97" />
                                    </div>
                                    <div class="col-sm-12" style="text-align: left; display: flex; font-weight: bold;">
                                        <asp:Label CssClass="control-label" Text="there in immediately.In case any of the above information is found to be false or untrue or misleading or misrepresenting. I am aware that I may be held   liable for it."
                                            runat="server" ID="lblAppDeclare1" maxlength="15" />
                                    </div>
                                    <br />
                                    <br />
                                    <br />
                                    <div class="col-sm-12" style="text-align: left; display: flex;">
                                        <%--   <asp:label cssclass="control-label" text="I hereby consent to receiving information from Central KYC Registry through SMS/Email on the above registered number/email address"
                                    onchange="setDateFormat('txtRemarks')" runat="server" id="Label7" maxlength="15"
                                    tabindex="12" />--%>
                                        <asp:CheckBox ID="chkAppDeclare2" Text="I hereby consent to receiving information from Central KYC Registry through SMS/Email on the above registered number/email address"
                                            CssClass="control-label" AutoPostBack="false" runat="server"
                                            TabIndex="98" />
                                    </div>
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblDate" Text=" " runat="server" CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" onmousedown="$('#EmptyPagePlaceholder_txtDate').datepicker({ changeMonth: true, changeYear: true, maxDate: '0D', dateFormat: 'dd/mm/yy' });"
                                            runat="server" ID="txtDate" MaxLength="15"
                                            TabIndex="99" />
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblPlace1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" runat="server"
                                            ID="txtPlace" MaxLength="15" TabIndex="100" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--  Added for Applicant Declaration end--%>
                        <%--  Added for Attestation/For Office Use Only start--%>
                        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                            <div id="Div16" runat="server" class="panel-heading subheader"
                                onclick="ShowReqDtl1('div17','Span7');return false;">
                                <div class="row">
                                    <div class="col-sm-10" style="text-align: left">
                                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                                        <asp:Label ID="lblAttesOfc" Text="" runat="server" CssClass="control-label">
                                        </asp:Label>
                                    </div>
                                    <div class="col-sm-2">
                                        <span id="Span7" class="glyphicon glyphicon-resize-full" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                                    </div>
                                </div>
                            </div>
                            <div id="div17" style="display: block;" class="panel-body">
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblDocRec" Text="Document Received" runat="server" Font-Bold="true"
                                            CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <%-- <asp:CheckBox ID="chkCertifyCopy" Text="Certified Copies" CssClass="standardcheckbox"
                                            Enabled="false" AutoPostBack="true" runat="server" TabIndex="101" />--%>

                                        <asp:DropDownList ID="ddlDocReceived" runat="server" CssClass="form-control" TabIndex="2">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12" style="text-align: center">
                                        <asp:Label ID="lblKYCVerify" Style='text-align: center' CssClass="control-label"
                                            Font-Bold="true" Text="" runat="server" />
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblDate3" Text="" runat="server" CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" onmousedown="$('#EmptyPagePlaceholder_txtDateKYCver').datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' });"
                                            onchange="setDateFormat('txtDateKYCver')" runat="server" ID="txtDateKYCver" MaxLength="15" Enabled="true"
                                            TabIndex="102" />
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblEmpName" Text="Employee Name" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtEmpName" MaxLength="15"
                                            Enabled="true" TabIndex="103" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblEmpCode" Text="Employee Code" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtEmpCode" MaxLength="15"
                                            Enabled="true" TabIndex="104" />
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblEmpDesignation" Text="Employee Designation" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtEmpDesignation" MaxLength="15"
                                            Enabled="true" TabIndex="105" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblEmpBranch" Text="Employee Branch" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtEmpBranch" MaxLength="15"
                                            Enabled="true" TabIndex="106" />
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-sm-12" style="text-align: center">
                                        <asp:Label ID="lblInsDtls" Style='text-align: center' CssClass="control-label" Font-Bold="true"
                                            Text="" runat="server" />
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
                                            onchange="setDateFormat('txtDate3')" runat="server" ID="txtInsName" MaxLength="15"
                                            Enabled="true" TabIndex="107" />
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblInsCode" Text="Code" runat="server" CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtInsCode" MaxLength="15"
                                            Enabled="true" TabIndex="108" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--  Added for Attestation/For Office Use Only  end--%>
                    </div>
                </div>

            </div>

            <div class="row" style="margin-top: 12px;">
                <center>
            <div class="col-sm-12" >
                <asp:LinkButton ID="btnUpdate"  runat="server" CssClass="btn-animated bg-green"
                    Visible="false" CausesValidation="false" OnClick="btnUpdate_Click" TabIndex="109"> <span class="glyphicon glyphicon-floppy-disk BtnGlyphicon"> Update</span> </asp:LinkButton>
                <asp:LinkButton ID="btnKYCUpdate"  runat="server" CssClass="btn-animated bg-green"
                    Visible="false" CausesValidation="false" OnClick="btnKYCUpdate_Click" TabIndex="109"> <span class="glyphicon glyphicon-floppy-disk BtnGlyphicon"> Update</span> </asp:LinkButton>
               
               
                <asp:LinkButton ID="btnPartialSave"  OnClick="btnPartialSave_Click"
                    CssClass="btn-animated bg-green" runat="server" TabIndex="109">
                   
                    <span class="glyphicon glyphicon-floppy-disk BtnGlyphicon"></span> Partial Save
                </asp:LinkButton>


                <asp:LinkButton ID="btnPartialUpdate"  OnClick="btnPartialUpdate_Click" Visible="false"
                    CssClass="btn-animated bg-green" runat="server" TabIndex="109">
                    <asp:HiddenField ID="HiddenField6" runat="server" />
                    <span class="glyphicon glyphicon-floppy-disk BtnGlyphicon"></span> Partial Update
                </asp:LinkButton>

                <asp:LinkButton ID="btnSave"  OnClick="btnSave_Click"
                    CssClass="btn-animated bg-green" runat="server" TabIndex="109"> <%--OnClientClick="return funCKYC();"--%>
                    <asp:HiddenField ID="TabName" runat="server" />
                    <span class="glyphicon glyphicon-floppy-disk BtnGlyphicon"></span> Save
                </asp:LinkButton>
                 <asp:LinkButton ID="btnCancel" OnClick="btnCancel_Click" CssClass="btn-animated bg-horrible"
                    runat="server" TabIndex="110">
                             <span class="glyphicon glyphicon-remove BtnGlyphicon"> </span> Cancel </asp:LinkButton>
                <div id="divloader" runat="server" style="display: none;">
                    <%--<img id="Img1" alt="" src="~/images/spinner.gif" runat="server" />--%>
                    <img id="Img1" alt="" src="Common/Images/spinner.gif" runat="server" />
                    Loading...
                </div>
            </div>
                </center>
            </div>

            <input id="hdnUpdate" type="hidden" runat="server" />
            <asp:HiddenField ID="hdnUserId" runat="server" />
            <asp:HiddenField ID="hdnmissingfield" runat="server" />
            <%--<div class="modal fade" id="myModal" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header" style="text-align: center; background-color: #dff0d8;">
                    <asp:Label ID="Label16" Text="INFORMATION" runat="server" Font-Bold="true"></asp:Label>
                </div>
                <div class="modal-body" style="text-align: center">
                    <asp:Label ID="lbl" Text="" runat="server"></asp:Label>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <button type="button" class="btn btn-primary" data-dismiss="modal">
                        <span class="glyphicon glyphicon-ok  BtnGlyphicon"></span> OK
                    </button>
                </div>
            </div>
        </div>
    </div>--%>
            <div class="modal" id="myModalRaise" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
                aria-hidden="true" style="padding-top: 0px;">
                <div class="modal-dialog" style="padding-top: 0px; margin-top: 2px; width: 95%;">
                    <div class="modal-content">
                        <div class="modal-header" style="margin-top: -10px; margin-bottom: -20px; padding-bottom: 6px ! important;">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title" id="myModalLabel">CKYC Related Person Details</h4>
                        </div>
                        <div class="modal-body">
                            <iframe id="iframeCFR" src="" width="100%" height="505" frameborder="0" allowtransparency="true"></iframe>
                        </div>
                        <div class="modal-footer">
                            <div style="text-align: center;">
                                <asp:LinkButton ID="lnkRaise" TabIndex="43" runat="server" CssClass="btn-animated bg-horrible"
                                    data-dismiss="modal" aria-hidden="true">
                                    <span class="glyphicon glyphicon-remove" style="color:White"> </span> Cancel
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
