<%@ Page Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="IdentityCodeMaster.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.CKYC.IdentityCodeMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/bundles/CKYCValidationjs") %>
    <%: Styles.Render("~/bundles/CKYCcss") %>
    <style type="text/css">
        .Lpad{
            /*padding-left:10px;*/
            height: 40px;
            text-transform: uppercase;
                white-space: nowrap;
        }
        .Center{
            text-align:center!important;
        }
        .left{
            text-align:left!important;
        }
    </style>
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div>
                <div class="" style="margin-left: 0px; margin-right: 0px; background-color: transparent !important;">
<%--                    <div id="Div21" runat="server" class="panel-heading">
                        <div class="row">
                            <div class="col-sm-10" style="text-align: left">
                                <span class="glyphicon glyphicon-menu-hamburger"></span>
                                <asp:Label ID="lblDtlOfRtltpr" Text="Identity Code Master"
                                    runat="server" CssClass="control-label">
                                </asp:Label>
                            </div>
                            <div class="col-sm-2" onclick="showHideDiv('menu4','Span10');return false;">
                                <span id="Span10" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div>--%>
                    <div style="background-color: #d5d5c3; display: none;" id="DivSrcReqDtlsNote4">
                        <div class="row">
                            <div class="col-sm-12" style="margin-left: 17px; margin-top: 10px;">
                                <span class="glyphicon glyphicon-alert" style="color: yellow"></span>&#160;&#160;
                           
                                <span id="SrcReqDtlsNote4" style="text-align: justify; font-size: 11px;"></span>
                            </div>
                        </div>
                    </div>
                    <div id="menu4" style="padding-left: 30px; display: block; padding-right: 30px; text-align: -webkit-center;" class="panel-body">
                        <%--  Added for Details of Related Person start--%>
                        <div class="row">
                        </div>
                        <div class="row" style="width:90%;">
                            <div id="div12" class="col-sm-12" style="text-align: center" runat="server">
                                <asp:Label ID="lblRelRecordShow" Style="text-align: center" Text="  No Records Found" Visible="false" runat="server" />
                            </div>
                            <asp:GridView ID="gvIdentityCodeMaster" Width="100%" runat="server" AllowSorting="True" CssClass="footable"
                                PageSize="10" AllowPaging="true" CellPadding="1"
                                AutoGenerateColumns="False">
                                <RowStyle CssClass="GridViewRow"></RowStyle>
                                <%--<PagerStyle CssClass="disablepage" />--%>
                                <%--OnPageIndexChanging="gvMemDtls_PageIndexChanging" OnRowCreated="gvMemDtls_RowCreated"--%>
                                <FooterStyle CssClass="GridViewFooter" />
                                <HeaderStyle HorizontalAlign="Center" BackColor="#00b4bf" />
                                <SelectedRowStyle CssClass="GridViewSelectedRow" />
                                <AlternatingRowStyle CssClass="GridViewAlternateRow"></AlternatingRowStyle>
                                <Columns>
                                    <%-- <asp:TemplateField HeaderText="Sr. No." ItemStyle-Width="10%" SortExpression="Reference No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Identity Code" HeaderStyle-CssClass="Lpad Center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" SortExpression="Reference No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("Code") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad Center" HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Identity Description" HeaderStyle-CssClass="Lpad Center" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" SortExpression="Reference No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRelTypVal" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="pad left" HorizontalAlign="Left" Width="30%" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <br />
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
