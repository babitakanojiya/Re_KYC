<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.CKYC.Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/bundles/CKYCValidationjs") %>
    <%: Styles.Render("~/bundles/CKYCcss") %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">
    <style>
        .container {
            width: 1300px !important;
        }
       
        .HeaderText {
            white-space: nowrap;
            padding: 10px;
        }

        .modal-content {
            position: relative;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #999;
            border: 1px solid rgba(0,0,0,.2);
            border-radius: 6px;
            -webkit-box-shadow: 0 3px 9px rgb(0 0 0 / 50%);
            box-shadow: 0 3px 9px rgb(0 0 0 / 50%);
            outline: 0;
        }

        .modal-dialog {
            position: relative;
            width: auto;
            margin: 10px;
        }

        .tsname {
            text-align: center;
            padding-left: 44px;
        }

        .padding38 {
            padding-left: 38px;
        }

        .tsview {
            padding-left: 0px;
            color: Blue;
        }

        .tsheader {
            text-align: center;
            padding-right: 0px;
        }

        .tsheader1 {
            padding: 0;
        }

        .loader {
            /*position: absolute;
            padding-left: 650px;
            background-color: white;
            background-position: center;
            display: block;
            width: 105%;
            height: 105%;*/
            display: block;
            visibility: visible;
            position: absolute;
            z-index: 999;
            top: 0px;
            left: 0px;
            width: 105%;
            height: 105%;
            background-color: white;
            vertical-align: bottom;
            padding-top: 20%;
            filter: alpha(opacity=75);
            opacity: 0.75;
            font-size: 16px;
            color: blue;
            font-style: normal;
            font-weight: bold;
            background-image: url("../../assets/images/dashboard-icon/Loader.gif");
            background-repeat: no-repeat;
            background-attachment: fixed;
            background-position: center;
        }

        .wrapper_cust_Journey {
            background-color: #565656;
            margin: auto;
            height: 100px;
            width: 1200px;
        }


        .box_start {
            background-image: url(../../Image/01_Strat_icon.png);
            background-repeat: no-repeat;
            background-position: center;
            height: 82px;
            text-align: center;
            display: block;
            margin-top: 22px;
        }

        .box_Reg {
            /*background-image: url(../../Image/02_New_Registration.png);*/
            background-repeat: no-repeat;
            background-position: center;
            height: 98px;
            margin-top: 1px;
            text-align: center;
            border-right: solid 1px #ccc;
            display: block;
        }

        .box_upload {
            /*background-image: url(../../Image/03_Upload_Documents.png);*/
            background-repeat: no-repeat;
            background-position: center;
            height: 98px;
            margin-top: 1px;
            text-align: center;
            border-right: solid 1px #ccc;
            display: block;
        }

        .QC_icon {
            /*background-image: url(../../Image/04_QC_icon.png);*/
            background-repeat: no-repeat;
            background-position: center;
            height: 98px;
            margin-top: 1px;
            text-align: center;
            border-right: solid 1px #ccc;
            display: block;
        }

        .zip_generation_icon {
            /*background-image: url(../../Image/05_zip_generation_icon.png);*/
            background-repeat: no-repeat;
            background-position: center;
            height: 98px;
            margin-top: 1px;
            text-align: center;
            border-right: solid 1px #ccc;
            display: block;
        }

        .Push_CRS_icon {
            /*background-image: url(../../Image/06_Push_CRS_icon.png);*/
            background-repeat: no-repeat;
            background-position: center;
            height: 98px;
            margin-top: 1px;
            text-align: center;
            border-right: solid 1px #ccc;
            display: block;
        }

        .Response_From_CRS {
            /*background-image: url(../../Image/07_Response_From_CRS.png);*/
            background-repeat: no-repeat;
            background-position: center;
            height: 98px;
            margin-top: 1px;
            text-align: center;
            display: block;
        }

        .End {
            background-image: url(../../Image/08_End.png);
            background-repeat: no-repeat;
            background-position: center;
            height: 82px;
            margin-top: 22px;
            text-align: center;
            display: block;
        }
    </style>
    <script type="text/javascript">
        function LoadCtrl(flag) {
            ////debugger;
            document.getElementById("<%= divVal.ClientID %>").style.display = flag;
            document.getElementById("<%= btnClr.ClientID %>").style.display = flag;
            document.getElementById("<%= divValTyp.ClientID %>").style.display = flag;
        }
        function ShowHideSearchIcon(IdShow, IDHide) {
            document.getElementById(IdShow).style.display = "block";
            document.getElementById(IDHide).style.display = "none";
        }
        function ClrCtrl(flag) {
            //debugger;
        }
        function AlertMsg(msg) {
            //debugger;
            var varFooter = '<center><button type="button" class="btn btn-success" data-dismiss="modal" style="margin-top: -6px;"><span class="glyphicon glyphicon-ok" style="color: White;"></span> OK </button></center>';
            showModal('#myModal', 'Alert', 'alert-warning', varFooter, '', msg);
        }

        function ShowHideModal() {
            //debugger;
            document.getElementById('myModal23').style.display = "none";
        }

        function successBindata(data) {
            //debugger;
            AlertMsg(data.d);
        }

        function LoadQCPage(Flag, refno, Regno, CndStatus, batchid) {
            debugger;
            document.getElementById("hdnbatchid").value = batchid;
            document.getElementById("hdnId").value = refno;
            if (true) {
                document.getElementById("EmptyPagePlaceholder_hdnFIRefID").value = Regno;
                if (Flag == "ERR") {
                    ShowDivProgressBar('Error Loading...')
                    /*document.getElementById("EmptyPagePlaceholder_btnArchive").click();*/
                    document.getElementById("<%= getError.ClientID %>").click();
                }
                else if (Flag == "ZIP") {
                    document.getElementById("hdnQCAppro").value = "ZIP";
                }
                else if (Flag == "QC") {
                    ShowDivProgressBarWithNoTime('Please wait ZIP File Generating...')
                    document.getElementById("hdnQCAppro").value = "ZIP";
                    document.getElementById("EmptyPagePlaceholder_btnZipFileGeneration").click();
                }
                else if (Flag == "PUSH") {

                    document.getElementById("hdnQCAppro").value = "PUSH";
                }
                else if (Flag == "RESP") {

                    document.getElementById("hdnQCAppro").value = "RESP";
                }
                else if (Flag == "SRCH") {
                    ShowDivProgressBarWithNoTime('Please wait Processing Data...')
                    /*Added and commented by Akash on 12 march 24*/
                    /*THis line for FTP search data */
                    document.getElementById("EmptyPagePlaceholder_btnZipFileGeneration").click();
                    /*THis line for FTP search data */
                    /*THis line for API search data & commnted by babita on 09 dec 24 */
                    /*document.getElementById("EmptyPagePlaceholder_btnsrchAPI").click();*/
                    /*THis line for API search data & commnted by babita on 09 dec 24 */
                    /*Added and commented by Akash on 12 march 24*/
                    document.getElementById("EmptyPagePlaceholder_btnUpdAfterProcessOnWrkBench").click();
                    document.getElementById("hdnQCAppro").value = "";
                }
                else if (Flag == "DOWN") {
                    ShowDivProgressBarWithNoTime('Sending Download Request...')
                    /*Added and commented by Akash on 16 nov 2023*/
                    document.getElementById("EmptyPagePlaceholder_btnZipFileGeneration").click();
                    /*document.getElementById("EmptyPagePlaceholder_btnDWNAPI").click();*/
                    /*Added and commented by Akash on 16 nov 2023*/
                    document.getElementById("EmptyPagePlaceholder_btnUpdAfterProcessOnWrkBench").click();
                    document.getElementById("hdnQCAppro").value = "";
                }
                else if (Flag == "UPD") {
                    ShowDivProgressBar('Sending Update Request...');
                    setTimeout(function () {
                        HideProgressBar();
                        document.getElementById("EmptyPagePlaceholder_btnUpdAfterProcessOnWrkBench").click();
                        document.getElementById("hdnQCAppro").value = "";
                    }, 3000);
                }
                else if (Flag == "USN") {
                    debugger;
                    ShowDivProgressBar('Please wait... Processing...')
                    

                    //added by Akash on 16 nov 2023
                        document.getElementById("EmptyPagePlaceholder_btnunsoliDwndexcelgenerate").click();
                        //document.getElementById("EmptyPagePlaceholder_btnExcelUpload_Click").click();
                        //document.getElementById("EmptyPagePlaceholder_btnValidationJob_Click").click();
                        document.getElementById("EmptyPagePlaceholder_btnUpdAfterProcessOnWrkBench").click();
                    document.getElementById("hdnQCAppro").value = "";
                    document.getElementById(refno).style.display = "none";
                        //added by Akash on 16 nov 2023
                    //setTimeout(function () {
                    //    HideProgressBar();
                    //    //document.getElementById("lblNoRecord").style.display = "block";
                        
                    //}, 3000);
                }

                else {
                    ShowDivProgressBar('QC Approval Loading...')
                    var modal = document.getElementById('myModalRaise');
                    document.getElementById("myModalLabel").textContent = "QC Approval";
                    var modaliframe = document.getElementById("iframeCFR");
                    modaliframe.src = Flag;// set flag as TargetURL by Shubham 22Mar2021
                    $('#myModalRaise').modal();
                    document.getElementById("hdnQCAppro").value = "QC";
                    document.getElementById("hdnId").value = Regno;
                    document.getElementById("hndCndStatus").value = CndStatus;
                }

                setTimeout(function () { HideProgressBar(); }, 3000);

            }
            if (Flag == "Download") {

                ShowProgressBar('Sending Download Request...');
                var modal = document.getElementById('myModalRaise');
                document.getElementById("myModalLabel").textContent = "View Details";
                var modaliframe = document.getElementById("iframeCFR");
                modaliframe.src = "../../Application/CKYC/CKYCViewDetails.aspx?Status=view&refno=&kycno=" + refno;// + refno;
                $('#myModalRaise').modal();
            }
            if (Flag == "PROBABLEMATCH") {
                var modal = document.getElementById('myModalRaise');
                document.getElementById("myModalLabel").textContent = "PROBABLE MATCH";
                var modaliframe = document.getElementById("iframeCFR");
                modaliframe.src = "../../Application/CKYC/CkycPMSVerify.aspx?FlagPageTyp=Legal&refno=" + refno;// + refno;
                $('#myModalRaise').modal();
            }
        }

    </script>
    <%--Added By Shubham--%>
    <script type="text/javascript">
        function hexToBase64(str) {
            return btoa(String.fromCharCode.apply(null, str.replace(/\r|\n/g, "").replace(/([\da-fA-F]{2}) ?/g, "0x$1 ").replace(/ +$/, "").split(" ")));
        }
        function ChangeImg(divID, FID, ImgUrl) {
            ShowProgressBar('Please Wait Loading Data...')
            setTimeout(function () { HideProgressBar(); }, 3000);
            if (ImgUrl != "") {
                document.getElementById(divID).src = 'ImageCSharp.aspx?ImageID=' + ImgUrl;
            }
            document.getElementById(FID).style.display = "block";
        }
        function HideLink(FIDchck, count) {
            //debugger;
            if (count == "Y") {
                document.getElementById(FIDchck).style.display = "none";
            }
            else {
                document.getElementById(FIDchck).style.display = "block";
            }
        }
        function ModalbtnClose() {
            //debugger;
            document.getElementById('EmptyPagePlaceholder_myModal23').style.display = "none";
            Search();
        }
        function showFullText(ddlID) {
            $("#" + ddlID).addClass("form-control");
        }

        function hideFullText(ddlID) {
            $("#" + ddlID).addClass("form-control");
        }
    </script>

    <script type="text/javascript">

        function ShowProgressBar(Msg) {
            //debugger;
            var Msg = Msg
            document.getElementById('dvProgressBar').style.display = "block";
            document.getElementById('EmptyPagePlaceholder_lblMsg').innerHTML = Msg;
        }

        function ShowProgressBar_New(Msg) {
            //debugger;
            var Msg = Msg
            document.getElementById('dvProgressBar').style.display = "block";
            document.getElementById('EmptyPagePlaceholder_lblMsg').innerHTML = Msg;
            setTimeout(function () { HideProgressBar(); }, 5000);
        }

        function HideProgressBar() {
            //debugger;
            document.getElementById('dvProgressBar').style.display = "none";
        }

        function Search() {
            //debugger;
            document.getElementById("EmptyPagePlaceholder_btnSearch").click();
        }
        function RecordSearch() {
            //debugger;
            if (document.getElementById("EmptyPagePlaceholder_ddlSrchTyp").selectedIndex != 0) {
                document.getElementById("EmptyPagePlaceholder_btnSearch").click();
            }
            else { AlertMsg("Please Select Search Type"); }
        }

        function EnableSearchSegment() {
            //debugger;
            if (document.getElementById("divSearchSegment").style.display == "block")
                document.getElementById("divSearchSegment").style.display = "none";
            else
                if (document.getElementById("divSearchSegment").style.display == "none")
                    document.getElementById("divSearchSegment").style.display = "block";
        }

    </script>

    <%--Added By Shubham--%>

    <script type="text/javascript">

        function Start(divID) {
            //PageLoad(divID);
            ShowDivProgressBarWithTime('Loading..Please wait',35000);
            document.getElementById("EmptyPagePlaceholder_btnExcelUpload").click();
            document.getElementById("EmptyPagePlaceholder_btnValidationJob").click();
            MstShowHide(divID, "block");
            document.getElementById("divCls").style.display = "block";
            document.getElementById("divUnsolicited").style.display = "block";
            document.getElementById("divDownload").style.display = "block";
            document.getElementById("divSearch").style.display = "block";
            document.getElementById("divUpdDoc").style.display = "block";
            document.getElementById("divRegis").style.display = "block";
        }
        function PageLoad(divID) {

            document.getElementById("divStart").style.left = "5%";
            document.getElementById("divCls").style.display = "block";
            document.getElementById("divCls").style.left = "86%";
            document.getElementById("divUnsolicited").style.display = "block";
            document.getElementById("divUnsolicited").style.left = "71%";
            document.getElementById("divDownload").style.display = "block";
            document.getElementById("divUpdDoc").style.left = "56%";
            document.getElementById("divSearch").style.display = "block";
            document.getElementById("divDownload").style.left = "41%";
            document.getElementById("divUpdDoc").style.display = "block";
            document.getElementById("divSearch").style.left = "26%";
            document.getElementById("divRegis").style.display = "block";
            document.getElementById("divRegis").style.left = "11%";
        }
        function Close(divID) {
            $(".divCls").animate({ left: '600px' });
            //$(".divRespFrmCRS").animate({ left: '600px' });
            $(".divUnsolicited").animate({ left: '600px' });
            $(".divDownload").animate({ left: '600px' });
            $(".divSearch").animate({ left: '600px' });
            $(".divUpdDoc").animate({ left: '600px' });
            $(".divRegis").animate({ left: '600px' });
            $(".divStart").animate({ left: '600px' });
            document.getElementById("divCls").style.display = "none";
            document.getElementById("divUnsolicited").style.display = "none";
            document.getElementById("divDownload").style.display = "none";
            document.getElementById("divSearch").style.display = "none";
            document.getElementById("divUpdDoc").style.display = "none";
            document.getElementById("divRegis").style.display = "none";

        }
        function MstShowHide(Id, Action) {

            document.getElementById(Id).style.display = Action;
        }
        function MstSetActive(Id) {

            document.getElementById(Id).style.border = "4px solid #00b4bf;";
        }
        function SetActive(NxtActID) {
            debugger;
            LoadCtrl('none');
            if (document.getElementById("<%= txtNum.ClientID %>") != null) {
                document.getElementById("<%= txtNum.ClientID %>").value = "";
            }
            if (document.getElementById("<%= DDStatus.ClientID %>") != null) {
                document.getElementById("<%= DDStatus.ClientID %>").selectedIndex = 0;
            }
            document.getElementById("<%= ddlSrchTyp.ClientID %>").selectedIndex = 0;
            document.getElementById("hdnCurrActId").value = NxtActID;
            Search();
            if (NxtActID = "REG") {

            }
        }
        function ShowDivProgressBar(Msg) {
            //debugger;
            var Msg = Msg

            document.getElementById('divProgressBar').style.display = "block";
            document.getElementById('EmptyPagePlaceholder_Label2').innerHTML = Msg;
            setTimeout(function () { HideDivProgressBar(); }, 3000);
        }

        function ShowDivProgressBarWithNoTime(Msg) {
            //debugger;
            var Msg = Msg

            document.getElementById('divProgressBar').style.display = "block";
            document.getElementById('EmptyPagePlaceholder_Label2').innerHTML = Msg;
            // setTimeout(function () { HideDivProgressBar(); }, 3000);
        }

        function ShowDivProgressBarWithTime(Msg, time) {
            //debugger;
            var Msg = Msg
            document.getElementById('divProgressBar').style.display = "block";
            document.getElementById('EmptyPagePlaceholder_Label2').innerHTML = Msg;
            setTimeout(function () { HideDivProgressBar(); }, time);
        }

        function HideDivProgressBar() {
            //debugger;

            document.getElementById('divProgressBar').style.display = "none";

        }
    </script>
    <style type="text/css">
        .Divloader {
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

            .Divloader::after {
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
            }
    </style>
    <style type="text/css">
        .DivComm {
            width: 197px;
            height: 110px;
            position: absolute;
            cursor: pointer;
            /*background-repeat: no-repeat;
            background-position: center;
            ;*/
            /*padding-top: 85px;*/
            text-align: center;
            display: block;
            /*background-color: white;*/
        }

        .Active {
            border: 3px solid #03a1ab;
            /*border-style: ridge ridge hidden ridge;*/
        }

        .divStart {
            background-image: url(../../Image/01_Strat_icon.png);
        }

        .divCls {
            background-image: url(../../Image/08_End.png);
        }

        .divUnsolicited {
            background-image: url(../../assets/images/WorkbenchIcons/unsolicited_update_btn.png);
        }

        .divDownload {
            background-image: url(../../assets/images/WorkbenchIcons/update_btn.png);
        }

        .divSearch {
            background-image: url(../../assets/images/WorkbenchIcons/search_btn.png);
        }

        .divUpdDoc {
            background-image: url(../../assets/images/WorkbenchIcons/download_btn.png);
        }

        .divRegis {
            background-image: url(../../assets/images/WorkbenchIcons/registration_btnNew.png);
        }
    </style>

    <%--Added By Shubham--%>
    <asp:ScriptManager ID="CKYCSearch" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="container" style="margin-top: 0px; width: 100%;">
                <div class="col-sm-12" style="padding: 0 70px 20px 70px;">
                    <h3 style="padding-left: 5px; margin: 0px;">WELCOME TO CKYC MIDDLEWARE WORKBENCH</h3>
                    <hr style="margin-right: 40px;" />
                    <span style="margin: 0px;">An unified workbench to view, process and monitor all cases for Search, Download, Registration,Update and Unsolicited update notifications. Each case is loaded in the middleware anvil and is processed through the dynamic workflow stages till completion.   
                    </span>
                </div>
                <%--Added By Shubham--%>
                <asp:UpdatePanel ID="divTopMenu" runat="server">
                    <ContentTemplate>
                        <div class="row" style="margin-bottom: 111px;">
                            <div class="col-sm-12" style="width: 100%!important;">
                                <div id="divCls" class="col-sm-1 DivComm" style="left: 88%; width: 101px; display: block;">
                                    CLOSE
                        <img src="../../Image/08_End.png" />
                                </div>
                                <div id="divUnsolicited" class="col-sm-2 DivComm" style="left: 72%; display: block;" onclick="ShowDivProgressBar('Loading..Please wait');">
                                    <img id="ImgUnSolicited" runat="server" src="../../assets/images/WorkbenchIcons/unsolicited_update_btn.png" onclick="SetActive('ULS');" />
                                </div>
                                <div id="divUpdDoc" class="col-sm-2 DivComm" style="left: 56.5%; display: block;" onclick="ShowDivProgressBar('Loading..Please wait');">
                                    <img id="Imgupdate" runat="server" src="../../assets/images/WorkbenchIcons/update_btn.png" onclick="SetActive('UPD');" />
                                </div>
                                <div id="divDownload" class="col-sm-2 DivComm" style="left: 41%; display: block;" onclick="ShowDivProgressBar('Loading..Please wait');">
                                    <img id="Imgdownload" runat="server" src="../../assets/images/WorkbenchIcons/download_btn.png" onclick="SetActive('DOWN');" />
                                </div>
                                <div id="divSearch" class="col-sm-2 DivComm" style="left: 25.5%; display: block;" onclick="ShowDivProgressBar('Loading..Please wait');">
                                    <img id="Imgsearch" runat="server" src="../../assets/images/WorkbenchIcons/search_btn.png" onclick=" SetActive('SRCH');" />
                                </div>
                                <div id="divRegis" class="col-sm-2 DivComm" style="left: 10%; display: block;" onclick="Search();ShowDivProgressBar('Loading..Please wait');">
                                    <img id="Imgregistration" runat="server" src="../../assets/images/WorkbenchIcons/registration_btnNew.png" onclick="SetActive('REG');" />
                                </div>
                                <div id="divStart" class="col-sm-1 DivComm" style="left: 4%; width: 101px;" onclick="Start('EmptyPagePlaceholder_iFrameLoadPage');MstShowHide('divImg', 'none');">START<img src="../../Image/01_Strat_icon.png" /></div>
                                <asp:Button ID="btnValidationJob" runat="server" Style="display: none;" OnClick="btnValidationJob_Click" />
                                <asp:Button ID="btnExcelUpload" runat="server" Style="display: none;" OnClick="btnExcelUpload_Click" />
                                <asp:Button ID="btnZipFileGeneration" runat="server" Style="display: none;" OnClick="btnZipFileGeneration_Click" />
                                <asp:Button ID="btnUpdAfterProcessOnWrkBench" runat="server" Style="display: none;" OnClick="btnUpdAfterProcessOnWrkBench_Click" />
                                <asp:Button ID="btnTopMenu" runat="server" Visible="false" OnClick="btnTopMenu_Click" />
                                <asp:Button ID="btnunsoliDwndexcelgenerate" runat="server" Style="display: none;" OnClick="btnunsoliDwndexcelgenerate_Click" />
                                <asp:Button ID="btnDWNAPI" runat="server" Style="display: none;" OnClick="btnDWNAPI_Click" />
                                <asp:Button ID="btnsrchAPI" runat="server" Style="display: none;" OnClick="btnsrchAPI_Click" />
                                <%--<asp:Button ID="btnArchive" runat="server" Style="display: none;" />--%>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <%--Ended By Shubham--%>


                <div class="row" style="margin-left: 31px; margin-top: 1%; display: none;">
                    <asp:Image ID="Image1" runat="server" Style="width: 97%; height: fit-content;" ImageUrl="~/Images/Cust_journey_imag.jpg" />
                </div>
                <div class="panel  panel-success" id="divSearchSegment" style="margin-left: 2%; margin-right: 2%; margin-top: 0.5%; display: none;">

                    <div>
                        <div class="row">
                        </div>
                    </div>

                    <div id="trSearchDetails" class="panel-body">

                        <div id="divSrvcReqReport1" style="display: block;" class="panel-body panel-collapse collapse in">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-sm-3">
                                        <asp:Label ID="Label8" Text="CKYC No." runat="server" CssClass="control-label">
                                        </asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtCKYCNo" runat="server" CssClass="form-control"
                                            MaxLength="50" TabIndex="2" placeholder="CKYC No.">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Label ID="Label10" Text="FI Ref.No." runat="server" CssClass="control-label">
                                        </asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtFiRefNo" runat="server" CssClass="form-control"
                                            MaxLength="50" TabIndex="2" placeholder="FI Ref.No.">
                                        </asp:TextBox>
                                    </div>

                                    <%--</div>--%>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <%--<div class="col-sm-10" style="padding: 0">--%>
                                    <div class="col-sm-3">
                                        <asp:Label ID="Label11" Text="CKYC Ref.No." runat="server" CssClass="control-label">
                                        </asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtCKYCRefNo" runat="server" CssClass="form-control"
                                            MaxLength="50" TabIndex="2" placeholder="CKYC Ref.No.">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Label ID="Label1"
                                            Text="Status" runat="server" CssClass="control-label">
                                        </asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                    <%--</div>--%>
                                </div>
                            </div>
                            <div class="row">
                                <center>
                            <div class="col-sm-12" style='margin-top: 15px;'>
                                <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click"  CssClass="btn-animated bg-green" runat="server">   <%--OnClientClick="return validtab();"--%>
                                   
                                </asp:Button>
                                <asp:Button ID="btnClear" OnClick="btnClear_Click" CssClass="btn-animated bg-horrible" Text="Clear" runat="server">  
                                </asp:Button>

</div>
                                    </center>
                            </div>
                        </div>
                    </div>

                </div>

                <div id="trDgViewDtl" runat="server" class="page-container" style="display: none; margin-top: 0.5%">
                    <div class="panel panel-success" style='margin-right: 60px; margin-left: 60px; border: 1px solid #00b4bf;'>
                        <div runat="server" id="trtitle" class="panel-heading" onclick="showHideDiv('trgridsponsorship','span1');return false;" style="display: none;">
                            <div class="row" id="trDetails" runat="server">
                                <div class="col-sm-10" style="text-align: left">
                                    <span class="glyphicon glyphicon-menu-hamburger"></span>
                                    <asp:Label ID="lblprospectsearch" runat="server" Text="WORKBENCH PIPELINE"></asp:Label>
                                </div>
                                <div class="col-sm-2">
                                    <span id="span1" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                                </div>
                            </div>
                        </div>
                        <asp:GridView ID="GridView" runat="server" AutoGenerateColumns="False" GridLines="None" Width="100%" Visible="false" OnRowCommand="GridView_RowCommand">
                            <HeaderStyle HorizontalAlign="Center" />
                            <FooterStyle CssClass="GridViewFooter" />
                            <RowStyle CssClass="GridViewRow" />
                            <Columns>
                                <asp:TemplateField HeaderText="10000387" HeaderStyle-CssClass="tsheader  tsheader1">
                                    <ItemTemplate>
                                        <img src="../../assets/images/Legal_Entity_icon.jpg" style="width: 150px; height: 150px; padding-left: 0px;" />
                                        <asp:Image runat="server" ImageUrl="../../assets/images/Legal_Entity_icon.jpg" Style="padding-top: -1px; display: none" ToolTip="Zip" Width="10px" Height="20px" /><br />
                                        <asp:Label ID="lblHello1" runat="server" CssClass="tsname"
                                            Text='<%# Eval("Key") %>' /><br />
                                        <%--                                    <asp:Label ID="lblWorld" runat="server"
                                        Text='<%# Eval("Value") %>' /><br />--%>
                                        <asp:LinkButton ID="lblshortview1" runat="server" ForeColor="Blue" Text='QC Approval' CommandName="QC Approval1" OnClientClick="ShowProgressBar('QC Approval Loading...')" CssClass="tsview"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="10000388" HeaderStyle-CssClass="tsheader">
                                    <ItemTemplate>
                                        <img src="../../assets/images/Legal_Entity_icon.jpg" style="width: 150px; height: 150px; padding-left: 0px;" />
                                        <asp:Image runat="server" ImageUrl="../../assets/images/Legal_Entity_icon.jpg" Style="padding-top: -1px; display: none" ToolTip="Zip" Width="10px" Height="20px" /><br />
                                        <asp:Label ID="lblHello2" runat="server" CssClass="tsname"
                                            Text='<%# Eval("Key") %>' /><br />
                                        <%--                                    <asp:Label ID="lblWorld" runat="server"
                                        Text='<%# Eval("Value") %>' /><br />--%>
                                        <asp:LinkButton ID="lblshortview2" runat="server" ForeColor="Blue" Text='QC Approval' CommandName="QC Approval2" OnClientClick="ShowProgressBar('QC Approval Loading...')" CssClass="tsview"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="10000389" HeaderStyle-CssClass="tsheader">
                                    <ItemTemplate>
                                        <img src="../../assets/images/Legal_Entity_icon.jpg" style="width: 150px; height: 150px; padding-left: 0px;" />
                                        <asp:Image runat="server" ImageUrl="../../assets/images/Legal_Entity_icon.jpg" Style="padding-top: -1px; display: none" ToolTip="Zip" Width="10px" Height="20px" /><br />
                                        <asp:Label ID="lblHello3" runat="server" CssClass="tsname"
                                            Text='<%# Eval("Key") %>' /><br />
                                        <%--                                    <asp:Label ID="lblWorld" runat="server"
                                        Text='<%# Eval("Value") %>' /><br />--%>
                                        <asp:LinkButton ID="lblshortview3" runat="server" ForeColor="Blue" Text='QC Approval' CommandName="QC Approval3" OnClientClick="ShowProgressBar('QC Approval Loading...')" CssClass="tsview"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="20086435564680" HeaderStyle-CssClass="tsheader">
                                    <ItemTemplate>
                                        <img src="../../assets/images/Legal_Entity_icon.jpg" style="width: 150px; height: 150px; padding-left: 0px;" />
                                        <asp:Image runat="server" ImageUrl="../../assets/images/Legal_Entity_icon.jpg" Style="padding-top: -1px; display: none" ToolTip="Zip" Width="10px" Height="20px" /><br />
                                        <asp:Label ID="lblHello4" runat="server" CssClass="tsname"
                                            Text='<%# Eval("Key") %>' /><br />
                                        <%--                                    <asp:Label ID="lblWorld" runat="server"
                                        Text='<%# Eval("Value") %>' /><br />--%>
                                        <asp:LinkButton ID="lblshortview4" runat="server" ForeColor="Blue" Text='Update Details' CommandName="QC Approval4" OnClientClick="ShowProgressBar('Sending Update Request...')" CssClass="tsview"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="AJSPS2665N" HeaderStyle-CssClass="tsheader">
                                    <ItemTemplate>
                                        <img src="../../assets/images/Legal_Entity_icon.jpg" style="width: 150px; height: 150px; padding-left: 0px;" />
                                        <asp:Image runat="server" ImageUrl="../../assets/images/Legal_Entity_icon.jpg" Style="padding-top: -1px; display: none" ToolTip="Zip" Width="10px" Height="20px" /><br />
                                        <asp:Label ID="lblHello5" runat="server" CssClass="tsname"
                                            Text='<%# Eval("Key") %>' /><br />
                                        <%--                                    <asp:Label ID="lblWorld" runat="server"
                                        Text='<%# Eval("Value") %>' /><br />--%>
                                        <asp:LinkButton ID="lblshortview5" runat="server" ForeColor="Blue" Text='Search Details' CommandName="QC Approval5" OnClientClick="ShowProgressBar('Sending Search Request....')" CssClass="tsview"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="20086435564680" HeaderStyle-CssClass="tsheader">
                                    <ItemTemplate>
                                        <img src="../../assets/images/Legal_Entity_icon.jpg" style="width: 150px; height: 150px; padding-left: 0px;" />
                                        <asp:Image runat="server" ImageUrl="../../assets/images/Legal_Entity_icon.jpg" Style="padding-top: -1px; display: none" ToolTip="Zip" Width="10px" Height="20px" /><br />
                                        <asp:Label ID="lblHello6" runat="server" CssClass="tsname"
                                            Text='<%# Eval("Key") %>' /><br />
                                        <%--                                    <asp:Label ID="lblWorld" runat="server"
                                        Text='<%# Eval("Value") %>' /><br />--%>
                                        <asp:LinkButton ID="lblshortview6" runat="server" ForeColor="Blue" Text='Download' CommandName="Download" OnClientClick="ShowProgressBar('Sending Download Request....')" CssClass="tsview"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <br />
                        <%--Added By Shubham--%>
                        <div class="row" style="display: none;">
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    <div style="text-align: center;">
                                        <asp:Label ID="lblHdrtxt" Text="20086435564680" runat="server" />
                                    </div>
                                    <div style="text-align: center;">
                                        <img src="../../assets/images/Legal_Entity_icon.jpg" />
                                    </div>
                                    <div style="text-align: center;">
                                        <asp:LinkButton ID="btnStatus1" runat="server" ForeColor="Blue" Text='Download' OnClick="btnStatus_Click"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-sm-3">
                                    <div id="divValTyp" runat="server" style="display: none;">
                                        <asp:Label ID="lblSrchTyp" Visible="false" Text="Search" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <asp:DropDownList ID="ddlSrchTyp" runat="server" CssClass="form-control" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSrchTyp_SelectedIndexChanged" TabIndex="2">
                                            <asp:ListItem Text="Select" Value="" />
                                            <%--<asp:ListItem Text="CKYC No." Value="CKYCNo" />
                                        <asp:ListItem Text="FI Reference No." Value="FIREFNo" />
                                        <asp:ListItem Text="CKYC Ref No." Value="CKYCREFNo" />
                                        <asp:ListItem Text="Status" Value="STATUS" />--%>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div id="divVal" runat="server" style="display: none;">
                                        <asp:Label ID="lblNum" Visible="false" Text="Value" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <asp:TextBox ID="txtNum" runat="server" onChange="RecordSearch();" CssClass="form-control" MaxLength="50" TabIndex="2">
                                        </asp:TextBox>
                                        <asp:DropDownList ID="DDStatus" runat="server" Visible="false" CssClass="form-control" DataTextField="ParamDesc"
                                            DataValueField="ParamValue" AppendDataBoundItems="True" TabIndex="2" onBlur='hideFullText(this.id);'
                                            onMouseDown='showFullText(this.id);' onChange='hideFullText(this.id);RecordSearch();'>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                </div>
                                <div class="col-sm-3">
                                    <asp:LinkButton ID="btnClr" TabIndex="43" runat="server" OnClick="btnClr_Click" Style="display: none;">
                                    <span class="glyphicon glyphicon-remove" style="float: right; font-size: 20px; line-height: 2; color: #00b4bf;"> </span>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnSearchDtls" TabIndex="43" runat="server" OnClientClick="LoadCtrl('block');" OnClick="btnSrch_Click" Style="display: block;">
                                    <span class="glyphicon glyphicon-search" style="float: right; font-size: 20px; line-height: 2; color: #00b4bf;"> </span>
                                    </asp:LinkButton>
                                    <%-- <span ID="btnSearchDtls" class="glyphicon glyphicon-search" onclick="LoadCtrl('block');" style="float: right; font-size: 20px; line-height: 2; color: #00b4bf;"></span>
                                 <asp:Button ID="btnSrch" runat="server" Style="display: none;" OnClick="btnSrch_Click" />--%>
                                </div>
                            </div>
                        </div>
                        <asp:Panel ID="panel1" runat="server" Style="height: 200px; overflow: hidden; overflow-x: scroll;">
                            <div id="divSearchResult" runat="server">
                                <%--<span id="lblNoRecord" style='display: none; padding: 0px 530px 0px 540px; color: black; font - size: 15px;'>No Record Found</span>--%>
                            </div>
                        </asp:Panel>
                        <%--Added By Shubham--%>
                    </div>
                </div>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div id="divRegSub" class="row" style="display: none;" runat="server">
                            <div class="wrapper_cust_Journey" style="margin-top: 2px;">
                                <div style="width: 161px; display: none; height: 130px; float: left; background-color: #414141;">

                                    <a href="#" onclick="Search();" style="cursor: pointer;">
                                        <div class="box_start">
                                        </div>
                                    </a>
                                    <div style="text-align: center; color: #fff; font: bold 14px arial; height: 10px;">START</div>
                                </div>
                                <div style="width: 200px; float: left;">
                                    <a href="#" onclick="Search();" style="cursor: pointer;">
                                        <div class="box_Reg">
                                            <img id="ImgReg" style="margin-top: 15px;" runat="server" src="../../assets/images/Customer_Journey_New/02_New_Registration.png" />
                                            <div style="padding: 10px; text-align: center; color: #fff; font: bold 12px arial; height: 10px;">New Registration</div>
                                        </div>
                                    </a>
                                </div>
                                <div style="width: 200px; height: 130px; float: left;">
                                    <a href="#">
                                        <div class="box_upload">
                                            <img id="ImgUpload" style="margin-top: 15px;" runat="server" src="../../assets/images/Customer_Journey_New/03_Upload_Documents.png" />
                                            <div style="padding: 10px; text-align: center; color: #fff; font: bold 12px arial; height: 10px;">Upload Documents</div>
                                        </div>
                                    </a>
                                </div>
                                <div style="width: 200px; height: 130px; float: left;">
                                    <a href="#">
                                        <div class="QC_icon">
                                            <img id="ImgQc" style="margin-top: 15px;" runat="server" src="../../assets/images/Customer_Journey_New/04_QC_icon.png" />
                                            <div style="padding: 10px; text-align: center; color: #fff; font: bold 12px arial; height: 10px;">Quality Check</div>
                                        </div>
                                    </a>
                                </div>
                                <div style="width: 200px; height: 130px; float: left;">
                                    <a href="#">
                                        <div class="zip_generation_icon">
                                            <img id="ImgZip" style="margin-top: 15px;" runat="server" src="../../assets/images/Customer_Journey_New/05_zip_generation_icon.png" />
                                            <div style="padding: 10px; text-align: center; color: #fff; font: bold 12px arial; height: 10px;">Zip Generation</div>
                                        </div>
                                    </a>
                                </div>
                                <div style="width: 200px; height: 130px; float: left;">
                                    <a href="#">
                                        <div class="Push_CRS_icon">
                                            <img id="ImgPush" style="margin-top: 15px;" runat="server" src="../../assets/images/Customer_Journey_New/06_Push_CRS_icon.png" />
                                            <div style="padding: 10px; text-align: center; color: #fff; font: bold 12px arial; height: 10px;">Push To CRS</div>
                                        </div>
                                    </a>
                                </div>
                                <div style="width: 200px; height: 130px; float: left;">
                                    <a href="#">
                                        <div class="Response_From_CRS">
                                            <img id="ImgResp" style="margin-top: 15px;" runat="server" src="../../assets/images/Customer_Journey_New/07_Response_From_CRS.png" />
                                            <div style="padding: 10px; text-align: center; color: #fff; font: bold 12px arial; height: 10px;">Response From CRS</div>
                                        </div>
                                    </a>
                                </div>
                                <div style="width: 161px; display: none; height: 130px; float: left; background-color: #414141;">

                                    <a href="#" onclick="EnableSearchSegment()" style="cursor: pointer;">
                                        <div class="End">
                                        </div>
                                    </a>
                                    <div style="text-align: center; color: #fff; font: bold 14px arial; height: 10px;">END</div>
                                </div>

                            </div>
                        </div>
                        <div style="clear: both;"></div>

                    </ContentTemplate>

                </asp:UpdatePanel>
                <!-- Display Modal popup window in division -->
                <div class="modal fade" id="myModal" role="dialog" style="width: 600px; margin: 50px 0px 0px 350px;">
                </div>
                <div class="modal" id="myModalRaise" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="padding-top: 0px;">
                    <div class="modal-dialog" style="padding-top: 0px; margin: 50px;">
                        <div class="modal-content">
                            <div class="modal-header" style="margin-top: -10px; margin-bottom: -20px; padding-bottom: 6px ! important;">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="ModalbtnClose();">&times;</button>
                                <h4 class="modal-title" id="myModalLabel">CKYC QC</h4>
                            </div>
                            <div class="modal-body">

                                <iframe id="iframeCFR" src="" width="100%" height="570" frameborder="0" allowtransparency="true"></iframe>
                            </div>
                            <div class="modal-footer" style="display: none">
                            </div>
                        </div>
                        <!-- /.modal-content -->
                    </div>
                    <!-- /.modal-dialog -->
                </div>

                <div class="modal" id="myModalRaise1" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
                    aria-hidden="true" style="padding-top: 0px;">
                    <div class="modal-dialog" style="padding-top: 0px; padding: 20px 20px; margin-top: 2px; width: 100%;">
                        <div class="modal-content">
                            <div class="modal-header" style="margin-top: -10px; margin-bottom: -20px; padding-bottom: 6px ! important;">
                                <button type="button" class="close" onclick=" MstShowHide('myModalRaise1', 'none');" data-dismiss="modal" aria-hidden="true">
                                    &times;</button>
                                <h4 class="modal-title" id="myModalLabel" style="text-align: left;">CKYC Related Person Details</h4>
                            </div>
                            <div class="modal-body">
                                <iframe id="iframeCFR1" src="" width="100%" height="505" frameborder="0" allowtransparency="true"></iframe>
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
                <!-- End Display Modal popup window in division -->
                <div id="dvProgressBar" style="display: none" class="loader">
                    <center>
                      <br />
       <asp:Label ID="lblMsg" Text="" runat="server" > </asp:Label>
            </center>
                </div>

                <div id="divProgressBar" style="display: none; text-align: center" class="Divloader">
                    <center>
                         <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /> <br /><br /><br /><br /><br />
                          <asp:Image id="ldr" src="../../Images/horizonal_loader.gif"   height="50px" alt="" runat="server" ImageAlign="Middle"/>
                         <br />
                      <asp:Label ID="Label2" Text="" runat="server" ForeColor="Blue" style="font-size: medium; font-weight:bold" > </asp:Label>
                
            </center>
                </div>
                <div id="divImg" class="panel  panel-success" style="margin-left: 2%; margin-right: 2%; margin-top: 130px">
                    <div id="trtitle" class="panel-heading" style="background-color: #00C5CC; color: white;" onclick="showHideDiv('trgridsponsorship','span1');return false;">
                        <div class="row" id="Div1" runat="server">
                            <div class="col-sm-10" style="text-align: left">
                                <span class="glyphicon glyphicon-menu-hamburger"></span>
                                <asp:Label ID="Label3" runat="server" Text="ANVIL FOR DATA PROCESSING & DATA FLOW"></asp:Label>
                            </div>
                            <div class="col-sm-2">
                                <span id="span1" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div>
                    <div class="panel-body" style="text-align: center;">
                        <img src="../../assets/images/Anvil_BG.jpg" style="width: 1175px;" />
                    </div>
                </div>

                <iframe id="iFrameLoadPage" runat="server" src="ZipFileDetail.aspx" frameborder="0" width="100%" height="1100px" style="margin-top: 1%; display: none" allowtransparency="true"></iframe>
                <div class="modal fade in" id="myModal23" runat="server" style="display: none">
                    <div class="modal-dialog">

                        <!-- Modal content-->
                        <div class="modal-content" style="">
                            <div class="modal-header" style="height: 40px;">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="ModalbtnClose();">&times;</button>
                                <h4 class="modal-title">Error Result</h4>
                            </div>
                            <div class="modal-body" style="height: 450px; overflow-x: scroll; overflow-y: scroll;">
                                <asp:GridView ID="gvErrorSearch" CssClass="footable" ShowFooter="false"  EmptyDataText="No Record Found"
                                    AllowPaging="true" PageSize="15" AutoGenerateColumns="false" runat="server">
                                    <EditRowStyle BackColor="#7C6F57" />
                                    <HeaderStyle BackColor="#00c5cc" ForeColor="White" Width="100%" Height="35px" CssClass="gridViewHeader" />
                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#E3EAEB" />
                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="false" ForeColor="#333333" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="FIRefNo" HeaderStyle-CssClass="HeaderText">
                                            <ItemTemplate>
                                                <asp:Label ID="lblfirefno" runat="server" Text='<%#Eval("firefno") %> '></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Error Description" HeaderStyle-CssClass="HeaderText">
                                            <ItemTemplate>
                                                <asp:Label ID="lblerrordesc" runat="server" Text='<%#Eval("errordesc") %> '></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Error Code" HeaderStyle-CssClass="HeaderText">
                                            <ItemTemplate>
                                                <asp:Label ID="lblerrorcode" runat="server" Text='<%#Eval("errorcode") %> '></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Batch Id" HeaderStyle-CssClass="HeaderText">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbatchid" runat="server" Text='<%#Eval("batchid") %> '></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="mGrid" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <span id="lblErrNoRecord" runat="server" visible="false" style="padding: 0px 530px 0px 540px; color: black; font - size: 15px;">No Record Found</span>
                           </div>
                        </div>

                    </div>
                </div>
            </div>
            <asp:Button ID="getError" runat="server" Style="display: none;" OnClick="getError_Click" />
            <input type="hidden" id="hdnFIRefID" runat="server" />
            <input type="hidden" id="hdnFlagPageTyp" runat="server" />
            <input type="hidden" id="hdnFD" runat="server" />
            <asp:HiddenField ID="hndCndStatus" ClientIDMode="Static" runat="server" Value="" />
            <asp:HiddenField ID="hdnbatchid" ClientIDMode="Static" runat="server" Value="" />
            <asp:HiddenField ID="hdnId" ClientIDMode="Static" runat="server" Value="" />
            <asp:HiddenField ID="hdnQCAppro" ClientIDMode="Static" runat="server" Value="" />
            <asp:HiddenField ID="hdnCurrActId" ClientIDMode="Static" runat="server" Value="" />
            <%--<input id="hdnCurrActId"  type="hidden" runat="server" value="" />--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
