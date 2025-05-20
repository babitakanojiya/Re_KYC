<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="CKYCLegalEntityQC.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.CKYC.CKYCLegalEntityQC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/bundles/CKYCValidationjs") %>
    <%: Styles.Render("~/bundles/CKYCcss") %>
    <style>
        .gridview th {
            padding: 3px;
            height: 40px;
            background-color: #d6d6c2;
            color: #337ab7;
            text-align: center !important;
        }

        th {
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
    <script type="text/javascript">

        function openModal() {
            $('#demoModal').modal('show');
        }

        function OpenRltdPrsnQCPage(RelRefnNo, refno) {
            debugger;
            var modal = document.getElementById('RltdPrsnQC');
            var modaliframe = document.getElementById("iframeCFR1");
            modaliframe.src = "../../Application/CKYC/CKYCRltdPrsnQC.aspx?Status=QC&refno=" + refno;
            $('#RltdPrsnQC').modal();
        }

        function OpenCntrlPrsnQCPage(RelRefnNo, refno) {
            debugger;
            var modal = document.getElementById('CntrlPrsnQC');
            var modaliframe = document.getElementById("iframeCFR2");
            modaliframe.src = "../../Application/CKYC/CKYCCntrlPrsnQC.aspx?Status=QC&refno=" + refno;
            $('#CntrlPrsnQC').modal();
        }

        function OpenStateWindow(Flag1) {
            debugger;

            var flag = Flag1
            var e = document.getElementById("<%= ddlState.ClientID %>");
            var strUser = e.options[e.selectedIndex].value;
            //var strUser = e.options[e.selectedIndex].innerText;
            // var StCode = document.getElementById("<%= ddlState.ClientID %>").value;

            window.open("PinCodeDtls.aspx?StateCode=" + strUser + "&flag=" + flag + "", '', 'width=640,height=354,toolbar=no,scrollbars=yes,resizable=yes,left=300,top=230,location=0;');
        }

        function OpenStateWindow1(Flag1) {
            debugger;

            var flag = Flag1
            var e = document.getElementById("<%= ddlState1.ClientID %>");
            var strUser = e.options[e.selectedIndex].value;
            //var strUser = e.options[e.selectedIndex].innerText;
            // var StCode = document.getElementById("<%= ddlState.ClientID %>").value;
            window.open("PinCodeDtls.aspx?StateCode=" + strUser + "&flag=" + flag + "", '', 'width=640,height=354,toolbar=no,scrollbars=yes,resizable=yes,left=300,top=230,location=0;');
        }

        function OpenStateWindow2(Flag1) {
            debugger;

            var flag = Flag1
            var e = document.getElementById("<%= ddlState2.ClientID %>");
            var strUser = e.options[e.selectedIndex].value;
            //var strUser = e.options[e.selectedIndex].innerText;
            // var StCode = document.getElementById("<%= ddlState.ClientID %>").value;
            window.open("PinCodeDtls.aspx?StateCode=" + strUser + "&flag=" + flag + "", '', 'width=640,height=354,toolbar=no,scrollbars=yes,resizable=yes,left=300,top=230,location=0;');
        }

        function ShowReqDtl1(divName, btnName) {
            debugger;
            var objnewdiv = document.getElementById(divName);
            var objnewbtn = document.getElementById(btnName);

            if (objnewdiv.style.display == "block") {
                objnewdiv.style.display = "none";
                //objnewbtn.className = 'glyphicon glyphicon-resize-full'
                objnewbtn.className = 'glyphicon glyphicon-resize-small'
            }
            else {
                objnewdiv.style.display = "block";
                objnewbtn.className = 'glyphicon glyphicon-resize-full'
                //objnewbtn.className = 'glyphicon glyphicon-resize-small'
            }
        }

        function showHideDiv(divName, btnName) {
            try {
                var objnewdiv = document.getElementById(divName)
                var objnewbtn = document.getElementById(btnName);
                if (objnewdiv.style.display == "block") {
                    objnewdiv.style.display = "none";
                    objnewbtn.className = 'glyphicon glyphicon-collapse-up'
                }
                else {
                    objnewdiv.style.display = "block";
                    objnewbtn.className = 'glyphicon glyphicon-collapse-down'
                }
            }
            catch (err) {
                ShowError(err.description);
            }
        }

        function UnCheckOther(sender, name) {
            var ck_list = document.getElementsByName(name);
            for (var i = 0; i < ck_list.length; i++) {
                ck_list[i].checked = false;
            }
            sender.checked = true;
        }


        function popup(msg) {
            showModal('#myModal', 'Alert', 'alert-warning', '', '', msg);
        }

        function OpenRelatedPersonPage() {
            debugger;
            var modal = document.getElementById('myModalRaise');
            var modaliframe = document.getElementById("iframeCFR");
            modaliframe.src = "CKYCRelatedPrsn.aspx?Status=reg&mdl=myModalRaise";
            $('#myModalRaise').modal();
        }

        function OpenRelatedPersonPageView(RelRefnNo, refno) {
            debugger;
            var modal = document.getElementById('myModalRaise');
            var modaliframe = document.getElementById("iframeCFR");

            modaliframe.src = "CKYCRelatedPrsn.aspx?Status=Mod&refno=" + refno + "&relrefno=" + RelRefnNo;
            $('#myModalRaise').modal('show');
        }

        function OpenRelatedPersonPageEdit(RelRefnNo, refno) {
            debugger;
            var modal = document.getElementById('myModalRaise');
            var modaliframe = document.getElementById("iframeCFR");

            modaliframe.src = "CKYCRelatedPrsn.aspx?Status=Mod&refno=" + refno + "&relrefno=" + RelRefnNo;
            $('#myModalRaise').modal('show');
        }

        function OpenPartialRelatedPersonPageView(RelRefnNo, refno) {
            debugger;
            var modal = document.getElementById('myModalRaise');
            var modaliframe = document.getElementById("iframeCFR");

            modaliframe.src = "CKYCRelatedPrsn.aspx?Status=PMod&Action=View&refno=" + refno + "&relrefno=" + RelRefnNo;
            $('#myModalRaise').modal('show');
        }

        function OpenPartialRelatedPersonPageEdit(RelRefnNo, refno) {
            debugger;
            var modal = document.getElementById('myModalRaise');
            var modaliframe = document.getElementById("iframeCFR");

            modaliframe.src = "CKYCRelatedPrsn.aspx?Status=PMod&Action=Edit&refno=" + refno + "&relrefno=" + RelRefnNo;
            $('#myModalRaise').modal('show');
        }

        function OpenControllingPersonPage() {
            debugger;
            var modal = document.getElementById('myModalRaise1');
            var modaliframe = document.getElementById("iframeCCFR");
            modaliframe.src = "CKYCControllingPrsn.aspx?Status=reg&mdl=myModalRaise1";
            $('#myModalRaise1').modal();
        }

        function OpenControllingPersonPageView(RelRefnNo, refno) {
            debugger;
            var modal = document.getElementById('myModalRaise1');
            var modaliframe = document.getElementById("iframeCCFR");

            modaliframe.src = "CKYCControllingPrsn.aspx?Status=Mod&refno=" + refno + "&relrefno=" + RelRefnNo;
            $('#myModalRaise1').modal('show');
        }

        function OpenControllingPersonPageEdit(RelRefnNo, refno) {
            debugger;
            var modal = document.getElementById('myModalRaise1');
            var modaliframe = document.getElementById("iframeCCFR");

            modaliframe.src = "CKYCControllingPrsn.aspx?Status=Mod&refno=" + refno + "&relrefno=" + RelRefnNo;
            $('#myModalRaise1').modal('show');
        }

        function OpenPartialControllingPersonPageView(RelRefnNo, refno) {
            debugger;
            var modal = document.getElementById('myModalRaise1');
            var modaliframe = document.getElementById("iframeCCFR");

            modaliframe.src = "CKYCControllingPrsn.aspx?Status=PMod&Action=View&refno=" + refno + "&relrefno=" + RelRefnNo;
            $('#myModalRaise1').modal('show');
        }

        function OpenPartialControllingPersonPageEdit(RelRefnNo, refno) {
            debugger;
            var modal = document.getElementById('myModalRaise1');
            var modaliframe = document.getElementById("iframeCCFR");

            modaliframe.src = "CKYCControllingPrsn.aspx?Status=PMod&Action=Edit&refno=" + refno + "&relrefno=" + RelRefnNo;
            $('#myModalRaise1').modal('show');
        }

        function funddlProofRelPerson() {
            $('#menu1').attr("class", "tab-pane fade");
            $('#EmptyPagePlaceholder_personal').attr("aria-expanded", false);
            $('#EmptyPagePlaceholder_m1').removeAttr("class");
            $('#EmptyPagePlaceholder_m3').attr("class", "active");
            $('#EmptyPagePlaceholder_A3').attr("aria-expanded", true);
            $('#menu4').attr("class", "tab-pane fade in active");
        }

    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="container">

        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
            <div id="Div18" runat="server" class="panel-heading" onclick="showHideDiv('divCKYCdtls','btnCKYCdtls');return false;">
                <div class="row">
                    <div class="col-sm-10" style="text-align: left">
                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                        <asp:Label ID="lblOfcuseOnly" Text="" runat="server" CssClass="control-label">
                        </asp:Label>
                    </div>
                    <div class="col-sm-2" style="text-align: right">
                        <asp:Label ID="Label3" Text="Version 1.6" runat="server" CssClass="control-label"></asp:Label>
                        <span id="btnCKYCdtls" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                    </div>
                </div>
            </div>
            <div id="divCKYCdtls" style="display: block;" class="panel-body">
                <div class="row" style="margin-bottom: 5px">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblAppType" Text="" runat="server" Font-Bold="false">
                        </asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:CheckBox ID="cbNew" runat="server" CssClass="standardcheckbox" Text="New" AutoPostBack="true" Checked="true"
                            Enabled="false" TabIndex="20" />
                        <asp:CheckBox ID="cbUpdate" runat="server" CssClass="standardcheckbox" Text="Update"
                            AutoPostBack="true" Visible="false" TabIndex="1" />
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblRefNumber" Text="" runat="server" Font-Bold="false"></asp:Label>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:TextBox ID="txtRefNumber" runat="server" CssClass="form-control" Enabled="false"
                            Font-Bold="false" TabIndex="2">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="row" style="margin-bottom: 5px">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblKYCNumber" Text="" runat="server" Font-Bold="false"></asp:Label>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:TextBox ID="txtKYCNumber" runat="server" CssClass="form-control" Enabled="false"
                            Font-Bold="false" TabIndex="2">
                        </asp:TextBox>
                    </div>


                </div>
                <div class="row" style="margin-bottom: 5px">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblAccountType" Text="" runat="server" Font-Bold="false">
                        </asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <%--<asp:CheckBoxList runat="server">--%>
                        <%--<input id="chkUSReport" class="standardcheckbox" title="US Reportable" type="checkbox"
                                    name="AccType" value="US" onclick="UnCheckOther(this, 'AccType')"/>&nbsp;<span>US Reportable</span>
                                 <input id="chkOtherReport" class="standardcheckbox" title="Other Reportable" type="checkbox"
                                    name="AccType" value="Other" onclick="UnCheckOther(this, 'AccType')"/>&nbsp;<span>Other Reportable</span>--%>
                        <%--<asp:CheckBox ID="chkOtherReport" runat="server" CssClass="standardcheckbox" Text="Other Reportable"
                                    AutoPostBack="true" TabIndex="3" name="AccType" value="value1" />--%>
                        <%--</asp:CheckBoxList>--%>
                        <asp:CheckBox ID="chkUSReport" runat="server" CssClass="standardcheckbox" Enabled="false" Text=" US Reportable"
                            AutoPostBack="true"></asp:CheckBox><%--OnCheckedChanged="chkUSReport_CheckedChanged"--%>
                        <asp:CheckBox ID="chkOtherReport" runat="server" CssClass="standardcheckbox" Enabled="false" Text=" Other Reportable"
                            AutoPostBack="true"></asp:CheckBox><%--OnCheckedChanged="chkOtherReport_CheckedChanged"--%>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:DropDownList ID="ddlAccHolderType" runat="server" Enabled="false" CssClass="form-control" TabIndex="62">
                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row" style="margin-bottom: 5px">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblNatureOfBuss" Text="" runat="server" Font-Bold="false">
                        </asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:DropDownList ID="ddlNatureOfBuss" runat="server" Enabled="false" CssClass="form-control" TabIndex="62">
                            <asp:ListItem Value="02" Text="02"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>

    </div>

    <%-- Img Section--%>
    <div class="container" width="100%">
        <div id="divImg" runat="server" class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
            <div id="Div25" runat="server" class="panel-heading">
                <div class="row">
                    <div class="col-sm-10" style="text-align: left">
                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                        <asp:Label ID="lbluploadDoc" Text="UPLOADED DOCUMENTS" runat="server" CssClass="control-label"></asp:Label>

                    </div>
                    <div class="col-sm-2" onclick="showHideDiv('divnavigate','btnToggle');return false;">
                        <span id="btnnavigate" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                    </div>
                </div>
            </div>
            <div id="divnavigate" style="display: block;" runat="server" class="panel-body">
                <div class="row">
                    <div class="col-sm-12" style="text-align: right">
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
                    <div class="col-sm-3" style="text-align: left" style="display: none">


                        <asp:Label ID="lblPageInfo" runat="server" Visible="false"></asp:Label>
                    </div>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div id="divloaderqc" class="col-sm-12" runat="server" style="display: none; position: absolute;">
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
                            <div id="Div26" runat="server" class="panel-body">
                                <div class="row">


                                    <asp:GridView ID="GridImage" runat="server" AllowSorting="True" CssClass="footable"
                                        Width="100%" AutoGenerateColumns="False" PageSize="10" AllowPaging="true" CellPadding="1"
                                        OnRowDataBound="GridImage_RowDataBound">
                                        <%--OnRowDataBound="GridImage_RowDataBound"--%>
                                        <%--OnPageIndexChanging="GridImage_PageIndexChanging"
                                        --%>
                                        <RowStyle CssClass="GridViewRow"></RowStyle>
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
                                                <ControlStyle CssClass="left_padding" Width="30%" />
                                                <%--Height="100%"--%>
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
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnnext" EventName="Click"></asp:AsyncPostBackTrigger>
                        </Triggers>
                    </asp:UpdatePanel>


                </div>
            </div>
        </div>
    </div>
    <%--Img section End--%>



    <div class="container" width="100%">
        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
            <div id="divEntityDetail" runat="server" class="panel-heading" onclick="showHideDiv('divDetailOfEntity','btnEntitydtls');return false;">
                <div class="row">
                    <div class="col-sm-10" style="text-align: left; top: 0px; left: 0px;">
                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                        <asp:Label ID="lblOfEntiDetl" Text="ENTITY DETAILS" runat="server" CssClass="control-label">
                        </asp:Label>
                    </div>
                    <div class="col-sm-2" style="text-align: right">
                        <span id="btnEntitydtls" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                    </div>
                </div>
            </div>
            <div id="divDetailOfEntity" style="display: block;" class="panel-body">
                <div class="row" style="margin-bottom: 5px">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblKYCName" Text="" runat="server" Font-Bold="false"></asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3" style="text-align: left; top: 2px; left: 0px;">
                        <asp:TextBox ID="txtKYCName" runat="server" Enabled="false" CssClass="form-control" Font-Bold="false"
                            TabIndex="2">
                        </asp:TextBox>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblDatOfInc" Text="" runat="server" Font-Bold="false"></asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3" style="text-align: left; top: 0px; left: 0px;">
                        <asp:TextBox ID="txtDatOfInc" runat="server" Enabled="false" CssClass="form-control" onmousedown="$(this).datepicker({ maxDate: new Date() ,changeMonth: true, changeYear: true, dateFormat: 'dd-mm-yy' });" MaxLength="10"></asp:TextBox>
                    </div>
                </div>
                <div class="row" style="margin-bottom: 5px">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblDateOfCom" Text="" runat="server" Font-Bold="false"></asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3" style="text-align: left; top: 0px; left: 0px;">
                        <asp:TextBox ID="txtDtOfCom" runat="server" Enabled="false" CssClass="form-control" onmousedown="$(this).datepicker({ maxDate: new Date() ,changeMonth: true, changeYear: true, dateFormat: 'dd-mm-yy' });" MaxLength="10"></asp:TextBox>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblPlaceOfIncor" Text="" runat="server" Font-Bold="false"></asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3" style="text-align: left; top: 2px; left: 0px;">
                        <asp:TextBox ID="txtPlaceOfInc" Enabled="false" runat="server" CssClass="form-control" Font-Bold="false"
                            TabIndex="2">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="row" style="margin-bottom: 5px">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblCountrOfInc" Text="" runat="server" Font-Bold="false"></asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3" style="text-align: left; top: 0px; left: 0px;">
                        <asp:DropDownList ID="ddlCountrOfInc" Enabled="false" runat="server" CssClass="form-control" Font-Bold="false"
                            TabIndex="2">
                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblCountryOfRsidens" Text="" runat="server"
                            Font-Bold="false"></asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3" style="text-align: left; top: 0px; left: 0px;">
                        <asp:DropDownList ID="ddlCountryOfRsidens" Enabled="false" runat="server" CssClass="form-control" Font-Bold="false"
                            TabIndex="2">
                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row" style="margin-bottom: 5px">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblIdentyType" Text="" runat="server" Font-Bold="false"></asp:Label>
                        <%--<span style="color: red">*</span>--%>
                    </div>
                    <div class="col-sm-3" style="text-align: left; top: 0px; left: 0px;">
                        <asp:DropDownList ID="ddlIdentyType" Enabled="false" runat="server" CssClass="form-control" Font-Bold="false"
                            TabIndex="2">
                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblTypeIdentiNo" Text="" runat="server"
                            Font-Bold="false"></asp:Label>
                        <%--<span style="color: red">*</span>--%>
                    </div>
                    <div class="col-sm-3" style="text-align: left; top: 0px; left: 0px;">
                        <asp:TextBox ID="txtTypeIdentiNo" Enabled="false" runat="server" CssClass="form-control" Font-Bold="false"
                            TabIndex="2">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="row" style="margin-bottom: 5px">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblTINCountry" Text="" runat="server" Font-Bold="false"></asp:Label>
                        <%--<span style="color: red">*</span>--%>
                    </div>
                    <div class="col-sm-3" style="text-align: left; top: 0px; left: 0px;">
                        <asp:DropDownList ID="ddlTINCountry" Enabled="false" runat="server" CssClass="form-control" Font-Bold="false"
                            TabIndex="2">
                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblPanNo" Text="" runat="server" Font-Bold="false"></asp:Label>
                        <%--<span style="color: red">*</span>--%>
                    </div>
                    <div class="col-sm-3" style="text-align: left; top: 0px; left: 0px;">
                        <asp:TextBox ID="txtPanNo" Enabled="false" runat="server" CssClass="form-control" Font-Bold="false"
                            TabIndex="2">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="row" style="margin-bottom: 5px">

                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblNumberOfPerson" Text="" runat="server" Font-Bold="false"></asp:Label>
                        <%--<span style="color: red">*</span>--%>
                    </div>
                    <div class="col-sm-3" style="text-align: left; top: 0px; left: 0px;">
                        <asp:TextBox ID="txtNumberOfPerson" Enabled="false" runat="server" CssClass="form-control" Font-Bold="false" MaxLength="2"
                            TabIndex="2">
                        </asp:TextBox>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <div class="container" width="100%">
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
                <div class="row" style="margin-bottom: 5px">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblCertifiecopy" Text="" runat="server" Font-Bold="false"></asp:Label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlCertifiecopy" Enabled="false" runat="server" CssClass="form-control" TabIndex="62">
                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                            <%--<asp:ListItem Value="" Text="Certificate of Incorporation / Formation"></asp:ListItem>
                            <asp:ListItem Value="" Text="Registration Certificate"></asp:ListItem>
                            <asp:ListItem Value="" Text="Resoluation of Board / Managing Committee"></asp:ListItem>
                            <asp:ListItem Value="" Text="Memorandum and Article of Association / Partnership Deed / Trust Deed"></asp:ListItem>
                            <asp:ListItem Value="" Text="Officially valid document(s) in respect of person authorised to transact"></asp:ListItem>--%>
                        </asp:DropDownList>
                    </div>
                    <div id="divIdProof" runat="server">
                        <div id="divPassNo" class="col-sm-3" runat="server" style="text-align: left">
                            <asp:Label ID="lblPassportNo" runat="server" CssClass="control-label"></asp:Label>
                            <span style="color: red">*</span>
                        </div>

                        <div id="divPassNotxt" runat="server" class="col-sm-3">
                            <asp:TextBox ID="txtPassNo" runat="server" MaxLength="20" TabIndex="37" Enabled="false" onkeypress="funIsAlphaNumeric()"
                                CssClass="form-control" Font-Bold="false" onChange="javascript:this.value=this.value.toUpperCase();">
                            </asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--Came till here --%>

    <div class="container" width="100%">
        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
            <div id="divProofOfAddress" runat="server" class="panel-heading" onclick="showHideDiv('menu3','Span9');return false;">
                <div class="row">
                    <div class="col-sm-10" style="text-align: left">
                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                        <asp:Label ID="ProofOfAdd" Text="PROOF OF ADDRESS (PoA)" runat="server" CssClass="control-label">
                        </asp:Label>
                    </div>
                    <div class="col-sm-2">
                        <span id="Span9" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                    </div>
                </div>
            </div>
            <div id="menu3" style="display: block;" class="panel-body">
                <%-- Added for Address Details start --%>
                <div class="panel panel-success" style="margin-left: 5px; margin-right: 5px">
                    <div id="Div6" runat="server" class="panel-heading subheader" style="background-color: #EDF1cc !important" onclick="ShowReqDtl1('div7','Span2');return false;">
                        <div class="row">
                            <div class="col-sm-10" style="text-align: left">
                                <span class="glyphicon glyphicon-menu-hamburger"></span>
                                <asp:CheckBox ID="ChkUpdAddr" runat="server" CssClass="standardcheckbox" Text=""
                                    AutoPostBack="true" TabIndex="1" /><%-- OnCheckedChanged="ChkUpdAddr_Checked" --%>
                                <asp:Label ID="lblpfofAddr2" Text="" runat="server" CssClass="control-label"></asp:Label><span style="color: red">*</span>
                            </div>
                            <div class="col-sm-2" onclick="ShowReqDtl1('div7','Span2');return false;">
                                <span id="Span2" class="glyphicon glyphicon-resize-full" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div>
                    <div id="div7" style="display: block;" class="panel-body">
                        <div class="row">
                            <div class="col-sm-12" style="text-align: left">
                                <asp:CheckBox ID="chkPerAddress" Enabled="false" Text="CURRENT / PERMANENT / OVERSEAS ADDRESS DETAILS"
                                    AutoPostBack="true" runat="server" CssClass="control-label"
                                    TabIndex="40" /><%--OnCheckedChanged="chkPerAddress_Checked"--%>
                                <span style="color: red">*</span>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblAddressType" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlAddressType" runat="server" Enabled="false" CssClass="form-control"
                                            TabIndex="41">
                                            <%--OnSelectedIndexChanged="ddlAddressType_SelectedIndexChanged"--%>
                                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblProofOfAddress" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddlProofOfAddress" Enabled="false" runat="server" CssClass="form-control"
                                    TabIndex="42">
                                    <%--OnSelectedIndexChanged="ddlProofOfAddress_SelectedIndexChanged"--%>
                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div id="divAddProof" runat="server" class="row" visible="false">
                            <div id="divPassNoAdd" runat="server" class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblPassportNoAdd" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div id="divPassNotxtAdd" runat="server" class="col-sm-3">
                                <asp:TextBox CssClass="form-control" runat="server" onChange="javascript:this.value=this.value.toUpperCase();"
                                    ID="txtPassNoAdd" MaxLength="15" TabIndex="43" />

                            </div>
                            <div id="divPassAdd" runat="server" class="col-sm-3" style="text-align: left">
                                <asp:Label ID="llPassExpDateAdd" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div id="divPassDateAdd" runat="server" class="col-sm-3">
                                <asp:TextBox CssClass="form-control" onmousedown="$('#EmptyPagePlaceholder_txtPassExpDateAdd').datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy' });"
                                    runat="server"
                                    ID="txtPassExpDateAdd" MaxLength="15" TabIndex="44" />
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtPassOthrAdd" MaxLength="15"
                                    TabIndex="45" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblAddressLine1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox CssClass="form-control" runat="server"
                                    ID="txtAddressLine1" Enabled="false" MaxLength="55" TabIndex="46" />
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblAddressLine2" Text="" runat="server" CssClass="control-label"></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox CssClass="form-control"
                                    runat="server" ID="txtAddressLine2" Enabled="false" MaxLength="55" TabIndex="47" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblAddressLine3" Text="" runat="server" CssClass="control-label"></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtDrivLicNum')" runat="server"
                                    ID="txtAddressLine3" Enabled="false" MaxLength="55" TabIndex="48" />
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblCity" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtCity" Enabled="false" runat="server" CssClass="form-control" TabIndex="49" MaxLength="50">
                                </asp:TextBox>
                            </div>
                        </div>


                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblState" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <div class="">
                                    <asp:DropDownList ID="ddlState" Enabled="false" runat="server" CssClass="form-control" TabIndex="52">
                                        <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:HiddenField runat="server" ID="hdnddlPinCode" />
                                    <%--<div class="input-group-btn">
                                <asp:LinkButton runat="server" ID="btnShow" CssClass="btn btn-primary btn-lg-kmi"  title="Search" data-toggle="tooltip" OnClick="GetModelData">
                                    <span class="glyphicon glyphicon-search BtnGlyphicon"></span>
                                    </asp:LinkButton>
                                        </div>--%>
                                </div>
                            </div>

                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblPinCode" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span><font color="red">*</font></span>
                            </div>
                            <div class="col-sm-3">
                                <%--style="display:flex"--%>

                                <div class="input-group">
                                    <asp:TextBox ID="txtPinCode" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:HiddenField runat="server" ID="HiddenField1" />
                                    <div class="input-group-btn">
                                        <asp:LinkButton runat="server" ID="LinkButton2" CssClass="btn btn-primary btn-lg-kmi" title="Search" data-toggle="tooltip"><%--OnClick="GetModelData"--%>
                                    <span class="glyphicon glyphicon-search BtnGlyphicon"></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblDistrict" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span><font color="red">*</font></span>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox runat="server" ID="txtDistrictname" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>



                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblIsoCountryCode" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <%--       <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtState')" runat="server"
                                                    ID="txtCountryCode" MaxLength="15"  TabIndex="12" Enabled="false" />--%>
                                <asp:DropDownList ID="ddlCountryCode" Enabled="false" runat="server" CssClass="form-control" AutoPostBack="true"
                                    TabIndex="53">
                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div style="margin-top: 25px; margin-bottom: 25px">
                            <div class="row">
                                <div class="col-sm-12" style="text-align: left">
                                    <asp:CheckBox ID="chkLocalAddress" Enabled="false" Text="CORRESPONDENCE / LOCAL ADDRESS DETAILS" runat="server"
                                        CssClass="control-label" TabIndex="54" />
                                    <span style="color: red">*</span>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12" style="text-align: left">
                                    <asp:CheckBox ID="chkCuurentAddress" Enabled="false" Text="Same as Current / Permanent / Overseas Address details"
                                        AutoPostBack="true" runat="server"
                                        CssClass="control-label" TabIndex="55" />
                                    <%--OnCheckedChanged="chkCuurentAddress_Checked"--%>
                                    <span style="color: red">*</span>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblAddressType1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                    <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-3">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlAddressType1" Enabled="false" runat="server" CssClass="form-control" AutoPostBack="true"
                                                TabIndex="41">
                                                <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblProofOfAddress1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                    <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlProofOfAddress1" Enabled="false" runat="server" CssClass="form-control"
                                        TabIndex="42">
                                        <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblLocAddLine1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                    <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox CssClass="form-control" runat="server"
                                        ID="txtLocAddLine1" Enabled="false" MaxLength="55" TabIndex="56" />
                                </div>
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblLocAddLine2" Text="" runat="server" CssClass="control-label"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox CssClass="form-control" runat="server"
                                        ID="txtLocAddLine2" Enabled="false" MaxLength="55" TabIndex="57" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblLocAddLine3" Text="" runat="server" CssClass="control-label"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox CssClass="form-control" runat="server"
                                        ID="txtLocAddLine3" Enabled="false" MaxLength="55" TabIndex="58" />
                                </div>
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblCity1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                    <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtCity1" Enabled="false" runat="server" CssClass="form-control" TabIndex="59" MaxLength="50"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblState1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                    <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-3">
                                    <div class="">
                                        <asp:DropDownList ID="ddlState1" Enabled="false" runat="server" CssClass="form-control" TabIndex="62">
                                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:HiddenField runat="server" ID="hdnddlPinCode1" />
                                        <%--<div class="input-group-btn">
                                        <asp:LinkButton runat="server" ID="btnsearchddlPinCode1" CssClass="btn btn-primary btn-lg-kmi"  title="Search" data-toggle="tooltip" OnClick="GetModelData1">
                                    <span class="glyphicon glyphicon-search BtnGlyphicon"></span>
                                    </asp:LinkButton>
                                 </div>--%>
                                    </div>
                                </div>



                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblPin1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                    <span><font color="red">*</font></span>
                                </div>
                                <div class="col-sm-3">
                                    <%-- style="display:flex"--%>
                                    <%--  <asp:DropDownList ID="ddlPinCode1" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPinCode1_SelectedIndexChanged"
                                                AutoPostBack="True" TabIndex="61">
                                                <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                            </asp:DropDownList>--%>
                                    <%--  <div class="input-group">

                                              <asp:TextBox ID="ddlPinCode1" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hdnddlPinCode1" />
                                        <asp:LinkButton runat="server" ID="btnsearchddlPinCode1" CssClass="btn btn-primary btn-lg-kmi" 
                                            title="Search" data-toggle="tooltip" OnClick="GetModelData1">
                            <span class="glyphicon glyphicon-search BtnGlyphicon"></span>
                          </asp:LinkButton>

                                            </div>--%>

                                    <div class="input-group">
                                        <asp:TextBox ID="ddlPinCode1" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="HiddenField2" />
                                        <div class="input-group-btn">
                                            <asp:LinkButton runat="server" ID="btnsearchddlPinCode1" CssClass="btn btn-primary btn-lg-kmi" title="Search" data-toggle="tooltip"><%--OnClick="GetModelData1"--%>
                                    <span class="glyphicon glyphicon-search BtnGlyphicon"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>

                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblDistrict1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                    <span><font color="red">*</font></span>
                                </div>
                                <div class="col-sm-3">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtDistrict1" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblCountryCode1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                    <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-3">
                                    <%--  <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtState')" runat="server"
                                                    ID="txtCountryCode1" MaxLength="15" TabIndex="12"  Enabled="false"/>--%>
                                    <asp:DropDownList ID="ddlCountryCode1" Enabled="false" runat="server" CssClass="form-control" AutoPostBack="true"
                                        TabIndex="63">
                                        <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12" style="text-align: left">
                                <asp:CheckBox ID="chkAddResident" Enabled="false" Text="ADDRESS IN THE JURISDICTION DETAILS WHERE ENTITY IS RESIDENT OUTSIDE INDIA FOR TAX PURPOSES"
                                    runat="server" CssClass="control-label" TabIndex="64" />
                                <span style="color: red">*</span>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6" style="text-align: left">
                                <asp:CheckBox ID="chkCurrentAdd" Enabled="false" Text="Same as Current / Permanent / Overseas Address details"
                                    TabIndex="65" AutoPostBack="true" runat="server"
                                    CssClass="control-label" />
                                <%--OnCheckedChanged="chkCurrentAdd_Checked"--%>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-6" style="text-align: left">
                                <asp:CheckBox ID="chkCorresAdd" Enabled="false" Text="Same as Correspondance / Local Address details"
                                    TabIndex="66" AutoPostBack="true" runat="server"
                                    CssClass="control-label" /><%--OnCheckedChanged="chkCorresAdd_Checked"--%>
                                <span style="color: red">*</span>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblAddressType2" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlAddressType2" Enabled="false" runat="server" CssClass="form-control" AutoPostBack="true"
                                            TabIndex="41">
                                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblProofOfAddress2" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddlProofOfAddress2" Enabled="false" runat="server" CssClass="form-control"
                                    TabIndex="42">
                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblAddLine1" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox CssClass="form-control" runat="server"
                                    ID="txtAddLine1" Enabled="false" MaxLength="55" TabIndex="67" />
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblAddLine2" Text="" runat="server" CssClass="control-label"></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox CssClass="form-control" runat="server"
                                    ID="txtAddLine2" Enabled="false" MaxLength="55" TabIndex="68" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblAddLine3" Text="" runat="server" CssClass="control-label"></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox CssClass="form-control" runat="server"
                                    ID="txtAddLine3" Enabled="false" MaxLength="55" TabIndex="69" />
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblCity2" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtCity2" Enabled="false" runat="server" CssClass="form-control" TabIndex="70" MaxLength="50"></asp:TextBox>
                            </div>
                        </div>
                        <%--<div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblDistrict2" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddlDistrict2" runat="server" CssClass="form-control" AutoPostBack="true"
                                    Enabled="false" TabIndex="71">
                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblPin2" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddlPinCode2" runat="server" CssClass="form-control" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlPinCode2_SelectedIndexChanged" TabIndex="72">
                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>--%>

                        <div class="row">

                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblState2" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <div class="">
                                    <asp:DropDownList ID="ddlState2" Enabled="false" runat="server" CssClass="form-control" TabIndex="73">
                                        <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:HiddenField runat="server" ID="hdnddlPinCode2" />
                                    <%--<div class="input-group-btn">
                                    <asp:LinkButton runat="server" ID="btnsearchddlPinCode2" CssClass="btn btn-primary btn-lg-kmi"  title="Search" data-toggle="tooltip" OnClick="GetModelData2">
                                    <span class="glyphicon glyphicon-search BtnGlyphicon"></span>
                                    </asp:LinkButton>
                                 </div>--%>
                                </div>
                            </div>
                            <%-- <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblDistrict2" Text="" runat="server" CssClass="control-label"></asp:Label>
                                        <span><font color="red">*</font> </span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtDistrict2" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>--%>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblPin2" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span><font color="red">*</font></span>
                            </div>
                            <div class="col-sm-3">

                                <%-- <asp:DropDownList ID="ddlPinCode2" runat="server" CssClass="form-control" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlPinCode2_SelectedIndexChanged" TabIndex="72">
                                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                        </asp:DropDownList>--%>
                                <%-- <div class="input-group">
                                          <asp:TextBox ID="ddlPinCode2" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hdnddlPinCode2" />
                                        <asp:LinkButton runat="server" ID="btnsearchddlPinCode2" CssClass="btn btn-primary btn-lg-kmi" 
                                            title="Search" data-toggle="tooltip" OnClick="GetModelData2">
                            <span class="glyphicon glyphicon-search BtnGlyphicon"></span>
                          </asp:LinkButton>
                                            </div>--%>

                                <div class="input-group">
                                    <asp:TextBox ID="ddlPinCode2" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:HiddenField runat="server" ID="HiddenField3" />
                                    <div class="input-group-btn">
                                        <asp:LinkButton runat="server" ID="btnsearchddlPinCode2" CssClass="btn btn-primary btn-lg-kmi" title="Search" data-toggle="tooltip"><%--OnClick="GetModelData2"--%>
                                    <span class="glyphicon glyphicon-search BtnGlyphicon"></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>


                            </div>
                        </div>

                        <div class="row">
                            <%-- <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblState2" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <div class="input-group">
                                <asp:DropDownList ID="ddlState2" runat="server" CssClass="form-control" TabIndex="73">
                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                </asp:DropDownList>
                                    <asp:HiddenField runat="server" ID="hdnddlPinCode2" />
                                    <div class="input-group-btn">
                                    <asp:LinkButton runat="server" ID="btnsearchddlPinCode2" CssClass="btn btn-primary btn-lg-kmi"  title="Search" data-toggle="tooltip" OnClick="GetModelData2">
                                    <span class="glyphicon glyphicon-search BtnGlyphicon"></span>
                                    </asp:LinkButton>
                                 </div>
                                 </div>
                            </div>--%>

                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblDistrict2" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span><font color="red">*</font></span>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtDistrict2" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblIsoCountry2" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <%-- <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtIsoCountryCode')"
                                                      runat="server" ID="txtIsoCountryCode" MaxLength="15" TabIndex="12" />--%>
                                <asp:DropDownList ID="ddlIsoCountryCode" Enabled="false" runat="server" CssClass="form-control" AutoPostBack="true"
                                    TabIndex="74">
                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <%-- Added for Address Details end --%>

                <%-- Added for Contact Details start --%>
                <div class="panel panel-success" style="margin-left: 5px; margin-right: 5px">
                    <div id="Div4" runat="server" class="panel-heading subheader" style="background-color: #EDF1cc !important"
                        onclick="ShowReqDtl1('div9','Span3');return false;">
                        <div class="row">
                            <div class="col-sm-10" style="text-align: left">
                                <span class="glyphicon glyphicon-menu-hamburger"></span>CONTACT DETAILS
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
                                    <span class="input-group-addon input-group-addon-tel" style="width: 20% !important; border-top-left-radius: 7% !important; padding: 0px !important; border: 0px !important;">
                                        <asp:TextBox ID="txtTelOff" Enabled="false" runat="server" CssClass="form-control" MaxLength="4"></asp:TextBox>
                                    </span>
                                    <asp:TextBox ID="txtTelOff2" Enabled="false" runat="server" CssClass="form-control" onkeypress="fncInputNumericValuesOnly();"
                                        MaxLength="10"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblTelRes" runat="server" CssClass="control-label" Text=""></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <div class="input-group">
                                    <span class="input-group-addon input-group-addon-tel" style="width: 20% !important; border-top-left-radius: 7% !important; padding: 0px !important; border: 0px !important;">
                                        <asp:TextBox ID="txtTelRes" Enabled="false" runat="server" CssClass="form-control" TabIndex="77"
                                            onkeypress="fncInputNumericValuesOnly();" MaxLength="4" Style="border-top-left-radius: 4px; border-bottom-left-radius: 4px; top: 0px; left: 0px;"></asp:TextBox>
                                    </span>
                                    <asp:TextBox ID="txtTelRes2" Enabled="false" runat="server" CssClass="form-control" MaxLength="10"
                                        onkeypress="fncInputNumericValuesOnly();" TabIndex="78"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblMobile" runat="server" Text="" CssClass="control-label"></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <div class="input-group">
                                    <span class="input-group-addon " style="width: 20% !important; border-top-left-radius: 7% !important; padding: 0px !important; border: 0px !important;">
                                        <asp:TextBox ID="txtMobile" Enabled="false" runat="server" CssClass="form-control" onkeypress="fncInputNumericValuesOnly();"
                                            MaxLength="2" Style="border-top-left-radius: 4px; border-bottom-left-radius: 4px;"></asp:TextBox>
                                    </span>
                                    <asp:TextBox ID="txtMobile2" Enabled="false" runat="server" CssClass="form-control" onkeypress="fncInputNumericValuesOnly();"
                                        MaxLength="10"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblFax" runat="server" Text="" CssClass="control-label"></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <div class="input-group">
                                    <span class="input-group-addon input-group-addon-tel" style="width: 20% !important; border-top-left-radius: 7% !important; padding: 0px !important; border: 0px !important;">
                                        <asp:TextBox ID="txtFax1" Enabled="false" runat="server" MaxLength="4" CssClass="form-control" TabIndex="81"></asp:TextBox>
                                    </span>
                                    <asp:TextBox ID="txtFax2" Enabled="false" runat="server" CssClass="form-control" MaxLength="10" onkeypress="fncInputNumericValuesOnly();"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                Email ID
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtemail" Enabled="false" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <%-- Added for Contact Details end --%>
            </div>
        </div>
    </div>

    <%--Related Person Details section start--%>
    <div class="container" width="100%">
        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
            <div id="Div1" runat="server" class="panel-heading">
                <div class="row">
                    <div class="col-sm-10" style="text-align: left">
                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                        <asp:CheckBox ID="ChkUpdRelated" runat="server" CssClass="standardcheckbox" Text="" Enabled="false"
                            AutoPostBack="true" TabIndex="1" /><%--OnCheckedChanged="ChkUpdRelated_Checked"--%>
                        <%--OnCheckedChanged="ChkUpdRelated_Checked"--%>
                        <asp:Label ID="lblDtlOfRtltpr" Text="" runat="server" CssClass="control-label">
                        </asp:Label>
                    </div>
                    <div class="col-sm-2" onclick="showHideDiv('menu4','Span10');return false;">
                        <span id="Span10" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                    </div>
                </div>
            </div>
            <div id="menu4" style="display: block;" class="panel-body">

                <%--  Added for Details of Related Person start--%>
                <div class="row">
                    <div id="divchkAddRel" class="col-sm-3" style="text-align: left" runat="server">
                        <asp:CheckBox ID="chkAddRel" Enabled="false" Text=" Addition of Related Person" TabIndex="84" AutoPostBack="true"
                            runat="server" CssClass="control-label" />
                        <%--OnCheckedChanged="chkAddRel_Checked" --%>
                        <span style="color: red">*</span>
                    </div>
                    <div id="divchkDelRel" class="col-sm-6" style="text-align: left" runat="server" visible="false">
                        <asp:CheckBox ID="chkDelRel" Enabled="false" Text=" Deletion of Related Person" TabIndex="85" runat="server"
                            CssClass="control-label" /><%--OnCheckedChanged="chkAddRel_Checked" --%>
                        <span style="color: red">*</span>
                    </div>
                    <div id="div10" class="col-sm-3" style="text-align: left" runat="server">
                    </div>
                    <div id="div11" class="col-sm-3" style="text-align: left" runat="server">
                    </div>
                    <div id="div5" class="col-sm-3" style="text-align: left" runat="server">

                        <asp:LinkButton ID="lnkViewRel" runat="server" Enabled="false" Text="View Related Person Detail" style="display:none;" FontBold="true"></asp:LinkButton>
                    </div>
                </div>

                <div id="Div23" runat="server" class="panel-body">

                    <div class="row">
                        <div id="div12" class="col-sm-12" style="text-align: center" runat="server">
                            <asp:Label ID="lblRelRecordShow" Style="text-align: center" Text="  No Records Found" Visible="false" runat="server" />
                        </div>
                        <asp:GridView ID="gvMemDtls" Width="100%" runat="server" CssClass="footable"
                            PageSize="10" AllowPaging="true" CellPadding="1"
                            AutoGenerateColumns="False">
                            <%--OnRowDataBound="gvMemDtls_RowDataBound"--%>
                            <RowStyle CssClass="GridViewRow"></RowStyle>
                            <%--<PagerStyle CssClass="disablepage" />--%>
                            <%--OnPageIndexChanging="gvMemDtls_PageIndexChanging" OnRowCreated="gvMemDtls_RowCreated"--%>
                            <FooterStyle CssClass="GridViewFooter" />
                            <HeaderStyle HorizontalAlign="Center" BackColor="#dce9f9" />
                            <SelectedRowStyle CssClass="GridViewSelectedRow" />
                            <AlternatingRowStyle CssClass="GridViewAlternateRow"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="Sr. No." ItemStyle-Width="10%" SortExpression="Reference No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Relative Reference No." ItemStyle-Width="20%" SortExpression="Reference No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("RelRefNo") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Relative Type" ItemStyle-Width="20%" SortExpression="Reference No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRelTypVal" runat="server" Text='<%# Eval("RelationTypetxt") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Relative Name" ItemStyle-Width="20%" SortExpression="Candidate Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNameVal" runat="server" Text='<%# Eval("FNameRel") + " " + Eval("LNameRel")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date Of Birth" ItemStyle-Width="20%" SortExpression="KYC No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMemDOBVal" runat="server" Text='<%# Eval("DOBRel") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Gender" ItemStyle-Width="20%" SortExpression="KYC No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMemGenVal" runat="server" Text='<%# Eval("GenderReltxt") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Marital Status " ItemStyle-Width="20%" SortExpression="KYC No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMemMrtVal" runat="server" Text='<%# Eval("MaritalStatusReltxt") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Citizenship" ItemStyle-Width="20%" SortExpression="KYC No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMemCizVal" runat="server" Text='<%# Eval("CitizenshipReltxt") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Residential Status " ItemStyle-Width="20%" SortExpression="KYC No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMemResiVal" runat="server" Text='<%# Eval("ResiStatusReltxt") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Occupation Type" ItemStyle-Width="20%" SortExpression="KYC No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMemOccVal" runat="server" Text='<%# Eval("OccuTypeReltxt") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="15%" SortExpression="Request" HeaderText="Action">
                                    <ItemTemplate>
                                        <div style="width: 20%; white-space: nowrap;">
                                            <span class="glyphicon glyphicon-flag"></span>
                                            <asp:LinkButton ID="lnkEdit" runat="server" Text="View" OnCommand="lnkEdit_Command" CommandName='<%# Eval("RelRefNo")%>'></asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="pad" HorizontalAlign="Left" Width="10%" />
                                </asp:TemplateField>




                            </Columns>
                            <PagerTemplate>
                                <table class="tablePager" cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td class="tablePagerDataSmall" align="left">
                                            <asp:Label ID="lblpageindx2" CssClass="standardlabelCRM" Text="Page : " runat="server"></asp:Label>
                                        </td>
                                        <td align="center" class="tablePagerData" style="display: none;">
                                            <table cellspacing="2">
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ToolTip="First Page" CommandName="Page" CommandArgument="First"
                                                            runat="server" ID="ImgbtnFirst" ImageUrl="../../Content/Images/ImgArrFirst.gif" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ToolTip="Previous Page" CommandName="Page" CommandArgument="Prev"
                                                            runat="server" ID="ImgbtnPrevious" ImageUrl="../../Content/Images/ImgArrPrevious.gif" />
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ToolTip="Goto Page" ID="ddlPageSelectorL" runat="server" AutoPostBack="true"
                                                            CssClass="standardPagerDropdown">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ToolTip="Next Page" CommandName="Page" CommandArgument="Next" runat="server"
                                                            ID="ImgbtnNext" ImageUrl="../../Content/Images/ImgArrNext.gif" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ToolTip="Last Page" CommandName="Page" CommandArgument="Last" runat="server"
                                                            ID="ImgbtnLast" ImageUrl="../../Content/Images/ImgArrLast.gif" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="right" class="tablePagerData">
                                            <table cellspacing="2">
                                                <tr>
                                                    <td>
                                                        <span style="padding-left: 5px;"></span>
                                                        <asp:ImageButton ToolTip="First Page" CommandName="Page" CommandArgument="First"
                                                            runat="server" ID="ImgbtnFirst1" ImageUrl="../../Content/Images/ImgArrFirst.gif" />
                                                    </td>
                                                    <td>
                                                        <span style="padding-left: 5px;"></span>
                                                        <asp:ImageButton ToolTip="Previous Page" CommandName="Page" CommandArgument="Prev"
                                                            runat="server" ID="ImgbtnPrevious1" ImageUrl="../../Content/Images/ImgArrPrevious.gif" />
                                                    </td>
                                                    <td>
                                                        <span style="padding-left: 5px;"></span>
                                                        <asp:DropDownList ToolTip="Goto Page" ID="ddlPageSelectorR" runat="server" AutoPostBack="true"
                                                            CssClass="standardPagerDropdown">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <span style="padding-left: 5px;"></span>
                                                        <asp:ImageButton ToolTip="Next Page" CommandName="Page" CommandArgument="Next" runat="server"
                                                            ID="ImgbtnNext1" ImageUrl="../../Content/Images/ImgArrNext.gif" />
                                                    </td>
                                                    <td>
                                                        <span style="padding-left: 5px;"></span>
                                                        <asp:ImageButton ToolTip="Last Page" CommandName="Page" CommandArgument="Last" runat="server"
                                                            ID="ImgbtnLast1" ImageUrl="../../Content/Images/ImgArrLast.gif" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="tablePagerDataSmall" align="right" style="display: none">
                                            <asp:Label ID="lblpageindx" CssClass="standardlabelCRM" Text="Page : " runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                </table>
                            </PagerTemplate>
                        </asp:GridView>
                    </div>
                </div>

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
    <%--Related Person Details section end--%>


    <%--Controlling Person Details section start--%>
    <div class="container" width="100%">
        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
            <div id="Div2" runat="server" class="panel-heading">
                <div class="row">
                    <div class="col-sm-10" style="text-align: left">
                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                        <asp:CheckBox ID="ChkUpdControlling" runat="server" CssClass="standardcheckbox" Text="" Enabled="false"
                            AutoPostBack="true" TabIndex="1" /><%--OnCheckedChanged="ChkUpdControlling_Checked"--%>
                        <%--OnCheckedChanged="ChkUpdRelated_Checked"--%>
                        <asp:Label ID="lblDtlOfCtrlpr" Text="" runat="server" CssClass="control-label">
                        </asp:Label>
                    </div>
                    <div class="col-sm-2" onclick="showHideDiv('dvCtrlPrsn','spnControlPrsn');return false;">
                        <span id="spnControlPrsn" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                    </div>
                </div>
            </div>
            <div id="dvCtrlPrsn" style="display: block;" class="panel-body">

                <%--  Added for Details of Related Person start--%>
                <div class="row">
                    <div id="div3" class="col-sm-3" style="text-align: left" runat="server">
                        <asp:CheckBox ID="chkAddCtrl" Enabled="false" Text=" Addition of Controlling Person" TabIndex="84" AutoPostBack="true"
                            runat="server" CssClass="control-label" /><%--OnCheckedChanged="chkAddCtrl_Checked" --%>
                        <span style="color: red">*</span>
                    </div>
                    <div id="div8" class="col-sm-6" style="text-align: left" runat="server" visible="false">
                        <asp:CheckBox ID="chkDelCtrl" Enabled="false" Text=" Deletion of Controlling Person" TabIndex="85" runat="server"
                            CssClass="control-label" /><%--OnCheckedChanged="chkAddCtrl_Checked" --%>
                        <span style="color: red">*</span>
                    </div>
                    <div id="div13" class="col-sm-3" style="text-align: left" runat="server">
                    </div>
                    <div id="div14" class="col-sm-3" style="text-align: left" runat="server">
                    </div>
                    <div id="div19" class="col-sm-3" style="text-align: left" runat="server">

                        <asp:LinkButton ID="lnkViewCtrl" runat="server" Text="View Controlling Person Detail" style="display:none;" FontBold="true"></asp:LinkButton><%--OnClick="lnkViewCtrl_Click"--%>
                    </div>
                </div>

                <div id="Div24" runat="server" class="panel-body">
                    <div class="row">
                        <div id="div20" class="col-sm-12" style="text-align: center" runat="server">
                            <asp:Label ID="Label2" Style="text-align: center" Text="  No Records Found" Visible="false" runat="server" />
                        </div>
                        <asp:GridView ID="gvCtrlPrson" Width="100%" runat="server" CssClass="footable"
                            PageSize="10" AllowPaging="true" CellPadding="1"
                            AutoGenerateColumns="False">
                            <%--OnRowDataBound="gvCtrlPrson_RowDataBound"--%>
                            <RowStyle CssClass="GridViewRow"></RowStyle>
                            <%--<PagerStyle CssClass="disablepage" />--%>
                            <%--OnPageIndexChanging="gvMemDtls_PageIndexChanging" OnRowCreated="gvMemDtls_RowCreated"--%>
                            <FooterStyle CssClass="GridViewFooter" />
                            <HeaderStyle HorizontalAlign="Center" BackColor="#dce9f9" />
                            <SelectedRowStyle CssClass="GridViewSelectedRow" />
                            <AlternatingRowStyle CssClass="GridViewAlternateRow"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="Sr. No." ItemStyle-Width="10%" SortExpression="Reference No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Controlling Reference No." ItemStyle-Width="20%" SortExpression="Reference No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("RelRefNo") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Controlling Type" ItemStyle-Width="20%" SortExpression="Reference No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRelTypVal" runat="server" Text='<%# Eval("RelationTypetxt") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Controlling Name" ItemStyle-Width="20%" SortExpression="Candidate Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNameVal" runat="server" Text='<%# Eval("FNameRel") + " " + Eval("LNameRel")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date Of Birth" ItemStyle-Width="20%" SortExpression="KYC No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMemDOBVal" runat="server" Text='<%# Eval("DOBRel") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Gender" ItemStyle-Width="20%" SortExpression="KYC No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMemGenVal" runat="server" Text='<%# Eval("GenderReltxt") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Marital Status " ItemStyle-Width="20%" SortExpression="KYC No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMemMrtVal" runat="server" Text='<%# Eval("MaritalStatusReltxt") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Citizenship" ItemStyle-Width="20%" SortExpression="KYC No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMemCizVal" runat="server" Text='<%# Eval("CitizenshipReltxt") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Residential Status " ItemStyle-Width="20%" SortExpression="KYC No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMemResiVal" runat="server" Text='<%# Eval("ResiStatusReltxt") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Occupation Type" ItemStyle-Width="20%" SortExpression="KYC No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMemOccVal" runat="server" Text='<%# Eval("OccuTypeReltxt") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="15%" SortExpression="Request" HeaderText="Action">
                                    <ItemTemplate>
                                        <div style="width: 20%; white-space: nowrap;">
                                            <span class="glyphicon glyphicon-flag"></span>
                                            <asp:LinkButton ID="lnkEdit" runat="server" Text="View" OnCommand="lnkEdit_Command1" CommandName='<%# Eval("RelRefNo") %>'></asp:LinkButton><%--OnClick="lnkEditCtrl_Click"--%>
                                        </div>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="pad" HorizontalAlign="Left" Width="10%" />
                                </asp:TemplateField>




                            </Columns>
                            <PagerTemplate>
                                <table class="tablePager" cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td class="tablePagerDataSmall" align="left">
                                            <asp:Label ID="lblpageindx2" CssClass="standardlabelCRM" Text="Page : " runat="server"></asp:Label>
                                        </td>
                                        <td align="center" class="tablePagerData" style="display: none;">
                                            <table cellspacing="2">
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ToolTip="First Page" CommandName="Page" CommandArgument="First"
                                                            runat="server" ID="ImgbtnFirst" ImageUrl="../../Content/Images/ImgArrFirst.gif" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ToolTip="Previous Page" CommandName="Page" CommandArgument="Prev"
                                                            runat="server" ID="ImgbtnPrevious" ImageUrl="../../Content/Images/ImgArrPrevious.gif" />
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ToolTip="Goto Page" ID="ddlPageSelectorL" runat="server" AutoPostBack="true"
                                                            CssClass="standardPagerDropdown">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ToolTip="Next Page" CommandName="Page" CommandArgument="Next" runat="server"
                                                            ID="ImgbtnNext" ImageUrl="../../Content/Images/ImgArrNext.gif" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ToolTip="Last Page" CommandName="Page" CommandArgument="Last" runat="server"
                                                            ID="ImgbtnLast" ImageUrl="../../Content/Images/ImgArrLast.gif" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="right" class="tablePagerData">
                                            <table cellspacing="2">
                                                <tr>
                                                    <td>
                                                        <span style="padding-left: 5px;"></span>
                                                        <asp:ImageButton ToolTip="First Page" CommandName="Page" CommandArgument="First"
                                                            runat="server" ID="ImgbtnFirst1" ImageUrl="../../Content/Images/ImgArrFirst.gif" />
                                                    </td>
                                                    <td>
                                                        <span style="padding-left: 5px;"></span>
                                                        <asp:ImageButton ToolTip="Previous Page" CommandName="Page" CommandArgument="Prev"
                                                            runat="server" ID="ImgbtnPrevious1" ImageUrl="../../Content/Images/ImgArrPrevious.gif" />
                                                    </td>
                                                    <td>
                                                        <span style="padding-left: 5px;"></span>
                                                        <asp:DropDownList ToolTip="Goto Page" ID="ddlPageSelectorR" runat="server" AutoPostBack="true"
                                                            CssClass="standardPagerDropdown">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <span style="padding-left: 5px;"></span>
                                                        <asp:ImageButton ToolTip="Next Page" CommandName="Page" CommandArgument="Next" runat="server"
                                                            ID="ImgbtnNext1" ImageUrl="../../Content/Images/ImgArrNext.gif" />
                                                    </td>
                                                    <td>
                                                        <span style="padding-left: 5px;"></span>
                                                        <asp:ImageButton ToolTip="Last Page" CommandName="Page" CommandArgument="Last" runat="server"
                                                            ID="ImgbtnLast1" ImageUrl="../../Content/Images/ImgArrLast.gif" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="tablePagerDataSmall" align="right" style="display: none">
                                            <asp:Label ID="lblpageindx" CssClass="standardlabelCRM" Text="Page : " runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                </table>
                            </PagerTemplate>
                        </asp:GridView>
                    </div>
                </div>

                <div id="div22" runat="server" class="row">
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
    <%--Controlling Person Details section end--%>


    <%--Proof of Personal Details Start--%>
    <%--<div class="container" width="100%">
        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
            <div id="divPersonalDetl" runat="server" class="panel-heading" onclick="showHideDiv('menu1','Span8');return false;">
                <div class="row">
                    <div class="col-sm-10" style="text-align: left">
                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                        <asp:Label ID="lblPersonalDetail" Text="PERSONAL DETAIL" runat="server" CssClass="control-label">
                        </asp:Label>
                    </div>
                    <div class="col-sm-2">
                        <span id="Span8" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                    </div>
                </div>
            </div>
            <div id="menu1" style="display: block;" class="panel-body">
                <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                    <div id="divPersonalDtl1" runat="server" class="panel-heading subheader" style="background-color: #EDF1cc !important"
                        onclick="ShowReqDtl1('divPersonal','btnpersnl');return false;">
                        <div class="row">
                            <div class="col-sm-10" style="text-align: left">
                                <span class="glyphicon glyphicon-menu-hamburger"></span>
                                <asp:Label ID="lblpfPersonal" Text="PERSONAL DETAILS" runat="server" CssClass="control-label">
                                </asp:Label>
                            </div>
                            <div class="col-sm-2">
                                <span id="btnpersnl" class="glyphicon glyphicon-resize-full" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div>
                    <div id="divPersonal" style="display: block;" class="form-group panel-body">
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                            </div>
                            <div class="col-sm-9" style="padding-left: 0">
                                <div class="col-sm-2" style="padding-left: 3%">
                                    <asp:Label ID="lblprefx" Text="Prefix" runat="server" CssClass="control-label">
                                    </asp:Label>
                                </div>
                                <div class="col-sm-10" style="padding-left: 0">
                                    <div class="col-sm-4">
                                        <asp:Label ID="lblfname" Text="First Name" runat="server" CssClass="control-label">
                                        </asp:Label>
                                    </div>
                                    <div class="col-sm-4" style="padding-left: 3%">
                                        <asp:Label ID="lblMname" Text="Middle Name" runat="server" CssClass="control-label">
                                        </asp:Label>
                                    </div>
                                    <div class="col-sm-4" style="padding-left: 4%">
                                        <asp:Label ID="lblLname" Text="Last Name" runat="server" CssClass="control-label">
                                        </asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblName" Text="Name" runat="server" CssClass="control-label">
                                </asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-9" style="padding: 0">
                                <div class="col-sm-2">
                                    <asp:DropDownList ID="cboTitle" runat="server" CssClass="form-control" DataTextField="ParamDesc"
                                        DataValueField="ParamValue" AppendDataBoundItems="True">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-10" style="padding: 0">
                                    <div class="col-sm-4" style="padding-left: 0">
                                        <asp:TextBox ID="txtGivenName" runat="server" CssClass="form-control" onkeypress="funIsAlphaNumericWithSpace();"
                                            onchange="javascript:this.value=this.value.toUpperCase();" MaxLength="50" placeholder="First Name">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-4" style="padding-left: 0">
                                        <asp:TextBox ID="txtMiddleName" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"
                                            onkeypress="funIsAlphaNumericWithSpace();" MaxLength="50" onblur="CheckSpaces();return false;"
                                            placeholder="Middle Name">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-4" style="padding-left: 0">
                                        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"
                                            onkeypress="funIsAlphaNumericWithSpace();" MaxLength="50" onblur="CheckSpaces();return false;"
                                            placeholder="Last Name">
                                        </asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblMaidenName" Text="Maiden Name" CssClass="control-label" runat="server">
                                </asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-9" style="padding: 0">
                                <div class="col-sm-2">
                                    <asp:DropDownList ID="cboTitle1" runat="server" CssClass="form-control" DataTextField="ParamDesc"
                                        DataValueField="ParamValue" AppendDataBoundItems="True">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-10" style="padding: 0">
                                    <div class="col-sm-4" style="padding-left: 0">
                                        <asp:TextBox ID="txtGivenName1" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"
                                            onkeypress="funIsAlphaNumericWithSpace();" MaxLength="50" onblur="CheckSpaces();return false;"
                                            placeholder="First Name">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-4" style="padding-left: 0">
                                        <asp:TextBox ID="txtMiddleName1" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"
                                            onkeypress="funIsAlphaNumericWithSpace();" MaxLength="50" onblur="CheckSpaces();return false;"
                                            placeholder="Middle Name">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-4" style="padding-left: 0">
                                        <asp:TextBox ID="txtLastName1" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"
                                            onkeypress="funIsAlphaNumericWithSpace();" MaxLength="50" onblur="CheckSpaces();return false;"
                                            placeholder="Last Name">
                                        </asp:TextBox>
                                    </div>
                                    <asp:HiddenField ID="hdnGenderTitle1" runat="server"></asp:HiddenField>
                                    <asp:HiddenField ID="hdnGenderTitle2" runat="server"></asp:HiddenField>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3">
                                <div class="col-sm-6" style="padding: 0">
                                    <asp:Label ID="lblFatherName" Text=" Father/Spouse Name" CssClass="control-label"
                                        runat="server"></asp:Label>
                                    <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-6" style="padding: 0">
                                    <asp:UpdatePanel ID="UpdFSFlag" runat="server">
                                        <ContentTemplate>
                                            <asp:RadioButtonList ID="rbtFS" runat="server" CssClass="radiobtn" RepeatDirection="Horizontal"
                                                Visible="true" AutoPostBack="true">
                                                <asp:ListItem Value="F">Father</asp:ListItem>
                                                <asp:ListItem Value="S">Spouse</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="col-sm-9" style="padding: 0">
                                <div class="col-sm-2">
                                    <asp:UpdatePanel ID="upcboTitle2" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cboTitle2" runat="server" CssClass="form-control" DataTextField="ParamDesc"
                                                DataValueField="ParamValue" AppendDataBoundItems="True">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-sm-10" style="padding: 0">
                                    <div class="col-sm-4" style="padding-left: 0">
                                        <asp:TextBox ID="txtGivenName2" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"
                                            onkeypress="funIsAlphaNumericWithSpace();" MaxLength="50" onblur="CheckSpaces();return false;"
                                            placeholder="First Name">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-4" style="padding-left: 0">
                                        <asp:TextBox ID="txtMiddleName2" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"
                                            onkeypress="funIsAlphaNumericWithSpace();" MaxLength="50" onblur="CheckSpaces();return false;"
                                            placeholder="Middle Name">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-4" style="padding-left: 0">
                                        <asp:TextBox ID="txtLastName2" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"
                                            onkeypress="funIsAlphaNumericWithSpace();" MaxLength="50" onblur="CheckSpaces();return false;"
                                            placeholder="Last Name">
                                        </asp:TextBox>
                                    </div>
                                    <asp:HiddenField ID="HiddenField1" runat="server"></asp:HiddenField>
                                    <asp:HiddenField ID="HiddenField2" runat="server"></asp:HiddenField>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblMotherName" Text="Mother Name" CssClass="control-label" runat="server">
                                </asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-9" style="padding: 0">
                                <div class="col-sm-2">
                                    <asp:DropDownList ID="cboTitle3" runat="server" CssClass="form-control" DataTextField="ParamDesc"
                                        DataValueField="ParamValue" AppendDataBoundItems="True">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-10" style="padding: 0">
                                    <div class="col-sm-4" style="padding-left: 0">
                                        <asp:TextBox ID="txtGivenName3" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"
                                            onkeypress="funIsAlphaNumericWithSpace();" MaxLength="50" onblur="CheckSpaces();return false;"
                                            placeholder="First Name">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-4" style="padding-left: 0">
                                        <asp:TextBox ID="txtMiddleName3" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"
                                            onkeypress="funIsAlphaNumericWithSpace();" MaxLength="50" onblur="CheckSpaces();return false;"
                                            placeholder="Middle Name">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-4" style="padding-left: 0">
                                        <asp:TextBox ID="txtLastName3" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"
                                            onkeypress="funIsAlphaNumericWithSpace();" MaxLength="50" onblur="CheckSpaces();return false;"
                                            placeholder="Last Name">
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
                                <asp:Label ID="lbldob" Text="DOB (dd/mm/yyyy) " runat="server" CssClass="control-label">
                                </asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtDOB" MaxLength="15" onmousedown="$('#EmptyPagePlaceholder_txtDOB').datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy', yearRange: '1945:'+(new Date).getFullYear()  });"
                                    onblur="setDateFormat('EmptyPagePlaceholder_txtDOB');" />
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblGender" Text="Gender" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="upcboGender" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cboGender" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="row">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblOccupation" Text="Occupation" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlOccupation" AutoPostBack="true" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left" id="divOccuSubType" runat="server">
                                        <asp:Label ID="lblOccuSubType" Text="Occupation Sub Type" runat="server" CssClass="control-label">
                                        </asp:Label>
                                        <span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlOccuSubType" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlOccupation" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblMarStatus" Text="Marital Status" runat="server" CssClass="control-label">
                                </asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="upMaritalStatus" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlMaritalStatus" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblCitizenship" Text="Nationality" runat="server" CssClass="control-label">
                                </asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="upCitizenship" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlCitizenship" runat="server" CssClass="form-control" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblResStatus" Text="Residential Status" runat="server" CssClass="control-label">
                                </asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="upResStatus" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlResStatus" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:UpdatePanel ID="uplblIsoCountryCodeOthr" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lblIsoCountryCodeOthr" Text="" Visible="false" runat="server" CssClass="control-label"></asp:Label>
                                        <span id="asteriskIsoCountryCodeOthr" style="color: red" runat="server" visible="false">*</span>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="upIsoCountryCodeOthr" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlIsoCountryCodeOthr" runat="server" CssClass="form-control"
                                            AutoPostBack="true" Visible="false">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                    <div id="divTickIfApplicable" runat="server" class="panel-heading subheader" style="background-color: #EDF1cc !important"
                        onclick="ShowReqDtl1('divAdditionalDtl','Span5');return false;">
                        <div class="row">
                            <div class="col-sm-10" style="text-align: left">
                                <span class="glyphicon glyphicon-menu-hamburger"></span>
                                <asp:Label ID="lbltick" Text="TICK IF APPLICABLE" runat="server" CssClass="control-label">
                                </asp:Label>
                            </div>
                            <div class="col-sm-2">
                                <span id="Span5" class="glyphicon glyphicon-resize-full" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div>
                    <div id="divAdditionalDtl" style="display: block;" class="panel-body">
                        ADIITIONAL DETAILS REQUIRED*(Mandatory only if section 5.2 is ticked)
                        <div class="row">
                            <div class="col-sm-12" style="text-align: left">
                                <asp:CheckBox ID="chkTick" Text=" RESIDENCE FOR TAX PURPOSES IN JURISDICTIONS(S) OUTSIDE INDIA"
                                    CssClass="standardcheckbox" runat="server" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblIsoCountryCode2" Text="ISO 3166 Country Code of Jurisdiction of Residence"
                                    runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddlIsoCountryCode2" runat="server" CssClass="form-control"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblTaxIden" Text="Tax Identification Number or equivalent(if issued by jurisdiction)"
                                    runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtIDResTax" MaxLength="20"
                                    onkeypress="funIsAlphaNumeric();" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblPlace" Text="Place/City of Birth" runat="server" CssClass="control-label">
                                </asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtDOBRes" MaxLength="15" />
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblIsoContry" Text="ISO 3166 Country Code of Birth" runat="server"
                                    CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddlIsoCountry" runat="server" CssClass="form-control" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>
    <%--Proof of Personal Details End--%>

    <%--Proof of ID Start--%>
    <%--<div class="container" width="100%">
        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px" onclick="showHideDiv('divProof','btnProofIde');return false;">
            <div id="divProofofidenty" runat="server" class="panel-heading">
                <div class="row">
                    <div class="col-sm-10" style="text-align: left">
                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                        <asp:Label ID="lblProofOfIdentity11" Text=" PROOF OF IDENTITY(Pol)" runat="server"
                            CssClass="control-label"></asp:Label>
                    </div>
                    <div class="col-sm-2">
                        <span id="btnProofIde" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                    </div>
                </div>
            </div>
            <div id="divProof" style="display: block;" class="panel-body">
                <div class="row">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblProof" Text="(Certified copy of any one the following Proof of Identity [Pol] needs to be submitted)"
                            runat="server" CssClass="control-label"></asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlProofIdentity" runat="server" CssClass="form-control" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>
    <%--Proof of ID End--%>

    <%--Proof of Address Start--%>
    <%--<div class="container" width="100%">
        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
            <div id="divProofOfAddress1" runat="server" class="panel-heading" onclick="showHideDiv('divAddressDtl','divPOA');return false;">
                <div class="row">
                    <div class="col-sm-10" style="text-align: left">
                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                        <asp:Label ID="lblProofOfAddress" Text="PROOF OF ADDRESS(POA)" runat="server" CssClass="control-label">
                        </asp:Label>
                    </div>
                    <div class="col-sm-2">
                        <span id="divPOA" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                    </div>
                </div>
            </div>
            <div id="divAddressDtl" style="display: block;" class="panel-body">
                <div class="row" style="margin-bottom: 5px">
                    <div class="col-sm-3">
                        <asp:Label ID="Label44" Text="Address Type" runat="server" Font-Bold="false"></asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="DropDownList19" runat="server" CssClass="form-control" TabIndex="62">
                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                            <asp:ListItem Value="" Text="Residential / Bussiness"></asp:ListItem>
                            <asp:ListItem Value="" Text="Residential"></asp:ListItem>
                            <asp:ListItem Value="" Text="Bussiness"></asp:ListItem>
                            <asp:ListItem Value="" Text="Registered Office"></asp:ListItem>
                            <asp:ListItem Value="" Text="Unspecified"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-3">
                        <asp:Label ID="Label45" Text="Proof of Address Type" runat="server" Font-Bold="false"></asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="DropDownList20" runat="server" CssClass="form-control" TabIndex="62">
                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                            <asp:ListItem Value="" Text="Certificate of Incorporation / Formation"></asp:ListItem>
                            <asp:ListItem Value="" Text="Registration Certificate"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row" style="margin-bottom: 5px">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="Label46" Text="Address Line1" runat="server" Font-Bold="false"></asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3" style="text-align: left; top: 2px; left: 0px;">
                        <asp:TextBox ID="TextBox23" runat="server" CssClass="form-control" Font-Bold="false"
                            TabIndex="2">
                        </asp:TextBox>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="Label48" Text="Address Line2" runat="server" Font-Bold="false"></asp:Label>
                    </div>
                    <div class="col-sm-3" style="text-align: left; top: 2px; left: 0px;">
                        <asp:TextBox ID="TextBox24" runat="server" CssClass="form-control" Font-Bold="false"
                            TabIndex="2">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="row" style="margin-bottom: 5px">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="Label49" Text="Address Line3" runat="server" Font-Bold="false"></asp:Label>
                    </div>
                    <div class="col-sm-3" style="text-align: left; top: 2px; left: 0px;">
                        <asp:TextBox ID="TextBox25" runat="server" CssClass="form-control" Font-Bold="false"
                            TabIndex="2">
                        </asp:TextBox>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="Label50" Text="City/Town/Village" runat="server" Font-Bold="false"></asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3" style="text-align: left; top: 2px; left: 0px;">
                        <asp:TextBox ID="TextBox26" runat="server" CssClass="form-control" Font-Bold="false"
                            TabIndex="2">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="row" style="margin-bottom: 5px">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="Label51" Text="District" runat="server" Font-Bold="false"></asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3" style="text-align: left; top: 2px; left: 0px;">
                        <asp:DropDownList ID="DropDownList21" runat="server" CssClass="form-control" TabIndex="62">
                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="Label52" Text="Pin/Post Code" runat="server" Font-Bold="false"></asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3" style="text-align: left; top: 2px; left: 0px;">
                        <asp:DropDownList ID="DropDownList22" runat="server" CssClass="form-control" TabIndex="62">
                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row" style="margin-bottom: 5px">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="Label53" Text="State/U.T Code" runat="server" Font-Bold="false"></asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3" style="text-align: left; top: 2px; left: 0px;">
                        <asp:DropDownList ID="DropDownList23" runat="server" CssClass="form-control" TabIndex="62">
                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="Label54" Text="ISO 3166 Country Code" runat="server" Font-Bold="false"></asp:Label>
                        <span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3" style="text-align: left; top: 2px; left: 0px;">
                        <asp:DropDownList ID="DropDownList24" runat="server" CssClass="form-control" TabIndex="62">
                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>
    <%--Proof of Address End--%>

    <div class="container" width="100%">
        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
            <div id="divRemark" runat="server" class="panel-heading" onclick="showHideDiv('divRemarkDtl','Span11');return false;">
                <div class="row">
                    <div class="col-sm-10" style="text-align: left">
                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                        <asp:Label ID="lblRemarks" Text=" REMARKS" runat="server" CssClass="control-label">
                        </asp:Label>
                    </div>
                    <div class="col-sm-2">
                        <span id="Span11" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                    </div>
                </div>
            </div>
            <div id="divRemarkDtl" style="display: block;" class="panel-body">
                <div class="row">
                    <div class="col-sm-12">
                        <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtRemarks')" runat="server"
                            ID="txtRemarks" Enabled="false" TextMode="MultiLine" MaxLength="15" TabIndex="96" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%-- APPLICATION DECLARATION start --%>
    <div class="container" width="100%">
        <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
            <div id="divApplivationDeclare" runat="server" class="panel-heading" onclick="showHideDiv('menu6','Span12');return false;">
                <div class="row">
                    <div class="col-sm-10" style="text-align: left">
                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                        <asp:Label ID="lblattstn" Text="APPLICATION DECLARATION" runat="server" CssClass="control-label">
                        </asp:Label>
                    </div>
                    <div class="col-sm-2">
                        <span id="Span12" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                    </div>
                </div>
            </div>
            <div id="menu6" style="display: block;" class="panel-body">
                <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                    <div id="Div15" runat="server" class="panel-heading subheader" style="background-color: #EDF1cc !important"
                        onclick="ShowReqDtl1('div16','Span7');return false;">
                        <div class="row">
                            <div class="col-sm-10" style="text-align: left">
                                <span class="glyphicon glyphicon-menu-hamburger"></span>
                                <asp:Label ID="lbldec" Text="APPLICATION DECLARATION" runat="server" CssClass="control-label">
                                </asp:Label>
                            </div>
                            <div class="col-sm-2">
                                <span id="Span7" class="glyphicon glyphicon-resize-full" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div>
                    <div id="div16" style="display: block;" class="panel-body">
                        <div class="row">
                            <div class="col-sm-12" style="text-align: left; display: flex;">
                                <asp:CheckBox Style="margin-bottom: -0.5%;" ID="chkAppDeclare1" Enabled="false" Text="I/We hereby declare that the details furnished above are true and correct to the best of my/our knowledge and belief and I/we undertake to inform you of "
                                    CssClass="control-label" runat="server" onchange="setDateFormat('txtRemarks')"
                                    TabIndex="97" />
                            </div>
                            <div class="col-sm-12" style="text-align: left; display: flex; font-weight: bold; padding-left: 2.5%;">
                                <asp:Label CssClass="control-label" Enabled="false" Text="any changes therein, immediately.In case any of the above information is found to be false or untrue or misleading or misrepresenting, I/we am/are aware that I/we may be held liable for it."
                                    runat="server" ID="lblAppDeclare1" maxlength="15" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12" style="text-align: left; display: flex;">
                                <asp:CheckBox ID="chkAppDeclare2" Enabled="false" Text="My/Our personal KYC details may be shared with Central KYC Registry."
                                    CssClass="control-label" runat="server" onchange="setDateFormat('txtRemarks')"
                                    TabIndex="98" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12" style="text-align: left; display: flex;">
                                <asp:CheckBox ID="chkAppDeclare3" Enabled="false" Text="I/We hereby consent to receiving information from Central KYC Registry through SMS/Email on the above registered number/email address."
                                    CssClass="control-label" runat="server" onchange="setDateFormat('txtRemarks')"
                                    TabIndex="98" />
                            </div>
                        </div>
                        <br />
                        <br />
                        <%-- <br />
                            <br />--%>
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblDate" Text="Date" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3" style="text-align: left; top: 0px; left: 0px;">
                                <asp:TextBox ID="txtDate" Enabled="false" runat="server" CssClass="form-control" onmousedown="$(this).datepicker({ maxDate: new Date() ,changeMonth: true, changeYear: true, dateFormat: 'dd-mm-yy' });" MaxLength="10"></asp:TextBox>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblPlace1" Text="Place" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox CssClass="form-control" Enabled="false" onchange="setDateFormat('txtPlace')" runat="server"
                                    ID="txtPlace" MaxLength="15" TabIndex="100" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">
                    <div id="Div17" runat="server" class="panel-heading subheader" style="background-color: #EDF1cc !important"
                        onclick="ShowReqDtl1('div21','Span10');return false;">
                        <div class="row">
                            <div class="col-sm-10" style="text-align: left">
                                <span class="glyphicon glyphicon-menu-hamburger"></span>
                                <asp:Label ID="lblAttesOfc" Text="ATTESTATION / FOR OFFICE USE ONLY" runat="server"
                                    CssClass="control-label">
                                </asp:Label>
                            </div>
                            <div class="col-sm-2">
                                <span id="Span10" class="glyphicon glyphicon-resize-full" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div>
                    <div id="div21" style="display: block;" class="panel-body">
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblDocRec" Text="" runat="server" Font-Bold="true"
                                    CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-4">
                                <asp:CheckBox ID="chkSelfCerti" Text="" CssClass="standardcheckbox"
                                    AutoPostBack="true" runat="server" TabIndex="101" />
                                <span>Self-Certified</span>
                                <asp:CheckBox ID="chkTrueCopies" Text="" CssClass="standardcheckbox"
                                    AutoPostBack="true" runat="server" TabIndex="101" />
                                <span>True Copies</span>
                                <asp:CheckBox ID="chkNotary" Text="" CssClass="standardcheckbox"
                                    AutoPostBack="true" runat="server" TabIndex="101" />
                                <span>Notary</span>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblRiskCategory" Text="" runat="server" Font-Bold="true"
                                    CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-4">
                                <asp:CheckBox ID="chkHigh" Text="" CssClass="standardcheckbox"
                                    AutoPostBack="true" runat="server" TabIndex="101" />
                                <span>High</span>
                                <asp:CheckBox ID="chkMedium" Text="" CssClass="standardcheckbox"
                                    AutoPostBack="true" runat="server" TabIndex="101" />
                                <span>Medium</span>
                                <asp:CheckBox ID="chkLow" Text="" CssClass="standardcheckbox"
                                    AutoPostBack="true" runat="server" TabIndex="101" />
                                <span>Low</span>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-sm-12" style="text-align: center">
                                <asp:Label ID="lblKYCVerify" Style='text-align: center' CssClass="control-label"
                                    Font-Bold="true" Text="" runat="server" />
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblIdVerif" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3" style="text-align: left; top: 0px; left: 0px;">
                                <asp:CheckBox ID="chkDone" Text="" CssClass="standardcheckbox"
                                    AutoPostBack="true" runat="server" TabIndex="101" />
                                <span>Done</span>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblDate3" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3" style="text-align: left; top: 0px; left: 0px;">
                                <asp:TextBox ID="txtDateKYCver" runat="server" Enabled="false" CssClass="form-control" MaxLength="10"></asp:TextBox>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblEmpName" Text="" runat="server" CssClass="control-label">
                                </asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtEmpName" MaxLength="15"
                                    Enabled="False" TabIndex="103" />
                                <br />
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblEmpCode" Text="Employee Code" runat="server" CssClass="control-label">
                                </asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtEmpCode" MaxLength="15"
                                    Enabled="False" TabIndex="104" />
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblEmpDesignation" Text="Employee Designation" runat="server" CssClass="control-label">
                                </asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtEmpDesignation" MaxLength="15"
                                    Enabled="false" TabIndex="105" />
                                <%--<br/>--%>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblEmpBranch" Text="Employee Branch" runat="server" CssClass="control-label">
                                </asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtEmpBranch" MaxLength="15"
                                    Enabled="false" TabIndex="106" />
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-12" style="text-align: center">
                                <asp:Label ID="lblInsDtls" Style='text-align: center' CssClass="control-label" Font-Bold="true"
                                    Text="INSTITUTION DETAILS" runat="server" />
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblInsName" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox CssClass="form-control" onchange="setDateFormat('txtDate3')" runat="server"
                                    ID="txtInsName" MaxLength="15" Enabled="false" TabIndex="107" />
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblInsCode" Text="" runat="server" CssClass="control-label"></asp:Label>
                                <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtInsCode" MaxLength="15"
                                    Enabled="false" TabIndex="108" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdnRegRefNo" runat="server" />
    <%-- APPLICATION DECLARATION End --%>

    <%-- APPLICATION Action Button start --%>
    <div class="container" width="100%">
        <div class="row">
            <%--style="margin-top: 12px;"--%>
            <center>
                <div class="col-sm-12">
                    <asp:Button ID="btnUpdate" runat="server" CssClass="btn-animated bg-green"  Text="Approve" OnClick="btnUpdate_Click">
                        </asp:Button><%--OnClick="btnUpdate_Click"--%> 
             <asp:Button ID="btnReject" runat="server" CssClass="btn-animated bg-horrible"   Text="Reject" OnClick="btnReject_Click">
                        </asp:Button><%--OnClick="btnReject_Click"--%>
              
        <div id="divloader" runat="server" style="display:none;">
                <img id="Img1" alt="" src="~/images/spinner.gif" runat="server" /> Loading...
        </div>
                </div>
            </center>
        </div>
    </div>
    <%-- APPLICATION Action Button End --%>
    <input id="hdnUpdate" type="hidden" runat="server" />
    <asp:HiddenField ID="hdnUserId" runat="server" />
    <asp:HiddenField ID="hdnmissingfield" runat="server" />

    <%-- Modal Pop UP for Related Person start --%>
    <div class="modal" id="myModalRaise" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true" style="padding-top: 0px;">
        <div class="modal-dialog" style="padding-top: 0px; margin-top: 2px; width: 95%;">
            <div class="modal-content">
                <div class="modal-header" style="margin-top: -10px; margin-bottom: -20px; padding-bottom: 6px ! important;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title" id="myModalLabel">CKYC Related Person Details</h4>
                </div>
                <div class="modal-body">
                    <iframe id="iframeCFR" src="" width="100%" height="505" frameborder="0" allowtransparency="true"></iframe>
                </div>
                <div class="modal-footer">
                    <div style="text-align: center;">
                        <asp:LinkButton ID="LinkButton1" TabIndex="43" runat="server" CssClass="btn-animated bg-horrible"
                            data-dismiss="modal" aria-hidden="true">
                                    <span class="glyphicon glyphicon-remove" style="color:White"> </span> Cancel
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <%-- Modal Pop UP for Related Person end --%>


    <%-- Modal Pop UP for Controlling Person start --%>
    <div class="modal" id="myModalRaise1" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true" style="padding-top: 0px;">
        <div class="modal-dialog" style="padding-top: 0px; margin-top: 2px; width: 95%;">
            <div class="modal-content">
                <div class="modal-header" style="margin-top: -10px; margin-bottom: -20px; padding-bottom: 6px ! important;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title" id="myModalLabel">CKYC Controlling Person Details</h4>
                </div>
                <div class="modal-body">
                    <iframe id="iframeCCFR" src="" width="100%" height="505" frameborder="0" allowtransparency="true"></iframe>
                </div>
                <div class="modal-footer">
                    <div style="text-align: center;">
                        <asp:LinkButton ID="lnkRaise" TabIndex="43" runat="server" CssClass="btn-animated bg-horrible"
                            data-dismiss="modal" aria-hidden="true">
                                    <span class="glyphicon glyphicon-remove" style="color:White"> </span> Cancel
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <%-- Modal Pop UP for Controlling Person end --%>

    <%-- Modal Pop UP for Related Person QC --%>
    <div class="modal" id="RltdPrsnQC" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true" style="padding-top: 0px;">
        <div class="modal-dialog" style="padding-top: 0px; margin-top: 2px; width: 95%;">
            <div class="modal-content">
                <div class="modal-header" style="margin-top: -10px; margin-bottom: -20px; padding-bottom: 6px ! important;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title" id="myModalLabel1">CKYC Related Person Details</h4>
                </div>
                <div class="modal-body">
                    <iframe id="iframeCFR1" src="" width="100%" height="505" frameborder="0" allowtransparency="true"></iframe>
                </div>
                <div class="modal-footer">
                    <div style="text-align: center;">
                        <asp:LinkButton ID="LinkButton3" TabIndex="43" runat="server" CssClass="btn-animated bg-horrible"
                            data-dismiss="modal" aria-hidden="true">
                                    <span class="glyphicon glyphicon-remove" style="color:White"> </span> Cancel
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <%-- Modal Pop UP for Related Person QC --%>

    <%-- Modal Pop UP for Related Person QC --%>
    <div class="modal" id="CntrlPrsnQC" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true" style="padding-top: 0px;">
        <div class="modal-dialog" style="padding-top: 0px; margin-top: 2px; width: 95%;">
            <div class="modal-content">
                <div class="modal-header" style="margin-top: -10px; margin-bottom: -20px; padding-bottom: 6px ! important;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title" id="myModalLabel2">CKYC Controlling Person Details</h4>
                </div>
                <div class="modal-body">
                    <iframe id="iframeCFR2" src="" width="100%" height="505" frameborder="0" allowtransparency="true"></iframe>
                </div>
                <div class="modal-footer">
                    <div style="text-align: center;">
                        <asp:LinkButton ID="LinkButton4" TabIndex="43" runat="server" CssClass="btn-animated bg-horrible"
                            data-dismiss="modal" aria-hidden="true">
                                    <span class="glyphicon glyphicon-remove" style="color:White"> </span> Cancel
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <%-- Modal Pop UP for Related Person QC --%>
</asp:Content>
