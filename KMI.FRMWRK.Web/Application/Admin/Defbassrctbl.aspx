<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="Defbassrctbl.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.Admin.Defbassrctbl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <%: Styles.Render("~/bundles/CKYCcss") %>

    <link href="../../assets/css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../assets/js/bootstrap-multiselect.js"></script>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">
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

        .ui-menu {
            position: fixed !important;
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

        .custom-map {
            margin-bottom: 11px !important;
        }

        .control-label {
            margin-bottom: 8px !important;
        }

        .radio-list [type='radio'] {
            margin: 10px !important;
        }

        .check-list [type='checkbox'] {
            margin: 10px !important;
        }

        .form-control, .input-group-addon, .panel, .panel-header, .panel-body {
            border-radius: 0px !important;
        }

        .btn {
            border-radius: 0px !important;
        }

        .glyphicon-eye-open, .glyphicon-eye-close {
            cursor: pointer;
        }

        .AlignCenter {
            text-align: center !Important;
        }
    </style>
     <script>
         function gotoHome() {
             parent.location.href = parent.location.href;
         }
         function confPromptBox() {
             var resp = confirm("Please Enter Table Name")
             if (resp == true) {

                 location.replace(location.href);

             }
             else {

             }

         }
         function confPromptBox1() {
             var resp = confirm("Please select Table Type")
             if (resp == true) {

                 location.replace(location.href);

             }
             else {

             }

         }
         function confPromptBox2() {
             var resp = confirm("Please select Status")
             if (resp == true) {

                 location.replace(location.href);

             }
             else {

             }

         }
         function confPromptBox3() {
             var resp = confirm("Please Enter Table Description")
             if (resp == true) {

                 location.replace(location.href);

             }
             else {

             }

         }
         function confPromptBox4() {
             var resp = confirm("Please Enter Effective date")
             if (resp == true) {

                 location.replace(location.href);

             }
             else {

             }

         }

         function ToggleDiv(Flag) 
         {
             debugger;

             //alert(Flag);
             
             if (Flag == "col") {
                 document.getElementById('EmptyPagePlaceholder_divbassrctblcolhdrcollapse').style.display = 'block';
                 <%--   document.getElementById('<%=divdefindhdrcollapse.ClientID %>').style.display = 'none';--%>
             }
            else {
                <%-- document.getElementById('<%=divbassrctblcolhdrcollapse.ClientID %>').style.display = 'none';--%>
                 
             }
            
         }

         function gotoHome() {
             parent.location.href = parent.location.href;
         }

       <%-- function ToggleDiv1(Flag1) {
            if (Flag1 == "col1") {
                document.getElementById('<%=divdefindhdrcollapse.ClientID %>').style.display = 'block';
                document.getElementById('<%=divbassrctblcolhdrcollapse.ClientID %>').style.display = 'none';
            }
            else {
               <%-- document.getElementById('<%=divdefindhdrcollapse.ClientID %>').style.display = 'none';
            } --%>
        
         </script>


    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     <asp:UpdatePanel runat="server">
        <ContentTemplate>
          <div class="page-container">
    <div id="divbassrctblhdrcollapse" runat="server" style="margin-left: 2%; margin-right: 2%; margin-top: 0.5%" class="panel panel-success">
        <div id="Div6" runat="server" class="panel-heading" onclick="ShowReqDtl1('ctl00_ContentPlaceHolder1_divbassrctblhdr','btnToggleNew');return false;">
            <div class="row">
                <div class="col-sm-10" style="text-align: left">
                    <asp:Label ID="Label1" Text="Define Base And Source Table" runat="server" Font-Size="19px" />
                </div>
                <div class="col-sm-2">
                     <span id="btnToggleNew" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                </div>
            </div>
        </div>

          <div id="divbassrctblhdr" runat="server" style="width: 96%;" class="panel-body">
              <div class="row" style="margin-bottom: 5px;">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblTblNmae" Text="Table Name" runat="server" Style="font-size: 14px;" CssClass="control-label" />
                                <asp:Label Text="*" runat="server" Style="color: Red" />
                            </div>
                            <div class="col-sm-3">
                          <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtTblNmae" runat="server" CssClass="form-control" TabIndex="2"
                                    placeholder="Place Enter Table Name" MaxLength="128" onkeyup="ToUpper(this)" />
                              <%--  <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                    FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" ValidChars="_,@,#"
                                    FilterMode="ValidChars" TargetControlID="txtTblNmae">
                                </ajaxToolkit:FilteredTextBoxExtender>  --%>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblTblType" Text="Table Type" runat="server" Style="font-size: 14px;" CssClass="control-label" />
                                <asp:Label ID="Label3" Text="*" runat="server" Style="color: Red" />
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlTblType" runat="server" CssClass="select2-container form-control"
                                            AutoPostBack="true" TabIndex="1" Height="35px">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="row" style="margin-bottom: 5px;">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblStatus" Text="Status" runat="server" Style="font-size: 14px;" CssClass="control-label" />
                                <asp:Label Text="*" runat="server" Style="color: Red" />
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="select2-container form-control"
                                            AutoPostBack="true" TabIndex="1" Height="35px">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblTblDesc" Text="Table Description" runat="server" Style="font-size: 14px;" CssClass="control-label" />
                                <asp:Label ID="Label5" Text="*" runat="server" Style="color: Red" />
                            </div>
                            <div class="col-sm-3">
                           <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="TxtTblDesc" runat="server" CssClass="form-control" TabIndex="2"
                                    placeholder="Please Enter Table Description" MaxLength="400" />
                             <%--   <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                    FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" ValidChars=" "
                                    FilterMode="ValidChars" TargetControlID="TxtTblDesc">
                                </ajaxToolkit:FilteredTextBoxExtender>--%>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="row" style="margin-bottom: 5px;">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblEfDate" Text="Effective Date" Style="font-size: 14px;" runat="server" CssClass="control-label" />
                                <asp:Label ID="Label23" Text="*" runat="server" Style="color: Red" />
                            </div>
                            <div class="col-sm-3">
                                 <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                                    <ContentTemplate>
                                <asp:TextBox ID="txtEfDate" runat="server" CssClass="form-control" 
                                    onmousedown="PopulateCalender()"
                                        onmouseup="PopulateCalender()"
                                     placeholder="DD/MM/YYYY"  />
                                        </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblCeDate" Text="Cease Date" Style="font-size: 14px;" runat="server" CssClass="control-label" />
                                <asp:Label ID="Label26" Text="*" runat="server" Style="color: Red" Visible="false" />
                            </div>
                            <div class="col-sm-3">
                                 <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                                    <ContentTemplate>
                                <asp:TextBox ID="txtCeDate" runat="server" CssClass="form-control" 
                                    onmousedown="PopulateCalender1()"
                                    onmouseup="PopulateCalender1()"
                                    placeholder="DD/MM/YYYY" />
                                          </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    <div class="row" style="margin-top: 12px;margin-bottom:6px">
                    <div class="col-sm-12" align="center">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <asp:LinkButton ID="btnSave" runat="server" CssClass="btn btn-primary" OnClick="btnSave_Click" >  <%--OnClick="btnSave_Click"--%>
                                        <span class="glyphicon glyphicon-floppy-disk" style="color: White;"></span> Save
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnClear" runat="server" CssClass="btn btn-danger" OnClick="btnClear_Click"> <%--OnClick="btnClear_Click"--%>
                                        <span class="glyphicon glyphicon-erase BtnGlyphicon" style="color: White;"></span> Clear
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnCncl" runat="server" CssClass="btn btn-danger"  OnClientClick="gotoHome(); return false;">  <%--OnClick="btnCnCl_Click"--%>
                                <span class="glyphicon glyphicon-remove" style="color:White"></span> Cancel
                                </asp:LinkButton>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    </div>                  
                 <div id="div19" runat="server" style="width: 100%; border: none; margin: 0px 0 !important;"
                        class="table-scrollable">
                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="dgbassrctbl" runat="server" CssClass="footable" PageSize="10" AllowSorting="True"
                                    AllowPaging="true" AutoGenerateColumns="false">   <%--OnRowDataBound="dgbassrctbl_RowDataBound"--%>
                                    <RowStyle CssClass="GridViewRow"></RowStyle>
                                    <PagerStyle CssClass="disablepage" />
                                    <HeaderStyle CssClass="gridview th" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Table Name" HeaderStyle-HorizontalAlign="Left" SortExpression="TBL_NAME">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltblnam" runat="server" Text='<%# Bind("TBL_NAME")%>'></asp:Label>
                                                <asp:HiddenField ID="hdntblnam" runat="server" Value='<%# Bind("TBL_NAME")%>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="20%" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Table Description" HeaderStyle-HorizontalAlign="Left"
                                            SortExpression="TBL_DESC">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltbldesc" runat="server" Text='<%# Bind("TBL_DESC")%>'></asp:Label>
                                                <asp:HiddenField ID="hdntbldesc" runat="server" Value='<%# Bind("TBL_DESC")%>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="20%" HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left" SortExpression="STATUS">
                                            <ItemTemplate>
                                                <asp:Label ID="lblstat" runat="server" Text='<%# Bind("STATUS")%>'></asp:Label>
                                                <asp:HiddenField ID="hdnstat" runat="server" Value='<%# Bind("STATUS")%>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="20%" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Effective Date" HeaderStyle-HorizontalAlign="Left" SortExpression="EFF_DTIM">
                                            <ItemTemplate>
                                                <asp:Label ID="lbleffdat" runat="server" Text='<%# Bind("EFF_DTIM")%>'></asp:Label>
                                                <asp:HiddenField ID="hdneffdat" runat="server" Value='<%# Bind("EFF_DTIM")%>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="20%" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cease Date" HeaderStyle-HorizontalAlign="Left" SortExpression="CSE_DTIM">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcsedat" runat="server" Text='<%# Bind("CSE_DTIM")%>'></asp:Label>
                                                <asp:HiddenField ID="hdncsedat" runat="server" Value='<%# Bind("CSE_DTIM")%>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="20%" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Deftblcol" runat="server" Text="Define Table Column"  ForeColor="#337ab7" OnClick="Deftblcol_Click" OnClientClick="return ToggleDiv('col')"></asp:LinkButton>   <%--OnClick="Deftablecol_Click" --%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="20%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkdeftblind" runat="server" Text="Define Table Index" OnClientClick="return ToggleDiv1('col1')" ForeColor="#337ab7"></asp:LinkButton>   <%--OnClick="lnkdeftblind_Click"--%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="20%" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                   <div class="pagination" style="width: 100%;padding-left:45%">
                                <table>
                                    <tr>
                                        <td style="white-space: nowrap">
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>
                                                    <asp:Button ID="btnprevious" Text="<" CssClass="form-submit-button" runat="server" Width="40px"
                                                        Enabled="false" Style="border-style: solid; border-width: 1px; background-repeat: no-repeat;
                                                        background-color: transparent; float: left; margin: 0; height: 30px;" Visible="false" OnClick="btnprevious_Click" />  <%--OnClick="btnprevious_Click"--%>
                                                    <asp:TextBox runat="server" ID="txtPage" Style="width: 35px; border-style: solid;
                                                        border-width: 1px; border-color: Gray; height: 30px; float: left; margin: 0;
                                                        text-align: center;" Text="1" CssClass="form-control" ReadOnly="true" Visible="false" />
                                                    <asp:Button ID="btnnext" Text=">" CssClass="form-submit-button" runat="server" Style="border-style: solid;
                                                        border-width: 1px; background-repeat: no-repeat; background-color: transparent;
                                                        float: left; margin: 0; height: 30px;" Width="40px" Visible="false" OnClick="btnnext_Click" />   <%--OnClick="btnnext_Click"--%>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                        </div>

                     </div>
              </div>
        </div>

    <%-- Define Base Source Table Column Section --%>
        
     <div id="divbassrctblcolhdrcollapse" runat="server" style="margin-left: 2%; margin-right: 2%; margin-top: 0.5%;" class="panel panel-success">
       <div id="Div2" runat="server" class="panel-heading" onclick="ShowReqDtl1('ctl00_ContentPlaceHolder1_divbassrctblcolhdr','myImg1');return false;">
            <div class="row">
                <div class="col-sm-10" style="text-align: left">
                    <asp:Label ID="Label2" Text="Define Base Source Table Column" runat="server" Font-Size="19px" />
                </div>
                <div class="col-sm-2">
                    <span id="btnToggleNew1" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                </div>
            </div>
        </div>  
              
                       <div id="divbassrctblcolhdr" runat="server" style="width: 96%;" class="panel-body">
                            <div class="row" style="margin-bottom: 5px;">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblColName" Text="Column Name" runat="server" Style="font-size: 14px;" CssClass="control-label" />
                                <asp:Label Text="*" runat="server" Style="color: Red" />
                            </div>
                            <div class="col-sm-3">
                          <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtColName" runat="server" CssClass="form-control" TabIndex="2"
                                    placeholder="Place Enter Column Name" MaxLength="128"  onkeyup="ToUpper(this)"/>
                             <%--   <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                    FilterType="Numbers,LowercaseLetters,LowercaseLetters,UppercaseLetters,Custom" ValidChars="_,@,#"
                                    FilterMode="ValidChars" TargetControlID="txtColName">
                                </ajaxToolkit:FilteredTextBoxExtender>--%>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblColdesc" Text="Column Description" runat="server" Style="font-size: 14px;" CssClass="control-label" />
                                <asp:Label ID="Label7" Text="*" runat="server" Style="color: Red" />
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                    <ContentTemplate>
                                <asp:TextBox ID="txtColdesc" runat="server" CssClass="form-control" TabIndex="2"
                                    placeholder="Place Enter Column Description" MaxLength="400" />
                              <%--  <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                    FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" ValidChars="_,@,#, "
                                    FilterMode="ValidChars" TargetControlID="txtColdesc">
                                </ajaxToolkit:FilteredTextBoxExtender>--%>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="row" style="margin-bottom: 5px;">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lbldattyp" Text="Data Type" runat="server" Style="font-size: 14px;" CssClass="control-label" />
                                <asp:Label Text="*" runat="server" Style="color: Red" />
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddldattyp" runat="server" CssClass="select2-container form-control" AutoPostBack="true" TabIndex="1"
                                             Height="35px" OnSelectedIndexChanged="ddldattyp_SelectedIndexChanged"> <%-- OnSelectedIndexChanged="ddldattyp_SelectedIndexChanged"--%>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblSize" Text="Size" runat="server" Style="font-size: 14px;" CssClass="control-label" />
                            </div>
                            <div class="col-sm-3">
                           <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtSize" runat="server" CssClass="form-control" TabIndex="2"
                                    placeholder="Please Enter Size" MaxLength="40" />
                               <%-- <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                    FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" ValidChars=" "
                                    FilterMode="ValidChars" TargetControlID="txtSize">
                                </ajaxToolkit:FilteredTextBoxExtender>  --%>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="row" style="margin-bottom: 5px;">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblcoltyp" Text="Column Type" runat="server" Style="font-size: 14px;" CssClass="control-label" />
                                <asp:Label Text="*" runat="server" Style="color: Red" />
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel32" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlcoltyp" runat="server" CssClass="select2-container form-control" AutoPostBack="true" TabIndex="1" Height="35px" OnSelectedIndexChanged="ddldattyp_SelectedIndexChanged"> <%--OnSelectedIndexChanged="ddldattyp_SelectedIndexChanged"--%>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblabsflg" Text="ABS Flag" runat="server" Style="font-size: 14px;" CssClass="control-label" />
                                <asp:Label Text="*" runat="server" Style="color: Red" />
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel33" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlabsflg" runat="server" CssClass="select2-container form-control" AutoPostBack="true" TabIndex="1" Height="35px" OnSelectedIndexChanged="ddldattyp_SelectedIndexChanged"> <%-- OnSelectedIndexChanged="ddldattyp_SelectedIndexChanged"--%>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="row" style="margin-bottom: 5px;">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="Label15" Text="Precision" runat="server" Style="font-size: 14px;" CssClass="control-label" />
                            </div>
                            <div class="col-sm-3">
                           <asp:UpdatePanel ID="UpdatePanel31" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtprec" runat="server" Enabled="false" CssClass="form-control" TabIndex="2"
                                    placeholder="Please Enter Precision" MaxLength="40" />
                               <%-- <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                    FilterType="Numbers" ValidChars=" "
                                    FilterMode="ValidChars" TargetControlID="txtprec">
                                </ajaxToolkit:FilteredTextBoxExtender>--%>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="Label9" Text="Effective Date" Style="font-size: 14px;" runat="server" CssClass="control-label" />
                                <asp:Label ID="Label10" Text="*" runat="server" Style="color: Red" />
                            </div>
                            <div class="col-sm-3">
                                 <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                                    <ContentTemplate>
                                <asp:TextBox ID="txteffdatecol" runat="server" CssClass="form-control" 
                                    onmousedown="PopulateCalender2()"  onmouseup="PopulateCalender2()"  placeholder="DD/MM/YYYY"  />
                                        </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <div class="row" style="margin-bottom: 5px;">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="Label12" Text="Cease Date" Style="font-size: 14px;" runat="server" CssClass="control-label" />
                                <asp:Label ID="Label13" Text="*" runat="server" Style="color: Red" Visible="false" />
                            </div>
                            <div class="col-sm-3">
                                 <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                                    <ContentTemplate>
                                <asp:TextBox ID="txtcsedatecol" runat="server" CssClass="form-control" 
                                    onmousedown="PopulateCalender3()"
                                    onmouseup="PopulateCalender3()"
                                    placeholder="DD/MM/YYYY" />
                                          </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
              </div>
           <%--   </div>--%>
              
               <div id="divDefConhdrcollapse" runat="server" style="margin-left: 2%; margin-right: 2%; margin-top: 0.5%" class="panel panel-success">
        <div id="Div3" runat="server" class="panel-heading" onclick="ShowReqDtl1('ctl00_ContentPlaceHolder1_divDefConhdr','myImg1');return false;">
            <div class="row">
                <div class="col-sm-10" style="text-align: left">
                    <asp:Label ID="Label4" Text="Define Constraint" runat="server" Font-Size="19px" />
                </div>
                <div class="col-sm-2">
                    <span id="btnToggleNew2" class="glyphicon glyphicon-collapse-down" style="float: right;  padding: 1px 10px ! important; font-size: 18px;"></span>
                </div>
            </div>
        </div>
                            <div id="divDefConhdr" runat="server" style="width: 96%;" class="panel-body">
                            <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                            <ContentTemplate>
                            <div id="divIsIdentity" class="col-sm-5" runat="server" style="padding-right:200px">
                            <asp:Label ID="lblIsIdentity" runat="server" Style="font-size: 14px;" CssClass="control-label" />
                                <asp:Label Text="*" runat="server" Style="color: Red" />
                                &nbsp
                                &nbsp
                                 <asp:RadioButton ID="rdSingleCycle" runat="server" GroupName="Is Identity" Text="Yes" />
                                 <asp:RadioButton ID="rdSingleCycle1" runat="server" GroupName="Is Identity" Text="No" />
                            </div>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                            <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                            <ContentTemplate>
                            <div id="divIsPrimary" class="col-sm-5" runat="server" style="padding-right:200px">
                            <asp:Label ID="lblIsPrimary" runat="server" Style="font-size: 14px;" CssClass="control-label" />
                                <asp:Label Text="*" runat="server" Style="color: Red" />
                                &nbsp
                                &nbsp
                                 <asp:RadioButton ID="rdIsPrimary" runat="server" GroupName="Is Primary" Text="Yes" />
                                 <asp:RadioButton ID="rdIsPrimary1" runat="server" GroupName="Is Primary" Text="No" />
                            </div>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                            <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                            <ContentTemplate>
                            <div id="divIsAvailable" class="col-sm-5" runat="server" style="padding-right:200px">
                            <asp:Label ID="lblIsAvailable" runat="server" Style="font-size: 14px;" CssClass="control-label" />
                                <asp:Label Text="*" runat="server" Style="color: Red" />
                                &nbsp
                                &nbsp
                                 <asp:RadioButton ID="rdIsAvailable" runat="server" GroupName="Is Available" Text="Yes" />
                                 <asp:RadioButton ID="rdIsAvailable1" runat="server" GroupName="Is Available" Text="No" />
                            </div>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
</div>

               <div id="divdefforkeyhdrcollapse" runat="server" style="margin-left: 2%; margin-right: 2%; margin-top: 0.5%" class="panel panel-success">
        <div id="Div4" runat="server" class="panel-heading" onclick="ShowReqDtl1('ctl00_ContentPlaceHolder1_divdefforkeyhdr','myImg1');return false;">
            <div class="row">
                <div class="col-sm-10" style="text-align: left">
                    <asp:Label ID="Label6" Text="Define Foreign Key" runat="server" Font-Size="19px" />
                </div>
                <div class="col-sm-2">
                    <span id="btnToggleNew3" class="glyphicon glyphicon-collapse-down" style="float: right;  padding: 1px 10px ! important; font-size: 18px;"></span>
                </div>
            </div>
        </div>
                            <div id="divdefforkeyhdr" runat="server" style="width: 96%;" class="panel-body">
                                <div class="row" style="margin-bottom: 5px;">
                            <div id="divIsforkey" class="col-sm-6" runat="server" style="padding-right:200px">
                            <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                            <ContentTemplate>
                            <asp:Label ID="lblIsforkey" runat="server" Style="font-size: 14px;" CssClass="control-label" />
                                <asp:Label Text="*" runat="server" Style="color: Red" />
                                &nbsp
                                &nbsp
                                &nbsp
                                 <asp:RadioButton ID="rdIsforkey" runat="server" GroupName="Is Foreign Key" Text="Yes" AutoPostBack="True" OnCheckedChanged="rdIsforkey_CheckedChanged"/> <%--OnCheckedChanged="rdIsforkey_CheckedChanged"--%>
                                &nbsp
                                &nbsp
                                 <asp:RadioButton ID="rdIsforkey1" runat="server" GroupName="Is Foreign Key" AutoPostBack="True"  Text="No" OnCheckedChanged="rdIsforkey1_CheckedChanged" />  <%--OnCheckedChanged="rdIsforkey1_CheckedChanged"--%>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblFortbl" Text="Foreign Table" runat="server" Style="font-size: 14px;" CssClass="control-label" />
                                <asp:Label Text="*" runat="server" Style="color: Red" />
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlFortbl" runat="server" CssClass="select2-container form-control"
                                            AutoPostBack="true" TabIndex="1" Height="35px" OnSelectedIndexChanged="ddlFortbl_SelectedIndexChanged" >  <%--OnSelectedIndexChanged="ddlFortbl_SelectedIndexChanged"--%>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                                    </div>
                            <div class="row" style="margin-bottom: 5px;">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblForCol" Text="Foreign Column" runat="server" Style="font-size: 14px;" CssClass="control-label" />
                                <asp:Label Text="*" runat="server" Style="color: Red" />
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlForCol" runat="server" CssClass="select2-container form-control" TabIndex="1" Height="35px">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                                    </div>
                            <div class="row" style="margin-bottom: 5px;">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblStatus1" Text="Status" runat="server" Style="font-size: 14px;" CssClass="control-label" />
                                <asp:Label Text="*" runat="server" Style="color: Red" />
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlstatus1" runat="server" CssClass="select2-container form-control" TabIndex="1" Height="35px">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lbltablename" Text="Table Name" runat="server" Style="font-size: 14px;" CssClass="control-label" />
                                <asp:Label Text="*" runat="server" Style="color: Red" />
                            </div>
                            <div class="col-sm-3" style="text-align:left">
                             <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                    <ContentTemplate>
                                <asp:Label ID="lbltablenameVal" runat="server" CssClass="control-label" Style="font-size: 12px;text-align:left">
                                    </asp:Label>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                                    </div>
                                   <div class="row" style="margin-top: 12px;margin-bottom:6px">
                    <div class="col-sm-12" align="center">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <asp:LinkButton ID="btnSave1" runat="server" CssClass="btn btn-primary" OnClick="btnSave1_Click">
                                        <span class="glyphicon glyphicon-floppy-disk" style="color: White;"></span> Save
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnClear1" runat="server" CssClass="btn btn-danger" OnClick="btnClear1_Click">
                                        <span class="glyphicon glyphicon-erase BtnGlyphicon" style="color: White;"></span> Clear
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnCnCl1" runat="server" CssClass="btn btn-danger"  OnClientClick="gotoHome(); return false;">
                                <span class="glyphicon glyphicon-remove" style="color:White"></span> Cancel
                                </asp:LinkButton>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    </div>

                                                 <div id="div1" runat="server" style="width: 100%; border: none; margin: 0px 0 !important;"
                        class="table-scrollable">
                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="dgbassrctblcol" runat="server" CssClass="footable" PageSize="10" AllowSorting="True"
                                  AllowPaging="true" AutoGenerateColumns="false">    <%--OnRowDataBound="dgbassrctblcol_RowDataBound" --%>
                                    <RowStyle CssClass="GridViewRow"></RowStyle>
                                    <PagerStyle CssClass="disablepage" />
                                    <HeaderStyle CssClass="gridview th" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Column Name" HeaderStyle-HorizontalAlign="Left"
                                            SortExpression="COL_NAM">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcolnam" runat="server" Text='<%# Bind("COL_NAM")%>'></asp:Label>
                                                <asp:HiddenField ID="hdncolnam" runat="server" Value='<%# Bind("COL_NAM")%>'>
                                                </asp:HiddenField>
                                                <asp:HiddenField ID="hdntblobjid" runat="server" Value='<%# Bind("OBJ_ID")%>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Column Description" HeaderStyle-HorizontalAlign="Left" SortExpression="COL_DESC">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcoldesc" runat="server" Text='<%# Bind("COL_DESC")%>'></asp:Label>
                                                <asp:HiddenField ID="hdncoldesc" runat="server" Value='<%# Bind("COL_DESC")%>' />
                                                <asp:HiddenField ID="hdncoltyp" runat="server" Value='<%# Bind("COL_TYP")%>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="20%" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Data Type" HeaderStyle-HorizontalAlign="Left"
                                            SortExpression="DATA_TYPE_DESC">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldattyp" runat="server" Text='<%# Bind("DATA_TYPE_DESC")%>'></asp:Label>
                                                <asp:HiddenField ID="hdndattyp" runat="server" Value='<%# Bind("DATA_TYPE_DESC")%>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="20%" HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left"
                                            SortExpression="STATUS">
                                            <ItemTemplate>
                                                <asp:Label ID="lblstatuscol" runat="server" Text='<%# Bind("STATUS")%>'></asp:Label>
                                                <asp:HiddenField ID="hdnstatuscol" runat="server" Value='<%# Bind("STATUS")%>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="20%" HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Size" HeaderStyle-HorizontalAlign="Left"
                                            SortExpression="SIZE">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsize" runat="server" Text='<%# Bind("SIZE")%>'></asp:Label>
                                                <asp:HiddenField ID="hdnsize" runat="server" Value='<%# Bind("SIZE")%>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="20%" HorizontalAlign="Center"  CssClass="CenterAlign" />
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="I P/C" HeaderStyle-HorizontalAlign="Left"
                                            SortExpression="IS_PRIMARY">
                                            <ItemTemplate>
                                                <asp:Label ID="lblipc" runat="server" Text='<%# Bind("IS_PRIMARY")%>'></asp:Label>
                                                <asp:HiddenField ID="hdnipc" runat="server" Value='<%# Bind("IS_PRIMARY")%>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="20%" HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Effective Date" HeaderStyle-HorizontalAlign="Left" SortExpression="EFF_DTIM">
                                            <ItemTemplate>
                                                <asp:Label ID="lbleffdatcol" runat="server" Text='<%# Bind("EFF_DTIM")%>'></asp:Label>
                                                <asp:HiddenField ID="hdneffdatcol" runat="server" Value='<%# Bind("EFF_DTIM")%>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="20%" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cease Date" HeaderStyle-HorizontalAlign="Left" SortExpression="CSE_DTIM">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcsedatcol" runat="server" Text='<%# Bind("CSE_DTIM")%>'></asp:Label>
                                                <asp:HiddenField ID="hdncsedatcol" runat="server" Value='<%# Bind("CSE_DTIM")%>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="20%" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkedit" runat="server" Text="Edit" OnClick="lnkedit_Click" ForeColor="#3333cc"> </asp:LinkButton>   <%--OnClick="lnkedit_Click"--%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="20%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                <div class="pagination" style="width: 100%;padding-left:45%">
                                <table>
                                    <tr>
                                        <td style="white-space: nowrap">
                                            <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                                <ContentTemplate>
                                                    <asp:Button ID="btnpreviouscol" Text="<" CssClass="form-submit-button" runat="server" Width="40px"
                                                        Enabled="false" Style="border-style: solid; border-width: 1px; background-repeat: no-repeat;
                                                        background-color: transparent; float: left; margin: 0; height: 30px;" OnClick="btnpreviouscol_Click"  Visible="false" />  <%-- OnClick="btnpreviouscol_Click"--%>
                                                   <asp:TextBox runat="server" ID="txtpagecol" Style="width: 35px; border-style: solid;
                                                        border-width: 1px; border-color: Gray; height: 30px; float: left; margin: 0;
                                                        text-align: center;" Text="1" CssClass="form-control" ReadOnly="true" Visible="false" />
                                                    <asp:Button ID="btnnextcol" Text=">" CssClass="form-submit-button" runat="server" Style="border-style: solid;
                                                        border-width: 1px; background-repeat: no-repeat; background-color: transparent;
                                                        float: left; margin: 0; height: 30px;" Width="40px" OnClick="btnnextcol_Click" Visible="false" /> <%-- OnClick="btnnextcol_Click"--%>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                        </div>

                     
</div>


    </div>
         </div>
              </div>
              

       </ContentTemplate>
         </asp:UpdatePanel>


</asp:Content>
