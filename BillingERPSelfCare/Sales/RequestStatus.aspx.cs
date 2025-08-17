using BillingERPConn;
using BillingERPSelfCare.Session;
using BillingERPSelfCare.Utility;
using System;
using System.Collections;
using System.Data;
using System.Web.UI.WebControls;

namespace BillingERPSelfCare.Sales
{
    public partial class RequestStatus : BasePageClass
    {
        DBUtility db = new DBUtility();

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            Literal pageLtl = ((Literal)PageUtility.FindControlIterative(this.Master, "ltlPageName"));
            if (pageLtl != null)
            {
                pageLtl.Text = "Request Status";
            }

            if (!IsPostBack)
            {
               
                LoadRequestList();
            }

        }
        #endregion


        private void LoadRequestList()
        {
            try
            {

                Hashtable ht = new Hashtable();
                ht.Add("CustomerID", (Session[SessionInfo.loginid].ToString()));
                DataTable dataTableDetails = db.GetDataByProc(ht, "sp_getStatusList");
                grdRequestList.DataSource = dataTableDetails;
                grdRequestList.DataBind();
                grdRequestList.MasterTableView.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
                grdRequestList.MasterTableView.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            }
            catch (Exception ex)
            {
                Message.Show(ex.Message);
            }

        }
    }
}