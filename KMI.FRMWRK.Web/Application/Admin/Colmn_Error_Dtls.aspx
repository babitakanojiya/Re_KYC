<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="Colmn_Error_Dtls.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.Admin.Colmn_Error_Dtls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <%: Styles.Render("~/bundles/CKYCcss") %>
     <%: Scripts.Render("~/bundles/CKYCValidationjs") %>
    <link href="../../assets/css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../assets/js/bootstrap-multiselect.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">
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
</script>
    
     <style>
        .gridview th {
            padding: 3px;
            height: 40px;
            background-color: #dce9f9;
            /*color: #337ab7;*/
            text-align: center;
        }
        /*.disablepage
        {
             color: #3333CC;
        }*/

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



         <asp:ScriptManager ID="crsverhst" runat="server"></asp:ScriptManager>


     <asp:UpdatePanel runat="server">
        <ContentTemplate>
          <div class="page-container">
              <center>
                <div id="divfinhdrcollapse" class="panel panel-success" style="margin-left: 2%; margin-right: 2%; margin-top: 0.5%">
              <%--Megha--%>
                       <div class="panel-heading" onclick="ShowReqDtl('EmptyPagePlaceholder_divfinhdr','btnToggleNew');return false;">
                        <div class="row" style="margin: 0px">
                            <div class="col-sm-10" style="text-align: left">
                                <asp:Label ID="Label1" runat="server" Font-Bold="true" Font-Size="18px" Text="ERROR DETAILS"></asp:Label>
                            </div>
                            <div class="col-sm-2">
                                <span id="btnToggleNew" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                            </div>
                        </div>
                    </div>

                     <div id="divfinhdr" runat="server" style="width: 96%;" class="panel-body">

                           
                         <div id="divcrsvergrd" runat="server" style="width: 100%; border: none; margin: 0px 0 !important;" class="table-scrollable">
                      <div id="divGridMap" runat="server" style="width: 100%; overflow-x:scroll" >
                    
                <asp:UpdatePanel ID="UpdatePanelgrd" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grderrdt" runat="server" AutoGenerateColumns="false" PageSize="10" AllowSorting="True" AllowPaging="true"  CssClass="footable"  Width="100%" 
                            OnPageIndexChanging="grderrdt_PageIndexChanging">
                            <RowStyle CssClass="GridViewRowNEW"></RowStyle>
                            <PagerStyle ForeColor="#3333CC" />
                            <HeaderStyle CssClass="gridview th" />
                               <%--<EmptyDataTemplate>
                                  <asp:Label ID="Label2" Text="No Synonym have been Created" ForeColor="Red" CssClass="control-label" runat="server" />
                                </EmptyDataTemplate>--%>
                            <Columns>
                                  <asp:TemplateField HeaderText="Column Name"  SortExpression="ColumnName" ItemStyle-CssClass="text-center"  HeaderStyle-ForeColor
                                      ="#444444">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCOL"  runat="server" Text='<%# Bind("ColumnName")%>'></asp:Label>
                                        <asp:Label ID="lblsynSeqNo" Text='<%# Bind("RecId") %>' Visible="false" runat="server" />
                                    </ItemTemplate>                                                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Error Code"  SortExpression="ErrorCode" ItemStyle-CssClass="text-center"  HeaderStyle-ForeColor
                                      ="#444444">
                                    <ItemTemplate>
                                        <asp:Label ID="lblERRD"  runat="server" Text='<%# Bind("ErrorCode")%>'></asp:Label>
                                        </ItemTemplate>                                                                    
                                </asp:TemplateField>
                                 
                                <asp:TemplateField HeaderText="Error Description"  SortExpression="ErrorDesc" ItemStyle-CssClass="text-center"  HeaderStyle-ForeColor
                                      ="#444444">
                                    <ItemTemplate>
                                         <asp:Label ID="lblERRDESC" Text='<%# Bind("ErrorDesc") %>' Visible="true" runat="server" />
                                    </ItemTemplate>                                                                    
                                </asp:TemplateField>
                               
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                          </div>
                             
                         </div>
                         </div>
                         </div>

                    </div>
                  </center>
              </div>
                  </ContentTemplate>
       </asp:UpdatePanel>
</asp:Content>
