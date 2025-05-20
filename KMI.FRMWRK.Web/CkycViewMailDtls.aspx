<%@ Page Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="CkycViewMailDtls.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.CKYC.CkycViewMailDtls" %>


<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/bundles/CKYCValidationjs") %>
    <%: Styles.Render("~/bundles/CKYCcss") %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">
  <style type="text/css">
  .headerCls{
    background-color: #DCE9F9;
    border-radius: 6px 0 0;
    background-image: linear-gradient(to bottom,#ebf3fc,#dce9f9);
    box-shadow: 0 1px 0 rgba(255,255,255,.8) inset;
    border-top: 0;
    text-shadow: 0 1px 0 rgba(255,255,255,.5);
    padding: 10px;
    padding: 10px;
    position: relative;
    text-align: center !important;
    display: table-cell;
    vertical-align: inherit;
    font-weight: bold;
    border-spacing: 0;
    border: solid #ccc 1px;
    border-left: 1px solid #ccc;
    box-sizing: border-box;
    text-align:center !important;
                }
  .font{
      font-weight: bold !important;
  }
          </style>
<%--        <script type="text/javascript">--%>

        <div>
                        <div id="dvRsltTmplt" runat="server" visible="true" class="panel panel-success">
                      <div id="dvHdrRsltTmplt" runat="server" class="panel-heading" onclick="ShowReqDtl('dvRsultTmpView','spnRsltTmplt');return false;">
                    <div class="row">
					<div class="col-sm-10">
						<span id="tableHeaderTitle">Communication Logger</span>
					
					</div>
                        <div class="col-sm-2">
                            <span id="spnRsltTmplt" class="glyphicon glyphicon-collapse-down" style="float: right;
                                color: Orange; padding: 1px 10px ! important; font-size: 18px;"></span>
                        </div>
                    </div>
                </div>
                      <div id="dvRsultTmpView" runat="server" class="panel-body">
                    <div class="row"  runat="server" id="divfrtemp">
                            <div runat="server" id="divKIN" >
                        </div>
                    </div>
                </div>
           
                        <div id="dvemail" runat="server" class="panel panel-success" style="width:96%; display: none">
		         	<div id="tblMain" runat="server" style="width: 100%;" class="panel-heading" >
			 <div class="row">            
                            <div class="col-sm-10" style="width:70%;">
	                       <span id="btnServReq" class="glyphicon glyphicon-minus icon" style="float: left; color: white;"
							   onclick="ShowReqDtl1('divMailDtlContainer', 'btnServReq'); return false;"></span>
                                <span class=""></span>&nbsp;Email Content
                            </div>
                            <div class="col-sm-2" style="margin-left:35px;width: 25%;">
						    <asp:Label ID="lblsrcntdtl" runat="server" MaxLength="10" Text=" " CssClass="control-label"
								style="background-color: transparent"></asp:Label>
                            </div>
                        </div>
				 </div>

                  <div id="divMailDtlContainer" runat="server" class="panel-body" style="width: 100%; height: 390px; overflow: scroll; display: none">
				<asp:GridView ID="gvmail" runat="server" PageSize="05" AllowPaging="True" AutoGenerateColumns="False"
					HorizontalAlign="Center" RowStyle-CssClass="formtable" AllowSorting="true" Width="100%" OnRowDataBound="gvmail_RowDataBound"
					Height="300px" >
					<RowStyle CssClass="GridViewRow"></RowStyle>
					<Columns>
						<asp:TemplateField HeaderText="Mail Content" HeaderStyle-CssClass="headerCls font" >
							<ItemTemplate>
								<table bgcolor="#DCDCDC" width="100%">
									<tr>
										<td align="left" style="width: 17.5%">
											<asp:Label ID="lblrefno" runat="server" CssClass="font">Ckyc No :</asp:Label>&#160&nbsp;
                                        <asp:Label ID="lblmsgrefno" runat="server" class="standardlabel3" Text='<%# Eval("CkycNo") %>'></asp:Label>
										</td>
										<td style="width: 17.5%">
											<asp:Label ID="lbldate" runat="server" class="font">Send Date :</asp:Label>&#160&nbsp;
                                        <asp:Label ID="lblsenddate" runat="server" class="standardlabel3" Text='<%# Eval("CreatedDTim") %>'></asp:Label>
										</td>
									</tr>
									<tr>
										<td align="left" colspan="2" style="width: 50%;">
											<asp:Label ID="lblemailto" runat="server" class="font">To :</asp:Label>&#160;&#160;
                                        <asp:Label ID="lbltomail" runat="server" class="standardlabel3" Text='<%# Eval("RectEm") %>'></asp:Label>
										</td>
									</tr>
									<tr>
										<td align="left" style="width: 100%;" colspan="4">
											<asp:Label ID="lblSubject1" runat="server" class="font">Subject :</asp:Label>&#160;
                                        <asp:Label ID="lblsubject" runat="server" class="standardlabel3" Text='<%# Eval("MailSubject") %>'></asp:Label>
										</td>
									</tr>
								</table>
								<table id="Table2" width='100%'>
									<tr>
										<td align="left" style="width: 100%;" colspan="4">
											<asp:Label ID='lblmailcontent' runat="server" class="font">Mail Content  :</asp:Label>&#160;
                                        <br />
											<br />
											<div id="divmailcontent" runat="server">
											</div>
											<div id="divMailAttachment" runat="server">
											</div>
										</td>
									</tr>
								</table>
							</ItemTemplate>
						</asp:TemplateField>
					</Columns>
				</asp:GridView>
			</div>

    </div>
                             </div>
 </div>
</asp:Content>
