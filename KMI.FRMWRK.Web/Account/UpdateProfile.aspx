<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="UpdateProfile.aspx.cs" Inherits="KMI.FRMWRK.Web.Account.UpdateProfile" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">
    <link href="../Content/Bootstrap/css/jquery.Jcrop.css" rel="stylesheet" />
    <script src="../Content/Bootstrap/js/jquery.Jcrop.min.js"></script>
    <script src="../Content/Bootstrap/js/jquery.color.js"></script>

    <%--STYLE AND SCRIPT FOR IMAGE CROP--%>
    <style type="text/css">
        /* leave this part out */
        /* leave this part out */
        .uploadbtn {
            border: none;
            padding: 11px 15px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
            margin: 4px 2px;
            cursor: pointer;
            -webkit-transition-duration: 0.4s; /* Safari */
            transition-duration: 0.4s;
            background-color: #1bbc9b;
            color: white;
        }

            .uploadbtn:hover {
                box-shadow: 0 12px 16px 0 rgba(0,0,0,0.24), 0 17px 50px 0 rgba(0,0,0,0.19);
            }

        .clearfix {
            *zoom: 1;
        }

            .clearfix:before, .clearfix:after {
                display: table;
                content: "";
                line-height: 0;
            }

            .clearfix:after {
                clear: both;
            }

        .hide-text {
            font: 0/0 a;
            color: transparent;
            text-shadow: none;
            background-color: transparent;
            border: 0;
        }

        .input-block-level {
            display: block;
            width: 100%;
            min-height: 30px;
            -webkit-box-sizing: border-box;
            -moz-box-sizing: border-box;
            box-sizing: border-box;
        }

        .btn-file {
            overflow: hidden;
            position: relative;
            vertical-align: middle;
        }

            .btn-file > input {
                position: absolute;
                top: 0;
                right: 0;
                margin: 0;
                opacity: 0;
                filter: alpha(opacity=0);
                transform: translate(-300px, 0) scale(4);
                font-size: 23px;
                direction: ltr;
                cursor: pointer;
            }

        .fileupload {
            margin-bottom: 9px;
        }

            .fileupload .uneditable-input {
                display: inline-block;
                margin-bottom: 0px;
                vertical-align: middle;
                cursor: text;
            }

            .fileupload .thumbnail {
                overflow: hidden;
                display: inline-block;
                margin-bottom: 5px;
                vertical-align: middle;
                text-align: center;
            }

                .fileupload .thumbnail > img {
                    display: inline-block;
                    vertical-align: middle;
                    max-height: 100%;
                }

            .fileupload .btn {
                vertical-align: middle;
            }

        .fileupload-exists .fileupload-new, .fileupload-new .fileupload-exists {
            display: none;
        }

        .fileupload-inline .fileupload-controls {
            display: inline;
        }

        .fileupload-new .input-append .btn-file {
            -webkit-border-radius: 0 3px 3px 0;
            -moz-border-radius: 0 3px 3px 0;
            border-radius: 0 3px 3px 0;
        }

        .thumbnail-borderless .thumbnail {
            border: none;
            padding: 0;
            -webkit-border-radius: 0;
            -moz-border-radius: 0;
            border-radius: 0;
            -webkit-box-shadow: none;
            -moz-box-shadow: none;
            box-shadow: none;
        }

        .fileupload-new.thumbnail-borderless .thumbnail {
            border: 1px solid #ddd;
        }
    </style>

    <script type="text/javascript">
        !function (e) { var t = function (t, n) { this.$element = e(t), this.type = this.$element.data("uploadtype") || (this.$element.find(".thumbnail").length > 0 ? "image" : "file"), this.$input = this.$element.find(":file"); if (this.$input.length === 0) return; this.name = this.$input.attr("name") || n.name, this.$hidden = this.$element.find('input[type=hidden][name="' + this.name + '"]'), this.$hidden.length === 0 && (this.$hidden = e('<input type="hidden" />'), this.$element.prepend(this.$hidden)), this.$preview = this.$element.find(".fileupload-preview"); var r = this.$preview.css("height"); this.$preview.css("display") != "inline" && r != "0px" && r != "none" && this.$preview.css("line-height", r), this.original = { exists: this.$element.hasClass("fileupload-exists"), preview: this.$preview.html(), hiddenVal: this.$hidden.val() }, this.$remove = this.$element.find('[data-dismiss="fileupload"]'), this.$element.find('[data-trigger="fileupload"]').on("click.fileupload", e.proxy(this.trigger, this)), this.listen() }; t.prototype = { listen: function () { this.$input.on("change.fileupload", e.proxy(this.change, this)), e(this.$input[0].form).on("reset.fileupload", e.proxy(this.reset, this)), this.$remove && this.$remove.on("click.fileupload", e.proxy(this.clear, this)) }, change: function (e, t) { if (t === "clear") return; var n = e.target.files !== undefined ? e.target.files[0] : e.target.value ? { name: e.target.value.replace(/^.+\\/, "") } : null; if (!n) { this.clear(); return } this.$hidden.val(""), this.$hidden.attr("name", ""), this.$input.attr("name", this.name); if (this.type === "image" && this.$preview.length > 0 && (typeof n.type != "undefined" ? n.type.match("image.*") : n.name.match(/\.(gif|png|jpe?g)$/i)) && typeof FileReader != "undefined") { var r = new FileReader, i = this.$preview, s = this.$element; r.onload = function (e) { i.html('<img src="' + e.target.result + '" ' + (i.css("max-height") != "none" ? 'style="max-height: ' + i.css("max-height") + ';"' : "") + " />"), s.addClass("fileupload-exists").removeClass("fileupload-new") }, r.readAsDataURL(n) } else this.$preview.text(n.name), this.$element.addClass("fileupload-exists").removeClass("fileupload-new") }, clear: function (e) { this.$hidden.val(""), this.$hidden.attr("name", this.name), this.$input.attr("name", ""); if (navigator.userAgent.match(/msie/i)) { var t = this.$input.clone(!0); this.$input.after(t), this.$input.remove(), this.$input = t } else this.$input.val(""); this.$preview.html(""), this.$element.addClass("fileupload-new").removeClass("fileupload-exists"), e && (this.$input.trigger("change", ["clear"]), e.preventDefault()) }, reset: function (e) { this.clear(), this.$hidden.val(this.original.hiddenVal), this.$preview.html(this.original.preview), this.original.exists ? this.$element.addClass("fileupload-exists").removeClass("fileupload-new") : this.$element.addClass("fileupload-new").removeClass("fileupload-exists") }, trigger: function (e) { this.$input.trigger("click"), e.preventDefault() } }, e.fn.fileupload = function (n) { return this.each(function () { var r = e(this), i = r.data("fileupload"); i || r.data("fileupload", i = new t(this, n)), typeof n == "string" && i[n]() }) }, e.fn.fileupload.Constructor = t, e(document).on("click.fileupload.data-api", '[data-provides="fileupload"]', function (t) { var n = e(this); if (n.data("fileupload")) return; n.fileupload(n.data()); var r = e(t.target).closest('[data-dismiss="fileupload"],[data-trigger="fileupload"]'); r.length > 0 && (r.trigger("click.fileupload"), t.preventDefault()) }) }(window.jQuery)
    </script>

    <%--STYLE AND SCRIPT FOR IMAGE CROP--%>

    <script type="text/javascript">
        //CROP IMAGE 
        jQuery(document).ready(function () {
            jQuery('#EmptyPagePlaceholder_imgCrop').Jcrop({
                onSelect: storeCoords
            });
        });

        function storeCoords(c) {
            jQuery('#EmptyPagePlaceholder_X').val(c.x);
            jQuery('#EmptyPagePlaceholder_Y').val(c.y);
            jQuery('#EmptyPagePlaceholder_W').val(c.w);
            jQuery('#EmptyPagePlaceholder_H').val(c.h);
        };

        //VALIDATE FILE SIZE
        function UploadFile() {
            alert("Photo size should not be greater than 10 KB.");
            return false;
        }

        //OPEN POPUP 
        function getCallCrop() {
            debugger;
            var headerCSS = 'alert-success';
            var msg = $('#EmptyPagePlaceholder_divCrop').html();
            showModal('#myModal', 'Crop Image', headerCSS, '', '', msg);
        }

        function ShowModalPopup() {
            getCallCrop();
            return false;
        }

        function validateAddress() {
            if (document.getElementById("TxtAddress").value.length >= 50) {
                alert("Invalid length, accept character length upto 50.");
                document.form1.TxtAddress.value = "";
                return false;
            }
            return true;
        }

        //function getImage() {
        //    $('#btnGetImage').click();
        //}

    </script>
    <div>
        <div class="panel panel-success">
            <div class="panel-heading" onclick="showHideDiv('divMangerPro','btnToggle');return false;">
                <div class="row">
                    <div class="col-sm-12">
                        <span class="glyphicon glyphicon-menu-hamburger"></span> Manage My Profile
                        <span id="btnToggle" class="glyphicon glyphicon-collapse-down" style="float: right; padding: 1px 10px ! important; font-size: 18px;"></span>
                    </div>
                </div>
            </div>
            <div id="divMangerPro" class="panel-body">
                <div class="row">
                    <div class="col-sm-3">
                        <asp:Label runat="server" CssClass="control-label">User ID</asp:Label>
                    </div>
                    <div class="col-sm-3">
                        <asp:Label ID="lblUserId" runat="server" CssClass="form-control custom-disable"></asp:Label>
                    </div>
                    <div class="col-sm-3">
                        <asp:Label  runat="server" CssClass="control-label">User Name</asp:Label>
                    </div>
                    <div class="col-sm-3">
                       <asp:Label ID="lblUserName" runat="server" CssClass="form-control custom-disable"></asp:Label>
                    </div>
                    <div class="col-sm-3">
                        <asp:Label runat="server" CssClass="control-label">Gender</asp:Label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlGender_SelectedIndexChanged">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Male</asp:ListItem>
                            <asp:ListItem Value="2">Female</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-3">
                        <asp:Label runat="server" CssClass="control-label">Contact No</asp:Label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtContactNo" runat="server" CssClass="form-control" MaxLength="10"
                            onblur="fncInputNumericValuesOnly()" placeholder="Contact No"></asp:TextBox>
                    </div>
                    <div class="col-sm-3">
                        <asp:Label runat="server" CssClass="control-label">Email Address</asp:Label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtEmailId" runat="server" CssClass="form-control"
                            onblur="funEmailValidation(document.form1.txtEmailId)" placeholder="Email Address"></asp:TextBox>
                    </div>
                    <div class="col-sm-3">
                        <asp:Label runat="server" CssClass="control-label">Residental Address</asp:Label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" Height="70px"
                            TextMode="MultiLine" onblur="validateAddress()" placeholder="Residental Address"></asp:TextBox>
                    </div>
                    <div class="col-sm-3">
                        <asp:Label ID="lblFileUploadImage" runat="server" CssClass="control-label">Upload Image</asp:Label>
                    </div>
                    <div class="col-sm-3">
                        <%--<asp:FileUpload ID="FileUploadImage" runat="server" CssClass="form-control" Style="float: left;" />--%>
                        <div class="fileupload fileupload-new" data-provides="fileupload">
                            <span class="uploadbtn btn-file"><span class="fileupload-new">Select file</span>
                                <span class="fileupload-exists">Change</span>
                                <input id="inputFileUpload" runat="server" type="file" /></span>
                            <asp:Label ID="fileName" runat="server" class="fileupload-preview"></asp:Label>
                            <a href="#" class="close fileupload-exists" data-dismiss="fileupload" style="float: none">×</a>
                        </div>
                    </div>
                    <div class="col-sm-2">
                        <asp:Button runat="server" ID="btnGetImage" OnClick="btnGetImage_Click" Text="Upload" CssClass="btn-animated bg-green"></asp:Button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-3">
                        <asp:Label ID="lblImagePreView" CssClass="control-label" runat="server">Digital preview</asp:Label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Image ID="ImagePreView" runat="server" ImageUrl="/Content/Images/ProfileImage/download.jpg"
                            Width="100px" BorderStyle="Solid" BorderWidth="1px" Height="110px" onclick="return ShowModalPopup()" />
                    </div>
                </div>
            </div>
        </div>
        <div style="align-content: center;">
            <div>
                <div class="col-sm-12" style="text-align: center">
                    <asp:Button ID="btnUpdate" CssClass="btn-animated bg-green" runat="server"
                        Text="Update" OnClick="btnUpdate_Click"></asp:Button>
                    <asp:Button ID="btnSave" CssClass="btn-animated bg-green" runat="server"
                        Visible="false" OnClick="btnSave_Click" Text="Save"></asp:Button>
                    <asp:Button ID="btnClear" CssClass="btn-animated bg-horrible" runat="server"
                        Visible="true" OnClick="btnClear_Click" Text="Clear"></asp:Button>
                    <asp:Button ID="btnCancel" CssClass="btn-animated bg-horrible" runat="server"
                        Visible="true" OnClick="btnCancel_Click" Text="Cancel"></asp:Button>
                </div>
            </div>
        </div>
        <div style="width: 97%;">
            <div>
                <div style="text-align: center; width: 100%">
                    <asp:Label ID="lblError" runat="server" ForeColor="red" Font-Bold="true" CssClass="control-label"></asp:Label>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="myModal" role="dialog">
    </div>

    <%--CROP IMAGE POPUP CONTENT--%>
    <div id="divCrop" runat="server" style="display: none;">
        <div style="text-align: center;">
            <asp:Image ID="Imgfullimage" AlternateText="Not Available" runat="server" ImageUrl="~/Content/Images/ProfileImage/download.jpg"
                Width="200px" BorderStyle="Solid" BorderWidth="1px" Height="200px" />
        </div>
        <div id="pnlCrop" runat="server" visible="false" style="text-align: center;">
            <asp:Image ID="imgCrop" runat="server" />
            <asp:HiddenField ID="X" runat="server" />
            <asp:HiddenField ID="Y" runat="server" />
            <asp:HiddenField ID="W" runat="server" />
            <asp:HiddenField ID="H" runat="server" />
            <div style="text-align: center;">
                <asp:Button ID="btnCrop" CssClass="btn-animated bg-green" runat="server" Text="Crop" OnClick="btnCrop_Click"
                    OnClientClick="if ( !confirm('Do you want to submit image?')) return false;" />
            </div>
        </div>
    </div>
    <asp:HiddenField ID="HdnFilepath" runat="server" />

</asp:Content>
