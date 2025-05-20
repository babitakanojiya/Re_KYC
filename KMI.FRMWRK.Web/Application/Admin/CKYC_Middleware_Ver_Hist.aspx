<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="CKYC_Middleware_Ver_Hist.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.Admin.CKYC_Middleware_Ver_Hist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <meta name="viewport" content="width=device-width, initial-scale=1">

    <%: Styles.Render("~/bundles/CKYCcss") %>
    <%-- <%: Scripts.Render("~/bundles/CKYCValidationjs") %>--%>
    <link href="../../assets/css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../assets/js/bootstrap-multiselect.js"></script>
    <%--  <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.10.1/jquery.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <%--   <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
   <script type="text/javascript" src="https://code.jquery.com/jquery-1.12.0.min.js"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js" integrity="sha384-0mSbJDEHialfmuBBQP6A4Qrprq5OVfW37PRR3j5ELqxss1yVqOtnepnHVP9aJ7xS" crossorigin="anonymous"></script>--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">
    <script type="text/javascript">

        //function ShowReqDtl(divName, btnName) {
        //    debugger;
        //    try {
        //        var objnewdiv = document.getElementById(divName)
        //        var objnewbtn = document.getElementById(btnName);
        //        if (objnewdiv.style.display == "block") {
        //            objnewdiv.style.display = "none";
        //            objnewbtn.className = 'glyphicon glyphicon-collapse-up'
        //        }
        //        else {
        //            objnewdiv.style.display = "block";
        //            objnewbtn.className = 'glyphicon glyphicon-collapse-down'
        //        }
        //    }
        //    catch (err) {
        //        ShowError(err.description);
        //    }
        //}


