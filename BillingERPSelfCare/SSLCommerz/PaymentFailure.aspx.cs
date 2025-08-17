﻿using BillingERPSelfCare.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BillingERPSelfCare.SSLCommerz
{
    public partial class PaymentFailure : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Remove(SessionInfo.InvoiceId);
            Session.Remove(SessionInfo.InvoiceAmount);
        }

        protected void GoBack_Click(object sender, EventArgs e)
        {

            Response.Redirect("~/SSLCommerz/Payment.aspx");


        }
    }
}