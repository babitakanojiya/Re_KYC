<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="RegisterUser.aspx.cs" Inherits="KMI.FRMWRK.Web.Account.RegisterUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">
    <style type="text/css">

         a {
             color: rgba(21, 62, 60, 0.93); 
              
        }

        ul#menu {
            padding: 0;
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
                        background-color: #F55856;
                    }

    </style>
    <script type="text/javascript" language="javascript">
        function ClearSearch() {
            var answer = confirm("jump to another javascript page")
            if (answer) {
                alert("yes");
                return true;
            }
            else {
                alert("no");
                return false;
            }
        }
        function enableDisable(strValue) {

        }


        function ChangeToUpper() {
            key = window.event.which || window.event.keyCode;
            if ((key > 0x60) && (key < 0x7B))
                window.event.keyCode = key - 0x20;
        }

        function SelectSingleRadiobutton(rdbtnid) {
            debugger;
            var rdBtnInternal = document.getElementById("ctl00_ContentPlaceHolder1_rdInternal");
            var rdBtnExternal = document.getElementById("ctl00_ContentPlaceHolder1_rdExternal");
            var rdBtn = document.getElementById(rdbtnid);
            var rdBtnList = document.getElementsByTagName("input");
            for (i = 0; i < rdBtnList.length; i++) {
                if (rdBtnList[i].type == "radio" && rdBtnList[i].id != rdBtn.id && rdBtnList[i].id != rdBtnInternal.id && rdBtnList[i].id != rdBtnExternal.id) {
                    rdBtnList[i].checked = false;
                }
            }
        }

    </script>
    <br />
    <div class="panel panel-success" style="margin-left: 2%; margin-right: 1%;">

        <div id="div1" runat="server" class="panel panel-heading" onclick="ShowReqDtl('divSearch','btnWfParam');return false;">
            <div id="Td1" class="row">
                <div class="col-sm-10">
                    <span class="glyphicon glyphicon-menu-hamburger"></span>
                    <asp:Label ID="lblTitle" runat="server"></asp:Label>
                </div>
                <div class="col-sm-2">

                    <div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:Label ID="lblDplUserId" runat="server" CssClass="control-label"></asp:Label>

                                <asp:Label ID="lblErrorMsg" runat="server" Visible="false" CssClass="control-label"></asp:Label>

                                <asp:Label ID="lblModVer" Visible="false" runat="server" Text="" CssClass="control-label"></asp:Label>
                                 <span id="btnWfParam" class="glyphicon glyphicon-collapse-down" style="float: right;  padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>

                        </div>
                    </div>
                   
                </div>
            </div>
        </div>

        <div id="divSearch" class="panel-body">
            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                <asp:View ID="View1" runat="server">
                    <div style="margin-left: 1%;">
                        <ul id="menu">
                            <li class="current">
                                <asp:LinkButton ID="L1" runat="server" CssClass="control-lable" OnClick="v1_click">User Details</asp:LinkButton></li>
                            <li>
                                <asp:LinkButton ID="L3" runat="server" CssClass="control-lable" OnClick="v3_click">Service Sanctioning</asp:LinkButton></li>
                        </ul>
                        <!--<li><asp:LinkButton ID="L2" runat="server" OnClick="v2_click">User Sanctioning</asp:LinkButton></li></ul>-->
                        <div id="Search" runat="server" style="text-align: center; text-align: center; margin-left: -3%;" class="panel-body">
                            <div class="row">
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblUserType" runat="server" CssClass="control-lable"></asp:Label>
                                </div>
                                <%--  <div class="col-sm-3">
                        </div>--%>
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:RadioButton ID="rdInternal" runat="server" GroupName="Internal" OnCheckedChanged="rdInternal_CheckedChanged"
                                        AutoPostBack="true" />
                                    Internal
                            <asp:RadioButton ID="rdExternal" runat="server" GroupName="Internal" OnCheckedChanged="rdInternal_CheckedChanged"
                                AutoPostBack="true" />
                                    External
                                </div>
                                <div class="col-sm-3" style="text-align: left;">
                                    <asp:Label ID="lblUserName" runat="server" CssClass="control-lable"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" onKeypress="ChangeToUpper();"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please insert user name."
                                        Display="Dynamic" Font-Size="8pt" CssClass="msgerror" ControlToValidate="txtUserName"
                                        SetFocusOnError="true" ValidationGroup="NewsUserGroup"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblUserID" runat="server" CssClass="control-lable">
                             
                                    </asp:Label>
                                </div>
                                <div class="col-sm-3">
                                   <%-- <div class="row">
                                        <div class="col-sm-12" style="display: flex; text-align: left; margin-left: -8%;">--%>
                                            <asp:TextBox ID="txtUserID" runat="server" CssClass="form-control" onKeypress="ChangeToUpper();"></asp:TextBox>
                                   <%-- <span class="glyphicon glyphicon-search"></span>&nbsp;
                                        <asp:LinkButton ID="btnCheckID" runat="server" OnClick="btnCheckID_Click" CausesValidation="true"
                                            ValidationGroup="checkUserGroup">
                                                  
                                        </asp:LinkButton>--%>
                                       <%-- </div>
                                    </div>--%>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please insert a user ID."
                                        Display="Dynamic" Font-Size="8pt" CssClass="msgerror" ControlToValidate="txtUserID"
                                        SetFocusOnError="true" ValidationGroup="checkUserGroup"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="regExpValUserName" runat="server" ControlToValidate="txtUserID"
                                        Display="Dynamic" ErrorMessage="Alphanumeric only!" SetFocusOnError="True" ValidationExpression="\w*\d*"
                                        ValidationGroup="checkUserGroup"></asp:RegularExpressionValidator>
                                    <asp:Label ID="lblerror" runat="server" CssClass="msgerror" Font-Size="8pt" Visible="False"></asp:Label>
                                    <asp:Label ID="lblSuccess" runat="server" CssClass="msgsuccess" Font-Size="8pt" Visible="False"></asp:Label>
                                </div>


                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblPwd" runat="server" CssClass="control-lable"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtPwd" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblConfirmPwd" runat="server" CssClass="control-lable"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtConfirmPwd" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblStatus" runat="server" CssClass="control-lable"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="cboStatus" runat="server" DataSourceID="SqlDataSource1" DataTextField="ParamName01"
                                        DataValueField="paramValue" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblLogonName" runat="server" CssClass="control-lable"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtLogonName" runat="server" CssClass="form-control" onKeypress="ChangeToUpper();"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please insert logon name."
                                        Display="Dynamic" Font-Size="8pt" CssClass="msgerror" ControlToValidate="txtLogonName"
                                        SetFocusOnError="true" ValidationGroup="NewsUserGroup"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtLogonName"
                                        Display="Dynamic" ErrorMessage="Alphanumeric only!" Font-Size="8pt" CssClass="msgerror"
                                        SetFocusOnError="True" ValidationExpression="\w*\d*" ValidationGroup="NewsUserGroup"></asp:RegularExpressionValidator>
                                </div>
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblLanguage" runat="server" CssClass="control-lable"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="cboLanguage" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="1">English</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblUsrEffectiveDT" runat="server" Text="User Effective Date" CssClass="control-lable"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <%--        <asp:TextBox ID="txtUsrEffectiveDT" runat="server" CssClass="form-control"onfocus="showCalendarControl(this)" 
                              onchange="setDateFormat('ctl00_ContentPlaceHolder1_txtUsrEffectiveDT');calendarControl.hide();" 
                              onkeypress="funInputNumericCharOnly()" maxlength="10">--%>
                                    <asp:TextBox ID="txtUsrEffectiveDT" runat="server" CssClass="form-control" onfocus="showCalendarControl(this)"
                                        onchange="setDateFormat('ctl00_ContentPlaceHolder1_txtUsrCeaseDT');calendarControl.hide();"
                                        onkeypress="funInputNumericCharOnly()" MaxLength="10"></asp:TextBox>
                                </div>

                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblUsrCeaseDT" runat="server" Text="User Cease Date" CssClass="control-lable"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtUsrCeaseDT" runat="server" CssClass="form-control" onfocus="showCalendarControl(this)"
                                        onchange="setDateFormat('ctl00_ContentPlaceHolder1_txtUsrCeaseDT');calendarControl.hide();"
                                        onkeypress="funInputNumericCharOnly()" MaxLength="10"></asp:TextBox>
                                </div>
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblEmail" runat="server" CssClass="control-lable" Text="Email ID"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Enter Valid Email ID"
                                        ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                </div>
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="Label2" runat="server" Text="DOB" CssClass="control-lable"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtDob" runat="server" CssClass="form-control" onfocus="showCalendarControl(this)"
                                        onchange="setDateFormat('ctl00_ContentPlaceHolder1_txtDob');calendarControl.hide();"
                                        onkeypress="funInputNumericCharOnly()" MaxLength="10"></asp:TextBox>
                                </div>



                            </div>

                            <div class="row">
                                <div class="col-sm-12" style="text-align: left; margin-top: -1%; margin-left: -1%;">
                                    <div class="col-sm-3" style="text-align: left;">
                                        <asp:Label ID="lblMobnumber" runat="server" Text="Mobile number" CssClass="control-lable"></asp:Label>
                                    </div>

                                    <div class="col-sm-3" style="text-align: left; margin-left: 0.3%;">
                                        <%--  <asp:TextBox ID="txtMobNumber" runat="server" CssClass="form-control"MaxLength="10" >
                               </asp:TextBox>--%>
                                        <asp:TextBox ID="txtMobNumber" runat="server" CssClass="form-control" Width="104%" MaxLength="10"></asp:TextBox>
                                        <asp:CompareValidator ID="cv" runat="server" ControlToValidate="txtMobNumber" Type="Integer"
                                            Operator="DataTypeCheck" ErrorMessage="Enter valid mobile number" />
                                    </div>

                                </div>







                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblNonLife" runat="server" Text="Non Life User Role" CssClass="control-lable"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="cboNonLife" runat="server" DataSourceID="SqlDataSource5" DataTextField="ParamName01"
                                        DataValueField="paramValue" Width="90%" CssClass="standardDropdown">
                                    </asp:DropDownList>
                                </div>

                            </div>
                            <div>
                            </div>







                        </div>


                        <div runat="server" id="LoadDynamic1" style="overflow: scroll;" class="row">

                            <asp:GridView ID="GridViewApp" runat="server" Width="100%" AutoGenerateColumns="False"
                                CssClass="footable" OnRowDataBound="GridViewApp_RowDataBound1">
                                <Columns>
                                    <%--1 --%>
                                    <asp:TemplateField HeaderText="Application">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkApp" Checked="false" AutoPostBack="true" OnCheckedChanged="chkSelectAll_CheckedChanged"
                                                runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AppName01")%>' />
                                            <asp:HiddenField ID="hdnAppId" runat="server" Value='<%# Eval("appId") %>' />
                                            <asp:HiddenField ID="hdnIsChecked" runat="server" Value='<%# Eval("AppStatus") %>' />
                                            <%--<asp:HiddenField ID="hdnLocType" runat="server"  Value='<%# Eval("LocType") %>'/>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--2 --%>
                                    <asp:TemplateField HeaderText="Location Type">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlLocation_OnSelectedIndexChanged">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>


                                    </asp:TemplateField>
                                    <%--3 --%>
                                    <asp:TemplateField HeaderText="Location code">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlLoactionCode" runat="server" AutoPostBack="true" CssClass="form-control"
                                                OnSelectedIndexChanged="ddlLoactionCode_OnSelectedIndexChanged">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--4 --%>
                                    <asp:TemplateField HeaderText="Department">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlDepartment" AutoPostBack="true" runat="server" CssClass="form-control"
                                                OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--5 --%>
                                    <asp:TemplateField HeaderText="User role code">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlUserroleCode" AutoPostBack="true" runat="server" CssClass="form-control"
                                                OnSelectedIndexChanged="ddlUserroleCode_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--6 --%>
                                    <asp:TemplateField HeaderText="Application Effective Date">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAppEffectDT" runat="server" CssClass="form-control"></asp:TextBox>
                                            <%--  <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtAppEffectDT"
            Display="Dynamic" ErrorMessage="Enter Valid Date (MM/DD/YYYY)" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--7 --%>
                                    <asp:TemplateField HeaderText="Application Cease Date">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAppCeaseDT" runat="server" CssClass="form-control"></asp:TextBox>
                                            <%--  <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtAppCeaseDT" 
            Display="Dynamic" ErrorMessage="Enter Valid Date (MM/DD/YYYY)" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--  <asp:TemplateField HeaderText="User Effective Date">
            <ItemTemplate>
             <asp:TextBox ID="txtUserEffectDT" runat="server" Width="60pt"></asp:TextBox>
            
             </ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField HeaderText="User Cease Date">
            <ItemTemplate>
             <asp:TextBox ID="txtUserCeaseDT" runat="server" Width="60pt"></asp:TextBox>
          
             </ItemTemplate>
           </asp:TemplateField>--%>
                                    <%--8 --%>
                                    <asp:TemplateField HeaderText="Application Status">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlAppEnblStatus" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                <asp:ListItem Value="E" Text="Enabled"></asp:ListItem>
                                                <asp:ListItem Value="D" Text="Disabled"></asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--9 --%>
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:RadioButton ID="rdbDefaultApp" runat="server" />
                                            <%--onclick="javascript:SelectSingleRadiobutton(this.id)"--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--10 --%>
                                    <asp:TemplateField HeaderText="Team Leader">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkTeamLead" Checked="false" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" BackColor="#dce9f9" />
                                <FooterStyle CssClass="GridViewFooter" />
                                <RowStyle CssClass="GridViewRow" />

                                <SelectedRowStyle CssClass="GridViewSelectedRow" />
                                <AlternatingRowStyle CssClass="GridViewAlternateRow"></AlternatingRowStyle>
                            </asp:GridView>
                        </div>
                        <div class="row">
                            <div class="col-sm-12" style="margin-top: 1%;">
                                <asp:CheckBox ID="chkIsSysAdmin" runat="server" CssClass="subtitle" />
                                &nbsp;
                            <asp:CheckBox ID="chkIsUsrAdmin" runat="server" CssClass="subtitle" />


                                <asp:CheckBox Visible="false" ID="chkTimingRestrict" runat="server" CssClass="subtitle" />
                                <asp:Button Visible="false" ID="btnEditTime" runat="server" Text="Edit Time Control"
                                    CssClass="standardbutton" Width="120px" />

                                <asp:CheckBox Visible="false" ID="chkIsForumModerator" runat="server" CssClass="subtitle" />
                                <asp:CheckBox Visible="false" ID="chkDownload" runat="server" CssClass="subtitle" />

                                <asp:CheckBox Visible="false" ID="chkLogonLocally" runat="server" CssClass="subtitle"
                                    Text="User cannot logon locally" />


                                <%--
                            <asp:Button ID="btnResetpw" runat="server" Text="Clear Password" CssClass="buttonClear" />

                                --%>
                                <asp:LinkButton ID="btnResetpw" CssClass="btn btn-primary"
                                    runat="server" TabIndex="6">
                                <span class="glyphicons glyphicons-registration-mark"></span> Clear Password
                                </asp:LinkButton>

                            </div>


                        </div>
                    </div>
                </asp:View>
                <asp:View ID="View4" runat="server">
                    <table width="100%" align="center">
                        <tr>
                            <td style="width: 1190px">
                                <div class="ImgTab">
                                    <ul>
                                        <li>
                                            <asp:LinkButton ID="L21" runat="server" OnClick="v1_click">User Details</asp:LinkButton></li>
                                        <li
                                            class="current">
                                            <asp:LinkButton ID="L22" runat="server" OnClick="v3_click">User Sanctioning</asp:LinkButton></li>
                                    </ul>
                                    <div class="TabContent">
                                        <table>
                                            <tr>
                                                <td style="height: 318px;" valign="top">
                                                    <asp:Panel ID="Panel6" runat="server" Height="318px" ScrollBars="Auto" Width="582px"
                                                        HorizontalAlign="Left" BorderStyle="Solid" BorderColor="black" BorderWidth="1pt"
                                                        BackColor="#f2f5ff" Enabled="true">
                                                        <%--<strong><span style="font-family: Tahoma">--%>
                                                        <asp:GridView ID="dgSRARights" runat="server" AutoGenerateColumns="False" Width="590px"
                                                            DataKeyNames="SrvcReqTypeCode" AllowPaging="False" AllowSorting="True" HorizontalAlign="left"
                                                            RowStyle-CssClass="formtable" GridLines="Vertical" OnRowDataBound="dgSRARights_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="SrvcReqTypeCode" Visible="true" SortExpression="SrvcReqTypeCode">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="SrvcReqTypeCode" runat="server" CssClass="control-lable" Text='<%# Bind("SrvcReqTypeCode") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Group Code" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="SrvcGrpCode" runat="server" CssClass="control-lable" Text='<%# Bind("SrvcGroupCode") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Group Name" Visible="true" SortExpression="SrvcGroupName">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="SrvcGroupName" runat="server" CssClass="control-lable" Text='<%# Bind("SrvcGroupName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Service Name" Visible="true" SortExpression="SrvReqTypeName"
                                                                    ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="SrvReqTypeName" runat="server" CssClass="control-lable" Text='<%# Bind("SrvReqTypeName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Access Right" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkAccessRight" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <%--Commecnted by vinita on 12 Apr 2012--%>
                                                                <%--<asp:TemplateField HeaderText="Post to LA" Visible="true">
                                                            <ItemTemplate>
                                                                <input type="hidden" runat="server" id="txtPostTOLA" name="txtPostTOLA" value='<%# Bind("PostTOLA") %>' />
                                                                <asp:CheckBox ID="chkPostTOLA" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                                <asp:TemplateField HeaderText="ACC" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="ACC" runat="server" CssClass="control-lable" Text='<%# Bind("ACC") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <%-- <asp:TemplateField HeaderText="AcCPTL" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="AcCPTL" runat="server" Text='<%# Bind("AcCPTL") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                            </Columns>
                                                            <FooterStyle CssClass="GridViewFooter" />
                                                            <PagerStyle CssClass="GridViewPager"></PagerStyle>
                                                            <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                                            <SelectedRowStyle CssClass="GridViewSelectedRow" />
                                                            <AlternatingRowStyle CssClass="GridViewAlternateRow"></AlternatingRowStyle>
                                                            <EmptyDataTemplate>
                                                                <asp:Label ID="lblNoRecord" runat="server" Text="No Record Found"></asp:Label>
                                                            </EmptyDataTemplate>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </td>
                                                <td style="height: 345px;" valign="top">
                                                    <asp:Label ID="lblUserGroupGrp" runat="server" Visible="False" />
                                                    <asp:Panel ID="Panel1" runat="server" Height="318px" ScrollBars="Auto" Width="360px"
                                                        HorizontalAlign="Left" BorderStyle="Solid" BorderColor="black" BorderWidth="1pt"
                                                        BackColor="#f2f5ff">
                                                        <strong><span style="font-family: Tahoma">
                                                            <asp:Label ID="lblSelectedModule" runat="server" Text="Selected Module"></asp:Label></span></strong>
                                                        <asp:TreeView ID="TrVUser" runat="server" ShowCheckBoxes="All" ShowLines="True" Width="324px"
                                                            CssClass="T1" Style="text-align: left">
                                                            <LeafNodeStyle CssClass="T1" />
                                                            <ParentNodeStyle CssClass="T1" />
                                                            <RootNodeStyle CssClass="T1" />
                                                            <NodeStyle CssClass="T1" />
                                                        </asp:TreeView>
                                                        <br />
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                         
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="View3" runat="server">
                    <table width="100%" align="center">
                        <tr>
                            <td style="width: 1309px">
                                <div class="ImgTab">
                                    <ul>
                                        <li>
                                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="v1_click">User Details</asp:LinkButton></li>
                                        <li class="current">
                                            <asp:LinkButton ID="LinkButton3" runat="server" OnClick="v3_click">Service Sanctioning</asp:LinkButton></li>
                                    </ul>
                                </div>
                                <div class="TabContent">
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:View>
            </asp:MultiView>
        </div>
    </div>
    <br />

    <asp:Panel ID="Panel2" runat="server" Height="349px" ScrollBars="Auto" Width="397px"
        BackColor="#f2f5ff" BorderColor="black" BorderStyle="solid" BorderWidth="1px"
        HorizontalAlign="Center" Style="left: 211pt; position: absolute; top: 623pt; z-index: 101;"
        Visible="false">
        <strong><span style="font-family: Tahoma" id="title" runat="server">User's Module Accessibility
            Summary</span></strong>
        <table align="center">
            <tr>
                <td align="center">
                    <asp:Panel ID="P1234" Height="300px" runat="server" ScrollBars="Auto">
                        <asp:TreeView ID="TrVModule" runat="server" ShowCheckBoxes="All" ShowLines="True"
                            Enabled="false" Height="300px" Width="334px" CssClass="T1" Style="text-align: left">
                            <LeafNodeStyle Font-Names="Tahoma" Font-Size="X-Small" CssClass="T1" />
                        </asp:TreeView>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="right" style="height: 12px">
                    <asp:Label ID="lblUGID" runat="server" Visible="False" />
                    <asp:Label ID="lblUGName" runat="server" Visible="False" />
                    <asp:Label ID="lblUGCC" runat="server" Visible="False" />
                    <asp:CheckBox ID="CheckBox4" runat="server" Font-Bold="True" Font-Names="Tahoma"
                        Font-Size="Small" Text="Apply to all existing user" Width="261px" Visible="false" />
                    <asp:Button ID="btnUpdate" runat="server" CssClass="standardbutton" Text="Update"
                        OnClick="btnUpdate_Click" Visible="false" />
                </td>
            </tr>
        </table>
        <asp:Button ID="btnClose" runat="server" Text="Cancel" CssClass="standardbutton"
            OnClick="btnClose_Click" />
    </asp:Panel>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConn %>" />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConn %>"
        SelectCommand="SELECT lookupCode, paramValue, ParamName01, sortOrder, paramNote, paramDescShort FROM ST_KsysLookupParam WHERE (lookupCode = 'userstat')"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConn %>"
        SelectCommand="SELECT lookupCode, paramValue, ParamName01, sortOrder, paramNote, paramDescShort FROM ST_KsysLookupParam WHERE (lookupCode = 'usertype') ORDER BY sortOrder"></asp:SqlDataSource>
    <%--<asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConn %>"
        SelectCommand="SELECT distinct opUnitCode as paramCode, UnitLegalName FROM iOpUnit LEFT JOIN vUnitDesc ON opUnitCode = unitCode"></asp:SqlDataSource>--%>
    <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConn %>"
        SelectCommand="SELECT lookupCode, paramValue, ParamName01, sortOrder, paramNote, paramDescShort FROM ST_KsysLookupParam WHERE (lookupCode = 'userRole') ORDER BY sortOrder"></asp:SqlDataSource>

    <div class="row">
        <div class="col-sm-12">
            <center>
                         <asp:LinkButton ID="linkSave" OnClick="linkSave_Click"  CausesValidation="true" ValidationGroup="NewsUserGroup"
                                                        CssClass="btn-animated bg-green"   runat="server"  >
                                    <span class="glyphicon glyphicon-search"></span> Save
                                                    </asp:LinkButton>
                    <asp:Button ID="btnHdnDDL" runat="server" Style="display: none;" OnClick="btnHdnDDL_Click">
                        </asp:Button>
                            <asp:LinkButton ID="linkClear" OnClientClick="return confirm('Are you sure you want to clear this module?');"  
                                                        CssClass="btn-animated bg-horrible" runat="server" >
                                    <span class="glyphicon glyphicon-erase" Visible="false" ></span> Clear
                                                    </asp:LinkButton>

                           <asp:LinkButton ID="linkCancel" OnClick="linkCancel_Click"  
                                                        CssClass="btn-animated bg-horrible" runat="server" >
                                    <span class="glyphicon glyphicon-erase"></span> Cancel
                                                    </asp:LinkButton>

             </center>
        </div>
    </div>
 
    <input type="hidden" id="hdnDDLLocTypeID" runat="server" />
    <input type="hidden" id="hiddenchkvalue" runat="server" />
    <asp:HiddenField ID="returnarray" runat="server" Value="0" />
    <asp:HiddenField runat="server" ID="hdnUsrRoleCode" />

    <!-- Display Modal popup window in division -->
         <div class="modal fade" id="myModal" role="dialog">
         </div>
         <!-- End Display Modal popup window in division -->

</asp:Content>
