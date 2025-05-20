<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="CKYCQC.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.CKYC.CKYCQC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/bundles/CKYCValidationjs") %>
    <%: Styles.Render("~/bundles/CKYCcss") %>



    <style>
        .container {
            width: 1300px !important;
        }

        .center {
            text-align: center !important;
        }

        .left2 {
            text-align: left !important;
        }
        /*Added by Rutuja on15sep2021*/

        .standardcheckbox {
            padding-right: 9px !important;
        }

        .chkClass label {
            margin-left: 3px !important;
        }

        input[type=checkbox], input[type=radio] {
            margin-right: 7px !important;
        }

            input[type=checkbox] + label, input[type=radio] + label {
                vertical-align: middle !important;
            }
    </style>
    <style>
        .gridview th {
            padding: 3px;
            height: 40px;
            background-color: #d6d6c2;
            color: #337ab7;
            text-align: center;
        }

        .left_padding {
            margin-left: 35%;
        }

        /*.loader {
            /*position: absolute;
            padding-left: 650px;
            background-color: white;
            background-position: center;
            display: block;
            width: 105%;
            height: 105%;*/
        /*      display: block;
            visibility: visible;
            position: fixed;
            float: inherit;
            z-index: 999;
            bottom: 100px;
            left: 0px;
            width: 105%;
            height: 105%;
            background-color: white;
            vertical-align: middle;
            padding-top: 20%;
            filter: alpha(opacity=75);
            opacity: 0.75;
            font-size: 16px;
            color: blue;
            font-style: normal;
            font-weight: bold !important;
            /*background-image: url("../../assets/images/dashboard-icon/Loader.gif");*/
        /*     background-repeat: no-repeat;
            background-attachment: fixed;
            background-position: center;
        }*/
        .loader {
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

            .loader::after {
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
                /*-webkit-animation: spin .8s linear infinite;
    animation: spin .8s linear infinite;*/
            }
    </style>
    <script type="text/javascript">

        function ValidationMsg(id) {
            debugger;
            if ($('#EmptyPagePlaceholder_' + id)[0].files.length == 0) {
                AlertMsg('Please Select the Document');
                return true;
            }
        }
        function rotateImage() {
            debugger;
            var options;

            var box = $('#EmptyPagePlaceholder_img3');
            counter += 90;
            $('#EmptyPagePlaceholder_hdnRotateValue').val(counter);
            $('#EmptyPagePlaceholder_img3').css('transform', 'rotate(' + counter + 'deg)')
        }

        function OpenRelatedPersonPageView(RelRefnNo, refno, FIRefNo, FlagPageTyp, Row) {
            debugger;
            var modal = document.getElementById('myModalRaise1');
            var modaliframe = document.getElementById("iframeCFR1");
            modaliframe.src = "CKYCRelatedPrsn.aspx?Status=QC&refno=" + refno + "&relrefno=" + RelRefnNo + "&FIRefNo=" + FIRefNo + "&FlagPageTyp=" + FlagPageTyp + "&RowNo=" + Row;
            modal.style.display = "block"
            $('#myModalRaise1').modal('show');
        }
        function MstShowHide(Id, Action) {

            document.getElementById(Id).style.display = Action;
        }
        function AlertMsg(msg) {
            debugger;
            var varFooter = '<center><button type="button" class="btn btn-success" data-dismiss="modal" style="margin-top: -6px;"><span class="glyphicon glyphicon-ok" style="color: White;"></span> OK </button></center>';
            showModalAlert('#Alert', 'Alert', 'alert-warning', varFooter, '', msg, 'Alert');
        }

        function MstDivAction(DivId, Action) {
            // var Div = document.getElementById("EmptyPagePlaceholder_" + DivId);
            var Div = document.getElementById(DivId);
            if (Div != null) {
                Div.style.display = Action;
            }
        }

        function showHideDiv(divName, btnName) {
            try {
                debugger;
                var objnewdiv = document.getElementById("EmptyPagePlaceholder_" + divName);
                var objnewbtn = document.getElementById(btnName);
                if (objnewdiv.style.display == "block") {
                    objnewdiv.style.display = "none";
                    objnewbtn.className = 'glyphicon glyphicon-collapse-up';
                }
                else {
                    objnewdiv.style.display = "block";
                    objnewbtn.className = 'glyphicon glyphicon-collapse-down';
                }
            }
            catch (err) {

            }
        }

        function showHideDivWOCPH(divName, btnName) {
            try {
                debugger;
                var objnewdiv = document.getElementById(divName);
                var objnewbtn = document.getElementById(btnName);
                if (objnewdiv.style.display == "block") {
                    objnewdiv.style.display = "none";
                    objnewbtn.className = 'glyphicon glyphicon-collapse-up';
                }
                else {
                    objnewdiv.style.display = "block";
                    objnewbtn.className = 'glyphicon glyphicon-collapse-down';
                }
            }
            catch (err) {

            }
        }



        //$('body').append('<div style="" id="loadingDiv"><div class="loader">Loading...</div></div>');
        //$(window).on('load', function () {
        //    setTimeout(removeLoader, 2000); //wait for page load PLUS two seconds.
        //});
        //function removeLoader() {
        //    $("#dvProgressBar").fadeOut(500, function () {
        //        // fadeOut complete. Remove the loading div
        //        $("#dvProgressBar").remove(); //makes page more lightweight 
        //    });
        //}

    </script>
    <script>
        // Get the modal
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

        function funcopencrop2() {
            debugger;
            var docId = document.getElementById('<%=hdnId.ClientID%>').value;//document.getElementById('<%=HiddenField1.ClientID%>').value
            var modal = document.getElementById('myModalCrop');
            var modaliframe = document.getElementById("iframe1");
            var cndno = document.getElementById('<%=hdnRegRefNo.ClientID%>').value;
            var userid = document.getElementById('<%=hdnUserId.ClientID%>').value;

           // modaliframe.src = "../../Application/CKYC/CropImage.aspx?TrnRequest=Preview&RefNo=" + cndno + "&args3=" + document.getElementById('<%=HiddenField1.ClientID%>').value + "&mdlpopup=MdlPopRaiseCrop";
            modaliframe.src = "../../Application/CKYC/CropImage.aspx?TrnRequest=Preview&RefNo=" + cndno + "&args3=" + docId + "&mdlpopup=MdlPopRaiseCrop";

            var span = document.getElementsByClassName("close")[0];
            var modal = document.getElementById('myModalCrop');
            modal.style.display = "block";
            //span.onclick = function () {
            //    debugger;
            //    modal.style.display = "none";

            //}

            $("#myModalCrop").modal();
        }


        function Confirm(row) {
            debugger;
            ShowProgressBarWithOutTimer('Please Wait... Loading Document...');
            var rowData = row.parentNode.parentNode;
            var rowIndex = rowData.rowIndex;
            var ri = parseInt(rowIndex);
            var grvUpld = document.getElementById('<%=gvDocDtls.ClientID%>');
            var ID = grvUpld.rows[ri].cells[0].children[0];
            var hdnid = ID.innerHTML;
            // document.getElementById('<%=HiddenField1.ClientID%>').value = hdnid;
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
            HideProgressBar();

        }
        function showpdf(base64String, ImgId, ImgCode, Height, Width, ZinSize1, ZoutSize1, MstWidth1, MstHeight1, Flag) {
            debugger;
            var modal = document.getElementById('myModalPDF');
            var pdfview = document.getElementById('pdfview');
            modal.style.display = "block";
            pdfview.src = "data:application/pdf;base64," + base64String;
            HideProgressBar();
        }
        function Cancel(modalimg) {
            debugger;
            var modal = modalimg;
            var span = document.getElementsByClassName("close")[0];
            modal.style.display = "none";
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">

    <asp:ScriptManager ID="SM1" runat="server">
    </asp:ScriptManager>
    <br />


    <center>
            <div class="container" style="margin-top: 0px; width: 100%;">
                  <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                    <div id="Div25" runat="server" class="panel-heading" >
                        <div class="row">
                            <div class="col-sm-10" style="text-align: left">
                                        <span class="glyphicon glyphicon-menu-hamburger" ></span>
                                <asp:Label ID="lblOfcuseOnly" Text="For Office use only" runat="server" CssClass="control-label"></asp:Label>
                            </div>
                             <div class="col-sm-2" >
                        <span id="btnToggle" class="glyphicon glyphicon-collapse-down" onclick="showHideDiv('divCKYCdtls','btnToggle');return false;" style="cursor:pointer;float:right; padding: 1px 10px ! important; font-size: 18px;"></span>
                    </div>
                        </div>
                    </div>
                    <div id="divCKYCdtls" style="display: block;" runat="server" class="panel-body">
                     <div class="row">
                             <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblRefNumber" Text="CKYC Reference No." runat="server" Font-Bold="false"></asp:Label>
                                 <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:TextBox ID="txtRefNumber" runat="server" CssClass="form-control" Enabled="false"
                            Font-Bold="false" TabIndex="2">
                        </asp:TextBox>
                            </div>
                              <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblKYCNumber" Text="FI Reference No." runat="server" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-sm-3" style="text-align: left; margin-bottom:5px;">
                                    <asp:TextBox ID="txtKYCNumber" runat="server" CssClass="form-control" Enabled="false"
                            Font-Bold="false" TabIndex="2">
                        </asp:TextBox>
                            </div>
      </div>
                <div class="row" style="margin-bottom: 5px">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblNatureOfBuss" Text="Constitution Type" runat="server" Font-Bold="false" Visible="false">
                                </asp:Label>
                        <span id="lblNatureOfBussImp" runat="server" style="color: red">*</span>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:DropDownList ID="ddlNatureOfBuss" AutoPostBack="true" Visible="false" OnSelectedIndexChanged="ddlNatureOfBuss_SelectedIndexChanged" runat="server" CssClass="form-control" TabIndex="2">
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="Label5" Text="Constitution Type others" Visible="false" runat="server" Font-Bold="false">
                                </asp:Label>
                        <span id="Label5Imp" runat="server" style="color: red">*</span>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:TextBox ID="txtConstitutionTypeothers" runat="server" Visible="false" MaxLength="200" CssClass="form-control" Font-Bold="false"
                            TabIndex="2" />
                    </div>
                </div>
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblAppType"  runat="server" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:CheckBox ID="cbNew" runat="server" CssClass="standardcheckbox" Text="New" AutoPostBack="true" TabIndex="3"
                                    Enabled="false" />
                                    &nbsp;
                                <asp:Label ID="cbNewtxt" runat="server" Text="New" CssClass="standardcheckbox" Style="padding-left: 3%;" Visible="false"></asp:Label>

                                <asp:CheckBox ID="cbUpdate" runat="server" CssClass="standardcheckbox" Text="Update" TabIndex="3"
                                    AutoPostBack="true" Enabled="false" />
                            </div>
                             <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblAccountType"  runat="server" Font-Bold="false"></asp:Label>
                                        <span id="lblAccountTypeImp" runat="server" style="color: red">*</span>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <%--<asp:CheckBox ID="chkNormal" runat="server" CssClass="standardcheckbox" Text="Normal"
                                    AutoPostBack="true"  TabIndex="4" />
                                    &nbsp;
                                <asp:CheckBox ID="chkSimplified" runat="server" CssClass="standardcheckbox" Text="Simplified"
                                    AutoPostBack="true"  TabIndex="5" />
                                    &nbsp;
                                <asp:CheckBox ID="Chksmall" runat="server" CssClass="standardcheckbox" Text="Small"
                                    AutoPostBack="true"  TabIndex="6" />--%>
                                  <%--Added by tushar for--%>
                                <asp:DropDownList ID="ddlAccountType" runat="server" CssClass="form-control" TabIndex="2" ClientIDMode="Static">
                                </asp:DropDownList>
                                <%--Added by tushar for--%>
                            </div>
                        </div>
                      
                    </div>
                </div>

         <div id="divImg" runat="server" class="panel panel-success" style="margin-left:0px;margin-right:0px">
          <div id="Div19"  runat="server" class="panel-heading" >           
        <div class="row">
        <div class="col-sm-10" style="text-align:left">
        <span class="glyphicon glyphicon-menu-hamburger" ></span>
        <asp:Label ID="lbluploadDoc" Text="UPLOADED DOCUMENTS" runat="server"  CssClass="control-label" ></asp:Label>
                 
        </div>
        <div class="col-sm-2" >
        <span id="btnnavigate" class="glyphicon glyphicon-collapse-down" onclick="showHideDiv('divnavigate','btnnavigate');return false;" style="cursor:pointer;float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
        </div>
        </div>
        </div>
            <div id="divnavigate" style="display:block;" runat="server" class="panel-body">
                <div class="row">
                  <div class="col-sm-12" style="text-align:right">
                      
                         <asp:GridView ID="gvDocDtls" runat="server" Width="100%" CssClass="footable"
                             AutoGenerateColumns="false" OnRowCommand="gvDocDtls_RowCommand">
                                    <%--<AlternatingRowStyle BackColor="White" />--%>
                                    <EditRowStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" ForeColor="White" />
                                    <HeaderStyle BackColor="#00c5cc" ForeColor="White" Height="35px" CssClass="gridViewHeader" />
                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#E3EAEB" />
                                    <Columns>
                                        
                                        <asp:TemplateField HeaderText="Document Code" HeaderStyle-CssClass="center" Visible="false">
                                            <ItemTemplate>
                                               <asp:Label ID="lbldocCode" runat="server"  Text='<%#Bind("DOC_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle  CssClass="center"    />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Document Name" HeaderStyle-CssClass="center" >
                                            <ItemTemplate>
                                               <asp:Label ID="lbldocName" runat="server" Text='<%#Bind("DOC_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                             <ItemStyle  CssClass="left2"    />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DocumentName" HeaderStyle-CssClass="center" Visible="false">
                                            <ItemTemplate>
                                               <asp:Label ID="lbldocTyp" runat="server" Text='<%#Bind("Image_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                             <ItemStyle  CssClass="left2"    />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="View Document" HeaderStyle-CssClass="center">
                                            <ItemTemplate >
                                                 <asp:LinkButton ID="btnView" runat="server" class="glyphicon glyphicon-eye-open" Style="color: #337ab7;"  OnClientClick="Confirm(this);"  OnClick="btnView_Click" CommandName="View"></asp:LinkButton>
                                                    <%--style="color:black;" OnClientClick="Confirm(this); ShowProgressBarWithOutTimer('Loading Document... Please Wait...');"  OnClick="btnView_Click" CommandName="View"></asp:LinkButton>--%>
                                            </ItemTemplate>
                                             <ItemStyle  CssClass="center"    />
                                        </asp:TemplateField>
                                    </Columns>
                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="false" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                                </asp:GridView>
                        <asp:UpdatePanel runat="server" ID="upnlPrev"  style="display:none;">
                            <ContentTemplate>
                               

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
                  <div class="col-sm-3" style="text-align: left" style="display:none">
                                                           
                                                            
                                                            <asp:Label ID="lblPageInfo" runat="server" Visible="false"></asp:Label>
                                                        </div>
                  <asp:UpdatePanel runat="server">
                      <ContentTemplate>
                          <div id="divloaderqc" class="col-sm-12" runat="server" style="display: none;">
                              <caption>
                                  <img id="img31" alt="" src="~/images/spinner.gif" runat="server" />
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
        
               <div class="row">
                 
             
                <asp:GridView ID="GridImage" runat="server" AllowSorting="True" CssClass="footable"
                                          width="1200px"              AutoGenerateColumns="False" PageSize="10" AllowPaging="true" CellPadding="1"
                                                    OnRowDataBound="GridImage_RowDataBound"> <%--OnPageIndexChanging="GridImage_PageIndexChanging"
                                        --%>
                                                          <RowStyle CssClass="GridViewRow" ></RowStyle>  
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
                                                <ControlStyle CssClass="left_padding" Width="30%" /> <%--Height="100%"--%>
                                            </asp:ImageField>
                                        </Columns> 
                                                    </asp:GridView>
                                                     <center>
                                                  

                                                        <table style="display:none;">
                                                            <tr>
                                                                <td>
                                                                    <asp:Button ID="btnprev" Text="<" CssClass="form-submit-button" runat="server"
                                                                        Width="40px" Enabled="false" Style="border-style: solid; border-width: 1px; background-repeat: no-repeat;
                                                                        background-color: transparent; float: left; margin: 0; height: 30px;" OnClick="btnprev_Click" OnClientClick="ShowProgressBar('Image Loading...');" />
                                                                  <%--  <asp:TextBox runat="server" ID="txtPage" Text="1" Style="width: 35px; border-style: solid;
                                                                        border-width: 1px; border-color: Gray; height: 30px; float: left; margin: 0;funload();
                                                                        text-align: center;" CssClass="form-control" ReadOnly="true" />--%>
                                                                    <asp:Button ID="btnnext" Text=">" CssClass="form-submit-button" runat="server" Enabled="false" Style="border-style: solid;
                                                                        border-width: 1px; background-repeat: no-repeat; background-color: transparent;
                                                                        float: left; margin: 0; height: 30px;" Width="40px" OnClick="btnnext_Click" OnClientClick="ShowProgressBar('Image Loading...');" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </center>

                                                    
                                                      </div>
               </ContentTemplate>
                  <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnnext" EventName="Click"></asp:AsyncPostBackTrigger>
                </Triggers>
             </asp:UpdatePanel>

              
           </div>
        </div>
        </div>
                               
        <%-- <div class="panel panel-success" style="margin-left:0px;margin-right:0px">--%>
             <%-- Below div18 commented by Rutuja--%>
<%--        <div id="Div18"  runat="server" class="panel-heading" >           
        <div class="row">
        <div class="col-sm-10" style="text-align:left">
        <span class="glyphicon glyphicon-menu-hamburger"></span>
         <asp:Label ID="lblpfPersonal1" Text="" runat="server" CssClass="control-label">
                                </asp:Label>
                 
        </div>
        <div class="col-sm-2" onclick="showHideDiv('menu1','btnpersnl');return false;">
        <span id="btnpersnl"class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
        </div>
        </div>
        </div>               --%>
       <%-- <div id="menu1" style="display:block;" runat="server" class="panel-body"> --%>
          <div class="panel panel-success" style="margin-left:0px;margin-right:0px">
             <div id="Div2"  runat="server" class="panel-heading">           
        <div class="row">
        <div class="col-sm-10" style="text-align:left">
        <span class="glyphicon glyphicon-menu-hamburger" style="color: white;"></span>
        <asp:Label ID="lblpfPersonal" Text="Personal Details" runat="server"  CssClass="control-label" ></asp:Label>
                 
        </div>
        <div class="col-sm-2">
        <span id="btnpersnl1" class="glyphicon glyphicon-collapse-down" onclick="showHideDiv('divDetailOfEntity','btnpersnl1');return false;" style="cursor:pointer;float: right;padding: 1px 10px ! important; font-size: 18px;"></span>
        </div>
        </div>
        </div>

            <div id="divPersonal" style="display:block;" runat="server" class="panel-body">
                                
           <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                    </div>
                                    <div class="col-sm-9" style="padding-left:0">
                                        <div class="col-sm-2" style="padding-left:3%">
                                            <asp:Label ID="Label7" Text="Prefix" runat="server" CssClass="control-label">
                                            </asp:Label>
                                        </div>
                                        <div class="col-sm-10" style="padding-left:0">
                                            <div class="col-sm-4">
                                                <asp:Label ID="Label8"
                                                    Text="First Name" runat="server" CssClass="control-label">
                                                </asp:Label>
                                            </div>
                                            <div class="col-sm-4" style="padding-left:3%">
                                                <asp:Label ID="lblmiddle" Text="Middle Name" runat="server" CssClass="control-label">
                                                </asp:Label>
                                            </div>
                                            <div class="col-sm-4" style="padding-left:4%">
                                                <asp:Label ID="Label11" Text="Last Name" runat="server" CssClass="control-label">
                                                </asp:Label>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblName" Text="" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-9" style="padding:0">
                                        <div class="col-sm-2">
                                            <asp:UpdatePanel ID="upcboTitle" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="cboTitle" runat="server" CssClass="form-control" DataTextField="ParamDesc"
                                                        DataValueField="ParamValue" AppendDataBoundItems="True" TabIndex="6">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-sm-10" style="padding:0">
                                            <div class="col-sm-4" style="padding-left:0">
                                                <asp:TextBox ID="txtGivenName" runat="server" CssClass="form-control" onkeypress="funIsAlphaNumericWithSpace();" onchange="javascript:this.value=this.value.toUpperCase();"
                                                    MaxLength="50" TabIndex="7" placeholder="First Name">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-sm-4" style="padding-left:0">
                                                <asp:TextBox ID="txtMiddleName" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="funIsAlphaNumericWithSpace();"
                                                    MaxLength="50" TabIndex="8" onblur="CheckSpaces();return false;" placeholder="Middle Name">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-sm-4" style="padding-left:0">
                                                <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="funIsAlphaNumericWithSpace();"
                                                    MaxLength="50" TabIndex="9" onblur="CheckSpaces();return false;" placeholder="Last Name">
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblMaidenName" Text="" CssClass="control-label" runat="server">
                                        </asp:Label>
                                    </div>
                                    <div class="col-sm-9" style="padding:0">
                                        <div class="col-sm-2">
                                            <asp:UpdatePanel ID="ipcboTitle1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="cboTitle1" runat="server" CssClass="form-control" DataTextField="ParamDesc"
                                                        DataValueField="ParamValue" AppendDataBoundItems="True"  TabIndex="10">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-sm-10" style="padding:0">
                                            <div class="col-sm-4" style="padding-left:0">
                                                <asp:TextBox ID="txtGivenName1" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="funIsAlphaNumericWithSpace();"
                                                    MaxLength="50" TabIndex="11" onblur="CheckSpaces();return false;" placeholder="First Name">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-sm-4" style="padding-left:0">
                                                <asp:TextBox ID="txtMiddleName1" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="funIsAlphaNumericWithSpace();"
                                                    MaxLength="50" TabIndex="12" onblur="CheckSpaces();return false;" placeholder="Middle Name">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-sm-4" style="padding-left:0">
                                                <asp:TextBox ID="txtLastName1" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="funIsAlphaNumericWithSpace();"
                                                    MaxLength="50" TabIndex="13" onblur="CheckSpaces();return false;" placeholder="Last Name">
                                                </asp:TextBox>
                                            </div>
                                            <asp:HiddenField ID="hdnGenderTitle1" runat="server"></asp:HiddenField>
                                            <asp:HiddenField ID="hdnGenderTitle2" runat="server"></asp:HiddenField>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                        <div class="col-sm-3">
                                            <div class="col-sm-6" style="padding:0;     text-align: left;">
                                            <asp:Label ID="lblFatherName" Text="" CssClass="control-label"
                                                runat="server"></asp:Label>
                                            <span style="color: red">*</span>
                                                </div>
                                            <div class="col-sm-6" style="padding:0">
                                            <asp:UpdatePanel ID="UpdFSFlag" runat="server">
                                                <ContentTemplate>
                                                    <asp:RadioButtonList ID="rbtFS" runat="server" CssClass="radiobtn" RepeatDirection="Horizontal"
                                                        Visible="true" TabIndex="14" AutoPostBack="true">
                                                        <asp:ListItem Value="F">Father</asp:ListItem>
                                                        <asp:ListItem Value="S">Spouse</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div></div>
                                        <div class="col-sm-9" style="padding:0">
                                            <div class="col-sm-2">
                                                <asp:UpdatePanel ID="upcboTitle2" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="cboTitle2" runat="server" CssClass="form-control" DataTextField="ParamDesc"
                                                            DataValueField="ParamValue" AppendDataBoundItems="True" TabIndex="15">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-sm-10" style="padding:0">
                                                <div class="col-sm-4" style="padding-left:0">
                                                    <asp:TextBox ID="txtGivenName2" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="funIsAlphaNumericWithSpace();"
                                                        MaxLength="50" TabIndex="16" onblur="CheckSpaces();return false;" placeholder="First Name">
                                                    </asp:TextBox>
                                                </div>
                                                <div class="col-sm-4" style="padding-left:0">
                                                    <asp:TextBox ID="txtMiddleName2" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="funIsAlphaNumericWithSpace();"
                                                        MaxLength="50" TabIndex="17" onblur="CheckSpaces();return false;" placeholder="Middle Name">
                                                    </asp:TextBox>
                                                </div>
                                                <div class="col-sm-4" style="padding-left:0">
                                                    <asp:TextBox ID="txtLastName2" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="funIsAlphaNumericWithSpace();"
                                                        MaxLength="50" TabIndex="18" onblur="CheckSpaces();return false;" placeholder="Last Name">
                                                    </asp:TextBox>
                                                </div>

                                                <%--  <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConn %>">
                                                         </asp:SqlDataSource>--%>
                                                <asp:HiddenField ID="HiddenField1" runat="server"></asp:HiddenField>
                                                <asp:HiddenField ID="HiddenField2" runat="server"></asp:HiddenField>
                                            </div>
                                        </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblMotherName" Text="" CssClass="control-label" runat="server">
                                        </asp:Label>
                                        <%--<span style="color: red">*</span>--%>
                                    </div>
                                    <div class="col-sm-9" style="padding:0">
                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="cboTitle3" runat="server" CssClass="form-control" DataTextField="ParamDesc"
                                                DataValueField="ParamValue" AppendDataBoundItems="True" TabIndex="19">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-10" style="padding:0">
                                            <div class="col-sm-4" style="padding-left:0">
                                                <asp:TextBox ID="txtGivenName3" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="funIsAlphaNumericWithSpace();"
                                                    MaxLength="50" TabIndex="20" onblur="CheckSpaces();return false;" placeholder="First Name">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-sm-4" style="padding-left:0">
                                                <asp:TextBox ID="txtMiddleName3" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="funIsAlphaNumericWithSpace();"
                                                    MaxLength="50" TabIndex="21" onblur="CheckSpaces();return false;" placeholder="Middle Name">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-sm-4" style="padding-left:0">
                                                <asp:TextBox ID="txtLastName3" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();" onkeypress="funIsAlphaNumericWithSpace();"
                                                    MaxLength="50" TabIndex="22" onblur="CheckSpaces();return false;" placeholder="Last Name">
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
                                        <asp:Label ID="lbldob" Text=" " runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control"  runat="server" ID="txtDOB" TabIndex="24"/>
                                    </div>

                                <div class="col-sm-3" style="text-align:left"> 
                                <asp:Label ID="lblGender" Text="Gender" runat="server" CssClass="control-label"></asp:Label>
                                    <span style="color: red">*</span>
                        </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList  id="cboGender" runat="server" CssClass="form-control"   TabIndex="25" ></asp:DropDownList>
                            </div>

                </div>

            <%--<div class="row">
                                    <asp:UpdatePanel ID="upOccuSubType" runat="server">
                                        <ContentTemplate>
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblOccupation" Text="" runat="server" CssClass="control-label">
                                                </asp:Label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlOccupation" AutoPostBack="true" runat="server" CssClass="form-control"
                                                    OnSelectedIndexChanged="ddlOccupation_SelectedIndexChanged" TabIndex="25">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-3" style="text-align: left" id="divOccuSubType" runat="server">
                                                <asp:Label ID="lblOccuSubType" Text="" runat="server" CssClass="control-label">
                                                </asp:Label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlOccuSubType" runat="server" CssClass="form-control" TabIndex="26">
                                                </asp:DropDownList>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>--%>
                                
                                <div class="row">
                                    <%--<div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblResStatus" Text="" runat="server" CssClass="control-label">
                                        </asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:UpdatePanel ID="upResStatus" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlResStatus" runat="server" CssClass="form-control" TabIndex="29">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>--%>
                                      <%--Added BY Pratik--%>
                                     <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="Label1" Text="PAN" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                    <asp:TextBox CssClass="form-control"  runat="server" ID="txtPANNo"/>
                                                 </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <%--Added BY Pratik--%>
                                    <%-- Added by Rutuja --%>
                                    <div class="col-sm-3"  style="text-align:left">
                                    <asp:CheckBox ID="chkPanForm" Text="Form 60 furnished" Enabled="false"
                                        AutoPostBack="true" runat="server" CssClass="standardcheckbox"  />
                                     <%--<span style="color: red">*</span>--%>
                                </div>
                                    <%-- ended by Rutuja --%>
                                   
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:UpdatePanel ID="uplblIsoCountryCodeOthr" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="lblIsoCountryCodeOthr" Text="" Visible="false"
                                                    runat="server" CssClass="control-label"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:UpdatePanel ID="upIsoCountryCodeOthr" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlIsoCountryCodeOthr" runat="server" CssClass="form-control"
                                                    AutoPostBack="true" TabIndex="30" Visible="false">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                    </div>
                    <%--Added By Shubham--%>
                    <div id="divDetailOfEntity" style="display:block;" runat="server" class="panel-body">
                        <div class="row" style="margin-bottom: 8px">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblKYCName" Text="Name" runat="server" Font-Bold="false"></asp:Label>
                                <span id="lblKYCNameImp" runat="server" style="color: red">*</span>
                            </div>
                            <div class="col-sm-3" style="text-align: left; top: 2px; left: 0px;">
                                <asp:TextBox ID="txtKYCName" runat="server" MaxLength="200" CssClass="form-control" Font-Bold="false"
                                    TabIndex="2">
                                </asp:TextBox>
                            </div>

                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblDatOfInc" Text="Date of Incorporation" runat="server" Font-Bold="false"></asp:Label>
                                <span id="lblDatOfIncImp" runat="server" style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <div class="input-group">
                                    <asp:TextBox ID="txtDatOfInc" runat="server" CssClass="form-control" AutoPostBack="true" MaxLength="10" TabIndex="2" Enabled="false"></asp:TextBox>
                                    <div class="input-group-btn">
                                        <div class="btn btn-primary btn-lg-kmi" onclick="callCalender1()" disabled="disabled">
                                            <span class="glyphicon glyphicon-calendar BtnGlyphicon"></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin-bottom: 5px">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblDateOfCom" Text="Date of Commencement of Business" runat="server" Font-Bold="false"></asp:Label>
                                <span id="lblDateOfComImp" style="display: none; color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <div class="input-group">
                                    <asp:TextBox ID="txtDtOfCom" runat="server" CssClass="form-control" MaxLength="10" TabIndex="2" Enabled="false"></asp:TextBox>
                                    <div class="input-group-btn">
                                        <div class="btn btn-primary btn-lg-kmi" onclick="checkDateOfCommencement()" disabled="disabled">
                                            <span class="glyphicon glyphicon-calendar BtnGlyphicon"></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblPlaceOfIncor" Text="Place of Incorporation" runat="server" Font-Bold="false"></asp:Label>
                                <span id="lblPlaceOfIncorImp" style="color: red">*</span>
                            </div>
                            <div class="col-sm-3" style="text-align: left; top: 2px; left: 0px; margin-top: -3px">
                                <asp:TextBox ID="txtPlaceOfInc" runat="server" CssClass="form-control" Enabled="false" Font-Bold="false" onkeypress="fncInputcharacterOnlyNew();"
                                    TabIndex="2">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="row" style="margin-bottom: 5px">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblCountrOfInc" Text="Country of Incorporation" runat="server" Font-Bold="false"></asp:Label>
                                <span id="lblCountrOfIncImp" style="color: red">*</span>
                            </div>
                            <div class="col-sm-3" style="text-align: left; top: 0px; left: 0px;">
                                <asp:DropDownList ID="ddlCountrOfInc" runat="server" CssClass="form-control" Enabled="false" Font-Bold="false"
                                    TabIndex="2">
                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row" style="margin-bottom: 5px">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblPanNo" Text="PAN " runat="server" Font-Bold="false"></asp:Label>
                                <span id="lblPanNoImp" runat="server" style="color: red">*</span>
                            </div>
                            <div class="col-sm-3" style="text-align: left; top: 0px; left: 0px;">
                                <asp:TextBox ID="txtPanNoLegal" runat="server" AutoPostBack="true" Enabled="false" MaxLength="10" CssClass="form-control"
                                    onChange="javascript:this.value=this.value.toUpperCase();" onblur="validatePAN(this)" Font-Bold="false"
                                    TabIndex="2">
                                </asp:TextBox>
                            </div>
                            <div class="col-sm-3"  style="text-align:left">
                                <asp:CheckBox ID="chkPanFormLegal" Text="FORM 60" Enabled="false"
                                    AutoPostBack="true" runat="server" CssClass="standardcheckbox" TabIndex="2" />
                            </div>
                        </div>
                        <div class="row" style="margin-bottom: 5px">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblTypeIdentiNo" Text="TIN/GST Registration number" runat="server" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-sm-3" style="text-align: left; top: 0px; left: 0px;">
                                <asp:TextBox ID="txtTypeIdentiNo" runat="server" MaxLength="11" CssClass="form-control" Font-Bold="false" Enabled="false" onblur="tinvalidation(this);"
                                    AutoPostBack="true" onkeypress="fncInputNumericValuesWithHyphenOnly();" TabIndex="2">
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
                    </div>
                    <%--Ended By Shubham--%>

           </div>
                <%--  Added for Personal Details end --%>
                         
        <%--  Added for Tick If Applicable start --%>
            <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px;display:none">
                            <div id="Div1" runat="server" class="panel-heading subheader" 
                                onclick=" showHideDiv('div3','Span1');return false;">
                                <div class="row">
                                    <div class="col-sm-10" style="text-align: left">
                                        <span class="glyphicon glyphicon-menu-hamburger" style="color: white;"></span>
                                        <asp:Label ID="lbltick" Text="" runat="server" CssClass="control-label">
                                        </asp:Label>&nbsp;&nbsp;
                                        <%-- <asp:CheckBox ID="chkTick" Text=" RESIDENCE FOR TAX PURPOSES IN JURISDICTIONS(S) OUTSIDE INDIA" OnCheckedChanged="chkTick_Checked"
                                            CssClass="standardcheckbox"  runat="server" />--%>
                                    </div>
                                    <div class="col-sm-2">
                                        <span id="Span1" class="glyphicon glyphicon-resize-full" style="cursor:pointer;float: right; color: white;
                                            padding: 1px 10px ! important; font-size: 18px;"></span>
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
                                            OnCheckedChanged="chkTick_Checked" CssClass="standardcheckbox" runat="server"
                                            TabIndex="31" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblIsoCountryCode2" Text=""
                                            runat="server" CssClass="control-label"></asp:Label>
                                        <%--<span><font color="red">*</font> </span>--%>
                                    </div>
                                    <div class="col-sm-3">
                                        <%-- <asp:textbox cssclass="form-control" onmousedown="$('#txtDob').datepicker({ changeMonth: true, changeYear: true });"
                                    onchange="setDateFormat('txtDob')" runat="server" id="txtIsoCountryCode2" maxlength="15"
                                    tabindex="12" enabled="true" />--%>
                                        <asp:DropDownList ID="ddlIsoCountryCode2" runat="server" CssClass="form-control"
                                            AutoPostBack="true" TabIndex="32">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblTaxIden" Text=""
                                            runat="server" CssClass="control-label"></asp:Label>
                                        <%--<span><font color="red">*</font> </span>--%>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" onmousedown="$('#txtDob').datepicker({ changeMonth: true, changeYear: true });"
                                           runat="server" ID="txtIDResTax" MaxLength="15"
                                            TabIndex="33" />

                                           
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblPlace" Text="" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <%--<span><font color="red">*</font> </span>--%>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" onmousedown="$('#txtDob').datepicker({ changeMonth: true, changeYear: true });"
                                           runat="server" ID="txtDOBRes" MaxLength="15"
                                            TabIndex="34" />
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblIsoContry" Text="" runat="server"
                                            CssClass="control-label"></asp:Label>
                                        <%--<span><font color="red">*</font> </span>--%>
                                    </div>
                                    <div class="col-sm-3">
                                        <%--<asp:textbox cssclass="form-control" onmousedown="$('#txtDob').datepicker({ changeMonth: true, changeYear: true });"
                                    onchange="setDateFormat('txtDob')" runat="server" id="txtIsoCountry" maxlength="15"
                                    tabindex="12" />--%>
                                        <asp:DropDownList ID="ddlIsoCountry" runat="server" CssClass="form-control" AutoPostBack="true"
                                            TabIndex="35">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
        <%--  Added for Tick If Applicable end --%>
            <%--</div>--%>
            <%--</div>--%>
                 <div id="divPOISection" runat="server" visible="false" class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                    <div id="Div4" runat="server" class="panel-heading">
                        <div class="row">
                            <div class="col-sm-10" style="text-align: left">
                                <span class="glyphicon glyphicon-menu-hamburger"></span>
                                <asp:Label ID="Label3" Text="PROOF OF IDENTITY" runat="server"
                                    CssClass="control-label"></asp:Label>
                            </div>
                            <div class="col-sm-2">                               
                                <span id="btnProofIdentity" class="glyphicon glyphicon-collapse-down" onclick="showHideDiv('divPOI','btnProofIdentity');return false;" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div>
                      <div id="divPOI" style="display: block;" runat="server" class="panel-body">
                          
                              <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="Label4" Text="Document Type" runat="server" CssClass="control-label"></asp:Label>
                                        <%--<span style="color:red;">*</span>--%>
                                    </div>
                                    <div class="col-sm-3">
                                    <asp:DropDownList CssClass="form-control" Enabled="false" runat="server"  ID="ddlProofIdentityLegal"  TabIndex="2" AutoPostBack="true" />

                </div>
                   </div>
                          <div class="row">
                                             <div  class="col-sm-3" style="text-align: left">
                                <asp:Label ID="Label9" Text="Document Number" runat="server" CssClass="control-label"></asp:Label>
                                <%--<span style="color:red;">*</span>--%>
                            </div>
                            <div  class="col-sm-3">
                                <asp:TextBox CssClass="form-control"  runat="server" TabIndex="2"
                                    ID="txtPOIVal" Enabled="false"  />
                          </div>
                              </div>
                            <div class="container" style="padding:0px 10px 10px 10px;">
                                <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="footable" AutoGenerateColumns="false">
                                    <%--<AlternatingRowStyle BackColor="White" />--%>
                                <EditRowStyle BackColor="#7C6F57" />
                                <FooterStyle BackColor="#1C5E55" ForeColor="White" />
                                <HeaderStyle BackColor="#00c5cc" ForeColor="White" Height="35px" CssClass="gridViewHeader" />
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#E3EAEB" />
                                    <Columns>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="ChkPOIDocumentTxt" runat="server" Visible="false"></asp:Label>
                                                <asp:CheckBox ID="ChkPOIDocument" runat="server" onclick="ToChkUnchkChkPOIDocument(this.id)" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Document ID" HeaderStyle-CssClass="center" Visible="false" ItemStyle-Width="8%" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocID" runat="server" Text='<%# Eval("DocID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad center" HorizontalAlign="Center" Width="8%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Document Type" HeaderStyle-CssClass="center" ItemStyle-Width="25%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocType" runat="server" Text='<%# Eval("DocType") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="25%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Document Number" HeaderStyle-CssClass="center" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocNumber" runat="server" Text='<%# Eval("DocNumber") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="15%" />
                                    </asp:TemplateField>
                                    </Columns>
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="false" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                <SortedAscendingHeaderStyle BackColor="#246B61" />
                                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                <SortedDescendingHeaderStyle BackColor="#15524A" />
                                </asp:GridView>
                                <%--Tushar multiple Doc--%>
                            </div>
                      </div>
                     </div>
         <div class="panel panel-success" style="margin-left:0px;margin-right:0px">
        <div id="Div20"  runat="server" class="panel-heading" >           
        <div class="row">
        <div class="col-sm-10" style="text-align:left">
        <span class="glyphicon glyphicon-menu-hamburger"></span>
        <asp:Label ID="Label10" Text="PROOF OF IDENTITY AND ADDRESS" runat="server"  CssClass="control-label" ></asp:Label>
                 
        </div>
        <div class="col-sm-2" >
        <span id="btnId" class="glyphicon glyphicon-collapse-down" onclick="showHideDiv('menu2','btnId');return false;" style="cursor:pointer; float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
        </div>
        </div>
        </div>               
       
        <div id="menu2" style="display:block;" runat="server" class="panel-body">
        <%--  Added for Proof of Identity start--%>
              
           <div class="row">
                            <div class="col-sm-12" style="text-align: left">
                                <asp:Label ID="Label12" Text="Registered Office Address / Place of Business" Visible="false" runat="server" CssClass="control-label" Style="font-weight: 700;"></asp:Label>
                                <span ID="Label12Imp" runat="server" style="color: red" Visible="false">*</span>
                            </div>
                        </div>
                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblProof" Text="Document Type" runat="server" CssClass="control-label"></asp:Label>
                                        <%--<span><font color="red">*</font> </span>--%>
                                    </div>
                                    <div class="col-sm-3">
                                    <asp:DropDownList CssClass="form-control" runat="server"  ID="ddlProofIdentity"  TabIndex="36"
                                     OnSelectedIndexChanged="ddlProofIdentity_SelectedIndexChanged" AutoPostBack="true" />

                </div>
                   <div class="col-sm-6" align="left">
         <asp:Button ID="lnkVrfy" runat="server" CssClass="btn-animated bg-green" Enabled="false"  Text="Verify">
                        </asp:Button>
       
        </div>
                   </div>
                <div id="divIdProof" runat="server" class="row">
                    <div style="padding:10px;">
                                <asp:GridView ID="GridView2" runat="server" Width="100%" CssClass="footable" AutoGenerateColumns="false">
                                    <%--<AlternatingRowStyle BackColor="White" />--%>
                                    <EditRowStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" ForeColor="White" />
                                    <HeaderStyle BackColor="#00c5cc" ForeColor="White" Height="35px" CssClass="gridViewHeader" />
                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#E3EAEB" />
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="5%" Visible="false" >
                                            <ItemTemplate >
                                                <asp:Label ID="ChkPOIDocumentTxt" runat="server" ></asp:Label>
                                                <asp:CheckBox ID="ChkPOIDocument" runat="server" Enabled="false" />
                                            </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="5%" />
                                        </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Document ID" Visible="false" HeaderStyle-CssClass="pad" ItemStyle-Width="8%" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocID" runat="server" Text='<%# Eval("DocID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="8%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Document Type" HeaderStyle-CssClass="pad" ItemStyle-Width="25%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocType" runat="server" Text='<%# Eval("DocType") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="25%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Document Number" HeaderStyle-CssClass="pad" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocNumber" runat="server" Text='<%# Eval("DocNumber") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="15%" />
                                    </asp:TemplateField>
                                    </Columns>
                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="false" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                                </asp:GridView>
                    </div>

                            <div id="divPassNo" runat="server" class="col-sm-3" style="display:none;text-align: left;">
                                <asp:Label ID="lblPassportNo" Placeholder="Document Number" runat="server" CssClass="control-label"></asp:Label>
                                <%--<span><font color="red">*</font> </span>--%>
                            </div>
                            <div id="divPassNotxt" runat="server" class="col-sm-3" style="display:none">
                                <asp:TextBox CssClass="form-control"  runat="server" TabIndex="37"
                                    ID="txtPassNo" MaxLength="35" />
                                        <%--<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" 
                                    runat="server" FilterType="Numbers,UppercaseLetters,LowercaseLetters" 
                                    TargetControlID="txtPassNo"  >
                                    </ajaxToolkit:FilteredTextBoxExtender>--%>
                            </div>
                            <div id="divPass" runat="server" class="col-sm-3" style="text-align: left">
                                <asp:Label ID="llPassExpDate" runat="server" CssClass="control-label"></asp:Label>
                                <%--<span><font color="red">*</font> </span>--%>
                            </div>
                            <div id="divPassDate" runat="server" class="col-sm-3">
                                <asp:TextBox CssClass="form-control"  runat="server" ID="txtPassExpDate" TabIndex="38"
                                  onmousedown="$('#txtPassExpDate').datepicker({ changeMonth: true, changeYear: true });"
                                                            onchange="setDateFormat('txtPassExpDate')"   />
                                <asp:TextBox CssClass="form-control" runat="server" Visible="false"
                                                            ID="txtPassOthr" MaxLength="15" TabIndex="39" />
                            </div>
                            </div>
                <div class="row" style="display: none">
                    <br/>
                        <div class="col-sm-12" style="overflow-x:scroll;">
                        <asp:GridView ID="gvIdProof"  CssClass="footable" 
                            AutoGenerateColumns="false" AutoGenerateDeleteButton="false" runat="server">
                            <RowStyle CssClass="GridViewRow"></RowStyle>
        <PagerStyle CssClass="disablepage" />
        <HeaderStyle CssClass="gridview th" />
                                               
                            <Columns>
                                <asp:TemplateField HeaderText="ID Proof Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdProofType" runat="server" Text='<%# Eval("ID Proof Type") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ID Proof Number">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdProofNum" runat="server" Text='<%# Eval("ID Proof Number") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ID Proof Submited">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdProofSub" runat="server" Text='<%# Eval("ID Proof Submited") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                              
                            </Columns>
                        </asp:GridView>
                        </div>
                    </div>
 
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblAddressLine1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font> </span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control"  runat="server"
                                            ID="txtAddressLine1" MaxLength="300" TabIndex="46" />
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblAddressLine2" Text="" runat="server" CssClass="control-label"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" 
                                            runat="server" ID="txtAddressLine2" MaxLength="300" TabIndex="47" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblAddressLine3" Text="" runat="server" CssClass="control-label"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtDrivLicNum')" runat="server"
                                            ID="txtAddressLine3" MaxLength="300" TabIndex="48" />
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblCity" Text="" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font> </span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" TabIndex="49">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblDistrict" Text="" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font> </span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" Enabled="false"
                                            TabIndex="50">
                                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblPinCode" Text="" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font> </span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlPinCode" runat="server" CssClass="form-control"
                                            TabIndex="51" Enabled="false"> <%--Enabled="false" added by rutuja --%>
                                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblState" Text="" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font> </span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" Enabled="false"
                                            TabIndex="52">
                                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblIsoCountryCode" Text="" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font> </span>
                                    </div>
                                    <div class="col-sm-3">
                                        <%--       <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtState')" runat="server"
                                                    ID="txtCountryCode" MaxLength="15"  TabIndex="12" Enabled="false" />--%>
                                        <asp:DropDownList ID="ddlCountryCode" runat="server" CssClass="form-control" AutoPostBack="true"
                                            TabIndex="53">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div style="margin-top: 25px; margin-bottom: 25px">
                                    <div class="row">
                                        <div class="col-sm-12" style="text-align: left">
                                            <asp:CheckBox ID="chkLocalAddress" Text="CORRESPONDENCE/LOCAL ADDRESS DETAILS" runat="server"
                                                CssClass="control-label" TabIndex="54"  Style="display: none" />
                                            <span><font color="red" Style="display: none">*</font> </span>
                              <asp:Label ID="Label2" Text="CURRENT ADDRESS DETAILS" runat="server" CssClass="control-label" TabIndex="54" Style="font-weight: 700;"></asp:Label>
                                        </div>
                                    </div>
                                    
                                    <div class="row">
                                <div class="col-sm-12" style="text-align: left">
                                    <asp:Label ID="Label13" Text="Local Address In India" runat="server" Visible="false" CssClass="control-label" Style="font-weight: 700;"></asp:Label>
                                    <span ID="Label13Imp" runat="server" Visible="false" style="color: red">*</span>
                                </div>
                            </div>
                                    <div class="row">
                                        <div class="col-sm-12" style="text-align: left">
                                            <asp:CheckBox ID="chkCuurentAddress" Text="Same as above mentioned address"
                                                OnCheckedChanged="chkCuurentAddress_Checked" AutoPostBack="true" runat="server"
                                                CssClass="control-label" TabIndex="55" />
                                            <span><font color="red">*</font> </span>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3" style="text-align: left">
                                            <asp:Label ID="lblLocAddLine1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                            <span><font color="red">*</font> </span>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtLocAddLine1')" runat="server"
                                                ID="txtLocAddLine1" MaxLength="300" TabIndex="56" />
                                        </div>
                                        <div class="col-sm-3" style="text-align: left">
                                            <asp:Label ID="lblLocAddLine2" Text="" runat="server" CssClass="control-label"></asp:Label>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtLocAddLine2')" runat="server"
                                                ID="txtLocAddLine2" MaxLength="300" TabIndex="57" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3" style="text-align: left">
                                            <asp:Label ID="lblLocAddLine3" Text="" runat="server" CssClass="control-label"></asp:Label>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtDrivLicNum')" runat="server"
                                                ID="txtLocAddLine3" MaxLength="300" TabIndex="58" />
                                        </div>
                                        <div class="col-sm-3" style="text-align: left">
                                            <asp:Label ID="lblCity1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                            <span><font color="red">*</font> </span>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtCity1" runat="server" CssClass="form-control" TabIndex="59"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3" style="text-align: left">
                                            <asp:Label ID="lblDistrict1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                            <span><font color="red">*</font> </span>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlDistrict1" runat="server" CssClass="form-control" Enabled="false"
                                                TabIndex="60">
                                                <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3" style="text-align: left">
                                            <asp:Label ID="lblPin1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                            <span><font color="red">*</font> </span>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlPinCode1" runat="server" CssClass="form-control" 
                                                 TabIndex="61" Enabled="false"> <%--Enabled="false" added by rutuja --%>
                                                <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3" style="text-align: left">
                                            <asp:Label ID="lblState1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                            <span><font color="red">*</font> </span>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlState1" runat="server" CssClass="form-control" Enabled="false"
                                                TabIndex="62">
                                                <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3" style="text-align: left">
                                            <asp:Label ID="lblCountryCode1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                            <span><font color="red">*</font> </span>
                                        </div>
                                        <div class="col-sm-3">
                                            <%--  <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtState')" runat="server"
                                                    ID="txtCountryCode1" MaxLength="15" TabIndex="12"  Enabled="false"/>--%>
                                            <asp:DropDownList ID="ddlCountryCode1" runat="server" CssClass="form-control" AutoPostBack="true"
                                                TabIndex="63">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <%--<div class="row">
                                    <div class="col-sm-12" style="text-align: left">
                                        <asp:CheckBox ID="chkAddResident" Text="ADDRESS IN THE JURISDICTION DETAILS WHERE APPLICANT IS RESIDENT OUTSIDE INDIA FOR TAX PURPOSES"
                                            runat="server" CssClass="control-label" TabIndex="64" />
                                        <span><font color="red">*</font> </span>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6" style="text-align: left">
                                        <asp:CheckBox ID="chkCurrentAdd" Text="Same as Current/Permanent/Overseas Address details"
                                            TabIndex="65" OnCheckedChanged="chkCurrentAdd_Checked" AutoPostBack="true" runat="server"
                                            CssClass="control-label" />
                                        <span><font color="red">*</font> </span>
                                    </div>
                                    <div class="col-sm-6" style="text-align: left">
                                        <asp:CheckBox ID="chkCorresAdd" Text="Same as Correspondance/Local Address details"
                                            TabIndex="66" OnCheckedChanged="chkCorresAdd_Checked" AutoPostBack="true" runat="server"
                                            CssClass="control-label" />
                                        <span><font color="red">*</font> </span>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblAddLine1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font> </span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtAddLine1')" runat="server"
                                            ID="txtAddLine1" MaxLength="300" TabIndex="67" />
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblAddLine2" Text="" runat="server" CssClass="control-label"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtAddLine2')" runat="server"
                                            ID="txtAddLine2" MaxLength="300" TabIndex="68" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblAddLine3" Text="" runat="server" CssClass="control-label"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtAddLine3')" runat="server"
                                            ID="txtAddLine3" MaxLength="300" TabIndex="69" />
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblCity2" Text="" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font> </span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtCity2" runat="server" CssClass="form-control" TabIndex="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblDistrict2" Text="" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font> </span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlDistrict2" runat="server" CssClass="form-control" AutoPostBack="true"
                                            Enabled="false" TabIndex="71">
                                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblPin2" Text="" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font> </span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlPinCode2" runat="server" CssClass="form-control" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlPinCode2_SelectedIndexChanged" TabIndex="72">
                                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblState2" Text="" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font> </span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlState2" runat="server" CssClass="form-control" TabIndex="73"
                                            Enabled="false">
                                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblIsoCountry2" Text="" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font> </span>
                                    </div>
                                    <div class="col-sm-3">
                                        <%-- <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtIsoCountryCode')"
                                        <asp:DropDownList ID="ddlIsoCountryCode" runat="server" CssClass="form-control" AutoPostBack="true"
                                            TabIndex="74">
                                        </asp:DropDownList>
                                    </div>
                                </div>--%>
                            </div>
                        </div>
                              <%--</div>--%>
                        <%-- Added for Proof of Address end--%>
                        <%--  Added for Contact Details start--%>
                     <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                            <div id="Div8" runat="server" class="panel-heading" >
                                <div class="row">
                                    <div class="col-sm-10" style="text-align: left">
                                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                                       
                                        <asp:Label ID="lblContactDetails" Text="CONTACT DETAILS (All communication will be sent on provided MobileNo/Email-ID)" 
                                            runat="server" CssClass="control-label"></asp:Label> <%-- Text added by Rutuja --%>
                                    </div>
                                    <div class="col-sm-2" >
                                        <span id="btnTo" class="glyphicon glyphicon-collapse-down" onclick="showHideDivWOCPH('divContactDetails','btnTo');return false;"  style="cursor:pointer; float:right; padding: 1px 10px ! important; font-size: 18px;"></span>
                                    </div>
                                </div>
                            </div>
                            <div id="divContactDetails" style="display:block;" class="panel-body">
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblTelOff1" runat="server" Text="" CssClass="control-label"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="input-group"> 
                                            <span class="input-group-addon input-group-addon-tel" style="padding:0px 0px;">
                                                <asp:TextBox ID="txtTelOff" runat="server" CssClass="form-control" TabIndex="75" onkeypress="fncInputNumericValuesOnly();" MaxLength="4" Style="border-top-left-radius: 4px; border-bottom-left-radius: 4px; width: 50px;"></asp:TextBox>
                                            </span>
                                            <asp:TextBox ID="txtTelOff2" runat="server" CssClass="form-control" onkeypress="fncInputNumericValuesOnly();"
                                                MaxLength="10" TabIndex="76"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left;"> <%-- None by Rutuja--%>
                                        <asp:Label ID="lblFax" runat="server" Text="Fax No." Visible="false" CssClass="control-label"></asp:Label>
                                    </div>
                                    <div id="divFax" runat="server" style="display:none" class="col-sm-3">
                                    <div class="input-group">
                                        <span class="input-group-addon input-group-addon-tel" style="padding:0px 0px;">
                                            <asp:TextBox ID="txtFax1" runat="server" CssClass="form-control"  TabIndex="81" onkeypress="fncInputNumericValuesOnly();" MaxLength="4" Style="border-top-left-radius: 4px; border-bottom-left-radius: 4px; width: 50px;"></asp:TextBox>
                                            </span>
                                            <asp:TextBox ID="txtFax2" runat="server" CssClass="form-control"   MaxLength="10" TabIndex="82" onkeypress="fncInputNumericValuesOnly();"></asp:TextBox>
                                        </div> <%-- Visible false by Rutuja--%>
                                        </div>

                                </div>
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblMobile" runat="server" Text="" CssClass="control-label"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="input-group">
                                            <span class="input-group-addon input-group-addon-tel" style="padding:0px 0px;">
                                                <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" TabIndex="79" onkeypress="fncInputNumericValuesOnly();" MaxLength="3" Style="border-top-left-radius: 4px; border-bottom-left-radius: 4px;  width: 50px;"></asp:TextBox>
                                            </span>
                                            <asp:TextBox ID="txtMobile2" runat="server" CssClass="form-control" onkeypress="fncInputNumericValuesOnly();"
                                                MaxLength="10" TabIndex="80"></asp:TextBox>
                                        </div>
                                    </div>
                                
                                    <%--Added By Shubham--%>
                                    <div id="divMob2" runat="server" style="display:none">
                                    <div class="col-sm-3" style="text-align: left">
                                      <asp:Label ID="Label6" runat="server" Text="Mobile Number 2" CssClass="control-label"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                     <div class="input-group">
                                     <span class="input-group-addon " style="width: 20% !important; border-top-left-radius: 7% !important; padding: 0px 0px !important; border: 0px !important;  width: 50px;">
                                    <asp:TextBox ID="txtMobile1" Enabled="false" runat="server" CssClass="form-control" onkeypress="fncInputNumericValuesOnly();"
                                        MaxLength="3" Style="border-top-left-radius: 4px; border-bottom-left-radius: 4px;" TabIndex="2"></asp:TextBox>
                                     </span>
                                   <asp:TextBox ID="txtMobile3" Enabled="false" runat="server" CssClass="form-control" onblur="validateMobileNumber(this);" onkeypress="fncInputNumericValuesOnly();"
                                    TabIndex="2" MaxLength="20"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                                    <%--Ended By Shubham--%>
                                </div>
                                <div class="row" id ="divhide" runat="server" style="display:block"> 
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblTelRes" runat="server" CssClass="control-label" Text="Tel.(Res)"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="input-group">
                                            <span class="input-group-addon input-group-addon-tel" style="padding:0px 0px;">
                                                <asp:TextBox ID="txtTelRes" runat="server" CssClass="form-control" TabIndex="77" onkeypress="fncInputNumericValuesOnly();" MaxLength="4" Style="border-top-left-radius: 4px; border-bottom-left-radius: 4px;  width: 50px;"></asp:TextBox>
                                            </span>
                                            <asp:TextBox ID="txtTelRes2" runat="server" CssClass="form-control" MaxLength="10" onkeypress="fncInputNumericValuesOnly();"
                                                TabIndex="78"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left; display:none">

                                    </div>
                                    <div class="col-sm-3" style="text-align: left; display:none">
                                    
                                    </div>
                                 </div> 
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblpfemail" runat="server" Text="" CssClass="control-label"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtemail" runat="server" CssClass="form-control" MaxLength="100"
                                            TabIndex="83"></asp:TextBox>
                                    </div>
                                    <%--Added By Shubham--%>
                                    <div id="divEmail2" runat="server" style="display:none">
                                <div class="col-sm-3" style="text-align: left">
                                    Email ID 2
                           
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtemail2" runat="server" Enabled="false" CssClass="form-control" MaxLength="100" TabIndex="2" onblur="checkEmailN(this.id)"></asp:TextBox>
                                </div>
                            </div>
                                    <%--Ended By Shubham--%>
                                </div>
                                </div>
                                
                            </div>
                        
                   <%-- </ContentTemplate>
                </asp:UpdatePanel> --%>
                <%--  Added for Contact Details end--%>
            <%--</div>--%>
        <%--</div>--%>

         <div id="divRelPersonDtls" runat="server" class="panel panel-success" style="margin-left:0px;margin-right:0px" visible="false">
        <div id="Div22"  runat="server" class="panel-heading" >           
        <div class="row">
        <div class="col-sm-10" style="text-align:left">
        <span class="glyphicon glyphicon-menu-hamburger" ></span>
        <asp:Label ID="lblDtlOfRtltpr" Text="DETAILS OF RELATED PERSON" runat="server"  CssClass="control-label" ></asp:Label>
                 
        </div>
        <div class="col-sm-2" onclick="showHideDiv('menu4','btnRel');return false;">
        <span id="btnRel" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
        </div>
        </div>
        </div> 
        <div id="menu4" style="display:block;" runat="server" class="panel-body">

            <%--Added by tushar for Related person gridview--%>
            <div class="row">
                            <div id="div12" class="col-sm-12" style="text-align: center" runat="server">
                                <asp:Label ID="lblRelRecordShow" Style="text-align: center" Text="No Related Person Found" Visible="false" runat="server" />
                            </div>
                            <div style="padding:15px;">
                            <asp:GridView ID="gvMemDtls" Width="100%" runat="server" AllowSorting="True" CssClass="footable"
                                PageSize="10" AllowPaging="true" CellPadding="1"
                                AutoGenerateColumns="False" >
                                <EditRowStyle BackColor="#7C6F57" />
                                <FooterStyle BackColor="#1C5E55" ForeColor="White" />
                                <HeaderStyle BackColor="#00c5cc" ForeColor="White" Height="35px" CssClass="gridViewHeader" />
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#E3EAEB" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr. No." ItemStyle-Width="10%" SortExpression="Reference No." HeaderStyle-CssClass="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad center" HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Relative Reference No." ItemStyle-Width="20%" SortExpression="Reference No." HeaderStyle-CssClass="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("RelRefNo") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Relative Type" ItemStyle-Width="20%" SortExpression="Reference No." HeaderStyle-CssClass="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRelTypVal" runat="server" Text='<%# Eval("RelationTypetxt") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Related Person Name" ItemStyle-Width="20%" SortExpression="Candidate Name" HeaderStyle-CssClass="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNameVal" runat="server" Text='<%# Eval("FNameRel") + " " + Eval("LNameRel")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date Of Birth" ItemStyle-Width="20%" SortExpression="KYC No" HeaderStyle-CssClass="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMemDOBVal" runat="server" Text='<%# Eval("DOBRel") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gender" ItemStyle-Width="20%" SortExpression="KYC No" HeaderStyle-CssClass="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMemGenVal" runat="server" Text='<%# Eval("GenderReltxt") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
<%--                                    <asp:TemplateField HeaderText="Marital Status " ItemStyle-Width="20%" SortExpression="KYC No" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMemMrtVal" runat="server" Text='<%# Eval("RelMaritalStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Citizenship" ItemStyle-Width="20%" SortExpression="KYC No" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMemCizVal" runat="server" Text='<%# Eval("RelCitizenship") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Residential Status " ItemStyle-Width="20%" SortExpression="KYC No" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMemResiVal" runat="server" Text='<%# Eval("RelResistatus") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Occupation Type" ItemStyle-Width="20%" SortExpression="KYC No" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMemOccVal" runat="server" Text='<%# Eval("RelOccuType") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField ItemStyle-Width="15%" SortExpression="Request" HeaderText="Action"  HeaderStyle-CssClass="center">
                                        <ItemTemplate>
                                            <div style="width: 20%; white-space: nowrap;">
                                                <span class="glyphicon glyphicon-flag"></span>
                                              <asp:LinkButton ID="lnkView" runat="server" OnClick="lnkView_Click" ForeColor="Black" Text="View" CommandArgument='<%# Eval("RelRefNo") %>'></asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad center" HorizontalAlign="Left" Width="10%" />
                                    </asp:TemplateField>
                                </Columns>
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="false" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                <SortedAscendingHeaderStyle BackColor="#246B61" />
                                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                <SortedDescendingHeaderStyle BackColor="#15524A" />
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
            <%--Added by tushar for Related person gridview--%>
                </div>
         </div>

         <div class="panel panel-success" style="margin-left:0px;margin-right:0px">
        <div id="Div23"  runat="server" class="panel-heading" >           
        <div class="row">
        <div class="col-sm-10" style="text-align:left">
        <span class="glyphicon glyphicon-menu-hamburger" ></span>
       <asp:Label ID="lblRemarks" Text="" runat="server" CssClass="control-label">
                        </asp:Label>
                 
        </div>
        <div class="col-sm-2" onclick="showHideDiv('menu5','btnOthr');return false;">
        <span id="btnOthr" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
        </div>
        </div>
        </div> 
        <div id="menu5" style="display:block;" runat="server" class="panel-body">
            <div class="row">
                                    <div class="col-sm-12">
                                        <asp:TextBox CssClass="form-control"  runat="server" ID="txtRemarks" TextMode="MultiLine" TabIndex="88"/>
                                    </div>
                </div>
                <%--  Added for Details of Remarks end--%>
                </div>
                </div>

         <div class="panel panel-success" style="margin-left:0px;margin-right:0px">
        <div id="Div24"  runat="server" class="panel-heading" >           
        <div class="row">
        <div class="col-sm-10" style="text-align:left">
        <span class="glyphicon glyphicon-menu-hamburger" ></span>
        <asp:Label ID="lblattstn" Text="ATTESTATION" runat="server"  CssClass="control-label" ></asp:Label>
                 
        </div>
        <div class="col-sm-2"  >
        <span id="btnAtts" class="glyphicon glyphicon-collapse-down" onclick="showHideDiv('menu6','btnAtts');return false;" style="cursor:pointer;float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
        </div>
        </div>
        </div> 

        <div id="menu6" style="display:block;" runat="server" class="panel-body">
                <%--  Added for Applicant Declaration start--%>
                        <div class="panel panel-success" style="margin-left:0px;margin-right:0px">
                <div id="Div14"  runat="server" class="panel-heading subheader" onclick=" showHideDiv('this','btnApp');return false;"
                >           
        <div class="row">
        <div class="col-sm-10" style="text-align:left">
        <span class="glyphicon glyphicon-menu-hamburger" style="color: white;"></span>
        <asp:Label ID="lbldec" Text="APPLICANT DECLARATION" runat="server"  CssClass="control-label" ></asp:Label>
                 
        </div>
        <div class="col-sm-2">
        <span id="btnApp" class="glyphicon glyphicon-collapse-down" style="float: right;
        padding: 1px 10px ! important; font-size: 18px;"></span>
        </div>
        </div>
        </div>
        <div id="div15" style="display:block;" runat="server" class="panel-body">
            <div class="row">
                            <div class="col-sm-12" style="text-align: left; display: flex;">
                                <%--  <asp:label cssclass="control-label" text="I hereby declare that the details furnished above are true and correct to the best of my knowledge and belief and I undertake to inform you of any changes therein immediately.In case any of the above information is found to be false or untrue or misleading or misrepresenting. I am aware that I may be held liable for it."
                                    onchange="setDateFormat('txtRemarks')" runat="server" id="lblAppDeclare1" maxlength="15"
                                    tabindex="12" />--%>
                                <asp:CheckBox ID="chkAppDeclare1" Enabled="false" Text="I hereby declare that the details furnished above are true and correct to the best of my knowledge and belief and I undertake  to inform you of any changes "
                                    CssClass="control-label" AutoPostBack="true" runat="server" onchange="setDateFormat('txtRemarks')"
                                    TabIndex="97" />
                            </div>
                            <div class="col-sm-12" style="text-align: left; display: flex; font-weight: bold;">
                                <asp:Label CssClass="control-label" Text="there in immediately.In case any of the above information is found to be false or untrue or misleading or misrepresenting. I am aware that I may be held   liable for it."
                                    runat="server" ID="lblAppDeclare1" maxlength="15" /></div>
                            <br />
                            <br />
                            <br />
                            <div class="col-sm-12" style="text-align: left; display: flex;">
                                <%--   <asp:label cssclass="control-label" text="I hereby consent to receiving information from Central KYC Registry through SMS/Email on the above registered number/email address"
                                    onchange="setDateFormat('txtRemarks')" runat="server" id="Label7" maxlength="15"
                                    tabindex="12" />--%>
                                <asp:CheckBox ID="chkAppDeclare2" Enabled="false" Text="I/we hereby consent to receiving information from Central KYC Registry through SMS/Email on the above registered number/email address"
                                    CssClass="control-label" AutoPostBack="true" runat="server" onchange="setDateFormat('txtRemarks')"
                                    TabIndex="98" />
                            </div>
                            <br />
                            <br />
                            <br />
                            <br />
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblDate" Text=" " runat="server" CssClass="control-label"></asp:Label>
                                <span><font color="red">*</font> </span>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox CssClass="form-control" onmousedown="$('#txtDate').datepicker({ changeMonth: true, changeYear: true });"
                                  runat="server" ID="txtDate" MaxLength="15"
                                    TabIndex="99" />
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblPlace1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span><font color="red">*</font> </span>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtPlace')" runat="server"
                                    ID="txtPlace" MaxLength="15" TabIndex="100" />
                            </div>
                        </div>
        </div>
        </div>
                    <%--  Added for Applicant Declaration end--%>
                                      
                    <%--  Added for Attestation/For Office Use Only start--%>
                    <div class="panel panel-success" style="margin-left:0px;margin-right:0px">
                <div id="Div16"  runat="server" class="panel-heading subheader" onclick=" showHideDiv('div17','btnOffc');return false;"
                    >           
        <div class="row">
        <div class="col-sm-10" style="text-align:left">
        <span class="glyphicon glyphicon-menu-hamburger" style="color: white;"></span>
        <asp:Label ID="lblAttesOfc" Text="ATTESTATION/FOR OFFICE USE ONLY" runat="server"  CssClass="control-label" ></asp:Label>
                 
        </div>
        <div class="col-sm-2">
        <span id="btnOffc" class="glyphicon glyphicon-collapse-down" style="float: right;
        padding: 1px 10px ! important; font-size: 18px;"></span>
        </div>
        </div>
        </div>
        <div id="div17" style="display:block;" runat="server" class="panel-body">
           <%-- <div class="row">
            <div class="col-sm-3" style="text-align: left">
                            <asp:Label ID="lblDocRec" Text="Document Received" runat="server" Font-Bold="true"  CssClass="control-label" ></asp:Label>
                            <span><font color="red">*</font> </span>
                            </div>
            <div class="col-sm-3">
        <asp:CheckBox ID="chkCertifyCopy" Text="Certified Copies" CssClass="standardcheckbox"   TabIndex="91"
         enabled="false" autopostback="true" runat="server" />   
        </div>     
                </div>--%>

           <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblDocRec" Text="Document Received" runat="server" Font-Bold="true"
                                            CssClass="control-label"></asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <%-- <asp:CheckBox ID="chkCertifyCopy" Text="Certified Copies" CssClass="standardcheckbox"
                                            Enabled="false" AutoPostBack="true" runat="server" TabIndex="101" />--%>

                                        <asp:DropDownList ID="ddlDocReceived" runat="server" CssClass="form-control" TabIndex="2">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                 
                    <div class="row">
                                    <div class="col-sm-12" style="text-align:center">
                                        <asp:Label ID="lblKYCVerify" style='text-align:center' CssClass="control-label" Font-Bold="true" Text="KYC VERIFICATION CARRIED OUT BY"  runat="server"
                                        />
                                    </div>
                                 
                </div>
                <br />
                    <div class="row">
                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblDate3" Text="Date" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font> </span>
                                    </div>
                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control"   runat="server" ID="txtDateKYCver"  TabIndex="92"/>
                                    </div>
                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblEmpName" Text="Employee Name" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font> </span>
                                    </div>
                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtEmpName" TabIndex="93"  />
                                    </div>
                                    </div>
                    <div class="row">
                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblEmpCode" Text="Employee Code" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font> </span>
                                    </div>
                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtEmpCode" TabIndex="94"/>
                                    </div>
                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblEmpDesignation" Text="Employee Designation" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font> </span>
                                    </div>
                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtEmpDesignation"  TabIndex="95"/>
                                    </div>
                                    </div>
                    <div class="row">
                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblEmpBranch" Text="Employee Code" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font> </span>
                                    </div>
                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtEmpBranch"  TabIndex="96"/>
                                    </div>
                                    </div>

                <br />
                <div class="row">
                                    <div class="col-sm-12" style="text-align:center">
                                        <asp:Label ID="lblInsDtls" style='text-align:center' CssClass="control-label" Font-Bold="true" Text="Institution Details"  runat="server"
                                     />
                                    </div>
                                 
                </div>
                <br />
                    <div class="row">
                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblInsName" Text="Name" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font> </span>
                                    </div>
                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control"   runat="server" ID="txtInsName" TabIndex="97" />
                                    </div>
                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblInsCode" Text="Code" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font> </span>
                                    </div>
                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtInsCode"  TabIndex="98"/>
                                    </div>
                                    </div>

        </div>
        </div>
                    <%--  Added for Attestation/For Office Use Only  end--%>



            

                    </div>
                    </div>
                    <div class="panel panel-success" id="QCRejectMainDiv" runat="server" style="margin-left:0px;margin-right:0px; display:none">
                <div id="DivQCReject"  runat="server" class="panel-heading subheader" onclick=" showHideDiv('Divreject','btnQCReject');return false;">
        <div class="row">
        <div class="col-sm-10" style="text-align:left">
        <span class="glyphicon glyphicon-menu-hamburger" style="color: white;"></span>
        <asp:Label ID="LabelQCReject" Text="Reason For Reject" runat="server"  CssClass="control-label" ></asp:Label>
                 
        </div>
        <div class="col-sm-2">
        <span id="btnQCReject" class="glyphicon glyphicon-collapse-down" style="float: right;
        padding: 1px 10px ! important; font-size: 18px;"></span>
        </div>
        </div>
                </div>   
       <div id="Divreject" style="display:block;" runat="server" class="panel-body">
            <div class="row">
                                    <div class="col-sm-12">
                                        <asp:TextBox CssClass="form-control"  runat="server" ID="TxtQcRejectRemark" TextMode="MultiLine" TabIndex="89"/>
                                    </div>
                </div>
                <%--  Added for Details of Remarks end--%>
                </div>

                        </div>                         
       <div class="row" style="margin-top:12px;">
        <div class="col-sm-12" align="center" >
            
            <%--<asp:Label ID="lblMsg" Text="" runat="server" style="display:none;"> </asp:Label>--%>
           <%-- <div id="dvProgressBar" runat="server" style="display:none;" align="center" >
                  
                      <img id="Img1" alt="" src="../../assets/images/dashboard-icon/Loader.gif" runat="server" /> Loading...
            
               </div> --%>
            <%--<button class="btn-animated bg-green has-spinner" id="two" >Approve</button> --%>
           
        <asp:Button ID="btnUpdate" runat="server" CssClass="btn-animated bg-green"  OnClientClick="ShowProgressBar('Processing...');"  OnClick="btnUpdate_Click"  Text="Approve">
                        </asp:Button>
             <asp:Button ID="btnReject" runat="server" CssClass="btn-animated bg-horrible has-spinner" OnClientClick="ShowProgressBar('Processing...');"   OnClick="btnReject_Click" Text="Reject">
                        </asp:Button>
         
               
      
        </div>
        </div>
         
</div>
          
        </center>

    <center>
         <div id="DivFloat" clientidmode="Static" runat="server" visible="false">

           
        </div>
          <asp:HiddenField ID="hdnRegRefNo" runat="server" />     
          <input id="hdnUpdate" type="hidden" runat="server" />
          <div id="myModalImage" class="modal" role="dialog" style="padding: 10px 100px 0 100px;overflow: scroll;">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" onclick="return Cancel(myModalImage);">&times;</button>
                        <div class="modal-title">

                            <asp:HiddenField ID="HiddenField11" runat="server" />
                            <asp:HiddenField ID="HiddenField12" runat="server" />
                            <asp:Label ID="Label16" Text="Document Name:" CssClass="control-label" runat="server"></asp:Label>
                            <asp:Label ID="Label17" runat="server" Text="" CssClass="control-label"></asp:Label>
                        </div>
                    </div>
                    <div class="modal-body">
                        <div id="img-preview" style="width: 100% !important; height: 100% !important">

                            <asp:Image ID="img3" runat="server" class="image-box" Style="cursor: move;" />
                        </div>
                        <br />
                        <div class="img-op">

                            <asp:HiddenField ID="HiddenField13" runat="server" />
                            <asp:HiddenField ID="HiddenField14" runat="server" />
                            <asp:HiddenField ID="HiddenField15" runat="server" />
                            
                            <span class="btn-animated bg-green" onclick="return  rotateImage();">Rotate</span>
                        </div>

                        <div class="img-op">
                            
                            <div style="width:30%;">
                            <asp:FileUpload ID="FileUpload" CssClass="form-control" runat="server"></asp:FileUpload></div>
                             <asp:LinkButton ID="btnReupload" runat="server" Text="Re-Upload" CssClass="btn-animated bg-green" OnClientClick="ValidationMsg('FileUpload');"
                                  OnClick="btnReupload_Click">
                              </asp:LinkButton>
                             <asp:LinkButton ID="LinkButton1" runat="server" Text="Save Image" CssClass="btn-animated bg-green" OnClick="SaveButn">
                              </asp:LinkButton>
                        </div>
                    </div>
                    <div class="modal-footer" style="text-align: center;">

                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                  <asp:LinkButton runat="server" class="btn-animated bg-green" ID="btnCroppreview" OnClick="btnCroppreview_Click" Text="Crop Image">Crop Image</asp:LinkButton>
                                <button type="button" class="btn-animated bg-horrible" onclick="return Cancel(myModalImage);" text="Cancel">Cancel</button>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

            </div>

           <div id="myModalPDF" class="modal" role="dialog" style="padding-top: 20px;overflow: scroll;">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" onclick="return Cancel(myModalPDF);">&times;</button>
                        <div class="modal-title">
                            <asp:HiddenField ID="HiddenField6" runat="server" />
                            <asp:HiddenField ID="HiddenField7" runat="server" />
                            <asp:Label ID="Label14" Text="Document Name:" CssClass="control-label" runat="server"></asp:Label>
                            <asp:Label ID="Label15" runat="server" Text="" CssClass="control-label"></asp:Label>
                        </div>
                    </div>
                    <div class="modal-body">
                        <div id="img-PDFPrev" style="width: 100% !important; height: 100% !important">
                            <embed src="" type="application/pdf" id="pdfview" width="100%" height="600px" />
                        </div>
                        <br />
                        <div class="img-op">
                            <asp:HiddenField ID="HiddenField8" runat="server" />
                            <asp:HiddenField ID="HiddenField9" runat="server" />
                            <asp:HiddenField ID="HiddenField10" runat="server" />
                            <div style="width:30%;">
                            <asp:FileUpload ID="FileUpload1" CssClass="form-control" runat="server"></asp:FileUpload></div>
                             <asp:LinkButton ID="btnReupload1" runat="server" Text="Re-Upload" CssClass="btn-animated bg-green"
                                 OnClientClick="ValidationMsg('FileUpload1');" OnClick="btnReupload1_Click">
                              </asp:LinkButton>
                        </div>
                    </div>
                    <div class="modal-footer" style="text-align: center;">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <button type="button" class="btn-animated bg-horrible" onclick="return Cancel(myModalPDF);" text="Cancel">Cancel</button>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

            </div>

<!-- Display Modal popup window in division -->
       
         <!-- End Display Modal popup window in division -->

        <div id="myModalImage1" class="modal" style="padding-top:10px"  >
 
        <div class="modal-content">
            <div class="modal-header">
               <%-- <button type="button" class="close" data-dismiss="modal">
                    &times;</button>--%>
                <div class="modal-title">
                
                   <asp:HiddenField ID="hdnId" runat="server" />
                     <asp:HiddenField ID="HiddenField5" runat="server" />
                <asp:Label ID="lblDocType" Text="Document Name:" CssClass="control-label" runat="server"></asp:Label>
                <asp:Label ID="lblDocDesc"  runat="server" Text=""  CssClass="control-label"></asp:Label>
                   </div>
            </div>
            <div  class="modal-body" style="text-align: center;">
                        <div id="img-preview">
                          
                               <asp:Image id="img4" runat="server"   class="image-box" style="cursor: move;" /></div>
                               <br />
                        <div class="img-op">
                        
                          <asp:HiddenField ID="ZoutSize" runat="server" />
                            <asp:HiddenField ID="hdnRotateValue" runat="server" />
                           <asp:HiddenField ID="ZinSize" runat="server" />
                            
                         <%--  <span class="btn btn-primary zoom-in" onclick="return  zoomIn();">Zoom In</span>
                                <span class="btn btn-primary zoom-out" onclick="return  zoomOut();">Zoom Out</span>--%>
                                <span class="btn btn-primary rotate" onclick="return  rotateImage();">Rotate</span>
                   <%-- </div>
                   
                  <div class="img-op">
                  </div>--%>
            
                    <asp:LinkButton ID="btnSaveImage" runat="server" Text="Save Image" CssClass="btn btn-primary"    OnClick="SaveButn" >
                              </asp:LinkButton>
                               <%--  <asp:LinkButton ID="btnApp" runat="server" Text="Approve Image" CssClass="btn btn-success"    OnClick="App" >
                              </asp:LinkButton>--%>
                      </div>
                      </div>
                    
            <div class="modal-footer" style="text-align: center;">
             
                <asp:UpdatePanel ID="updbuttons" runat="server">
                    <ContentTemplate>
                      <button class="btn btn-primary" id="btnCrop"  onclick="return funcopencrop1();">Crop Image</button>
                    
                   <button class="btn btn-warning"  onclick="return RaiseCFR();">CFR Raise</button>
                    <button type="button" class="btn btn-danger"  onclick="return Cancel(myModalImage);">
                      <span class="glyphicon glyphicon-remove" style="color:White"> </span> Cancel</button>
              
                     </ContentTemplate>
                   </asp:UpdatePanel>
            </div>
       </div>
</div>
             <asp:HiddenField ID="hdnDocType" runat="server" />
             <asp:HiddenField ID="hdnDocAction" runat="server" />
             <asp:HiddenField ID="hdnIsReUpload" runat="server" />
             <asp:HiddenField ID="hdnImgId" runat="server" />
             <asp:HiddenField ID="hdnHt" runat="server" />
             <asp:HiddenField ID="hdnWt" runat="server" />
             <asp:HiddenField ID="hdndoccode" runat="server" />
            <asp:HiddenField ID="hdnUserId" runat="server" />


        <div class="modal" id="myModalRaise1" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
                aria-hidden="true" style="padding-top: 0px;">
                <div class="modal-dialog" style="padding-top: 0px;   padding: 8px 15px; margin-top: 2px; width: 100%;">
                    <div class="modal-content">
                        <div class="modal-header"  style="margin-top: -10px; margin-bottom: -6px; padding-bottom: 6px ! important;">
                            <button type="button" class="close" onclick=" MstShowHide('myModalRaise1', 'none');" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title" id="myModalLabel" style="text-align: left;">CKYC Related Person Details</h4>
                        </div>
                        <div class="modal-body">
                            <iframe id="iframeCFR1" src="" width="100%" height="300" frameborder="0" allowtransparency="true"></iframe>
                        </div>
                        <div class="modal-footer" style="padding: 0px">
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

          <div class="modal" id="myModalCrop" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="padding-top: 10px;">
                <div class="modal-dialog" style="padding-top: 0px; width: 100%;">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="MstDivAction('myModalCrop', 'none');">&times;</button>
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
            
               <asp:UpdatePanel ID="ldr" runat="server">
            <ContentTemplate>
                <center>
        <div id="dvProgressBar" runat="server" style="display:none;" align="center" class="loader">
                 <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /> <br /><br /><br /><br /><br />
                          <asp:Image id="Img1" src="../../Images/horizonal_loader.gif"   height="50px" alt="" runat="server" ImageAlign="Middle"/>
                         <br />  
           <asp:Label ID="lblMsg" Text="" runat="server" > </asp:Label>
            
               </div> 
                </center>
             </ContentTemplate>
        </asp:UpdatePanel>

         <script type="text/javascript" src="Common/Scripts/jquery.min.js"></script>
         <script type="text/javascript" src="../../Scripts/jquery.elevatezoom.js"></script>
         <script type="text/javascript" >
             function ShowProgressBar(Msg) {
                 debugger;
                 var Msg = Msg

                 document.getElementById('EmptyPagePlaceholder_dvProgressBar').style.display = "block";
                 document.getElementById('EmptyPagePlaceholder_lblMsg').innerHTML = Msg;

                 setTimeout(function () {
                     HideProgressBar();
                 }, 5000);
             }
             function ShowProgressBarWithOutTimer(Msg) {
                 debugger;
                 var Msg = Msg

                 document.getElementById('EmptyPagePlaceholder_dvProgressBar').style.display = "block";
                 document.getElementById('EmptyPagePlaceholder_lblMsg').innerHTML = Msg;
             }
             function HideProgressBar() {
                 debugger;

                 document.getElementById('EmptyPagePlaceholder_dvProgressBar').style.display = "none";

             }
             function popupAlert() {
                 $("#myModalPop").modal();
             }
             </script>


        <div class="container">
        <div class="modal fade" id="myModalPop" role="dialog">
            <div class="modal-dialog modal-sm">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header" style="text-align: center; background-color: #dff0d8;">
                            <asp:Label ID="Label18" Text="INFORMATION" runat="server" Font-Bold="true"></asp:Label>
                        </div>
                        <div class="modal-body" style="text-align: left">
                            <asp:Label ID="lblmassage" runat="server"></asp:Label>
                        </div>
                        <div class="modal-footer" style="text-align: center">
                            <button type="button" class="btn btn-primary" data-dismiss="modal" id="btnclose">
                                <span class="glyphicon glyphicon-ok"></span>OK
                            </button>
                        </div>
                    </div>
                </div>
        </div>
    </div>

     <%--<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />--%>
    <link href="../../Content/Bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
          <div class="modal fade" id="Alert" role="dialog">
         </div>

    </center>
</asp:Content>
