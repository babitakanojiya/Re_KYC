// For common validation --Created by Pravin


function fnValidatePAN(Obj) {                                       //Function added by Pravin--PAN Validation
    if (Obj == null) Obj = window.event.srcElement;
    if (Obj.value != "") {
        ObjVal = Obj.value;
        var panPat = /^([a-zA-Z]{5})(\d{4})([a-zA-Z]{1})$/;
        var code = /([C,P,H,F,A,T,B,L,J,G])/;
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

function checkemail(str) {
    //var str=document.validation.emailcheck.value
    var filter = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i
    if (filter.test(str)) { return true; } else { return false; }
}

function checkEmailN(obj) {
    var StrMailID = document.getElementById(obj).value
    //Modified by kalpak for cross browser working
    //var XMLDocFS=new ActiveXObject("Msxml2.DOMDocument.3.0");  
    //XMLDocFS.async = false;
    var x = document.getElementById(obj).value;
    var strTotalLenght = x.length
    var atpos = x.indexOf("@");
    var dotpos = x.lastIndexOf(".");
    var strCheckfirstChar = x.substring(0, 1);
    // if (strCheckfirstChar <= 9) {
    //     alert("Not a valid e-mail address");
    //     document.getElementById(obj).value="";
    //     return false;

    // }
    if (strTotalLenght > 1) {
        var lastPrepix = x.substring(dotpos, x.length);
        if (atpos < 3) {
            //alert("Not a valid e-mail address");
            AlertMsg("Please enter valid email address");
            document.getElementById(obj).value = "";
            return false;
        }

        if (lastPrepix == ".com" || lastPrepix == ".net" || lastPrepix == ".org" || lastPrepix == ".biz" || lastPrepix == ".in") {

            if (lastPrepix == ".in") {

                var NewEmailId = x.substring(0, dotpos);
                var dotpos2 = NewEmailId.lastIndexOf(".");
                var lastPrepix2 = NewEmailId.substring(dotpos2, NewEmailId.length);
                if (lastPrepix2 != ".co" || lastPrepix2 == ".co") {

                }
                else {
                    //alert("Not a valid e-mail address");
                    AlertMsg("Please enter valid email address");
                    document.getElementById(obj).value = "";
                    return false;
                }
            }
        }

        else {
            //alert("Not a valid e-mail address");
            AlertMsg("Please enter valid email address");
            document.getElementById(obj).value = "";
            return false;
        }
    }
}


function ValidationPassport(Obj) {                                       //Function added by daksh-- Validation
    debugger;
    if (Obj == null) Obj = window.event.srcElement;
    if (Obj.value != "") {
        ObjVal = Obj.value;
        var panPat = /^[A-Z]{1}[0-9]{7}$/;
        //var panPat = /^[A-PR-WYa-pr-wy][1-9]\d\d{4}[1-9]$/;
        if (panPat.test(Obj.value)) {
            return true;
        }
        else {
            //alert("Invalid Passport no");
            AlertMsg("Please enter valid passport number");
            Obj.value = "";
            Obj.focus();
            return false;
        }
    }
}





function ValidationDriving(Obj) {                                       //Function added by daksh Validation
    debugger;
    if (Obj == null) Obj = window.event.srcElement;
    if (Obj.value != "") {
        ObjVal = Obj.value;
        var panPat = /^[A-Z]{2}[0-9]{13}$/;
        //var panPat = /^[A-PR-WYa-pr-wy][1-9]\d\d{4}[1-9]$/;
        if (panPat.test(Obj.value)) {
            return true;
        }
        else {
            //alert("Invalid Drving License no");
            AlertMsg("Please enter valid driving license number");
            Obj.value = "";
            Obj.focus();
            return false;
        }
    }
}



function ValidationVoterId(Obj) {                                       //Function added by daksh-- Validation
    debugger;
    if (Obj == null) Obj = window.event.srcElement;
    if (Obj.value != "") {
        ObjVal = Obj.value;
        var panPat = /^[A-Z]{3}[0-9]{7}$/;
        //var panPat = /^[A-PR-WYa-pr-wy][1-9]\d\d{4}[1-9]$/;
        if (panPat.test(Obj.value)) {
            return true;
        }
        else {
            AlertMsg("Please enter valid voter id number");
            Obj.value = "";
            Obj.focus();
            return false;
        }
    }
}



function fnValidateAdhar(Obj) {                                       //Function added by Pravin--Aadhaar Validation
    debugger;
    if (Obj == null) Obj = window.event.srcElement;
    if (Obj.value != "") {
        var filter = /^\d{4}$/;
        if (filter.test(Obj.value)) {
            return true;
        }
        else {
            AlertMsg("Please enter valid aadhaar card number");
            Obj.value = "";
            Obj.focus();
            return false;
        }
    }
}

function fnValidateEkyc(Obj) {                                       //Function added by Pravin--KYC Validation
    debugger;
    if (Obj == null) Obj = window.event.srcElement;
    if (Obj.value != "") {
        var filter = /^\d{4}$/;
        if (filter.test(Obj.value)) {
            return true;
        }
        else {
            AlertMsg("Please enter valid E-KYC Authentication number");
            Obj.value = "";
            Obj.focus();
            return false;
        }
    }
}


//function ValidationPassport(id) {                                       //Function added by Pravin--Passport Validation
//    debugger;
//    if (document.getElementById(id).value != "") {
//        if (document.getElementById(id).value.length < 8) {
//            alert("passport number should be more than 7 digits");
//            document.getElementById(id).focus();
//            event.returnValue = false;
//        }
//        var regsaid = /^[A-PR-WYa-pr-wy][1-9]\d\d{4}[1-9]$/;
//        var passtxt = document.getElementById(id).value;
//        var passArray = passtxt.match(regsaid);
//        if (passArray == null) {
//            alert("Passport is not valid.");
//            document.getElementById(id).focus();
//            event.returnValue = false;
//        }
//    }
//}

function ShowReqDtl1(divName, btnName) {
    debugger;
    try {
        var objnewdiv = document.getElementById(divName)
        var objnewbtn = document.getElementById(btnName);
        if (objnewdiv.style.display == "block") {
            objnewdiv.style.display = "none";
            objnewbtn.className = 'glyphicon glyphicon-resize-small'
        }
        else {
            objnewdiv.style.display = "block";
            objnewbtn.className = 'glyphicon glyphicon-resize-full'
        }
    }
    catch (err) {
        ShowError(err.description);
    }
}

function CheckSpaces() {
    //debugger;
    //alert('1.2');
    var strContent = "EmptyPagePlaceholder";
    var strText = document.getElementById(strContent + "txtGivenName").value;
    strText = SpaceTrim(strText);
    document.getElementById(strContent + "txtGivenName").value = strText;
    var count = 0;
    AlertMsg(strText);
    if (strText.length > 0) {
        for (var i = 0; i < strText.length; i++) {
            if (strText.charAt(i) == " ") {
                count++;
            }
        }
        if (count > 2) {
            AlertMsg("More than 2 spaces are not allowed in Given Name");
            document.getElementById(strContent + "txtGivenName").focus();
            return false;
        }
    }
}
//function CheckSpaces(id) {
//    debugger;
//    alert('33');
//    alert(id);
//    //var strContent = "EmptyPagePlaceholder";
//    var strText = document.getElementById(id).value;
//    alert(strText);
//    //strText = SpaceTrim(strText);
//    //document.getElementById(strContent + "txtGivenName").value = strText;
//    var count = 0;
    
//    if (strText.length > 0) {
//        alert('11');
//        for (var i = 0; i < strText.length; i++) {
//            if (strText.charAt(i) == " ") {
//                count++;
//            }
//        }
//        if (count > 2) {
//            alert("More than 2 spaces are not allowed in Given Name");
//            //document.getElementById(id).focus();
//            return false;
//        }
//        else
//        {
//            return true;
//        }
//        alert('22');
//    }
//    return true;
//}

function SpaceTrim(InString) {
    var LoopCtrl = true;
    while (LoopCtrl) {
        if (InString.indexOf("  ") != -1) {
            Temp = InString.substring(0, InString.indexOf("  "));
            InString = Temp + InString.substring(InString.indexOf("  ") + 1, InString.length);
        }
        else
            LoopCtrl = false;
    }
    if (InString.substring(0, 1) == " ")
        InString = InString.substring(1, InString.length);
    if (InString.substring(InString.length - 1) == " ")
        InString = InString.substring(0, InString.length - 1);
    return (InString);
}