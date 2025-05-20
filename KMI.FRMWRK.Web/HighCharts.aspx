<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HighCharts.aspx.cs" Inherits="KMI.FRMWRK.Web.HighCharts" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    
    <title>Your ASP.NET application</title>
    <link href="Content/Bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="Content/Bootstrap/js/jquery-3.2.1.js"></script>
    <script src="Content/Bootstrap/js/bootstrap.min.js"></script>
    <script src="Content/Bootstrap/js/exporting.js"></script>

    <style>
        .HeaderColor {
          
            color: #044DA1;
            font-size: 16px;
        }

        .card {
            position: relative;
            display: flex;
            flex-direction: column;
            min-width: 0;
            word-wrap: break-word;
            background-clip: border-box;
            border: 1px solid rgba(0,0,0,.125);
            border-radius: 1.25rem !important;
        }

        .panel {
            margin-bottom: 20px;
            background-color: #fff;
            border: 1px solid transparent;
            border-radius: 4px;
            -webkit-box-shadow: 0 1px 1px rgba(0, 0, 0, .05);
            box-shadow: 0 1px 1px rgba(0, 0, 0, .05);
        }

        .panel-body {
            padding: 0px;
        }

        .panel-heading {
            padding: 10px 15px;
            border-bottom: 1px solid transparent;
            border-top-left-radius: 3px;
            border-top-right-radius: 3px;
        }

            .panel-heading > .dropdown .dropdown-toggle {
                color: inherit;
            }

        .panel-title {
            margin-top: 0;
            margin-bottom: 0;
            font-size: 16px;
            color: inherit;
        }

        .PanelInsideTab {
            margin-left: 4%;
            margin-right: 2%;
            padding: 1%;
            background-color: white;
        }

        .panelheadingAliginment {
            margin-left: 18px;
        }

        .spancl{ height:20%;
                    text-align: center;
                    width: 31%;
                    margin: 1% 1% 1% 1%;
                    padding: 10px;
                    box-shadow: 0 7px 7px grey;
                    border:1px solid rgba(0,0,0,.125);
                    border-radius:1.25rem !important;
         }


    </style>
    <%--new added --%>
     <style type="text/css">
        .radio-group label {
            overflow: hidden;
        }

        .panel-primary {
            border-color: #337ab7
        }
        .radio-group input {
           
            height: 1px;
            width: 1px;
            position: absolute;
            top: -20px;
        }

        .radio-group .not-active {
            color: #3276b1;
            background-color: #fff;
        }
        .hide{
             display:none;  
         }
    </style>
     <style>
        .panel > .panel-heading {
            background-color: #00b4bf !important;
        }

        @media (min-width: 1200px) {
    .container {
        width: 1279px;
    }
}

        .panel-body {
            padding: 35px !important;
        }

        .panel-header, .panel-body {
            border-radius: 0px !important;
        }

        .nav-tabs > li {
            background-color: transparent;
        }

            .nav-tabs > li > a {
                border-radius: 0px !important;
                padding: 20px 20px;
                background-color: #fff;
                border: 1px solid #ddd;
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
            border-left: 1px solid #337ab7;
            border-right: 1px solid #337ab7;
            border: 1px solid #337ab7;
            padding: 35px !important;
        }

        .nav-tabs > li.active > a, .nav-tabs > li.active > a:focus, .nav-tabs > li.active > a:hover {
            color: #555;
            cursor: default;
            background-color: darkblue !important;
            border: 1px solid darkblue;
            border-bottom-color: darkblue;
        }

        .box {
            padding: 15px;
            text-align: center;
            color: #000;
            width: 32%;
            margin: 5px;
            border-radius: 4px;
            box-shadow: 0px 0px 11px 0px #c3c0c0;
        }

            .box.light-blue {
                background-color: #34a6e2;
            }

            .box.green {
                background-color: #5bb85d;
            }

            .box.orange {
                background-color: #efad4d;
            }

            .box.maroon {
                background-color: #d52728;
            }

            .box.color-1 {
                background-color: #4a5b79;
            }

            .box.dark-yellow {
                background-color: #d5aa27;
            }

            .box.violate {
                background-color: #523a6e;
            }

            .box.dark-blue {
                background-color: #5974ff;
            }

            .box.dark-something {
                background-color: #8b60e3;
            }

            

            .box .number {
                font-size: 24px;
                color: #000;
            }

            .box .number-label {
                font-size: 16px;
                color: #000;
            }
    </style>
    <script language="javascript" type="text/javascript">
         function ShowReqDtlMst(divName, btnName) {
            debugger;
            try {
                var objnewdiv = document.getElementById(divName)
                var objnewbtn = document.getElementById(btnName);
                if (objnewdiv.style.display == "block") {
                    objnewdiv.style.display = "none";
                    objnewbtn.className = 'glyphicon glyphicon-chevron-down'
                }
                else {
                    objnewdiv.style.display = "block";
                    objnewbtn.className = 'glyphicon glyphicon-chevron-up'
                }
            }
            catch (err) {
                ShowError(err.description);
            }
        }

    </script>

    <script language="javascript" type="text/javascript">
          
          window.addEventListener('load', function () {
              setTimeout(function () {
                  
                  document.getElementById('datetime').style.display = 'block';
              }, 1000); 
              setTimeout(function () {
                  document.getElementById('Panel4').style.display = 'block';
              }, 1000); 

              setTimeout(function () {
                  document.getElementById('divost').style.display = 'block';
              }, 3000); 

              setTimeout(function () {
                  document.getElementById('divzip').style.display = 'block';
              }, 4000); 

              setTimeout(function () {
                  document.getElementById('divaccept').style.display = 'block'; 
              }, 5000); 

              setTimeout(function () {
                  document.getElementById('divnewkyc').style.display = 'block';
              }, 6000); 
              setTimeout(function () {
                  document.getElementById('divexisting').style.display = 'block';
              }, 7000); 

              
              setTimeout(function () {
                  document.getElementById('diverror').style.display = 'block';
              }, 8000); 

              setTimeout(function () {
                  document.getElementById('divprobable').style.display = 'block';
              }, 9000); 

              setTimeout(function () {
                  document.getElementById('divInsufficient').style.display = 'block';
              }, 10000); 

              setTimeout(function () {
                  document.getElementById('divreject').style.display = 'block';
              }, 11000); 

          


        });

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
</head>

