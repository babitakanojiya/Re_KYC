<%@ Page Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeFile="CKYCSecuredSearch.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.CKYC.CKYCSecuredSearch" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../../assets/KMI%20Styles/assets/jqueryCalendar/jquery-1.10.2.js"
        type="text/javascript"></script>
    <script src="../../../assets/KMI%20Styles/assets/jqueryCalendar/jquery-ui.js" type="text/javascript"></script>
    <link href="../../../KMI%20Styles/Bootstrap/css/bootstrap.min.css" rel="stylesheet"
        type="text/css" />
    <link href="../../../KMI%20Styles/assets/css/footable.min.css" rel="stylesheet" type="text/css" />
    <link href="../../../KMI Styles/assets/plugins/bootstrap/css/bootstrap.css" rel="stylesheet"
        type="text/css" />
    <link href="../../../KMI Styles/assets/css/KMI.css" rel="stylesheet" type="text/css" />
    <link href="../../../KMI Styles/assets/css/jquery.ui.all.css" rel="stylesheet" type="text/css" />
    <link href="../../../KMI Styles/assets/jqueryCalendar/jquery-ui.css" rel="stylesheet"
        type="text/css" />
    <meta http-equiv='content-type' content='text/html;charset=UTF-8;' />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="chrome=1" />
    <meta http-equiv="X-UA-Compatible" content="IE=8,chrome=1" />
    <script src="../PSSNEW/Script/COMM/CBFRMCommon.js" type="text/javascript"></script>
    <script src="../../../Scripts/JQuery/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="../../../KMI%20Styles/assets/plugins/bootstrap/js/footable.js" type="text/javascript"></script>
    <script src="../../../KMI%20Styles/Bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../../../KMI Styles/assets/jqueryCalendar/jquery-ui.js"></script>
    <script src="../../../KMI Styles/assets/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../PSSNEW/Script/COMM/CBFRMCommon.js"></script>
    <script type="text/javascript" src="../../../Scripts/ValidateInput.js"></script>
    <script type="text/javascript" src="/../Script/COMM/CalendarControl.js"></script>
    <script language="javascript" type="text/javascript" src="/../../../Script/JQuery/jquery-latest.js"></script>
    <script type="text/javascript" src="../../../Scripts/ValidateInput.js"></script>
    <script type="text/javascript" src="/../Script/COMM/CBFRMCommon.js"></script>
    <script type="text/javascript" src="../../../Scripts/common.js"></script>
    <script type="text/javascript" src="../../../Scripts/ValidationDefeater.js"></script>
    <script type="text/javascript" src="../../../Scripts/jsAgtCheck.js"></script>
    <script type="text/javascript" src="../../../Scripts/formatting.js"></script>

    <style type="text/css">
        .gridview th {
            padding: 3px;
            height: 40px;
            background-color: #d6d6c2;
        }

        body .modal {
            width: 100%;
            height: 100%;
            padding: 2%;
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
    <script type="text/javascript">
        function ShowReqDtl(divName, btnName) {
            //debugger;
            var objnewdiv = document.getElementById(divName);
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
        function funload() {
            document.getElementById('ctl00_ContentPlaceHolder1_divloaderqc').style.display = 'block'
            document.getElementById('divloaderqc').style.top = '264px';
        }
    </script>
    <script type="text/javascript">

        $(function () {
            $("#ctl00_ContentPlaceHolder1_txtDOB").datepicker({ dateFormat: 'dd/mm/yy' });




        });
    </script>
    <center>
        <asp:ScriptManager ID="SM1" runat="server">
        </asp:ScriptManager>
           <asp:updatepanel runat="server">
            <contenttemplate>
            <div id="divloaderqc" class="col-sm-12" runat="server" style="display: none;position:absolute;top:264px;">
                             <caption>
                                  <img id="Img3" alt="" src="~/images/spinner.gif" runat="server" />
                                  Please wait... Connecting to CERSAI Server for API Service call
                              </caption>
                          </div></contenttemplate>
        </asp:updatepanel>
        <br />
        <center>
  
       
            <div class="container">
                 
                  <asp:UpdatePanel ID="Upd1" runat="server">
               <ContentTemplate>
       

            <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
              <div id="Div4" runat="server" class="panel-heading" onclick="ShowReqDtl('ctl00_ContentPlaceHolder1_menu2','btnProofIdentity');return false;">
                                        <div class="row">
                                            <div class="col-sm-10" style="text-align: left; top: 0px; left: 0px;">
                                                <span class="glyphicon glyphicon-menu-hamburger" style="color: Orange;"></span>
                                                <asp:Label ID="lblProofOfIdentity11" Text="PROOF OF IDENTITY" runat="server"
                                                    CssClass="control-label"></asp:Label>
                                            </div>
                                            <div class="col-sm-2">
                                                <span id="btnProofIdentity" class="glyphicon glyphicon-collapse-down" style="float: right;
                                                    color: Orange; padding: 1px 10px ! important; font-size: 18px;"></span>
                                            </div>
                                        </div>
                                    </div>
            <div id="menu2" style="display: block;" runat="server" class="panel-body">
                <%--  Added for Proof of Identity start--%>
                <asp:updatepanel id="upMenu2" runat="server">
                    <contenttemplate>
                                <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                                   <div id="Div25" runat="server" class="panel-heading subheader" style="background-color:#EDF1cc !important"  onclick="ShowReqDtl('ctl00_ContentPlaceHolder1_div5','btnProofIdentity');return false;">
                                        <div class="row">
                                            <div class="col-sm-10" style="text-align: left">
                                                <span class="glyphicon glyphicon-menu-hamburger" style="color: Orange;"></span>
                                                <asp:Label ID="lblProofOfIdentity" Text="PROOF OF IDENTITY(Pol)*" runat="server"
                                                    CssClass="control-label"></asp:Label>
                                            </div>
                                            <div class="col-sm-2">
                                                <span id="Span14" class="glyphicon glyphicon-collapse-down" style="float: right;
                                                    color: Orange; padding: 1px 10px ! important; font-size: 18px;"></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="div5" style="display: block;" runat="server" class="panel-body">
                                        <div class="row">
                                            <div class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblProof" Text="(Certified copy of any one the following Proof of Identity [Pol] needs to be submitted)"
                                                    runat="server" CssClass="control-label"></asp:Label>
                                                <span><font color="red">*</font> </span>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlProofIdentity" runat="server" CssClass="form-control" AutoPostBack="true"
                                                 OnSelectedIndexChanged="ddlProofIdentity_SelectedIndexChanged" TabIndex="9">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div id="divIdProof" runat="server" class="row">
                                         
                                            <div id="divPassNo" runat="server" class="col-sm-3" style="text-align: left">
                                                <asp:Label ID="lblPassportNo" Placeholder="Passport Number" runat="server" CssClass="control-label"></asp:Label>
                                                <span><font color="red">*</font> </span>
                                            </div>
                                            <div id="divPassNotxt" runat="server" class="col-sm-3">
                                                <asp:TextBox CssClass="form-control"  runat="server" 
                                        onChange="javascript:this.value=this.value.toUpperCase();"   ID="txtPassNo" MaxLength="15" TabIndex="12" />
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server"
                                                    FilterType="Numbers,UppercaseLetters,LowercaseLetters" TargetControlID="txtPassNo">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                          </div>
                                      
                                        <div id="divDob" visible ="false"  runat="server" class="row">
                                    <div class="col-sm-3" style="text-align: left">
                        <asp:checkbox id="chkNormal" runat="server"  cssclass="standardcheckbox" 
                            tabindex="3" name="cb1" value="value1"  />
                             <asp:label id="lbldob" text="DOB (dd/mm/yyyy) " runat="server" cssclass="control-label">
                                </asp:label>
                                <span><font color="red">*</font> </span>
                            </div>
                            <div class="col-sm-3">


                            <asp:TextBox CssClass="form-control" onmousedown="$('#ctl00_ContentPlaceHolder1_txtDob').datepicker({ changeMonth: true, changeYear: true });"
                                                            onchange="setDateFormat('ctl00_ContentPlaceHolder1_txtDob')" runat="server" ID="txtDob"
                                                            MaxLength="15" TabIndex="38" />
                            
                            </div>
                         
                            </div>
                                        

                            <div class="row">
                            <div class="col-sm-12" style="text-align: center">
                                <asp:Label ID="lblMessage" runat="server" ForeColor="red" Visible="False" Width="310px"></asp:Label>
                            </div>
                        </div>
                                    </div>
                                </div>
                            </contenttemplate>
                </asp:updatepanel>
              
            </div>
          
    

                        <div class="row" id="divSearchDetails" runat="server">
                            <div class="panel  panel-success">
                                <div class="panel-heading subheader" onclick="ShowReqDtl('ctl00_ContentPlaceHolder1_trdg','btnDeptMstGrd');return false;"
                                    style="background-color: #EDF1cc !important;">
                                    <div class="row" id="trdgHdr" runat="server" visible="false">
                                        <div class="col-sm-10" style="text-align: left">
                                            <span class="glyphicon glyphicon-menu-hamburger" style="color: Orange;"></span>
                                            <asp:Label ID="lblSearch" runat="server" Font-Size="Small"></asp:Label>
                                        </div>
                                        <div class="col-sm-2">
                                            <span id="btnDeptMstGrd" class="glyphicon glyphicon-collapse-down" style="float: right;
                                                color: Orange; padding: 1px 10px ! important; font-size: 18px;"></span>
                                        </div>
                                    </div>
                                </div>
                                <div id="trdg" runat="server" visible="false">
                                    <div>
                                        <div style="overflow-x: scroll;">
                                            <asp:GridView ID="dgDownload" runat="server" AutoGenerateColumns="false" CssClass="footable"
                                                PageSize="10" AllowSorting="False" >
                                                <RowStyle CssClass="GridViewRow"></RowStyle>
                                                <HeaderStyle CssClass="gridview th" />
                                                <PagerStyle CssClass="disablepage" />
                                                <Columns>
                                                    <asp:BoundField DataField="CKYC_NO" HeaderText="CKYC_NO" />
                                                    <asp:BoundField DataField="name" HeaderText="name" />
                                                    <asp:BoundField DataField="FatherName" HeaderText="FatherName" />
                                                    <asp:BoundField DataField="DOB" HeaderText="DOB" />
                                                    <asp:BoundField DataField="KYC_DATE" HeaderText="KYC_DATE" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <br />
                                        
                                        
                                        <asp:HiddenField ID="hdnEnbl" runat="server" />
                                        <asp:HiddenField ID="hdncheck" runat="server" />
                                       
                                        <br />
                                    </div>
                                </div>
                                
                            </div>
                        </div>


                      <%--  Download Xml data--%>
                           <div class="row" id="div1" runat="server" style="display: none">
                            <div class="panel  panel-success">
                                <div class="panel-heading subheader" onclick="ShowReqDtl('ctl00_ContentPlaceHolder1_trdg','btnDeptMstGrd');return false;"
                                    style="background-color: #EDF1cc !important;">
                                    <div class="row" id="Dwnld" runat="server" visible="false">
                                        <div class="col-sm-10" style="text-align: left">
                                            <span class="glyphicon glyphicon-menu-hamburger" style="color: Orange;"></span>
                                            <asp:Label ID="Label2" runat="server" Font-Size="Small"></asp:Label>
                                        </div>
                                        <div class="col-sm-2">
                                            <span id="Span2" class="glyphicon glyphicon-collapse-down" style="float: right;
                                                color: Orange; padding: 1px 10px ! important; font-size: 18px;"></span>
                                        </div>
                                    </div>
                                </div>

                                <div style="overflow-x: scroll;">
                                            <asp:GridView ID="GridDownloadResponse" runat="server" AutoGenerateColumns="true" CssClass="footable"
                                                PageSize="10" AllowSorting="False">
                                                <RowStyle CssClass="GridViewRow"></RowStyle>
                                                <HeaderStyle CssClass="gridview th" />
                                                <PagerStyle CssClass="disablepage" />
                                            </asp:GridView>
                                        </div>
                                        </div>
                                        </div>



                        <div id="btncan" runat="server" class="row">
                        
                            <div class="col-sm-12" style='margin-top:15px;'>
                            <asp:LinkButton ID="btnSearch"  OnClick="btnSearch_Click" OnClientClick="funload();"
                                                CssClass="btn btn-primary" runat="server">
                                                <asp:HiddenField ID="TabName" runat="server" />
                                                <span class="glyphicon glyphicon-search BtnGlyphicon"></span> Search
                                            </asp:LinkButton>
                                             <asp:LinkButton ID="btnExport" Visible ="false" Text ="Download"  runat="server" CssClass="btn btn-primary"
                                                    OnClick="btnExport_Click">
                                  <%--  <span class="glyphicon glyphicon-download BtnGlyphicon"></span> Download--%>
                                    </asp:LinkButton>
                                <asp:LinkButton ID="btnpopcancel" runat="server" CssClass="btn btn-danger" 
                                    onclick="btnpopcancel_Click"> <span class="glyphicon glyphicon-remove BtnGlyphicon"></span>Cancel
                                    </asp:LinkButton>
                           
                               
                                 </div>
                        </div>
                    </div>
                    </ContentTemplate>
                    </asp:UpdatePanel>

                      </div>
                
           
            <div class="modal fade" id="mypopup" role="dialog">
                <div class="modal-dialog modal-sm">
                    <!-- Modal content-->
                    <div class="modal-content" style='width: 400px; height: 225px;'>
                        <div class="modal-header" style="text-align: center; background-color: #dff0d8;">
                            <asp:Label ID="Label1" Text="INFORMATION" runat="server" Font-Bold="true"></asp:Label>
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
            <%-- </ContentTemplate>
        </asp:UpdatePanel>--%>
        </center>
       
     
        <asp:Label runat="server" ID="lbl1" Style="display: none" />
        <ajaxToolkit:ModalPopupExtender runat="server" ID="mdlView" BehaviorID="mdlViewBID"
            DropShadow="true" TargetControlID="lbl1" PopupControlID="pnlMdl" BackgroundCssClass="modalPopupBg">
        </ajaxToolkit:ModalPopupExtender>
        <ajaxToolkit:ModalPopupExtender ID="mdlpopup" runat="server" TargetControlID="Lbl"
            BehaviorID="mdlpopup" BackgroundCssClass="modalPopupBg" PopupControlID="pnl"
            DropShadow="true" OkControlID="btnok" Y="100">
        </ajaxToolkit:ModalPopupExtender>
      
        <asp:HiddenField ID="hiddenField1" runat="server" />
        <asp:Panel ID="Panel1" runat="server" Style="display: none; background-color: modalBackground;">
            <%--background-color: #C0C0C0;--%>
            <center>
                <img src="../../../../theme/iflow/animated_progress_bar.gif" />
                <br />
                <asp:Label ID="waitingMsg" runat="server" Text="Please wait..." ForeColor="red" BackgroundCssClass="modalBackground"></asp:Label>
            </center>
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Lbl"
            PopupControlID="pnlpopup" CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>

        <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="269px" Width="400px"
            Style="display: none">
            <div class="modal-content" style='width: 400px; height: 225px;'>
                <div class="modal-header" style="text-align: center; background-color: #dff0d8;">
                    <asp:Label ID="lblinfon" Text="INFORMATION" runat="server" Font-Bold="true"></asp:Label>
                </div>
                <div class="modal-body" style="text-align: center">
                    <asp:Label ID="lblpop" runat="server"></asp:Label>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <asp:Button ID="btnCancel" runat="server" Text="OK" class="btn btn-primary glyphicon glyphicon-ok"
                        Style="color: White;" />
                </div>
            </div>
        </asp:Panel>
    </center>
</asp:Content>
