using BillingERPConn;
using BillingERPSelfCare.Bkash.Models;
using BillingERPSelfCare.Session;
using BillingERPSelfCare.Utility;
using BillingERPSelfCare.UI.Client;
using System;
using System.Collections;
using System.Data;
using System.Web.UI.WebControls;

namespace BillingERPSelfCare.Bkash
{
    public partial class BKashAccountAdd : System.Web.UI.Page
    {
        private readonly TokenSevice _tokenService = new TokenSevice();
        private readonly BkashService _bkashService = new BkashService();
        private readonly DBUtility _db = new DBUtility();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string paymentIDFromUrl = Request.QueryString["paymentID"];
                string status = Request.QueryString["status"];

                if (!string.IsNullOrEmpty(paymentIDFromUrl) && status == "success" && Session[SessionInfo.Bkash_paymentID].ToString() == paymentIDFromUrl)
                {
                    ExecuteAgreement(paymentIDFromUrl);
                }
                BindBkashAccounts();
            }
        }
        protected void btnbKashAdd_OnClick(object sender, EventArgs e)
        {
            Hashtable ht = new Hashtable();
            var InvoiceAmount =Session[SessionInfo.InvoiceAmount].ToString();
            TokenResponse tokenResponse = _tokenService.GrantToken();
            string baseUrl = string.Format("{0}://{1}:{2}",Request.Url.Scheme, Request.Url.Host, Request.Url.Port);
            string payerRef = Session[SessionInfo.loginid].ToString();
            AgreementCreateResponse agreementCreateResponse = _bkashService.CreateAgreement(tokenResponse.id_token, InvoiceAmount, baseUrl, payerRef);
            Session.Add(SessionInfo.Bkash_paymentID, agreementCreateResponse.paymentID);
            if (agreementCreateResponse.statusCode == "0000")//Successful
            {
                Response.Redirect(agreementCreateResponse.bkashURL);
            }
            BindBkashAccounts();
        }
        private void ExecuteAgreement(string paymentIDFromUrl)
        {
            Hashtable ht = new Hashtable();

            var InvoiceAmount = Conversion.TryCastInteger(Session[SessionInfo.InvoiceAmount].ToString());
            string customerid = Session[SessionInfo.loginid].ToString();
            TokenResponse tokenResponse = _tokenService.GrantToken();
            AgreementExecuteResponse agreementExecuteResponse = _bkashService.ExecuteAgreement(paymentIDFromUrl, tokenResponse.id_token);

            if (agreementExecuteResponse.statusCode == "0000")
            {
                ht.Add("customerID", customerid);
                ht.Add("paymentID", agreementExecuteResponse.paymentID);
                ht.Add("agreementID", agreementExecuteResponse.agreementID);
                ht.Add("agreementStatus", agreementExecuteResponse.agreementStatus);
                ht.Add("agreementCreateTime", DateTime.Now);
                ht.Add("agreementExecuteTime", agreementExecuteResponse.agreementExecuteTime);
                ht.Add("payerReference", agreementExecuteResponse.payerReference);
                ht.Add("customerMsisdn", agreementExecuteResponse.customerMsisdn);
                ht.Add("payerAccount", agreementExecuteResponse.payerAccount);
                ht.Add("payerType", agreementExecuteResponse.payerType);
                ht.Add("statusCode", agreementExecuteResponse.statusCode);
                ht.Add("statusMessage", agreementExecuteResponse.statusMessage);
                DataTable response = _db.GetDataByProc(ht, "sp_SelfCare_bKash_InsertAgreementResponseLog");
                var status = response.Rows[0]["Feedback"].ToString();
                if (status == "Success")
                {
                    Response.Redirect("~/Bkash/BKashAccountAdd.aspx", false);
                }
                else
                {
                    Response.Redirect("~/UI/Client/PaymentFailure.aspx", false);
                }
            }
            
        }
        private void BindBkashAccounts()
        {
            Hashtable ht = new Hashtable();
            string customerid = Session[SessionInfo.loginid].ToString();
            ht.Add("customerID", customerid);
            DataTable res = _db.GetDataByProc(ht, "sp_SelfCare_bKash_GetAgreement_by_customerId");
                rptBkashAccounts.DataSource = res;
                rptBkashAccounts.DataBind();
        }
        protected void btnPay_OnClick(object sender, EventArgs e)
        {
            string agreementID = ((LinkButton)sender).CommandArgument;
            TokenResponse tokenResponse = _tokenService.GrantToken();
            AgreementStatusResponse agreementStatus = _bkashService.AgreementStatus(agreementID, tokenResponse.id_token);
            if(agreementStatus.statusCode == "0000")
            {
                Session.Add(SessionInfo.agreementID, agreementID);
                Session.Remove(SessionInfo.Bkash_paymentID);
                Response.Redirect("~/UI/Client/PaymentGateway.aspx", false);
            }
            else
            {
                Response.Redirect("~/UI/Client/PaymentFailure.aspx", false);
            }
            
        }

        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            var InvoiceAmount = Session[SessionInfo.InvoiceAmount].ToString();
            string customerid = Session[SessionInfo.loginid].ToString();
            TokenResponse tokenResponse = _tokenService.GrantToken();
            string agreementID = ((LinkButton)sender).CommandArgument;
            AgreementCancelResponse agreementCancelResponse = _bkashService.CancelAgreement(agreementID, tokenResponse.id_token);
            if (agreementCancelResponse.statusCode=="0000")
            {
                Hashtable ht = new Hashtable();
                ht.Add("agreementID", agreementID);
                ht.Add("customerID", customerid);
                DataTable response = _db.GetDataByProc(ht, "sp_SelfCare_bKash_DeleteAgreement");
                var status = response.Rows[0]["Feedback"].ToString();
                if (status == "Success")
                {
                    Message.Show("Sucessfully Deleted");
                }
                else
                {
                    Response.Redirect("~/UI/Client/PaymentFailure.aspx", false);
                }
            }
            BindBkashAccounts();

        }
    }
}