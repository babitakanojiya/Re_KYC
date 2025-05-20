<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="DataPrepration.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.Admin.DataPrepration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <%: Styles.Render("~/bundles/CKYCcss") %>

    <link href="../../assets/css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../assets/js/bootstrap-multiselect.js"></script>
    <%--<link href="styles/jquery-ui.css" rel="stylesheet" />  
<script src="scripts/jquery-1.11.3.min.js"></script>  
<script src="scripts/jquery-ui.js"></script> --%>
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

        function dataPRPRValidate() {
            var strcontent = "EmptyPagePlaceholder_";

            if (document.getElementById(strcontent + "ddldpt") != null) {
                if (document.getElementById(strcontent + "ddldpt").value == "") {
                    alert("Please Select Data Preparation Type Field.");
                    document.getElementById(strcontent + "ddldpt").focus();
                    return false;
                }
                //}
                if (document.getElementById("EmptyPagePlaceholder_txtED").value == "") {
                        alert("Please enter Effective Date.");
                        return false;
                    }
                }
            }
            function dataSettblcolValidate() {
                var strcontent = "EmptyPagePlaceholder_";
                //if (document.getElementById(strcontent + "ddlsttblcol") != null) {
                //    if (document.getElementById(strcontent + "ddlsttblcol").value == "") {
                //        alert("Please Select Set Table Column Field.");
                //        document.getElementById(strcontent + "ddlsttblcol").focus();
                //        return false;
                //    }
                //}
                var ddlsvf = document.getElementById("EmptyPagePlaceholder_ddlsvf")
                 <%--   ("<%=ddlsvf.ClientID%>");--%>
                    var optionSelIndex = ddlsvf.options[ddlsvf.selectedIndex].value;
                    if (optionSelIndex == 0) {
                        alert("Please select Set Value From Field.");
                        return false;
                    }
                    var ddlsttblcol = document.getElementById("EmptyPagePlaceholder_ddlsttblcol")
                       <%-- ("<%=ddlsttblcol.ClientID%>");--%>
                    var optionSelIndex = ddlsttblcol.options[ddlsttblcol.selecteditem.text].value;
                    if (optionSelIndex == "-- SELECT --") {
                        alert("Please select Set Table Column Field.");
                        return false;
                    }
                }
                function defineJoinValidate() {
                    debugger;
                    var strcontent = "EmptyPagePlaceholder";


                    var ddlsttbcol = document.getElementById("EmptyPagePlaceholder_ddlsttbcol")
                        <%--("<%=ddlsttbcol.ClientID%>");--%>
            var optionSelIndex = ddlsttbcol.options[ddlsttbcol.selectedIndex].value;
            if (optionSelIndex == 0) {
                alert("Please select Set Table Column Field.");
                return false;
            }

            if (document.getElementById(strcontent + "ddlfrmtbcol") != null) {
                if (document.getElementById(strcontent + "ddlfrmtbcol").value == "") {
                    alert("Please Select From Table Column Field.");
                    document.getElementById(strcontent + "ddlfrmtbcol").focus();
                    return false;
                }
                //}

                var ddljstat = document.getElementById("EmptyPagePlaceholder_ddljstat");
                   <%-- ("<%=ddljstat.ClientID%>");--%>
                var optionSelIndex = ddljstat.options[ddljstat.selectedIndex].value;
                var csdte = document.getElementById("EmptyPagePlaceholder_txtces");
                    <%--("<%=txtces.ClientID%>").value;--%>
                if (optionSelIndex == 2 && csdte == "") {
                    alert("Please Select Cease Date.")
                    return false;
                }
            }
        }
        function definewhrcndn() {
            var strcontent = "EmptyPagePlaceholder_";
            if (document.getElementById(strcontent + "ddltb") != null) {
                if (document.getElementById(strcontent + "ddltb").value == "") {
                    alert("Please Select Table.");
                    document.getElementById(strcontent + "ddltb").focus();
                    return false;
                }
            }
            if (document.getElementById(strcontent + "ddlcn") != null) {
                if (document.getElementById(strcontent + "ddlcn").value == "") {
                    alert("Please Select Column Name.");
                    document.getElementById(strcontent + "ddlcn").focus();
                    return false;
                }
            }
            if (document.getElementById(strcontent + "ddlop") != null) {
                if (document.getElementById(strcontent + "ddlop").value == "") {
                    alert("Please Select Operator.");
                    document.getElementById(strcontent + "ddlop").focus();
                    return false;
                }
            }
           <%--  if (document.getElementById("EmptyPagePlaceholder_txtwhrcolval"))
               ("<%=txtwhrcolval.ClientID%>").value == "")--%>
            //{
            //            alert("Please enter Column Value.");
            //            return false;
            //        }
        }

        function PopulateCalender1() {
            debugger;
            minDate: new Date()
            <%--$("#<%= txtcd.ClientID  %>").datepicker({--%>
                $("#EmptyPagePlaceholder_txtcd").datepicker({
                            dateFormat: 'dd/mm/yy',
                            changeMonth: true,
                            changeYear: true,
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
                        var EffDate = document.getElementById("txtED").value;
                          <%--  $('#<%= txtED.ClientID  %>').val();--%>
                        var CeDate = document.getElementById("txtcd").value;
                            <%--$('#<%= txtcd.ClientID  %>').val();--%>
                        var strcontent = "EmptyPagePlaceholder_";
            debugger;
            if (EffDate != "" && CeDate != "") {
                if (!checkDateIsGreaterThanToday(EffDate, CeDate)) {
                    // alert("Please select the correct cease date");
                    document.getElementById("EmptyPagePlaceholder_txtcd").value = "";
                    alert("fail");
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

    </script>
           <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

     <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="page-container">
                <center>
 <div id="divdata" runat="server" class="panel panel-success" style="margin-left: 2%; margin-right: 2%; margin-top: 0.5%">
                    <div id="Div6" runat="server" class="panel-heading" onclick="ShowReqDtl1('ctl00_ContentPlaceHolder1_divd','myImg');return false;">
                        <div class="row">
                            <div class="col-sm-10" style="text-align: left">
                                <asp:Label ID="lblhdr" Text="Data Preparation" runat="server" Font-Size="19px" />                                  
                            </div>
                            <div class="col-sm-2">
                                <span id="btnToggleNew" class="glyphicon glyphicon-collapse-down" style="float: right; 
                                    padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div>
                    </div>

                      <div id="divd" runat="server" class="panel-body" style="display: block; margin-top: 0.9%; margin-bottom: 0.9%">
                   <%--<asp:UpdatePanel ID="updadd" runat="server">
                    <ContentTemplate>--%>
                           <div class="row" style="margin-bottom: 5px;">
                                <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lbldtaprp" Text="Data Preparation ID" runat="server" CssClass="control-label" />
                               </div>
                               <div class="col-sm-3" style="text-align: left">
                               <asp:TextBox ID="txtdprpId" runat="server" CssClass="form-control" Enabled="false"/>
                               </div>
                                 <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="LBLINTID" Text="Integration ID" runat="server" CssClass="control-label" />
                               </div>
                               <div class="col-sm-3" style="text-align:center">
                               <asp:Label ID="lblintgrtnId" runat="server" CssClass="form-control-static new_text_new" Font-Size="Medium" />
                               </div>
                               </div>
                        <div class="row" style="margin-bottom: 5px;">
                           
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="Label9" Text="Data Preparation Type" runat="server" CssClass="control-label" />
                                <span id="Span14" runat="server" style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddldpt" runat="server"  CssClass="form-control" TabIndex="6"  AutoPostBack="true" OnSelectedIndexChanged="ddldpt_SelectedIndexChanged"> 
                                            <%--OnSelectedIndexChanged="ddldpt_SelectedIndexChanged"--%>
                                          </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                              <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblanthrtbl" Text="Set From Another Table" runat="server" CssClass="control-label" />
                                  <span id="Span1" runat="server" style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <%--<asp:RadioButton ID="rdanthrtbly" runat="server" Text="Yes" GroupName="Set From Another Table" Checked="true"  OnCheckedChanged="rdanthrtbly_CheckedChanged"></asp:RadioButton>
                                <asp:RadioButton ID="rdanthrtbln" runat="server" Text="No" GroupName="Set From Another Table"  OnCheckedChanged="rdanthrtbln_CheckedChanged" AutoPostBack="false"></asp:RadioButton>
                                --%> 
                                  <asp:RadioButtonList ID="RDSETFRMANTHER" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="RDSETFRMANTHER_SelectedIndexChanged" >
                                 <%--     OnSelectedIndexChanged="RDSETFRMANTHER_SelectedIndexChanged"--%>
                                    <asp:ListItem Text="Yes" Value="1" Selected="True" ></asp:ListItem>
                                    <asp:ListItem Text="No" Value="0" ></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                             </div>

                        <div class="row" style="margin-bottom: 5px;">
                           
                             <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblsettbl" Text="Set Table" runat="server" CssClass="control-label" />
                                <span id="Span3" runat="server" style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlsettbl" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="6"  Enabled="true">
                                            <%--OnSelectedIndexChanged="ddlsettbl_SelectedIndexChanged"--%>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                              <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblfrmtbl" Text="From Table" runat="server" CssClass="control-label" />
                                <span id="Span2" runat="server" style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlfrmtbl" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="6" Enabled="true">
                                           <%-- OnSelectedIndexChanged="ddlfrmtbl_SelectedIndexChanged" --%>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            
                            </div>
                       <div class="row" style="margin-bottom: 5px;">
                             <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lbled" Text="Effective From" runat="server" CssClass="control-label" />
                                  <span id="Span10" runat="server" style="color: red">*</span>
                            </div>
                           <div class="col-sm-3" style="text-align: left">
                                  <asp:TextBox ID="txtED" runat="server" CssClass="form-control"  placeholder="DD/MM/YYYY" Enabled="false"/> <%--onmousedown="PopulateEff_date1(); return false;" onmouseup="PopulateCalender()"--%>
                               <%-- onmousedown="PopulateCalender()" onmouseup="PopulateCalender()" --%>
                               </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblcd" Text="Cease Date" runat="server" CssClass="control-label" />
                                 <span id="Spancsdt1" runat="server" style="color: red" visible="false">*</span>
                            </div>
                           <div class="col-sm-3" style="text-align: left">
                                 <asp:TextBox ID="txtcd" runat="server" CssClass="form-control"  placeholder="DD/MM/YYYY"  onmouseup="PopulateCalender1()" onmousedown="PopulateCalender1()" Enabled="False" onchange="PopulateCalender1()" />
                                </div>
                           </div>
                       <div class="row" style="margin-bottom: 5px;">
                      <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblst" Text="Status" runat="server" CssClass="control-label" />
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlst" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="6" Enabled="false" OnSelectedIndexChanged="ddlst_SelectedIndexChanged">
                                           <%-- OnSelectedIndexChanged="ddlst_SelectedIndexChanged"--%>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                             <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="Label10" Text="Processor Name" runat="server" CssClass="control-label" />
                            </div>
                           <div class="col-sm-3" style="text-align: left">
                                 <asp:TextBox ID="txtprcnme" runat="server" CssClass="form-control" Enabled="false" />
                                </div>
                           </div>
                           </div>

                      <div class="row" style="margin-top: 12px;" runat="server"  id="div2"  >
                            <div class="col-sm-12" align="center">
                                <asp:LinkButton ID="btndataprep" runat="server" CssClass="btn btn-primary" Enabled="true"  OnClientClick="return dataPRPRValidate();" OnClick="btndataprep_Click"> <%--OnClick="btndataprep_Click"--%>
                                        <span class="glyphicon glyphicon-floppy-disk BtnGlyphicon" style="color: White;"></span> Save
                                </asp:LinkButton>
                                <asp:LinkButton ID="btndataprepclr" runat="server" CssClass="btn btn-danger" OnClick="btndataprepclr_Click" >  <%--OnClick="btndataprepclr_Click"--%> 
                                        <span class="glyphicon glyphicon-erase BtnGlyphicon" style="color: White;"></span> Clear
                                </asp:LinkButton>
                                 <asp:LinkButton ID="btndataprep_upd" runat="server" CssClass="btn btn-primary"  OnClientClick="return dataPRPRValidate();" Visible="false" OnClick="btndataprep_upd_Click">
                                     <%--OnClick="btndataprep_upd_Click" --%>  
                                     <span class="glyphicon glyphicon-floppy-disk BtnGlyphicon" style="color: White;"></span> Update
                                </asp:LinkButton>
                              <asp:LinkButton ID="btndataprepcncl" runat="server" CssClass="btn btn-danger"   OnClientClick="gotoHome(); return false;"> <%--OnClick="btndataprepcncl_Click"--%>
                                    <span class="glyphicon glyphicon-remove BtnGlyphicon" style="color: White;"></span> Cancel
                                   </asp:LinkButton>
                            </div>
                         </div>

                    <%----------------------- 1st GridView Start ---------------------------------%>
                          <div id="div5" runat="server" style="width: 95%; border: none; margin: 0px 0 !important;"
                            class="table-scrollable">
                               <div id="divGriddata" runat="server" style="width: 100%; overflow-x:scroll" >
      <asp:UpdatePanel ID="updgrd1" runat="server">
            <ContentTemplate>
              <asp:GridView ID="grddp" runat="server" AutoGenerateColumns="false" PageSize="10" AllowSorting="True" AllowPaging="true" CssClass="footable" >
                 <RowStyle CssClass="GridViewRowNEW"></RowStyle>
                 <PagerStyle CssClass="disablepage" />
                  <HeaderStyle CssClass="gridview th" />
                   <EmptyDataTemplate>
                     <asp:Label ID="lblDPRCF" Text="No records found" ForeColor="Red" CssClass="control-label" runat="server" />
                       </EmptyDataTemplate>
                   <Columns>
                          <asp:TemplateField HeaderText="DATA_PRPRTN_ID" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" SortExpression="DATA_PRPRTN_ID">
                         <ItemTemplate>
                            <asp:Label ID="lbldpid" Text='<%# Bind("DATA_PRPRTN_ID")%>' runat="server" ></asp:Label>
                             <asp:HiddenField ID="hdndpi" runat="server" Value='<%#Bind("DATA_PRPRTN_ID")%>' />   <%--ClientIDMode="Static" --%>
                             <asp:HiddenField ID="hdnintid" runat="server" Value='<%#Bind("INTGRTN_ID")%>' />
                             <asp:Label ID="lbldpseq" runat="server" Text='<%#Bind("SEQNO")%>' Visible="false" />    
                                                     </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Center" />
                             <ItemStyle  HorizontalAlign="Center" />
                        </asp:TemplateField>
                      <asp:TemplateField HeaderText="Data Preparation type" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" SortExpression="DATA_PRPR_TYPE_Desc">
                         <ItemTemplate>
                            <asp:Label ID="lbldptdesc" Text='<%# Bind("DATA_PRPR_TYPE_Desc")%>' runat="server"></asp:Label>
                             <asp:HiddenField ID="hdndpt" runat="server" Value='<%# Bind("DATA_PRPR_TYPE")%>' />
                            </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Center" />
                             <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="Set From Another Table" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" SortExpression="SET_FRM_ANTHR_TBL">
                         <ItemTemplate>
                            <asp:Label ID="lblsttban" Text='<%# Bind("SET_FRM_ANTHR_TBL")%>' runat="server"></asp:Label>
                              <asp:Label ID="lblsfanvl" Text='<%# Bind("SET_FRM_ANTHR_TBL_VLUE")%>' runat="server" Visible="false"></asp:Label>
                            </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Center" />
                             <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="Table Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" SortExpression="SET_TBL">
                         <ItemTemplate>
                            <asp:Label ID="lbltb" Text='<%# Bind("SET_TBL")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Center" />
                             <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="From Table" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" SortExpression="FRM_TBL">
                         <ItemTemplate>
                            <asp:Label ID="lblfrmtb" Text='<%# Bind("FRM_TBL")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Center" />
                             <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                           <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" SortExpression="Status">
                         <ItemTemplate>
                            <asp:Label ID="lbl1st" Text='<%# Bind("STATUS")%>' runat="server"></asp:Label>
                              <asp:Label ID="Lblstatus1" Text='<%# Bind("ParamValue")%>' runat="server" Visible="false"></asp:Label>
                            </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Center" />
                             <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Processor Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" SortExpression="PRCSR_NAME">
                         <ItemTemplate>
                            <asp:Label ID="lblprcnm" Text='<%# Bind("PRCSR_NAME")%>' runat="server"></asp:Label>
                                 </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Center" />
                             <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                           <asp:TemplateField HeaderText="Effective Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" SortExpression="EFF_FRM_DT">
                         <ItemTemplate>
                            <asp:Label ID="lblefd" Text='<%# Bind("EFF_FRM_DT")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Center" />
                             <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="Cease Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" SortExpression="CEASE_DT">
                         <ItemTemplate>
                            <asp:Label ID="lblcgd" Text='<%# Bind("CEASE_DT")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Center" />
                             <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="true">
                         <ItemTemplate>
                            <asp:LinkButton ID="lnksettbcol" runat="server" Text="Set Table Column" Font-Bold="true"  data-myData='<%# Eval("SET_FRM_ANTHR_TBL") %>' Visible='<%# Eval("DATA_PRPR_TYPE_Desc").ToString() =="AUTOMATIC" ? true : false %>' ForeColor="#3333cc" OnClick="lnksettbcol_Click" >       
                            </asp:LinkButton><br /> <%--<span id="sttbcol" runat="server"></span> |  OnClick="lnksettbcol_Click"--%>
                               <asp:LinkButton ID="lnkdj" runat="server" Text="Define Join" Font-Bold="true" Visible='<%# Eval("SET_FRM_ANTHR_TBL").ToString() == "Yes" && Eval("DATA_PRPR_TYPE_Desc").ToString() =="AUTOMATIC" ? true : false %>' ForeColor="#3333cc" OnClick="lnkdj_Click"> <%--Visible='<%# Eval("SET_FRM_ANTHR_TBL").ToString() == "Yes" ? true : false %>'--%>
                            </asp:LinkButton> <br /><%--<span id="defj" runat="server"> </span> | OnClick="lnkdj_Click" --%>
                               <asp:LinkButton ID="lnkwhcnd" runat="server" Text="Define Where Condition" Font-Bold="true" data-myData='<%# Eval("SET_FRM_ANTHR_TBL") %>' Visible='<%# Eval("DATA_PRPR_TYPE_Desc").ToString() =="AUTOMATIC" ? true : false %>' ForeColor="#3333cc" OnClick="lnkwhcnd_Click">
                                  <%--  OnClick="lnkwhcnd_Click"--%>
                            </asp:LinkButton>
                            </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Center" />
                             <ItemStyle HorizontalAlign="Center"  />
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                         <ItemTemplate>
                            <asp:LinkButton ID="lnkdpedt" runat="server" Text="Edit" ShowEditButton="true" OnClick="lnkdpedt_Click" ForeColor="#3333cc">       <%--OnClick="lnkdpedt_Click"--%>
                            </asp:LinkButton>
                             </ItemTemplate>
                           </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                         <ItemTemplate>
                             <asp:LinkButton ID="lnkdpdel" runat="server" Text="Delete"  OnClientClick="return confirm('Are you sure you want to Delete?'); return true;" OnClick="lnkdpdel_Click" ForeColor="#3333cc">
                                 <%--OnClick="lnkdpdel_Click"--%>
                                  </asp:LinkButton> 
                            </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Center" />
                             <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>        
                 </asp:GridView>
            </ContentTemplate>
            </asp:UpdatePanel>
                        <br/>
                                    <div class="pagination" style="padding: 10px;">
                      
                                <table>
                                    <tr>
                                        <td style="white-space: nowrap;">
                                            <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                                <ContentTemplate>
                                                    <asp:Button ID="btnprevious" Text="<" CssClass="form-submit-button" runat="server"
                                                        Width="40px" Enabled="false" Style="border-style: solid; border-width: 1px; background-repeat: no-repeat;
                                                        background-color: transparent; float: left; margin: 0; height: 30px;"  OnClick="btnprevious_Click" Visible="false" />  <%--OnClick="btnprevious_Click"--%>
                                                     <asp:TextBox runat="server" ID="txtPage" Style="width: 50px; border-style: solid;
                                                        border-width: 1px; border-color: Gray; height: 30px; float: left; margin: 0;
                                                        text-align: center;" CssClass="form-control" ReadOnly="true"  Text="1" Visible="false" />
                                                    <asp:Button ID="btnnext" Text=">" CssClass="form-submit-button" runat="server" Style="border-style: solid;
                                                        border-width: 1px; background-repeat: no-repeat; background-color: transparent;
                                                        float: left; margin: 0; height: 30px;" Width="40px" OnClick="btnnext_Click"  Visible="false" />   <%--OnClick="btnnext_Click"--%>
                                                 </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                                
                        </div>
                                   </div>
                              </div>
<%------------------------------ 1st GridView End --------------------------------------------%>   
                        

            <%------------ Set Table Column ---------------%>
                        <div id="divsettblcol" runat="server" class="panel panel-success" style="margin-left: 2%; margin-right: 2%; margin-top: 0.5%" visible="false">
                    <div id="Div1" runat="server" class="panel-heading" onclick="ShowReqDtl1('ctl00_ContentPlaceHolder1_divcmphdr','myImg');return false;">
                        <div class="row">
                            <div class="col-sm-10" style="text-align: left">
                                <asp:Label ID="Label6" Text="Set Table Column" runat="server" Font-Size="19px" />                                  
                            </div>
                            <div class="col-sm-2">
                                <span id="btnToggleNew1" class="glyphicon glyphicon-collapse-down" style="float: right; 
                                    padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div>
                    </div>
                                  
              <div id="divtblcolbdy" runat="server" class="panel-body" style="display: block; margin-top: 0.9%; margin-bottom: 0.9%" visible="false">
                  <div class="row" style="margin-bottom: 5px;">
                             <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblsvf" Text="Set Value From" runat="server" CssClass="control-label" />
                               <span id="Span15" runat="server" style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlsvf" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="6" OnSelectedIndexChanged="ddlsvf_SelectedIndexChanged">
                                          <%--  OnSelectedIndexChanged="ddlsvf_SelectedIndexChanged"--%>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                      </div>     
                   <div class="row" style="margin-bottom: 5px;">
                             <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="Label1" Text="Set Table Column" runat="server" CssClass="control-label" />
                               <span id="Spanstc" runat="server" style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlsttblcol" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="6"  >
                                           <%-- OnSelectedIndexChanged="ddlsttblcol_SelectedIndexChanged"--%>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                         <%--<div class="row" style="margin-bottom: 5px;">--%>
                             <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblvalstcol" Text="Value Set Column" runat="server" CssClass="control-label" />
                                <span id="spnvlstco" runat="server" style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlvalstcol" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="6" >
                                          </asp:DropDownList>
                                                 </ContentTemplate>
                                </asp:UpdatePanel>
                                 <div class="input-group-btn">
                                <div class="">
                                        <asp:LinkButton ID="Lnkvalstcol" runat="server" style="width:15%;" CssClass="btn btn-danger" Visible="false" >
                                             <%-- OnClientClick="return fnselSrcTblCol();"OnClick="Lnkvalstcol_Click"--%>
                                        <span class="glyphicon glyphicon-plus BtnGlyphicon" style="color: White;"></span> 
                                    </asp:LinkButton>
                                    <%--data-toggle="modal" data-target="#myModal" <span class="glyphicon glyphicon-plus" data-toggle="modal" data-target="#myModal" style="color:white;"></span>--%>
                                </div>
                            </div>
                            </div>
                             </div>
                   <div class="row" style="margin-bottom: 5px;">
                             <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lbls" Text="Effective From" runat="server" CssClass="control-label" />
                                  <span id="Span11" runat="server" style="color: red">*</span>
                            </div>
                           <div class="col-sm-3" style="text-align: left">
                                  <asp:TextBox ID="txtEF" runat="server" CssClass="form-control"  placeholder="DD/MM/YYYY" Enabled="false" /> <%-- onmousedown="PopulateCalender()" onmouseup="PopulateCalender()" --%>
                               <%-- onmousedown="PopulateCalender()" onmouseup="PopulateCalender()" --%>
                               </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblc" Text="Cease Date" runat="server" CssClass="control-label" />
                                  <span id="spancsdt2" runat="server" style="color: red" visible="false">*</span>
                            </div>
                           <div class="col-sm-3" style="text-align: left">
                                 <asp:TextBox ID="txtCED" runat="server" CssClass="form-control"  placeholder="DD/MM/YYYY" onmousedown="PopulateCalender2()" onmouseup="PopulateCalender2()" Enabled="false" onfocus="PopulateCalender2()" onchange="PopulateCalender2()" />
                               </div>
                           </div>
                  <div class="row" style="margin-bottom: 5px;">
                      <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblsta" Text="Status" runat="server" CssClass="control-label" />
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlsest" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="6" Enabled="false" OnSelectedIndexChanged="ddlsest_SelectedIndexChanged" >
                                            <%--OnSelectedIndexChanged="ddlsest_SelectedIndexChanged" --%>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                      <div id="vlset" class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblvl" Text="Value" runat="server" CssClass="control-label" Visible="false" />
                            </div>
                      <div class="col-sm-3" style="text-align: left">
                                 <asp:TextBox ID="txtval" runat="server" CssClass="form-control" Visible="false"  />
                               </div>
                  </div>
                   <div class="row" style="margin-top: 12px;" runat="server"  id="div3"  >
                            <div class="col-sm-12" align="center">
                                <asp:LinkButton ID="btnsetadd" runat="server" CssClass="btn btn-primary" OnClientClick="return dataSettblcolValidate();" OnClick="btnsetadd_Click">   <%--OnClick="btnsetadd_Click"--%>
                                        <span class="glyphicon glyphicon-plus BtnGlyphicon" style="color: White;"></span> Add
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnsetclr" runat="server" CssClass="btn btn-danger" OnClick="btnsetclr_Click" >  <%--OnClick="btnsetclr_Click"--%>
                                        <span class="glyphicon glyphicon-erase BtnGlyphicon" style="color: White;"></span> Clear
                                </asp:LinkButton>
                               <asp:LinkButton ID="btnsetupd" runat="server" CssClass="btn btn-primary" OnClientClick="return dataSettblcolValidate();" Visible="false" OnClick="btnsetupd_Click">   <%--OnClick="btnsetupd_Click"--%>
                                    <span class="glyphicon glyphicon-floppy-disk BtnGlyphicon" style="color: White;"></span>Update
                                   </asp:LinkButton>
                                 <asp:LinkButton ID="btnsetcncl" runat="server" CssClass="btn btn-danger"  OnClientClick="gotoHome(); return false;">  <%--OnClick="btnsetcncl_Click"--%>
                                    <span class="glyphicon glyphicon-remove BtnGlyphicon" style="color: White;"></span>Cancel
                                   </asp:LinkButton>
                            </div>
                         </div> <br />
 <%------------------- 2nd Gridview Start ------------------------%>
            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
            <ContentTemplate>
              <asp:GridView ID="gridsttblcol" runat="server" AutoGenerateColumns="false" Width="100%" PageSize="10" AllowSorting="True" AllowPaging="true" 
                  CssClass="footable"  > <%-- OnRowDataBound="gridsttblcol_RowDataBound"--%>
                 <RowStyle CssClass="GridViewRowNEW"></RowStyle>
                 <PagerStyle CssClass="disablepage" />
                  <HeaderStyle CssClass="gridview th" />
                    <EmptyDataTemplate>
                     <asp:Label ID="lblsettblcol" Text="No records found" ForeColor="Red" CssClass="control-label" runat="server" />
                       </EmptyDataTemplate>
                   <Columns>
                           <asp:TemplateField HeaderText="Set Value from" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" SortExpression="SET_VAL_FRM_desc">
                         <ItemTemplate>
                            <asp:Label ID="lblstvlfrm" Text='<%# Bind("SET_VAL_FRM_desc")%>' runat="server"></asp:Label>
                             <asp:HiddenField ID="hdnstvlfrm" runat="server" Value='<%#Bind("DATA_PRPRTN_ID")%>' />
                               <asp:Label ID="LBLSETVALFRM" runat="server" Text='<%#Bind("SET_VAL_FRM")%>' Visible="false" />  
                             <asp:Label ID="lblstvlseq" runat="server" Text='<%#Bind("SEQNO")%>' Visible="false" />    
                            </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Center" />
                             <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                      <asp:TemplateField HeaderText="Set Table Column" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" SortExpression="SET_TBL_COL">
                         <ItemTemplate>
                            <asp:Label ID="lbltbcol" Text='<%# Bind("SET_TBL_COL")%>' runat="server"></asp:Label>
                             <asp:HiddenField ID="hdndpistc" runat="server" Value='<%#Bind("DATA_PRPRTN_ID")%>' />
                             <asp:Label ID="lblSTid" Text='<%# Bind("DATA_PRPRTN_ID")%>' runat="server" Visible="false"></asp:Label>                   
                             <asp:Label ID="lblstseq" runat="server" Text='<%#Bind("SEQNO")%>' Visible="false" />    
                            </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Center" />
                             <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="Value Set Column" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" SortExpression="VAL_SET_COL">
                         <ItemTemplate>
                            <asp:Label ID="lblstvl" Text='<%# Bind("VAL_SET_COL")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Center" />
                             <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                      <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" SortExpression="Status">
                         <ItemTemplate>
                            <asp:Label ID="lbl2st" Text='<%# Bind("STATUS")%>' runat="server"></asp:Label>
                              <asp:Label ID="Lblstatus2" Text='<%# Bind("ParamValue")%>' runat="server" Visible="false"></asp:Label>
                            </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Center" />
                             <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="Value" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" SortExpression="VALUE">
                         <ItemTemplate>
                            <asp:Label ID="lblVALUE" Text='<%# Bind("VALUE")%>' runat="server"></asp:Label>
                                           </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Center" />
                             <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                           <asp:TemplateField HeaderText="Effective Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" SortExpression="EFF_FRM_DT">
                         <ItemTemplate>
                            <asp:Label ID="lbl2efd" Text='<%# Bind("EFF_FRM_DT")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Center" />
                             <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="Cease Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" SortExpression="CEASE_DT">
                         <ItemTemplate>
                            <asp:Label ID="lbl2cd" Text='<%# Bind("CEASE_DT")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Center" />
                             <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                         <ItemTemplate>
                            <asp:LinkButton ID="lnkedstc" runat="server" Text="Edit" ShowEditButton="true"  data-myData='<%# Eval("SET_TBL_COL") %>' OnClick="lnkedstc_Click" ForeColor="#3333cc">    <%--data-myData='<%# Eval("SET_TBL_COL") %>' OnClick="lnkedstc_Click"    --%>
                            </asp:LinkButton> </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                         <ItemTemplate>
                             <asp:LinkButton ID="lnksttbldel" runat="server" Text="Delete" OnClientClick="return confirm('Are you sure you want to Delete?'); return true;" OnClick="lnksttbldel_Click" ForeColor="#3333cc">  <%--OnClick="lnksttbldel_Click"--%>
                                  </asp:LinkButton> 
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                             <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>        
                 </asp:GridView>
                <asp:HiddenField ID="hdnCnt" runat="server" />
                <asp:HiddenField ID="hdnFlag" runat="server" />
            </ContentTemplate>
            </asp:UpdatePanel>
                  </div>
<%------------------- 2nd Gridview End ------------------------%>    


                     <%------------ Define Joins Column ---------------%>    
                    <div id="divp" runat="server" class="panel panel-success" style="margin-left: 2%; margin-right: 2%; margin-top: 0.5%" visible="false">
                    <div id="Divdjc" runat="server" class="panel-heading" onclick="ShowReqDtl1('ctl00_ContentPlaceHolder1_divcmphdr','myImg');return false;">
                        <div class="row">
                            <div class="col-sm-10" style="text-align: left">
                                <asp:Label ID="Label2" Text="Define Joins Column" runat="server" Font-Size="19px" />                                  
                            </div>
                            <div class="col-sm-2">
                                <span id="btnToggleNew2" class="glyphicon glyphicon-collapse-down" style="float: right; 
                                    padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div>
                    </div>
                        
                                 <div id="divjc" runat="server" class="panel-body" style="display: block; margin-top: 0.9%; margin-bottom: 0.9%" visible="false">
                        <div class="row" style="margin-bottom: 5px;">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblsttblcol" Text="Set Table Col" runat="server" CssClass="control-label" />
                                <span id="Span4" runat="server" style="color: red">*</span>
                                </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanelco" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlsttbcol" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="6" >
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                              <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblfrmtblcol" Text="From Table Col" runat="server" CssClass="control-label" />
                                <span id="Span5" runat="server" style="color: red">*</span>
                                </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlfrmtbcol" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="6" >
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            </div>
                            <div class="row" style="margin-bottom: 5px;">
                             <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lbleff" Text="Effective From" runat="server" CssClass="control-label" />
                                  <span id="Span12" runat="server" style="color: red">*</span>
                            </div>
                           <div class="col-sm-3" style="text-align: left">
                                  <asp:TextBox ID="txteff" runat="server" CssClass="form-control"  placeholder="DD/MM/YYYY" Enabled="false" /> <%--onmousedown="PopulateCalender()" onmouseup="PopulateCalender()" --%>
                               <%-- onmousedown="PopulateCalender()" onmouseup="PopulateCalender()" --%>
                               </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblces" Text="Cease Date" runat="server" CssClass="control-label" />
                                <span id="spancsdt3" runat="server" style="color: red" visible="false">*</span>
                            </div>
                           <div class="col-sm-3" style="text-align: left">
                                 <asp:TextBox ID="txtces" runat="server" CssClass="form-control"  placeholder="DD/MM/YYYY" onmousedown="PopulateCalender3()" onmouseup="PopulateCalender3()" Enabled="false" />
                               </div>
                           </div>
                                     <div class="row" style="margin-bottom: 5px;">
                      <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lbljstat" Text="Status" runat="server" CssClass="control-label" />
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddljstat" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="6" Enabled="false" OnSelectedIndexChanged="ddljstat_SelectedIndexChanged">  <%--OnSelectedIndexChanged="ddljstat_SelectedIndexChanged"--%>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                  </div>
                         <div class="row" style="margin-top: 12px;" runat="server"  id="divbutton"  >
                            <div class="col-sm-12" align="center">
                                <asp:LinkButton ID="btnAdd" runat="server" CssClass="btn btn-primary"  OnClientClick="return defineJoinValidate();" OnClick="btnAdd_Click" > <%--OnClick="btnAdd_Click"--%>
                                        <span class="glyphicon glyphicon-plus BtnGlyphicon" style="color: White;"></span> Add
                                </asp:LinkButton>
                                <asp:LinkButton ID="lnkclrj" runat="server" CssClass="btn btn-danger" OnClick="lnkclrj_Click" >  <%--OnClick="lnkclrj_Click" --%>
                                        <span class="glyphicon glyphicon-erase BtnGlyphicon" style="color: White;"></span> Clear
                                </asp:LinkButton>
                                 <asp:LinkButton ID="BtnJ_Upd" runat="server" CssClass="btn btn-primary" Visible="false" OnClientClick="return defineJoinValidate();" OnClick="BtnJ_Upd_Click" >   <%--OnClick="BtnJ_Upd_Click"--%>
                                    <span class="glyphicon glyphicon-floppy-disk BtnGlyphicon" style="color: White;"></span>Update
                                   </asp:LinkButton>
                                <asp:LinkButton ID="btnjcncl" runat="server" CssClass="btn btn-danger" OnClientClick="gotoHome(); return false;" >
                                    <span class="glyphicon glyphicon-remove BtnGlyphicon" style="color: White;" ></span>Cancel
                                   </asp:LinkButton>
                            </div>
                         </div>  <br />
<%-------------- 3rd GridView Start -------------------------%>
       <asp:UpdatePanel ID="UpdatePanel11" runat="server">
            <ContentTemplate>
              <asp:GridView ID="grddefjoin" runat="server" AutoGenerateColumns="false" Width="100%" PageSize="10" AllowSorting="True" AllowPaging="true" CssClass="footable">
                 <RowStyle CssClass="GridViewRowNEW"></RowStyle>
                 <PagerStyle CssClass="disablepage" />
                  <HeaderStyle CssClass="gridview th" />
                    <EmptyDataTemplate>
                     <asp:Label ID="lblDefjn" Text="No records found" ForeColor="Red" CssClass="control-label" runat="server" />
                       </EmptyDataTemplate>
                   <Columns>
                      <asp:TemplateField HeaderText="Set Table Column" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" SortExpression="TBL_1_COL">
                         <ItemTemplate>
                            <asp:Label ID="lblstd" Text='<%# Bind("TBL_1_COL")%>' runat="server"></asp:Label>
                             <asp:HiddenField ID="hdnJnd" runat="server" Value='<%#Eval("JN_ID")%>' />
                             <asp:Label ID="LBLDJJD" Text='<%# Bind("JN_ID")%>' runat="server" Visible="false"></asp:Label>
                             <asp:Label ID="lbldjdp" Text='<%# Bind("DATA_PRPRTN_ID")%>' runat="server" Visible="false"></asp:Label>            
                             <asp:Label ID="lbldjseq" runat="server" Text='<%#Bind("SEQNO")%>' Visible="false" />    
                            </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Center" />
                             <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="From Table Column" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" SortExpression="TBL_2_COL">
                         <ItemTemplate>
                            <asp:Label ID="lblftc" Text='<%# Bind("TBL_2_COL")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Center" />
                             <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                           <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" SortExpression="Status">
                         <ItemTemplate>
                            <asp:Label ID="lbl3st" Text='<%# Bind("STATUS")%>' runat="server"></asp:Label>
                             <asp:Label ID="Lblstatus3" Text='<%# Bind("ParamValue")%>' runat="server" Visible="false"></asp:Label>
                            </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Center" />
                             <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                           <asp:TemplateField HeaderText="Effective Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" SortExpression="EFF_FRM_DT">
                         <ItemTemplate>
                            <asp:Label ID="lbl3efd" Text='<%# Bind("EFF_FRM_DT")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Center" />
                             <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="Cease Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" SortExpression="CEASE_DT">
                         <ItemTemplate>
                            <asp:Label ID="lbl3cd" Text='<%# Bind("CEASE_DT")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Center" />
                             <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                         <ItemTemplate>
                            <asp:LinkButton ID="lnkeddj" runat="server" Text="Edit" OnClick="lnkeddj_Click" ForeColor="#3333cc" ShowEditButton="true"  data-myData='<%# Eval("TBL_1_COL") %>'  >  <%--OnClick="lnkeddj_Click"--%>     
                            </asp:LinkButton>
                             </ItemTemplate>
                           </asp:TemplateField>
                             <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                         <ItemTemplate>
                             <asp:LinkButton ID="lnkDefjndel" runat="server" Text="Delete" OnClick="lnkDefjndel_Click" ForeColor="#3333cc" OnClientClick="return confirm('Are you sure you want to Delete?'); return true;" >  <%--OnClick="lnkDefjndel_Click"--%>
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
<%-------------- 3rd GridView End -------------------------%>
                

             <%------------ Define Where Condition  ---------------%>  
                   <div id="divjcond" runat="server" class="panel panel-success" style="margin-left: 2%; margin-right: 2%; margin-top: 0.5%" visible="false">
                    <div id="divwhcnd" runat="server" class="panel-heading" onclick="ShowReqDtl1('ctl00_ContentPlaceHolder1_divcmphdr','myImg');return false;">
                        <div class="row">
                            <div class="col-sm-10" style="text-align: left">
                                <asp:Label ID="Label3" Text="Define Where Condition" runat="server" Font-Size="19px" />                                  
                            </div>
                            <div class="col-sm-2">
                                <span id="btnToggleNew3" class="glyphicon glyphicon-collapse-down" style="float: right; 
                                    padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div> 
                    </div>

                  <div id="divwhcondn" runat="server" class="panel-body" style="display: block; margin-top: 0.9%; margin-bottom: 0.9%" visible="false">
                      <div class="row" style="margin-bottom: 5px;">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="Label4" Text="Table" runat="server" CssClass="control-label" />
                                <span id="Span6" runat="server" style="color: red">*</span>
                                </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddltb" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="6"  OnSelectedIndexChanged="ddltb_SelectedIndexChanged">
                                           <%-- OnSelectedIndexChanged="ddltb_SelectedIndexChanged"--%>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                           <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="Label5" Text="Column Name" runat="server" CssClass="control-label" />
                                <span id="Span7" runat="server" style="color: red">*</span>
                                </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlcn" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="6" >
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                          </div>     

                      <div class="row" style="margin-bottom: 5px;">
                           <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="Label7" Text="Operator" runat="server" CssClass="control-label" />
                                <span id="Span8" runat="server" style="color: red">*</span>
                                </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlop" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="6" >  
                                            <%--OnSelectedIndexChanged="ddlop_SelectedIndexChanged"--%>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                           <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="Label8" Text="Column Value" runat="server" CssClass="control-label" />
                                <span id="Span9" runat="server" style="color: red">*</span>
                                </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                    <ContentTemplate>
                                        <%--<asp:DropDownList ID="ddlcv" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="6" >
                                        </asp:DropDownList>--%>
                                        <asp:TextBox ID="txtwhrcolval" runat="server" CssClass="form-control"/>
                                  <%--      <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderlnkid" runat="server" InvalidChars=";#$@%^!*&^~`:+{}[]?><|*"
                                         FilterMode="InvalidChars" TargetControlID="txtwhrcolval" FilterType="Custom"></ajaxToolkit:FilteredTextBoxExtender>--%>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                      </div>
                  
                       <div class="row" style="margin-bottom: 5px;">
                             <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblefrm" Text="Effective From" runat="server" CssClass="control-label" />
                                  <span id="Span13" runat="server" style="color: red">*</span>
                            </div>
                           <div class="col-sm-3" style="text-align: left">
                                  <asp:TextBox ID="txtefrm" runat="server" CssClass="form-control"  placeholder="DD/MM/YYYY" Enabled="false"  />
                               <%-- onmousedown="PopulateCalender()" onmouseup="PopulateCalender()" --%>
                               </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblcsdt" Text="Cease Date" runat="server" CssClass="control-label" />
                                <span id="spncsdtwhrc" runat="server" style="color: red" visible="false">*</span>
                            </div>
                           <div class="col-sm-3" style="text-align: left">
                                 <asp:TextBox ID="txtcsdt" runat="server" CssClass="form-control"  placeholder="DD/MM/YYYY" onmousedown="PopulateCalender4()" onmouseup="PopulateCalender4()" Enabled="false" />
                               </div>
                           </div>
                      <div class="row" style="margin-bottom: 5px;">
                      <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblwhstat" Text="Status" runat="server" CssClass="control-label" />
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlwhstat" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="6" Enabled="false" OnSelectedIndexChanged="ddlwhstat_SelectedIndexChanged">
                                            <%-- OnSelectedIndexChanged="ddlwhstat_SelectedIndexChanged"--%>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                  </div>
                      <div class="row" style="margin-top: 12px;" runat="server"  id="div4"  >
                            <div class="col-sm-12" align="center">
                                <asp:LinkButton ID="btndwcsave" runat="server" CssClass="btn btn-primary"  OnClientClick="return definewhrcndn();" OnClick="btndwcsave_Click">  <%--OnClick="btndwcsave_Click"--%>
                                        <span class="glyphicon glyphicon-floppy-disk BtnGlyphicon" style="color: White;"></span> Save
                                </asp:LinkButton>
                                <asp:LinkButton ID="btndwcClr" runat="server" CssClass="btn btn-danger" OnClick="btndwcClr_Click"  >  <%--OnClick="btndwcClr_Click"--%>
                                        <span class="glyphicon glyphicon-erase BtnGlyphicon" style="color: White;"></span> Clear
                                </asp:LinkButton>
                         
                            <asp:LinkButton ID="btnwcupdte" runat="server" CssClass="btn btn-primary" Visible="false"  OnClientClick="return definewhrcndn();" OnClick="btnwcupdte_Click">
                               <%-- OnClick="btnwcupdte_Click" --%>
                                       <span class="glyphicon glyphicon-floppy-disk BtnGlyphicon" style="color: White;"></span> Update
                                </asp:LinkButton>
                            <asp:LinkButton ID="btndwcCncl" runat="server" CssClass="btn btn-danger" OnClientClick="gotoHome(); return false;" >
                                    <span class="glyphicon glyphicon-remove BtnGlyphicon" style="color: White;"></span>Cancel
                                   </asp:LinkButton>
                            </div>
                         </div> <br />
<%----------------- 4th GridView Start ---------------------%>
    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
            <ContentTemplate>
              <asp:GridView ID="grddefwhcnd" runat="server" AutoGenerateColumns="false" Width="100%" PageSize="10" AllowSorting="True" AllowPaging="true" CssClass="footable">
                 <RowStyle CssClass="GridViewRowNEW"></RowStyle>
                 <PagerStyle CssClass="disablepage" />
                  <HeaderStyle CssClass="gridview th" />
                    <EmptyDataTemplate>
                     <asp:Label ID="lblDefwhrcnd" Text="No records found" ForeColor="Red" CssClass="control-label" runat="server" />
                       </EmptyDataTemplate>
                   <Columns>
                      <asp:TemplateField HeaderText="Table Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" SortExpression="TBL_NAME">
                         <ItemTemplate>
                            <asp:Label ID="lblwctbnm" Text='<%# Bind("TBL_NAME")%>' runat="server"></asp:Label>
                             <%--<asp:HiddenField ID="hdndpiwc" runat="server" Value='<%#Bind("DATA_PRPRTN_ID")%>' />--%>
                                <asp:Label ID="lblwhrdp" Text='<%# Bind("DATA_PRPRTN_ID")%>' runat="server" Visible="false"></asp:Label>
                             <asp:Label ID="lblwcseq" runat="server" Text='<%#Bind("SEQNO")%>' Visible="false" />  
                            </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Center" />
                             <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="Column Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" SortExpression="WHR_CNDTN_COL_NAME">
                         <ItemTemplate>
                            <asp:Label ID="lblwccol" Text='<%# Bind("WHR_CNDTN_COL_NAME")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Center" />
                             <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Operator" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" SortExpression="WHR_CNDTN_OPRT">
                         <ItemTemplate>
                            <asp:Label ID="lblwcop" Text='<%# Bind("WHR_CNDTN_OPRT")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Center" />
                             <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Column Value" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" SortExpression="WHR_CNDTN_COL_VAL">
                         <ItemTemplate>
                            <asp:Label ID="lblwccv" Text='<%# Bind("WHR_CNDTN_COL_VAL")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Center" />
                             <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" SortExpression="Status">
                         <ItemTemplate>
                            <asp:Label ID="lbl4st" Text='<%# Bind("STATUS")%>' runat="server"></asp:Label>     
                              <asp:Label ID="Lblstatus4" Text='<%# Bind("ParamValue")%>' runat="server" Visible="false"></asp:Label>
                            </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Center" />
                             <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                           <asp:TemplateField HeaderText="Effective Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" SortExpression="EFF_FRM_DT">
                         <ItemTemplate>
                            <asp:Label ID="lbl4efd" Text='<%# Bind("EFF_FRM_DT")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Center" />
                             <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="Cease Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" SortExpression="CEASE_DT">
                         <ItemTemplate>
                            <asp:Label ID="lbl4cd" Text='<%# Bind("CEASE_DT")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Center" />
                             <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                         <ItemTemplate>
                            <asp:LinkButton ID="lnkedt" runat="server" Text="Edit" ShowEditButton="true"  data-myData='<%# Eval("TBL_NAME") %>' OnClick="lnkedt_Click" ForeColor="#3333cc" >  <%--OnClick="lnkedt_Click"     --%>
                            </asp:LinkButton> 
                             </ItemTemplate>
                           </asp:TemplateField>
                              <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                         <ItemTemplate>
                             <asp:LinkButton ID="lnkwhrdel" runat="server" Text="Delete" OnClientClick="return confirm('Are you sure you want to Delete?'); return true;" OnClick="lnkwhrdel_Click" ForeColor="#3333cc" >  <%--OnClick="lnkwhrdel_Click"--%>
                                  </asp:LinkButton> 
                            </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Center" />
                             <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>        
                 </asp:GridView>
            </ContentTemplate>
            </asp:UpdatePanel>
<%----------------- 4th GridView End ----------------------%>
                  </div>


             





                    </center>
                </div>
            </ContentTemplate>
         </asp:UpdatePanel>

</asp:Content>
