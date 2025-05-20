<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="CKYCUpload.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.CKYC.CKYCUpload" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">
    <center>
          <div class="container">
                <div class="panel panel-success">
                    <div class="panel-heading" onclick="showHideDiv('divSearch','btnToggle');return false;">
                <div class="row">
                    <div class="col-sm-10" style="text-align:left">
                       <span class="glyphicon glyphicon-menu-hamburger" ></span>
                                <asp:Label ID="lbltitle" runat="server"   CssClass="control-label"></asp:Label>
                    </div>
                    <div class="col-sm-2">
                        <span id="btnToggle" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                    </div>
                </div>
            </div>
                    <div id="divSearch" runat="server" style="display:block;" class="panel-body">
                     <%--  <asp:UpdatePanel ID="updfile" runat="server">
                        <ContentTemplate>--%>
                        <div class="row">
                            <div class="col-sm-3">
                                <asp:Label ID="lblUpload" runat="server" CssClass="control-label"></asp:Label>
                            </div>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlUpload" runat="server" CssClass="form-control" DataTextField="ParamDesc"
                                    DataValueField="ParamValue" AutoPostBack="true" OnSelectedIndexChanged="ddlUpload_SelectedIndexChanged">
                                </asp:DropDownList>
                                
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
                      <%--  </ContentTemplate>
                        </asp:UpdatePanel> --%>
                        <br/>
                         <div class="row" >
                             <div class="col-sm-12" align="center">
                                 <asp:Button runat="server" ID="btn_Upload" OnClick="btn_Upload_Click" Text="Upload" CssClass="btn-animated bg-green"></asp:Button>
                                  <asp:Button runat="server" ID="btn_Validate" OnClick="btn_Validate_Click" Text="Validate" CssClass="btn-animated bg-green"></asp:Button>
                                 <asp:Button runat="server" ID="btn_Process" OnClick="btn_Process_Click" Text="Process" CssClass="btn-animated bg-green"></asp:Button>
                                <asp:Button runat="server" ID="btn_Cancel" OnClick="btn_Cancel_Click" Text="Cancel" CssClass="btn-animated bg-horrible"></asp:Button>
                                
                             </div>
                     </div>
                    
                     <div id="trFileSize" runat="server" class="row" style="text-align:center">
                        <asp:Label ID="lblfilesize" runat="server" ForeColor="Red"  CssClass="control-label"
                             ></asp:Label>
                     </div>
                      
                      <br/>
                        <div class="row" id="tbl_grid" runat="server" style="display: none">
                            <div class="panel  panel-success">
                                <div class="panel-heading subheader" onclick="ShowReqDtl('ctl00_ContentPlaceHolder1_divSearchDetails','btnUploadDtls');return false;"
                                    >
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
                        </div>
                        <div class="row" id="tblErrorLog" runat="server" visible="false">
                            <div class="panel  panel-success">
                                  <div class="panel-heading subheader" onclick="ShowReqDtl('ctl00_ContentPlaceHolder1_divValid','btnValid');return false;"
                                    >
                                    <div class="row" >
                                        <div class="col-sm-10" style="text-align: left">
                                            <span class="glyphicon glyphicon-menu-hamburger" style="color: Orange;"></span> Process Log Details
                                            
                                        </div>
                                        <div class="col-sm-2">
                                            <span id="btnValid" class="glyphicon glyphicon-collapse-down" style="float: right;
                                                color: Orange; padding: 1px 10px ! important; font-size: 18px;"></span>
                                        </div>
                                    </div>
                                </div>
                                <div id="divValid" style="display:block;" runat="server">
                                    <table class="table table-bordered" style="margin:1%;width:98%">
                                        <thead>
                                            <tr style="background-color: #d6d6c2;">
                                                <td >
                                                    <asp:Label ID="lblDesc" runat="server" CssClass="control-label" ></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCountDesc" runat="server" CssClass="control-label" ></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblview" runat="server" CssClass="control-label" ></asp:Label>
                                                </td>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblTotlCount" runat="server" CssClass="control-label" ></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbltCountDesc" runat="server" ></asp:Label>
                                                </td>
                                                <td >
                                                    <asp:Label ID="lbl11" runat="server" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr >
                                                <td >
                                                    <asp:Label ID="lblSuccessCount" runat="server"  CssClass="control-label"
                                                       ></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSuccessCountDesc" runat="server" 
                                                        ForeColor="Green"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lnkSuccess" runat="server" Font-Underline="True" ForeColor="Green" OnClick="lnkSuccess_Click">Success Log</asp:LinkButton>
                                                </td>
                                            </tr>
                                            <tr >
                                                <td >
                                                    <asp:Label ID="lblErrorCount" runat="server"  CssClass="control-label"
                                                      ></asp:Label>
                                                </td>
                                                <td >
                                                    <asp:Label ID="lblErrorCountDesc" runat="server"  ForeColor="Red"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lnkFail" runat="server" Font-Underline="true" ForeColor="Red" OnClick="lnkFail_Click">Error Log</asp:LinkButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="griderror" runat="server" visible="false" >
                            <div class="panel  panel-success">
                                <div class="panel-heading subheader" onclick="ShowReqDtl('ctl00_ContentPlaceHolder1_trError','btnValidLog');return false;"
                                    >
                                    <div class="row">
                                        <div class="col-sm-10" style="text-align: left">
                                            <span class="glyphicon glyphicon-menu-hamburger" style="color: Orange;"></span> 
                                             <asp:Label ID="lblGridError" runat="server" CssClass="control-label"></asp:Label>
                                        </div>
                                        <div class="col-sm-2">
                                            <span id="btnValidLog" class="glyphicon glyphicon-collapse-down" style="float: right; color: Orange;
                                                padding: 1px 10px ! important; font-size: 18px;"></span>
                                        </div>
                                    </div>
                                </div>
                              
                                <div class="row" id="trError" runat="server" style="display:none;margin:1%;">
                                  <div>
                                 <div style="overflow-x: scroll;">
                                  <asp:GridView ID="ErrorGrid" AutoGenerateColumns="false" PageSize="10" runat="server" CssClass="footable"
                                EmptyDataText="No records has been added."  Width="2000px"  AllowPaging="true" OnPageIndexChanging="ErrorGrid_PageIndexChanging"
                                        >
                                    
                                    <Columns>
                                        <asp:BoundField DataField="Batchid" HeaderText="Batch ID">
                                            <ItemStyle HorizontalAlign="Center" Font-Bold="False"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UniqueRefNo" HeaderText="Unique Ref. No.">
                                            <ItemStyle HorizontalAlign="Center" Font-Bold="False"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ErrorDesc" HeaderText="Error Description">
                                            <ItemStyle HorizontalAlign="Left" Font-Bold="False"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ErrorCode" HeaderText="Error Code">
                                            <ItemStyle HorizontalAlign="Center" Font-Bold="False"></ItemStyle>
                                        </asp:BoundField>
                                    </Columns>
                                  
                                </asp:GridView>
                                </div>
                                </div>
                                </div>
                            </div>
                        </div>
                          <div class="row" id="trSuccessTitle" runat="server" visible="false" >
                            <div class="panel  panel-success">
                                <div class="panel-heading subheader" onclick="ShowReqDtl('ctl00_ContentPlaceHolder1_trSuccess','btnValidSuccess');return false;"
                                    >
                                    <div class="row">
                                        <div class="col-sm-10" style="text-align: left">
                                            <span class="glyphicon glyphicon-menu-hamburger" style="color: Orange;"></span> 
                                             <asp:Label ID="lblGridSuccess" runat="server" CssClass="control-label"></asp:Label>
                                        </div>
                                        <div class="col-sm-2">
                                            <span id="btnValidSuccess" class="glyphicon glyphicon-collapse-down" style="float: right; color: Orange;
                                                padding: 1px 10px ! important; font-size: 18px;"></span>
                                        </div>
                                    </div>
                                </div>
                                
                                <div class="row" id="trSuccess" runat="server" style="display:none;margin:1%;">
                                 <div style="overflow-x: scroll;">
                               
                                                  <asp:GridView ID="SuccessGrid" AutoGenerateColumns="false" PageSize="10" runat="server"
                                  AllowPaging="true" CssClass="footable"   EmptyDataText="No records has been added."  Width="2000px"
                                         OnPageIndexChanging="SuccessGrid_PageIndexChanging">
                                    <RowStyle CssClass="GridViewRow"></RowStyle>
                                                <HeaderStyle CssClass="gridview th" />
                                                <PagerStyle CssClass="disablepage" />
                                    <Columns>
                                        <asp:BoundField DataField="Batchid" HeaderText="Batch ID">
                                            <ItemStyle HorizontalAlign="Center" Font-Bold="False"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UniqueRefNo" HeaderText="Unique Ref. No.">
                                            <ItemStyle HorizontalAlign="Center" Font-Bold="False"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Insurer Ref No" HeaderText="Insurer Ref. No.">
                                            <ItemStyle HorizontalAlign="Center" Font-Bold="False"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Applicant Name" HeaderText="Applicant Name">
                                            <ItemStyle HorizontalAlign="Left" Font-Bold="False"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SuccessDesc" HeaderText="Success Description">
                                            <ItemStyle HorizontalAlign="Center" Font-Bold="False"></ItemStyle>
                                        </asp:BoundField>
                                    </Columns>
                                      </asp:GridView>
                                </div>
                                </div>
                               
                            </div>
                        </div>
                        
                         <div class="row" style="padding-bottom: 6px ! important;">
                            <div class="col-md-12" style="text-align: center">
                              
                              <asp:Button runat="server" ID="btnExport" OnClick="btnExport_Click" Visible="false" Text="Export Errors" CssClass="btn-animated bg-green"></asp:Button>  
                              <asp:Button runat="server" ID="btnFailCase" OnClick="btnFailCase_Click" Visible="false" Text="Export Failed Cases" CssClass="btn-animated bg-green"></asp:Button>  
                                   
                            </div>
                        </div>    
                </div>
               </div>
           </div>    
        </center>
    <div style="width: 100%; display: none;">
        <table class="tableIsys" width="100%">
            <tr id="trErrorTitle" runat="server" visible="false">
                <td class="test" align="left" style="border-collapse: separate; border-right-width: 0; height: 20px;"
                    width="80%"></td>
                <td class="test" align="right" style="border-collapse: separate; border-right-width: 0; height: 20px;"
                    width="20%">
                    <asp:Label ID="lblPageInfo" runat="server" Style="text-align: right;"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="formcontent" align="center" colspan="2"></td>
            </tr>
            <tr>
                <td class="test" align="left" style="border-collapse: separate; border-right-width: 0; height: 20px;"
                    width="80%"></td>
                <td class="test" align="right" style="border-collapse: separate; border-right-width: 0; height: 20px;"
                    width="20%">
                    <asp:Label ID="lblSPageInfo" runat="server" Style="text-align: right;"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="formcontent" align="center" colspan="2"></td>
            </tr>
            <tr>
                <td class="tableIsys" colspan="2">
                    <center>
                                    <asp:Label ID="lblErrMsg" runat="server" Width="400px" Font-Bold="true" ForeColor="Red"
                                        Visible="false"></asp:Label>
                                </center>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2"></td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hdnBatchid" runat="server" />
    <asp:HiddenField ID="hdnFileStsFlag" runat="server" />
    <asp:HiddenField ID="hdnFlagCheck" runat="server" />
    <div class="container">
        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog modal-sm">

                <!-- Modal content-->
                <div class="modal-content">
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
    </div>
</asp:Content>
