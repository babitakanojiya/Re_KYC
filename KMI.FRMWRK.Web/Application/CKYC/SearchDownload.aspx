<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Empty.Master" CodeBehind="SearchDownload.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.CKYC.SearchDownload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/bundles/CKYCValidationjs") %>
    <%: Styles.Render("~/bundles/CKYCcss") %>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet"/>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css" rel="stylesheet"/>
    <script lang="javascript" type="text/javascript">

        function popup() {
            var varFooter = '<center><button type="button" class="btn btn-success" data-dismiss="modal" style="margin-top: -6px;"><span class="glyphicon glyphicon-ok" style="color: White;"></span> OK </button></center>';
            showModal('#myModal', 'Alert', 'alert-warning', varFooter, '', 'Document uploaded successfully.. Please proceed with Quality Approval process ');
        }
    </script>

    <script type="text/javascript">

        function callCalender(id) {
            debugger;
            if (id == "txtDOB") {
                var dateArr = $("#<%=txtDOB.ClientID%>").val().split('-');
                $("#<%= txtDOB.ClientID%>").datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd-mm-yy', yearRange: '1945:' + (new Date).getFullYear() });
                $.datepicker.initialized = true;
                $("#<%= txtDOB.ClientID%>").focus();
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
         $(document).ready(function () {
             debugger;
             if (EmptyPagePlaceholder_hdnSearch.value != "EmptyPagePlaceholder_Search") {
                 checktab(document.getElementById('<%= Search.ClientID %>'), menu1);
            }
            else {
                checktab(document.getElementById('<%= Download.ClientID %>'), menu2);
            }
        });

        function ShowProgressBar(Msg) {
            debugger;
            var Msg = Msg

            document.getElementById('dvProgressBar').style.display = "block";
            document.getElementById('EmptyPagePlaceholder_lblMsg').innerHTML = Msg;
            setTimeout(function () { HideProgressBar(); }, 5000);
        }
        function HideProgressBar() {
            debugger;

            document.getElementById('dvProgressBar').style.display = "none";

        }

        function checktab(id, menu) {
            debugger;
            var tab;
            if (id == 'EmptyPagePlaceholder_Search' || id == 'EmptyPagePlaceholder_Download') {
                tab = id;
            }
            else {
                tab = id.id;
            } // gets text contents of clicked li
            if (tab == 'EmptyPagePlaceholder_Search') {
                document.getElementById('EmptyPagePlaceholder_Download').classList.remove("active");
                document.getElementById('EmptyPagePlaceholder_Search').classList.add("active");
                document.getElementById('menu1').classList.add('tab-pane', 'fade', 'active');
                document.getElementById('menu2').classList.remove('tab-pane', 'fade');
                //document.getElementById('EmptyPagePlaceholder_hdnSearch').value ="Search"
            }
            if (tab == 'EmptyPagePlaceholder_Download') {
                document.getElementById('EmptyPagePlaceholder_Download').classList.add("active");
                document.getElementById('EmptyPagePlaceholder_Search').classList.remove("active");
                document.getElementById('menu2').classList.add('tab-pane', 'fade', 'active');
                document.getElementById('menu1').classList.remove('tab-pane', 'fade');
                //document.getElementById('EmptyPagePlaceholder_hdnDownload').value ="Download"
            }
            if (tab == 'EmptyPagePlaceholder_Search') {
                document.getElementById('EmptyPagePlaceholder_div2').style.display = 'block';
                document.getElementById('EmptyPagePlaceholder_div4').style.display = 'none';
                document.getElementById('EmptyPagePlaceholder_hdnSearch').value ="Search"
            }
            else if (tab == 'EmptyPagePlaceholder_Download') {
                var flag = "Relatedtab"
                document.getElementById('EmptyPagePlaceholder_div2').style.display = 'none';
                document.getElementById('EmptyPagePlaceholder_div4').style.display = 'block';
                document.getElementById('EmptyPagePlaceholder_Div1').style.display = 'none';
                document.getElementById('EmptyPagePlaceholder_hdnSearch').value ="Download"
            }

        };

       <%-- function searchContent() {
        var currentTab = document.getElementById('<%= hdnCurrentTab.ClientID %>').value;

        // Perform different actions based on the current tab
        if (currentTab === 'Search') {
            // Perform search for Tab 1
            alert('Searching content for Tab 1...');
        } else if (currentTab === 'Download') {
            // Perform search for Tab 2
            alert('Searching content for Tab 2...');
        }
        }--%>
    </script>



    <style>
/*        .panel-success {
            border-color: #00b4bf;
        }*/

        .nav-tabs > li.active > a > span {
            padding: 10px 15px;
            font-weight: bold;
            color: #fff;
            background-color: darkblue;
        }

        .nav-tabs > li > a {
            border-radius: 0px !important;
            padding: 10px 10px;
            background-color: #fff;
            border: 1px solid #00b4bf;
            border-bottom-color: transparent;
        }


            .nav-tabs > li > a > span {
                font-weight: bold;
                color: #000;
            }

        .nav-tabs > li.active > a > span {
            padding: 10px 15px;
            font-weight: bold;
            color: #fff;
            background-color: darkblue;
        }

        .tab-content {
            border-left: 1px solid #00b4bf;
            border-right: 1px solid #00b4bf;
            border: 1px solid #00b4bf;
            padding: 0px !important;
        }

        .nav-tabs > li.active > a, .nav-tabs > li.active > a:focus, .nav-tabs > li.active > a:hover {
            color: #555;
            cursor: default;
            background-color: darkblue !important;
            border: 1px solid darkblue;
            border-bottom-color: darkblue;
        }
    </style>

    <style>
        .nav-tabs > li.active > a > span {
            padding: 10px 15px;
            font-weight: bold;
            color: #fff;
            background-color: darkblue;
        }

        .nav-tabs > li > a {
            border-radius: 0px !important;
            padding: 10px 10px;
            background-color: #fff;
            border: 1px solid #00b4bf;
            border-bottom-color: transparent;
        }


            .nav-tabs > li > a > span {
                font-weight: bold;
                color: #000;
            }

        .nav-tabs > li.active > a > span {
            padding: 10px 15px;
            font-weight: bold;
            color: #fff;
            background-color: darkblue;
        }

        .tab-content {
            border-left: 1px solid #00b4bf;
            border-right: 1px solid #00b4bf;
            border: 1px solid #00b4bf;
            padding: 0px !important;
        }

        .nav-tabs > li.active > a, .nav-tabs > li.active > a:focus, .nav-tabs > li.active > a:hover {
            color: #555;
            cursor: default;
            background-color: darkblue !important;
            border: 1px solid darkblue;
            border-bottom-color: darkblue;
        }
    </style>
    <style type="text/css">
        .pad {
            text-align: center !important;
        }
		.card {
			position: relative;
			display: flex;
			flex-direction: column;
			min-width: 0;
			word-wrap: break-word;
			background-color: #fff;
			background-clip: border-box;
			border: 1px solid rgba(0,0,0,.125);
			border-radius: 0.25rem;
		}
        h1{
            color: #1f50a7;
            font-weight: bold;
            text-align:center;
        }
        h3{
            color:#1999ed;
            text-align:center;

        }
        #nextarrow{
                  width: 50px;
                  height: 50px;
                  border-radius: 50%;
                  border: 2px solid #007bff;
                  background-color: white;
                  color: #007bff;
                  font-size: 24px;
                  cursor: pointer;
                  display: flex;
                  align-items: start;
                  justify-content: center;
        }
    </style>
    <%--Added by Vikash K on 21May2025--%>
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
  <style>
    .step-labels {
      display: flex;
      justify-content: space-between;
      margin-bottom: 1rem;
    }
    .step-label {
      flex: 1;
      text-align: center;
      padding: 10px;
      border-radius: 5px;
      font-weight: 500;
      color: #000;
      position: relative;
      display: flex;
      justify-content: center;
      align-items: center;
      gap: 1rem;
    }
    .step-label.active {
      border: 2px solid #0d6efd;
      background-color: #e6f0ff;
      color: #0d6efd;
      font-weight: bold;
    }
    .step-label::before {
      display: flex;
      justify-content: center;
      align-items: center;
      gap: 1rem;
        }
    .form-section {
      border: 1px solid #ddd;
      padding: 1.5rem;
      border-radius: 8px;
      background-color: #fff;
    }
    .form-section h6 {
      background-color: #e9f4ff;
      padding: 10px;
      font-weight: 500;
      margin-bottom: 20px;
      border-left: 4px solid #0d6efd;
    }
    .btn-group-footer {
      display: flex;
      justify-content: center;
      gap: 1rem;
      margin-top: 20px;
    }
    .verification-btn {
    width: 13rem;
    border-radius: 5rem;
    border: 2px solid black;
    background-color: white;
    color: black;
    transition: background-color 0.3s, color 0.3s, border-color 0.3s;
}

.verification-btn:hover {
    background-color: blue;
    color: white;
    border-color: blue;
}
.loader-overlay {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(255, 255, 255, 0.8);
    z-index: 9999;
    display: flex;
    justify-content: center;
    align-items: center;
    flex-direction: column;
}

.loader-content img {
    width: 60px; /* Adjust size of your spinner gif */
    height: 60px;
}
  </style>
    <script>
        function hideDiv(toHide, toShow) {
            document.getElementById(toHide).style.display = "none";
/*            document.getElementById("divloadergrid").style.display = "flex";*/
            setTimeout(function () {
                document.getElementById('loader').style.display = "none";
                document.getElementById(toShow).style.display = "block";
            }, 3000);

        }

        function showmodalpopup(toShow) {
            document.getElementById(toShow).style.display = "block";
        }
        function validateAadhaar() {
            debugger;
            var input = document.getElementById("aadhaarNumber").value.trim();
            var name = document.getElementById("fullName").value.trim(); 
            var dob = document.getElementById("dob").value.trim();
            var gender = document.querySelector('input[name="gender"]:checked');
            var aadhaarPattern = /^[2-9]{1}[0-9]{11}$/;

                if (!aadhaarPattern.test(input)) {
                    alert("Invalid AADHAAR number.\nPlease enter a valid 12-digit number first digit cannot be 0 or 1(e.g. 1234 5678 9012)");
                    return false;
                }
                else if (name == "") {
                    alert("Please Enter Full name");
                    return false;
                }
                else if (dob === "") {
                    alert("Please Enter Date of Birth)");
                    return false;
                }
                else if (!gender) {
                    alert("Please select your gender.");
                    return false;
            }
            //if (is18OrOlder(dob)) {
            //    return true;
            //}
            //else {
            //    return false;
            //}
                hideDiv('Aadharpage2', 'PANpage3');
            return true;
        }

        function resetFields(...fieldIds) {
            fieldIds.forEach(id => {
                const field = document.getElementById(id);

                if (!field) {
                    // Special case: clear radio group by name
                    if (id === "gender") {
                        const radios = document.getElementsByName("gender");
                        radios.forEach(radio => radio.checked = false);
                     }
                    return;
                }
                
                else {
                    return false;
                }
                if (field.type === "checkbox") {
                    field.checked = false;
                } else if (field.type === "radio") {
                    document.getElementsByName(field.name).forEach(r => r.checked = false);
                } else {
                    field.value = "";
                }
            });
        }

        function callSearchPanAjax() {
            debugger;
            var pan = document.getElementById('<%= PanNo.ClientID %>').value.trim();
            var dob = document.getElementById('<%= pandob.ClientID %>').value.trim();

            $.ajax({
                type: "POST",
                url: "SearchDownload.aspx/SearchPan",
                data: JSON.stringify({ pan: pan, dob: dob }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    debugger;
                    // Parse the stringified JSON returned by WebMethod
                    var data = JSON.parse(response.d);
                    var fullName = data.fullName;
                    var dbData = data.dbData;
                    var ckycrefno = dbData.REFERENCE_ID;
                    var ckycno1 = dbData.CKYCNo;
                    var fathername = dbData.FatherName;
                    var verificationdate = dbData.KYCVerificationDate;
                    var dateofbirth = dbData.Age;
                    var name = dbData.Fullname;
                    var docs = dbData.DOC_NAME;
                    var image = '<%= Session["ApplicantImageBytes"] != null ? Session["ApplicantImageBytes"].ToString() : "" %>';
                    //var image = dbData.PhotoBase64;
                    // Set full name (from API)
                    if (docs =="Proof of Possession of Aadhaar") {
                        docs="Aadhar"
                    }
                    document.getElementById('<%= lblAccountName.ClientID %>').innerText = fullName; 
                    document.getElementById('<%= lblFullName.ClientID %>').innerText = name;
                    document.getElementById('<%= lblCKYCRefNo.ClientID %>').innerText = ckycrefno;
                    document.getElementById('<%= lblCKYCNo1.ClientID %>').innerText = ckycno1;
                    document.getElementById('<%= lblFatherName.ClientID %>').innerText = fathername;
                    document.getElementById('<%= lblCKYCDate.ClientID %>').innerText = verificationdate;
                    document.getElementById('<%= lblage1.ClientID %>').innerText = dateofbirth;
                    document.getElementById('<%= lblIdentityDocs.ClientID %>').innerText = docs; 
                    document.getElementById('<%= lblAccountNumber.ClientID %>').innerText = pan;
                    if (image) {
                        document.getElementById('<%= applicantImage.ClientID %>').src = "data:image/jpeg;base64," + image;
                     }


                    // Simulate "redirect" by hiding/showing panels
                    hideDiv('PANpage2', 'PANpage3');
                    startTimer();
                },
                error: function (xhr, status, error) {
                    alert("API call failed: " + error);
                }
            });
        }


        function CKYCPANValidate() {
            var pan = document.getElementById('<%= PanNo.ClientID %>').value.trim();
            var dob = document.getElementById('<%= pandob.ClientID %>').value.trim();
    var panRegex = /^[A-Z]{5}[0-9]{4}[A-Z]$/;

        if (!panRegex.test(pan)) {
            alert("Please enter a valid PAN Number (e.g., ABCDE1234F).");
            return false;
        }

            if (!is18OrOlder('<%= pandob.ClientID %>')) {
                alert("You must be at least 18 years old.");
                return false;
            }

            // Validation passed, call AJAX
            callSearchPanAjax();

            // Prevent full postback (we are handling everything via AJAX)
            return false;
        }


        function CKYCValidate() {
          var ckycno = document.getElementById('<%= txtCKYC.ClientID %>').value.trim();  
          var regex = /^[1-9][0-9]{13}$/;
          // regx = ^\d{14}$
 
 
         if (!regex.test(ckycno)) {
             alert("Please enter a valid 14-digit CKYC Number.");
             return false;
         }
            if (is18OrOlder('<%= ckycDOB.ClientID %>')) {
                return true;
            }
            else {
                return false;
            }
            callSearchPanAjax();
         return true;
        }

        function is18OrOlder(dobId) {
            const dobInput = document.getElementById(dobId);
            if (!dobInput || !dobInput.value) {
                alert("Please enter your Date of Birth.");
                return false;
            }

            const dob = new Date(dobInput.value);
            const today = new Date();

            const age = today.getFullYear() - dob.getFullYear();
            const m = today.getMonth() - dob.getMonth();
            const isBirthdayPassed = m > 0 || (m === 0 && today.getDate() >= dob.getDate());

            const finalAge = isBirthdayPassed ? age : age - 1;

            if (finalAge < 18) {
                alert("You must be at least 18 years old.");
                return false;
            }

            return true;
        }

        function generateUniqueID() {
            const timestamp = Date.now(); // e.g. 1717082030000
            const randomNum = Math.floor(Math.random() * 100000); // e.g. 83920
            return "Web-" + timestamp + "-" + randomNum;
        }

        let timerInterval;  // To hold the interval ID
        let totalTime = 5 * 60; // 5 minutes in seconds

        function startTimer() {
            const timerLabel = document.getElementById('countdownTimer');
            totalTime = 3 * 60; // reset timer to 3 minutes if needed

            // Clear any existing timer before starting new one
            if (timerInterval) {
                clearInterval(timerInterval);
            }

            timerInterval = setInterval(() => {
                let minutes = Math.floor(totalTime / 60);
                let seconds = totalTime % 60;
                timerLabel.textContent = minutes + ":" + (seconds < 10 ? "0" + seconds : seconds);

                if (totalTime === 0) {
                    clearInterval(timerInterval);
                    // Optional: do something when timer ends, e.g. disable OTP input
                } else {
                    totalTime--;
                }
            }, 1000);
        }
        function validateOtp(inputId) {
            var otpInput = document.getElementById(inputId);
            var otp = otpInput.value.trim();

            if (!/^\d{6}$/.test(otp)) {
                alert("Please enter a valid 6-digit OTP.");
                otpInput.focus();
                return false;
            }
            return true;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="container" style="margin-top: 0px; width: 100%;">
                <div class="page-container" style="margin-top: 0px;">
                    <div id="trDgViewDtl" runat="server" class="page-container" style="margin-top: 0px;">
                        <div class="panel" style='margin: 0px'>
                        <%--<div class="panel" style='margin-right: 26px; margin-left: 26px;'>--%>
                            <%--<div id="tblupload" runat="server" class="panel-heading" onclick="showHideDiv('div9','btnpnlcfrdtls');return false;">--%>

<%--                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <span class="glyphicon glyphicon-menu-hamburger" style=""></span>
                                        <asp:Label ID="lblCanddoc" runat="server" Text="SEARCH & DOWNLOAD" CssClass="control-label"></asp:Label>

                                    </div>
                                </div>--%>
                            <%--</div>--%>

                            <div id="div9" class="panel-body" style="margin-left: 2%; margin-right: 2%; margin-top: 0">
                                <asp:Button ID="btnend" runat="server" OnClick="btnend_Click" Style="display: none" OnClientClick="ShowProgressBar('Loading Data..Please wait')" />
                                <div class="panel-body">
                                    <div id="loader" class="loader-overlay" style="display: none;">
                                        <div class="loader-content">
                                            <img id="Img3" alt="Loading..." src="~/images/spinner.gif" runat="server" />
                                            <p style="margin-top: 1rem; font-size: 1.2rem;">Loading...</p>
                                        </div>
                                    </div>

                                    <%-- Added by Vikash K on 26May2025 page1 --%>
                                    <div id="onboardpage1">
                                            <h1 style="color:#1f50a7;font-size:4rem;font-weight:bold;">Let's complete your KYC</h1>
                                                <div style="margin-top: 20px; text-align: center;">
                                                    <img id="onboardimage" src="Images/Onboardp1.jpg" alt="Onboarding Image" style="max-width: 100%; height: 30rem; display: block; margin: -1.5rem auto;" />
    
                                                    <h4 style="line-height: 1.6; margin-top: -2rem;">
                                                    KYC means Know-your-customer,which every financial institution<br />
                                                    must perform before offering there services to any customer.<br />
                                                    So please follow simple steps.
                                                </h4>
                                                </div>

                                            <div style="display: flex; justify-content: center; margin-top: 20px;">
                                               <%-- <button type="button" id="nextarrow" onclick="hideDiv('onboardpage1','CKYCpage1')">
                                                    &gt;
                                                </button>--%>
                                                <button type="button" id="start" class="btn btn-primary btn-lg" style="width: 13rem; font-size: initial; border-radius: 5rem;" onclick="hideDiv('onboardpage1','CKYCpage1')">
                                                    Start</button>
                                            </div>
                                        </div>
                                    <%-- Ended by Vikash K on 26May2025 --%>

                                    <%-- Added by Vikash K on 26May2025 CKYCpage1 --%>
                                    <div id="CKYCpage1" style="display:none;">
                                        <h1 style="color:#1f50a7;font-size:4rem;font-weight:bold;">Do you have a CKYC No.?</h1>
                                            <div style="margin-top: 20px; text-align: center;">
                                                <img src="Images/CKYCSample.jpg" alt="CKYC Card Image" style="max-width: 100%; height: 25rem; display: block; margin: 0 auto;" />
    
                                                <h4 style="line-height: 1.6; margin-top: 1rem;font-weight:unset;">
                                                    CKYC number is a unique 14-digit number, assigned by CERSAI as an identifier across all financial institutions.If you<br />
                                                   already have a CKYC number.Please enter this number to seamlessly complete your verification<br />
                                                    without submitting any document.
                                                </h4>
                                            </div>

                                        <div style="display: flex; justify-content: center; margin-top: 20px;gap:6rem;">
                                            <button type="button" id="ckycyes" class="btn btn-primary btn-lg" style="width: 13rem;border-radius: 5rem;" onclick="hideDiv('CKYCpage1','CKYCpage2')">
                                                YES I DO HAVE
                                            </button>
                                            <button type="button" id="ckycno"class="btn btn-light btn-lg" style="width: 13rem;border-radius: 5rem;border:0.2rem solid blue;" onclick="hideDiv('CKYCpage1','PANpage1')">
                                                NO I DON'T HAVE
                                            </button>
<%--                                             <button type="button" id="donot"class="btn btn-primary btn-lg" style="width: 13rem;border-radius: 5rem;" onclick="showmodalpopup('ckycModalpopup')">
                                                I DON'T KNOW
                                            </button>--%>
                                        </div>
                                    </div>
                                    <%-- 28/05/2025 POP-UP PAGE --%>

                                    <%-- Ended by Vikash K on 26May2025 --%>
                                    <div id="CKYCpage2" style="display:none;">
                                    <h1 style="color:#1f50a7;font-size:4rem;font-weight:bold;">Please enter CKYC No.</h1>
                                    <div style="margin-top: 0px; text-align: center;">
                                        <img src="Images/CKYCSample.jpg" alt="CKYC Card Image" style="max-width: 100%; height: 28rem; display: block; margin: 0 auto;" />
    
 
                                        <div style="margin-top: 0px; display: flex; justify-content: center; gap: 30px; flex-wrap: wrap;">
                                        <!-- CKYC Number -->
                                        <div style="text-align: left;">
                                        <label for="txtCKYC" style="font-size:1.3rem;">CKYC Number:</label><br />
                                        <asp:TextBox ID="txtCKYC" runat="server" MaxLength="14" Width="220px" placeholder="e.g. 29473186504217" style=" border-radius: 10px;font-size:1.3rem;" CssClass="form-control"/>
                                        </div>
 
                                          <!-- Date of Birth -->
                                        <div style="text-align: left;">
                                        <label for="txtDOB" style="font-size:1.3rem;">Date of Birth:</label><br />
                                        <asp:TextBox ID="ckycDOB" runat="server" MaxLength="8" TextMode="Date" Width="220px" style=" border-radius: 10px;font-size:1.3rem;"  CssClass="form-control"/>
                                        </div>
                                        </div>
 
                                        <div style="margin-top: 5px;">
                                          <asp:Button ID="Button1" runat="server" Text="SUBMIT" type="button"
                                          BackColor="#007bff" ForeColor="White" BorderStyle="None" Width="150px" Height="35px" 
                                          style="border-radius: 10px;font-size:1.3rem;" 
                                          OnClientClick="if (CKYCValidate()) { hideDiv('CKYCpage2','PANpage3'); } return false;" />
                                        </div>
                                        <div>
                                        <h3 style="color:blue; font-weight:bold; font-size: 12px; margin-top: 7px; ">Please enter your 14 digit CKYC number and Date of Birth</h3>
                                        </div>
                                    </div>
                                        </div>
                                    <%-- Added by Rahul Sawal on 27May2025 for CKYC page2 --%>

                                    <div id="ckycModalpopup" style="display: none; position: fixed; z-index: 999; padding-top: 100px; left: 0; top: 0; width: 100%; height: 100%; overflow: auto; background-color: rgba(0,0,0,0.4);">
                                    <div style="background-color: #fff; margin: auto; padding: 20px 30px; border-radius: 12px; width: 60%; max-width: 500px; box-shadow: 0 4px 10px rgba(0,0,0,0.2); font-family: Arial, sans-serif; text-align: center;">
                                    <p style="line-height: 1.6; font-size: 14px;">
                                     If you have ever submitted your identity and address documents<br />
                                     to any financial institution like a bank or mutual fund,<br /><br />
                                     you may already be having a CKYC number.<br />
                                     Please check with your bank or Mutual Fund Company.<br /><br />
                                     Don’t worry, you can still complete this verification with us,<br />
                                     using some other identity and address proof documents, by clicking on the <strong>[NO I DON'T HAVE]</strong> button.<br /><br />
                                     You can also submit your KYC documents with us,<br />
                                     to get yourself CKYC-registered in the following screens, for your future convenience.
                                     </p>
                                    <button onclick="hideDiv('ckycModalpopup','CKYCpage1')" 
                                                         style="margin-top: 20px; padding: 10px 25px; background-color: #007bff; border: none; color: white; border-radius: 25px; cursor: pointer; font-weight: bold; width: 130px;">
                                                     OK
                                    </button>
                                    </div>
                                    </div>

                                    <%-- Added by Vikash K on 26May2025 CKYCpage1 --%>
                                    <%--<div id="PANpage1" style="display:none;">
                                        <h1 style="color:#1f50a7;font-size:5rem;font-weight:bold;">Do you have a PAN Number?</h1>
                                            <div style="margin-top: 20px; text-align: center;">
                                                <img src="Images/PANSample.jpg" alt="PAN Card Image" style="max-width: 100%; height: auto; display: block; margin: 0 auto;" />
    
                                                <h4 style="line-height: 1.6; margin-top: 1rem;font-weight:unset;">
                                                    Permanent Account Number (PAN) is a 10 character identification number, consisting <br />
                                                    of letters and numbers, issued to all taxpayers, by the Indian Income Tax Department.<br />
                                                    If you have a PAN number, please click Yes. If you don’t have one, you can still continue <br />
                                                    the verification process with other identity documents.
                                                </h4>
                                            </div>

                                        <div style="display: flex; justify-content: center; margin-top: 20px;gap:6rem;">
                                            <button type="button" id="panyes" class="btn btn-primary btn-lg" style="width: 13rem;border-radius: 5rem;" onclick="hideDiv('PANpage1','PANpage2')">
                                                Yes
                                            </button>
                                            <button type="button" id="panno"class="btn btn-light btn-lg" style="width: 13rem;border-radius: 5rem;border:0.2rem solid blue;" onclick="hideDiv('PANpage1','Aadharpage1')">
                                                No
                                            </button>
                                        </div>
                                    </div>--%>
                                    <div id="PANpage1" style="display:none;">
                                    <h1 style="color:#1f50a7;font-size:4rem;font-weight:bold;">OK, Do you have a PAN No.?</h1>
                                        <div style="margin-top: 20px; text-align: center;">
                                            <img src="Images/PANSample.jpg" alt="PAN Card Image" style="max-width: 100%; height: 25rem; display: block; margin: 0 auto;" />
    
                                            <h4 style="line-height: 1.4; margin-top: 0.5rem; font-weight: normal; text-align: center; color: #2b4db0;">Permanent Account Number (PAN) is a 10 character identification number,<br />
                                                consisting of letters and numbers, issued to all taxpayers, by the Indian Income Tax Department.
                                            </h4>
                                            <p style="text-align: center; margin-top: 1rem; font-size: 1.5rem; color: #000;">
                                                If you have a PAN number, please click <strong>YES I DO HAVE</strong>. If you don’t have one,<br />
                                                please click <strong>NO I DON'T HAVE</strong> for the verification process with other identity documents.
                                            </p>
                                        </div>

                                    <div style="display: flex; justify-content: center; margin-top: 20px;gap:6rem;">
                                        <button type="button" id="panyes" class="btn btn-primary btn-lg" style="width: 13rem;border-radius: 5rem;" onclick="hideDiv('PANpage1','PANpage2')">
                                            YES I DO HAVE
                                        </button>
                                        <button type="button" id="panno"class="btn btn-light btn-lg" style="width: 13rem;border-radius: 5rem;border:0.2rem solid blue;" onclick="hideDiv('PANpage1','Aadharpage1')">
                                            NO I DON'T HAVE
                                        </button>
                                    </div>
                                </div>
                                    <div id="PANpage2" style="display:none;">
                                        <h1 style="color:#1f50a7;font-size:4rem;font-weight:bold;">Please enter PAN Number</h1>
                                        <div style="margin-top: 20px; text-align: center;">
                                            <img src="Images/PANSample.jpg" alt="PAN Card Image" style="max-width: 100%; height: 25rem; display: block; margin: 0 auto;" />
                                                <div>
                                            <p style="color:blue; font-weight:bold; font-size: 12px; margin-top: 7px; ">Please enter your 10 digit PAN number and Date of Birth</p>
                                            </div>
 
                                            <div style="margin-top: 20px; display: flex; justify-content: center; gap: 30px; flex-wrap: wrap;">
                                            <!-- PAN Number -->
                                            <div style="text-align: left;">
                                            <label for="txtPAN" style="font-size:1.3rem;">PAN Number:</label><br />
                                            <asp:TextBox ID="PanNo" runat="server" MaxLength="10" Width="220px" placeholder="e.g. BHASD8457D" style=" border-radius: 10px;font-size:1.3rem;" CssClass="form-control"/>
                                            </div>
 
                                              <!-- Date of Birth -->
                                            <div style="text-align: left;">
                                            <label for="txtDOB" style="font-size:1.3rem;">Date of Birth:</label><br />
                                            <asp:TextBox ID="pandob" runat="server" MaxLength="8" TextMode="Date" Width="220px" style=" border-radius: 10px;font-size:1.3rem;"  CssClass="form-control"/>
                                            </div>
                                            </div>
                                          <div style="display: flex; justify-content: center; margin-top: 20px;gap:6rem;">
                                        <button 
                                            id="pansearch" 
                                            type="button"
                                            class="btn btn-primary btn-lg" 
                                            style="width: 13rem; border-radius: 5rem;" 
                                            onclick="CKYCPANValidate();">Submit</button>

                                            <button type="button" id="panclear"class="btn btn-light btn-lg" style="width: 13rem;border-radius: 5rem;border:0.2rem solid blue" onclick="resetFields('<%= PanNo.ClientID %>','<%= pandob.ClientID %>')"> 
                                                Clear
                                            </button>

                                        </div>
                                        </div>
                                            </div>

                                       <div id="PANpage3" style="display:none;">
                                     <h1 style="color:#1f50a7;font-size:3rem;font-weight:bold;">Hurray!</h1>
                                     <div style="text-align: center; margin-top:10px;font-size:medium;">

                                         <!-- Wrapper to hold both cards side by side -->
                                        <!-- Flex Container to hold both cards side by side -->
                                        <div class="d-flex justify-content-center flex-wrap gap-4">

                                            <!-- Block 1: Applicant Info Card -->
                                            <div>
                                                <!-- Line Above Card -->
                                                <h1 style="color: #1f50a7; font-size: 2rem; font-weight: bold;" class="text-center">
                                                    We found you in CKYC Registry
                                                </h1>

                                                <!-- Applicant's Information Card -->
                                                <div class="card shadow-lg p-4 mb-4 bg-white rounded-4" style="max-width: 45rem;">
                                                    <!-- Card Title -->
                                                    <h5 class="card-title text-center mb-4 py-2 rounded-3"
                                                        style="background: linear-gradient(to right, #d5c0c5, #b6b8d6); color: #0a4090; font-size: 1.6rem; font-weight: bold;">
                                                        Applicant's Information
                                                    </h5>

                                                    <!-- Content: Image + Info -->
                                                    <div class="d-flex align-items-start gap-4">
                                                        <!-- Applicant Photo -->
                                                        <div class="text-center">
                                                            <img runat="server" src="~/application/ckyc/images/vikashphoto.jpg" id="applicantImage"
                                                                alt="applicant's photo" class="rounded-circle border border-2 shadow-sm"
                                                                style="width: 120px; height: 120px; object-fit: cover;" />
                                                        </div>

                                                        <!-- Applicant Info -->
                                                        <div class="flex-grow-1">
                                                            <div class="d-flex justify-content-between py-1 border-bottom">
                                                                <span><strong>CKYC Ref No:</strong></span>
                                                                <span><asp:Label ID="lblCKYCRefNo" runat="server" Text="" /></span>
                                                            </div>
                                                            <div class="d-flex justify-content-between py-1 border-bottom">
                                                                <span><strong>CKYC No:</strong></span>
                                                                <span><asp:Label ID="lblCKYCNo1" runat="server" Text="XXXXXXXXXX8278" /></span>
                                                            </div>
                                                            <div class="d-flex justify-content-between py-1 border-bottom">
                                                                <span><strong>Full Name:</strong></span>
                                                                <span><asp:Label ID="lblFullName" runat="server" Text="" /></span>
                                                            </div>
                                                            <div class="d-flex justify-content-between py-1 border-bottom">
                                                                <span><strong>Father's Name:</strong></span>
                                                                <span><asp:Label ID="lblFatherName" runat="server" Text="" /></span>
                                                            </div>
                                                            <div class="d-flex justify-content-between py-1 border-bottom">
                                                                <span><strong>CKYC Date:</strong></span>
                                                                <span><asp:Label ID="lblCKYCDate" runat="server" Text="" /></span>
                                                            </div>
                                                            <div class="d-flex justify-content-between py-1 border-bottom">
                                                                <span><strong>Age</strong></span>
                                                                <span><asp:Label ID="lblage1" runat="server" Text="" /></span>
                                                            </div>
                                                            <div class="d-flex justify-content-between py-1">
                                                                <span><strong>ID Document:</strong></span>
                                                                <span><asp:Label ID="lblIdentityDocs" runat="server" Text="" /></span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <!-- Block 2: Account Details Card -->
                                            <div>
                                                <!-- Line Above Card -->
                                                <h1 style="color: #1f50a7; font-size: 2rem; font-weight: bold;" class="text-center">
                                                    We found you in PAN Registry
                                                </h1>

                                                <!-- Account Details Card -->
                                                <div class="card shadow-lg p-4 mb-4 bg-white rounded-4" style="max-width: 45rem;height: 28.2rem;">
                                                    <!-- Card Title -->
                                                    <h5 class="card-title text-center mb-4 py-2 rounded-3"
                                                        style="background: linear-gradient(to right, #d5c0c5, #b6b8d6); color: #0a4090; font-size: 1.6rem; font-weight: bold;">
                                                        Account Details
                                                    </h5>

                                                    <!-- Content: Image + Details -->
                                                    <div class="d-flex align-items-start gap-4">
                                                        <!-- Image -->
                                                        <div class="text-center">
                                                            <img runat="server" src="~/application/ckyc/images/PersonSample.JPG" id="Img4"
                                                                alt="applicant's photo" class="rounded-circle border border-2 shadow-sm"
                                                                style="width: 120px; height: 120px; object-fit: cover;" />
                                                        </div>

                                                        <!-- Details -->
                                                        <div class="flex-grow-1">
                                                            <div class="d-flex justify-content-between py-1 border-bottom">
                                                                <span><strong>Name:</strong></span>
                                                                <span><asp:Label ID="lblAccountName" runat="server" Text="" /></span>
                                                            </div>
                                                            <div class="d-flex justify-content-between py-1">
                                                                <span><strong>Permanent Account Number:</strong></span>
                                                                <span><asp:Label ID="lblAccountNumber" runat="server" Text="" /></span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>




                                    
                                        <p style="color: black; margin-top: -0.75rem">Please enter the otp sent to your registered mobile number.</p>
                                        <label for="txtCKYC">Enter OTP </label><br />
                                        <div style="display: flex; justify-content: center; margin-top: 20px;">
                                        <asp:TextBox ID="TextBox7" runat="server" CssClass="form-control" placeholder="e.g. 849477" maxlength="6" style="width: 220px; border-radius: 10px; margin-top: -1.5rem" />
                                        <asp:RegularExpressionValidator ID="RegexOtp" runat="server" ControlToValidate="TextBox7" ValidationExpression="^\d{6}$" ErrorMessage="OTP must be exactly 6 digits." ForeColor="Red" Display="Dynamic" />
                                        </div>
                                        <div><label id="countdownTimer">3:00</label>
                                        <asp:LinkButton ID="LinkButton1" runat="server" > Resend</asp:LinkButton>
                                        </div>
                                                                                 <div style="display: flex; gap: 20px; justify-content: center; margin: 0px;">
                                        <asp:Button ID="Button3" runat="server" Text="Download" OnClientClick="validateOtp('<%= TextBox7.ClientID %>');" Onclick="Button3_Click"
                                            style="padding: 12px 40px; background-color:#007bff;  color:white; border:none; border-radius:5rem; cursor:pointer;" />
                                        </div>
                                                                                 </div>
                                     </div>
                                     

                                    <%-- Ended by Vikash K on 26May2025 --%>
                                    <%--Added by Vikash K on 20May2025--%>
                                    <div id="Aadharpage1" style="display:none;">
                                        <h1 style="color:#1f50a7;font-size:4rem;font-weight:bold;">OK, Do you have an AADHAR card?</h1>
                                        <div style="margin-top: 20px; text-align: center;">
                                        <img src="Images/AadharSample.jpg" alt="Aadhar Card Image" style="max-width: 100%; height: 25rem; display: block; margin: 0 auto;" />
                                        <h4 style="line-height: 1.6; margin-top: 1rem;font-weight:unset;">
                                                AADHAR is a unique 12 digit number, issued by UIDAI, that is based on your bio-<br />
                                                metrics and serves as proof of identity and address. If you have an AADHAR <br />
                                                number, please click Yes. If you don’t have one, you can still continue the verifica-<br />
                                                tion process with other identity documents.
                                        </h4>
                                        </div>
                                        <div style="display: flex; justify-content: center; margin-top: 20px;gap:6rem;">
                                        <button type="button" id="aadharyes" class="btn btn-primary btn-lg" style="width: 13rem;border-radius: 5rem;" onclick="hideDiv('Aadharpage1','Aadharpage2')">
                                                Yes
                                        </button>
                                        <button type="button" id="aadharno"class="btn btn-light btn-lg" style="width: 13rem;border-radius: 5rem;border:0.2rem solid blue;" onclick="hideDiv('Aadharpage1','OtherDocs')">
                                                No
                                        </button>
                                        </div>
                                        </div>

                                    <%--<div id="Aadharpage2" style="display:none;">
                                <h1 style="color:#1f50a7;font-size:5rem;font-weight:bold;">Please enter <br /> AADHAR details</h1>
                                <div style="margin-top: 20px; text-align: center;">
                                        <img src="Images/AadharSample.jpg" alt="Aadhar Card Image" style="max-width: 100%; height: auto; display: block; margin: 0 auto;" />

                                    </div>
                                            
                                <div class="row justify-content-center m-5">
 
                                                <!-- Aadhaar Number -->
                                <div class="col-md-2 form-group">
                                <label for="aadhaarNumber" style="font-size:1.3rem;" class="form-label">AADHAAR Number:</label>
                                <input type="text" id="aadhaarNumber" style="font-size:1.3rem;" class="form-control" placeholder="e.g. 3652 746 352" />
                                </div>
 
                                                <!-- Full Name -->
                                <div class="col-md-3 form-group">
                                <label for="fullName" style="font-size:1.3rem;" class="form-label">Full Name (As per Aadhaar):</label>
                                <input type="text" id="fullName" style="font-size:1.3rem;" placeholder="Full Name" class="form-control" />
                                </div>
 
                                                <!-- DOB -->
                                <div class="col-md-2 form-group">
                                <label for="dob" style="font-size:1.3rem;" class="form-label">Date of Birth:</label>
                                <input type="date" style="font-size:1.3rem;" id="dob" class="form-control" />
                                </div>
 
                                                <!-- Gender Radio Buttons -->
                                <div class="col-12 col-md-5 form-group">
                                    <div>
                                        <label class="form-label me-md-3 mb-2 mb-md-0" style="font-size:1.3rem;">Gender:</label>

                                        <div class="d-flex flex-wrap gap-2" style="margin-top: 1rem;">
                                            <div class="form-check form-check-inline">
                                                <input class="form-check-input" type="radio" name="gender" id="male" value="Male" style="border: 1px solid #0d6efd;">
                                                <label class="form-check-label" style="font-size:1.3rem;" for="male">Male</label>
                                            </div>

                                            <div class="form-check form-check-inline">
                                                <input class="form-check-input" type="radio" name="gender" id="female" value="Female" style="border: 1px solid #0d6efd;">
                                                <label class="form-check-label" style="font-size:1.3rem;" for="female">Female</label>
                                            </div>

                                            <div class="form-check form-check-inline">
                                                <input class="form-check-input" type="radio" name="gender" id="transgender" value="Transgender" style="border: 1px solid #0d6efd;">
                                                <label class="form-check-label" style="font-size:1.3rem;" for="transgender">Transgender</label>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                </div>
                                <div style="display: flex; justify-content: center; margin-top: 20px;gap:6rem;">
                                    <button type="button" id="searchaadhar" class="btn btn-primary btn-lg" style="width: 13rem;border-radius: 5rem;" onclick="validateAadhaar();">
                                        Search
                                    </button>
                                    <button type="button" id="clearaadhar"class="btn btn-light btn-lg" style="width: 13rem;border-radius: 5rem;border:0.2rem solid blue;" onclick="resetFields('aadhaarNumber','fullName','dob','gender')">
                                        Clear
                                    </button>
                                </div>
                                </div>--%>
                                    <div id="Aadharpage2" style="display:none;">
                                    <h1 style="color:#1f50a7;font-size:4rem;font-weight:bold;margin-bottom: -2rem;">Please enter <br /> AADHAR details</h1>
                                    <div style="margin-top: 20px; text-align: center;">
                                    <img src="Images/AadharSample.jpg" alt="Aadhar Card Image" style="max-width: 100%; height: 25rem; display: block; margin: -1rem auto;" />
 
                                        </div>
                                    <div class="row justify-content-center m-5">
                                    <!-- Aadhaar Number -->
                                    <div class="col-md-2 form-group">
                                    <label for="aadhaarNumber" style="font-size:1.3rem;" class="form-label">AADHAAR Number:</label>
                                    <input type="text" id="aadhaarNumber" style="font-size:1.3rem;" class="form-control" placeholder="e.g. 3652 746 352" />
                                    </div>
                                    <!-- Full Name -->
                                    <div class="col-md-3 form-group">
                                    <label for="fullName" style="font-size:1.3rem;" class="form-label">Full Name (As per Aadhaar):</label>
                                    <input type="text" id="fullName" style="font-size:1.3rem;" placeholder="Full Name" class="form-control" />
                                    </div>
                                    <!-- DOB -->
                                    <div class="col-md-2 form-group">
                                    <label for="dob" style="font-size:1.3rem;" class="form-label">Date of Birth:</label>
                                    <input type="date" style="font-size:1.3rem;" id="dob" class="form-control" />
                                    </div>
                                    <!-- Gender Radio Buttons -->
                                    <div class="col-12 col-md-5 form-group">
                                    <div>
                                    <label class="form-label me-md-3 mb-2 mb-md-0" style="font-size:1.3rem;">Gender:</label>
 
                                            <div class="d-flex flex-wrap gap-2" style="margin-top: 1rem;">
                                    <div class="form-check form-check-inline">
                                    <input class="form-check-input" type="radio" name="gender" id="male" value="Male" style="border: 1px solid #0d6efd;">
                                    <label class="form-check-label" style="font-size:1.3rem;" for="male">Male</label>
                                    </div>
 
                                                <div class="form-check form-check-inline">
                                    <input class="form-check-input" type="radio" name="gender" id="female" value="Female" style="border: 1px solid #0d6efd;">
                                    <label class="form-check-label" style="font-size:1.3rem;" for="female">Female</label>
                                    </div>
 
                                                <div class="form-check form-check-inline">
                                    <input class="form-check-input" type="radio" name="gender" id="transgender" value="Transgender" style="border: 1px solid #0d6efd;">
                                    <label class="form-check-label" style="font-size:1.3rem;" for="transgender">Transgender</label>
                                    </div>
                                    </div>
                                    </div>
                                    </div>
 
                                    </div>
                                    <div style="display: flex; justify-content: center; margin-top: -2rem;gap:6rem;">
                                    <button type="button" id="searchaadhar" class="btn btn-primary btn-lg" style="width: 13rem;border-radius: 5rem;" onclick="validateAadhaar();">
                                            Search
                                    </button>
                                    <button type="button" id="clearaadhar"class="btn btn-light btn-lg" style="width: 13rem;border-radius: 5rem;border:0.2rem solid blue;" onclick="resetFields('aadhaarNumber','fullName','dob','gender')">
                                            Clear
                                    </button>
                                    </div>
                                    </div>

                                <div id="aadharotp1" style="display:none;">
                                    <div class="step-labels mb-3">
                                    <div class="step-label active">
                                      <div style="font-size: 4rem; font-weight: bold;">1</div>
                                      <div style="display: flex; flex-direction: column; align-items: flex-start; line-height: 1;">
                                        <span style="font-size: 1rem;">PERSONAL</span>
                                        <span style="font-size: 1rem;">DETAILS</span>
                                      </div>
                                    </div>
                                    <div class="step-label"> 
                                          <div style="font-size: 4rem; font-weight: bold;">2</div>
                                  <div style="display: flex; flex-direction: column; align-items: flex-start; line-height: 1;">
                                    <span style="font-size: 1rem;">CONTACT</span>
                                    <span style="font-size: 1rem;">DETAILS</span>
                                  </div>
                                    </div>
                                    <div class="step-label">
                                            <div style="font-size: 4rem; font-weight: bold;">3</div>
                                            <div style="display: flex; flex-direction: column; align-items: flex-start; line-height: 1;">
                                                <span style="font-size: 1rem;">ADDRESS</span>
                                                <span style="font-size: 1rem;">DETAILS</span>
                                            </div>
                                        </div>
                                    <div class="step-label">
                                      <div style="font-size: 4rem; font-weight: bold;">4</div>
                                      <div style="display: flex; flex-direction: column; align-items: flex-start; line-height: 1;">
                                        <span style="font-size: 1rem;">DOCUMENT</span>
                                        <span style="font-size: 1rem;">DETAILS</span>
                                      </div>
                                    </div>
                                    <div class="step-label">
                                  <div style="font-size: 4rem; font-weight: bold;">5</div>
                                  <div style="display: flex; flex-direction: column; align-items: flex-start; line-height: 1;">
                                    <span style="font-size: 1rem;">VERIFICATION</span>
                                    <span style="font-size: 1rem;">DETAILS</span>
                                  </div>
                                </div>
                                  </div>
                                     <div class="form-section">
    <h6>CONTACT DETAILS (All Communication will be sent on provided Mobile No. / Email -ID)</h6>
    <form>
      <div class="row mb-3">
        <div class="col-md-6">
          <label class="form-label">Tel.(Off)</label>
          <input type="text" class="form-control">
        </div>
        <div class="col-md-6">
          <label class="form-label">Tel.(Res.)</label>
          <input type="text" class="form-control">
        </div>
      </div>

      <div class="mb-3">
        <label class="form-label">Mobile</label>
        <input type="text" class="form-control">
      </div>

      <div class="mb-3">
        <label class="form-label">Email</label>
        <input type="email" class="form-control">
      </div>

      <div class="btn-group-footer">
        <button type="button" class="btn btn-lg btn-primary">Previous</button>
        <button type="button" class="btn btn-lg btn-outline-primary">Cancel</button>
        <button type="submit" class="btn btn-lg btn-primary">Next</button>
      </div>
    </form>
  </div>

                                </div>
                                    <div id="CKYCpage3" style="display:none;">
                                    <h1 style="color:#1f50a7;font-size:5rem;font-weight:bold;">Please enter OTP</h1>
                                <div style="margin-top: 20px; text-align: center;">
                                                <img src="Images/CKYCSample.jpg" alt="CKYC Card Image" style="max-width: 100%; height: auto; display: block; margin: 0 auto;">
                                            </div>                               
                                 <div style="text-align: center; margin-top:10px;font-size:medium;">

                                <label for="txtCKYC"  >Enter OTP </label><br />
                                <div style="display: flex; justify-content: center; margin-top: 20px;">
                                <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control" placeholder="e.g. 849477" style="width: 220px; border-radius: 10px; margin-top: -8px;" />
                                <asp:RegularExpressionValidator ID="revOTP" runat="server" ControlToValidate="TextBox6"  ValidationExpression="^\d{6}$" ForeColor="Red" Display="Dynamic" /> 
                                </div>
                                <label for="txtCKYC">5:00 </label>
                                <div style="display: flex; gap: 20px; justify-content: center; margin-top: 5px;">
                                <asp:Button ID="btnVerify" runat="server" Text="Verify" 
                                    style="padding: 12px 40px; background-color:#007bff; color:white; border:none; border-radius:5rem; cursor:pointer;" />
                                <asp:Button ID="btnRetry" runat="server" Text="Retry" 
                                   style="padding: 12px 40px; background-color:white; color:black; border-color:#007bff; border-radius:5rem; cursor:pointer;" />
                                </div>
                                <p style="color: blue; margin-top: 20px">Record found please download the record</p>
                                </div>
<%--                                <div class="text-center mt-5">
                                    <button class="btn btn-outline-primary btn-lg" style="border-radius: 5rem;">
                                        <span class="bi bi-arrow-down" style="display: inline-block; border-bottom: 2px solid currentColor; padding-bottom: 0px;"></span>
                                    </button>
                                </div>--%>


                                 </div>

                                        

                                    <div id="Aadharpage3" style="display:none;">
                                        <h1 style="color:#1f50a7;font-size:5rem;font-weight:bold;">Please enter OTP</h1>
<div style="margin-top: 20px; text-align: center;">
                <img src="Images/AadharSample.jpg" alt="Aadhar Card Image" style="max-width: 100%; height: auto; display: block; margin: 0 auto;">
            </div>                               
 <div style="text-align: center; margin-top:10px;font-size:medium;">
<label for="txtCKYC"  >Enter OTP </label><br />
<div style="display: flex; justify-content: center; margin-top: 20px;">
<asp:TextBox ID="TextBox8" runat="server" CssClass="form-control" placeholder="e.g. 849477" style="width: 220px; border-radius: 10px; margin-top: -8px;" />
<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TextBox8"  ValidationExpression="^\d{6}$" ForeColor="Red" Display="Dynamic" /> 
</div>
<label for="txtCKYC">5:00 </label>
<div style="display: flex; gap: 20px; justify-content: center; margin-top: 5px;">
<asp:Button ID="Button2" runat="server" Text="Verify" 
    style="padding: 12px 40px; background-color:#007bff; color:white; border:none; border-radius:5rem; cursor:pointer;" />
<asp:Button ID="Button5" runat="server" Text="Retry" 
   style="padding: 12px 40px; background-color:white; color:black; border-color:#007bff; border-radius:5rem;border:0.2rem solid blue; cursor:pointer;" />
</div>
<p style="color: blue; margin-top: 20px">Pleas enter the otp sent to your registered mobile number.</p>
</div>
<%--<div class="text-center mt-5">
    <button class="btn btn-outline-primary btn-lg" style="border-radius: 5rem;">
        <span class="bi bi-arrow-down" style="display: inline-block; border-bottom: 2px solid currentColor; padding-bottom: 0px;"></span>
    </button>
</div>--%>


 </div>

                                    <div id="OtherDocs" style="display:none;">
                                        <h1 style="color:#1f50a7;font-size:5rem;font-weight:bold;">Do you have any of the <br /> required documents?</h1>
                                     <h3 style="color:#1999ed;font-size:3.5rem;font-weight: 350;">If yes, please select one.</h3>

                                        <div style="margin-top: 20px; text-align: center;">                                            
                                    <div style="display: flex; justify-content: center; margin-top: 20px;gap:6rem;">
                                        <button type="button" id="btnpassport" class="btn btn-light btn-lg verification-btn"  onclick="hideDiv('PANpage1','PANpage2')">
                                            PASSPORT
                                        </button>
                                        <button type="button" id="btnprivinglic"class="btn btn-light btn-lg verification-btn"  onclick="hideDiv('PANpage1','Aadharpage1')">
                                            DRIVING LICENCE
                                        </button>
                                         <button type="button" id="btnvoter"class="btn btn-light btn-lg verification-btn" onclick="hideDiv('PANpage1','Aadharpage1')">
                                             VOTER ID
                                         </button>
                                    </div>
                                        </div>

                                    <div style="display: flex; justify-content: center; margin-top: 20%;gap:6rem;">
                                        <button type="button" id="otheryes" class="btn btn-primary btn-lg" style="width: 13rem;border-radius: 5rem;" onclick="hideDiv('OtherDocs','PANpage1')">
                                            Submit
                                        </button>
                                        <button type="button" id="otherno"class="btn btn-light btn-lg" style="width: 13rem;border-radius: 5rem;border:2px solid darkblue;" onclick="hideDiv('OtherDocs','onboardpage1')">
                                            No
                                        </button>
                                    </div>
                                </div>
                                    <ul class="nav nav-tabs" id="myList" runat="server" style="display:none"><%-- Added by Vikash K on 26May2025 for hiding old ui --%>
                                        <li class="active" id="Search" runat="server" onclick="checktab(this,'menu1')">
                                            <a data-toggle="tab" href="#menu1">
                                                <span id="LItab" style="font-weight: bold" runat="server">SEARCH</span>
                                            </a>
                                            </a>
                                        </li>
                                        <li id="Download" runat="server" onclick="checktab(this,'menu2')">
                                            <a data-toggle="tab" href="#menu2">
                                                <span style="font-weight: bold">DOWNLOAD</span>
                                                <asp:Label ID="lblcount" runat="server"></asp:Label>
                                            </a>
                                        </li>
                                        <div style="text-align: center; display: none">
                                            <asp:Label ID="lblNote" runat="server" CssClass="control-label" Text="NOTE: All Documents to be Uploaded/Reuploaded should be in TIFF/JPEG/JPG/PDF format"
                                                ForeColor="red"></asp:Label>
                                        </div>
                                    </ul>
                                    <div class="tab-content" style="display:none">
                                        <%-- Added by Vikash K 0n 26May2025 --%>
                                        <div id="menu1" class="tab-pane fade active in">
                                            <div id="div2" class="panel-body" runat="server" style="overflow: auto;">
                                                <div class="row" id="srchby" style="margin-bottom: 5px" runat="server">
                                                    <div class="col-sm-3" style="text-align: left">
                                                        <asp:Label ID="lblSrchBy" runat="server" CssClass="control-label" Text="Search By"
                                                            Font-Bold="False"></asp:Label>
                                                        <span id="Span1" runat="server" style="color: red">*</span>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="ddlSearchby" runat="server" CssClass="form-control"
                                                            AutoPostBack="true" TabIndex="2" OnSelectedIndexChanged="ddlSearchby_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-3" style="text-align: left; display: none">
                                                        <asp:Label ID="lblCndName" CssClass="control-label" runat="server" Text="Name"
                                                            Font-Bold="False"></asp:Label>
                                                        <span id="Span2" runat="server" style="color: red">*</span>
                                                    </div>
                                                    <div class="col-sm-3" style="display: none">
                                                        <asp:TextBox ID="lblAdvNameValue" runat="server" CssClass="form-control" MaxLength="20"
                                                            Enabled="false"> 
                                                        </asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row" id="prfofidnty" style="margin-bottom: 5px; display: none" runat="server">
                                                    <div class="col-sm-3" id="prfid" style="text-align: left">
                                                        <asp:Label ID="lblProofofidn" Text="Proof Of Identity" CssClass="control-label" runat="server"
                                                            Font-Bold="False"></asp:Label>
                                                        <span id="Span3" runat="server" style="color: red">*</span>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="ddlProofofidn" runat="server" CssClass="form-control"
                                                            AutoPostBack="true" TabIndex="2">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-3" style="text-align: left">
                                                        <asp:Label ID="Lblidntynum" Text="Identity Number" CssClass="control-label" runat="server"
                                                            Font-Bold="False"></asp:Label>
                                                        <span id="Span4" runat="server" style="color: red">*</span>
                                                    </div>
                                                    <div class="col-sm-3" id="txtidtnum" style="display: none" runat="server">
                                                        <div class="col-sm-4" style="padding: 0px;">
                                                            <asp:TextBox ID="txtidentynum" CssClass="form-control" Enabled="false" placeholder="XXXX" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-4" style="padding: 0px; padding-left: 6px;">
                                                            <asp:TextBox ID="TextBox1"  placeholder="XXXX" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-4" style="padding: 0px; padding-left: 6px;">
                                                            <asp:TextBox ID="TextBox2" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3" id="dividentynum" style="display:none" runat="server">
                                                        <asp:TextBox ID="txtidnummm" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row" id="applcntnme" style="margin-bottom: 5px; display: none" runat="server">
                                                    <div class="col-sm-3" style="text-align: left">
                                                        <asp:Label ID="lblAppfullnme" Text="Applicant's Full Name" CssClass="control-label" runat="server"
                                                            Font-Bold="False"></asp:Label>
                                                        <span id="Span5" runat="server" style="color: red">*</span>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtAppfullname" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-3" style="text-align: left">
                                                        <asp:Label ID="lblDOB" Text="DOB" CssClass="control-label" runat="server" Font-Bold="False"></asp:Label>
                                                        <span id="Span6" runat="server" style="color: red">*</span>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtDOB" CssClass="form-control" placeholder="dd-mm-yyyy"
                                                                onchange="ValidateDOB(this.value);" runat="server"></asp:TextBox>
                                                            <div class="input-group-btn">
                                                                <div class="btn btn-primary btn-lg-kmi" onclick="callCalender('txtDOB')">
                                                                    <span class="glyphicon glyphicon-calendar BtnGlyphicon"></span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" id="gender" style="margin-bottom: 5px; display: none" runat="server">
                                                    <div class="col-sm-3" style="text-align: left">
                                                        <asp:Label ID="lblGender" Text="Gender" CssClass="control-label" runat="server"
                                                            Font-Bold="False"></asp:Label>
                                                        <span id="Span7" runat="server" style="color: red">*</span>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control"
                                                            AutoPostBack="true" TabIndex="2">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="menu2" class="tab-pane fade active in">
                                                <div id="div4" class="panel-body" runat="server" style="overflow: auto;">
                                                    <div class="row" id="ckyccno" style="margin-bottom: 5px;" runat="server">
                                                        <div class="col-sm-3" style="text-align: left">
                                                            <asp:Label ID="lblckycno" Text="CKYC No." CssClass="control-label" runat="server"
                                                                Font-Bold="False"></asp:Label>
                                                            <span id="Span8" runat="server" style="color: red">*</span>
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <asp:TextBox ID="txtckyno" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-3" style="text-align: left">
                                                            <asp:Label ID="lblAuttype" Text="Authentication Factor Type" CssClass="control-label" runat="server"
                                                                Font-Bold="False"></asp:Label>
                                                            <span id="Span9" runat="server" style="color: red">*</span>
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <asp:DropDownList ID="ddlauthtype" runat="server" CssClass="form-control"
                                                                AutoPostBack="false" TabIndex="2">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="row" id="auth" style="margin-bottom: 5px;" runat="server">
                                                        <div class="col-sm-3" style="text-align: left">
                                                            <asp:Label ID="lblauthfctor" Text="Authentication Factor" CssClass="control-label" runat="server"
                                                                Font-Bold="False"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <asp:TextBox ID="txtauthfactor" CssClass="form-control"
                                                                runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 12px;display:none;" id="divButtons" runat="server">
                                        <%-- Added by Vikash K to hide the buttons on 26May2025 --%>
                                        <div class="col-sm-12" align="center">
                                            <%--    <asp:LinkButton ID="Btncrop" runat="server"  CssClass="btn btn-primary" Text="CROP" visible="false"
                                    CausesValidation="false"  TabIndex="43"></asp:LinkButton>--%>
                                            <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn-animated bg-green" OnClick="btnSubmit_Click" Text="Search" CausesValidation="false" TabIndex="32" OnClientClick="ShowProgressBar('Document submission process is in progress..Please wait')">  </asp:LinkButton>
                                            <asp:LinkButton ID="btnCancel" OnClick="btnCancel_Click" TabIndex="43" runat="server" Text="Cancel"
                                                CssClass="btn-animated bg-horrible">
                            
                                            </asp:LinkButton>
                                        </div>
                                    </div>

                                  

                                    <asp:HiddenField ID="hdnDownload" runat="server" />
                                    <asp:HiddenField ID="hdnSearch" runat="server" />
                                    <asp:HiddenField ID="hdnCurrentTab" runat="server" />

                                
                                    

                            </div>
                        </div>

                    </div>
                    
                        <div id="Div1" runat="server" class="page-container" style="margin-top: 0px;display:none;">
                        <div class="panel panel-success" style='margin-right: 26px; margin-left: 26px;'>
                            <div runat="server" id="trtitle" class="panel-heading" onclick="showHideDiv('trgridsponsorship','span1');return false;">
                                <div class="row" id="trDetails" runat="server">
                                    <div class="col-sm-10" style="text-align: left">
                                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                                        <asp:Label ID="lblprospectsearch" runat="server" Text="CKYC Search Results"></asp:Label>
                                    </div>
                                    <div class="col-sm-2">
                                        <span id="span1" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                                    </div>
                                </div>
                            </div>
                            <div id="trgridsponsorship" runat="server" class="panel-body">
								<div id="divresult" class="card" style="width: 35%;height: 450px;margin-left:29vw;">
									<div class="row" style="text-align:center;background-color:#00b4bf;width: 100%;margin-left:0px;height:7%">
										<asp:Label ID="Label1" runat="server" Text="Applicant Information" style="color:white;font-size:14px"></asp:Label>
									</div>
									<div class="row" style="text-align:center">
										<asp:Image Id="Img1" runat="server" ImageUrl="" Width="20%" style="border:1px solid;"/>
									</div>
									<div class="row" style="text-align:center;padding-top:10px;margin-left:40px">
                                      <div class="col-sm-6" style="text-align: left">
                                        <asp:Label ID="Label2" Text="CKYC Number" CssClass="control-label" runat="server"
                                           Font-Bold="True"></asp:Label>           
                                        </div>
                                      <div class="col-sm-6" style="text-align: left">
                                        <asp:Label ID="lblCkycNoRes" Text="" CssClass="control-label" runat="server"
                                           Font-Bold="False"></asp:Label>           
                                        </div>
									</div>
									<div class="row" style="text-align:center;margin-left:40px">
                                      <div class="col-sm-6" style="text-align: left">
                                        <asp:Label ID="Label4" Text="Full Name" CssClass="control-label" runat="server"
                                           Font-Bold="True"></asp:Label>           
                                        </div>
                                      <div class="col-sm-6" style="text-align: left">
                                        <asp:Label ID="lblFullNm" Text="" CssClass="control-label" runat="server"
                                           Font-Bold="False"></asp:Label>           
                                        </div>									
									</div>
									<div class="row" style="text-align:center;margin-left:40px">
                                      <div class="col-sm-6" style="text-align: left">
                                        <asp:Label ID="Label6" Text="Father's Name" CssClass="control-label" runat="server"
                                           Font-Bold="True"></asp:Label>           
                                        </div>
                                      <div class="col-sm-6" style="text-align: left">
                                        <asp:Label ID="lblFthNm" Text="" CssClass="control-label" runat="server"
                                           Font-Bold="False"></asp:Label>           
                                        </div>								
									</div>
									<div class="row" style="text-align:center;margin-left:40px">
                                      <div class="col-sm-6" style="text-align: left">
                                        <asp:Label ID="Label8" Text="KYC Date" CssClass="control-label" runat="server"
                                           Font-Bold="True"></asp:Label>           
                                        </div>
                                      <div class="col-sm-6" style="text-align: left">
                                        <asp:Label ID="lblKycDt" Text="" CssClass="control-label" runat="server"
                                           Font-Bold="False"></asp:Label>           
                                        </div>								
									</div>
									<div class="row" style="text-align:center;margin-left:40px">
                                      <div class="col-sm-6" style="text-align: left">
                                        <asp:Label ID="Label10" Text="Age" CssClass="control-label" runat="server"
                                           Font-Bold="True"></asp:Label>           
                                        </div>
                                      <div class="col-sm-6" style="text-align: left">
                                        <asp:Label ID="lblAge" Text="" CssClass="control-label" runat="server"
                                           Font-Bold="False"></asp:Label>           
                                        </div>							
									</div>
									<div class="row" style="text-align:center;margin-left:40px">
                                      <div class="col-sm-6" style="text-align: left">
                                        <asp:Label ID="Label3" Text="Identity Document Provided" CssClass="control-label" runat="server"
                                           Font-Bold="True"></asp:Label>           
                                        </div>
                                      <div id="DivDocC" class="col-sm-6" runat="server" style="text-align: left">
<%--                                        <asp:Label ID="lblIdDoc" Text="" CssClass="control-label" runat="server"
                                           Font-Bold="False"></asp:Label>  --%>         
                                        </div>							
									</div>
									<div class="row" style="text-align:center;margin-left:40px">
                                      <div class="col-sm-6" style="text-align: left">
                                        <asp:Label ID="Label12" Text="Remark" CssClass="control-label" runat="server"
                                           Font-Bold="True"></asp:Label>           
                                        </div>
                                      <div class="col-sm-6" style="text-align: left">
                                        <asp:Label ID="lblRmk" Text="" CssClass="control-label" runat="server"
                                           Font-Bold="False"></asp:Label>           
                                        </div>									
									</div>
								</div>
<%--                                <asp:GridView ID="dgView" runat="server" AllowSorting="True" CssClass="footable" Width="100%"
                                    AutoGenerateColumns="False" PageSize="100" AllowPaging="true" CellPadding="1">
                                    <HeaderStyle HorizontalAlign="Center" BackColor="#dce9f9" />
                                    <FooterStyle CssClass="GridViewFooter" />
                                    <RowStyle CssClass="GridViewRow" />

                                    <SelectedRowStyle CssClass="GridViewSelectedRow" />
                                    <AlternatingRowStyle CssClass="GridViewAlternateRow"></AlternatingRowStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="CKYC NO" ItemStyle-Width="20%" HeaderStyle-CssClass="pad">
                                            <ItemTemplate>
												<asp:Label ID="lblCkycNo" runat="server" Text='<%# Bind("CKYC_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="12%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NAME" ItemStyle-Width="20%" HeaderStyle-CssClass="pad">
                                            <ItemTemplate>
												<asp:Label ID="lblName" runat="server" Text='<%# Bind("NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FATHERS NAME" ItemStyle-Width="20%" HeaderStyle-CssClass="pad">
                                            <ItemTemplate>
												<asp:Label ID="lblFathNam" runat="server" Text='<%# Bind("FATHERS_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="AGE" ItemStyle-Width="15%" HeaderStyle-CssClass="pad">
                                            <ItemTemplate>
												<asp:Label ID="lblAge" runat="server" Text='<%# Bind("AGE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="pad" Width="8%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IMAGE TYPE" ItemStyle-Width="10%" HeaderStyle-CssClass="pad">
                                            <ItemTemplate>
												<asp:Label ID="lblImgTyp" runat="server" Text='<%# Bind("IMAGE_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="pad" Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PHOTO" ItemStyle-Width="20%" HeaderStyle-CssClass="pad">
                                            <ItemTemplate>
												<asp:Image ID="img" runat="server" ImageUrl='<%# Eval("PHOTO") %>' Width="50%"></asp:Image>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="12%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="KYC DATE" ItemStyle-Width="20%" HeaderStyle-CssClass="pad">
                                            <ItemTemplate>
												<asp:Label ID="lblKycdt" runat="server" Text='<%# Bind("KYC_DATE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UPDATED DATE" ItemStyle-Width="20%" HeaderStyle-CssClass="pad">
                                            <ItemTemplate>
												<asp:Label ID="lblUpddt" runat="server" Text='<%# Bind("UPDATED_DATE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="18%" HeaderText="REMARKS" HeaderStyle-CssClass="pad">
                                            <ItemTemplate>
												<asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("REMARKS") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="7%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>--%>
                                <br />

                                <br />
                                <div class="col-sm-3" style="text-align: left;display: none" >
                                    <asp:Label ID="lblPageInfo" runat="server" Visible="false"></asp:Label>
                                </div>
                                <div id="divloadergrid" class="col-sm-12" runat="server" style="display: none;">
                                    <caption>
                                        <img id="Img2" alt="" src="~/images/spinner.gif" runat="server" />
                                        Loading...
                                    </caption>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
