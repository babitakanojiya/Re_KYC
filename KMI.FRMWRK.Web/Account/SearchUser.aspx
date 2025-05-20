<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="SearchUser.aspx.cs" Inherits="KMI.FRMWRK.Web.Account.SearchUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ClearSearch() {
            document.getElementById("ctl00_ContentPlaceHolder1_txtUserName").value = "";
            document.getElementById("ctl00_ContentPlaceHolder1_txtUserID").value = "";

            var cboPolSts = document.getElementById("ctl00_ContentPlaceHolder1_cboStatus");
            cboPolSts.selectedIndex = 0;

            var cboShowRecord = document.getElementById("ctl00_ContentPlaceHolder1_cboReturnRecord");
            cboShowRecord.selectedIndex = 0;
        }

        function doBasicSearch() {
            //commented by darshana 5 July 2013
            //__doPostBack('ctl00$ContentPlaceHolder1_txtUserID','');
            //commented by darshana 5 July 2013   
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <center>
    <div>
     <div class="panel panel-success"  style ="margin-left:2%;margin-top:2%;margin-right:1%;">

     <div id="div1" runat="server" class="panel panel-heading" onclick="ShowReqDtl('Search','Span2');return false;">
            <div id="Td1" class="row" style="text-align: left; cursor: pointer">

                 <asp:Label ID="lblTitle" runat="server" Text="User Enquiry/User Sanction" Visible="false" CssClass="tableHeaderTitle"></asp:Label>
                  <asp:Label ID="Label2" runat="server" Text="User Enquiry/User Sanction" Visible="false"  CssClass="tableHeaderTitle"></asp:Label>
                
                
                 



                                      <div class="col-sm-10" style="text-align: left">
                                            <span class="glyphicon glyphicon-menu-hamburger"></span>&nbsp;User Enquiry/User Sanction

                                            <asp:Label ID="lblModVer" runat="server" Visible="false" Text="Admin/User Enquiry/User Sanction"
                                    CssClass="tableHeaderTitle"></asp:Label>
                                        </div>
                                        <div class="col-sm-2">
                                            <span id="Span2" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important;
                                                font-size: 18px;"></span>

                                                             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="Label3" runat="server" CssClass="control-lable" ></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                                     
                                        </div>

                </div>

                </div>
                <div id="Search" runat="server" style="text-align: center;" class="panel-body">
                    <div class="row">

                    <div class="col-sm-3" style="text-align: left">
                            <asp:Label ID="lblUserID" runat="server" CssClass="control-label"></asp:Label>
                        </div>

                          
                    <div class="col-sm-3" style="text-align: left">
                    <asp:TextBox ID="txtUserID" runat="server" CssClass="form-control"></asp:TextBox>
               </div> 
<div class="col-sm-3" style="text-align: left">
                            <asp:Label ID="lblUserName" runat="server" CssClass="control-lable"></asp:Label>
                        </div>

                        <div class="col-sm-3" style="text-align: left">
                    <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>
               </div>

                        <div class="col-sm-3" style="text-align: left">
                            <asp:Label ID="lblStatus" runat="server" CssClass="control-lable"></asp:Label>
                        </div>

                       <div class="col-sm-3" style="text-align: left">
                    <asp:DropDownList ID="cboStatus" runat="server" DataSourceID="SqlDataSource1" DataTextField="ParamName01"
                        DataValueField="paramValue" OnDataBound="cboStatus_DataBound" CssClass="form-control">
                    </asp:DropDownList>
                </div>

                        <div class="col-sm-3" style="text-align: left">
                           <asp:Label ID="lblReturnRecord" runat="server" CssClass="control-lable"></asp:Label>
                        </div>

                       <div class="col-sm-3" style="text-align: left">
                    <asp:DropDownList ID="cboReturnRecord" runat="server" CssClass="form-control">
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>30</asp:ListItem>
                    </asp:DropDownList>
              </div>
                    </div>
                      <div class="row">
                      <center>
                         <asp:LinkButton ID="btnSearchAdv"  OnClientClick="return doBasicSearch();"
                                                        CssClass="btn btn-primary" runat="server">
                                    <span class="glyphicon glyphicon-search BtnGlyphicon"></span> Search
                                                    </asp:LinkButton>
                       <asp:LinkButton ID="btnHClear" OnClientClick="return ClearSearch();"   
                                                        CssClass="btn btn-primary" runat="server">
                                    <span class="glyphicon glyphicon-erase BtnGlyphicon"></span> Clear
                                                    </asp:LinkButton>
                    </center>
                      
                      </div>
                      <br />
                 <div class="panel-heading subHeader" onclick="ShowReqDtl('div3','Span1');return false;">
                                    <div class="row">
                                        <div class="col-sm-10" style="text-align: left">
                                            <span class="glyphicon glyphicon-menu-hamburger"></span>&nbsp;Search Result
                                        </div>
                                        <div class="col-sm-2">
                                            <span id="Span1" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important;
                                                font-size: 18px;"></span>
                                                  <asp:UpdatePanel ID="UpdatePanel" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblPageIndex" runat="server" CssClass="control-lable" Text="1"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                                        </div>
                                    </div>
</div>
                    <div class="row">

                       <div class="col-sm-12" >
                    <asp:UpdatePanel ID="upd1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False"
                                AllowSorting="True" Width="100%" CellPadding="1" CssClass="footable" 
                                AllowPaging="True" GridLines="Vertical" OnPageIndexChanging="gvResult_PageIndexChanging">
                                <EmptyDataTemplate>
                                    <center>
                                        <asp:Label ID="lblNoRecord" runat="server" CssClass="control-lable" Text="No Record Found"></asp:Label>
                                    </center>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:HyperLinkField DataTextField="UserId" SortExpression="UserId" ItemStyle-CssClass="control-lable" ControlStyle-ForeColor="Black"
                                        ItemStyle-HorizontalAlign="Left" DataNavigateUrlFields="UserId" DataNavigateUrlFormatString="~/Account/RegisterUser.aspx?mode=edit&amp;userid={0}"
                                        HeaderText="User ID" />
                                    <asp:BoundField DataField="UserName1" SortExpression="UserName1" HeaderText="User Name"
                                        HeaderStyle-CssClass="control-lable" ItemStyle-CssClass="control-lable" ItemStyle-HorizontalAlign="Left"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Status" SortExpression="UserStatus" HeaderStyle-CssClass="control-lable">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" Text='<%# Bind("UserStatus") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" CssClass="control-lable" Text='<%# Bind("UserStatusDesc") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                  <HeaderStyle HorizontalAlign ="Center" BackColor="#dce9f9" />
                     <FooterStyle CssClass="GridViewFooter" />
                    <RowStyle CssClass="GridViewRow" />
                   
                    <SelectedRowStyle CssClass="GridViewSelectedRow" />
                    <AlternatingRowStyle CssClass="GridViewAlternateRow"></AlternatingRowStyle>
                               
                                <%-- <AlternatingRowStyle CssClass="GridViewAlternateRow"></AlternatingRowStyle>--%>
                                <PagerTemplate>
                                    <table class="tablePager" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td class="tablePagerDataSmall" align="left" style="width: 11%">
                                                <asp:Label ID="lblpageindx" CssClass="standardlabelCRM" Text="Page : " runat="server"></asp:Label>
                                            </td>
                                            <td align="center" class="tablePagerData" style="width: 98%">
                                                <table cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <span style="padding-left: 5px;"></span>
                                                            <asp:ImageButton ToolTip="First Page" CommandName="Page" CommandArgument="First"
                                                                runat="server" ID="ImgbtnFirst1" ImageUrl="~/assets/css/Images/ImgArrFirst.gif" />
                                                        </td>
                                                        <td>
                                                            <span style="padding-left: 5px;"></span>
                                                            <asp:ImageButton ToolTip="Previous Page" CommandName="Page" CommandArgument="Prev"
                                                                runat="server" ID="ImgbtnPrevious1" ImageUrl="~/assets/css/Images/ImgArrPrevious.gif" />
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
                                                                ID="ImgbtnNext1" ImageUrl="~/assets/css/Images/ImgArrNext.gif" />
                                                        </td>
                                                        <td>
                                                            <span style="padding-left: 5px;"></span>
                                                            <asp:ImageButton ToolTip="Last Page" CommandName="Page" CommandArgument="Last" runat="server"
                                                                ID="ImgbtnLast1" ImageUrl="~/assets/css/Images/ImgArrLast.gif" />
                                                         </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </PagerTemplate>
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtUserID"  />
                            <asp:AsyncPostBackTrigger ControlID="txtUserName"  />
                            <asp:AsyncPostBackTrigger ControlID="cboStatus"  />
                            <asp:AsyncPostBackTrigger ControlID="cboReturnRecord" />
                        </Triggers>
                    </asp:UpdatePanel>
                    </div>
                    </div>
            </div>
             </div>  
             </div>
    </center>
   <%-- <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetSearchUserBy" TypeName="KMI.FRMWRK.Web.Admin.AdminDAL">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtUserID" DefaultValue="" Name="UserId" PropertyName="Text"
                Type="String" />
            <asp:ControlParameter ControlID="txtUserName" Name="UserName" PropertyName="Text"
                Type="String" />
            <asp:ControlParameter ControlID="cboStatus" Name="UserStatus" PropertyName="SelectedValue"
                Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>--%>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConn %>"
        SelectCommand="select * from ST_KsysLookupParam where lookupcode='userstat'" SelectCommandType="Text"></asp:SqlDataSource>
</asp:Content>
