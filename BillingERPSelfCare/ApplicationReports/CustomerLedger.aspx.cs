using BillingERPConn;
using BillingERPSelfCare.Session;
using BillingERPSelfCare.Utility;
using ReportBilling;
using System;
using System.Web.UI.WebControls;

namespace BillingERPSelfCare.ApplicationReports
{
    public partial class CustomerLedger : System.Web.UI.Page
    {
        DBUtility objDBUitility = new DBUtility();

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            Literal pageLtl = ((Literal)PageUtility.FindControlIterative(this.Master, "ltlPageName"));
            



            if (!IsPostBack)
            {
                ReportViewer1.Visible = false;
        
            }

            if (pageLtl != null)
            {
                pageLtl.Text = "Customer Ledger";
            }
        }

      
        #endregion

        #region Show Report
        private void LoadReport()
        {
            try
            {
                

                ReportViewer1.Report = new rptCustomerLedger();
                (ReportViewer1.Report as rptCustomerLedger).CustomerID = (Session[SessionInfo.loginid].ToString());

                

                ReportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                Message.Show(ex.Message.ToString());
            }
        }
        #endregion

        protected void btn_Click(object sender, EventArgs e)
        {
            ReportViewer1.Visible = true;

            LoadReport();
        }
    }
}