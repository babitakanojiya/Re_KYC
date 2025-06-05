<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="SearchDataForCkyc.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.CKYC.SearchDataForCkyc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/bundles/CKYCValidationjs") %>
    <%: Styles.Render("~/bundles/CKYCcss") %>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">

    <style type="text/css">
        .container {
            width: 1300px !important;
        }
        /*AP*/
        a {
            color: rgba(21, 62, 60, 0.93);
        }
        /*AP*/
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

    <style type="text/css">
        .pad {
            text-align: center !important;
        }
    </style>

    <style type="text/css">
        .loader {
            position: fixed;
            width: 100%;
            left: 0;
            right: 0;
            top: 0;
            bottom: 0;
            background-color: rgba(255,255,255,0.7);
            z-index: 9999;
            margin: auto;
            padding: 10px;
            /*display:none;*/
        }

            .loader::after {
                /*content:'';*/
                /*display:block;*/
                position: absolute;
                left: 0%;
                top: 0%;
                width: 100vw;
                height: 50vh;
                border-style: solid;
                border-color: black;
                border-top-color: transparent;
                border-width: 4px;
                border-radius: 50%;
                /*-webkit-animation: spin .8s linear infinite;
    animation: spin .8s linear infinite;*/
            }
        /*  .imglder
        {
            margin-bottom:10%;
        }*/
        .panel-body {
            padding:5px !important;
        }

    </style>

    <script type="text/javascript">


        function zoomImage(obj) {
            $(obj).elevateZoom({
                cursor: 'pointer',
                imageCrossfade: true,
                loadingIcon: 'loading.gif',
                scrollZoom: true,
                zoomWindowPosition: 12,
                //zoomWindowPosition: 1,
                zoomWindowOffsetX: 5
            });
        }
        $(function () {

        });




        function funRedirect() {
            document.getElementById('EmptyPagePlaceholder_divloaderqc').style.display = 'block'
            document.getElementById('divloaderqc').style.top = '264px';
        }

        //function OpenZipFilePage() {
        //    debugger;
        //    var modal = document.getElementById('myModalRaise_NEw');
        //    var modaliframe = document.getElementById("iframeCFR_New");
        //    modaliframe.src = "../../Application/CKYC/ZipFileDetail.aspx?Status=Zip";
        //    $('#myModalRaise_NEw').modal();
        //}

        function OpenQCPage(refno, FlagPageTyp) {
            debugger;
            var modal = document.getElementById('myModalRaise');
            var modaliframe = document.getElementById("iframeCFR");
            modaliframe.src = "../../Application/CKYC/CKYCQC.aspx?Status=QC&refno=" + refno + "&PageFlag=" + FlagPageTyp;
            $('#myModalRaise').modal();

            HideProgressBar();  //Added by Megha Bhave 26.03.2021
        }

        function OpenConstTypeQCPage(refno) {
            debugger;
            var modal = document.getElementById('myModalRaise');
            var modaliframe = document.getElementById("iframeCFR");
            modaliframe.src = "../../Application/CKYC/CKYCLegalEntityQC.aspx?Status=QC&refno=" + refno;
            $('#myModalRaise').modal();

        }

        function AlertMsg(msg) {
            showModal('#myModal', 'Alert', 'alert-warning', '', '', msg);
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

    </script>
    <script type="text/javascript">
        function callCalender(controlId) {
            var textbox = document.getElementById(controlId);
            if (textbox) {
                $('#' + controlId).datepicker({
                    dateFormat: 'dd-mm-yy',
                    changeMonth: true,
                    changeYear: true,
                    yearRange: "1900:+0",
                    showButtonPanel: true
                }).datepicker("show");
            }
        }
    </script>
  <script type="text/javascript">
      //function openPopup(url) {
      //    window.open(url, '_blank', 'width=700,height=300,resizable=yes,scrollbars=yes');
      //}
      function openPopup(url) {
          var width = 700;
          var height = 300;

          // Calculate center position
          var left = (window.screen.width / 2) - (width / 2);
          var top = (window.screen.height / 2) - (height / 2);

          var features =
              'width=' + width +
              ',height=' + height +
              ',resizable=yes,scrollbars=yes' +
              ',top=' + top +
              ',left=' + left;

          window.open(url, '_blank', features);
      }

  </script>




    <asp:ScriptManager ID="CKYCSearch" runat="server">
    </asp:ScriptManager>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div>
                <configuration>
  <system.web>
    <httpruntime maxRequestLength="102400" />    
  </system.web>
</configuration>


            </div>
            <div class="container" style="margin-top: 0px; width: 100%;">
                <div class="page-container" style="margin-top: 0px;">
                    <div class="panel  panel-success" style="margin-left: 2%; margin-right: 2%; margin-top: 0.5%">

                        <div class="panel-heading" onclick="showHideDiv('trSearchDetails','btnToggle');return false;">
                            <div class="row">
                                <div class="col-sm-10" style="text-align: left">
                                    <span class="glyphicon glyphicon-menu-hamburger"></span>
                                    <asp:Label ID="lblTitle" runat="server" Font-Bold="False" Text="CKYC Search View Data"></asp:Label>
                                </div>
                                <div class="col-sm-2">
                                    <span id="btnToggle" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                                </div>
                            </div>
                        </div>

                        <div id="trSearchDetails" class="panel-body">
                            <div id="divSrvcReqReport1" style="display: block;" class="panel-body panel-collapse collapse in">
                                <div id="div9" class="panel-body" style="margin-left: 2%; margin-right: 2%; margin-top: 0.5%">
    <asp:Button ID="btnend" runat="server" OnClick="btnend_Click" Style="display: none" OnClientClick="ShowProgressBar('Loading Data..Please wait')" />
    <div class="panel-body">
        <%--<ul class="nav nav-tabs" id="myList" runat="server">
            <li class="active" id="Search" runat="server" onclick="checktab(this,'menu1')">
                <a data-toggle="tab" href="#menu1" style="padding:10px">
                    <span id="LItab" style="font-weight: bold" runat="server">SEARCH</span>
                </a>
                </a>
            </li>
            
            <div style="text-align: center; display: none">
                <asp:Label ID="lblNote" runat="server" CssClass="control-label" Text="NOTE: All Documents to be Uploaded/Reuploaded should be in TIFF/JPEG/JPG/PDF format"
                    ForeColor="red"></asp:Label>
            </div>
        </ul>--%>
        <div class="tab-content">
            <div id="menu1" class="tab-pane fade active in">
                <div id="div2" class="panel-body" runat="server" style="padding:12px !important;">
                    <div class="row" id="srchby" style="margin-bottom: 9px" runat="server">
                        <div class="col-sm-3" style="text-align: left">
                            <asp:Label ID="lblSrchBy" runat="server" CssClass="control-label" Text="Search By"
                                Font-Bold="False"></asp:Label>
                            <span id="Span2" runat="server" style="color: red">*</span>
                        </div>
                        <div class="col-sm-3">
                            <asp:DropDownList ID="ddlSearchby" runat="server" CssClass="form-control"
                                AutoPostBack="true" TabIndex="2" OnSelectedIndexChanged="ddlSearchby_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-3" style="text-align: left; display: none">
                            <asp:Label ID="lblCndName" CssClass="control-label" runat="server" Text="Name"
                                Font-Bold="False"></asp:Label>
                            <span id="Span3" runat="server" style="color: red">*</span>
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
                            <span id="Span4" runat="server" style="color: red">*</span>
                        </div>
                        <div class="col-sm-3">
                            <asp:DropDownList ID="ddlProofofidn" runat="server" CssClass="form-control"
                                AutoPostBack="true" TabIndex="2">
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-3" style="text-align: left">
                            <asp:Label ID="Lblidntynum" Text="Identity Number" CssClass="control-label" runat="server"
                                Font-Bold="False"></asp:Label>
                            <span id="Span5" runat="server" style="color: red">*</span>
                        </div>
                        <div class="col-sm-3" id="txtidtnum" style="display: none" runat="server">
                            <div class="col-sm-4" style="padding: 0px;">
                                <asp:TextBox ID="txtidentynum" CssClass="form-control" Enabled="false" placeholder="XXXX" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-sm-4" style="padding: 0px; padding-left: 6px;">
                                <asp:TextBox ID="TextBox1"  placeholder="XXXX" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-sm-4" style="padding: 0px; padding-left: 6px;">
                                <asp:TextBox ID="txtaadhar" CssClass="form-control" runat="server"></asp:TextBox>
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
                            <span id="Span6" runat="server" style="color: red">*</span>
                        </div>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtAppfullname" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <%--<div class="col-sm-3" style="text-align: left">
                            <asp:Label ID="lblDOB" Text="DOB" CssClass="control-label" runat="server" Font-Bold="False"></asp:Label>
                            <span id="Span7" runat="server" style="color: red">*</span>
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
                        </div>--%>
                        <div class="col-sm-3" style="text-align: left">
    <asp:Label ID="lblDOB" Text="DOB" CssClass="control-label" runat="server" Font-Bold="False"></asp:Label>
    <span id="Span7" runat="server" style="color: red">*</span>
</div>
<div class="col-sm-3">
    <div class="input-group">
        <asp:TextBox ID="txtDOB" ClientIDMode="Static" CssClass="form-control" placeholder="dd-mm-yyyy"
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
                            <span id="Span8" runat="server" style="color: red">*</span>
                        </div>
                        <div class="col-sm-3">
                            <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control"
                                AutoPostBack="true" TabIndex="2">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div id="menu2" class="tab-pane fade active in" style="display:none"> 
                    <div id="div4" class="panel-body" runat="server" style="overflow: auto;">
                        <div class="row" id="ckyccno" style="margin-bottom: 5px;" runat="server">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblckycno" Text="CKYC No." CssClass="control-label" runat="server"
                                    Font-Bold="False"></asp:Label>
                                <span id="Span9" runat="server" style="color: red">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtckyno" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblAuttype" Text="Authentication Factor Type" CssClass="control-label" runat="server"
                                    Font-Bold="False"></asp:Label>
                                <span id="Span10" runat="server" style="color: red">*</span>
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
        

    </div>
</div>
                                <div style="display:none">
                                <div class="row" style="margin-bottom: 5px">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblbatchno" CssClass="control-label" runat="server" Text="Batch Number"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtbatchno" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblKycNo" CssClass="control-label" runat="server" Text="KYC Number"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtKycNo" runat="server" CssClass="form-control" MaxLength="14"></asp:TextBox>
                                    </div>
                                </div>

                                <div id="trregstrtndt" runat="server" class="row" style="margin-bottom: 5px">

                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblDTsearchFrom" runat="server" CssClass="control-label" Text="Search Date From"> </asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtDTsearchFrom" runat="server" CssClass="form-control"
                                            MaxLength="15" onmousedown="$('#EmptyPagePlaceholder_txtDTsearchFrom').datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd-mm-yy', yearRange: '1945:'+(new Date).getFullYear()  });"></asp:TextBox>
                                    </div>
                                    <div id="Div3" class="col-sm-3" style="text-align: left" runat="server">
                                        <asp:Label ID="lblDTsearchTO" runat="server" Font-Bold="False" Text="Search Date To"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtDTsearchTo" runat="server" CssClass="form-control" MaxLength="15" onmousedown="$('#EmptyPagePlaceholder_txtDTsearchTo').datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd-mm-yy', yearRange: '1945:'+(new Date).getFullYear()  });"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row" style="margin-bottom: 5px">
                                    <%--<div class="col-sm-3" style="text-align: left">
          <asp:Label ID="lblGivenName" runat="server" CssClass="control-label" Text="Applicant Name"></asp:Label>
      </div>
      <div class="col-sm-3">
          <asp:TextBox ID="txtName" runat="server" CssClass="form-control" MaxLength="60" onchange="javascript:this.value=this.value.toUpperCase();"></asp:TextBox>

      </div>--%>
                                    <%--<div id="tdPan" class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblPanno" runat="server" CssClass="control-label" Text="Identity Number"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtPanno" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"></asp:TextBox>
                                    </div>--%>

                                </div>
                                <div id="trShw" runat="server" class="row" style="margin-bottom: 5px">

                                   <%-- <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblShwRecords" runat="server" CssClass="control-label" Text="Records shown per page"></asp:Label>
                                    </div>--%>

                                    <%--<div class="col-sm-3">
          <asp:DropDownList ID="ddlShwRecrds" runat="server" AutoPostBack="true" CssClass="form-control"
              OnSelectedIndexChanged="ddlShwRecrds_SelectedIndexChanged">
          </asp:DropDownList>
      </div>--%>
                                    <%--  <div class="col-sm-3">
                                        <asp:TextBox ID="txtIdno" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>--%>
                                </div>
                                    </div>
                                <div class="row">
                                    <center>
    <div class="col-sm-12" style='margin-top: 2px;'>
        <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click" CssClass="btn-animated bg-green" runat="server"  OnClientClick="ShowProgressBar('Searching..Please wait')">                                   
        </asp:Button>
        <asp:Button ID="btnClear" OnClick="btnClear_Click" CssClass="btn-animated bg-horrible" Text="Clear" runat="server">    </asp:Button>                            
        <asp:Button ID="btnAddnew" runat="server"  CssClass="btn-animated bg-green"   Text="Add New" Visible="false" TabIndex="12"></asp:Button>
             <asp:Button ID="btnexceldata" Text="Export To Excel" OnClick="btnexceldata_Click" CssClass="btn-animated bg-green" runat="server"  OnClientClick="ShowProgressBar('Searching..Please wait')">                                   
        </asp:Button>
    
        
        <asp:Button ID="btnReFresh" runat="server" CssClass="btn btn-primary" Style="display: none;"
            ClientIDMode="Static" />
        <div id="divloader" runat="server" style="display: none;">
            <caption>
                <img id="Img1" alt="" src="~/images/spinner.gif" runat="server" />
                Loading...
            </caption>
        </div>
    </div>
</center>
                                </div>

                                <br />
                                <div id="trnote" runat="server" class="col-sm-12" style="margin-bottom: 5px; text-align: center;">
                                    <asp:Label ID="Label2" runat="server" Text="Note: All dates are in (dd-mm-yyyy)"
                                        ForeColor="Red"></asp:Label>
                                </div>
                                <div id="trRecord" runat="server" visible="false" colspan="6" style="height: 18px; text-align: center;">
                                    <asp:Label ID="lblMessage" runat="server" CssClass="standardlabelErr"></asp:Label>
                                </div>

                            </div>
                        </div>

                    </div>


                    <div id="trDgViewDtl" runat="server" class="page-container" style="margin-top: 0px;">
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
                            <div id="trgridsponsorship" class="panel-body">
                                <asp:GridView ID="dgView" runat="server" AllowSorting="True" CssClass="footable" Width="100%"
                                    AutoGenerateColumns="False" PageSize="100" AllowPaging="true" CellPadding="1" OnPageIndexChanging="dgView_PageIndexChanging"
                                    OnSorting="dgView_Sorting" OnRowCreated="dgView_RowCreated">

                                    <HeaderStyle HorizontalAlign="Center" BackColor="#dce9f9" />
                                    <FooterStyle CssClass="GridViewFooter" />
                                    <RowStyle CssClass="GridViewRow" />

                                    <SelectedRowStyle CssClass="GridViewSelectedRow" />
                                    <AlternatingRowStyle CssClass="GridViewAlternateRow"></AlternatingRowStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr. No." ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="pad">
                                            <ItemTemplate><%#Container.DataItemIndex+1%></ItemTemplate>
                                            <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="6%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Customer Name" ItemStyle-Width="20%" SortExpression="FIRefNo" HeaderStyle-CssClass="pad">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbachno" runat="server" Text='<%# Eval("CustomerName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="12%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Identity Number" ItemStyle-Width="20%" SortExpression="NAME" HeaderStyle-CssClass="pad">
                                            <ItemTemplate>
                                                <asp:Label ID="lblapplicantname" runat="server" Text='<%# Eval("IDENTITY_NUMBER") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Refrence No." ItemStyle-Width="20%" SortExpression="NAME" HeaderStyle-CssClass="pad">
    <ItemTemplate>
        <asp:Label ID="lblapprefno" runat="server" Text='<%# Eval("CKYCReferenceNumber") %>'></asp:Label>
    </ItemTemplate>
    <ItemStyle Width="15%" />
</asp:TemplateField>
                                        <asp:TemplateField HeaderText="CKYC Number" ItemStyle-Width="20%" SortExpression="KYC_NO" HeaderStyle-CssClass="pad">
                                            <ItemTemplate>
                                                <asp:Label ID="lblKYCNo" runat="server" Text='<%# Eval("CKYC_No") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="12%" />
                                        </asp:TemplateField>

                                        <%--<asp:TemplateField HeaderText="Download Data" ItemStyle-Width="20%" HeaderStyle-CssClass="pad">
    <ItemTemplate>
        <asp:HyperLink ID="lnkDownload" runat="server" NavigateUrl='<%# Eval("DownloadUrl") %>' Text="Download" Target="_blank" />
    </ItemTemplate>
    <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="12%" />
</asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Download Data" ItemStyle-Width="20%" HeaderStyle-CssClass="pad">
    <%--<ItemTemplate>
        <asp:HyperLink ID="lnkDownload" runat="server" NavigateUrl='<%# Eval("DownloadUrl") %>' Text="Download" Target="_blank" />
        
    </ItemTemplate>--%>
<ItemTemplate>
    <a href='javascript:void(0);'
       onclick='openPopup("<%# Eval("DownloadUrl").ToString().Replace("'", "\\'") %>"); return false;'>
       Download
    </a>
</ItemTemplate>




    <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="12%" />
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
                                                                    OnSelectedIndexChanged="ddlPageSelectorL_SelectedIndexChanged" CssClass="standardPagerDropdown">
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
                                                                    OnSelectedIndexChanged="ddlPageSelectorR_SelectedIndexChanged" CssClass="standardPagerDropdown">
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
                                <br />
                                <div class="col-sm-3" style="text-align: left">
                                    <asp:Label ID="lblPageInfo" runat="server" Visible="false"></asp:Label>
                                </div>

                            </div>


                        </div>
                    </div>

                    <%--added for data are not found--%>
                    <div>
                        <div class="d-flex justify-content-center p-3" style="margin-top: 15px;display: none;margin-left: 561px;" id="divregistration">


    <asp:Button ID="btnregistration"  Text="Registration" OnClick="btnregistration_Click" CssClass="btn-animated bg-green" runat="server"  OnClientClick="ShowProgressBar('Searching..Please wait')">                                   
    </asp:Button>
    
                    </div>
                    <%--ended for data are not found--%>

                    <table>
                        <tr>
                            <td>
                                <asp:HiddenField ID="hdnbachNo" runat="server" />
                            </td>
                            <td>
                                <asp:HiddenField ID="hdnKycNo" runat="server" />
                            </td>
                            <td></td>
                            <td>
                                <asp:HiddenField ID="hdnTrnReqDt" runat="server" />
                            </td>
                            <td></td>
                            <td>
                                <asp:HiddenField ID="hdnName" runat="server" />
                            </td>


                        </tr>
                    </table>


                </div>
            </div>

            <script type="text/javascript" src="../../Scripts/jquery.min.js"></script>

            <script type="text/javascript" src="../../Scripts/jquery.elevatezoom.js"></script>
                <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css">
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script src="https://code.jquery.com/ui/1.13.2/jquery-ui.min.js"></script>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
