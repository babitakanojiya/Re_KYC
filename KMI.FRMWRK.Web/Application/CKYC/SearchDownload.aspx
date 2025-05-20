<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Empty.Master" CodeBehind="SearchDownload.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.CKYC.SearchDownload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/bundles/CKYCValidationjs") %>
    <%: Styles.Render("~/bundles/CKYCcss") %>

    <script lang="javascript" type="text/javascript">

        function popup() {
            var varFooter = '<center><button type="button" class="btn btn-success" data-dismiss="modal" style="margin-top: -6px;"><span class="glyphicon glyphicon-ok" style="color: White;"></span> OK </button></center>';
            showModal('#myModal', 'Alert', 'alert-warning', varFooter, '', 'Document uploaded successfully.. Please proceed with Quality Approval process ');
        }
    </script>

    <script type="text/javascript">

        function callCalender(id) {
            debugger;
            if (id == "txtDOB") {
                var dateArr = $("#<%=txtDOB.ClientID%>").val().split('-');
                $("#<%= txtDOB.ClientID%>").datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd-mm-yy', yearRange: '1945:' + (new Date).getFullYear() });
                $.datepicker.initialized = true;
                $("#<%= txtDOB.ClientID%>").focus();
             }

         }
         function ValidateDOB(date) {
             debugger;
             var dateObj = date.split('-');
             if (!getYearDiff(new Date(dateObj[2], dateObj[1] - 1, dateObj[0]))) {
                 popup("DOB should not be less then 18 years");
                 document.getElementById("<%= txtDOB.ClientID %>").value = "";
             }
         }
         $(document).ready(function () {
             debugger;
             if (EmptyPagePlaceholder_hdnSearch.value != "EmptyPagePlaceholder_Search") {
                 checktab(document.getElementById('<%= Search.ClientID %>'), menu1);
            }
            else {
                checktab(document.getElementById('<%= Download.ClientID %>'), menu2);
            }
        });

        function ShowProgressBar(Msg) {
            debugger;
            var Msg = Msg

            document.getElementById('dvProgressBar').style.display = "block";
            document.getElementById('EmptyPagePlaceholder_lblMsg').innerHTML = Msg;
            setTimeout(function () { HideProgressBar(); }, 5000);
        }
        function HideProgressBar() {
            debugger;

            document.getElementById('dvProgressBar').style.display = "none";

        }

        function checktab(id, menu) {
            debugger;
            var tab;
            if (id == 'EmptyPagePlaceholder_Search' || id == 'EmptyPagePlaceholder_Download') {
                tab = id;
            }
            else {
                tab = id.id;
            } // gets text contents of clicked li
            if (tab == 'EmptyPagePlaceholder_Search') {
                document.getElementById('EmptyPagePlaceholder_Download').classList.remove("active");
                document.getElementById('EmptyPagePlaceholder_Search').classList.add("active");
                document.getElementById('menu1').classList.add('tab-pane', 'fade', 'active');
                document.getElementById('menu2').classList.remove('tab-pane', 'fade');
                //document.getElementById('EmptyPagePlaceholder_hdnSearch').value ="Search"
            }
            if (tab == 'EmptyPagePlaceholder_Download') {
                document.getElementById('EmptyPagePlaceholder_Download').classList.add("active");
                document.getElementById('EmptyPagePlaceholder_Search').classList.remove("active");
                document.getElementById('menu2').classList.add('tab-pane', 'fade', 'active');
                document.getElementById('menu1').classList.remove('tab-pane', 'fade');
                //document.getElementById('EmptyPagePlaceholder_hdnDownload').value ="Download"
            }
            if (tab == 'EmptyPagePlaceholder_Search') {
                document.getElementById('EmptyPagePlaceholder_div2').style.display = 'block';
                document.getElementById('EmptyPagePlaceholder_div4').style.display = 'none';
                document.getElementById('EmptyPagePlaceholder_hdnSearch').value ="Search"
            }
            else if (tab == 'EmptyPagePlaceholder_Download') {
                var flag = "Relatedtab"
                document.getElementById('EmptyPagePlaceholder_div2').style.display = 'none';
                document.getElementById('EmptyPagePlaceholder_div4').style.display = 'block';
                document.getElementById('EmptyPagePlaceholder_Div1').style.display = 'none';
                document.getElementById('EmptyPagePlaceholder_hdnSearch').value ="Download"
            }

        };

       <%-- function searchContent() {
        var currentTab = document.getElementById('<%= hdnCurrentTab.ClientID %>').value;

        // Perform different actions based on the current tab
        if (currentTab === 'Search') {
            // Perform search for Tab 1
            alert('Searching content for Tab 1...');
        } else if (currentTab === 'Download') {
            // Perform search for Tab 2
            alert('Searching content for Tab 2...');
        }
        }--%>
    </script>



    <style>
        .panel-success {
            border-color: #00b4bf;
        }

        .nav-tabs > li.active > a > span {
            padding: 10px 15px;
            font-weight: bold;
            color: #fff;
            background-color: darkblue;
        }

        .nav-tabs > li > a {
            border-radius: 0px !important;
            padding: 10px 10px;
            background-color: #fff;
            border: 1px solid #00b4bf;
            border-bottom-color: transparent;
        }


            .nav-tabs > li > a > span {
                font-weight: bold;
                color: #000;
            }

        .nav-tabs > li.active > a > span {
            padding: 10px 15px;
            font-weight: bold;
            color: #fff;
            background-color: darkblue;
        }

        .tab-content {
            border-left: 1px solid #00b4bf;
            border-right: 1px solid #00b4bf;
            border: 1px solid #00b4bf;
            padding: 0px !important;
        }

        .nav-tabs > li.active > a, .nav-tabs > li.active > a:focus, .nav-tabs > li.active > a:hover {
            color: #555;
            cursor: default;
            background-color: darkblue !important;
            border: 1px solid darkblue;
            border-bottom-color: darkblue;
        }
    </style>

    <style>
        .nav-tabs > li.active > a > span {
            padding: 10px 15px;
            font-weight: bold;
            color: #fff;
            background-color: darkblue;
        }

        .nav-tabs > li > a {
            border-radius: 0px !important;
            padding: 10px 10px;
            background-color: #fff;
            border: 1px solid #00b4bf;
            border-bottom-color: transparent;
        }


            .nav-tabs > li > a > span {
                font-weight: bold;
                color: #000;
            }

        .nav-tabs > li.active > a > span {
            padding: 10px 15px;
            font-weight: bold;
            color: #fff;
            background-color: darkblue;
        }

        .tab-content {
            border-left: 1px solid #00b4bf;
            border-right: 1px solid #00b4bf;
            border: 1px solid #00b4bf;
            padding: 0px !important;
        }

        .nav-tabs > li.active > a, .nav-tabs > li.active > a:focus, .nav-tabs > li.active > a:hover {
            color: #555;
            cursor: default;
            background-color: darkblue !important;
            border: 1px solid darkblue;
            border-bottom-color: darkblue;
        }
    </style>
    <style type="text/css">
        .pad {
            text-align: center !important;
        }
		.card {
			position: relative;
			display: flex;
			flex-direction: column;
			min-width: 0;
			word-wrap: break-word;
			background-color: #fff;
			background-clip: border-box;
			border: 1px solid rgba(0,0,0,.125);
			border-radius: 0.25rem;
		}
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="container" style="margin-top: 0px; width: 100%;">
                <div class="page-container" style="margin-top: 0px;">
                    <div id="trDgViewDtl" runat="server" class="page-container" style="margin-top: 0px;">
                        <div class="panel panel-success" style='margin-right: 26px; margin-left: 26px;'>
                            <div id="tblupload" runat="server" class="panel-heading" onclick="showHideDiv('div9','btnpnlcfrdtls');return false;">

                                <div class="row">
                                    <div class="col-sm-3" style="text-align: left">
                                        <span class="glyphicon glyphicon-menu-hamburger" style=""></span>
                                        <asp:Label ID="lblCanddoc" runat="server" Text="SEARCH & DOWNLOAD" CssClass="control-label"></asp:Label>

                                    </div>
                                </div>
                            </div>

                            <div id="div9" class="panel-body" style="margin-left: 2%; margin-right: 2%; margin-top: 0.5%">
                                <asp:Button ID="btnend" runat="server" OnClick="btnend_Click" Style="display: none" OnClientClick="ShowProgressBar('Loading Data..Please wait')" />
                                <div class="panel-body">
                                    <ul class="nav nav-tabs" id="myList" runat="server">
                                        <li class="active" id="Search" runat="server" onclick="checktab(this,'menu1')">
                                            <a data-toggle="tab" href="#menu1">
                                                <span id="LItab" style="font-weight: bold" runat="server">SEARCH</span>
                                            </a>
                                            </a>
                                        </li>
                                        <li id="Download" runat="server" onclick="checktab(this,'menu2')">
                                            <a data-toggle="tab" href="#menu2">
                                                <span style="font-weight: bold">DOWNLOAD</span>
                                                <asp:Label ID="lblcount" runat="server"></asp:Label>
                                            </a>
                                        </li>
                                        <div style="text-align: center; display: none">
                                            <asp:Label ID="lblNote" runat="server" CssClass="control-label" Text="NOTE: All Documents to be Uploaded/Reuploaded should be in TIFF/JPEG/JPG/PDF format"
                                                ForeColor="red"></asp:Label>
                                        </div>
                                    </ul>
                                    <div class="tab-content">
                                        <div id="menu1" class="tab-pane fade active in">
                                            <div id="div2" class="panel-body" runat="server" style="overflow: auto;">
                                                <div class="row" id="srchby" style="margin-bottom: 5px" runat="server">
                                                    <div class="col-sm-3" style="text-align: left">
                                                        <asp:Label ID="lblSrchBy" runat="server" CssClass="control-label" Text="Search By"
                                                            Font-Bold="False"></asp:Label>
                                                        <span id="Span1" runat="server" style="color: red">*</span>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="ddlSearchby" runat="server" CssClass="form-control"
                                                            AutoPostBack="true" TabIndex="2" OnSelectedIndexChanged="ddlSearchby_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-3" style="text-align: left; display: none">
                                                        <asp:Label ID="lblCndName" CssClass="control-label" runat="server" Text="Name"
                                                            Font-Bold="False"></asp:Label>
                                                        <span id="Span2" runat="server" style="color: red">*</span>
                                                    </div>
                                                    <div class="col-sm-3" style="display: none">
                                                        <asp:TextBox ID="lblAdvNameValue" runat="server" CssClass="form-control" MaxLength="20"
                                                            Enabled="false"> 
                                                        </asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row" id="prfofidnty" style="margin-bottom: 5px; display: none" runat="server">
                                                    <div class="col-sm-3" id="prfid" style="text-align: left">
                                                        <asp:Label ID="lblProofofidn" Text="Proof Of Identity" CssClass="control-label" runat="server"
                                                            Font-Bold="False"></asp:Label>
                                                        <span id="Span3" runat="server" style="color: red">*</span>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="ddlProofofidn" runat="server" CssClass="form-control"
                                                            AutoPostBack="true" TabIndex="2">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-3" style="text-align: left">
                                                        <asp:Label ID="Lblidntynum" Text="Identity Number" CssClass="control-label" runat="server"
                                                            Font-Bold="False"></asp:Label>
                                                        <span id="Span4" runat="server" style="color: red">*</span>
                                                    </div>
                                                    <div class="col-sm-3" id="txtidtnum" style="display: none" runat="server">
                                                        <div class="col-sm-4" style="padding: 0px;">
                                                            <asp:TextBox ID="txtidentynum" CssClass="form-control" Enabled="false" placeholder="XXXX" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-4" style="padding: 0px; padding-left: 6px;">
                                                            <asp:TextBox ID="TextBox1"  placeholder="XXXX" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-4" style="padding: 0px; padding-left: 6px;">
                                                            <asp:TextBox ID="TextBox2" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3" id="dividentynum" style="display:none" runat="server">
                                                        <asp:TextBox ID="txtidnummm" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row" id="applcntnme" style="margin-bottom: 5px; display: none" runat="server">
                                                    <div class="col-sm-3" style="text-align: left">
                                                        <asp:Label ID="lblAppfullnme" Text="Applicant's Full Name" CssClass="control-label" runat="server"
                                                            Font-Bold="False"></asp:Label>
                                                        <span id="Span5" runat="server" style="color: red">*</span>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtAppfullname" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-3" style="text-align: left">
                                                        <asp:Label ID="lblDOB" Text="DOB" CssClass="control-label" runat="server" Font-Bold="False"></asp:Label>
                                                        <span id="Span6" runat="server" style="color: red">*</span>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtDOB" CssClass="form-control" placeholder="dd-mm-yyyy"
                                                                onchange="ValidateDOB(this.value);" runat="server"></asp:TextBox>
                                                            <div class="input-group-btn">
                                                                <div class="btn btn-primary btn-lg-kmi" onclick="callCalender('txtDOB')">
                                                                    <span class="glyphicon glyphicon-calendar BtnGlyphicon"></span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" id="gender" style="margin-bottom: 5px; display: none" runat="server">
                                                    <div class="col-sm-3" style="text-align: left">
                                                        <asp:Label ID="lblGender" Text="Gender" CssClass="control-label" runat="server"
                                                            Font-Bold="False"></asp:Label>
                                                        <span id="Span7" runat="server" style="color: red">*</span>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control"
                                                            AutoPostBack="true" TabIndex="2">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="menu2" class="tab-pane fade active in">
                                                <div id="div4" class="panel-body" runat="server" style="overflow: auto;">
                                                    <div class="row" id="ckyccno" style="margin-bottom: 5px;" runat="server">
                                                        <div class="col-sm-3" style="text-align: left">
                                                            <asp:Label ID="lblckycno" Text="CKYC No." CssClass="control-label" runat="server"
                                                                Font-Bold="False"></asp:Label>
                                                            <span id="Span8" runat="server" style="color: red">*</span>
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <asp:TextBox ID="txtckyno" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-3" style="text-align: left">
                                                            <asp:Label ID="lblAuttype" Text="Authentication Factor Type" CssClass="control-label" runat="server"
                                                                Font-Bold="False"></asp:Label>
                                                            <span id="Span9" runat="server" style="color: red">*</span>
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <asp:DropDownList ID="ddlauthtype" runat="server" CssClass="form-control"
                                                                AutoPostBack="false" TabIndex="2">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="row" id="auth" style="margin-bottom: 5px;" runat="server">
                                                        <div class="col-sm-3" style="text-align: left">
                                                            <asp:Label ID="lblauthfctor" Text="Authentication Factor" CssClass="control-label" runat="server"
                                                                Font-Bold="False"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <asp:TextBox ID="txtauthfactor" CssClass="form-control"
                                                                runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 12px;" id="divButtons" runat="server">
                                        <div class="col-sm-12" align="center">
                                            <%--    <asp:LinkButton ID="Btncrop" runat="server"  CssClass="btn btn-primary" Text="CROP" visible="false"
                                    CausesValidation="false"  TabIndex="43"></asp:LinkButton>--%>
                                            <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn-animated bg-green" OnClick="btnSubmit_Click" Text="Search" CausesValidation="false" TabIndex="32" OnClientClick="ShowProgressBar('Document submission process is in progress..Please wait')">  </asp:LinkButton>
                                            <asp:LinkButton ID="btnCancel" OnClick="btnCancel_Click" TabIndex="43" runat="server" Text="Cancel"
                                                CssClass="btn-animated bg-horrible">
                            
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <asp:HiddenField ID="hdnDownload" runat="server" />
                                    <asp:HiddenField ID="hdnSearch" runat="server" />
                                    <asp:HiddenField ID="hdnCurrentTab" runat="server" />

                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="Div1" runat="server" class="page-container" style="margin-top: 0px;display:none;">
                        <div class="panel panel-success" style='margin-right: 26px; margin-left: 26px;'>
                            <div runat="server" id="trtitle" class="panel-heading" onclick="showHideDiv('trgridsponsorship','span1');return false;">
                                <div class="row" id="trDetails" runat="server">
                                    <div class="col-sm-10" style="text-align: left">
                                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                                        <asp:Label ID="lblprospectsearch" runat="server" Text="CKYC Search Results"></asp:Label>
                                    </div>
                                    <div class="col-sm-2">
                                        <span id="span1" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                                    </div>
                                </div>
                            </div>
                            <div id="trgridsponsorship" runat="server" class="panel-body">
								<div id="divresult" class="card" style="width: 35%;height: 450px;margin-left:29vw;">
									<div class="row" style="text-align:center;background-color:#00b4bf;width: 100%;margin-left:0px;height:7%">
										<asp:Label ID="Label1" runat="server" Text="Applicant Information" style="color:white;font-size:14px"></asp:Label>
									</div>
									<div class="row" style="text-align:center">
										<asp:Image Id="Img1" runat="server" ImageUrl="" Width="20%" style="border:1px solid;"/>
									</div>
									<div class="row" style="text-align:center;padding-top:10px;margin-left:40px">
                                      <div class="col-sm-6" style="text-align: left">
                                        <asp:Label ID="Label2" Text="CKYC Number" CssClass="control-label" runat="server"
                                           Font-Bold="True"></asp:Label>           
                                        </div>
                                      <div class="col-sm-6" style="text-align: left">
                                        <asp:Label ID="lblCkycNoRes" Text="" CssClass="control-label" runat="server"
                                           Font-Bold="False"></asp:Label>           
                                        </div>
									</div>
									<div class="row" style="text-align:center;margin-left:40px">
                                      <div class="col-sm-6" style="text-align: left">
                                        <asp:Label ID="Label4" Text="Full Name" CssClass="control-label" runat="server"
                                           Font-Bold="True"></asp:Label>           
                                        </div>
                                      <div class="col-sm-6" style="text-align: left">
                                        <asp:Label ID="lblFullNm" Text="" CssClass="control-label" runat="server"
                                           Font-Bold="False"></asp:Label>           
                                        </div>									
									</div>
									<div class="row" style="text-align:center;margin-left:40px">
                                      <div class="col-sm-6" style="text-align: left">
                                        <asp:Label ID="Label6" Text="Father's Name" CssClass="control-label" runat="server"
                                           Font-Bold="True"></asp:Label>           
                                        </div>
                                      <div class="col-sm-6" style="text-align: left">
                                        <asp:Label ID="lblFthNm" Text="" CssClass="control-label" runat="server"
                                           Font-Bold="False"></asp:Label>           
                                        </div>								
									</div>
									<div class="row" style="text-align:center;margin-left:40px">
                                      <div class="col-sm-6" style="text-align: left">
                                        <asp:Label ID="Label8" Text="KYC Date" CssClass="control-label" runat="server"
                                           Font-Bold="True"></asp:Label>           
                                        </div>
                                      <div class="col-sm-6" style="text-align: left">
                                        <asp:Label ID="lblKycDt" Text="" CssClass="control-label" runat="server"
                                           Font-Bold="False"></asp:Label>           
                                        </div>								
									</div>
									<div class="row" style="text-align:center;margin-left:40px">
                                      <div class="col-sm-6" style="text-align: left">
                                        <asp:Label ID="Label10" Text="Age" CssClass="control-label" runat="server"
                                           Font-Bold="True"></asp:Label>           
                                        </div>
                                      <div class="col-sm-6" style="text-align: left">
                                        <asp:Label ID="lblAge" Text="" CssClass="control-label" runat="server"
                                           Font-Bold="False"></asp:Label>           
                                        </div>							
									</div>
									<div class="row" style="text-align:center;margin-left:40px">
                                      <div class="col-sm-6" style="text-align: left">
                                        <asp:Label ID="Label3" Text="Identity Document Provided" CssClass="control-label" runat="server"
                                           Font-Bold="True"></asp:Label>           
                                        </div>
                                      <div id="DivDocC" class="col-sm-6" runat="server" style="text-align: left">