</script>

    <style>
        .container {
            width: 1300px !important;
        }

        .gridview th {
            padding: 3px;
            height: 40px;
            background-color: #00b4bf; /*#dce9f9;*/ /*#d6d6c2;*/
            /*color: black;*/
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
                    color: black;
                    background-color: white;
                    cursor: pointer;
                    padding: 10px 20px;
                    text-decoration: none;
                    border-radius: 4px 4px 0 0;
                }

                    ul#menu li a:active {
                        background: white;
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

        .pad {
            text-align: center !important;
        }

        .nav-tabs {
            background-color: black;
        }

            .nav-tabs > li {
                background-color: black;
            }

                .nav-tabs > li.active > a {
                    color: white; /*#555;*/
                    background-color: #22afb8 !important;
                    cursor: pointer;
                    border-color: #22afb8; /*#555;*/
                    border-bottom: 3px solid #22afb8; /*#555;*/
                }

                    .nav-tabs > li.active > a:hover {
                        color: #555;
                        background-color: white !important;
                        cursor: pointer;
                        border: 1px solid #555;
                    }

        .tab-content {
            border-left: 1px solid #ddd;
            border-right: 1px solid #ddd;
            border-bottom: 1px solid #ddd;
            padding: 35px !important;
            font-size: 13px;
            color: #999898;
            line-height: 25px;
        }

        /*.nav*/
        nav-tabs-sub > li > a {
            border-radius: 0px !important;
            padding: 20px 20px;
            float: left;
        }


        .nav-tabs-sub > li > a > span {
            padding: 10px 15px;
            font-weight: bold;
            color: black;
        }

        .nav-tabs-sub > li.active > a > span {
            padding: 10px 15px;
            font-weight: bold;
            color: black;
        }

        /*New for li box*/
        /*.nav-tabs-sub-lefttabs li a:hover /*, .nav-tabs-sub-lefttabs li.active a*/
        /*  {
             color: #22afb8 !important; /*.nav-tabs-sub-tabs li.active a:hover*/
        /*     background-color: white;
             border: 2px solid black;
         }*/

        .nav-tabs-sub-lefttabs > li.active > a {
            /*color: white;*/ /*#555;*/
            /*background-color:#22afb8 !important;*/
            cursor: pointer;
            border: 1px solid black; /*#555;*/
            color: #22afb8 !important;
            /*border-bottom: 3px solid #22afb8;*/ /*#555;*/
        }

        .nav-tabs-sub-lefttabs li {
            /*float: none;*/
            vertical-align: top;
        }

        /*.nav-tabs-sub-lefttabs{
           /*display: table-cell;*/
        /*width: 5%;
           min-width: 5%;*/
        /* border: none;
           vertical-align: top;
           position: relative;
        }*/

        .nav-tabs-sub-lefttabs > li > a {
            /*color: var(--text_color);*/
            /*background-color: transparent;
    font-size: 18px;*/
            text-align: center;
            /*text-transform: uppercase;
    padding: 8px 15px;
    margin: 0 -2px 0 0;*/
            border-radius: 0;
            border: 2px solid transparent;
            overflow: hidden;
            position: relative;
            z-index: 1;
            transition: all 0.20s ease 0s;
        }

        .tab-pane active {
            background-color: white;
            color: black;
        }

        .grd-content {
            /*width: 75.2%; 
            border: 2px solid #00b4bf;
            overflow-x: scroll;
            height: 10%;
            padding: 10px 15px;
            float: right;
            padding-bottom: 27%;
            margin-top: -1%;*/
            position: absolute;
            width: 76%;
            border: 2px solid #00b4bf;
            overflow-x: scroll;
            height: 86.5%;
            padding: 10px 15px;
            float: right;
            padding-bottom: 27%;
            margin-top: -1%;
            margin-right: 16px;
            right: 0;
            background-color: white;
        }

        /*CSS for active tab down arrow*/
        .nav-tabs li.active a:after {
            content: "";
            border-top: 10px solid #22afb8; /*#ddd;*/
            border-left: 10px solid transparent;
            border-right: 10px solid transparent;
            position: absolute;
            bottom: -11px;
            left: 43%;
        }


        .AlignCenter {
            text-align: center !Important;
            border-left: hidden;
            /*border-right-style: none;  .nav-tabs-sub 
               , .tabs-left li.active a:hover */
        }

        .footable td {
            border-right: hidden;
        }

        .nav-tabs > li > a {
            border-radius: 0px 0px 0 0 !important;
        }

        .activesub {
            border: 2px solid #22afb8 !important;
            border-right: hidden;
            right: 15px;
            height: 3.5em;
            line-height: 15px;
            width: 115%;
            padding-right: 20px;
            margin-right: 5px;
            border-left: 7.5px solid #22afb8 !important;
            box-sizing: border-box;
            /*background-color:#22afb8 !important;*/
            /*border-left-width:inherit;*/
        }
    </style>

    <script type="text/javascript">




        function fnSetTabs(strTabIndex) {
            debugger;
            if (strTabIndex == 'KCustReg') {
                document.getElementById('EmptyPagePlaceholder_hdntext').value = "KCustReg";
                document.getElementById("<%= btnGetMenuSelection.ClientID %>").click();
                //   myFunction();
                // changeImage();
            }
            if (strTabIndex == 'KCustInfo') {

                document.getElementById('EmptyPagePlaceholder_hdntext').value = "KCustInfo";
                document.getElementById("<%= btnGetMenuSelection.ClientID %>").click();
            }
            if (strTabIndex == 'KLglCustReg') {

                document.getElementById('EmptyPagePlaceholder_hdntext').value = "KLglCustReg";
                document.getElementById("<%= btnGetMenuSelection.ClientID %>").click();
            }
            if (strTabIndex == 'KRprts') {

                document.getElementById('EmptyPagePlaceholder_hdntext').value = "KRprts";
                document.getElementById("<%= btnGetMenuSelection.ClientID %>").click();
            }
        }

        function fnindlglTabs(strindlglTabs) {
            debugger;
            if (strindlglTabs == 'Individual') {
                document.getElementById('EmptyPagePlaceholder_hdnindleglflag').value = "Individual";
                document.getElementById("<%= BTNLEGLINTAB.ClientID %>").click();
            }
            if (strindlglTabs == 'Legal')
            // else
            {

                document.getElementById('EmptyPagePlaceholder_hdnindleglflag').value = "Legal";
                document.getElementById("<%= BTNLEGLINTAB.ClientID %>").click();

                $(document).ready(function () {
                    fnTabs('1');
                });
            }
        }

        function fnsetsubmenu(submenu) {
            debugger;

            if (submenu == 'CReg') {
                document.getElementById('EmptyPagePlaceholder_hdnsubflag').value = "CReg";
                document.getElementById("<%= btnclicksubmenu.ClientID %>").click();

            }
            if (submenu == 'LglCReg') {
                document.getElementById('EmptyPagePlaceholder_hdnsubflag').value = "LglCReg";
                document.getElementById("<%= btnclicksubmenu.ClientID %>").click();

            }
            if (submenu == 'LglDocUpd') {
                document.getElementById('EmptyPagePlaceholder_hdnsubflag').value = "LglDocUpd";
                document.getElementById("<%= btnclicksubmenu.ClientID %>").click();

            }
            if (submenu == 'LglVwDtls') {
                document.getElementById('EmptyPagePlaceholder_hdnsubflag').value = "LglVwDtls";
                document.getElementById("<%= btnclicksubmenu.ClientID %>").click();

            }
            if (submenu == 'DocUpd') {
                document.getElementById('EmptyPagePlaceholder_hdnsubflag').value = "DocUpd";
                document.getElementById("<%= btnclicksubmenu.ClientID %>").click();
            }
            if (submenu == 'VwDtls') {
                document.getElementById('EmptyPagePlaceholder_hdnsubflag').value = "VwDtls";
                document.getElementById("<%= btnclicksubmenu.ClientID %>").click();
            }
            if (submenu == 'BlkUpd') {
                document.getElementById('EmptyPagePlaceholder_hdnsubflag').value = "BlkUpd";
                document.getElementById("<%= btnclicksubmenu.ClientID %>").click();
            }
            if (submenu == 'BlkUpdSTS') {
                document.getElementById('EmptyPagePlaceholder_hdnsubflag').value = "BlkUpdSTS";
                document.getElementById("<%= btnclicksubmenu.ClientID %>").click();
            }
            if (submenu == 'PrbMtch') {
                document.getElementById('EmptyPagePlaceholder_hdnsubflag').value = "PrbMtch";
                document.getElementById("<%= btnclicksubmenu.ClientID %>").click();
            }
            if (submenu == 'PSHFIL') {
                document.getElementById('EmptyPagePlaceholder_hdnsubflag').value = "PSHFIL";
                document.getElementById("<%= btnclicksubmenu.ClientID %>").click();
            }
            if (submenu == 'Wrkbnch') {
                document.getElementById('EmptyPagePlaceholder_hdnsubflag').value = "Wrkbnch";
                document.getElementById("<%= btnclicksubmenu.ClientID %>").click();
            }
            if (submenu == 'PrbMtchAprv') {
                document.getElementById('EmptyPagePlaceholder_hdnsubflag').value = "PrbMtchAprv";
                document.getElementById("<%= btnclicksubmenu.ClientID %>").click();
            }
            if (submenu == 'ExeHst') {
                document.getElementById('EmptyPagePlaceholder_hdnsubflag').value = "ExeHst";
                document.getElementById("<%= btnclicksubmenu.ClientID %>").click();
            }
            if (document.getElementById("EmptyPagePlaceholder_hdnindleglflag").value == "Legal") {
                document.getElementById('EmptyPagePlaceholder_divsyngrd').style.width = "76%";
                //divsyngrd.Attributes.Add("style", "width: 76%;");
            }
            else {
                document.getElementById('EmptyPagePlaceholder_divsyngrd').style.width = "79%";
                //divsyngrd.Attributes.Add("style", "width: 79%;");
            }
        }
        </script>
    <script>
        // expand collapse func 21.04.2021 
        //$("[src*=Plus_icon]").live("click", function () {
        //        debugger;
        //        $(this).closest("tr").after("<tr><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
        //        $(this).attr("src", "../../Images/minus_icon.png");
        //      //  alert('minus');
        //    });
        //$("[src*=minus_icon]").live("click", function () {
        //        debugger;
        //        $(this).attr("src", "../../Images/Plus_icon.png");
        //        $(this).closest("tr").next().remove();


        //    });



        $(document).ready(function () {
            fnTabs('1');
        });

        function fnTabs(strTab) {
            debugger;
            document.getElementById('EmptyPagePlaceholder_hdntimeflag').value = strTab;
            if (strTab == '1') {
                document.getElementById('EmptyPagePlaceholder_spntdy').style.visibility = "hidden";
                document.getElementById('EmptyPagePlaceholder_spn3mnt').style.visibility = "visible";
                document.getElementById('EmptyPagePlaceholder_spn6mnt').style.visibility = "visible";

                document.getElementById('EmptyPagePlaceholder_divtab').style.display = "block";
                //   document.getElementById('EmptyPagePlaceholder_divjbhst').style.display = "none";
                document.getElementById("lisje").className = "active";
                document.getElementById("liJH").classList.remove("active");
                document.getElementById("li6mnth").classList.remove("active");
                document.getElementById("li1yr").classList.remove("active");

                document.getElementById('EmptyPagePlaceholder_hdnTabIndex').value = "1";
            }
            if (strTab == '2') {
                document.getElementById('EmptyPagePlaceholder_spn3mnt').style.visibility = "hidden";
                document.getElementById('EmptyPagePlaceholder_spntdy').style.visibility = "visible";
                document.getElementById('EmptyPagePlaceholder_spn6mnt').style.visibility = "visible";

                document.getElementById('EmptyPagePlaceholder_divtab').style.display = "block";
                //    document.getElementById('EmptyPagePlaceholder_divjbhst').style.display = "block";
                document.getElementById("lisje").classList.remove("active");
                document.getElementById("liJH").className = "active";
                document.getElementById("li6mnth").classList.remove("active");
                document.getElementById("li1yr").classList.remove("active");
                document.getElementById('EmptyPagePlaceholder_hdnTabIndex').value = "2";
            }
            if (strTab == '3') {
                document.getElementById('EmptyPagePlaceholder_spn6mnt').style.visibility = "hidden";
                document.getElementById('EmptyPagePlaceholder_spntdy').style.visibility = "visible";
                document.getElementById('EmptyPagePlaceholder_spn3mnt').style.visibility = "visible";


                document.getElementById('EmptyPagePlaceholder_divtab').style.display = "block";
                //    document.getElementById('EmptyPagePlaceholder_divjbhst').style.display = "block";
                document.getElementById("lisje").classList.remove("active");
                document.getElementById("li6mnth").className = "active";
                document.getElementById("liJH").classList.remove("active");
                document.getElementById("li1yr").classList.remove("active");
                document.getElementById('EmptyPagePlaceholder_hdnTabIndex').value = "3";
            }
            if (strTab == '4') {
                document.getElementById('EmptyPagePlaceholder_spn6mnt').style.visibility = "visible";
                document.getElementById('EmptyPagePlaceholder_spntdy').style.visibility = "visible";
                document.getElementById('EmptyPagePlaceholder_spn3mnt').style.visibility = "visible";

                document.getElementById('EmptyPagePlaceholder_divtab').style.display = "block";
                //    document.getElementById('EmptyPagePlaceholder_divjbhst').style.display = "block";
                document.getElementById("lisje").classList.remove("active");
                document.getElementById("li1yr").className = "active";
                document.getElementById("liJH").classList.remove("active");
                document.getElementById("li6mnth").classList.remove("active");
                document.getElementById('EmptyPagePlaceholder_hdnTabIndex').value = "4";
            }
        }









        //$(document).ready(function () {
        //      $('a').click(function () {
        //          debugger;
        //          var Y = (this);
        //          alert(Y);
        //          $('a').addClass('in-active');       // ADD CLASS TO ALL THE TAGS.


        //          if ($(this).hasClass('in-active')) {    // CHECK IF THE TAG HAS 'in-active' CLASS.
        //              alert("in");
        //              $(this)
        //                  .removeClass('in-active')
        //                  .addClass('active');
        //              alert("1");

        //          }
        //      })
        //  }); 



    </script>


    <script type="text/javascript">
        function test(clickedID) {
            debugger;

            document.getElementById(clickedID).classList.add("activesub");

            // var elem = clickedID;
            // alert(clickedID);
            //// clickedID.className = "active";
            // elem.classList.add("active");
        }

    </script>

    <asp:ScriptManager ID="scrusdtls" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="container" style="margin-top: 0px; width: 100%;">
                <div class="page-container">
                    <center>
                <%--<div id="divfinhdrcollapse" class="panel " style="margin-left: 4%; margin-right: 4%; margin-top: 0.5%">--%>
                <div id="divfinhdrcollapse" class="panel " >
              <%--Megha--%>
                       <div class="panel-heading" onclick="ShowReqDtl('EmptyPagePlaceholder_divfinhdr','btnToggleNew');return false;">
                        <div class="row" style="margin: 0px">
                            <div class="col-sm-10" style="text-align: left">
                                <asp:Label ID="Label1" runat="server" Font-Bold="true" Font-Size="18px" Text="MIDDLEWARE VERSION HISTORY"></asp:Label>
                            </div>
                            </div> 
                           <hr style="padding: 0.1px; width:98%">  
                            <div class="row" style="margin: 0px;display:none;">
                                 <div id="sent" class="col-sm-12" style="text-align: left; margin-bottom:15px">   <%--color:#D3D3D3--%>
                                <%--<span id="btnToggleNew" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>--%> 
                            Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Quis ipsum
                            suspendisse ultrices gravida. Risus commodo viverra maecenas accumsan lacus vel facilisis.
                            </div>
                            </div>
                        
                    </div><%--<br />--%>
                     <div class="row" style="margin-left: 2%; margin-right: 0; margin-top: 0.5%;">
                         <div class="col-sm-5" style="text-align: left">
                                <img id="indvdl" runat="server" src="../../Images/Individual_tab_hover.jpg" style="cursor: pointer" onclick="fnindlglTabs('Individual')" />
                                                &nbsp;&nbsp;                    
                                <img id="lglimg"  runat="server" src="../../Images/Legal_entityl_tab.jpg" style="cursor: pointer" onclick="fnindlglTabs('Legal');" />
                           </div>
                         </div>
                    
                       <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 1%">
                    <div class="col-md-12">
                        <div class="card">
                            <ul id="myTab" class="nav nav-tabs">
                                <li id="lisje"><a id="tabsje"  onclick="return fnTabs('1');" style="font-weight: bold; text-align:center" role="tab" data-toggle="tab">Today  
                                     <span id="spntdy" class="" runat="server"  style="color: White; visibility:visible"> &nbsp; | </span>  </a>  </li>                                 
                                <li id="liJH"><a id="tabJH" onclick="return fnTabs('2');" style="font-weight: bold;">3 Month 
                                    <span id="spn3mnt" class="" runat="server"  style="color: White; visibility:visible"> &nbsp; | </span>   </a></li>
                                <li id="li6mnth"><a id="tab6mnth" onclick="return fnTabs('3');" style="font-weight: bold;">6 Month 
                                    <span id="spn6mnt" class="" runat="server"  style="color: White; visibility:visible"> &nbsp; | </span>   </a></li>
                                 <li id="li1yr"><a id="tab1yr" onclick="return fnTabs('4');" style="font-weight: bold;">1 Year </a></li>
                             </ul>
                            <ul></ul>
                            
                        
                              <asp:UpdatePanel ID="ul" runat="server">
                                <ContentTemplate>
                        <div id="divtab" runat="server" style="float:left; display: block">
                            <asp:UpdatePanel ID="updul1" runat="server">
                                <ContentTemplate>
                           <ul id="myTabsub" class="nav nav-tabs-sub" style="display:block" runat="server"   >
                                <li id="licustreg" style="margin-top:-10px; line-height:12px; height:2em">
                                    <a id="tabcustreg" style="font-weight: bold; float:left; color:black; margin-bottom:-15px" runat="server" >Customer Registration
                                      &nbsp; 
                                      <span id="img" class="toggle"> <img id="img2" runat="server" style="cursor: pointer" src="../../Images/Plus_icon.png" 
                                          onclick="fnSetTabs('KCustReg')" />  </span> 
                                     
                                    </a>
                                     <br />
                                  <asp:UpdatePanel ID="submenuppnl" runat="server">
                                <ContentTemplate>
                                            <ul id="submen" class="nav nav-tabs-sub-tabs" runat="server" style="float:left; display:block;" >
                                    </ul>
                                   
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                           
                                       <%--onserverclick="tabcustreg_ServerClick"   runat="server" 
                                            <asp:ImageButton ID="img2" runat="server"  style="cursor: pointer;  display:inline" src="../../Images/Plus_icon.png"  />
                                           nav nav-tabs-sub--%>
                               </li>
                            <%--    <ul id='submen' class='nav nav-tabs-sub' runat="server">
                                    </ul>--%>
                            </ul>
                                   
                              <hr id="hrindcuinf" runat="server" style="width:91%;text-align:left;margin-left:0;height:1px; color:gray; background-color:gray; margin-top:2%" /> 
                           <ul id="myTabsub1" class="nav nav-tabs-sub" style="display:block" runat="server">
                                  <li id="licustinfo" style="margin-top:-8px; line-height:12px; height:2em; margin-bottom:-9px;">
                                      <a id="tabcustinfo"  style="font-weight: bold; float:left; color:black; margin-bottom:-12px; margin-top:-18px">Customer Info
                                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <span id="imginf" class="toggle">    <img alt="" runat="server" id="img3" style="cursor: pointer" src="../../Images/Plus_icon.png" 
                                               onclick="fnSetTabs('KCustInfo')"/> </span>
                               
                                  </a><br />
                                      <asp:UpdatePanel ID="UPPNLcustinF" runat="server">
                                <ContentTemplate>
                                      <ul id='Ulcustinf' class='nav nav-tabs-sub-tabs' runat="server" style="float:left;width:100%;">
                                    </ul>
                                       
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                               
                                  </li>
                             </ul>

                            <%--------- LEGAL ENTITY BLOCK STARTS --------%>

                            <ul id="lglcustreg" class="nav nav-tabs-sub" style="display:none" runat="server">
                                <li id="lilglcustreg" style="margin-top:-8px; line-height:12px; height:2em; margin-bottom:-9px">
                                    <a id="alglcustreg" style="font-weight: bold; float:left; color:black; margin-bottom:-12px; margin-top:0px" runat="server" onclick="">Customer Registration
                                      &nbsp; <img alt="" id="imglglcst" style="cursor: pointer" src="../../Images/Plus_icon.png" runat="server" 
                                          onclick="fnSetTabs('KLglCustReg')" />
                                     
                                    </a>
                                     <br />
                                     <ul id='subulglreg' class='nav nav-tabs-sub' runat="server" style="float:left; display:block;" >
                                    </ul>                                           
                                    </li>
                              </ul>

                             <hr id="hrlgl" runat="server" style="width:101%;text-align:left;margin-left:0;height:1px; color:gray; background-color:gray; margin-top:17px;
                                display: none;"/> 
                       
                             <ul id="lglrprts" class="nav nav-tabs-sub" style="display:none" runat="server">
                                  <li id="lilglrprts" style="margin-top:-8px; line-height:12px; height:2em; margin-bottom:-9px">
                                      <a id="alglrprts"  style="font-weight: bold; float:left; color:black; margin-bottom:-12px; margin-top:-18px">Reports
                                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                           <img alt="" id="imglglrprt" style="cursor: pointer" src="../../Images/Plus_icon.png"  runat="server"
                                               onclick="fnSetTabs('KRprts')"/>
                               
                                  </a><br />
                                      <ul id='sublglrprt' class='nav nav-tabs-sub' runat="server" style="float:left;width:100%;">
                                    </ul>
                                           
                                  </li>
                             </ul>

                                </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                                     </ContentTemplate>
                                    </asp:UpdatePanel>
                    
                     <asp:Button ID="btnGetMenuSelection" runat="server" Style="display: none" Text="Button" OnClick="btnGetMenuSelection_Click"/>
      
                    <asp:Button ID="btnclicksubmenu" runat="server" Style="display: none" Text="Button" OnClick="btnclicksubmenu_Click"/>
      <asp:Button ID="BTNLEGLINTAB" runat="server" Style="display: none" Text="Button" OnClick="BTNLEGLINTAB_Click"/>
      
                                   
                     <%--<div id="divfinhdr" runat="server" style="width: 96%;" class="panel-body">--%>

                            <br /><%--  style="margin-right: 50px;margin-left: 60px;border: 1px solid #00b4bf;"--%>
                    <%--<div id="divsyngrd" runat="server" style="width: 80%; border: 1px solid #00b4bf; margin: 0px 0 !important; float:right; height:10%; display:block" class="table-scrollable">
                    --%>
                            <div id="divsyngrd" runat="server" class="grd-content" style="display:none; float:right">
                             
                    <div class="panel-heading" onclick="ShowReqDtl('EmptyPagePlaceholder_divfinhdr','btnToggleNew');return false;" style="float:left; margin-bottom:10px">
                        <div class="row" style="margin: 0px">
                            <div class="col-sm-12" style="text-align: left">
                                <asp:Label ID="lblgrdhead" runat="server" Font-Bold="true" Font-Size="15px" Text="" Visible="false"></asp:Label>
                                   <asp:Label ID="lblverhst" runat="server" Text="Version History" Visible="false" ForeColor="#6495ED" ></asp:Label>
                            </div>
                            </div> 
                         </div> <br />
                                 <div id="divGridMap" runat="server" style="display:block; width:100%" class="table-scrollable" >
                                             <div id="dixflow" runat="server" style="width: 100%; overflow-x:scroll" >
                <asp:UpdatePanel ID="UpdatePanelgrd" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grdhst" runat="server" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True" CssClass="footable"  Width="95%"
                        BackColor="White"  BorderStyle="None" BorderWidth="0px" CellPadding="4" ForeColor="Black"  
                            GridLines="Horizontal" >
                            <RowStyle CssClass="GridViewRowNEW" BorderStyle="None"></RowStyle>
                            <PagerStyle CssClass="disablepage" BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                            <HeaderStyle CssClass="gridview th" BackColor="#333333" Font-Bold="True" ForeColor="White" />
                             <EmptyDataTemplate>
                                  <asp:Label ID="Label2" Text="No Record Found!" ForeColor="Red" CssClass="control-label" runat="server" />
                                </EmptyDataTemplate>
                            <Columns>
                                 <%--BorderColor="#CCCCCC" <asp:TemplateField HeaderText="Entity Type"  SortExpression="EntityType" ItemStyle-CssClass="text-center" HeaderStyle-ForeColor
                                      ="#444444">
                                    <ItemTemplate>
                                        <asp:Label ID="lblenttyp"  runat="server" Text='<%# Bind("EntityType")%>'></asp:Label>
                                        <asp:Label ID="lblsynSeqNo" Text='<%# Bind("SeqNo") %>' Visible="false" runat="server" />
                                    </ItemTemplate>                                                                    
                                </asp:TemplateField>--%>
                               <%-- <asp:TemplateField HeaderText="Module Name"  SortExpression="ModuleName" ItemStyle-CssClass="text-center"  HeaderStyle-ForeColor
                                      ="#444444">
                                    <ItemTemplate>
                                        <asp:Label ID="lblmdl"  runat="server" Text='<%# Bind("ModuleName")%>'></asp:Label>
                                        </ItemTemplate>                                                                    
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Seq No"  SortExpression="SeqNo"  ItemStyle-CssClass="AlignCenter"  HeaderStyle-ForeColor
                                      ="White">
                                    <ItemTemplate>
                                         <asp:Label ID="lblseq" Text='<%# Bind("SeqNo") %>' Visible="true" runat="server" />
                                    </ItemTemplate>                                                                    
                                    <HeaderStyle ForeColor="White" />
                                    <ItemStyle  CssClass="AlignCenter" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remark"  SortExpression="Remark" ItemStyle-CssClass="AlignCenter"  HeaderStyle-ForeColor
                                      ="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lblrmk"  runat="server" Text='<%# Bind("Remark")%>'></asp:Label>
                                        </ItemTemplate>                                                                    
                                    <HeaderStyle ForeColor="White" />
                                  
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Created By"  SortExpression="CreatedBy" ItemStyle-CssClass="AlignCenter"  HeaderStyle-ForeColor
                                      ="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcrtby"  runat="server" Text='<%# Bind("CreatedBy")%>'></asp:Label>
                                        </ItemTemplate>                                                                    
                                     <HeaderStyle ForeColor="White" />
                                    
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Created DateTime"  SortExpression="Remark" ItemStyle-CssClass="AlignCenter"  HeaderStyle-ForeColor
                                      ="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldt"  runat="server" Text='<%# Bind("CreatedDTim")%>'></asp:Label>
                                        </ItemTemplate>                                                                    
                                     <HeaderStyle ForeColor="White" />
                                    
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Version"  SortExpression="Version" ItemStyle-CssClass="AlignCenter"  HeaderStyle-ForeColor
                                      ="white">
                                    <ItemTemplate>
                                        <asp:Label ID="lblver"  runat="server" Text='<%# Bind("Version")%>'></asp:Label>
                                        </ItemTemplate>                                                                    
                                      <HeaderStyle ForeColor="White" />
                                     
                                </asp:TemplateField>
                                </Columns>
                         
                          <%--  <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                            <SortedDescendingHeaderStyle BackColor="#242121" />--%>
                         
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                         
                             </div>
                      
                           </div>  <%--end of grd-content --%>
                        </div> <%--end of divtab --%>
                                </div>
                           </div>
                           </div>

                    </div>
                  </center>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:HiddenField ID="hdntext" runat="server" />
    <asp:HiddenField ID="hdnTabIndex" runat="server" />
    <asp:HiddenField ID="hdnsubflag" runat="server" />
    <asp:HiddenField ID="hdnindleglflag" runat="server" />
    <asp:HiddenField ID="hdnCurrActId" ClientIDMode="Static" runat="server" Value="" />
    <asp:HiddenField ID="hdntimeflag" runat="server" />
</asp:Content>
