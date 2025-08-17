using BillingERPSelfCare.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BillingERPSelfCare.SSLCommerz
{
    public partial class PaymentGateway : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            var invoiceId = Session[SessionInfo.InvoiceId];
            var invoiceAmount = Session[SessionInfo.InvoiceAmount].ToString();
            var transactionId = Guid.NewGuid().ToString();
            transactionId = Session[SessionInfo.loginid].ToString() + "#" + transactionId;
            WriteLogFile.WriteLog("SEND=>" + transactionId);

            if (invoiceAmount != "")
            {
                if (invoiceId != null)
                {
                    invoiceId = invoiceId.ToString();
                }
              
                var storeId = "amberit001live";
                var baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);
                var redirectUrl = "https://securepay.sslcommerz.com/gwprocess/v3/process.php";
                var formString =
                    "<form id='payment_gw' name='payment_gw' method='POST' action='" + redirectUrl + "'>" +
                    "<input type='hidden' name='total_amount' value='" + invoiceAmount + "' />" +
                    "<input type='hidden' name='store_id' value='" + storeId + "' />" +
                    "<input type='hidden' name='tran_id' value='" + transactionId + "' />" +
                    "<input type='hidden' name='success_url' value='" + baseUrl + "/SSLCommerz/PaymentSuccess.aspx' />" +
                    "<input type='hidden' name='fail_url'  value='" + baseUrl + "/SSLCommerz/PaymentFailure.aspx' />" +
                    "<input type='hidden' name='cancel_url'  value='" + baseUrl + "/SSLCommerz/Payment.aspx' />" +
                    "<input type='hidden' name='version' value='3.00' /></form>" +
                    "<script type='text/javascript'>document.getElementById('payment_gw').submit();</script>";

            
                Response.Write(formString);
            }
            else
            {
                Response.Redirect("~/SSLCommerz/Payment.aspx");
            }

        }
    }
}