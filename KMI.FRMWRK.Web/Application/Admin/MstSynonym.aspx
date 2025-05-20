<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.Admin.test" %>--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="MstSynonym.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.Admin.test" %>


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
       
        $(document).ready(function () {
               debugger;
               window.history.forward();
           });
               function PopulateCalender() {
               debugger;
               //minDate:new Date()
               $("#<%= txtED.ClientID  %>").datepicker({
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true,
                }); $("#<%= txtED.ClientID  %>").datepicker('setDate', new Date());
        }

         function PopulateCalender1() {
             debugger;
             minDate: new Date()
             $("#<%= txtCD.ClientID  %>").datepicker({
                  dateFormat: 'dd/mm/yy',
                  changeMonth: true,
                  changeYear: true,
                  // minDate: 2,
                  //defaultDate: "+1D",
                  onSelect: function (d, i) {
                      if (d != i.lastVal) {
                          debugger;
                          $(this).change()
                          checkDate();
                      }
                  }
              });
          }
          function checkDate() {
              debugger;

              var EffDate = $('#<%= txtED.ClientID  %>').val();
           //   document.getElementById('EmptyPagePlaceholder_lstbdonSynm');
            var CeDate = $('#<%= txtCD.ClientID  %>').val();
              var strcontent = "EmptyPagePlaceholder_";
            debugger;
            if (EffDate != "" && CeDate != "") {
                if (!checkDateIsGreaterThanToday(EffDate, CeDate)) {
                    // alert("Please select the correct cease date");
                    document.getElementById("EmptyPagePlaceholder_txtCD").value = "";
                    return false;
                }
                else {
                    //alert("step2");
                }
            }
        }

        function checkDateIsGreaterThanToday(fromDay, toDay) {
            debugger;
            var fromArr = fromDay.split('/');
            var toArr = toDay.split('/');

            if (fromArr[2] == toArr[2]) {
                if (fromArr[1] < toArr[1]) {
                    if (fromArr[0] < toArr[0]) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else if (fromArr[1] == toArr[1]) {
                    if (fromArr[0] < toArr[0]) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else {
                    return false;
                }
            }
            else if (fromArr[2] < toArr[2]) {

                return true;

            }
            else {
                return false;
            }
        }
          
        function Validate() {
            debugger;
            if (document.getElementById("<%=txtlnkid.ClientID%>").value == "") {
                alert("Please enter Link Server ID.");
                return false;}

            if (document.getElementById("<%=txtdb.ClientID%>").value == "") {
                alert("Please enter Database Name.");
                return false;
            }

            if (document.getElementById("<%=txttblnm.ClientID%>").value == "") {
                alert("Please enter Table Name.");
                return false;
            }
            var strcontent = "ctl00_ContentPlaceHolder1_";
                if (document.getElementById(strcontent + "ddlDestnDb") != null) {
                if (document.getElementById(strcontent + "ddlDestnDb").value == "") {
                    alert("Please Select Destination Database.");
                    document.getElementById(strcontent + "ddlDestnDb").focus();
                    return false;
                }
            }
                if (document.getElementById("<%=txtsyndesc.ClientID%>").value == "") {
                    alert("Please Enter Synonym Description.");
                    return false;
                }
              
        }

        function gotoHome() {
            parent.location.href = parent.location.href;
        }
    </script>

       <asp:ScriptManager ID="scrusdtls" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
          <div class="page-container">
              <center>
                <div id="divfinhdrcollapse" class="panel panel-success" style="margin-left: 2%; margin-right: 2%; margin-top: 0.5%">
              <%--Megha--%>
                       <div class="panel-heading" onclick="showHideDiv('divCustomerAdd','btnToggleNew');return false;">
                        <div class="row" style="margin: 0px">
                            <div class="col-sm-10" style="text-align: left">
                                <asp:Label ID="Label1" runat="server" Font-Bold="true" Font-Size="18px" Text="Synonym Setup"></asp:Label>
                            </div>
                            <div class="col-sm-2">
                                <span id="btnToggleNew" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div>

                       <div id="divfinhdr" runat="server" style="width: 96%;" class="panel-body">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                         <div class="row" style="margin-bottom: 5px;">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lbllinkserverid" Text="Link Server ID" runat="server" CssClass="control-label" />
                                <span id="Span2" runat="server" style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtlnkid" runat="server" CssClass="form-control" TabIndex="2" onkeypress="return disableSpace()" />
                                       <%-- <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderlnkid" runat="server" InvalidChars=";,#$@%^!*()&''%^~`_:-+{}[]?/><|*a"
                                         FilterMode="InvalidChars" TargetControlID="txtlnkid" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom"></ajaxToolkit:FilteredTextBoxExtender>--%>
                            </div>

                              <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lbldb" Text="Database" runat="server" CssClass="control-label" />
                                  <span id="Span3" runat="server" style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtdb" runat="server" CssClass="form-control" TabIndex="2" onkeypress="return disableSpace()" />
<%--                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" InvalidChars=";,#$@%^!*()&''%^~`.:-+{}[]?/><|*" 
                                         FilterMode="InvalidChars" TargetControlID="txtdb" FilterType="LowercaseLetters,UppercaseLetters,Custom"></ajaxToolkit:FilteredTextBoxExtender>--%>
                            </div>
                             </div>
                           <div class="row" style="margin-bottom: 5px;">
                                <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="tbltblnm" Text="Table Name" runat="server" CssClass="control-label" />
                                    <span id="Span4" runat="server" style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txttblnm" runat="server" CssClass="form-control" TabIndex="2" AutoPostBack="true"  onkeypress="return disableSpace()" OnTextChanged="txttblnm_TextChanged" /> <%-- OnTextChanged="txttblnm_TextChanged"--%>
                                
                                <%--<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" InvalidChars=";,#$@%^!*()&''%^~`:-+.{}[]?/><|*"
                                         FilterMode="InvalidChars" TargetControlID="txttblnm" FilterType="LowercaseLetters,UppercaseLetters,Custom"></ajaxToolkit:FilteredTextBoxExtender>--%>
                            </div>

                               <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblsyn" Text="Synonym Name" runat="server" CssClass="control-label" />
                                   <span id="Span5" runat="server" style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtsynm" runat="server" CssClass="form-control" TabIndex="2" Enabled="false" />
                            </div>
                             </div>
                          <div class="row" style="margin-bottom: 5px;">
                               <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lbldestndb" Text="Destination Database" runat="server" CssClass="control-label" />
                                   <span id="Span6" runat="server" style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlDestnDb" runat="server" AutoPostBack="true" CssClass="form-control"
                                            TabIndex="4"  >
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                               <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblst" Text="Status" runat="server" CssClass="control-label" />
                                   <span id="Span7" runat="server" style="color: red">*</span>
                            </div>
                                <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlstts" runat="server" AutoPostBack="true" CssClass="form-control"
                                            TabIndex="4" >
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                               </div>
                         <div class="row" style="margin-bottom: 5px;">
                             <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblED" Text="Effective Date" runat="server" CssClass="control-label" />
                                 <span id="Span8" runat="server" style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtED" runat="server" CssClass="form-control"  placeholder="DD/MM/YYYY" Enabled="false"  /> <%--onmousedown="PopulateCalender(); return false;" onmouseup="PopulateCalender()" --%>
                                </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblCD" Text="Cease Date" runat="server" CssClass="control-label" />
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtCD" runat="server" CssClass="form-control"  placeholder="DD/MM/YYYY"  onmousedown="PopulateCalender1()" onmouseup="PopulateCalender1()" Enabled="false"/>
                                </div>
                            </div>
                             <div class="row" style="margin-bottom: 5px;">
                             <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="Label2" Text="Synonym Description" runat="server" CssClass="control-label" />
                                   <span id="spansyndesc" runat="server" style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtsyndesc" runat="server" CssClass="form-control" TabIndex="2" />
                                
                               <%-- <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" InvalidChars=";,#$@%^!*()&''%^~`_:-+{}[].?/><|*"
                                         FilterMode="InvalidChars" TargetControlID="txtsyndesc" FilterType="LowercaseLetters,UppercaseLetters,Custom"></ajaxToolkit:FilteredTextBoxExtender>--%>
                            </div>
                            </div>
                                   <div id="divsyncrete" runat="server" class="row" style="margin-top: 12px;">
                            <div class="col-sm-12" align="center">
                                <asp:LinkButton ID="btnSave" runat="server" CssClass="btn btn-primary" TabIndex="17"  OnClientClick="return Validate();" OnClick="btnSave_Click">  <%--OnClick="btnSave_Click"--%>
                                   <span class="glyphicon glyphicon-plus BtnGlyphicon" style="color:White"></span> Create
                                 </asp:LinkButton>

                                   <asp:LinkButton ID="btnupd" runat="server" CssClass="btn btn-primary" TabIndex="17" Visible="false" OnClick="btnupd_Click">  <%--OnClick="btnupd_Click"--%>
                                     <span class="glyphicon glyphicon-floppy-disk"  style="color:White"></span> Update
                                   </asp:LinkButton>

                                 <asp:LinkButton ID="btnclr" runat="server" CssClass="btn btn-danger" TabIndex="17" OnClick="btnclr_Click" >   <%--OnClick="btnclr_Click"--%>
                                     <span class="glyphicon glyphicon-erase BtnGlyphicon" style="color: White;"></span> Clear
                                   </asp:LinkButton>
                                <asp:LinkButton ID="btncncl" runat="server" CssClass="btn btn-danger" TabIndex="17" OnClientClick="JavaScript:window.history.back(1); return false;">
                                     <span class="glyphicon glyphicon-remove BtnGlyphicon" style="color: White;"></span> Cancel
                                   </asp:LinkButton>
                                </div>
                           
                        </div>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                           
                           </div>
  </div>

              <asp:Updatepanel id="uppnl" runat="server">
            <ContentTemplate>
                   <div id="divcmpsrchhdrcollapse" runat="server" style="margin-left: 2%; margin-right: 2%; margin-top: 0.5%"  class="panel panel-success ">
                        <div id="Div1" runat="server" class="panel-heading" onclick="ShowReqDtl1('ctl00_ContentPlaceHolder1_divsyngrd','myImg1');return false;">
                <div class="row">
                    <div class="col-sm-10" style="text-align: left">
                     
                        <asp:Label ID="Label3" Text="Details" runat="server" Style="font-size:19px;" />
                    </div>
                    <div class="col-sm-2">
                        <span id="btnToggleNew1" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                   
                    </div>
                </div>
                            </div>
                       <%--    panel body start--%>
                       <%--<div id="divsyngrd" runat="server" style="width: 96%; padding: 10px;" >--%>
                         <div id="divsyngrd" runat="server" style="width: 100%; border: none; margin: 0px 0 !important;" class="table-scrollable">
                      <div id="divGridMap" runat="server" style="width: 100%; overflow-x:scroll" >
                    
                <asp:UpdatePanel ID="UpdatePanelgrd" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grdsyn" runat="server" AutoGenerateColumns="false" PageSize="10" AllowSorting="True" AllowPaging="true" CssClass="footable"  Width="100%" 
                            >
                            <RowStyle CssClass="GridViewRowNEW"></RowStyle>
                            <PagerStyle CssClass="disablepage" />
                            <HeaderStyle CssClass="gridview th" />
                               <%--<EmptyDataTemplate>
                                  <asp:Label ID="Label2" Text="No Synonym have been Created" ForeColor="Red" CssClass="control-label" runat="server" />
                                </EmptyDataTemplate>--%>
                            <Columns>
                                <asp:TemplateField HeaderText="Link Server ID"  SortExpression="LNK_SVR_ID" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLnksrvr"  runat="server" Text='<%# Bind("LNK_SVR_ID")%>'></asp:Label>
                                        <asp:Label ID="lblsynSeqNo" Text='<%# Bind("SEQNO") %>' Visible="false" runat="server" />
                                    </ItemTemplate>                                                                    
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Database" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                     SortExpression="DB_NAME" >
                                    <ItemTemplate>
                                        <asp:Label ID="lbldbnm"  runat="server" Text='<%# Bind("DB_NAME")%>'></asp:Label>
                                    </ItemTemplate>                                                                    
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Table Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                     SortExpression="TBL_NAME" >
                                    <ItemTemplate>
                                        <asp:Label ID="lbltblnm"  runat="server" Text='<%# Bind("TBL_NAME")%>'></asp:Label>
                                    </ItemTemplate>                                                                    
                                </asp:TemplateField>

                               <asp:TemplateField HeaderText="Synonym Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                     SortExpression="SYNYM_NAME" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblSynym"  runat="server" Text='<%# Bind("SYNYM_NAME")%>'></asp:Label>
                                    </ItemTemplate>
                                    <%-- <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />--%>                                   
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Synonym Description" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                     SortExpression="SYNYM_DESC" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblSynymDesc"  runat="server" Text='<%# Bind("SYNYM_DESC")%>'></asp:Label>
                                    </ItemTemplate>    
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Destination Database" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                     SortExpression="DSTN_DB" >
                                    <ItemTemplate>
                                        <asp:Label ID="lbldstndb"  runat="server" Text='<%# Bind("DSTN_DB")%>'></asp:Label>
                                    </ItemTemplate>                                                                    
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                     SortExpression="STATUS" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblsts"  runat="server" Text='<%# Bind("STATUS")%>'></asp:Label>
                                    </ItemTemplate>                                                                    
                                </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="Effective Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                     SortExpression="EFF_DTIM" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblefd"  runat="server" Text='<%# Bind("EFF_DTIM")%>'></asp:Label>
                                    </ItemTemplate>                                                                    
                                </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Cease Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                     SortExpression="CSE_DTIM" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblcsd"  runat="server" Text='<%# Bind("CSE_DTIM")%>'></asp:Label>
                                    </ItemTemplate>                                                                    
                                </asp:TemplateField>                               
                               <asp:TemplateField HeaderText="Action" >
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkedit" Text="Edit" runat="server" ShowEditButton="true" data-myData='<%# Eval("SYNYM_NAME") %>' OnClick="lnkedit_Click" ForeColor="#3333cc" />      <%--OnClick="lnkedit_Click"--%>
                                     </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Action" >
                                    <ItemTemplate> 
                                    <asp:LinkButton ID="lnksyndel" runat="server" Text="Delete" OnClientClick="return confirm('Are you sure you want delete?'); return true;"  Visible="TRUE" OnClick="lnksyndel_Click" ForeColor="#3333cc"/>  <%--OnClick="lnksyndel_Click"--%>
                                  </asp:LinkButton>
                                        </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                                 </div>
                       <br />
                 
      <%--          </ContentTemplate>
                                 </asp:UpdatePanel>--%>

           <%--          <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                <ContentTemplate>    --%>
                        <div id="divPage" visible="true" runat="server" class="pagination"> 
                            <center>
                                <table>
                                    <tr>
                                        <td style="white-space: nowrap;">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:Button ID="btnprevious" Text="<" CssClass="form-submit-button" runat="server"
                                                        Width="40px" Enabled="false" Style="border-style: solid; border-width: 1px; background-repeat: no-repeat;
                                                        background-color: transparent; float: left; margin: 0; height: 30px;" OnClick="btnprevious_Click" /> <%-- OnClick="btnprevious_Click"--%>
                                                    <asp:TextBox runat="server" ID="txtPage" Style="width: 40px; border-style: solid;
                                                        border-width: 1px; border-color: Gray; height: 30px; float: left; margin: 0;
                                                        text-align: center;" Text="1" CssClass="form-control" ReadOnly="true" />
                                                    <asp:Button ID="btnnext" Text=">" CssClass="form-submit-button" runat="server" Style="border-style: solid;
                                                        border-width: 1px; background-repeat: no-repeat; background-color: transparent;
                                                        float: left; margin: 0; height: 30px;" Width="40px" OnClick="btnnext_Click" />  <%-- OnClick="btnnext_Click"--%>
                                               </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </center>
                        </div>
                                   </div>
                       </div>
              <%--     </ContentTemplate>
                                  </asp:UpdatePanel>--%>
                </center>            
                 </div>
                             
            </div>
            </ContentTemplate>
        </asp:Updatepanel>
  </ContentTemplate>
              </asp:UpdatePanel>
               </asp:Content>