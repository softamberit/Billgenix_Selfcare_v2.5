using BillingERPConn;
using BillingERPSelfCare.Session;
using BillingERPSelfCare.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace BillingERPSelfCare.Sales
{
    public partial class PackageUpgradation : BasePageClass
    {
        DBUtility dbConn = new DBUtility();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Literal pageLtl = ((Literal)PageUtility.FindControlIterative(this.Master, "ltlPageName"));
                if (!IsPostBack)
                {
                    txtCustomerID.Text = Session[SessionInfo.loginid].ToString();
                    Messages.Visible = false;
                    GetCustomerInfo();
                }
                if (pageLtl != null)
                {
                    pageLtl.Text = "Package Upgradation";
                }
            }
            catch (Exception ex)
            {
                Message.Show(ex.Message);
            }
        }

        protected void cmbNewPackage_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("CustomerID", txtCustomerID.Text.Trim());
                ht.Add("status", 1);

                DataTable dt = dbConn.GetDataByProc(ht, "sp_getPackageList"); // Using existing procedure

                foreach (DataRow dataRow in dt.Rows)
                {
                    RadComboBoxItem item = new RadComboBoxItem();
                    item.Text = dataRow["BandWidth"].ToString();
                    item.Value = dataRow["BID"].ToString();
                    cmbNewPackage.Items.Add(item);
                    item.DataBind();
                }
            }
            catch (Exception ex)
            {
                Message.Show(ex.Message);
            }
        }

        protected void cmbNewPackage_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                txtNewMRC.Text = string.Empty;
                txtMinReqBal.Text = string.Empty;

                if (dpEffectiveDate.SelectedDate != null)
                {
                    if (cmbNewPackage.SelectedValue != "")
                    {
                        Hashtable ht = new Hashtable();
                        ht.Add("BID", cmbNewPackage.SelectedValue);
                        DataTable dt = dbConn.GetDataBySQLString("select convert(decimal(18, 0), TotalMRC) as TotalMRC from PackageDetail where BID = " + cmbNewPackage.SelectedValue + " and IsActive = 1");

                        txtNewMRC.Text = dt.Rows[0]["TotalMRC"].ToString();
                        if (decimal.Parse(txtNewMRC.Text) - decimal.Parse(txtCummulativeBalalnce.Text) > 0)
                            txtMinReqBal.Text = (decimal.Parse(txtNewMRC.Text) - decimal.Parse(txtCummulativeBalalnce.Text)).ToString("N0");
                        else
                            txtMinReqBal.Text = "0";
                    }
                }
                else
                {
                    cmbNewPackage.ClearSelection();
                    cmbNewPackage.Text = string.Empty;
                    Message.Show("Please select a effective date first.");
                }
            }
            catch (Exception ex)
            {
                Message.Show(ex.Message);
            }
        }

        protected void GetCustomerInfo()
        {
            try
            {
                if (txtCustomerID.Text != "")
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("CustomerID", txtCustomerID.Text.Trim());
                    ht.Add("IsSetEffectiveDate", 0);

                    DataTable dt = dbConn.GetDataByProc(ht, "sp_getInfoForPackageUp");

                    if (dt.Rows[0]["Feedback"].ToString() != "Found")
                    {
                        Message.Show(dt.Rows[0]["Feedback"].ToString());
                        dpEffectiveDate.Enabled = false;
                        cmbNewPackage.Enabled = false;
                    }
                    else
                    {
                        Messages.Visible = true;
                        txtPresentBal.Text = dt.Rows[0]["PresentBalance"].ToString();
                        txtCustomerName.Text = dt.Rows[0]["CustomerName"].ToString();
                        txtExistingPackage.Text = dt.Rows[0]["PackageName"].ToString();
                        txtMRC.Text = dt.Rows[0]["TotalMRC"].ToString();
                        txtDiscount.Text = dt.Rows[0]["Discount"].ToString();
                        txtNetMRC.Text = dt.Rows[0]["NetMRC"].ToString();

                        lblMessageOne.Text = dt.Rows[0]["MessageOne"].ToString();
                        lblMessageTwo.Text = dt.Rows[0]["MessageTwo"].ToString();

                        dpEffectiveDate.MinDate = DateTime.Parse(dt.Rows[0]["StartDate"].ToString());
                        dpEffectiveDate.MaxDate = DateTime.Parse(dt.Rows[0]["EndDate"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Message.Show(ex.Message);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!chkDisclaimer.Checked)
                {
                    Message.Show("Please accept the disclaimer.");
                }
                else
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("CustomerID", txtCustomerID.Text.Trim());
                    ht.Add("EffectiveDate", dpEffectiveDate.SelectedDate);
                    ht.Add("NewPackageID", cmbNewPackage.SelectedValue.Trim());
                    ht.Add("CreatedBy", Conversion.TryCastInteger(Session[SessionInfo.loginid].ToString()));
                    ht.Add("RequestSource", "SELF_CARE");
                    ht.Add("RequestNotes", txtNotes.Text.Trim());

                    DataTable dt = dbConn.GetDataByProc(ht, "sp_raiseUpgradationRequest");

                    if (dt.Rows[0]["Feedback"].ToString() == "Success")
                    {
                        Message.Show("Request submission successfully completed.");
                        ClearOnDateChanged();
                        dpEffectiveDate.Clear();
                        chkDisclaimer.Checked = false;
                        txtNotes.Text = string.Empty;
                    }
                    else
                    {
                        Message.Show(dt.Rows[0]["Feedback"].ToString());
                        ClearOnDateChanged();
                        dpEffectiveDate.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                Message.Show(ex.Message);
            }
        }

        //private void Clear()
        //{
        //    Messages.Visible = false;
        //    dpEffectiveDate.Enabled = false;
        //    dpEffectiveDate.Clear();
        //    txtPresentBal.Text = string.Empty;
        //    txtCustomerName.Text = string.Empty;
        //    txtExistingPackage.Text = string.Empty;
        //    txtMRC.Text = string.Empty;
        //    txtDiscount.Text = string.Empty;
        //    txtNetMRC.Text = string.Empty;
        //    txtBalAfterAdj.Text = string.Empty;
        //    cmbNewPackage.ClearSelection();
        //    cmbNewPackage.Text = string.Empty;
        //    txtNewMRC.Text = string.Empty;
        //    txtMinReqBal.Text = string.Empty;
        //    txtCummulativeBalalnce.Text = string.Empty;
        //    chkDisclaimer.Checked = false;
        //    txtNotes.Text = string.Empty;
        //}

        private void ClearOnDateChanged()
        {
            txtBalAfterAdj.Text = string.Empty;
            cmbNewPackage.ClearSelection();
            cmbNewPackage.Text = string.Empty;
            txtNewMRC.Text = string.Empty;
            txtCummulativeBalalnce.Text = string.Empty;
            txtMinReqBal.Text = string.Empty;
        }

        protected void dpEffectiveDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            try
            {
                ClearOnDateChanged();
                if (dpEffectiveDate.SelectedDate != null)
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("CustomerID", txtCustomerID.Text.Trim());
                    ht.Add("IsSetEffectiveDate", 1);
                    ht.Add("EffectiveDate", dpEffectiveDate.SelectedDate);

                    DataTable dt = dbConn.GetDataByProc(ht, "sp_getInfoForPackageUp");

                    if (dt.Rows[0]["Feedback"].ToString() != "Success")
                    {
                        dpEffectiveDate.Clear();
                        Message.Show(dt.Rows[0]["Feedback"].ToString());
                    }
                    else
                    {
                        txtBalAfterAdj.Text = dt.Rows[0]["BalAfterAdj"].ToString();
                        txtCummulativeBalalnce.Text = dt.Rows[0]["CummulativeBal"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Message.Show(ex.Message.ToString());
            }
        }
    }
}