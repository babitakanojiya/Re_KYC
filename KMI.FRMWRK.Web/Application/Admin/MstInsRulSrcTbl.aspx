<%--<%@ Page Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="MstInsRulSrcTbl.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.Admin.MstInsRulSrcTbl" %>--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="MstInsRulSrcTbl.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.Admin.MstInsRulSrcTbl" %>


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
         .nav-tabs > li {
            background-color: #094090;
        }

            .nav-tabs > li > a {
                border-radius: 0px !important;
                padding: 20px 20px;
            }


                .nav-tabs > li > a > span {
                    padding: 10px 15px;
                    font-weight: bold;
                    color: #fff;
                }

            .nav-tabs > li.active > a > span {
                padding: 10px 15px;
                font-weight: bold;
                color: #000;
            }

        .tab-content {
            border-left: 1px solid #ddd;
            border-right: 1px solid #ddd;
            border-bottom: 1px solid #ddd;
            padding: 35px !important;
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
              
                    color: black;
                    cursor: pointer;
                    padding: 10px 20px;
                    text-decoration: none;
                    border-radius: 4px 4px 0 0;
                }

                    ul#menu li a:active {

                    }

                    ul#menu li a:hover {
                       /*background-color: #F55856;*/
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
        var bsdonsynmLength = 0;

        $(document).ready(function () {
            debugger;
            window.history.forward();

            $('#EmptyPagePlaceholder_lstbdonSynm').multiselect({
                includeSelectAllOption: true,
                onDropdownHidden: function (e) {
                    bindValFactor()
                }
            });
            //fnSetTabs('1',"");
        });

        function pageLoad(sender, args) {

            $('#EmptyPagePlaceholder_lstbdonSynm').multiselect({
                includeSelectAllOption: true,
                onDropdownHidden: function (e) {
                    bindValFactor()
                }
            });
        }

          function bindValFactor() {
            bsdonsynmLength = 0;
            var sel = document.getElementById('EmptyPagePlaceholder_lstbdonSynm');
            var fixlistLength = sel.options.length;
            for (var i = 0; i < fixlistLength; i++) {
                if (sel.options[i].selected) {
                    bsdonsynmLength += 1;
                }
            }
        }
    </script>

    <asp:ScriptManager ID="scrusdtls" runat="server"></asp:ScriptManager>
    <center>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
          <div class="page-container">
               <div id="divRulSrcTbl" runat="server" style="margin-left: 2%; margin-right: 2%; margin-top: 0.5%" class="panel panel-success">
                     <div id="Div1" runat="server" class="panel-heading" onclick="ShowReqDtl1('ctl00_ContentPlaceHolder1_divRulsrch','btnToggleNew');return false;">
                        
                            <div class="row">
                                <div class="col-sm-10" style="text-align: left">
                                    <asp:Label ID="lblRulSrch" Text="Map Synonyms and Source Table" Font-Size="19px" runat="server" />
                                </div>
                                <div class="col-sm-2">
                                <span id="btnToggleNew" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                            </div>
                       
                    </div>
               
                           <div id="divRulsrch" runat="server" style="padding: 30px;" class="panel-body">
                       <div class="row" style="margin-bottom: 5px;">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblIntGid" Text="Integration ID" runat="server" CssClass="control-label" />
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtIntGid" runat="server" CssClass="form-control" TabIndex="1"
                                     />
                            </div>

                              <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblST" Text="Source Table" runat="server" CssClass="control-label" />
                                  <span style="color: red;">*</span>
                            </div>
                            <div class="col-sm-3">
                                 <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlSrcTbl" runat="server"  CssClass="form-control"
                                            TabIndex="4"  >
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                          </div>
                     
                       <div class="row" style="margin-bottom: 5px;">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblSyn" Text="Based on Synonyms" runat="server" CssClass="control-label" />
                                <span style="color: red;">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <%--<asp:DropDownList ID="ddlbdonSynm" runat="server" AutoPostBack="true" CssClass="form-control"
                                            TabIndex="4"  >
                                        </asp:DropDownList>--%>
                                        <asp:ListBox ID="lstbdonSynm" runat="server" SelectionMode="Multiple" TabIndex="4"></asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                              <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="Label2" Text="Data Synchronization Type" runat="server" CssClass="control-label" />
                                  <span style="color: red;">*</span>
                            </div>
                            <div class="col-sm-3">
                                 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlDATASYNCTYP" runat="server"  CssClass="form-control" AutoPostBack="true" 
                                            TabIndex="4"  >  <%--OnSelectedIndexChanged="ddlDATASYNCTYP_SelectedIndexChanged"--%>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                          </div>
                       <div class="row" style="margin-bottom: 5px;">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblEffFrom" Text="Effective From" runat="server" CssClass="control-label" />
                                <span style="color: red;">*</span>
                            </div>
                            <div class="col-sm-3">
                                 <asp:TextBox ID="txtEffFrom" 
                                     placeholder="DD/MM/YYYY" runat="server" CssClass="form-control" ></asp:TextBox>  <%--onclick="callEffectiveDateFrom()"--%> 
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblEffTo" Text="Cease Date" runat="server" CssClass="control-label" />
                            </div>
                            <div class="col-sm-3">
                                 <asp:TextBox ID="txtEffTo" 
                                     placeholder="DD/MM/YYYY" runat="server" CssClass="form-control" ></asp:TextBox> <%-- onclick="callEffectiveDateTo()"--%>
                            </div>
                          </div>
                       <div class="row" style="margin-bottom: 5px;">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblStatus" Text="Status" runat="server" CssClass="control-label" />
                                <span style="color: red;">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlStatus" runat="server"  CssClass="form-control"
                                            TabIndex="4"  >
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                       </div>
                      <div id="div2" runat="server" style="padding: 30px;" visible="false" class="panel-body">
                       <div class="row" style="margin-bottom: 5px;">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblIncreTYp" Text="Increamental Type" runat="server" CssClass="control-label" />
                                <span style="color: red;">*</span>
                            </div>
                            <div class="col-sm-3">
                                 <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlIncreTYp" runat="server"  CssClass="form-control" AutoPostBack="true" 
                                            TabIndex="4"  >  <%--OnSelectedIndexChanged="ddlIncreTYp_SelectedIndexChanged"--%>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                              <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblrefcol" Text="Reference Column" runat="server" CssClass="control-label" />
                                <asp:Label ID="lblprmykeycol" Text="Primary Key Column" style="display:none;" runat="server" CssClass="control-label" />
                                <span style="color: red;">*</span>
                            </div>
                            <div class="col-sm-3">
                                 <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlrefcol" runat="server"  CssClass="form-control"
                                            TabIndex="4"  >
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                 <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlprmykeycol" runat="server"  CssClass="form-control" style="display:none;"
                                            TabIndex="4"  >
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                          </div>
                       <div class="row" style="margin-bottom: 5px;">
                              <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblperfreq" Text="Period Frequency" style="display:none;" runat="server" CssClass="control-label" />
                                <asp:Label ID="lblveridcol" Text="Version ID Column" style="display:none;" runat="server" CssClass="control-label" />
                                <span id="Var1" style="color: red;display:none" runat="server">*</span>
                            </div>
                            <div class="col-sm-3">
                                 <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlperfreq" runat="server"  CssClass="form-control" style="display:none;"
                                            TabIndex="4"  >
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                 <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlveridcol" runat="server"  CssClass="form-control" style="display:none;"
                                            TabIndex="4"  >
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                          </div>
                 </div>                      
                           <div id="divsyncrete" runat="server" class="row" style="margin-top: 12px;">
                            <div class="col-sm-12" >
                                <asp:LinkButton ID="btnSave" runat="server" CssClass="btn btn-primary" TabIndex="17" OnClientClick="return fnValidate();" OnClick="btnSave_Click">
                                   <span class="glyphicon glyphicon-floppy-save" style="color: White;"></span> Save
                                 </asp:LinkButton>

                                   <asp:LinkButton ID="btnClr" runat="server" CssClass="btn btn-danger" TabIndex="17" OnClick="btnClr_Click" >
                                     <span class="glyphicon glyphicon-erase BtnGlyphicon" style="color: White;"></span> Clear
                                   </asp:LinkButton>

                                <asp:LinkButton ID="btnCancl" runat="server" CssClass="btn btn-danger" TabIndex="17" OnClientClick="JavaScript:window. history. back(1); return false;">
                                     <span class="glyphicon glyphicon-remove" style="color: White;"></span> Cancel
                                   </asp:LinkButton>
                                </div>
                           
                        </div>

                          <div id="divGridReslts" runat="server" style="overflow-x:auto;width: 100%;" class="">
                            <div id="divGrid" runat="server" style="width: 98%; padding: 10px;">
                                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="gvMSAST" runat="server" AutoGenerateColumns="false" Width="100%" 
                                    AllowPaging="true" PageSize="10" AllowSorting="True" 
                                    CssClass="footable"
                                    ShowHeader="true">  <%--OnRowDataBound="gvMSAST_RowDataBound"  OnSorting="gvMSAST_Sorting" --%>
                                    
                                    <RowStyle CssClass="GridViewRowNew"></RowStyle>
                                    <PagerStyle CssClass="disablepage" />
                                    <HeaderStyle CssClass="gridview th" />
                                    <EmptyDataTemplate>
                                                <asp:Label ID="lblerror" Text="No records found" ForeColor="Red"
                                                    CssClass="control-label" runat="server" />
                                            </EmptyDataTemplate>
                                       <Columns>
                                        <asp:TemplateField HeaderText="Source Table " HeaderStyle-HorizontalAlign="Left" SortExpression="SRC_TBL_ID"
                                            HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSRC_TBL_COL" Text='<%# Bind("SRC_TBL_ID") %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Based on Synonym" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center"
                                            SortExpression="SYNYM_NAME">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSynonyms" Text='<%# Bind("BSED_ON_SYNM") %>' runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" CssClass="clsCenter"/>
                                        </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Effective Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center"
                                            SortExpression="EFF_DTIM">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEffDatenew" Text='<%# Bind("EFF_DTIM_NEW") %>' runat="server" />
                                                <asp:Label ID="lblEffDate" Text='<%# Bind("EFF_DTIM") %>' Visible="false" runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" CssClass="clsCenter"/>
                                        </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Cease Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center"
                                            SortExpression="CSE_DTIM">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCeaseDatenew" Text='<%# Bind("CSE_DTIM_NEW") %>' runat="server" />
                                                <asp:Label ID="lblCeaseDate" Text='<%# Bind("CSE_DTIM") %>' Visible="false" runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" CssClass="clsCenter"/>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Integration ID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center"
                                            SortExpression="INTGRTN_ID">
                                            
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkINTGID" CssClass="col-xs-12"  runat="server" Text='<%# Bind("INTGRTN_ID") %>' ForeColor="#3333cc" OnClick="lnkINTGID_Click"  ></asp:LinkButton>  <%--OnClick="lnkINTGID_Click"--%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" CssClass="clsCenter"/>
                                        </asp:TemplateField>
                                     
                                    </Columns>
                                </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <br />
                                <div class="pagination"  style="padding: 10px; display:block">
                            <center>
                                <table>
                                    <tr>
                                        <td style="white-space: nowrap;">
                                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                <ContentTemplate>
                                                    <asp:Button ID="btnprevious" Text="<" CssClass="form-submit-button" runat="server"
                                                        Width="40px" Enabled="false" Style="border-style: solid; border-width: 1px; background-repeat: no-repeat;
                                                        background-color: transparent; float: left; margin: 0; height: 30px;" OnClick="btnprevious_Click" Visible="false"  /> <%-- OnClick="btnprevious_Click"--%>
                                                    <asp:TextBox runat="server" ID="txtPage" Text="1" Style="width: 50px; border-style: solid;
                                                        border-width: 1px; border-color: Gray; height: 30px; float: left; margin: 0;
                                                        text-align: center;" CssClass="form-control" ReadOnly="true" Visible="false" />
                                                    <asp:Button ID="btnnext" Text=">" CssClass="form-submit-button" runat="server" Style="border-style: solid;
                                                        border-width: 1px; background-repeat: no-repeat; background-color: transparent;
                                                        float: left; margin: 0; height: 30px;" Width="40px" Visible="false"  OnClick="btnnext_Click" />  <%-- OnClick="btnnext_Click"--%>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </center>
                        </div>
                            </div>
                          </div>
                             
                          <br />
                          <br />  
                               <br />
                            <div class="row" id="divTabs" style="display:none;">
                                <div class="col-md-12">
                                    <div class="card">
                                        <ul id="myTab" class="nav nav-tabs">
                                            <li id="liSRFUL"><a id="tabSRFUL" onclick="return fnSetTabs('1','','');" style="font-weight: bold;">Source Table Fill Up Logic </a></li>
                                            <li id="liDP"><a id="tabDP" onclick="return fnSetTabs('2','','');" style="font-weight: bold;">Data Preparation </a></li>
                                             <li id="liEH"><a id="tabEH" onclick="return fnSetTabs('3','','');" style="font-weight: bold;">Error Handling </a></li>
                                            <li id="liJSD"><a id="tabJSD" onclick="return fnSetTabs('4','','');" style="font-weight: bold;">Job Schedule Details </a></li>
                                            <li id="liJD"><a id="tabJD" onclick="return fnSetTabs('5','','');" style="font-weight: bold;">Job Execution Details</a></li>
                                        </ul>
                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <div class="tab-content tab-pane active">
                                                    <asp:Panel runat="server" Height="494px" Width="102%" ID="PanelIntgrSrc" display="none" Style="text-align: center; padding-right: 15px;">
                                                        <iframe runat="server" id="ifrmIntgSrc" width="100%" frameborder="0"
                                                        display="none" style="height: 100%;" >
                                                        </iframe>
                                                    </asp:Panel>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                    </div>
                                </div>
                            </div>

                          <br />
        
                         
   
                        
                 </div>
               </div>
              </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </center>

     <asp:HiddenField ID="hdnTabIndex" runat="server" />
    <asp:HiddenField ID="ValueHiddenField" Value="" runat="server" />
    <asp:HiddenField ID="HdnSrcTbl" Value="" runat="server" />

    <script>
        function onINTGCLICK(activeTab, INTGID, SrcTbl) {
            debugger;
            document.getElementById("divTabs").style.display = "block";
            document.getElementById("EmptyPagePlaceholder_ValueHiddenField").value = INTGID;
            document.getElementById("EmptyPagePlaceholder_HdnSrcTbl").value = SrcTbl;
            fnSetTabs(activeTab, INTGID, SrcTbl)
        }

        function fnSetTabs(strTabIndex, INTGID, SrcTbl) {
            debugger;
            var INT_ID = INTGID;
            var SrcTbl = SrcTbl;
            if (INTGID == "") {
                INT_ID = document.getElementById("EmptyPagePlaceholder_ValueHiddenField").value;
                SrcTbl = document.getElementById("EmptyPagePlaceholder_HdnSrcTbl").value;
            }
            else {
                INT_ID = INT_ID;
                SrcTbl = SrcTbl;
            }
            if (strTabIndex == '1') {
                document.getElementById("liSRFUL").className = "active";
                document.getElementById("liEH").classList.remove("active");
                document.getElementById("liDP").classList.remove("active");
                document.getElementById("liJD").classList.remove("active");
                document.getElementById("liJSD").classList.remove("active");
                document.getElementById("EmptyPagePlaceholder_ifrmIntgSrc").src = "IntgrtnSrcTblLogic.aspx?INTG_ID=" + INT_ID;
                document.getElementById("EmptyPagePlaceholder_hdnTabIndex").value = "1";
            }

            if (strTabIndex == '2') {
                document.getElementById("EmptyPagePlaceholder_ifrmIntgSrc").src = "DataPrepration.aspx?INTG_ID=" + INT_ID;
                document.getElementById("EmptyPagePlaceholder_hdnTabIndex").value = "2";
                document.getElementById("liSRFUL").classList.remove("active");
                document.getElementById("liDP").className = "active";
                document.getElementById("liEH").classList.remove("active");
                document.getElementById("liJSD").classList.remove("active");
                document.getElementById("liJD").classList.remove("active");
            }

            if (strTabIndex == '3') {
                document.getElementById("EmptyPagePlaceholder_ifrmIntgSrc").src = "ErrorHandling.aspx?INTG_ID=" + INT_ID;
                document.getElementById("EmptyPagePlaceholder_hdnTabIndex").value = "3";
                document.getElementById("liSRFUL").classList.remove("active");
                document.getElementById("liDP").classList.remove("active");
                document.getElementById("liEH").className = "active";
                document.getElementById("liJSD").classList.remove("active");
                document.getElementById("liJD").classList.remove("active");
            }
            if (strTabIndex == '4') {


                document.getElementById("EmptyPagePlaceholder_ifrmIntgSrc").src = "JobDetails.aspx?INTG_ID=" + INT_ID + "&SrcTbl=" + SrcTbl;
                document.getElementById("EmptyPagePlaceholder_hdnTabIndex").value = "4";
                document.getElementById("liSRFUL").classList.remove("active");
                document.getElementById("liEH").classList.remove("active");
                document.getElementById("liDP").classList.remove("active");
                document.getElementById("liJSD").className = "active";
                document.getElementById("liJD").classList.remove("active");
            }
            if (strTabIndex == '5') {
                document.getElementById("EmptyPagePlaceholder_ifrmIntgSrc").src = "Job_Run_Status.aspx?INTG_ID=" + INT_ID;
                document.getElementById("EmptyPagePlaceholder_hdnTabIndex").value = "5";
                document.getElementById("liSRFUL").classList.remove("active");
                document.getElementById("liEH").classList.remove("active");
                document.getElementById("liDP").classList.remove("active");
                document.getElementById("liJSD").classList.remove("active");
                document.getElementById("liJD").className = "active";
            }
        }

    </script>


    </asp:Content>
