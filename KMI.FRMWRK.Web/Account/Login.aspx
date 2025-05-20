<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="KMI.FRMWRK.Web.Account.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <%: Styles.Render("~/bundles/FRMWRKcss") %>
    <%: Scripts.Render("~/bundles/FRMWRKjs") %>

    <style>
        body {
            background: rgba(49, 112, 143, 0.06);
        }
    </style>
    <script>
        function ForgotPassword() {
            showPageInModal('#myModal', 'Forgot Password', '', '/CKYCILFS/Account/ForgotPassword.aspx', 'frame-small');
        }

        function AlertMsg(msg) {
            showModal('#myModal', 'Alert', 'alert-warning', '', '', msg);
        }
    </script>
    <style type="text/css">
        /*
            @media only screen and (min-device-width : 10px) and (max-device-width : 1366px) and (-webkit-min-device-pixel-ratio: 1.5) {

            body {
                background-color: white;
            }

            .closeDiv {
                display: block !important;
                width: 100%;
                height: auto;
            }

            .divmain {
                display: none !important;
            }

            #divclose {
                margin-top: 3%;
            }
        }

        */

        /* Start iPads (landscape) ----------- */
        @media only screen and (min-device-width : 768px) and (max-device-width : 1366px) and (orientation : landscape) and (-webkit-min-device-pixel-ratio: 1.5) {
            #bafindialogo {
                height: 113px !important;
                width: 315px !important;
            }

            #ErrorLogo {
                height: 145px !important;
            }

            #divbafinfialogo {
                margin-top: 0% !important;
            }

            #divmsg {
                margin-top: 1% !important;
            }

            #divclose {
                height: auto;
            }

            #diverrorimg {
                margin-top: 3%;
            }

            #divkmiLogofooter {
                margin-top: 7%;
            }

            #KMILogo {
                width: 40% !important;
            }

            #spn1 {
                font-size: 20px !important;
            }

            #spn2 {
                font-size: 20px !important;
            }
        }

        @media only screen and (min-device-width : 768px) and (max-device-width : 1024px) and (orientation : portrait) and (-webkit-min-device-pixel-ratio: 1.5) {
            #divbafinfialogo {
                margin-top: 10% !important;
            }

            #diverrorimg {
                margin-top: 10% !important;
            }

            #divkmiLogofooter {
                margin-top: 25% !important;
            }

            #bafindialogo {
            }

            #divmsg {
                margin-top: 9% !important;
            }

            #divclose {
                height: auto;
            }

            #KMILogo {
                width: 56% !important;
            }
        }
        /* End iPads (portrait) ----------- */

        /*my mobile*/
        @media screen and (max-device-width: 360px) and (orientation: portrait) {
            #bafindialogo {
                width: 70% !important;
            }

            #diverrorimg {
                margin-top: 5% !important;
            }

            #divbafinfialogo {
                margin-top: 9% !important;
            }

            #ErrorLogo {
                height: 230px !important;
                width: 230px !important;
            }

            span {
                font-size: 30px !important;
            }

            #divmsg {
                margin-top: 10% !important;
            }

            #divkmiLogofooter {
                margin-top: 15% !important;
            }

            #spn1 {
                font-size: 30px !important;
            }

            #spn2 {
                font-size: 30px !important;
            }

            #KMILogo {
                /*height: 200px !important;*/
                width: 60% !important;
            }
        }

        @media screen and (max-device-width: 640px) and (orientation: landscape) {
            #divclose {
                margin-top: 1% !important;
                height: auto;
            }

            #bafindialogo {
                height: 100px !important;
                width: 250px !important;
            }

            #ErrorLogo {
                height: 120px !important;
                width: 120px !important;
            }

            #KMILogo {
                width: auto !important;
            }

            #divmsg {
                margin-top: 1% !important;
            }

            #divkmiLogofooter {
                margin-top: 1% !important;
            }

            #KMILogo {
                width: auto !important;
            }

            #spn1 {
                font-size: 25px !important;
            }

            #spn2 {
                font-size: 25px !important;
            }
        }
        /*my mobile*/

        /* Galaxy j7 pro portrait */
        /*@media screen and (max-device-width: 1080px) and (orientation: portrait) {

            #bafindialogo {
                width: 70% !important;
            }

            #diverrorimg {
                margin-top: 7% !important;
            }

            #divbafinfialogo {
                margin-top: 2% !important;
            }

            #ErrorLogo {
                height: 35% !important;
                width: 35% !important;
            
            }

            span {
                font-size: 30px !important;
            }

            #divmsg {
                margin-top: 14% !important;
            }

            #divkmiLogofooter {
                margin-top: 20% !important;
            }

            #spn1 {
                font-size: 32px !important;
            }

            #spn2 {
                font-size: 32px !important;
            }

            #KMILogo {
                width: 63% !important;
            }
        }*/


        /*@media screen and (max-device-width: 1080px) and (orientation: landscape) {

            #divclose {
                margin-top: 1% !important;
                height: auto;
            }

            #bafindialogo {
                height: 80px !important;
                width: 230px !important;
            }

            #ErrorLogo {
                height: 120px !important;
                width: 120px !important;
            }

            #KMILogo {
                width: auto !important;
            }

            #divmsg {
                margin-top: 1% !important;
            }

            #divkmiLogofooter {
                margin-top: 1% !important;
            }

            #KMILogo {
                width: auto !important;
            }

            #spn1 {
                font-size: 25px !important;
            }

            #spn2 {
                font-size: 25px !important;
            }
        }*/
        /* Galaxy S5 landscape */


        /* iPhone 6 landscape */
        @media only screen and (min-device-width: 375px) and (max-device-width: 667px) and (orientation: landscape) and (-webkit-min-device-pixel-ratio: 2) {

            #bafindialogo {
                height: 90px !important;
                width: 270px !important;
            }

            #ErrorLogo {
                height: 120px !important;
            }

            #divbafinfialogo {
                margin-top: 0% !important;
            }

            #divmsg {
                margin-top: auto !important;
            }

            #divclose {
                height: auto;
            }

            #diverrorimg {
                margin-top: 1%;
            }

            #divkmiLogofooter {
                margin-top: 7%;
            }

            #KMILogo {
                width: 40% !important;
            }

            #spn1 {
                font-size: 22px !important;
            }

            #spn2 {
                font-size: 22px !important;
            }
        }

        /* iPhone 6 portrait */
        @media only screen and (min-device-width: 375px) and (max-device-width: 667px) and (orientation: portrait) and (-webkit-min-device-pixel-ratio: 2) {
            #bafindialogo {
                /*height: 300px !important;
                width: 350px !important;*/
                width: 70% !important;
            }

            #ErrorLogo {
                /*height: 175px !important;*/
                height: 270px !important;
            }

            #divbafinfialogo {
                margin-top: 5% !important;
                /*width: 60%;*/
            }

            #divmsg {
                margin-top: 15% !important;
            }

            #divclose {
                height: auto;
            }

            #diverrorimg {
                margin-top: 18%;
            }

            #divkmiLogofooter {
                margin-top: 30% !important;
            }

            #KMILogo {
                width: 80% !important;
            }

            #spn1 {
                font-size: 40px !important;
            }

            #spn2 {
                font-size: 40px !important;
            }
        }

        /* iPhone 6 Plus landscape */
        @media only screen and (min-device-width: 414px) and (max-device-width: 736px) and (orientation: landscape) and (-webkit-min-device-pixel-ratio: 3) {

            #bafindialogo {
                height: 90px !important;
                width: 270px !important;
            }

            #ErrorLogo {
                height: 120px !important;
            }

            #divbafinfialogo {
                margin-top: 0% !important;
            }

            #divmsg {
                margin-top: auto !important;
            }

            #divclose {
                height: auto;
            }

            #diverrorimg {
                margin-top: 1%;
            }

            #divkmiLogofooter {
                margin-top: 7%;
            }

            #KMILogo {
                width: 40% !important;
            }

            #spn1 {
                font-size: 22px !important;
            }

            #spn2 {
                font-size: 22px !important;
            }
        }

        /* iPhone 6 Plus portrait */
        @media only screen and (min-device-width: 414px) and (max-device-width: 736px) and (orientation: portrait) and (-webkit-min-device-pixel-ratio: 3) {
        }

        /* iPhone 6 and 6 Plus */
        /*@media only screen and (max-device-width: 812px), only screen and (max-device-width: 667px), only screen and (max-width: 480px) {*/
        @media only screen and (max-device-width: 812px) and (orientation: landscape) {
            #bafindialogo {
                height: 90px !important;
                width: 270px !important;
            }

            #ErrorLogo {
                height: 120px !important;
            }

            #divbafinfialogo {
                margin-top: 0% !important;
            }

            #divmsg {
                margin-top: auto !important;
                font-size: 21px !important;
            }

            #divclose {
                height: auto;
            }

            #diverrorimg {
                margin-top: 1%;
            }

            #divkmiLogofooter {
                margin-top: 2%;
            }

            #KMILogo {
                width: 40% !important;
            }

            #spn1 {
                font-size: 22px !important;
            }

            #spn2 {
                font-size: 22px !important;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container divmain" id="divmain">
            <%--  <div class="row" style="background-color: white; height: 95px; border-radius: 10px; margin-left: 10%; margin-right: 10%; margin-top: 5%;">
                <div id="divbafindialogo" runat="server" class="col-md-6" style="padding-top: 0.7%; padding-right: 24%;">
                    <center>
                            <asp:Image ID="Image3" runat="server" ImageUrl="~/Image/ILFS-logo.gif"  />
                          </center>
                </div>
                <div id="divKmilogo" runat="server" class="col-md-6" style="padding-top: 1.2%; padding-left: 19%;">
                    <center>
                         <asp:Image ID="Image4" runat="server" ImageUrl="~/Image/KMIlogo.png" Style="width: 90%;" /><br />
                   </center>
                </div>

            </div>--%>

            <div class="row">
                <div class="col-sm-12">
                    <div class="container login" style="background: none; height: auto; background-color: white; margin-top: 3%;">

                        <%--style="background-color: white;"	style="height: 50px !important; width: 70%;" --%>
                        <%--height: 10%; border-radius: 10px; margin-left: 10%; margin-right: 10%; margin-top: 5%;--%>
                        <div id="divbafindialogo" runat="server" class="col-sm-12" style="padding-top: 1%;">
                            <asp:Image ID="Image3" runat="server" ImageUrl="~/Image/Login_top.jpg" CssClass="img-responsive" />
                        </div>
                        <%--<div id="divKmilogo" runat="server" class="col-sm-6" style="padding-top: 1.2%; padding-left: 11%;">
                            <asp:Image ID="Image4" runat="server" ImageUrl="~/Image/KMIlogo.png" CssClass="img-responsive" Style="float: right; margin-bottom: 4%;" /><br />
                        </div>--%>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-sm-12">
                    <div class="container login animated fadeInDown" style="margin-top: 0% !important;">
                        <div class="row">
                            <div class="col-sm-5 left-panel">
                                <div class="header" >
                                    <label style="font-size:22px;">C-KYC MIDDLEWARE VER 2.0</label>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <asp:TextBox ID="txtUserName" CssClass="form-control" runat="server" placeholder="User Name"></asp:TextBox>
                                        <span class="glyphicon glyphicon-user form-control-feedback"></span>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="form-control" runat="server" placeholder="password"></asp:TextBox>
                                        <span class="glyphicon glyphicon-lock form-control-feedback"></span>
                                    </div>
                                </div>

                                <div class="col-sm-12 btn-section">
                                    <asp:Button ID="btnLogin" runat="server" CssClass="btn-animated bg-horrible" Text="Login" OnClick="btnLogin_Click" />
                                </div>
                                <div class="col-sm-12 btn-section">
                                    <a id="lnkForgotPassword" class="link-lg white" onclick="ForgotPassword()">Forgot password?</a>
                                    <a href="#" class="link-lg white" hidden="hidden">Register</a>
                                </div>
                            </div>
                            <div class="col-sm-7">
                            </div>

                            <div class="row">
                                <div id="aa" runat="server" class="col-md-12" style="margin-top: -7px">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Image/Login_bot.jpg" Width="100%" /><br />
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Display Modal popup window in division -->
        <div class="modal fade" id="myModal" role="dialog">
        </div>
        <!-- End Display Modal popup window in division -->


        <div class="container-fluid closeDiv" style="display: none;" id="divclose">
            <%--margin-top: 12%--%>
            <div class="container" id="divbafinfialogo">
                <div class="row">
                    <div class="col-sm-12">
                        <center>
                            <asp:Image ID="bafindialogo" runat="server" ImageUrl="~/Image/ILFS-logo.gif" CssClass="img-responsive" style="width: 56%;" />
                        </center>
                    </div>
                </div>
                <div class="row" id="diverrorimg">
                    <%--style="margin-top: 18%"--%>
                    <div class="col-sm-12">
                        <center>
                             <asp:Image ID="ErrorLogo" runat="server" ImageUrl="~/Image/Error5.png" Style="height:185px" CssClass="img-responsive" />
                            </center>
                    </div>
                </div>
                <div id="divmsg" class="row" style="margin-top: 9%">
                    <div class="col-sm-12" style="font-family: Verdana; font-size: 30px; color: orangered">

                        <span id="spn1" style="overflow-wrap: break-word;">Sorry! The current CKYC Middleware App isn't designed for smaller devices.
                        </span>
                        <span id="spn2" style="overflow-wrap: break-word;">In case you need a responsive app please send an email to: <a href="mailto:response@krishmark.com">response@krishmark.com</a>
                        </span>
                    </div>

                    <div id="divkmiLogofooter" class="col-sm-12">
                        <%--style="margin-top: 50%"--%>
                        <center>
                        <asp:Image ID="KMILogo" runat="server" ImageUrl="~/Image/KMIlogo.png" Style="width: 60%;" CssClass="img-responsive" /></center>
                    </div>
                </div>
            </div>
        </div>
    </form>

</body>
</html>
