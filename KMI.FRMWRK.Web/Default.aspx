<%@ Page Title="CKYC Middleware Ver 2.0" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="KMI.FRMWRK.Web._Default" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <%: Styles.Render("~/bundles/FRMWRKcss") %>
    <%: Scripts.Render("~/bundles/FRMWRKjs") %>
</head>
<body style="width: 100%; padding-left: 0px;">
    <form id="form1" runat="server">

        <%--HEADER CONTENING FRMWRK NAME--%>
        <div class="bg-gradient">
            <div class="row" style="margin: 0">
                <div class="col-sm-12">
                    <div runat="server" id="DivMenu">
                    </div>
                </div>
            </div>

            <asp:Label ID="lblCBCRMInfo" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
        </div>

        <%--DIV TO DISPLAY MENU SELECTION PATH--%>
        <div style="padding-right: 5%; margin-top: 0.3%; margin-bottom: 0.3%; text-align: right;">
            <asp:Label ID="lblFileName" runat="server" Text="" Visible="false" ForeColor="Brown"></asp:Label>
        </div>

        <%--CONTENT FRAME DIV--%>
        <div id="iContentFrame" style="width: 100%; text-align: center;">
            <iframe name="__TARGETCONTENT" id="__TARGETCONTENT" runat="server" frameborder="0"
                class="framecontent"></iframe>
        </div>

        <asp:Button ID="btnGetMenuSelection" runat="server" Style="display: none;" Text="Button" OnClick="btnGetMenuSelection_Click" />
        <asp:HiddenField ID="HdnScrumbSelectedModule" runat="server" />
        <footer>
            <p style="margin: 5px 0 0px 0;">© 2017 - KMIFramework</p>
        </footer>
    </form>


    <script type="text/javascript">
        $(document).ready(function () {
            //debugger
            //if (window.matchMedia('(max-width: 315)')) {
            //    $('.navbar a.dropdown-toggle').on('click', function (e) {
            //        var $el = $(this);
            //        var $parent = $(this).offsetParent(".dropdown-menu");
            //        $(this).parent("li").toggleClass('open');

            //        if (!$parent.parent().hasClass('nav')) {
            //            $el.next().css({ "top": $el[0].offsetTop, "left": $parent.outerWidth() - 4 });
            //        }

            //        $('.nav li.open').not($(this).parents("li")).removeClass("open");

            //        return false;
            //    });
            //}
            //else {
            $('.navbar a.dropdown-toggle').mouseover(function (e) {
                var $el = $(this);
                var $parent = $(this).offsetParent(".dropdown-menu");
                $(this).parent("li").toggleClass('open');

                if (!$parent.parent().hasClass('nav')) {
                    $el.next().css({ "top": $el[0].offsetTop, "left": $parent.outerWidth() - 4 });
                }

                $('.nav li.open').not($(this).parents("li")).removeClass("open");

                return false;
            });
            //}

        });

    </script>


    <script type="text/javascript">

        var objResize = 0;
        window.onresize = function initResizeTimer() {
            if (objResize == 0) {
                objResize = 1;
                window.clearTimeout('resizeTimer');
                resizeTimer = window.setTimeout(FinishResize, 0);
            }
        };

        function FinishResize() {
            objResize = 0;
        }

        function RenderPage(id) {
            /// debugger;
            document.getElementById("HdnScrumbSelectedModule").value = id;
            document.getElementById('btnGetMenuSelection').click();
        }
    </script>
</body>
</html>
