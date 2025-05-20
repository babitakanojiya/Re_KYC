<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="ErrorHandling.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.Admin.ErrorHandling" %>
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

        function ShowReqDtl1(divName, btnName) {

            try {
                var objnewdiv = document.getElementById(divName)
                var objnewbtn = document.getElementById(btnName);
                if (objnewdiv.style.display == "block") {
                    objnewdiv.style.display = "none";
                }
                else {
                    objnewdiv.style.display = "block";
                }
            }
            catch (err) {
                ShowError(err.description);
            }
        }

        function ShowReqDtl(divId, btnId, img) {
            var strContent = "ctl00_EmptyPagePlaceholder_";
            $(document.getElementById(strContent + divId)).slideToggle();
            if ($(img).attr('src') == "../../../assets/img/portlet-collapse-icon-white.png") {
                $(img).attr('src', '../../../assets/img/portlet-expand-icon-white.png');
            }
            else {
                $(img).attr('src', '../../../assets/img/portlet-collapse-icon-white.png')
            }
        }

        function CloseDiv(divId) {

            var strContent = "ctl00_EmptyPagePlaceholder_";
            if (document.getElementById(strContent + divId) != null) {
                document.getElementById(strContent + divId).style.display = 'none';
            }
        }

        function PopulateCalender1() {
            debugger;
            minDate: new Date()
            $("#<%= txtCseDate.ClientID  %>").datepicker({
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

        function Validate() {
            debugger;
            var strcontent = "ctl00_EmptyPagePlaceholder_";
            if (document.getElementById(strcontent + "ddlSourcetbl") != null) {
                if (document.getElementById(strcontent + "ddlSourcetbl").value == "") {
                    alert("Please Select Source Table.");
                    document.getElementById(strcontent + "ddlSourcetbl").focus();
                    return false;
                }
            }
            if (document.getElementById(strcontent + "ddlTblcolmnl") != null) {
                if (document.getElementById(strcontent + "ddlTblcolmnl").value == "") {
                    alert("Please Select Table Column.");
                    document.getElementById(strcontent + "ddlTblcolmnl").focus();
                    return false;
                }
            }
            if (document.getElementById(strcontent + "ddlOperator") != null) {
                if (document.getElementById(strcontent + "ddlOperator").value == "") {
                    alert("Please Select Operator.");
                    document.getElementById(strcontent + "ddlOperator").focus();
                    return false;
                }
            }
            if (document.getElementById("<%=txtColmnVal.ClientID%>").value == "") {
                alert("Please enter Column Value.");
                return false;
            }
            if (document.getElementById("<%=txtErrDesc.ClientID%>").value == "") {
                alert("Please enter Error Description.");
                return false;
            }
        }


        function gotoHome() {
            parent.location.href = parent.location.href;
        }

        function checkDate() {
            debugger;
            var EffDate = $('#<%= txtEfDate.ClientID  %>').val();
            var CeDate = $('#<%= txtCseDate.ClientID  %>').val();
            var strcontent = "ctl00_EmptyPagePlaceholder_";
            debugger;
            if (EffDate != "" && CeDate != "") {
                if (!checkDateIsGreaterThanToday(EffDate, CeDate)) {
                    // alert("Please select the correct cease date");
                    document.getElementById("ctl00_EmptyPagePlaceholder_txtCseDate").value = "";
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

<%--    <style type="text/css">
        /*.disablepage td {
            display: none;
        }

        .gridview th
        {
            padding: 3px;
            height: 40px;
            background-color: #d6d6c2;
            color: #337ab7;
            white-space: nowrap;
        }
        .color-white {
            color: #fff !important;
        }

        .grid-container {
            margin-top: 10px;
            max-height: 300px;
            overflow-y: scroll;
        }*/

        /*.divBorder {
            border: 1px solid #3399ff;
            padding-top: 5px;
            border-top: 0;
            vertical-align: top;
        }

        .divBorder1 {
            border: 1px solid #3399ff;
            border-top: 0;
            vertical-align: top;
        }

        .custom {
            width: 100px !important;
        }

        
            .grid-container::-webkit-scrollbar {
                width: 5px;
            }

            .grid-container::-webkit-scrollbar-track {
                background: #f1f1f1;
            }

            /* Handle */
            /*.grid-container::-webkit-scrollbar-thumb {
                background: #888;
            }*/

                /* Handle on hover */

        /*.bg-primary {
            color: #fff !important;
            background-color: #337ab7 !important;
        }

        .p-0 {
            padding: 0px;
        }

        .font-14 {
            font-size: 14px;
        }

        .text-black {
            color: #000;
        }

        .glyphicon {
            color: black;
            margin-left: 5px;
            margin-right: 5px;
            cursor: pointer;
        }

        .CenterAlign {
            text-align: center !important;
        }

        .LeftAlign {
            text-align: left !important;
        }

        .RightAlign {
            text-align: right !important;
        }*/

        /*.multiselect {
            overflow: hidden;
            width: 245px;
        }

        .glyphicon:hover {
            color: #fff;
        }

        .btn.focus, .btn:focus, .btn:hover {
            color: #fff !important;
        }

        .multiselect {
            overflow: hidden;
            width: 245px;
        }

        .multiselect-container {
            max-height: 200px;
            overflow: scroll;
        }*/

            /*.multiselect-container::-webkit-scrollbar {
                width: 7px;
                height: 0px;
            }

            /* Track */
            /*.multiselect-container::-webkit-scrollbar-track {
                background: #f1f1f1;
            }

            /* Handle */
            /*.multiselect-container::-webkit-scrollbar-thumb {
                background: #888;
            }

                /* Handle on hover */
               /* .multiselect-container::-webkit-scrollbar-thumb:hover {
                    background: #555;
                }*/
        .auto-style1 {
            position: relative;
            min-height: 1px;
            float: left;
            width: -2147483648%;
            left: -23px;
            top: 0px;
            padding-left: 15px;
            padding-right: 15px;
        }
           .new_text_new {
            color: #066de7;
        }

        .divBorder {
            border: 1px solid #3399ff;
            padding-top: 5px;
            border-top: 0;
            vertical-align: top;
        }

        .divBorder1 {
            border: 1px solid #3399ff;
            border-top: 0;
        }

        .disablepage {
            display: none;
        }

        .box {
            background-color: #efefef;
            padding-left: 5px;
        }

        .gridview th {
            padding: 3px;
            height: 40px;
            background-color: #F04E5E;
            color: White;
            white-space: nowrap;
        }
    </style>--%>
    <asp:ScriptManager ID="ScriptManagererrjbs" runat="server">
    </asp:ScriptManager>
    <div id="div2" runat="server"   class="panel panel-success" style="margin-left: 2%; margin-right: 2%; margin-top: 0.5%">
          <div id="Div4" runat="server" class="panel-heading" >
            <div class="row">
                <div class="col-sm-10" style="text-align: left">
                    <asp:Label ID="Label2" Text="Error Handling Condition" runat="server" Font-Size="19px" />
                </div>
                <div class="col-sm-2">
                    <span id="btnToggleNew" class="glyphicon glyphicon-collapse-down" style="float: right;  padding: 1px 10px ! important; font-size: 18px;"
                         onclick="ShowReqDtl1('ctl00_EmptyPagePlaceholder_div3','myImg1');return false;"></span>
                </div>
            </div>
        </div>
                </div>

                <div id="div3" runat="server" style="width: 96%;" class="panel-body">
                    <%--<asp:UpdatePanel ID="uperrbdy" runat="server">
                    <ContentTemplate>--%>
                    <div class="row" style="margin-bottom: 5px;">
                        <div class="col-sm-3" style="text-align: left">
                            <asp:Label ID="Sourcetbl" Text="Source Table" Style="font-size: 14px;" runat="server" CssClass="control-label" />
                            <asp:Label ID="Label4" Text="*" runat="server" Style="color: Red" />
                        </div>
                        <div class="col-sm-3">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlSourcetbl" runat="server" AutoPostBack="true" CssClass="select2-container form-control"
                                        TabIndex="7" OnSelectedIndexChanged="ddlSourcetbl_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-sm-3" style="text-align: left">
                            <asp:Label ID="lblTblcolmn" Text="Table Column" Style="font-size: 14px;" runat="server" CssClass="control-label" />
                            <asp:Label ID="Label24" Text="*" runat="server" Style="color: Red" />
                        </div>
                        <div class="col-sm-3">
                            <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlTblcolmnl" runat="server" CssClass="select2-container form-control" TabIndex="7"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlTblcolmnl_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                             <div class="row" style="margin-bottom: 5px;">
                             <div class="col-sm-3" style="text-align: left">
                            <asp:Label ID="lblOperator" Text="Operator" Style="font-size: 14px;" runat="server" CssClass="control-label" />
                            <asp:Label ID="Label8" Text="*" runat="server" Style="color: Red" />
                        </div>
                        <div class="col-sm-3">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlOperator" runat="server" CssClass="select2-container form-control" TabIndex="7"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlOperator_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                                         <div class="col-sm-3" style="text-align: left">
                            <asp:Label ID="lblColmnVal" Text="Column Value" runat="server" Style="font-size: 14px;" CssClass="control-label" />
                      <asp:Label ID="lblcolvl" Text="*" runat="server" Style="color: Red" />
                        </div>
                        <div class="col-sm-3">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                       <asp:TextBox ID="txtColmnVal" runat="server" CssClass="form-control"></asp:TextBox>
                                    <%-- <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderlnkid" runat="server" InvalidChars=";,#$@%^!*()&''%^~`_:-+{}[]?><|*"
                                         FilterMode="InvalidChars" TargetControlID="txtColmnVal" FilterType="Custom"></ajaxToolkit:FilteredTextBoxExtender>--%>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                      </div>
                    <div class="row" style="margin-bottom: 5px;">
                        <div class="col-sm-3" style="text-align: left">
                            <asp:Label ID="lblErrDsc" Text="Error Description" runat="server" Style="font-size: 14px;" CssClass="control-label" />
                            <asp:Label ID="Label6" Text="*" runat="server" Style="color: Red" />
                        </div>
                        <div class="col-sm-3">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                     <asp:TextBox ID="txtErrDesc" runat="server" CssClass="form-control"></asp:TextBox>
                                   <%-- <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" InvalidChars=";,#$@%^!*()&''%^~`_:-+{}[]?/><|*"
                                         FilterMode="InvalidChars" TargetControlID="txtErrDesc" FilterType="Custom"></ajaxToolkit:FilteredTextBoxExtender>--%>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-sm-3" style="text-align: left">
                            <asp:Label ID="lblStatus" Text="Status" runat="server" Style="font-size: 14px;" CssClass="control-label" />
                            <asp:Label ID="Label1" Text="*" runat="server" Style="color: Red" />
                        </div>
                        <div class="col-sm-3">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" CssClass="select2-container form-control"
                                        TabIndex="7" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" Enabled="false">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                       <div class="row" style="margin-bottom: 5px;">
                          <div class="col-sm-3" style="text-align: left">
                               <asp:Label ID="lblEfDate" Text="Effective Date" runat="server" Style="font-size: 14px;" CssClass="control-label" />
                             </div> 
                               <div class="col-sm-3">
                                 <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtEfDate" runat="server" CssClass="form-control" Enabled="false" placeholder="DD/MM/YYYY" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                              </div>
                         
                        <div class="col-sm-3" style="text-align: left">
                            <asp:Label ID="lblCseDate" Text="Cease Date" Style="font-size: 14px;" runat="server" CssClass="control-label" />
                        </div>
                        <div class="col-sm-3">
                            <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtCseDate" runat="server" CssClass="form-control" placeholder="DD/MM/YYYY"  onmousedown="PopulateCalender1()" onmouseup="PopulateCalender1()" Enabled="false"/>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        </div>
                     </div>
<%--    </div>--%>
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
    <div class="row" style="margin-bottom: 12px;">
        <div class="col-sm-12" align="center">
            <asp:LinkButton ID="btnSave" runat="server" CssClass="btn btn-primary" OnClick="btnSave_Click"  TabIndex="17" Visible="true" OnClientClick="return Validate();">
               <span class="glyphicon glyphicon-floppy-disk" style="color: White;"></span> Save
            </asp:LinkButton>
            <asp:LinkButton ID="BtnClear" runat="server" CssClass="btn btn-danger" OnClick="BtnClear_Click" TabIndex="17">
               <span class="glyphicon glyphicon-erase" style="color: White;"></span> Clear
            </asp:LinkButton>
      <asp:LinkButton ID="btnupd" runat="server" CssClass="btn btn-primary" TabIndex="17" Visible="true" OnClick="btnupd_Click" OnClientClick="return Validate();">
         <span class="glyphicon glyphicon-floppy-disk" style="color:White"></span> Update
         </asp:LinkButton>
              <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-danger"  TabIndex="17" OnClientClick="gotoHome(); return false;" OnClick="btnCancel_Click">
                 <span class="glyphicon glyphicon-remove" style="color:White"></span> Cancel
            </asp:LinkButton>
        </div>
    </div>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
  <%---------------- Gridview Start ---------------------%>
               <asp:updatepanel id="uppnl" runat="server">
            <ContentTemplate>
                   <div id="divcmpsrchhdrcollapse" runat="server"  style="margin-left: 2%; margin-right: 2%; margin-top: 0.5%"  class="panel ">
                 <%--       <div id="Div5" runat="server" class="panel-heading" onclick="ShowReqDtl1('ctl00_EmptyPagePlaceholder_diverrdesc','myImg');return false;">
                <div class="row">
                    <div class="col-sm-10" style="text-align: left">
                     
                        <asp:Label ID="Label3" Text="Details" runat="server" Style="font-size:19px;" />
                    </div>
                    <div class="col-sm-2">
                        <span id="Span1" class="glyphicon glyphicon-menu-hamburger" style="float: right;color:#034ea2;
                            padding: 1px 10px ! important; font-size: 18px;"></span>
                    </div>
                </div>
                            </div>--%>
                                 <div id="diverrdesc" runat="server" style="width: 100%; border: none; margin: 0px 0 !important;" class="table-scrollable">
                      <div id="divGridMap" runat="server" style="width: 100%; overflow-x:scroll" >
                          <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <asp:GridView ID="dgErrHandlng" runat="server" AutoGenerateColumns="false" Width="100%" PageSize="10" AllowSorting="True" 
                        AllowPaging="true" HorizontalAlign="Center"
                        CssClass="footable" DataKeyNames="TBL_COL"  >
                        <RowStyle CssClass="GridViewRowNew"></RowStyle>
                        <PagerStyle CssClass="disablepage" />
                        <HeaderStyle CssClass="gridview th" />
                    <EmptyDataTemplate>
                                                <asp:Label ID="lblerror" Text="No records found" ForeColor="Red"
                                                    CssClass="control-label" runat="server" />
                                            </EmptyDataTemplate>
                        <Columns>
                            
                           <asp:TemplateField HeaderText="Source Table " HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                     SortExpression="TBL_SRC" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltblsrc"  runat="server" Text='<%# Bind("TBL_SRC")%>'></asp:Label>
                                        <asp:HiddenField ID="hdnintg_id" runat="server" />
                                        <asp:Label ID="lblseq"  runat="server" Text='<%# Bind("SEQNO")%>' Visible="false"></asp:Label>
                                    </ItemTemplate>   
                                <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" CssClass="clsCenter"/>                                                                 
                                </asp:TemplateField>
                             <asp:TemplateField HeaderText="Table Column" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                     SortExpression="TBL_COL" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltblcol"  runat="server" Text='<%# Bind("TBL_COL")%>'></asp:Label>
                                    </ItemTemplate> 
                                  <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" CssClass="clsCenter"/>                                                                   
                                </asp:TemplateField>
                             <asp:TemplateField HeaderText="Operator" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                     SortExpression="OPRTR" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbloprt"  runat="server" Text='<%# Bind("OPRTR")%>'></asp:Label>
                                    </ItemTemplate>                                                                    
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Column value" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                     SortExpression="COL_VAL" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcolvl"  runat="server" Text='<%# Bind("COL_VAL")%>'></asp:Label>
                                    </ItemTemplate>                                                                    
                                </asp:TemplateField>
                             <asp:TemplateField HeaderText="Error Description" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                     SortExpression="ERR_DESC" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblerrdesc"  runat="server" Text='<%# Bind("ERR_DESC")%>'></asp:Label>
                                    </ItemTemplate> 
                                  <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" CssClass="clsCenter"/>                                                                   
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                     SortExpression="STATUS" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsts"  runat="server" Text='<%# Bind("STATUS")%>'></asp:Label>                     
                             <asp:Label ID="lblerrsttsvl" Text='<%# Bind("ParamValue")%>' runat="server" Visible="false"></asp:Label>
                                    </ItemTemplate>  
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" CssClass="clsCenter"/>                                                                  
                                </asp:TemplateField>
                            <asp:TemplateField HeaderText="Effective Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                     SortExpression="EFF_DATE" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblefd"  runat="server" Text='<%# Bind("EFF_DATE")%>'></asp:Label>
                                    </ItemTemplate>                                                                    
                                </asp:TemplateField>
                             <asp:TemplateField HeaderText="Cease Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                     SortExpression="CEASE_DTM" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcsd"  runat="server" Text='<%# Bind("CEASE_DTM")%>'></asp:Label>
                                    </ItemTemplate>                                                                    
                                </asp:TemplateField>
                              <asp:TemplateField HeaderText="Action" >
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkedit" Text="Edit" runat="server"  ForeColor="#3333cc"  OnClick="lnkedit_Click" data-myData='<%# Eval("TBL_COL") %>' /> <%-- data-myData='<%# Eval("TBL_COL") %>'--%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                              <asp:TemplateField HeaderText="Action" >
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkdelt" Text="Delete" runat="server"  ForeColor="#3333cc"  OnClick="lnkdelt_Click"  OnClientClick="return confirm('Are you sure you want to Delete?'); return true;"  /> 
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                    
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
                                     </div>
                    </div>
                    <%--</div>--%>
               </ContentTemplate>
                   </asp:updatepanel>
       <%-- </div>--%>
<%--    </div>--%>
</asp:Content>


