<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="Job_Run_Status.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.Admin.Job_Run_Status" EnableEventValidation="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <%: Styles.Render("~/bundles/CKYCcss") %>

    <link href="../../assets/css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../assets/js/bootstrap-multiselect.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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

        .gvtCenter {
            text-align: center !important;
        }

        .IMGDWNLALLERR {
            text-align: left !important;
        }

        .ui-progressbar {
            position: relative;
        }

        .progress-bar {
            position: absolute;
            left: 50%;
            top: 4px;
            font-weight: bold;
            text-shadow: 1px 1px 0 #fff;
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

    </style>

    <script>
        $("[src*=btnexp]").live("click", function () {
            debugger;
            $(this).closest("tr").after("<tr><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "../../images/btncol.png");

        });
        $("[src*=btncol]").live("click", function () {
            debugger;
            $(this).attr("src", "../../images/btnexp.png");
            $(this).closest("tr").next().remove();
        });

        //function StateCity(input) {
        //    debugger;
        //    var displayIcon = "img" + input;
        //    if ($("#" + displayIcon).hasClass("btnexp")) {
        //        $("#" + displayIcon).closest("tr")
        //        .after("<tr><td></td><td colspan = '100%'>" + $("#" + input)
        //        .html() + "</td></tr>");
        //        $("#" + displayIcon).removeClass("btnexp").addClass("btncol");
        //    } else {
        //        $("#" + displayIcon).closest("tr").next().remove();
        //        $("#" + displayIcon).removeClass("btncol").addClass("btnexp");
        //    }
        //}

        $(document).ready(function () {
            fnSetTabs('1');
        });

        function fnSetTabs(strTabIndex) {
            debugger;
            if (strTabIndex == '1') {
                document.getElementById('EmptyPagePlaceholder_divstartjb').style.display = "block";
                document.getElementById('EmptyPagePlaceholder_divjbhst').style.display = "none";
                document.getElementById("lisje").className = "active";
                document.getElementById("liJH").classList.remove("active");
                document.getElementById('EmptyPagePlaceholder_hdnTabIndex').value = "1";
            }
            if (strTabIndex == '2') {
                document.getElementById('EmptyPagePlaceholder_divstartjb').style.display = "none";
                document.getElementById('EmptyPagePlaceholder_divjbhst').style.display = "block";
                document.getElementById("lisje").classList.remove("active");
                document.getElementById("liJH").className = "active";
                document.getElementById('EmptyPagePlaceholder_hdnTabIndex').value = "2";
            }
        }
    </script>


    <asp:ScriptManager ID="scrusdtls" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="uex" runat="server">
        <ContentTemplate>
            <div class="page-container">

                <div class="row" style="width: 100%;">
                    <div class="col-md-12">
                        <div class="card">
                            <ul id="myTab" class="nav nav-tabs">
                                <li id="lisje"><a id="tabsje" onclick="return fnSetTabs('1');" style="font-weight: bold;">Start Job Execution </a></li>
                                <li id="liJH"><a id="tabJH" onclick="return fnSetTabs('2');" style="font-weight: bold;">Job History</a></li>
                            </ul>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                                <ContentTemplate>
                                    <div id="divstartjb" class="tab-pane active" runat="server" style="display: none">
                                        <div id="Div1" runat="server" class="panel-heading" onclick="ShowReqDtl1('ctl00_ContentPlaceHolder1_divgrdjbexec,'');return false;">

                                            <%-- <div class="row">
                                <div class="col-sm-10" style="text-align: left">
                                    <asp:Label ID="lblRulSrch" Text="Define Join" Font-Size="19px" runat="server" />
                                </div>
                                <div class="col-sm-2">
                                    <span id="myImgDJoin" class="glyphicon glyphicon-menu-hamburger" style="float: right; color: #034ea2;
                                        padding: 1px 10px ! important; font-size: 18px;"></span>
                                </div> 
                            </div>--%>
                                        </div>

                                        <div id="divgrdjbexec" runat="server" style="width: 100%; border: none; margin: 0px 0 !important; overflow: auto;" class="table-scrollable">

                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:GridView ID="dgprntgrd" runat="server" CssClass="footable" AllowSorting="True" PageSize="10" AllowPaging="true" AutoGenerateColumns="false"
                                                        DataKeyNames="INTGRTN_ID, PRCS_ID" BorderColor="#d0d0d0" OnRowDataBound="dgprntgrd_RowDataBound">
                                                        <%--OnRowDataBound="dgprntgrd_RowDataBound"--%>
                                                        <RowStyle CssClass="GridViewRowNew"></RowStyle>
                                                        <PagerStyle CssClass="disablepage" />
                                                        <HeaderStyle CssClass="gridview th" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-BackColor="#ed1c24" ItemStyle-BackColor="#f4e5cd" HeaderStyle-BorderColor="#d0d0d0" ItemStyle-BorderColor="#d0d0d0">
                                                                <ItemTemplate>
                                                                    <img alt="" id="img2" style="cursor: pointer" src="../../Images/btnexp.png"  />


                                                                    <div id="divChild" runat="server" style="display: none; width: auto; margin: 0px 0 !important;"
                                                                        class="table-scrollable">
                                                                        <asp:UpdatePanel ID="UpdatePanel61" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:GridView ID="dgchild" runat="server" AutoGenerateColumns="false" Width="100%"
                                                                                    PageSize="10" AllowSorting="False" AllowPaging="true" CssClass="footable" HeaderStyle-BorderColor="White">
                                                                                    <%-- OnRowDataBound="dgchild_RowDataBound"--%>
                                                                                    <RowStyle CssClass="GridViewRowNew"></RowStyle>
                                                                                    <PagerStyle CssClass="disablepage" />
                                                                                    <HeaderStyle CssClass="gridview th" />

                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Sub Process Description" HeaderStyle-HorizontalAlign="Left" SortExpression="SUB_PRC_DESC"
                                                                                            HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#ffc7c9" HeaderStyle-ForeColor="#ac080e" HeaderStyle-BorderColor="#d0d0d0" ItemStyle-BorderColor="#d0d0d0">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblsbprcdesccld" Text='<%# Bind("SUB_PRC_DESC") %>' runat="server" />
                                                                                            </ItemTemplate>

                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Sub Process ID" HeaderStyle-HorizontalAlign="Left" SortExpression="SUB_PRC_ID"
                                                                                            HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#ffc7c9" HeaderStyle-ForeColor="#ac080e" HeaderStyle-BorderColor="#d0d0d0" ItemStyle-BorderColor="#d0d0d0">
                                                                                            <ItemStyle CssClass="gvtCenter" />
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblsbprcid" Text='<%# Bind("SUB_PRC_ID") %>' runat="server" />
                                                                                                <asp:Label ID="LBLSUBPRCIDVAL" Text='<%# Bind("SUBPRCID1") %>' runat="server" Visible="false" />
                                                                                                <asp:Label ID="lblprcid" Text='<%# Bind("PRCS_ID") %>' runat="server" Visible="false" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Run Date" HeaderStyle-HorizontalAlign="Left" SortExpression="RUN_DT"
                                                                                            HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#ffc7c9" HeaderStyle-ForeColor="#ac080e" HeaderStyle-BorderColor="#d0d0d0" ItemStyle-BorderColor="#d0d0d0">
                                                                                            <ItemStyle CssClass="gvtCenter" />
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblsbrndt" Text='<%# Bind("RUN_DT") %>' runat="server" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Execution Status" HeaderStyle-HorizontalAlign="Left" SortExpression="EXEC_STATUS"
                                                                                            HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#ffc7c9" HeaderStyle-ForeColor="#ac080e" HeaderStyle-BorderColor="#d0d0d0" ItemStyle-BorderColor="#d0d0d0">
                                                                                            <ItemStyle CssClass="gvtCenter" />
                                                                                            <ItemTemplate>
                                                                                                <%-- <asp:Label ID="lblsbexecstts" Text='<%# Bind("EXEC_STATUS_VAL") %>' runat="server" Visible="false" />--%>
                                                                                                <img alt="" id="imgrnning" style="cursor: pointer" src='<%# Eval("ImagePath") %>' runat="server" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Re-Run Count" HeaderStyle-HorizontalAlign="Left" SortExpression="RUN_DT"
                                                                                            HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#ffc7c9" HeaderStyle-ForeColor="#ac080e" HeaderStyle-BorderColor="#d0d0d0" ItemStyle-BorderColor="#d0d0d0">
                                                                                            <ItemStyle CssClass="gvtCenter" />
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblrncnt" Text='<%# Bind("RERUN_CNT") %>' runat="server" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Left"
                                                                                            HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#ffc7c9" HeaderStyle-ForeColor="#ac080e" HeaderStyle-BorderColor="#d0d0d0" ItemStyle-BorderColor="#d0d0d0">
                                                                                            <ItemStyle CssClass="gvtCenter" />
                                                                                            <ItemTemplate>
                                                                                                <%--<asp:LinkButton ID="lnkbtnrn"  runat="server" Text='Run'  ></asp:LinkButton>--%>
                                                                                                <%--<img alt="" id="imgbtnrn"  style="cursor: pointer" src="../../../../images/btn_rerun.png"  />--%>
                                                                                                <asp:UpdatePanel ID="uprn" runat="server">
                                                                                                    <ContentTemplate>
                                                                                                        <asp:ImageButton ID="imgrun" ImageUrl="../../Images/btn_rerun.png" runat="server" OnClick="imgrun_Click"  />
                                                                                                        <%--OnClick="imgrun_Click" --%>
                                                                                                    </ContentTemplate>
                                                                                                </asp:UpdatePanel>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Error Description" HeaderStyle-HorizontalAlign="Center"
                                                                                            HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="right" HeaderStyle-BackColor="#ffc7c9" HeaderStyle-ForeColor="#ac080e"
                                                                                            HeaderStyle-BorderColor="#d0d0d0" ItemStyle-BorderColor="#d0d0d0">
                                                                                            <%--<ItemStyle CssClass="gvtCenter" />--%>
                                                                                            <ItemTemplate>
                                                                                                <asp:UpdatePanel ID="updact" runat="server">
                                                                                                    <ContentTemplate>
                                                                                                        <%--<asp:LinkButton ID="lnkdwnld"  runat="server" Text='Download Error Records' OnClick="lnkdwnld_Click" Visible="false" ClientIDMode="Static" ></asp:LinkButton>--%> <%--Visible='<%# Eval("SUBPRCID1").ToString() == "102" ? true : false %>' --%>

                                                                                                        <asp:ImageButton ID="imgdwnld" ImageUrl="../../Images/icon_error_record.png" Visible='<%# Eval("SUBPRCID1").ToString() == "103"? true : false %>' runat="server" ImageAlign="Middle" />
                                                                                                        <%--  OnClick="imgdwnld_Click" --%>

                                                                                                        <asp:ImageButton ID="IMGDWNLALLERR" ImageUrl="../../Images/icon_log.png" runat="server" ImageAlign="Right" CssClass="IMGDWNLALLERR" OnClick="IMGDWNLALLERR_Click"/>
                                                                                                        <%-- OnClick="IMGDWNLALLERR_Click"--%>
                                                                                                    </ContentTemplate>
                                                                                                  <Triggers>
                                                                                                        <%-- <asp:AsyncPostBackTrigger ControlID="lnkdwnld" EventName="Click" />--%>
                                                                                                      <asp:PostBackTrigger ControlID="IMGDWNLALLERR" />
                                                                                                    </Triggers>
                                                                                                </asp:UpdatePanel>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                    </Columns>
                                                                                </asp:GridView>


                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Integration ID" HeaderStyle-HorizontalAlign="Left" SortExpression="INTGRTN_ID"
                                                                HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#ed1c24" ItemStyle-BackColor="#f4e5cd" HeaderStyle-BorderColor="#d0d0d0" ItemStyle-BorderColor="#d0d0d0"><%--ItemStyle-BackColor="#ed1c24"--%>
                                                                <ItemStyle CssClass="gvtCenter" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblintgid" Text='<%# Bind("INTGRTN_ID") %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Process ID" HeaderStyle-HorizontalAlign="Left" SortExpression="PRCS_ID"
                                                                HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#ed1c24" ItemStyle-BackColor="#f4e5cd" HeaderStyle-BorderColor="#d0d0d0" ItemStyle-BorderColor="#d0d0d0">
                                                                <ItemStyle CssClass="gvtCenter" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblprcidtrx" Text='<%# Bind("PRCS_ID") %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Run Date" HeaderStyle-HorizontalAlign="Left" SortExpression="RUN_DT"
                                                                HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#ed1c24" ItemStyle-BackColor="#f4e5cd" HeaderStyle-BorderColor="#d0d0d0" ItemStyle-BorderColor="#d0d0d0">
                                                                <ItemStyle CssClass="gvtCenter" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrndthst" Text='<%# Bind("RUN_DT") %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Integration Description" HeaderStyle-HorizontalAlign="Left" SortExpression="INTGRTN_DESC"
                                                                HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#ed1c24" ItemStyle-BackColor="#f4e5cd" HeaderStyle-BorderColor="#d0d0d0" ItemStyle-BorderColor="#d0d0d0">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblintgdeschst" Text='<%# Bind("INTGRTN_DESC") %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Job Status" HeaderStyle-HorizontalAlign="Left" SortExpression="JOB_STATUS"
                                                                HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#ed1c24" ItemStyle-BackColor="#f4e5cd" HeaderStyle-BorderColor="#d0d0d0" ItemStyle-BorderColor="#d0d0d0">
                                                                <ItemTemplate>
                                                                    <%--<asp:Label ID="lbljbsts" Text='<%# Bind("JOB_STATUS") %>' runat="server" />--%>
                                                                    <img alt="" id="imgunbl" style="cursor: pointer" src="../../Images/icon_unable.png" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Job Execution Percentgae" HeaderStyle-HorizontalAlign="Left" SortExpression="JOB_EXEC_PRCNT"
                                                                HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#ed1c24" ItemStyle-BackColor="#f4e5cd" HeaderStyle-BorderColor="#d0d0d0" ItemStyle-BorderColor="#d0d0d0">
                                                                <ItemTemplate>
                                                                    <%--<asp:Label ID="lbljbexprcnt" Text='<%# Bind("JOB_EXEC_PRCNT") %>' runat="server" />--%>
                                                                    <%--<div class='progress'>  
                                                   <div class="progress-bar" role="progressbar" aria-valuenow="70" aria-valuemin="0" aria-valuemax="100" > --%>
                                                                    <%--<%# Eval("JOB_EXEC_PRCNT") %>--%>

                                                                    <%--  </div>  
                                                    </div> --%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Last Run Date" HeaderStyle-HorizontalAlign="Left" SortExpression="LAST_RUN_DATE"
                                                                HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#ed1c24" ItemStyle-BackColor="#f4e5cd" HeaderStyle-BorderColor="#d0d0d0" ItemStyle-BorderColor="#d0d0d0">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbllstrndthst" Text='<%# Bind("LAST_RUN_DATE") %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <br />
                                            <center>
                        <asp:UpdatePanel ID="updactbtncyc" runat="server">
                       <ContentTemplate>
                           
                     <asp:ImageButton ID="imgacccycl"  ImageUrl="../../Images/btn_generate_new_cycle.png"  runat="server" ImageAlign="Middle" Visible="False" OnClick="imgacccycl_Click"/>     <%--OnClick="imgacccycl_Click"--%>
                          </ContentTemplate>
             </asp:UpdatePanel>
         </center>

                                        </div>
                                    </div>

                                    <%------------------- Start of job Histroy Grid Block -------------------------------%>

                                    <div id="divjbhst" class="tab-pane active" runat="server" style="display: none">
                                        <div id="Div3" runat="server" class="panel-heading" onclick="ShowReqDtl1('ctl00_ContentPlaceHolder1_divjbhst,'');return false;">
                                        </div>
                                        <div id="div2" runat="server" style="width: 100%; border: none; margin: 0px 0 !important; overflow: auto;" class="table-scrollable">

                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:GridView ID="grdhst" runat="server" CssClass="footable" AllowSorting="True" PageSize="10" AllowPaging="true" AutoGenerateColumns="false"
                                                        DataKeyNames="INTGRTN_ID, PRCS_ID" BorderColor="#d0d0d0" OnRowDataBound="grdhst_RowDataBound">
                                                        <%--OnRowDataBound="grdhst_RowDataBound"--%>
                                                        <RowStyle CssClass="GridViewRowNew"></RowStyle>
                                                        <PagerStyle CssClass="disablepage" />
                                                        <HeaderStyle CssClass="gridview th" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-BackColor="#ed1c24" ItemStyle-BackColor="#f4e5cd" HeaderStyle-BorderColor="#d0d0d0" ItemStyle-BorderColor="#d0d0d0">
                                                                <ItemTemplate>
                                                                    <img alt="" id="img2" style="cursor: pointer" src="../../Images/btnexp.png"  />


                                                                    <div id="divChildhst" runat="server" style="display: none; width: auto; margin: 0px 0 !important;"
                                                                        class="table-scrollable">
                                                                        <asp:UpdatePanel ID="UpdatePanel61" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:GridView ID="dgchildst" runat="server" AutoGenerateColumns="false" Width="100%"
                                                                                    PageSize="10" AllowSorting="False" AllowPaging="true" CssClass="footable" HeaderStyle-BorderColor="White">

                                                                                    <RowStyle CssClass="GridViewRowNew"></RowStyle>
                                                                                    <PagerStyle CssClass="disablepage" />
                                                                                    <HeaderStyle CssClass="gridview th" />

                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Sub Process Description" HeaderStyle-HorizontalAlign="Left" SortExpression="SUB_PRC_DESC"
                                                                                            HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#ffc7c9" HeaderStyle-ForeColor="#ac080e" HeaderStyle-BorderColor="#d0d0d0" ItemStyle-BorderColor="#d0d0d0">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblsbprcdesccldhs" Text='<%# Bind("SUB_PRC_DESC") %>' runat="server" />
                                                                                            </ItemTemplate>

                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Sub Process ID" HeaderStyle-HorizontalAlign="Left" SortExpression="SUB_PRC_ID"
                                                                                            HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#ffc7c9" HeaderStyle-ForeColor="#ac080e" HeaderStyle-BorderColor="#d0d0d0" ItemStyle-BorderColor="#d0d0d0">
                                                                                            <ItemStyle CssClass="gvtCenter" />
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblsbprcidhhs" Text='<%# Bind("SUB_PRC_ID") %>' runat="server" />
                                                                                                <asp:Label ID="LBLSUBPRCIDVALhs" Text='<%# Bind("SUBPRCID1") %>' runat="server" Visible="false" />
                                                                                                <asp:Label ID="lblprcidtrxhs" Text='<%# Bind("PRCS_ID") %>' runat="server" Visible="false" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Run Date" HeaderStyle-HorizontalAlign="Left" SortExpression="RUN_DT"
                                                                                            HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#ffc7c9" HeaderStyle-ForeColor="#ac080e" HeaderStyle-BorderColor="#d0d0d0" ItemStyle-BorderColor="#d0d0d0">
                                                                                            <ItemStyle CssClass="gvtCenter" />
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblsbrndths" Text='<%# Bind("RUN_DT") %>' runat="server" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Execution Status" HeaderStyle-HorizontalAlign="Left" SortExpression="EXEC_STATUS"
                                                                                            HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#ffc7c9" HeaderStyle-ForeColor="#ac080e" HeaderStyle-BorderColor="#d0d0d0" ItemStyle-BorderColor="#d0d0d0">
                                                                                            <ItemStyle CssClass="gvtCenter" />
                                                                                            <ItemTemplate>
                                                                                                <%-- <asp:Label ID="lblsbexecstts" Text='<%# Bind("EXEC_STATUS_VAL") %>' runat="server" Visible="false" />--%>
                                                                                                <img alt="" id="imgrnnings" style="cursor: pointer" src='<%# Eval("ImagePath") %>' runat="server" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <%--<asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Left"
                                            HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#ffc7c9" HeaderStyle-ForeColor="#ac080e" HeaderStyle-BorderColor="#d0d0d0" ItemStyle-BorderColor="#d0d0d0">
                                            <ItemStyle CssClass="gvtCenter" />
                                                <ItemTemplate>
                                                   <asp:LinkButton ID="lnkbtnrn"  runat="server" Text='Run'  ></asp:LinkButton>
                                                  <img alt="" id="imgbtnrn"  style="cursor: pointer" src="../../../../images/btn_rerun.png"  />
                                                 <asp:UpdatePanel ID="uprnhst" runat="server">
                                                    <ContentTemplate>
                                                     <asp:ImageButton ID="imgruns"  ImageUrl="../../../../images/btn_rerun.png"  runat="server" Visible="false"/>     
                                                    </ContentTemplate>
                                                     </asp:UpdatePanel>
                                            </ItemTemplate>
                                        </asp:TemplateField> --%>
                                                                                        <asp:TemplateField HeaderText="Error Description" HeaderStyle-HorizontalAlign="Center"
                                                                                            HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#ffc7c9" HeaderStyle-ForeColor="#ac080e"
                                                                                            HeaderStyle-BorderColor="#d0d0d0" ItemStyle-BorderColor="#d0d0d0">
                                                                                            <ItemStyle CssClass="gvtCenter" />
                                                                                            <ItemTemplate>
                                                                                                <asp:UpdatePanel ID="updact" runat="server">
                                                                                                    <ContentTemplate>
                                                                                                        <%--<asp:LinkButton ID="lnkdwnld"  runat="server" Text='Download Error Records' OnClick="lnkdwnld_Click" Visible="false" ClientIDMode="Static" ></asp:LinkButton>--%> <%--Visible='<%# Eval("SUBPRCID1").ToString() == "102" ? true : false %>' --%>

                                                                                                        <asp:ImageButton ID="imgdwnlds" ImageUrl="../../Images/icon_error_record.png" Visible='<%# Eval("SUBPRCID1").ToString() == "103"? true : false %>' runat="server" ImageAlign="Middle" />
                                                                                                        <%-- OnClick="imgdwnlds_Click"--%>
                                                                                                        <asp:ImageButton ID="IMGDWNLALLERRhs" ImageUrl="../../Images/icon_log.png" runat="server" ImageAlign="Right" CssClass="IMGDWNLALLERR"/>
                                                                                                        <%--OnClick="IMGDWNLALLERRhs_Click"--%>
                                                                                                    </ContentTemplate>
                                                                                                    <Triggers>
                                                                                                        <%-- <asp:AsyncPostBackTrigger ControlID="lnkdwnld" EventName="Click" />--%>
                                                                                                        <asp:PostBackTrigger ControlID="IMGDWNLALLERRhs" />
                                                                                                    </Triggers>
                                                                                                </asp:UpdatePanel>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                    </Columns>
                                                                                </asp:GridView>


                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Integration ID" HeaderStyle-HorizontalAlign="Left" SortExpression="INTGRTN_ID"
                                                                HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#ed1c24" ItemStyle-BackColor="#f4e5cd" HeaderStyle-BorderColor="#d0d0d0" ItemStyle-BorderColor="#d0d0d0"><%--ItemStyle-BackColor="#ed1c24"--%>
                                                                <ItemStyle CssClass="gvtCenter" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblintgidhst" Text='<%# Bind("INTGRTN_ID") %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Process ID" HeaderStyle-HorizontalAlign="Left" SortExpression="PRCS_ID"
                                                                HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#ed1c24" ItemStyle-BackColor="#f4e5cd" HeaderStyle-BorderColor="#d0d0d0" ItemStyle-BorderColor="#d0d0d0">
                                                                <ItemStyle CssClass="gvtCenter" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblprcidhst" Text='<%# Bind("PRCS_ID") %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Run Date" HeaderStyle-HorizontalAlign="Left" SortExpression="RUN_DT"
                                                                HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#ed1c24" ItemStyle-BackColor="#f4e5cd" HeaderStyle-BorderColor="#d0d0d0" ItemStyle-BorderColor="#d0d0d0">
                                                                <ItemStyle CssClass="gvtCenter" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrndthst1" Text='<%# Bind("RUN_DT") %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Integration Description" HeaderStyle-HorizontalAlign="Left" SortExpression="INTGRTN_DESC"
                                                                HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#ed1c24" ItemStyle-BackColor="#f4e5cd" HeaderStyle-BorderColor="#d0d0d0" ItemStyle-BorderColor="#d0d0d0">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblintgdeschst1" Text='<%# Bind("INTGRTN_DESC") %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Job Status" HeaderStyle-HorizontalAlign="Left" SortExpression="JOB_STATUS"
                                                                HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#ed1c24" ItemStyle-BackColor="#f4e5cd" HeaderStyle-BorderColor="#d0d0d0" ItemStyle-BorderColor="#d0d0d0">
                                                                <ItemTemplate>
                                                                    <%--<asp:Label ID="lbljbsts" Text='<%# Bind("JOB_STATUS") %>' runat="server" />--%>
                                                                    <img alt="" id="imgunbl" style="cursor: pointer" src="../../Images/icon_unable.png" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Job Execution Percentgae" HeaderStyle-HorizontalAlign="Left" SortExpression="JOB_EXEC_PRCNT"
                                                                HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#ed1c24" ItemStyle-BackColor="#f4e5cd" HeaderStyle-BorderColor="#d0d0d0" ItemStyle-BorderColor="#d0d0d0">
                                                                <ItemTemplate>
                                                                    <%--<asp:Label ID="lbljbexprcnt" Text='<%# Bind("JOB_EXEC_PRCNT") %>' runat="server" />--%>
                                                                    <div class='progress'>
                                                                        <div class="progress-bar" role="progressbar" aria-valuenow="70" aria-valuemin="0" aria-valuemax="100">
                                                                            <%--<%# Eval("JOB_EXEC_PRCNT") %>--%>
                                                                        </div>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Last Run Date" HeaderStyle-HorizontalAlign="Left" SortExpression="LAST_RUN_DATE"
                                                                HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#ed1c24" ItemStyle-BackColor="#f4e5cd" HeaderStyle-BorderColor="#d0d0d0" ItemStyle-BorderColor="#d0d0d0">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbllstrndthst1" Text='<%# Bind("LAST_RUN_DATE") %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>

                                            <center>
<%--                                              <div id="divPage" visible="true" runat="server" class="pagination">
                            <center>
                                <table>
                                    <tr>
                                        <td style="white-space: nowrap;">
                                            <asp:UpdatePanel ID="updpgin" runat="server">
                                                <ContentTemplate>
                                                    <asp:Button ID="btnprevious_hst" Text="<" CssClass="form-submit-button" runat="server"
                                                        Width="40px" Enabled="false" Style="border-style: solid; border-width: 1px; background-repeat: no-repeat;
                                                        background-color: transparent; float: left; margin: 0; height: 30px;" OnClick="btnprevious_hst_Click" />
                                                    <asp:TextBox runat="server" ID="txtPage" Style="width: 40px; border-style: solid;
                                                        border-width: 1px; border-color: Gray; height: 30px; float: left; margin: 0;
                                                        text-align: center;" Text="1" CssClass="form-control" ReadOnly="true" />
                                                    <asp:Button ID="btnnext_hst" Text=">" CssClass="form-submit-button" runat="server" Style="border-style: solid;
                                                        border-width: 1px; background-repeat: no-repeat; background-color: transparent;
                                                        float: left; margin: 0; height: 30px;" Width="40px" OnClick="btnnext_hst_Click" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </center>
                        
                                              </div>--%>
             </center>
                                        </div>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>




                        </div>
                    </div>
                </div>








            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:HiddenField ID="hdnTabIndex" runat="server" />
</asp:Content>
