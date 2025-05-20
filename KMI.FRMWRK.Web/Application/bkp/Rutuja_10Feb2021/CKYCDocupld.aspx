<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="CKYCDocupld.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.CKYC.CKYCDocupld" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        function popup() {
            $("#myModal").modal();
            lblRefNoValue
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">
    <style type="text/css">
        .disablepage {
            display: none;
        }

        .gridview th {
            padding: 3px;
            height: 40px;
            background-color: #d6d6c2;
            color: #337ab7;
        }

        .imgheight {
            display: block;
            max-width: 100%;
            height: 50px;
        }

        .textalign th {
            padding-left: 42%;
        }

        .modal-dialog {
            position: relative;
            display: table;
            overflow-y: auto;
            overflow-x: auto;
            width: auto;
            min-width: 50px;
        }
    </style>

    <style type="text/css">
        .disablepage {
            display: none;
        }

        .gridview th {
            padding: 3px;
            height: 40px;
            background-color: #d6d6c2;
            color: #337ab7;
        }

        .imgheight {
            display: block;
            max-width: 100%;
            height: 50px;
        }

        .textalign th {
            padding-left: 42%;
        }

        .modal-dialog {
            position: relative;
            display: table;
            overflow-y: auto;
            overflow-x: auto;
            width: auto;
            min-width: 50px;
        }
    </style>
    <style type="text/css">
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
    <script type="text/javascript">


        var doccode;
        var arg03, Transfr;
        var ZinSize, ZoutSize;
        var MstWidth, MstHeight;
        var ImgWidth, ImgHeight, Size, flag;
        var counter;
        function Cancel(modalimg) {
            debugger;
            var modal = modalimg;
            var span = document.getElementsByClassName("close")[0];
            modal.style.display = "none";
        }

        function Confirm(row) {
            debugger;
            var rowData = row.parentNode.parentNode;
            var rowIndex = rowData.rowIndex;
            var ri = parseInt(rowIndex);
            var grvUpld = document.getElementById('<%=dgView.ClientID%>');
            var ID = grvUpld.rows[ri].cells[0].children[0];
            var hdnid = ID.innerHTML;
            // document.getElementById('<%=HiddenField1.ClientID%>').value = hdnid;
        }

        function funcopencrop2() {
            debugger;
            var modal = document.getElementById('myModalCrop');
            var modaliframe = document.getElementById("iframe1");
            var cndno = document.getElementById('<%=hdnRegRefNo.ClientID%>').value;
            var userid = document.getElementById('<%=hdnUserId.ClientID%>').value;

            modaliframe.src = "../../Application/CKYC/CropImage.aspx?TrnRequest=Preview&RefNo=" + cndno + "&args3=" + document.getElementById('<%=HiddenField1.ClientID%>').value + "&mdlpopup=MdlPopRaiseCrop";

            var span = document.getElementsByClassName("close")[0];
            var modal = document.getElementById('myModalCrop');
            modal.style.display = "block";
            //span.onclick = function () {
            //    debugger;
            //    modal.style.display = "none";

            //}

            $("#myModalCrop").modal();
        }

        // Get the modal
        function showimage(ImgId, ImgCode, Height, Width, ZinSize1, ZoutSize1, MstWidth1, MstHeight1, Flag) {
            debugger;
            if (ImgCode.toString().length == 1) {
                ImgCode = "0" + ImgCode;
            }
            $('#EmptyPagePlaceholder_hdnRotateValue').val("0");
            $("#EmptyPagePlaceholder_hdnHt").val(Height);
            $("#EmptyPagePlaceholder_hdnWt").val(Width);
            //('#EmptyPagePlaceholder_btnSaveImage').attr("disabled", true);
            counter = 0;
            flag = 1;
            MstWidth = MstWidth1;
            MstHeight = MstHeight1;
            ZinSize = ZinSize1;
            ZoutSize = ZoutSize1;
            Size = ((ZoutSize1 / 1024) * 20) / 100;
            ImgWidth = Width;
            ImgHeight = Height;
            var cndno = document.getElementById('<%=hdnRegRefNo.ClientID%>').value;
            var modal = document.getElementById('myModalImage');
            var ImgSrc = "";
            ImgSrc = "ImageCSharp.aspx?ImageID=" + "CKYC" + ImgId;

            var img = document.getElementById('myImg');
            var modalImg = document.getElementById("EmptyPagePlaceholder_img3");

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
            $("#EmptyPagePlaceholder_HiddenField1").val(ImgId);
            // $("#EmptyPagePlaceholder_HiddenField1").val(myBookId);  usha
            //            if (myBookId == "Photo" || myBookId == "Signature") {
            //                $("#btnCrop").show();
            //            }
            //            else {
            //                $("#btnCrop").hide();
            //            }
            if (Flag == 2) {
                $('#EmptyPagePlaceholder_btnApp').attr("disabled", true);
            }
            else {
                $('#EmptyPagePlaceholder_btnApp').attr("disabled", false);
            }
            arg03 = myBookId;


            var span = document.getElementsByClassName("close")[0];
            var span1 = document.getElementsByClassName("btn btn-default")[0];

        }

        function rotateImage() {
            debugger;
            var options;

            var box = $('#EmptyPagePlaceholder_img3');
            counter += 90;
            $('#EmptyPagePlaceholder_hdnRotateValue').val(counter);
            $('#EmptyPagePlaceholder_img3').css('transform', 'rotate(' + counter + 'deg)')
        }

        function funcopencrop1() {
            debugger;
            var modal = document.getElementById('myModalCrop');
            var modaliframe = document.getElementById("iframe1");
            var cndno = document.getElementById('<%=hdnRegRefNo.ClientID%>').value;
            var userid = document.getElementById('<%=hdnUserId.ClientID%>').value;

            modaliframe.src = "../../Application/CKYC/CropImage.aspx?TrnRequest=Preview&RefNo=" + cndno + "&args3=" + document.getElementById('<%=HiddenField1.ClientID%>').value + "&mdlpopup=MdlPopRaiseCrop";

            var span = document.getElementsByClassName("close")[0];
            var modal = document.getElementById('myModalCrop');
            modal.style.display = "block";
            span.onclick = function () {
                debugger;
                modal.style.display = "none";
            }
            $('#myModalCrop').modal('show');
        }

        //        ----------------Image end -----



        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();

        });

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

        function ShowReqDtl(divName, btnName) {
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
                ShowError(err.description);
            }
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="page-container" style="margin-top: 0px;">

        <div class="panel panel-success" style="margin-left: 2%; margin-right: 2%; margin-top: 0.5%">
            <div id="Div3" runat="server" class="panel-heading" onclick="showHideDiv('divSearchDetails','btndivSearchDetails');return false;">
                <div class="row">
                    <div class="col-sm-10" style="text-align: left">
                        <span class="glyphicon glyphicon-menu-hamburger" style=""></span>
                        <asp:Label ID="lblTitle" runat="server" Text="CKYC Candidate Details" CssClass="control-label"></asp:Label>
                    </div>
                    <div class="col-sm-2">
                        <span id="btndivSearchDetails" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                    </div>
                </div>
            </div>
            <div id="divSearchDetails" class="panel-body" style="display: block">
                <div class="row" style="margin-bottom: 5px">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblRefNo" runat="server" CssClass="control-label" Text="CKYC Reference Number"
                            Font-Bold="False"></asp:Label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="lblRefNoValue" runat="server" CssClass="form-control" MaxLength="20"
                            Enabled="false"> 
                        </asp:TextBox>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblCndName" CssClass="control-label" runat="server" Text="Name"
                            Font-Bold="False"></asp:Label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="lblAdvNameValue" runat="server" CssClass="form-control" MaxLength="20"
                            Enabled="false"> 
                        </asp:TextBox>
                    </div>
                </div>
                <div class="row" style="margin-bottom: 5px">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblCreateDt" Text="Registered Date" CssClass="control-label" runat="server"
                            Font-Bold="False"></asp:Label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="lblCreateDtValue" runat="server" CssClass="form-control" MaxLength="20"
                            Enabled="false"> 
                        </asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <div id="trDgViewDtl" runat="server" class="page-container" style="margin-top: 0px;">
            <div class="panel panel-success" style='margin-right: 26px; margin-left: 26px;'>
                <div id="tblupload" runat="server" class="panel-heading" onclick="showHideDiv('div9','btnpnlcfrdtls');return false;">

                    <div class="row">
                        <div class="col-sm-3" style="text-align: left">
                            <span class="glyphicon glyphicon-menu-hamburger" style=""></span>
                            <asp:Label ID="lblCanddoc" runat="server" Text="Candidate Document Upload" CssClass="control-label"></asp:Label>

                        </div>
                        <div class="col-md-5" style="text-align: center">
                            <asp:Label ID="txtcolr" runat="server" Height="12px" Width="20px" CssClass="form-control" BackColor="LightPink"></asp:Label>
                            <asp:Label ID="LinkButton1" runat="server" Text="Mandatory Documents">
                            </asp:Label>&nbsp;&nbsp;&nbsp;
                        </div>
                        <div class="col-sm-4">
                            <span id="btnpnlcfrdtls" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                        </div>
                    </div>
                </div>

                <div id="div9" class="panel-body" style="margin-left: 2%; margin-right: 2%; margin-top: 0.5%">
                    <div class="row">
                        <div class="col-md-12" style="text-align: center">
                            <asp:Label ID="lblNote" runat="server" CssClass="control-label" Text="NOTE: All Documents to be Uploaded/Reuploaded should be in JPEG/JPG/GIF format"
                                ForeColor="red"></asp:Label>
                        </div>
                    </div>
                    <div id="div2" runat="server" style="display: block;">
                        <div class="row" style="overflow: auto;">
                            <asp:UpdatePanel ID="upddgView" runat="server">
                                <ContentTemplate>
                                    <asp:GridView AllowSorting="True" ID="dgView" runat="server" CssClass="footable" Width="100%"
                                        OnRowCommand="dgView_RowCommand"
                                        OnRowDataBound="dgView_RowDataBound" OnRowCreated="dgView_RowCreated"
                                        AutoGenerateColumns="False" PageSize="10" AllowPaging="true" CellPadding="1">
                                        <%--OnRowDataBound="dgView_RowDataBound"--%>
                                        <HeaderStyle HorizontalAlign="Center" BackColor="#dce9f9" />
                                        <FooterStyle CssClass="GridViewFooter" />
                                        <RowStyle CssClass="GridViewRow" />

                                        <SelectedRowStyle CssClass="GridViewSelectedRow" />
                                        <AlternatingRowStyle CssClass="GridViewAlternateRow"></AlternatingRowStyle>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Document Name" HeaderStyle-Width="175px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldocName" runat="server" Font-Size="11px" Text='<%#Bind("ImgDesc01") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" CssClass="pad" Font-Bold="False"></ItemStyle>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Document Description" HeaderStyle-Width="310px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldocDescription" runat="server" Font-Size="11px" Style="text-transform: capitalize;"
                                                        Text='<%#Bind("ImgDesc02") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" CssClass="pad" Font-Bold="False"></ItemStyle>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Max. Size(kb)" HeaderStyle-Width="100px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblupdSize" runat="server" Font-Size="11px" Style="text-transform: capitalize;"
                                                        Text='<%#Bind("MaxImgSize") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" CssClass="pad" Font-Bold="False"></ItemStyle>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Upload Documents">
                                                <ItemTemplate>
                                                    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="updFU">
                                                        <ContentTemplate>
                                                            <asp:FileUpload ID="FileUpload" runat="server" Width="340px"></asp:FileUpload>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="btn_Upload" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" CssClass="pad" Font-Bold="False"></ItemStyle>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="updFU1">
                                                        <ContentTemplate>
                                                            <asp:LinkButton ID="btn_Upload" runat="server" CssClass="standardlabel" Text="Upload"
                                                                Visible="false" OnClick="btn_Upload_Click" Width="80px" />
                                                            <%----%>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="btn_Upload" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="Reupd11">
                                                        <ContentTemplate>
                                                            <asp:LinkButton ID="btn_ReUpload" runat="server" CssClass="standardlabel" Text="ReUpload" Width="80px"
                                                                OnClick="btn_ReUpload_Click" />
                                                            <%--"--%>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="btn_ReUpload" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" CssClass="pad" Font-Bold="False"></ItemStyle>

                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblimgsize" runat="server" Visible="false" Font-Size="11px" Text='<%#Bind("MaxImgSize") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" CssClass="pad" Font-Bold="False"></ItemStyle>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ImgShrt" HeaderStyle-Width="370px" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblimgshrt" runat="server" Font-Size="11px" Style="text-transform: capitalize;"
                                                        Text='<%#Bind("ImgShortCode") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" CssClass="pad" Font-Bold="False"></ItemStyle>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Imgwidth" HeaderStyle-Width="370px" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblimgwidth" runat="server" Font-Size="11px" Style="text-transform: capitalize;"
                                                        Text='<%#Bind("ImgWidth") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" CssClass="pad" Font-Bold="False"></ItemStyle>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ImgHeight" HeaderStyle-Width="370px" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblimgheight" runat="server" Font-Size="11px" Style="text-transform: capitalize;"
                                                        Text='<%#Bind("ImgHeight") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" CssClass="pad" Font-Bold="False"></ItemStyle>

                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblManDoc" runat="server" Text='<%#Bind("IsMandatory") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" CssClass="pad" Font-Bold="False"></ItemStyle>

                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldoccode" runat="server" Font-Size="11px" Style="text-transform: capitalize;"
                                                        Text='<%#Bind("DocCode") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" CssClass="pad" Font-Bold="False"></ItemStyle>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" HeaderStyle-Width="100px">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkPreview" runat="server" Text="Preview" OnClientClick="Confirm(this);" CommandArgument='<%# Eval("DocCode") %>' CommandName="Preview"
                                                        Font-Size="11px" Style="text-transform: capitalize;">
                                             <%-- OnClientClick=" return showimage(2,'<%#Bind("DocCode") %>','<%#Bind("ImgHeight") %>','<%#Bind("ImgWidth") %>',
                                              '<%#Bind("MaxImgSize") %>','<%#Bind("MaxImgSize") %>',100,100,1)"--%>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" CssClass="pad" Font-Bold="False"></ItemStyle>

                                            </asp:TemplateField>
                                        </Columns>

                                    </asp:GridView>
                                    <br />

                                </ContentTemplate>
                                <Triggers>
                                    <%-- <asp:AsyncPostBackTrigger ControlID="cbTrfrFlag" EventName="checkedchanged" />--%>
                                </Triggers>
                            </asp:UpdatePanel>

                        </div>
                    </div>


                    <div class="row" style="margin-top: 12px;" id="divButtons" runat="server">
                        <div class="col-sm-12" align="center">
                            <%--    <asp:LinkButton ID="Btncrop" runat="server"  CssClass="btn btn-primary" Text="CROP" visible="false"
                                    CausesValidation="false"  TabIndex="43"></asp:LinkButton>--%>
                            <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn-animated bg-green" OnClick="btnSubmit_Click" Text="Submit" CausesValidation="false" TabIndex="32">  </asp:LinkButton>


                            <asp:LinkButton ID="btnCancel" OnClick="btnCancel_Click" TabIndex="43" runat="server" Text="Cancel"
                                CssClass="btn-animated bg-horrible">
                            
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <div id="myModalImage" class="modal" role="dialog" style="padding-top: 10px">

        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" onclick="return Cancel(myModalImage);">&times;</button>
                <div class="modal-title">

                    <asp:HiddenField ID="hdnId" runat="server" />
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <asp:Label ID="lblDocType" Text="Document Name:" CssClass="control-label" runat="server"></asp:Label>
                    <asp:Label ID="lblDocDesc" runat="server" Text="" CssClass="control-label"></asp:Label>
                </div>
            </div>
            <div class="modal-body">
                <div id="img-preview">

                    <asp:Image ID="img3" runat="server" class="image-box" Style="cursor: move;" />
                </div>
                <br />
                <div class="img-op">

                    <asp:HiddenField ID="ZoutSize" runat="server" />
                    <asp:HiddenField ID="hdnRotateValue" runat="server" />
                    <asp:HiddenField ID="ZinSize" runat="server" />

                    <%--  <span class="btn btn-primary zoom-in" onclick="return  zoomIn();">Zoom In</span>
                                <span class="btn btn-primary zoom-out" onclick="return  zoomOut();">Zoom Out</span>--%>
                    <span class="btn-animated bg-green" onclick="return  rotateImage();">Rotate</span>
                </div>

                <div class="img-op">
                    <asp:LinkButton ID="btnSaveImage" runat="server" Text="Save Image" CssClass="btn-animated bg-green" OnClick="SaveButn">
                    </asp:LinkButton>
                </div>


                <%--  <asp:LinkButton ID="btnApp" runat="server" Text="Approve Image" CssClass="btn btn-success"    OnClick="App" >
                              </asp:LinkButton>--%>
            </div>
            <div class="modal-footer" style="text-align: center;">

                <asp:UpdatePanel ID="updbuttons" runat="server">
                    <ContentTemplate>
                        <asp:LinkButton runat="server" class="btn-animated bg-green" ID="btnCroppreview" OnClick="btnCroppreview_click" Text="Crop Image">Crop Image</asp:LinkButton>
                        <%--onclick="return funcopencrop1();"--%> <%-- OnClick="btnCroppreview_click" --%>
                        <%-- <button class="btn-animated bg-green" id="btnCrop"  onclick="return funcopencrop2();">Crop Image</button>--%>
                        <%--  <button class="btn btn-warning"  onclick="return RaiseCFR();">CFR Raise</button>--%>
                        <button type="button" class="btn-animated bg-horrible" onclick="return Cancel(myModalImage);" text="Cancel">Cancel</button>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

    </div>

    <!-- Display Modal popup window in division -->
    <div class="modal" id="myModal" role="dialog" width="45%">
        <div class="modal-dialog modal-sm">

            <!-- Modal content-->
            <div class="modal-content" style="width:100% !important">
                <div class="modal-header" style="text-align: center; background-color: #dff0d8;">
                    <asp:Label ID="Label3" Text="INFORMATION" runat="server" Font-Bold="true"></asp:Label>

                </div>
                <div class="modal-body" style="text-align: center">
                    <asp:Label ID="lbl" runat="server"></asp:Label>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <button type="button" class="btn btn-primary" data-dismiss="modal" style='margin-top: -6px;'>
                        <span class="glyphicon glyphicon-ok" style='color: White;'></span>OK

                    </button>

                </div>
            </div>

        </div>
    </div>
    <!-- End Display Modal popup window in division -->
    <div class="modal" id="myModalCrop" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="padding-top: 10px;">
        <div class="modal-dialog" style="padding-top: 0px; width: 100%;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="H1">Crop Image</h4>
                </div>
                <div class="modal-body">
                    <iframe id="iframe1" src="" width="675" height="300" frameborder="0" allowtransparency="true"></iframe>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn-animated bg-horrible" id="lnkModalCrop" onclick="return Cancel(myModalCrop);">
                        Cancel</button>

                    <%--  <asp:LinkButton ID="lnkModalCrop"  
                                runat="server" 
                                CssClass="btn btn-danger" onclick="Cancel">
                                    <span class="glyphicon glyphicon-remove" style="color:White"> </span> Cancel
                                      </asp:LinkButton>--%>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>


    <asp:HiddenField ID="hdnUserId" runat="server" />
    <asp:HiddenField ID="hdnRegRefNo" runat="server" />
    <asp:HiddenField ID="hdnImgId" runat="server" />
    <asp:HiddenField ID="hdnHt" runat="server" />
    <asp:HiddenField ID="hdnWt" runat="server" />
    <asp:HiddenField ID="hdndoccode" runat="server" />
</asp:Content>
