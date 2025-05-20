using KMI.FRMWRK.DAL;
using KMI.FRMWRK.Web.Application.CKYC.Common;
using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMI.FRMWRK.Web.Application.CKYC
{
    public partial class IndividualRegistration : System.Web.UI.Page
    {
        CommonClass cc = new CommonClass();
        CommonUtility oCommonUtility = new CommonUtility();
        int AppID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ControlSetup();

                    ProofofIdentityandAddressStateUT.Items.Insert(0, new ListItem("Select", ""));
                    CurrentAddressState.Items.Insert(0, new ListItem("Select", ""));
                    AddressinJurisdictionState.Items.Insert(0, new ListItem("Select", ""));

                    DivDdlAddressinJurisdictionState.Visible = true;
                    DivTxtAddressinJurisdictionState.Visible = false;

                    DivDdlCurrentAddressState.Visible = true;
                    DivTxtCurrentAddressState.Visible = false;

                    DivDdlProofofIdentityandAddressStateUT.Visible = true;
                    DivTxtProofofIdentityandAddressStateUT.Visible = false;

                    DivPOACurrent.Style.Add("display", "none");
                    DivPOACurrentExpDate.Style.Add("display", "none");

                    DivPOIExpDate.Style.Add("display", "none");
                    DivPOINo.Style.Add("display", "none");

                    DivPOAIdentExpDate.Style.Add("display", "none");
                    DivPOAIdentNo.Style.Add("display", "none");
                }
            }
            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    ErrorLog objErr = new ErrorLog();
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "Fillcountrycd1", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "CKYC");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }

        public void Hide_Controls()
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ConstType", "Individual");
            DataAccessLayer objDAL = new DataAccessLayer("CKYCConnectionString");
            DataTable dt = objDAL.GetDataTable("Prc_getHiddenControls");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string Div = Convert.ToString(dt.Rows[0]["ContainerDiv"]);
                if (Div != "")
                    FindControl(Div).Visible = false; ;
            }
        }



        public void ControlSetup()
        {
            //Application Type
            ApplicationType.Items[0].Selected = true;
            ApplicationType.Items[0].Enabled = false;


            //Type of document Submitted
            cc.FillDocumentReceived(TypeofDocumentSubmitted, "KI");

            //Identification Type
            //oCommonUtility.GetCKYC(IdentificationType, "KEntIdentTyp");
            //IdentificationType.Items.Insert(0, new ListItem("Select", ""));

            //Country
            FillCountry();

            //Update Flag
            SetUpdateFlag();

            //Constitution Type
            oCommonUtility.GetCKYC(ConstitutionType, "KConstTyp");
            ConstitutionType.Items.Insert(0, new ListItem("Select", ""));

            //Prefix
            oCommonUtility.GetCKYC(ApplicantNamePrefix, "KTitle");
            ApplicantNamePrefix.Items.Insert(0, new ListItem("Select", ""));

            oCommonUtility.GetCKYC(ApplicantMaidenNamePrefix, "KTitle");
            ApplicantMaidenNamePrefix.Items.Insert(0, new ListItem("Select", ""));

            oCommonUtility.GetCKYC(ApplicantFatherSpouseNamePrefix, "KTitle");
            ApplicantFatherSpouseNamePrefix.Items.Insert(0, new ListItem("Select", ""));

            oCommonUtility.GetCKYC(ApplicantMotherNamePrefix, "KMTitle");
            ApplicantMotherNamePrefix.Items.Insert(0, new ListItem("Select", ""));

            //Marital status
            //oCommonUtility.GetCKYC(Maritalstatus, "KMstatus");
            //Maritalstatus.Items.Insert(0, new ListItem("Select", "")); 
            //Change By tushar 

            //Nationality
            //oCommonUtility.GetCKYC(Nationality, "KCitizn");
            //Nationality.Items.Insert(0, new ListItem("Select", ""));
            //Change By tushar 

            //OccupationType
            //oCommonUtility.GetCKYC(OccupationType, "KOcc");
            //OccupationType.Items.Insert(0, new ListItem("Select", ""));
            //Change By tushar 

            //Gender
            oCommonUtility.GetCKYC(Gender, "KGender");
            Gender.Items.Insert(0, new ListItem("Select", ""));

            //Address Type
            //oCommonUtility.GetCKYC(CorrespondenceLocalAddressType, "KAddr");
            //CorrespondenceLocalAddressType.Items.Insert(0, new ListItem("Select", ""));

            //oCommonUtility.GetCKYC(AddressinJurisdictionType, "KAddr");
            //AddressinJurisdictionType.Items.Insert(0, new ListItem("Select", ""));

            //oCommonUtility.GetCKYC(CurrentPermanentOverseasAddressType, "KAddr");
            //CurrentPermanentOverseasAddressType.Items.Insert(0, new ListItem("Select", ""));
            //Change By tushar 

            //Address Proof
            oCommonUtility.GetCKYC(ProofofAddresssubmittedforProofofIdentityandAddress, "KAddrPrf");
            ProofofAddresssubmittedforProofofIdentityandAddress.Items.Insert(0, new ListItem("Select", ""));

            oCommonUtility.GetCKYC(ProofofaddresssubmittedforCurrentAddress, "KAddrPrf");
            ProofofaddresssubmittedforCurrentAddress.Items.Insert(0, new ListItem("Select", ""));

            //oCommonUtility.GetCKYC(ProofofAddresssubmittedforAddressinJurisdiction, "KAddrPrf");
            //ProofofAddresssubmittedforAddressinJurisdiction.Items.Insert(0, new ListItem("Select", ""));

            //Residental Status 
            //oCommonUtility.GetCKYC(ResidentialStatus, "KResi");
            //ResidentialStatus.Items.Insert(0, new ListItem("Select", ""));
            //Change By tushar 

            // Account type
            oCommonUtility.GetCKYC(AccountType, "KAccountType");
            AccountType.Items.Insert(0, new ListItem("Select", ""));

            //Identity Proof
            oCommonUtility.GetCKYC(ddlPOI, "KId");
            ddlPOI.Items.Insert(0, new ListItem("Select", ""));

        }

        public void SetUpdateFlag()
        {
            CommonUtility oCommonUtility = new CommonUtility();

            //Update Flag
            //oCommonUtility.GetCKYC(ApplicantEntityNameUpdateFlag, "KUpdateFlag");
            //ApplicantEntityNameUpdateFlag.Items.Insert(0, new ListItem("Select", ""));

            //oCommonUtility.GetCKYC(PersonalEntityDetailsUpdateFlag, "KUpdateFlag");
            //PersonalEntityDetailsUpdateFlag.Items.Insert(0, new ListItem("Select", ""));

            //oCommonUtility.GetCKYC(AddressDetailsUpdateFlag, "KUpdateFlag");
            //AddressDetailsUpdateFlag.Items.Insert(0, new ListItem("Select", ""));

            //oCommonUtility.GetCKYC(ContactDetailsUpdateFlag, "KUpdateFlag");
            //ContactDetailsUpdateFlag.Items.Insert(0, new ListItem("Select", ""));

            //oCommonUtility.GetCKYC(RemarksUpdateFlag, "KUpdateFlag");
            //RemarksUpdateFlag.Items.Insert(0, new ListItem("Select", ""));

            //oCommonUtility.GetCKYC(KYCVerificationUpdateFlag, "KUpdateFlag");
            //KYCVerificationUpdateFlag.Items.Insert(0, new ListItem("Select", ""));

            //oCommonUtility.GetCKYC(IdentityDetailsUpdateFlag, "KUpdateFlag");
            //IdentityDetailsUpdateFlag.Items.Insert(0, new ListItem("Select", ""));

            //oCommonUtility.GetCKYC(RelatedPersonDetailsUpdateFlag, "KUpdateFlag");
            //RelatedPersonDetailsUpdateFlag.Items.Insert(0, new ListItem("Select", ""));

            //oCommonUtility.GetCKYC(Accounttypeupdateflag, "KUpdateFlag");
            //Accounttypeupdateflag.Items.Insert(0, new ListItem("Select", ""));

            //oCommonUtility.GetCKYC(ImageDetailsUpdateFlag, "KUpdateFlag");
            //ImageDetailsUpdateFlag.Items.Insert(0, new ListItem("Select", ""));
        }

        public void FillCountry()
        {
            try
            {
                DataAccessLayer objDAL = new DataAccessLayer("CKYCConnectionString");
                DataTable dt = objDAL.GetDataTable("Prc_GetcountrycodeCKYC");
                if (dt.Rows.Count > 0)
                {
                    //oCommonUtility.FillDropDown(CountryofIncorporation, dt, "Country_CODE", "Country_Desc", true);
                    //oCommonUtility.FillDropDown(CountryofresidenceasperTaxLaws, dt, "Country_CODE", "Country_Desc", true);
                    //oCommonUtility.FillDropDown(TINIssuingCountry, dt, "Country_CODE", "Country_Desc", true);
                    //oCommonUtility.FillDropDown(CountryofBirth, dt, "Country_CODE", "Country_Desc", true);
                    oCommonUtility.FillDropDown(ProofofIdentityandAddressCountry, dt, "Country_CODE", "Country_Desc", true);
                    oCommonUtility.FillDropDown(CurrentAddressCountry, dt, "Country_CODE", "Country_Desc", true);
                    oCommonUtility.FillDropDown(AddressinJurisdictionCountry, dt, "Country_CODE", "Country_Desc", true);
                    //oCommonUtility.FillDropDown(Jurisdictionofresidence, dt, "Country_CODE", "Country_Desc", true);
                }
            }
            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    ErrorLog objErr = new ErrorLog();
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "Fillcountrycd1", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "CKYC");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    ErrorLog objErr = new ErrorLog();
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "Fillcountrycd1", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "CKYC");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("@RecordType", "");
                ht.Add("@LineNumber", "");
                ht.Add("@ApplicationType", ApplicationType.SelectedValue);
                ht.Add("@BranchCode", "");
                ht.Add("@ApplicantEntityNameUpdateFlag", "");
                ht.Add("@PersonalEntityDetailsUpdateFlag", "");
                ht.Add("@AddressDetailsUpdateFlag", "");
                ht.Add("@ContactDetailsUpdateFlag", "");
                ht.Add("@RemarksUpdateFlag", "");
                ht.Add("@KYCVerificationUpdateFlag", "");
                ht.Add("@IdentityDetailsUpdateFlag", "");
                ht.Add("@RelatedPersonDetailsUpdateFlag", "");
                ht.Add("@Accounttypeupdateflag", "");
                ht.Add("@ImageDetailsUpdateFlag", "");
                ht.Add("@ConstitutionType", ConstitutionType.SelectedValue);
                ht.Add("@ConstitutionTypeothers", "");
                ht.Add("@AccountHolderType", "");
                ht.Add("@AccountType", AccountType.SelectedValue);
                ht.Add("@CKYCnoFIreferenceNo", CKYCnoFIreferenceNo.Text);
                ht.Add("@ApplicantNamePrefix", ApplicantNamePrefix.SelectedValue);
                ht.Add("@ApplicantFirstName", ApplicantFirstName.Text);
                ht.Add("@ApplicantMiddleName", ApplicantMiddleName.Text);
                ht.Add("@ApplicantLastName", ApplicantLastName.Text);
                ht.Add("@NameoftheEntity", "");
                ht.Add("@ApplicantMaidenNamePrefix", ApplicantMaidenNamePrefix.SelectedValue);
                ht.Add("@ApplicantMaidenFirstName", ApplicantMaidenFirstName.Text);
                ht.Add("@ApplicantMaidenMiddleName", ApplicantMaidenMiddleName.Text);
                ht.Add("@ApplicantMaidenLastName", ApplicantMaidenLastName.Text);
                ht.Add("@ApplicantMaidenFullName", ApplicantMaidenFullName.Text);
                ht.Add("@FlagindicatingFatherorSpouseName", FlagindicatingFatherorSpouseName.SelectedValue);
                ht.Add("@ApplicantFatherSpouseNamePrefix", ApplicantFatherSpouseNamePrefix.SelectedValue);
                ht.Add("@FatherSpouseFirstName", FatherSpouseFirstName.Text);
                ht.Add("@FatherSpouseMiddleName", FatherSpouseMiddleName.Text);
                ht.Add("@FatherSpouseLastName", FatherSpouseLastName.Text);
                ht.Add("@FatherSpouseFullName", FatherSpouseFullName.Text);
                ht.Add("@ApplicantMotherNamePrefix", ApplicantMotherNamePrefix.SelectedValue);
                ht.Add("@MothersFirstName", MothersFirstName.Text);
                ht.Add("@MothersMiddleName", MothersMiddleName.Text);
                ht.Add("@MothersLastName", MothersLastName.Text);
                ht.Add("@MothersFullName", MothersFullName.Text);
                ht.Add("@Gender", Gender.SelectedValue);
                ht.Add("@Maritalstatus", ""); //Change By tushar 
                ht.Add("@Nationality", ""); //Change By tushar 
                ht.Add("@OccupationType", ""); //Change By tushar 
                ht.Add("@DateofBirthDateofIncorporation", DateofBirthDateofIncorporation.Text);
                ht.Add("@PlaceofIncorporation", "");
                ht.Add("@DateofCommencementofbusiness", "");
                ht.Add("@CountryofIncorporation", "");
                ht.Add("@CountryofresidenceasperTaxLaws", "");
                ht.Add("@IdentificationType", "");
                ht.Add("@TINGSTRegistrationnumber", "");
                ht.Add("@TINIssuingCountry", "");
                ht.Add("@PANorform60", PANorform60.Text);
                ht.Add("@ResidentialStatus", ""); //Change By tushar 
                ht.Add("@FlagindicatingifapplicantresidentfortaxpurposesinJurisdictionoutsideIndia", "");
                ht.Add("@Jurisdictionofresidence", "");
                ht.Add("@TINGSTRegistrationnumber1", "");
                ht.Add("@CountryofBirth", "");
                ht.Add("@CityPlaceofBirth", "");
                ht.Add("@CurrentPermanentOverseasAddressType", ""); //Change By tushar 
                ht.Add("@ProofofIdentityandAddressLine1", ProofofIdentityandAddressLine1.Text);
                ht.Add("@ProofofIdentityandAddressLine2", ProofofIdentityandAddressLine2.Text);
                ht.Add("@ProofofIdentityandAddressLine3", ProofofIdentityandAddressLine3.Text);
                ht.Add("@ProofofIdentityandAddressCityTownVillage", ProofofIdentityandAddressCityTownVillage.Text);
                ht.Add("@ProofofIdentityandAddressDistrict", ProofofIdentityandAddressDistrict.Text);
                ht.Add("@ProofofIdentityandAddressCountry", ProofofIdentityandAddressCountry.SelectedValue);

                if (ProofofIdentityandAddressCountry.SelectedValue == "IN")
                {
                    ht.Add("@ProofofIdentityandAddressStateUT", ProofofIdentityandAddressStateUT.SelectedValue);
                }
                else
                {
                    ht.Add("@ProofofIdentityandAddressStateUT", TxtProofofIdentityandAddressStateUT.Text);
                }

                ht.Add("@ProofofIdentityandAddressPINCode", ProofofIdentityandAddressPINCode.Text);
                ht.Add("@ProofofAddresssubmittedforProofofIdentityandAddress", ProofofAddresssubmittedforProofofIdentityandAddress.SelectedValue);
                ht.Add("@POI_POA_NO", txtPOAIdentNo.Text);
                ht.Add("@POI_POA_ExpDate", txtPOAIdentExpDate.Text);

                ht.Add("@ProofofAddresssubmittedforProofofIdentityandAddressOthers", ProofofAddresssubmittedforProofofIdentityandAddressOthers.Text);
                ht.Add("@POA_NO", txtPOACurrent.Text);
                ht.Add("@POA_ExpDate", txtPOACurrentExpDate.Text);

                ht.Add("@FlagindicatingifProofofIdentityandAddressissameascurrentAddress", FlagindicatingifProofofIdentityandAddressissameascurrentAddress.Checked ? "Y" : "N");
                ht.Add("@CorrespondenceLocalAddressType", "");
                ht.Add("@CurrentAddressLine1", CurrentAddressLine1.Text);
                ht.Add("@CurrentAddressLine2", CurrentAddressLine2.Text);
                ht.Add("@CurrentAddressLine3", CurrentAddressLine3.Text);
                ht.Add("@CurrentAddressCityTownVillage", CurrentAddressCityTownVillage.Text);
                ht.Add("@CurrentAddressDistrict", CurrentAddressDistrict.Text);
                ht.Add("@CurrentAddressCountry", CurrentAddressCountry.SelectedValue);

                if (CurrentAddressCountry.SelectedValue == "IN")
                {
                    ht.Add("@CurrentAddressState", CurrentAddressState.SelectedValue);
                }
                else
                {
                    ht.Add("@CurrentAddressState", TxtCurrentAddressState.Text);
                }

                ht.Add("@CurrentAddressPINCode", CurrentAddressPINCode.Text);
                ht.Add("@ProofofaddresssubmittedforCurrentAddress", ProofofaddresssubmittedforCurrentAddress.SelectedValue);
                ht.Add("@Flagindicatingifaddressinjurisdiction", Flagindicatingifaddressinjurisdiction.Checked ? "Y" : "N");
                ht.Add("@AddressinJurisdictionType", "");
                ht.Add("@AddressinJurisdictionLine1", ""); //Change By tushar 
                ht.Add("@AddressinJurisdictionLine2", ""); //Change By tushar 
                ht.Add("@AddressinJurisdictionLine3", ""); //Change By tushar 
                ht.Add("@AddressinJurisdictionCityTownVillage", "");
                ht.Add("@AddressinJurisdictionCountry", AddressinJurisdictionCountry.SelectedValue);

                if (AddressinJurisdictionCountry.SelectedValue == "IN")
                {
                    ht.Add("@AddressinJurisdictionState", AddressinJurisdictionState.SelectedValue);
                }
                else
                {
                    ht.Add("@AddressinJurisdictionState", TxtAddressinJurisdictionState.Text);
                }

                ht.Add("@AddressinJurisdictionZIPPostCode", AddressinJurisdictionZIPPostCode.Text);
                ht.Add("@ProofofAddresssubmittedforAddressinJurisdiction", "");
                ht.Add("@ResidenceTelephoneNoSTDCode", ResidenceTelephoneNoSTDCode.Text);
                ht.Add("@ResidenceTelephoneNo", ResidenceTelephoneNo.Text);
                ht.Add("@OfficeTelephoneNoSTDCode", OfficeTelephoneNoSTDCode.Text);
                ht.Add("@OfficeTelephoneNo", OfficeTelephoneNo.Text);
                ht.Add("@MobileNoISDCode", MobileNoISDCode.Text);
                ht.Add("@MobileNo", MobileNo.Text);
                ht.Add("@FaxNoSTDCode", "");
                ht.Add("@FaxNo", "");
                ht.Add("@EmailID", EmailID.Text);
                ht.Add("@Remarksifany", Remarksifany.Text);
                ht.Add("@DateofDeclaration", DateofDeclaration.Text);
                ht.Add("@PlaceofDeclaration", PlaceofDeclaration.Text);
                ht.Add("@KYCVerificationDate", KYCVerificationDate.Text);
                ht.Add("@TypeofDocumentSubmitted", TypeofDocumentSubmitted.SelectedValue);
                ht.Add("@KYCVerificationName", KYCVerificationName.Text);
                ht.Add("@KYCVerificationDesignation", KYCVerificationDesignation.Text);
                ht.Add("@KYCVerificationBranch", KYCVerificationBranch.Text);
                ht.Add("@KYCVerificationEMPcode", KYCVerificationEMPcode.Text);
                ht.Add("@OrganisationName", OrganisationName.Text);
                ht.Add("@OrganisationCode", OrganisationCode.Text);
                ht.Add("@NumberofIdentityDetails", "");
                ht.Add("@Numberofrelatedpeople", "");
                ht.Add("@IdentityVerificationFlag", "");
                ht.Add("@NumberofLocalAddressDetails", "");
                ht.Add("@NumberofImages", "");
                ht.Add("@ErrorCode", "");
                ht.Add("@MobileNo2ISDCode", "");
                ht.Add("@MobileNo2", "");
                ht.Add("@EmailID2", "");

                ht.Add("@POIType", ddlPOI.SelectedValue);
                ht.Add("@POINo", txtPOINo.Text);
                ht.Add("@POIExpDate", txtPOIExpDate.Text);



                ht.Add("@Mode", "Web");
                //ht.Add("@status", "");
                ht.Add("@Createdby", Convert.ToString(Session["UserId"]));
                //ht.Add("@BatchID", "");
                ht.Add("@UserID", "");
                //ht.Add("@ProcessStatus", "");
                DataAccessLayer objDal = new DataAccessLayer();
                objDal.ExecuteNonQuery("Prc_InsCKYCdata", ht, "CKYCConnectionString");
            }
            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    ErrorLog objErr = new ErrorLog();
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "Fillcountrycd1", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "CKYC");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }

        protected void ProofofIdentityandAddressCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ProofofIdentityandAddressCountry.SelectedIndex == 0)
                {
                    ProofofIdentityandAddressStateUT.Items.Clear();
                    ProofofIdentityandAddressStateUT.Items.Insert(0, new ListItem("Select", ""));
                    DivDdlProofofIdentityandAddressStateUT.Visible = true;
                    DivTxtProofofIdentityandAddressStateUT.Visible = false;
                    ProofofIdentityandAddressDistrict.Attributes.Add("readonly", "readonly");
                    ProofofIdentityandAddressPINCode.Attributes.Add("readonly", "readonly");
                    return;
                }

                if (ProofofIdentityandAddressCountry.SelectedValue == "IN")
                {
                    cc.FillState(ProofofIdentityandAddressStateUT);
                    DivDdlProofofIdentityandAddressStateUT.Visible = true;
                    DivTxtProofofIdentityandAddressStateUT.Visible = false;
                    ProofofIdentityandAddressDistrict.Attributes.Add("readonly", "readonly");
                    ProofofIdentityandAddressPINCode.Attributes.Add("readonly", "readonly");
                    return;
                }

                DivDdlProofofIdentityandAddressStateUT.Visible = false;
                DivTxtProofofIdentityandAddressStateUT.Visible = true;
                ProofofIdentityandAddressDistrict.Attributes.Remove("readonly");
                ProofofIdentityandAddressPINCode.Attributes.Remove("readonly");
                TxtProofofIdentityandAddressStateUT.Text = "";
            }
            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    ErrorLog objErr = new ErrorLog();
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "Fillcountrycd1", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "CKYC");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }

        protected void CurrentAddressCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (CurrentAddressCountry.SelectedIndex == 0)
                {
                    CurrentAddressState.Items.Clear();
                    CurrentAddressState.Items.Insert(0, new ListItem("Select", ""));
                    DivDdlCurrentAddressState.Visible = true;
                    DivTxtCurrentAddressState.Visible = false;
                    CurrentAddressDistrict.Attributes.Add("readonly", "readonly");
                    CurrentAddressPINCode.Attributes.Add("readonly", "readonly");
                    return;
                }

                if (CurrentAddressCountry.SelectedValue == "IN")
                {
                    cc.FillState(CurrentAddressState);
                    DivDdlCurrentAddressState.Visible = true;
                    DivTxtCurrentAddressState.Visible = false;
                    CurrentAddressDistrict.Attributes.Add("readonly", "readonly");
                    CurrentAddressPINCode.Attributes.Add("readonly", "readonly");
                    return;
                }

                DivDdlCurrentAddressState.Visible = false;
                DivTxtCurrentAddressState.Visible = true;
                CurrentAddressDistrict.Attributes.Remove("readonly");
                CurrentAddressPINCode.Attributes.Remove("readonly");

                TxtCurrentAddressState.Text = "";
            }
            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    ErrorLog objErr = new ErrorLog();
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "Fillcountrycd1", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "CKYC");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }

        protected void AddressinJurisdictionCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (AddressinJurisdictionCountry.SelectedIndex == 0)
                {
                    AddressinJurisdictionState.Items.Clear();
                    AddressinJurisdictionState.Items.Insert(0, new ListItem("Select", ""));
                    DivDdlAddressinJurisdictionState.Visible = true;
                    DivTxtAddressinJurisdictionState.Visible = false;
                    AddressinJurisdictionZIPPostCode.Attributes.Add("readonly", "readonly");
                    return;
                }

                if (AddressinJurisdictionCountry.SelectedValue == "IN")
                {
                    cc.FillState(AddressinJurisdictionState);
                    DivDdlAddressinJurisdictionState.Visible = true;
                    DivTxtAddressinJurisdictionState.Visible = false;
                    AddressinJurisdictionZIPPostCode.Attributes.Add("readonly", "readonly");
                    return;
                }

                DivDdlAddressinJurisdictionState.Visible = false;
                DivTxtAddressinJurisdictionState.Visible = true;
                AddressinJurisdictionZIPPostCode.Attributes.Remove("readonly");
                TxtProofofIdentityandAddressStateUT.Text = "";
            }
            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    ErrorLog objErr = new ErrorLog();
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "Fillcountrycd1", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "CKYC");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }

        protected void ProofofaddresssubmittedforCurrentAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtPOACurrent.Text = "";
                txtPOACurrentExpDate.Text = "";
                if (ProofofaddresssubmittedforCurrentAddress.SelectedIndex == 0)
                {
                    DivPOACurrent.Style.Add("display", "none");
                    DivPOACurrentExpDate.Style.Add("display", "none");
                    return;
                }

                DivPOACurrent.Style.Add("display", "block");
                DivPOACurrentExpDate.Style.Add("display", "block");

                if (ProofofaddresssubmittedforCurrentAddress.SelectedValue == "01" || ProofofaddresssubmittedforCurrentAddress.SelectedValue == "10")
                {
                    lblPOACurrent.Text = "Aadhaar Card";
                }
                else if (ProofofaddresssubmittedforCurrentAddress.SelectedValue == "02")
                {
                    lblPOACurrent.Text = "Passport";
                }
                else if (ProofofaddresssubmittedforCurrentAddress.SelectedValue == "03")
                {
                    lblPOACurrent.Text = "Driving License";
                }
                else if (ProofofaddresssubmittedforCurrentAddress.SelectedValue == "04")
                {
                    lblPOACurrent.Text = "Voters ID";
                }
                else if (ProofofaddresssubmittedforCurrentAddress.SelectedValue == "05")
                {
                    lblPOACurrent.Text = "v";
                }
                else if (ProofofaddresssubmittedforCurrentAddress.SelectedValue == "08")
                {
                    lblPOACurrent.Text = "National Population Register Letter";
                }
                else if (ProofofaddresssubmittedforCurrentAddress.SelectedValue == "09")
                {
                    lblPOACurrent.Text = " E - KYC Authentication";
                }
            }
            catch (Exception ex)
            {
                ErrorLog objErr = new ErrorLog();
                objErr.LogErr(AppID, "CkycReg.aspx.cs", "Fillcountrycd1", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "CKYC");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
            }
        }

        protected void ddlPOI_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtPOINo.Text = "";
                txtPOIExpDate.Text = "";
                if (ddlPOI.SelectedIndex == 0)
                {
                    DivPOINo.Style.Add("display", "none");
                    DivPOIExpDate.Style.Add("display", "none");
                    return;
                }

                DivPOINo.Style.Add("display", "block");
                DivPOIExpDate.Style.Add("display", "block");

                if (ddlPOI.SelectedValue == "A") //Passport
                {
                    lblPOINo.Text = "Passport No";
                }
                else if (ddlPOI.SelectedValue == "B") //VoterID
                {
                    lblPOINo.Text = "Voter ID";
                }
                if (ddlPOI.SelectedValue == "D") //Driving License
                {
                    lblPOINo.Text = "Driving Licence";
                }
                else if (ddlPOI.SelectedValue == "E" || ddlPOI.SelectedValue == "I") //Proof of Possession of Aadhaar
                {
                    lblPOINo.Text = "Aadhaar Card";
                }
                if (ddlPOI.SelectedValue == "F") //NREGA Job Card
                {
                    lblPOINo.Text = "NREGA Job Card";
                }
                else if (ddlPOI.SelectedValue == "G") //National Population Register Letter
                {
                    lblPOINo.Text = "National Population Register Letter";
                }
                if (ddlPOI.SelectedValue == "H") //E-KYC Authentication
                {
                    lblPOINo.Text = "E-KYC Authentication";
                }
            }
            catch (Exception ex)
            {
                ErrorLog objErr = new ErrorLog();
                objErr.LogErr(AppID, "CkycReg.aspx.cs", "Fillcountrycd1", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "CKYC");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
            }
        }

        protected void ProofofAddresssubmittedforProofofIdentityandAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPOAIdentNo.Text = "";
            txtPOAIdentExpDate.Text = "";
            if (ProofofAddresssubmittedforProofofIdentityandAddress.SelectedIndex == 0)
            {
                DivPOAIdentNo.Style.Add("display", "none");
                DivPOAIdentExpDate.Style.Add("display", "none");
                return;
            }

            DivPOAIdentNo.Style.Add("display", "block");
            DivPOAIdentExpDate.Style.Add("display", "block");

            if (ProofofAddresssubmittedforProofofIdentityandAddress.SelectedValue == "01" || ProofofAddresssubmittedforProofofIdentityandAddress.SelectedValue == "10")
            {
                lblPOAIdentNo.Text = "Aadhaar Card";
            }
            else if (ProofofAddresssubmittedforProofofIdentityandAddress.SelectedValue == "02")
            {
                lblPOAIdentNo.Text = "Passport";
            }
            else if (ProofofAddresssubmittedforProofofIdentityandAddress.SelectedValue == "03")
            {
                lblPOAIdentNo.Text = "Driving License";
            }
            else if (ProofofAddresssubmittedforProofofIdentityandAddress.SelectedValue == "04")
            {
                lblPOAIdentNo.Text = "Voters ID";
            }
            else if (ProofofAddresssubmittedforProofofIdentityandAddress.SelectedValue == "05")
            {
                lblPOAIdentNo.Text = "NREGA Job Card";
            }
            else if (ProofofAddresssubmittedforProofofIdentityandAddress.SelectedValue == "08")
            {
                lblPOAIdentNo.Text = "National Population Register Letter";
            }
            else if (ProofofAddresssubmittedforProofofIdentityandAddress.SelectedValue == "09")
            {
                lblPOAIdentNo.Text = " E - KYC Authentication";
            }
        }

        protected void FlagindicatingifProofofIdentityandAddressissameascurrentAddress_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (FlagindicatingifProofofIdentityandAddressissameascurrentAddress.Checked)
                {
                    CurrentAddressLine1.Text = ProofofIdentityandAddressLine1.Text;
                    CurrentAddressLine2.Text = ProofofIdentityandAddressLine2.Text;
                    CurrentAddressLine3.Text = ProofofIdentityandAddressLine3.Text;
                    CurrentAddressCityTownVillage.Text = ProofofIdentityandAddressCityTownVillage.Text;
                    CurrentAddressCountry.SelectedValue = ProofofIdentityandAddressCountry.SelectedValue;
                    CurrentAddressCountry_SelectedIndexChanged(null, EventArgs.Empty);
                    if (CurrentAddressCountry.SelectedValue == "IN")
                    {
                        CurrentAddressState.SelectedValue = ProofofIdentityandAddressStateUT.SelectedValue;
                    }
                    else
                    {
                        TxtCurrentAddressState.Text = ProofofIdentityandAddressStateUT.SelectedValue;
                    }

                    CurrentAddressDistrict.Text = ProofofIdentityandAddressDistrict.Text;
                    CurrentAddressPINCode.Text = ProofofIdentityandAddressPINCode.Text;
                    ProofofaddresssubmittedforCurrentAddress.SelectedValue = ProofofAddresssubmittedforProofofIdentityandAddress.SelectedValue;
                    ProofofaddresssubmittedforCurrentAddress_SelectedIndexChanged(null, EventArgs.Empty);
                    txtPOACurrent.Text = txtPOAIdentNo.Text;
                    txtPOACurrentExpDate.Text = txtPOAIdentExpDate.Text;
                }
                else
                {
                    CurrentAddressLine1.Text = "";
                    CurrentAddressLine2.Text = "";
                    CurrentAddressLine3.Text = "";
                    CurrentAddressCityTownVillage.Text = "";
                    CurrentAddressCountry.SelectedIndex = 0;
                    CurrentAddressCountry_SelectedIndexChanged(null, EventArgs.Empty);
                    CurrentAddressState.SelectedIndex = 0;
                    TxtCurrentAddressState.Text = "";
                    CurrentAddressDistrict.Text = "";
                    CurrentAddressPINCode.Text = "";
                    ProofofaddresssubmittedforCurrentAddress.SelectedIndex = 0;
                    ProofofaddresssubmittedforCurrentAddress_SelectedIndexChanged(null, EventArgs.Empty);
                    txtPOACurrent.Text = "";
                    txtPOACurrentExpDate.Text = "";
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}