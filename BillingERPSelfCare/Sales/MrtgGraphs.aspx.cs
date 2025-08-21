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
    public partial class MrtgGraphs : BasePageClass
    {
        DBUtility objDBUitility = new DBUtility();
        //MkConnection objMKConnection = new MkConnection();
        BillgenixRadiusClient Client = new BillgenixRadiusClient();

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            Literal pageLtl = ((Literal)PageUtility.FindControlIterative(this.Master, "ltlPageName"));
            if (!IsPostBack)
            {
                hdnCustomerId.Value = Session[SessionInfo.loginid].ToString();
                LoadGraph();
            }
            if (pageLtl != null)
            {
                pageLtl.Text = "MRTG GRAPH";
            }
        }

        private void LoadGraph()
        {
            var cid = Session[SessionInfo.loginid].ToString();
            var imageDaily = Client.GetMrtgGraphBase64(cid, "daily");
            if (!string.IsNullOrEmpty(imageDaily))
            {
                imgDailyGraph.Src = string.Format("data:image/gif;base64,{0}", imageDaily);
            }
            var imageWeekly = Client.GetMrtgGraphBase64(cid, "weekly");
            if (!string.IsNullOrEmpty(imageWeekly))
            {
                imgWeekly.Src = string.Format("data:image/gif;base64,{0}", imageWeekly);
            }
            var imageMonthly = Client.GetMrtgGraphBase64(cid, "monthly");
            if (!string.IsNullOrEmpty(imageMonthly))
            {
                imgMonthly.Src = string.Format("data:image/gif;base64,{0}", imageMonthly);
            }
            var imageYearly = Client.GetMrtgGraphBase64(cid, "yearly");
            if (!string.IsNullOrEmpty(imageYearly))
            {
                imgYearly.Src = string.Format("data:image/gif;base64,{0}", imageYearly);
            }
        }
        #endregion



    }
}