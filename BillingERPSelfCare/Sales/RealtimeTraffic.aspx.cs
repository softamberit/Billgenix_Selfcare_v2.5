using BillgenixTicketing.ApiIntigration;
using BillgenixTicketing.Models;
using BillingERPConn;
using BillingERPSelfCare.Models;
using BillingERPSelfCare.Session;
using BillingERPSelfCare.Utility;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Threading;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace BillingERPSelfCare.Sales
{
    public partial class RealtimeTraffic : BasePageClass
    {
        DBUtility objDBUitility = new DBUtility();
        //MkConnection objMKConnection = new MkConnection();

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            Literal pageLtl = ((Literal)PageUtility.FindControlIterative(this.Master, "ltlPageName"));
            if (!IsPostBack)
            {
                hdnCustomerId.Value = Session[SessionInfo.loginid].ToString();
                hdnHubUrl.Value=ConfigurationManager.AppSettings["baseUrlSignalrHub"].ToString();
            }
            if (pageLtl != null)
            {
                pageLtl.Text = "LIVE TRAFFIC";
            }
        }
        #endregion


        [WebMethod]
        public static string RequestTrafficData(string cid, string connectionId)
        {
            //var traffic = new CustomerTraffic();

            if (string.IsNullOrWhiteSpace(cid) || cid.Length > 10 || string.IsNullOrWhiteSpace(connectionId))
            {
                return ""; // return empty object if invalid
            }

            Thread.Sleep(1000);

            DBUtility objDB = new DBUtility();
            Hashtable ht = new Hashtable();
            ht.Add("CustomerID", cid);
            ht.Add("ConnectionId", connectionId);
            DataTable dt = objDB.GetDataByProc(ht, "sp_RequestTrafficData");
            ht.Clear();
             
            return dt.Rows[0]["Feeadback"].ToString();


        }

    }
}