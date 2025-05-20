<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="KMI.FRMWRK.Web.Application.Test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EmptyPagePlaceholder" runat="server">
    <div class="row custom-map">
        <div class="col-sm-3">
            <span class="control-label">Record Type    <span style="color: red; vertical-align: baseline">*</span>   </span>
            <asp:TextBox runat="server" ID="RecordType" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Line Number    <span style="color: red; vertical-align: baseline">*</span>   </span>
            <asp:TextBox runat="server" ID="LineNumber" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <asp:CheckBox Text="Application Type" runat="server" ID="ApplicationType" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Branch Code    <span style="color: red; vertical-align: baseline">*</span>   </span>
            <asp:TextBox runat="server" ID="BranchCode" CssClass="form-control" />
        </div>
    </div>
    <div class="row custom-map">
        <div class="col-sm-3">
            <span class="control-label">Applicant/Entity Name Update Flag   </span>
            <asp:TextBox runat="server" ID="ApplicantEntityNameUpdateFlag" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Personal/Entity Details Update Flag   </span>
            <asp:TextBox runat="server" ID="PersonalEntityDetailsUpdateFlag" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Address Details Update Flag   </span>
            <asp:TextBox runat="server" ID="AddressDetailsUpdateFlag" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Contact Details Update Flag   </span>
            <asp:TextBox runat="server" ID="ContactDetailsUpdateFlag" CssClass="form-control" />
        </div>
    </div>
    <div class="row custom-map">
        <div class="col-sm-3">
            <span class="control-label">Remarks Update Flag   </span>
            <asp:TextBox runat="server" ID="RemarksUpdateFlag" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">KYC Verification Update Flag   </span>
            <asp:TextBox runat="server" ID="KYCVerificationUpdateFlag" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Identity Details Update Flag   </span>
            <asp:TextBox runat="server" ID="IdentityDetailsUpdateFlag" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Related Person Details  Update Flag   </span>
            <asp:TextBox runat="server" ID="RelatedPersonDetailsUpdateFlag" CssClass="form-control" />
        </div>
    </div>
    <div class="row custom-map">
        <div class="col-sm-3">
            <span class="control-label">Account type update flag   </span>
            <asp:TextBox runat="server" ID="Accounttypeupdateflag" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Image Details Update Flag   </span>
            <asp:TextBox runat="server" ID="ImageDetailsUpdateFlag" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Constitution Type    <span style="color: red; vertical-align: baseline">*</span>   </span>
            <asp:DropDownList runat="server" ID="ConstitutionType" CssClass="form-control"></asp:DropDownList>
        </div>
        <div class="col-sm-3">
            <span class="control-label">Constitution Type others   </span>
            <asp:TextBox runat="server" ID="ConstitutionTypeothers" CssClass="form-control" />
        </div>
    </div>
    <div class="row custom-map">
        <div class="col-sm-3">
            <span class="control-label">Account Holder Type   </span>
            <asp:TextBox runat="server" ID="AccountHolderType" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Account Type    <span style="color: red; vertical-align: baseline">*</span>   </span>
            <asp:DropDownList runat="server" ID="AccountType" CssClass="form-control"></asp:DropDownList>
        </div>
        <div class="col-sm-3">
            <span class="control-label">CKYC no / FI reference No    <span style="color: red; vertical-align: baseline">*</span>   </span>
            <asp:TextBox runat="server" ID="CKYCnoFIreferenceNo" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Applicant Name Prefix    <span style="color: red; vertical-align: baseline">*</span>   </span>
            <asp:DropDownList runat="server" ID="ApplicantNamePrefix" CssClass="form-control"></asp:DropDownList>
        </div>
    </div>
    <div class="row custom-map">
        <div class="col-sm-3">
            <span class="control-label">Applicant First Name    <span style="color: red; vertical-align: baseline">*</span>   </span>
            <asp:TextBox runat="server" ID="ApplicantFirstName" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Applicant  Middle Name   </span>
            <asp:TextBox runat="server" ID="ApplicantMiddleName" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Applicant Last Name   </span>
            <asp:TextBox runat="server" ID="ApplicantLastName" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Name of the Entity   </span>
            <asp:TextBox runat="server" ID="NameoftheEntity" CssClass="form-control" />
        </div>
    </div>
    <div class="row custom-map">
        <div class="col-sm-3">
            <span class="control-label">Applicant Maiden Name Prefix   </span>
            <asp:DropDownList runat="server" ID="ApplicantMaidenNamePrefix" CssClass="form-control"></asp:DropDownList>
        </div>
        <div class="col-sm-3">
            <span class="control-label">Applicant Maiden First Name   </span>
            <asp:TextBox runat="server" ID="ApplicantMaidenFirstName" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Applicant Maiden Middle Name   </span>
            <asp:TextBox runat="server" ID="ApplicantMaidenMiddleName" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Applicant Maiden Last Name   </span>
            <asp:TextBox runat="server" ID="ApplicantMaidenLastName" CssClass="form-control" />
        </div>
    </div>
    <div class="row custom-map">
        <div class="col-sm-3">
            <span class="control-label">Applicant Maiden Full Name   </span>
            <asp:TextBox runat="server" ID="ApplicantMaidenFullName" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <asp:RadioButton Text="Flag indicating Father or Spouse Name" runat="server" ID="FlagindicatingFatherorSpouseName" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Applicant Father/Spouse Name Prefix   </span>
            <asp:DropDownList runat="server" ID="ApplicantFatherSpouseNamePrefix" CssClass="form-control"></asp:DropDownList>
        </div>
        <div class="col-sm-3">
            <span class="control-label">Father / Spouse First Name   </span>
            <asp:TextBox runat="server" ID="FatherSpouseFirstName" CssClass="form-control" />
        </div>
    </div>
    <div class="row custom-map">
        <div class="col-sm-3">
            <span class="control-label">Father / Spouse Middle Name   </span>
            <asp:TextBox runat="server" ID="FatherSpouseMiddleName" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Father / Spouse Last Name   </span>
            <asp:TextBox runat="server" ID="FatherSpouseLastName" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Father / Spouse Full Name   </span>
            <asp:TextBox runat="server" ID="FatherSpouseFullName" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Applicant Mother Name Prefix   </span>
            <asp:DropDownList runat="server" ID="ApplicantMotherNamePrefix" CssClass="form-control"></asp:DropDownList>
        </div>
    </div>
    <div class="row custom-map">
        <div class="col-sm-3">
            <span class="control-label">Mother's First Name   </span>
            <asp:TextBox runat="server" ID="MothersFirstName" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Mother's Middle Name   </span>
            <asp:TextBox runat="server" ID="MothersMiddleName" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Mother's Last Name   </span>
            <asp:TextBox runat="server" ID="MothersLastName" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Mother's Full Name   </span>
            <asp:TextBox runat="server" ID="MothersFullName" CssClass="form-control" />
        </div>
    </div>
    <div class="row custom-map">
        <div class="col-sm-3">
            <asp:RadioButton Text="Gender" runat="server" ID="Gender" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Marital status   </span>
            <asp:DropDownList runat="server" ID="Maritalstatus" CssClass="form-control"></asp:DropDownList>
        </div>
        <div class="col-sm-3">
            <span class="control-label">Nationality   </span>
            <asp:DropDownList runat="server" ID="Nationality" CssClass="form-control"></asp:DropDownList>
        </div>
        <div class="col-sm-3">
            <span class="control-label">Occupation Type   </span>
            <asp:DropDownList runat="server" ID="OccupationType" CssClass="form-control"></asp:DropDownList>
        </div>
    </div>
    <div class="row custom-map">
        <div class="col-sm-3">
            <span class="control-label">Date of Birth/ Date of Incorporation    <span style="color: red; vertical-align: baseline">*</span>   </span>
            <asp:TextBox runat="server" ID="DateofBirthDateofIncorporation" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Place of Incorporation    <span style="color: red; vertical-align: baseline">*</span>   </span>
            <asp:TextBox runat="server" ID="PlaceofIncorporation" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Date of Commencement of business   </span>
            <asp:TextBox runat="server" ID="DateofCommencementofbusiness" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Country of Incorporation    <span style="color: red; vertical-align: baseline">*</span>   </span>
            <asp:DropDownList runat="server" ID="CountryofIncorporation" CssClass="form-control"></asp:DropDownList>
        </div>
    </div>
    <div class="row custom-map">
        <div class="col-sm-3">
            <span class="control-label">Country of residence as per Tax Laws   </span>
            <asp:DropDownList runat="server" ID="CountryofresidenceasperTaxLaws" CssClass="form-control"></asp:DropDownList>
        </div>
        <div class="col-sm-3">
            <span class="control-label">Identification Type   </span>
            <asp:DropDownList runat="server" ID="IdentificationType" CssClass="form-control"></asp:DropDownList>
        </div>
        <div class="col-sm-3">
            <span class="control-label">TIN/GST Registration number   </span>
            <asp:TextBox runat="server" ID="TINGSTRegistrationnumber" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">TIN Issuing Country   </span>
            <asp:DropDownList runat="server" ID="TINIssuingCountry" CssClass="form-control"></asp:DropDownList>
        </div>
    </div>
    <div class="row custom-map">
        <div class="col-sm-3">
            <span class="control-label">PAN or form 60    <span style="color: red; vertical-align: baseline">*</span>   </span>
            <asp:TextBox runat="server" ID="PANorform60" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Residential Status   </span>
            <asp:TextBox runat="server" ID="ResidentialStatus" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <asp:RadioButton Text="Flag indicating if applicant resident for tax purposes in Jurisdiction outside India " runat="server" ID="FlagindicatingifapplicantresidentfortaxpurposesinJurisdictionoutsideIndia" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Jurisdiction of residence    </span>
            <asp:TextBox runat="server" ID="Jurisdictionofresidence" CssClass="form-control" />
        </div>
    </div>
    <div class="row custom-map">
        <div class="col-sm-3">
            <span class="control-label">TIN/GST Registration Number1   </span>
            <asp:TextBox runat="server" ID="TINGSTRegistrationnumber1" CssClass="form-control" />
        </div>

        <div class="col-sm-3">
            <span class="control-label">Country of Birth   </span>
            <asp:DropDownList runat="server" ID="CountryofBirth" CssClass="form-control"></asp:DropDownList>
        </div>
        <div class="col-sm-3">
            <span class="control-label">City/Place of Birth   </span>
            <asp:TextBox runat="server" ID="CityPlaceofBirth" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Current/ Permanent/ Overseas Address   Type   </span>
            <asp:DropDownList runat="server" ID="CurrentPermanentOverseasAddressType" CssClass="form-control"></asp:DropDownList>
        </div>
    </div>
    <div class="row custom-map">
        <div class="col-sm-3">
            <span class="control-label">Proof of Identity and Address  Line 1    <span style="color: red; vertical-align: baseline">*</span>   </span>
            <asp:TextBox runat="server" ID="ProofofIdentityandAddressLine1" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Proof of Identity and Address  Line 2   </span>
            <asp:TextBox runat="server" ID="ProofofIdentityandAddressLine2" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Proof of Identity and Address  Line 3   </span>
            <asp:TextBox runat="server" ID="ProofofIdentityandAddressLine3" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Proof of Identity and Address -  City / Town / Village    <span style="color: red; vertical-align: baseline">*</span>   </span>
            <asp:TextBox runat="server" ID="ProofofIdentityandAddressCityTownVillage" CssClass="form-control" />
        </div>
    </div>
    <div class="row custom-map">
        <div class="col-sm-3">
            <span class="control-label">Proof of Identity and Address -  District   </span>
            <asp:TextBox runat="server" ID="ProofofIdentityandAddressDistrict" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Proof of Identity and Address - State/ U.T   
             <span style="color: red; vertical-align: baseline">*</span>   </span>
            <div class="input-group">

            </div>
            <asp:DropDownList runat="server" ID="ProofofIdentityandAddressStateUT" CssClass="form-control"></asp:DropDownList>
        </div>
        <div class="col-sm-3">
            <span class="control-label">Proof of Identity and Address  - Country    <span style="color: red; vertical-align: baseline">*</span>   </span>
            <asp:DropDownList runat="server" ID="ProofofIdentityandAddressCountry" CssClass="form-control"></asp:DropDownList>
        </div>
        <div class="col-sm-3">
            <span class="control-label">Proof of Identity and Address -  PIN Code   </span>
            <asp:TextBox runat="server" ID="ProofofIdentityandAddressPINCode" CssClass="form-control" />
        </div>
    </div>
    <div class="row custom-map">
        <div class="col-sm-3">
            <span class="control-label">Proof of Address submitted for Proof of Identity and Address    <span style="color: red; vertical-align: baseline">*</span>   </span>
            <asp:DropDownList runat="server" ID="ProofofAddresssubmittedforProofofIdentityandAddress" CssClass="form-control"></asp:DropDownList>
        </div>
        <div class="col-sm-3">
            <span class="control-label">Proof of Address submitted for Proof of Identity and Address (Others)   </span>
            <asp:TextBox runat="server" ID="ProofofAddresssubmittedforProofofIdentityandAddressOthers" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <asp:RadioButton Text="Flag indicating if Proof of Identity and Address is same as current Address" runat="server" ID="FlagindicatingifProofofIdentityandAddressissameascurrentAddress" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Correspondence/ Local Address Type    <span style="color: red; vertical-align: baseline">*</span>   </span>
            <asp:DropDownList runat="server" ID="CorrespondenceLocalAddressType" CssClass="form-control"></asp:DropDownList>
        </div>
    </div>

    <div class="row custom-map">
        <div class="col-sm-3">
            <span class="control-label">Current Address -  District   </span>
            <asp:TextBox runat="server" ID="CurrentAddressDistrict" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Current Address - State   </span>
            <asp:DropDownList runat="server" ID="CurrentAddressState" CssClass="form-control"></asp:DropDownList>
        </div>
        <div class="col-sm-3">
            <span class="control-label">Current Address - Country    <span style="color: red; vertical-align: baseline">*</span>   </span>
            <asp:DropDownList runat="server" ID="CurrentAddressCountry" CssClass="form-control"></asp:DropDownList>
        </div>
        <div class="col-sm-3">
            <span class="control-label">Current Address -  PIN Code   </span>
            <asp:TextBox runat="server" ID="CurrentAddressPINCode" CssClass="form-control" />
        </div>
    </div>
    <div class="row custom-map">
        <div class="col-sm-3">
            <asp:RadioButton Text="Proof of address submitted for Current Address" runat="server" ID="ProofofaddresssubmittedforCurrentAddress" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <asp:RadioButton Text="Flag indicating if address in jurisdiction" runat="server" ID="Flagindicatingifaddressinjurisdiction" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Address in Jurisdiction Type   </span>
            <asp:DropDownList runat="server" ID="AddressinJurisdictionType" CssClass="form-control"></asp:DropDownList>
        </div>
        <div class="col-sm-3">
            <span class="control-label">Address in Jurisdiction Line 1   </span>
            <asp:TextBox runat="server" ID="AddressinJurisdictionLine1" CssClass="form-control" />
        </div>
    </div>
    <div class="row custom-map">
        <div class="col-sm-3">
            <span class="control-label">Address in Jurisdiction Line 2   </span>
            <asp:TextBox runat="server" ID="AddressinJurisdictionLine2" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Address in Jurisdiction Line 3   </span>
            <asp:TextBox runat="server" ID="AddressinJurisdictionLine3" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Address in Jurisdiction -  City / Town / Village   </span>
            <asp:TextBox runat="server" ID="AddressinJurisdictionCityTownVillage" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Address in Jurisdiction - State   </span>
            <asp:DropDownList runat="server" ID="AddressinJurisdictionState" CssClass="form-control"></asp:DropDownList>
        </div>
    </div>
    <div class="row custom-map">
        <div class="col-sm-3">
            <span class="control-label">Address in Jurisdiction - Country   </span>
            <asp:DropDownList runat="server" ID="AddressinJurisdictionCountry" CssClass="form-control"></asp:DropDownList>
        </div>
        <div class="col-sm-3">
            <span class="control-label">Address in Jurisdiction -  ZIP/Post Code   </span>
            <asp:TextBox runat="server" ID="AddressinJurisdictionZIPPostCode" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <asp:RadioButton Text="Proof of Address submitted for Address in Jurisdiction" runat="server" ID="ProofofAddresssubmittedforAddressinJurisdiction" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Residence Telephone No. (STD Code)   </span>
            <asp:TextBox runat="server" ID="ResidenceTelephoneNoSTDCode" CssClass="form-control" />
        </div>
    </div>
    <div class="row custom-map">
        <div class="col-sm-3">
            <span class="control-label">Residence Telephone No.   </span>
            <asp:TextBox runat="server" ID="ResidenceTelephoneNo" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Office Telephone No. (STD Code)   </span>
            <asp:TextBox runat="server" ID="OfficeTelephoneNoSTDCode" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Office Telephone No.   </span>
            <asp:TextBox runat="server" ID="OfficeTelephoneNo" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Mobile No. (ISD Code)   </span>
            <asp:TextBox runat="server" ID="MobileNoISDCode" CssClass="form-control" />
        </div>
    </div>
    <div class="row custom-map">
        <div class="col-sm-3">
            <span class="control-label">Mobile No.   </span>
            <asp:TextBox runat="server" ID="MobileNo" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Fax No. (STD Code)   </span>
            <asp:TextBox runat="server" ID="FaxNoSTDCode" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Fax No.   </span>
            <asp:TextBox runat="server" ID="FaxNo" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Email ID   </span>
            <asp:TextBox runat="server" ID="EmailID" CssClass="form-control" />
        </div>
    </div>
    <div class="row custom-map">
        <div class="col-sm-3">
            <span class="control-label">Remarks, if any   </span>
            <asp:TextBox runat="server" ID="Remarksifany" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Date of Declaration    <span style="color: red; vertical-align: baseline">*</span>   </span>
            <asp:TextBox runat="server" ID="DateofDeclaration" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Place of Declaration    <span style="color: red; vertical-align: baseline">*</span>   </span>
            <asp:TextBox runat="server" ID="PlaceofDeclaration" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">KYC Verification Date    <span style="color: red; vertical-align: baseline">*</span>   </span>
            <asp:TextBox runat="server" ID="KYCVerificationDate" CssClass="form-control" />
        </div>
    </div>
    <div class="row custom-map">
        <div class="col-sm-3">
            <span class="control-label">Type of Document Submitted     <span style="color: red; vertical-align: baseline">*</span>   </span>
            <asp:DropDownList runat="server" ID="TypeofDocumentSubmitted" CssClass="form-control"></asp:DropDownList>
        </div>
        <div class="col-sm-3">
            <span class="control-label">KYC Verification Name    <span style="color: red; vertical-align: baseline">*</span>   </span>
            <asp:TextBox runat="server" ID="KYCVerificationName" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">KYC Verification Designation    <span style="color: red; vertical-align: baseline">*</span>   </span>
            <asp:TextBox runat="server" ID="KYCVerificationDesignation" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">KYC Verification Branch    <span style="color: red; vertical-align: baseline">*</span>   </span>
            <asp:TextBox runat="server" ID="KYCVerificationBranch" CssClass="form-control" />
        </div>
    </div>
    <div class="row custom-map">
        <div class="col-sm-3">
            <span class="control-label">KYC Verification EMP code    <span style="color: red; vertical-align: baseline">*</span>   </span>
            <asp:TextBox runat="server" ID="KYCVerificationEMPcode" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Organisation Name    <span style="color: red; vertical-align: baseline">*</span>   </span>
            <asp:TextBox runat="server" ID="OrganisationName" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Organisation Code    <span style="color: red; vertical-align: baseline">*</span>   </span>
            <asp:TextBox runat="server" ID="OrganisationCode" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Number of Identity Details    <span style="color: red; vertical-align: baseline">*</span>   </span>
            <asp:TextBox runat="server" ID="NumberofIdentityDetails" CssClass="form-control" />
        </div>
    </div>
    <div class="row custom-map">
        <div class="col-sm-3">
            <span class="control-label">Number of related people    <span style="color: red; vertical-align: baseline">*</span>   </span>
            <asp:TextBox runat="server" ID="Numberofrelatedpeople" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <asp:RadioButton Text="Identity Verification Flag" runat="server" ID="IdentityVerificationFlag" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Number of Local Address Details   </span>
            <asp:TextBox runat="server" ID="NumberofLocalAddressDetails" CssClass="form-control" />
        </div>
        <div class="col-sm-3">
            <span class="control-label">Number of Images    <span style="color: red; vertical-align: baseline">*</span>   </span>
            <asp:TextBox runat="server" ID="NumberofImages" CssClass="form-control" />
        </div>
    </div>
    <div class="row custom-map">
        <div class="col-sm-3">
            <span class="control-label">Error Code   </span>
            <asp:TextBox runat="server" ID="ErrorCode" CssClass="form-control" />
        </div>

    </div>
</asp:Content>
