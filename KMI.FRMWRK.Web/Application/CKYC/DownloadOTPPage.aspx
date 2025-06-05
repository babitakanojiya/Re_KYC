<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="DownloadOTPPage.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.CKYC.DownloadOTPPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/bundles/CKYCValidationjs") %>
    <%: Styles.Render("~/bundles/CKYCcss") %>


    <style>
    .loader-overlay {
        position: fixed;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        background-color: rgba(255, 255, 255, 0.8); /* translucent background */
        display: flex;
        justify-content: center;
        align-items: center;
        z-index: 9999;
    }

    .loader-content {
        text-align: center;
    }
</style>

    
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
<script>
    function ShowProgressBar(message) {
        document.getElementById('<%= dvProgressBar.ClientID %>').style.display = 'flex';
        document.getElementById('<%= lblMsg.ClientID %>').innerText = message || "Loading...";
    }

 
function HideProgressBar() {
     debugger;
     document.getElementById('dvProgressBar').style.display = "none";
    }
</script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">
    <div>
               <div id="dvProgressBar" style="display: none;" class="loader-overlay" runat="server">
    <div class="loader-content">
        <asp:Image id="ldr" src="../../Images/horizonal_loader.gif" height="50px" alt="" runat="server" ImageAlign="Middle"/>
        <br />
        <asp:Label ID="lblMsg" Text="" runat="server" ForeColor="Blue" style="font-size: medium; font-weight: bold;"></asp:Label>
    </div>
</div>

        
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
            
         <%-- <asp:Button ID="btnqcpageopen" runat="server" Text="Verify"
    OnClientClick="return onVerifyClick();" 
    UseSubmitBehavior="false"
    style="padding: 12px 40px; background-color:#007bff; color:white; border:none; border-radius:5rem; cursor:pointer;" />--%>

            <asp:Button ID="btnqcpageopen" runat="server" Text="Verify"
    OnClientClick="ShowProgressBar('Searching..Please wait'); return onVerifyClick();"
    UseSubmitBehavior="false"
    style="padding: 12px 40px; background-color:#007bff; color:white; border:none; border-radius:5rem; cursor:pointer;" />

            <asp:Button ID="Button4" runat="server" Text="Retry" 
                style="padding: 12px 40px; background-color:white; color:black; border-color:#007bff; border-radius:5rem; border:0.2rem solid blue; cursor:pointer;" />
        </div>

        <p style="color: blue; margin-top: 20px;">Please enter the OTP sent to your registered mobile number.</p>
        <asp:HiddenField ID="hdnRegRefNo" runat="server" />

    </div>
        
        </div>
</asp:Content>
