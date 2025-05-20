<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="CKYCEntView.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.CKYC.CKYCEntView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/bundles/CKYCValidationjs") %>
    <%: Styles.Render("~/bundles/CKYCcss") %>
    <style>
        .gridview th {
            padding: 3px;
            height: 40px;
            background-color: #d6d6c2;
            color: #337ab7;
            text-align: center !important;
        }

        .left_padding {
            margin-left: 35%;
        }

        th {
            text-align: center;
        }


        .standardcheckbox {
            /*display: inline-flex !important;*/
        }

        input[type=checkbox], input[type=radio] {
            /*margin: 4px 0 0 !important;*/
            /*margin-top: 1px\9;    */
            line-height: normal !important;
            margin-right: 4px !important;
        }

        .container {
            width: 1220px !important;
        }
    </style>
    <script type="text/javascript">

        function funload() {
            document.getElementById('EmptyPagePlaceholder_divloaderqc').style.display = 'block'
        }

        function fncInputcharacterOnlyNew() {
            if (!(event.keyCode == 32 || event.keyCode == 65 || event.keyCode == 66 || event.keyCode == 67 || event.keyCode == 68 || event.keyCode == 69 || event.keyCode == 70 || event.keyCode == 71 || event.keyCode == 72 || event.keyCode == 73 || event.keyCode == 74 || event.keyCode == 75 || event.keyCode == 76 || event.keyCode == 77 || event.keyCode == 78 || event.keyCode == 79 || event.keyCode == 80 || event.keyCode == 81 || event.keyCode == 82 || event.keyCode == 83 || event.keyCode == 84 || event.keyCode == 85 || event.keyCode == 86 || event.keyCode == 87 || event.keyCode == 88 || event.keyCode == 89 || event.keyCode == 90 || event.keyCode == 97 || event.keyCode == 98 || event.keyCode == 99 || event.keyCode == 100 || event.keyCode == 101 || event.keyCode == 102 || event.keyCode == 103 || event.keyCode == 104 || event.keyCode == 105 || event.keyCode == 106 || event.keyCode == 107 || event.keyCode == 108 || event.keyCode == 109 || event.keyCode == 110 || event.keyCode == 111 || event.keyCode == 112 || event.keyCode == 113 || event.keyCode == 114 || event.keyCode == 115 || event.keyCode == 116 || event.keyCode == 117 || event.keyCode == 118 || event.keyCode == 119 || event.keyCode == 120 || event.keyCode == 121 || event.keyCode == 122)) {
                event.returnValue = false;
                //e.preventDefault();
            }
        }

        function funIsAlphaNumericHypenWithoutSpace() {
            //if (((event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode >= 65 && event.keyCode <= 90) || (event.keyCode >= 97 && event.keyCode <= 122)) == false && !(event.keyCode == 45 || event.keyCode == 95)) {
            //    event.returnValue = false;
            //}
        }

        function fncInputValidateDesignation() {
            if (!(event.keyCode == 40 || event.keyCode == 41 || event.keyCode == 46 || event.keyCode == 47 || event.keyCode == 32 || event.keyCode == 65 || event.keyCode == 66 || event.keyCode == 67 || event.keyCode == 68 || event.keyCode == 69 || event.keyCode == 70 || event.keyCode == 71 || event.keyCode == 72 || event.keyCode == 73 || event.keyCode == 74 || event.keyCode == 75 || event.keyCode == 76 || event.keyCode == 77 || event.keyCode == 78 || event.keyCode == 79 || event.keyCode == 80 || event.keyCode == 81 || event.keyCode == 82 || event.keyCode == 83 || event.keyCode == 84 || event.keyCode == 85 || event.keyCode == 86 || event.keyCode == 87 || event.keyCode == 88 || event.keyCode == 89 || event.keyCode == 90 || event.keyCode == 97 || event.keyCode == 98 || event.keyCode == 99 || event.keyCode == 100 || event.keyCode == 101 || event.keyCode == 102 || event.keyCode == 103 || event.keyCode == 104 || event.keyCode == 105 || event.keyCode == 106 || event.keyCode == 107 || event.keyCode == 108 || event.keyCode == 109 || event.keyCode == 110 || event.keyCode == 111 || event.keyCode == 112 || event.keyCode == 113 || event.keyCode == 114 || event.keyCode == 115 || event.keyCode == 116 || event.keyCode == 117 || event.keyCode == 118 || event.keyCode == 119 || event.keyCode == 120 || event.keyCode == 121 || event.keyCode == 122)) {
                event.returnValue = false;
                //e.preventDefault();
            }
        }

        function fncInputValidateName() {
            if (!(event.keyCode == 39 || event.keyCode == 46 || event.keyCode == 32 || event.keyCode == 65 || event.keyCode == 66 || event.keyCode == 67 || event.keyCode == 68 || event.keyCode == 69 || event.keyCode == 70 || event.keyCode == 71 || event.keyCode == 72 || event.keyCode == 73 || event.keyCode == 74 || event.keyCode == 75 || event.keyCode == 76 || event.keyCode == 77 || event.keyCode == 78 || event.keyCode == 79 || event.keyCode == 80 || event.keyCode == 81 || event.keyCode == 82 || event.keyCode == 83 || event.keyCode == 84 || event.keyCode == 85 || event.keyCode == 86 || event.keyCode == 87 || event.keyCode == 88 || event.keyCode == 89 || event.keyCode == 90 || event.keyCode == 97 || event.keyCode == 98 || event.keyCode == 99 || event.keyCode == 100 || event.keyCode == 101 || event.keyCode == 102 || event.keyCode == 103 || event.keyCode == 104 || event.keyCode == 105 || event.keyCode == 106 || event.keyCode == 107 || event.keyCode == 108 || event.keyCode == 109 || event.keyCode == 110 || event.keyCode == 111 || event.keyCode == 112 || event.keyCode == 113 || event.keyCode == 114 || event.keyCode == 115 || event.keyCode == 116 || event.keyCode == 117 || event.keyCode == 118 || event.keyCode == 119 || event.keyCode == 120 || event.keyCode == 121 || event.keyCode == 122)) {
                event.returnValue = false;
                //e.preventDefault();
            }
        }

        function fncInputValidateNameNew() {
            //if (!(event.keyCode == 38 || event.keyCode == 39 || event.keyCode == 46 || event.keyCode == 32 || event.keyCode == 65 || event.keyCode == 66 || event.keyCode == 67 || event.keyCode == 68 || event.keyCode == 69 || event.keyCode == 70 || event.keyCode == 71 || event.keyCode == 72 || event.keyCode == 73 || event.keyCode == 74 || event.keyCode == 75 || event.keyCode == 76 || event.keyCode == 77 || event.keyCode == 78 || event.keyCode == 79 || event.keyCode == 80 || event.keyCode == 81 || event.keyCode == 82 || event.keyCode == 83 || event.keyCode == 84 || event.keyCode == 85 || event.keyCode == 86 || event.keyCode == 87 || event.keyCode == 88 || event.keyCode == 89 || event.keyCode == 90 || event.keyCode == 97 || event.keyCode == 98 || event.keyCode == 99 || event.keyCode == 100 || event.keyCode == 101 || event.keyCode == 102 || event.keyCode == 103 || event.keyCode == 104 || event.keyCode == 105 || event.keyCode == 106 || event.keyCode == 107 || event.keyCode == 108 || event.keyCode == 109 || event.keyCode == 110 || event.keyCode == 111 || event.keyCode == 112 || event.keyCode == 113 || event.keyCode == 114 || event.keyCode == 115 || event.keyCode == 116 || event.keyCode == 117 || event.keyCode == 118 || event.keyCode == 119 || event.keyCode == 120 || event.keyCode == 121 || event.keyCode == 122)) {
            //    event.returnValue = false;
            //    //e.preventDefault();
            //}
        }

        function fnValidateEntityPAN(Obj) {                                       //Function added by Pravin--PAN Validation
            if (Obj == null) Obj = window.event.srcElement;
            if (Obj.value != "") {
                ObjVal = Obj.value;
                var panPat = /^([a-zA-Z]{5})(\d{4})([a-zA-Z]{1})$/;
                var code = /([C,F,T])/;
                var code_chk = ObjVal.substring(3, 4);
                if (ObjVal.search(panPat) == -1) {
                    //alert("Please enter valid PAN Number");
                    AlertMsg("Please enter valid PAN Number");
                    Obj.value = "";
                    Obj.focus();
                    return false;
                }
                if (code.test(code_chk) == false) {
                    //alert("Please enter valid PAN Number"); //Invaild pan card number
                    AlertMsg("Please enter valid PAN Number");
                    Obj.value = "";
                    return false;
                }
            }
        }

        function fncInputNumericValuesWithHyphenOnly() {
            if (!(event.keyCode == 45 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                event.returnValue = false;
            }
        }

        function AlertMsg(msg) {
            debugger;
            var varFooter = '<center><button type="button" class="btn btn-success" data-dismiss="modal" style="margin-top: -6px;"><span class="glyphicon glyphicon-ok" style="color: White;"></span> OK </button></center>';
            showModal('#myModal', 'Alert', 'alert-warning', varFooter, '', msg);
        }

        function AlertMsgInfo(msg) {
            debugger;
            var varFooter = '<center><button type="button" class="btn btn-success" data-dismiss="modal" style="margin-top: -6px;"><span class="glyphicon glyphicon-ok" style="color: White;"></span> OK </button></center>';
            showModal('#myModal', 'Information', 'alert-warning', varFooter, '', msg);
        }

        function callCalender() {
            debugger;
            var dateArr = $("#<%=txtDatOfInc.ClientID%>").val().split('-');
            $("#<%= txtDatOfInc.ClientID%>").datepicker({ maxDate: "-1d", changeMonth: true, changeYear: true, dateFormat: 'dd-mm-yy' });
            $.datepicker.initialized = true;
            $("#<%= txtDatOfInc.ClientID%>").focus();

        }

        function callCalender1() {
            debugger;
            $("#<%= txtDate.ClientID%>").focus();
            var dateArr = $("#<%=txtDate.ClientID%>").val().split('-');
            $("#<%= txtDate.ClientID%>").datepicker({ maxDate: new Date(), changeMonth: true, changeYear: true, dateFormat: 'dd-mm-yy' });
            $.datepicker.initialized = true;
            $("#<%= txtDate.ClientID%>").focus();
        }

        function callCalender2() {
            debugger;
            $("#<%= txtDateKYCver.ClientID%>").focus();
            var dateArr = $("#<%=txtDateKYCver.ClientID%>").val().split('-');
            $("#<%= txtDateKYCver.ClientID%>").datepicker({ maxDate: new Date(), changeMonth: true, changeYear: true, dateFormat: 'dd-mm-yy' });
            $.datepicker.initialized = true;
            $("#<%= txtDateKYCver.ClientID%>").focus();
        }

    </script>

    <script type="text/javascript">
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
        //}
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">
    <script type="text/javascript">

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
                        document.getElementById("<%= dvTINCntry.ClientID %>").style.display = "block";

                    }
                    else {
                        //document.getElementById("spntincnt").style.display = "none";
                        document.getElementById("<%= dvTINCntry.ClientID %>").style.display = "none";
                    }
                    return true;
                }
                else {
                    //document.getElementById("spntincnt").style.display = "none";
                    document.getElementById("<%= dvTINCntry.ClientID %>").style.display = "none";
                    AlertMsg("Please enter Valid TIN/GST Registration Number");
                    Obj.value = "";
                    Obj.focus();
                    return false;
                }
            }
            else {
                //document.getElementById("spntincnt").style.display = "none";
                document.getElementById("<%= dvTINCntry.ClientID %>").style.display = "none";
            }
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

        function ShowReqDtl1(divName, btnName) {
            debugger;
            var objnewdiv = document.getElementById(divName);
            var objnewbtn = document.getElementById(btnName);

            if (objnewdiv.style.display == "block") {
                objnewdiv.style.display = "none";
                //objnewbtn.className = 'glyphicon glyphicon-resize-full'
                objnewbtn.className = 'glyphicon glyphicon-resize-small'
            }
            else {
                objnewdiv.style.display = "block";
                objnewbtn.className = 'glyphicon glyphicon-resize-full'
                //objnewbtn.className = 'glyphicon glyphicon-resize-small'
            }
        }

        function showHideDiv(divName, btnName) {
            debugger;
            try {
                var objnewdiv = document.getElementById(divName)
                var objnewbtn = document.getElementById(btnName);
                if (objnewdiv.style.display == "block") {
                    objnewdiv.style.display = "none";
                    objnewbtn.className = 'glyphicon glyphicon-collapse-up'
                }
                else {
                    objnewdiv.style.display = "block";
                    objnewbtn.className = 'glyphicon glyphicon-collapse-down'
                }
            }
            catch (err) {
                //ShowError(err.description);
            }
        }

        function UnCheckOther(sender, name) {
            var ck_list = document.getElementsByName(name);
            for (var i = 0; i < ck_list.length; i++) {
                ck_list[i].checked = false;
            }
            sender.checked = true;
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

        function OpenRelatedPersonPageViewNew(RelRefnNo, refno) {
            debugger;
            var modal = document.getElementById('myModalRaise');
            var modaliframe = document.getElementById("iframeCFR");

            modaliframe.src = "CKYCRelatedPrsnView.aspx?Status=View&refno=" + refno + "&relrefno=" + RelRefnNo;
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

        function OpenPartialRelatedPersonPageEditNew(RelRefnNo, refno, drNo) {
            debugger;
            var modal = document.getElementById('myModalRaise');
            var modaliframe = document.getElementById("iframeCFR");

            modaliframe.src = "CKYCRelatedPrsn.aspx?Status=PMod&Action=Edit&refno=" + refno + "&relrefno=" + RelRefnNo + "&drNo=" + drNo;
            $('#myModalRaise').modal('show');
        }

        function OpenPartialControllingPersonPageEditNew(RelRefnNo, refno, drNo) {
            debugger;
            var modal = document.getElementById('myModalRaise1');
            var modaliframe = document.getElementById("iframeCCFR");

            modaliframe.src = "CKYCControllingPrsn.aspx?Status=PMod&Action=Edit&refno=" + refno + "&relrefno=" + RelRefnNo + "&drNo=" + drNo;
            $('#myModalRaise1').modal('show');
        }

        function OpenControllingPersonPage() {
            debugger;
            var modal = document.getElementById('myModalRaise1');
            var modaliframe = document.getElementById("iframeCCFR");
            modaliframe.src = "CKYCControllingPrsn.aspx?Status=reg&mdl=myModalRaise1";
            $('#myModalRaise1').modal();
        }

        function OpenControllingPersonPageView(RelRefnNo, refno) {
            debugger;
            var modal = document.getElementById('myModalRaise1');
            var modaliframe = document.getElementById("iframeCCFR");

            modaliframe.src = "CKYCControllingPrsn.aspx?Status=Mod&refno=" + refno + "&relrefno=" + RelRefnNo;
            $('#myModalRaise1').modal('show');
        }

        function OpenControllingPersonPageViewNew(RelRefnNo, refno) {
            debugger;
            var modal = document.getElementById('myModalRaise1');
            var modaliframe = document.getElementById("iframeCCFR");

            modaliframe.src = "CKYCControlPrsnView.aspx?Status=View&refno=" + refno + "&relrefno=" + RelRefnNo;
            $('#myModalRaise1').modal('show');
        }

        function OpenControllingPersonPageEdit(RelRefnNo, refno) {
            debugger;
            var modal = document.getElementById('myModalRaise1');
            var modaliframe = document.getElementById("iframeCCFR");

            modaliframe.src = "CKYCControllingPrsn.aspx?Status=Mod&refno=" + refno + "&relrefno=" + RelRefnNo;
            $('#myModalRaise1').modal('show');
        }

        function OpenPartialControllingPersonPageView(RelRefnNo, refno) {
            debugger;
            var modal = document.getElementById('myModalRaise1');
            var modaliframe = document.getElementById("iframeCCFR");

            modaliframe.src = "CKYCControllingPrsn.aspx?Status=PMod&Action=View&refno=" + refno + "&relrefno=" + RelRefnNo;
            $('#myModalRaise1').modal('show');
        }

        function OpenPartialControllingPersonPageEdit(RelRefnNo, refno) {
            debugger;
            var modal = document.getElementById('myModalRaise1');
            var modaliframe = document.getElementById("iframeCCFR");

            modaliframe.src = "CKYCControllingPrsn.aspx?Status=PMod&Action=Edit&refno=" + refno + "&relrefno=" + RelRefnNo;
            $('#myModalRaise1').modal('show');
        }

        function funddlProofRelPerson() {
            $('#menu1').attr("class", "tab-pane fade");
            $('#EmptyPagePlaceholder_personal').attr("aria-expanded", false);
            $('#EmptyPagePlaceholder_m1').removeAttr("class");
            $('#EmptyPagePlaceholder_m3').attr("class", "active");
            $('#EmptyPagePlaceholder_A3').attr("aria-expanded", true);
            $('#menu4').attr("class", "tab-pane fade in active");
        }

         <%--<added by ramesh on dated 21-05-2018>--%>
        function specialcharecter() {
            debugger;
            var iChars = "!`@#$%^&*()+=-[]\\\';,./{}|\":<>?~_";
            var data = document.getElementById("<%=txtPlaceOfInc.ClientID %>").value;
            for (var i = 0; i < data.length; i++) {
                if (iChars.indexOf(data.charAt(i)) != -1) {
                    AlertMsg("special characters are not allowed.");
                    document.getElementById("<%=txtPlaceOfInc.ClientID %>").value = "";
                     return false;
                 }
             }
             var data1 = document.getElementById("<%=txtEmpName.ClientID %>").value;
            for (var i = 0; i < data1.length; i++) {
                if (iChars.indexOf(data1.charAt(i)) != -1) {
                    AlertMsg("special characters are not allowed.");
                    document.getElementById("<%=txtEmpName.ClientID %>").value = "";
                     return false;
                 }
             }

             var data2 = document.getElementById("<%=txtEmpDesignation.ClientID %>").value;
            for (var i = 0; i < data2.length; i++) {
                if (iChars.indexOf(data2.charAt(i)) != -1) {
                    AlertMsg("special characters are not allowed.");
                    document.getElementById("<%=txtEmpDesignation.ClientID %>").value = "";
                     return false;
                 }
             }
             var data3 = document.getElementById("<%=txtInsName.ClientID %>").value;
            for (var i = 0; i < data3.length; i++) {
                if (iChars.indexOf(data3.charAt(i)) != -1) {
                    AlertMsg("special characters are not allowed.");
                    document.getElementById("<%=txtInsName.ClientID %>").value = "";
                     return false;
                 }
             }
             var data4 = document.getElementById("<%=txtPlace.ClientID %>").value;
            for (var i = 0; i < data4.length; i++) {
                if (iChars.indexOf(data4.charAt(i)) != -1) {
                    AlertMsg("special characters are not allowed.");
                    document.getElementById("<%=txtPlace.ClientID %>").value = "";
                     return false;
                 }
             }
           <%-- var data5 = document.getElementById("<%=txtRefNumber.ClientID %>").value;
            for (var i = 0; i < data5.length; i++) {
                if (iChars.indexOf(data5.charAt(i)) != -1) {
                    AlertMsg("special characters are not allowed.");
                    document.getElementById("<%=txtRefNumber.ClientID %>").value = "";
                    return false;
                }
            }--%>
            var data6 = document.getElementById("<%=txtEmpCode.ClientID %>").value;
            for (var i = 0; i < data6.length; i++) {
                if (iChars.indexOf(data6.charAt(i)) != -1) {
                    AlertMsg("special characters are not allowed.");
                    document.getElementById("<%=txtEmpCode.ClientID %>").value = "";
                     return false;
                 }
             }
             var data7 = document.getElementById("<%=txtEmpBranch.ClientID %>").value;
            for (var i = 0; i < data7.length; i++) {
                if (iChars.indexOf(data7.charAt(i)) != -1) {
                    AlertMsg("special characters are not allowed.");
                    document.getElementById("<%=txtEmpBranch.ClientID %>").value = "";
                     return false;
                 }
             }
             var data8 = document.getElementById("<%=txtInsCode.ClientID %>").value;
            for (var i = 0; i < data8.length; i++) {
                if (iChars.indexOf(data8.charAt(i)) != -1) {
                    AlertMsg("special characters are not allowed.");
                    document.getElementById("<%=txtInsCode.ClientID %>").value = "";
                     return false;
                 }
             }
             var data9 = document.getElementById("<%=txtKYCName.ClientID %>").value;
            for (var i = 0; i < data9.length; i++) {
                if (iChars.indexOf(data9.charAt(i)) != -1) {
                    AlertMsg("special characters are not allowed.");
                    document.getElementById("<%=txtKYCName.ClientID %>").value = "";
                     return false;
                 }
             }

             var data10 = document.getElementById("<%=txtTypeIdentiNo.ClientID %>").value;
            for (var i = 0; i < data10.length; i++) {
                if (iChars.indexOf(data10.charAt(i)) != -1) {
                    AlertMsg("special characters are not allowed.");
                    document.getElementById("<%=txtTypeIdentiNo.ClientID %>").value = "";
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



    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="container">

        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
            <div id="Div18" runat="server" class="panel-heading" onclick="showHideDiv('divCKYCdtls','btnCKYCdtls');return false;">
                <div class="row">
                    <div class="col-sm-10" style="text-align: left">
                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                        <asp:Label ID="lblOfcuseOnly" Text="" runat="server" CssClass="control-label">
                        </asp:Label>
                    </div>
                    <div class="col-sm-2" style="text-align: right">
                        <asp:Label ID="Label3" Text="Version 1.6" Visible="false" runat="server" CssClass="control-label"></asp:Label>
                        <span id="btnCKYCdtls" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                    </div>
                </div>
            </div>
            <div id="divCKYCdtls" style="display: block;" class="panel-body">
                <div class="row" style="margin-bottom: 5px">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblAppType" Text="" runat="server" Font-Bold="false">
                        </asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:CheckBox ID="cbNew" runat="server" CssClass="standardcheckbox" Text="New" AutoPostBack="true" Checked="true"
                            Enabled="false" TabIndex="2" />
                        <asp:CheckBox ID="cbUpdate" runat="server" CssClass="standardcheckbox" Text="Update"
                            AutoPostBack="true" Visible="false" TabIndex="2" />
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblRefNumber" Text="" runat="server" Font-Bold="false"></asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:TextBox ID="txtRefNumber" runat="server" MaxLength="14" CssClass="form-control" OnTextChanged="txtRefNumber_TextChanged" AutoPostBack="true" onChange="javascript:this.value=this.value.toUpperCase();"
                            Font-Bold="false" TabIndex="2"> <%--onkeypress="funIsAlphaNumericHypenWithoutSpace();"--%>
                        </asp:TextBox>
                        <span id="spnValidRefNo" runat="server" style="display: none; color: green !important; padding-left: 1% !important;"><u>Valid Reference Number </u></span>
                    </div>
                </div>
                <div class="row" style="margin-bottom: 5px">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblKYCNumber" Text="" runat="server" Font-Bold="false"></asp:Label>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:TextBox ID="txtKYCNumber" runat="server" Enabled="false" CssClass="form-control" onkeypress="funIsAlphaNumericWithoutSpace();" MaxLength="14"
                            Font-Bold="false" TabIndex="2">     <%--OnTextChanged="txtKYCNumber_TextChanged" AutoPostBack="true"--%>
                        </asp:TextBox>
                    </div>


                </div>
                <div class="row" style="margin-bottom: 5px">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblAccountType" Text="" runat="server" Font-Bold="false">
                        </asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <%--<asp:CheckBoxList runat="server">--%>
                        <%--<input id="chkUSReport" class="standardcheckbox" title="US Reportable" type="checkbox"
                                    name="AccType" value="US" onclick="UnCheckOther(this, 'AccType')"/>&nbsp;<span>US Reportable</span>
                                 <input id="chkOtherReport" class="standardcheckbox" title="Other Reportable" type="checkbox"
                                    name="AccType" value="Other" onclick="UnCheckOther(this, 'AccType')"/>&nbsp;<span>Other Reportable</span>--%>
                        <%--<asp:CheckBox ID="chkOtherReport" runat="server" CssClass="standardcheckbox" Text="Other Reportable"
                                    AutoPostBack="true" TabIndex="3" name="AccType" value="value1" />--%>
                        <%--</asp:CheckBoxList>--%>
                        <asp:CheckBox ID="chkUSReport" runat="server" CssClass="standardcheckbox" Text=" US Reportable"
                            AutoPostBack="true" OnCheckedChanged="chkUSReport_CheckedChanged" TabIndex="2"></asp:CheckBox>
                        <asp:CheckBox ID="chkOtherReport" runat="server" CssClass="standardcheckbox" Text=" Other Reportable"
                            AutoPostBack="true" OnCheckedChanged="chkOtherReport_CheckedChanged" TabIndex="2"></asp:CheckBox>

                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblAccountSubType" Text="" runat="server" Font-Bold="false">
                        </asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:DropDownList ID="ddlAccHolderType" runat="server" CssClass="form-control" TabIndex="2">
                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row" style="margin-bottom: 5px">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblNatureOfBuss" Text="" runat="server" Font-Bold="false">
                        </asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:DropDownList ID="ddlNatureOfBuss" runat="server" Enabled="false" CssClass="form-control" TabIndex="2">
                            <%--<asp:ListItem Value="02" Text="02"></asp:ListItem>--%>
                        </asp:DropDownList>

                    </div>
                    <div style="display: none" runat="server">
                        <asp:TextBox ID="txtConstitutionTypeothers" Enabled="false" runat="server" MaxLength="200" CssClass="form-control" Font-Bold="false"
                            TabIndex="2" />
                    </div>
                </div>
            </div>
        </div>

    </div>

    <%-- Img Section--%>
    <div class="container" width="100%">
        <div id="divImg" runat="server" class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
            <div id="Div25" runat="server" class="panel-heading" onclick="showHideDiv('divnavigate','btnnavigate');return false;">
                <div class="row">
                    <div class="col-sm-10" style="text-align: left">
                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                        <asp:Label ID="lbluploadDoc" Text="UPLOADED DOCUMENTS" runat="server" CssClass="control-label"></asp:Label>

                    </div>
                    <div class="col-sm-2">
                        <span id="btnnavigate" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                    </div>
                </div>
            </div>
            <div id="divnavigate" style="display: block;" class="panel-body">
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
    </div>
    <%--Img section End--%>


    <div class="container" width="100%">
        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
            <div id="divEntityDetail" runat="server" class="panel-heading" onclick="showHideDiv('divDetailOfEntity','btnEntitydtls');return false;">
                <div class="row">
                    <div class="col-sm-10" style="text-align: left; top: 0px; left: 0px;">
                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                        <asp:Label ID="lblOfEntiDetl" Text="ENTITY DETAILS" runat="server" CssClass="control-label">
                        </asp:Label>
                    </div>
                    <div class="col-sm-2" style="text-align: right">
                        <span id="btnEntitydtls" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                    </div>
                </div>
            </div>
            <div id="divDetailOfEntity" style="display: block;" class="panel-body">
                <div class="row" style="margin-bottom: 8px">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblKYCName" Text="" runat="server" Font-Bold="false"></asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3" style="text-align: left; top: 2px; left: 0px;">
                        <asp:TextBox ID="txtKYCName" runat="server" MaxLength="200" CssClass="form-control" Font-Bold="false"
                            TabIndex="2">
                        </asp:TextBox>
                    </div>

                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblDatOfInc" Text="" runat="server" Font-Bold="false"></asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3">
                        <div class="input-group">
                            <asp:TextBox ID="txtDatOfInc" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtDatOfInc_TextChanged" MaxLength="10" TabIndex="2"></asp:TextBox>
                            <div class="input-group-btn">
                                <%--<asp:LinkButton runat="server" ID="LinkButton3" CssClass="btn btn-primary btn-lg-kmi"   onmousedown="checkDateOfIncorp(this);" title="Date" data-toggle="tooltip"  TabIndex="2">--%>
                                <div class="btn btn-primary btn-lg-kmi" onclick="callCalender()">
                                    <span class="glyphicon glyphicon-calendar BtnGlyphicon"></span>
                                </div>
                                <%--</asp:LinkButton>--%>
                            </div>
                        </div>
                    </div>
                    <%--</div>--%>


                    <%--<div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblDatOfInc" Text="" runat="server" Font-Bold="false"></asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3" style="text-align: left; top: 0px; left: 0px;">
                        <asp:TextBox ID="txtDatOfInc" runat="server" CssClass="form-control" AutoPostBack="true" onmousedown="$(this).datepicker({ maxDate: new Date() ,changeMonth: true, changeYear: true, dateFormat: 'dd-mm-yy' });" OnTextChanged="txtDatOfInc_TextChanged" MaxLength="10" TabIndex="2"></asp:TextBox>
                    </div>--%>
                </div>


                <div class="row" style="margin-bottom: 5px">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblDateOfCom" Text="" runat="server" Font-Bold="false"></asp:Label>
                        <span style="display: none; color: red">*</span>
                    </div>
                    <div class="col-sm-3">
                        <div class="input-group">
                            <asp:TextBox ID="txtDtOfCom" runat="server" CssClass="form-control" MaxLength="10" TabIndex="2"></asp:TextBox>
                            <div class="input-group-btn">
                                <%--<asp:LinkButton runat="server" ID="LinkButton3" CssClass="btn btn-primary btn-lg-kmi"   onmousedown="checkDateOfIncorp(this);" title="Date" data-toggle="tooltip"  TabIndex="2">--%>
                                <div class="btn btn-primary btn-lg-kmi" onclick="checkDateOfCommencement()">
                                    <span class="glyphicon glyphicon-calendar BtnGlyphicon"></span>
                                </div>
                                <%--</asp:LinkButton>--%>
                            </div>
                        </div>
                    </div>
                    <%--    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblDateOfCom" Text="" runat="server" Font-Bold="false"></asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3" style="text-align: left; top: 0px; left: 0px;">
                        <asp:TextBox ID="txtDtOfCom" runat="server" CssClass="form-control" onmousedown="checkDateOfCommencement()" MaxLength="10" TabIndex="2"></asp:TextBox>
                    </div>--%>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblPlaceOfIncor" Text="" runat="server" Font-Bold="false"></asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3" style="text-align: left; top: 2px; left: 0px; margin-top: -3px">
                        <asp:TextBox ID="txtPlaceOfInc" runat="server" CssClass="form-control" Font-Bold="false" onkeypress="fncInputcharacterOnlyNew();"
                            TabIndex="2">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="row" style="margin-bottom: 5px">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblCountrOfInc" Text="" runat="server" Font-Bold="false"></asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3" style="text-align: left; top: 0px; left: 0px;">
                        <asp:DropDownList ID="ddlCountrOfInc" runat="server" CssClass="form-control" Font-Bold="false"
                            TabIndex="2">
                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblCountryOfRsidens" Text="" runat="server"
                            Font-Bold="false"></asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3" style="text-align: left; top: 0px; left: 0px;">
                        <asp:DropDownList ID="ddlCountryOfRsidens" runat="server" CssClass="form-control" Font-Bold="false"
                            TabIndex="2">
                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row" style="margin-bottom: 5px">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblPanNo" Text="" runat="server" Font-Bold="false"></asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3" style="text-align: left; top: 0px; left: 0px;">
                        <asp:TextBox ID="txtPanNo" runat="server" AutoPostBack="true" OnTextChanged="txtPanNo_TextChanged" MaxLength="10" CssClass="form-control" onChange="javascript:this.value=this.value.toUpperCase();" onblur="fnValidateEntityPAN(this)" Font-Bold="false"
                            TabIndex="2">
                        </asp:TextBox>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblIdentyType" Text="" runat="server" Font-Bold="false"></asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3" style="text-align: left; top: 0px; left: 0px;">
                        <asp:DropDownList ID="ddlIdentyType" runat="server" CssClass="form-control" Font-Bold="false"
                            TabIndex="2">
                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                </div>
                <div class="row" style="margin-bottom: 5px">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblTypeIdentiNo" Text="" runat="server"
                            Font-Bold="false"></asp:Label>
                        <%--<span style="color: red">*</span>--%>
                    </div>
                    <div class="col-sm-3" style="text-align: left; top: 0px; left: 0px;">
                        <asp:TextBox ID="txtTypeIdentiNo" runat="server" MaxLength="11" CssClass="form-control" Font-Bold="false" onblur="tinvalidation(this);"
                            AutoPostBack="true" OnTextChanged="txtTypeIdentiNo_TextChanged" onkeypress="fncInputNumericValuesWithHyphenOnly();" TabIndex="2">
                        </asp:TextBox>
                    </div>
                    <div id="dvTINCntry" runat="server" style="display: none;">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="col-sm-3" style="text-align: left; display: flex;">
                                    <asp:Label ID="lblTINCountry" Text="" runat="server" Font-Bold="false"></asp:Label>&nbsp;
                                <span id="spntincnt" style="color: red;">*</span>
                                </div>
                                <div class="col-sm-3" style="text-align: left; top: 0px; left: 0px;">
                                    <asp:DropDownList ID="ddlTINCountry" runat="server" CssClass="form-control" Font-Bold="false"
                                        TabIndex="2">
                                        <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="row" style="margin-bottom: 5px">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblNumberOfPerson" Text="" runat="server" Font-Bold="false"></asp:Label>
                        <%--<span style="color: red">*</span>--%>
                    </div>
                    <div class="col-sm-3" style="text-align: left; top: 0px; left: 0px;">
                        <%-- <asp:TextBox ID="txtNumberOfPerson" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtNumberOfPerson_TextChanged" Font-Bold="false" MaxLength="2"
                            onkeypress="return isNumber(event)" TabIndex="2">
                        </asp:TextBox>--%>
                        <asp:DropDownList ID="ddlNumberOfPerson" runat="server" AutoPostBack="true" Font-Bold="false" CssClass="form-control" OnSelectedIndexChanged="ddlNumberOfPerson_SelectedIndexChanged" TabIndex="2">
                            <%--<asp:ListItem Value="02" Text="02"></asp:ListItem>--%>
                        </asp:DropDownList>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <div class="container" width="100%">
        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
            <div id="divPrrofOfIdenty" runat="server" class="panel-heading" onclick="showHideDiv('divProofOfIdenti','lblPOfIdentity');return false;">
                <div class="row">
                    <div class="col-sm-10" style="text-align: left; top: 0px; left: 0px;">
                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                        <asp:Label ID="PrrofOfIdenti" Text="PROOF OF IDENTITY (PoI)" runat="server" CssClass="control-label">
                        </asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-2" style="text-align: right">
                        <span id="lblPOfIdentity" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                    </div>
                </div>
            </div>
            <div id="divProofOfIdenti" style="display: block;" class="panel-body">

                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="row" style="margin-bottom: 5px">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblCertifiecopy" Text="" runat="server" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <%--<asp:UpdatePanel runat="server">
                                    <ContentTemplate>--%>
                                <asp:DropDownList ID="ddlCertifiecopy" runat="server" CssClass="form-control" TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddlCertifiecopy_SelectedIndexChanged">

                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                    <%--<asp:ListItem Value="" Text="Certificate of Incorporation / Formation"></asp:ListItem>
                            <asp:ListItem Value="" Text="Registration Certificate"></asp:ListItem>
                            <asp:ListItem Value="" Text="Resoluation of Board / Managing Committee"></asp:ListItem>
                            <asp:ListItem Value="" Text="Memorandum and Article of Association / Partnership Deed / Trust Deed"></asp:ListItem>
                            <asp:ListItem Value="" Text="Officially valid document(s) in respect of person authorised to transact"></asp:ListItem>--%>
                                </asp:DropDownList>
                            </div>

                            <div id="divIdProof" runat="server">
                                <div id="divPassNo" class="col-sm-3" runat="server" style="text-align: left">
                                    <asp:Label ID="lblPassportNo" runat="server" CssClass="control-label"></asp:Label>
                                    <span style="color: red">*</span>
                                </div>

                                <div id="divPassNotxt" runat="server" class="col-sm-3">
                                    <asp:TextBox ID="txtPassNo" AutoPostBack="true" OnTextChanged="txtPassNo_TextChanged" runat="server" MaxLength="50" TabIndex="2"
                                        CssClass="form-control" Font-Bold="false" onChange="javascript:this.value=this.value.toUpperCase();">
                                    </asp:TextBox>
                                    <%--onkeypress="funIsAlphaNumericHypenWithoutSpace()"--%>
                                </div>
                            </div>
                            <%--</ContentTemplate>
                                </asp:UpdatePanel>--%>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>


            </div>
        </div>
    </div>
    <%--Came till here --%>

    <div class="container" width="100%">
        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
            <div id="divProofOfAddress" runat="server" class="panel-heading" onclick="showHideDiv('menu3','Span9');return false;">
                <div class="row">
                    <div class="col-sm-10" style="text-align: left">
                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                        <asp:Label ID="ProofOfAdd" Text="PROOF OF ADDRESS (PoA)" runat="server" CssClass="control-label">
                        </asp:Label>
                    </div>
                    <div class="col-sm-2">
                        <span id="Span9" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                    </div>
                </div>
            </div>
            <div id="menu3" style="display: block;" class="panel-body">
                <%-- Added for Address Details start --%>
                <div class="panel panel-success" style="margin-left: 5px; margin-right: 5px">
                    <div id="Div6" runat="server" class="panel-heading subheader" onclick="ShowReqDtl1('div7','Span2');return false;">
                        <div class="row">
                            <div class="col-sm-10" style="text-align: left">
                                <span class="glyphicon glyphicon-menu-hamburger"></span>
                                <%--<asp:CheckBox ID="ChkUpdAddr" runat="server" CssClass="standardcheckbox" Text=""
                                    OnCheckedChanged="ChkUpdAddr_Checked" AutoPostBack="true" TabIndex="2" />--%>
                                <asp:Label ID="lblpfofAddr2" Text="" runat="server" CssClass="control-label"></asp:Label><span style="color: red">*</span>
                            </div>
                            <div class="col-sm-2">
                                <%--onclick="ShowReqDtl1('div7','Span2');return false;"--%>
                                <span id="Span2" class="glyphicon glyphicon-resize-full" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div>
                    <div id="div7" style="display: block;" class="panel-body">

                        <div class="row">
                            <div class="col-sm-12" style="text-align: left">
                                <asp:CheckBox ID="chkPerAddress" Text="CURRENT / PERMANENT / OVERSEAS ADDRESS DETAILS"
                                    AutoPostBack="true" OnCheckedChanged="chkPerAddress_Checked" runat="server" CssClass="standardcheckbox"
                                    TabIndex="2" />
                                <span style="color: red">*</span>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12" style="text-align: left">
                                <asp:CheckBox ID="chkSameAsPOI" Text="Proof Of Address Same as Above Proof Of Identity" OnCheckedChanged="SameIdentityProof_CheckedChanged"
                                    AutoPostBack="true" runat="server"
                                    CssClass="standardcheckbox" TabIndex="2" />
                                <%--<span style="color: red">*</span>--%>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblAddressType" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlAddressType" runat="server" CssClass="form-control" TabIndex="2">
                                            <%-- AutoPostBack="true" OnSelectedIndexChanged="ddlAddressType_SelectedIndexChanged"--%>
                                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblProofOfAddress" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddlProofOfAddress" runat="server" CssClass="form-control"
                                    TabIndex="2">
                                    <%--OnSelectedIndexChanged="ddlProofOfAddress_SelectedIndexChanged"--%>
                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div id="divAddProof" runat="server" class="row" visible="false">
                            <div id="divPassNoAdd" runat="server" class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblPassportNoAdd" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div id="divPassNotxtAdd" runat="server" class="col-sm-3">
                                <asp:TextBox CssClass="form-control" runat="server" onChange="javascript:this.value=this.value.toUpperCase();"
                                    ID="txtPassNoAdd" MaxLength="15" TabIndex="2" />

                            </div>
                            <div id="divPassAdd" runat="server" class="col-sm-3" style="text-align: left">
                                <asp:Label ID="llPassExpDateAdd" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div id="divPassDateAdd" runat="server" class="col-sm-3">
                                <asp:TextBox CssClass="form-control" onmousedown="$('#EmptyPagePlaceholder_txtPassExpDateAdd').datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' });"
                                    runat="server"
                                    ID="txtPassExpDateAdd" MaxLength="15" TabIndex="2" />
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtPassOthrAdd" MaxLength="15"
                                    TabIndex="2" />
                            </div>
                        </div>
                        <div class="row" style="margin-top: 4px">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblAddressLine1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox CssClass="form-control" runat="server"
                                    ID="txtAddressLine1" MaxLength="55" TabIndex="2" />
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblAddressLine2" Text="" runat="server" CssClass="control-label"></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox CssClass="form-control"
                                    runat="server" ID="txtAddressLine2" MaxLength="55" TabIndex="2" />
                            </div>
                        </div>
                        <div class="row" style="margin-top: 4px">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblAddressLine3" Text="" runat="server" CssClass="control-label"></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtDrivLicNum')" runat="server"
                                    ID="txtAddressLine3" MaxLength="55" TabIndex="2" />
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblCity" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" onkeypress="fncInputcharacterOnlyNew();" TabIndex="2" MaxLength="50">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="row" style="margin-top: 4px">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblState" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <div id="dvState" runat="server" class="input-group">
                                    <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" TabIndex="2">
                                        <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:HiddenField runat="server" ID="hdnddlPinCode" />
                                    <div class="input-group-btn">
                                        <asp:LinkButton runat="server" ID="LinkButton2" CssClass="btn btn-primary btn-lg-kmi" title="Search" data-toggle="tooltip" OnClick="GetModelData" TabIndex="2">
                                    <span class="glyphicon glyphicon-search BtnGlyphicon"></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <asp:TextBox ID="txtState" runat="server" CssClass="form-control" MaxLength="100" Visible="false"></asp:TextBox>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblPinCode" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span><font color="red">*</font></span>
                            </div>
                            <div class="col-sm-3">
                                <%--style="display:flex"--%>

                                <div class="">
                                    <asp:TextBox ID="txtPinCode" MaxLength="10" onkeypress="fncInputNumericValuesOnly();" runat="server" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                    <asp:HiddenField runat="server" ID="HiddenField1" />

                                </div>
                            </div>
                        </div>

                        <div class="row" style="margin-top: 4px">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblDistrict" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span><font color="red">*</font></span>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox runat="server" ID="txtDistrictname" CssClass="form-control"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblIsoCountryCode" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlCountryCode" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlCountryCode_SelectedIndexChanged"
                                            TabIndex="2">
                                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                    <%--   <Triggers>
                                       <asp:AsyncPostBackTrigger ControlID="ddlCountryCode" EventName="ddlCountryCode_SelectedIndexChanged" />
                                    </Triggers>--%>
                                </asp:UpdatePanel>
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
                                    <asp:Label ID="lblAddressType1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                    <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-3">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlAddressType1" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlAddressType1_SelectedIndexChanged" TabIndex="2">
                                                <%--AutoPostBack="true"--%>
                                                <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblProofOfAddress1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                    <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlProofOfAddress1" runat="server" CssClass="form-control"
                                        TabIndex="2">
                                        <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row" style="margin-top: 4px">
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
                            <div class="row" style="margin-top: 4px">
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
                                    <asp:TextBox ID="txtCity1" runat="server" onkeypress="fncInputcharacterOnlyNew();" CssClass="form-control" TabIndex="2" MaxLength="50"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row" style="margin-top: 4px">
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblState1" Text="" runat="server" CssClass="control-label"></asp:Label>
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
                                    <asp:Label ID="lblPin1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                    <span><font color="red">*</font></span>
                                </div>
                                <div class="col-sm-3">
                                    <div class="">
                                        <asp:TextBox ID="ddlPinCode1" MaxLength="10" onkeypress="fncInputNumericValuesOnly();" runat="server" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="HiddenField2" />

                                    </div>

                                </div>
                            </div>

                            <div class="row" style="margin-top: 4px">
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblDistrict1" Text="" runat="server" CssClass="control-label"></asp:Label>
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
                                    <asp:Label ID="lblCountryCode1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                    <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-3">
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
                                <asp:CheckBox ID="chkCorresAdd" Text="Same as Correspondence / Local Address details"
                                    TabIndex="2" runat="server" OnCheckedChanged="chkCorresAdd_Checked" AutoPostBack="true"
                                    CssClass="standardcheckbox" /><%--OnCheckedChanged="chkCorresAdd_Checked"--%>
                                <%--<span style="color: red">*</span>--%>
                            </div>
                        </div>
                        <div class="row" style="margin-top: 4px">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblAddressType2" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlAddressType2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAddressType2_SelectedIndexChanged" CssClass="form-control"
                                            TabIndex="2">
                                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblProofOfAddress2" Text="" Visible="false" runat="server" CssClass="control-label"></asp:Label>
                                <%--<span style="color: red">*</span>--%>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddlProofOfAddress2" runat="server" Visible="false" CssClass="form-control"
                                    TabIndex="2">
                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row" style="margin-top: 4px">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblAddLine1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox CssClass="form-control" runat="server"
                                    ID="txtAddLine1" MaxLength="55" TabIndex="2" />
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblAddLine2" Text="" runat="server" CssClass="control-label"></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox CssClass="form-control" runat="server"
                                    ID="txtAddLine2" MaxLength="55" TabIndex="2" />
                            </div>
                        </div>
                        <div class="row" style="margin-top: 4px">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblAddLine3" Text="" runat="server" CssClass="control-label"></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox CssClass="form-control" runat="server"
                                    ID="txtAddLine3" MaxLength="55" TabIndex="2" />
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblCity2" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtCity2" runat="server" CssClass="form-control" onkeypress="fncInputcharacterOnlyNew();" TabIndex="2" MaxLength="50"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row" style="margin-top: 4px">

                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblState2" Text="" runat="server" CssClass="control-label"></asp:Label>
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
                                <asp:Label ID="lblPin2" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span><font color="red">*</font></span>
                            </div>
                            <div class="col-sm-3">
                                <div class="">
                                    <asp:TextBox ID="ddlPinCode2" runat="server" MaxLength="10" onkeypress="fncInputNumericValuesOnly();" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                    <asp:HiddenField runat="server" ID="HiddenField3" />

                                </div>


                            </div>
                        </div>

                        <div class="row" style="margin-top: 4px">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblDistrict2" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span><font color="red">*</font></span>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtDistrict2" runat="server" CssClass="form-control"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblIsoCountry2" Text="" runat="server" CssClass="control-label"></asp:Label>
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
                <%-- Added for Address Details end --%>

                <%-- Added for Contact Details start --%>
                <div class="panel panel-success" style="margin-left: 5px; margin-right: 5px">
                    <div id="Div4" runat="server" class="panel-heading subheader"
                        onclick="ShowReqDtl1('div9','Span3');return false;">
                        <div class="row">
                            <div class="col-sm-10" style="text-align: left">
                                <span class="glyphicon glyphicon-menu-hamburger"></span>CONTACT DETAILS
                            </div>
                            <div class="col-sm-2">
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
                                    <span class="input-group-addon input-group-addon-tel" style="width: 23% !important; border-top-left-radius: 7% !important; padding: 0px !important; border: 0px !important;">
                                        <asp:TextBox ID="txtTelOff" runat="server" CssClass="form-control" MaxLength="4" TabIndex="2" onkeypress="fncInputNumericValuesOnly();"></asp:TextBox>
                                    </span>
                                    <asp:TextBox ID="txtTelOff2" runat="server" CssClass="form-control" onkeypress="fncInputNumericValuesOnly();" TabIndex="2"
                                        MaxLength="10"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblTelRes" runat="server" CssClass="control-label" Text=""></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <div class="input-group">
                                    <span class="input-group-addon input-group-addon-tel" style="width: 23% !important; border-top-left-radius: 7% !important; padding: 0px !important; border: 0px !important;">
                                        <asp:TextBox ID="txtTelRes" runat="server" CssClass="form-control" TabIndex="2"
                                            onkeypress="fncInputNumericValuesOnly();" MaxLength="4" Style="border-top-left-radius: 4px; border-bottom-left-radius: 4px; top: 0px; left: 0px;"></asp:TextBox>
                                    </span>
                                    <asp:TextBox ID="txtTelRes2" runat="server" CssClass="form-control" MaxLength="10"
                                        onkeypress="fncInputNumericValuesOnly();" TabIndex="2"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin-top: 4px">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblMobile" runat="server" Text="" CssClass="control-label"></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <div class="input-group">
                                    <span class="input-group-addon " style="width: 23% !important; border-top-left-radius: 7% !important; padding: 0px !important; border: 0px !important;">
                                        <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" onkeypress="fncInputNumericValuesOnly();"
                                            MaxLength="3" Style="border-top-left-radius: 4px; border-bottom-left-radius: 4px;" TabIndex="2"></asp:TextBox>
                                    </span>
                                    <asp:TextBox ID="txtMobile2" runat="server" CssClass="form-control" onkeypress="fncInputNumericValuesOnly();" TabIndex="2"
                                        MaxLength="10"></asp:TextBox>
                                </div>
                            </div>
                             <div class="col-sm-3" style="display:none">
                                <div class="input-group">
                                    <span class="input-group-addon " style="width: 23% !important; border-top-left-radius: 7% !important; padding: 0px !important; border: 0px !important;">
                                        <asp:TextBox ID="txtMobile1" runat="server" CssClass="form-control" onkeypress="fncInputNumericValuesOnly();"
                                            MaxLength="3" Style="border-top-left-radius: 4px; border-bottom-left-radius: 4px;" TabIndex="2"></asp:TextBox>
                                    </span>
                                    <asp:TextBox ID="txtMobile3" runat="server" CssClass="form-control" onkeypress="fncInputNumericValuesOnly();" TabIndex="2"
                                        MaxLength="10"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblFax" runat="server" Text="" CssClass="control-label"></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <div class="input-group">
                                    <span class="input-group-addon input-group-addon-tel" style="width: 23% !important; border-top-left-radius: 7% !important; padding: 0px !important; border: 0px !important;">
                                        <asp:TextBox ID="txtFax1" runat="server" MaxLength="4" onkeypress="fncInputNumericValuesOnly();" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                    </span>
                                    <asp:TextBox ID="txtFax2" runat="server" CssClass="form-control" MaxLength="10" onkeypress="fncInputNumericValuesOnly();" TabIndex="2"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin-top: 4px">
                            <div class="col-sm-3" style="text-align: left">
                                Email ID
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtemail" runat="server" CssClass="form-control" MaxLength="100" TabIndex="2" onblur="checkEmailN(this.id)"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <%-- Added for Contact Details end --%>
            </div>
        </div>
    </div>

    <%--Related Person Details section start--%>
    <div class="container" width="100%">
        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
            <div id="Div1" runat="server" class="panel-heading">
                <div class="row">
                    <div class="col-sm-10" style="text-align: left">
                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                        <%-- <asp:CheckBox ID="ChkUpdRelated" runat="server" CssClass="standardcheckbox" Text=""
                            AutoPostBack="true" OnCheckedChanged="ChkUpdRelated_Checked" TabIndex="2" />--%>
                        <%--OnCheckedChanged="ChkUpdRelated_Checked"--%>
                        <asp:Label ID="lblDtlOfRtltpr" Text="" runat="server" CssClass="control-label">
                        </asp:Label>
                    </div>
                    <div class="col-sm-2" onclick="showHideDiv('menu4','Span50');return false;">
                        <span id="Span50" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                    </div>
                </div>
            </div>
            <div id="menu4" style="display: block;" class="panel-body">

                <%--  Added for Details of Related Person start--%>
                <div class="row">
                    <div id="divchkAddRel" class="col-sm-3" style="text-align: left" runat="server">
                        <asp:CheckBox ID="chkAddRel" Text=" Addition of Related Person" TabIndex="2" AutoPostBack="true"
                            runat="server" CssClass="standardcheckbox" OnCheckedChanged="chkAddRel_Checked" />
                        <span style="color: red">*</span>
                    </div>
                    <div id="divchkDelRel" class="col-sm-6" style="text-align: left" runat="server" visible="false">
                        <asp:CheckBox ID="chkDelRel" OnCheckedChanged="chkAddRel_Checked" Text=" Deletion of Related Person" runat="server" TabIndex="2"
                            CssClass="standardcheckbox" />
                        <span style="color: red">*</span>
                    </div>
                    <div id="div10" class="col-sm-3" style="text-align: left" runat="server">
                    </div>
                    <div id="div11" class="col-sm-3" style="text-align: left" runat="server">
                    </div>
                    <div id="div5" class="col-sm-3" style="text-align: left" runat="server">

                        <asp:LinkButton ID="lnkViewRel" runat="server" Text="View Related Person Detail" FontBold="true" OnClick="lnkViewRel_Click" TabIndex="2"></asp:LinkButton>
                    </div>
                </div>

                <div class="row">
                    <div id="div12" class="col-sm-12" style="text-align: center" runat="server">
                        <asp:Label ID="lblRelRecordShow" Style="text-align: center" Text="  No Records Found" Visible="false" runat="server" />
                    </div>
                    <div class="container">
                        <asp:GridView ID="gvMemDtls" Width="97%" runat="server" AllowSorting="false" CssClass="footable"
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
                                            <asp:LinkButton ID="lnkView" runat="server" OnClick="lnkView_Click" Text="View" CommandArgument='<%# Eval("RelRefNo") %>'></asp:LinkButton>&nbsp;
                                        <asp:LinkButton ID="lnkEdit" runat="server" OnClick="lnkEdit_Click" Text="Edit" CommandArgument='<%# Eval("RelRefNo") %>'></asp:LinkButton>&nbsp;
                                        <asp:LinkButton ID="lnkdelete" runat="server" OnClick="lnkdelete_Click" Text="Delete"></asp:LinkButton>
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
                </div>


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
    </div>
    <%--Related Person Details section end--%>


    <%--Controlling Person Details section start--%>
    <div class="container" width="100%">
        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
            <div id="Div2" runat="server" class="panel-heading">
                <div class="row">
                    <div class="col-sm-10" style="text-align: left">
                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                        <%--<asp:CheckBox ID="ChkUpdControlling" runat="server" CssClass="standardcheckbox" Text=""
                            AutoPostBack="true" OnCheckedChanged="ChkUpdControlling_Checked" TabIndex="2" />--%>
                        <%--OnCheckedChanged="ChkUpdRelated_Checked"--%>
                        <asp:Label ID="lblDtlOfCtrlpr" Text="" runat="server" CssClass="control-label">
                        </asp:Label>
                    </div>
                    <div class="col-sm-2" onclick="showHideDiv('dvCtrlPrsn','spnControlPrsn');return false;">
                        <span id="spnControlPrsn" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                    </div>
                </div>
            </div>
            <div id="dvCtrlPrsn" style="display: block;" class="panel-body">

                <%--  Added for Details of Related Person start--%>
                <div class="row">
                    <div id="div3" class="col-sm-3" style="text-align: left" runat="server">
                        <asp:CheckBox ID="chkAddCtrl" Text=" Addition of Controlling Person" AutoPostBack="true"
                            runat="server" CssClass="standardcheckbox" OnCheckedChanged="chkAddCtrl_Checked" TabIndex="2" />
                        <%--<span style="color: red">*</span>--%>
                    </div>
                    <div id="div8" class="col-sm-6" style="text-align: left" runat="server" visible="false">
                        <asp:CheckBox ID="chkDelCtrl" OnCheckedChanged="chkAddCtrl_Checked" Text=" Deletion of Controlling Person" runat="server"
                            CssClass="standardcheckbox" TabIndex="2" />
                        <span style="color: red">*</span>
                    </div>
                    <div id="div13" class="col-sm-3" style="text-align: left" runat="server">
                    </div>
                    <div id="div14" class="col-sm-3" style="text-align: left" runat="server">
                    </div>
                    <div id="div19" class="col-sm-3" style="text-align: left" runat="server">

                        <asp:LinkButton ID="lnkViewCtrl" runat="server" Text="View Controlling Person Detail" FontBold="true" OnClick="lnkViewCtrl_Click" TabIndex="2"></asp:LinkButton>
                    </div>
                </div>

                <div class="row">
                    <div id="div20" class="col-sm-12" style="text-align: center" runat="server">
                        <asp:Label ID="Label2" Style="text-align: center" Text="  No Records Found" Visible="false" runat="server" />
                    </div>
                    <div class="container">
                        <asp:GridView ID="gvCtrlPrson" Width="97%" runat="server" AllowSorting="false" CssClass="footable"
                            PageSize="10" AllowPaging="true" CellPadding="1"
                            AutoGenerateColumns="False" OnRowDataBound="gvCtrlPrson_RowDataBound">
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
                                <asp:TemplateField HeaderText="Controlling Reference No." ItemStyle-Width="20%" SortExpression="Reference No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("RelRefNo") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Controlling Type" ItemStyle-Width="20%" SortExpression="Reference No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRelTypVal" runat="server" Text='<%# Eval("RelationTypetxt") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Controlling Name" ItemStyle-Width="20%" SortExpression="Candidate Name">
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
                                            <asp:LinkButton ID="lnkView" runat="server" OnClick="lnkView1_Click" Text="View" CommandArgument='<%# Eval("RelRefNo") %>'></asp:LinkButton>&nbsp;
                                        <asp:LinkButton ID="lnkEdit" runat="server" OnClick="lnkEditCtrl_Click" Text="Edit" CommandArgument='<%# Eval("RelRefNo") %>'></asp:LinkButton>&nbsp;
                                        <asp:LinkButton ID="lnkdelete" runat="server" OnClick="lnkdeleteCtrl_Click" Text="Delete"></asp:LinkButton>
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
                </div>

                <div id="div22" runat="server" class="row">

                    <div id="diverror" class="alert alert-danger alert-dismissible col-sm-10" role="alert" style="float: right; display: none;">
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <span id="spnRed">Oh snap! Please correct the values and submit again.</span>
                    </div>

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
    </div>
    <%--Controlling Person Details section end--%>


    <%--Proof of Personal Details Start--%>
    <%--<div class="container" width="100%">
        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
            <div id="divPersonalDetl" runat="server" class="panel-heading" onclick="showHideDiv('menu1','Span8');return false;">
                <div class="row">
                    <div class="col-sm-10" style="text-align: left">
                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                        <asp:Label ID="lblPersonalDetail" Text="PERSONAL DETAIL" runat="server" CssClass="control-label">
                        </asp:Label>
                    </div>
                    <div class="col-sm-2">
                        <span id="Span8" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                    </div>
                </div>
            </div>
            <div id="menu1" style="display: block;" class="panel-body">
                <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                    <div id="divPersonalDtl1" runat="server" class="panel-heading subheader" 
                        onclick="ShowReqDtl1('divPersonal','btnpersnl');return false;">
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
                                    <asp:Label ID="lblprefx" Text="Prefix" runat="server" CssClass="control-label">
                                    </asp:Label>
                                </div>
                                <div class="col-sm-10" style="padding-left: 0">
                                    <div class="col-sm-4">
                                        <asp:Label ID="lblfname" Text="First Name" runat="server" CssClass="control-label">
                                        </asp:Label>
                                    </div>
                                    <div class="col-sm-4" style="padding-left: 3%">
                                        <asp:Label ID="lblMname" Text="Middle Name" runat="server" CssClass="control-label">
                                        </asp:Label>
                                    </div>
                                    <div class="col-sm-4" style="padding-left: 4%">
                                        <asp:Label ID="lblLname" Text="Last Name" runat="server" CssClass="control-label">
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
                                    <asp:DropDownList ID="cboTitle" runat="server" CssClass="form-control" DataTextField="ParamDesc"
                                        DataValueField="ParamValue" AppendDataBoundItems="True">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-10" style="padding: 0">
                                    <div class="col-sm-4" style="padding-left: 0">
                                        <asp:TextBox ID="txtGivenName" runat="server" CssClass="form-control" onkeypress="funIsAlphaNumericWithSpace();"
                                            onchange="javascript:this.value=this.value.toUpperCase();" MaxLength="50" placeholder="First Name">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-4" style="padding-left: 0">
                                        <asp:TextBox ID="txtMiddleName" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"
                                            onkeypress="funIsAlphaNumericWithSpace();" MaxLength="50" onblur="CheckSpaces();return false;"
                                            placeholder="Middle Name">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-4" style="padding-left: 0">
                                        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"
                                            onkeypress="funIsAlphaNumericWithSpace();" MaxLength="50" onblur="CheckSpaces();return false;"
                                            placeholder="Last Name">
                                        </asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblMaidenName" Text="Maiden Name" CssClass="control-label" runat="server">
                                </asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-9" style="padding: 0">
                                <div class="col-sm-2">
                                    <asp:DropDownList ID="cboTitle1" runat="server" CssClass="form-control" DataTextField="ParamDesc"
                                        DataValueField="ParamValue" AppendDataBoundItems="True">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-10" style="padding: 0">
                                    <div class="col-sm-4" style="padding-left: 0">
                                        <asp:TextBox ID="txtGivenName1" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"
                                            onkeypress="funIsAlphaNumericWithSpace();" MaxLength="50" onblur="CheckSpaces();return false;"
                                            placeholder="First Name">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-4" style="padding-left: 0">
                                        <asp:TextBox ID="txtMiddleName1" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"
                                            onkeypress="funIsAlphaNumericWithSpace();" MaxLength="50" onblur="CheckSpaces();return false;"
                                            placeholder="Middle Name">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-4" style="padding-left: 0">
                                        <asp:TextBox ID="txtLastName1" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"
                                            onkeypress="funIsAlphaNumericWithSpace();" MaxLength="50" onblur="CheckSpaces();return false;"
                                            placeholder="Last Name">
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
                                    <asp:Label ID="lblFatherName" Text=" Father/Spouse Name" CssClass="control-label"
                                        runat="server"></asp:Label>
                                    <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-6" style="padding: 0">
                                    <asp:UpdatePanel ID="UpdFSFlag" runat="server">
                                        <ContentTemplate>
                                            <asp:RadioButtonList ID="rbtFS" runat="server" CssClass="radiobtn" RepeatDirection="Horizontal"
                                                Visible="true" AutoPostBack="true">
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
                                            <asp:DropDownList ID="cboTitle2" runat="server" CssClass="form-control" DataTextField="ParamDesc"
                                                DataValueField="ParamValue" AppendDataBoundItems="True">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-sm-10" style="padding: 0">
                                    <div class="col-sm-4" style="padding-left: 0">
                                        <asp:TextBox ID="txtGivenName2" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"
                                            onkeypress="funIsAlphaNumericWithSpace();" MaxLength="50" onblur="CheckSpaces();return false;"
                                            placeholder="First Name">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-4" style="padding-left: 0">
                                        <asp:TextBox ID="txtMiddleName2" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"
                                            onkeypress="funIsAlphaNumericWithSpace();" MaxLength="50" onblur="CheckSpaces();return false;"
                                            placeholder="Middle Name">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-4" style="padding-left: 0">
                                        <asp:TextBox ID="txtLastName2" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"
                                            onkeypress="funIsAlphaNumericWithSpace();" MaxLength="50" onblur="CheckSpaces();return false;"
                                            placeholder="Last Name">
                                        </asp:TextBox>
                                    </div>
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
                                    <asp:DropDownList ID="cboTitle3" runat="server" CssClass="form-control" DataTextField="ParamDesc"
                                        DataValueField="ParamValue" AppendDataBoundItems="True">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-10" style="padding: 0">
                                    <div class="col-sm-4" style="padding-left: 0">
                                        <asp:TextBox ID="txtGivenName3" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"
                                            onkeypress="funIsAlphaNumericWithSpace();" MaxLength="50" onblur="CheckSpaces();return false;"
                                            placeholder="First Name">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-4" style="padding-left: 0">
                                        <asp:TextBox ID="txtMiddleName3" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"
                                            onkeypress="funIsAlphaNumericWithSpace();" MaxLength="50" onblur="CheckSpaces();return false;"
                                            placeholder="Middle Name">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-4" style="padding-left: 0">
                                        <asp:TextBox ID="txtLastName3" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"
                                            onkeypress="funIsAlphaNumericWithSpace();" MaxLength="50" onblur="CheckSpaces();return false;"
                                            placeholder="Last Name">
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
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtDOB" MaxLength="15" onmousedown="$('#EmptyPagePlaceholder_txtDOB').datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy', yearRange: '1945:'+(new Date).getFullYear()  });"
                                    onblur="setDateFormat('EmptyPagePlaceholder_txtDOB');" />
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblGender" Text="Gender" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="upcboGender" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cboGender" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="row">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblOccupation" Text="Occupation" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlOccupation" AutoPostBack="true" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left" id="divOccuSubType" runat="server">
                                        <asp:Label ID="lblOccuSubType" Text="Occupation Sub Type" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlOccuSubType" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlOccupation" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblMarStatus" Text="Marital Status" runat="server" CssClass="control-label">
                                </asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="upMaritalStatus" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlMaritalStatus" runat="server" CssClass="form-control">
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
                                        <asp:DropDownList ID="ddlCitizenship" runat="server" CssClass="form-control" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblResStatus" Text="Residential Status" runat="server" CssClass="control-label">
                                </asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="upResStatus" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlResStatus" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:UpdatePanel ID="uplblIsoCountryCodeOthr" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lblIsoCountryCodeOthr" Text="" Visible="false" runat="server" CssClass="control-label"></asp:Label>
                                        <span id="asteriskIsoCountryCodeOthr" style="color: red" runat="server" visible="false">*</span>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="upIsoCountryCodeOthr" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlIsoCountryCodeOthr" runat="server" CssClass="form-control"
                                            AutoPostBack="true" Visible="false">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                    <div id="divTickIfApplicable" runat="server" class="panel-heading subheader" 
                        onclick="ShowReqDtl1('divAdditionalDtl','Span5');return false;">
                        <div class="row">
                            <div class="col-sm-10" style="text-align: left">
                                <span class="glyphicon glyphicon-menu-hamburger"></span>
                                <asp:Label ID="lbltick" Text="TICK IF APPLICABLE" runat="server" CssClass="control-label">
                                </asp:Label>
                            </div>
                            <div class="col-sm-2">
                                <span id="Span5" class="glyphicon glyphicon-resize-full" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div>
                    <div id="divAdditionalDtl" style="display: block;" class="panel-body">
                        ADIITIONAL DETAILS REQUIRED*(Mandatory only if section 5.2 is ticked)
                        <div class="row">
                            <div class="col-sm-12" style="text-align: left">
                                <asp:CheckBox ID="chkTick" Text=" RESIDENCE FOR TAX PURPOSES IN JURISDICTIONS(S) OUTSIDE INDIA"
                                    CssClass="standardcheckbox" runat="server" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblIsoCountryCode2" Text="ISO 3166 Country Code of Jurisdiction of Residence"
                                    runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddlIsoCountryCode2" runat="server" CssClass="form-control"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblTaxIden" Text="Tax Identification Number or equivalent(if issued by jurisdiction)"
                                    runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtIDResTax" MaxLength="20"
                                    onkeypress="funIsAlphaNumeric();" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblPlace" Text="Place/City of Birth" runat="server" CssClass="control-label">
                                </asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtDOBRes" MaxLength="15" />
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblIsoContry" Text="ISO 3166 Country Code of Birth" runat="server"
                                    CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddlIsoCountry" runat="server" CssClass="form-control" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>
    <%--Proof of Personal Details End--%>

    <%--Proof of ID Start--%>
    <%--<div class="container" width="100%">
        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px" onclick="showHideDiv('divProof','btnProofIde');return false;">
            <div id="divProofofidenty" runat="server" class="panel-heading">
                <div class="row">
                    <div class="col-sm-10" style="text-align: left">
                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                        <asp:Label ID="lblProofOfIdentity11" Text=" PROOF OF IDENTITY(Pol)" runat="server"
                            CssClass="control-label"></asp:Label>
                    </div>
                    <div class="col-sm-2">
                        <span id="btnProofIde" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                    </div>
                </div>
            </div>
            <div id="divProof" style="display: block;" class="panel-body">
                <div class="row">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblProof" Text="(Certified copy of any one the following Proof of Identity [Pol] needs to be submitted)"
                            runat="server" CssClass="control-label"></asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlProofIdentity" runat="server" CssClass="form-control" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>
    <%--Proof of ID End--%>

    <%--Proof of Address Start--%>
    <%--<div class="container" width="100%">
        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
            <div id="divProofOfAddress1" runat="server" class="panel-heading" onclick="showHideDiv('divAddressDtl','divPOA');return false;">
                <div class="row">
                    <div class="col-sm-10" style="text-align: left">
                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                        <asp:Label ID="lblProofOfAddress" Text="PROOF OF ADDRESS(POA)" runat="server" CssClass="control-label">
                        </asp:Label>
                    </div>
                    <div class="col-sm-2">
                        <span id="divPOA" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                    </div>
                </div>
            </div>
            <div id="divAddressDtl" style="display: block;" class="panel-body">
                <div class="row" style="margin-bottom: 5px">
                    <div class="col-sm-3">
                        <asp:Label ID="Label44" Text="Address Type" runat="server" Font-Bold="false"></asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="DropDownList19" runat="server" CssClass="form-control" TabIndex="62">
                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                            <asp:ListItem Value="" Text="Residential / Bussiness"></asp:ListItem>
                            <asp:ListItem Value="" Text="Residential"></asp:ListItem>
                            <asp:ListItem Value="" Text="Bussiness"></asp:ListItem>
                            <asp:ListItem Value="" Text="Registered Office"></asp:ListItem>
                            <asp:ListItem Value="" Text="Unspecified"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-3">
                        <asp:Label ID="Label45" Text="Proof of Address Type" runat="server" Font-Bold="false"></asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="DropDownList20" runat="server" CssClass="form-control" TabIndex="62">
                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                            <asp:ListItem Value="" Text="Certificate of Incorporation / Formation"></asp:ListItem>
                            <asp:ListItem Value="" Text="Registration Certificate"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row" style="margin-bottom: 5px">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="Label46" Text="Address Line1" runat="server" Font-Bold="false"></asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3" style="text-align: left; top: 2px; left: 0px;">
                        <asp:TextBox ID="TextBox23" runat="server" CssClass="form-control" Font-Bold="false"
                            TabIndex="2">
                        </asp:TextBox>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="Label48" Text="Address Line2" runat="server" Font-Bold="false"></asp:Label>
                    </div>
                    <div class="col-sm-3" style="text-align: left; top: 2px; left: 0px;">
                        <asp:TextBox ID="TextBox24" runat="server" CssClass="form-control" Font-Bold="false"
                            TabIndex="2">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="row" style="margin-bottom: 5px">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="Label49" Text="Address Line3" runat="server" Font-Bold="false"></asp:Label>
                    </div>
                    <div class="col-sm-3" style="text-align: left; top: 2px; left: 0px;">
                        <asp:TextBox ID="TextBox25" runat="server" CssClass="form-control" Font-Bold="false"
                            TabIndex="2">
                        </asp:TextBox>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="Label50" Text="City/Town/Village" runat="server" Font-Bold="false"></asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3" style="text-align: left; top: 2px; left: 0px;">
                        <asp:TextBox ID="TextBox26" runat="server" CssClass="form-control" Font-Bold="false"
                            TabIndex="2">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="row" style="margin-bottom: 5px">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="Label51" Text="District" runat="server" Font-Bold="false"></asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3" style="text-align: left; top: 2px; left: 0px;">
                        <asp:DropDownList ID="DropDownList21" runat="server" CssClass="form-control" TabIndex="62">
                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="Label52" Text="Pin/Post Code" runat="server" Font-Bold="false"></asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3" style="text-align: left; top: 2px; left: 0px;">
                        <asp:DropDownList ID="DropDownList22" runat="server" CssClass="form-control" TabIndex="62">
                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row" style="margin-bottom: 5px">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="Label53" Text="State/U.T Code" runat="server" Font-Bold="false"></asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3" style="text-align: left; top: 2px; left: 0px;">
                        <asp:DropDownList ID="DropDownList23" runat="server" CssClass="form-control" TabIndex="62">
                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="Label54" Text="ISO 3166 Country Code" runat="server" Font-Bold="false"></asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3" style="text-align: left; top: 2px; left: 0px;">
                        <asp:DropDownList ID="DropDownList24" runat="server" CssClass="form-control" TabIndex="62">
                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>
    <%--Proof of Address End--%>

    <div class="container" width="100%">
        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
            <div id="divRemark" runat="server" class="panel-heading" onclick="showHideDiv('divRemarkDtl','Span11');return false;">
                <div class="row">
                    <div class="col-sm-10" style="text-align: left">
                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                        <asp:Label ID="lblRemarks" Text=" REMARKS" runat="server" CssClass="control-label">
                        </asp:Label>
                    </div>
                    <div class="col-sm-2">
                        <span id="Span11" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                    </div>
                </div>
            </div>
            <div id="divRemarkDtl" style="display: block;" class="panel-body">
                <div class="row">
                    <div class="col-sm-12">
                        <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtRemarks')" runat="server"
                            ID="txtRemarks" TextMode="MultiLine" MaxLength="15" TabIndex="2" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%-- APPLICATION DECLARATION start --%>
    <div class="container" width="100%">
        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
            <div id="divApplivationDeclare" runat="server" class="panel-heading" onclick="showHideDiv('menu6','Span12');return false;">
                <div class="row">
                    <div class="col-sm-10" style="text-align: left">
                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                        <asp:Label ID="lblattstn" Text="APPLICANT DECLARATION" runat="server" CssClass="control-label">
                        </asp:Label>
                    </div>
                    <div class="col-sm-2">
                        <span id="Span12" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                    </div>
                </div>
            </div>
            <div id="menu6" style="display: block;" class="panel-body">
                <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                    <div id="Div15" runat="server" class="panel-heading subheader"
                        onclick="ShowReqDtl1('div16','Span7');return false;">
                        <div class="row">
                            <div class="col-sm-10" style="text-align: left">
                                <span class="glyphicon glyphicon-menu-hamburger"></span>
                                <asp:Label ID="lbldec" Text="APPLICATION DECLARATION" runat="server" CssClass="control-label">
                                </asp:Label>
                            </div>
                            <div class="col-sm-2">
                                <span id="Span7" class="glyphicon glyphicon-resize-full" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div>
                    <div id="div16" style="display: block;" class="panel-body">
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
                                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" MaxLength="10" TabIndex="2"></asp:TextBox>
                                    <div class="input-group-btn">
                                        <%--<asp:LinkButton runat="server" ID="LinkButton3" CssClass="btn btn-primary btn-lg-kmi"   onmousedown="checkDateOfIncorp(this);" title="Date" data-toggle="tooltip"  TabIndex="2">--%>
                                        <div class="btn btn-primary btn-lg-kmi" onclick="callCalender1()">
                                            <span class="glyphicon glyphicon-calendar BtnGlyphicon"></span>
                                        </div>
                                        <%--</asp:LinkButton>--%>
                                    </div>
                                </div>
                            </div>

                            <%--<div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblDate" Text="Date" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3" style="text-align: left; top: 0px; left: 0px;">
                                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" onmousedown="$(this).datepicker({ maxDate: new Date() ,changeMonth: true, changeYear: true, dateFormat: 'dd-mm-yy' });" MaxLength="10" TabIndex="2"></asp:TextBox>
                            </div>--%>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblPlace1" Text="Place" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox CssClass="form-control" onkeypress="fncInputcharacterOnlyNew();" runat="server"
                                    ID="txtPlace" MaxLength="50" TabIndex="2" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                    <div id="Div17" runat="server" class="panel-heading subheader"
                        onclick="ShowReqDtl1('div21','Span10');return false;">
                        <div class="row">
                            <div class="col-sm-10" style="text-align: left">
                                <span class="glyphicon glyphicon-menu-hamburger"></span>
                                <asp:Label ID="lblAttesOfc" Text="ATTESTATION / FOR OFFICE USE ONLY" runat="server"
                                    CssClass="control-label">
                                </asp:Label>
                            </div>
                            <div class="col-sm-2">
                                <span id="Span10" class="glyphicon glyphicon-resize-full" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div>
                    <div id="div21" style="display: block;" class="panel-body">
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblDocRec" Text="" runat="server" Font-Bold="true"
                                    CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3" style="display: flex;">
                                <asp:CheckBox ID="chkSelfCerti" Text="" CssClass="standardcheckbox"
                                    runat="server" AutoPostBack="true" OnCheckedChanged="chkSelfCerti_CheckedChanged" TabIndex="2" />
                                <span>Self-Certified</span>
                                <asp:CheckBox ID="chkTrueCopies" Text="" Style="margin-left: 4px;" CssClass="standardcheckbox"
                                    runat="server" AutoPostBack="true" OnCheckedChanged="chkTrueCopies_CheckedChanged" TabIndex="2" />
                                <span>True Copies</span>
                                <asp:CheckBox ID="chkNotary" Style="margin-left: 5px;" Text="" CssClass="standardcheckbox"
                                    runat="server" AutoPostBack="true" OnCheckedChanged="chkNotary_CheckedChanged" TabIndex="2" />
                                <span>Notary</span>
                            </div>
                            <div style="display: none;">
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblRiskCategory" Text="" runat="server" Font-Bold="true"
                                        CssClass="control-label"></asp:Label>
                                    <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-3">
                                    <asp:CheckBox ID="chkHigh" Text="" CssClass="standardcheckbox"
                                        AutoPostBack="true" runat="server" TabIndex="2" OnCheckedChanged="chkHigh_CheckedChanged" />
                                    <span>High</span>
                                    <asp:CheckBox ID="chkMedium" Text="" CssClass="standardcheckbox"
                                        AutoPostBack="true" runat="server" TabIndex="2" OnCheckedChanged="chkMedium_CheckedChanged" />
                                    <span>Medium</span>
                                    <asp:CheckBox ID="chkLow" Text="" CssClass="standardcheckbox"
                                        AutoPostBack="true" runat="server" TabIndex="2" OnCheckedChanged="chkLow_CheckedChanged" />
                                    <span>Low</span>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-12" style="text-align: center">
                                <asp:Label ID="lblKYCVerify" Style='text-align: center' CssClass="control-label"
                                    Font-Bold="true" Text="" runat="server" />
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblIdVerif" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3" style="text-align: left; top: 0px; left: 0px;">
                                <asp:CheckBox ID="chkDone" Text="" CssClass="standardcheckbox" Checked="true" Enabled="false"
                                    runat="server" TabIndex="2" />
                                <span>Done</span>
                            </div>



                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblDate3" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <div class="input-group">
                                    <asp:TextBox ID="txtDateKYCver" runat="server" CssClass="form-control" MaxLength="10" TabIndex="2"></asp:TextBox>
                                    <div class="input-group-btn">
                                        <div class="btn btn-primary btn-lg-kmi" onclick="callCalender2()">
                                            <span class="glyphicon glyphicon-calendar BtnGlyphicon"></span>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <%--<div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblDate3" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3" style="text-align: left; top: 0px; left: 0px;">
                                <asp:TextBox ID="txtDateKYCver" runat="server" CssClass="form-control" onmousedown="$(this).datepicker({ maxDate: new Date() ,changeMonth: true, changeYear: true, dateFormat: 'dd-mm-yy' });" MaxLength="10" TabIndex="2"></asp:TextBox>
                            </div>--%>
                        </div>
                        <div class="row" style="margin-top: 17px">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblEmpName" Text="" runat="server" CssClass="control-label">
                                </asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtEmpName" MaxLength="150"
                                    onkeypress="fncInputValidateName();" Enabled="true" TabIndex="2" />
                                <br />
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblEmpCode" Text="Employee Code" runat="server" CssClass="control-label">
                                </asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtEmpCode" MaxLength="50" onkeypress="funIsAlphaNumericWithoutSpace();"
                                    Enabled="true" TabIndex="2" />
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblEmpDesignation" Text="Employee Designation" runat="server" CssClass="control-label">
                                </asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <%--style="margin-top: -16px"--%>
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtEmpDesignation" MaxLength="50" onkeypress="fncInputValidateDesignation();"
                                    Enabled="true" TabIndex="2" />
                                <%--<br/>--%>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblEmpBranch" Text="Employee Branch" runat="server" CssClass="control-label">
                                </asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <%--style="margin-top: -16px"--%>
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtEmpBranch" MaxLength="50" onkeypress="fncInputcharacterOnlyNew();"
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
                                <asp:Label ID="lblInsName" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox CssClass="form-control" runat="server"
                                    ID="txtInsName" MaxLength="150" Enabled="true" TabIndex="2" />
                                <%--onkeypress="fncInputValidateNameNew();" --%>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblInsCode" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtInsCode" MaxLength="6" onkeypress="funIsAlphaNumericWithoutSpace();"
                                    Enabled="true" TabIndex="2" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%-- APPLICATION DECLARATION End --%>

    <%-- APPLICATION Action Button start --%>
    <div class="container" width="100%">
        <div class="row">
            <%--style="margin-top: 12px;"--%>
            <center>
                <div class="col-sm-12">

                    <asp:LinkButton ID="btnUpdate"  runat="server" CssClass="btn-animated bg-green"
                    Visible="false"  TabIndex="2"> <span class="glyphicon glyphicon-floppy-disk BtnGlyphicon"> Update</span> </asp:LinkButton>
                    <asp:LinkButton ID="btnKYCUpdate"  runat="server" CssClass="btn-animated bg-green" 
                    Visible="false"  TabIndex="2"> <span class="glyphicon glyphicon-floppy-disk BtnGlyphicon"> Update</span> </asp:LinkButton>
               <%-- OnClick="btnUpdate_Click" OnClick="btnKYCUpdate_Click"--%>

                    <asp:LinkButton ID="btnPartialSave" OnClick="btnPartialSave_Click" CssClass="btn-animated bg-green" runat="server" TabIndex="2"> 
                    <span class="glyphicon glyphicon-floppy-disk BtnGlyphicon"></span> Partial Save
                    </asp:LinkButton>

                     <asp:LinkButton ID="btnPartialUpdate"  Visible="false" OnClick="btnPartialUpdate_Click"
                    CssClass="btn-animated bg-green" runat="server" TabIndex="2">
                    <asp:HiddenField ID="HiddenField6" runat="server" />
                    <span class="glyphicon glyphicon-floppy-disk BtnGlyphicon"></span> Partial Update
                    </asp:LinkButton>   <%--OnClick="btnPartialUpdate_Click"--%>

                    <asp:LinkButton ID="btnSave"  OnClick="btnSave_Click" CssClass="btn-animated bg-green" runat="server" TabIndex="2">
                        <asp:HiddenField ID="TabName" runat="server" />
                        <span class="glyphicon glyphicon-floppy-disk BtnGlyphicon"></span> Save
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnCancel" CssClass="btn-animated bg-horrible" runat="server" OnClick="btnCancel_Click" TabIndex="2">
                             <span class="glyphicon glyphicon-remove BtnGlyphicon"> </span> Cancel 
                    </asp:LinkButton>
                     <div id="divloader" runat="server" style="display: none;">
                    <%--<img id="Img1" alt="" src="~/images/spinner.gif" runat="server" />--%>
                    <img id="Img1" alt="" src="Common/Images/spinner.gif" runat="server" />
                    Loading...
                </div>
                </div>
            </center>
        </div>
    </div>
    <%-- APPLICATION Action Button End --%>
    <input id="hdnUpdate" type="hidden" runat="server" />
    <asp:HiddenField ID="hdnUserId" runat="server" />
    <asp:HiddenField ID="hdnmissingfield" runat="server" />

    <%-- Modal Pop UP for Related Person start --%>
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
                    <iframe id="iframeCFR" src="" width="100%" height="505" style="margin-top: 1%;" frameborder="0" allowtransparency="true"></iframe>
                </div>
                <div class="modal-footer">
                    <div style="text-align: center;">
                        <asp:LinkButton ID="LinkButton1" TabIndex="2" runat="server" CssClass="btn-animated bg-horrible"
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
    <%-- Modal Pop UP for Related Person end --%>


    <%-- Modal Pop UP for Controlling Person start --%>
    <div class="modal" id="myModalRaise1" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true" style="padding-top: 0px;">
        <div class="modal-dialog" style="padding-top: 0px; margin-top: 2px; width: 95%;">
            <div class="modal-content">
                <div class="modal-header" style="margin-top: -10px; margin-bottom: -20px; padding-bottom: 6px ! important;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title" id="myModalLabel">CKYC Controlling Person Details</h4>
                </div>
                <div class="modal-body">
                    <iframe id="iframeCCFR" src="" width="100%" height="505" style="margin-top: 1%;" frameborder="0" allowtransparency="true"></iframe>
                </div>
                <div class="modal-footer">
                    <div style="text-align: center;">
                        <asp:LinkButton ID="lnkRaise" TabIndex="2" runat="server" CssClass="btn-animated bg-horrible"
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
    <%-- Modal Pop UP for Controlling Person end --%>

    <%--<div class="modal" id="myModal" role="dialog" width="45%">
                <div class="modal-dialog modal-sm">

                    <!-- Modal content-->
                    <div class="modal-content" style="width: 100% !important">
                        <div class="modal-header" style="text-align: center; background-color: #dff0d8;">
                            <asp:Label ID="Label1" Text="INFORMATION" runat="server" Font-Bold="true"></asp:Label>

                        </div>
                        <div class="modal-body" style="text-align: center">
                            <asp:Label ID="lbl" runat="server"></asp:Label>
                        </div>
                        <div class="modal-footer" style="text-align: center">
                            <button type="button" class="btn btn-primary" data-dismiss="modal" style='margin-top: -6px;'>
                                <span class="glyphicon glyphicon-ok" style='color: White;'></span> OK

                            </button>

                        </div>
                    </div>

                </div>
            </div>--%>
</asp:Content>