<%--                                        <asp:Label ID="lblIdDoc" Text="" CssClass="control-label" runat="server"
                                           Font-Bold="False"></asp:Label>  --%>         
                                        </div>							
									</div>
									<div class="row" style="text-align:center;margin-left:40px">
                                      <div class="col-sm-6" style="text-align: left">
                                        <asp:Label ID="Label12" Text="Remark" CssClass="control-label" runat="server"
                                           Font-Bold="True"></asp:Label>           
                                        </div>
                                      <div class="col-sm-6" style="text-align: left">
                                        <asp:Label ID="lblRmk" Text="" CssClass="control-label" runat="server"
                                           Font-Bold="False"></asp:Label>           
                                        </div>									
									</div>
								</div>
<%--                                <asp:GridView ID="dgView" runat="server" AllowSorting="True" CssClass="footable" Width="100%"
                                    AutoGenerateColumns="False" PageSize="100" AllowPaging="true" CellPadding="1">
                                    <HeaderStyle HorizontalAlign="Center" BackColor="#dce9f9" />
                                    <FooterStyle CssClass="GridViewFooter" />
                                    <RowStyle CssClass="GridViewRow" />

                                    <SelectedRowStyle CssClass="GridViewSelectedRow" />
                                    <AlternatingRowStyle CssClass="GridViewAlternateRow"></AlternatingRowStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="CKYC NO" ItemStyle-Width="20%" HeaderStyle-CssClass="pad">
                                            <ItemTemplate>
												<asp:Label ID="lblCkycNo" runat="server" Text='<%# Bind("CKYC_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="12%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NAME" ItemStyle-Width="20%" HeaderStyle-CssClass="pad">
                                            <ItemTemplate>
												<asp:Label ID="lblName" runat="server" Text='<%# Bind("NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FATHERS NAME" ItemStyle-Width="20%" HeaderStyle-CssClass="pad">
                                            <ItemTemplate>
												<asp:Label ID="lblFathNam" runat="server" Text='<%# Bind("FATHERS_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="AGE" ItemStyle-Width="15%" HeaderStyle-CssClass="pad">
                                            <ItemTemplate>
												<asp:Label ID="lblAge" runat="server" Text='<%# Bind("AGE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="pad" Width="8%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IMAGE TYPE" ItemStyle-Width="10%" HeaderStyle-CssClass="pad">
                                            <ItemTemplate>
												<asp:Label ID="lblImgTyp" runat="server" Text='<%# Bind("IMAGE_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="pad" Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PHOTO" ItemStyle-Width="20%" HeaderStyle-CssClass="pad">
                                            <ItemTemplate>
												<asp:Image ID="img" runat="server" ImageUrl='<%# Eval("PHOTO") %>' Width="50%"></asp:Image>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="12%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="KYC DATE" ItemStyle-Width="20%" HeaderStyle-CssClass="pad">
                                            <ItemTemplate>
												<asp:Label ID="lblKycdt" runat="server" Text='<%# Bind("KYC_DATE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UPDATED DATE" ItemStyle-Width="20%" HeaderStyle-CssClass="pad">
                                            <ItemTemplate>
												<asp:Label ID="lblUpddt" runat="server" Text='<%# Bind("UPDATED_DATE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="18%" HeaderText="REMARKS" HeaderStyle-CssClass="pad">
                                            <ItemTemplate>
												<asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("REMARKS") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="7%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>--%>
                                <br />

                                <br />
                                <div class="col-sm-3" style="text-align: left" style="display: none">
                                    <asp:Label ID="lblPageInfo" runat="server" Visible="false"></asp:Label>
                                </div>
                                <div id="divloadergrid" class="col-sm-12" runat="server" style="display: none;">
                                    <caption>
                                        <img id="Img2" alt="" src="~/images/spinner.gif" runat="server" />
                                        Loading...
                                    </caption>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
