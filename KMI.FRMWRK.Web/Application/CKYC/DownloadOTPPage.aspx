<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="DownloadOTPPage.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.CKYC.DownloadOTPPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script type="text/javascript">
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

    // Countdown Timer
    window.onload = function () {
        var timeLeft = 180; // 3 minutes in seconds
        var timerLabel = document.getElementById("countdownTimer");

        var countdown = setInterval(function () {
            var minutes = Math.floor(timeLeft / 60);
            var seconds = timeLeft % 60;

            // Pad with leading zero if needed
            var formattedTime = minutes + ":" + (seconds < 10 ? "0" + seconds : seconds);
            timerLabel.textContent = formattedTime;

            timeLeft--;

            if (timeLeft < 0) {
                clearInterval(countdown);
                timerLabel.textContent = "OTP expired";
                // Optional: Disable the verify button
                var verifyBtn = document.getElementById('<%= btnqcpageopen.ClientID %>');
                if (verifyBtn) {
                    verifyBtn.disabled = true;
                    verifyBtn.style.opacity = "0.5";
                    verifyBtn.style.cursor = "not-allowed";
                }
            }
        }, 1000);
    };
</script>--%>
    <%--<script type="text/javascript">
    function onVerifyClick() {
        var otpInput = document.getElementById('<%= TextBox7.ClientID %>');
        var otp = otpInput.value.trim();

        if (!/^\d{6}$/.test(otp)) {
            alert("Please enter a valid 6-digit OTP.");
            otpInput.focus();
            return false;
        }

        // Optional: You can validate OTP server-side using AJAX before redirecting here

        // Get hidden field value (RefNo)
        var refNo = document.getElementById('<%= hdnRegRefNo.ClientID %>').value;

        // Set PageFlag
        var pageFlag = "01"; // You can make this dynamic if needed

        // Redirect to CKYCQC.aspx with query string
        //window.location.href = "CKYCQC.aspx?RefNo=" + encodeURIComponent(refNo) + "&PageFlag=" + pageFlag  ;
        window.location.href = "CKYCQC.aspx?RefNo=" + encodeURIComponent(refNo) + "&PageFlag=" + pageFlag + "&Status=QC";


        return false; // prevent postback
    }
</script>--%>

    
    <script type="text/javascript">
    // Countdown Timer Initialization
    var countdown;

    window.onload = function () {
        var timeLeft = 180; // 3 minutes in seconds
        var timerLabel = document.getElementById("countdownTimer");

        countdown = setInterval(function () {
            var minutes = Math.floor(timeLeft / 60);
            var seconds = timeLeft % 60;

            var formattedTime = minutes + ":" + (seconds < 10 ? "0" + seconds : seconds);
            timerLabel.textContent = formattedTime;

            timeLeft--;

            if (timeLeft < 0) {
                clearInterval(countdown);
                timerLabel.textContent = "OTP expired";

                var verifyBtn = document.getElementById('<%= btnqcpageopen.ClientID %>');
                if (verifyBtn) {
                    verifyBtn.disabled = true;
                    verifyBtn.style.opacity = "0.5";
                    verifyBtn.style.cursor = "not-allowed";
                }
            }
        }, 1000);
    };

    // OTP Validation + Redirection
    function onVerifyClick() {
        var otpInput = document.getElementById('<%= TextBox7.ClientID %>');
        var otp = otpInput.value.trim();

        if (!/^\d{6}$/.test(otp)) {
            alert("Please enter a valid 6-digit OTP.");
            otpInput.focus();
            return false;
        }

        // Get hidden field value (RefNo)
        var refNo = document.getElementById('<%= hdnRegRefNo.ClientID %>').value;
        var pageFlag = "01";

        // Redirect to CKYCQC.aspx with query string
        window.location.href = "CKYCQC.aspx?RefNo=" + encodeURIComponent(refNo) + "&PageFlag=" + pageFlag + "&Status=QC";

        return false; // Prevent postback
    }
</script>



</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">
    <div style="text-align: center; padding: 30px;">
        <h1>Enter OTP</h1>

        <label for="TextBox7">Enter OTP</label><br />
        <div style="display: flex; justify-content: center; margin-top: 10px;">
            <asp:TextBox ID="TextBox7" runat="server" CssClass="form-control" placeholder="e.g. 849477" 
                         MaxLength="6" style="width: 220px; border-radius: 10px;" />
            <asp:RegularExpressionValidator ID="RegexOtp" runat="server" ControlToValidate="TextBox7" 
                ValidationExpression="^\d{6}$" ErrorMessage="OTP must be exactly 6 digits." 
                ForeColor="Red" Display="Dynamic" ValidationGroup="otpGroup" />
        </div>

        <label id="countdownTimer" style="display: block; margin-top: 15px;">3:00</label>

        <div style="display: flex; gap: 20px; justify-content: center; margin-top: 20px;">
            <%--<asp:Button ID="btnqcpageopen" runat="server" Text="Verify" 
                OnClientClick="return validateOtp('<%= TextBox7.ClientID %>');" 
                OnClick="QCPageopen_Click"
                CausesValidation="true"
                ValidationGroup="otpGroup"
                style="padding: 12px 40px; background-color:#007bff; color:white; border:none; border-radius:5rem; cursor:pointer;" />--%>
          <asp:Button ID="btnqcpageopen" runat="server" Text="Verify"
    OnClientClick="return onVerifyClick();" 
    UseSubmitBehavior="false"
    style="padding: 12px 40px; background-color:#007bff; color:white; border:none; border-radius:5rem; cursor:pointer;" />


            <asp:Button ID="Button4" runat="server" Text="Retry" 
                style="padding: 12px 40px; background-color:white; color:black; border-color:#007bff; border-radius:5rem; border:0.2rem solid blue; cursor:pointer;" />
        </div>

        <p style="color: blue; margin-top: 20px;">Please enter the OTP sent to your registered mobile number.</p>
        <asp:HiddenField ID="hdnRegRefNo" runat="server" />

    </div>
</asp:Content>
