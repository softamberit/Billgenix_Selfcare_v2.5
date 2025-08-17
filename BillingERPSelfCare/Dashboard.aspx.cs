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


        [WebMethod]
        public static void GetTrafficData(string cid)
        {
            var traffic = new CustomerTraffic();

            if (string.IsNullOrWhiteSpace(cid) || cid.Length > 10)
            {
                return; // return empty object if invalid
            }

            Thread.Sleep(2000);

            // Your code here
            BillgenixRadiusClient radiusClient = new BillgenixRadiusClient();

            HttpContext context = HttpContext.Current;
            context.Response.AppendHeader("Connection", "keep-alive");
            context.Response.ContentType = "application/json";
            context.Response.BufferOutput = false; // disable buffering
            context.Response.Buffer = false;
            context.Response.CacheControl = "no-cache";
            context.Response.Flush();

            for (int i = 0; i < 10; i++)
            {
                traffic = radiusClient.GetTrafficData(cid);
                var respone = JsonConvert.SerializeObject(traffic);
                context.Response.Write(respone);
                context.Response.Flush();
                Thread.Sleep(1000);
            }



        }

    }
}