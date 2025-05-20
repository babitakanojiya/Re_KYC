<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="IntgrtnSrcTblLogic.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.Admin.IntgrtnSrcTblLogic" %>
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
                    /*background-color: Silver;*/
                    color: black;
                    cursor: pointer;
                    padding: 10px 20px;
                    text-decoration: none;
                    border-radius: 4px 4px 0 0;
                }

                    ul#menu li a:active {
                        /*background: white;*/
                    }

                    ul#menu li a:hover {
                        background-color: #F55856;
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
            fnSetTabs('1');
        });

        function fnSetTabs(strTabIndex) {
            debugger;
            if (strTabIndex == '1') {
                document.getElementById('EmptyPagePlaceholder_divDJoin').style.display = "block";
                document.getElementById('EmptyPagePlaceholder_divDwhereCondition').style.display = "none";
                document.getElementById("liDJ").className = "active";
                document.getElementById("liDWC").classList.remove("active");
                document.getElementById('EmptyPagePlaceholder_hdnTabIndex').value = "1";
            }
            if (strTabIndex == '2') {
                document.getElementById('EmptyPagePlaceholder_divDJoin').style.display = "none";
                document.getElementById('EmptyPagePlaceholder_divDwhereCondition').style.display = "block";
                document.getElementById("liDJ").classList.remove("active");
                document.getElementById("liDWC").className = "active";
                document.getElementById('EmptyPagePlaceholder_hdnTabIndex').value = "2";
            }
        }

        function fnselSrcTblCol() {
            debugger;
            var ddlSTcol = document.getElementById('EmptyPagePlaceholde_ddlSTcol');
            var optionSelIndex = ddlSTcol.options[ddlSTcol.selectedIndex].value;
            if (optionSelIndex == 0) {
                alert("Please select Source Table Column.");
                return false;
            }
            fnCallPOP(optionSelIndex)
        }

        function fnCallPOP(SRC_TBL_COL) {
            debugger;
            $find("mdlPopBIDHybrid").show();
            var Mode = "B";
            document.getElementById('EmptyPagePlaceholde_ddlSynmCol').disabled = true;
            var INTGRTN_ID = document.getElementById('EmptyPagePlaceholder_txtIntGid').value;
            var SRC_TBL = document.getElementById('EmptyPagePlaceholde_TextBoxSrcTbl').value;
            document.getElementById("ctl00_ContentPlaceHolder1_Iframe1").src = ("FormulaPopUp.aspx?&SRC_TBL_COL=" + SRC_TBL_COL + "&SRC_TBL=" + SRC_TBL + "&INTGRTN_ID=" + INTGRTN_ID + "&mdlpopup=mdlPopBIDHybrid");
            return false;
        }

    </script>


       <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

     <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="page-container">
                <center>
                <div id="divfinhdrcollapse" class="panel panel-success" style="margin-left: 2%; margin-right: 2%; margin-top: 0.5%">
                     <div id="Div2" runat="server" class="panel-heading" onclick="ShowReqDtl1('ctl00_ContentPlaceHolder1_divIntRul','myImg1');return false;">
                        
                            <div class="row">
                                <div class="col-sm-10" style="text-align: left">
                                    <asp:Label ID="Label1" Text="Integration Source Table Fill Up Logic" Font-Size="19px" runat="server" />
                                </div>
                                <div class="col-sm-2">
                                    <span id="btnToggleNew" class="glyphicon glyphicon-collapse-down" style="float: right; 
                                        padding: 1px 10px ! important; font-size: 18px;"></span>
                                </div>
                            </div>
                    </div>

                    <div id="divIntRul" runat="server" style="padding: 25px;" class="panel-body">
                 <div class="row" style="margin-bottom: 5px;">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="Label3" Text="Source Table" runat="server" CssClass="control-label" />
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="TextBoxSrcTbl" runat="server" ToolTip = "" CssClass="form-control" TabIndex="1"
                                     />
                            </div>

                              <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="Label4" Text="Synonyms" runat="server" CssClass="control-label" />
                                  <span style="color: red;">*</span>
                            </div>
                            <div class="col-sm-3">
                                 <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlSynm" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="4" OnSelectedIndexChanged="ddlSynm_SelectedIndexChanged"  >
                                            <%--OnSelectedIndexChanged="ddlSynm_SelectedIndexChanged"--%>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                          </div>
                 <div class="row" style="margin-bottom: 5px;">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="Label5" Text="Source Table Column" runat="server" CssClass="control-label" />
                                <span style="color: red;">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlSTcol" Enabled="false" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="4" OnSelectedIndexChanged="ddlSTcol_SelectedIndexChanged"  >
                                            <%-- OnSelectedIndexChanged="ddlSTcol_SelectedIndexChanged"--%>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="Label6" Text="Synonyms Column" runat="server" CssClass="control-label" />
                                <span style="color: red;">*</span>
                            </div>
                           <%-- <div class="input-group mb-3 col-sm-3">
                                 
                                <div class="input-group-append">
                                    <span class="input-group-text">@example.com</span>
                                </div>
                            </div>--%>
                     <div class="col-sm-3">
                        <div class="input-group">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlSynmCol" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="4"  >
                                           <%-- OnSelectedIndexChanged="ddlSynmCol_SelectedIndexChanged"--%>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            <div class="input-group-btn">
                                <div class="">
                                    <button type="button" id="divbtnGrp1" runat="server" onclick="return fnselSrcTblCol();" class="btn btn-danger" >
                                        <span class="glyphicon glyphicon-plus" style="color:white;"></span>
                                    </button>
                                    <%--data-toggle="modal" data-target="#myModal" <span class="glyphicon glyphicon-plus" data-toggle="modal" data-target="#myModal" style="color:white;"></span>--%>
                                </div>
                            </div>
                        </div>
                    </div>

                        </div>
                 <div class="row" style="margin-bottom: 5px;">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblEffFrom" Text="Effective From" runat="server" CssClass="control-label" />
                        <span style="color: red;">*</span>
                    </div>
                    <div class="col-sm-3">
                            <asp:TextBox ID="txtEffFrom" placeholder="DD/MM/YYYY" runat="server" CssClass="form-control" ></asp:TextBox>
                      <%--  onclick="callEffectiveDateFrom()"--%>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblEffTo" Text="Cease Date" runat="server" CssClass="control-label" />
                    </div>
                    <div class="col-sm-3">
                            <asp:TextBox ID="txtEffTo" placeholder="DD/MM/YYYY" runat="server" CssClass="form-control" ></asp:TextBox>
                        <%--onclick="callEffectiveDateTo()"--%>
                    </div>
                 </div>
                 <div class="row" style="margin-bottom: 5px;">
                    <div class="col-sm-3" style="text-align: left">
                        <asp:Label ID="lblStatus" Text="Status" runat="server" CssClass="control-label" />
                        <span style="color: red;">*</span>
                    </div>
                    <div class="col-sm-3">
                        <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlStatus" runat="server"  CssClass="form-control"
                                    TabIndex="4"  >
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                 </div>
                 <div id="div3" runat="server" class="row" style="margin-top: 12px;">
                            <div class="col-sm-12" >
                            <asp:LinkButton ID="btnUpdSTFUL" runat="server" CssClass="btn btn-primary" style="display:none;" TabIndex="17"  OnClientClick="return fnValidate();" OnClick="btnUpdSTFUL_Click">  <%--OnClick="btnUpdSTFUL_Click"--%>
                                <span class="glyphicon glyphicon-plus" style="color: White;"></span> Update
                            </asp:LinkButton>

                            <asp:LinkButton ID="btnAdd" runat="server" CssClass="btn btn-primary" style="display:inline-block;" TabIndex="17"  OnClientClick="return fnValidate();" OnClick="btnAdd_Click">  <%--OnClick="btnAdd_Click"--%>
                                <span class="glyphicon glyphicon-plus" style="color: White;"></span> Add
                            </asp:LinkButton>

                            <asp:LinkButton ID="btnClear" runat="server" CssClass="btn btn-danger" TabIndex="17" OnClick="btnClear_Click">   <%--OnClick="btnClear_Click"--%>
                                <span class="glyphicon glyphicon-erase BtnGlyphicon" style="color: White;"></span> Clear
                            </asp:LinkButton>

                            <asp:LinkButton ID="BtnCncl" runat="server" CssClass="btn btn-danger" TabIndex="17" OnClientClick="JavaScript:window. history. back(1); return false;" >
                                <span class="glyphicon glyphicon-remove" style="color: White;"></span> Cancel
                            </asp:LinkButton>
                        </div>
                        </div>
                 <div id="divGridReslts" runat="server" style="width: 100%;" class="">
                    <div id="divGrid" runat="server" style="width: 98%; padding: 10px;">
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvINTSTFillUP" runat="server" AutoGenerateColumns="false" Width="100%" 
                                    AllowPaging="true" PageSize="10" AllowSorting="True" CssClass="footable" 
                                    ShowHeader="true"> <%-- OnSorting="gvINTSTFillUP_Sorting"   OnRowDataBound="gvINTSTFillUP_RowDataBound"--%>
                                    
                                    <RowStyle CssClass="GridViewRowNew"></RowStyle>
                                    <PagerStyle CssClass="disablepage" />
                                    <HeaderStyle CssClass="gridview th" />
                                    <EmptyDataTemplate>
                                                <asp:Label ID="lblerror" Text="No records found" ForeColor="Red"
                                                    CssClass="control-label" runat="server" />
                                            </EmptyDataTemplate>
                                       <Columns>
                                        <asp:TemplateField HeaderText="Synonyms" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center"
                                            SortExpression="Synonyms">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSTFULSEQNO" Text='<%# Bind("SEQ_NO") %>' Visible="false" runat="server" />
                                                <asp:Label ID="lblsrcTblISTFUL" Text='<%# Bind("SRC_TBL") %>' Visible="false" runat="server" />
                                                <asp:Label ID="lblSynonyms" Text='<%# Bind("Synonyms") %>' runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" CssClass="clsCenter"/>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Source Table Column" HeaderStyle-HorizontalAlign="Left" SortExpression="SCR_TBL_COL"
                                            HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSRC_TBL_COL" Text='<%# Bind("SCR_TBL_COL") %>' runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" CssClass="clsCenter"/>
                                        </asp:TemplateField>

                                           <asp:TemplateField HeaderText="SYNM Column" HeaderStyle-HorizontalAlign="Left" SortExpression="SYNM_TBL_COL"
                                            HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSYNM_COL" Text='<%# Bind("SYNM_TBL_COL") %>' runat="server" />
                                            </ItemTemplate>
                                               <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" CssClass="clsCenter"/>
                                        </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Effective Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center"
                                            SortExpression="EFF_DTIM">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEffDate" Visible="false" Text='<%# Bind("EFF_DTIM") %>' runat="server" />
                                                <asp:Label ID="lblEffDatenew" Text='<%# Bind("EFF_DTIM_NEW") %>' runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" CssClass="clsCenter"/>
                                        </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Cease Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center"
                                            SortExpression="CSE_DTIM">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCeaseDate" Visible="false" Text='<%# Bind("CSE_DTIM") %>' runat="server" />
                                                <asp:Label ID="lblCeaseDatenew" Text='<%# Bind("CSE_DTIM_NEW") %>' runat="server" />
                                                <asp:Label ID="lblstatusISTFUL" Text='<%# Bind("STATUS") %>' Visible="false" runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" CssClass="clsCenter"/>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Action">
                                            <%--<HeaderStyle CssClass="gridview th" />--%>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit"  runat="server" Text="Edit" OnClick="lnkEdit_Click" ForeColor="#3333cc"></asp:LinkButton>  <%--OnClick="lnkEdit_Click"--%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" CssClass="clsCenter"/>
                                        </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                            <%--<HeaderStyle CssClass="gridview th" />--%>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete"  runat="server" Text="Delete"  OnClientClick="return confirm('Are you sure you want to Delete?');" ForeColor="#3333cc" OnClick="lnkDelete_Click"></asp:LinkButton> <%--OnClick="lnkDelete_Click"--%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" CssClass="clsCenter"/>
                                        </asp:TemplateField>
                                     
                                     
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="pagination" style="padding: 10px;">
                            <center>
                                <table>
                                    <tr>
                                        <td style="white-space: nowrap;">
                                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                <ContentTemplate>
                                                    <asp:Button ID="btnprevious" Text="<" CssClass="form-submit-button" runat="server"
                                                        Width="40px" Enabled="false" Style="border-style: solid; border-width: 1px; background-repeat: no-repeat;
                                                        background-color: transparent; float: left; margin: 0; height: 30px;" OnClick="btnprevious_Click" />
                                                    <asp:TextBox runat="server" ID="txtPage" Text="1" Style="width: 50px; border-style: solid;
                                                        border-width: 1px; border-color: Gray; height: 30px; float: left; margin: 0;
                                                        text-align: center;" CssClass="form-control" ReadOnly="true" />
                                                    <asp:Button ID="btnnext" Text=">" CssClass="form-submit-button" runat="server" Style="border-style: solid;
                                                        border-width: 1px; background-repeat: no-repeat; background-color: transparent;
                                                        float: left; margin: 0; height: 30px;" Width="40px" OnClick="btnnext_Click" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </center>
                        </div>
                    </div>
                        </div>
              </div>
                    <br />
                      <div class="row" style="width:100%;">
                <div class="col-md-12">
                    <div class="card">
                        <ul id="myTab" class="nav nav-tabs">
                            <li id="liDJ"><a id="tabDJ" onclick="return fnSetTabs('1');" style="font-weight: bold;">Define Join </a></li>
                            <li id="liDWC"><a id="tabDWC" onclick="return fnSetTabs('2');" style="font-weight: bold;">Define Where Condition</a></li>
                        </ul>
                        <asp:UpdatePanel runat="server" RenderMode="Inline">
                            <ContentTemplate>
                                 <div id ="divDJoin" class="panel panel-success" runat="server" style="display:none">
                    <div id="Div1" runat="server" class="panel-heading" onclick="ShowReqDtl1('ctl00_ContentPlaceHolder1_divDEFJOIN','myImgDJoin');return false;">
                        
                            <div class="row">
                                <div class="col-sm-10" style="text-align: left">
                                    <asp:Label ID="lblRulSrch" Text="Define Join" Font-Size="19px" runat="server" />
                                </div>
                                <div class="col-sm-2">
                                    <span id="btnToggleNew1" class="glyphicon glyphicon-collapse-down" style="float: right; 
                                        padding: 1px 10px ! important; font-size: 18px;"></span>
                                </div>
                            </div>
                       
                    </div>

                    <div id="divDEFJOIN" runat="server" style="padding: 30px;" class="panel-body">
                       <div class="row" style="margin-bottom: 5px;">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblIntGid" Text="Integration ID" runat="server" CssClass="control-label" />
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtIntGid" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblST" Text="Join ID" runat="server" CssClass="control-label" />
                            </div>
                            <div class="col-sm-3">
                                
                                        <asp:TextBox ID="txtJId" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                   
                            </div>
                          </div>
                       <div class="row" style="margin-bottom: 5px;">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblSyn1" Text="Synonyms 1" runat="server" CssClass="control-label" />
                                <span style="color: red;">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlSynm1" runat="server"  CssClass="form-control"
                                            TabIndex="4"  >
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblSyn2" Text="Synonyms 2" runat="server" CssClass="control-label" />
                                <span style="color: red;">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlSynm2" runat="server"  CssClass="form-control"
                                            TabIndex="4"  >
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                              
                          </div>
                       <div class="row" style="margin-bottom: 5px;">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="Label2" Text="Is-Primary Join" runat="server" CssClass="control-label" />
                                <span style="color: red;">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlPrmJnt" runat="server"  CssClass="form-control"
                                            TabIndex="4"  >
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblJointType" Text="Join Type" runat="server" CssClass="control-label" />
                                <span style="color: red;">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlJoinType" runat="server"  CssClass="form-control"
                                            TabIndex="4"  >
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                          </div>
                       <div class="row" style="margin-bottom: 5px;">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblDJEffFrom" Text="Effective From" runat="server" CssClass="control-label" />
                                <span style="color: red;">*</span>
                            </div>
                            <div class="col-sm-3">
                                 <asp:TextBox ID="txtDJEffFrom" placeholder="DD/MM/YYYY" runat="server" CssClass="form-control" ></asp:TextBox>
                                <%--onclick="callEffectiveDateFromDEFJOIN()"--%>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblDJEffTo" Text="Cease Date" runat="server" CssClass="control-label" />
                            </div>
                            <div class="col-sm-3">
                                 <asp:TextBox ID="txtDJEffTo" placeholder="DD/MM/YYYY" runat="server" CssClass="form-control" ></asp:TextBox>
                               <%-- onclick="callEffectiveDateToDEFJOIN()"--%>
                            </div>
                          </div>
                       <div class="row" style="margin-bottom: 5px;">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblDJStatus" Text="Status" runat="server" CssClass="control-label" />
                                <span style="color: red;">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlDJStatus" runat="server"  CssClass="form-control"
                                            TabIndex="4"  >
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                       </div>
                       <div id="divsyncrete" runat="server" class="row" style="margin-top: 12px;">
                            <div class="col-sm-12" >
                                <asp:LinkButton ID="btnDEFJOINUpd" runat="server" CssClass="btn btn-primary" style="display:none;" TabIndex="17" OnClientClick="return fnValidateDEFJOIN();" OnClick="btnDEFJOINUpd_Click">
                                   <%-- OnClick="btnDEFJOINUpd_Click"--%>
                                    <span class="glyphicon glyphicon-floppy-save" style="color: White;"></span> Update
                                 </asp:LinkButton>

                                <asp:LinkButton ID="btnDEFJOINSave" runat="server" CssClass="btn btn-primary" style="display:inline-block;" TabIndex="17"  OnClientClick="return fnValidateDEFJOIN();" OnClick="btnDEFJOINSave_Click">
                                   <%-- OnClick="btnDEFJOINSave_Click"--%>
                                   <span class="glyphicon glyphicon-floppy-save" style="color: White;"></span> Save
                                 </asp:LinkButton>

                                   <asp:LinkButton ID="btnClr" runat="server" CssClass="btn btn-danger" TabIndex="17" OnClick="btnClr_Click">  <%--OnClick="btnClr_Click"--%>
                                     <span class="glyphicon glyphicon-erase BtnGlyphicon" style="color: White;"></span> Clear
                                   </asp:LinkButton>

                                <asp:LinkButton ID="btnCancl" runat="server" CssClass="btn btn-danger" TabIndex="17" OnClientClick="JavaScript:window. history. back(1); return false;" >  <%--OnClick="btnCancl_Click"--%>
                                     <span class="glyphicon glyphicon-remove" style="color: White;"></span> Cancel
                                   </asp:LinkButton>

                                

                                </div>
                           
                        </div>
                        <br />
                       <div id="divGRIDDefJoin" runat="server" style="width: 97%;" >

                       <asp:updatepanel runat="server">
                        <contenttemplate>
                            <asp:GridView ID="gridDefJoin" runat="server" AutoGenerateColumns="false" Width="100%"
                            PageSize="10" AllowSorting="True" AllowPaging="true"  CssClass="footable"  ShowHeader="true">  
                                <%--OnRowDataBound="gridDefJoin_RowDataBound"   OnSorting="gridDefJoin_Sorting" --%>
                                
                            <RowStyle CssClass="GridViewRow"></RowStyle>
                            <PagerStyle CssClass="disablepage" />
                            <HeaderStyle CssClass="gridview th" />
                            <Columns>
                                <asp:TemplateField HeaderText="Join ID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center"
                                    SortExpression="JN_ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblJN_ID" Text='<%# Bind("JN_ID") %>' runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" CssClass="clsCenter"/>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Synonyms 1" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center"
                                    SortExpression="TABLE_1">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTABLE_1" Text='<%# Bind("TABLE_1") %>' runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" CssClass="clsCenter"/>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Synonyms 2" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" SortExpression="TABLE_2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTABLE_2" Text='<%# Bind("TABLE_2") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Is Primary" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center"
                                    SortExpression="IS_PRIMARY_JOIN">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIS_PRIMARY_JOIN_NEW" CssClass="col-xs-12" Text='<%# Bind("IS_PRIMARY_JOIN_NEW") %>' runat="server" />
                                        <asp:Label ID="lblIS_PRIMARY_JOIN" CssClass="col-xs-12" Visible="false" Text='<%# Bind("IS_PRIMARY_JOIN") %>' runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Center" CssClass="clsCenter"/>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Join Type" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center"
                                    SortExpression="JN_TYP">
                                    <ItemTemplate>
                                        <asp:Label ID="lblJN_TYP" Text='<%# Bind("JN_TYP") %>' runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" CssClass="clsCenter"/>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Effective Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center"
                                            SortExpression="EFF_DTIM">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDJEffDate" Text='<%# Bind("EFF_DTIM_NEW") %>' runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" CssClass="clsCenter"/>
                                        </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Cease Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center"
                                            SortExpression="CSE_DTIM">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDJCeaseDate" Text='<%# Bind("CSE_DTIM_NEW") %>' runat="server" />
                                                
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" CssClass="clsCenter"/>
                                        </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                            <HeaderStyle CssClass="gridview th" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkGRIDDefJOINEdit"  runat="server" Text="Define Join Column" ForeColor="#3333cc" OnClick="lnkGRIDDefJOINEdit_Click"></asp:LinkButton> <%--OnClick="lnkGRIDDefJOINEdit_Click"--%>
                                            </ItemTemplate>
                                 </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                            <HeaderStyle CssClass="gridview th" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkGRIDDefJOINEdit1"  runat="server" Text="Edit" ForeColor="#3333cc" OnClick="lnkGRIDDefJOINEdit1_Click" ></asp:LinkButton> <%-- OnClick="lnkGRIDDefJOINEdit1_Click"--%>
                                            </ItemTemplate>
                                 </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                            <HeaderStyle CssClass="gridview th" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkGRIDDefJOINDelete"  runat="server" Text="Delete" OnClientClick="return confirm('Are you sure you want to Delete?');" ForeColor="#3333cc" OnClick="lnkGRIDDefJOINDelete_Click"></asp:LinkButton>
                                               <%-- OnClick="lnkGRIDDefJOINDelete_Click"--%>
                                            </ItemTemplate>
                                 </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        </contenttemplate>
                       </asp:updatepanel>
                         <%--<div class="pagination" style="padding: 10px;">
                            <center>
                                <table>
                                    <tr>
                                        <td style="white-space: nowrap;">
                                            <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                                <ContentTemplate>
                                                    <asp:Button ID="btnGrdDEFJOINPrev" Text="<" CssClass="form-submit-button" runat="server"
                                                        Width="40px" Enabled="false" Style="border-style: solid; border-width: 1px; background-repeat: no-repeat;
                                                        background-color: transparent; float: left; margin: 0; height: 30px;" OnClick="btnGrdDEFJOINPrev_Click" />
                                                    <asp:TextBox runat="server" ID="TextBox3" Text="1" Style="width: 50px; border-style: solid;
                                                        border-width: 1px; border-color: Gray; height: 30px; float: left; margin: 0;
                                                        text-align: center;" CssClass="form-control" ReadOnly="true" />
                                                    <asp:Button ID="btnGrdDEFJOINNext" Text=">" CssClass="form-submit-button" runat="server" Style="border-style: solid;
                                                        border-width: 1px; background-repeat: no-repeat; background-color: transparent;
                                                        float: left; margin: 0; height: 30px;" Width="40px" OnClick="btnGrdDEFJOINNext_Click" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </center>
                        </div>--%>
                      </div>
                    </div>
                    <br />
                    <div id ="divDefColJoin" runat="server" style="display:none;">
                     <div id="Div4" runat="server" class="panel-heading" onclick="ShowReqDtl1('ctl00_ContentPlaceHolder1_divRulsrch','myImgcol');return false;">
                        
                            <div class="row">
                                <div class="col-sm-10" style="text-align: left">
                                    <asp:Label ID="Label7" Text="Define Column for Join" Font-Size="19px" runat="server" />
                                </div>
                                <div class="col-sm-2">
                                    <span id="myImgcol" class="glyphicon glyphicon-menu-hamburger" style="float: right; color: #034ea2;
                                        padding: 1px 10px ! important; font-size: 18px;"></span>
                                </div>
                            </div>
                       
                    </div>

                     <div id="div5" runat="server" style="padding: 30px;" class="panel-body">
                        <div class="row" style="margin-bottom: 5px;">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblSVACol" Text="Set value as column" runat="server" CssClass="control-label" />
                            </div>
                            <div class="col-sm-3">
                                <asp:RadioButtonList ID="rdbSVACol" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdbSVACol_SelectedIndexChanged" >
                                   <%-- OnSelectedIndexChanged="rdbSVACol_SelectedIndexChanged"--%>
                                    <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="NO" Value="0" Selected="True"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblSValCol" Text="Set value column" runat="server" CssClass="control-label" />
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlSValCol" runat="server"  CssClass="form-control"
                                            TabIndex="4"  >
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="row" style="margin-bottom: 5px;">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblDCFJSynmCol" Text="Synonyms Column" runat="server" CssClass="control-label" />
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlDCFJSynmCol" runat="server"  CssClass="form-control"
                                            TabIndex="4"  >
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblDCFJSOpertr" Text="Operator" runat="server" CssClass="control-label" />
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlDCFJSOpertr" runat="server"  CssClass="form-control"
                                            TabIndex="4"  >
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="row" style="margin-bottom: 5px;">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblDCFJColVal" Text="Column Value" runat="server" CssClass="control-label" />
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlDCFJColVal" runat="server" CssClass="form-control"
                                            TabIndex="4"  >
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="row" style="margin-bottom: 5px;">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="Label10" Text="Synonyms 1 Column" runat="server" CssClass="control-label" />
                                <span style="color: red;">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlSyn1Col" runat="server" AutoPostBack="true" CssClass="form-control"
                                            TabIndex="4"  >
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="Label11" Text="Synonyms 2 Column" runat="server" CssClass="control-label" />
                                <span style="color: red;">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlSyn2Col" runat="server" AutoPostBack="true" CssClass="form-control"
                                            TabIndex="4"  >
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                              
                          </div>
                        <div class="row" style="margin-bottom: 5px;">
                           
                              <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="Label9" Text="Join ID" runat="server" CssClass="control-label" />
                            </div>
                            <div class="col-sm-3">
                                
                                        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                   
                            </div>
                          <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="Label8" Text="Integration ID" runat="server" CssClass="control-label" />
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                            </div>

                          </div>
                        <div class="row" style="margin-bottom: 5px;">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblDCJoinEffFrm" Text="Effective From" runat="server" CssClass="control-label" />
                                <span style="color: red;">*</span>
                            </div>
                            <div class="col-sm-3">
                                 <asp:TextBox ID="txtDCJoinEffFrm" placeholder="DD/MM/YYYY" runat="server" CssClass="form-control" ></asp:TextBox>
                           <%-- onclick="callEffectiveDateFromDEFCOLJOIN()"--%>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblDCJoinEffTo" Text="Cease Date" runat="server" CssClass="control-label" />
                            </div>
                            <div class="col-sm-3">
                                 <asp:TextBox ID="txtDCJoinEffTo" placeholder="DD/MM/YYYY" runat="server" CssClass="form-control" ></asp:TextBox>
                               <%-- onclick="callEffectiveDateToDEFCOLJOIN()"--%>
                            </div>
                          </div>
                        <div class="row" style="margin-bottom: 5px;">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblDCJoinStatus" Text="Status" runat="server" CssClass="control-label" />
                                <span style="color: red;">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlDCJoinStatus" runat="server"  CssClass="form-control"
                                            TabIndex="4"  >
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>

                     <div id="div7" runat="server" class="row" style="margin-top: 12px;">
                        <div class="col-sm-12" >
                            <asp:LinkButton ID="lnkUPDDefColJoinbtn" runat="server" CssClass="btn btn-primary" style="display:none;"  TabIndex="17"  OnClientClick="return fnValidateDEFCOLJOIN();" OnClick="lnkUPDDefColJoinbtn_Click">  <%--OnClick="lnkUPDDefColJoinbtn_Click"--%>
                                <span class="glyphicon glyphicon-floppy-save" style="color: White;"></span> Update
                            </asp:LinkButton>

                            <asp:LinkButton ID="lnkDefColJoinbtn" runat="server" CssClass="btn btn-primary" style="display:inline-block;"  TabIndex="17"  OnClientClick="return fnValidateDEFCOLJOIN();" OnClick="lnkDefColJoinbtn_Click">  <%--OnClick="lnkDefColJoinbtn_Click"--%>
                                <span class="glyphicon glyphicon-floppy-save" style="color: White;"></span> Add
                            </asp:LinkButton>

                            <asp:LinkButton ID="lnkDefColJoinClrbtn" runat="server" CssClass="btn btn-danger" TabIndex="17" OnClick="lnkDefColJoinClrbtn_Click" >  <%--OnClick="lnkDefColJoinClrbtn_Click"--%>
                                <span class="glyphicon glyphicon-erase BtnGlyphicon" style="color: White;"></span> Clear
                            </asp:LinkButton>

                           
                        </div>
                     </div>
                        <br />
                     <div id="divGRIDDEFCOLJOIN" runat="server" style="width: 97%;" >
                        <asp:updatepanel runat="server">
                            <contenttemplate>
                                <asp:GridView ID="GridDefCol" runat="server" AutoGenerateColumns="false" Width="100%"
                            PageSize="10" AllowSorting="True" AllowPaging="true" CssClass="footable"  ShowHeader="true">
                                    <%--OnSorting="GridDefCol_Sorting" OnRowDataBound="GridDefCol_RowDataBound"--%>
                            <RowStyle CssClass="GridViewRow"></RowStyle>
                            <PagerStyle CssClass="disablepage" />
                            <HeaderStyle CssClass="gridview th" />
                            <Columns>
                                <asp:TemplateField HeaderText="Join ID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center"
                                    SortExpression="JN_ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDEFCOLJOINSEQNO" Text='<%# Bind("SEQ_NO") %>' visible="false" runat="server" />
                                        
                                        
                                        <asp:Label ID="lblDEFCOLJOINJSTATUS" Text='<%# Bind("STATUS") %>' visible="false" runat="server" />
                                        <asp:Label ID="lblDEFCOLJOINJSVACol" Text='<%# Bind("SET_VAL_AS_CLMN") %>' visible="false" runat="server" />
                                        <asp:Label ID="lblDEFCOLJOINJN_ID" Text='<%# Bind("JN_ID") %>'  runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" CssClass="clsCenter"/>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Synonym 1" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center"
                                    SortExpression="TBL_1_COL">
                                    <ItemTemplate>
                                        <asp:Label ID="lblJDEFCOLJOINTBL_1_COL" Text='<%# Bind("TBL_1_COL") %>' runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" CssClass="clsCenter"/>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Synonym 2" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center"
                                    SortExpression="TBL_2_COL">
                                    <ItemTemplate>
                                        <asp:Label ID="lblJDEFCOLJOINTBL_2_COL" Text='<%# Bind("TBL_2_COL") %>' runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" CssClass="clsCenter"/>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Join ID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center"
                                    SortExpression="INTGRTN_ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblJDEFCOLJOININTGRTN_ID" Text='<%# Bind("INTGRTN_ID") %>' runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" CssClass="clsCenter"/>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Effective Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center"
                                            SortExpression="EFF_DTIM">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDEFCOLJOINEFFDTIM" Visible="false" Text='<%# Bind("EFF_DTIM") %>'  runat="server" />
                                                <asp:Label ID="lblDEFCOLJOINEFFDTIMNEW" Text='<%# Bind("EFF_DTIM_NEW") %>'  runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" CssClass="clsCenter"/>
                                        </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Cease Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center"
                                            SortExpression="CSE_DTIM">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDEFCOLJOINCSEDTIM" Visible="false" Text='<%# Bind("CSE_DTIM") %>'  runat="server" />
                                                <asp:Label ID="lblDEFCOLJOINCSEDTIMNEW" Text='<%# Bind("CSE_DTIM_NEW") %>'  runat="server" />                                                
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" CssClass="clsCenter"/>
                                        </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                            <HeaderStyle CssClass="gridview th" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDEFCOLJOINEdit"  runat="server" Text="Edit"  ForeColor="#3333cc" OnClick="lnkDEFCOLJOINEdit_Click"></asp:LinkButton>  <%--OnClick="lnkDEFCOLJOINEdit_Click"--%>
                                            </ItemTemplate>
                                 </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                            <HeaderStyle CssClass="gridview th" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDEFCOLJOINDelete"  runat="server" Text="Delete" OnClientClick="return confirm('Are you sure you want to Delete?');" OnClick="lnkDEFCOLJOINDelete_Click" ForeColor="#3333cc"></asp:LinkButton>  <%--OnClick="lnkDEFCOLJOINDelete_Click"--%>
                                            </ItemTemplate>
                                 </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                            </contenttemplate>
                        </asp:updatepanel>
                      <%--  <div class="pagination" style="padding: 10px;">
                            <center>
                                <table>
                                    <tr>
                                        <td style="white-space: nowrap;">
                                            <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                                <ContentTemplate>
                                                    <asp:Button ID="btnGrdDEFWHERECondPrev" Text="<" CssClass="form-submit-button" runat="server"
                                                        Width="40px" Enabled="false" Style="border-style: solid; border-width: 1px; background-repeat: no-repeat;
                                                        background-color: transparent; float: left; margin: 0; height: 30px;" OnClick="btnGrdDEFWHERECondPrev_Click" />
                                                    <asp:TextBox runat="server" ID="txtPageDEFWCond" Text="1" Style="width: 50px; border-style: solid;
                                                        border-width: 1px; border-color: Gray; height: 30px; float: left; margin: 0;
                                                        text-align: center;" CssClass="form-control" ReadOnly="true" />
                                                    <asp:Button ID="btnGrdDEFWHERECondNext" Text=">" CssClass="form-submit-button" runat="server" Style="border-style: solid;
                                                        border-width: 1px; background-repeat: no-repeat; background-color: transparent;
                                                        float: left; margin: 0; height: 30px;" Width="40px" OnClick="btnGrdDEFWHERECondNext_Click" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </center>
                        </div>--%>
                     </div>
                 </div>    
               </div>

                                 <div id ="divDwhereCondition" runat="server" style="display:none" class="panel panel-success">
                                    <div id="Div6" runat="server" class="panel-heading" onclick="ShowReqDtl1('ctl00_ContentPlaceHolder1_divDEFWhereCond','myImgDwhereCondition');return false;">
                            <div class="row">
                                <div class="col-sm-10" style="text-align: left">
                                    <asp:Label ID="Label12" Text="Define Where Condition" Font-Size="19px" runat="server" />
                                </div>
                                <div class="col-sm-2">
                                    <span id="btnToggleNew2" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                                </div>
                            </div>
                    </div>
                    
                                    <div id="divDEFWhereCond" runat="server" style="padding: 30px;" class="panel-body">
                                        <div class="row" style="margin-bottom: 5px;">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblDEFWCondSynm" Text="Synonyms" runat="server" CssClass="control-label" />
                                <span style="color: red;">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlDEFWCondSynm" runat="server" AutoPostBack="true" CssClass="form-control" 
                                            TabIndex="4"  OnSelectedIndexChanged="ddlDEFWCondSynm_SelectedIndexChanged" > <%--OnSelectedIndexChanged="ddlDEFWCondSynm_SelectedIndexChanged"--%>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblDEFWCondColName" Text="Column Name" runat="server" CssClass="control-label" />
                                <span style="color: red;">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlDEFWCondColName" runat="server"  CssClass="form-control"
                                            TabIndex="4"  >
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                       </div>
                                        <div class="row" style="margin-bottom: 5px;">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblDEFWCondOptr" Text="Operator" runat="server" CssClass="control-label" />
                                <span style="color: red;">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlDEFWCondOptr" runat="server"  CssClass="form-control"
                                            TabIndex="4"  >
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblDEFWColVal" Text="Column Value" runat="server" CssClass="control-label" />
                                <span style="color: red;">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtDEFWColVal" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                            </div>
                       </div>
                                        <div class="row" style="margin-bottom: 5px;">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblDEFWEffFrm" Text="Effective From" runat="server" CssClass="control-label" />
                                <span style="color: red;">*</span>
                            </div>
                            <div class="col-sm-3">
                                 <asp:TextBox ID="txtDEFWEffFrm" placeholder="DD/MM/YYYY" runat="server" CssClass="form-control" ></asp:TextBox> <%--onclick="callEffectiveDateFromDEFWCond()"--%>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblDEFWEffTo" Text="Cease Date" runat="server" CssClass="control-label" />
                            </div>
                            <div class="col-sm-3">
                                 <asp:TextBox ID="txtDEFWEffTo" placeholder="DD/MM/YYYY" runat="server" CssClass="form-control" ></asp:TextBox> <%--onclick="callEffectiveDateToDEFWCond()"--%>
                            </div>
                          </div>
                                        <div class="row" style="margin-bottom: 5px;">
                            <div class="col-sm-3" style="text-align: left">
                                <asp:Label ID="lblDEFWstatus" Text="Status" runat="server" CssClass="control-label" />
                                <span style="color: red;">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlDEFWstatus" runat="server" CssClass="form-control"
                                            TabIndex="4"  >
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                       </div>
                                        <div id="divDEFWhereCondButtons" runat="server" class="row" style="margin-top: 12px;">
                                            <div class="col-sm-12" >
                                                 <asp:LinkButton ID="lnkUPDDWCondBtn" runat="server" CssClass="btn btn-primary" style="display:none;" TabIndex="17" OnClick="lnkUPDDWCondBtn_Click"  OnClientClick="return fnValidateDEFWHERECondtn();">  <%--OnClick="lnkUPDDWCondBtn_Click"--%>
                                                    <span class="glyphicon glyphicon-floppy-save" style="color: White;"></span> Update
                                                </asp:LinkButton>

                                                <asp:LinkButton ID="lnkDWCondBtn" runat="server" CssClass="btn btn-primary" style="display:inline-block;" TabIndex="17"  OnClientClick="return fnValidateDEFWHERECondtn();" OnClick="lnkDWCondBtn_Click"> <%--OnClick="lnkDWCondBtn_Click"--%>
                                                    <span class="glyphicon glyphicon-floppy-save" style="color: White;"></span> Save
                                                </asp:LinkButton>

                                                <asp:LinkButton ID="lnkDWCondclrbtn" runat="server" CssClass="btn btn-danger" TabIndex="17" OnClick="lnkDWCondclrbtn_Click" >  <%--OnClick="lnkDWCondclrbtn_Click"--%>
                                                    <span class="glyphicon glyphicon-erase BtnGlyphicon" style="color: White;"></span> Clear
                                                </asp:LinkButton>

                                                <asp:LinkButton ID="lnkDWCondCnclbtn" runat="server" CssClass="btn btn-danger" TabIndex="17" OnClientClick="JavaScript:window. history. back(1); return false;" >  <%--OnClick="lnkDWCondCnclbtn_Click"--%>
                                                    <span class="glyphicon glyphicon-remove BtnGlyphicon" style="color: White;"></span> Cancel
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <br />
                                        <div id="divGRIDDwhereCondition" runat="server" style="width: 97%;" >
                                            <asp:updatepanel runat="server">
                                                <contenttemplate>
                                                    <asp:GridView ID="gvDwhereCondition" runat="server" AutoGenerateColumns="false" Width="100%"
                                                PageSize="10" AllowSorting="True" AllowPaging="true" CssClass="footable"  ShowHeader="true">
                                                <%--        OnSorting="gvDwhereCondition_Sorting"  OnRowDataBound="gvDwhereCondition_RowDataBound"--%>
                                                <RowStyle CssClass="GridViewRow"></RowStyle>
                                                <PagerStyle CssClass="disablepage" />
                                                <HeaderStyle CssClass="gridview th" />
                                                     <Columns>
                                                         <asp:TemplateField HeaderText="Synonym Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center"
                                                        SortExpression="SYNM_NAME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDWCSynmName" Text='<%# Bind("SYNM_NAME") %>' runat="server" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" CssClass="clsCenter"/>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Column Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center"
                                                        SortExpression="WHR_CNDTN_COL_NAME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDWCSEQNO" Text='<%# Bind("SEQNO") %>' Visible="false" runat="server" />
                                                                <asp:Label ID="lblDWCEFF_DTIM" Text='<%# Bind("EFF_DTIM") %>' Visible="false" runat="server" />
                                                                <asp:Label ID="lblDWCCSE_DTIM" Text='<%# Bind("CSE_DTIM") %>' Visible="false" runat="server" />
                                                                <asp:Label ID="lblDWCColName" Text='<%# Bind("WHR_CNDTN_COL_NAME") %>' runat="server" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" CssClass="clsCenter"/>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Operator" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center"
                                                        SortExpression="WHR_CNDTN_OPRT">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDWCOPRTR" Text='<%# Bind("WHR_CNDTN_OPRT") %>' runat="server" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" CssClass="clsCenter"/>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Column Value" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center"
                                                        SortExpression="WHR_CNDTN_COL_VAL">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDWCCOlVAL" Text='<%# Bind("WHR_CNDTN_COL_VAL") %>' runat="server" />
                                                                <asp:Label ID="lblDWCStatus" Text='<%# Bind("STATUS") %>' Visible="false" runat="server" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" CssClass="clsCenter"/>
                                                        </asp:TemplateField>
                                                        
                                                        <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkDWCEdit" Text="Edit" runat="server" OnClick="lnkDWCEdit_Click" ForeColor="#3333cc"/>  <%--OnClick="lnkDWCEdit_Click" --%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkDWCDelete" Text="Delete" runat="server" OnClick="lnkDWCDelete_Click" OnClientClick="return confirm('Are you sure you want to Delete?');"  ForeColor="#3333cc"/>

                                                               <%-- OnClick="lnkDWCDelete_Click"--%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>     
                                                     </Columns>
                                            </asp:GridView>

                                                </contenttemplate>
                                            </asp:updatepanel>
                                          <%--  <div class="pagination" style="padding: 10px;">
                                            <center>
                                                <table>
                                                    <tr>
                                                        <td style="white-space: nowrap;">
                                                        <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Button ID="btnDwhereConditionPREV" Text="<" CssClass="form-submit-button" runat="server"
                                                                    Width="40px" Enabled="false" Style="border-style: solid; border-width: 1px; background-repeat: no-repeat;
                                                                    background-color: transparent; float: left; margin: 0; height: 30px;" OnClick="btnDwhereConditionPREV_Click" />
                                                                <asp:TextBox runat="server" ID="txtGVSWCPage" Text="1" Style="width: 50px; border-style: solid;
                                                                    border-width: 1px; border-color: Gray; height: 30px; float: left; margin: 0;
                                                                    text-align: center;" CssClass="form-control" ReadOnly="true" />
                                                                <asp:Button ID="btnDwhereConditionNEXT" Text=">" CssClass="form-submit-button" runat="server" Style="border-style: solid;
                                                                    border-width: 1px; background-repeat: no-repeat; background-color: transparent;
                                                                    float: left; margin: 0; height: 30px;" Width="40px" OnClick="btnDwhereConditionNEXT_Click" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </center>
                                            </div>--%>
                                        </div>
                                    </div>
                                 </div>                    
                             </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

</div>
                    </center>
                </div>
            </ContentTemplate>
            </asp:UpdatePanel>

<%--     <asp:Panel runat="server" Height="430px" Width="1090px" ID="Panel1" display="none" Style="text-align: center; top: 59px !important; padding: 5px; left: -2px !important; margin-left: -8px;" CssClass="panel panel-success">
        <iframe runat="server" id="Iframe1" scrolling="yes" width="100%" frameborder="0" display="none" style="height: 100%;"></iframe>
    </asp:Panel>
    <asp:Label runat="server" ID="lblpnl10" Style="display: none" />--%>
<%--    <ajaxToolkit:ModalPopupExtender runat="server" ID="ModalPopupExtender1" BehaviorID="mdlPopBIDHybrid"
        DropShadow="false" TargetControlID="lblpnl10" PopupControlID="Panel1" BackgroundCssClass="modalPopupBg" X="-4" Y="0">
    </ajaxToolkit:ModalPopupExtender>--%>



    <asp:HiddenField ID="hdnTabIndex" runat="server" />
    <asp:HiddenField ID="hdnSTFULSEQNO" runat="server" />

</asp:Content>
