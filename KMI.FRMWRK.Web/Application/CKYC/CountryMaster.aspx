<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="CountryMaster.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.CKYC.CountryMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <%: Scripts.Render("~/bundles/CKYCValidationjs") %>
    <%: Styles.Render("~/bundles/CKYCcss") %>
    <style type="text/css">
        table > thead > tr > th {
            background-image: linear-gradient(to bottom,#d6d6c2,#d6d6c2) !important;
            text-align: center !Important;
        }

        .AlignCenter {
            text-align: center !Important;
        }
    </style>
    <script type="text/javascript">

        function ShowReqDtl(divName, btnName) {
            debugger;
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

        //function AlertMsg(msg) {
        //    debugger;
        //    showModal('#myModal', 'Information', 'alert-warning', '', '', msg);
        //}
    </script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />

    <!-- jQuery library -->
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

    <!-- Latest compiled JavaScript -->
    <script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">
<%-- <form id="form1" runat="server">--%>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <%--EnablePageMethods="true"--%>
    <asp:UpdatePanel ID="updMstState" runat="server" RenderMode="Inline" UpdateMode="Conditional">
        <ContentTemplate>
            <br />
            <div class="panel panel-success ">
                <div id="divMstState" runat="server" class="panel-heading" onclick="ShowReqDtl('EmptyPagePlaceholder_divSearch','btnMstIdentity');return false;">
                    <div class="row">
                        <div class="col-sm-10">
                            <span class="glyphicon glyphicon-menu-hamburger" style="margin-right: 5px; color: Orange;"></span>Search
                            Criteria - Country Master
                        </div>
                        <div class="col-sm-2">
                            <span id="btnMstIdentity" class="glyphicon glyphicon-collapse-down" style="float: right; color: Orange; padding: 1px 10px ! important; font-size: 18px;"></span>
                        </div>
                    </div>
                </div>
                <div id="divSearch" runat="server" class="panel-body" style="display: block;">
                    <div class="row">
                        <div class="col-sx-12 col-sm-6 col-md-6 col-lg-6">
                            <div class="form-group form-group-Grp">
                                <div class="col-md-6">
                                    <asp:Label ID="lblCountryCode" Text="Country Code" runat="server" CssClass="control-label" />
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtCountryCode" runat="server" CssClass="form-control" TabIndex="1"
                                        MaxLength="4" placeholder="Country Code" />
                                </div>
                            </div>
                        </div>
                        <div class="col-sx-12 col-sm-6 col-md-6 col-lg-6">
                            <div class="form-group form-group-Grp">
                                <div class="col-md-6">
                                    <asp:Label ID="lblCountryDesc" Text="Country Desc" runat="server" CssClass="control-label" />
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtCountryDesc" runat="server" CssClass="form-control" TabIndex="2"
                                        MaxLength="100" placeholder="Country Desc" />
                                </div>
                            </div>
                        </div>
                        <br />
                        <br />
                        <div class="col-sx-12 col-sm-6 col-md-6 col-lg-6">
                            <div class="form-group form-group-Grp">
                                <div class="col-md-6">
                                    <asp:Label ID="lblActiveChk" Text="Is Active" runat="server" CssClass="control-label" />
                                </div>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlActiveChk" runat="server" CssClass="form-control" TabIndex="3">
                                        <asp:ListItem Value="">Select</asp:ListItem>
                                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                                        <asp:ListItem Value="N">No</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-sx-12 col-sm-6 col-md-6 col-lg-6">
                            <div class="form-group form-group-Grp">
                                <div class="col-md-6">
                                    <asp:Label ID="lblPageSize" runat="server" Text="#of records to be shown on page"
                                        CssClass="standardlabel"></asp:Label>
                                </div>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="form-control" AppendDataBoundItems="false"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlPageSize_OnSelectedIndexChanged"
                                        TabIndex="4">
                                        <asp:ListItem Value="0" Selected="True">10</asp:ListItem>
                                        <asp:ListItem Value="1">20</asp:ListItem>
                                        <asp:ListItem Value="2">40</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-12" align="center">
                            <div class="col-sm-12" align="center">
                                <asp:LinkButton ID="btnSearch" CssClass="btn btn-primary" OnClick="btnSearch_Click"
                                    runat="server" TabIndex="5">
                                 <span class="glyphicon glyphicon-search BtnGlyphicon"> </span> Search
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnClear" OnClick="btnClear_Click" CssClass="btn btn-primary"
                                    TabIndex="6" runat="server">
                                 <span class="glyphicon glyphicon-erase BtnGlyphicon"> </span> Clear
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnCancel" OnClick="btnCancel_Click" CssClass="btn btn-danger"
                                    runat="server" TabIndex="7">
                                <span class="glyphicon glyphicon-remove BtnGlyphicon"> </span> Cancel
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <br />
                </div>
            </div>
            <div id="divLnkCustom" runat="server" class="row">
                <div class="col-sm-12">
                    <asp:LinkButton ID="lnkCustomFields" runat="server" class="btn-link" Visible="true"
                        Text="Show custom fields" OnClick="lnkCustomFields_Click" Style="float: right; padding: 1px 27px 17px ! important;">
                    </asp:LinkButton>
                </div>
            </div>
            <div class="panel panel-success ">
                <div id="divMstStateSearch" runat="server" class="panel-heading" onclick="ShowReqDtl('EmptyPagePlaceholder_DivGrdActMSt','btnMstStateSearch');return false;">
                    <div class="row">
                        <div class="col-sm-10">
                            <span class="glyphicon glyphicon-menu-hamburger" style="margin-right: 5px; color: Orange;"></span>Search
                            Result
                        </div>
                        <div class="col-sm-2">
                            <span id="btnMstStateSearch" class="glyphicon glyphicon-collapse-down" style="float: right; color: Orange; padding: 1px 10px ! important; font-size: 18px;"></span>
                        </div>
                    </div>
                </div>
                <div id="DivGrdActMSt" runat="server" style="display:block; overflow: auto;">
                    <asp:GridView ID="gvMstStateSearch" runat="server" AutoGenerateColumns="false"
                        PageSize="10" AllowPaging="True" AllowSorting="True" DataKeyNames="SeqNo"
                        OnRowDataBound="OnRowDataBound" OnSorting="gvMstStateSearch_Sorting" OnRowEditing="OnRowEditing"
                        OnRowCancelingEdit="OnRowCancelingEdit" OnPageIndexChanging="gvMstStateSearch_PageIndexChanging"
                        OnRowUpdating="OnRowUpdating" OnRowDeleting="OnRowDeleting" EmptyDataText="No records has been added."
                        ShowFooter="false" CssClass="footable" OnRowCreated="gvMstStateSearch_RowCreated"
                        Width="1400px">
                        <HeaderStyle ForeColor="Black" />
                        <Columns>
                            <%--<asp:BoundField DataField="SeqNo" HeaderText="SrNo" ReadOnly="true" Visible="false" />--%>
                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="AlignCenter">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkAlls_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSoftLock" runat="server" AutoPostBack="true" />
                                </ItemTemplate>
                                <ItemStyle Width="2%" CssClass="AlignCenter" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="SeqNo" HeaderStyle-CssClass="AlignCenter"
                                SortExpression="SeqNo">
                                <ItemTemplate>
                                    <asp:Label ID="lblSeqNo" runat="server" Text='<%# Eval("SeqNo") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtSeqNo" Enabled="false" runat="server" CssClass="form-control AlignCenter"
                                        Text='<%# Eval("SeqNo") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblSeqNoFt" runat="server" CssClass="AlignCenter"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle CssClass="AlignCenter" />
                                <ItemStyle Width="5%" CssClass="AlignCenter" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Country Code" HeaderStyle-CssClass="AlignCenter"
                                SortExpression="Country_CODE">
                                <ItemTemplate>
                                    <asp:Label ID="lblCountryCode" runat="server" Text='<%# Eval("Country_CODE") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblCountryCode" runat="server" Visible="false" Text='<%# Eval("Country_CODE") %>'></asp:Label>
                                    <asp:TextBox ID="txtCountryCode" runat="server" onkeypress="EnterKeyUpd(this.id)"
                                        MaxLength="100" CssClass="form-control" Style="background-color: #FFE5B4" Text='<%# Eval("Country_CODE") %>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                                
                                <FooterTemplate>
                                    <asp:Label ID="lblCountryCodeFt" runat="server" CssClass="AlignCenter"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle CssClass="AlignCenter" />
                                <ItemStyle Width="5%" CssClass="AlignCenter" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Country Desc" HeaderStyle-CssClass="AlignCenter"
                                SortExpression="Country_Desc">
                                <ItemTemplate>
                                    <asp:Label ID="lblCountryDesc" runat="server" Text='<%# Eval("Country_Desc") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCountryDesc" runat="server" 
                                         CssClass="form-control"  Text='<%# Eval("Country_Desc") %>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblCountryDescFt" runat="server" CssClass="AlignCenter"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle Width="9%" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Other CountryCodeFI" HeaderStyle-CssClass="AlignCenter"
                                SortExpression="OtherCountryCodeFI">
                                <ItemTemplate>
                                    <asp:Label ID="lblOtherCountryCodeFI" runat="server" Text='<%# Eval("OtherCountryCodeFI") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtOtherCountryCodeFI" runat="server" 
                                         CssClass="form-control"  Text='<%# Eval("OtherCountryCodeFI") %>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblOtherCountryCodeFIFt" runat="server" CssClass="AlignCenter"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle Width="5%" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Other CountryCode2" HeaderStyle-CssClass="AlignCenter"
                                SortExpression="OtherCountryCode2">
                                <ItemTemplate>
                                    <asp:Label ID="lblOtherCountryCode2" runat="server" Text='<%# Eval("OtherCountryCode2") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtOtherCountryCode2" runat="server" 
                                         CssClass="form-control"  Text='<%# Eval("OtherCountryCode2") %>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblOtherCountryCode2Ft" runat="server" CssClass="AlignCenter"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle Width="5%" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="ChngFlag" HeaderStyle-CssClass="AlignCenter"
                                SortExpression="ChngFlag">
                                <ItemTemplate>
                                    <asp:Label ID="lblChngFlag" runat="server" Text='<%# Eval("ChngFlag") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtChngFlag" Enabled="false" runat="server" CssClass="form-control AlignCenter"
                                        Text='<%# Eval("ChngFlag") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblChngFlagFt" runat="server" CssClass="AlignCenter"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle CssClass="AlignCenter" />
                                <ItemStyle Width="5%" CssClass="AlignCenter" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Created By" HeaderStyle-CssClass="AlignCenter"
                                SortExpression="CreatedBy">
                                <ItemTemplate>
                                    <asp:Label ID="lblCreatedBy" runat="server" Text='<%# Eval("CreatedBy") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCreatedBy" Enabled="false" runat="server" CssClass="form-control AlignCenter"
                                        Text='<%# Eval("CreatedBy") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblCreatedByFt" runat="server" CssClass="AlignCenter"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle CssClass="AlignCenter" />
                                <ItemStyle Width="5%" CssClass="AlignCenter" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Created Dtim" HeaderStyle-CssClass="AlignCenter"
                                SortExpression="CreatedDtim">
                                <ItemTemplate>
                                    <asp:Label ID="lblCreatedDtim" runat="server" Text='<%# Eval("CreatedDtim") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCreatedDtim" Enabled="false" runat="server" CssClass="form-control AlignCenter"
                                        Text='<%# Eval("CreatedDtim") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblCreatedDtimFt" runat="server" CssClass="AlignCenter"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle CssClass="AlignCenter" />
                                <ItemStyle Width="5%" CssClass="AlignCenter" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Updated By" HeaderStyle-CssClass="AlignCenter"
                                SortExpression="UpdatedBy">
                                <ItemTemplate>
                                    <asp:Label ID="lblUpdatedBy" runat="server" Text='<%# Eval("UpdatedBy") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtUpdatedBy" Enabled="false" runat="server" CssClass="form-control AlignCenter"
                                        Text='<%# Eval("UpdatedBy") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblUpdatedByFt" runat="server" CssClass="AlignCenter"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle CssClass="AlignCenter" />
                                <ItemStyle Width="5%" CssClass="AlignCenter" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Updated Dtim" HeaderStyle-CssClass="AlignCenter"
                                SortExpression="updatedDtim">
                                <ItemTemplate>
                                    <asp:Label ID="lblupdatedDtim" runat="server" Text='<%# Eval("updatedDtim") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtupdatedDtim" Enabled="false" runat="server" CssClass="form-control AlignCenter"
                                        Text='<%# Eval("updatedDtim") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblupdatedDtimFt" runat="server" CssClass="AlignCenter"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle CssClass="AlignCenter" />
                                <ItemStyle Width="5%" CssClass="AlignCenter" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderStyle-CssClass="AlignCenter" HeaderText="Actions">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbEdit"  Enabled="false" runat="server" ShowEditButton="true" CommandName="Edit"
                                        Style="padding-left: 17%;" Text="Edit">
                                    </asp:LinkButton>
                                    <span id="spnEdit" runat="server">&#124;</span>
                                    <asp:LinkButton ID="lnkDeleteGrd" Enabled="false"  runat="server" OnClientClick="return DelConfirm(this);"
                                        Text="Delete">  
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lbUpdate" runat="server" CommandName="Update" Text="Update" Style="padding-left: 8%;">
                                    </asp:LinkButton>
                                    <span id="spnUpdate" runat="server">&#124;</span>
                                    <asp:LinkButton ID="lnkCancelBtn" runat="server" CommandName="Cancel" Text="Cancel" />
                                </ItemTemplate>
                                <ItemStyle CssClass="AlignCenter" />
                                <ItemStyle CssClass="AlignCenter" Width="6%" />
                                <FooterTemplate>
                                    <asp:LinkButton ID="lnkSave" runat="server" OnClick="lnkSave_Click" Text="Save" Style="padding-left: 14%;" />
                                    <span id="SpnSave" runat="server">&#124;</span>
                                    <asp:LinkButton ID="lnkCancel" runat="server" CommandName="Cancel" Text="Cancel" />
                                </FooterTemplate>
                                <FooterStyle CssClass="AlignCenter" />
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
                                                        runat="server" ID="ImgbtnFirst" ImageUrl="../../Content/Images/ImgArrFirst.gif"/>
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
                    <asp:Button ID="DeleteButn" runat="server" Style="display: none;" OnClick="DeleteButn_Click"></asp:Button>
                    <asp:Button ID="btnUpdateClick" runat="server" Style="display: none;"></asp:Button>
                </div>
                <br />
                <div class="row" style="padding-left: 30px;">
                    <span id="spanNote" style="color: Red;"><b>Note:</b> In case any value in the grid is
                        not completely visible (i.e. displayed with a suffix ...) due to the width of the
                        column, please use mouse hover on that cell to display the full text in tooltip.</span>
                </div>
                <br />
                <div class="row">
                    <div class="col-sm-12" align="center">
                        <asp:LinkButton ID="btnAddNew" runat="server" OnClick="btnAddNew_Click" CssClass="btn btn-primary"
                            TabIndex="8" Enabled="true">   
                                <span class="glyphicon glyphicon-plus BtnGlyphicon"> </span> Add New
                        </asp:LinkButton>
                        <asp:LinkButton ID="lnkDelete" runat="server" CssClass="btn btn-danger" OnClick="lnkDelete_Click"
                            TabIndex="9" Enabled="false">    
                                <span class="glyphicon glyphicon-remove BtnGlyphicon"> </span> Delete
                        </asp:LinkButton>
                    </div>
                </div>
                <br />
            </div>
            <div class="row" style="text-align: center;">
                <asp:Label ID="lblErr" runat="server" ForeColor="Red" Text="" Visible="false"></asp:Label>
            </div>
            <br />
            <div id="dvMstRef" runat="server" class="panel panel-success" visible="false">
                <div id="dvHdrRef" runat="server" class="panel-heading" onclick="ShowReqDtl('dvHdrRefSrch','Span1');return false;">
                    <div class="row">
                        <div class="col-sm-10">
                            <span class="glyphicon glyphicon-menu-hamburger" style="margin-right: 5px; color: Orange;"></span>Master
                            References
                        </div>
                        <div class="col-sm-2">
                            <span id="Span1" class="glyphicon glyphicon-collapse-down" style="float: right; color: Orange; padding: 1px 10px ! important; font-size: 18px;"></span>
                        </div>
                    </div>
                </div>
                <div id="dvHdrRefSrch" runat="server" class="panel-body">
                    <div class="row" style="text-align: center;">
                        <asp:Label ID="lblRefAlert" runat="server" ForeColor="Red" Text="" Visible="false"></asp:Label>
                    </div>
                </div>
            </div>
            </div>
           
            <asp:HiddenField ID="hdnIsActive" runat="server" />
            <asp:HiddenField ID="hdnCheck" runat="server" />
            <asp:HiddenField ID="hdnrow" runat="server" />
            <asp:HiddenField ID="hdnPrimaryColumn" runat="server" />
            <asp:HiddenField ID="hdnFlag" runat="server" Value="N" />
            <asp:HiddenField ID="hdnPrimaryValue" runat="server" />
            <asp:HiddenField ID="hdnActive" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

    
    <%--</form>--%>
</asp:Content>

