using KMI.FRMWRK.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMI.FRMWRK.Web
{
    public partial class HighCharts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetDashBoardDtls();
            Getmonthcount();
        }

        public void GetDashBoardDtls()
        {
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                //DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                System.Collections.Hashtable ht = new System.Collections.Hashtable();
                ht.Add("@TabFlag", "Legal");
                ds = dataAccessLayer.GetDataSet("Prc_GetDashBoardDtls_for_new", ht);
                if (ds.Tables.Count > 0)
                {
                    int NewCases = int.Parse(ds.Tables[0].Rows[0]["NewCases"].ToString());
                    int ZipGenerated = int.Parse(ds.Tables[0].Rows[0]["ZipGenerated"].ToString());
                    int Accepted = int.Parse(ds.Tables[0].Rows[0]["Accepted"].ToString());
                    int NewKYCGenerated = int.Parse(ds.Tables[0].Rows[0]["NewKYCGenerated"].ToString());
                    int ExistingKYCLinked = int.Parse(ds.Tables[0].Rows[0]["ExistingKYCLinked"].ToString());
                    int ErrorFound = int.Parse(ds.Tables[0].Rows[0]["ErrorFound"].ToString());
                    int ProbableMatch = int.Parse(ds.Tables[0].Rows[0]["ProbableMatch"].ToString());
                    int InsufficientBalance = int.Parse(ds.Tables[0].Rows[0]["InsufficientBalance"].ToString());
                    int Rejected = int.Parse(ds.Tables[0].Rows[0]["Rejected"].ToString());

                    // added by babita on 10 oct 2023 for % count 
                    string Pnew = ds.Tables[0].Rows[0]["perNewCases"].ToString();
                    string PZipGenerated = ds.Tables[0].Rows[0]["perZipGenerated"].ToString();
                    string PAccepted = ds.Tables[0].Rows[0]["perAccepted"].ToString();
                    string PNewKYCGenerated = ds.Tables[0].Rows[0]["perNewKYCGenerated"].ToString();
                    string PExistingKYCLinked = ds.Tables[0].Rows[0]["perExistingKYCLinked"].ToString();
                    string PErrorFound = ds.Tables[0].Rows[0]["perErrorFound"].ToString();
                    string PProbableMatch = ds.Tables[0].Rows[0]["perProbableMatch"].ToString();
                    string PInsufficientBalance = ds.Tables[0].Rows[0]["perInsufficientBalance"].ToString();
                    string PRejected = ds.Tables[0].Rows[0]["perRejected"].ToString();

                    SpnNewPendingFI.InnerHtml = NewCases.ToString("N0"); // to get commo in count 
                    SpanDraftUplCersai.InnerHtml = ZipGenerated.ToString("N0");
                    SpanAcceptedCersai.InnerHtml = Accepted.ToString("N0");
                    SpanInsufiBal.InnerHtml = NewKYCGenerated.ToString("N0");
                    SpanCompletedmatch.InnerHtml = ExistingKYCLinked.ToString("N0");
                    SpanProbableMatch.InnerHtml = ErrorFound.ToString("N0");
                    SpanRejectedFirTATLapse.InnerHtml = ProbableMatch.ToString("N0");
                    SpanIDNConfirmISSUER.InnerHtml = InsufficientBalance.ToString("N0");
                    SpanKYCGenertedCersai.InnerHtml = Rejected.ToString("N0");
                    SpnBatchDT.InnerHtml = ds.Tables[0].Rows[0]["date"].ToString();

                    newper.InnerHtml = Pnew.ToString();
                    spanzipper.InnerHtml = PZipGenerated.ToString();
                    Acceptedper.InnerHtml = PAccepted.ToString();
                    newkycper.InnerHtml = PNewKYCGenerated.ToString();
                    existingper.InnerHtml = PExistingKYCLinked.ToString();
                    errorper.InnerHtml = PErrorFound.ToString();
                    probableper.InnerHtml = PProbableMatch.ToString();
                    balanceper.InnerHtml = PInsufficientBalance.ToString();
                    rejectper.InnerHtml = PRejected.ToString();

                }

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
            }
        }
        public void Getmonthcount()
        {
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                //DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                System.Collections.Hashtable ht = new System.Collections.Hashtable();
                ht.Add("@TabmonthFlag", "monthdata");
                ds = dataAccessLayer.GetDataSet("Prc_Getdashbordmonthcount", ht);
                if (ds.Tables.Count > 0)
                {
                    
                    int jan = int.Parse(ds.Tables[0].Rows[0]["January"].ToString());
                    int feb = int.Parse(ds.Tables[0].Rows[0]["February"].ToString());
                    int mar = int.Parse(ds.Tables[0].Rows[0]["March"].ToString());
                    int apr = int.Parse(ds.Tables[0].Rows[0]["April"].ToString());
                    int may = int.Parse(ds.Tables[0].Rows[0]["May"].ToString());
                    int jun = int.Parse(ds.Tables[0].Rows[0]["June"].ToString());
                    int jul = int.Parse(ds.Tables[0].Rows[0]["July"].ToString());
                    int aug = int.Parse(ds.Tables[0].Rows[0]["August"].ToString());
                    int sep = int.Parse(ds.Tables[0].Rows[0]["September"].ToString());
                    int oct = int.Parse(ds.Tables[0].Rows[0]["October"].ToString());
                    int nov = int.Parse(ds.Tables[0].Rows[0]["November"].ToString());
                    int dec = int.Parse(ds.Tables[0].Rows[0]["December"].ToString());
                    int total = int.Parse(ds.Tables[0].Rows[0]["totalcount"].ToString());
                    spanjancount.InnerHtml = jan.ToString("N0");
                    spanfebcount.InnerHtml = feb.ToString("N0");
                    spanmarcount.InnerHtml = mar.ToString("N0");
                    spanaprcount.InnerHtml = apr.ToString("N0");
                    Spspanmaycoun.InnerHtml = may.ToString("N0");
                    spanJunecount.InnerHtml = jun.ToString("N0");
                    spanjulycount.InnerHtml = jul.ToString("N0");
                    spanaugcount.InnerHtml = aug.ToString("N0");
                    spanseptcount.InnerHtml = sep.ToString("N0");
                    spanoctcount.InnerHtml = oct.ToString("N0");
                    spannovcount.InnerHtml = nov.ToString("N0");
                    spandeccount.InnerHtml = dec.ToString("N0");
                    totalcount.InnerHtml = total.ToString("N0");

                }

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
            }
        }
    }
}