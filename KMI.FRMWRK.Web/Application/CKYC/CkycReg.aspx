<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="CkycReg.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.CKYC.CkycReg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/bundles/CKYCValidationjs") %>
    <%: Styles.Render("~/bundles/CKYCcss") %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">
    <style type="text/css">
        .spinner-border {
            color: #007bff !important;
            display: inline-block;
            width: 2rem;
            height: 2rem;
            vertical-align: text-bottom;
            border: .25em solid currentColor;
            border-right-color: transparent;
            border-radius: 50%;
            -webkit-animation: spinner-border .75s linear infinite;
            animation: spinner-border .75s linear infinite;
            margin-top: 5px;
            margin-left: 5px;
        }

        @keyframes spinner-border {
            to {
                transform: rotate(360deg);
            }
        }

        .spinner-border-sm {
            width: $spinner-width-sm;
            height: $spinner-height-sm;
            border-width: $spinner-border-width-sm;
        }

        @keyframes spinner-grow {
            0% {
                transform: scale(0);
            }

            50% {
                opacity: 1;
                transform: none;
            }
        }

        .spinner-grow {
            display: inline-block;
            width: $spinner-width;
            height: $spinner-height;
            vertical-align: text-bottom;
            background-color: currentColor;
            border-radius: 50%;
            opacity: 0;
            animation: spinner-grow .75s linear infinite;
        }

        .spinner-grow-sm {
            width: $spinner-width-sm;
            height: $spinner-height-sm;
        }
    </style>
    <script type="text/javascript">
        <%-- setInterval(function () {
            document.getElementById("<%= btnReloadRelPersCount.ClientID %>").click();
        }, 3000);--%>
        function AutoBindRelPersGV() {
            document.getElementById("<%= btnReloadRelPersCount.ClientID %>").click();
        }

        function AddLoader(id) {
            var div = document.getElementById(id);
            $(div).addClass('spinner-border');
            //setTimeout(function () {
            //    $(div).removeClass('spinner-border')
            //}, 4000);
        }
        function RemoveLoader(id) {
            var div = document.getElementById(id);
            // $(div).addClass('spinner-border');
            //setTimeout(function () {
            $(div).removeClass('spinner-border');
            //}, 4000);
        }
    </script>
    <style>
       .center{
           text-align: center!important;
       }
        .pointer {
            cursor: pointer;
        }

        .panel-success3 {
            border-color: transparent !important;
        }

        .container {
            width: 1300px !important;
        }

        .gridViewHeader {
            background-color: #0066CC;
            color: #FFFFFF;
            padding: 4px 50px 4px 4px;
            text-align: left;
            border-width: 0px;
            border-collapse: collapse;
        }

            .gridViewHeader > th {
                padding-left: 9px;
            }

        .standardcheckbox {
            padding-right: 9px !important;
        }

        .chkClass label {
            margin-left: 3px !important;
        }

        input[type=checkbox], input[type=radio] {
            margin-right: 3px !important;
        }

            input[type=checkbox] + label, input[type=radio] + label {
                vertical-align: middle !important;
                margin-right: 3px !important;
            }

        .ImgPOI {
            background-repeat: no-repeat;
            background-image: url(../../assets/images/View_Identify_Master_sm.png);
        }

        .ImgPOA {
            background-repeat: no-repeat;
            background-image: url(../../assets/images/View_Address_Master_sm.png);
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
    <script type="text/javascript">
        $(document).ready(function () {
            debugger;
            ShowDivProgressBar('Loading..Please wait..');
        });
        function ShowDivProgressBar(Msg) {
            debugger;
            var Msg = Msg

            document.getElementById('divProgressBar').style.display = "block";
            document.getElementById('EmptyPagePlaceholder_Label13').innerHTML = Msg;
            setTimeout(function () { HideDivProgressBar(); }, 5000);
        }

        function HideDivProgressBar() {
            debugger;

            document.getElementById('divProgressBar').style.display = "none";

        }
    </script>
    <script type="text/javascript">

        //$(window).on("load", function () {
        //    ShowProgressBar('Loading..Please wait..');
        //});

        //$(document).ready(ShowProgressBar('Loading..Please wait..'));
        $(document).ready(function () {
            debugger;
            //ShowProgressBar('Loading..Please wait..');
        });
         <%-- Added By Megha Bhave 25.03.2021 --%>
        //function ShowProgressBar(Msg) {
        //    debugger;
        //    var Msg = Msg
        //    document.getElementById('dvProgressBar').style.display = "block";
        //    document.getElementById('EmptyPagePlaceholder_lblMsg').innerHTML = Msg;
        //    setTimeout(function () { HideProgressBar(); }, 3000);
        //}

        //function HideProgressBar() {
        //    debugger;
        //    document.getElementById('dvProgressBar').style.display = "none";
        //}

        //Added by tushar For Master,s Data Show
        function OpenIdentityCodeMasterPage() {
            debugger;
            //var modal = document.getElementById('myModalRaise_Master');
            //var modaliframe = document.getElementById("iframeCFR_Master");
            ////modaliframe.src = "../../Application/CKYC/CommunicationLog_NEW.aspx?refno=" + refno;
            //ShowProgressBar('Searching..Please wait')
            LoadingPagInPopUp("IdentityCodeMaster.aspx?CustType=02&Status=reg&CrS=" + "CF", "IDENTITY CODE MASTER");
            // window.open("IdentityCodeMaster.aspx?Status=reg&CrS=" + "CF", 'popupViewSr', 'width=800,height=530,toolbar=no,scrollbars=yes,resizable=yes,left=50,top=10,location=0');
            //$('#myModalRaise_Master').modal();
        }

        function OpenProofofAddressMaster() {
            debugger;
            //ShowProgressBar ('Searching..Please wait')
            //var modal = document.getElementById('myModalRaise_Master');
            //var modaliframe = document.getElementById("iframeCFR_Master");
            //modaliframe.src = "../../Application/CKYC/CommunicationLog_NEW.aspx?refno=" + refno;
            LoadingPagInPopUp("ProofofAddressMaster.aspx?CustType=02&Status=reg&CrS=" + "CF", "PROOF OF ADDRESS MASTER");
            // window.open("ProofofAddressMaster.aspx?Status=reg&CrS=" + "CF", 'popupViewSr', 'width=800,height=530,toolbar=no,scrollbars=yes,resizable=yes,left=50,top=10,location=0');
            //$('#myModalRaise_Master').modal();
        }
        //Added by tushar For Master,s Data Show

        //^[a-zA-Z][a-zA-Z '-]*$
        function ToChkUnchkChkPOIDocument(id) {//new
            debugger;
            var chk0 = document.getElementById("EmptyPagePlaceholder_GridView1_ChkPOIDocument_0");
            var chk1 = document.getElementById("EmptyPagePlaceholder_GridView1_ChkPOIDocument_1");
            var chk2 = document.getElementById("EmptyPagePlaceholder_GridView1_ChkPOIDocument_2");
            var chk3 = document.getElementById("EmptyPagePlaceholder_GridView1_ChkPOIDocument_3");
            var chk4 = document.getElementById("EmptyPagePlaceholder_GridView1_ChkPOIDocument_4");
            var chk5 = document.getElementById("EmptyPagePlaceholder_GridView1_ChkPOIDocument_5");
            var chk6 = document.getElementById("EmptyPagePlaceholder_GridView1_ChkPOIDocument_6");
            var chkid = document.getElementById(id).value;

            if (id == chk0.id) {
                if (chkid.checked != true) {
                    chkid.checked = true;
                    chk1.checked = false;
                    chk2.checked = false;
                    chk3.checked = false;
                    chk4.checked = false;
                    chk5.checked = false;
                    chk6.checked = false;
                }
            }
            else
                if (id == chk1.id) {
                    if (chkid.checked != true) {
                        chkid.checked = true;
                        chk0.checked = false;
                        chk2.checked = false;
                        chk3.checked = false;
                        chk4.checked = false;
                        chk5.checked = false;
                        chk6.checked = false;
                    }
                }
                else
                    if (id == chk2.id) {
                        if (chkid.checked != true) {
                            chkid.checked = true;
                            chk0.checked = false;
                            chk1.checked = false;
                            chk3.checked = false;
                            chk4.checked = false;
                            chk5.checked = false;
                            chk6.checked = false;
                        }
                    }
                    else
                        if (id == chk3.id) {
                            if (chkid.checked != true) {
                                chkid.checked = true;
                                chk0.checked = false;
                                chk1.checked = false;
                                chk2.checked = false;
                                chk4.checked = false;
                                chk5.checked = false;
                                chk6.checked = false;
                            }
                        }
                        else
                            if (id == chk4.id) {
                                if (chkid.checked != true) {
                                    chkid.checked = true;
                                    chk0.checked = false;
                                    chk2.checked = false;
                                    chk3.checked = false;
                                    chk1.checked = false;
                                    chk5.checked = false;
                                    chk6.checked = false;
                                }
                            }
                            else
                                if (id == chk5.id) {
                                    if (chkid.checked != true) {
                                        chkid.checked = true;
                                        chk0.checked = false;
                                        chk1.checked = false;
                                        chk2.checked = false;
                                        chk3.checked = false;
                                        chk4.checked = false;
                                        chk6.checked = false;
                                    }
                                }
                                else
                                    if (id == chk6.id) {
                                        if (chkid.checked != true) {
                                            chkid.checked = true;
                                            chk0.checked = false;
                                            chk2.checked = false;
                                            chk3.checked = false;
                                            chk4.checked = false;
                                            chk5.checked = false;
                                            chk1.checked = false;
                                        }
                                    }
                                    else { }
        }

        function callCalender() {
            debugger;
            var dateArr = $("#<%=txtDOB.ClientID%>").val().split('-');
            $("#<%= txtDOB.ClientID%>").datepicker({ maxDate: new Date(), changeMonth: true, changeYear: true, dateFormat: 'dd-mm-yy' });
            $.datepicker.initialized = true;
            $("#<%= txtDOB.ClientID%>").focus();

        }
        
        function callCalender(id) {
            debugger;
            if (id == "txtDateKYCver") {
                var dateArr = $("#<%=txtDateKYCver.ClientID%>").val().split('-');
                $("#<%= txtDateKYCver.ClientID%>").datepicker({ maxDate: new Date(), changeMonth: true, changeYear: true, dateFormat: 'dd-mm-yy' });
                $.datepicker.initialized = true;
                $("#<%= txtDateKYCver.ClientID%>").focus();
             }
             if (id == "txtDate") {
                 var dateArr = $("#<%=txtDate.ClientID%>").val().split('-');
                 $("#<%= txtDate.ClientID%>").datepicker({ maxDate: new Date(), changeMonth: true, changeYear: true, dateFormat: 'dd-mm-yy' });
                 $.datepicker.initialized = true;
                 $("#<%= txtDate.ClientID%>").focus();
             }

         }
        function ValidateDOB(date) {
            debugger;
            var dateObj = date.split('-');
            if (!getYearDiff(new Date(dateObj[2], dateObj[1] - 1, dateObj[0]))) {
                popup("DOB should not be less then 18 years");
                document.getElementById("<%= txtDOB.ClientID %>").value = "";
            }
        }

        function getYearDiff(startDate) {
            debugger;
            var endDate = new Date();
            var yearDiff = endDate.getFullYear() - startDate.getFullYear();
            if (startDate.getMonth() > endDate.getMonth()) {
                yearDiff--;
            } else if (startDate.getMonth() === endDate.getMonth()) {
                if (startDate.getDate() > endDate.getDate()) {
                    yearDiff--;
                } else if (startDate.getDate() === endDate.getDate()) {
                    if (startDate.getHours() > endDate.getHours()) {
                        yearDiff--;
                    } else if (startDate.getHours() === endDate.getHours()) {
                        if (startDate.getMinutes() > endDate.getMinutes()) {
                            yearDiff--;
                        }
                    }
                }
            }
            if (yearDiff >= 18) {
                return true;
            }
            else {
                return false;
            }
        }

        function fnValidateNumber(id, No) {
            debugger;
            var Mobile1 = document.getElementById(id).value;
            if (Mobile1 != "") {

                if (parseInt(Mobile1.length) != parseInt(No)) {
                    //AlertMsg("Number should be " + No + " digit");
                    AlertMsg("Number at least " + No + " digit long");
                    document.getElementById(id).value = "";
                    document.getElementById(id).focus();
                    return false;
                }
            }
        }

        function lettersOnly() {
            var charCode = event.keyCode;
            if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8 || charCode == 32 || charCode == 39)
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
                popup("Please enter valid PAN Number.");
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
                popup("Please enter valid email address");
            }
        }

        function validateMobileNumber(obj) {
            debugger;
            if ($("#<%=txtMobile.ClientID%>").val().trim() == "91") {
                var startWith = ["6", "7", "8", "9"]
                var first = obj.value.split('');

                if (obj.value == "") return;
                if (startWith.indexOf(first[0]) == -1 || obj.value.length != 10) {
                    obj.value = "";
                    popup("Mobile number should start with  6, 7, 8, 9 and 10 digit long");
                }
            }
            else if ($("#<%=txtMobile.ClientID%>").val().trim() != "91") {
                //if (obj.value.length != ) {
                //    obj.value = "";
                //    popup("Please Enter Valid Mobile Number");
                //}
            }
        }


        function popup(msg) {
            showModal('#myModal', 'Alert', 'alert-warning', '', '', msg);
        }

        function OpenRelatedPersonPopUpPage(FiRefNo, FlagPageTyp) {
            debugger;
            var modal = document.getElementById('myModalRaise');
            var modaliframe = document.getElementById("iframeCFR");
            modaliframe.src = "CKYCRelatedPrsn.aspx?Status=reg&mdl=myModalRaise&FiRefNo=" + FiRefNo + "&FlagPageTyp=" + FlagPageTyp;
            $('#myModalRaise').modal();
            //HideProgressBar();
        }

        function OpenRelatedPersonPopUpPageEdit(refno, RelRefnNo) {
            debugger;
            var modal = document.getElementById('myModalRaise');
            var modaliframe = document.getElementById("iframeCFR");
            modaliframe.src = "CKYCRelatedPrsn.aspx?Status=reg&refno=" + refno + "&relrefno=" + RelRefnNo;
            $('#myModalRaise').modal();
            //HideProgressBar();
        }
        function OpenRelatedPersonPage(FiRefNo) {
            debugger;
            var modal = document.getElementById('myModalRaise');
            var modaliframe = document.getElementById("iframeCFR");
            modaliframe.src = "CKYCRelatedPrsn.aspx?Status=reg&mdl=myModalRaise&FiRefNo=" + FiRefNo;
            $('#myModalRaise').modal();
        }

        function OpenRelatedPersonPageView(RelRefnNo, refno) {
            debugger;
            var modal = document.getElementById('myModalRaise');
            var modaliframe = document.getElementById("iframeCFR");

            modaliframe.src = "CKYCRelatedPrsn.aspx?Status=Mod&refno=" + refno + "&relrefno=" + RelRefnNo;
            $('#myModalRaise').modal('show');
        }

        function OpenRelatedPersonPageEdit(RelRefnNo, refno, PageTyp, Row) {
            debugger;
            var modal = document.getElementById('myModalRaise');
            var modaliframe = document.getElementById("iframeCFR");

            modaliframe.src = "CKYCRelatedPrsn.aspx?Status=Mod&refno=" + refno + "&relrefno=" + RelRefnNo + "&FlagPageTyp=" + PageTyp + "&RowNo=" + Row;
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
    <script type="text/javascript">
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
            //debugger;
            var getParam = "{'id':'" + Code + "'}";
            //var strDesc = 'testt'
            //if (strDesc != "N") {
            //    document.getElementById(idTo).innerHTML = strDesc;
            //}

            $.ajax({
                type: "POST",
                url: "CKYCReg.aspx/getHeaderbyIDMethod",
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
            //var BatchDtls = JSON.parse(data.d);
            document.getElementById(Code).innerHTML = data.d;
        }
        //var x = document.getElementById(id).innerHTML;
        //document.getElementById(idTo).innerHTML = "Note: Please use the following section to capture the customer's recent contact details to improve contactibility. The system would auto generate a endorsement request to update the contact details for the customer.";

    </script>
    <script type="text/javascript">
        //Added By Shubham

        function fncInputNumericValuesWithHyphenOnly() {
            if (!(event.keyCode == 45 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                event.returnValue = false;
            }
        }
        function LoadingPagInPopUp(URL, Headertxt) {
            debugger;
            if (URL != "") {
                var modal = document.getElementById('myModalRaiseMst');
                document.getElementById("myModalLabelMst").textContent = Headertxt;
                var modaliframe = document.getElementById("iframeCFRMst");
                modaliframe.src = URL;
                $('#myModalRaiseMst').modal();
            }
        }
        function tinvalidation(Obj) {
            //Function added by daksh-- Validation
            debugger;
            if (Obj == null) Obj = window.event.srcElement;
            if (Obj.value != "") {
                ObjVal = Obj.value;
                var value = document.getElementById("<%=ddlCountrOfInc.ClientID%>");
                var getvalue = value.options[value.selectedIndex].value;
                // alert(getvalue);
                var panPat = /^(?:\d{3}-\d{2}-\d{4}|\d{3}-\d{3}-\d{4}|\d{2}[A-Z]{5}\d{4}[A-Z]{1}[A-Z\d]{1}[Z]{1}[A-Z\d]{1})$/;
                var tin = /^\d{ 3}-\d{ 2}-\d{ 4}$/;



                if (panPat.test(Obj.value) || tin.test(Obj.value) && getvalue == "IN") {
                    if (Obj.value != "") {
                        //document.getElementById("spntincnt").style.display = "block";
                        //  document.getElementById("<%= dvTINCntry.ClientID %>").style.display = "block";
                    }
                    else {
                        //document.getElementById("spntincnt").style.display = "none";
                        //   document.getElementById("<%= dvTINCntry.ClientID %>").style.display = "none";
                    }
                   return true;
               }
               else {
                   //document.getElementById("spntincnt").style.display = "none";
                   //  document.getElementById("<%= dvTINCntry.ClientID %>").style.display = "none";
                   AlertMsg("Please enter Valid TIN/GST Registration Number");
                   Obj.value = "";
                   Obj.focus();
                   return false;
               }
           }
            <%--else {
                //document.getElementById("spntincnt").style.display = "none";
                document.getElementById("<%= dvTINCntry.ClientID %>").style.display = "none";
            }--%>
        }

        function fncInputcharacterOnlyNew() {
            if (!(event.keyCode == 32 || event.keyCode == 65 || event.keyCode == 66 || event.keyCode == 67 || event.keyCode == 68 || event.keyCode == 69 || event.keyCode == 70 || event.keyCode == 71 || event.keyCode == 72 || event.keyCode == 73 || event.keyCode == 74 || event.keyCode == 75 || event.keyCode == 76 || event.keyCode == 77 || event.keyCode == 78 || event.keyCode == 79 || event.keyCode == 80 || event.keyCode == 81 || event.keyCode == 82 || event.keyCode == 83 || event.keyCode == 84 || event.keyCode == 85 || event.keyCode == 86 || event.keyCode == 87 || event.keyCode == 88 || event.keyCode == 89 || event.keyCode == 90 || event.keyCode == 97 || event.keyCode == 98 || event.keyCode == 99 || event.keyCode == 100 || event.keyCode == 101 || event.keyCode == 102 || event.keyCode == 103 || event.keyCode == 104 || event.keyCode == 105 || event.keyCode == 106 || event.keyCode == 107 || event.keyCode == 108 || event.keyCode == 109 || event.keyCode == 110 || event.keyCode == 111 || event.keyCode == 112 || event.keyCode == 113 || event.keyCode == 114 || event.keyCode == 115 || event.keyCode == 116 || event.keyCode == 117 || event.keyCode == 118 || event.keyCode == 119 || event.keyCode == 120 || event.keyCode == 121 || event.keyCode == 122)) {
                event.returnValue = false;
                //e.preventDefault();
            }
        }
        var previousCommDate = '';
        function checkDateOfCommencement() {
            debugger;
            //if ($.datepicker.initialized) {
            $.datepicker.initialized = false;
            if ($("#<%=txtDatOfInc.ClientID%>").val().trim() == '') {
                AlertMsg("Please select Date of Incorporation.");
                $("#<%= txtDtOfCom.ClientID%>").val('');
                $("#<%=txtDatOfInc.ClientID%>").focus();
                return;
            }

            if (previousCommDate != $("#<%=txtDatOfInc.ClientID%>").val().trim()) {
                previousCommDate = $("#<%=txtDatOfInc.ClientID%>").val().trim();
                $("#<%= txtDtOfCom.ClientID%>").val('');
            }
            $("#<%= txtDtOfCom.ClientID%>").focus();
            var dateArr = $("#<%=txtDatOfInc.ClientID%>").val().split('-');
            $("#<%= txtDtOfCom.ClientID%>").datepicker({ maxDate: "-1d", minDate: new Date(dateArr[2], dateArr[1] - 1, dateArr[0]), changeMonth: true, changeYear: true, dateFormat: 'dd-mm-yy' });
            $.datepicker.initialized = true;
            $("#<%= txtDtOfCom.ClientID%>").focus();
        }
        function callCalender1() {
            debugger;
            var dateArr = $("#<%=txtDatOfInc.ClientID%>").val().split('-');
            $("#<%= txtDatOfInc.ClientID%>").datepicker({ maxDate: "-1d", changeMonth: true, changeYear: true, dateFormat: 'dd-mm-yy' });
            $.datepicker.initialized = true;
            $("#<%= txtDatOfInc.ClientID%>").focus();

        }
        //Ended By Shubham

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

            <div id="divProgressBar" style="display: none; text-align: center" class="loader">
                <center>
                         <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /> <br /><br /><br /><br /><br />
                          <asp:Image id="Image1" src="../../Images/horizonal_loader.gif"   height="50px" alt="" runat="server" ImageAlign="Middle"/>
                         <br />
                      <asp:Label ID="Label13" Text="" runat="server" ForeColor="Blue" style="font-size: medium; font-weight:bold" > </asp:Label>
                
            </center>
            </div>
            <div class="container" style="margin-top: 0px;">
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
                                <span id="ReqDtlsInfoIcon" class="glyphicon glyphicon-question-sign" onmouseover="NotificationDivSlide('DivSrcReqDtlsNote','ReqDtlsInfoIcon');
                                    getHeaderbyID('SrcReqDtlsNote');"
                                    style="float: left; padding: 1px 10px ! important; font-size: 18px; color: red; margin-left: 95px;"></span>
                                <span id="btnCKYCdtls" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>

                        </div>
                    </div>
                    <div style="background-color: #d5d5c3; display: none;" id="DivSrcReqDtlsNote">
                        <div class="row">
                            <div class="col-sm-12" style="margin-left: 17px; margin-top: 10px;">
                                <span class="glyphicon glyphicon-alert" style="color: yellow"></span>&#160;&#160;
          <span id="SrcReqDtlsNote" style="text-align: justify; font-size: 11px;"></span>
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
                                <asp:CheckBox ID="cbNew" runat="server" CssClass="standardcheckbox" AutoPostBack="true" Visible="false"
                                    Enabled="false" TabIndex="20" />
                                <asp:Label ID="cbNewtxt" runat="server" Text="New" CssClass="standardcheckbox" Style="padding-left: 3%;" Visible="false"></asp:Label>
                                <asp:CheckBox ID="cbUpdate" runat="server" CssClass="standardcheckbox" Text="Update"
                                    AutoPostBack="true" Visible="false" TabIndex="1" />
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblRefNumber" Text="" runat="server" Font-Bold="false"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:TextBox ID="txtRefNumber" runat="server" CssClass="form-control" OnTextChanged="txtRefNumber_TextChanged" AutoPostBack="true"
                                    Font-Bold="false" onChange="javascript:this.value=this.value.toUpperCase();" TabIndex="2" MaxLength="14">
                                </asp:TextBox>
                                <span id="spnValidRefNo" runat="server" style="display: none; color: green !important; padding-left: 1% !important;"><u>Valid FI Reference Number </u></span>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblAccountType" Text="" runat="server" Font-Bold="false">
                                </asp:Label>
                                <span id="lblAccountTypeImp" runat="server" style="color: red">*</span>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <%--<asp:CheckBox ID="chkNormal" runat="server" CssClass="standardcheckbox chkClass" Text="Normal"
                                    AutoPostBack="true" TabIndex="3" name="cb1" value="value1" />
                                <asp:CheckBox ID="chkSimplified" runat="server" CssClass="standardcheckbox chkClass" Text="Simplified"
                                    AutoPostBack="true" TabIndex="3" name="cb2" value="value1" />
                                <asp:CheckBox ID="Chksmall" runat="server" CssClass="standardcheckbox chkClass" Text="Small"
                                    AutoPostBack="true" TabIndex="5"
                                    name="cb3" value="value1" />--%>
                                <%--Added by tushar for--%>

                                <asp:DropDownList ID="ddlAccountType" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="2" ClientIDMode="Static" OnSelectedIndexChanged="ddlAccountType_SelectedIndexChanged">
                                </asp:DropDownList>
                                <%--Added by tushar for--%>
                            </div>

                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="Label2" Text="Constitution Type" runat="server" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:DropDownList ID="ConstitutionType" runat="server" CssClass="form-control" TabIndex="2" ClientIDMode="Static">
                                </asp:DropDownList>
                            </div>

                            <div class="col-sm-3" style="text-align: left; display: none;">
                                <asp:Label ID="lblKYCNumber" Text="" runat="server" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-sm-3" style="text-align: left; display: none;">
                                <asp:TextBox ID="txtKYCNumber" runat="server" onkeypress="funIsAlphaNumericWithoutSpace();" CssClass="form-control" Enabled="false"
                                    Font-Bold="false" TabIndex="2">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="row" style="margin-bottom: 5px">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblNatureOfBuss" Text="Constitution Type " runat="server" Font-Bold="false" Visible="false">
                                </asp:Label>
                                <span id="lblNatureOfBussImp" runat="server" style="color: red">*</span>
                            </div>
                            <div class="col-sm-3" style="text-align: left; display: flex;">
                                <asp:DropDownList ID="ddlNatureOfBuss" AutoPostBack="true" Visible="false" onChange="javascript:AddLoader('ddlNatureOfBussLoader');"
                                    OnSelectedIndexChanged="ddlNatureOfBuss_SelectedIndexChanged" runat="server" CssClass="form-control" TabIndex="2">
                                </asp:DropDownList>
                                <div id="ddlNatureOfBussLoader"></div>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="Label5" Text="Constitution Type others" runat="server" Font-Bold="false">
                                </asp:Label>
                                <span id="Label5Imp" runat="server" style="color: red">*</span>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:TextBox ID="txtConstitutionTypeothers" runat="server" MaxLength="20" CssClass="form-control" Font-Bold="false" Enabled="false"
                                    TabIndex="2" />
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
                                <span id="ReqDtlsInfoIcon1" class="glyphicon glyphicon-question-sign" onmouseover="NotificationDivSlide('DivSrcReqDtlsNote1','ReqDtlsInfoIcon1'); 
                                    getHeaderbyID('SrcReqDtlsNote1');"
                                    style="float: left; padding: 1px 10px ! important; font-size: 18px; color: red; margin-left: 95px;"></span>
                                <span id="Span8" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>

                    </div>
                    <div style="background-color: #d5d5c3; display: none;" id="DivSrcReqDtlsNote1">
                        <div class="row">
                            <div class="col-sm-12" style="margin-left: 17px; margin-top: 10px;">
                                <span class="glyphicon glyphicon-alert" style="color: yellow"></span>&#160;&#160;
                                <span id="SrcReqDtlsNote1" style="text-align: justify; font-size: 11px;"></span>
                            </div>
                        </div>
                    </div>
                    <div id="menu1" style="display: block;" class="panel-body">
                        <%--  Added for Personal Details start --%>
                        <%-- <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">--%>
                        <%--<div id="Div2" runat="server" class="panel-heading subheader"
                                onclick="showHideDiv('divPersonal','btnpersnl');return false;">
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
                            </div>--%>
                        <div id="divPersonal" runat="server" style="display: block; padding: 0px" class="form-group panel-body">
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
                                        <asp:DropDownList ID="cboTitle" AutoPostBack="true" runat="server" CssClass="form-control" DataTextField="ParamDesc"
                                            DataValueField="ParamValue" AppendDataBoundItems="True" TabIndex="2" ClientIDMode="Static" onchange="CheckGenderPrefix('prefix')" OnSelectedIndexChanged="cboTitle_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <%-- </ContentTemplate>
                                            </asp:UpdatePanel>--%>
                                    </div>
                                    <div class="col-sm-10" style="padding: 0">
                                        <div class="col-sm-4" style="padding-left: 0">
                                            <asp:TextBox ID="txtGivenName" runat="server" CssClass="form-control" onkeypress="return lettersOnly();" onchange="javascript:this.value=this.value.toUpperCase();"
                                                MaxLength="50" TabIndex="2" placeholder="First Name">
                                            </asp:TextBox>
                                        </div>
                                        <div class="col-sm-4" style="padding-left: 0">
                                            <asp:TextBox ID="txtMiddleName" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="return lettersOnly();"
                                                MaxLength="50" TabIndex="2" onblur="CheckSpaces();return false;" placeholder="Middle Name">
                                            </asp:TextBox>
                                        </div>
                                        <div class="col-sm-4" style="padding-left: 0">
                                            <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="return lettersOnly()"
                                                MaxLength="50" TabIndex="2" onblur="CheckSpaces();return false;" placeholder="Last Name">
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
                                            DataValueField="ParamValue" AppendDataBoundItems="True" TabIndex="2" Enabled="false">
                                        </asp:DropDownList>
                                        <%-- </ContentTemplate>
                                            </asp:UpdatePanel>--%>
                                    </div>
                                    <div class="col-sm-10" style="padding: 0">
                                        <div class="col-sm-4" style="padding-left: 0">
                                            <asp:TextBox ID="txtGivenName1" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="return lettersOnly()"
                                                MaxLength="50" TabIndex="2" onblur="CheckSpaces();return false;" placeholder="First Name" Enabled="false">
                                            </asp:TextBox>
                                        </div>
                                        <div class="col-sm-4" style="padding-left: 0">
                                            <asp:TextBox ID="txtMiddleName1" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="return lettersOnly()"
                                                MaxLength="50" TabIndex="2" onblur="CheckSpaces();return false;" placeholder="Middle Name" Enabled="false">
                                            </asp:TextBox>
                                        </div>
                                        <div class="col-sm-4" style="padding-left: 0">
                                            <asp:TextBox ID="txtLastName1" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="return lettersOnly()"
                                                MaxLength="50" TabIndex="2" onblur="CheckSpaces();return false;" placeholder="Last Name" Enabled="false">
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
                                    <div class="col-sm-6" style="padding: 0">
                                        <%--   <asp:UpdatePanel ID="UpdFSFlag" runat="server">
                                                <ContentTemplate>--%>
                                        <asp:RadioButtonList ID="rbtFS" runat="server" CssClass="radiobtn" ClientIDMode="Static" RepeatDirection="Horizontal"
                                            Visible="true" TabIndex="2" AutoPostBack="false">
                                            <asp:ListItem Value="F">Father</asp:ListItem>
                                            <asp:ListItem Value="S">Spouse</asp:ListItem>
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
                                            DataValueField="ParamValue" AppendDataBoundItems="True" TabIndex="2" AutoPostBack="false" ClientIDMode="Static">
                                        </asp:DropDownList>
                                        <%--</ContentTemplate>
                                                </asp:UpdatePanel>--%>
                                    </div>
                                    <div class="col-sm-10" style="padding: 0">
                                        <div class="col-sm-4" style="padding-left: 0">
                                            <asp:TextBox ID="txtGivenName2" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="return lettersOnly()"
                                                MaxLength="50" TabIndex="2" onblur="CheckSpaces();return false;" placeholder="First Name">
                                            </asp:TextBox>
                                        </div>
                                        <div class="col-sm-4" style="padding-left: 0">
                                            <asp:TextBox ID="txtMiddleName2" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="return lettersOnly()"
                                                MaxLength="50" TabIndex="2" onblur="CheckSpaces();return false;" placeholder="Middle Name">
                                            </asp:TextBox>
                                        </div>
                                        <div class="col-sm-4" style="padding-left: 0">
                                            <asp:TextBox ID="txtLastName2" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="return lettersOnly()"
                                                MaxLength="50" TabIndex="2" onblur="CheckSpaces();return false;" placeholder="Last Name">
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
                                    <%--<span style="color: red">*</span>--%>
                                </div>
                                <div class="col-sm-9" style="padding: 0">
                                    <div class="col-sm-2">
                                        <asp:DropDownList ID="cboTitle3" runat="server" CssClass="form-control" DataTextField="ParamDesc"
                                            DataValueField="ParamValue" AppendDataBoundItems="True" TabIndex="2">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-10" style="padding: 0">
                                        <div class="col-sm-4" style="padding-left: 0">
                                            <asp:TextBox ID="txtGivenName3" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="return lettersOnly()"
                                                MaxLength="50" TabIndex="2" onblur="CheckSpaces();return false;" placeholder="First Name">
                                            </asp:TextBox>
                                        </div>
                                        <div class="col-sm-4" style="padding-left: 0">
                                            <asp:TextBox ID="txtMiddleName3" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="return lettersOnly()"
                                                MaxLength="50" TabIndex="2" onblur="CheckSpaces();return false;" placeholder="Middle Name">
                                            </asp:TextBox>
                                        </div>
                                        <div class="col-sm-4" style="padding-left: 0">
                                            <asp:TextBox ID="txtLastName3" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="return lettersOnly()"
                                                MaxLength="50" TabIndex="2" onblur="CheckSpaces();return false;" placeholder="Last Name">
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
                                    <asp:Label ID="lbldob" Text="DOB (dd-mm-yyyy)" runat="server" CssClass="control-label">
                                    </asp:Label>
                                    <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-3">
                                    <div class="input-group">
                                        <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control" placeholder="dd-mm-yyyy" onchange="ValidateDOB(this.value);" MaxLength="10" TabIndex="2"></asp:TextBox>
                                        <div class="input-group-btn">
                                            <div class="btn btn-primary btn-lg-kmi" onclick="callCalender()">
                                                <span class="glyphicon glyphicon-calendar BtnGlyphicon"></span>
                                            </div>
                                        </div>
                                    </div>
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
                                    <asp:DropDownList ID="cboGender" runat="server" CssClass="form-control" TabIndex="2" ClientIDMode="Static" onchange="CheckGenderPrefix('gender')">
                                    </asp:DropDownList>
                                    <%-- </ContentTemplate>
                                        </asp:UpdatePanel>--%>
                                </div>
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
                                    <asp:TextBox runat="server" ID="txtPanNo" CssClass="form-control" OnTextChanged="txtPanNo_TextChanged"
                                        ClientIDMode="Static" AutoPostBack="true" onblur="return validatePAN(this)" onkeyup="javascript: this.value = this.value.toUpperCase()" TabIndex="2" />
                                </div>
                                <div class="col-sm-3">
                                    <asp:CheckBox ID="chkPanForm" Text="Form 60 furnished" OnCheckedChanged="chkPanForm_CheckedChanged" Enabled="true"
                                        AutoPostBack="true" runat="server" CssClass="standardcheckbox" TabIndex="2" />
                                    <%--<span style="color: red">*</span>--%>
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
                                        AutoPostBack="true" TabIndex="2" Visible="false">
                                    </asp:DropDownList>
                                    <%--</ContentTemplate>
                                </asp:UpdatePanel>--%>
                                </div>
                            </div>
                        </div>
                        <%--Added By Shubham--%>
                        <div id="divDetailOfEntity" runat="server" class="panel-body">
                            <div class="row" style="margin-bottom: 8px">
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblKYCName" Text="Entity Name" runat="server" Font-Bold="false"></asp:Label>
                                    <span id="lblKYCNameImp" runat="server" style="color: red">*</span>
                                </div>
                                <div class="col-sm-3" style="text-align: left; top: 2px; left: 0px;">
                                    <asp:TextBox ID="txtKYCName" runat="server" MaxLength="150" CssClass="form-control" Font-Bold="false"
                                        TabIndex="2">
                                    </asp:TextBox>
                                </div>

                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblDatOfInc" Text="Date of Incorporation" runat="server" Font-Bold="false"></asp:Label>
                                    <span id="lblDatOfIncImp" runat="server" style="color: red">*</span>
                                </div>
                                <div class="col-sm-3">
                                    <div class="input-group">
                                        <asp:TextBox ID="txtDatOfInc" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtDatOfInc_TextChanged" MaxLength="10" TabIndex="2"></asp:TextBox>
                                        <div class="input-group-btn">
                                            <div class="btn btn-primary btn-lg-kmi" onclick="callCalender1()">
                                                <span class="glyphicon glyphicon-calendar BtnGlyphicon"></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="margin-bottom: 5px">
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblDateOfCom" Text="Date of Commencement of Business" runat="server" Font-Bold="false"></asp:Label>
                                    <span id="lblDateOfComImp" style="color: red">*</span>
                                </div>
                                <div class="col-sm-3">
                                    <div class="input-group">
                                        <asp:TextBox ID="txtDtOfCom" runat="server" CssClass="form-control" MaxLength="10" TabIndex="2"></asp:TextBox>
                                        <div class="input-group-btn">
                                            <div class="btn btn-primary btn-lg-kmi" onclick="checkDateOfCommencement()">
                                                <span class="glyphicon glyphicon-calendar BtnGlyphicon"></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3" style="text-align: left; display: flex;">
                                    <asp:Label ID="lblPlaceOfIncor" Text="Place of Incorporation" runat="server" Font-Bold="false"></asp:Label>
                                    <span id="lblPlaceOfIncorImp" style="color: red">*</span>
                                </div>
                                <div class="col-sm-3" style="text-align: left; top: 2px; left: 0px; margin-top: -3px">
                                    <asp:TextBox ID="txtPlaceOfInc" runat="server" CssClass="form-control" Font-Bold="false" MaxLength="150" onkeypress="fncInputcharacterOnlyNew();"
                                        TabIndex="2">
                                    </asp:TextBox>
                                </div>
                            </div>
                            <div class="row" style="margin-bottom: 5px">
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblCountrOfInc" Text="Country of Incorporation" runat="server" Font-Bold="false"></asp:Label>
                                    <span id="lblCountrOfIncImp" style="color: red">*</span>
                                </div>
                                <div class="col-sm-3" style="text-align: left; top: 0px; left: 0px; display: flex">
                                    <asp:DropDownList ID="ddlCountrOfInc" runat="server" CssClass="form-control" Font-Bold="false" OnSelectedIndexChanged="ddlCountrOfInc_SelectedIndexChanged"
                                        onChange="AddLoader('ddlCountrOfIncLoader');" AutoPostBack="true" TabIndex="2">
                                        <%--<asp:ListItem Value="" Text="Select">onChange="javascript:this.value=AddLoader('ddlCountrOfIncLoader');"</asp:ListItem>--%>
                                    </asp:DropDownList>
                                    <div id="ddlCountrOfIncLoader"></div>
                                </div>
                            </div>
                            <div class="row" style="margin-bottom: 5px">
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblPanNo" Text="PAN " runat="server" Font-Bold="false"></asp:Label>
                                    <span id="lblPanNoImp" runat="server" style="color: red">*</span>
                                </div>
                                <div class="col-sm-3" style="text-align: left; top: 0px; left: 0px; display: flex;">
                                    <asp:TextBox ID="txtPanNoLegal" runat="server" AutoPostBack="true" OnTextChanged="txtPanNoLegal_TextChanged" MaxLength="10" CssClass="form-control"
                                        onChange="javascript:this.value=this.value.toUpperCase();AddLoader('txtPanNoLegalLoader');"
                                         Font-Bold="false"
                                        TabIndex="2"> <%--onblur="validatePAN(this)"--%>
                                    </asp:TextBox>
                                    <div id="txtPanNoLegalLoader"></div>
                                </div>
                                <div class="col-sm-3">
                                    <asp:CheckBox ID="chkPanFormLegal" Text="FORM 60" OnCheckedChanged="chkPanFormLegal_CheckedChanged" Enabled="true"
                                        AutoPostBack="true" runat="server" CssClass="standardcheckbox" TabIndex="2" />
                                </div>
                            </div>
                            <div class="row" style="margin-bottom: 5px">
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblTypeIdentiNo" Text="TIN/GST Registration number" runat="server" Font-Bold="false"></asp:Label>
                                </div>
                                <div class="col-sm-3" style="text-align: left; top: 0px; left: 0px;">
                                    <asp:TextBox ID="txtTypeIdentiNo" runat="server" MaxLength="20" CssClass="form-control" Font-Bold="false"
                                        AutoPostBack="true" OnTextChanged="txtTypeIdentiNo_TextChanged" TabIndex="2"> 
                                    </asp:TextBox>
                                </div>
                                <div id="dvTINCntry" runat="server" style="display: none;">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <div class="col-sm-3" style="text-align: left; display: flex;">
                                                <asp:Label ID="lblTINCountry" Text="TIN Issuing Country" runat="server" Font-Bold="false"></asp:Label>&nbsp;
                               
                                                <span id="spntincnt" style="color: red;">*</span>
                                            </div>
                                            <div class="col-sm-3" style="text-align: left; top: 0px; left: 0px;">
                                                <asp:DropDownList ID="ddlTINCountry" runat="server" CssClass="form-control" Font-Bold="false"
                                                    TabIndex="2" Enabled="false">
                                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <%--Ended By Shubham--%>
                        <%--</div>--%>
                        <%--  Added for Personal Details end --%>
                        <%--  Added for Tick If Applicable start --%>
                        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px; display: none">
                            <div id="Div1" runat="server" class="panel-heading subheader" style="background-color: #EDF1cc !important"
                                onclick="showHideDiv('div3','Span1');return false;">
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
                                            TabIndex="2" />
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
                                            TabIndex="2" />
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
                                            TabIndex="2" />
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
                                            TabIndex="2">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--  Added for Tick If Applicable end --%>
                    </div>
                </div>


                <%-- Added for Proof of Identity Start--%>
                <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                    <div id="Div4" runat="server" class="panel-heading">
                        <div class="row">
                            <div class="col-sm-10" style="text-align: left">
                                <span class="glyphicon glyphicon-menu-hamburger"></span>
                                <asp:CheckBox ID="ChkUpdID" runat="server" CssClass="standardcheckbox" Text="" AutoPostBack="true"
                                    TabIndex="2" OnCheckedChanged="ChkUpdID_Checked" />
                                <asp:Label ID="lblProofOfIdentity11" Text="PROOF OF IDENTITY" runat="server"
                                    CssClass="control-label"></asp:Label>
                            </div>
                            <div class="col-sm-2" onclick="showHideDiv('menu2','btnProofIdentity');return false;">
                                <span id="ReqDtlsInfoIcon2" class="glyphicon glyphicon-question-sign" onmouseover="NotificationDivSlide('DivSrcReqDtlsNote2','ReqDtlsInfoIcon2'); 
                                    getHeaderbyID('SrcReqDtlsNote2');"
                                    style="float: left; padding: 1px 10px ! important; font-size: 18px; color: red; margin-left: 95px;"></span>
                                <span id="btnProofIdentity" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div>
                    <div style="background-color: #d5d5c3; display: none;" id="DivSrcReqDtlsNote2">
                        <div class="row">
                            <div class="col-sm-12" style="margin-left: 17px; margin-top: 10px;">
                                <span class="glyphicon glyphicon-alert" style="color: yellow"></span>&#160;&#160;
                                <span id="SrcReqDtlsNote2" style="text-align: justify; font-size: 11px;"></span>
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
                            <div class="col-sm-3" style="display: flex;">
                                <asp:DropDownList ID="ddlProofIdentity" runat="server" CssClass="form-control" AutoPostBack="true" onChange="javascript:AddLoader('ddlProofIdentityLoader');"
                                    OnSelectedIndexChanged="ddlProofIdentity_SelectedIndexChanged" TabIndex="2">
                                </asp:DropDownList>
                                <div id="ddlProofIdentityLoader"></div>
                            </div>
                            <div id="div28" class="col-sm-3" style="text-align: left" runat="server">
                                <asp:Label ID="Label9" runat="server" Visible="false" Text="View Identity Master" ForeColor="#99ccff" CssClass="pointer"
                                    FontBold="true" onclick="OpenIdentityCodeMasterPage();"></asp:Label>
                                <img src="../../assets/images/View_Identity_Master_sm.png" class="pointer" onclick="OpenIdentityCodeMasterPage();" />
                            </div>
                            <div id="div2" runat="server" class="col-sm-3" style="padding-left: 158px;" visible="false">
                                <asp:LinkButton ID="btnAddPOI" OnClick="btnAddPOI_Click"
                                    CssClass="btn-animated bg-green" runat="server" TabIndex="2">
                                    <span style="padding-top:7px">Add New</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div id="divIdProof" runat="server" class="row">
                            <div id="divPassNo" runat="server" class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblPassportNo" runat="server" CssClass="control-label" Text="Document Name"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div id="divPassNotxt" runat="server" class="col-sm-2">
                                <div class="input-group">
                                    <span id="MaskCodeSpan" runat="server" class="input-group-addon input-group-addon-tel">
                                        <asp:TextBox ID="txtMaskCode" runat="server" CssClass="form-control" Text="X X X X X X X X" Enabled="false" TabIndex="2" MaxLength="8" Style="border-top-left-radius: 4px; border-bottom-left-radius: 4px;"></asp:TextBox>
                                    </span>
                                    <asp:TextBox CssClass="form-control" runat="server" onChange="javascript:this.value=this.value.toUpperCase();"
                                        ID="txtPassNo" MaxLength="20" TabIndex="2" onkeypress="funIsAlphaNumeric()" AutoPostBack="true" OnTextChanged="txtPassNo_TextChanged" />
                                </div>
                            </div>
                            <div id="divPass" runat="server" class="col-sm-3" style="text-align: left">
                                <asp:Label ID="llPassExpDate" runat="server" CssClass="control-label"></asp:Label>
                                <%--<span style="color: red">*</span>--%>
                            </div>
                            <%-- <div class="col-sm-4" style="float: right; padding-top: 10px;">
                                <span id="lblGv1Note" style="color: red" visible="false"
                                    runat="server" cssclass="control-label">(Note:Please Select One of the document)</span>
                            </div>--%>
                            <div id="divPassDate" runat="server">
                                <asp:TextBox CssClass="form-control" onmousedown="$('#EmptyPagePlaceholder_txtPassExpDate').datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' });"
                                    runat="server"
                                    ID="txtPassExpDate" MaxLength="15" TabIndex="2" />
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtPassOthr" MaxLength="15"
                                    TabIndex="2" />
                            </div>

                        </div>
                        <div id="div13" runat="server" class="row">
                            <div id="div20" runat="server" class="col-sm-9">
                            </div>
                            <%--Tushar multiple Doc--%>
                            <div class="container" style="padding-right: 46px;">
                                <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="footable" OnDataBinding="GridView1_DataBinding">
                                    <%--<AlternatingRowStyle BackColor="White" />--%>
                                    <EditRowStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" ForeColor="White" />
                                    <HeaderStyle BackColor="#00c5cc" ForeColor="White" Height="35px" CssClass="gridViewHeader" />
                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#E3EAEB" />
                                    <Columns>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="ChkPOIDocumentTxt" runat="server" Visible="false"></asp:Label>
                                                <asp:CheckBox ID="ChkPOIDocument" runat="server" onclick="ToChkUnchkChkPOIDocument(this.id)" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="false" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                                </asp:GridView>
                                <%--Tushar multiple Doc--%>
                            </div>
                        </div>
                    </div>
                </div>
                <%-- Added for Proof of Identity End--%>

                <%--  Added for Proof of Address start--%>
                <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                    <div id="DivP" runat="server" class="panel-heading subheader">
                        <div class="row">
                            <div class="col-sm-10" style="text-align: left">
                                <span class="glyphicon glyphicon-menu-hamburger"></span>
                                <asp:Label ID="lblpfofAddr2" Text="ADDRESS" runat="server" CssClass="control-label"></asp:Label>
                            </div>
                            <%--<div class="col-sm-2" onclick="showHideDisv('divk','Span2');return false;">
                                <span id="Span2" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>--%>

                            <div class="col-sm-2" onclick="showHideDisv('divk','Span2');return false;">
                                <span id="ReqDtlsInfoIcon9" class="glyphicon glyphicon-question-sign" onmouseover="NotificationDivSlide('DivSrcReqDtlsNote9','ReqDtlsInfoIcon9'); 
                                    getHeaderbyID('SrcReqDtlsNote9');"
                                    style="float: left; padding: 1px 10px ! important; font-size: 18px; color: red; margin-left: 95px;"></span>
                                <span id="Span2" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div>

                    <div style="background-color: #d5d5c3; display: none;" id="DivSrcReqDtlsNote9">
                        <div class="row">
                            <div class="col-sm-12" style="margin-left: 17px; margin-top: 10px;">
                                <span class="glyphicon glyphicon-alert" style="color: yellow"></span>&#160;&#160;
                                <span id="SrcReqDtlsNote9" style="text-align: justify; font-size: 11px;"></span>
                            </div>
                        </div>
                    </div>

                    <div id="divk" style="display: block;" class="panel-body">
                        <div class="row">
                            <div class="col-sm-12" style="text-align: left">
                                <asp:Label ID="Label12" Text="Registered Office Address / Place of Business" runat="server" CssClass="control-label" Style="font-weight: 700;"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblProofOfAddress" Text="Proof Of Address" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3" style="display: flex;">
                                <asp:DropDownList ID="ddlProofOfAddress" runat="server" OnSelectedIndexChanged="ddlProofOfAddress_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true"
                                    onChange="javascript:AddLoader('ddlProofOfAddressLoader');" TabIndex="42">
                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                </asp:DropDownList>
                                <div id="ddlProofOfAddressLoader"></div>
                            </div>
                            <div id="divMster" class="col-sm-3" style="text-align: left" runat="server">
                                <asp:Label ID="lnkProofofAddressMaster" Visible="false" runat="server" Text="View Address Master" ForeColor="#99ccff" CssClass="pointer"
                                    FontBold="true" onclick="OpenProofofAddressMaster();"></asp:Label>
                                <img src="../../assets/images/View_Address_Master_sm.png" class="pointer" onclick="OpenProofofAddressMaster();" />
                            </div>
                        </div>
                        <div id="divAddProof" visible="false" runat="server" class="row">
                            <div id="divPassNoAdd" runat="server" visible="false" class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblPassportNoAdd" Text="Document Name" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div id="divPassNotxtAdd" runat="server" visible="false" class="col-sm-3">
                                <asp:TextBox CssClass="form-control" runat="server" onChange="javascript:this.value=this.value.toUpperCase();"
                                    ID="txtPassNoAdd" MaxLength="75" TabIndex="43" />
                            </div>
                            <div id="divPassAdd" runat="server" visible="false" class="col-sm-3" style="text-align: left">
                                <asp:Label ID="llPassExpDateAdd" Text="Identification Number" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div id="divPassDateAdd" runat="server" visible="false" class="col-sm-3">
                                <asp:TextBox CssClass="form-control" onmousedown="$('#EmptyPagePlaceholder_txtPassExpDateAdd').datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' });"
                                    runat="server" ID="txtPassExpDateAdd" MaxLength="15" TabIndex="44" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblAddressLine1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox CssClass="form-control" runat="server" Enabled="false"
                                    ID="txtAddressLine1" MaxLength="55" TabIndex="2" />
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblAddressLine2" Text="" runat="server" CssClass="control-label"></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox CssClass="form-control" Enabled="false"
                                    runat="server" ID="txtAddressLine2" MaxLength="55" TabIndex="2" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblAddressLine3" Text="" runat="server" CssClass="control-label"></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtDrivLicNum')" runat="server" Enabled="false"
                                    ID="txtAddressLine3" MaxLength="55" TabIndex="2" />
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblCity" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtCity" runat="server" Enabled="false" CssClass="form-control" TabIndex="2" MaxLength="50">
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
                                    TabIndex="2">
                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="txtddlDistrict" Visible="false" runat="server" CssClass="form-control" TabIndex="2" ></asp:TextBox>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblPinCode" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3" style="display: flex">
                                <asp:DropDownList ID="ddlPinCode" runat="server" Enabled="false" CssClass="form-control" OnSelectedIndexChanged="ddlPinCode_SelectedIndexChanged"
                                    onChange="javascript:AddLoader('ddlPinCodeLoader');" AutoPostBack="True" TabIndex="2">
                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                </asp:DropDownList>
                                <div id="ddlPinCodeLoader"></div>
                                <asp:TextBox ID="txtddlPinCode" Visible="false" runat="server" CssClass="form-control" TabIndex="2" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblState" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" Enabled="false"
                                    TabIndex="2">
                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="txtddlState" Visible="false" runat="server" Enabled="false" Text="XX" CssClass="form-control" TabIndex="2"></asp:TextBox>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblIsoCountryCode" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddlCountryCode" runat="server" Enabled="false" CssClass="form-control" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlCountryCode_SelectedIndexChanged" TabIndex="2">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div style="margin-top: 25px; margin-bottom: 25px">
                            <div class="row">
                                <div class="col-sm-12" style="text-align: left">
                                    <%--  <asp:CheckBox ID="chkLocalAddress" Text="CORRESPONDENCE/LOCAL ADDRESS DETAILS" runat="server"
                                        OnCheckedChanged="chkLocalAddress_CheckedChanged" AutoPostBack="true" CssClass="control-label" TabIndex="54"  />
                                    <span style="color: red">*</span>--%>
                                    <asp:Label ID="Label4" Text="Local Address In India" runat="server" CssClass="control-label" Style="font-weight: 700;"></asp:Label>
                                    <span style="color: red">*</span>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12" style="text-align: left">
                                    <asp:CheckBox ID="chkCuurentAddress" Text="Same as above mentioned address " onChange="javascript:this.value=AddLoader('chkCuurentAddressLoader');"
                                        OnCheckedChanged="chkCuurentAddress_Checked" AutoPostBack="true" runat="server" Enabled="true"
                                        CssClass="control-label" TabIndex="2" />
                                    <div id="chkCuurentAddressLoader"></div>
                                </div>
                            </div>
                            <div class="row" style="display: none;">
                                <div class="col-sm-3" style="text-align: left">
                                    <%--<asp:Label ID="Label5" Text="Proof of Address" runat="server" CssClass="control-label"></asp:Label>--%>
                                    <asp:Label ID="lblProofOfAddress1" Text="Document Type" runat="server" CssClass="control-label"></asp:Label>
                                    <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlProofOfAddress1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProofOfAddress1_SelectedIndexChanged"
                                        CssClass="form-control"
                                        TabIndex="2">
                                        <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div id="divAddProof1" runat="server" class="row">
                                <div id="divPassNoAdd1" runat="server" class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblPassportNoAdd1" runat="server" CssClass="control-label"></asp:Label>
                                    <span style="color: red">*</span>
                                </div>

                                <div id="div25" runat="server" class="col-sm-3">
                                    <div class="input-group">
                                        <span id="MaskCodeSpan1" runat="server" class="input-group-addon input-group-addon-tel">
                                            <asp:TextBox ID="txtMaskCode1" runat="server" CssClass="form-control" Text="X X X X X X X X" Enabled="false" TabIndex="2" MaxLength="8" Style="border-top-left-radius: 4px; border-bottom-left-radius: 4px;"></asp:TextBox>
                                        </span>
                                        <asp:TextBox CssClass="form-control" runat="server" onChange="javascript:this.value=this.value.toUpperCase();"
                                            ID="txtPassNoAdd1" MaxLength="40" TabIndex="2" />
                                        <asp:DropDownList ID="ddlDeemProfofAddr" runat="server" OnSelectedIndexChanged="ddlDeemProfofAddr_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control " TabIndex="2" Visible="false">
                                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div id="divPassAdd1" runat="server" class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="llPassExpDateAdd1" runat="server" CssClass="control-label"></asp:Label>
                                    <%--<span style="color: red">*</span>--%>
                                </div>
                                <div id="divPassDateAdd1" runat="server" class="col-sm-3">
                                    <asp:TextBox CssClass="form-control" onmousedown="$('#EmptyPagePlaceholder_txtPassExpDateAdd').datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' });"
                                        runat="server"
                                        ID="txtPassExpDateAdd1" MaxLength="15" TabIndex="2" />
                                    <asp:TextBox CssClass="form-control" runat="server" ID="txtPassOthrAdd1" MaxLength="15"
                                        TabIndex="2" />
                                </div>
                            </div>

                            <div id="div6" runat="server" class="row">
                                <div id="div7" runat="server" class="col-sm-9">
                                </div>
                                <%--Tushar multiple Doc--%>
                                <div class="container" style="padding-right: 46px;">
                                    <asp:GridView ID="GridView2" runat="server" Width="100%" CssClass="footable" Visible="false">
                                        <%--<AlternatingRowStyle BackColor="White" />--%>
                                        <EditRowStyle BackColor="#7C6F57" />
                                        <FooterStyle BackColor="#1C5E55" ForeColor="White" />
                                        <HeaderStyle BackColor="#00c5cc" ForeColor="White" Height="35px" CssClass="gridViewHeader" />
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#E3EAEB" />
                                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="false" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                        <SortedAscendingHeaderStyle BackColor="#246B61" />
                                        <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                        <SortedDescendingHeaderStyle BackColor="#15524A" />
                                    </asp:GridView>
                                    <%--Tushar multiple Doc--%>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblLocAddLine1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                    <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox CssClass="form-control" runat="server"
                                        ID="txtLocAddLine1" MaxLength="55" TabIndex="2" />
                                </div>
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblLocAddLine2" Text="" runat="server" CssClass="control-label"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox CssClass="form-control" runat="server"
                                        ID="txtLocAddLine2" MaxLength="55" TabIndex="2" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblLocAddLine3" Text="" runat="server" CssClass="control-label"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox CssClass="form-control" runat="server"
                                        ID="txtLocAddLine3" MaxLength="55" TabIndex="2" />
                                </div>
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblCity1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                    <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtCity1" runat="server" CssClass="form-control" Enabled="true" TabIndex="2" MaxLength="50"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblDistrict1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                    <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlDistrict1" runat="server" CssClass="form-control"
                                        TabIndex="2">
                                        <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                    </asp:DropDownList>
                                <asp:TextBox ID="txtddlDistrict1" Visible="false" runat="server" CssClass="form-control" TabIndex="2" ></asp:TextBox>
                                </div>
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblPin1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                    <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-3" style="display: flex;">
                                    <asp:DropDownList ID="ddlPinCode1" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPinCode1_SelectedIndexChanged"
                                        onChange="javascript:AddLoader('ddlPinCode1Loader');" AutoPostBack="True" TabIndex="2">
                                        <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                    </asp:DropDownList>
                                    <div id="ddlPinCode1Loader"></div>
                                <asp:TextBox ID="txtddlPinCode1" Visible="false" runat="server" CssClass="form-control" TabIndex="2" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblState1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                    <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlState1" runat="server" CssClass="form-control"
                                        TabIndex="2">
                                        <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                    </asp:DropDownList>
                                <asp:TextBox ID="txtddlState1" Visible="false" runat="server" Enabled="false" Text="XX" CssClass="form-control" TabIndex="2" ></asp:TextBox>
                                </div>
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblCountryCode1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                    <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-3">
                                    <%--  <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtState')" runat="server"
                                                    ID="txtCountryCode1" MaxLength="15" TabIndex="12"  Enabled="false"/>--%>
                                    <asp:DropDownList ID="ddlCountryCode1" runat="server" CssClass="form-control" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlCountryCode1_SelectedIndexChanged"
                                        TabIndex="2">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <%--  Added for Contact Details start--%>

                <%--</div>--%>
                <%--</div>--%>

                <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                    <div id="Div8" runat="server" class="panel-heading subheader">
                        <div class="row">
                            <div class="col-sm-10" style="text-align: left">
                                <span class="glyphicon glyphicon-menu-hamburger"></span>
                                <asp:CheckBox ID="ChkUpdContact" runat="server" CssClass="standardcheckbox" Text=""
                                    OnCheckedChanged="ChkUpdContact_Checked" AutoPostBack="true" TabIndex="2" />
                                <asp:Label ID="lblContactDetails" Text=""
                                    runat="server" CssClass="control-label"></asp:Label>
                            </div>
                            <div class="col-sm-2" onclick="showHideDiv('div9','Span3');return false;">
                                <span id="ReqDtlsInfoIcon3" class="glyphicon glyphicon-question-sign" onmouseover="NotificationDivSlide('DivSrcReqDtlsNote3','ReqDtlsInfoIcon3'); 
                                    getHeaderbyID('SrcReqDtlsNote3');"
                                    style="float: left; padding: 1px 10px ! important; font-size: 18px; color: red; margin-left: 95px;"></span>
                                <span id="Span3" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div>
                    <div style="background-color: #d5d5c3; display: none;" id="DivSrcReqDtlsNote3">
                        <div class="row">
                            <div class="col-sm-12" style="margin-left: 17px; margin-top: 10px;">
                                <span class="glyphicon glyphicon-alert" style="color: yellow"></span>&#160;&#160;
          <span id="SrcReqDtlsNote3" style="text-align: justify; font-size: 11px;"></span>
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
                                    <span class="input-group-addon input-group-addon-tel" style="width: 23% !important; border-top-left-radius: 7% !important; padding: 0px !important; border: 0px !important;">
                                        <asp:TextBox ID="txtTelOff" runat="server" CssClass="form-control" TabIndex="2" onkeypress="fncInputNumericValuesOnly();" MaxLength="4" Style="border-top-left-radius: 4px; border-bottom-left-radius: 4px;"></asp:TextBox>
                                    </span>
                                    <asp:TextBox ID="txtTelOff2" runat="server" CssClass="form-control" onblur="fnValidateNumber(this.id,'8');" onkeypress="fncInputNumericValuesOnly();"
                                        MaxLength="10" TabIndex="2"></asp:TextBox>
                                </div>
                            </div>
                        <%--Added By Shubham--%>
                        <div id="divFax" runat="server" visible="false">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblFax" runat="server" Text="Fax" CssClass="control-label"></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <div class="input-group">
                                    <span class="input-group-addon input-group-addon-tel" style="width: 23% !important; border-top-left-radius: 7% !important; padding: 0px !important; border: 0px !important;">
                                        <asp:TextBox ID="txtFax1" runat="server" MaxLength="4" onkeypress="fncInputNumericValuesOnly();"
                                            CssClass="form-control" TabIndex="2"></asp:TextBox>
                                    </span>
                                    <asp:TextBox ID="txtFax2" runat="server" CssClass="form-control" onblur="fnValidateNumber(this.id,'8');" MaxLength="10" onkeypress="fncInputNumericValuesOnly();" TabIndex="2"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <%--Ended By Shubham--%>
                        </div>
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblMobile" runat="server" Text="" CssClass="control-label"></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <div class="input-group">
                                    <span class="input-group-addon input-group-addon-tel" style="width: 23% !important; border-top-left-radius: 7% !important; padding: 0px !important; border: 0px !important;">
                                        <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" TabIndex="2" onkeypress="fncInputNumericValuesOnly();" MaxLength="3" Style="border-top-left-radius: 4px; border-bottom-left-radius: 4px;"></asp:TextBox>
                                    </span>
                                    <asp:TextBox ID="txtMobile2" runat="server" CssClass="form-control" onblur="validateMobileNumber(this)" onkeypress="fncInputNumericValuesOnly();"
                                        MaxLength="20" TabIndex="2"></asp:TextBox>
                                </div>
                            </div>
                            <%--Added By Shubham--%>
                            <div id="divMob2" runat="server" visible="false">
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="Label6" runat="server" Text="Mobile Number 2" CssClass="control-label"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <div class="input-group">
                                        <span class="input-group-addon " style="width: 23% !important; border-top-left-radius: 7% !important; padding: 0px !important; border: 0px !important;">
                                            <asp:TextBox ID="txtMobile1" runat="server" CssClass="form-control" onkeypress="fncInputNumericValuesOnly();"
                                                MaxLength="3" Style="border-top-left-radius: 4px; border-bottom-left-radius: 4px;" TabIndex="2"></asp:TextBox>
                                        </span>
                                        <asp:TextBox ID="txtMobile3" runat="server" CssClass="form-control" onblur="validateMobileNumber(this);" onkeypress="fncInputNumericValuesOnly();"
                                            TabIndex="2" MaxLength="20"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <%--Ended By Shubham--%>
                            <%-- <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblFax" runat="server" Text="" CssClass="control-label"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="input-group">
                                            <span class="input-group-addon input-group-addon-tel">
                                                <asp:TextBox ID="txtFax1" runat="server" CssClass="form-control" TabIndex="81" onkeypress="fncInputNumericValuesOnly();" MaxLength="4" Style="border-top-left-radius: 4px; border-bottom-left-radius: 4px;"></asp:TextBox>
                                            </span>
                                            <asp:TextBox ID="txtFax2" runat="server" CssClass="form-control" MaxLength="10" TabIndex="82"  onblur="fnValidateNumber(this.id,'8');" onkeypress="fncInputNumericValuesOnly();"></asp:TextBox>
                                        </div>
                                    </div>--%>
                        </div>
                            <div class="col-sm-3" style="text-align: left; display:none">
                                <asp:Label ID="lblTelRes" runat="server" CssClass="control-label" Text="Tel.(Res)"></asp:Label>
                            </div>
                            <div class="col-sm-3" style="display:none">
                                <div class="input-group">
                                    <span class="input-group-addon input-group-addon-tel" style="width: 23% !important; border-top-left-radius: 7% !important; padding: 0px !important; border: 0px !important;">
                                        <asp:TextBox ID="txtTelRes" runat="server" CssClass="form-control" TabIndex="2" onkeypress="fncInputNumericValuesOnly();" MaxLength="4" Style="border-top-left-radius: 4px; border-bottom-left-radius: 4px;"></asp:TextBox>
                                    </span>
                                    <asp:TextBox ID="txtTelRes2" runat="server" CssClass="form-control" onblur="fnValidateNumber(this.id,'8');" MaxLength="10" onkeypress="fncInputNumericValuesOnly();"
                                        TabIndex="2"></asp:TextBox>
                                </div>
                            </div>

                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblpfemail" runat="server" Text="" CssClass="control-label"></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtemail" runat="server" CssClass="form-control" MaxLength="100" onblur="validateEmail(this)"
                                    TabIndex="2"></asp:TextBox>
                            </div>
                            <%--Added By Shubham--%>
                            <div id="divEmail2" runat="server" visible="false">
                                <div class="col-sm-3" style="text-align: left">
                                    Email ID 2
                           
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtemail2" runat="server" CssClass="form-control" MaxLength="100" TabIndex="2" onblur="checkEmailN(this.id)"></asp:TextBox>
                                </div>
                            </div>
                            <%--Ended By Shubham--%>
                        </div>
                    </div>

                </div>

                <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                    <div id="Div21" runat="server" class="panel-heading">
                        <div class="row">
                            <div class="col-sm-10" style="text-align: left">
                                <span class="glyphicon glyphicon-menu-hamburger"></span>
                                <asp:CheckBox ID="ChkUpdRelated" runat="server" CssClass="standardcheckbox" Text=""
                                    OnCheckedChanged="ChkUpdRelated_Checked" AutoPostBack="true" TabIndex="2" />
                                <asp:Label ID="lblDtlOfRtltpr" Text=""
                                    runat="server" CssClass="control-label">
                                </asp:Label>
                            </div>
                            <div class="col-sm-2" onclick="showHideDiv('menu4','Span10');return false;">
                                <span id="ReqDtlsInfoIcon4" class="glyphicon glyphicon-question-sign" onmouseover="NotificationDivSlide('DivSrcReqDtlsNote4','ReqDtlsInfoIcon4'); 
                                    getHeaderbyID('SrcReqDtlsNote4');"
                                    style="float: left; padding: 1px 10px ! important; font-size: 18px; color: red; margin-left: 95px;"></span>
                                <span id="Span10" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div>
                    <div style="background-color: #d5d5c3; display: none;" id="DivSrcReqDtlsNote4">
                        <div class="row">
                            <div class="col-sm-12" style="margin-left: 17px; margin-top: 10px;">
                                <span class="glyphicon glyphicon-alert" style="color: yellow"></span>&#160;&#160;
          <span id="SrcReqDtlsNote4" style="text-align: justify; font-size: 11px;"></span>
                            </div>
                        </div>
                    </div>
                    <div id="menu4" style="display: block;" class="panel-body">

                        <%--  Added for Details of Related Person start--%>
                        <div class="row">
                            <div id="divchkAddRel" class="col-sm-3" style="text-align: left" runat="server">
                                <asp:CheckBox ID="chkAddRel" Text=" Addition of Related Person" TabIndex="2" AutoPostBack="true"
                                    runat="server" CssClass="control-label" OnCheckedChanged="chkAddRel_Checked" />
                                <span style="color: red">*</span>

                            </div>
                            <div id="divchkDelRel" class="col-sm-6" style="text-align: left" runat="server" visible="false">
                                <asp:CheckBox ID="chkDelRel" OnCheckedChanged="chkAddRel_Checked" Text=" Deletion of Related Person" TabIndex="2" runat="server"
                                    CssClass="control-label" />
                                <span style="color: red">*</span>
                            </div>
                            <div id="div10" class="col-sm-3" style="text-align: left" runat="server">
                            </div>
                            <div id="div11" class="col-sm-3" style="text-align: left" runat="server">
                            </div>
                            <div id="div5" class="col-sm-3" style="text-align: left" runat="server">
                                <asp:LinkButton ID="lnkViewRel" runat="server" Text="View Related Person Detail" ForeColor="#99ccff" FontBold="true" OnClick="lnkViewRel_Click"></asp:LinkButton>
                                <span id="lblRelPersCount" style="color: #99ccff;display: none;" runat="server" visible="false"></span>
                                <asp:Button ID="btnReloadRelPersCount" runat="server" OnClick="btnReloadRelPersCount_Click" Style="display: none;" />
                            </div>
                        </div>
                        <div class="row" style="padding: 0 20px;">
                            <div id="div12" class="col-sm-12" style="text-align: center" runat="server">
                                <asp:Label ID="lblRelRecordShow" Style="text-align: center" Text="  No Records Found" Visible="false" runat="server" />
                            </div>
                            <asp:GridView ID="gvMemDtls" Width="100%" runat="server" AllowSorting="True" CssClass="footable"
                                PageSize="10" AllowPaging="true" CellPadding="1"
                                AutoGenerateColumns="False" OnRowDataBound="gvMemDtls_RowDataBound">
                                <RowStyle CssClass="GridViewRow"></RowStyle>
                                <%--<PagerStyle CssClass="disablepage" />--%>
                                <%--OnPageIndexChanging="gvMemDtls_PageIndexChanging" OnRowCreated="gvMemDtls_RowCreated"--%>
                                <%--<FooterStyle CssClass="GridViewFooter" />
                                <HeaderStyle HorizontalAlign="Center" BackColor="Black" />
                                <SelectedRowStyle CssClass="GridViewSelectedRow" />
                                <AlternatingRowStyle CssClass="GridViewAlternateRow"></AlternatingRowStyle>--%>
                                <EditRowStyle BackColor="#7C6F57" />
                                <FooterStyle BackColor="#1C5E55" ForeColor="White" />
                                <HeaderStyle BackColor="#00c5cc" ForeColor="White" Height="35px" CssClass="gridViewHeader" />
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#E3EAEB" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr. No." ItemStyle-Width="10%" SortExpression="Reference No." HeaderStyle-CssClass="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad center" HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Relative Reference No." ItemStyle-Width="20%" SortExpression="Reference No." HeaderStyle-CssClass="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("RelRefNo") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Relative Type" ItemStyle-Width="20%" SortExpression="Reference No." HeaderStyle-CssClass="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRelTypVal" runat="server" Text='<%# Eval("RelationTypetxt") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Relative Name" ItemStyle-Width="20%" SortExpression="Candidate Name" HeaderStyle-CssClass="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNameVal" runat="server" Text='<%# Eval("FNameRel") + " " + Eval("LNameRel")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date Of Birth" Visible="false" ItemStyle-Width="20%" SortExpression="KYC No" HeaderStyle-CssClass="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMemDOBVal" runat="server" Text='<%# Eval("DOBRel") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gender" Visible="false" ItemStyle-Width="20%" SortExpression="KYC No" HeaderStyle-CssClass="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMemGenVal" runat="server" Text='<%# Eval("GenderReltxt") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Marital Status " ItemStyle-Width="20%" SortExpression="KYC No" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMemMrtVal" runat="server" Text='<%# Eval("MaritalStatusReltxt") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Citizenship" ItemStyle-Width="20%" SortExpression="KYC No" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMemCizVal" runat="server" Text='<%# Eval("CitizenshipReltxt") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Residential Status " ItemStyle-Width="20%" SortExpression="KYC No" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMemResiVal" runat="server" Text='<%# Eval("ResiStatusReltxt") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Occupation Type" ItemStyle-Width="20%" SortExpression="KYC No" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMemOccVal" runat="server" Text='<%# Eval("OccuTypeReltxt") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="15%" SortExpression="Request" HeaderText="Action" HeaderStyle-CssClass="center">
                                        <ItemTemplate>
                                            <div style="width: 100%; white-space: nowrap;">
                                                <span class="glyphicon glyphicon-flag"></span>
                                                <asp:LinkButton ID="lnkdelete" runat="server" OnClick="lnkdelete_Click" ForeColor="Black" Text="Delete"></asp:LinkButton>&nbsp;
                                              <asp:LinkButton ID="lnkView" runat="server" OnClick="lnkView_Click" ForeColor="Black" Text="View" CommandArgument='<%# Eval("RelRefNo") %>'></asp:LinkButton>
                                                <asp:LinkButton ID="lnkEdit" runat="server" OnClick="lnkEdit_Click" ForeColor="Black" Text="Edit" CommandArgument='<%# Eval("RelRefNo") %>'></asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad center" HorizontalAlign="Left" Width="10%" />
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
                                    OnCheckedChanged="ChkUpdRemark_Checked" AutoPostBack="true" TabIndex="2" />
                                <asp:Label ID="lblRemarks" Text="" runat="server" CssClass="control-label">
                                </asp:Label>
                            </div>
                            <div class="col-sm-2" onclick="showHideDiv('menu5','Span11');return false;">
                                <span id="ReqDtlsInfoIcon5" class="glyphicon glyphicon-question-sign" onmouseover="NotificationDivSlide('DivSrcReqDtlsNote5','ReqDtlsInfoIcon5'); 
                                    getHeaderbyID('SrcReqDtlsNote5');"
                                    style="float: left; padding: 1px 10px ! important; font-size: 18px; color: red; margin-left: 95px;"></span>
                                <span id="Span11" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div>
                    <div style="background-color: #d5d5c3; display: none;" id="DivSrcReqDtlsNote5">
                        <div class="row">
                            <div class="col-sm-12" style="margin-left: 17px; margin-top: 10px;">
                                <span class="glyphicon glyphicon-alert" style="color: yellow"></span>&#160;&#160;
          <span id="SrcReqDtlsNote5" style="text-align: justify; font-size: 11px;"></span>
                            </div>
                        </div>
                    </div>
                    <div id="menu5" style="display: block;" class="panel-body">
                        <%--  Added for Details of Remarks start--%>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtRemarks')" runat="server"
                                    ID="txtRemarks" MaxLength="300" TabIndex="2" />
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
                                    OnCheckedChanged="ChkUpdControlPrsn_Checked" AutoPostBack="true" TabIndex="2" />
                                <asp:Label ID="lblattstn" Text="" runat="server" CssClass="control-label">
                                </asp:Label>
                            </div>
                            <div class="col-sm-2" onclick="showHideDiv('menu6','Span12');return false;">
                                <span id="ReqDtlsInfoIcon6" class="glyphicon glyphicon-question-sign" onmouseover="NotificationDivSlide('DivSrcReqDtlsNote6','ReqDtlsInfoIcon6'); 
                                    getHeaderbyID('SrcReqDtlsNote6');"
                                    style="float: left; padding: 1px 10px ! important; font-size: 18px; color: red; margin-left: 95px;"></span>
                                <span id="Span12" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div>
                    <div style="background-color: #d5d5c3; display: none;" id="DivSrcReqDtlsNote6">
                        <div class="row">
                            <div class="col-sm-12" style="margin-left: 17px; margin-top: 10px;">
                                <span class="glyphicon glyphicon-alert" style="color: yellow"></span>&#160;&#160;
          <span id="SrcReqDtlsNote6" style="text-align: justify; font-size: 11px;"></span>
                            </div>
                        </div>
                    </div>
                    <div id="menu6" style="display: block;" class="panel-body">
                        <%--  Added for Applicant Declaration start--%>
                        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                            <div id="Div14" runat="server" class="panel-heading subheader"
                                onclick="showHideDiv('div15','Span6');return false;">
                                <div class="row">
                                    <div class="col-sm-10" style="text-align: left">
                                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                                        <asp:Label ID="lbldec" Text="" runat="server" CssClass="control-label">
                                        </asp:Label>
                                    </div>
                                    <div class="col-sm-2">
                                        <span id="ReqDtlsInfoIcon7" class="glyphicon glyphicon-question-sign" onmouseover="NotificationDivSlide('DivSrcReqDtlsNote7','ReqDtlsInfoIcon7'); 
                                            getHeaderbyID('SrcReqDtlsNote7');"
                                            style="float: left; padding: 1px 10px ! important; font-size: 18px; color: red; margin-left: 90px;"></span>
                                        <span id="Span6" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                                    </div>
                                </div>
                            </div>
                            <div style="background-color: #d5d5c3; display: none;" id="DivSrcReqDtlsNote7">
                                <div class="row">
                                    <div class="col-sm-12" style="margin-left: 17px; margin-top: 10px;">
                                        <span class="glyphicon glyphicon-alert" style="color: yellow"></span>&#160;&#160;
          <span id="SrcReqDtlsNote7" style="text-align: justify; font-size: 11px;"></span>
                                    </div>
                                </div>
                            </div>
                            <div id="div15" style="display: block;" class="panel-body">
                                <div class="row">
                                    <div class="col-sm-12" style="text-align: left; display: flex;">
                                        <%--  <asp:label cssclass="control-label" text="I hereby declare that the details furnished above are true and correct to the best of my knowledge and belief and I undertake to inform you of any changes therein immediately.In case any of the above information is found to be false or untrue or misleading or misrepresenting. I am aware that I may be held liable for it."
                                    onchange="setDateFormat('txtRemarks')" runat="server" id="lblAppDeclare1" maxlength="15"
                                    tabindex="12" />--%>
                                        <asp:CheckBox ID="chkAppDeclare1" Text="I hereby declare that the details furnished above are true and correct to the best of my knowledge and belief and I undertake  to inform you of any changes"
                                            CssClass="control-label" AutoPostBack="false" runat="server"
                                            TabIndex="2" />
                                    </div>
                                    <div class="col-sm-12" style="text-align: left; display: flex; font-weight: bold;">
                                        <asp:Label CssClass="control-label" Text="there in immediately. In case any of the above information is found to be false or untrue or misleading or misrepresenting. I am aware that I may be held   liable for it."
                                            runat="server" ID="lblAppDeclare1" maxlength="15" />
                                    </div>
                                    <br />
                                    <br />
                                    <br />
                                    <div class="col-sm-12" style="text-align: left; display: flex;">
                                        <%--   <asp:label cssclass="control-label" text="I hereby consent to receiving information from Central KYC Registry through SMS/Email on the above registered number/email address"
                                    onchange="setDateFormat('txtRemarks')" runat="server" id="Label7" maxlength="15"
                                    tabindex="12" />--%>
                                        <asp:CheckBox ID="chkAppDeclare2" Text="I/we hereby consent to receiving information from Central KYC Registry through SMS / Email on the above registered number / email address"
                                            CssClass="control-label" AutoPostBack="false" runat="server"
                                            TabIndex="2" />
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
                                         <div class="input-group">
                                            <asp:TextBox CssClass="form-control" runat="server" ID="txtDate" MaxLength="15" TabIndex="2" />
                                            <div class="input-group-btn">
                                                <div class="btn btn-primary btn-lg-kmi" onclick="callCalender('txtDate')">
                                                    <span class="glyphicon glyphicon-calendar BtnGlyphicon"></span>
                                                </div>
                                            </div>
                                        </div>
<%--                                        <asp:TextBox CssClass="form-control" onmousedown="$('#EmptyPagePlaceholder_txtDate').datepicker({ changeMonth: true, changeYear: true, maxDate: '0D', dateFormat: 'dd-mm-yy' });"
                                            runat="server" ID="txtDate" MaxLength="15"
                                            TabIndex="2" />--%>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblPlace1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" runat="server"
                                            ID="txtPlace" MaxLength="15" TabIndex="2" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--  Added for Applicant Declaration end--%>
                        <%--  Added for Attestation/For Office Use Only start--%>
                        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                            <div id="Div16" runat="server" class="panel-heading subheader"
                                onclick="showHideDiv('div17','Span7');return false;">
                                <div class="row">
                                    <div class="col-sm-10" style="text-align: left">
                                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                                        <asp:Label ID="lblAttesOfc" Text="" runat="server" CssClass="control-label">
                                        </asp:Label>
                                    </div>
                                    <div class="col-sm-2">
                                        <span id="ReqDtlsInfoIcon8" class="glyphicon glyphicon-question-sign" onmouseover="NotificationDivSlide('DivSrcReqDtlsNote8','ReqDtlsInfoIcon8'); 
                                            getHeaderbyID('SrcReqDtlsNote8');"
                                            style="float: left; padding: 1px 10px ! important; font-size: 18px; color: red; margin-left: 90px;"></span>
                                        <span id="Span7" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                                    </div>
                                </div>
                            </div>
                            <div style="background-color: #d5d5c3; display: none;" id="DivSrcReqDtlsNote8">
                                <div class="row">
                                    <div class="col-sm-12" style="margin-left: 17px; margin-top: 10px;">
                                        <span class="glyphicon glyphicon-alert" style="color: yellow"></span>&#160;&#160;
          <span id="SrcReqDtlsNote8" style="text-align: justify; font-size: 11px;"></span>
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
                                    <div class="col-sm-3" style="display: flex">
                                        <%-- <asp:CheckBox ID="chkCertifyCopy" Text="Certified Copies" CssClass="standardcheckbox"
                                            Enabled="false" AutoPostBack="true" runat="server" TabIndex="101" />--%>

                                        <asp:DropDownList ID="ddlDocReceived" runat="server" CssClass="form-control" AutoPostBack="true"
                                            onChange="AddLoader('ddlDocReceivedLoader');" OnSelectedIndexChanged="ddlDocReceived_SelectedIndexChanged" TabIndex="2">
                                        </asp:DropDownList>
                                        <div id="ddlDocReceivedLoader"></div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12" style="text-align: center">
                                        <asp:Label ID="lblKYCVerify" Style='text-align: center' CssClass="control-label"
                                            Font-Bold="true" Text="" runat="server" />
                                    </div>
                                </div>
                                <br />
                                <%--//Added by Shubham--%>
                                <div id="divIdVer" runat="server" class="row" visible="false">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblIdVerif" Text="Identity Verification" runat="server" CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left; top: 0px; left: 0px;">
                                        <asp:CheckBox ID="chkDone" Text="" CssClass="standardcheckbox" Checked="true" Enabled="false"
                                            runat="server" TabIndex="2" />
                                        <span>Done</span>
                                    </div>
                                </div>
                                <%--Ended by Shubham--%>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblDate3" Text="" runat="server" CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="input-group">
                                            <asp:TextBox CssClass="form-control" runat="server" ID="txtDateKYCver" MaxLength="15" Enabled="true"
                                                TabIndex="2" />
                                            <div class="input-group-btn">
                                                <div class="btn btn-primary btn-lg-kmi" onclick="callCalender('txtDateKYCver')">
                                                    <span class="glyphicon glyphicon-calendar BtnGlyphicon"></span>
                                                </div>
                                            </div>
                                        </div>
                                       <%-- <asp:TextBox CssClass="form-control" onmousedown="$('#EmptyPagePlaceholder_txtDateKYCver').datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' });"
                                            onchange="setDateFormat('txtDateKYCver')" runat="server" ID="txtDateKYCver" MaxLength="15" Enabled="true"
                                            TabIndex="2" />--%>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblEmpName" Text="Employee Name" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtEmpName" MaxLength="15"
                                            Enabled="true" TabIndex="2" />
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
                                            Enabled="true" TabIndex="2" />
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblEmpDesignation" Text="Employee Designation" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtEmpDesignation" MaxLength="15"
                                            Enabled="true" TabIndex="2" />
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
                                            Enabled="true" TabIndex="2" />
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
                                        <asp:Label ID="lblInsName" Text="Institution Name" runat="server" CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control"
                                            onchange="setDateFormat('txtDate3')" runat="server" ID="txtInsName" MaxLength="15"
                                            Enabled="true" TabIndex="2" />
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblInsCode" Text="Institution Code" runat="server" CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtInsCode" MaxLength="7"
                                            Enabled="true" TabIndex="2" />
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
                    Visible="false" CausesValidation="false" OnClick="btnUpdate_Click" TabIndex="2"> <span class="glyphicon glyphicon-floppy-disk BtnGlyphicon"> Update</span> </asp:LinkButton>
                <asp:LinkButton ID="btnKYCUpdate"  runat="server" CssClass="btn-animated bg-green"
                    Visible="false" CausesValidation="false" OnClick="btnKYCUpdate_Click" TabIndex="2"> <span class="glyphicon glyphicon-floppy-disk BtnGlyphicon"> Update</span> </asp:LinkButton>
               
               
                <asp:LinkButton ID="btnPartialSave"  OnClick="btnPartialSave_Click" style="display:none"
                    CssClass="btn-animated bg-green" runat="server" TabIndex="2">
                   
                    <span class="glyphicon glyphicon-floppy-disk BtnGlyphicon"></span> Partial Save
                </asp:LinkButton>


                <asp:LinkButton ID="btnPartialUpdate"  OnClick="btnPartialUpdate_Click" Visible="false"
                    CssClass="btn-animated bg-green" runat="server" TabIndex="2">
                    <asp:HiddenField ID="HiddenField6" runat="server" />
                    <span class="glyphicon glyphicon-floppy-disk BtnGlyphicon"></span> Partial Update
                </asp:LinkButton>

                <asp:LinkButton ID="btnSave"  OnClick="btnSave_Click"
                    CssClass="btn-animated bg-green" runat="server" TabIndex="2"> <%--OnClientClick="return funCKYC();"--%>
                    <asp:HiddenField ID="TabName" runat="server" />
                    <span class="glyphicon glyphicon-floppy-disk BtnGlyphicon"></span> Save
                </asp:LinkButton>
                 <asp:LinkButton ID="btnCancel" OnClick="btnCancel_Click" CssClass="btn-animated bg-horrible"
                    runat="server" TabIndex="2">
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
            <div class="modal" id="myModalRaise" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
                aria-hidden="true" style="padding-top: 0px;">
                <div class="modal-dialog" style="padding-top: 0px; margin-top: 2px; width: 98%;">
                    <div class="modal-content">
                        <div class="modal-header" style="margin-top: -10px; margin-bottom: -20px; padding-bottom: 6px ! important;">
                            <button type="button" class="close" onclick="AutoBindRelPersGV();" data-dismiss="modal" aria-hidden="true">
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
            <div class="modal" id="myModalRaise_Master" tabindex="-1" role="dialog" aria-labelledby="myModalLabel_MAster"
                aria-hidden="true" style="padding-top: 0px;">
                <div class="modal-dialog" style="padding-top: 0px; margin-top: 2px; width: 95%;">
                    <div class="modal-content">
                        <div class="modal-header" style="margin-top: -10px; margin-bottom: -20px; padding-bottom: 6px ! important;">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="false">
                                &times;</button>
                            <h4 class="modal-title" id="myModalLabel_MAster"></h4>
                        </div>
                        <div class="modal-body">
                            <iframe id="iframeCFR_Master" src="" width="100%" height="505" frameborder="0" allowtransparency="false"></iframe>
                        </div>
                        <div class="modal-footer">
                            <div style="text-align: center;">
                                <asp:LinkButton ID="LinkButton3" TabIndex="43" runat="server" CssClass="btn-animated bg-horrible"
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

            <!-- Display Modal popup window in division -->
            <%-- <div class="modal fade" id="myModalMst" role="dialog">
            </div>--%>
            <div class="modal" id="myModalRaiseMst" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="padding-top: 0px; height: 95%;">
                <div class="modal-dialog" style="padding-top: 0px; margin-top: 2px; width: 60%;">
                    <div class="modal-content">
                        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px; border-color: transparent !important;">
                            <div id="Div24" runat="server" class="panel-heading">
                                <div class="row">
                                    <div class="col-sm-10" style="text-align: left">
                                        <span class="glyphicon glyphicon-menu-hamburger"></span>

                                        <span class="modal-title" id="myModalLabelMst">CKYC QC</span>
                                    </div>
                                    <div class="col-sm-2">
                                        <span id="Span20" class="glyphicon glyphicon-remove" style="float: right; padding: 1px 10px ! important; font-size: 18px;" data-dismiss="modal" aria-hidden="true" onclick="ModalbtnClose();"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-body">

                                <iframe id="iframeCFRMst" src="" width="100%" height="470" frameborder="0" allowtransparency="true"></iframe>
                            </div>
                            <div class="modal-footer" style="display: none">
                            </div>
                        </div>
                        <!-- /.modal-content -->
                    </div>
                    <!-- /.modal-dialog -->
                </div>

                <%-- Added By Megha Bhave 25.03.2021 --%>
                <%--<div id="dvProgressBar" style="display: none; text-align: center" class="loader">
                <center>
                         <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /> <br /><br /><br /><br /><br />
                          <asp:Image id="ldr" src="../../Images/horizonal_loader.gif"   height="50px" alt="" runat="server" ImageAlign="Middle"/>
                         <br />
                      <asp:Label ID="lblMsg" Text="" runat="server" ForeColor="Blue" style="font-size: medium; font-weight:bold" > </asp:Label>
                
            </center>
            </div>--%>
                <%-- Ended By Megha Bhave 25.03.2021 --%>
                <!-- End Display Modal popup window in division -->
                <input id="hdnChkPOADoc" type="hidden" runat="server" />
                <input id="hdnChkPOIDocument" type="hidden" runat="server" />
                <input id="hdnddlProofOfAddress1" type="hidden" runat="server" />
                <asp:HiddenField ID="hdnrefno" ClientIDMode="Static" runat="server" />
                <asp:HiddenField ID="hdnFiRefNo" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
