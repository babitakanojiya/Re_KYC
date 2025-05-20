<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="CKYCRelatedPrsnView.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.CKYC.CKYCRelatedPrsnView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/bundles/CKYCValidationjs") %>
    <%: Styles.Render("~/bundles/CKYCcss") %>

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
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">
    
    <script type="text/javascript">

        function funload() {
            document.getElementById('EmptyPagePlaceholder_divloaderqc').style.display = 'block'
        }

        function funIsAlphaNumericHypenWithoutSpace() {
            //if (((event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode >= 65 && event.keyCode <= 90) || (event.keyCode >= 97 && event.keyCode <= 122)) == false && !(event.keyCode == 45 || event.keyCode == 95)) {
            //    event.returnValue = false;
            //}
        }

        function fncInputNumericValuesWithHyphenOnly() {
            if (!(event.keyCode == 45 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                event.returnValue = false;
            }
        }

        function fncInputcharacterOnlyNew() {
            if (!(event.keyCode == 32 || event.keyCode == 65 || event.keyCode == 66 || event.keyCode == 67 || event.keyCode == 68 || event.keyCode == 69 || event.keyCode == 70 || event.keyCode == 71 || event.keyCode == 72 || event.keyCode == 73 || event.keyCode == 74 || event.keyCode == 75 || event.keyCode == 76 || event.keyCode == 77 || event.keyCode == 78 || event.keyCode == 79 || event.keyCode == 80 || event.keyCode == 81 || event.keyCode == 82 || event.keyCode == 83 || event.keyCode == 84 || event.keyCode == 85 || event.keyCode == 86 || event.keyCode == 87 || event.keyCode == 88 || event.keyCode == 89 || event.keyCode == 90 || event.keyCode == 97 || event.keyCode == 98 || event.keyCode == 99 || event.keyCode == 100 || event.keyCode == 101 || event.keyCode == 102 || event.keyCode == 103 || event.keyCode == 104 || event.keyCode == 105 || event.keyCode == 106 || event.keyCode == 107 || event.keyCode == 108 || event.keyCode == 109 || event.keyCode == 110 || event.keyCode == 111 || event.keyCode == 112 || event.keyCode == 113 || event.keyCode == 114 || event.keyCode == 115 || event.keyCode == 116 || event.keyCode == 117 || event.keyCode == 118 || event.keyCode == 119 || event.keyCode == 120 || event.keyCode == 121 || event.keyCode == 122)) {
                event.returnValue = false;
                //e.preventDefault();
            }
        }

        function fncInputValidateDesignation() {
            if (!(event.keyCode == 40 || event.keyCode == 41 || event.keyCode == 46 || event.keyCode == 47 || event.keyCode == 32 || event.keyCode == 65 || event.keyCode == 66 || event.keyCode == 67 || event.keyCode == 68 || event.keyCode == 69 || event.keyCode == 70 || event.keyCode == 71 || event.keyCode == 72 || event.keyCode == 73 || event.keyCode == 74 || event.keyCode == 75 || event.keyCode == 76 || event.keyCode == 77 || event.keyCode == 78 || event.keyCode == 79 || event.keyCode == 80 || event.keyCode == 81 || event.keyCode == 82 || event.keyCode == 83 || event.keyCode == 84 || event.keyCode == 85 || event.keyCode == 86 || event.keyCode == 87 || event.keyCode == 88 || event.keyCode == 89 || event.keyCode == 90 || event.keyCode == 97 || event.keyCode == 98 || event.keyCode == 99 || event.keyCode == 100 || event.keyCode == 101 || event.keyCode == 102 || event.keyCode == 103 || event.keyCode == 104 || event.keyCode == 105 || event.keyCode == 106 || event.keyCode == 107 || event.keyCode == 108 || event.keyCode == 109 || event.keyCode == 110 || event.keyCode == 111 || event.keyCode == 112 || event.keyCode == 113 || event.keyCode == 114 || event.keyCode == 115 || event.keyCode == 116 || event.keyCode == 117 || event.keyCode == 118 || event.keyCode == 119 || event.keyCode == 120 || event.keyCode == 121 || event.keyCode == 122)) {
                event.returnValue = false;
                //e.preventDefault();
            }
        }

        function fncInputValidateName() {
            //if (!(event.keyCode == 39 || event.keyCode == 46 || event.keyCode == 32 || event.keyCode == 65 || event.keyCode == 66 || event.keyCode == 67 || event.keyCode == 68 || event.keyCode == 69 || event.keyCode == 70 || event.keyCode == 71 || event.keyCode == 72 || event.keyCode == 73 || event.keyCode == 74 || event.keyCode == 75 || event.keyCode == 76 || event.keyCode == 77 || event.keyCode == 78 || event.keyCode == 79 || event.keyCode == 80 || event.keyCode == 81 || event.keyCode == 82 || event.keyCode == 83 || event.keyCode == 84 || event.keyCode == 85 || event.keyCode == 86 || event.keyCode == 87 || event.keyCode == 88 || event.keyCode == 89 || event.keyCode == 90 || event.keyCode == 97 || event.keyCode == 98 || event.keyCode == 99 || event.keyCode == 100 || event.keyCode == 101 || event.keyCode == 102 || event.keyCode == 103 || event.keyCode == 104 || event.keyCode == 105 || event.keyCode == 106 || event.keyCode == 107 || event.keyCode == 108 || event.keyCode == 109 || event.keyCode == 110 || event.keyCode == 111 || event.keyCode == 112 || event.keyCode == 113 || event.keyCode == 114 || event.keyCode == 115 || event.keyCode == 116 || event.keyCode == 117 || event.keyCode == 118 || event.keyCode == 119 || event.keyCode == 120 || event.keyCode == 121 || event.keyCode == 122)) {
            //    event.returnValue = false;
            //    //e.preventDefault();
            //}
        }

        function fncInputValidateNameNew() {
            if (!(event.keyCode == 38 || event.keyCode == 39 || event.keyCode == 46 || event.keyCode == 32 || event.keyCode == 65 || event.keyCode == 66 || event.keyCode == 67 || event.keyCode == 68 || event.keyCode == 69 || event.keyCode == 70 || event.keyCode == 71 || event.keyCode == 72 || event.keyCode == 73 || event.keyCode == 74 || event.keyCode == 75 || event.keyCode == 76 || event.keyCode == 77 || event.keyCode == 78 || event.keyCode == 79 || event.keyCode == 80 || event.keyCode == 81 || event.keyCode == 82 || event.keyCode == 83 || event.keyCode == 84 || event.keyCode == 85 || event.keyCode == 86 || event.keyCode == 87 || event.keyCode == 88 || event.keyCode == 89 || event.keyCode == 90 || event.keyCode == 97 || event.keyCode == 98 || event.keyCode == 99 || event.keyCode == 100 || event.keyCode == 101 || event.keyCode == 102 || event.keyCode == 103 || event.keyCode == 104 || event.keyCode == 105 || event.keyCode == 106 || event.keyCode == 107 || event.keyCode == 108 || event.keyCode == 109 || event.keyCode == 110 || event.keyCode == 111 || event.keyCode == 112 || event.keyCode == 113 || event.keyCode == 114 || event.keyCode == 115 || event.keyCode == 116 || event.keyCode == 117 || event.keyCode == 118 || event.keyCode == 119 || event.keyCode == 120 || event.keyCode == 121 || event.keyCode == 122)) {
                event.returnValue = false;
                //e.preventDefault();
            }
        }

        function fncInputDocsKeyValidate() {
            if (!((event.keyCode > 47 && event.keyCode < 58) || event.keyCode == 45 || event.keyCode == 47 || event.keyCode == 32 || event.keyCode == 65 || event.keyCode == 66 || event.keyCode == 67 || event.keyCode == 68 || event.keyCode == 69 || event.keyCode == 70 || event.keyCode == 71 || event.keyCode == 72 || event.keyCode == 73 || event.keyCode == 74 || event.keyCode == 75 || event.keyCode == 76 || event.keyCode == 77 || event.keyCode == 78 || event.keyCode == 79 || event.keyCode == 80 || event.keyCode == 81 || event.keyCode == 82 || event.keyCode == 83 || event.keyCode == 84 || event.keyCode == 85 || event.keyCode == 86 || event.keyCode == 87 || event.keyCode == 88 || event.keyCode == 89 || event.keyCode == 90 || event.keyCode == 97 || event.keyCode == 98 || event.keyCode == 99 || event.keyCode == 100 || event.keyCode == 101 || event.keyCode == 102 || event.keyCode == 103 || event.keyCode == 104 || event.keyCode == 105 || event.keyCode == 106 || event.keyCode == 107 || event.keyCode == 108 || event.keyCode == 109 || event.keyCode == 110 || event.keyCode == 111 || event.keyCode == 112 || event.keyCode == 113 || event.keyCode == 114 || event.keyCode == 115 || event.keyCode == 116 || event.keyCode == 117 || event.keyCode == 118 || event.keyCode == 119 || event.keyCode == 120 || event.keyCode == 121 || event.keyCode == 122)) {
                event.returnValue = false;
                //e.preventDefault();
            }
        }

        function tinvalidation(Obj) {                                       //Function added by daksh-- Validation
            debugger;
            if (Obj == null) Obj = window.event.srcElement;
            if (Obj.value != "") {
                ObjVal = Obj.value;
                var panPat = /^(?:\d{3}-\d{2}-\d{4}|\d{2}-\d{7})$/;
                //var panPat = /^[A-PR-WYa-pr-wy][1-9]\d\d{4}[1-9]$/;
                if (panPat.test(Obj.value)) {
                    if (Obj.value != "") {
                        //document.getElementById("spntincnt").style.display = "block";
                        //document.getElementById("dvTINCntry").style.display = "block";
                        
                    }
                    else {
                        //document.getElementById("spntincnt").style.display = "none";
                        //document.getElementById("dvTINCntry").style.display = "none";
                    }
                    return true;
                }
                else {
                    //document.getElementById("spntincnt").style.display = "none";
                    //document.getElementById("dvTINCntry").style.display = "none";
                    AlertMsg("Please enter Valid TIN/GST Registration Number");
                    Obj.value = "";
                    Obj.focus();
                    return false;
                }
            }
            else {
                //document.getElementById("spntincnt").style.display = "none";
                //document.getElementById("dvTINCntry").style.display = "none";
            }
        }

        
        function  toggleTitle(elem) {
            debugger;
            var val = $('#<%= rbtFS.ClientID%>  input:checked').val();
            if (val == 'F') {
                $("#<%=cboTitle2.ClientID%>").children("option[value='MR']").css('display', 'block');
                $("#<%=cboTitle2.ClientID%>").children("option[value='MRS']").hide();
            } else {
                $("#<%=cboTitle2.ClientID%>").children("option[value='MRS']").css('display','block');
                $("#<%=cboTitle2.ClientID%>").children("option[value='MR']").hide();
            }
            //$("#<%=cboTitle2.ClientID%>").children("option[value^=" + $(this).val() + "]").show()
        }

       

        function callCalender() {
            debugger;
            var dateArr = $("#<%=txtDOB.ClientID%>").val().split('-');
            $("#<%= txtDOB.ClientID%>").datepicker({ maxDate: new Date(), changeMonth: true, changeYear: true, dateFormat: 'dd-mm-yy' });
            $.datepicker.initialized = true;
            $("#<%= txtDOB.ClientID%>").focus();

        }

        function callCalender1() {
            debugger;
            var dateArr = $("#<%=txtPassExpDate.ClientID%>").val().split('-');
            $("#<%= txtPassExpDate.ClientID%>").datepicker({ minDate: new Date(), changeMonth: true, changeYear: true, dateFormat: 'dd-mm-yy' });
            $.datepicker.initialized = true;
            $("#<%= txtPassExpDate.ClientID%>").focus();

        }

        function callCalender2() {
            debugger;
            var dateArr = $("#<%=txtDate.ClientID%>").val().split('-');
            $("#<%= txtDate.ClientID%>").datepicker({ maxDate: new Date(), changeMonth: true, changeYear: true, dateFormat: 'dd-mm-yy' });
            $.datepicker.initialized = true;
            $("#<%= txtDate.ClientID%>").focus();

        }


        function callCalender3() {
            debugger;
            var dateArr = $("#<%=txtDate3.ClientID%>").val().split('-');
            $("#<%= txtDate3.ClientID%>").datepicker({ maxDate: new Date(), changeMonth: true, changeYear: true, dateFormat: 'dd-mm-yy' });
            $.datepicker.initialized = true;
            $("#<%= txtDate3.ClientID%>").focus();

        }

        function callCalender5() {
            debugger;
            var dateArr = $("#<%=txtPassExpDateAdd.ClientID%>").val().split('-');
            $("#<%= txtPassExpDateAdd.ClientID%>").datepicker({ minDate: new Date(), changeMonth: true, changeYear: true, dateFormat: 'dd-mm-yy' });
            $.datepicker.initialized = true;
            $("#<%= txtPassExpDateAdd.ClientID%>").focus();

        }

        function AlertMsg(msg) {
            debugger;
            var varFooter = '<center><button type="button" class="btn btn-success" data-dismiss="modal" style="margin-top: -6px;"><span class="glyphicon glyphicon-ok" style="color: White;"></span> OK </button></center>';
            showModal('#myModal', 'Alert', 'alert-warning', varFooter, '', msg);
        }

        function ValidateDOB(date) {
            debugger;
            var dateObj = date.split('-');
            if (!getYearDiff(new Date(dateObj[2], dateObj[1] - 1, dateObj[0]))) {
                AlertMsg("DOB should not be less then 18 years");
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

        function OpenStateWindow1(Flag1) {
            debugger;

            var flag = Flag1
            var e = document.getElementById("<%= ddlState1.ClientID %>");
            var strUser = e.options[e.selectedIndex].value;
            //var strUser = e.options[e.selectedIndex].innerText;
            // var StCode = document.getElementById("<%= ddlState.ClientID %>").value;
            window.open("PinCodeDtls.aspx?StateCode=" + strUser + "&flag=" + flag + "", '', 'width=640,height=354,toolbar=no,scrollbars=yes,resizable=yes,left=300,top=230,location=0;');
        }

        function OpenStateWindow2(Flag1) {
            debugger;

            var flag = Flag1
            var e = document.getElementById("<%= ddlState2.ClientID %>");
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
                 //$("#btnAdd").css("display","block");
                 //$("#btnPartialAdd").css("display","block");
                 //$("#btnUpdate").css("display","none");
                 //$("#btnPSUpdate").css("display","none");
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

        $(document).ready(function () {
            
        })

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
                AlertMsg("Please enter valid email-1 address");
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

    <script type="text/javascript">

          <%--<added by ramesh on dated 21-05-2018>--%>

        function specialcharecter() {
            debugger;
            var iChars = "!`@#$%^&*()+=-[]\\\';,./{}|\":<>?~_";
            var data = document.getElementById("<%=txtPlace.ClientID %>").value;
            for (var i = 0; i < data.length; i++) {
                if (iChars.indexOf(data.charAt(i)) != -1) {
                    AlertMsg("special characters are not allowed.");
                    document.getElementById("<%=txtPlace.ClientID %>").value = "";
                    document.getElementById("<%=txtPlace.ClientID %>").focus();
                    return false;
                }
            }
            var data1 = document.getElementById("<%=txtEmpName.ClientID %>").value;
            for (var i = 0; i < data1.length; i++) {
                if (iChars.indexOf(data1.charAt(i)) != -1) {
                    AlertMsg("special characters are not allowed.");
                    document.getElementById("<%=txtEmpName.ClientID %>").value = "";
                    document.getElementById("<%=txtEmpName.ClientID %>").focus();
                    return false;
                }
            }

            var data2 = document.getElementById("<%=txtEmpCode.ClientID %>").value;
            for (var i = 0; i < data2.length; i++) {
                if (iChars.indexOf(data2.charAt(i)) != -1) {
                    AlertMsg("special characters are not allowed.");
                    document.getElementById("<%=txtEmpCode.ClientID %>").value = "";
                    document.getElementById("<%=txtEmpCode.ClientID %>").focus();
                    return false;
                }
            }
            var data3 = document.getElementById("<%=txtEmpDesignation.ClientID %>").value;
            for (var i = 0; i < data3.length; i++) {
                if (iChars.indexOf(data3.charAt(i)) != -1) {
                    AlertMsg("special characters are not allowed.");
                    document.getElementById("<%=txtEmpDesignation.ClientID %>").value = "";
                    document.getElementById("<%=txtEmpDesignation.ClientID %>").focus();
                    return false;
                }
            }
            var data4 = document.getElementById("<%=txtEmpBranch.ClientID %>").value;
            for (var i = 0; i < data4.length; i++) {
                if (iChars.indexOf(data4.charAt(i)) != -1) {
                    AlertMsg("special characters are not allowed.");
                    document.getElementById("<%=txtEmpBranch.ClientID %>").value = "";
                    document.getElementById("<%=txtEmpBranch.ClientID %>").focus();
                    return false;
                }
            }

            var data5 = document.getElementById("<%=txtInsName.ClientID %>").value;
            for (var i = 0; i < data5.length; i++) {
                if (iChars.indexOf(data5.charAt(i)) != -1) {
                    AlertMsg("special characters are not allowed.");
                    document.getElementById("<%=txtInsName.ClientID %>").value = "";
                    document.getElementById("<%=txtInsName.ClientID %>").focus();
                    return false;
                }
            } var data5 = document.getElementById("<%=txtRefNumber.ClientID %>").value;
            for (var i = 0; i < data5.length; i++) {
                if (iChars.indexOf(data5.charAt(i)) != -1) {
                    AlertMsg("special characters are not allowed.");
                    document.getElementById("<%=txtRefNumber.ClientID %>").value = "";
                    document.getElementById("<%=txtRefNumber.ClientID %>").focus();
                    return false;
                }
            }
        }
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
        <%--<end>--%>


    </script>

    <asp:ScriptManager ID="SM1" runat="server">
    </asp:ScriptManager>

    <div class="container" style="margin-top: 0px; width: 100%;">
        <%-- Added for CKYC Details header start--%>
        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
            <div id="Div18" runat="server" class="panel-heading" onclick="showHideDiv('divCKYCdtls','btnCKYCdtls');return false;">
                <div class="row">
                    <div class="col-sm-10" style="text-align: left">
                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                        <asp:Label ID="Label6" Text="DETAILS OF RELATED PERSON" runat="server" CssClass="control-label">
                        </asp:Label>
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
                        <asp:Label ID="lblRelRefNumber" Text="Relative Reference Number" runat="server" Font-Bold="false"></asp:Label>
                    </div>

                    <div class="col-sm-3" style="text-align: left">
                        <asp:TextBox ID="txtRelRefNumber" runat="server" CssClass="form-control" Enabled="false"
                            Font-Bold="false" TabIndex="2">
                        </asp:TextBox>
                    </div>
                    <div class="col-sm-3">
                        <asp:Label ID="lblKYCNum" Text="KYC Number of Related Person (if available)" placeholder="KYC Number"
                            runat="server" CssClass="control-label"></asp:Label>

                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox CssClass="form-control" runat="server" ID="txtKYCNum" AutoPostBack="true" OnTextChanged="txtKYCNum_TextChanged" onkeypress="funIsAlphaNumericWithoutSpace();"
                            placeholder="KYC Number" MaxLength="14" TabIndex="2" />
                        <%--OnTextChanged="txtKYCNumber_TextChanged" AutoPostBack="true"  onkeypress="funIsAlphaNumericWithoutSpace();"--%>
                    </div>
                </div>
                <div class="row" style="margin-top: 4px">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblRefNumber" Text="FI Reference Number" runat="server" Font-Bold="false"></asp:Label>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:TextBox ID="txtRefNumber" runat="server" MaxLength="14" CssClass="form-control"  onChange="javascript:this.value=this.value.toUpperCase();"
                            placeholder="FI Reference Number" Font-Bold="false" TabIndex="2"> <%--OnTextChanged="txtRefNumber_TextChanged" AutoPostBack="true" onkeypress="funIsAlphaNumericHypenWithoutSpace();"--%>
                        </asp:TextBox>
                    </div>

                    <div class="col-sm-3">
                        <asp:Label ID="lblRelType" Text="Related Person Type" runat="server" CssClass="control-label">
                        </asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlRelType" runat="server" CssClass="form-control" TabIndex="2">
                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row" style="margin-top: 4px">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblNum" Visible="false" runat="server" Font-Bold="false"></asp:Label>
                        <span ID="lblNumImp" Visible="false" runat="server" style="color: red">*</span>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:TextBox ID="txtNum" Visible="false" runat="server" AutoPostBack="true" MaxLength="14" CssClass="form-control" OnTextChanged="txtNum_TextChanged" Font-Bold="false" TabIndex="2">
                        </asp:TextBox>
                    </div>
                </div>
            </div>
        </div>

          <%-- Img Section--%>
    <%--<div class="container" width="100%">--%>
        <div id="divImg" runat="server" class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
            <div id="Div25" runat="server" class="panel-heading">
                <div class="row">
                    <div class="col-sm-10" style="text-align: left">
                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                        <asp:Label ID="lbluploadDoc" Text="UPLOADED DOCUMENTS" runat="server" CssClass="control-label"></asp:Label>

                    </div>
                    <div class="col-sm-2" onclick="showHideDiv('divnavigate','btnToggle');return false;">
                        <span id="btnnavigate" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                    </div>
                </div>
            </div>
            <div id="divnavigate" style="display: block;" runat="server" class="panel-body">
                <div class="row">
                    <div class="col-sm-12" style="text-align: right">
                        <asp:UpdatePanel runat="server" ID="upnlPrev">
                            <ContentTemplate>
                                <%----%>

                                <%--  <asp:LinkButton ID="btnprev" Text="Prev" runat="server" CssClass="btn btn-primary" CausesValidation="false"
         OnClick="btnprev_Click"    TabIndex="244" >
        <span class="glyphicon glyphicon-arrow-left">Prev</span> 
        </asp:LinkButton> --%>
                                <%----%>
                                <%-- <asp:LinkButton ID="btnnext" Text="Next" runat="server" CssClass="btn btn-primary" CausesValidation="false"
           OnClick="btnnext_Click" TabIndex="244" >
        <span class="glyphicon glyphicon-arrow-right">Next</span> 
        </asp:LinkButton> --%>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div>
                    <div class="col-sm-3" style="text-align: left" style="display: none">


                        <asp:Label ID="lblPageInfo" runat="server" Visible="false"></asp:Label>
                    </div>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div id="divloaderqc" class="col-sm-12" runat="server" style="display: none; position: absolute;">
                                <caption>
                                    <img id="Img3" alt="" src="~/images/spinner.gif" runat="server" />
                                    Loading...
                                </caption>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel runat="server" ID="upnlHeader">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-sm-12" align="center">
                                    <asp:Label ID="lblpanelheader" runat="server" CssClass="control-label" />
                                    <asp:HiddenField ID="hdnDocId" runat="server" />
                                </div>
                            </div>
                            <div id="Div26" runat="server" class="panel-body">
                                <div class="row">


                                    <asp:GridView ID="GridImage" runat="server" AllowSorting="True" CssClass="footable"
                                        Width="100%" AutoGenerateColumns="False" PageSize="10" AllowPaging="true" CellPadding="1"
                                        OnRowDataBound="GridImage_RowDataBound">
                                        <%--OnRowDataBound="GridImage_RowDataBound"--%>
                                        <%--OnPageIndexChanging="GridImage_PageIndexChanging"
                                        --%>
                                        <RowStyle CssClass="GridViewRow"></RowStyle>
                                        <PagerStyle CssClass="disablepage" />
                                        <HeaderStyle CssClass="gridview th" />


                                        <Columns>
                                            <asp:TemplateField SortExpression="SR_NO" HeaderText="SR_NO" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblCndNo1" runat="server" Text='<%# Eval("SR_NO") %>'></asp:LinkButton>
                                                    <asp:HiddenField ID="hdnid" runat="server" Value='<%# Eval("SR_NO") %>'></asp:HiddenField>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:ImageField DataImageUrlField="SR_NO" DataImageUrlFormatString="ImageCSharp.aspx?ImageID=ckyc+{0}"
                                                HeaderText="Preview Image">
                                                <ControlStyle CssClass="left_padding" Width="30%" />
                                                <%--Height="100%"--%>
                                            </asp:ImageField>
                                        </Columns>
                                    </asp:GridView>
                                    <center>
                                                  

                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Button ID="btnprev" Text="<" CssClass="form-submit-button" runat="server"
                                                                        Width="40px" Enabled="false" Style="border-style: solid; border-width: 1px; background-repeat: no-repeat;
                                                                        background-color: transparent; float: left; margin: 0; height: 30px;" OnClick="btnprev_Click" />
                                                                  <%--  <asp:TextBox runat="server" ID="txtPage" Text="1" Style="width: 35px; border-style: solid;
                                                                        border-width: 1px; border-color: Gray; height: 30px; float: left; margin: 0;
                                                                        text-align: center;" CssClass="form-control" ReadOnly="true" />--%>
                                                                    <asp:Button ID="btnnext" Text=">" CssClass="form-submit-button" runat="server" Style="border-style: solid;
                                                                        border-width: 1px; background-repeat: no-repeat; background-color: transparent;
                                                                        float: left; margin: 0; height: 30px;" Width="40px" OnClick="btnnext_Click" OnClientClick="funload();" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </center>


                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnnext" EventName="Click"></asp:AsyncPostBackTrigger>
                        </Triggers>
                    </asp:UpdatePanel>


                </div>
            </div>
        </div>
    <%--</div>--%>
    <%--Img section End--%>

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
                                                            <asp:DropDownList ID="cboTitle" runat="server" CssClass="form-control" DataTextField="ParamDesc" AutoPostBack="true" OnSelectedIndexChanged="cboTitle_SelectedIndexChanged"
                                                                DataValueField="ParamValue" AppendDataBoundItems="True" TabIndex="2">
                                                                <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                                <%----%>
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-sm-10" style="padding: 0">
                                                    <div class="col-sm-4" style="padding-left: 0">
                                                        <asp:TextBox ID="txtGivenName" runat="server" CssClass="form-control" onkeypress="funIsAlphaNumericWithSpace();" onchange="javascript:this.value=this.value.toUpperCase();"
                                                            MaxLength="50" TabIndex="2" placeholder="First Name">
                                                        </asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-4" style="padding-left: 0">
                                                        <asp:TextBox ID="txtMiddleName" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="funIsAlphaNumericWithSpace();"
                                                            MaxLength="50" TabIndex="2" onblur="CheckSpaces();return false;" placeholder="Middle Name">
                                                        </asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-4" style="padding-left: 0">
                                                        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="funIsAlphaNumericWithSpace();"
                                                            MaxLength="50" TabIndex="2" onblur="CheckSpaces();return false;" placeholder="Last Name">
                                                        </asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 4px">
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblMaidenName" Text="Maiden Name" CssClass="control-label" runat="server">
                                                </asp:Label>
                                            </div>
                                            <div class="col-sm-9" style="padding: 0">
                                                <div class="col-sm-2">
                                                    <asp:UpdatePanel ID="ipcboTitle1" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="cboTitle1" runat="server" CssClass="form-control" DataTextField="ParamDesc"
                                                                DataValueField="ParamValue" AppendDataBoundItems="True" TabIndex="2">
                                                                <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-sm-10" style="padding: 0">
                                                    <div class="col-sm-4" style="padding-left: 0">
                                                        <asp:TextBox ID="txtGivenName1" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="funIsAlphaNumericWithSpace();"
                                                            MaxLength="50" TabIndex="2" onblur="CheckSpaces();return false;" placeholder="First Name">
                                                        </asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-4" style="padding-left: 0">
                                                        <asp:TextBox ID="txtMiddleName1" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="funIsAlphaNumericWithSpace();"
                                                            MaxLength="50" TabIndex="2" onblur="CheckSpaces();return false;" placeholder="Middle Name">
                                                        </asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-4" style="padding-left: 0">
                                                        <asp:TextBox ID="txtLastName1" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="funIsAlphaNumericWithSpace();"
                                                            MaxLength="50" TabIndex="2" onblur="CheckSpaces();return false;" placeholder="Last Name">
                                                        </asp:TextBox>
                                                    </div>
                                                    <asp:HiddenField ID="hdnGenderTitle1" runat="server"></asp:HiddenField>
                                                    <asp:HiddenField ID="hdnGenderTitle2" runat="server"></asp:HiddenField>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 4px">
                                            <div class="col-sm-3">
                                                <div class="col-sm-5" style="padding: 0">
                                                    <asp:Label ID="lblFatherName" Text="Father/Spouse Name" CssClass="control-label"
                                                        runat="server"></asp:Label>
                                                    <span style="color: red">*</span>
                                                </div>
                                                <div class="col-sm-7" style="padding: 0">
                                                    <asp:UpdatePanel ID="UpdFSFlag" runat="server">
                                                        <ContentTemplate>
                                                            <asp:RadioButtonList ID="rbtFS" onchange="toggleTitle(this)" runat="server" CssClass="standardcheckbox" RepeatDirection="Horizontal"
                                                                Visible="true" TabIndex="2">
                                                                <asp:ListItem Value="F">Father&nbsp;</asp:ListItem>
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
                                                            <asp:DropDownList ID="cboTitle2" runat="server" CssClass="form-control" DataTextField="ParamDesc"
                                                                DataValueField="ParamValue" AppendDataBoundItems="True" TabIndex="2">
                                                                <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-sm-10" style="padding: 0">
                                                    <div class="col-sm-4" style="padding-left: 0">
                                                        <asp:TextBox ID="txtGivenName2" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="funIsAlphaNumericWithSpace();"
                                                            MaxLength="50" TabIndex="2" onblur="CheckSpaces();return false;" placeholder="First Name">
                                                        </asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-4" style="padding-left: 0">
                                                        <asp:TextBox ID="txtMiddleName2" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="funIsAlphaNumericWithSpace();"
                                                            MaxLength="50" TabIndex="2" onblur="CheckSpaces();return false;" placeholder="Middle Name">
                                                        </asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-4" style="padding-left: 0">
                                                        <asp:TextBox ID="txtLastName2" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="funIsAlphaNumericWithSpace();"
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
                                        <div class="row" style="margin-top: 4px">
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblMotherName" Text="Mother Name" CssClass="control-label" runat="server">
                                                </asp:Label>
                                                <span style="display: none; color: red">*</span>
                                            </div>
                                            <div class="col-sm-9" style="padding: 0">
                                                <div class="col-sm-2">
                                                    <asp:DropDownList ID="cboTitle3" runat="server" CssClass="form-control" DataTextField="ParamDesc"
                                                        DataValueField="ParamValue" AppendDataBoundItems="True" TabIndex="2">
                                                        <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-10" style="padding: 0">
                                                    <div class="col-sm-4" style="padding-left: 0">
                                                        <asp:TextBox ID="txtGivenName3" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="funIsAlphaNumericWithSpace();"
                                                            MaxLength="50" TabIndex="2" onblur="CheckSpaces();return false;" placeholder="First Name">
                                                        </asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-4" style="padding-left: 0">
                                                        <asp:TextBox ID="txtMiddleName3" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="funIsAlphaNumericWithSpace();"
                                                            MaxLength="50" TabIndex="2" onblur="CheckSpaces();return false;" placeholder="Middle Name">
                                                        </asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-4" style="padding-left: 0">
                                                        <asp:TextBox ID="txtLastName3" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="funIsAlphaNumericWithSpace();"
                                                            MaxLength="50" TabIndex="2" onblur="CheckSpaces();return false;" placeholder="Last Name">
                                                        </asp:TextBox>
                                                    </div>
                                                    <asp:HiddenField ID="HiddenField3" runat="server"></asp:HiddenField>
                                                    <asp:HiddenField ID="HiddenField4" runat="server"></asp:HiddenField>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row" style="margin-top: 4px">

                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lbldob" Text="DOB" runat="server" CssClass="control-label"></asp:Label>
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
                                                <asp:Label ID="lblGender" Text="Gender" runat="server" CssClass="control-label"></asp:Label>
                                                <span style="color: red">*</span>

                                                <%-- <span style="color: #ff0000">*</span>--%>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:UpdatePanel ID="upcboGender" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="cboGender" runat="server" CssClass="form-control" TabIndex="2">
                                                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                             <div class="col-sm-3" style="display:none">
                                                <asp:TextBox CssClass="form-control" Visible="false"
                                                    runat="server" ID="txtPanNo" MaxLength="11" TabIndex="2" Font-Bold="false" />
                                            </div>                                            
                                        </div>
                                        <div class="row">
                                             <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblNationality" Text="Nationality" runat="server" CssClass="control-label" ></asp:Label>
                                                <span style="color: red">*</span>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                    <ContentTemplate> 
                                                        <asp:DropDownList ID="ddlNationality" runat="server"  CssClass="form-control" TabIndex="2" >
                                                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <%--<div class="row">
                                           
                                        </div>--%>
                                        <div class="row" style="margin-top: 4px">
                                            <div class="col-sm-3" style="text-align: left">
                                                <%--Added by shreela on 6/03/14 to remove space--%>
                                                <asp:Label ID="lblMarStatus" Text="Marital Status" runat="server" CssClass="control-label">
                                                </asp:Label>
                                                <span style="display: none; color: red">*</span>

                                                <%-- <span style="color: #ff0000">*</span>--%>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:UpdatePanel ID="upMaritalStatus" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlMaritalStatus" runat="server" CssClass="form-control" TabIndex="2">
                                                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
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
                                                        <asp:DropDownList ID="ddlCitizenship" runat="server" CssClass="form-control" TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddlCitizenship_SelectedIndexChanged">
                                                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 4px">

                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:UpdatePanel ID="uplblIsoCountryCodeOthr" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lblIsoCountryCodeOthr" Text="ISO 3166 Country Code" Visible="false"
                                                            runat="server" CssClass="control-label"></asp:Label>
                                                        <span id="spnISOCntryCode" runat="server" visible="false" style="color: red">*</span>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-sm-3">

                                                <asp:UpdatePanel ID="upIsoCountryCodeOthr" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlIsoCountryCodeOthr" runat="server" CssClass="form-control" AutoPostBack="true" TabIndex="2" Visible="false">
                                                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 4px">
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblResStatus" Text="Residential Status" runat="server" CssClass="control-label">
                                                </asp:Label>
                                                <span style="color: red">*</span>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:UpdatePanel ID="upResStatus" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlResStatus" runat="server" CssClass="form-control" TabIndex="2">
                                                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
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
                                                        <asp:DropDownList ID="ddlOccupation" OnSelectedIndexChanged="ddlOccupation_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control"
                                                            TabIndex="2">
                                                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
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

                                        <div class="row" style="margin-top: 4px">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-sm-3" style="text-align: left" id="divOccuSubType" runat="server">
                                                        <asp:Label ID="lblOccuSubType" Text="Occupation Sub Type" runat="server" CssClass="control-label">
                                                        </asp:Label>
                                                        <span style="color: red">*</span>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="ddlOccuSubType" runat="server" CssClass="form-control" TabIndex="2">
                                                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>


                                    </div>
                                </div>

                                <%--  Added for Personal Details end --%>
                                <%--  Added for Tick If Applicable start --%>
                                <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                                    <div id="Div1" runat="server" class="panel-heading subheader" style="background-color: #EDF1cc !important" onclick="ShowReqDtl1('div3','Span1');return false;">
                                        <div class="row">
                                            <div class="col-sm-10" style="text-align: left">
                                                <span class="glyphicon glyphicon-menu-hamburger"></span>
                                                <asp:Label ID="Label1" Text="TICK IF APPLICABLE" runat="server" CssClass="control-label">
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
                                        ADDITIONAL DETAILS REQUIRED <%--(Mandatory only if section 2 is ticked)--%>
                                        <span id="spnAddDtls" runat="server" visible="false" style="color: red">*</span>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-12" style="text-align: left">
                                                <asp:CheckBox ID="chkTick" Text="RESIDENCE FOR TAX PURPOSES IN JURISDICTION(S) OUTSIDE INDIA" AutoPostBack="true" OnCheckedChanged="chkTick_Checked"
                                                    CssClass="standardcheckbox" runat="server" TabIndex="2" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblIsoCountryCode2" Text="ISO 3166 Country Code of Jurisdiction of Residence"
                                                    runat="server" CssClass="control-label"></asp:Label>
                                                <span id="spnISO3166" runat="server" visible="false" style="color: red">*</span>
                                            </div>
                                            <div class="col-sm-3">
                                                <%-- <asp:textbox cssclass="form-control" onmousedown="$('#txtDob').datepicker({ changeMonth: true, changeYear: true });"
                                    onchange="setDateFormat('txtDob')" runat="server" id="txtIsoCountryCode2" maxlength="15"
                                    tabindex="12" enabled="true" />--%>
                                                <asp:DropDownList ID="ddlIsoCountryCode2" runat="server" CssClass="form-control" AutoPostBack="true" TabIndex="2">
                                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblTaxIden" Text="Tax Identification Number or equivalent (if issued by jurisdiction)"
                                                    runat="server" CssClass="control-label"></asp:Label>
                                                <span id="spnTINNo" runat="server" visible="false" style="color: red">*</span>

                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox CssClass="form-control" placeholder="TIN Number" onkeypress="fncInputNumericValuesWithHyphenOnly();" onblur="tinvalidation(this);"
                                                    runat="server" ID="txtIDResTax" MaxLength="11" TabIndex="2" Font-Bold="false" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblPlace" Text="Place / City of Birth" runat="server" CssClass="control-label">
                                                </asp:Label>
                                                <span id="spnPlaceOfBirth" runat="server" visible="false" style="color: red">*</span>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox CssClass="form-control" placeholder="Place Of Birth" onkeypress="fncInputcharacterOnlyNew();"
                                                    runat="server" ID="txtDOBRes" MaxLength="15"
                                                    TabIndex="2" />
                                            </div>
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblIsoContry" Text="ISO 3166 Country Code of Birth" runat="server"
                                                    CssClass="control-label"></asp:Label>
                                                <span id="spnISOCntryCodeBrth" runat="server" visible="false" style="color: red">*</span>
                                            </div>
                                            <div class="col-sm-3">

                                                <asp:DropDownList ID="ddlIsoCountry" runat="server" CssClass="form-control" AutoPostBack="true" TabIndex="2">
                                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                    </div>
                                </div>
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
                                <asp:DropDownList ID="ddlProofIdentity" runat="server" CssClass="form-control" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlProofIdentity_SelectedIndexChanged" TabIndex="2">
                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>
                        <div id="divIdProof" runat="server" class="row">
                            <div id="divPassNo" runat="server" class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblPassportNo" Placeholder="Passport Number" runat="server" CssClass="control-label"></asp:Label>
                                <span><font color="red">*</font></span>
                            </div>
                            <div id="divPassNotxt" runat="server" class="col-sm-3">
                                <asp:TextBox CssClass="form-control" runat="server"
                                    onChange="javascript:this.value=this.value.toUpperCase();" onkeypress="fncInputDocsKeyValidate();" ID="txtPassNo" MaxLength="15" TabIndex="2" />
                                <%--<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server"
                                                    FilterType="Numbers,UppercaseLetters,LowercaseLetters" TargetControlID="txtPassNo">
                                                </ajaxToolkit:FilteredTextBoxExtender>--%>
                            </div>

                            <div id="divPass" runat="server" class="col-sm-3" style="text-align: left">
                                <asp:Label ID="llPassExpDate" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>

                            <div id="divPassDate" class="col-sm-3">
                                <div class="input-group">
                                    <asp:TextBox ID="txtPassExpDate" runat="server" CssClass="form-control" MaxLength="10" TabIndex="2"></asp:TextBox>

                                    <div id="hidePassExpDate" runat="server" class="input-group-btn">
                                        <div class="btn btn-primary btn-lg-kmi" onclick="callCalender1()">
                                            <span class="glyphicon glyphicon-calendar BtnGlyphicon"></span>
                                        </div>
                                    </div>
                                </div>
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtPassOthr" onkeypress="funIsAlphaNumericWithoutSpace();" MaxLength="15" TabIndex="2" />
                            </div>


                            <%--<div id="divPass" runat="server" class="col-sm-3" style="text-align: left">
                                <asp:Label ID="llPassExpDate" runat="server" CssClass="control-label"></asp:Label>
                                <span><font color="red">*</font></span>
                            </div>
                            <div id="divPassDate" runat="server" class="col-sm-3">
                                <asp:TextBox CssClass="form-control" onmousedown="$('#EmptyPagePlaceholder_txtPassExpDate').datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd-mm-yy' });"
                                    runat="server" ID="txtPassExpDate" MaxLength="15" TabIndex="2" />
                                <asp:TextBox CssClass="form-control" runat="server"
                                    ID="txtPassOthr" MaxLength="15" TabIndex="2" />
                            </div>--%>
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
                            <div class="col-sm-2" onclick="showHideDiv('menu3','Span9');return false;">
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
                                                <asp:CheckBox ID="chkPerAddress" Text="CURRENT / PERMANENT / OVERSEAS ADDRESS DETAILS" AutoPostBack="true" OnCheckedChanged="chkPerAddress_Checked"
                                                    runat="server" Checked="true" Enabled="false" CssClass="standardcheckbox" TabIndex="2" />
                                                <span style="color: red">*</span>

                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12" style="text-align: left">
                                                <asp:CheckBox ID="CheckBox1" Text="Proof Of Address Same as Above Proof Of Identity" OnCheckedChanged="SameIdentityProof_CheckedChanged"
                                                    AutoPostBack="true" runat="server"
                                                    CssClass="standardcheckbox" TabIndex="2" />
                                                <%--<span style="color: red">*</span>--%>
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
                                                        <asp:DropDownList ID="ddlAddressType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlAddressType_SelectedIndexChanged"
                                                            TabIndex="2">
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
                                                <asp:DropDownList ID="ddlProofOfAddress" runat="server" CssClass="form-control" AutoPostBack="true"
                                                    TabIndex="2">
                                                    <%--OnSelectedIndexChanged="ddlProofOfAddress_SelectedIndexChanged"--%>
                                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div id="divAddProof" runat="server" class="row" style="margin-top: 4px">
                                            <div id="divPassNoAdd" runat="server" class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblPassportNoAdd" Placeholder="Passport Number" runat="server" CssClass="control-label"></asp:Label>
                                                <span style="color: red">*</span>
                                            </div>

                                            <div id="divPassNotxtAdd" runat="server" class="col-sm-3">
                                                <asp:TextBox CssClass="form-control" runat="server"
                                                    ID="txtPassNoAdd" MaxLength="15" />
                                            </div>

                                            <div id="divPassAdd" runat="server" class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="llPassExpDateAdd" runat="server" CssClass="control-label"></asp:Label>
                                                <span style="color: red">*</span>
                                            </div>

                                            <div id="divPassDateAdd" class="col-sm-3">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtPassExpDateAdd" runat="server" CssClass="form-control" MaxLength="10" TabIndex="2"></asp:TextBox>

                                                    <div id="hidetxtPassExpDateAdd" runat="server" class="input-group-btn">
                                                        <div class="btn btn-primary btn-lg-kmi" onclick="callCalender5()">
                                                            <span class="glyphicon glyphicon-calendar BtnGlyphicon"></span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <asp:TextBox CssClass="form-control" runat="server" ID="txtPassOthrAdd" MaxLength="15" TabIndex="2" />
                                            </div>

                                            <%-- <div id="divPassAdd" runat="server" class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="llPassExpDateAdd" runat="server" CssClass="control-label"></asp:Label>
                                                <span style="color: red">*</span>
                                            </div>
                                            <div id="divPassDateAdd" runat="server" class="col-sm-3">


                                                <asp:TextBox CssClass="form-control" onmousedown="$('#EmptyPagePlaceholder_txtPassExpDateAdd').datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd-mm-yy' });"
                                                    runat="server" ID="txtPassExpDateAdd" MaxLength="15" />


                                                <asp:TextBox CssClass="form-control" runat="server"
                                                    ID="txtPassOthrAdd" MaxLength="15" TabIndex="2" />
                                            </div>--%>
                                        </div>

                                        <div class="row" style="margin-top: 4px">
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblAddressLine1" Text="Address Line1" runat="server" CssClass="control-label"></asp:Label>
                                                <span style="color: red">*</span>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtDrivLicNum')" runat="server"
                                                    ID="txtAddressLine1" placeholder="Line 1" MaxLength="300" TabIndex="2" />
                                            </div>
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblAddressLine2" Text="Address Line2" runat="server" CssClass="control-label"></asp:Label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtAddressLine2')"
                                                    runat="server" ID="txtAddressLine2" placeholder="Line 2" MaxLength="300" TabIndex="2" />
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 4px">
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblAddressLine3" Text="Address Line3" runat="server" CssClass="control-label"></asp:Label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtDrivLicNum')" runat="server"
                                                    ID="txtAddressLine3" placeholder="Line 3" MaxLength="300" TabIndex="2" />
                                            </div>
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblCity" Text="City / Town / Village" runat="server" CssClass="control-label"></asp:Label>
                                                <span style="color: red">*</span>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtCity" placeholder="City / Town/ Village Name" runat="server" onkeypress="fncInputcharacterOnlyNew();" CssClass="form-control" TabIndex="2">
                                                </asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 4px">
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblState" Text="State / U.T Code" runat="server" CssClass="control-label"></asp:Label>
                                                <span style="color: red">*</span>
                                            </div>
                                            <div class="col-sm-3">
                                                <div id="dvState" runat="server" class="input-group">
                                                    <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" TabIndex="2">
                                                        <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:HiddenField runat="server" ID="hdnddlPinCode" />
                                                    <div class="input-group-btn">
                                                        <asp:LinkButton runat="server" ID="btnShow" CssClass="btn btn-primary btn-lg-kmi" title="Search" data-toggle="tooltip" OnClick="GetModelData" TabIndex="2">
                                                        <span class="glyphicon glyphicon-search BtnGlyphicon"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <asp:TextBox ID="txtState" runat="server" CssClass="form-control" MaxLength="100" Visible="false"></asp:TextBox>
                                            </div>



                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblPinCode" Text="Pin / Post Code" runat="server" CssClass="control-label"></asp:Label>
                                                <span style="color: red">*</span>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtPinCode" placeholder="Pin / Post Code" MaxLength="10" onkeypress="fncInputNumericValuesOnly();" runat="server" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 4px">

                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblDistrict" Text="District" runat="server" CssClass="control-label"></asp:Label>
                                                <span style="color: red">*</span>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox runat="server" placeholder="District Name" ID="txtDistrictname" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                            </div>

                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblIsoCountryCode" Text="ISO 3166 Country Code" runat="server" CssClass="control-label"></asp:Label>
                                                <span style="color: red">*</span>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlCountryCode" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCountryCode_SelectedIndexChanged" TabIndex="2">
                                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div style="margin-top: 25px; margin-bottom: 25px">
                                            <div class="row">
                                                <div class="col-sm-12" style="text-align: left">
                                                    <asp:CheckBox ID="chkLocalAddress" Text="CORRESPONDENCE / LOCAL ADDRESS DETAILS" runat="server" AutoPostBack="true" OnCheckedChanged="chkLocalAddress_CheckedChanged"
                                                        CssClass="standardcheckbox" TabIndex="2" />
                                                    <span style="color: red">*</span>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12" style="text-align: left">

                                                    <asp:CheckBox ID="chkCuurentAddress" Text="Same as Current / Permanent / Overseas Address details"
                                                        OnCheckedChanged="chkCuurentAddress_Checked" AutoPostBack="true" runat="server"
                                                        CssClass="standardcheckbox" TabIndex="2" />
                                                    <%--<span style="color: red">*</span>--%>
                                                </div>
                                            </div>
                                            <div class="row" style="margin-top: 4px">
                                                <div class="col-sm-3" style="text-align: left">
                                                    <asp:Label ID="lblAddressType1" Text="Address Type" runat="server" CssClass="control-label"></asp:Label>
                                                    <span style="color: red">*</span>
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlAddressType1" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlAddressType1_SelectedIndexChanged"
                                                                TabIndex="2">
                                                                <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>

                                                <%--<div class="col-sm-3" style="text-align: left">
                                                    <asp:Label ID="lblProofOfAddress1" Text="Proof Of Address" runat="server" CssClass="control-label"></asp:Label>
                                                    <span style="color: red">*</span>
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="ddlProofOfAddress1" runat="server" CssClass="form-control"
                                                        TabIndex="2">
                                                        <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>--%>
                                            </div>
                                            <div class="row" style="margin-top: 4px">
                                                <div class="col-sm-3" style="text-align: left">
                                                    <asp:Label ID="lblLocAddLine1" Text="Address Line 1" runat="server" CssClass="control-label"></asp:Label>
                                                    <span style="color: red">*</span>
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:TextBox CssClass="form-control" runat="server"
                                                        ID="txtLocAddLine1" MaxLength="55" TabIndex="2" />
                                                </div>
                                                <div class="col-sm-3" style="text-align: left">
                                                    <asp:Label ID="lblLocAddLine2" Text="Address Line 2" runat="server" CssClass="control-label"></asp:Label>
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:TextBox CssClass="form-control" runat="server"
                                                        ID="txtLocAddLine2" MaxLength="55" TabIndex="2" />
                                                </div>
                                            </div>
                                            <div class="row" style="margin-top: 4px">
                                                <div class="col-sm-3" style="text-align: left">
                                                    <asp:Label ID="lblLocAddLine3" Text="Address Line 3" runat="server" CssClass="control-label"></asp:Label>
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:TextBox CssClass="form-control" runat="server"
                                                        ID="txtLocAddLine3" MaxLength="55" TabIndex="2" />
                                                </div>
                                                <div class="col-sm-3" style="text-align: left">
                                                    <asp:Label ID="lblCity1" Text="City / Town / Village" runat="server" CssClass="control-label"></asp:Label>
                                                    <span style="color: red">*</span>
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtCity1" runat="server" CssClass="form-control" onkeypress="fncInputcharacterOnlyNew();" TabIndex="2" MaxLength="50"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 4px">
                                                <div class="col-sm-3" style="text-align: left">
                                                    <asp:Label ID="lblState1" Text="State / U.T Code" runat="server" CssClass="control-label"></asp:Label>
                                                    <span style="color: red">*</span>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div id="dvState1" runat="server" class="input-group">
                                                        <asp:DropDownList ID="ddlState1" runat="server" CssClass="form-control" TabIndex="2">
                                                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:HiddenField runat="server" ID="hdnddlPinCode1" />
                                                        <div class="input-group-btn">
                                                            <asp:LinkButton runat="server" ID="btnsearchddlPinCode1" CssClass="btn btn-primary btn-lg-kmi" title="Search" data-toggle="tooltip" OnClick="GetModelData1" TabIndex="2">
                                            <span class="glyphicon glyphicon-search BtnGlyphicon"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <asp:TextBox ID="txtState1" runat="server" CssClass="form-control" MaxLength="100" Visible="false"></asp:TextBox>
                                                </div>



                                                <div class="col-sm-3" style="text-align: left">
                                                    <asp:Label ID="lblPin1" Text="Pin / Post Code" runat="server" CssClass="control-label"></asp:Label>
                                                    <span><font color="red">*</font></span>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="">
                                                        <asp:TextBox ID="ddlPinCode1" MaxLength="10" onkeypress="fncInputNumericValuesOnly();" runat="server" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                                        <asp:HiddenField runat="server" ID="HiddenField5" />

                                                    </div>

                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 4px">
                                                <div class="col-sm-3" style="text-align: left">
                                                    <asp:Label ID="lblDistrict1" Text="District" runat="server" CssClass="control-label"></asp:Label>
                                                    <span><font color="red">*</font></span>
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox runat="server" ID="txtDistrict1" CssClass="form-control"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>

                                                <div class="col-sm-3" style="text-align: left">
                                                    <asp:Label ID="lblCountryCode1" Text="ISO 3166 Country Code" runat="server" CssClass="control-label"></asp:Label>
                                                    <span style="color: red">*</span>
                                                </div>
                                                <div class="col-sm-3">
                                                    <%--  <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtState')" runat="server"
                                                    ID="txtCountryCode1" MaxLength="15" TabIndex="12"  Enabled="false"/>--%>
                                                    <asp:DropDownList ID="ddlCountryCode1" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCountryCode1_SelectedIndexChanged"
                                                        TabIndex="2">
                                                        <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12" style="text-align: left">
                                                <asp:CheckBox ID="chkAddResident" Text="ADDRESS IN THE JURISDICTION DETAILS WHERE ENTITY IS RESIDENT OUTSIDE INDIA FOR TAX PURPOSES" AutoPostBack="true" OnCheckedChanged="chkAddResident_CheckedChanged"
                                                    runat="server" CssClass="standardcheckbox" TabIndex="2" />
                                                <span style="color: red">*</span>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6" style="text-align: left">
                                                <asp:CheckBox ID="chkCurrentAdd" Text="Same as Current / Permanent / Overseas Address details"
                                                    TabIndex="2" OnCheckedChanged="chkCurrentAdd_Checked" AutoPostBack="true" runat="server"
                                                    CssClass="standardcheckbox" />
                                                <%--<span style="color: red">*</span>--%>
                                            </div>
                                            <div class="col-sm-6" style="text-align: left">
                                                <asp:CheckBox ID="chkCorresAdd" Text="Same as Correspondance / Local Address details"
                                                    TabIndex="2" runat="server" AutoPostBack="true" OnCheckedChanged="chkCorresAdd_Checked"
                                                    CssClass="standardcheckbox" /><%--OnCheckedChanged="chkCorresAdd_Checked"--%>
                                                <%--<span style="color: red">*</span>--%>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 4px">
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblAddressType2" Text="Address Type" runat="server" CssClass="control-label"></asp:Label>
                                                <span style="color: red">*</span>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlAddressType2" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlAddressType2_SelectedIndexChanged"
                                                            TabIndex="2">
                                                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <%-- <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblProofOfAddress2" Text="Proof Of Address" runat="server" CssClass="control-label"></asp:Label>
                                                <span style="color: red">*</span>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlProofOfAddress2" runat="server" CssClass="form-control"
                                                    TabIndex="2">
                                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>--%>
                                        </div>
                                        <div class="row" style="margin-top: 4px">
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblAddLine1" Text="Address Line 1" runat="server" CssClass="control-label"></asp:Label>
                                                <span style="color: red">*</span>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox CssClass="form-control" runat="server"
                                                    ID="txtAddLine1" MaxLength="55" TabIndex="2" />
                                            </div>
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblAddLine2" Text="Address Line 2" runat="server" CssClass="control-label"></asp:Label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox CssClass="form-control" runat="server"
                                                    ID="txtAddLine2" MaxLength="55" TabIndex="2" />
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 4px">
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblAddLine3" Text="Address Line 3" runat="server" CssClass="control-label"></asp:Label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox CssClass="form-control" runat="server"
                                                    ID="txtAddLine3" MaxLength="55" TabIndex="2" />
                                            </div>
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblCity2" Text="City / Town / Village" runat="server" CssClass="control-label"></asp:Label>
                                                <span style="color: red">*</span>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtCity2" runat="server" CssClass="form-control" onkeypress="fncInputcharacterOnlyNew();" TabIndex="2" MaxLength="50"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 4px">

                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblState2" Text="State / U.T Code" runat="server" CssClass="control-label"></asp:Label>
                                                <span style="color: red">*</span>
                                            </div>
                                            <div class="col-sm-3">
                                                <div id="dvState2" runat="server" class="input-group">
                                                    <asp:DropDownList ID="ddlState2" runat="server" CssClass="form-control" TabIndex="2">
                                                        <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:HiddenField runat="server" ID="hdnddlPinCode2" />
                                                    <div class="input-group-btn">
                                                        <asp:LinkButton runat="server" ID="btnsearchddlPinCode2" CssClass="btn btn-primary btn-lg-kmi" title="Search" data-toggle="tooltip" OnClick="GetModelData2" TabIndex="2">
                                    <span class="glyphicon glyphicon-search BtnGlyphicon"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <asp:TextBox ID="txtState2" runat="server" CssClass="form-control" MaxLength="100" Visible="false"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblPin2" Text="Pin / Post Code" runat="server" CssClass="control-label"></asp:Label>
                                                <span><font color="red">*</font></span>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="">
                                                    <asp:TextBox ID="ddlPinCode2" runat="server" MaxLength="10" onkeypress="fncInputNumericValuesOnly();" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                                    <asp:HiddenField runat="server" ID="HiddenField6" />

                                                </div>


                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 4px">
                                            <%-- <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblState2" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <div class="input-group">
                                <asp:DropDownList ID="ddlState2" runat="server" CssClass="form-control" TabIndex="73">
                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                </asp:DropDownList>
                                    <asp:HiddenField runat="server" ID="hdnddlPinCode2" />
                                    <div class="input-group-btn">
                                    <asp:LinkButton runat="server" ID="btnsearchddlPinCode2" CssClass="btn btn-primary btn-lg-kmi"  title="Search" data-toggle="tooltip" OnClick="GetModelData2">
                                    <span class="glyphicon glyphicon-search BtnGlyphicon"></span>
                                    </asp:LinkButton>
                                 </div>
                                 </div>
                            </div>--%>

                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblDistrict2" Text="District" runat="server" CssClass="control-label"></asp:Label>
                                                <span><font color="red">*</font></span>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtDistrict2" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>

                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblIsoCountry2" Text="ISO 3166 Country Code" runat="server" CssClass="control-label"></asp:Label>
                                                <span style="color: red">*</span>
                                            </div>
                                            <div class="col-sm-3">
                                                <%-- <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtIsoCountryCode')"
                                                      runat="server" ID="txtIsoCountryCode" MaxLength="15" TabIndex="12" />--%>
                                                <asp:DropDownList ID="ddlIsoCountryCode" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlIsoCountryCode_SelectedIndexChanged"
                                                    TabIndex="2">
                                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%-- Added for Proof of Address end--%>

                                <%--  Added for Contact Details start--%>
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
                                                        <asp:TextBox ID="txtTelOff" runat="server" CssClass="form-control" MaxLength="4"></asp:TextBox>
                                                    </span>
                                                    <asp:TextBox ID="txtTelOff2" runat="server" CssClass="form-control" onkeypress="fncInputNumericValuesOnly();"
                                                        MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblTelRes" runat="server" CssClass="control-label" Text="Tel. (Res)"></asp:Label>
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
                                                <asp:Label ID="lblMobile" runat="server" Text="Mobile" CssClass="control-label"></asp:Label>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="input-group">
                                                    <span class="input-group-addon " style="width: 20% !important; border-top-left-radius: 7% !important; padding: 0px !important; border: 0px !important;">
                                                        <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" onkeypress="fncInputNumericValuesOnly();"
                                                            MaxLength="2" Style="border-top-left-radius: 4px; border-bottom-left-radius: 4px;"></asp:TextBox>
                                                    </span>
                                                    <asp:TextBox ID="txtMobile2" runat="server" CssClass="form-control" onkeypress="fncInputNumericValuesOnly();"
                                                        MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblFax" runat="server" Text="FAX" CssClass="control-label"></asp:Label>
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
                                        <asp:CheckBox Style="margin-bottom: -0.5%;" ID="chkAppDeclare1" Text="I/We hereby declare that the details furnished above are true and correct to the best of my/our knowledge and belief and I/we undertake to inform you of "
                                            CssClass="standardcheckbox" runat="server" onchange="setDateFormat('txtRemarks')" Checked="true" Enabled="false"
                                            TabIndex="2" />
                                    </div>
                                    <div class="col-sm-12" style="text-align: left; display: flex; font-weight: bold; padding-left: 2.5%;">
                                        <asp:Label CssClass="control-label" Text="any changes therein, immediately.In case any of the above information is found to be false or untrue or misleading or misrepresenting, I/we am/are aware that I/we may be held liable for it."
                                            runat="server" ID="lblAppDeclare1" maxlength="15" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12" style="text-align: left; display: flex;">
                                        <asp:CheckBox ID="chkAppDeclare2" Text="My/Our personal KYC details may be shared with Central KYC Registry."
                                            CssClass="standardcheckbox" runat="server" onchange="setDateFormat('txtRemarks')" Checked="true" Enabled="false"
                                            TabIndex="2" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12" style="text-align: left; display: flex;">
                                        <asp:CheckBox ID="chkAppDeclare3" Text="I/We hereby consent to receiving information from Central KYC Registry through SMS/Email on the above registered number/email address."
                                            CssClass="standardcheckbox" runat="server" onchange="setDateFormat('txtRemarks')" Checked="true" Enabled="false"
                                            TabIndex="2" />
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
                                    <div class="col-sm-3">
                                        <div class="input-group">
                                            <asp:TextBox ID="txtDate" placeholder="dd-mm-yyyy" runat="server" CssClass="form-control" MaxLength="10" TabIndex="2"></asp:TextBox>
                                            <div class="input-group-btn">
                                                <%--<asp:LinkButton runat="server" ID="LinkButton3" CssClass="btn btn-primary btn-lg-kmi"   onmousedown="checkDateOfIncorp(this);" title="Date" data-toggle="tooltip"  TabIndex="2">--%>
                                                <div class="btn btn-primary btn-lg-kmi" onclick="callCalender2()">
                                                    <span class="glyphicon glyphicon-calendar BtnGlyphicon"></span>
                                                </div>
                                                <%--</asp:LinkButton>--%>
                                            </div>
                                        </div>
                                    </div>

                                    <%-- <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblDate" Text="Date" runat="server" CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left; top: 0px; left: 0px;">
                                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" onmousedown="$(this).datepicker({ maxDate: new Date() ,changeMonth: true, changeYear: true, dateFormat: 'dd-mm-yy' })" MaxLength="10"
                                            TabIndex="2"></asp:TextBox>
                                    </div>--%>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblPlace1" Text="Place" runat="server" CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" placeholder="Place" runat="server"
                                            ID="txtPlace" MaxLength="50" TabIndex="2" onkeypress="fncInputcharacterOnlyNew();" />
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
                                    <div class="col-sm-3">
                                      <asp:DropDownList ID="ddlDocReceived" runat="server" CssClass="form-control" TabIndex="2">
                                            </asp:DropDownList>
                                    </div>


                                    <%--<div class="col-sm-3" style="display: flex;">
                                        <asp:CheckBox ID="chkSelfCerti" Text="" CssClass="standardcheckbox"
                                            runat="server" TabIndex="2" />
                                        <span>Self-Certified</span>
                                        <asp:CheckBox ID="chkTrueCopies" Text="" Style="margin-left: 4px;" CssClass="standardcheckbox"
                                            runat="server" TabIndex="2" />
                                        <span>True Copies</span>
                                        <asp:CheckBox ID="chkNotary" Text="" Style="margin-left: 5px;" CssClass="standardcheckbox"
                                            runat="server" TabIndex="2" />
                                        <span>Notary</span>
                                    </div>--%>


                                    <%-- <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblRiskCategory" Text="Risk Category" runat="server" Font-Bold="true"
                                            CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:CheckBox ID="chkHigh" Text="" CssClass="standardcheckbox" OnCheckedChanged="chkHigh_CheckedChanged"
                                            AutoPostBack="true" runat="server" TabIndex="2" />
                                        <span>High</span>
                                        <asp:CheckBox ID="chkMedium" Text="" CssClass="standardcheckbox" OnCheckedChanged="chkMedium_CheckedChanged"
                                            AutoPostBack="true" runat="server" TabIndex="2" />
                                        <span>Medium</span>
                                        <asp:CheckBox ID="chkLow" Text="" CssClass="standardcheckbox" OnCheckedChanged="chkLow_CheckedChanged"
                                            AutoPostBack="true" runat="server" TabIndex="2" />
                                        <span>Low</span>
                                    </div>--%>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-sm-12" style="text-align: center">
                                        <asp:Label ID="lblKYCVerify" Style='text-align: center' CssClass="control-label"
                                            Font-Bold="true" Text="KYC VERIFICATION CARRIED OUT BY" runat="server" />
                                    </div>
                                </div>
                                <br />
                                <div class="row" style="margin-top: 17px">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblIdVerif" Text="Identity Verification" runat="server" CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left; top: 0px; left: 0px;">
                                        <asp:CheckBox ID="chkDone" Text="" CssClass="standardcheckbox" Checked="true" Enabled="false"
                                            runat="server" TabIndex="2" />
                                        <span>Done</span>
                                    </div>

                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblDate3" Text="Date" runat="server" CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="input-group">
                                            <asp:TextBox ID="txtDate3" runat="server" CssClass="form-control" MaxLength="10" TabIndex="2"></asp:TextBox>
                                            <div class="input-group-btn">
                                                <%--<asp:LinkButton runat="server" ID="LinkButton3" CssClass="btn btn-primary btn-lg-kmi"   onmousedown="checkDateOfIncorp(this);" title="Date" data-toggle="tooltip"  TabIndex="2">--%>
                                                <div class="btn btn-primary btn-lg-kmi" onclick="callCalender3()">
                                                    <span class="glyphicon glyphicon-calendar BtnGlyphicon"></span>
                                                </div>
                                                <%--</asp:LinkButton>--%>
                                            </div>
                                        </div>
                                    </div>

                                    <%--<div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblDate3" Text="Date" runat="server" CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left; top: 0px; left: 0px;">
                                        <asp:TextBox ID="txtDate3" runat="server" CssClass="form-control" onmousedown="$(this).datepicker({ maxDate: new Date() ,changeMonth: true, changeYear: true, dateFormat: 'dd-mm-yy' });" MaxLength="10" TabIndex="2"></asp:TextBox><%--TextMode="Date"
                                        </div>--%>
                                </div>
                                <div class="row" style="margin-top: 17px">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblEmpName" Text="Employee Name" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" runat="server" placeholder="Employee Name" ID="txtEmpName" MaxLength="50"
                                            onkeypress="fncInputValidateName();" Enabled="true" TabIndex="2" />
                                        <br />
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblEmpCode" Text="Employee Code" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" runat="server" placeholder="Employee Code" ID="txtEmpCode" MaxLength="50"
                                            Enabled="true" TabIndex="2" onkeypress="funIsAlphaNumericWithoutSpace();" />
                                    </div>

                                </div>
                                <div class="row" style="margin-top: 17px">
                                    <div class="col-sm-3" style="text-align: left; margin-top: -9px">
                                        <asp:Label ID="lblEmpDesignation" Text="Employee Designation" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3" style="margin-top: -13px">
                                        <asp:TextBox CssClass="form-control" runat="server" onkeypress="fncInputValidateDesignation();" placeholder="Employee designation" ID="txtEmpDesignation" MaxLength="50"
                                            Enabled="true" TabIndex="2" />
                                        <%--<br/>--%>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left; margin-top: -9px">
                                        <asp:Label ID="lblEmpBranch" Text="Employee Branch" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3" style="margin-top: -13px">
                                        <asp:TextBox CssClass="form-control" onkeypress="fncInputcharacterOnlyNew();" placeholder="Employee Branch" runat="server" ID="txtEmpBranch" MaxLength="50"
                                            Enabled="true" TabIndex="2" />
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
                                        <asp:TextBox CssClass="form-control" runat="server" placeholder="Institution Name" 
                                            ID="txtInsName" MaxLength="150" Enabled="true" TabIndex="2" />  <%--onkeypress="fncInputValidateNameNew();"--%>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblInsCode" Text="Code" runat="server" CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" runat="server" placeholder="Institution Code" ID="txtInsCode" MaxLength="6" onkeypress="funIsAlphaNumericWithoutSpace();"
                                            Enabled="true" TabIndex="2" />
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

        <div class="row" style="margin-top: 12px;">
            <div class="col-sm-12" align="center">
                <asp:LinkButton id="btnPSUpdate" runat="server" cssclass="btn-animated bg-green"
                    Visible="false" onclick="btnPSUpdate_Click" > <span class="glyphicon glyphicon-floppy-disk BtnGlyphicon"></span> Partial Update </asp:LinkButton>

                <asp:LinkButton id="btnUpdate" runat="server" cssclass="btn-animated bg-green"
                    Visible="false" onclick="btnUpdate_Click" > <span class="glyphicon glyphicon-floppy-disk BtnGlyphicon"></span> Update </asp:LinkButton>

               <%--    <asp:linkbutton id="btnKYCUpdate" text="UPDATE" runat="server" cssclass="btn btn-primary"
                    visible="false" causesvalidation="false" onclick="btnKYCUpdate_Click" tabindex="109"> <span class="glyphicon glyphicon-floppy-disk BtnGlyphicon"> Update</span> </asp:linkbutton>

              <asp:LinkButton ID="btnSave" OnClientClick="return funCKYC();" OnClick="btnSave_Click"
                                                CssClass="btn btn-primary" runat="server" TabIndex="109">
                                                <asp:HiddenField ID="TabName" runat="server" />
                                                <span class="glyphicon glyphicon-floppy-disk BtnGlyphicon"></span> Save
                                            </asp:LinkButton>--%>


                <asp:LinkButton ID="btnAdd" OnClick="btnAdd_Click" CssClass="btn-animated bg-green" runat="server" TabIndex="2">
                                               <span class="glyphicon glyphicon-plus BtnGlyphicon"></span> Add</asp:LinkButton>

                <%--  <asp:LinkButton ID="btnPartialAdd" OnClick="btnPartialAdd_Click"  CssClass="btn btn-primary" runat="server" TabIndex="109">
                                               <span class="glyphicon glyphicon-floppy-disk BtnGlyphicon"></span>Partial Add</asp:LinkButton>--%>


                <asp:LinkButton ID="btnPartialAdd" OnClick="btnPartialAdd_Click" CssClass="btn-animated bg-green"
                    runat="server" TabIndex="110">
                             <span class="glyphicon glyphicon-plus BtnGlyphicon"> </span> Partial Add </asp:LinkButton>

                <div id="divloader" runat="server" style="display: none;">
                    <img id="Img1" alt="" src="Common/Images/spinner.gif" runat="server" />
                    Loading...
                </div>
            </div>
        </div>
    </div>

    <input id="hdnUpdate" type="hidden" runat="server" />
    <asp:HiddenField ID="hdnUserId" runat="server" />
    <%-- <asp:panel runat="server" id="pnlMdl" width="500" display="none">
        <iframe runat="server" id="ifrmMdlPopup" width="610px" height="300px" frameborder="0"
            display="none"></iframe>
        <%--<asp:label runat="server" ID="lblMsg1" Text="Hi This Is PopUp Label"/>
    </asp:panel>--%>
    <asp:Label runat="server" ID="lbl1" Style="display: none" />
    <%--<ajaxToolkit:ModalPopupExtender runat="server" ID="mdlView" BehaviorID="mdlViewBID"
        DropShadow="false" TargetControlID="lbl1" PopupControlID="pnlMdl" BackgroundCssClass="modalPopupBg"
        X="260" Y="100">
    </ajaxToolkit:ModalPopupExtender>--%>
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
                        <span class="glyphicon glyphicon-ok  BtnGlyphicon"></span>OK</button>
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
                    <asp:LinkButton ID="btnYesConfirmYesNo" type="button" class="btn-animated bg-green" runat="server" OnClick="ConfirmYes"><span class="glyphicon glyphicon-ok  BtnGlyphicon"></span> Yes</asp:LinkButton>
                    <asp:LinkButton ID="btnNoConfirmYesNo" type="button" class="btn-animated bg-horrible" runat="server" OnClick="ConfirmNo"><span class="glyphicon glyphicon-remove BtnGlyphicon"></span> No</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>

    <script>   
        $(document).ready(function (){
            $('#EmptyPagePlaceholder_cboTitle').on("select" , function () {
                debugger;
                var val = $('#<%= cboTitle.ClientID%>').val();
                if (val == 'MR') {
                    $("#<%=cboGender.ClientID%>").children("option[value='M']").show();
                    $("#<%=cboGender.ClientID%>").children("option[value!='M']").hide();
                } else if (val == 'MRS' || val == 'MS') {
                    $("#<%=cboGender.ClientID%>").children("option[value='F']").show();
                    $("#<%=cboGender.ClientID%>").children("option[ddlAddressType1!='F']").hide();
                } else{
                    $("#<%=cboGender.ClientID%>").children().show();
                }
            })
        })
       
    </script>


</asp:Content>
