<%@ Page Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="~/Application/CKYC/CKYCSecuredSearch_NEW.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.CKYC.CKYCSecuredSearch_NEW" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/bundles/CKYCValidationjs") %>
    <%: Styles.Render("~/bundles/CKYCcss") %>
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
    </style>
    <script type="text/javascript">

        function AllowOnlyNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
        }

    </script>
    <script type="text/javascript">
        function AlertMsg(msg) {
            debugger;
            showModal('#myModal', 'Information', 'alert-warning', '', '', msg);
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server">
    </asp:ScriptManager>
    <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
        <div id="divPrrofOfIdenty" runat="server" class="panel-heading" onclick="showHideDiv('divProofOfIdenti','lblPOfIdentity');return false;">
            <div class="row">
                <div class="col-sm-10" style="text-align: left; top: 0px; left: 0px;">
                    <span class="glyphicon glyphicon-menu-hamburger"></span>
                    <asp:Label ID="PrrofOfIdenti" Text="PROOF OF IDENTITY (PoI)" runat="server" CssClass="control-label">
                    </asp:Label>
                    <span style="color: red">*</span>
                </div>
                <div class="col-sm-2" style="text-align: right">
                    <span id="lblPOfIdentity" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                </div>
            </div>
        </div>
        <div id="divProofOfIdenti" style="display: block;" class="panel-body">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="row" style="margin-bottom: 5px">
                        <div class="col-sm-3" style="text-align: left">
                            <asp:Label ID="lblCertifiecopy" Text="(Certified copy of any one the following Proof of identity [POI] needs to be submitted)" runat="server" Font-Bold="false"></asp:Label>
                        </div>
                        <div class="col-sm-3">
                            <asp:DropDownList ID="ddlCertifiecopy" runat="server" CssClass="form-control" TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddlCertifiecopy_SelectedIndexChanged">
                                <asp:ListItem Value="" Text="Select"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row" style="margin-bottom: 5px">
                        <div id="divIdProof" runat="server">
                            <div id="div2" class="col-sm-3" runat="server" style="text-align: left">
                                <asp:Label ID="lblPassportNo" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>

                            <div id="divPassNotxt" runat="server" class="col-sm-3">
                                <asp:TextBox ID="txtPassNo" AutoPostBack="true" runat="server" MaxLength="50" TabIndex="2"
                                    CssClass="form-control" Font-Bold="false" onChange="javascript:this.value=this.value.toUpperCase();">
                                </asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="divAdharD" runat="server" visible="false">
                        <div id="div3" class="col-sm-3" runat="server" style="text-align: left">
                            <asp:Label ID="lblAppName" Text="Applicant Full Name" runat="server" CssClass="control-label"></asp:Label>
                            <span style="color: red">*</span>
                        </div>
                        <div id="div4" runat="server" class="col-sm-3">
                            <asp:TextBox ID="txtname" AutoPostBack="true" runat="server" MaxLength="50" TabIndex="2"
                                CssClass="form-control" Font-Bold="false" Placeholder="FIRST NAME" onChange="javascript:this.value=this.value.toUpperCase();">
                            </asp:TextBox>
                        </div>
                        <div id="div5" runat="server" class="col-sm-3">
                            <asp:TextBox ID="txtmname" AutoPostBack="true" runat="server" MaxLength="50" TabIndex="2"
                                CssClass="form-control" Font-Bold="false" Placeholder="MIDDLE NAME" onChange="javascript:this.value=this.value.toUpperCase();">
                            </asp:TextBox>
                        </div>
                        <div id="div6" runat="server" class="col-sm-3">
                            <asp:TextBox ID="txtlname" AutoPostBack="true" runat="server" MaxLength="50" TabIndex="2"
                                CssClass="form-control" Font-Bold="false" Placeholder="LAST NAME" onChange="javascript:this.value=this.value.toUpperCase();">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div id="divAdharD2" runat="server" class="row" visible="false">
                        <div id="div1" class="col-sm-3" runat="server" style="text-align: left">
                            <asp:Label ID="Label1" runat="server" Text="Date Of Birth" CssClass="control-label"></asp:Label>
                            <span style="color: red">*</span>
                        </div>
                        <div id="div7" runat="server" class="col-sm-3">
                            <asp:TextBox ID="dob" AutoPostBack="true" runat="server" MaxLength="50" TabIndex="2"
                                CssClass="form-control" Font-Bold="false" onmousedown="$('#EmptyPagePlaceholder_txtPassExpDateAdd').datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' });">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div id="div8" runat="server" class="row" visible="false">
                        <div id="div9" class="col-sm-3" runat="server" style="text-align: left">
                            <asp:Label ID="Label2" runat="server" Text="Gender" CssClass="control-label"></asp:Label>
                            <span style="color: red">*</span>
                        </div>
                        <div id="div10" runat="server" class="col-sm-3">
                            <asp:DropDownList ID="ddlgender" runat="server" CssClass="form-control" TabIndex="2">
                                <asp:ListItem Value="S" Text="Select"></asp:ListItem>
                                <asp:ListItem Value="M" Text="Male"></asp:ListItem>
                                <asp:ListItem Value="F" Text="Female"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="row">
                        <center>
                       <div class="col-sm-12" style='margin-top: 15px;'>
                        <div style="width: 20%; white-space: nowrap;">
                            <asp:Button ID="btnSearch" Text="Search" CssClass="btn-animated bg-green" runat="server" OnClick="btnSearch_Click">
                                <%--OnClientClick=" return ValidateDate(); return true;"--%>
                            </asp:Button>
                            <asp:Button ID="btnClear" CssClass="btn-animated bg-horrible" Text="Clear" runat="server" OnClick="btnClear_Click"></asp:Button>
                            <%--OnClick="btnClear_Click"--%>
                        </div>
                    </div>
                      </center>
                    </div>

                    <div class="col-sm-12" style="text-align: center">
                        <asp:Label ID="lblMessage" runat="server" ForeColor="red" Visible="False" Width="310px"></asp:Label>
                    </div>

                    <div class="panel  panel-success" id="divSearchDetails" runat="server" style="display:none">
                            <div class="panel-heading" onclick="ShowReqDtl('ctl00_ContentPlaceHolder1_trdg','btnDeptMstGrd');return false;">
                                <div class="row" id="trdgHdr" runat="server" visible="false">
                                    <div class="col-sm-10" style="text-align: left">
                                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                                        <asp:Label ID="lblSearch" Text="Search Result" runat="server" Font-Size="Small"></asp:Label>
                                    </div>
                                    <div class="col-sm-2">
                                        <%--<span id="btnDeptMstGrd" class="glyphicon glyphicon-collapse-down" style="float: right; color: Orange; padding: 1px 10px ! important; font-size: 18px;"></span>--%>
                                        <span id="btnDeptMstGrd" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                                    </div>
                                </div>
                            </div>
                            <div id="trdg" runat="server" visible="false">
                                <div>
                                    <div>
                                        <asp:GridView ID="dgDownload" runat="server" AutoGenerateColumns="false" CssClass="footable"
                                            PageSize="10" AllowSorting="False">
                                            <RowStyle CssClass="GridViewRow"></RowStyle>
                                            <HeaderStyle CssClass="gridview th" />
                                            <PagerStyle CssClass="disablepage" />
                                            <Columns>
                                                <asp:BoundField DataField="CKYC_NO" HeaderText="CKYC_NO" />
                                                <asp:BoundField DataField="NAME" HeaderText="name" />
                                                <asp:BoundField DataField="FATHERS_NAME" HeaderText="FATHERS_NAME" />
                                                <asp:BoundField DataField="AGE" HeaderText="AGE" />
                                                <asp:BoundField DataField="IMAGE_TYPE" HeaderText="IMAGE_TYPE" />
                                                <%--<asp:BoundField DataField="PHOTO" HeaderText="PHOTO" />--%>
                                                <asp:TemplateField HeaderText="View">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="CommandBtn" runat="server" ForeColor="Blue" Text="View Photo" CommandArgument='<%# Eval("PHOTO") %>' OnCommand="CommandBtn_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="KYC_DATE" HeaderText="KYC_DATE" />
                                                <asp:BoundField DataField="UPDATED_DATE" HeaderText="UPDATED_DATE" />
                                                <asp:BoundField DataField="REMARKS" HeaderText="REMARKS" />
                                                <asp:BoundField DataField="PID_ID" HeaderText="PID_ID" />
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
                    <div class="row" id="div11" runat="server" style="display: none">
                        <div class="panel  panel-success">
                            <div class="panel-heading subheader" onclick="ShowReqDtl('ctl00_ContentPlaceHolder1_trdg','btnDeptMstGrd');return false;"
                                style="background-color: #EDF1cc !important;">
                                <div class="row" id="Dwnld" runat="server" visible="false">
                                    <div class="col-sm-10" style="text-align: left">
                                        <span class="glyphicon glyphicon-menu-hamburger" style="color: Orange;"></span>
                                        <asp:Label ID="Label3" runat="server" Font-Size="Small"></asp:Label>
                                    </div>
                                    <div class="col-sm-2">
                                        <span id="Span2" class="glyphicon glyphicon-collapse-down" style="float: right; color: Orange; padding: 1px 10px ! important; font-size: 18px;"></span>
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
                        <center>
                        <div class="col-sm-12" style='margin-top: 15px;'>
                            <%-- <asp:LinkButton ID="LinkButton1" OnClick="btnSearch_Click" OnClientClick="funload();"
                                CssClass="btn btn-primary" runat="server">
                                <asp:HiddenField ID="TabName" runat="server" />
                                <span class="glyphicon glyphicon-search BtnGlyphicon"></span>Search
                            </asp:LinkButton>--%>
                            <asp:LinkButton ID="btnExport" Visible="false" Text="Download" runat="server" CssClass="btn btn-primary"
                                OnClick="btnExport_Click">
                            </asp:LinkButton>
                            <%-- <asp:LinkButton ID="btnpopcancel" runat="server" CssClass="btn btn-danger"
                                OnClick="btnpopcancel_Click"> <span class="glyphicon glyphicon-remove BtnGlyphicon"></span>Cancel
                            </asp:LinkButton>--%>
                        </div>
                            </center>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <%--<asp:HiddenField ID="hdnBatchid" runat="server" />--%>
</asp:Content>
