<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="SearchViewdata.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.CKYC.CkycSearch" %>

<script runat="server">

    protected void dgView_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
</script>




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
    </style>

    <script type="text/javascript">

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

    <asp:ScriptManager ID="CKYCSearch" runat="server">
    </asp:ScriptManager>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <%--Added by Tushar for sync Zip File--%>
            <%--<div class="row" style="display: none;">
                <div class="col-sm-12" style="margin-left: 86.5%;">
                    <asp:Button ID="btnsyncFile" Text="Sync Zip File" OnClick="btnsyncFile_Click" CssClass="btn-animated bg-green" runat="server"></asp:Button>
                </div>
            </div>--%>
            <%--Added by Tushar for sync Zip File--%>
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
                                <div class="row" style="margin-bottom: 5px">
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblGivenName" runat="server" CssClass="control-label" Text="Applicant Name"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" MaxLength="60" onchange="javascript:this.value=this.value.toUpperCase();"></asp:TextBox>

                                    </div>
                                    <div id="tdPan" class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblPanno" runat="server" CssClass="control-label" Text="Identity Number"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtPanno" runat="server" CssClass="form-control" onchange="javascript:this.value=this.value.toUpperCase();"></asp:TextBox>
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
                                
                                <div id="trShw" runat="server" class="row" style="margin-bottom: 5px">

                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:Label ID="lblShwRecords" runat="server" CssClass="control-label" Text="Records shown per page"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlShwRecrds" runat="server" AutoPostBack="true" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlShwRecrds_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    

                                </div>
                    

                                <div class="row">
                                    <center>
                            <div class="col-sm-12" style='margin-top: 15px;'>
                                <asp:Button ID="btnSearch" Text="Search"  OnClick="btnSearch_Click" CssClass="btn-animated bg-green" runat="server"  OnClientClick="ShowProgressBar('Searching..Please wait')">                                   
                                </asp:Button>
                                <asp:Button ID="btnClear" CssClass="btn-animated bg-horrible" Text="Clear" runat="server">    </asp:Button>                            
                                <asp:Button ID="btnAddnew" runat="server"  CssClass="btn-animated bg-green"   Text="Add New" Visible="false" TabIndex="12">
                            
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
                            <div id="trgridsponsorship" class="panel-body" >
                               
                                <asp:GridView ID="dgView" runat="server" AllowSorting="True" CssClass="footable" Width="100%"
                                    AutoGenerateColumns="False" PageSize="100" AllowPaging="true" CellPadding="1" OnPageIndexChanging="dgView_PageIndexChanging"
                                    OnSorting="dgView_Sorting"  OnRowDataBound="dgView_RowDataBound" OnRowCreated="dgView_RowCreated" OnRowCommand="dgView_RowCommand" OnSelectedIndexChanged="dgView_SelectedIndexChanged">
                                    
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
                                        
                                        <asp:TemplateField HeaderText="Batch ID." ItemStyle-Width="20%" SortExpression="FIRefNo" HeaderStyle-CssClass="pad">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbatchno" runat="server" Text='<%# Eval("BATCHID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="12%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Create Date" HeaderStyle-CssClass="pad" ItemStyle-Width="20%" SortExpression="RegRefNo">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldate" runat="server" Text='<%# Eval("Create_Date") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="pad" HorizontalAlign="Center" Width="15%" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Applicant Name" ItemStyle-Width="20%" SortExpression="NAME" HeaderStyle-CssClass="pad">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNAME" runat="server" Text='<%# Eval("Applicant_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="KYC Number" ItemStyle-Width="20%" SortExpression="KYC_NO" HeaderStyle-CssClass="pad">
                                            <ItemTemplate>
                                                <asp:Label ID="lblKYCNo" runat="server" Text='<%# Eval("KYC_Number_20") %>'></asp:Label>
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

                    <table>
                        <tr>
                            <td>
                                <asp:HiddenField ID="hdnbatchno" runat="server" />
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
                            <td>
                                <asp:HiddenField ID="hdnCandCode" runat="server" />
                            </td>
                            <td>
                                <asp:HiddenField ID="hdnRecruiterCode" runat="server" />
                            </td>
                            <td>
                                <asp:HiddenField ID="hdnMenuName" runat="server" />
                                <input type="hidden" id="hdnEmpName" runat="server" />
                            </td>
                            
                        </tr>
                    </table>
                </div>
                
                <div class="modal fade" id="myModal" role="dialog">
                </div>
             <%--   <div class="modal" id="myModalRaise" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="padding-top: 0px;">
                    <div class="modal-dialog" style="padding-top: 0px; margin-top: 2px; width: 95%;">
                        <div class="modal-content">
                            <div class="modal-header" style="margin-top: -10px; margin-bottom: -20px; padding-bottom: 6px ! important;">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title" id="myModalLabel">CKYC QC</h4>
                            </div>
                            <div class="modal-body">

                                <iframe id="iframeCFR" src="" width="100%" height="450" frameborder="0" allowtransparency="true"></iframe>
                            </div>
                            <div class="modal-footer" style="display: none">
                            </div>
                        </div>
                        
                    </div>
                    
                </div>--%>
                
                <div class="modal fade" id="myModal_NEw" role="dialog">
                </div>
               <%-- <div class="modal" id="myModalRaise_NEw" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="padding-top: 0px;">
                    <div class="modal-dialog" style="padding-top: 43px; margin-top: 2px; width: 95%; padding-left: 22px; padding-right: 2%;">
                        <div class="modal-content">
                            <div class="modal-header" style="margin-top: -10px; margin-bottom: -20px; padding-bottom: 6px ! important;">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title" id="myModalLabel_New">CKYC Pending File Details </h4>
                            </div>
                            <div class="modal-body">
                                <iframe id="iframeCFR_New" src="" width="100%" height="300" frameborder="0" allowtransparency="true"></iframe>
                            </div>
                            <div class="modal-footer" style="display: none">
                            </div>
                        </div>
                        
                    </div>
                    
                </div>--%>
                <!-- End Display Modal popup window in division -->
                <%-- Added By Megha Bhave 25.03.2021 --%>
                <div id="dvProgressBar" style="display: none; text-align: center" class="loader">
                    <center>
                         <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /> <br /><br /><br /><br /><br />
                          <asp:Image id="ldr" src="../../Images/horizonal_loader.gif"   height="50px" alt="" runat="server" ImageAlign="Middle"/>
                         <br />
                      <asp:Label ID="lblMsg" Text="" runat="server" ForeColor="Blue" style="font-size: medium; font-weight:bold" > </asp:Label>
                
            </center>
                </div>
                <%-- Ended By Megha Bhave 25.03.2021 --%>
            </div>




            <%--        <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdn.rawgit.com/elevateweb/elevatezoom/master/jquery.elevateZoom-3.0.8.min.js"></script>--%>

            <script type="text/javascript" src="../../Scripts/jquery.min.js"></script>
            <%--  <script type="text/javascript" src="../../Scripts/jquery.elevateZoom-3.0.8.min.js"></script>--%>
            <script type="text/javascript" src="../../Scripts/jquery.elevatezoom.js"></script>
            <%--<script type="text/javascript" src="../../Scripts/jquery.elevateZoom-3.0.8.min.js"></script>--%>

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

                 <%-- Added By Megha Bhave 25.03.2021 --%>
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
            </script>

        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
