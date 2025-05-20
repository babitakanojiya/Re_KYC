<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="IndividualRegistration.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.CKYC.IndividualRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/bundles/CKYCValidationjs") %>
    <%: Styles.Render("~/bundles/CKYCcss") %>

    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
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

        .form-control, .input-group-control, .panel, .panel-header, .panel-body {
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


        .input-group-control {
            display: table-cell;
            width: 15%;
        }

        .text-line {
            font: inherit;
            width: 101%;
            background-color: transparent;
            color: black;
            outline: none;
            outline-style: none;
            outline-offset: 0;
            border-top: none;
            border-left: none;
            border-right: none;
            border-bottom: solid #eeeeee 1px;
            padding: 9px 10px;
        }

        .no-event {
            pointer-events: none;
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

        .input-group-addon {
            cursor: pointer;
            color: #fff;
            background-color: #337ab7;
            border-color: #2e6da4;
            border: none;
            border-radius: 0;
        }
    </style>
    <script>
        $(document).ready(function () {
            SetDatepicker();
        })

        function SetDatepicker() {
            $(".datepicker").datepicker({
                dateFormat: "dd-mm-yy",
                changeYear: true,
                changeMonth: true
            });
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            SetDatepicker();
        });

        function OpenStateWindow(flag) {
            debugger;
            var state = "";
            var ddl = null;
            if (flag == '1') {
                ddl = document.getElementById('<%= ProofofIdentityandAddressStateUT.ClientID %>');
            }
            if (flag == '2') {
                ddl = document.getElementById('<%= CurrentAddressState.ClientID %>');
            }
            if (flag == '3') {
                ddl = document.getElementById('<%= AddressinJurisdictionState.ClientID %>');
            }

            if (ddl.selectedIndex == 0 || ddl.options.length == 0) {
                AlertMsg("Please select state.");
                return false;
            }
            state = ddl.options[ddl.selectedIndex].value;
            window.open("PinCodeDtls.aspx?StateCode=" + state + "&flag=" + flag + "", '', 'width=640,height=354,toolbar=no,scrollbars=yes,resizable=yes,left=300,top=230,location=0;');
            return false;
        }

        function BindDistrictPincode(flag, district_value, pincode_value) {
            if (flag == '1') {
                var district = document.getElementById('<%= ProofofIdentityandAddressDistrict.ClientID %>');
                var pincode = document.getElementById('<%= ProofofIdentityandAddressPINCode.ClientID %>');
                district.value = district_value;
                pincode.value = pincode_value;
            }
            if (flag == '2') {
                var district = document.getElementById('<%= CurrentAddressDistrict.ClientID %>');
                var pincode = document.getElementById('<%= CurrentAddressPINCode.ClientID %>');
                district.value = district_value;
                pincode.value = pincode_value;
            }
            if (flag == '3') {
                //var state = document.getElementById('<%= AddressinJurisdictionState.ClientID %>');
                var pincode = document.getElementById('<%= AddressinJurisdictionZIPPostCode.ClientID %>');
                pincode.value = pincode_value;
            }
        }

        function SetFullName(fullname, prefix, first, middle, last) {
            var firstname = document.getElementById(first).value;
            var middlename = document.getElementById(middle).value;
            var lastname = document.getElementById(last).value;
            var ddl = document.getElementById(prefix);
            var prefix_text = ''

            if (ddl.selectedIndex != 0)
                prefix_text = ddl.options[ddl.selectedIndex].text

            var data = [prefix_text, firstname.trim(), middlename.trim(), lastname.trim()]
            document.getElementById(fullname).value = data.join(' ');
        }

        function clearData(id) {
            document.getElementById(id).value = "";
        }
    </script>
    <asp:ScriptManager ID="scrusdtls" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="container-fluid">
                    <div class="row">
                        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                            <div id="Div18" runat="server" class="panel-heading" onclick="showHideDiv('divCKYCdtls','btnCKYCdtls');return false;">
                                <div class="row">
                                    <div class="col-sm-10" style="text-align: left">
                                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                                        <asp:Label ID="lblOfcuseOnly" Text="For System Use" runat="server" CssClass="control-label">
                                        </asp:Label>
                                    </div>
                                    <div class="col-sm-2" style="text-align: right">
                                        <asp:Label ID="Label3" runat="server" CssClass="control-label"></asp:Label>
                                        <span id="btnCKYCdtls" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-body" id="divCKYCdtls">
                                <div class="container-fluid">
                                    <div class="row custom-map">
                                        <div class="col-sm-3" runat="server" id="ParentDivApplicationType">
                                            <span class="control-label">Application Type
                                                <span style="color: red; vertical-align: baseline">*</span>
                                            </span>
                                            <div>
                                                <asp:CheckBoxList runat="server" ID="ApplicationType">
                                                    <asp:ListItem Text="New" Value="01" />
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivCKYCnoFIreferenceNo">
                                            <span class="control-label">FI Reference Number
                                            <span style="color: red; vertical-align: baseline">*</span>
                                            </span>
                                            <asp:TextBox runat="server" ID="CKYCnoFIreferenceNo" CssClass="form-control" />
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivConstitutionType">
                                            <span class="control-label">Constitution Type
                                            <span style="color: red; vertical-align: baseline">*</span>
                                            </span>
                                            <asp:DropDownList runat="server" ID="ConstitutionType" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivAccountType">
                                            <span class="control-label">Account Type
                                            <span style="color: red; vertical-align: baseline">*</span>
                                            </span>
                                            <asp:DropDownList runat="server" ID="AccountType" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                            <div id="Div2" runat="server" class="panel-heading" onclick="showHideDiv('divCKYCPersondtls','btnCKYCPersondtls');return false;">
                                <div class="row">
                                    <div class="col-sm-10" style="text-align: left">
                                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                                        <asp:Label ID="Label4" Text="Personal Details" runat="server" CssClass="control-label">
                                        </asp:Label>
                                    </div>
                                    <div class="col-sm-2" style="text-align: right">
                                        <asp:Label ID="Label5" runat="server" CssClass="control-label"></asp:Label>
                                        <span id="btnCKYCPersondtls" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-body" id="divCKYCPersondtls">
                                <div class="container-fluid">
                                    <div class="row custom-map">
                                        <div class="col-sm-12">
                                            <h4>Applicants Full Name</h4>
                                        </div>
                                    </div>
                                    <div class="row custom-map">
                                        <div class="col-sm-3" runat="server" id="ParentDivApplicantNamePrefix">
                                            <span class="control-label">Applicant Name Prefix    
                                                <span style="color: red; vertical-align: baseline">*</span>
                                            </span>
                                            <asp:DropDownList runat="server" ID="ApplicantNamePrefix" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivApplicantFirstName">
                                            <span class="control-label">Applicant First Name    
                                            <span style="color: red; vertical-align: baseline">*</span>   </span>
                                            <asp:TextBox runat="server" ID="ApplicantFirstName" onkeypress="fncInputcharacterOnly()" CssClass="form-control text-uppercase" />
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivApplicantMiddleName">
                                            <span class="control-label">Applicant  Middle Name   </span>
                                            <asp:TextBox runat="server" ID="ApplicantMiddleName" onkeypress="fncInputcharacterOnly()" CssClass="form-control text-uppercase" />
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivApplicantLastName">
                                            <span class="control-label">Applicant Last Name   </span>
                                            <asp:TextBox runat="server" ID="ApplicantLastName" onkeypress="fncInputcharacterOnly()" CssClass="form-control text-uppercase" />
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row custom-map">
                                        <div class="col-sm-8">
                                            <h4>Applicants Maiden Name</h4>
                                        </div>
                                        <div class="col-sm-4" runat="server" id="ParentDivApplicantMaidenFullName">
                                            <asp:TextBox runat="server" ID="ApplicantMaidenFullName" ClientIDMode="Static" placeholder="Applicant Maiden Full Name " CssClass="text-line text-uppercase no-event" />
                                        </div>
                                    </div>
                                    <div class="row custom-map">
                                        <div class="col-sm-3" runat="server" id="ParentDivApplicantMaidenNamePrefix">
                                            <span class="control-label">Applicant Maiden Name Prefix   </span>
                                            <asp:DropDownList runat="server" ID="ApplicantMaidenNamePrefix" ClientIDMode="Static"
                                                onblur="SetFullName('ApplicantMaidenFullName', 'ApplicantMaidenNamePrefix', 'ApplicantMaidenFirstName', 'ApplicantMaidenMiddleName', 'ApplicantMaidenLastName' )"
                                                CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivApplicantMaidenFirstName">
                                            <span class="control-label">Applicant Maiden First Name   </span>
                                            <asp:TextBox runat="server" ID="ApplicantMaidenFirstName" ClientIDMode="Static" onkeypress="fncInputcharacterOnly()"
                                                onblur="SetFullName('ApplicantMaidenFullName', 'ApplicantMaidenNamePrefix', 'ApplicantMaidenFirstName', 'ApplicantMaidenMiddleName', 'ApplicantMaidenLastName' )"
                                                CssClass="form-control text-uppercase" />
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivApplicantMaidenMiddleName">
                                            <span class="control-label">Applicant Maiden Middle Name   </span>
                                            <asp:TextBox runat="server" ID="ApplicantMaidenMiddleName" ClientIDMode="Static" onkeypress="fncInputcharacterOnly()"
                                                onblur="SetFullName('ApplicantMaidenFullName', 'ApplicantMaidenNamePrefix', 'ApplicantMaidenFirstName', 'ApplicantMaidenMiddleName', 'ApplicantMaidenLastName' )"
                                                CssClass="form-control text-uppercase" />
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivApplicantMaidenLastName">
                                            <span class="control-label">Applicant Maiden Last Name   </span>
                                            <asp:TextBox runat="server" ID="ApplicantMaidenLastName" ClientIDMode="Static" onkeypress="fncInputcharacterOnly()"
                                                onblur="SetFullName('ApplicantMaidenFullName', 'ApplicantMaidenNamePrefix', 'ApplicantMaidenFirstName', 'ApplicantMaidenMiddleName', 'ApplicantMaidenLastName' )"
                                                CssClass="form-control text-uppercase" />
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row custom-map">
                                        <div class="col-sm-3">
                                            <h4>Applicants Father/Spouse Name</h4>
                                        </div>
                                        <div class="col-sm-5">
                                            <div style="padding-top: 10px;" runat="server" id="ParentDivFlagindicatingFatherorSpouseName">
                                                <asp:RadioButtonList runat="server" ID="FlagindicatingFatherorSpouseName" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Father" Value="1" />
                                                    <asp:ListItem Text="Spouse" Value="2" />
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                        <div class="col-sm-4" runat="server" id="ParentDivFatherSpouseFullName">
                                            <%--<span class="control-label" id="FatherSpouseCompleteName">   </span>--%>
                                            <asp:TextBox runat="server" ID="FatherSpouseFullName" ClientIDMode="Static" placeholder="Father / Spouse Full Name" CssClass="text-line no-event text-uppercase" />
                                        </div>
                                    </div>
                                    <div class="row custom-map">
                                        <div class="col-sm-3" runat="server" id="ParentDivApplicantFatherSpouseNamePrefix">
                                            <span class="control-label" runat="server" id="FatherSpousePrefix">Father / Spouse Prefix   </span>
                                            <asp:DropDownList runat="server" ID="ApplicantFatherSpouseNamePrefix" ClientIDMode="Static"
                                                onblur="SetFullName('FatherSpouseFullName', 'ApplicantFatherSpouseNamePrefix', 'FatherSpouseFirstName', 'FatherSpouseMiddleName', 'FatherSpouseLastName' )"
                                                CssClass="form-control ">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivFatherSpouseFirstName">
                                            <span class="control-label" id="FatherSpouseFName">Father / Spouse First Name   </span>
                                            <asp:TextBox runat="server" ID="FatherSpouseFirstName" onkeypress="fncInputcharacterOnly()" ClientIDMode="Static"
                                                onblur="SetFullName('FatherSpouseFullName', 'ApplicantFatherSpouseNamePrefix', 'FatherSpouseFirstName', 'FatherSpouseMiddleName', 'FatherSpouseLastName' )"
                                                CssClass="form-control text-uppercase" />
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivFatherSpouseMiddleName">
                                            <span class="control-label" id="FatherSpouseMName">Father / Spouse Middle Name   </span>
                                            <asp:TextBox runat="server" ID="FatherSpouseMiddleName" onkeypress="fncInputcharacterOnly()" ClientIDMode="Static"
                                                onblur="SetFullName('FatherSpouseFullName', 'ApplicantFatherSpouseNamePrefix', 'FatherSpouseFirstName', 'FatherSpouseMiddleName', 'FatherSpouseLastName' )"
                                                CssClass="form-control text-uppercase" />
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivFatherSpouseLastName">
                                            <span class="control-label" id="FatherSpouseLName">Father / Spouse Last Name   </span>
                                            <asp:TextBox runat="server" ID="FatherSpouseLastName" onkeypress="fncInputcharacterOnly()" ClientIDMode="Static"
                                                onblur="SetFullName('FatherSpouseFullName', 'ApplicantFatherSpouseNamePrefix', 'FatherSpouseFirstName', 'FatherSpouseMiddleName', 'FatherSpouseLastName' )"
                                                CssClass="form-control text-uppercase" />
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row custom-map">
                                        <div class="col-sm-8">
                                            <h4>Applicants Mother Name</h4>
                                        </div>
                                        <div class="col-sm-4" runat="server" id="ParentDivMothersFullName">
                                            <%--<span class="control-label">Applicants Mother Full Name   </span>--%>
                                            <asp:TextBox runat="server" ID="MothersFullName" ClientIDMode="Static" placeholder="Mothers Full Name" CssClass="text-line no-event text-uppercase" />
                                        </div>
                                    </div>
                                    <div class="row custom-map">
                                        <div class="col-sm-3" runat="server" id="ParentDivApplicantMotherNamePrefix">
                                            <span class="control-label">Applicant Mother Name Prefix   </span>
                                            <asp:DropDownList runat="server" ID="ApplicantMotherNamePrefix" ClientIDMode="Static"
                                                onblur="SetFullName('MothersFullName', 'ApplicantMotherNamePrefix', 'MothersFirstName', 'MothersMiddleName', 'MothersLastName' )"
                                                CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivMothersFirstName">
                                            <span class="control-label">Mother's First Name   </span>
                                            <asp:TextBox runat="server" ID="MothersFirstName" onkeypress="fncInputcharacterOnly()" ClientIDMode="Static"
                                                onblur="SetFullName('MothersFullName', 'ApplicantMotherNamePrefix', 'MothersFirstName', 'MothersMiddleName', 'MothersLastName' )"
                                                CssClass="form-control text-uppercase" />
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivMothersMiddleName">
                                            <span class="control-label">Mother's Middle Name   </span>
                                            <asp:TextBox runat="server" ID="MothersMiddleName" onkeypress="fncInputcharacterOnly()" ClientIDMode="Static"
                                                onblur="SetFullName('MothersFullName', 'ApplicantMotherNamePrefix', 'MothersFirstName', 'MothersMiddleName', 'MothersLastName' )"
                                                CssClass="form-control text-uppercase" />
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivMothersLastName">
                                            <span class="control-label">Mother's Last Name   </span>
                                            <asp:TextBox runat="server" ID="MothersLastName" onkeypress="fncInputcharacterOnly()" ClientIDMode="Static"
                                                onblur="SetFullName('MothersFullName', 'ApplicantMotherNamePrefix', 'MothersFirstName', 'MothersMiddleName', 'MothersLastName' )"
                                                CssClass="form-control text-uppercase" />
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row custom-map">
                                        <div class="col-sm-3" runat="server" id="ParentDivGender">
                                            <span class="control-label">Gender</span>
                                            <asp:DropDownList runat="server" ID="Gender" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivMaritalstatus" style="display:none">
                                            <%--//Change By tushar --%>
                                            <span class="control-label">Marital status </span>
                                            <asp:DropDownList runat="server" ID="Maritalstatus" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivNationality" style="display:none">
                                            <%--//Change By tushar --%>
                                            <span class="control-label">Nationality   </span>
                                            <asp:DropDownList runat="server" ID="Nationality" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivOccupationType" style="display:none">
                                            <%--//Change By tushar --%>
                                            <span class="control-label">Occupation Types</span>
                                            <asp:DropDownList runat="server" ID="OccupationType" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row custom-map">
                                        <div class="col-sm-3" runat="server" id="ParentDivDateofBirthDateofIncorporation">
                                            <span class="control-label">Date of Birth <span style="color: red; vertical-align: baseline">*</span>   </span>

                                            <div class="input-group mb-3" runat="server" id="Div1">
                                                <asp:TextBox runat="server" ID="DateofBirthDateofIncorporation" ClientIDMode="Static" CssClass="form-control datepicker" />
                                                <div class="input-group-addon">
                                                    <span onclick="clearData('DateofBirthDateofIncorporation')">
                                                        <i class="glyphicon glyphicon-remove"></i>
                                                    </span>
                                                </div>
                                            </div>


                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivPANorform60">
                                            <span class="control-label">PAN or form 60    <span style="color: red; vertical-align: baseline">*</span>   </span>
                                            <asp:TextBox runat="server" ID="PANorform60" CssClass="form-control text-uppercase" />
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivResidentialStatus" style="display:none">
                                            <%--Change By tushar--%> 
                                            <span class="control-label">Residential Status   </span>
                                            <asp:DropDownList runat="server" ID="ResidentialStatus" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                            <div id="Div8" runat="server" class="panel-heading" onclick="showHideDiv('divCKYCPOIdtls','btnCKYCPOIdtls');return false;">
                                <div class="row">
                                    <div class="col-sm-10" style="text-align: left">
                                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                                        <asp:Label ID="Label1" Text="Proof Of Identity" runat="server" CssClass="control-label">
                                        </asp:Label>
                                    </div>
                                    <div class="col-sm-2" style="text-align: right">
                                        <asp:Label ID="Label2" runat="server" CssClass="control-label"></asp:Label>
                                        <span id="btnCKYCPOIdtls" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                                    </div>
                                </div>
                            </div>
                            <div id="divCKYCPOIdtls" class="panel-body">
                                <div class="container-fluid">
                                    <div class="row custom-map">
                                        <div class="col-sm-3" runat="server" id="Div10">
                                            <span class="control-label">
                                                <asp:Label Text="(Certified copy of any one the following Proof of Identity [Pol] needs to be submitted)" runat="server" ID="Label14" />
                                                <span style="color: red; vertical-align: baseline">*</span>   </span>
                                            <asp:DropDownList runat="server" ID="ddlPOI" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPOI_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3" runat="server" id="DivPOINo">
                                            <span class="control-label">
                                                <asp:Label Text="" runat="server" ID="lblPOINo" />
                                                <span style="color: red; vertical-align: baseline">*</span>   </span>
                                            <div style="visibility: hidden">hidden</div>

                                            <asp:TextBox runat="server" ID="txtPOINo" CssClass="form-control" />
                                        </div>
                                        <div class="col-sm-3" runat="server" id="DivPOIExpDate">
                                            <span class="control-label">
                                                <asp:Label Text="Expiry Date" runat="server" ID="lblPOIExpDate" />
                                            </span>
                                            <div style="visibility: hidden">hidden</div>
                                            <asp:TextBox runat="server" ID="txtPOIExpDate" CssClass="form-control datepicker" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                            <div id="Div3" runat="server" class="panel-heading" onclick="showHideDiv('divCKYCAddressdtls','btnCKYCAddressdtls');return false;">
                                <div class="row">
                                    <div class="col-sm-10" style="text-align: left">
                                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                                        <asp:Label ID="Label6" Text="Address Details" runat="server" CssClass="control-label">
                                        </asp:Label>
                                    </div>
                                    <div class="col-sm-2" style="text-align: right">
                                        <asp:Label ID="Label7" runat="server" CssClass="control-label"></asp:Label>
                                        <span id="btnCKYCAddressdtls" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-body" id="divCKYCAddressdtls">
                                <div class="container-fluid">
                                    <div class="row custom-map">
                                        <div class="col-sm-3" runat="server" id="ParentDivCurrentPermanentOverseasAddressType" style="display:none">
                                            <%--Change By tushar--%> 
                                            <span class="control-label">Current/Permanent/Overseas Address Type   </span>
                                            <asp:DropDownList runat="server" ID="CurrentPermanentOverseasAddressType" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row custom-map">
                                        <div class="col-sm-3" runat="server" id="ParentDivProofofIdentityandAddressLine1">
                                            <span class="control-label">Proof of Identity and Address  Line 1    <span style="color: red; vertical-align: baseline">*</span>   </span>
                                            <asp:TextBox runat="server" ID="ProofofIdentityandAddressLine1" CssClass="form-control" />
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivProofofIdentityandAddressLine2">
                                            <span class="control-label">Proof of Identity and Address  Line 2   </span>
                                            <asp:TextBox runat="server" ID="ProofofIdentityandAddressLine2" CssClass="form-control" />
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivProofofIdentityandAddressLine3">
                                            <span class="control-label">Proof of Identity and Address  Line 3   </span>
                                            <asp:TextBox runat="server" ID="ProofofIdentityandAddressLine3" CssClass="form-control" />
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivProofofIdentityandAddressCityTownVillage">
                                            <span class="control-label">City / Town / Village<span style="color: red; vertical-align: baseline">*</span>   </span>
                                            <asp:TextBox runat="server" ID="ProofofIdentityandAddressCityTownVillage" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <div class="row custom-map">
                                        <div class="col-sm-3" runat="server" id="ParentDivProofofIdentityandAddressCountry">
                                            <span class="control-label">Country    
                                                <span style="color: red; vertical-align: baseline">*</span>
                                            </span>
                                            <asp:DropDownList runat="server" ID="ProofofIdentityandAddressCountry" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ProofofIdentityandAddressCountry_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivProofofIdentityandAddressStateUT">
                                            <span class="control-label">State/U.T
                                                <span style="color: red; vertical-align: baseline">*</span>
                                            </span>
                                            <div class="input-group mb-3" runat="server" id="DivDdlProofofIdentityandAddressStateUT">
                                                <asp:DropDownList runat="server" ID="ProofofIdentityandAddressStateUT" CssClass="form-control"></asp:DropDownList>
                                                <div class="input-group-btn">
                                                    <asp:LinkButton runat="server" ID="POIPOASearchBtn" OnClientClick="OpenStateWindow('1')"
                                                        CssClass="btn btn-primary btn-lg-kmi" title="Search">
                                                        <span class="glyphicon glyphicon-search BtnGlyphicon"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div runat="server" id="DivTxtProofofIdentityandAddressStateUT">
                                                <asp:TextBox runat="server" ID="TxtProofofIdentityandAddressStateUT" CssClass="form-control" />
                                            </div>
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivProofofIdentityandAddressDistrict">
                                            <span class="control-label">District   </span>
                                            <asp:TextBox runat="server" ID="ProofofIdentityandAddressDistrict" CssClass="form-control" />
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivProofofIdentityandAddressPINCode">
                                            <span class="control-label">PIN Code   </span>
                                            <asp:TextBox runat="server" ID="ProofofIdentityandAddressPINCode" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <div class="row custom-map">
                                        <div class="col-sm-3" runat="server" id="ParentDivProofofAddresssubmittedforProofofIdentityandAddress">
                                            <span class="control-label">Proof of Address submitted for Proof of Identity and Address  <span style="color: red; vertical-align: baseline">*</span>   </span>
                                            <asp:DropDownList runat="server" ID="ProofofAddresssubmittedforProofofIdentityandAddress" AutoPostBack="true" OnSelectedIndexChanged="ProofofAddresssubmittedforProofofIdentityandAddress_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3" runat="server" id="DivPOAIdentNo">
                                            <span class="control-label">
                                                <asp:Label Text="" runat="server" ID="lblPOAIdentNo" />
                                                <span style="color: red; vertical-align: baseline">*</span>   </span>
                                            <div style="visibility: hidden">hidden</div>
                                            <asp:TextBox runat="server" ID="txtPOAIdentNo" CssClass="form-control" />
                                        </div>
                                        <div class="col-sm-3" runat="server" id="DivPOAIdentExpDate">
                                            <span class="control-label">
                                                <asp:Label runat="server" ID="lblPOAIdentExpDate"  >Expiry Date</asp:Label>
                                            </span>
                                            <div style="visibility: hidden">hidden</div>
                                            <asp:TextBox runat="server" ID="txtPOAIdentExpDate" CssClass="form-control datepicker" />
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivProofofAddresssubmittedforProofofIdentityandAddressOthers">
                                            <span class="control-label">Proof of Address submitted for Proof of Identity and Address (Others)   </span>
                                            <asp:TextBox runat="server" ID="ProofofAddresssubmittedforProofofIdentityandAddressOthers" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row custom-map">
                                        <div class="col-sm-12  custom-map" runat="server" id="ParentDivFlagindicatingifProofofIdentityandAddressissameascurrentAddress">
                                            <asp:CheckBox Text="Current Address is same as Proof of Address submitted for Proof of Identity and Address" runat="server" ID="FlagindicatingifProofofIdentityandAddressissameascurrentAddress" 
                                                OnCheckedChanged="FlagindicatingifProofofIdentityandAddressissameascurrentAddress_CheckedChanged" AutoPostBack="true" CssClass="text-uppercase" />
                                        </div>
                                    </div>
                                    <div class="row custom-map">
                                        <div class="col-sm-3" runat="server" id="ParentDivCurrentAddressLine1">
                                            <span class="control-label">Current Address Line 1   </span>
                                            <asp:TextBox runat="server" ID="CurrentAddressLine1" CssClass="form-control" />
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivCurrentAddressLine2">
                                            <span class="control-label">Current Address Line 2   </span>
                                            <asp:TextBox runat="server" ID="CurrentAddressLine2" CssClass="form-control" />
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivCurrentAddressLine3">
                                            <span class="control-label">Current Address Line 3   </span>
                                            <asp:TextBox runat="server" ID="CurrentAddressLine3" CssClass="form-control" />
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivCurrentAddressCityTownVillage">
                                            <span class="control-label">City / Town / Village    <span style="color: red; vertical-align: baseline">*</span>   </span>
                                            <asp:TextBox runat="server" ID="CurrentAddressCityTownVillage" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <div class="row custom-map">
                                        <div class="col-sm-3" runat="server" id="ParentDivCurrentAddressCountry">
                                            <span class="control-label">Country<span style="color: red; vertical-align: baseline">*</span>   </span>
                                            <asp:DropDownList runat="server" ID="CurrentAddressCountry" AutoPostBack="true" OnSelectedIndexChanged="CurrentAddressCountry_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivCurrentAddressState">
                                            <span class="control-label">State</span>
                                            <div class="input-group mb-3" runat="server" id="DivDdlCurrentAddressState">
                                                <asp:DropDownList runat="server" ID="CurrentAddressState" CssClass="form-control"></asp:DropDownList>
                                                <div class="input-group-btn">
                                                    <asp:LinkButton runat="server" ID="CurrentStateSearchBtn" OnClientClick="OpenStateWindow('2')"
                                                        CssClass="btn btn-primary btn-lg-kmi" title="Search">
                                                        <span class="glyphicon glyphicon-search BtnGlyphicon"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div runat="server" id="DivTxtCurrentAddressState">
                                                <asp:TextBox runat="server" ID="TxtCurrentAddressState" CssClass="form-control" />
                                            </div>
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivCurrentAddressDistrict">
                                            <span class="control-label">District   </span>
                                            <asp:TextBox runat="server" ID="CurrentAddressDistrict" CssClass="form-control" />
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivCurrentAddressPINCode">
                                            <span class="control-label">PIN Code   </span>
                                            <asp:TextBox runat="server" ID="CurrentAddressPINCode" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <div class="row custom-map">
                                        <div class="col-sm-3" runat="server" id="ParentDivProofofaddresssubmittedforCurrentAddress">
                                            <span class="control-label">Proof of address submitted for Current Address  </span>
                                            <asp:DropDownList runat="server" ID="ProofofaddresssubmittedforCurrentAddress" AutoPostBack="true" OnSelectedIndexChanged="ProofofaddresssubmittedforCurrentAddress_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3" runat="server" id="DivPOACurrent">
                                            <span class="control-label">
                                                <asp:Label Text="" runat="server" ID="lblPOACurrent" />
                                                <span style="color: red; vertical-align: baseline">*</span>   </span>
                                            <div style="visibility: hidden">hidden</div>

                                            <asp:TextBox runat="server" ID="txtPOACurrent" CssClass="form-control" />
                                        </div>
                                        <div class="col-sm-3" runat="server" id="DivPOACurrentExpDate">
                                            <span class="control-label">
                                                <asp:Label Text="Expiry Date" runat="server" ID="lblPOACurrentExpDate" />
                                            </span>
                                            <div style="visibility: hidden">hidden</div>

                                            <asp:TextBox runat="server" ID="txtPOACurrentExpDate" CssClass="form-control datepicker" />
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row custom-map" runat="server" id="ParentDivFlagindicatingifaddressinjurisdiction">
                                        <div class="col-sm-12">
                                            <asp:CheckBox Text="ADDRESS IN THE JURISDICTION DETAILS WHERE APPLICANT/ENTITY IS RESIDENT OUTSIDE INDIA FOR TAX PURPOSES" runat="server" ID="Flagindicatingifaddressinjurisdiction" />
                                        </div>
                                    </div>
                                    <div class="row custom-map">
                                        <div class="col-sm-3" runat="server" id="ParentDivAddressinJurisdictionLine1" style="display:none">
                                            <%--Change By tushar--%> 
                                            <span class="control-label">Address in Jurisdiction Line 1   </span>
                                            <asp:TextBox runat="server" ID="AddressinJurisdictionLine1" CssClass="form-control" />
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivAddressinJurisdictionLine2" style="display:none">
                                            <%--Change By tushar--%> 
                                            <span class="control-label">Address in Jurisdiction Line 2   </span>
                                            <asp:TextBox runat="server" ID="AddressinJurisdictionLine2" CssClass="form-control" />
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivAddressinJurisdictionLine3" style="display:none">
                                            <%--Change By tushar--%> 
                                            <span class="control-label">Address in Jurisdiction Line 3   </span>
                                            <asp:TextBox runat="server" ID="AddressinJurisdictionLine3" CssClass="form-control" />
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivAddressinJurisdictionCityTownVillage" style="display:none">
                                            <%--Change By tushar--%> 
                                            <span class="control-label">City / Town / Village   </span>
                                            <asp:TextBox runat="server" ID="AddressinJurisdictionCityTownVillage" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <div class="row custom-map">
                                        <div class="col-sm-3" runat="server" id="ParentDivAddressinJurisdictionCountry">
                                            <span class="control-label">Country</span>
                                            <asp:DropDownList runat="server" ID="AddressinJurisdictionCountry" AutoPostBack="true" OnSelectedIndexChanged="AddressinJurisdictionCountry_SelectedIndexChanged" 
                                                CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3">
                                            <span class="control-label" runat="server" id="ParentDivAddressinJurisdictionState">State</span>
                                            <div class="input-group mb-3" runat="server" id="DivDdlAddressinJurisdictionState">
                                                <asp:DropDownList runat="server" ID="AddressinJurisdictionState" CssClass="form-control"></asp:DropDownList>
                                                <div class="input-group-btn">
                                                    <asp:LinkButton runat="server" ID="JurisdictionStateSearchBtn" OnClientClick="OpenStateWindow('3')"
                                                        CssClass="btn btn-primary btn-lg-kmi" title="Search">
                                                        <span class="glyphicon glyphicon-search BtnGlyphicon"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div runat="server" id="DivTxtAddressinJurisdictionState">
                                                <asp:TextBox runat="server" ID="TxtAddressinJurisdictionState" CssClass="form-control" />
                                            </div>
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivAddressinJurisdictionZIPPostCode">
                                            <span class="control-label">ZIP/Post Code   </span>
                                            <asp:TextBox runat="server" ID="AddressinJurisdictionZIPPostCode" CssClass="form-control" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                            <div id="Div4" runat="server" class="panel-heading" onclick="showHideDiv('divCKYCContactdtls','btnCKYCContactdtls');return false;">
                                <div class="row">
                                    <div class="col-sm-10" style="text-align: left">
                                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                                        <asp:Label ID="Label8" Text="Contact Details" runat="server" CssClass="control-label">
                                        </asp:Label>
                                    </div>
                                    <div class="col-sm-2" style="text-align: right">
                                        <asp:Label ID="Label9" runat="server" CssClass="control-label"></asp:Label>
                                        <span id="btnCKYCContactdtls" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-body" id="divCKYCContactdtls">
                                <div class="container-fluid">
                                    <div class="row custom-map">
                                        <div class="col-sm-3" runat="server" id="ParentDivResidenceTelephoneNo">
                                            <span class="control-label">Residence Telephone No.   </span>
                                            <div class="input-group mb-3">
                                                <div class="input-group-control" runat="server" id="ParentDivResidenceTelephoneNoSTDCode">
                                                    <asp:TextBox runat="server" ID="ResidenceTelephoneNoSTDCode" CssClass="form-control" />
                                                </div>
                                                <asp:TextBox runat="server" ID="ResidenceTelephoneNo" CssClass="form-control" />
                                            </div>
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivOfficeTelephoneNo">
                                            <span class="control-label">Office Telephone No.   </span>
                                            <div class="input-group mb-3">
                                                <div class="input-group-control" runat="server" id="ParentDivOfficeTelephoneNoSTDCode">
                                                    <asp:TextBox runat="server" ID="OfficeTelephoneNoSTDCode" CssClass="form-control" />
                                                </div>
                                                <asp:TextBox runat="server" ID="OfficeTelephoneNo" CssClass="form-control" />
                                            </div>
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivMobileNo">
                                            <span class="control-label">Mobile No.   </span>
                                            <div class="input-group mb-3">
                                                <div class="input-group-control" runat="server" id="ParentDivMobileNoISDCode">
                                                    <asp:TextBox runat="server" ID="MobileNoISDCode" CssClass="form-control" />
                                                </div>
                                                <asp:TextBox runat="server" ID="MobileNo" CssClass="form-control" />
                                            </div>
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivEmailID">
                                            <span class="control-label">Email ID   </span>
                                            <asp:TextBox runat="server" ID="EmailID" CssClass="form-control" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                            <div id="Div5" runat="server" class="panel-heading" onclick="showHideDiv('divCKYCRemarksdtls','btnCKYCRemarksdtls');return false;">
                                <div class="row">
                                    <div class="col-sm-10" style="text-align: left">
                                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                                        <asp:Label ID="Label10" Text="Remarks" runat="server" CssClass="control-label">
                                        </asp:Label>
                                    </div>
                                    <div class="col-sm-2" style="text-align: right">
                                        <asp:Label ID="Label11" runat="server" CssClass="control-label"></asp:Label>
                                        <span id="btnCKYCRemarksdtls" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-body" id="divCKYCRemarksdtls">
                                <div class="container-fluid">
                                    <div class="row custom-map" runat="server" id="ParentDivRemarksifany">
                                        <div class="col-sm-12">
                                            <span class="control-label">Remarks </span>
                                            <asp:TextBox TextMode="MultiLine" Rows="2" runat="server" ID="Remarksifany" CssClass="form-control" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                            <div id="Div6" runat="server" class="panel-heading" onclick="showHideDiv('divCKYC_KYCdtls','btnCKYC_KYCdtls');return false;">
                                <div class="row">
                                    <div class="col-sm-10" style="text-align: left">
                                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                                        <asp:Label ID="Label12" Text="KYC Verification" runat="server" CssClass="control-label">
                                        </asp:Label>
                                    </div>
                                    <div class="col-sm-2" style="text-align: right">
                                        <asp:Label ID="Label13" runat="server" CssClass="control-label"></asp:Label>
                                        <span id="btnCKYC_KYCdtls" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-body" id="divCKYC_KYCdtls">
                                <div class="container-fluid">
                                    <div class="row custom-map">
                                        <div class="col-sm-3" runat="server" id="ParentDivTypeofDocumentSubmitted">
                                            <span class="control-label">Type of Document Submitted     <span style="color: red; vertical-align: baseline">*</span>   </span>
                                            <asp:DropDownList runat="server" ID="TypeofDocumentSubmitted" CssClass="form-control"></asp:DropDownList>
                                        </div>

                                        <div class="col-sm-3" runat="server" id="ParentDivDateofDeclaration">
                                            <span class="control-label">Date of Declaration  <span style="color: red; vertical-align: baseline">*</span>
                                            </span>
                                            <div class="input-group mb-3" runat="server">
                                                <asp:TextBox runat="server" ID="DateofDeclaration" ClientIDMode="Static" CssClass="form-control datepicker" />
                                                <div class="input-group-addon">
                                                    <span onclick="clearData('DateofDeclaration')">
                                                        <i class="glyphicon glyphicon-remove"></i>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivPlaceofDeclaration">
                                            <span class="control-label">Place of Declaration    
                                        <span style="color: red; vertical-align: baseline">*</span>   </span>
                                            <asp:TextBox runat="server" ID="PlaceofDeclaration" CssClass="form-control" />
                                        </div>

                                    </div>
                                    <div class="row" style="padding: 15px;">
                                        <div class="col-sm-12">
                                            <center>
                                                <span style="letter-spacing: 3px;">-----------------------------------------------------</span>
                                                <label class="text-bold">KYC VERIFICATION CARRIED OUT BY</label>
                                                <span style="letter-spacing: 3px;">-----------------------------------------------------</span>
                                            </center>
                                        </div>
                                    </div>
                                    <div class="row custom-map">
                                        <div class="col-sm-3" runat="server" id="ParentDivKYCVerificationName">
                                            <span class="control-label">KYC Verification Name    
                                            <span style="color: red; vertical-align: baseline">*</span>   </span>
                                            <asp:TextBox runat="server" ID="KYCVerificationName" CssClass="form-control" />
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivKYCVerificationEMPcode">
                                            <span class="control-label">KYC Verification EMP code    
                                            <span style="color: red; vertical-align: baseline">*</span>   </span>
                                            <asp:TextBox runat="server" ID="KYCVerificationEMPcode" CssClass="form-control" />
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivKYCVerificationDesignation">
                                            <span class="control-label">KYC Verification Designation    
                                            <span style="color: red; vertical-align: baseline">*</span>   </span>
                                            <asp:TextBox runat="server" ID="KYCVerificationDesignation" CssClass="form-control" />
                                        </div>
                                        <div class="col-sm-3" runat="server" id="ParentDivKYCVerificationBranch">
                                            <span class="control-label">KYC Verification Branch    
                                            <span style="color: red; vertical-align: baseline">*</span>   </span>
                                            <asp:TextBox runat="server" ID="KYCVerificationBranch" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <div class="row custom-map">
                                        <div class="col-sm-3" runat="server" id="ParentDivKYCVerificationDate">
                                            <span class="control-label">KYC Verification Date <span style="color: red; vertical-align: baseline">*</span>   </span>
                                            <div class="input-group mb-3" runat="server">
                                                <asp:TextBox runat="server" ID="KYCVerificationDate" ClientIDMode="Static" CssClass="form-control datepicker" />
                                                <div class="input-group-addon">
                                                    <span onclick="clearData('DateofDeclaration')">
                                                        <i class="glyphicon glyphicon-remove"></i>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="padding: 15px;">
                                        <div class="col-sm-12">
                                            <center>
                                                <span style="letter-spacing: 3px;">-----------------------------------------------------</span>
                                                <label class="text-bold">INSTITUTION DETAILS</label>
                                                <span style="letter-spacing: 3px;">-----------------------------------------------------</span>
                                            </center>
                                        </div>
                                    </div>
                                    <div class="row custom-map">
                                        <div class="col-sm-8" runat="server" id="ParentDivOrganisationName">
                                            <span class="control-label">Organisation Name    
                                            <span style="color: red; vertical-align: baseline">*</span>   </span>
                                            <asp:TextBox runat="server" ID="OrganisationName" CssClass="form-control" />
                                        </div>
                                        <div class="col-sm-4" runat="server" id="ParentDivOrganisationCode">
                                            <span class="control-label">Organisation Code    <span style="color: red; vertical-align: baseline">*</span>   </span>
                                            <asp:TextBox runat="server" ID="OrganisationCode" CssClass="form-control" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <center>
                            <asp:Button Text="Save" runat="server" CssClass="btn btn-primary" ID="btnSave" OnClick="btnSave_Click" />
                            <asp:Button Text="Cancel" runat="server" CssClass="btn btn-danger" ID="btnCancel" OnClick="btnCancel_Click" />
                        </center>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
