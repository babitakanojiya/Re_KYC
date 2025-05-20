<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="CKYCExcelUpload.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.CKYC.CKYCExcelUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/bundles/CKYCValidationjs") %>
    <%: Styles.Render("~/bundles/CKYCcss") %>
    <style type="text/css">
        table > thead > tr > th {
            background-image: linear-gradient(to bottom,#d6d6c2,#d6d6c2) !important;
            text-align: center !Important;
        }

        .AlignCenter {
            text-align: center !Important;
        }
    </style>

    <script language="javascript" type="text/javascript">
        function AlertMsg(msg) {
            debugger;
            showModal('#myModal', 'Information', 'alert-warning', '', '', msg);
        }
    </script>

    <script type="text/javascript">
        function ClearControl() {
            debugger;
            $('#ddlUpload').val("Select");
            $('#fileuploading').val('');

        }

        function popup() {
            $("#myModalPop").modal();
        }

    </script>

    <script type="text/javascript">

        //function showHideDiv(divName, btnName) {
        //    debugger;
        //    try {
        //        var objnewdiv = document.getElementById(divName)
        //        var objnewbtn = document.getElementById(btnName);
        //        if (objnewdiv.style.display == "block") {
        //            objnewdiv.style.display = "none";
        //            objnewbtn.className = 'glyphicon glyphicon-collapse-up'
        //        }
        //        else {
        //            objnewdiv.style.display = "block";
        //            objnewbtn.className = 'glyphicon glyphicon-collapse-down'
        //        }
        //    }
        //    catch (err) {
        //        ShowError(err.description);
        //    }
        //}

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <center>
          <div class="container">
                <div class="panel panel-success">
                    <div class="panel-heading" onclick="showHideDiv('EmptyPagePlaceholder_divSearch','btnToggle');return false;">
                <div class="row">
                    <div class="col-sm-10" style="text-align:left">
                       <span class="glyphicon glyphicon-menu-hamburger" ></span>
                                <asp:Label ID="lbltitle" runat="server"  Text="Bulk Upload"   CssClass="control-label"></asp:Label>
                    </div>
                    <div class="col-sm-2">
                        <span id="btnToggle" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                    </div>
                </div>
            </div>
                    <div id="divSearch" runat="server" class="panel-body" style="display:block">
                        <div class="row">
                            <div class="col-sm-3">
                                <asp:Label ID="lblUpload" runat="server" CssClass="control-label"></asp:Label>
                            </div>
                            <div class="col-sm-6">
                                <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                            <asp:DropDownList ID="ddlUpload" runat="server" CssClass="form-control" DataTextField="ParamDesc"
                                                DataValueField="ParamValue" AutoPostBack="true" OnSelectedIndexChanged="ddlUpload_SelectedIndexChanged">
                                            </asp:DropDownList>
                                     </ContentTemplate>
                                  <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlUpload" EventName="SelectedIndexChanged" />
                                  </Triggers>    
                             </asp:UpdatePanel>
                                
                            </div>
                            <div class="col-sm-3">
                                <asp:Button runat="server" ID="btnUpldFrmt" OnClick="btnUpldFrmt_Click" Text="Download Blank Format" CssClass="btn-animated bg-green"></asp:Button>
                            </div>
                        </div>
                       
                        <div class="row">
                            <div class="col-sm-3" >
                                <asp:Label ID="lblFileUpload" CssClass="control-label" runat="server" Width="80px"></asp:Label>
                            </div>
                            <div class="col-sm-6" style="text-align:left">
                                <asp:FileUpload ID="fileuploading" class="form-control" runat="server" />
                              
                            </div>
                        </div>
                        
                        <div class="row" style="margin-top:8px">
                             <div class="col-sm-12" align="center">
                                 <asp:Button runat="server" ID="btn_Upload" Text="Upload" CssClass="btn-animated bg-green" OnClick="btn_Upload_Click"></asp:Button>
                                <asp:Button runat="server" ID="btn_Cancel" Text="Cancel" CssClass="btn-animated bg-horrible" OnClick="btn_Cancel_Click" ></asp:Button> 
                             </div>
                     </div>
                    
                        <div id="trFileSize" runat="server" class="row" style="text-align:center">
                        <asp:Label ID="lblfilesize" runat="server" ForeColor="Red"  CssClass="control-label"></asp:Label>
                  </div>
                    
                      <%--  <div class="row" id="tbl_grid" runat="server" style="display: none">
                            <div class="panel  panel-success">
                                <div class="panel-heading subheader" onclick="ShowReqDtl('ctl00_ContentPlaceHolder1_divSearchDetails','btnUploadDtls');return false;"
                                    style="background-color: #EDF1cc !important;">
                                    <div class="col-sm-10" style="text-align: left">
                                        <span class="glyphicon glyphicon-menu-hamburger" style="color: Orange;"></span>
                                        <asp:Label ID="lblSearch" runat="server" CssClass="control-label"></asp:Label>
                                    </div>
                                    <div class="col-sm-2">
                                        <span id="btnUploadDtls" class="glyphicon glyphicon-collapse-down" style="float: right;
                                            color: Orange; padding: 1px 10px ! important; font-size: 18px;"></span>
                                    </div>
                                    <asp:Label ID="lblNotes" runat="server" Text="Notes" Font-Bold="true" Font-Size="10px"></asp:Label>
                                </div>
                                <div class="panel-body">
                                   
                                </div>
                            </div>
                        </div>--%>
                </div>
               </div>
           </div>    
    </center>
    <asp:HiddenField ID="hdnBatchid" runat="server" />
    <div class="container">
        <div class="modal fade" id="myModalPop" role="dialog">
            <div class="modal-dialog modal-sm">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header" style="text-align: center; background-color: #dff0d8;">
                        <asp:Label ID="Label3" Text="INFORMATION" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="modal-body" style="text-align: left">
                        <asp:Label ID="lbl" runat="server"></asp:Label>
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

</asp:Content>
