using BillingERPConn;
using BillingERPSelfCare.Bkash;
using BillingERPSelfCare.Bkash.Models;
using BillingERPSelfCare.Session;
using BillingERPSelfCare.Utility;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Web;

namespace BillingERPSelfCare.UI.Client
{
    public partial class PaymentGateway : System.Web.UI.Page
    {
        private readonly TokenSevice _tokenService = new TokenSevice();
        private readonly BkashService _bkashService = new BkashService();
        private readonly DBUtility _db = new DBUtility();
        protected void Page_Load(object sender, EventArgs e)
        {
            var alreadyExecutePayment = 0;
            if (!IsPostBack)
            {
                string paymentIDFromUrl = Request.QueryString["paymentID"];
                string status = Request.QueryString["status"];

                if (!string.IsNullOrEmpty(paymentIDFromUrl) && status == "success" && Session[SessionInfo.Bkash_paymentID].ToString() == paymentIDFromUrl && Session[SessionInfo.agreementID] != null)
                {
                    alreadyExecutePayment = ExecutePayment(paymentIDFromUrl);
                    if (alreadyExecutePayment == 0)
                    {
                        Response.Redirect("~/UI/Client/PaymentFailure.aspx", false);
                    }
                }

            }
            var paymentOption = Session[SessionInfo.PaymentOption];
            if (paymentOption == null && alreadyExecutePayment ==0)
            {
                Response.Redirect("~/UI/Client/Payment.aspx", false);

            }
            else if(paymentOption == null && alreadyExecutePayment == 1)
            {
                Response.Redirect("~/Success.aspx", false);
            }
            else
            {
                if (paymentOption.ToString() == "SSL")
                {
                    SSLService();
                }
                else if (paymentOption.ToString() == "Bkash" && alreadyExecutePayment == 0)
                {
                    BkashService();
                }
            }
        }
        protected void SSLService()
        {
            try
            {
                DBUtility db = new DBUtility();

                var invoiceAmount = Session[SessionInfo.InvoiceAmount].ToString();
                var transactionId = Guid.NewGuid().ToString();

                transactionId = Session[SessionInfo.loginid].ToString() + "@" + transactionId;

                //   WriteLogFile.WriteLog("SEND=>" + transactionId);

                if (invoiceAmount != "")
                {

                    var storeId = "amberit001live";
                    var baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);

                    var redirectUrl = "https://securepay.sslcommerz.com/gwprocess/v3/process.php";

                    // var redirectUrl = "https://sandbox.sslcommerz.com/gwprocess/v4/api.php";



                    NameValueCollection PostData = new NameValueCollection();

                    PostData.Add("total_amount", invoiceAmount);
                    PostData.Add("tran_id", transactionId);
                    PostData.Add("success_url", baseUrl + "/Success.aspx");
                    PostData.Add("fail_url", baseUrl + "/UI/Client/PaymentFailure.aspx"); // "Fail.aspx" page needs to be created
                    PostData.Add("cancel_url", baseUrl + "/UI/Client/Payment.aspx"); // "Cancel.aspx" page needs to be created
                    PostData.Add("version", "3.00");
                    PostData.Add("cus_name", "ABC XY");
                    PostData.Add("cus_email", "abc.xyz@mail.co");
                    PostData.Add("cus_add1", "Address Line On");
                    PostData.Add("cus_add2", "Address Line Tw");
                    PostData.Add("cus_city", "City Nam");
                    PostData.Add("cus_state", "State Nam");
                    PostData.Add("cus_postcode", "Post Cod");
                    PostData.Add("cus_country", "Country");
                    PostData.Add("cus_phone", "0111111111");
                    PostData.Add("cus_fax", "0171111111");
                    PostData.Add("ship_name", "ABC XY");
                    PostData.Add("ship_add1", "Address Line On");
                    PostData.Add("ship_add2", "Address Line Tw");
                    PostData.Add("ship_city", "City Nam");
                    PostData.Add("ship_state", "State Nam");
                    PostData.Add("ship_postcode", "Post Coyd");
                    PostData.Add("ship_country", "Country");
                    PostData.Add("value_a", "ref00");
                    PostData.Add("value_b", "ref00");
                    PostData.Add("value_c", "ref00");
                    PostData.Add("value_d", "ref00");
                    PostData.Add("shipping_method", "NO");
                    PostData.Add("num_of_item", "1");
                    PostData.Add("product_name", "Demo");
                    PostData.Add("product_profile", "general");
                    PostData.Add("product_category", "Demo");


                    var storeID = "amberit001live"; //Replace with LiveID
                    var storePass = "amberit001live31461"; //Replace with LivePassword


                    var storeid_test = "amberit001live";  // sandbox
                    var store_Pass_test = "amberit001live31461";

                    SSLCommarz sslcz = new SSLCommarz("amberit001live", "amberit001live31461", false);
                    String response = sslcz.InitiateTransaction(PostData);

                    var ip = Session[SessionInfo.PublicIP].ToString();

                    Hashtable ht = new Hashtable()
                    {
                        {"CustomerId", Session[SessionInfo.loginid].ToString() },
                        {"TransactionId", transactionId },
                        {"Amount", invoiceAmount },
                        {"IpAddress", ip}

                    };

                    DataTable LogInsert = db.GetDataByProc(ht, "sp_InsertSSLPaymentLog");

                    // WriteLogFile.WriteLog("tran_id :" + transactionId + "Amount" + invoiceAmount);


                    Response.Redirect(response);
                }
                else
                {
                    Response.Redirect("~/UI/Client/Payment.aspx");
                }
            }
            catch (Exception ex)
            {
                WriteLogFile.WriteLog("PaymentGateway:" + ex.Message);
            }
        }
        protected void BkashService()
        {
            DBUtility db = new DBUtility();
            Hashtable ht = new Hashtable();
            var invoiceAmount = Session[SessionInfo.InvoiceAmount].ToString();
            TokenResponse tokenResponse = _tokenService.GrantToken();
            string agreementID = Session[SessionInfo.agreementID].ToString();
            if (agreementID == "" || agreementID == null)
            {
                Message.Show("Add Bkash Account");
            }
            else
            {
                string CallBackBaseUrl = string.Format("{0}://{1}:{2}", Request.Url.Scheme, Request.Url.Host, Request.Url.Port);
                string payerRef = Session[SessionInfo.loginid].ToString();
                PaymentCreateResponse paymentCreateResponse = _bkashService.CreatePayment(agreementID, tokenResponse.id_token, invoiceAmount, CallBackBaseUrl, payerRef);
                Session.Add(SessionInfo.Bkash_paymentID, paymentCreateResponse.paymentID);
                if (paymentCreateResponse.statusCode == "0000")
                {
                    Session.Remove(SessionInfo.PaymentOption);
                    Response.Redirect(paymentCreateResponse.bkashURL);
                }
            }
        }
        private int ExecutePayment(string paymentIDFromUrl)
        {
            Hashtable ht = new Hashtable();

            var InvoiceAmount = Conversion.TryCastInteger(Session[SessionInfo.InvoiceAmount].ToString());
            string customerid = Session[SessionInfo.loginid].ToString();
            TokenResponse tokenResponse = _tokenService.GrantToken();
            PaymentExecuteResponse paymentExecuteResponse = _bkashService.ExecutePayment(paymentIDFromUrl, tokenResponse.id_token);

            if (paymentExecuteResponse.statusCode == "0000")
            {
                ht.Add("customerID", customerid);
                ht.Add("paymentID", paymentExecuteResponse.paymentID);
                ht.Add("trxID", paymentExecuteResponse.trxID);
                ht.Add("transactionStatus", paymentExecuteResponse.transactionStatus);
                ht.Add("amount", paymentExecuteResponse.amount);
                ht.Add("paymentCreateTime", DateTime.Now);
                ht.Add("currency", paymentExecuteResponse.currency);
                ht.Add("intent", paymentExecuteResponse.intent);
                ht.Add("paymentExecuteTime", paymentExecuteResponse.paymentExecuteTime);
                ht.Add("merchantInvoiceNumber", paymentExecuteResponse.merchantInvoiceNumber);
                ht.Add("payerType", paymentExecuteResponse.payerType);
                ht.Add("agreementID", paymentExecuteResponse.agreementID);
                ht.Add("payerReference", paymentExecuteResponse.payerReference);
                ht.Add("customerMsisdn", paymentExecuteResponse.customerMsisdn);
                ht.Add("payerAccount", paymentExecuteResponse.payerAccount);
                ht.Add("statusCode", paymentExecuteResponse.statusCode);
                ht.Add("statusMessage", paymentExecuteResponse.statusMessage);
                DataTable response = _db.GetDataByProc(ht, "sp_SelfCare_bKash_InsertPaymentResponseLog");
                var status = response.Rows[0]["Feedback"].ToString();
                if (status == "Success")
                {
                    Session.Remove(SessionInfo.Bkash_paymentID);
                    Session.Remove(SessionInfo.Bkash_id_token);
                    Session.Remove(SessionInfo.agreementID);
                }
                else
                {
                    Response.Redirect("~/UI/Client/PaymentFailure.aspx", false);
                }
                return 1; // successfully payment execute
            }
            else
            {
                return 0; // payment execute fail
            }
        }

    }
}
