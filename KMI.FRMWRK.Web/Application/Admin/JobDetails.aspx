<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="JobDetails.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.Admin.JobDetails" %>
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

    
    <script type="text/javascript">

        function gotoHome() {
            parent.location.href = parent.location.href;
        }

        var PTFromTonumber = 1
        var PWTFromTonumber = 1
        var PWOTFromTonumber = 1
        var LDnumber = 1
        var TDnumber = 1
        function customAddClick(mainDiv, runtimeDiv, toBindDiv, divDesc, lblName1, lblName2) {
            debugger;
            var counter = 1;
            if (divDesc == "JS") {
                $(toBindDiv).append(customGetDynamicTxtBox(mainDiv, runtimeDiv, parseInt(counter), PTFromTonumber, divDesc, lblName1, lblName2));
                PTFromTonumber += 1;
            }
        }

        function customGetDynamicTxtBox(mainDiv, runtimeDiv, counter, number, divDesc, lblName1, lblName2) {
            debugger;
            var desc = divDesc;
            return '<div id = "' + mainDiv + number + '"><div id="' + runtimeDiv + number + '"><div class="col-sm-2" style="text-align:left"><label ID ="lbl' + lblName1 + (counter + 1) + '" Class="control-label required">Step Name <span class="counter"> ' + (number + 1) + '</span></Label></div><div class="col-sm-3"><input type="text" name = "txt' + lblName1 + number + '" placeholder="Step Name" maxlength="200"  Class="form-control"></div><div class="col-sm-2" style="text-align:left"><label ID ="lbl' + lblName2 + number + '" Class="control-label required">Procedure Name <span class="counter"> ' + (number + 1) + '</span></Label></div><div class="col-sm-3"><input type="text" name="txt' + lblName2 + number + '" placeholder="Procedure Name" maxlength="200"  Class="form-control"></div><div class="col-sm-2"><button id="btnRemove"  class="icon-button" type = "button"><span style = "font-size:x-large" class="closeimg"></span></button></div></div></div>'
        }

        $(document).on("click", "#btnRemove", function () {
            debugger;
            var a = $(this).closest("[id ^= 'divJOBStepsBody']")
            var counter = $(a)[0].id.substring("divJOBStepsBody".length);
            $(a).siblings().find()
            var labels = $(a).siblings().find('[id ^= "lblStepName"]');
            if (confirm('Are you sure you want to delete?')) {
                $(a).remove();
                PTFromTonumber = PTFromTonumber - 1;
                update_Index(PTFromTonumber, "divJOBStepsBody");
            }
        });

        function update_Index(index, div) {
            debugger;
            var a = $("[id ^= '" + div + "']");
            for (var i = 0; i < a.length; i++) {
                var counter = $(a[i]).find('.counter');
                for (var j = 0; j < counter.length; j++) {
                    $(counter[j]).html(i + 1)
                }
            }
        }



        function FnChkTime() {
            debugger;
            var ddlSchType = document.getElementById("<%=ddlSchType.ClientID%>");
            var optionSelIndex = ddlSchType.options[ddlSchType.selectedIndex].value;
            if (optionSelIndex == 0) {
                alert("Please Select Schedule Type.");
                return false;
            }
            var str = document.getElementById("<%=txtTime.ClientID%>").value
            if (str == "") {
                document.getElementById("<%=txtTime.ClientID%>").focus();
                alert("Please Enter Schedule Time in HH:MM Format");
                return false;
            }
            else if (str.length < 5) {
                document.getElementById("<%=txtTime.ClientID%>").focus();
                    alert("Invalid Time! Please Enter Schedule Time in HH:MM Format");
                    return false;
                }

            var chkVal = validate_time(str);
            if (chkVal == false) {
                document.getElementById("<%=txtTime.ClientID%>").focus();
                alert("Invalid Time! Please Enter Schedule Time in HH:MM Format");
                return false;
            }

<%--            if (str.length == 0) {
                return true;
            }
            if (str.length < 4) {
                alert("Invalid Time.");
                return false;
            }
            var x = str.indexOf(":");
            if (x < 0) {
                str = str.substr(0, 2) + ":" + str.substr(2, 2);
                document.getElementById("<%=txtTime.ClientID%>") = str;
                document.getElementById("<%=txtTime.ClientID%>").focus();
                return true;
            }
            if (
                (str.substr(0, 2) >= 0) &&
                (str.substr(0, 2) <= 24) &&
                (str.substr(3, 2) >= 0) &&
                (str.substr(3, 2) <= 59) &&
                (str.substr(0, 2) < 24 || (str.substr(0, 2) == 24 && str.substr(3, 2) == 0))
                )
                return true;

            alert("Invalid Time.");
            return false;--%>


        }

        function validate_time(time) {
            var a = true;

            var time_arr = time.split(":"); //if (time_arr.length != 2)
            if (time_arr.length != 2) {
                a = false;
            } else {
                if (isNaN(time_arr[0]) || isNaN(time_arr[1])) {
                    a = false;
                }
                if (time_arr[0] < 24 && time_arr[1] < 60) {

                } else {
                    a = false;
                }
            }
            return a;

        }

        function checkDate() {
            var EnteredDate = document.getElementById("txtEffFrom").value; //for javascript

            //  var EnteredDate = $("#txtdate").val(); // For JQuery

            var date = EnteredDate.substring(0, 2);
            var month = EnteredDate.substring(3, 5);
            var year = EnteredDate.substring(6, 10);

            var myDate = new Date(year, month - 1, date);

            var today = new Date();

            if (myDate < today)// {
                // alert("Entered date is greater than today's date ");
                // }
                // else 
            {
                alert("Entered date is less than today's date ");
            }
        }


        function callEffectiveDateFrom() {
            debugger;
            var dateArr = $("#<%=txtEffFrom.ClientID%>").val().split('-');
            $("#<%= txtEffFrom.ClientID%>").datepicker({ minDate: new Date(), changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' });
            $.datepicker.initialized = true;
            $("#<%= txtEffFrom.ClientID%>").focus();
            $("#<%=txtEffTo.ClientID%>").val('')
        }

        function callEffectiveDateTo() {
            var previousCommDate = '';
            debugger;
            $.datepicker.initialized = false;
            if ($("#<%=txtEffFrom.ClientID%>").val().trim() == '') {
                alert("Please select Effective Date.");
                $("#<%= txtEffTo.ClientID%>").val('');
                $("#<%=txtEffFrom.ClientID%>").focus();
                return;
            }

            if (previousCommDate != $("#<%=txtEffFrom.ClientID%>").val().trim()) {
                previousCommDate = $("#<%=txtEffFrom.ClientID%>").val().trim();
                $("#<%= txtEffTo.ClientID%>").val('');
            }
            $("#<%= txtEffTo.ClientID%>").focus();
            var dateArr = $("#<%=txtEffFrom.ClientID%>").val().split('/');
            $("#<%= txtEffTo.ClientID%>").datepicker({ minDate: new Date(dateArr[2], dateArr[1] - 1, dateArr[0]), changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' });
            $.datepicker.initialized = true;
            $("#<%= txtEffTo.ClientID%>").focus();
        }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="updRl" runat="server">
        <ContentTemplate>
            <center>
                  <div id="div2" runat="server"   class="panel panel-success" style="margin-left: 2%; margin-right: 2%; margin-top: 0.5%">
                <div id="divPnlHdJOBDtls" runat="server" class="panel-heading" onclick="ShowReqDtl1('ctl00_EmptyPagePlaceholder_divPnlBdyJOBDtls','myImg1');return false;">
                    <div class="row">
                        <div class="col-sm-10" style="text-align: left">
                            <asp:Label ID="Label1" Text="Job Details" Font-Size="19px" runat="server" />
                        </div>
                        <div class="col-sm-2">
                            <span id="btnToggleNew" class="glyphicon glyphicon-collapse-down" style="float: right; 
                                padding: 1px 10px ! important; font-size: 18px;"></span>
                        </div>
                    </div>
                </div>
                <div id="divPnlBdyJOBDtls" runat="server" style="padding: 25px;" class="panel-body">
                    <div class="row" style="margin-bottom: 5px;">
                        <div class="col-sm-3" style="text-align: left">
                            <asp:Label ID="lblJOBID" Text="JOB ID" runat="server" CssClass="control-label" />
                        </div>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtJOBID" runat="server" CssClass="form-control" TabIndex="1" Enabled="false"/>
                        </div>
                        <div class="col-sm-3" style="text-align: left">
                            <asp:Label ID="lblJOBName" Text="JOB Name" runat="server" CssClass="control-label" />
                            <asp:Label Text="*" runat="server" Style="color: Red" />
                        </div>
                        <div class="col-sm-3">
                                <asp:TextBox ID="txtJOBName" runat="server" CssClass="form-control" TabIndex="1" Enabled="false"/>
                        </div>
                    </div>
                    <div class="row" style="margin-bottom: 5px;">
                        <div class="col-sm-3" style="text-align: left">
                            <asp:Label ID="lblDBName" Text="Database Name" runat="server" CssClass="control-label" />
                            <asp:Label Text="*" runat="server" Style="color: Red" />
                        </div>
                        <div class="col-sm-3">
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlDBName" runat="server"  CssClass="form-control" TabIndex="4" Enabled="false"></asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                         <div class="col-sm-3" style="text-align: left">
                            <asp:Label ID="Label2" Text="Schedule Type" runat="server" CssClass="control-label" />
                             <asp:Label Text="*" runat="server" Style="color: Red" />
                        </div>
                        <div class="col-sm-3">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlSchType" runat="server"  CssClass="form-control" TabIndex="4" AutoPostBack="true" OnSelectedIndexChanged="ddlSchType_SelectedIndexChanged"></asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
   <%----------------------- Added By Megha Bhave 19.11.2020 ------------------------------%>
                      <div class="row" style="margin-bottom: 5px;">
                           <div class="col-sm-3" style="text-align: left">
                            <asp:Label ID="Label3" Text="Recurs day" runat="server" CssClass="control-label" />
                            <asp:Label ID="Label4" Text="*" runat="server" Style="color: Red" />
                        </div>
                          <div class="col-sm-3">
                                                     <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                              <asp:TextBox ID="txtrcrsdy" runat="server"  CssClass="form-control" TabIndex="1" Enabled="false" onkeypress="return isNumberKey(event)" MaxLength="3"></asp:TextBox>
                               <%--<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderlnkid" runat="server" InvalidChars=";,#$@%^!*()&''%^~`_:-+{}[]?><|*"
                                         FilterMode="InvalidChars" TargetControlID="txtrcrsdy" FilterType="Custom"></ajaxToolkit:FilteredTextBoxExtender>--%>
                          <%--  <asp:RangeValidator ID="RangeValidator1" runat="server" Style="top: 194px; left: 365px; position: absolute; height: 22px; width: 105px" 
                     ControlToValidate="txtrcrsdy" MaximumValue="100" MinimumValue="1" Type="Integer"></asp:RangeValidator>--%>
                                    </ContentTemplate>
                                </asp:UpdatePanel>  
                               </div>
                         <div class="col-sm-3" style="text-align: left">
                            <asp:Label ID="Lblfrq" Text="Daily Frequency" runat="server" CssClass="control-label" />
                            <asp:Label ID="spnfrq" Text="*" runat="server" Style="color: Red" />
                        </div>
                           <div class="col-sm-3">
                            
                            <asp:UpdatePanel ID="FRQ" runat="server">
                                <ContentTemplate>
                                     <div class="input-group">
                                    <asp:DropDownList ID="ddlfrqdly" runat="server"  CssClass="select2-container form-control" TabIndex="4" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlfrqdly_SelectedIndexChanged" >
                                   
                                    </asp:DropDownList> 
                                <span class="input-group-btn" style="width: 25%;" runat="server">
                                    <input type="text" id="fname" class="form-control" runat="server" placeholder=""  disabled="disabled" maxlength="3" onkeypress="return isNumberKey(event)"/> <%--onkeypress="return isNumberKey(event)"--%>
                            <%--    <input type="number" id="fname" runat="server" name="fnameb" min="1" max="100" disabled="disabled" placeholder="" />  --%>     
                                </span></div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        
                               </div>
                             </div>
  <%-------------------- Ended By Megha Bhave 19.11.2020 --------------------%>

                        <div class="row" style="margin-bottom: 5px;">
                        <div class="col-sm-3" style="text-align: left">
                            <asp:Label ID="lblStepNm" Text="Step Name" runat="server" CssClass="control-label" Enabled="false" />
                            <asp:Label Text="*" runat="server" Style="color: Red" />
                        </div>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtStpNm" runat="server" CssClass="form-control" TabIndex="1" Enabled="false" />
                        </div>
                        <div class="col-sm-3" style="text-align: left">
                            <asp:Label ID="lblCmdTxt" Text="Command" runat="server" CssClass="control-label" />
                            <asp:Label Text="*" runat="server" Style="color: Red" />
                        </div>
                        <div class="col-sm-3">
                                <asp:TextBox ID="TxtCommand" runat="server" CssClass="form-control" TabIndex="1" Enabled="false"/>
                        </div>
                    </div>

                    <div id="divpnlRangefactorBlkUpd" runat="server" style="width: 97%;" class="panel" visible="false">
                        <div id="divpnlhdJOBSteps" runat="server" class="panel-heading" onclick="ShowReqDtl1('ctl00_EmptyPagePlaceholder_divpnlbdyJOBSteps','imgJOBSteps');return false;">
                            <div class="row">
                                <div class="col-sm-10" style="text-align: left">
                                    <asp:Label ID="lblJOBSteps" Text="Define Steps" Font-Size="19px" runat="server" />
                                </div>
                                <div class="col-sm-2">
                                    <span id="imgJOBSteps" class="glyphicon glyphicon-menu-hamburger" style="float: right;color:#034ea2;padding: 1px 10px ! important; font-size: 18px;"></span>
                                </div>
                            </div>
                        </div>
                        <div id="divpnlbdyJOBSteps" runat="server" style="width: 96%;" class="panel-body">
                            <div id="divJOBSteps" runat="server" class="row" style="margin-bottom: 5px;">
                            </div>
                        </div>   
                    </div>
                    <div class="row" style="margin-bottom: 5px;">
                        <div class="col-sm-3" style="text-align: left">
                            <asp:Label ID="lblEffFrom" Text="Schedule Date" runat="server" CssClass="control-label" />
                            <asp:Label Text="*" runat="server" Style="color: Red" />
                        </div>
                        <div class="col-sm-3">
                                <asp:TextBox ID="txtEffFrom" onclick="checkDate()" 
                                    placeholder="DD/MM/YYYY" runat="server" CssClass="form-control" ></asp:TextBox> <%--"callEffectiveDateFrom()"--%>
                        </div>
                         <div class="col-sm-3" style="text-align: left">
                            <asp:Label ID="lblSchTim" Text="Schedule Time" runat="server" CssClass="control-label" />
                             <asp:Label Text="*" runat="server" Style="color: Red" />
                        </div>
                        <div class="col-sm-3" style="text-align: left">
                          
                             <asp:TextBox ID="txtTime" runat="server" placeholder="HH:MM (24 Hrs)" CssClass="form-control" MaxLength="5" />
                              
                         </div>
                           
                           
                           <%--  <div >  <%-- class="form-group"
                            <div class='input-group date' style="width: 236px">
                                <asp:TextBox ID="txtTime" runat="server" CssClass="form-control" />
                                <span class="input-group-addon"><span class="glyphicon glyphicon-time" style="color:black"></span></span>
                            </div>
                           
                        </div>--%>
                                
                        </div>
                       
                    
                     

                    <div class="row" style="margin-bottom: 5px;">
                        <div class="col-sm-3" style="text-align: left">
                            <asp:Label ID="lblStatus" Text="Status" runat="server" CssClass="control-label" />
                            <asp:Label Text="*" runat="server" Style="color: Red" />
                        </div>
                        <div class="col-sm-3">
                            <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlStatus" runat="server"  CssClass="form-control" TabIndex="4" Enabled="false"  ></asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                         <div class="col-sm-3" style="text-align: left">
                            <asp:Label ID="lblEffTo" Text="Cease Date" runat="server" CssClass="control-label" Visible="false" />
                        </div>
                        <div class="col-sm-3">
                                <asp:TextBox ID="txtEffTo" onclick="callEffectiveDateTo()"
                                    placeholder="DD/MM/YYYY" runat="server" CssClass="form-control" Enabled="false" Visible="false" ></asp:TextBox>
                        </div>
                    </div>

                  

                   <%-- <div class="row" style="margin-bottom: 5px; display:none">
                          
                                  <MKB:TimeSelector ID="timeOccurs" runat="server" Width="120px" SelectedTimeFormat="TwentyFour" DisplaySeconds="false">
                            </MKB:TimeSelector>
                              <asp:Label ID="hdnSchTime"  runat="server" CssClass="control-label" Visible="false" />
                            <asp:Label ID="hdnStrtTime"  runat="server" CssClass="control-label" Visible="false" />

                               <asp:TextBox ID="txtSchTim" Visible="false"
                                    placeholder="hhmmss" runat="server" CssClass="form-control" ></asp:TextBox>
                         </div>--%>


                     </div>
                    <div id="div3" runat="server" class="row" style="margin-top: 12px;">
                        <div class="col-sm-12" >
                            <%--  <asp:HiddenField ID="hdnSchTime" runat="server" />
                              <asp:HiddenField ID="hdnStrtTime" runat="server" />--%>
                          


                            <asp:LinkButton ID="btnUpdSTFUL" runat="server" CssClass="btn btn-primary" style="display:none;" TabIndex="17" OnClick="btnUpdSTFUL_Click" OnClientClick="return FnChkTime();" >
                                <span class="glyphicon glyphicon-floppy-disk" style="color: White;"></span> Update
                            </asp:LinkButton>

                            <asp:LinkButton ID="btnAdd" runat="server" CssClass="btn btn-primary" style="display:inline-block;" TabIndex="17" OnClientClick="return FnChkTime();" OnClick="btnAdd_Click" >
                                <span class="glyphicon glyphicon-plus" style="color: White;"></span> Add
                            </asp:LinkButton>

                            <asp:LinkButton ID="btnClear" runat="server" CssClass="btn btn-danger" TabIndex="17" OnClick="btnClear_Click" >
                                <span class="glyphicon glyphicon-erase BtnGlyphicon" style="color: White;"></span> Clear
                            </asp:LinkButton>

                            <asp:LinkButton ID="BtnCncl" runat="server" CssClass="btn btn-danger" TabIndex="17" OnClientClick="gotoHome(); return false;" OnClick="BtnCncl_Click" >
                                <span class="glyphicon glyphicon-remove" style="color: White;"></span> Cancel
                            </asp:LinkButton>
                        </div>
                    </div>
              <br />
                     
                   <div id="div19" runat="server" style="width: 100%; border: none; margin: 0px 0 !important;"
                class="table-scrollable">
                       <asp:Label ID="lblGrid" Text="No Job have been defined" Style="color: Red; text-align:left" runat="server" CssClass="control-label" Visible="false" />
                        <center> 
                <div id="divGridMap" runat="server" style="width: 100%; overflow-x: scroll">
                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="dgJobDtls" runat="server" CssClass="footable" PageSize="10" AllowSorting="True"
                                AllowPaging="true" AutoGenerateColumns="false">
                                <RowStyle CssClass="GridViewRow"></RowStyle>
                                <PagerStyle CssClass="disablepage" />
                                <HeaderStyle CssClass="gridview th" />
                                <EmptyDataTemplate>
                                                <asp:Label ID="lblerror" Text="No records found" ForeColor="Red"
                                                    CssClass="control-label" runat="server" />
                                            </EmptyDataTemplate>

                                <Columns>
                                    <asp:TemplateField HeaderText="JOB Name" HeaderStyle-HorizontalAlign="Left"
                                        SortExpression="JOB_NM">
                                        <ItemTemplate>
                                            <asp:Label ID="lblJbName" runat="server" Text='<%# Bind("JOB_NM")%>'></asp:Label>
                                            <asp:HiddenField ID="hdnJBCode" runat="server" Value='<%# Bind("JOB_ID")%>'></asp:HiddenField>
                                           
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Database Name" HeaderStyle-HorizontalAlign="Left" SortExpression="JOB_ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDBNm" runat="server" Text='<%# Bind("DB_NM")%>'></asp:Label>
                                          
                                        </ItemTemplate>
                                        <ItemStyle Width="20%" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Schedule Type" HeaderStyle-HorizontalAlign="Left" SortExpression="SCH_TYPDesc">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSch" runat="server" Text='<%# Bind("SCH_TYPDesc")%>'></asp:Label>
                                            <asp:HiddenField ID="hdnSrctbl" runat="server" Value='<%# Bind("SCH_TYP")%>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="20%" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:TemplateField>

                        <asp:TemplateField HeaderText="Recurs Day" HeaderStyle-HorizontalAlign="Left" SortExpression="FREQ_INTRVL">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRCRDY" runat="server" Text='<%# Bind("FREQ_INTRVL")%>'></asp:Label>
                                            <asp:HiddenField ID="hdnRCRDY" runat="server" Value='<%# Bind("FREQ_INTRVL")%>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="20%" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Daily Frequency" HeaderStyle-HorizontalAlign="Left" SortExpression="FREQ_SUBDAY_TYPE_desc">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSchFRQ" runat="server" Text='<%# Bind("FREQ_SUBDAY_TYPE_desc")%>'></asp:Label>
                                            <asp:HiddenField ID="hdnfrqsubtyp" runat="server" Value='<%# Bind("FREQ_SUBDAY_TYPE")%>' />
                                             <asp:HiddenField ID="hdnsubintrvl" runat="server" Value='<%# Bind("FREQ_SUBDAY_INTRVL")%>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="20%" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Step Name" HeaderStyle-HorizontalAlign="Left"
                                        SortExpression="STEP_NM">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstpdesc" runat="server" Text='<%# Bind("STEP_NM")%>'></asp:Label>
                                         </ItemTemplate>
                                        <ItemStyle Width="20%" HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:TemplateField>

                                       <asp:TemplateField HeaderText="Command" HeaderStyle-HorizontalAlign="Left"
                                        SortExpression="COMMAND">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCmnd" runat="server" Text='<%# Bind("COMMAND")%>'></asp:Label>
                                            
                                        </ItemTemplate>
                                        <ItemStyle Width="20%" HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Schedule Date" HeaderStyle-HorizontalAlign="Center"
                                        SortExpression="EFF_FRM">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltxtEffDTM" runat="server" Text='<%# Bind("EFF_FRM")%>'></asp:Label>
                                            <%-- <asp:HiddenField ID="hdntbldesc" runat="server" Value='<%# Bind("TBL_DESC")%>' />--%>
                                        </ItemTemplate>
                                        <ItemStyle Width="20%" HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Schedule Time" HeaderStyle-HorizontalAlign="Center"
                                        SortExpression="SCH_TIME">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSCH_TIME" runat="server" Text='<%# Bind("SCH_TIME")%>'></asp:Label>
                                           
                                        </ItemTemplate>
                                        <ItemStyle Width="20%" HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left"
                                        SortExpression="StatusDesc">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("StatusDesc")%>'></asp:Label>
                                            <%-- <asp:HiddenField ID="hdntbldesc" runat="server" Value='<%# Bind("TBL_DESC")%>' />--%>
                                        </ItemTemplate>
                                        <ItemStyle Width="20%" HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Cease Date" HeaderStyle-HorizontalAlign="Center"
                                        SortExpression="CSE_DTIM" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltxtCse_DTM" runat="server" Text='<%# Bind("CSE_DTIM")%>'></asp:Label>
                                            <%-- <asp:HiddenField ID="hdntbldesc" runat="server" Value='<%# Bind("TBL_DESC")%>' />--%>
                                        </ItemTemplate>
                                        <ItemStyle Width="20%" HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkMapEdit" runat="server"  ForeColor="#3333cc" Text ="Edit"   OnClick="lnkMapEdit_Click" ></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkMapDel" runat="server" Text="Delete"
                                                OnClientClick="return confirm('Are you sure you want to Delete?');" OnClick="lnkMapDel_Click" ForeColor="#3333cc"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                    
                <div class="pagination" style="width: 100%" >
                    <table>
                        <tr>
                            <td style="white-space: nowrap">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="btnprevious" Text="<" CssClass="form-submit-button" runat="server" Width="40px"
                                            Enabled="false" Style="border-style: solid; border-width: 1px; background-repeat: no-repeat; background-color: transparent; float: left; margin: 0; height: 30px;"
                                         OnClick="btnprevious_Click"    />
                                        <asp:TextBox runat="server" ID="txtPage" Style="width: 35px; border-style: solid; border-width: 1px; border-color: Gray; height: 30px; float: left; margin: 0; text-align: center;"
                                            Text="1" CssClass="form-control" ReadOnly="true" />
                                        <asp:Button ID="btnnext" Text=">" CssClass="form-submit-button" runat="server" Style="border-style: solid; border-width: 1px; background-repeat: no-repeat; background-color: transparent; float: left; margin: 0; height: 30px;"
                                            Width="40px" OnClick="btnnext_Click"  />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </div>
</center>
            </div>
                     
                   </div>
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>

    
   

</asp:Content>
