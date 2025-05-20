<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="CKYCQC.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.CKYC.CKYCQC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/bundles/CKYCValidationjs") %>
    <%: Styles.Render("~/bundles/CKYCcss") %>
     <style>
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
    </style>
    <script type="text/javascript">
        function AlertMsg(msg) {
            debugger;
            showModal('#myModal', 'Information', 'alert-warning', '', '', msg);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">
    <center>
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
                             <div class="col-sm-2" onclick="showHideDiv('divCKYCdtls','btnToggle');return false;">
                        <span id="btnToggle" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                    </div>
                        </div>
                    </div>
                    <div id="divCKYCdtls" style="display: block;" runat="server" class="panel-body">
                     <div class="row">
        <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblRefNumber" Text="Reference NO." runat="server" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:TextBox ID="txtRefNumber" runat="server" CssClass="form-control" Enabled="false"
                            Font-Bold="false" TabIndex="2">
                        </asp:TextBox>
                            </div>
                              <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblKYCNumber" Text="KYC NO." runat="server" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-sm-3" style="text-align: left; margin-bottom:5px;">
                                    <asp:TextBox ID="txtKYCNumber" runat="server" CssClass="form-control" Enabled="false"
                            Font-Bold="false" TabIndex="2">
                        </asp:TextBox>
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
                                <asp:CheckBox ID="cbUpdate" runat="server" CssClass="standardcheckbox" Text="Update" TabIndex="3"
                                    AutoPostBack="true" Enabled="false" />
                            </div>
                             <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblAccountType"  runat="server" Font-Bold="false"></asp:Label>
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
        <div class="col-sm-2" onclick="showHideDiv('divnavigate','btnToggle');return false;">
        <span id="btnnavigate" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
        </div>
        </div>
        </div>
            <div id="divnavigate" style="display:block;" runat="server" class="panel-body">
                <div class="row">
                  <div class="col-sm-12" style="text-align:right">
                        <asp:UpdatePanel runat="server" ID="upnlPrev">
                            <ContentTemplate>
                                <%----%>

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
                          <div id="divloaderqc" class="col-sm-12" runat="server" style="display: none;position:absolute;">
                              <caption>
                                  <img id="Img3" alt="" src="~/images/spinner.gif" runat="server" />
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
                                                  

                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Button ID="btnprev" Text="<" CssClass="form-submit-button" runat="server"
                                                                        Width="40px" Enabled="false" Style="border-style: solid; border-width: 1px; background-repeat: no-repeat;
                                                                        background-color: transparent; float: left; margin: 0; height: 30px;" OnClick="btnprev_Click" />
                                                                  <%--  <asp:TextBox runat="server" ID="txtPage" Text="1" Style="width: 35px; border-style: solid;
                                                                        border-width: 1px; border-color: Gray; height: 30px; float: left; margin: 0;
                                                                        text-align: center;" CssClass="form-control" ReadOnly="true" />--%>
                                                                    <asp:Button ID="btnnext" Text=">" CssClass="form-submit-button" runat="server" Style="border-style: solid;
                                                                        border-width: 1px; background-repeat: no-repeat; background-color: transparent;
                                                                        float: left; margin: 0; height: 30px;" Width="40px" OnClick="btnnext_Click" OnClientClick="funload();" />
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
                               
         <div class="panel panel-success" style="margin-left:0px;margin-right:0px">
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
        <div id="menu1" style="display:block;" runat="server" class="panel-body">
          <div class="panel panel-success" style="margin-left:0px;margin-right:0px">
             <div id="Div2"  runat="server" class="panel-heading subheader" onclick="ShowReqDtl('ctl00_ContentPlaceHolder1_divPersonal','btnpersnl1');return false;"
             >           
        <div class="row">
        <div class="col-sm-10" style="text-align:left">
        <span class="glyphicon glyphicon-menu-hamburger" style="color: white;"></span>
        <asp:Label ID="lblpfPersonal" Text="Personal Details" runat="server"  CssClass="control-label" ></asp:Label>
                 
        </div>
        <div class="col-sm-2">
        <span id="btnpersnl1" class="glyphicon glyphicon-collapse-down" style="float: right;s
        padding: 1px 10px ! important; font-size: 18px;"></span>
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
                                        <span style="color: red">*</span>
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
                                        <asp:Label ID="Label1" Text="PAN No" runat="server" CssClass="control-label">
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
                                    <div class="col-sm-3">
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

           </div>
                <%--  Added for Personal Details end --%>
                         
        <%--  Added for Tick If Applicable start --%>
            <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px;display:none">
                            <div id="Div1" runat="server" class="panel-heading subheader" 
                                onclick="ShowReqDtl1('ctl00_ContentPlaceHolder1_div3','Span1');return false;">
                                <div class="row">
                                    <div class="col-sm-10" style="text-align: left">
                                        <span class="glyphicon glyphicon-menu-hamburger" style="color: white;"></span>
                                        <asp:Label ID="lbltick" Text="" runat="server" CssClass="control-label">
                                        </asp:Label>&nbsp;&nbsp;
                                        <%-- <asp:CheckBox ID="chkTick" Text=" RESIDENCE FOR TAX PURPOSES IN JURISDICTIONS(S) OUTSIDE INDIA" OnCheckedChanged="chkTick_Checked"
                                            CssClass="standardcheckbox"  runat="server" />--%>
                                    </div>
                                    <div class="col-sm-2">
                                        <span id="Span1" class="glyphicon glyphicon-resize-full" style="float: right; color: white;
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
            </div>
            </div>

         <div class="panel panel-success" style="margin-left:0px;margin-right:0px">
        <div id="Div20"  runat="server" class="panel-heading" onclick="ShowReqDtl('ctl00_ContentPlaceHolder1_menu2','btnId');return false;">           
        <div class="row">
        <div class="col-sm-10" style="text-align:left">
        <span class="glyphicon glyphicon-menu-hamburger"></span>
        <asp:Label ID="Label10" Text="PROOF OF IDENTITY(POI)" runat="server"  CssClass="control-label" ></asp:Label>
                 
        </div>
        <div class="col-sm-2" onclick="showHideDiv('menu2','btnId');return false;">
        <span id="btnId" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
        </div>
        </div>
        </div>               
       
        <div id="menu2" style="display:block;" runat="server" class="panel-body">
        <%--  Added for Proof of Identity start--%>
              
           
                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblProof" Text="(Certified copy of any one the following Proof of Identity [Pol] needs to be submitted)" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font> </span>
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
                            <div id="divPassNo" runat="server" class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblPassportNo" Placeholder="Passport Number" runat="server" CssClass="control-label"></asp:Label>
                                <span><font color="red">*</font> </span>
                            </div>
                            <div id="divPassNotxt" runat="server" class="col-sm-3">
                                <asp:TextBox CssClass="form-control"  runat="server" TabIndex="37"
                                    ID="txtPassNo" MaxLength="15" />
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
                                  onmousedown="$('#ctl00_ContentPlaceHolder1_txtPassExpDate').datepicker({ changeMonth: true, changeYear: true });"
                                                            onchange="setDateFormat('ctl00_ContentPlaceHolder1_txtPassExpDate')"   />
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
        </div>
        <%-- Added for Proof of Identity end--%>
         </div>   
                    

         <div class="panel panel-success" style="margin-left:0px;margin-right:0px">
      
            <div id="Div5" runat="server" class="panel-heading">
                <div class="row">
                    <div class="col-sm-10" style="text-align: left">
                        <span class="glyphicon glyphicon-menu-hamburger" ></span>
                        <asp:Label ID="lblpfofAddr1" Text="" runat="server" CssClass="control-label">
                        </asp:Label>
                    </div>
                    <div class="col-sm-2" onclick="showHideDiv('menu3','Span9');return false;">
                        <span id="Span9" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                    </div>
                </div>
            </div>
            <div id="menu3" style="display: block;" runat="server" class="panel-body">
                <asp:UpdatePanel ID="upMenu3" runat="server">
                    <ContentTemplate>
                        <%--  Added for Proof of Address start--%>
                        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                            <%--below div16 Commneted by rutuja--%>
<%--                            <div id="Div6" runat="server" class="panel-heading subheader" >
                                <div class="row">
                                    <div class="col-sm-10" style="text-align: left">
                                        <span class="glyphicon glyphicon-menu-hamburger" style="color: white;"></span>
                                        
                                        <asp:Label ID="lblpfofAddr2" Text="" runat="server" CssClass="control-label"></asp:Label>
                                    </div>
                                    <div class="col-sm-2" onclick="ShowReqDtl1('ctl00_ContentPlaceHolder1_div7','Span2');return false;">
                                        <span id="Span2" class="glyphicon glyphicon-resize-full" style="float: right; color: white;
                                            padding: 1px 10px ! important; font-size: 18px;"></span>
                                    </div>
                                </div>
                            </div>--%>
                            <div id="div7" style="display: block;" runat="server" class="panel-body">
                                <div class="row">
                                    <div class="col-sm-12" style="text-align: left">
                                        <asp:CheckBox ID="chkPerAddress" Text="CURRENT/PERMANENT/OVERSEAS ADDRESS DETAILS"
                                            AutoPostBack="true" OnCheckedChanged="chkPerAddress_Checked" runat="server" CssClass="control-label"
                                            TabIndex="40" />
                                        <span><font color="red">*</font> </span>
                                    </div>
                                </div>
                                <div class="row">
                                   <%-- <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblAddressType" Text="" runat="server" CssClass="control-label"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlAddressType" runat="server" CssClass="form-control" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlAddressType_SelectedIndexChanged" TabIndex="41">
                                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>--%>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblProofOfAddress" Text="" runat="server" CssClass="control-label"></asp:Label>
                                        
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlProofOfAddress" runat="server" CssClass="form-control" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlProofOfAddress_SelectedIndexChanged" TabIndex="42">
                                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div id="divAddProof" runat="server" class="row">
                                    <div id="divPassNoAdd" runat="server" class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblPassportNoAdd" Placeholder="" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font> </span>
                                    </div>
                                    <div id="divPassNotxtAdd" runat="server" class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" runat="server" onChange="javascript:this.value=this.value.toUpperCase();"
                                            ID="txtPassNoAdd" MaxLength="15" TabIndex="43" />
                                        <%--<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                            FilterType="Numbers,UppercaseLetters,LowercaseLetters" TargetControlID="txtPassNoAdd">
                                        </ajaxToolkit:FilteredTextBoxExtender>--%>
                                    </div>
                                    <div id="divPassAdd" runat="server" class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="llPassExpDateAdd" runat="server" CssClass="control-label"></asp:Label>
                                        <%--<span><font color="red">*</font> </span>--%>
                                    </div>
                                    <div id="divPassDateAdd" runat="server" class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" onmousedown="$('#ctl00_ContentPlaceHolder1_txtPassExpDateAdd').datepicker({ changeMonth: true, changeYear: true });"
                                     runat="server"
                                            ID="txtPassExpDateAdd" MaxLength="15" TabIndex="44" />
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtPassOthrAdd" MaxLength="15"
                                            TabIndex="45" />
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
                                        <asp:DropDownList ID="ddlPinCode" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPinCode_SelectedIndexChanged"
                                            AutoPostBack="True" TabIndex="51" Enabled="false" > <%--Enabled="false" added by rutuja --%>
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
                                                CssClass="control-label" TabIndex="54" />
                                            <span><font color="red">*</font> </span>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12" style="text-align: left">
                                            <asp:CheckBox ID="chkCuurentAddress" Text="Same as Current/Permanent/Overseas Address details"
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
                                            <asp:DropDownList ID="ddlPinCode1" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPinCode1_SelectedIndexChanged"
                                                AutoPostBack="True" TabIndex="61" Enabled="false"> <%--Enabled="false" added by rutuja --%>
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
                        <%-- Added for Proof of Address end--%>
                        <%--  Added for Contact Details start--%>
                     <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                            <div id="Div8" runat="server" class="panel-heading subheader" >
                                <div class="row">
                                    <div class="col-sm-10" style="text-align: left">
                                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                                       
                                        <asp:Label ID="lblContactDetails" Text="CONTACT DETAILS(All communication will be sent on provided MobileNo./Email-ID)" 
                                            runat="server" CssClass="control-label"></asp:Label> <%-- Text added by Rutuja --%>
                                    </div>
                                    <div class="col-sm-2" onclick="ShowReqDtl1('div9','Span3');return false;">
                                        <span id="Span3" class="glyphicon glyphicon-resize-full" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                                    </div>
                                </div>
                            </div>
                            <div id="div9" style="display: block;" class="panel-body">
                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblTelOff1" runat="server" Text="" CssClass="control-label"></asp:Label>
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
                                        <asp:Label ID="lblMobile" runat="server" Text="" CssClass="control-label"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="input-group">
                                            <span class="input-group-addon input-group-addon-tel">
                                                <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" TabIndex="79" onkeypress="fncInputNumericValuesOnly();" MaxLength="3" Style="border-top-left-radius: 4px; border-bottom-left-radius: 4px;"></asp:TextBox>
                                            </span>
                                            <asp:TextBox ID="txtMobile2" runat="server" CssClass="form-control" onkeypress="fncInputNumericValuesOnly();"
                                                MaxLength="10" TabIndex="80"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left; display:none"> <%-- None by Rutuja--%>
                                        <asp:Label ID="lblFax" runat="server" Text="" CssClass="control-label"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                    <div class="input-group">
                                        <span class="input-group-addon input-group-addon-tel">
                                            <asp:TextBox ID="txtFax1" runat="server" CssClass="form-control" Visible="false" TabIndex="81" onkeypress="fncInputNumericValuesOnly();" MaxLength="4" Style="border-top-left-radius: 4px; border-bottom-left-radius: 4px;"></asp:TextBox>
                                            </span>
                                            <asp:TextBox ID="txtFax2" runat="server" CssClass="form-control"  Visible="false" MaxLength="10" TabIndex="82" onkeypress="fncInputNumericValuesOnly();"></asp:TextBox>
                                        </div> <%-- Visible false by Rutuja--%>
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
                                </div>
                                </div>
                                
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <%--  Added for Contact Details end--%>
            </div>
        </div>

         <%--<div class="panel panel-success" style="margin-left:0px;margin-right:0px">
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
                        <div class="row">
                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblRelType" Text="Related Person Type" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font> </span>
                                    </div>
                        <div class="col-sm-3">
                                    <asp:DropDownList  id="ddlRelType" runat="server" CssClass="form-control" TabIndex="78"></asp:DropDownList>

                                    </div>
                    <div class="col-sm-3">
                                        <asp:Label ID="lblKYCNum" Text="KYC Number of Related Person(if available)" Placeholder="Passport Number" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font> </span>
                                    </div>
                        <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtKYCNum')" runat="server" ID="txtKYCNum" TabIndex="79" />
                                    </div>
                    </div>

                    <div class="row">
        <div class="col-sm-3" style="text-align:left">
                                                   
                                <asp:Label ID="txtName1" Text="Name" runat="server" CssClass="control-label" ></asp:Label>
                                <span style="color: red">  *</span>
                            </div>
        <div class="col-sm-9">
                            
        <div style="display:flex;">    
         <asp:DropDownList  id="ddlPrefix" runat="server" CssClass="form-control"    TabIndex="80"></asp:DropDownList> 
                                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"  placeholder="First Name" TabIndex="81" ></asp:TextBox>&nbsp;
                                            <asp:TextBox ID="txtMidddleName" runat="server" CssClass="form-control"  placeholder="Middle Name" TabIndex="82"></asp:TextBox>&nbsp;
                                            <asp:TextBox ID="txtSurName" runat="server" CssClass="form-control"  placeholder="Last Name" TabIndex="83"
                                            MaxLength="30"  ></asp:TextBox>
                                    
                                                       
                                        </div>
        </div>
        </div>

                    <br />

                    <div class="row">
                 
                        <div class="col-sm-3" style="text-align:left">
                                                   
                                  <asp:Label ID="lblProofOfIdentity11" Text="" runat="server"
                            CssClass="control-label"></asp:Label>
                                <span style="color: red">  *</span>
                            </div>
                            <div class="col-sm-3">
                                               

                                    <asp:DropDownList  id="ddlProofRelPerson" runat="server" CssClass="form-control" TabIndex="84" ></asp:DropDownList>
                                    </div>
                
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblPassNo2"  Placeholder="Passport Number" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font> </span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtPassNo2')" runat="server" ID="txtPassNo2" MaxLength="15"
                                        TabIndex="85" />
                                    </div>

                </div>

                                    <div class="row" style='text-align:left'>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblPassExpDate1"  runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font> </span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control"  onmousedown="$('#txtPassExpDate').datepicker({ changeMonth: true, changeYear: true });"
        onchange="setDateFormat('txtPassExpDate')" runat="server" ID="txtPassExpDate1" MaxLength="15"
        TabIndex="87" />
                                    </div>
                                    </div>
               
                </div>
                </div>--%>

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
        <div class="col-sm-2"  onclick="showHideDiv('menu6','btnAtts');return false;">
        <span id="btnAtts" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
        </div>
        </div>
        </div> 

        <div id="menu6" style="display:block;" runat="server" class="panel-body">
                <%--  Added for Applicant Declaration start--%>
                        <div class="panel panel-success" style="margin-left:0px;margin-right:0px">
                <div id="Div14"  runat="server" class="panel-heading subheader" onclick="ShowReqDtl('ctl00_ContentPlaceHolder1_div15','btnApp');return false;"
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
                                <asp:CheckBox ID="chkAppDeclare1" Text="I hereby declare that the details furnished above are true and correct to the best of my knowledge and belief and I undertake  to inform you of any changes "
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
                                <asp:CheckBox ID="chkAppDeclare2" Text="I hereby consent to receiving information from Central KYC Registry through SMS/Email on the above registered number/email address"
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
                                <asp:TextBox CssClass="form-control" onmousedown="$('#ctl00_ContentPlaceHolder1_txtDate').datepicker({ changeMonth: true, changeYear: true });"
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
                <div id="Div16"  runat="server" class="panel-heading subheader" onclick="ShowReqDtl('ctl00_ContentPlaceHolder1_div17','btnOffc');return false;"
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

       
         <div class="row" style="margin-top:12px;">
        <div class="col-sm-12" align="center">
        <asp:Button ID="btnUpdate" runat="server" CssClass="btn-animated bg-green"  OnClick="btnUpdate_Click" Text="Approve">
                        </asp:Button>
             <asp:Button ID="btnReject" runat="server" CssClass="btn-animated bg-horrible"  OnClick="btnReject_Click" Text="Reject">
                        </asp:Button>
              
        <div id="divloader" runat="server" style="display:none;">
                <img id="Img1" alt="" src="~/images/spinner.gif" runat="server" /> Loading...
        </div>
        </div>
        </div>
</div>
          
        </center>
         <div id="DivFloat" clientidmode="Static" runat="server">

           
        </div>
          <asp:HiddenField ID="hdnRegRefNo" runat="server" />     
          <input id="hdnUpdate" type="hidden" runat="server" />
          <!-- Display Modal popup window in division -->
         <div class="modal fade" id="myModal" role="dialog">
         </div>
         <!-- End Display Modal popup window in division -->

        <div id="myModalImage" class="modal" style="padding-top:10px"  >
 
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
        <asp:HiddenField ID="hdnImgId" runat="server" />
         <asp:HiddenField ID="hdnHt" runat="server" />
    <asp:HiddenField ID="hdnWt" runat="server" />
        <asp:HiddenField ID="hdndoccode" runat="server" />
    </center>
</asp:Content>
