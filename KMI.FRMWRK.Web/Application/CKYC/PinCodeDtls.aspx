<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="PinCodeDtls.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.CKYC.PinCodeDtls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/bundles/CKYCValidationjs") %>
    <%: Styles.Render("~/bundles/CKYCcss") %>

    <script type="text/javascript">
        function closeWin() {
            window.close();   // Closes the new window
        }
    </script>

    <script type="text/javascript">
        function ContactDetails() {//ddlPincodeList

            if (document.getElementById("<%= ddlPincodeList.ClientID %>").value != "") {

                var flagvalue = document.getElementById("<%= hdnflag.ClientID %>").value;

                if (flagvalue == 'Flag1') {
                    var e = document.getElementById("<%= ddlPincodeList.ClientID %>"); // select element    
                    var Pincode = e.options[e.selectedIndex].value;
                    var txtPinCode = window.opener.document.getElementById("EmptyPagePlaceholder_txtPinCode");
                    var txtName = window.opener.document.getElementById("EmptyPagePlaceholder_txtDistrictname");
                    txtPinCode.value = Pincode;
                    txtName.value = document.getElementById("<%= txtDistrict.ClientID %>").value;
                    window.close();
                }

                if (flagvalue == 'Flag2') {
                    var e1 = document.getElementById("<%= ddlPincodeList.ClientID %>"); // select element    
                    var Pincode1 = e1.options[e1.selectedIndex].value;
                    var txtPinCode1 = window.opener.document.getElementById("EmptyPagePlaceholder_ddlPinCode1");
                    var txtName1 = window.opener.document.getElementById("EmptyPagePlaceholder_txtDistrict1");
                    txtPinCode1.value = Pincode1;
                    txtName1.value = document.getElementById("<%= txtDistrict.ClientID %>").value;
                    window.close();
                }

                if (flagvalue == 'Flag3') {
                    var e2 = document.getElementById("<%= ddlPincodeList.ClientID %>"); // select element    
                    var Pincode2 = e2.options[e2.selectedIndex].value;
                    var txtPinCode2 = window.opener.document.getElementById("EmptyPagePlaceholder_ddlPinCode2");
                    var txtName2 = window.opener.document.getElementById("EmptyPagePlaceholder_txtDistrict2");
                    txtPinCode2.value = Pincode2;
                    txtName2.value = document.getElementById("<%= txtDistrict.ClientID %>").value;
                    window.close();
                }
                debugger;
                if (flagvalue == '1') {
                    var pincode = document.getElementById("<%= ddlPincodeList.ClientID %>").value;
                    var district = document.getElementById("<%= txtDistrict.ClientID %>").value;
                    window.opener.BindDistrictPincode(flagvalue, district, pincode)
                    window.close();
                }
                if (flagvalue == '2') {
                    var pincode = document.getElementById("<%= ddlPincodeList.ClientID %>").value;
                    var district = document.getElementById("<%= txtDistrict.ClientID %>").value;
                    window.opener.BindDistrictPincode(flagvalue, district, pincode)
                    window.close();
                }
                if (flagvalue == '3') {
                    var pincode = document.getElementById("<%= ddlPincodeList.ClientID %>").value;
                    var district = document.getElementById("<%= txtDistrict.ClientID %>").value;
                    window.opener.BindDistrictPincode(flagvalue, district, pincode)
                    window.close();
                }
            }
            else {
                AlertMsg("Please select Pincode");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">

    <asp:ScriptManager ID="SM1" runat="server">
    </asp:ScriptManager>
    <div class="panel panel-body">
        <div class="row">
            <div class="col-sm-3">
                <label>Enter PinCode</label>
                <span style="color: red">*</span>
            </div>
            <div class="col-sm-3">
                <%--  <asp:TextBox runat="server" ID="txt_PincodeList" CssClass="autosuggest form-control" placeholder="Pin/Post Code"></asp:TextBox>--%>

                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:DropDownList runat="server" ID="ddlPincodeList" OnSelectedIndexChanged="ddlPincodeList_SelectedIndexChanged" CssClass="autosuggest form-control" AutoPostBack="true"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <div class="col-sm-3">
                <label>District</label>
            </div>
            <div class="col-sm-3">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:TextBox runat="server" ID="txtDistrict" CssClass="form-control" placeholder="District" Enabled="false"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-12">
                <center>
                <asp:LinkButton ID="btnadd" runat="server" CssClass="btn-animated bg-green"  OnClientClick="ContactDetails()">
                    <span class="glyphicon glyphicon glyphicon-plus BtnGlyphicon"> </span> Add
                </asp:LinkButton>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               <%-- <button type="button" ID="btnadd" runat="server" CssClass="btn-animated bg-green" Text="" OnClientClick="ContactDetails()"><span class="glyphicon glyphicon glyphicon-plus BtnGlyphicon"> </span> Add</button>--%>
                <button type="button" class="btn-animated bg-horrible" data-dismiss="modal" onclick="closeWin()"><span class="glyphicon glyphicon glyphicon-remove BtnGlyphicon"> </span> Close</button>


                </center>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hdnflag" runat="server" />
</asp:Content>
