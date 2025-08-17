using BillingERPConn;
using BillingERPSelfCare.DataAccess;
using BillingERPSelfCare.Session;
using BillingERPSelfCare.Utility;
using System;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using static BillingERPSelfCare.SSLCommarz;

namespace BillingERPSelfCare.UI.Client
{
    public partial class IpnValidation : System.Web.UI.Page
    {
        DBUtility objDBUitility = new DBUtility();

        protected string Store_ID;
        protected string Store_Pass;
        protected bool Store_Test_Mode;



        protected string loginId;

        protected string SSLCz_URL = "https://securepay.sslcommerz.com/";
        protected string Submit_URL = "gwprocess/v4/api.php";
        protected string Validation_URL = "validator/api/validationserverAPI.php";
        protected string Checking_URL = "validator/api/merchantTransIDvalidationAPI.php";


        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            Literal pageLtl = ((Literal)PageUtility.FindControlIterative(this.Master, "ltlPageName"));
            Label lblUser = ((Label)PageUtility.FindControlIterative(this.Master, "lblUser"));

            if (!IsPostBack)
            {
                orderValidation();
            }
            if (pageLtl != null)
            {
                pageLtl.Text = "Payment Success";
            }


            Session.Add(SessionInfo.user_name, lblUser.Text);

        }
        #endregion

        private void orderValidation()
        {
            try
            {
                if (!String.IsNullOrEmpty(Request.Form["status"]) && Request.Form["status"] == "VALID")
                {
                    string TrxID = Request.Form["tran_id"];

                    // AMOUNT and Currency FROM DB FOR THIS TRANSACTION

                    string amount = Request.Form["amount"];
                    string currency = "BDT";

                    SSLCommarz sslcz = new SSLCommarz("amberit001live", "amberit001live31461", false);

                    bool orderValidate = sslcz.OrderValidate(TrxID, amount, currency, Request);

                    Response.Write("Validation Response: " + orderValidate);

                    SSLCommerzValidatorResponse result = GetResponseData(Request);



                    string transaction_id = result.tran_id.ToString();
                    string[] txnIds = transaction_id.Split('@');
                    loginId = txnIds[0].ToString();

                    Session.Add(SessionInfo.PIN, loginId);
                    Session.Add(SessionInfo.loginid, loginId);

                    if (orderValidate == true)
                    {
                        var narration = "Collection through SSL.";
                        string mrno = result.val_id.ToString();
                        DAPop.InsertCollectionEntrySSL_IPN(result, narration, mrno, loginId);
                    }

                    // save the payment info
                    var user = GetUserIP();
                    SavePayment(result, loginId, user);
                }
                else
                {

                    Response.Write("not found");
                }
            }
            catch (Exception ex)
            {
                WriteLogFile.WriteLog("orderValidation: " + ex.Message + " " + ex.Source);
                Message.Show(ex.Message);
            }
        }

        private SSLCommerzValidatorResponse GetResponseData(HttpRequest req)
        {
            try
            {

                SSLCommarz sslcz = new SSLCommarz("amberit001live", "amberit001live31461", false);

                bool hash_verified = sslcz.ipn_hash_verify(req);

                if (hash_verified)
                {

                    string json = string.Empty;

                    string EncodedValID = HttpUtility.UrlEncode(req.Form["val_id"]);
                    string EncodedStoreID = HttpUtility.UrlEncode(sslcz.Store_ID);
                    string EncodedStorePassword = HttpUtility.UrlEncode(sslcz.Store_Pass);

                    string validate_url = sslcz.SSLCz_URL + this.Validation_URL + "?val_id=" + EncodedValID + "&store_id=" + EncodedStoreID + "&store_passwd=" + EncodedStorePassword + "&v=1&format=json";

                    WriteLogFile.WriteLog("validate_url: " + validate_url);

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(validate_url);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream resStream = response.GetResponseStream();
                    using (StreamReader reader = new StreamReader(resStream))
                    {
                        json = reader.ReadToEnd();

                        WriteLogFile.WriteLog(json); // logWrite
                    }
                    if (json != "")
                    {
                        SSLCommerzValidatorResponse resp = new JavaScriptSerializer().Deserialize<SSLCommerzValidatorResponse>(json);

                        return resp;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                WriteLogFile.WriteLog("GetResponseData" + ex.Message);
                return null;
            }

        }

        private int SavePayment(SSLCommerzValidatorResponse objPay, string customerid, string ip)
        {
            int status = 0;
            try
            {

                status = DAPop.SavePaymentLogFromSSL(objPay, customerid, ip,"IPN_Validation");
            }
            catch (Exception ex)
            {
                // WriteLogFile.WriteLog("Log exception message " + ex.Message.ToString());
                status = 0;
            }
            return status;
        }


        private bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        protected void GoBack_Click(object sender, EventArgs e)
        {

            Response.Redirect("~/UI/Client/Payment.aspx");
        }
        protected string GetUserIP()
        {
            string ipList = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipList))
            {
                return ipList.Split(',')[0];
            }

            return Request.ServerVariables["REMOTE_ADDR"];
        }
    }
}