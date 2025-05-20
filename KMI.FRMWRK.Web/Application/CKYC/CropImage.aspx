<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CropImage.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.CKYC.CropImage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Common/Scripts/jquery-1.11.0.min.js"></script>
    <%--<script src="Common/CSS/jquery.min.js"></script>--%>
    <%--<script src="Common/CSS/jquery.Jcrop.js"></script>--%>
    <script src="Common/Scripts/jquery.Jcrop.js"></script>
    <script src="Common/Scripts/jquery.Jcrop.min.js"></script>
    <script src="../../Content/Bootstrap/js/bootstrap.js"></script>
    <link href="../../Content/Bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../Content/Bootstrap/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../Content/Bootstrap/css/animate.min.css" rel="stylesheet" />
    <link href="../../Content/kmi.framework.css" rel="stylesheet" />



    <script type="text/javascript">

        function popup() {
            debugger;
            $("#myModal").modal();
        }

        function doCancel() {
            window.parent.$find('<%=Request.QueryString["mdlpopup"].ToString()%>').hide();
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

        //function popup() {
        //    debugger;
        //    //$('#myModal').modal('hide');
        //    document.getElementById("myModalCrop").style.display = "none";
        //    $("#myModalCrop").modal();
        //    $("#myModal").modal();
        //}


        debugger;
        $(function () {
            $('#ImFullImage').Jcrop({
                onSelect: updateCoords,
                onChange: updateCoords
            });
        });
        function updateCoords(c) {
            $('#hfX').val(c.x);
            $('#hfY').val(c.y);
            $('#hfHeight').val(c.h);
            $('#hfWidth').val(c.w);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container" style="overflow-y: scroll;">
            <div class="panel panel-success" style="margin-left: 0px; margin-right: 0px">

                <%-- <table id="tbl1" style="border-collapse: separate; left: 0in; position: relative;  top: 0px; width: 90%; z-index: 4;" class="tableIsys" runat="server">--%>

                <div id="Div4" runat="server" class="panel-heading" onclick="showHideDiv('div5','btnTitle');return false;">
                    <div class="row" style="display: flex;">
                        <div class="col-sm-10" style="text-align: left; top: 0px; left: 0px;">
                            <span class="glyphicon glyphicon-menu-hamburger" style="color: Orange;"></span>
                            <asp:Label ID="lblTitle" Text="Image Crop" runat="server"
                                CssClass="control-label"></asp:Label>
                        </div>
                        <div class="col-sm-2" style="margin-left: 70%;">
                            <span id="btnTitle" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                        </div>
                    </div>
                </div>


                <div id="div5" style="display: block;" class="panel-body">
                    <div class="row" id="trInsuranceid1" runat="server">

                        <div class="col-sm-3" style="text-align: left">
                            <asp:Label ID="lbldocType" CssClass="control-label" Text="Document Type" runat="server" Font-Bold="False"></asp:Label>
                        </div>

                        <div class="col-sm-3">
                            <asp:TextBox ID="ddlDocType" runat="server" Enabled="false"
                                CssClass="form-control" TabIndex="11">
                                      
                            </asp:TextBox>
                        </div>
                    </div>
                
                <%--  <tr>--%>
                <center>
                            <div class="row" runat="server">
           <%--  <td colspan="4" align="center" style="height: 20px;">--%>
                               
             <div class="col-sm-12" style='margin-top:15px;'>
           <%-- Original Image: <br />--%>
     <%--ImageUrl="~/Application/ISys/Recruit/UploadedFiles/30000027/30000027_11.JPG"--%>
                 <div style="overflow:scroll !important;">
                    <asp:Image ID="ImFullImage"  runat="server" /><br />
                 </div>
      <asp:LinkButton ID="btnCrop" runat="server" CssClass="btn-animated bg-green" Text="Crop Image"  Class="btn-animated bg-green"
            onclick="btnCrop_Click" />
             <asp:LinkButton ID="btncancel" OnClick ="Btncancl_Click" Visible ="false"  runat="server" Text="Cancel"  Class="btn-animated bg-horrible"
               OnClientClick="doCancel();return false;" />
               </div>
           <%-- </td>--%>
            </div>
           
  
    <br />
    
      <div id="trcrop" class="row" runat="server">
        <div class="col-sm-12" style='margin-top:15px;'>
  <%--    Cropped Image:<br />--%>
        <asp:Image ID="imCropped" runat="server"/> <br/>
       
       <%--  <tr  id="trcrop" runat ="server" >--%>
                                <%--<td colspan="4" align="center" style="height: 20px;">--%>
                                <asp:LinkButton ID="btnok" runat="server" Text="OK"  CssClass="btn-animated bg-green"
                                   onclick="btnok_Click"  />  <%--OnClientClick="doCancel();return false;" --%>
                                </div>
        
    </div>
                    </center>
                    </div>

      <div class="modal fade" id="myModal" role="dialog" style="padding-left: 20%">
            <div class="modal-dialog modal-lg" style="width: 400px; margin-left:21%;">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header" style="text-align: center; background-color: #dff0d8;">
                        <asp:Label ID="Label161" Text="INFORMATION" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="modal-body" style="text-align: center">
                        <asp:Label ID="lblpopup" runat="server"></asp:Label>
                    </div>
                    <div class="modal-footer" style="text-align: center">
                        <button type="button" class="btn btn-primary" data-dismiss="modal">
                            <span class="glyphicon glyphicon-ok" style='color: White;'></span> OK
                        </button>
                    </div>
                </div>
            </div>
        </div>
     
            </div>
            <%--</table>--%>
        </div>


        <asp:HiddenField ID="hfX" runat="server" />
        <asp:HiddenField ID="hfY" runat="server" />
        <asp:HiddenField ID="hfHeight" runat="server" />
        <asp:HiddenField ID="hfWidth" runat="server" />
        <asp:HiddenField ID="hdnRegRefNo" runat="server" />
        <script type="text/javascript">
            function doCancel() {
                window.parent.$find('<%=Request.QueryString["mdlpopup"].ToString()%>').hide();
            }

            //function popup() {
            //    debugger;
            //    //$('#myModal').modal('hide');
            //    //setTimeout(function () {
            //        //document.getElementById("myModalCrop").style.display = "none";
            //        //$("#myModalCrop").modal();
            //    $("#myModal").modal();
            //    //}, 4000);
            //}

            
            </script>
        <%--  </asp:Content>--%>
    </form>
</body>
</html>
