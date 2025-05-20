using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMI.FRMWRK.Web.Application.CKYC.Common
{
    public class CkycValidtion
    {
        /// <summary>
        /// Validation for Registration 
        /// </summary>
        /// <param name="chkNormal"></param>
        /// <param name="chkSimplified"></param>
        /// <param name="Chksmall"></param>
        /// <param name="cboTitle"></param>
        /// <param name="txtGivenName"></param>
        /// <param name="txtLastName"></param>
        /// <param name="rbtFS"></param>
        /// <param name="cboTitle2"></param>
        /// <param name="txtGivenName2"></param>
        /// <param name="txtLastName2"></param>
        /// <param name="cboTitle3"></param>
        /// <param name="txtGivenName3"></param>
        /// <param name="txtLastName3"></param>
        /// <param name="txtDOB"></param>
        /// <param name="cboGender"></param>
        /// <param name="ddlMaritalStatus"></param>
        /// <param name="ddlCitizenship"></param>
        /// <param name="ddlResStatus"></param>
        /// <param name="ddlOccupation"></param>
        /// <param name="ddlOccuSubType"></param>
        /// <param name="chkTick"></param>
        /// <param name="ddlIsoCountryCode2"></param>
        /// <param name="txtIDResTax"></param>
        /// <param name="txtDOBRes"></param>
        /// <param name="ddlIsoCountryRes"></param>
        /// <param name="ddlProofIdentity"></param>
        /// <param name="txtPassNo"></param>
        /// <param name="txtPassExpDate"></param>
        /// <param name="chkPerAddress"></param>
        /// <param name="ddlAddressType"></param>
        /// <param name="ddlProofOfAddress"></param>
        /// <param name="txtAddressLine1"></param>
        /// <param name="txtCity"></param>
        /// <param name="ddlPinCode"></param>
        /// <param name="chkLocalAddress"></param>
        /// <param name="txtLocAddLine1"></param>
        /// <param name="txtCity1"></param>
        /// <param name="ddlPinCode2"></param>
        /// <param name="chkAddResident"></param>
        /// <param name="txtAddLine1"></param>
        /// <param name="txtCity2"></param>
        /// <param name="ddlIsoCountryCode"></param>
        /// <param name="chkAppDeclare1"></param>
        /// <returns></returns>
        /// 

        #region RegValidation
        public string Validation(CheckBox chkNormal, CheckBox chkSimplified, CheckBox Chksmall, DropDownList cboTitle, TextBox txtGivenName, TextBox txtLastName, RadioButtonList rbtFS, DropDownList cboTitle2, TextBox txtGivenName2, TextBox txtLastName2,
            DropDownList cboTitle3, TextBox txtGivenName3, TextBox txtLastName3, TextBox txtDOB, DropDownList cboGender,
            CheckBox chkTick, DropDownList ddlIsoCountryCode2, TextBox txtIDResTax, TextBox txtDOBRes, DropDownList ddlIsoCountryRes,
            DropDownList ddlProofIdentity, TextBox txtPassNo, TextBox txtPassExpDate, CheckBox chkPerAddress, DropDownList ddlProofOfAddress,
            TextBox txtAddressLine1, TextBox txtCity, DropDownList ddlPinCode, CheckBox chkLocalAddress, TextBox txtLocAddLine1, TextBox txtCity1, DropDownList ddlPinCode2,
            CheckBox chkAppDeclare1, TextBox txtDate, TextBox txtPlace,
            TextBox txtDateKYCver, TextBox txtEmpName, TextBox txtEmpCode, TextBox txtEmpDesignation, TextBox txtEmpBranch, TextBox txtInsName, TextBox txtInsCode, DropDownList ddlIsoCountryCodeOthr,
            DropDownList ddlIsoCountry, TextBox txtPassOthr, TextBox PanNo, DropDownList ddlDocReceived = null)
        {
            string msg = string.Empty;
            string date;
            date = DateTime.Today.ToString("dd\\/MM\\/yyyy");

            #region try
            try
            {
                  

                if (chkNormal.Checked == false && chkSimplified.Checked == false && Chksmall.Checked == false)
                {
                    msg = "Please select account type";
                    return msg;
                }

                if (cboTitle.SelectedIndex == 0)
                {
                    msg = "Please select prefix of name";
                    return msg;


                }
                if (txtGivenName.Text == "")
                {
                    msg = "Please enter first name";
                    return msg;
                }
                if (txtLastName.Text == "")
                {
                    msg = "Please enter last name";
                    return msg;
                }
                if (rbtFS.SelectedIndex == -1)
                {
                    msg = "Please select father/spouse";
                    return msg;
                }
                if (cboTitle2.SelectedIndex == 0)
                {
                    msg = "Please select prefix of father/spouse Name";
                    return msg;
                }
                if (rbtFS.SelectedValue == "F")
                {
                    if (cboTitle2.SelectedValue == "MRS" || cboTitle2.SelectedValue == "MS")
                    {
                        msg = "Please select valid prefix for father/spouse name";
                        return msg;
                    }
                }
                else if (rbtFS.SelectedValue == "S")

                    if (cboTitle2.SelectedValue == "MR")
                    {
                        msg = "Please select valid prefix for father/spouse name";
                        return msg;
                    }
                if (txtGivenName2.Text == "")
                {
                    msg = "Please enter first name of father/spouse";
                    return msg;
                }
                if (txtLastName2.Text == "")
                {
                    msg = "Please enter last name of father/spouse";
                    return msg;
                }
                if (cboTitle3.SelectedIndex == 0)
                {
                    msg = "Please select prefix of mother name";
                    return msg;
                }
                if (txtGivenName3.Text == "")
                {
                    msg = "Please enter first name of mother";
                    return msg;
                }

                if (txtLastName3.Text == "")
                {
                    msg = "Please enter last name of mother";
                    return msg;
                }

                if (txtDOB.Text == "")
                {
                    msg = "Please enter date of birth";
                    return msg;
                }
                if (txtDOB.Text != "")
                {
                    string pattern = "^(0[1-9]|[12][0-9]|3[01])[/](0[1-9]|1[012])[/](19|20)[0-9]{2}$";
                    Match match = Regex.Match(txtDOB.Text.ToString(), pattern);
                    if (!match.Success)
                    {
                        msg = "Check DOB date format it must be in dd/mm/yyyy";
                        return msg;
                    }
                    DateTime date1, date2;
                    date1 = DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    date2 = DateTime.ParseExact(txtDOB.Text.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    if (date1 < date2)
                    {
                        msg = "You cannot select future date";
                        return msg;
                    }
                }
                if (cboGender.SelectedIndex == 0)
                {
                    msg = "Please select gender";
                    return msg;
                }

                //Commented by Pratik Ckyc Ver 1.2 Start
                //if (ddlOccupation.SelectedIndex == 0)
                //{
                //    msg = "Please select occupation type";
                //    return msg;
                //}
 

                //Commented by Pratik Ckyc Ver 1.2 Start
                //if (ddlMaritalStatus.SelectedIndex == 0)
                //{
                //    msg = "Please select marital status";
                //    return msg;
                //}


                if (cboTitle.SelectedValue == "MRS" && rbtFS.SelectedValue == "F" )
                {
                    msg = "Please select correct father,prefix and marital status";
                    return msg;
                }

                if (cboTitle.SelectedValue == "MRS" )
                {
                    msg = "Invalid Title";
                    return msg;
                }

                //Commented by Pratik Ckyc Ver 1.2 Start

                //if (ddlCitizenship.SelectedIndex == 0)
                //{
                //    msg = "Please select Nationality";
                //    return msg;
                //}
                //if (ddlResStatus.SelectedIndex == 0)
                //{
                //    msg = "Please select residential status";
                //    return msg;
                //}

                
                if (PanNo.Text != "")
                {
                    //string pattern = "^[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}$";
                    //Match match = Regex.Match(PanNo.Text.ToString(), pattern);
                    //if (!match.Success)
                    //{
                    //    msg = "Invalid Pan Number";
                    //    return msg;
                    //}
                }
                else
                {
                    msg = "Please enter pan no";
                    return msg;
                }

                //Commented by Pratik Ckyc Ver 1.2 Start

                //if (chkTick.Checked == true)
                //{
                //    if (ddlIsoCountryCode2.SelectedIndex == 0)
                //    {
                //        msg = "Please select ISO 3166 country code of jurisdiction of residence";
                //        return msg;
                //    }
                //    if (txtIDResTax.Text == "")
                //    {
                //        msg = "Please enter tax identification number or equivalent(if issued by jurisdiction)";
                //        return msg;
                //    }
                //    if (txtDOBRes.Text == "")
                //    {
                //        msg = "Please enter place/city of birth of jurisdiction of residence";
                //        return msg;
                //    }
                //    if (ddlIsoCountry.SelectedIndex == 0)
                //    {
                //        msg = "Please select ISO 3166 country code of birth";
                //        return msg;
                //    }
                //}

                if (ddlProofIdentity.SelectedIndex == 0)
                {
                    msg = "Please select proof of identity";
                    return msg;
                }

                if (ddlProofIdentity.SelectedIndex != 0)
                {
                    if (ddlProofIdentity.SelectedIndex == 1)
                    {
                        if (txtPassNo.Text == "")
                        {
                            msg = "Please enter passport number";
                            return msg;
                        }
                        //if (txtPassExpDate.Text == "")
                        //{
                        //    msg = "Please enter passport expiry date";
                        //    return msg;
                        //}
                        if (txtPassExpDate.Text != "")
                        {
                            string pattern = "^(0[1-9]|[12][0-9]|3[01])[/](0[1-9]|1[012])[/](19|20)[0-9]{2}$";
                            Match match = Regex.Match(txtPassExpDate.Text.ToString(), pattern);
                            if (!match.Success)
                            {
                                msg = "Check driving license date format it must be in dd/mm/yyyy";
                                return msg;
                            }
                            DateTime date1, date2;
                            date1 = DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            date2 = DateTime.ParseExact(txtPassExpDate.Text.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            if (date1 > date2)
                            {
                                msg = "You cannot select past date as driving license expiry date";
                                return msg;
                            }

                        }
                    }

                    if (ddlProofIdentity.SelectedIndex == 2)
                    {
                        if (txtPassNo.Text == "")
                        {
                            msg = "Please enter voter id card";
                            return msg;
                        }

                    }
                    if (ddlProofIdentity.SelectedIndex == 3)
                    {
                        if (txtPassNo.Text == "")
                        {
                            msg = "Please enter pan card number";
                            return msg;
                        }

                    }
                    if (ddlProofIdentity.SelectedIndex == 4)
                    {
                        if (txtPassNo.Text == "")
                        {
                            msg = "Please enter driving licence number";
                            return msg;

                        }
                        //if (txtPassExpDate.Text == "")
                        //{
                        //    msg = "Please enter driving licence expiry date";
                        //    return msg;
                        //}
                        if (txtPassExpDate.Text != "")
                        {
                            string pattern = "^(0[1-9]|[12][0-9]|3[01])[/](0[1-9]|1[012])[/](19|20)[0-9]{2}$";
                            Match match = Regex.Match(txtPassExpDate.Text.ToString(), pattern);
                            if (!match.Success)
                            {
                                msg = "Check driving license date format it must be in dd/mm/yyyy";
                                return msg;
                            }
                            DateTime date1, date2;
                            date1 = DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            date2 = DateTime.ParseExact(txtPassExpDate.Text.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            if (date1 > date2)
                            {
                                msg = "You cannot select past date as driving license expiry date";
                                return msg;
                            }

                        }

                    }

                    if (ddlProofIdentity.SelectedIndex == 5)
                    {
                        if (txtPassNo.Text == "")
                        {
                            msg = "Please enter UID(aadhaar)";
                            return msg;
                        }

                    }
                    if (ddlProofIdentity.SelectedIndex == 6)
                    {
                        if (txtPassNo.Text == "")
                        {
                            msg = "Please enter NREGA job card";
                            return msg;
                        }

                    }
                    if (ddlProofIdentity.SelectedIndex == 7)
                    {
                        if (txtPassNo.Text == "")
                        {
                            //msg = "Please enter other no of proof of identity";
                            msg = "Please enter document name";
                            return msg;
                        }
                        if (txtPassOthr.Text == "")
                        {
                            msg = "Please enter other identity number";
                            return msg;
                        }

                    }
                    if (ddlProofIdentity.SelectedIndex == 8)
                    {
                        if (txtPassNo.Text == "")
                        {
                            msg = "Please enter simplified measures account";
                            return msg;
                        }
                    }

                }
                
                //Commented by Pratik Ckyc Ver 1.2 Start
                
                //if (ddlAddressType.SelectedIndex == 0)
                //{
                //    msg = "Please select address type";
                //    return msg;
                //}
                if (ddlProofOfAddress.SelectedIndex == 0)
                {
                    msg = "Please select proof of address";
                    return msg;
                }
                //if (txtAddressLine1.Text == "")
                //{
                //    msg = "Please enter permanent address line 1";
                //    return msg;
                //}
                //if (txtCity.Text == "")
                //{
                //    msg = "Please enter permanent city/town/village";
                //    return msg;
                //}
                //if (ddlPinCode.SelectedIndex == 0 && chkTick.Checked == false)
                //{
                //    msg = "Please enter permanent pin/post code";
                //    return msg;
                //}

                //Address for the Local/Correspondence address details start
                if (chkLocalAddress.Checked == true)
                {
                    //if (txtLocAddLine1.Text == "")
                    //{
                    //    msg = "Please enter local address line 1";
                    //    return msg;
                    //}
                    //if (txtCity1.Text == "")
                    //{
                    //    msg = "Please enter local city/town/village";
                    //    return msg;
                    //}
                    //if (ddlPinCode2.SelectedIndex == 0)
                    //{
                    //    msg = "Please enter local pin/post code";
                    //    return msg;
                    //}
                }

                //if (chkAddResident.Checked == true)// && chkTick.Checked == true
                //{
                //    if (txtAddLine1.Text == "")
                //    {
                //        msg = "Please enter jurisdiction detail address line 1";
                //        return msg;
                //    }
                //    if (txtCity2.Text == "")
                //    {
                //        msg = "Please enter local city/town/village";
                //        return msg;
                //    }
                //}
                
                ////Related person details

                //if (ddlRelType.SelectedIndex == 0)
                //{

                //}
                if (chkAppDeclare1.Checked == false)
                {
                    msg = "Please check application declaration";
                    return msg;
                }
                if (txtDate.Text == "")
                {
                    msg = "Please enter application declaration date";
                    return msg;
                }
                if (txtPlace.Text == "")
                {
                    msg = "Please enter application declaration place";
                    return msg;
                }
                if (txtDateKYCver.Text == "")
                {
                    msg = " Please enter KYC verification carried out date";
                    return msg;
                }
                if (txtEmpName.Text == "")
                {
                    msg = " Please enter KYC verification carried out employee name";
                    return msg;
                }
                if (txtEmpCode.Text == "")
                {
                    msg = "Please enter permanent city/town/village";
                    return msg;
                }
                if (txtEmpDesignation.Text == "")
                {
                    msg = " Please enter KYC verification carried out employee designation";
                    return msg;
                }
                if (txtEmpBranch.Text == "")
                {
                    msg = " Please enter KYC verification carried out employee branch";
                    return msg;
                }
                if (txtInsName.Text == "")
                {
                    msg = " Please enter KYC verification carried out institution name";
                    return msg;
                }
                if (txtInsCode.Text == "")
                {
                    msg = " Please enter KYC verification carried out institution Code";
                    return msg;
                }

                if(ddlDocReceived != null)
                {
                    if(ddlDocReceived.SelectedIndex == 0)
                    {
                        msg = " Please select document received";
                        return msg;
                    }
                }
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.IO.FileInfo oInfo = new System.IO.FileInfo(currentFile);
                string sRet = oInfo.Name;
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                String LogClassName = method.ReflectedType.Name;
                // objErr.LogErr("ISYS-RGIC", sRet, method.Name.ToString(), ex.Message.ToString(), HttpContext.Current.Session["UserID"].ToString().Trim());
            }
            #endregion
            return msg;
        }
        #endregion

        #region RelatedPrsnValidation
        public string RelatedPrsnValidation(DropDownList ddlRelType, DropDownList cboTitle, TextBox txtGivenName, TextBox txtLastName, DropDownList cboTitle2, RadioButtonList rbtFS,
            TextBox txtGivenName2, TextBox txtLastName2, DropDownList cboTitle3, TextBox txtGivenName3, TextBox txtLastName3, TextBox txtDOB, DropDownList cboGender, DropDownList ddlOccupation,
            DropDownList ddlMaritalStatus, DropDownList ddlCitizenship, DropDownList ddlResStatus, DropDownList ddlIsoCountryCodeOthr, CheckBox chkTick, DropDownList ddlIsoCountryCode2,
            TextBox txtIDResTax, TextBox txtDOBRes, DropDownList ddlIsoCountry, DropDownList ddlProofIdentity, TextBox txtPassNo, TextBox txtPassExpDate, DropDownList ddlAddressType,
            CheckBox chkPerAddress, CheckBox chkAppDeclare1, DropDownList ddlProofOfAddress, TextBox txtAddressLine1, TextBox txtCity, DropDownList ddlPinCode, TextBox txtDateKYCver,
            TextBox txtEmpName, TextBox txtEmpCode, TextBox txtEmpDesignation, TextBox txtEmpBranch, TextBox txtInsName, TextBox txtInsCode)
        {
            string msg = string.Empty;
            string date;
            date = DateTime.Today.ToString("dd\\/MM\\/yyyy");
            try
            {
                if (ddlRelType.SelectedIndex == 0)
                {
                    msg = "Please select related person type";
                    return msg;
                }
                if (cboTitle.SelectedIndex == 0)
                {
                    msg = "Please select prefix of name";
                    return msg;
                }
                if (txtGivenName.Text == "")
                {
                    msg = "Please enter first name";
                    return msg;
                }
                if (txtLastName.Text == "")
                {
                    msg = "Please enter last name";
                    return msg;
                }
                if (rbtFS.SelectedIndex == -1)
                {
                    msg = "Please select father/spouse";
                    return msg;
                }
                if (cboTitle2.SelectedIndex == 0)
                {
                    msg = "Please select prefix of father/spouse name";
                    return msg;
                }
                if (rbtFS.SelectedValue == "F")
                {
                    if (rbtFS.SelectedValue == "MRS" || rbtFS.SelectedValue == "MS")
                    {
                        msg = "Invalid prefix of father/spouse name";
                        return msg;
                    }
                }

                if (txtGivenName2.Text == "")
                {
                    msg = "Please enter first name of father/spouse";
                    return msg;
                }
                if (txtLastName2.Text == "")
                {
                    msg = "Please enter last name of father/spouse";
                    return msg;
                }
                if (cboTitle3.SelectedIndex == 0)
                {
                    msg = "Please select prefix of mother name";
                    return msg;
                }
                if (txtGivenName3.Text == "")
                {
                    msg = "Please enter first name of mother";
                    return msg;
                }
                if (txtLastName3.Text == "")
                {
                    msg = "Please enter last name of mother";
                    return msg;
                }
                if (txtDOB.Text == "")
                {
                    msg = "Please enter date of birth";
                    return msg;
                }
                if (txtDOB.Text != "")
                {
                    string pattern = "^(0[1-9]|[12][0-9]|3[01])[/](0[1-9]|1[012])[/](19|20)[0-9]{2}$";
                    Match match = Regex.Match(txtDOB.Text.ToString(), pattern);
                    if (!match.Success)
                    {
                        msg = "Check DOB date format it must be in dd/mm/yyyy";
                        return msg;
                    }
                    DateTime date1, date2;
                    date1 = DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    date2 = DateTime.ParseExact(txtDOB.Text.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    if (date1 < date2)
                    {
                        msg = "You cannot select future date";
                        return msg;
                    }
                }
                if (cboGender.SelectedIndex == 0)
                {
                    msg = "Please select gender";
                    return msg;
                }
                if (ddlOccupation.SelectedIndex == 0)
                {
                    msg = "Please select occupation type";
                    return msg;
                }
                if (ddlMaritalStatus.SelectedIndex == 0)
                {
                    msg = "Please select marital status";
                    return msg;
                }
                if (cboTitle.SelectedValue == "MRS" && ddlMaritalStatus.SelectedValue == "02")
                {
                    msg = "Invalid prefix";
                    return msg;
                }
                //added by ramesh ondated by 25-05-2018
                if (cboTitle.SelectedValue == "MRS" && rbtFS.SelectedValue == "F" && ddlMaritalStatus.SelectedValue == "02")
                {
                    msg = "Please select correct father,prefix and marital status";
                    return msg;
                }

                if (ddlCitizenship.SelectedIndex == 0)
                {
                    msg = "Please select the  Nationality ";
                    return msg;
                }
                if (ddlResStatus.SelectedIndex == 0)
                {
                    msg = "Please select residential status";
                    return msg;
                }
                if (ddlCitizenship.SelectedIndex == 2)
                {
                    if (ddlIsoCountryCodeOthr.SelectedIndex == 0)
                    {
                        msg = "Please select ISO 3166 country code";
                        return msg;
                    }
                }
                if (chkTick.Checked == true)
                {
                    if (ddlIsoCountryCode2.SelectedIndex == 0)
                    {
                        msg = "Please select ISO 3166 country code of jurisdiction of residence";
                        return msg;
                    }
                    if (txtIDResTax.Text == "")
                    {
                        msg = "Please enter tax identification number or equivalent(if issued by jurisdiction)";
                        return msg;
                    }
                    if (txtDOBRes.Text == "")
                    {
                        msg = "Please enter place/city of birth of jurisdiction of residence";
                        return msg;
                    }
                    if (ddlIsoCountry.SelectedIndex == 0)
                    {
                        msg = "Please select ISO 3166 country code of birth";
                        return msg;
                    }
                }
                if (ddlProofIdentity.SelectedIndex == 0)
                {
                    msg = "Please select proof of identity";
                    return msg;
                }
                if (ddlProofIdentity.SelectedIndex != 0)
                {
                    if (ddlProofIdentity.SelectedIndex == 1)
                    {
                        if (txtPassNo.Text == "")
                        {
                            msg = "Please enter passport number";
                            return msg;
                        }
                        if (txtPassExpDate.Text == "")
                        {
                            msg = "Please enter passport expiry date";
                            return msg;
                        }
                        if (txtPassExpDate.Text != "")
                        {
                            string pattern = "^(0[1-9]|[12][0-9]|3[01])[/](0[1-9]|1[012])[/](19|20)[0-9]{2}$";
                            Match match = Regex.Match(txtPassExpDate.Text.ToString(), pattern);
                            if (!match.Success)
                            {
                                msg = "Check driving license date format it must be in dd/mm/yyyy";
                                return msg;
                            }

                            DateTime date1, date2;
                            date1 = DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            date2 = DateTime.ParseExact(txtPassExpDate.Text.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            if (date1 > date2)
                            {
                                msg = "You cannot select past date as driving license expiry date";
                                return msg;
                            }
                        }
                    }
                    if (ddlProofIdentity.SelectedIndex == 2)
                    {
                        if (txtPassNo.Text == "")
                        {
                            msg = "Please enter voter id card";
                            return msg;
                        }
                    }
                    if (ddlProofIdentity.SelectedIndex == 3)
                    {
                        if (txtPassNo.Text == "")
                        {
                            msg = "Please enter pan card";
                            return msg;
                        }

                    }
                    if (ddlProofIdentity.SelectedIndex == 4)
                    {
                        if (txtPassNo.Text == "")
                        {
                            msg = "Please enter driving licence number";
                            return msg;
                        }
                        if (txtPassExpDate.Text == "")
                        {
                            msg = "Please enter driving licence expiry date";
                            return msg;
                        }
                        if (txtPassExpDate.Text != "")
                        {
                            string pattern = "^(0[1-9]|[12][0-9]|3[01])[/](0[1-9]|1[012])[/](19|20)[0-9]{2}$";
                            Match match = Regex.Match(txtPassExpDate.Text.ToString(), pattern);
                            if (!match.Success)
                            {
                                msg = "Check driving license date format it must be in dd/mm/yyyy";
                                return msg;
                            }
                            DateTime date1, date2;
                            date1 = DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            date2 = DateTime.ParseExact(txtPassExpDate.Text.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            if (date1 > date2)
                            {
                                msg = "You cannot select past date as driving license expiry date";
                                return msg;
                            }
                        }
                    }
                    if (ddlProofIdentity.SelectedIndex == 5)
                    {
                        if (txtPassNo.Text == "")
                        {
                            msg = "Please enter UID(aadhaar)";
                            return msg;
                        }
                    }
                    if (ddlProofIdentity.SelectedIndex == 6)
                    {
                        if (txtPassNo.Text == "")
                        {
                            msg = "Please enter NREGA job card";
                            return msg;
                        }
                    }
                    if (ddlProofIdentity.SelectedIndex == 7)
                    {
                        if (txtPassNo.Text == "")
                        {
                            msg = "Please enter document name";
                            return msg;
                        }
                    }
                    if (ddlProofIdentity.SelectedIndex == 8)
                    {
                        if (txtPassNo.Text == "")
                        {
                            msg = "Please enter simplified measures account";
                            return msg;
                        }
                    }
                }
                if (ddlAddressType.SelectedIndex != 0)
                {
                    if (chkPerAddress.Checked == false)
                    {
                        msg = "Please check current/permanent/overseas address details";
                        return msg;
                    }
                }
                if (ddlAddressType.SelectedIndex == 0)
                {
                    msg = "Please select address type";
                    return msg;
                }
                if (ddlProofOfAddress.SelectedIndex == 0)
                {
                    msg = "Please select proof of address";
                    return msg;
                }
                if (txtAddressLine1.Text == "")
                {
                    msg = "Please enter permanent address line 1";
                    return msg;
                }
                if (txtCity.Text == "")
                {
                    msg = "Please enter permanent city/town/village";
                    return msg;
                }
                if (ddlPinCode.SelectedIndex == 0 && chkTick.Checked == false)
                {
                    msg = "Please enter permanent pin/post code";
                    return msg;
                }
                if (chkAppDeclare1.Checked == false)
                {
                    msg = "Please check application declaration";
                    return msg;
                }
                if (txtDateKYCver.Text == "")
                {
                    msg = " Please enter KYC verification carried out Date";
                    return msg;
                }
                if (txtEmpName.Text == "")
                {
                    msg = " Please enter KYC verification carried out employee name";
                    return msg;
                }
                if (txtEmpCode.Text == "")
                {
                    msg = " Please enter KYC verification carried out employee code";
                    return msg;
                }
                if (txtEmpDesignation.Text == "")
                {
                    msg = " Please enter KYC verification carried out employee designation";
                    return msg;
                }
                if (txtEmpBranch.Text == "")
                {
                    msg = " Please enter KYC verification carried out employee branch";
                    return msg;
                }
                if (txtInsName.Text == "")
                {
                    msg = " Please enter KYC verification carried out institution name";
                    return msg;
                }
                if (txtInsCode.Text == "")
                {
                    msg = msg = " Please enter KYC verification carried out institution code";
                    return msg;
                }
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.IO.FileInfo oInfo = new System.IO.FileInfo(currentFile);
                string sRet = oInfo.Name;
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                String LogClassName = method.ReflectedType.Name;
            }
            return msg;
        }
        #endregion

        #region EntityRelatedPrsnValidation
        public string EntityRelatedPrsnValidation(DropDownList ddlRelType, DropDownList cboTitle, TextBox txtGivenName, TextBox txtLastName, DropDownList cboTitle2, RadioButtonList rbtFS,
            TextBox txtGivenName2, TextBox txtLastName2, DropDownList cboTitle3, TextBox txtGivenName3, TextBox txtLastName3, TextBox txtDOB, DropDownList cboGender, DropDownList ddlOccupation,
            DropDownList ddlOccuSubType, DropDownList ddlMaritalStatus, DropDownList ddlCitizenship, DropDownList ddlResStatus, DropDownList ddlIsoCountryCodeOthr, CheckBox chkTick, DropDownList ddlIsoCountryCode2,
            TextBox txtIDResTax, TextBox txtDOBRes, DropDownList ddlIsoCountry, DropDownList ddlProofIdentity, TextBox txtPassNo, TextBox txtPassExpDate, TextBox txtPassOthr, DropDownList ddlAddressType,
            CheckBox chkPerAddress, CheckBox chkAppDeclare1, DropDownList ddlProofOfAddress, TextBox txtAddressLine1, TextBox txtCity, TextBox txtPinCode, TextBox txtDateKYCver, TextBox txtDate,
            TextBox txtEmpName, TextBox txtEmpCode, TextBox txtEmpDesignation, TextBox txtEmpBranch, TextBox txtInsName, TextBox txtInsCode, TextBox txtPassExpDateAdd, TextBox txtPassNoAdd, CheckBox chkAppDeclare2, TextBox txtPlace,
            CheckBox chkSelfCerti, CheckBox chkTrueCopies, CheckBox chkNotary, DropDownList ddlState, CheckBox chkDone, TextBox txtPassOthrAdd
            , CheckBox chkLocalAddress, DropDownList ddlAddressType1, TextBox txtLocAddLine1, TextBox txtCity1, DropDownList ddlState1,
            TextBox ddlPinCode1, DropDownList ddlCountryCode1,
            CheckBox chkAddResident, DropDownList ddlAddressType2, TextBox txtAddLine1, TextBox txtCity2, DropDownList ddlState2, TextBox ddlPinCode2, DropDownList ddlIsoCountryCode)//DropDownList ddlProofOfAddress1,
        {
            string msg = string.Empty;
            string date;
            date = DateTime.Today.ToString("dd\\-MM\\-yyyy");
            try
            {
                #region PErsonal Details Validation
                if (ddlRelType.SelectedIndex == 0)
                {
                    msg = "Please select person type";
                    return msg;
                }

                if (cboTitle.SelectedIndex == 0)
                {
                    msg = "Please select prefix of name";
                    return msg;
                }
                if (txtGivenName.Text == "")
                {
                    msg = "Please enter first name";
                    return msg;
                }
                if (txtLastName.Text == "")
                {
                    msg = "Please enter last name";
                    return msg;
                }
                if (rbtFS.SelectedIndex == -1)
                {
                    msg = "Please select father/spouse";
                    return msg;
                }
                if (cboTitle2.SelectedIndex == 0)
                {
                    msg = "Please select prefix of father/spouse name";
                    return msg;
                }
                if (rbtFS.SelectedValue == "F")
                {
                    if (rbtFS.SelectedValue == "MRS" || rbtFS.SelectedValue == "MS")
                    {
                        msg = "Please select valid prefix of father/spouse name";
                        return msg;
                    }
                }

                if (txtGivenName2.Text == "")
                {
                    msg = "Please enter first name of father/spouse";
                    return msg;
                }
                if (txtLastName2.Text == "")
                {
                    msg = "Please enter last name of father/spouse";
                    return msg;
                }

                // Commented by akash for UAT environment only start//

                //if (cboTitle3.SelectedIndex == 0)
                //{
                //    msg = "Please select prefix of mother name";
                //    return msg;
                //}
                //if (txtGivenName3.Text == "")
                //{
                //    msg = "Please enter first name of mother";
                //    return msg;
                //}
                //if (txtLastName3.Text == "")
                //{
                //    msg = "Please enter last name of mother";
                //    return msg;
                //}

                // Commented by akash for UAT environment only end//
                if (txtDOB.Text == "")
                {
                    msg = "Please enter date of birth";
                    return msg;
                }
                if (txtDOB.Text != "")
                {
                    string pattern = "^(0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-](19|20)[0-9]{2}$";
                    Match match = Regex.Match(txtDOB.Text.ToString(), pattern);
                    if (!match.Success)
                    {
                        msg = "Check DOB date format it must be in dd/mm/yyyy";
                        return msg;
                    }
                    DateTime date1, date2;
                    date1 = DateTime.ParseExact(date.ToString(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    date2 = DateTime.ParseExact(txtDOB.Text.ToString(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    if (date1 < date2)
                    {
                        msg = "You cannot select future date";
                        return msg;
                    }
                }
                if (cboGender.SelectedIndex == 0)
                {
                    msg = "Please select gender";
                    return msg;
                }

                if (cboTitle.SelectedIndex != 0 && cboGender.SelectedIndex != 0)
                {
                    if (cboTitle.SelectedValue == "MR" && cboGender.SelectedValue == "F")
                    {
                        msg = "Please select valid gender according to prefix of the name";
                        return msg;
                    }
                }
                //added by ramesh on dated 25-05-2017

                if (cboTitle.SelectedValue == "MRS" && rbtFS.SelectedValue == "F" && ddlMaritalStatus.SelectedValue == "02")
                {
                    msg = "Please select valid prefix of name and Marital Status";
                    return msg;
                }


                // Commented by akash for UAT environment only start//

                //if (ddlMaritalStatus.SelectedIndex == 0)
                //{
                //    msg = "Please select marital status";
                //    return msg;
                //}

                // Commented by akash for UAT environment only end//

                if (cboTitle.SelectedValue == "MRS" && ddlMaritalStatus.SelectedValue == "02")
                {
                    msg = "Please select valid prefix";
                    return msg;
                }
                if (ddlCitizenship.SelectedIndex == 0)
                {
                    msg = "Please select Nationality";
                    return msg;
                }

                if (ddlCitizenship.SelectedValue == "OCIZ")
                {
                    if (ddlIsoCountryCodeOthr.SelectedIndex == 0)
                    {
                        msg = "Please select ISO 3166 Country Code";
                        return msg;
                    }
                }

                if (ddlResStatus.SelectedIndex == 0)
                {
                    msg = "Please select residential status";
                    return msg;
                }

                if (ddlOccupation.SelectedIndex == 0)
                {
                    msg = "Please select occupation type";
                    return msg;
                }
                if (ddlOccuSubType.SelectedIndex == 0)
                {
                    msg = "Please select occupation sub type";
                    return msg;
                }


                if (ddlCitizenship.SelectedIndex == 2)
                {
                    if (ddlIsoCountryCodeOthr.SelectedIndex == 0)
                    {
                        msg = "Please select ISO 3166 country code";
                        return msg;
                    }
                }
                #endregion Personal Details Valid END

                #region chkTick start
                if (chkTick.Checked == true)
                {
                    if (ddlIsoCountryCode2.SelectedIndex == 0)
                    {
                        msg = "Please select ISO 3166 country code of jurisdiction of residence";
                        return msg;
                    }
                    if (txtIDResTax.Text == "")
                    {
                        msg = "Please enter tax identification number or equivalent (if issued by jurisdiction)";
                        return msg;
                    }
                    if (txtDOBRes.Text == "")
                    {
                        msg = "Please enter place/city of birth of jurisdiction of residence";
                        return msg;
                    }
                    if (ddlIsoCountry.SelectedIndex == 0)
                    {
                        msg = "Please select ISO 3166 country code of birth";
                        return msg;
                    }
                }
                if (ddlProofIdentity.SelectedIndex == 0)
                {
                    msg = "Please select proof of identity";
                    return msg;
                }
                if (ddlProofIdentity.SelectedIndex != 0)
                {
                    if (ddlProofIdentity.SelectedIndex == 1)
                    {
                        if (txtPassNo.Text == "")
                        {
                            msg = "Please enter passport number";
                            return msg;
                        }
                        if (txtPassExpDate.Text == "")
                        {
                            msg = "Please enter passport expiry date";
                            return msg;
                        }
                        if (txtPassExpDate.Text != "")
                        {
                            string pattern = "^(0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-](19|20)[0-9]{2}$";
                            Match match = Regex.Match(txtPassExpDate.Text.ToString(), pattern);
                            if (!match.Success)
                            {
                                msg = "Please check passport expiry date format, it must be in dd/mm/yyyy";
                                return msg;
                            }

                            DateTime date1, date2;
                            date1 = DateTime.ParseExact(date.ToString(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            date2 = DateTime.ParseExact(txtPassExpDate.Text.ToString(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);

                            string strIdentityName = ddlProofIdentity.SelectedItem.Text;
                            if (date1 > date2)
                            {
                                msg = "You cannot select past date as " + strIdentityName.ToLower() + " expiry date";
                                return msg;
                            }
                        }
                    }
                    if (ddlProofIdentity.SelectedIndex == 2)
                    {
                        if (txtPassNo.Text == "")
                        {
                            msg = "Please enter voter ID card";
                            return msg;
                        }
                    }
                    if (ddlProofIdentity.SelectedIndex == 3)
                    {
                        if (txtPassNo.Text == "")
                        {
                            msg = "Please enter PAN number";
                            return msg;
                        }

                    }
                    if (ddlProofIdentity.SelectedIndex == 4)
                    {
                        if (txtPassNo.Text == "")
                        {
                            msg = "Please enter driving licence number";
                            return msg;
                        }
                        if (txtPassExpDate.Text == "")
                        {
                            msg = "Please enter driving licence expiry date";
                            return msg;
                        }
                        if (txtPassExpDate.Text != "")
                        {
                            string pattern = "^(0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-](19|20)[0-9]{2}$";
                            Match match = Regex.Match(txtPassExpDate.Text.ToString(), pattern);
                            if (!match.Success)
                            {
                                msg = "Please check driving license date format, it must be in dd/mm/yyyy";
                                return msg;
                            }
                            DateTime date1, date2;
                            date1 = DateTime.ParseExact(date.ToString(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            date2 = DateTime.ParseExact(txtPassExpDate.Text.ToString(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            if (date1 > date2)
                            {
                                msg = "You cannot select past date as driving license expiry date";
                                return msg;
                            }
                        }
                    }
                    if (ddlProofIdentity.SelectedIndex == 5)
                    {
                        if (txtPassNo.Text == "")
                        {
                            msg = "Please enter UID(aadhaar)";
                            return msg;
                        }
                    }
                    if (ddlProofIdentity.SelectedIndex == 6)
                    {
                        if (txtPassNo.Text == "")
                        {
                            msg = "Please enter NREGA job card";
                            return msg;
                        }
                    }
                    if (ddlProofIdentity.SelectedIndex == 7)
                    {
                        if (txtPassNo.Text == "")
                        {
                            msg = "Please enter document name";
                            return msg;
                        }
                        if (txtPassOthr.Text == "")
                        {
                            msg = "Please enter identification number";
                            return msg;
                        }
                    }
                    if (ddlProofIdentity.SelectedIndex == 8)
                    {
                        if (txtPassNo.Text == "")
                        {
                            msg = "Please enter simplified measures account";
                            return msg;
                        }
                        if (txtPassOthr.Text == "")
                        {
                            msg = "Please enter identification number";
                            return msg;
                        }
                    }
                    if (ddlProofIdentity.SelectedIndex == 9)
                    {
                        if (txtPassNo.Text == "")
                        {
                            msg = "Please enter simplified measures account";
                            return msg;
                        }
                        if (txtPassOthr.Text == "")
                        {
                            msg = "Please enter identification number";
                            return msg;
                        }
                    }
                }
                //if (ddlAddressType.SelectedIndex != 0)
                //{
                if (chkPerAddress.Checked == false)
                {
                    msg = "Please check current/permanent/overseas address details";
                    return msg;
                }
                //if (txtPassOthr.Text == "")
                //{
                //    msg = "Please enter identification number";
                //    return msg;
                //}
                //}
                if (ddlAddressType.SelectedIndex == 0)
                {
                    msg = "Please select address type";
                    return msg;
                }
                if (ddlProofOfAddress.SelectedIndex == 0)
                {
                    msg = "Please select proof of address";
                    return msg;
                }

                if (ddlProofOfAddress.SelectedIndex != 0)
                {
                    if (ddlProofOfAddress.SelectedIndex == 1)
                    {
                        if (txtPassNoAdd.Text == "")
                        {
                            msg = "please enter passport number";
                            return msg;
                        }
                        if (txtPassExpDateAdd.Text == "")
                        {
                            msg = "please enter passport expiry date";
                            return msg;
                        }
                        if (txtPassExpDateAdd.Text != "")
                        {
                            string pattern = "^(0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-](19|20)[0-9]{2}$";
                            Match match = Regex.Match(txtPassExpDateAdd.Text.ToString() , pattern);
                            if (!match.Success)
                            {
                                msg = "please check passport expiry date format, it must be in dd/mm/yyyy";
                                return msg;
                            }

                            DateTime date1, date2;
                            date1 = DateTime.ParseExact(date.ToString(), "dd-mm-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            date2 = DateTime.ParseExact(txtPassExpDateAdd.Text.ToString(), "dd-mm-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            if (date1 > date2)
                            {
                                msg = "you cannot select past date as passport expiry expiry date";
                                return msg;
                            }
                        }
                    }

                    if (ddlProofOfAddress.SelectedIndex == 2)
                    {
                        if (txtPassNoAdd.Text == "")
                        {
                            msg = "please enter driving licence number";
                            return msg;
                        }
                        if (txtPassExpDateAdd.Text == "")
                        {
                            msg = "please enter driving licence expiry date";
                            return msg;
                        }
                        if (txtPassExpDateAdd.Text != "")
                        {
                            string pattern = "^(0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-](19|20)[0-9]{2}$";
                            Match match = Regex.Match(txtPassExpDateAdd.Text.ToString(), pattern);
                            if (!match.Success)
                            {
                                msg = "please check driving license date format, it must be in dd/mm/yyyy";
                                return msg;
                            }
                            DateTime date1, date2;
                            date1 = DateTime.ParseExact(date.ToString(), "dd-mm-yyyy",System.Globalization.CultureInfo.InvariantCulture);
                            date2 = DateTime.ParseExact(txtPassExpDateAdd.Text.ToString(), "dd-mm-yyyy",System.Globalization.CultureInfo.InvariantCulture);
                            if (date1 > date2)
                            {
                                msg = "you cannot select past date as driving license expiry date";
                                return msg;
                            }
                        }
                    }

                    if (ddlProofOfAddress.SelectedIndex == 3)
                    {
                        if (txtPassNoAdd.Text == "")
                        {
                            msg = "please enter uid(aadhaar)";
                            return msg;
                        }
                    }

                    if (ddlProofOfAddress.SelectedIndex == 4)
                    {
                        if (txtPassNoAdd.Text == "")
                        {
                            msg = "please enter voter id card";
                            return msg;
                        }
                    }
                    //if (ddlProofOfAddress.SelectedIndex == 3)
                    //{
                    //    if (txtPassNoAdd.Text == "")
                    //    {
                    //        msg = "please enter pan card";
                    //        return msg;
                    //    }

                    //}


                    if (ddlProofOfAddress.SelectedIndex == 5)
                    {
                        if (txtPassNoAdd.Text == "")
                        {
                            msg = "please enter nrega job card";
                            return msg;
                        }
                    }
                    if (ddlProofOfAddress.SelectedIndex == 6)
                    {
                        if (txtPassNoAdd.Text == "")
                        {
                            msg = "please enter document name";
                            return msg;
                        }
                        if (txtPassOthrAdd.Text == "")
                        {
                            msg = "please enter identification number";
                            return msg;
                        }
                    }
                    //if (ddlProofOfAddress.SelectedIndex == 8)
                    //{
                    //    if (txtPassNoAdd.Text == "")
                    //    {
                    //        msg = "please enter simplified measures account";
                    //        return msg;
                    //    }
                    //}
                }

                if (txtAddressLine1.Text == "")
                {
                    msg = "Please enter permanent address line 1";
                    return msg;
                }
                if (txtCity.Text == "")
                {
                    msg = "Please enter permanent city/town/village";
                    return msg;
                }

                //if (ddlState.SelectedIndex == 0)
                //{
                //    msg = "Please select permanent state/ U.T. Code then search with icon";
                //    return msg;
                //}


                if (txtPinCode.Text == "" && chkTick.Checked == false)
                {
                    msg = "Please enter permanent pin/post code";
                    return msg;
                }

                if (txtPinCode.Text == "")
                {
                    msg = "Please enter permanent pin/post code";
                    return msg;
                }

                #endregion chkTick END

                #region TO ADD 1

                if (chkLocalAddress.Checked == true)
                {
                    if (ddlAddressType1.SelectedIndex == 0)
                    {
                        msg = "Please select the Address Type in Correspondence Address Details";
                        return msg;
                    }

                    if (txtLocAddLine1.Text == "")
                    {
                        msg = "Please enter the Address Line1 in Correspondence Address Details";
                        return msg;
                    }

                    if (txtCity1.Text == "")
                    {
                        msg = "Please enter the city/town/village in Correspondence Address Details";
                        return msg;
                    }

                    //if (ddlState1.SelectedIndex == 0)
                    //{
                        
                    //    msg = "Please select the state / U.T. of Correspondence Address Details then search with icon";
                    //    return msg;
                    //}

                    if (ddlPinCode1.Text == "")
                    {
                        msg = "Please enter the pin/post code in Correspondence Address Details";
                        return msg;
                    }

                    if (ddlCountryCode1.SelectedIndex == 0)
                    {
                        msg = "Please select the Country Code of Correspondence Address Details";
                        return msg;
                    }
                }

                #endregion END TO ADD

                #region TO ADD 2
                //COmmentedd BY pratik for ckyc ver 1.2 start
                //if (chkAddResident.Checked == true)
                //{
                //    if (ddlAddressType2.SelectedIndex == 0)
                //    {
                //        msg = "Please select the Address Type in Jurisdiction Address Details";
                //        return msg;
                //    }

                //    if (txtAddLine1.Text == "")
                //    {
                //        msg = "Please enter the Address Line1 in Jurisdiction Address Details";
                //        return msg;
                //    }

                //    if (txtCity2.Text == "")
                //    {
                //        msg = "Please enter the city/town/village in Jurisdiction Address Details";
                //        return msg;
                //    }

                //    //if (ddlState2.SelectedIndex == 0)
                //    //{
                //    //    msg = "Please select the state / U.T. of Jurisdiction Address Details then search with icon";
                //    //    return msg;
                //    //}

                //    if (ddlPinCode2.Text == "")
                //    {
                //        msg = "Please enter the pin/post code in Jurisdiction Address Details";
                //        return msg;
                //    }

                //    if (ddlIsoCountryCode.SelectedIndex == 0)
                //    {
                //        msg = "Please select the Country Code of Jurisdiction Address Details";
                //        return msg;
                //    }
                //}
                //COmmentedd BY pratik for ckyc ver 1.2 start
                #endregion END TO ADD 1


                if (chkAppDeclare1.Checked == false)
                {
                    msg = "Please check application declaration number 1";
                    return msg;
                }

                if (chkAppDeclare2.Checked == false)
                {
                    msg = "Please check application declaration number 2";
                    return msg;
                }

                if (txtDate.Text == "")
                {
                    msg = "Please enter declaration date";
                    return msg;
                }

                if (txtPlace.Text == "")
                {
                    msg = "Please enter declaration place";
                    return msg;
                }
                if (chkSelfCerti.Checked == false && chkTrueCopies.Checked == false && chkNotary.Checked == false)
                {
                    msg = "Please check document received checkbox";
                    return msg;
                }

                //if (chkHigh.Checked == false && chkMedium.Checked == false && chkLow.Checked == false)
                //{
                //    msg = "Please check risk category checkbox";
                //    return msg;
                //}

                //chkd
                if (!(chkDone.Checked))
                {
                    msg = "Please check KYC verification carried out identity verification checkbox";
                    return msg;
                }

                if (txtDateKYCver.Text == "")
                {
                    msg = "Please enter KYC verification carried out date";
                    return msg;
                }
                if (txtEmpName.Text == "")
                {
                    msg = "Please enter KYC verification carried out employee name";
                    return msg;
                }
                if (txtEmpCode.Text == "")
                {
                    msg = "Please enter KYC verification carried out employee code";
                    return msg;
                }
                if (txtEmpDesignation.Text == "")
                {
                    msg = "Please enter KYC verification carried out employee designation";
                    return msg;
                }
                if (txtEmpBranch.Text == "")
                {
                    msg = "Please enter KYC verification carried out employee branch";
                    return msg;
                }
                if (txtInsName.Text == "")
                {
                    msg = "Please enter institution name";
                    return msg;
                }
                if (txtInsCode.Text == "")
                {
                    msg = msg = "Please enter institution code";
                    return msg;
                }

            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.IO.FileInfo oInfo = new System.IO.FileInfo(currentFile);
                string sRet = oInfo.Name;
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                String LogClassName = method.ReflectedType.Name;
            }
            return msg;
        }
        #endregion

        #region EntityControlPrsnValidation
        public string EntityControlPrsnValidation(DropDownList ddlRelType, DropDownList cboTitle, TextBox txtGivenName, TextBox txtLastName, DropDownList cboTitle2, RadioButtonList rbtFS,
            TextBox txtGivenName2, TextBox txtLastName2, DropDownList cboTitle3, TextBox txtGivenName3, TextBox txtLastName3, TextBox txtDOB, DropDownList cboGender, DropDownList ddlOccupation,
            DropDownList ddlOccuSubType, DropDownList ddlMaritalStatus, DropDownList ddlCitizenship, DropDownList ddlResStatus, DropDownList ddlIsoCountryCodeOthr, CheckBox chkTick, DropDownList ddlIsoCountryCode2,
            TextBox txtIDResTax, TextBox txtDOBRes, DropDownList ddlIsoCountry, DropDownList ddlProofIdentity, TextBox txtPassNo, TextBox txtPassExpDate, TextBox txtPassOthr, DropDownList ddlAddressType,
            CheckBox chkPerAddress, CheckBox chkAppDeclare1, DropDownList ddlProofOfAddress, TextBox txtAddressLine1, TextBox txtCity, TextBox txtPinCode, TextBox txtDateKYCver,
            TextBox txtEmpName, TextBox txtEmpCode, TextBox txtEmpDesignation, TextBox txtEmpBranch, TextBox txtInsName, TextBox txtInsCode, TextBox txtDate, TextBox txtPassExpDateAdd, TextBox txtPassNoAdd, CheckBox chkAppDeclare2, TextBox txtPlace,
            CheckBox chkSelfCerti, CheckBox chkTrueCopies, CheckBox chkNotary, DropDownList ddlState, CheckBox chkDone, TextBox txtPassOthrAdd,
            CheckBox chkLocalAddress, DropDownList ddlAddressType1, TextBox txtLocAddLine1, TextBox txtCity1, DropDownList ddlState1,
            TextBox ddlPinCode1, DropDownList ddlCountryCode1,
            CheckBox chkAddResident, DropDownList ddlAddressType2, TextBox txtAddLine1, TextBox txtCity2, DropDownList ddlState2, TextBox ddlPinCode2, DropDownList ddlIsoCountryCode)//CheckBox chkHigh, CheckBox chkMedium, CheckBox chkLow,
        {
            string msg = string.Empty;
            string date;
            date = DateTime.Today.ToString("dd\\-MM\\-yyyy");
            try
            {

                #region person details
                if (ddlRelType.SelectedIndex == 0)
                {
                    msg = "Please select type of control";
                    return msg;
                }

                if (cboTitle.SelectedIndex == 0)
                {
                    msg = "Please select prefix of name";
                    return msg;
                }

                if (txtGivenName.Text == "")
                {
                    msg = "Please enter first name";
                    return msg;
                }

                if (txtLastName.Text == "")
                {
                    msg = "Please enter last name";
                    return msg;
                }
                if (rbtFS.SelectedIndex == -1)
                {
                    msg = "Please select father/spouse";
                    return msg;
                }
                if (cboTitle2.SelectedIndex == 0)
                {
                    msg = "Please select prefix of father/spouse name";
                    return msg;
                }
                if (rbtFS.SelectedValue == "F")
                {
                    if (rbtFS.SelectedValue == "MRS" || rbtFS.SelectedValue == "MS")
                    {
                        msg = "Please select valid prefix of father/spouse name";
                        return msg;
                    }
                }

                if (txtGivenName2.Text == "")
                {
                    msg = "Please enter first name of father/spouse";
                    return msg;
                }
                if (txtLastName2.Text == "")
                {
                    msg = "Please enter last name of father/spouse";
                    return msg;
                }

                // Commented by akash for UAT environment only start//

                //if (cboTitle3.SelectedIndex == 0)
                //{
                //    msg = "Please select prefix of mother name";
                //    return msg;
                //}
                //if (txtGivenName3.Text == "")
                //{
                //    msg = "Please enter first name of mother";
                //    return msg;
                //}
                //if (txtLastName3.Text == "")
                //{
                //    msg = "Please enter last name of mother";
                //    return msg;
                //}

                // Commented by akash for UAT environment only end//

                if (txtDOB.Text == "")
                {
                    msg = "Please enter date of birth";
                    return msg;
                }
                if (txtDOB.Text != "")
                {
                    string pattern = "^(0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-](19|20)[0-9]{2}$";
                    Match match = Regex.Match(txtDOB.Text.ToString(), pattern);
                    if (!match.Success)
                    {
                        msg = "Please check DOB date format, it must be in dd/mm/yyyy";
                        return msg;
                    }
                    DateTime date1, date2;
                    date1 = DateTime.ParseExact(date.ToString(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    date2 = DateTime.ParseExact(txtDOB.Text.ToString(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    if (date1 < date2)
                    {
                        msg = "You cannot select future date as DOB";
                        return msg;
                    }
                }
                if (cboGender.SelectedIndex == 0)
                {
                    msg = "Please select gender";
                    return msg;
                }

                if (cboTitle.SelectedIndex != 0 && cboGender.SelectedIndex != 0)
                {
                    if (cboTitle.SelectedValue == "MR" && cboGender.SelectedValue == "F")
                    {
                        msg = "Please select valid gender according to prefix of the name";
                        return msg;
                    }
                }
                //added by ramesh on dated 25-05-2017

                // Commented by akash for UAT environment only start//

                //if (ddlMaritalStatus.SelectedIndex == 0)
                //{
                //    msg = "Please select marital status";
                //    return msg;
                //}

                // Commented by akash for UAT environment only end//

                if (cboTitle.SelectedValue == "MRS" && rbtFS.SelectedValue == "F" && ddlMaritalStatus.SelectedValue == "02")
                {
                    msg = "Please select valid prefix of name and Marital Status";
                    return msg;
                }

                if (cboTitle.SelectedValue == "MRS" && ddlMaritalStatus.SelectedValue == "02")
                {
                    msg = "Please select valid prefix";
                    return msg;
                }
                if (ddlCitizenship.SelectedIndex == 0)
                {
                    msg = "Please select Nationality";
                    return msg;
                }

                if (ddlCitizenship.SelectedIndex == 2)
                {
                    if (ddlIsoCountryCodeOthr.SelectedIndex == 0)
                    {
                        msg = "Please select ISO 3166 country code";
                        return msg;
                    }
                }

                if (ddlResStatus.SelectedIndex == 0)
                {
                    msg = "Please select residential status";
                    return msg;
                }

                if (ddlOccupation.SelectedIndex == 0)
                {
                    msg = "Please select occupation type";
                    return msg;
                }
                if (ddlOccuSubType.SelectedIndex == 0)
                {
                    msg = "Please select occupation sub type";
                    return msg;
                }


                //if (chkTick.Checked == true)
                //{
                if (ddlIsoCountryCode2.SelectedIndex == 0)
                {
                    msg = "Please select ISO 3166 country code of jurisdiction of residence";
                    return msg;
                }
                if (txtIDResTax.Text == "")
                {
                    msg = "Please enter tax identification number or equivalent (if issued by jurisdiction)";
                    return msg;
                }
                if (txtDOBRes.Text == "")
                {
                    msg = "Please enter place/city of birth";
                    return msg;
                }
                if (ddlIsoCountry.SelectedIndex == 0)
                {
                    msg = "Please select ISO 3166 country code of birth";
                    return msg;
                }
                //}
                if (ddlProofIdentity.SelectedIndex == 0)
                {
                    msg = "Please select proof of identity";
                    return msg;
                }
                if (ddlProofIdentity.SelectedIndex != 0)
                {
                    if (ddlProofIdentity.SelectedIndex == 1)
                    {
                        if (txtPassNo.Text == "")
                        {
                            msg = "Please enter passport number";
                            return msg;
                        }
                        if (txtPassExpDate.Text == "")
                        {
                            msg = "Please enter passport expiry date";
                            return msg;
                        }
                        if (txtPassExpDate.Text != "")
                        {
                            string pattern = "^(0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-](19|20)[0-9]{2}$";
                            Match match = Regex.Match(txtPassExpDate.Text.ToString(), pattern);
                            if (!match.Success)
                            {
                                msg = "Please check passport expiry date format, it must be in dd/mm/yyyy";
                                return msg;
                            }

                            DateTime date1, date2;
                            date1 = DateTime.ParseExact(date.ToString(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            date2 = DateTime.ParseExact(txtPassExpDate.Text.ToString(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);

                            string strIdentityName = ddlProofIdentity.SelectedItem.Text;
                            if (date1 > date2)
                            {
                                msg = "You cannot select past date as " + strIdentityName.ToLower() + " expiry date";
                                return msg;
                            }
                        }
                    }
                    if (ddlProofIdentity.SelectedIndex == 2)
                    {
                        if (txtPassNo.Text == "")
                        {
                            msg = "Please enter voter id card";
                            return msg;
                        }
                    }
                    if (ddlProofIdentity.SelectedIndex == 3)
                    {
                        if (txtPassNo.Text == "")
                        {
                            msg = "Please enter pan card";
                            return msg;
                        }

                    }
                    if (ddlProofIdentity.SelectedIndex == 4)
                    {
                        if (txtPassNo.Text == "")
                        {
                            msg = "Please enter driving licence number";
                            return msg;
                        }
                        if (txtPassExpDate.Text == "")
                        {
                            msg = "Please enter driving licence expiry date";
                            return msg;
                        }
                        if (txtPassExpDate.Text != "")
                        {
                            string pattern = "^(0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-](19|20)[0-9]{2}$";
                            Match match = Regex.Match(txtPassExpDate.Text.ToString(), pattern);
                            if (!match.Success)
                            {
                                msg = "Please check driving license expiry date format, it must be in dd/mm/yyyy";
                                return msg;
                            }
                            DateTime date1, date2;
                            date1 = DateTime.ParseExact(date.ToString(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            date2 = DateTime.ParseExact(txtPassExpDate.Text.ToString(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            if (date1 > date2)
                            {
                                msg = "You cannot select past date as driving license expiry date";
                                return msg;
                            }
                        }
                    }
                    if (ddlProofIdentity.SelectedIndex == 5)
                    {
                        if (txtPassNo.Text == "")
                        {
                            msg = "Please enter UID(aadhaar)";
                            return msg;
                        }
                    }
                    if (ddlProofIdentity.SelectedIndex == 6)
                    {
                        if (txtPassNo.Text == "")
                        {
                            msg = "Please enter NREGA job card";
                            return msg;
                        }
                    }
                    if (ddlProofIdentity.SelectedIndex == 7)
                    {
                        if (txtPassNo.Text == "")
                        {
                            msg = "Please enter document name";
                            return msg;
                        }
                        if (txtPassOthr.Text == "")
                        {
                            msg = "Please enter identification number";
                            return msg;
                        }
                    }
                    if (ddlProofIdentity.SelectedIndex == 8)
                    {
                        if (txtPassNo.Text == "")
                        {
                            msg = "Please enter simplified measures account";
                            return msg;
                        }
                        if (txtPassOthr.Text == "")
                        {
                            msg = "Please enter identification number";
                            return msg;
                        }
                    }
                    if (ddlProofIdentity.SelectedIndex == 9)
                    {
                        if (txtPassNo.Text == "")
                        {
                            msg = "Please enter simplified measures account";
                            return msg;
                        }
                        if (txtPassOthr.Text == "")
                        {
                            msg = "Please enter identification number";
                            return msg;
                        }
                    }
                }
                #endregion person details

                #region check 1

                if (chkPerAddress.Checked == true)
                {
                    if (ddlAddressType.SelectedIndex == 0)
                    {
                        msg = "Please select address type";
                        return msg;
                    }
                    if (ddlProofOfAddress.SelectedIndex == 0)
                    {
                        msg = "Please select proof of address";
                        return msg;
                    }


                    if (txtAddressLine1.Text == "")
                    {
                        msg = "Please enter permanent address line 1";
                        return msg;
                    }
                    if (txtCity.Text == "")
                    {
                        msg = "Please enter permanent city/town/village";
                        return msg;
                    }

                    //if (ddlState.SelectedIndex == 0)
                    //{
                    //    msg = "Please select permanent state/ U.T. Code then search with icon";
                    //    return msg;
                    //}

                    if (txtPinCode.Text == "" && chkTick.Checked == false)
                    {
                        msg = "Please enter permanent pin/post code";
                        return msg;
                    }

                    if (txtPinCode.Text == "")
                    {
                        msg = "Please enter permanent pin/post code";
                        return msg;
                    }
                }

                #endregion check 1

                #region TO ADD 1

                if (chkLocalAddress.Checked == true)
                {
                    if (ddlAddressType1.SelectedIndex == 0)
                    {
                        msg = "Please select the Address Type in Correspondence Address Details";
                        return msg;
                    }

                    if (txtLocAddLine1.Text == "")
                    {
                        msg = "Please enter the Address Line1 in Correspondence Address Details";
                        return msg;
                    }

                    if (txtCity1.Text == "")
                    {

                        msg = "Please enter the city/town/village in Correspondence Address Details";
                        return msg;
                    }

                    //if (ddlState1.SelectedIndex == 0)
                    //{
                    //    msg = "Please select permanent state/ U.T. Code of Correspondence Address Details then search with icon";
                    //    //msg = "Please select the State/ U.T. of ";
                    //    return msg;
                    //}

                    if (ddlPinCode1.Text == "")
                    {
                        msg = "Please enter the pin/post code in Correspondence Address Details";
                        return msg;
                    }

                    if (ddlCountryCode1.SelectedIndex == 0)
                    {
                        msg = "Please select the Country Code of Correspondence Address Details";
                        return msg;
                    }
                }

                #endregion END TO ADD

                #region TO ADD 2

                if (chkAddResident.Checked == true)
                {
                    if (ddlAddressType2.SelectedIndex == 0)
                    {
                        msg = "Please select the Address Type in Jurisdiction Address Details";
                        return msg;
                    }

                    if (txtAddLine1.Text == "")
                    {
                        msg = "Please enter the Address Line1 in Jurisdiction Address Details";
                        return msg;
                    }

                    if (txtCity2.Text == "")
                    {
                        msg = "Please enter the city/town/village in Jurisdiction Address Details";
                        return msg;
                    }

                    //if (ddlState2.SelectedIndex == 0)
                    //{

                    //    msg = "Please select the state/ U.T. of Jurisdiction Address Details then search with icon";
                    //    return msg;
                    //}

                    if (ddlPinCode2.Text == "")
                    {
                        msg = "Please enter the pin/post code in Jurisdiction Address Details";
                        return msg;
                    }

                    if (ddlIsoCountryCode.SelectedIndex == 0)
                    {
                        msg = "Please select the Country Code of Jurisdiction Address Details";
                        return msg;
                    }
                }

                #endregion END TO ADD 1


                if (chkAppDeclare1.Checked == false)
                {
                    msg = "Please check application declaration number 1";
                    return msg;
                }

                if (chkAppDeclare2.Checked == false)
                {
                    msg = "Please check application declaration number 2";
                    return msg;
                }

                if (txtDate.Text.Trim() == "")
                {
                    msg = "Please enter declaration date";
                    return msg;
                }

                if (txtPlace.Text == "")
                {
                    msg = "Please enter declaration place";
                    return msg;
                }
                if (chkSelfCerti.Checked == false && chkTrueCopies.Checked == false && chkNotary.Checked == false)
                {
                    msg = "Please check document received checkbox";
                    return msg;
                }

                //if (chkHigh.Checked == false && chkMedium.Checked == false && chkLow.Checked == false)
                //{
                //    msg = "Please check risk category checkbox";
                //    return msg;
                //}

                if (chkDone.Checked == false)
                {
                    msg = "Please check KYC verification carried out identity verification checkbox";
                    return msg;
                }

                if (txtDateKYCver.Text == "")
                {
                    msg = "Please enter KYC verification carried out Date";
                    return msg;
                }
                if (txtEmpName.Text == "")
                {
                    msg = "Please enter KYC verification carried out employee name";
                    return msg;
                }
                if (txtEmpCode.Text == "")
                {
                    msg = "Please enter KYC verification carried out employee code";
                    return msg;
                }
                if (txtEmpDesignation.Text == "")
                {
                    msg = "Please enter KYC verification carried out employee designation";
                    return msg;
                }
                if (txtEmpBranch.Text == "")
                {
                    msg = "Please enter KYC verification carried out employee branch";
                    return msg;
                }
                if (txtInsName.Text == "")
                {
                    msg = "Please enter institution name";
                    return msg;
                }
                if (txtInsCode.Text == "")
                {
                    msg = msg = "Please enter institution code";
                    return msg;
                }
                //if (txtDate.Text == "")
                //{
                //    msg = "Please enter Related Declaration date";
                //    return msg;
                //}
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.IO.FileInfo oInfo = new System.IO.FileInfo(currentFile);
                string sRet = oInfo.Name;
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                String LogClassName = method.ReflectedType.Name;
            }
            return msg;
        }
        #endregion

        #region PersonalDtlsValidation
        public string PersonalDtlsValidation(CheckBox chkNormal, CheckBox chkSimplified, CheckBox Chksmall, DropDownList cboTitle, TextBox txtGivenName, TextBox txtLastName,
            DropDownList cboTitle2, RadioButtonList rbtFS, TextBox txtGivenName2, TextBox txtLastName2, DropDownList cboTitle3, TextBox txtGivenName3, TextBox txtLastName3,
            TextBox txtDOB, DropDownList cboGender,
            DropDownList ddlIsoCountryCodeOthr, DropDownList ddlRelType, string flag)
        {
            string msg = string.Empty;
            string date;
            date = DateTime.Today.ToString("dd\\/MM\\/yyyy");

            try
            {
                if (flag == "RelatedPrsn")
                {
                    if (ddlRelType.SelectedIndex == 0)
                    {
                        msg = "Please select related person type";
                        return msg;
                    }
                }
                if (flag == "Candidate")
                {
                    if (chkNormal.Checked == false && chkSimplified.Checked == false && Chksmall.Checked == false)
                    {
                        msg = "Please select account type";
                        return msg;
                    }
                }
                if (cboTitle.SelectedIndex == 0)
                {
                    msg = "Please select prefix of name";
                    return msg;
                }
                if (txtGivenName.Text == "")
                {
                    msg = "Please enter first name";
                    return msg;
                }
                if (txtLastName.Text == "")
                {
                    msg = "Please enter last name";
                    return msg;
                }
                if (rbtFS.SelectedIndex == -1)
                {
                    msg = "Please select father/spouse";
                    return msg;
                }
                if (cboTitle2.SelectedIndex == 0)
                {
                    msg = "Please select prefix of father/spouse Name";
                    return msg;
                }
                if (rbtFS.SelectedValue == "F")
                {
                    if (cboTitle2.SelectedValue == "MRS" || cboTitle2.SelectedValue == "MS")
                    {
                        msg = "Invalid prefix for father/spouse name";
                        return msg;
                    }
                }
                else if (rbtFS.SelectedValue == "S")

                    if (cboTitle2.SelectedValue == "MR")
                    {
                        msg = "Invalid prefix for father/spouse name";
                        return msg;
                    }
                if (txtGivenName2.Text == "")
                {
                    msg = "Please enter first name of father/spouse";
                    return msg;
                }
                if (txtLastName2.Text == "")
                {
                    msg = "Please enter last name of father/spouse";
                    return msg;
                }
                if (cboTitle3.SelectedIndex == 0)
                {
                    msg = "Please select prefix of mother name";
                    return msg;
                }
                if (txtGivenName3.Text == "")
                {
                    msg = "Please enter first name of mother";
                    return msg;
                }
                if (txtLastName3.Text == "")
                {
                    msg = "Please enter last name of mother";
                    return msg;
                }
                if (txtDOB.Text == "")
                {
                    msg = "Please enter date of birth";
                    return msg;
                }
                if (txtDOB.Text != "")
                {
                    string pattern = "^(0[1-9]|[12][0-9]|3[01])[/](0[1-9]|1[012])[/](19|20)[0-9]{2}$";
                    Match match = Regex.Match(txtDOB.Text.ToString(), pattern);
                    if (!match.Success)
                    {
                        msg = "Check DOB date format it must be in dd/mm/yyyy";
                        return msg;
                    }

                    DateTime date1, date2;
                    date1 = DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    date2 = DateTime.ParseExact(txtDOB.Text.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    if (date1 < date2)
                    {
                        msg = "DOB can not be future date";
                        return msg;
                    }
                }
                if (cboGender.SelectedIndex == 0)
                {
                    msg = "Please select gender";
                    return msg;
                }          
                if (cboTitle.SelectedValue == "MRS" )
                {
                    msg = "Invalid prefix";
                    return msg;
                }               
            }
            catch (Exception ex)
            {

            }
            return msg;
        }
        #endregion

        #region LegalEntitydtlsValidation
        public string LegalEntityDtlsValidation(CheckBox chkUSReport, CheckBox chkOtherReport, DropDownList ddlAccHolderType, DropDownList ddlNatureOfBuss, TextBox txtRefNumber, TextBox txtKYCName, TextBox txtDatOfInc, TextBox txtDtOfCom,
           TextBox txtPlaceOfInc, DropDownList ddlCountrOfInc, DropDownList ddlCountryOfRsidens, DropDownList ddlIdentyType, TextBox txtTypeIdentiNo, DropDownList ddlTINCountry,
           TextBox txtPanNo, DropDownList ddlNumberOfPerson, DropDownList ddlCertifiecopy, CheckBox chkPerAddress, CheckBox chkLocalAddress, CheckBox chkCuurentAddress,
           DropDownList ddlAddressType, DropDownList ddlProofOfAddress, TextBox txtAddressLine1, DropDownList ddlState, TextBox txtPinCode, DropDownList ddlCountryCode, DropDownList ddlState1, TextBox ddlPinCode1, DropDownList ddlCountryCode1, TextBox txtCity,
           DropDownList ddlAddressType1, TextBox txtLocAddLine1, TextBox txtCity1,
            DropDownList ddlProofOfAddress1, DropDownList ddlAddressType2, DropDownList ddlProofOfAddress2, TextBox txtAddLine1, CheckBox chkAddRel,
            CheckBox chkAddCtrl, CheckBox chkAppDeclare1, CheckBox chkAppDeclare2, CheckBox chkAppDeclare3, TextBox txtDate, TextBox txtPlace, CheckBox chkSelfCerti, CheckBox chkTrueCopies,
            CheckBox chkNotary, CheckBox chkHigh, CheckBox chkMedium, CheckBox chkLow, CheckBox chkAddResident, TextBox txtPassNo, CheckBox chkCorresAdd, CheckBox chkDone, TextBox txtDateKYCver, TextBox txtEmpName,
            TextBox txtEmpCode, TextBox txtEmpDesignation, TextBox txtEmpBranch, TextBox txtInsName, TextBox txtInsCode, CheckBox chkCurrentAdd, TextBox txtCity2,
            DropDownList ddlState2, TextBox ddlPinCode2, TextBox txtDistrict2, DropDownList ddlIsoCountryCode, DropDownList ddlDocReceived = null) /*CheckBox chkCurrentAdd, */
        {
            string msg = string.Empty;
            string date;
            date = DateTime.Today.ToString("dd\\-MM\\-yyyy");

            #region try
            try
            {
                if (txtRefNumber.Text == "")
                {
                    msg = "Please enter FI Reference Number";
                    //txtKYCName.Focus();
                    return msg;
                }
                if (chkUSReport.Checked == false && chkOtherReport.Checked == false)
                {
                    msg = "Please check account holder type checkbox";
                    return msg;
                }
                if (ddlAccHolderType.SelectedIndex == 0)
                {
                    msg = "Please select account holder sub type";
                    return msg;
                }

                //if (ddlNatureOfBuss.SelectedIndex == 0)
                //{
                //    msg = "Please select the Nature of Business type";
                //    return msg;
                //}

                if (txtKYCName.Text == "")
                {
                    msg = "Please enter the Name";
                    //txtKYCName.Focus();
                    return msg;
                }


                if (txtDatOfInc.Text == "")
                {
                    msg = "Please enter date of Incorporation";
                    return msg;
                }

                //if (txtDtOfCom.Text == "")
                //{
                //    msg = "Please enter date of Commencement of Business";
                //    return msg;
                //}


                if (txtPlaceOfInc.Text == "")
                {
                    msg = "Please enter the Place of Incorporation";
                    return msg;
                }

                if (ddlCountrOfInc.SelectedIndex == 0)
                {
                    msg = "Please select the Country of Incorporation";
                    return msg;
                }

                if (ddlCountryOfRsidens.SelectedIndex == 0)
                {
                    msg = "Please select the Country of Residence as per Tax laws";
                    return msg;
                }

                if (txtPanNo.Text == "")
                {
                    msg = "Please enter the PAN Number";
                    return msg;
                }

                if (ddlIdentyType.SelectedIndex == 0)
                {
                    msg = "Please select the Identification type";
                    return msg;
                }

                //if (txtTypeIdentiNo.Text == "")
                //{

                //    msg = "Please enter the Tax Identification Number";
                //    return msg;
                //}
                //if (txtTypeIdentiNo.Text != "")
                //{
                //    //string pattern = "^[0-9]{0,20}$";
                //    //Match match = Regex.Match(txtTypeIdentiNo.Text.ToString(), pattern);
                //    //if (!match.Success)
                //    //{
                //    //    msg = "Please enter the Tax Identification Number properly";
                //    //    return msg;
                //    //}
                //}


                if (txtTypeIdentiNo.Text != "")
                {
                    if (ddlTINCountry.SelectedIndex == 0)
                    {
                        msg = "Please select the TIN Issuing Country";
                        return msg;
                    }
                    //string pattern = "^[0-9]{0,20}$";
                    //Match match = Regex.Match(txtTypeIdentiNo.Text.ToString(), pattern);
                    //if (!match.Success)
                    //{
                    //    msg = "Please enter the Tax Identification Number properly";
                    //    return msg;
                    //}
                }

                //if (ddlTINCountry.SelectedIndex == 0)
                //{
                //    msg = "Please select the TIN  country";
                //    return msg;
                //}

                //if (txtPanNo.Text == "")
                //{
                //    msg = "Please enter the Pan  Number";
                //    return msg;
                //}

                if (txtPanNo.Text != "")
                {
                    //string pattern = "[A-Za-z]{5}[0-9]{4}[A-Za-z]{1}";
                    //Match match = Regex.Match(txtPanNo.Text.ToString(), pattern);
                    //if (!match.Success)
                    //{
                    //    msg = "Please enter the PAN Number properly";
                    //    return msg;
                    //}
                }

                //if (txtNumberOfPerson.Text == "")
                //{
                //    msg = "Please enter the Number of Person ";
                //    return msg;
                //}

                if (ddlCertifiecopy.SelectedIndex == 0)
                {
                    msg = "Please select the Proof Of Identity";
                    return msg;
                }

                if (txtPassNo.Text == "")
                {
                    msg = "Please enter the POI Number";
                    return msg;
                }


                if (chkPerAddress.Checked == false)
                {
                    msg = "Please check the Current/Permanent/Overseas Address Details checkbox";
                    return msg;
                }


                if (ddlAddressType.SelectedIndex == 0)
                {
                    msg = "Please select Address Type";
                    return msg;
                }

                if (ddlProofOfAddress.SelectedIndex == 0)
                {
                    msg = "Please select Proof of Address";
                    return msg;
                }

                if (txtAddressLine1.Text == "")
                {
                    msg = "Please enter Address Line1";
                    return msg;
                }

                if (txtCity.Text == "")
                {
                    msg = "Please enter permanent city/town/village";
                    return msg;
                }

                //if (ddlState.SelectedIndex == 0)
                //{
                //    msg = "Please select permanent state/ U.T. Code then search with icon";
                //    return msg;
                //}


                if (txtPinCode.Text == "")
                {
                    msg = "Please enter permanent pin/post code";
                    return msg;
                }


                if (ddlCountryCode.SelectedIndex == 0)
                {
                    msg = "Please select ISO Country Code";
                    return msg;
                }






                //if (chkLocalAddress.Checked == false)
                //{
                //    msg = "Please check the Correspondance / Local Address details checkbox";
                //    return msg;
                //}

                //if (chkCuurentAddress.Checked == false)
                //{
                //    msg = "Please check the Same as Current / Permanent / Overseas Address details checkbox";
                //    return msg;
                //}

                if (chkLocalAddress.Checked == true)
                {
                    if (ddlAddressType1.SelectedIndex == 0)
                    {
                        msg = "Please select the Address Type of Correspondence Address Details";
                        return msg;
                    }

                    if (ddlProofOfAddress1.SelectedIndex == 0)
                    {
                        msg = "Please select the Proof of Address in Correspondence Address Details";
                        return msg;
                    }

                    if (txtLocAddLine1.Text == "")
                    {
                        msg = "Please enter Address Line1 of Correspondence Address Details";
                        return msg;
                    }

                    if (txtCity1.Text == "")
                    {
                        msg = "Please enter the city/town/village of Correspondence Address Details";
                        return msg;
                    }

                    //if (ddlState1.SelectedIndex == 0)
                    //{
                    //    msg = "Please select the state / U.T. of Correspondence Address Details then search with icon";
                    //    return msg;
                    //}

                    if (ddlPinCode1.Text == "")
                    {
                        msg = "Please enter the pin/post code of Correspondence Address Details";
                        return msg;
                    }

                    if (ddlCountryCode1.Text == "")
                    {
                        msg = "Please select ISO Country Code of Correspondence Address Details";
                        return msg;
                    }
                }

                if (ddlCountryOfRsidens.SelectedValue != "IN")
                {
                    if (chkAddResident.Checked == false)
                    {
                        msg = "Please check the Address in the Jurisdiction details checkbox";
                        return msg;
                    }
                }

                if (ddlCountryOfRsidens.SelectedValue != "IN" || chkAddResident.Checked == true)
                {
                    if (ddlAddressType2.SelectedIndex == 0)
                    {
                        msg = "Please select the Address Type of Jurisdiction Address Details";
                        return msg;
                    }

                    //if (ddlProofOfAddress2.SelectedIndex == 0)
                    //{
                    //    msg = "Please select the Proof Of Address in Jurisdiction Address Details";
                    //    return msg;
                    //}

                    ///////////////////////////////////////////////////

                    if (txtAddLine1.Text == "")
                    {
                        msg = "Please enter Address Line1 of Jurisdiction Address Details";
                        return msg;
                    }

                    if (txtCity2.Text == "")
                    {
                        msg = "Please enter the city/town/village of Jurisdiction Address Details";
                        return msg;
                    }

                    //if (ddlState2.SelectedIndex == 0)
                    //{
                    //    msg = "Please select the state / U.T. of Jurisdiction Address Details then search with icon";
                    //    return msg;
                    //}

                    if (ddlPinCode2.Text == "")
                    {
                        msg = "Please enter the pin/post code of Jurisdiction Address Details";
                        return msg;
                    }

                    ///////////////////////////

                    if (txtDistrict2.Text == "")
                    {
                        msg = "Please enter the district of Jurisdiction Address Details";
                        return msg;
                    }

                    if (ddlIsoCountryCode.SelectedIndex == 0)
                    {
                        msg = "Please select ISO Country Code Of Jurisdiction Address Details";
                        return msg;
                    }
                }

                if (chkAddRel.Checked == false)
                {
                    msg = "Please check the Addition of Related Person checkbox";
                    return msg;
                }

                if(ddlNumberOfPerson.SelectedIndex != 0)
                {
                    if (chkAddCtrl.Checked == false)
                    {
                        msg = "Please check the Addition of Controlling Person checkbox";
                        return msg;
                    }
                }

                if (ddlCountryOfRsidens.SelectedValue != "IN")
                {
                    if (chkAddCtrl.Checked == false)
                    {
                        msg = "Please check the Addition of Controlling Person checkbox";
                        return msg;
                    }
                }



                if (chkAppDeclare1.Checked == false)
                {
                    msg = "Please check application declaration number 1";
                    return msg;
                }

                if (chkAppDeclare2.Checked == false)
                {
                    msg = "Please check application declaration number 2";
                    return msg;
                }

                if (chkAppDeclare3.Checked == false)
                {
                    msg = "Please check application declaration number 3";
                    return msg;
                }

                if (txtDate.Text == "")
                {
                    msg = "Please select declaration date";
                    return msg;
                }


                if (txtPlace.Text == "")
                {
                    msg = "Please enter declaration place";
                    return msg;
                }


                //if (chkSelfCerti.Checked == false && chkTrueCopies.Checked == false && chkNotary.Checked == false)
                //{
                //    msg = "Please check document received checkbox";
                //    return msg;
                //}

                if (ddlDocReceived != null)
                {
                    if (ddlDocReceived.SelectedIndex == 0)
                    {
                        msg = "Please select document received";
                        return msg;
                    }
                }
                //if (chkHigh.Checked == false && chkMedium.Checked == false && chkLow.Checked == false)
                //{
                //    msg = "Please check risk category checkbox";
                //    return msg;
                //}

                if (!(chkDone.Checked))
                {
                    msg = "Please check KYC verification carried out identity verification checkbox";
                    return msg;
                }

                if (txtDateKYCver.Text == "")
                {
                    msg = "Please enter KYC verification carried out date";
                    return msg;
                }
                if (txtEmpName.Text == "")
                {
                    msg = "Please enter KYC verification carried out employee name";
                    return msg;
                }
                if (txtEmpCode.Text == "")
                {
                    msg = "Please enter KYC verification carried out employee code";
                    return msg;
                }
                if (txtEmpDesignation.Text == "")
                {
                    msg = "Please enter KYC verification carried out employee designation";
                    return msg;
                }
                if (txtEmpBranch.Text == "")
                {
                    msg = "Please enter KYC verification carried out employee branch";
                    return msg;
                }
                if (txtInsName.Text == "")
                {
                    msg = "Please enter institution name";
                    return msg;
                }
                if (txtInsCode.Text == "")
                {
                    msg = msg = "Please enter institution code";
                    return msg;
                }


            }
            catch
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.IO.FileInfo oInfo = new System.IO.FileInfo(currentFile);
                string sRet = oInfo.Name;
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                String LogClassName = method.ReflectedType.Name;
            }
            #endregion
            return msg;
        }
        #endregion

        #region LegalEntityDtlsAsRelatedValidation
        public string LegalEntityDtlsAsRelatedValidation(DropDownList ddlRelType, TextBox txtKYCName, TextBox txtDatOfInc, TextBox txtDtOfCom,
           TextBox txtPlaceOfInc, DropDownList ddlCountrOfInc, DropDownList ddlCountryOfRsidens, DropDownList ddlIdentyType, TextBox txtTypeIdentiNo, DropDownList ddlTINCountry,
           TextBox txtPanNo, DropDownList ddlNumberOfPerson, DropDownList ddlCertifiecopy, CheckBox chkPerAddress, CheckBox chkLocalAddress, CheckBox chkCuurentAddress,
           DropDownList ddlAddressType, DropDownList ddlProofOfAddress, TextBox txtAddressLine1, DropDownList ddlState, TextBox txtPinCode, DropDownList ddlCountryCode, DropDownList ddlState1, TextBox ddlPinCode1, DropDownList ddlCountryCode1, TextBox txtCity,
           DropDownList ddlAddressType1, TextBox txtLocAddLine1, TextBox txtCity1,
             DropDownList ddlAddressType2, DropDownList ddlProofOfAddress2, TextBox txtAddLine1, CheckBox chkAddRel,
            CheckBox chkAddCtrl, CheckBox chkAppDeclare1, CheckBox chkAppDeclare2, CheckBox chkAppDeclare3, TextBox txtDate, TextBox txtPlace, CheckBox chkSelfCerti, CheckBox chkTrueCopies,
            CheckBox chkNotary, CheckBox chkHigh, CheckBox chkMedium, CheckBox chkLow, CheckBox chkAddResident, TextBox txtPassNo, CheckBox chkCorresAdd, CheckBox chkDone, TextBox txtDateKYCver, TextBox txtEmpName,
            TextBox txtEmpCode, TextBox txtEmpDesignation, TextBox txtEmpBranch, TextBox txtInsName, TextBox txtInsCode, CheckBox chkCurrentAdd, TextBox txtCity2,
            DropDownList ddlState2, TextBox ddlPinCode2, TextBox txtDistrict2, DropDownList ddlIsoCountryCode) /*CheckBox chkCurrentAdd, DropDownList ddlProofOfAddress1,*/
        {
            string msg = string.Empty;
            string date;
            date = DateTime.Today.ToString("dd\\-MM\\-yyyy");

            #region try
            try
            {
                //if (txtRefNumber.Text == "")
                //{
                //    msg = "Please enter FI Reference Number";
                //    //txtKYCName.Focus();
                //    return msg;
                //}
                //if (chkUSReport.Checked == false && chkOtherReport.Checked == false)
                //{
                //    msg = "Please check account holder type checkbox";
                //    return msg;
                //}
                //if (ddlAccHolderType.SelectedIndex == 0)
                //{
                //    msg = "Please select account holder sub type";
                //    return msg;
                //}

                //if (ddlNatureOfBuss.SelectedIndex == 0)
                //{
                //    msg = "Please select the Nature of Business type";
                //    return msg;
                //}

                if (ddlRelType.SelectedIndex == 0)
                {
                    msg = "Please select person type";
                    return msg;
                }

                if (txtKYCName.Text == "")
                {
                    msg = "Please enter the Name";
                    //txtKYCName.Focus();
                    return msg;
                }


                if (txtDatOfInc.Text == "")
                {
                    msg = "Please enter date of Incorporation";
                    return msg;
                }

                //if (txtDtOfCom.Text == "")
                //{
                //    msg = "Please enter date of Commencement of Business";
                //    return msg;
                //}


                if (txtPlaceOfInc.Text == "")
                {
                    msg = "Please enter the Place of Incorporation";
                    return msg;
                }

                if (ddlCountrOfInc.SelectedIndex == 0)
                {
                    msg = "Please select the Country of Incorporation";
                    return msg;
                }

                if (ddlCountryOfRsidens.SelectedIndex == 0)
                {
                    msg = "Please select the Country of Residence as per Tax laws";
                    return msg;
                }

                if (txtPanNo.Text == "")
                {
                    msg = "Please enter the PAN Number";
                    return msg;
                }

                if (ddlIdentyType.SelectedIndex == 0)
                {
                    msg = "Please select the Identification type";
                    return msg;
                }

                //if (txtTypeIdentiNo.Text == "")
                //{

                //    msg = "Please enter the Tax Identification Number";
                //    return msg;
                //}
                //if (txtTypeIdentiNo.Text != "")
                //{
                //    //string pattern = "^[0-9]{0,20}$";
                //    //Match match = Regex.Match(txtTypeIdentiNo.Text.ToString(), pattern);
                //    //if (!match.Success)
                //    //{
                //    //    msg = "Please enter the Tax Identification Number properly";
                //    //    return msg;
                //    //}
                //}


                if (txtTypeIdentiNo.Text != "")
                {
                    if (ddlTINCountry.SelectedIndex == 0)
                    {
                        msg = "Please select the TIN Issuing Country";
                        return msg;
                    }
                    //string pattern = "^[0-9]{0,20}$";
                    //Match match = Regex.Match(txtTypeIdentiNo.Text.ToString(), pattern);
                    //if (!match.Success)
                    //{
                    //    msg = "Please enter the Tax Identification Number properly";
                    //    return msg;
                    //}
                }

                //if (ddlTINCountry.SelectedIndex == 0)
                //{
                //    msg = "Please select the TIN  country";
                //    return msg;
                //}

                //if (txtPanNo.Text == "")
                //{
                //    msg = "Please enter the Pan  Number";
                //    return msg;
                //}

                if (txtPanNo.Text != "")
                {
                    //string pattern = "[A-Za-z]{5}[0-9]{4}[A-Za-z]{1}";
                    //Match match = Regex.Match(txtPanNo.Text.ToString(), pattern);
                    //if (!match.Success)
                    //{
                    //    msg = "Please enter the PAN Number properly";
                    //    return msg;
                    //}
                }

                //if (txtNumberOfPerson.Text == "")
                //{
                //    msg = "Please enter the Number of Person ";
                //    return msg;
                //}

                if (ddlCertifiecopy.SelectedIndex == 0)
                {
                    msg = "Please select the Proof Of Identity";
                    return msg;
                }

                if (txtPassNo.Text == "")
                {
                    msg = "Please enter the POI Number";
                    return msg;
                }


                if (chkPerAddress.Checked == false)
                {
                    msg = "Please check the Current/Permanent/Overseas Address Details checkbox";
                    return msg;
                }


                if (ddlAddressType.SelectedIndex == 0)
                {
                    msg = "Please select Address Type";
                    return msg;
                }

                if (ddlProofOfAddress.SelectedIndex == 0)
                {
                    msg = "Please select Proof of Address";
                    return msg;
                }

                if (txtAddressLine1.Text == "")
                {
                    msg = "Please enter Address Line1";
                    return msg;
                }

                if (txtCity.Text == "")
                {
                    msg = "Please enter permanent city/town/village";
                    return msg;
                }

                //if (ddlState.SelectedIndex == 0)
                //{
                //    msg = "Please select permanent state/ U.T. Code then search with icon";
                //    return msg;
                //}


                if (txtPinCode.Text == "")
                {
                    msg = "Please enter permanent pin/post code";
                    return msg;
                }


                if (ddlCountryCode.SelectedIndex == 0)
                {
                    msg = "Please select ISO Country Code";
                    return msg;
                }






                //if (chkLocalAddress.Checked == false)
                //{
                //    msg = "Please check the Correspondance / Local Address details checkbox";
                //    return msg;
                //}

                //if (chkCuurentAddress.Checked == false)
                //{
                //    msg = "Please check the Same as Current / Permanent / Overseas Address details checkbox";
                //    return msg;
                //}

                if (chkLocalAddress.Checked == true)
                {
                    if (ddlAddressType1.SelectedIndex == 0)
                    {
                        msg = "Please select the Address Type of Correspondence Address Details";
                        return msg;
                    }

                    //if (ddlProofOfAddress1.SelectedIndex == 0)
                    //{
                    //    msg = "Please select the Proof of Address in Correspondence Address Details";
                    //    return msg;
                    //}

                    if (txtLocAddLine1.Text == "")
                    {
                        msg = "Please enter Address Line1 of Correspondence Address Details";
                        return msg;
                    }

                    if (txtCity1.Text == "")
                    {
                        msg = "Please enter the city/town/village of Correspondence Address Details";
                        return msg;
                    }

                    //if (ddlState1.SelectedIndex == 0)
                    //{
                    //    msg = "Please select the state / U.T. of Correspondence Address Details then search with icon";
                    //    return msg;
                    //}

                    if (ddlPinCode1.Text == "")
                    {
                        msg = "Please enter the pin/post code of Correspondence Address Details";
                        return msg;
                    }

                    if (ddlCountryCode1.Text == "")
                    {
                        msg = "Please select ISO Country Code of Correspondence Address Details";
                        return msg;
                    }
                }

                if (ddlCountryOfRsidens.SelectedValue != "IN")
                {
                    if (chkAddResident.Checked == false)
                    {
                        msg = "Please check the Address in the Jurisdiction details checkbox";
                        return msg;
                    }
                }

                if (ddlCountryOfRsidens.SelectedValue != "IN" || chkAddResident.Checked == true)
                {
                    if (ddlAddressType2.SelectedIndex == 0)
                    {
                        msg = "Please select the Address Type of Jurisdiction Address Details";
                        return msg;
                    }

                    //if (ddlProofOfAddress2.SelectedIndex == 0)
                    //{
                    //    msg = "Please select the Proof Of Address in Jurisdiction Address Details";
                    //    return msg;
                    //}

                    ///////////////////////////////////////////////////

                    if (txtAddLine1.Text == "")
                    {
                        msg = "Please enter Address Line1 of Jurisdiction Address Details";
                        return msg;
                    }

                    if (txtCity2.Text == "")
                    {
                        msg = "Please enter the city/town/village of Jurisdiction Address Details";
                        return msg;
                    }

                    //if (ddlState2.SelectedIndex == 0)
                    //{
                    //    msg = "Please select the state / U.T. of Jurisdiction Address Details then search with icon";
                    //    return msg;
                    //}

                    if (ddlPinCode2.Text == "")
                    {
                        msg = "Please enter the pin/post code of Jurisdiction Address Details";
                        return msg;
                    }

                    ///////////////////////////

                    if (txtDistrict2.Text == "")
                    {
                        msg = "Please enter the district of Jurisdiction Address Details";
                        return msg;
                    }

                    if (ddlIsoCountryCode.SelectedIndex == 0)
                    {
                        msg = "Please select ISO Country Code Of Jurisdiction Address Details";
                        return msg;
                    }
                }

                //if (chkAddRel.Checked == false)
                //{
                //    msg = "Please check the Addition of Related Person checkbox";
                //    return msg;
                //}

                //if (ddlNumberOfPerson.SelectedIndex != 0)
                //{
                //    if (chkAddCtrl.Checked == false)
                //    {
                //        msg = "Please check the Addition of Controlling Person checkbox";
                //        return msg;
                //    }
                //}

                //if (ddlCountryOfRsidens.SelectedValue != "IN")
                //{
                //    if (chkAddCtrl.Checked == false)
                //    {
                //        msg = "Please check the Addition of Controlling Person checkbox";
                //        return msg;
                //    }
                //}



                if (chkAppDeclare1.Checked == false)
                {
                    msg = "Please check application declaration number 1";
                    return msg;
                }

                if (chkAppDeclare2.Checked == false)
                {
                    msg = "Please check application declaration number 2";
                    return msg;
                }

                if (chkAppDeclare3.Checked == false)
                {
                    msg = "Please check application declaration number 3";
                    return msg;
                }

                if (txtDate.Text == "")
                {
                    msg = "Please select declaration date";
                    return msg;
                }


                if (txtPlace.Text == "")
                {
                    msg = "Please enter declaration place";
                    return msg;
                }


                if (chkSelfCerti.Checked == false && chkTrueCopies.Checked == false && chkNotary.Checked == false)
                {
                    msg = "Please check document received checkbox";
                    return msg;
                }

                //if (chkHigh.Checked == false && chkMedium.Checked == false && chkLow.Checked == false)
                //{
                //    msg = "Please check risk category checkbox";
                //    return msg;
                //}

                if (!(chkDone.Checked))
                {
                    msg = "Please check KYC verification carried out identity verification checkbox";
                    return msg;
                }

                if (txtDateKYCver.Text == "")
                {
                    msg = "Please enter KYC verification carried out date";
                    return msg;
                }
                if (txtEmpName.Text == "")
                {
                    msg = "Please enter KYC verification carried out employee name";
                    return msg;
                }
                if (txtEmpCode.Text == "")
                {
                    msg = "Please enter KYC verification carried out employee code";
                    return msg;
                }
                if (txtEmpDesignation.Text == "")
                {
                    msg = "Please enter KYC verification carried out employee designation";
                    return msg;
                }
                if (txtEmpBranch.Text == "")
                {
                    msg = "Please enter KYC verification carried out employee branch";
                    return msg;
                }
                if (txtInsName.Text == "")
                {
                    msg = "Please enter institution name";
                    return msg;
                }
                if (txtInsCode.Text == "")
                {
                    msg = msg = "Please enter institution code";
                    return msg;
                }


            }
            catch
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.IO.FileInfo oInfo = new System.IO.FileInfo(currentFile);
                string sRet = oInfo.Name;
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                String LogClassName = method.ReflectedType.Name;
            }
            #endregion
            return msg;
        }
        #endregion

        #region RelatedPrsnValidationAccorKYC
        //added by daksh RelatedPrsnValidationAccorKYC
        public string RelatedPrsnValidationAccorKYC(DropDownList ddlRelType, DropDownList cboTitle, TextBox txtGivenName, TextBox txtLastName, DropDownList cboTitle2, RadioButtonList rbtFS,
            TextBox txtGivenName2, TextBox txtLastName2, DropDownList cboTitle3, TextBox txtGivenName3, TextBox txtLastName3, TextBox txtDOB, DropDownList cboGender, DropDownList ddlOccupation,
            DropDownList ddlOccuSubType, DropDownList ddlMaritalStatus, DropDownList ddlCitizenship, DropDownList ddlResStatus, DropDownList ddlIsoCountryCodeOthr, CheckBox chkTick, DropDownList ddlIsoCountryCode2,
            TextBox txtIDResTax, TextBox txtDOBRes, DropDownList ddlIsoCountry, DropDownList ddlProofIdentity, TextBox txtPassNo, TextBox txtPassExpDate, TextBox txtPassOthr, DropDownList ddlAddressType,
            CheckBox chkPerAddress, CheckBox chkAppDeclare1, DropDownList ddlProofOfAddress, TextBox txtAddressLine1, TextBox txtCity, TextBox txtPinCode, TextBox txtDateKYCver, TextBox txtDate,
            TextBox txtEmpName, TextBox txtEmpCode, TextBox txtEmpDesignation, TextBox txtEmpBranch, TextBox txtInsName, TextBox txtInsCode, TextBox txtPassExpDateAdd, TextBox txtPassNoAdd, CheckBox chkAppDeclare2, TextBox txtPlace,
            CheckBox chkSelfCerti, CheckBox chkTrueCopies, CheckBox chkNotary, DropDownList ddlState, CheckBox chkDone, TextBox txtPassOthrAdd)
        {
            string msg = string.Empty;
            string date;
            date = DateTime.Today.ToString("dd\\-MM\\-yyyy");
            try
            {
                if (ddlRelType.SelectedIndex == 0)
                {
                    msg = "Please select person type";
                    return msg;
                }

                if (cboTitle.SelectedIndex == 0)
                {
                    msg = "Please select prefix of name";
                    return msg;
                }
                if (txtGivenName.Text == "")
                {
                    msg = "Please enter first name";
                    return msg;
                }
                if (txtLastName.Text == "")
                {
                    msg = "Please enter last name";
                    return msg;
                }
                //if (rbtFS.SelectedIndex == -1)
                //{
                //    msg = "Please select father/spouse";
                //    return msg;
                //}
                //if (cboTitle2.SelectedIndex == 0)
                //{
                //    msg = "Please select prefix of father/spouse name";
                //    return msg;
                //}
                //if (rbtFS.SelectedValue == "F")
                //{
                //    if (rbtFS.SelectedValue == "MRS" || rbtFS.SelectedValue == "MS")
                //    {
                //        msg = "Please select valid prefix of father/spouse name";
                //        return msg;
                //    }
                //}

                //if (txtGivenName2.Text == "")
                //{
                //    msg = "Please enter first name of father/spouse";
                //    return msg;
                //}
                //if (txtLastName2.Text == "")
                //{
                //    msg = "Please enter last name of father/spouse";
                //    return msg;
                //}
                //if (cboTitle3.SelectedIndex == 0)
                //{
                //    msg = "Please select prefix of mother name";
                //    return msg;
                //}
                //if (txtGivenName3.Text == "")
                //{
                //    msg = "Please enter first name of mother";
                //    return msg;
                //}
                //if (txtLastName3.Text == "")
                //{
                //    msg = "Please enter last name of mother";
                //    return msg;
                //}
                //if (txtDOB.Text == "")
                //{
                //    msg = "Please enter date of birth";
                //    return msg;
                //}
                //if (txtDOB.Text != "")
                //{
                //    string pattern = "^(0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-](19|20)[0-9]{2}$";
                //    Match match = Regex.Match(txtDOB.Text.ToString(), pattern);
                //    if (!match.Success)
                //    {
                //        msg = "Check DOB date format it must be in dd/mm/yyyy";
                //        return msg;
                //    }
                //    DateTime date1, date2;
                //    date1 = DateTime.ParseExact(date.ToString(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                //    date2 = DateTime.ParseExact(txtDOB.Text.ToString(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                //    if (date1 < date2)
                //    {
                //        msg = "You cannot select future date";
                //        return msg;
                //    }
                //}
                //if (cboGender.SelectedIndex == 0)
                //{
                //    msg = "Please select gender";
                //    return msg;
                //}

                //if (cboTitle.SelectedIndex != 0 && cboGender.SelectedIndex != 0)
                //{
                //    if (cboTitle.SelectedValue == "MR" && cboGender.SelectedValue == "F")
                //    {
                //        msg = "Please select valid gender according to prefix of the name";
                //        return msg;
                //    }
                //}
                ////added by ramesh on dated 25-05-2017

                //if (cboTitle.SelectedValue == "MRS" && rbtFS.SelectedValue == "F" && ddlMaritalStatus.SelectedValue == "02")
                //{
                //    msg = "Please select valid prefix of name and Marital Status";
                //    return msg;
                //}




                //if (ddlMaritalStatus.SelectedIndex == 0)
                //{
                //    msg = "Please select marital status";
                //    return msg;
                //}



                //if (cboTitle.SelectedValue == "MRS" && ddlMaritalStatus.SelectedValue == "02")
                //{
                //    msg = "Please select valid prefix";
                //    return msg;
                //}
                //if (ddlCitizenship.SelectedIndex == 0)
                //{
                //    msg = "Please select Nationality";
                //    return msg;
                //}
                //if (ddlResStatus.SelectedIndex == 0)
                //{
                //    msg = "Please select residential status";
                //    return msg;
                //}

                //if (ddlOccupation.SelectedIndex == 0)
                //{
                //    msg = "Please select occupation type";
                //    return msg;
                //}
                //if (ddlOccuSubType.SelectedIndex == 0)
                //{
                //    msg = "Please select occupation sub type";
                //    return msg;
                //}


                //if (ddlCitizenship.SelectedIndex == 2)
                //{
                //    if (ddlIsoCountryCodeOthr.SelectedIndex == 0)
                //    {
                //        msg = "Please select ISO 3166 country code";
                //        return msg;
                //    }
                //}


                //if (chkAppDeclare1.Checked == false)
                //{
                //    msg = "Please check application declaration number 1";
                //    return msg;
                //}

                //if (chkAppDeclare2.Checked == false)
                //{
                //    msg = "Please check application declaration number 2";
                //    return msg;
                //}

                //if (txtDate.Text == "")
                //{
                //    msg = "Please enter declaration date";
                //    return msg;
                //}

                //if (txtPlace.Text == "")
                //{
                //    msg = "Please enter declaration Place";
                //    return msg;
                //}

                //if (!(chkDone.Checked))
                //{
                //    msg = "Please check KYC verification carried out identity verification checkbox";
                //    return msg;
                //}

                //if (txtDateKYCver.Text == "")
                //{
                //    msg = "Please enter KYC verification carried out date";
                //    return msg;
                //}
                //if (txtEmpName.Text == "")
                //{
                //    msg = "Please enter KYC verification carried out employee name";
                //    return msg;
                //}
                //if (txtEmpCode.Text == "")
                //{
                //    msg = "Please enter KYC verification carried out employee code";
                //    return msg;
                //}
                //if (txtEmpDesignation.Text == "")
                //{
                //    msg = "Please enter KYC verification carried out employee designation";
                //    return msg;
                //}
                //if (txtEmpBranch.Text == "")
                //{
                //    msg = "Please enter KYC verification carried out employee branch";
                //    return msg;
                //}
                if (txtInsName.Text == "")
                {
                    msg = "Please enter institution name";
                    return msg;
                }
                if (txtInsCode.Text == "")
                {
                    msg = msg = "Please enter institution code";
                    return msg;
                }

            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.IO.FileInfo oInfo = new System.IO.FileInfo(currentFile);
                string sRet = oInfo.Name;
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                String LogClassName = method.ReflectedType.Name;
            }
            return msg;
        }

        #endregion

        #region ControlPrsnValidationAccorKYC
        public string ControlPrsnValidationAccorKYC(DropDownList ddlRelType, DropDownList cboTitle, TextBox txtGivenName, TextBox txtLastName, DropDownList cboTitle2, RadioButtonList rbtFS,
            TextBox txtGivenName2, TextBox txtLastName2, DropDownList cboTitle3, TextBox txtGivenName3, TextBox txtLastName3, TextBox txtDOB, DropDownList cboGender, DropDownList ddlOccupation,
            DropDownList ddlOccuSubType, DropDownList ddlMaritalStatus, DropDownList ddlCitizenship, DropDownList ddlResStatus, DropDownList ddlIsoCountryCodeOthr, CheckBox chkTick, DropDownList ddlIsoCountryCode2,
            TextBox txtIDResTax, TextBox txtDOBRes, DropDownList ddlIsoCountry, DropDownList ddlProofIdentity, TextBox txtPassNo, TextBox txtPassExpDate, TextBox txtPassOthr, DropDownList ddlAddressType,
            CheckBox chkPerAddress, CheckBox chkAppDeclare1, DropDownList ddlProofOfAddress, TextBox txtAddressLine1, TextBox txtCity, TextBox txtPinCode, TextBox txtDateKYCver,
            TextBox txtEmpName, TextBox txtEmpCode, TextBox txtEmpDesignation, TextBox txtEmpBranch, TextBox txtInsName, TextBox txtInsCode, TextBox txtDate, TextBox txtPassExpDateAdd, TextBox txtPassNoAdd, CheckBox chkAppDeclare2, TextBox txtPlace,
            CheckBox chkSelfCerti, CheckBox chkTrueCopies, CheckBox chkNotary, DropDownList ddlState, CheckBox chkDone, TextBox txtPassOthrAdd)// CheckBox chkHigh, CheckBox chkMedium, CheckBox chkLow,
        {
            string msg = string.Empty;
            string date;
            date = DateTime.Today.ToString("dd\\-MM\\-yyyy");
            try
            {
                if (ddlRelType.SelectedIndex == 0)
                {
                    msg = "Please select type of control";
                    return msg;
                }

                if (cboTitle.SelectedIndex == 0)
                {
                    msg = "Please select prefix of name";
                    return msg;
                }

                if (txtGivenName.Text == "")
                {
                    msg = "Please enter first name";
                    return msg;
                }

                if (txtLastName.Text == "")
                {
                    msg = "Please enter last name";
                    return msg;
                }
                //    if (rbtFS.SelectedIndex == -1)
                //    {
                //        msg = "Please select father/spouse";
                //        return msg;
                //    }
                //    if (cboTitle2.SelectedIndex == 0)
                //    {
                //        msg = "Please select prefix of father/spouse name";
                //        return msg;
                //    }
                //    if (rbtFS.SelectedValue == "F")
                //    {
                //        if (rbtFS.SelectedValue == "MRS" || rbtFS.SelectedValue == "MS")
                //        {
                //            msg = "Please select valid prefix of father/spouse name";
                //            return msg;
                //        }
                //    }

                //    if (txtGivenName2.Text == "")
                //    {
                //        msg = "Please enter first name of father/spouse";
                //        return msg;
                //    }
                //    if (txtLastName2.Text == "")
                //    {
                //        msg = "Please enter last name of father/spouse";
                //        return msg;
                //    }
                //    if (cboTitle3.SelectedIndex == 0)
                //    {
                //        msg = "Please select prefix of mother name";
                //        return msg;
                //    }
                //    if (txtGivenName3.Text == "")
                //    {
                //        msg = "Please enter first name of mother";
                //        return msg;
                //    }
                //    if (txtLastName3.Text == "")
                //    {
                //        msg = "Please enter last name of mother";
                //        return msg;
                //    }
                //    if (txtDOB.Text == "")
                //    {
                //        msg = "Please enter date of birth";
                //        return msg;
                //    }
                //    if (txtDOB.Text != "")
                //    {
                //        string pattern = "^(0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-](19|20)[0-9]{2}$";
                //        Match match = Regex.Match(txtDOB.Text.ToString(), pattern);
                //        if (!match.Success)
                //        {
                //            msg = "Please check DOB date format, it must be in dd/mm/yyyy";
                //            return msg;
                //        }
                //        DateTime date1, date2;
                //        date1 = DateTime.ParseExact(date.ToString(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                //        date2 = DateTime.ParseExact(txtDOB.Text.ToString(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                //        if (date1 < date2)
                //        {
                //            msg = "You cannot select future date as DOB";
                //            return msg;
                //        }
                //    }
                //    if (cboGender.SelectedIndex == 0)
                //    {
                //        msg = "Please select gender";
                //        return msg;
                //    }

                //    if (cboTitle.SelectedIndex != 0 && cboGender.SelectedIndex != 0)
                //    {
                //        if (cboTitle.SelectedValue == "MR" && cboGender.SelectedValue == "F")
                //        {
                //            msg = "Please select valid gender according to prefix of the name";
                //            return msg;
                //        }
                //    }
                //    //added by ramesh on dated 25-05-2017

                //    if (ddlMaritalStatus.SelectedIndex == 0)
                //    {
                //        msg = "Please select marital status";
                //        return msg;
                //    }

                //    if (cboTitle.SelectedValue == "MRS" && rbtFS.SelectedValue == "F" && ddlMaritalStatus.SelectedValue == "02")
                //    {
                //        msg = "Please select valid prefix of name and Marital Status";
                //        return msg;
                //    }

                //    if (cboTitle.SelectedValue == "MRS" && ddlMaritalStatus.SelectedValue == "02")
                //    {
                //        msg = "Please select valid prefix";
                //        return msg;
                //    }
                //    if (ddlCitizenship.SelectedIndex == 0)
                //    {
                //        msg = "Please select Nationality";
                //        return msg;
                //    }

                //    if (ddlCitizenship.SelectedIndex == 2)
                //    {
                //        if (ddlIsoCountryCodeOthr.SelectedIndex == 0)
                //        {
                //            msg = "Please select ISO 3166 country code";
                //            return msg;
                //        }
                //    }

                //    if (ddlResStatus.SelectedIndex == 0)
                //    {
                //        msg = "Please select residential status";
                //        return msg;
                //    }

                //    if (ddlOccupation.SelectedIndex == 0)
                //    {
                //        msg = "Please select occupation type";
                //        return msg;
                //    }
                //    if (ddlOccuSubType.SelectedIndex == 0)
                //    {
                //        msg = "Please select occupation sub type";
                //        return msg;
                //    }


                //    //if (chkTick.Checked == true)
                //    //{
                //    if (ddlIsoCountryCode2.SelectedIndex == 0)
                //    {
                //        msg = "Please select ISO 3166 country code of jurisdiction of residence";
                //        return msg;
                //    }
                //    if (txtIDResTax.Text == "")
                //    {
                //        msg = "Please enter tax identification number or equivalent (if issued by jurisdiction)";
                //        return msg;
                //    }
                //    if (txtDOBRes.Text == "")
                //    {
                //        msg = "Please enter place/city of birth";
                //        return msg;
                //    }
                //    if (ddlIsoCountry.SelectedIndex == 0)
                //    {
                //        msg = "Please select ISO 3166 country code of birth";
                //        return msg;
                //    }


                //    if (chkAppDeclare1.Checked == false)
                //    {
                //        msg = "Please check application declaration number 1";
                //        return msg;
                //    }

                //    if (chkAppDeclare2.Checked == false)
                //    {
                //        msg = "Please check application declaration number 2";
                //        return msg;
                //    }

                //    if (txtDate.Text.Trim() == "")
                //    {
                //        msg = "Please enter declaration date";
                //        return msg;
                //    }

                //    if (txtPlace.Text == "")
                //    {
                //        msg = "Please enter declaration place";
                //        return msg;
                //    }


                //    if (chkDone.Checked == false)
                //    {
                //        msg = "Please check KYC verification carried out identity verification checkbox";
                //        return msg;
                //    }

                //    if (txtDateKYCver.Text == "")
                //    {
                //        msg = " Please enter KYC verification carried out Date";
                //        return msg;
                //    }
                //    if (txtEmpName.Text == "")
                //    {
                //        msg = " Please enter KYC verification carried out employee name";
                //        return msg;
                //    }
                //    if (txtEmpCode.Text == "")
                //    {
                //        msg = " Please enter KYC verification carried out employee code";
                //        return msg;
                //    }
                //    if (txtEmpDesignation.Text == "")
                //    {
                //        msg = " Please enter KYC verification carried out employee designation";
                //        return msg;
                //    }
                //    if (txtEmpBranch.Text == "")
                //    {
                //        msg = " Please enter KYC verification carried out employee branch";
                //        return msg;
                //    }
                //    if (txtInsName.Text == "")
                //    {
                //        msg = " Please enter institution name";
                //        return msg;
                //    }
                //    if (txtInsCode.Text == "")
                //    {
                //        msg = msg = " Please enter institution code";
                //        return msg;
                //    }

            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.IO.FileInfo oInfo = new System.IO.FileInfo(currentFile);
                string sRet = oInfo.Name;
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                String LogClassName = method.ReflectedType.Name;
            }
            return msg;
        }
        #endregion
    }
}