<body>
    <form id="form1" runat="server">
       


         <div class="container">
        <div class="panel panel-primary">
            <div class="panel-heading" style="font-size:18px; font-weight:bold">
                <img src="image/dashboard_iocn.png" style="margin-right : 10px; width:35px" />
                CKYC Dashboard
                             <%-- <div  style="text-align: right;display:none; margin-left: 857px;" id="datetime">--%>
            <span id="SpnBatchDT" runat="server" style="font-size: small;font-weight: bold;margin-left: 715px;">
            </span>
        <%--</div>--%>
            </div>
            <div id="Content">
            </div>
            <div class="panel-body">
                 <div class="panel-heading"  onclick="showHideDiv('tab1closed','btnToggle2');return false;" style="padding: 0px;">
                            <div class="row">
                                <div class="col-sm-10" style="text-align: left">
                <ul class="nav nav-tabs" id="mylist">
                    <li id="Ind" class="active" onclick="checktab(this,'menu1')">
                        <a data-toggle="tab" href="#menu1">
                            <span style="font-weight:bold">KYC INDIVIDUAL ENTITY REGISTRATION</span>
                        </a>
                    </li>
                    <li id="Legal" onclick="checktab(this,'menu2')">
                        <a data-toggle="tab" href="#menu2">
                            <span style="font-weight:bold">KYC LEGAL ENTITY REGISTRATION</span>
                        </a>
                    </li>
                </ul>
                                   </div>
                                <div class="col-sm-2">
                                    <span id="btnToggle2" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                                </div>
                            </div>
                        </div>

                <div class="tab-content" id="tab1closed">
                    <div id="menu1" class="tab-pane fade active in">
                        <div class="row">
                            <div class="col-sm-4 text-center box">
                                <div class="row">
                                    <div class="col-sm-2">
                                        <div style="margin-top:10px">
                                            <img src="assets/images/dashboard-icon/new_icon.png" alt=""style="width: 60px;" />
                                        </div>
                                    </div>
                                    <div class="col-sm-7">
                                         <span id="SpnNewPendingFI" runat="server" style="font-size: 200%; font-weight: bold;">
                                         </span>
                                            <br />
                               <span id="lblnew" style="font-size:130%; font-weight: bold;">
                                         New Cases
                                   </span>
                                 <br />
                         <span id="lblpendind" style="font-size: small;margin: -7px;">
                        (Pending for doc upload in MW)
                                 </span>
                                    </div>
                                    <div class="col-sm-1" style="margin-top:6px;">
        <img src="Image/blackline.PNG" =" style="height:76px; width:02px;" />
    </div>
    <div class="col-sm-2" style="padding:0px; margin-top: 35px; margin-left: -13px;" >
        <span id="newper" runat="server" style="font-size:127%; font-weight: bold; color:black;"> </span>
    </div>
                                </div>
                            </div>
                            <div class="col-sm-4 text-center box">
                                <div class="row">
                                    <div class="col-sm-2">
                                        <div style="margin-top:10px">
                                            <img src="assets/images/dashboard-icon/draft_icon.png" alt="" style="width: 60px;" />
                                        </div>
                                    </div>
                                    <div class="col-sm-7">
                                       <span id="SpanDraftUplCersai" runat="server" style="font-size: 200%; font-weight: bold;">
                        </span>
                        <br />
                        <span id="lblzip" style="font-size:130%; font-weight: bold;">
                            Zip Generated
                        </span>
                        <br />
                        <span id="lblziputc" style="font-size: small;">
                            (Uploaded to CKYCR)
                        </span>
                                    </div>
                                     <div class="col-sm-1" style="margin-top:6px;">
                        <img src="Image/blackline.PNG" =" style="height:76px; width:02px;" />
                    </div>
                    <div class="col-sm-2" style="padding:0px; margin-top: 35px; margin-left: -13px;">
                        <span id="spanzipper" runat="server" style="font-size:127%; font-weight: bold; color:black;"> </span>
                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4 text-center box">
                                <div class="row">
                                    <div class="col-sm-2">
                                        <div style="margin-top:10px">
                                            <img src="assets/images/dashboard-icon/accepted_icon.png" alt="" style="width: 60px;" />
                                        </div>
                                    </div>
                                    <div class="col-sm-7">
                                        <span id="SpanAcceptedCersai" runat="server" style="font-size: 200%; font-weight: bold;">
                        </span>
                        <br />
                        <span id="lblacce" style="font-size:130%; font-weight: bold;">
                            Accepted
                        </span>
                        <br />
                        <span id="lblziptockyc" style="font-size: small;">
                            (Zip accepted by CKYCR)
                        </span>
                                    </div>
                                     <div class="col-sm-1" style="margin-top:6px;">
                        <img src="Image/blackline.PNG" =" style="height:76px; width:02px;" />
                    </div>
                    <div class="col-sm-2" style="padding:0px; margin-top: 35px; margin-left: -13px;">
                        <span id="Acceptedper" runat="server" style="font-size:127%; font-weight: bold; color:black;"> </span>
                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-4 text-center box ">
                                <div class="row">
                                    <div class="col-sm-2">
                                        <div style="margin-top:10px">
                                              <img src="assets/images/dashboard-icon/kyc-generated_icon.png" alt=""  style="width: 60px;"/>
                                            
                                        </div>
                                    </div>
                                    <div class="col-sm-7">
                                         <span id="SpanInsufiBal" runat="server" style="font-size: 200%; font-weight: bold;">
                        </span>
                        <br />
                        <span id="spannewG" style="font-size:130%; font-weight: bold;">
                            New KYC Generated
                        </span>
                        <br />
                        <span id="lblkin" style="font-size: small;">
                            (KIN Generated by CKYCR)
                        </span>
                                    </div>
                                     <div class="col-sm-1" style="margin-top:6px;">
                        <img src="Image/blackline.PNG" =" style="height:76px; width:02px;" />
                    </div>
                    <div class="col-sm-2" style="padding:0px; margin-top: 35px; margin-left: -13px;">
                        <span id="newkycper" runat="server" style="font-size:127%; font-weight: bold; color:black;"> </span>
                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4 text-center box">
                                <div class="row">
                                    <div class="col-sm-2">
                                        <div style="margin-top:10px">
                                            <img src="assets/images/dashboard-icon/id_not_confirmed.png" alt=""  style="width: 60px;"/>
                                        </div>
                                    </div>
                                    <div class="col-sm-7">
                                         <span id="SpanCompletedmatch" runat="server" style="font-size: 200%; font-weight: bold;">
                        </span>
                        <br />
                        <span id="lblexis" style="font-size:130%; font-weight: bold;">
                            Existing KYC Linked
                        </span>
                        <br />
                        <span id="lblkyclink" style="font-size: small;margin: -7px;">
                            (Existing KYC linked by CKYCR)
                        </span>
                                    </div>
                                     <div class="col-sm-1" style="margin-top:6px;">
                        <img src="Image/blackline.PNG" =" style="height:76px; width:02px;" />
                    </div>
                    <div class="col-sm-2" style="padding:0px; margin-top: 35px; margin-left: -13px;">
                        <span id="existingper" runat="server" style="font-size:127%; font-weight: bold; color:black;"> </span>
                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4 text-center box ">
                                <div class="row">
                                    <div class="col-sm-2">
                                        <div style="margin-top:10px">
                                             
                                            <img src="assets/images/dashboard-icon/insufficient_balance_icon.png" alt="" style="width: 60px;"/>
                                        </div>
                                    </div>
                                    <div class="col-sm-7">
                                        <span id="SpanProbableMatch" runat="server" style="font-size: 200%; font-weight: bold;">
                        </span>
                        <br />
                        <span id="lblerror" style="font-size:130%; font-weight: bold;">
                            Error Found
                        </span>
                        <br />
                        <span id="lblrejectfile" style="font-size: small;">
                            (File rejected by CKYCR)
                        </span>
                                    </div>
                                     <div class="col-sm-1" style="margin-top:6px;">
                        <img src="Image/blackline.PNG" =" style="height:76px; width:02px;" />
                    </div>
                    <div class="col-sm-2" style="padding:0px; margin-top: 35px; margin-left: -13px;">
                        <span id="errorper" runat="server" style="font-size:127%; font-weight: bold; color:black;"> </span>
                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-4 text-center box ">
                                <div class="row">
                                    <div class="col-sm-2">
                                        <div style="margin-top:10px">
                                           <img src="assets/images/dashboard-icon/probable_match_icon.png" alt="" style="width: 60px;" />
                                        </div>
                                    </div>
                                    <div class="col-sm-7">
                                        <span id="SpanRejectedFirTATLapse" runat="server" style="font-size: 200%; font-weight: bold;">
                        </span>
                        <br />
                        <span id="lblprobable" style="font-size:130%; font-weight: bold;">
                            Probable Match
                        </span>
                        <br />
                        <span id="lblpm" style="font-size: small;">
                            (PM Cases shared by CKYCR)
                        </span>
                                    </div>
                                    <div class="col-sm-1" style="margin-top:6px;">
                        <img src="Image/blackline.PNG" =" style="height:76px; width:02px;" />
                    </div>
                    <div class="col-sm-2" style="padding:0px; margin-top: 35px; margin-left: -13px;">
                        <span id="probableper" runat="server" style="font-size:127%; font-weight: bold; color:black;"> </span>
                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4 text-center box">
                                <div class="row">
                                    <div class="col-sm-2">
                                        <div style="margin-top:10px">
                                            <img src="assets/images/dashboard-icon/completely_match.png" alt="" style="width: 60px;" />
                                        </div>
                                    </div>
                                    <div class="col-sm-7">
                                         <span id="SpanIDNConfirmISSUER" runat="server" style="font-size: 200%; font-weight: bold;">
                        </span>
                        <br />
                        <span id="lblbalance" style="font-size:130%; font-weight: bold;">
                            Insufficient Balance
                        </span>
                        <br />
                        <span id="lblwallet" style="font-size: small;">
                            (Insufficient balance in wallet)
                        </span>
                                    </div>
                                    <div class="col-sm-1" style="margin-top:6px;">
                        <img src="Image/blackline.PNG" =" style="height:76px; width:02px;" />
                    </div>
                    <div class="col-sm-2" style="padding:0px; margin-top: 35px; margin-left: -13px;">
                        <span id="balanceper" runat="server" style="font-size:127%; font-weight: bold; color:black;"> </span>
                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4 text-center box ">
                                <div class="row">
                                    <div class="col-sm-2">
                                        <div style="margin-top:10px">
                                            <img src="assets/images/dashboard-icon/rejected_icon.png" alt="" style="width: 60px;"/>
                                        </div>
                                    </div>
                                    <div class="col-sm-7">
                                        <span id="SpanKYCGenertedCersai" runat="server" style="font-size: 200%; font-weight: bold;">
                        </span>
                        <br />
                        <span id="lblreject" style="font-size:130%; font-weight: bold;">
                            Rejected
                        </span>
                        <br />
                        <span id="lbltat" style="font-size: small;">
                            (PM TAT lapsed)
                        </span>
                                    </div>
                                     <div class="col-sm-1" style="margin-top:6px;">
                        <img src="Image/blackline.PNG" =" style="height:76px; width:02px;" />
                    </div>
                    <div class="col-sm-2" style="padding:0px; margin-top: 35px; margin-left: -13px;">
                        <span id="rejectper" runat="server" style="font-size:127%; font-weight: bold; color:black;"> </span>
                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                     </div>
            </div>

             <div class="panel-body">

                    <div class="panel-heading"  onclick="showHideDiv('tabcolse','btnToggle');return false;" style="padding: 0px;">
                            <div class="row">
                                <div class="col-sm-10" style="text-align: left">
  <%--<ul class="nav nav-tabs" id="mylistcount">
                    <li id="Indcount" class="active" onclick="checktab(this,'menu1')" style="text-align: center;">
                        <a  style="padding-left: 114px;">
                            <span style="font-weight:bold;text-align: center;">FINANCIAL YEAR 2023-2024</span>
                        </a>
                    </li>
    <%--  <li id="Indcount" class="active" onclick="checktab(this,'menu1')">
        <a>
            <span style="font-weight: bold; display: inline-block; text-align: center;">FINANCIAL YEAR 2023-2024</span>
        </a>
    </li>--%>
                <%--    <li id="totalcountshow" >
                        <a style="padding-left: 114px;">
                            <span  style="font-weight: bold;color: black;">TOTAL COUNT : </span>
                            <span id="totalcount"  style="font-weight: bold;color: black;"  runat="server"></span>
                        </a>
                    </li>
                </ul>--%>

                         <ul class="nav nav-tabs" id="mylistcount" style="border-bottom: white;">
                    <li id="count" class="active" onclick="checktab(this,'menu1')">
                        <a data-toggle="tab" href="#menu1">
                            <span style="font-weight:bold;">FINANCIAL YEAR 2023-2024</span>
                            </a>
                            </li>
                            <li style="margin-left: 475px;">
                           <span  style="font-size: 155%;font-weight: bold;color: black;">TOTAL COUNT : </span>
                            <span id="totalcount"  style="font-size: 155%;font-weight: bold;color: black;"  runat="server"></span>
                                </li>
                        </ul>
                           </div>
                                <div class="col-sm-2">
                                    <span id="btnToggle" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                                </div>
                            </div>
                        </div>
                <div id="tabcolse" class="tab-content">
                    <div id="menu2" class="tab-pane fade active in">
                        <div class="row">
                            <div class="col-sm-4 text-center box">
                                 <div class="col-sm-7">
                                        <div style="margin-top:10px">
                                            <img src="assets/images/dashboard-icon/Apr.png" alt="" style="width: 100px;height: 78px;"/>
                                        </div>
                                    </div>
                                
                                    <div class="col-sm-5" style="padding: 16px;">
                                    
                                         <span id="spanaprcount" runat="server" style="font-size: 280%; font-weight: bold;">
                                         </span>
                                  <%--          <br />
                               <span id="lblapril" style="font-size:130%; font-weight: bold;">
                                         April
                                   </span>--%>
                                 <br />
                                    
                 
                                </div>
                            </div>
                            <div class="col-sm-4 text-center box">
                               <div class="col-sm-7">
                                        <div style="margin-top:10px">
                                            <img src="assets/images/dashboard-icon/may.png" alt="" style="width: 100px;height: 78px;"/>
                                        </div>
                                    </div>
                                
                                    <div class="col-sm-5" style="padding: 16px;">
                                    
                                         <span id="Spspanmaycoun" runat="server" style="font-size: 280%; font-weight: bold;">
                                         </span>
                                 <%--           <br />
                               <span id="spanmay" style="font-size:130%; font-weight: bold;">
                                         May
                                   </span>
                                 <br />--%>
                                    
                 
                                </div>
                            </div>
                            <div class="col-sm-4 text-center box">
                                 <div class="col-sm-7">
                                        <div style="margin-top:10px">
                                            <img src="assets/images/dashboard-icon/jun.png" alt="" style="width: 100px;height: 78px;"/>
                                        </div>
                                    </div>
                                
                                    <div class="col-sm-5" style="padding: 16px;">
                                    
                                         <span id="spanJunecount" runat="server" style="font-size: 280%; font-weight: bold;">
                                         </span>
                                 <%--           <br />
                               <span id="spanJune" style="font-size:130%; font-weight: bold;">
                                         June
                                   </span>
                                 <br />--%>
                                    
                 
                                </div>
                            </div>
                        </div>
                         <div class="row">
                            <div class="col-sm-4 text-center box">
                                <div class="col-sm-7">
                                        <div style="margin-top:10px">
                                            <img src="assets/images/dashboard-icon/jul.png" alt="" style="width: 100px;height: 78px;"/>
                                        </div>
                                    </div>
                                
                                    <div class="col-sm-5" style="padding: 16px;">
                                    
                                         <span id="spanjulycount" runat="server" style="font-size: 280%; font-weight: bold;">
                                         </span>
                                 <%--           <br />
                               <span id="spanjuly" style="font-size:130%; font-weight: bold;">
                                         July
                                   </span>
                                 <br />--%>
                                    
                                   
                                </div>
                            </div>
                            <div class="col-sm-4 text-center box">
                                <div class="col-sm-7">
                                        <div style="margin-top:10px">
                                            <img src="assets/images/dashboard-icon/aug.png" alt="" style="width: 100px;height: 78px;"/>
                                        </div>
                                    </div>
                                
                                    <div class="col-sm-5" style="padding: 16px;">
                                    
                                         <span id="spanaugcount" runat="server" style="font-size: 280%; font-weight: bold;">
                                         </span>
                                 <%--           <br />
                               <span id="spanaug" style="font-size:130%; font-weight: bold;">
                                         August
                                   </span>
                                 <br />--%>
                                    
                                   
                                </div>
                            </div>
                            <div class="col-sm-4 text-center box">
                                 <div class="col-sm-7">
                                        <div style="margin-top:10px">
                                            <img src="assets/images/dashboard-icon/sep.png" alt="" style="width: 100px;height: 78px;"/>
                                        </div>
                                    </div>
                                
                                    <div class="col-sm-5" style="padding: 16px;">
                                    
                                         <span id="spanseptcount" runat="server" style="font-size: 280%; font-weight: bold;">
                                         </span>
                                    <%--        <br />
                               <span id="spansept" style="font-size:130%; font-weight: bold;">
                                         September
                                   </span>
                                 <br />
                                    --%>
                                   
                                </div>
                            </div>
                        </div>
                         <div class="row">
                            <div class="col-sm-4 text-center box">
                                <div class="col-sm-7">
                                        <div style="margin-top:10px">
                                            <img src="assets/images/dashboard-icon/oct.png" alt="" style="width: 100px;height: 78px;"/>
                                        </div>
                                    </div>
                                
                                    <div class="col-sm-5" style="padding: 16px;">
                                    
                                         <span id="spanoctcount" runat="server" style="font-size: 280%; font-weight: bold;">
                                         </span>
                                 <%--           <br />
                               <span id="spanoct" style="font-size:130%; font-weight: bold;">
                                         October
                                   </span>
                                 <br />--%>
                                    
                                    
                                </div>
                            </div>
                            <div class="col-sm-4 text-center box">
                                 <div class="col-sm-7">
                                        <div style="margin-top:10px">
                                            <img src="assets/images/dashboard-icon/nov.png" alt="" style="width: 100px;height: 78px;"/>
                                        </div>
                                    </div>
                                
                                    <div class="col-sm-5" style="padding: 16px;">

                                         <span id="spannovcount" runat="server" style="font-size: 280%; font-weight: bold;">
                                         </span>
                                 <%--           <br />
                               <span id="spannoc" style="font-size:130%; font-weight: bold;">
                                         November
                                   </span>
                                 <br />--%>
                                    
                                   
                                </div>
                            </div>
                            <div class="col-sm-4 text-center box">
                                 <div class="col-sm-7">
                                        <div style="margin-top:10px">
                                            <img src="assets/images/dashboard-icon/dec.png" alt="" style="width: 100px;height: 78px;"/>
                                        </div>
                                    </div>
                                
                                    <div class="col-sm-5" style="padding: 16px;">
                                    
                                         <span id="spandeccount" runat="server" style="font-size: 280%; font-weight: bold;">
                                         </span>
                                 <%--           <br />
                               <span id="spnadec" style="font-size:130%; font-weight: bold;">
                                         December
                                   </span>
                                 <br />--%>
                                    
                                   
                                </div>
                            </div>
                        </div>
                         <div class="row">
                            <div class="col-sm-4 text-center box">
                                <div class="col-sm-7">
                                        <div style="margin-top:10px">
                                            <img src="assets/images/dashboard-icon/jan.png" alt="" style="width: 100px;height: 78px;"/>
                                        </div>
                                    </div>
                                
                                    <div class="col-sm-5" style="padding: 16px;">
                                    
                                         <span id="spanjancount" runat="server" style="font-size: 280%; font-weight: bold;">
                                         </span>
                                 <%--           <br />
                               <span id="spanjan" style="font-size:130%; font-weight: bold;">
                                         January
                                   </span>
                                 <br />--%>
                                    
                                   
                                </div>
                            </div>
                            <div class="col-sm-4 text-center box">
                                <div class="col-sm-7">
                                        <div style="margin-top:10px">
                                            <img src="assets/images/dashboard-icon/feb.png" alt="" style="width: 100px;height: 78px;"/>
                                        </div>
                                    </div>
                                
                                    <div class="col-sm-5" style="padding: 16px;">
                                         <span id="spanfebcount" runat="server" style="font-size: 280%; font-weight: bold;">
                                         </span>
                                 <%--           <br />
                               <span id="spanfeb" style="font-size:130%; font-weight: bold;">
                                         February
                                   </span>
                                 <br />--%>
                                    <%--</div>--%>
                                   
                                </div>
                            </div>
                            <div class="col-sm-4 text-center box">
                                 <div class="col-sm-7">
                                        <div style="margin-top:10px">
                                            <img src="assets/images/dashboard-icon/mar.png" alt="" style="width: 100px;height: 78px;"/>
                                        </div>
                                    </div>
                                    <div class="col-sm-5" style="padding: 16px;">
                                         <span id="spanmarcount" runat="server" style="font-size: 280%; font-weight: bold;"></span>
                                     </div>
                            </div>
                        </div>
                    </div>
                     </div>
            </div>
        </div>
    </div>


        <br />
    </form>
</body>
</html>
