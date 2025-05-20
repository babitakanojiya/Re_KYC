<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Empty.Master" CodeBehind="CommunicationLog_NEW.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.CKYC.CommunicationLog_NEW" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/bundles/CKYCValidationjs") %>
    <%: Styles.Render("~/bundles/CKYCcss") %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">

    <div class="page-container">
        <div class="panel panel-success" style="margin-left: 2%; margin-right: 2%; margin-top: 0.5%">
                <div class="panel-heading" onclick="showHideDiv('divsearch','btnToggle');return false;">
                        <div class="row">
                            <div class="col-sm-10" style="text-align: left">
                                <span class="glyphicon glyphicon-menu-hamburger"></span>
                                <asp:Label ID="title" runat="server" Font-Bold="False" Text="Communication Logger"></asp:Label>
                            </div>
                            <div class="col-sm-2">
                                <span id="btnToggle" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
<%--                    <i class="glyphicon glyphicon-minus icon" id="DocUploadImg" onclick="ShowHideForAll('divsearch','btnShwAdvSrh');return false;"></i>--%>
                    <input type="button" style="display: none" id="btnFileUploadDtl"></input>
                </div>
        <div id="divsearch" runat="server" style="display: block;" class="panel-body">
                <div class="row">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblSrNo" Text="CKYC Number" CssClass="control-label" runat="server">
                        </asp:Label>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:TextBox ID="txtSrNo" CssClass="form-control" runat="server" MaxLength="10" disabled="disabled"></asp:TextBox>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lbltypeofcomm" runat="server" Text="Type Of Communication" CssClass="control-label"></asp:Label>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:DropDownList ID="ddltypecomm" runat="server" CssClass="form-control" Width="150px">
                            <asp:ListItem Value="-1" Text="Select"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Email"></asp:ListItem>
                            <asp:ListItem Value="2" Text="SMS"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                 </div>
        </div>

        <div class="row">
                                <center>
                            <div class="col-sm-12" style='margin-top: 15px;'>
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn-animated bg-green" Text="Search" OnClick="btnSearch_click"
                                OnClientClick="return validate();" />
                            <asp:Button ID="btnClear" runat="server" CssClass="btn-animated bg-green" Text="Clear" OnClick="btnClear_Click" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn-animated bg-green" Text="Cancel" OnClick="btnCancel_Click" />
                                </div>
                        </center>
                </div>
        </div>



    <div id="dvemail" runat="server" class="page-container" style="margin-top: 0px; display: none">
                    <div class="panel panel-success" style='margin-right: 26px; margin-left: 26px;'>
                        <div runat="server" id="trtitle6" class="panel-heading" onclick="showHideDiv('trgridsponsorship2', 'span545');return false;">
                            <div class="row" id="trDetails1" runat="server">
                                <div class="col-sm-10" style="text-align: left">
                                    <span class="glyphicon glyphicon-menu-hamburger"></span>
                                    <asp:Label ID="Label7" runat="server" Text="Mail Content"></asp:Label>
                                </div>
                                <div class="col-sm-2">
                                    <%--<asp:Label ID="lblsrcntdtl" runat="server" MaxLength="10" Text=" " CssClass="standardlabel"></asp:Label>--%>
                                    <span id="span545" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                                </div>
                            </div>
                        </div>
                        <div id="trgridsponsorship2" class="panel-body">
        <div id="divMailDtlContainer" runat="server" style="width: 100%; height: 390px; overflow: scroll; display: none">
            <asp:GridView ID="gvmail" runat="server" PageSize="05" AllowPaging="True" AutoGenerateColumns="False" CssClass="footable" 
                HorizontalAlign="Center" RowStyle-CssClass="formtable" AllowSorting="true" Width="100%"
                Height="300px" OnPageIndexChanging="gvmail_PageIndexChanging" OnRowDataBound="gvmail_RowDataBound">
                                <HeaderStyle HorizontalAlign="Center" BackColor="#dce9f9" />
                                <FooterStyle CssClass="GridViewFooter" />
                                <RowStyle CssClass="GridViewRow" />

                                <SelectedRowStyle CssClass="GridViewSelectedRow" />
                                <AlternatingRowStyle CssClass="GridViewAlternateRow"></AlternatingRowStyle>
                <Columns>
                    <asp:TemplateField HeaderText="Select" Visible="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="pad">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSendMail" runat="server" AutoPostBack="false" />
                        </ItemTemplate>
                        <ItemStyle Width="05px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mail Content" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="pad">
                        <ItemTemplate>
                            <table bgcolor="#DCDCDC" width="100%">
                                <tr>

                                    <td align="left" style="width: 17.5%">
                                        <asp:Label ID="lblrefno" runat="server" CssClass="tableHeaderTitle">Msg RefNo :</asp:Label>&#160&nbsp;
                                        <asp:Label ID="lblmsgrefno" runat="server" class="standardlabel3" Text='<%# Eval("MailRefNo") %>'></asp:Label>
                                    </td>
                                    <td style="width: 17.5%">
                                        <asp:Label ID="lblmsgtp" runat="server" class="tableHeaderTitle">Msg Type :</asp:Label>&#160&nbsp;
                                        <asp:Label ID="lblmsgtype" class="standardlabel3" runat="server" Text='<%# Eval("Mode") %>'></asp:Label>
                                    </td>
                                    <td style="width: 17.5%">
                                        <asp:Label ID="lbldate" runat="server" class="tableHeaderTitle">Send Date :</asp:Label>&#160&nbsp;
                                        <asp:Label ID="lblsenddate" runat="server" class="standardlabel3" Text='<%# Eval("MailSendDate") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2" style="width: 50%;">
                                        <asp:Label ID="lblemailto" runat="server" class="tableHeaderTitle">To :</asp:Label>&#160;&#160;
                                        <asp:Label ID="lbltomail" runat="server" class="standardlabel3" Text='<%# Eval("MailTo") %>'></asp:Label>
                                    </td>
                                    <td align="left" colspan="2" style="width: 50%;">
                                        <asp:Label ID="lblemailfrm" runat="server" class="tableHeaderTitle">From :</asp:Label>&#160;
                                        <asp:Label ID="lblfrommail" runat="server" class="standardlabel3" Text='<%# Eval("MailFrom") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2" style="width: 50%;">
                                        <asp:Label ID="lblspcc" runat="server" class="tableHeaderTitle">Cc :</asp:Label>&#160;
                                        <asp:Label ID="lblcc" runat="server" class="standardlabel3" Text='<%# Eval("MailCC") %>'></asp:Label>
                                    </td>
                                    <td align="left" colspan="2" style="width: 50%;">
                                        <asp:Label ID="lblspbcc" runat="server" class="tableHeaderTitle">Bcc :</asp:Label>&#160;
                                        <asp:Label ID="lblbcc" runat="server" class="standardlabel3" Text='<%# Eval("MailBCC") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 100%;" colspan="4">
                                        <asp:Label ID="lblSubject1" runat="server" class="tableHeaderTitle">Subject :</asp:Label>&#160;
                                        <asp:Label ID="lblsubject" runat="server" class="standardlabel3" Text='<%# Eval("MailSubject") %>'></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table id="Table2" width='100%'>
                                <tr>
                                    <td align="left" style="width: 100%;" colspan="4">
                                        <asp:Label ID='lblmailcontent' runat="server" class="tableHeaderTitle">Mail Content :</asp:Label>&#160;
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
                    <%--Added by prity for AR81--%>
                    <asp:TemplateField HeaderText="Mail Event" ItemStyle-Wrap="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="pad">
                        <ItemTemplate>
                            <asp:Label ID="lbleventName" runat="server" Text='<%# Eval("eventName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" CssClass="pad" />
                    </asp:TemplateField>
                    <%--Ended by prity for AR81--%>
                </Columns>
            </asp:GridView>
        </div>

    </div></div>

    </div>
    <div id="dvsendbtn" runat="server" style="width: 100%; display: none;">
        <table class="formcontent" width="100%">
            <tr>
                <td align="center" class="formtable1" style="height: 25px" colspan="8">
                    <center>
                            <asp:Button ID="btnresendemail" runat="server" CssClass="bottonSend" Text="ReSend Email"
                                OnClick="btnresendemail_Click" />
                        </center>
                </td>
            </tr>
        </table>
    </div>

    <div id="dvsms" runat="server" class="page-container" style="margin-top: 0px; display: none">
                    <div class="panel panel-success" style='margin-right: 26px; margin-left: 26px;'>
                        <div runat="server" id="trtitle" class="panel-heading" onclick="showHideDiv('divsms', 'span1');return false;">
                            <div class="row" id="trDetails" runat="server">
                                <div class="col-sm-10" style="text-align: left">
                                    <span class="glyphicon glyphicon-menu-hamburger"></span>
                                    <asp:Label ID="Label1" runat="server" Text="SMS Content"></asp:Label>
                                </div>
                                <div class="col-sm-2">
                                    <%--<asp:Label ID="lblsmsconunt" runat="server" MaxLength="10" Text=" " CssClass="standardlabel" align="left"></asp:Label>--%>
                                    <span id="span1" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                                </div>
                            </div>
                        </div>
                        <div id="trgridsponsorship" class="panel-body">
        <div id="divsms" runat="server" style="width: 100%; height: 300px; overflow: scroll; display: none">
            <asp:GridView ID="grdsms" runat="server" PageSize="05" AllowPaging="True" AutoGenerateColumns="False" CssClass="footable"
                HorizontalAlign="Center" AllowSorting="true" Width="100%"
                OnPageIndexChanging="grdsms_PageIndexChanging" OnRowDataBound="grdsms_RowDataBound">
                                <HeaderStyle HorizontalAlign="Center" BackColor="#dce9f9" />
                                <FooterStyle CssClass="GridViewFooter" />
                                <RowStyle CssClass="GridViewRow" />

                                <SelectedRowStyle CssClass="GridViewSelectedRow" />
                                <AlternatingRowStyle CssClass="GridViewAlternateRow"></AlternatingRowStyle>
                <Columns>
                    <asp:TemplateField HeaderText="Select" Visible="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="pad">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSendSMS" runat="server" AutoPostBack="false" />
                        </ItemTemplate>
                        <ItemStyle Width="05px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SMS Content" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="pad">
                        <ItemTemplate>
                            <table bgcolor="#DCDCDC" width="100%">
                                <tr>
                                    <td>
                                        <tr>
                                            <td width="50%">
                                                <asp:Label ID="lblsmsrefno" runat="server" CssClass="tableHeaderTitle">SMS RefNo :</asp:Label>&#160;
                                                <asp:Label ID="lblsmsgrefno" runat="server" class="standardlabel3" Text='<%# Eval("SMSRefNo") %>'></asp:Label>
                                            </td>
                                            <td width="50%">
                                                <asp:Label ID="lblsmstype" runat="server" class="tableHeaderTitle">MSG Type     :</asp:Label>&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;
                                                <asp:Label ID="lblMsgtype" runat="server" class="standardlabel3" Text='<%# Eval("Mode") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="50%">
                                                <asp:Label ID="lblsmst" runat="server" class="tableHeaderTitle">SMS To :</asp:Label>&#160;&#160;&#160;&#160;&#160;&#160;
                                                <asp:Label ID="lblsmsto" runat="server" class="standardlabel3" Text='<%# Eval("SMSTo") %>'></asp:Label>
                                            </td>
                                            <td width="50%">
                                                <asp:Label ID="lblsmsdate" runat="server" class="tableHeaderTitle">Send Date :</asp:Label>&#160;
                                                <asp:Label ID="lblsmssenddate" runat="server" class="standardlabel3" Text='<%# Eval("CreateDTim") %>'></asp:Label>
                                            </td>
                                        </tr>
                                    </td>
                                </tr>
                            </table>
                            <table id="Table2" width='100%'>
                                <tr>
                                    <td align="left" colspan="2">
                                        <asp:Label ID="lblsmscontent" runat="server" class="tableHeaderTitle">SMS Content :</asp:Label>&#160;
                                        <br />
                                        <br />
                                        <div id="divsmscontent" runat="server">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--Added by prity for AR81--%>
                    <asp:TemplateField HeaderText="SMS Event" ItemStyle-Wrap="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="pad">
                        <ItemTemplate>
                            <asp:Label ID="lbleventName" runat="server" Text='<%# Eval("eventName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" CssClass="pad" />
                    </asp:TemplateField>
                    <%--Ended by prity for AR81--%>
                </Columns>
            </asp:GridView>
        </div>
        <div id="dvsendsmsbtn" runat="server" style="display: none;">
        <center>
                <table>
                    <tr>
                        <td class="formtable1" style="text-align: center" colspan="6">
                            <asp:Button ID="btnsendsms" runat="server" class="btn-animated bg-green" Text="ReSend SMS"
                                OnClick="btnsendsms_Click" />
                        </td>
                    </tr>
                </table>
            </center>
    </div>
    </div></div></div>

    <%--Added by tushar for whatsApp--%>
    <div id="DVWhatsApp" runat="server" style="display: none;">
        <table id="Table1" runat="server" class="tableHeader" width="100%">
            <tr>
                <td align="left" width="70%">
                    <input name="btnServReq" type="button" id="Button1" runat="server" value="-" class="tableHeaderButton"
                        onclick="ShowReqDtl1('divsms', 'btnsms'); return false;" style="text-align: center;" />
                    <asp:Label ID="Label2" runat="server" Text="WhatsApp Consent" CssClass="tableHeaderTitle"></asp:Label>
                </td>
                <td align="right" class="formcontent" style="width: 30%; background-color: transparent">
                    <asp:Label ID="Label3" runat="server" MaxLength="10" Text="WhatsApp Status "
                        CssClass="standardlabel"></asp:Label>
                </td>
            </tr>
        </table>
        <div id="DivWhatsApp" runat="server" style="width: 100%; height: 300px; overflow: scroll; display: none">
            <asp:GridView ID="GRDWhatsApp" runat="server" PageSize="05" AllowPaging="True" AutoGenerateColumns="False"
                HorizontalAlign="Center" RowStyle-CssClass="formtable" AllowSorting="true" Width="100%">
                <RowStyle CssClass="GridViewRow"></RowStyle>
                <Columns>
                    <asp:TemplateField HeaderText="WhatsApp Details">
                        <ItemTemplate>
                            <table width="100%" bgcolor="#DCDCDC">
                                <tr>
                                    <td>
                                        <tr>
                                            <td width="20%">
                                                <asp:Label ID="lblsmsrefno" runat="server" CssClass="tableHeaderTitle">WhatsApp To     :</asp:Label>&#160;
                                                <asp:Label ID="lblsmsgrefno" runat="server" class="standardlabel3" Text='<%# Eval("SMSTo") %>'></asp:Label>
                                            </td>
                                            <td width="20%">
                                                <asp:Label ID="lblsmstype" runat="server" class="tableHeaderTitle">Send Date :</asp:Label>&#160;
                                                <asp:Label ID="lblMsgtype" runat="server" class="standardlabel3" Text='<%# Eval("SMSSendDate") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="20%">
                                                <asp:Label ID="Label9" runat="server" class="tableHeaderTitle">WhatsApp Messsage Status :</asp:Label>&#160;
                                                <asp:Label ID="Label10" runat="server" class="standardlabel3" Text='<%# Eval("SMSResponse") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="20%">
                                                <asp:Label ID="lblsmscontent" runat="server" class="tableHeaderTitle">Message Template ID :</asp:Label>&#160;
                                                <asp:Label ID="lblResponse" runat="server" class="standardlabel3" Text='<%# Eval("HSMName") %>'></asp:Label>
                                            </td>
                                        </tr>
                                    </td>
                                </tr>
                            </table>
                            <table id="Table45" width='100%' bgcolor="#DCDCDC">
                                <td width='100%'>
                                    <asp:Label ID="Label11" runat="server" class="tableHeaderTitle">Dynamic Field :</asp:Label>&#160;
                                    <asp:Label ID="Label12" runat="server" class="standardlabel3" Text='<%# Eval("TemValue") %>'></asp:Label>
                                </td>
                            </table>
                            <table id="Table2" width='100%'>
                                <td width="100%">
                                    <asp:Label ID="Label6" runat="server" class="tableHeaderTitle">WhatsApp SMS Content :</asp:Label>&#160;
                                    <br />
                                    <br />
                                    <asp:Label ID="Label8" runat="server" class="standardlabel3" Text='<%# Eval("SMSContent") %>'></asp:Label>
                                </td>
                                <tr>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="WhatsApp Event" ItemStyle-Wrap="true">
                        <ItemTemplate>
                            <asp:Label ID="lbleventName" runat="server" Text='<%# Eval("eventName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" CssClass="pad" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    </div>

    <%--Ended By tushar For WhatsApp--%>
</asp:Content>
