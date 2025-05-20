<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="CKYCViewDetails.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.CKYC.CKYCViewDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/bundles/CKYCValidationjs") %>
    <%: Styles.Render("~/bundles/CKYCcss") %>
    <script type="text/javascript">

        //////////Show image Modal popup
        function showimage(ImgId, ImgCode, Height, Width, ZinSize1, ZoutSize1, MstWidth1, MstHeight1, Flag) {
            debugger;
            if (ImgCode.toString().length == 1) {
                ImgCode = "0" + ImgCode;
            }
            $('#EmptyPagePlaceholder_hdnRotateValue').val("0");
            $("#EmptyPagePlaceholder_hdnHt").val(Height);
            $("#EmptyPagePlaceholder_hdnWt").val(Width);
            $('#EmptyPagePlaceholder_btnSaveImage').attr("disabled", true);
            counter = 0;
            flag = 1;
            MstWidth = MstWidth1;
            MstHeight = MstHeight1;
            ZinSize = ZinSize1;
            ZoutSize = ZoutSize1;
            Size = ((ZoutSize1 / 1024) * 20) / 100;
            ImgWidth = Width;
            ImgHeight = Height;
            var cndno = document.getElementById('<%=hdnCndNo.ClientID%>').value;
            var modal = document.getElementById('myModalImage');

            var ImgSrc = "ImageCSharp.aspx?ImageID=CKYC" + ImgId;

            // Get the image and insert it inside the modal - use its "alt" text as a caption
            var img = document.getElementById('myImg');
            var modalImg = document.getElementById("EmptyPagePlaceholder_img3");
            // $("#EmptyPagePlaceholder_img3").imageBox();
            $("#EmptyPagePlaceholder_hdnId").val(ImgId);


            doccode = ImgCode;
            modal.style.display = "block";
            modalImg.src = ImgSrc;
            modalImg.alt = this.alt;
            modalImg.width = Width;
            modalImg.height = Height;
            $("#EmptyPagePlaceholder_img3").removeAttr("style");
            var myBookId = $("#" + ImgCode).data('original-title');
            $("#EmptyPagePlaceholder_lblDocDesc").text(myBookId);
            $("#EmptyPagePlaceholder_hdnImgId").val(myBookId);
            if (myBookId == "Photo" || myBookId == "Signature") {
                $("#btnCrop").hide();
            }
            else {
                $("#btnCrop").hide();
            }
            if (Flag == 2) {
                $('#EmptyPagePlaceholder_btnApp').attr("disabled", true);
            }
            else {
                $('#EmptyPagePlaceholder_btnApp').attr("disabled", false);
            }
            arg03 = myBookId;


            //  document.getElementById("lblDocType").value=Doctype; 
            //var captionText = $("#"+ImgId).attr("data-title");
            //document.getElementById("lblDocDesc").value() = captionText;
            // Get the <span> element that closes the modal
            var span = document.getElementsByClassName("close")[0];
            var span1 = document.getElementsByClassName("btn btn-default")[0];


            // When the user clicks on <span> (x), close the modal
            span.onclick = function () {
                debugger;
                modal.style.display = "none";
                var clickButton = document.getElementById("EmptyPagePlaceholder_PageReLoad");
                clickButton.click();
                return true;
            }
            span1.onclick = function () {
                debugger;
                modal.style.display = "none";
                var clickButton = document.getElementById("EmptyPagePlaceholder_PageReLoad");
                clickButton.click();
                return true;
            }

        }
        function Cancel(modalimg) {
            debugger;
            var modal = modalimg;
            var span = document.getElementsByClassName("close")[0];

            modal.style.display = "none";


        }



        function OpenRelatedPersonPageView(RelRefnNo, refno) {
            debugger;
            var modal = document.getElementById('myModalRaise');
            var modaliframe = document.getElementById("iframeCFR");

            modaliframe.src = "CKYCRelatedPrsn.aspx?Status=View&refno=" + refno + "&relrefno=" + RelRefnNo;
            $('#myModalRaise').modal('show');
        }

        function AlertMsg(msg) {
            showModal('#myModal', 'Alert', 'alert-warning', '', '', msg);
        }
    </script>

    <style type="text/css">
        .container {
            width: 1250px !important;
        }

        #myImg {
            border-radius: 5px;
            cursor: pointer;
            transition: 0.3s;
        }

            #myImg:hover {
                opacity: 0.7;
            }

        /* The Modal (background) */
        .modal {
            display: none; /* Hidden by default */
            position: fixed; /* Stay in place */
            z-index: 1; /* Sit on top */
            padding-top: 100px; /* Location of the box */
            left: 0;
            top: 0;
            width: 100%; /* Full width */
            height: 100%; /* Full height */
            overflow: auto; /* Enable scroll if needed */
            background-color: rgb(0,0,0); /* Fallback color */
            background-color: rgba(0,0,0,0.9); /* Black w/ opacity */
        }

        /* Modal Content (image) */
        .modal-content {
            margin: auto;
            display: block;
            width: 80%;
            max-width: 700px;
        }

        /* Caption of Modal Image */
        #caption {
            margin: auto;
            display: block;
            width: 80%;
            max-width: 700px;
            text-align: center;
            color: #ccc;
            padding: 10px 0;
            height: 150px;
        }

        /* Add Animation */
        .modal-content, #caption {
            -webkit-animation-name: zoom;
            -webkit-animation-duration: 0.6s;
            animation-name: zoom;
            animation-duration: 0.6s;
        }

        @-webkit-keyframes zoom {
            from {
                -webkit-transform: scale(0);
            }

            to {
                -webkit-transform: scale(1);
            }
        }

        @keyframes zoom {
            from {
                transform: scale(0);
            }

            to {
                transform: scale(1);
            }
        }

        /* The Close Button */
        .close {
            position: absolute;
            top: 15px;
            right: 35px;
            color: #f1f1f1;
            font-size: 40px;
            font-weight: bold;
            transition: 0.3s;
        }

            .close:hover,
            .close:focus {
                color: #bbb;
                text-decoration: none;
                cursor: pointer;
            }

        /* 100% Image Width on Smaller Screens */
        @media only screen and (max-width: 700px) {
            .modal-content {
                width: 100%;
            }
        }
    </style>
    <style type="text/css">
        #img-preview {
            height: 275px;
            width: auto;
            overflow: auto;
            text-align: center;
        }

        .img-op {
            margin-top: 5px;
            text-align: center;
        }

        .modal .modal-content .btn {
            border-radius: 0;
        }

        .img-op .bt {
            margin: 5px;
            width: 100px;
        }

        .modal-footer .btn-default {
            background-color: #fff;
            background-image: none;
            border: 1px solid #ccc;
        }

        .modal-backdrop {
            position: inherit;
            top: 0;
            right: 0;
            bottom: 0;
            left: 0;
            z-index: 1040;
            background-color: #000;
        }

            .modal-backdrop.fade {
                filter: alpha(opacity=0);
                opacity: 0;
            }

            .modal-backdrop.in {
                filter: alpha(opacity=50);
                opacity: .5;
            }
    </style>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">
    <span style="color: rgb(51, 51, 51); font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; font-size: 14px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: 20px; orphans: auto; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);"></span>
    <asp:ScriptManager ID="SM1" runat="server">
        <Scripts>
            <asp:ScriptReference Path="../../../Application/Common/Lookup.js" />
        </Scripts>
        <Services>
            <asp:ServiceReference Path="../../../Application/Common/Lookup.asmx" />
        </Services>
    </asp:ScriptManager>
    <div class="container" style="margin-top: 0px; width: 100%;">
        <%-- Added for CKYC Details header start--%>
        <div class="panel panel-success">
            <div id="Div18" runat="server" class="panel-heading" onclick="showHideDiv('divCKYCdtls','btnCKYCdtls');return false;">
                <div class="row">
                    <div class="col-sm-10" style="text-align: left">
                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                        <asp:Label ID="lblOfcuseOnly" Text="" runat="server" CssClass="control-label">
                        </asp:Label>
                    </div>
                    <div class="col-sm-2" style="text-align: right">
                        <asp:Label ID="Label3" Text="Version 1.6" runat="server" CssClass="control-label"></asp:Label>
                        <span id="btnCKYCdtls" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                    </div>
                </div>
            </div>
            <div id="divCKYCdtls" class="panel-body">
                <div class="row">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblAppType" runat="server" Font-Bold="false">
                        </asp:Label>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:CheckBox ID="cbNew" runat="server" CssClass="standardcheckbox" Text="New" AutoPostBack="true"
                            Enabled="false" TabIndex="20" />
                        <asp:CheckBox ID="cbUpdate" runat="server" CssClass="standardcheckbox" Text="Update"
                            AutoPostBack="true" Visible="false" TabIndex="1" />
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblRefNumber" runat="server" Font-Bold="false"></asp:Label>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:TextBox ID="txtRefNumber" runat="server" CssClass="form-control" Enabled="false"
                            Font-Bold="false" TabIndex="2">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblAccountType" runat="server" Font-Bold="false">
                        </asp:Label>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <%--                        <asp:CheckBox ID="chkNormal" runat="server" CssClass="standardcheckbox" Text="Normal"
                            AutoPostBack="true" TabIndex="3" name="cb1" value="value1" />
                        <asp:CheckBox ID="chkSimplified" runat="server" CssClass="standardcheckbox" Text="Simplified"
                            AutoPostBack="true" TabIndex="3" name="cb2" value="value1" />
                        <asp:CheckBox ID="Chksmall" runat="server" CssClass="standardcheckbox" Text="Small"
                            AutoPostBack="true" TabIndex="5
                            " name="cb3" value="value1" />--%> <%-- commented by rutuja --%>
                        <%--Added by rutuja for--%>
                        <asp:DropDownList ID="ddlAccountType" runat="server" CssClass="form-control" TabIndex="2" ClientIDMode="Static">
                        </asp:DropDownList>
                        <%--ended by rutuja for--%>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblKYCNumber" runat="server" Font-Bold="false"></asp:Label>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:TextBox ID="txtKYCNumber" runat="server" CssClass="form-control" Enabled="false"
                            Font-Bold="false" TabIndex="2">
                        </asp:TextBox>
                    </div>
                </div>
            </div>
        </div>

        <%-- Added for CKYC Details header end--%>


        <asp:UpdatePanel ID="Updatepanel3" runat="server">
            <ContentTemplate>
                <div class="panel panel-success">
                    <%--Div19 commented by Rutuja --%>
                    <%--   <div id="Div19" class="panel-heading" onclick="showHideDiv('menu1','Span8');return false;">  
                             <div class="row">
                            <div class="col-sm-10" style="text-align: left">
                                <span class="glyphicon glyphicon-menu-hamburger"></span>
                                
                                <asp:Label ID="lblpfPersonal1"  runat="server" CssClass="control-label">
                                </asp:Label>
                            </div>
                            <div class="col-sm-2" >                       
                                  <span id="Span8" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div>--%>
                    <%--<div id="menu1" style="display: block;" class="panel-body">--%>
                        <%--  Added for Personal Details start --%>
                        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                            <div id="Div2" runat="server" class="panel-heading subheader"
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
                                                    <asp:TextBox ID="cboTitle" runat="server" CssClass="form-control" DataTextField="ParamDesc"
                                                        DataValueField="ParamValue" AppendDataBoundItems="True" TabIndex="6">
                                                    </asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>


                                        <div class="col-sm-10" style="padding: 0">
                                            <div class="col-sm-4" style="padding-left: 0">
                                                <asp:TextBox ID="txtGivenName" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"
                                                    MaxLength="50" TabIndex="7">
                                                </asp:TextBox>

                                            </div>
                                            <div class="col-sm-4" style="padding-left: 0">
                                                <asp:TextBox ID="txtMiddleName" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"
                                                    MaxLength="50" TabIndex="8">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-sm-4" style="padding-left: 0">
                                                <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"
                                                    MaxLength="50" TabIndex="9">
                                                </asp:TextBox>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblMaidenName" Text="Maiden Name" CssClass="control-label" runat="server">
                                        </asp:Label>
                                    </div>
                                    <div class="col-sm-9" style="padding: 0">
                                        <div class="col-sm-2">
                                            <asp:UpdatePanel ID="ipcboTitle1" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="cboTitle1" runat="server" CssClass="form-control" DataTextField="ParamDesc"
                                                        DataValueField="ParamValue" AppendDataBoundItems="True" TabIndex="10">
                                                    </asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-sm-10" style="padding: 0">
                                            <div class="col-sm-4" style="padding-left: 0">
                                                <asp:TextBox ID="txtGivenName1" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"
                                                    MaxLength="50" TabIndex="11">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-sm-4" style="padding-left: 0">
                                                <asp:TextBox ID="txtMiddleName1" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"
                                                    MaxLength="50" TabIndex="12">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-sm-4" style="padding-left: 0">
                                                <asp:TextBox ID="txtLastName1" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"
                                                    MaxLength="50" TabIndex="13">
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
                                            <asp:UpdatePanel ID="UpdFSFlag" runat="server">
                                                <ContentTemplate>
                                                    <asp:RadioButtonList ID="rbtFS" runat="server" CssClass="radiobtn" RepeatDirection="Horizontal"
                                                        Visible="true" TabIndex="14" AutoPostBack="true">
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
                                                    <asp:TextBox ID="cboTitle2" runat="server" CssClass="form-control" DataTextField="ParamDesc"
                                                        DataValueField="ParamValue" AppendDataBoundItems="True" TabIndex="15">
                                                    </asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-sm-10" style="padding: 0">
                                            <div class="col-sm-4" style="padding-left: 0">
                                                <asp:TextBox ID="txtGivenName2" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"
                                                    MaxLength="30" TabIndex="16">
                                                </asp:TextBox>
                                            </div>

                                            <div class="col-sm-4" style="padding-left: 0">
                                                <asp:TextBox ID="txtMiddleName2" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"
                                                    MaxLength="30" TabIndex="17">
                                                </asp:TextBox>

                                            </div>

                                            <div class="col-sm-4" style="padding-left: 0">
                                                <asp:TextBox ID="txtLastName2" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"
                                                    MaxLength="30" TabIndex="18">
                                                </asp:TextBox>

                                                <asp:HiddenField ID="HiddenField1" runat="server"></asp:HiddenField>
                                                <asp:HiddenField ID="HiddenField2" runat="server"></asp:HiddenField>
                                            </div>
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
                                            <asp:TextBox ID="cboTitle3" runat="server" CssClass="form-control" DataTextField="ParamDesc"
                                                DataValueField="ParamValue" AppendDataBoundItems="True" TabIndex="19">
                                            </asp:TextBox>
                                        </div>
                                        <div class="col-sm-10" style="padding: 0">
                                            <div class="col-sm-4" style="padding-left: 0">
                                                <asp:TextBox ID="txtGivenName3" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"
                                                    MaxLength="50" TabIndex="20">
                                                </asp:TextBox>
                                            </div>

                                            <div class="col-sm-4" style="padding-left: 0">
                                                <asp:TextBox ID="txtMiddleName3" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"
                                                    MaxLength="50" TabIndex="21">
                                                </asp:TextBox>
                                            </div>

                                            <div class="col-sm-4" style="padding-left: 0">
                                                <asp:TextBox ID="txtLastName3" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"
                                                    MaxLength="50" TabIndex="22">
                                                </asp:TextBox>

                                                <asp:HiddenField ID="HiddenField3" runat="server"></asp:HiddenField>
                                                <asp:HiddenField ID="HiddenField4" runat="server"></asp:HiddenField>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lbldob" Text="DOB (dd/mm/yyyy) " runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <span><font color="red">*</font></span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control"
                                            onmousedown="$('#txtDOB').datepicker({ changeMonth: true, changeYear: true });"
                                            onchange="setDateFormat('txtDob')" runat="server" ID="txtDOB" MaxLength="15"
                                            TabIndex="23" />
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
                                                <asp:TextBox ID="cboGender" runat="server" CssClass="form-control" TabIndex="24">
                                                </asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="row">
                                    <asp:UpdatePanel ID="upOccuSubType" style="display: none" runat="server">
                                        <ContentTemplate>
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblOccupation" Text="Occupation Type" runat="server" CssClass="control-label">
                                                </asp:Label>
                                                <span><font color="red">*</font></span>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="ddlOccupation" AutoPostBack="true" runat="server" CssClass="form-control"
                                                    TabIndex="25">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-sm-3" style="text-align: left" id="divOccuSubType" runat="server">
                                                <asp:Label ID="lblOccuSubType" Text="Occupation Sub Type" runat="server" CssClass="control-label">
                                                </asp:Label>
                                                <span><font color="red">*</font></span>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="ddlOccuSubType" runat="server" CssClass="form-control" TabIndex="26">
                                                </asp:TextBox>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="row" style="display:none;">
                                    <div class="col-sm-3" style="text-align: left">
                                        <%--Added by shreela on 6/03/14 to remove space--%>
                                        <asp:Label ID="lblMarStatus" Text="Marital Status" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <span style="color: red">*</span>
                                        <%-- <span style="color: #ff0000">*</span>--%>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:UpdatePanel ID="upMaritalStatus" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="ddlMaritalStatus" runat="server" CssClass="form-control" TabIndex="27">
                                                </asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblCitizenship" Text="Citizenship" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <span><font color="red">*</font></span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:UpdatePanel ID="upCitizenship" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="ddlCitizenship" runat="server" CssClass="form-control" TabIndex="28"
                                                    AutoPostBack="true">
                                                </asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left;display:none;" >
                                        <asp:Label ID="lblResStatus" Text="Residential Status" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3" style="display:none;">
                                        <asp:UpdatePanel ID="upResStatus" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="ddlResStatus" runat="server" CssClass="form-control" TabIndex="29">
                                                </asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <%-- Below added by rutuja--%>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="Label1" Text="PAN" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:TextBox runat="server" ID="txtPanNo" CssClass="form-control"
                                            ClientIDMode="Static" AutoPostBack="true" TabIndex="29" />
                                    </div>
                                    <%-- ended added by rutuja--%>
                                    <div class="col-sm-3">
                                    <asp:CheckBox ID="chkPanForm" Text="Form 60 furnished" OnCheckedChanged="chkPanForm_CheckedChanged" Enabled="true"
                                        AutoPostBack="true" runat="server" CssClass="standardcheckbox" TabIndex="2" />
                                    <%--<span style="color: red">*</span>--%>
                                </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:UpdatePanel ID="uplblIsoCountryCodeOthr" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="lblIsoCountryCodeOthr" Text="ISO 3166 Country Code" Visible="false"
                                                    runat="server" CssClass="control-label"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:UpdatePanel ID="upIsoCountryCodeOthr" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="ddlIsoCountryCodeOthr" runat="server" CssClass="form-control"
                                                    AutoPostBack="true" TabIndex="30" Visible="false">
                                                </asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <%--  Added for Personal Details end --%>
                        <%--  Added for Tick If Applicable start --%>
                        <div class="panel panel-success" style="display: none">
                            <%-- Display none added by rutuja --%>
                            <div id="Div1" runat="server" class="panel-heading subheader"
                                onclick="ShowReqDtl1('EmptyPagePlaceholder_div3','btnpersnl');return false;">
                                <div class="row">
                                    <div class="col-sm-10" style="text-align: left">
                                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                                        <asp:Label ID="lbltick" Text="TICK IF APPLICABLE" runat="server" CssClass="control-label">
                                        </asp:Label>&nbsp;&nbsp;
                                        <%-- <asp:CheckBox ID="chkTick" Text=" RESIDENCE FOR TAX PURPOSES IN JURISDICTIONS(S) OUTSIDE INDIA" OnCheckedChanged="chkTick_Checked"
                                            CssClass="standardcheckbox"  runat="server" />--%>
                                    </div>
                                    <div class="col-sm-2">
                                        <span id="Span1" class="glyphicon glyphicon-resize-full" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                                    </div>
                                </div>
                            </div>
                            <div id="div3" style="display: block;" runat="server" class="panel-body">
                                ADIITIONAL DETAILS REQUIRED*(Mandatory only if section 2 is ticked)
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-sm-12" style="text-align: left">
                                        <asp:CheckBox ID="chkTick" Text=" RESIDENCE FOR TAX PURPOSES IN JURISDICTIONS(S) OUTSIDE INDIA"
                                            CssClass="standardcheckbox" runat="server"
                                            TabIndex="31" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblIsoCountryCode2" Text="ISO 3166 Country Code of Jurisdiction of Residence"
                                            runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font></span>
                                    </div>
                                    <div class="col-sm-3">
                                        <%-- <asp:textbox cssclass="form-control" onmousedown="$('#txtDob').datepicker({ changeMonth: true, changeYear: true });"
                                    onchange="setDateFormat('txtDob')" runat="server" id="txtIsoCountryCode2" maxlength="15"
                                    tabindex="12" enabled="true" />--%>
                                        <asp:TextBox ID="txtIsoCountryCode2" runat="server" CssClass="form-control"
                                            AutoPostBack="true" TabIndex="32">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblTaxIden" Text="Tax Identification Number or equivalent(if issued by jurisdiction)"
                                            runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font></span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" onmousedown="$('#txtDob').datepicker({ changeMonth: true, changeYear: true });"
                                            onchange="setDateFormat('txtDob')" runat="server" ID="txtIDResTax" MaxLength="15"
                                            TabIndex="33" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblPlace" Text="Place/City of Birth" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <span><font color="red">*</font></span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" onmousedown="$('#txtDob').datepicker({ changeMonth: true, changeYear: true });"
                                            onchange="setDateFormat('txtDob')" runat="server" ID="txtDOBRes" MaxLength="15"
                                            TabIndex="34" />
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblIsoContry" Text="ISO 3166 Country Code of Birth" runat="server"
                                            CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font></span>
                                    </div>
                                    <div class="col-sm-3">
                                        <%--<asp:textbox cssclass="form-control" onmousedown="$('#txtDob').datepicker({ changeMonth: true, changeYear: true });"
                                    onchange="setDateFormat('txtDob')" runat="server" id="txtIsoCountry" maxlength="15"
                                    tabindex="12" />--%>
                                        <asp:TextBox ID="txtIsoCountry" runat="server" CssClass="form-control" AutoPostBack="true"
                                            TabIndex="35">
                                        </asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--  Added for Tick If Applicable end --%>
                   <%-- </div>--%>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="panel panel-success">
                    <div id="Div4" class="panel-heading" onclick="showHideDiv('menu2','btnProofIdentity');return false;">
                        <div class="row">
                            <div class="col-sm-10" style="text-align: left">
                                <span class="glyphicon glyphicon-menu-hamburger"></span>

                                <asp:Label ID="lblProofOfIdentity11" Text="PROOF OF IDENTITY(POI)*" runat="server"
                                    CssClass="control-label"></asp:Label>
                                <%-- text changed by rutuja---%>
                            </div>
                            <div class="col-sm-2">
                                <span id="btnProofIdentity" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div>
                    <div id="menu2" class="panel-body">
                        <%--  Added for Proof of Identity start--%>

                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblProof" Text="(Certified copy of any one the folloing Proof of Identity [Pol] needs to be submitted)"
                                    runat="server" CssClass="control-label"></asp:Label>
                                <span><font color="red">*</font></span>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="ddlProofIdentity" runat="server" CssClass="form-control" AutoPostBack="true"
                                    TabIndex="36">
                                </asp:TextBox>

                            </div>
                        </div>
                        <div id="divIdProof" runat="server" class="row">
                            <div id="divPassNo" runat="server" class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblPassportNo" runat="server" CssClass="control-label"></asp:Label>
                                <span><font color="red">*</font></span>
                            </div>
                            <div id="divPassNotxt" runat="server" class="col-sm-3">
                                <asp:TextBox CssClass="form-control" runat="server" onChange="javascript:this.value=this.value.toUpperCase();"
                                    ID="txtPassNo" MaxLength="15" TabIndex="37" />

                            </div>
                            <div id="divPass" runat="server" class="col-sm-3" style="text-align: left">
                                <asp:Label ID="llPassExpDate" runat="server" CssClass="control-label"></asp:Label>
                                <span><font color="red">*</font></span>
                            </div>
                            <div id="divPassDate" runat="server" class="col-sm-3">
                                <asp:TextBox CssClass="form-control" onmousedown="$('#ctl00_ContentPlaceHolder1_txtPassExpDate').datepicker({ changeMonth: true, changeYear: true });"
                                    onchange="setDateFormat('ctl00_ContentPlaceHolder1_txtPassExpDate')" runat="server"
                                    ID="txtPassExpDate" MaxLength="15" TabIndex="38" />
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtPassOthr" MaxLength="15"
                                    TabIndex="39" />
                            </div>
                        </div>
                        <div class="row" style="text-align: center; display: none" visible="false">
                            <asp:LinkButton ID="btnAddTrnsfr" runat="server" CssClass="btn btn-primary"
                                TabIndex="12">
                               <span class="glyphicon glyphicon-plus BtnGlyphicon"> </span> Add
                            </asp:LinkButton>
                        </div>
                        <div class="row">
                            <br />
                            <div class="col-sm-12">
                                <asp:GridView ID="gvIdProof" CssClass="footable"
                                    AutoGenerateColumns="false" AutoGenerateDeleteButton="false" runat="server">
                                    <RowStyle CssClass="GridViewRow"></RowStyle>
                                    <PagerStyle CssClass="disablepage" />
                                    <HeaderStyle CssClass="gridview th" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="ID Proof Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdProofType" runat="server" Text='<%# Eval("ID Proof Type") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ID Proof Number">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdProofNum" runat="server" Text='<%# Eval("ID Proof Number") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ID Proof Submited">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdProofSub" runat="server" Text='<%# Eval("ID Proof Submited") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:CommandField ShowDeleteButton="true"  DeleteText="Delete" />--%>
                                        <%--   <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandArgument='<% #Eval("Agency_code_Number")%>'
                                                                CommandName="delete" /></ItemTemplate>
                                                    </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <%-- Added for Proof of Identity end--%>
        <div class="panel panel-success">
            <div id="Div20" runat="server" class="panel-heading" onclick="showHideDiv('menu3','btnpersnl');return false;">
                <div class="row">
                    <div class="col-sm-10" style="text-align: left">
                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                        <asp:Label ID="lblpfofAddr1" Text="PROOF OF ADDRESS(POI)" runat="server" CssClass="control-label">
                        </asp:Label>
                    </div>
                    <div class="col-sm-2">
                        <span id="Span9" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                    </div>
                </div>
            </div>
            <div id="menu3" class="panel-body">
                <asp:UpdatePanel ID="upMenu3" runat="server">
                    <ContentTemplate>
                        <%--  Added for Proof of Address start--%>
                        <div class="panel panel-success">
                            <%-- Div6 commented by rutuja--%>
                            <%--<div id="Div6" runat="server" class="panel-heading subheader">
                                <div class="row">
                                    <div class="col-sm-10" style="text-align: left">
                                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                                    
                                        <asp:Label ID="lblpfofAddr2" Text="PROOF OF ADDRESS(PoA)*" runat="server" CssClass="control-label"></asp:Label>
                                    </div>
                                    <div class="col-sm-2" onclick="ShowReqDtl1('EmptyPagePlaceholder_div7','Span2');return false;">
                                        <span id="Span2" class="glyphicon glyphicon-resize-full" style="float: right;
                                            padding: 1px 10px ! important; font-size: 18px;"></span>
                                    </div>
                                </div>
                            </div>--%>
                           
                                <div class="row">
                                    <div class="col-sm-12" style="text-align: left">
                                        <asp:CheckBox ID="chkPerAddress" Text="CURRENT/PERMANENT/OVERSEAS ADDRESS DETAILS"
                                            AutoPostBack="true" runat="server" CssClass="control-label" Style="display: none;"
                                            TabIndex="40" />
                                        <span><font color="red" style="display: none;">*</font></span>
                                        <asp:Label ID="Label5" Text="CURRENT/PERMANENT/OVERSEAS ADDRESS DETAILS" runat="server" CssClass="control-label" Style="font-weight: 700;"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblAddressType" Text="Address Type" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font></span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="ddlAddressType" runat="server" CssClass="form-control" AutoPostBack="true"
                                                    TabIndex="41">
                                                
                                                </asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblProofOfAddress" Text="Proof of Address" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font></span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="ddlProofOfAddress" runat="server" CssClass="form-control" AutoPostBack="true"
                                            TabIndex="42">
                                            
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div id="divAddProof" runat="server" class="row">
                                    <div id="divPassNoAdd" runat="server" class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblPassportNoAdd" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font></span>
                                    </div>
                                    <div id="divPassNotxtAdd" runat="server" class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" runat="server" onChange="javascript:this.value=this.value.toUpperCase();"
                                            ID="txtPassNoAdd" MaxLength="15" TabIndex="43" />

                                    </div>
                                    <div id="divPassAdd" runat="server" class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="llPassExpDateAdd" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font></span>
                                    </div>
                                    <div id="divPassDateAdd" runat="server" class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" onmousedown="$('#ctl00_ContentPlaceHolder1_txtPassExpDateAdd').datepicker({ changeMonth: true, changeYear: true });"
                                            onchange="setDateFormat('ctl00_ContentPlaceHolder1_txtPassExpDateAdd')" runat="server"
                                            ID="txtPassExpDateAdd" MaxLength="15" TabIndex="44" />
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtPassOthrAdd" MaxLength="15"
                                            TabIndex="45" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblAddressLine1" Text="Address Line1" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font></span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtDrivLicNum')" runat="server"
                                            ID="txtAddressLine1" MaxLength="300" TabIndex="46" />
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblAddressLine2" Text="AddressLine2" runat="server" CssClass="control-label"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtAddressLine2')"
                                            runat="server" ID="txtAddressLine2" MaxLength="300" TabIndex="47" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblAddressLine3" Text="Address Line3" runat="server" CssClass="control-label"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtDrivLicNum')" runat="server"
                                            ID="txtAddressLine3" MaxLength="300" TabIndex="48" />
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblCity" Text="City/Town/Village" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font></span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" TabIndex="49">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblDistrict" Text="District" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font></span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="ddlDistrict" runat="server" CssClass="form-control" Enabled="false"
                                            TabIndex="50">
                                     
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblPinCode" Text="Pin/Post Code" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font></span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="ddlPinCode" runat="server" CssClass="form-control"
                                            AutoPostBack="True" TabIndex="51">
                                       
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblState" Text="State/U.T Code" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font></span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="ddlState" runat="server" CssClass="form-control" Enabled="false"
                                            TabIndex="52">
                                        
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblIsoCountryCode" Text="ISO 3166 Country Code" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font></span>
                                    </div>
                                    <div class="col-sm-3">
                                        <%--       <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtState')" runat="server"
                                                    ID="txtCountryCode" MaxLength="15"  TabIndex="12" Enabled="false" />--%>
                                        <asp:TextBox ID="txtCountryCode" runat="server" CssClass="form-control" AutoPostBack="true"
                                            TabIndex="53">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div style="margin-top: 25px; margin-bottom: 25px">
                                    <div class="row">
                                        <div class="col-sm-12" style="text-align: left">
                                            <asp:CheckBox ID="chkLocalAddress" Text="CORRESPONDENCE/LOCAL ADDRESS DETAILS" runat="server" Style="display: none"
                                                CssClass="control-label" TabIndex="54" />
                                            <span><font color="red" style="display: none">*</font></span>
                                            <asp:Label ID="Label2" Text="CORRESPONDENCE/LOCAL ADDRESS DETAILS" runat="server" CssClass="control-label" TabIndex="54" Style="font-weight: 700;"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12" style="text-align: left">
                                            <asp:CheckBox ID="chkCuurentAddress" Text="Same as Current/Permanent/Overseas Address details"
                                                AutoPostBack="true" runat="server"
                                                CssClass="control-label" TabIndex="55" />
                                            <span><font color="red">*</font></span>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3" style="text-align: left">
                                            <asp:Label ID="lblLocAddLine1" Text="Address Line1" runat="server" CssClass="control-label"></asp:Label>
                                            <span><font color="red">*</font></span>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtLocAddLine1')" runat="server"
                                                ID="txtLocAddLine1" MaxLength="300" TabIndex="56" />
                                        </div>
                                        <div class="col-sm-3" style="text-align: left">
                                            <asp:Label ID="lblLocAddLine2" Text="AddressLine2" runat="server" CssClass="control-label"></asp:Label>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtLocAddLine2')" runat="server"
                                                ID="txtLocAddLine2" MaxLength="300" TabIndex="57" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3" style="text-align: left">
                                            <asp:Label ID="lblLocAddLine3" Text="Address Line3" runat="server" CssClass="control-label"></asp:Label>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtDrivLicNum')" runat="server"
                                                ID="txtLocAddLine3" MaxLength="300" TabIndex="58" />
                                        </div>
                                        <div class="col-sm-3" style="text-align: left">
                                            <asp:Label ID="lblCity1" Text="City/Town/Village" runat="server" CssClass="control-label"></asp:Label>
                                            <span><font color="red">*</font></span>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtCity1" runat="server" CssClass="form-control" TabIndex="59"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3" style="text-align: left">
                                            <asp:Label ID="lblDistrict1" Text="District" runat="server" CssClass="control-label"></asp:Label>
                                            <span><font color="red">*</font></span>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="ddlDistrict1" runat="server" CssClass="form-control" Enabled="false"
                                                TabIndex="60">
                                          
                                            </asp:TextBox>
                                        </div>
                                        <div class="col-sm-3" style="text-align: left">
                                            <asp:Label ID="lblPin1" Text="Pin/Post Code" runat="server" CssClass="control-label"></asp:Label>
                                            <span><font color="red">*</font></span>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="ddlPinCode1" runat="server" CssClass="form-control"
                                                AutoPostBack="True" TabIndex="61">
                                       
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3" style="text-align: left">
                                            <asp:Label ID="lblState1" Text="State/U.T Code" runat="server" CssClass="control-label"></asp:Label>
                                            <span><font color="red">*</font></span>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="ddlState1" runat="server" CssClass="form-control" Enabled="false"
                                                TabIndex="62">
                                            </asp:TextBox>
                                        </div>
                                        <div class="col-sm-3" style="text-align: left">
                                            <asp:Label ID="lblCountryCode1" Text="ISO 3166 Country Code" runat="server" CssClass="control-label"></asp:Label>
                                            <span><font color="red">*</font></span>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtCountryCode1" runat="server" CssClass="form-control" AutoPostBack="true"
                                                TabIndex="63">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <%--below commented by rutuja ---%>
                                <%--                                <div class="row">
                                    <div class="col-sm-12" style="text-align: left">
                                        <asp:CheckBox ID="chkAddResident" Text="ADDRESS IN THE JURISDICTION DETAILS WHERE APPLICANT IS RESIDENT OUTSIDE INDIA FOR TAX PURPOSES"
                                            runat="server" CssClass="control-label" TabIndex="64" />
                                        <span><font color="red">*</font> </span>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6" style="text-align: left">
                                        <asp:CheckBox ID="chkCurrentAdd" Text="Same as Current/Permanent/Overseas Address details"
                                            TabIndex="65"  AutoPostBack="true" runat="server"
                                            CssClass="control-label" />
                                        <span><font color="red">*</font> </span>
                                    </div>
                                    <div class="col-sm-6" style="text-align: left">
                                        <asp:CheckBox ID="chkCorresAdd" Text="Same as Correspondance/Local Address details"
                                            TabIndex="66"  AutoPostBack="true" runat="server"
                                            CssClass="control-label" />
                                        <span><font color="red">*</font> </span>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblAddLine1" Text="Address Line1" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font> </span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtAddLine1')" runat="server"
                                            ID="txtAddLine1" MaxLength="300" TabIndex="67" />
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblAddLine2" Text="AddressLine2" runat="server" CssClass="control-label"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtAddLine2')" runat="server"
                                            ID="txtAddLine2" MaxLength="300" TabIndex="68" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblAddLine3" Text="Address Line3" runat="server" CssClass="control-label"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtAddLine3')" runat="server"
                                            ID="txtAddLine3" MaxLength="300" TabIndex="69" />
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblCity2" Text="City/Town/Village" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font> </span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtCity2" runat="server" CssClass="form-control" TabIndex="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblDistrict2" Text="District" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font> </span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="ddlDistrict2" runat="server" CssClass="form-control" AutoPostBack="true"
                                            Enabled="false" TabIndex="71">
                                        
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblPin2" Text="Pin/Post Code" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font> </span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="ddlPinCode2" runat="server" CssClass="form-control" AutoPostBack="true"
                                            TabIndex="72">
                                       
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblState2" Text="State/U.T Code" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font> </span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="ddlState2" runat="server" CssClass="form-control" TabIndex="73"
                                            Enabled="false">
                                         
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblIsoCountry2" Text="ISO 3166 Country Code" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font> </span>
                                    </div>
                                    <div class="col-sm-3">
                                      
                                        <asp:TextBox ID="txtIsoCountryCode" runat="server" CssClass="form-control" AutoPostBack="true"
                                            TabIndex="74">
                                        </asp:TextBox>
                                    </div>
                                </div>--%>
                                <%--ended commented by rutuja ---%>
                            </div>
                        <%-- Added for Proof of Address end--%>
                        
                        
                        </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <%--  Added for Contact Details end--%>
            </div>
        </div>
        <%--   <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>--%>
        <%--  Added for Contact Details start--%>
        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                            <div id="Div8" runat="server" class="panel-heading subheader">
                                <div class="row">
                                    <div class="col-sm-10" style="text-align: left">
                                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                                        <asp:Label ID="lblContactDetails" Text="CONTACT DETAILS(All communication will be sent on provided MobileNo./Email-ID)"
                                            runat="server" CssClass="control-label"></asp:Label>
                                    </div>
                                    <div class="col-sm-2" onclick="ShowReqDtl1('EmptyPagePlaceholder_div9','Span3');return false;">
                                        <span id="Span3" class="glyphicon glyphicon-resize-full" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                                    </div>
                                </div>
                            </div>
                            <div id="div9" style="display: block;" runat="server" class="panel-body">
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblTelOff1" runat="server" Text="Tel.(Off)" CssClass="control-label"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="input-group">
                                            <span class="input-group-addon input-group-addon-tel">
                                                <asp:TextBox ID="txtTelOff" runat="server" CssClass="form-control" TabIndex="75" onkeypress="fncInputNumericValuesOnly();" MaxLength="4" Style="border-top-left-radius: 4px; border-bottom-left-radius: 4px;"></asp:TextBox>
                                            </span>
                                            <asp:TextBox ID="txtTelOff2" runat="server" CssClass="form-control" onkeypress="fncInputNumericValuesOnly();"
                                                MaxLength="10" TabIndex="76"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblTelRes" runat="server" CssClass="control-label" Text="Tel.(Res)"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="input-group">
                                            <span class="input-group-addon input-group-addon-tel">
                                                <asp:TextBox ID="txtTelRes" runat="server" CssClass="form-control" TabIndex="77" onkeypress="fncInputNumericValuesOnly();" MaxLength="4" Style="border-top-left-radius: 4px; border-bottom-left-radius: 4px;"></asp:TextBox>
                                            </span>
                                            <asp:TextBox ID="txtTelRes2" runat="server" CssClass="form-control" MaxLength="10" onkeypress="fncInputNumericValuesOnly();"
                                                TabIndex="78"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblMobile" runat="server" Text="Mobile" CssClass="control-label"></asp:Label>

                                    </div>
                                    <div class="col-sm-3">
                                        <div class="input-group">
                                            <span class="input-group-addon input-group-addon-tel">
                                                <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" TabIndex="79" onkeypress="fncInputNumericValuesOnly();" MaxLength="3" Style="border-top-left-radius: 4px; border-bottom-left-radius: 4px;"></asp:TextBox>
                                            </span>
                                            <asp:TextBox ID="txtMobile2" runat="server" CssClass="form-control" onkeypress="fncInputNumericValuesOnly();"
                                                MaxLength="10" TabIndex="80"></asp:TextBox>
                                        </div>
                                        <asp:Label ID="Label4" Visible="false" runat="server" Text="(Mobile No should be 10 digit.)"
                                            Font-Size="Smaller" ForeColor="Red"></asp:Label>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left; display: none">
                                        <%-- none by Rutuja --%>
                                        <asp:Label ID="lblFax" runat="server" Text="" CssClass="control-label"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="input-group">
                                            <span class="input-group-addon input-group-addon-tel">
                                                <asp:TextBox ID="txtFax1" runat="server" CssClass="form-control" Visible="false" TabIndex="81" onkeypress="fncInputNumericValuesOnly();" MaxLength="4" Style="border-top-left-radius: 4px; border-bottom-left-radius: 4px;"></asp:TextBox>
                                            </span>
                                            <asp:TextBox ID="txtFax2" runat="server" CssClass="form-control" Visible="false" MaxLength="10" TabIndex="82" onkeypress="fncInputNumericValuesOnly();"></asp:TextBox>
                                        </div>
                                        <%-- Visible false by Rutuja--%>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblpfemail" runat="server" Text="Email 1" CssClass="control-label"></asp:Label>

                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtemail" runat="server" CssClass="form-control" MaxLength="100"
                                            TabIndex="83"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
        <div class="panel panel-success">
            <div id="Div21" runat="server" class="panel-heading" onclick="showHideDiv('menu4','Span10');return false;">
                <div class="row">
                    <div class="col-sm-10" style="text-align: left">
                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                        <asp:Label ID="lblDtlOfRtltpr" Text="RELATED PERSONS"
                            runat="server" CssClass="control-label">  <%-- text changed by rutuja --%>
                        </asp:Label>
                    </div>
                    <div class="col-sm-2">
                        <span id="Span10" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>

                    </div>
                </div>
            </div>
            <div id="menu4" class="panel-body">

                <%--  Added for Details of Related Person start--%>
                <div class="row">
                    <div id="divchkAddRel" class="col-sm-3" style="text-align: left" runat="server">
                        <asp:CheckBox ID="chkAddRel" Text=" Addition of Related Person" TabIndex="84" AutoPostBack="true"
                            runat="server" CssClass="control-label" />
                        <span><font color="red">*</font></span>
                    </div>
                    <div id="divchkDelRel" class="col-sm-6" style="text-align: left" runat="server" visible="false">
                        <asp:CheckBox ID="chkDelRel" Text=" Deletion of Related Person" TabIndex="85" runat="server"
                            CssClass="control-label" />
                        <span><font color="red">*</font></span>
                    </div>
                    <div id="div10" class="col-sm-3" style="text-align: left" runat="server">
                    </div>
                    <div id="div11" class="col-sm-3" style="text-align: left" runat="server">
                    </div>
                    <div id="div5" class="col-sm-3" style="text-align: right" runat="server">

                        <asp:LinkButton ID="lnkViewRel" runat="server" Text="View Related Person Detail" FontBold="true" OnClick="lnkViewRel_Click"></asp:LinkButton>
                        <%-- --%>
                    </div>
                </div>
                <div class="row">
                    <div id="div12" class="col-sm-12" style="text-align: center" runat="server">
                        <asp:Label ID="lblRelRecordShow" Style="text-align: center" Text="  No Records Found" Visible="false" runat="server" />
                    </div>
                    <asp:GridView ID="gvMemDtls" CssClass="footable" Width="100%" runat="server" AllowSorting="True"
                        AutoGenerateColumns="False" OnRowDataBound="gvMemDtls_RowDataBound">

                        <HeaderStyle HorizontalAlign="Center" BackColor="#dce9f9" />
                        <FooterStyle CssClass="GridViewFooter" />
                        <RowStyle CssClass="GridViewRow" />
                        <SelectedRowStyle CssClass="GridViewSelectedRow" />
                        <AlternatingRowStyle CssClass="GridViewAlternateRow"></AlternatingRowStyle>

                        <Columns>
                            <asp:TemplateField HeaderText="Sr. No." ItemStyle-Width="20%" SortExpression="Reference No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Relative Reference No." ItemStyle-Width="20%" SortExpression="RelRefNo">
                                <ItemTemplate>
                                    <asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("RelRefNo") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Relative Type" ItemStyle-Width="20%" SortExpression="RelationTypetxt.">
                                <ItemTemplate>
                                    <asp:Label ID="lblRelTypVal" runat="server" Text='<%# Eval("RelationTypetxt") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Relative Name" ItemStyle-Width="20%" SortExpression="FNameRel">
                                <ItemTemplate>
                                    <asp:Label ID="lblNameVal" runat="server" Text='<%# Eval("FNameRel") + " " + Eval("LNameRel")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date Of Birth" ItemStyle-Width="20%" SortExpression="DOBRel">
                                <ItemTemplate>
                                    <asp:Label ID="lblMemDOBVal" runat="server" Text='<%# Eval("DOBRel") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Gender" ItemStyle-Width="20%" SortExpression="GenderReltxt">
                                <ItemTemplate>
                                    <asp:Label ID="lblMemGenVal" runat="server" Text='<%# Eval("GenderReltxt") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Marital Status " ItemStyle-Width="20%" SortExpression="MaritalStatusReltxt">
                                <ItemTemplate>
                                    <asp:Label ID="lblMemMrtVal" runat="server" Text='<%# Eval("MaritalStatusReltxt") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Citizenship" ItemStyle-Width="20%" SortExpression="CitizenshipReltxt">
                                <ItemTemplate>
                                    <asp:Label ID="lblMemCizVal" runat="server" Text='<%# Eval("CitizenshipReltxt") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Residential Status " ItemStyle-Width="20%" SortExpression="ResiStatusReltxt">
                                <ItemTemplate>
                                    <asp:Label ID="lblMemResiVal" runat="server" Text='<%# Eval("ResiStatusReltxt") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Occupation Type" ItemStyle-Width="20%" SortExpression="OccuTypeReltxt">
                                <ItemTemplate>
                                    <asp:Label ID="lblMemOccVal" runat="server" Text='<%# Eval("OccuTypeReltxt") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="15%" SortExpression="Request" HeaderText="Action">
                                <ItemTemplate>
                                    <div style="width: 20%; white-space: nowrap;">
                                        <span class="glyphicon glyphicon-flag"></span>

                                        <asp:LinkButton ID="lnkdelete" runat="server" OnClick="lnkdelete_Click" Text="Delete"></asp:LinkButton>&nbsp;
                                              <asp:LinkButton ID="lnkView" runat="server" OnClick="lnkView_Click" Text="View" CommandArgument='<%# Eval("RelRefNo") %>'></asp:LinkButton>
                                        <%--      CommandArgument='<%# Eval("RelRefNo") %>'--%>
                                    </div>
                                </ItemTemplate>
                                <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>




                        </Columns>
                    </asp:GridView>
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
        <%-- </ContentTemplate>
        </asp:UpdatePanel>--%>
        <div class="panel panel-success">
            <div id="Div22" class="panel-heading" onclick="showHideDiv('menu5','Span11');return false;">
                <div class="row">
                    <div class="col-sm-10" style="text-align: left">
                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                        <asp:Label ID="lblRemarks" Text="REMARKS" runat="server" CssClass="control-label">
                        </asp:Label>
                    </div>
                    <div class="col-sm-2">
                        <span id="Span11" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>

                    </div>
                </div>
            </div>
            <div id="menu5" class="panel-body">
                <%--  Added for Details of Remarks start--%>
                <div class="row">
                    <div class="col-sm-12">
                        <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtRemarks')" runat="server"
                            ID="txtRemarks" TextMode="MultiLine" MaxLength="15" TabIndex="96" />
                    </div>
                </div>
                <%--  Added for Details of Remarks end--%>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <div class="panel panel-success">
                    <div id="Div23" runat="server" class="panel-heading" onclick="showHideDiv('menu6','Span12');return false;">
                        <div class="row">
                            <div class="col-sm-10" style="text-align: left">
                                <span class="glyphicon glyphicon-menu-hamburger"></span>
                                <asp:Label ID="lblattstn" Text="ATTESTATION" runat="server" CssClass="control-label">
                                </asp:Label>
                            </div>
                            <div class="col-sm-2">
                                <span id="Span12" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>

                            </div>
                        </div>
                    </div>
                    <div id="menu6" class="panel-body">
                        <%--  Added for Applicant Declaration start--%>
                        <div class="panel panel-success">
                            <div id="Div14" runat="server" class="panel-heading subheader"
                                onclick="ShowReqDtl1('EmptyPagePlaceholder_div15','Span6');return false;">
                                <div class="row">
                                    <div class="col-sm-10" style="text-align: left">
                                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                                        <asp:Label ID="lbldec" Text="APPLICANT DECLARATION" runat="server" CssClass="control-label">
                                        </asp:Label>
                                    </div>
                                    <div class="col-sm-2">
                                        <span id="Span6" class="glyphicon glyphicon-resize-full" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                                    </div>
                                </div>
                            </div>
                            <div id="div15" style="display: block;" runat="server" class="panel-body">
                                <div class="row">
                                    <div class="col-sm-12" style="text-align: left; display: flex;">
                                        <%--  <asp:label cssclass="control-label" text="I hereby declare that the details furnished above are true and correct to the best of my knowledge and belief and I undertake to inform you of any changes therein immediately.In case any of the above information is found to be false or untrue or misleading or misrepresenting. I am aware that I may be held liable for it."
                                    onchange="setDateFormat('txtRemarks')" runat="server" id="lblAppDeclare1" maxlength="15"
                                    tabindex="12" />--%>
                                        <asp:CheckBox ID="AppDeclare1" Text="I hereby declare that the details furnished above are true and correct to the best of my knowledge and belief and I undertake  to inform you of any changes there in immediately."
                                            CssClass="control-label" AutoPostBack="true" runat="server" onchange="setDateFormat('txtRemarks')"
                                            TabIndex="97" />
                                    </div>
                                    <div class="col-sm-12" style="text-align: left; display: flex; font-weight: bold;">
                                        <asp:Label CssClass="control-label" Text=" In case any of the above information is found to be false or untrue or misleading or misrepresenting. I am aware that I may be held   liable for it."
                                            onchange="setDateFormat('txtRemarks')" runat="server" ID="lblAppDeclare1" />
                                    </div>
                                    <br />
                                    <br />
                                    <br />
                                    <div class="col-sm-12" style="text-align: left; display: flex;">
                                        <%--   <asp:label cssclass="control-label" text="I hereby consent to receiving information from Central KYC Registry through SMS/Email on the above registered number/email address"
                                    onchange="setDateFormat('txtRemarks')" runat="server" id="Label7" maxlength="15"
                                    tabindex="12" />--%>
                                        <asp:CheckBox ID="chkAppDeclare2" Text="I hereby consent to receiving information from Central KYC Registry through SMS/Email on the above registered number/email address"
                                            CssClass="control-label" AutoPostBack="true" runat="server" onchange="setDateFormat('txtRemarks')"
                                            TabIndex="98" />
                                    </div>
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblDate" Text="Date " runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font></span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" onmousedown="$('#txtDate').datepicker({ changeMonth: true, changeYear: true });"
                                            onchange="setDateFormat('txtDate')" runat="server" ID="txtDate" MaxLength="15"
                                            TabIndex="99" />
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblPlace1" Text="Place " runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font></span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtPlace" Enabled="false" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--  Added for Applicant Declaration end--%>
                        <%--  Added for Attestation/For Office Use Only start--%>
                        <div class="panel panel-success">
                            <div id="Div16" runat="server" class="panel-heading subheader"
                                onclick="ShowReqDtl1('EmptyPagePlaceholder_div17','Span7');return false;">
                                <div class="row">
                                    <div class="col-sm-10" style="text-align: left">
                                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                                        <asp:Label ID="lblAttesOfc" Text="ATTESTATION/FOR OFFICE USE ONLY" runat="server" CssClass="control-label">
                                        </asp:Label>
                                    </div>
                                    <div class="col-sm-2">
                                        <span id="Span7" class="glyphicon glyphicon-resize-full" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                                    </div>
                                </div>
                            </div>
                            <div id="div17" style="display: block;" runat="server" class="panel-body">
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblDocRec" Text="Document Received" runat="server" Font-Bold="true"
                                            CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font></span>
                                    </div>
                                    <div class="col-sm-3">
                                        <%--                                <asp:CheckBox ID="chkCertifyCopy" Text="Certified Copies" CssClass="standardcheckbox"
                                    Enabled="false" AutoPostBack="true" runat="server" TabIndex="101" />   commented by rutuja--%>
                                        <asp:DropDownList ID="ddlDocReceived" runat="server" CssClass="form-control" TabIndex="2">
                                        </asp:DropDownList>
                                        <%-- added by rutuja--%>
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
                                        <span><font color="red">*</font></span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" onmousedown="$('#txtDateKYCver').datepicker({ changeMonth: true, changeYear: true });"
                                            onchange="setDateFormat('txtDateKYCver')" runat="server" ID="txtDateKYCver" MaxLength="15"
                                            TabIndex="102" />
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblEmpName" Text="Employee Name" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <span><font color="red">*</font></span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtEmpName" MaxLength="15"
                                            Enabled="false" TabIndex="103" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblEmpCode" Text="Employee Code" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <span><font color="red">*</font></span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtEmpCode" MaxLength="15"
                                            Enabled="false" TabIndex="104" />
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblEmpDesignation" Text="Employee Designation" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <span><font color="red">*</font></span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtEmpDesignation" MaxLength="15"
                                            Enabled="false" TabIndex="105" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblEmpBranch" Text="Employee Branch" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <span><font color="red">*</font></span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtEmpBranch" MaxLength="15"
                                            Enabled="false" TabIndex="106" />
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
                                        <span><font color="red">*</font></span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" onmousedown="$('#txtInsName').datepicker({ changeMonth: true, changeYear: true });"
                                            onchange="setDateFormat('txtDate3')" runat="server" ID="txtInsName" MaxLength="15"
                                            Enabled="false" TabIndex="107" />
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblInsCode" Text="Code" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font></span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtInsCode" MaxLength="15"
                                            Enabled="false" TabIndex="108" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--  Added for Attestation/For Office Use Only  end--%>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div id="divImageBind" runat="server" class="panel panel-success">
            <div id="div13" class="panel-heading subheader" onclick="showHideDiv('divPhoto','btnImage');return false;">
                <div class="row">
                    <div class="col-sm-10" style="text-align: left; top: 0px; left: 0px;">
                        <span class="glyphicon glyphicon-menu-hamburger"></span>Uploaded
                                Documents
                    </div>
                    <div class="col-sm-2">
                        <span id="btnImage" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>

                    </div>
                </div>
            </div>
            <br />
            <div id="divPhoto" runat="server" class="panel-body">
                <div class="row" style="text-align: left; display: none">
                    <div style="padding-left: 2%;" class="col-sm-2">
                        <font color="blue"><b>Document Status: </b></font>
                    </div>
                    <div class="col-sm-2" style="display: none">
                        <font color="Red">Raised CFR</font><span id="raise" runat="server" class="badge">5</span>
                    </div>
                    <div class="col-sm-2" style="display: none">
                        <font color="darkorange">Responded CFR</font><span id="respond" runat="server" class="badge">10</span>
                    </div>
                    <div class="col-sm-2" style="display: none">
                        <font color="blue">Closed CFR </font><span id="close" runat="server" class="badge">10</span>
                    </div>
                    <div class="col-sm-2">
                        <font color="green">Approved Document</font><span id="approvedoc" runat="server"
                            class="badge">10</span>
                    </div>
                    <asp:HiddenField ID="hdnraise" runat="server" />
                    <asp:HiddenField ID="hdnrespond" runat="server" />
                    <asp:HiddenField ID="hdnclose" runat="server" />
                    <asp:HiddenField ID="hdnCndNo" runat="server" />
                </div>
                <div class="row" style="display: none">
                    <div class="col-sm-6" style="padding-left: 2%; text-align: left;">
                        <asp:CheckBox ID="chkOtherCFR" runat="server" CssClass="standardcheckbox" Enabled="true"
                            AutoPostBack="true" />
                        <%----%>
                        <asp:Label ID="lblOther" CssClass="control-label" Font-Bold="true" runat="server"
                            Text="Other CFR Details"></asp:Label>
                        <asp:HiddenField ID="ZoutSize" runat="server" />
                        <asp:HiddenField ID="ZinSize" runat="server" />
                        <asp:HiddenField ID="hdnCFR" runat="server" />
                    </div>
                </div>
            </div>
        </div>
        <%-- </div>
            <div id="menu8" class="tab-pane fade">--%>
        <%--  Added for movment start--%>
        <div class="panel panel-success" id="divmvmt" runat="server">
            <div id="cndmvmt" runat="server" class="panel-heading" onclick="showHideDiv('divCndMvmt','Span4');return false;">
                <div class="row" id="Div26" runat="server">
                    <div class="col-sm-10" style="text-align: left">
                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                        <asp:Label ID="Label12" Text="Candidate Movement Status" runat="server" CssClass="control-label"></asp:Label>
                    </div>
                    <div class="col-sm-2">
                        <span id="Span4" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>

                    </div>
                </div>
            </div>
            <div id="divCndMvmt" class="panel-body" runat="server">
                <div id="tblcndmvmt" runat="server">
                    <asp:GridView AllowSorting="True" ID="dgCndMvmt" runat="server" CssClass="footable" Width="100%"
                        AutoGenerateColumns="False" CellPadding="1">
                        <HeaderStyle HorizontalAlign="Center" BackColor="#dce9f9" />
                        <FooterStyle CssClass="GridViewFooter" />
                        <RowStyle CssClass="GridViewRow" />
                        <SelectedRowStyle CssClass="GridViewSelectedRow" />
                        <AlternatingRowStyle CssClass="GridViewAlternateRow"></AlternatingRowStyle>

                        <Columns>
                            <asp:TemplateField SortExpression="CreatedDt" HeaderText="Movement Date" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblCreatedDt" runat="server" Text='<%# Eval("CreatedDt","{0:dd/MM/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" CssClass="pad" Font-Bold="False"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="cndStatus" HeaderText="Status" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblcndstat" runat="server" Text='<%# Eval("cndStatus") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" CssClass="pad" Font-Bold="False"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="CreatedBy" HeaderText="Movement By" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblCreatedBy" runat="server" Text='<%# Eval("CreatedBy")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" CssClass="pad" Font-Bold="False"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="RemarkReject" HeaderText="Reason of Reject" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblReaRej" runat="server" Text='<%# Eval("ReasonRejection")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" CssClass="pad" Font-Bold="False"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>
                </div>
            </div>
        </div>

        <%--  Added for movment start--%>
        <div class="row" style="margin-top: 12px;" id="divbtn">
            <div class="col-sm-12" align="center">

                <asp:Button ID="LinkButton1" runat="server" CssClass="btn-animated bg-horrible" OnClick="btnCancel_Click" Text="Cancel"></asp:Button>

                <div id="div30" runat="server" style="display: none;">
                    <img id="Img2" alt="" src="~/images/spinner.gif" runat="server" />
                    Loading...
                </div>
            </div>
        </div>
    </div>



    <div id="myModalImage" class="modal" style="padding-top: 10px">
        <div class="modal-content">
            <div class="modal-header">
                <%-- <button type="button" class="close" data-dismiss="modal">
                    &times;</button>--%>
                <div class="modal-title">
                    <asp:HiddenField ID="hdnImgId" runat="server" />
                    <asp:HiddenField ID="hdnId" runat="server" />
                    <asp:Label ID="lblDocType" Text="Document Name:" CssClass="control-label" runat="server"></asp:Label>
                    <asp:Label ID="lblDocDesc" runat="server" Text="" CssClass="control-label"></asp:Label>
                </div>
            </div>
            <div class="modal-body">
                <div id="img-preview" style="height: 310px;">
                    <asp:Image ID="img3" runat="server" class="image-box" Style="cursor: move;" />
                </div>
                <div class="img-op">
                    <asp:HiddenField ID="HiddenField5" runat="server" />
                    <asp:HiddenField ID="hdnRotateValue" runat="server" />
                    <asp:HiddenField ID="HiddenField6" runat="server" />
                </div>
                <div class="img-op">
                </div>

            </div>
            <div class="modal-footer" style="text-align: center; padding: 5px;">
                <asp:UpdatePanel ID="updbuttons" runat="server">
                    <ContentTemplate>
                        <button type="button" class="btn btn-danger" onclick="return Cancel(myModalImage);">
                            <span class="glyphicon glyphicon-remove" style="color: White"></span>Cancel</button>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="modal" id="myModalRaise" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true" style="padding-top: 0px;">
        <div class="modal-dialog" style="padding-top: 0px; margin-top: 2px; width: 95%;">
            <div class="modal-content" style="width: 90%; max-width: 100%;">
                <div class="modal-header" style="margin-top: -10px; margin-bottom: -20px; padding-bottom: 6px ! important;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title" id="myModalLabel">CKYC Related Person Details</h4>
                </div>
                <div class="modal-body">
                    <iframe id="iframeCFR" src="" width="100%" height="505" frameborder="0" allowtransparency="true"></iframe>
                </div>
                <div class="modal-footer">
                    <div style="text-align: center;">
                        <asp:LinkButton ID="lnkRaise" TabIndex="43" runat="server" CssClass="btn btn-danger"
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
</asp:Content>
