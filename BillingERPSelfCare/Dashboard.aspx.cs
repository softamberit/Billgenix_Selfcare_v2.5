using BillgenixTicketing.ApiIntigration;
using BillingERPConn;
using BillingERPSelfCare.Models;
using BillingERPSelfCare.Session;
using BillingERPSelfCare.Utility;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Data;
using System.Text.Json.Serialization;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI;


namespace BillingERPSelfCare
{
    public partial class Dashboard : BasePageClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session[SessionInfo.loginid] == null)
                Response.Redirect("~/LogOut.aspx");

            if (!IsPostBack)
            {
                ShowCustBalance();
                //  LoadGraph();
                var IsOnlineCust = Session[SessionInfo.IsOnlineCust];
                if (IsOnlineCust != null && IsOnlineCust.ToString() == "True")
                {
                    DivHyperLink.Visible = true;
                }
                else
                {
                    DivHyperLink.Visible = false;
                }
                hdnCustomerId.Value = Session[SessionInfo.loginid].ToString();
            }
        }

        //public void LoadGraph()
        //{
        //    BillgenixRadiusClient _client = new BillgenixRadiusClient();

        //    var image = _client.GetMrtgGraphBase64(Session[SessionInfo.loginid].ToString());
        //    if (!string.IsNullOrEmpty(image))
        //    {
        //        imgDailyGraph.Src = string.Format("data:image/gif;base64,{0}", image);
        //    }
        //}

        public void ShowCustBalance()
        {
            try
            {
                DBUtility objDB = new DBUtility();

                Hashtable ht = new Hashtable();
                ht.Add("CustomerID", (Session[SessionInfo.loginid].ToString()));

                DataTable dt = objDB.GetDataByProc(ht, "sp_getdashboardSelfCare");
                foreach (DataRow dr in dt.Rows)
                {
                    txtInvoice.InnerText = dr["DEBIT"].ToString();
                    txtPayment.InnerText = dr["CREDIT"].ToString();
                    txtTotalInvoice.InnerText = dr["DEBITSUM"].ToString();
                    txtTotalAmount.InnerText = dr["CREDITSUM"].ToString();
                    //txtBalance.InnerText      = dr["BALANCE"].ToString();
                    txtCycleDate.InnerText = dr["NEXTDATE"].ToString();
                    txtPackage.InnerText = dr["PACKAGE"].ToString();
                    txtPendingRequest.InnerText = dr["PENDINGREQ"].ToString();
                    txtMRC.InnerText = dr["MRC"].ToString();
                }

                var cust_balance = Conversion.TryCastDecimal(dt.Rows[0]["Balance"].ToString());
                txtBalance.InnerText = ((cust_balance * (-1) + Conversion.TryCastDecimal(dt.Rows[0]["TEMPMRC"].ToString()))).ToString();
            }
            catch (Exception ex)
            {

                Message.Show(ex.Message);
            }
        }


        protected void btnPayment_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("UI/Client/Payment.aspx");
            }
            catch (Exception ex)
            {

                Message.Show(ex.Message);
            }
        }


      

    }
}