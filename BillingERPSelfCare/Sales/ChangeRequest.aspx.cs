using BillingERPSelfCare.Utility;
using BillingERPConn;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using BillingERPSelfCare.Session;
using MkCommunication;
using Telerik.Web.UI;

namespace BillingERPSelfCare.Sales
{
    public partial class ChangeRequest : BasePageClass
    {
        DBUtility objDBUitility = new DBUtility();
        //MkConnection objMKConnection = new MkConnection();

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            Literal pageLtl = ((Literal)PageUtility.FindControlIterative(this.Master, "ltlPageName"));
            if (!IsPostBack)
            {
                CustomerDetails();
            }
            if (pageLtl != null)
            {
                pageLtl.Text = "Change Request";
            }
        }
        #endregion

        #region Search

        private void CustomerDetails()
        {
            try
            {
                Clear();

                Hashtable htCustomerDetails = new Hashtable();
                string CustID = Session[SessionInfo.loginid].ToString();
                htCustomerDetails.Add("CustomerID", CustID);
                DataTable dtCustomerDetails = objDBUitility.GetDataByProc(htCustomerDetails, "sp_GetDetailForChangeRequestSelfCare");

                //foreach (DataRow dataRow in dtCustomerDetails.Rows)
                //{
                txtCustomerId.Text = dtCustomerDetails.Rows[0]["CustomerID"].ToString();
                decimal cust_balance = Conversion.TryCastDecimal(dtCustomerDetails.Rows[0]["Balance"].ToString());
                txtBalance.Text = cust_balance.ToString();
                txtCustomerName.Text = dtCustomerDetails.Rows[0]["CustomerName"].ToString();
                txtExistingPackage.Text = dtCustomerDetails.Rows[0]["Package"].ToString();
                txtMRC.Text = dtCustomerDetails.Rows[0]["TotalMRC"].ToString();
                txtDiscount.Text = dtCustomerDetails.Rows[0]["Discount"].ToString();
                txtNetMRC.Text = dtCustomerDetails.Rows[0]["NetMRC"].ToString();
                txtServiceCharge.Text = dtCustomerDetails.Rows[0]["ServiceCharge"].ToString();
                //}

                Hashtable htPackageDetails = new Hashtable();
                htPackageDetails.Add("CustomerID", CustID);
                DataTable dtPackageDetails = objDBUitility.GetDataByProc(htPackageDetails, "sp_GetPackageDetailForChangeRequestSelfCare");

                //foreach (DataRow dataRow in dtPackageDetails.Rows)
                //{
                lblPackageName.Text = dtPackageDetails.Rows[0]["PackageName"].ToString();
                lblAcquiredService.Text = dtPackageDetails.Rows[0]["ServiceName"].ToString();
                //}
                LoadExistingServiceGrid(CustID);

                string StatusID = dtCustomerDetails.Rows[0]["StatusID"].ToString();

                if (StatusID == "1")
                {
                    Hashtable htDate = new Hashtable();
                    htDate.Add("StatusID", 5);
                    htDate.Add("CustomerID", StatusID);
                    DataTable dtDate = objDBUitility.GetDataByProc(htDate, "sp_getEffectiveDate");
                    foreach (DataRow dr in dtDate.Rows)
                    {

                        dpEffectiveDate.SelectedDate = Conversion.TryCastDate(dr["EffectiveDate"].ToString());
                        lblEffectiveDate.Text = @"Effective date must be in next billing cycle start date."; //+ Conversion.TryCastDate(dr["MinDate"].ToString()).ToString("dd/MM/yyyy");
                    }
                }
                else if (StatusID == "2" || StatusID == "9")
                {
                    dpEffectiveDate.SelectedDate = DateTime.Now;
                    lblEffectiveDate.Text = @"Effective date will be the current date.";
                }
            }
            catch (Exception ex)
            {
                Message.Show(ex.Message.ToString());
            }
        }
        #endregion

        #region LoadDetails

        private void LoadExistingServiceGrid(string CustomerId)
        {
            try
            {
                grdServiceList.DataSource = null;
                grdServiceList.DataBind();
                Hashtable ht = new Hashtable();
                ht.Add("CustomerID", CustomerId);
                DataTable dataTable = objDBUitility.GetDataByProc(ht, "sp_GetExistingServiceForChangeRequestSelfCare");

                grdServiceList.DataSource = dataTable;
                grdServiceList.DataBind();
            }

            catch (Exception ex)
            {

                Alert.Show(ex.Message);
            }

        }

        #endregion


        #region ChangeEvent
         

        protected void chkAddRemoveServices_OnCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAddRemoveServices.Checked)
                {

                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Message.Show(ex.Message);
            }
        }

        protected void chbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                //int ServiceCharge = 0;
                //int TotalServiceCharge = 0;
                //foreach (GridDataItem item in grdServiceListNew.Items)
                //{
                //    CheckBox chkCreated = (CheckBox)item.FindControl("chbIsActive");

                //    if (chkCreated.Checked == true)
                //    {
                //        ServiceCharge = Conversion.TryCastInteger(item["ServicePrice"].Text);
                //        TotalServiceCharge += ServiceCharge;
                //    }
                //}

                //txtServiceCharge.Text = Conversion.TryCastString(TotalServiceCharge);



            }
            catch (Exception ex)
            {
                Message.Show(ex.Message);
            }

        }

        #endregion

        #region Clear
        private void Clear()
        {

        }
        #endregion 

        #region Submit
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();



            }
            catch (Exception ex)
            {
                Message.Show(ex.Message.ToString());
            }
        }
        #endregion
    }
}