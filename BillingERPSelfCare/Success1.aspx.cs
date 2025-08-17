using BillingERPSelfCare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace BillingERPSelfCare
{

    public partial class Success1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.Form["status"]) && Request.Form["status"] == "VALID")
            {
                string TrxID = Request.Form["tran_id"];
                // AMOUNT and Currency FROM DB FOR THIS TRANSACTION

                string amount = "15";
                string currency = "BDT";

                SSLCommarz sslcz = new SSLCommarz("amberit001live", "amberit001live31461", false);

                bool orderValidate = sslcz.OrderValidate(TrxID, amount, currency, Request);

                Response.Write("Validation Response: " + orderValidate);



                if (orderValidate == true)
                {
                    //divSuccess.Visible = true;
                    //divWarning.Visible = false;

                    //lblHeading.Text = "Thanking You! ";
                    //var narration = "Collection through SSL.";
                    //lblStatus.Text = narration;

                    //string mrno = result.val_id.ToString();

                    //DAPop.InsertCollectionEntrySSL(result, narration, mrno, loginId);
                }
            }
            else
            {
                Response.Write("not found");
            }
        }




    }
}
