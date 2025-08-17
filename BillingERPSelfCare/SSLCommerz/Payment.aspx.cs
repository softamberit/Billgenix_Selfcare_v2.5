using BillingERPSelfCare.Session;
using BillingERPSelfCare.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BillingERPSelfCare.SSLCommerz
{
    public partial class Payment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[SessionInfo.loginid] == null)
            {
                HttpContext.Current.Response.Redirect("~/SignIn.aspx");
            }
        }

        protected void Pay_Click(object sender, EventArgs e)
        {
            var invoiceId = txtInvoiceNo.Text.Trim().ToString();
            var invoiceAmount = txtInvoiceAmount.Text.Trim().ToString();

            if (invoiceAmount != "")
            {
                if (invoiceId != "")
                {
                    Session.Add(SessionInfo.InvoiceId, invoiceId);

                }
                Session.Add(SessionInfo.InvoiceAmount, invoiceAmount);
                //string ipaddress = Session[SessionInfo.PublicIP].ToString();
                string customerid = Session[SessionInfo.loginid].ToString();
               // WriteLogFile.WriteLog("SEND=> CustomerID=" + customerid + ":: Collection Amount= " + invoiceAmount + ":: IP=" + ipaddress + "  ");

                Response.Redirect("~/SSLCommerz/PaymentGateway.aspx");

            }
            else
            {
                Message.Show("Please Provide correct invoice amount!"); 
            }


        }


    }
}