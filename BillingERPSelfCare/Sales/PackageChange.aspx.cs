using BillingERPSelfCare.BusinessEntity;
using BillingERPSelfCare.DataAccess;
using BillingERPSelfCare.Session;
using BillingERPSelfCare.Utility;
using BillingERPConn;
using System;
using System.Collections;
using System.Data;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace BillingERPSelfCare.Sales
{
    public partial class PackageChange : BasePageClass
    {

        DBUtility objDBUitility = new DBUtility();
        //BOCustomer boItem = new BOCustomer();
        //DACustomer boItemdata = new DACustomer();

        protected void Page_Load(object sender, EventArgs e)
        {
            dpEffectiveDate.MinDate = DateTime.Today.Date;
            Literal pageLtl = ((Literal)PageUtility.FindControlIterative(this.Master, "ltlPageName"));
            if (!IsPostBack)
            {
                if (radiType.SelectedIndex == -1)
                {
                    radiType.SelectedIndex = 0;
                    dpEffectiveDate.Enabled = true;
                }

                InfoLoad();
            }

            if (pageLtl != null)
            {
                pageLtl.Text = "Package Change Request";
            }
        }

        protected void cmbPackage_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            try
            {
                //DataTable dataTable = objDBUitility.GetDataBySQLString("SELECT BID,BandWidth FROM PackageMaster WHERE IsActive =1");
                int status = 0;
                if (radiType.Text.Trim() == "UP-GRADATION")
                {
                    status = 1;
                }
                if (radiType.Text.Trim() == "DOWNGRADATION")
                {
                    status = 2;
                }

                Hashtable ht = new Hashtable();
                ht.Add("CustomerID", (Session[SessionInfo.loginid].ToString()));
                ht.Add("status", status);


                DataTable dt = objDBUitility.GetDataByProc(ht, "sp_getPackageList");

                foreach (DataRow dataRow in dt.Rows)
                {
                    RadComboBoxItem item = new RadComboBoxItem();
                    item.Text = (string)dataRow["BandWidth"];
                    item.Value = dataRow["BID"].ToString();
                    cmbPackage.Items.Add(item);
                    item.DataBind();
                }
            }
            catch (Exception ex)
            {
                Message.Show(ex.Message.ToString());
            }
        }

        protected void cmbPackage_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                txtNewMRC.Text = string.Empty;

                if (cmbPackage.SelectedValue != "")
                {

                    Hashtable ht = new Hashtable();
                    ht.Add("BID", cmbPackage.SelectedValue);
                    DataTable dt = objDBUitility.GetDataByProc(ht, "sp_getNewMRC");

                    foreach (DataRow dataRow in dt.Rows)
                    {
                        txtNewMRC.Text = dataRow["TotalMRC"].ToString();
                    }

                }
            }
            catch (Exception ex)
            {
                Message.Show(ex.Message.ToString());
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (chkDisclaimer.Checked)
                {
                    int statusID = 0;
                    decimal MRC = Conversion.TryCastDecimal(txtNewMRC.Text.Trim());
                    decimal Discount = Conversion.TryCastDecimal(txtDiscount.Text.Trim());
                    decimal Balance = Conversion.TryCastDecimal(txtCustomerBalance.Text.Trim());
                    DateTime EffectiveDate = (DateTime)dpEffectiveDate.SelectedDate;
                    var RequestNotes = (txtnotes.Text.Trim());
                    int BID = Conversion.TryCastInteger(cmbPackage.SelectedValue);

                    if (radiType.Text.Trim() == "UP-GRADATION")
                    {
                        statusID = 4;
                    }
                    if (radiType.Text.Trim() == "DOWNGRADATION")
                    {
                        statusID = 5;
                    }

                    /************Added Date Logic*************/
                    if (statusID == 4)
                    {
                        Hashtable ht = new Hashtable();
                        ht.Add("StatusID", statusID);
                        ht.Add("CustomerID", txtCustomerID.Text.Trim());
                        DataTable dtDate = objDBUitility.GetDataByProc(ht, "sp_getEffectiveDate");
                        foreach (DataRow dr in dtDate.Rows)
                        {
                            if (DateTime.Today > DateTime.Parse(dr["EffectiveDate"].ToString()))
                            {
                                Message.Show("Effective date must be in next billing cylcle start date and can not less than today");
                                return;
                            }
                        }
                    }
                    if (statusID == 5)
                    {
                        Hashtable ht = new Hashtable();
                        ht.Add("StatusID", statusID);
                        ht.Add("CustomerID", txtCustomerID.Text.Trim());
                        DataTable dtDate = objDBUitility.GetDataByProc(ht, "sp_getEffectiveDate");
                        foreach (DataRow dr in dtDate.Rows)
                        {
                            if (DateTime.Today > DateTime.Parse(dr["EffectiveDate"].ToString()))
                            {
                                Message.Show("Effective date must be in next billing cylcle start date and can not less than today");
                                return;
                            }
                        }
                    }

                    var CustomerID = txtCustomerID.Text.Trim();
                    int EntryID = Conversion.TryCastInteger((Session[SessionInfo.loginid].ToString()));

                    decimal CustBalance = 0;

                    Hashtable hash = new Hashtable();
                    hash.Add("CustomerID", txtCustomerID.Text.Trim());
                    DataTable dt = objDBUitility.GetDataByProc(hash, "sp_getbalancebyid");

                    foreach (DataRow dr in dt.Rows)

                        CustBalance = decimal.Parse(dr["Balance"].ToString());

                    if (CustBalance >= MRC)
                    {
                        Hashtable hashTable1 = new Hashtable();
                        hashTable1.Add("CustomerID", CustomerID);
                        hashTable1.Add("EntryID", 0);
                        hashTable1.Add("EffectiveDate", EffectiveDate);
                        hashTable1.Add("StatusID", statusID);
                        hashTable1.Add("BID", BID);
                        hashTable1.Add("Amount", MRC);
                        hashTable1.Add("RequestNotes", RequestNotes);
                        hashTable1.Add("ProcessID", 35);

                        DataTable Dtab = objDBUitility.GetDataByProc(hashTable1, "customerrequestinsertupdateSelfCare");

                        if (Dtab.Rows.Count > 0)
                        {
                            foreach (DataRow dataRow in Dtab.Rows)
                            {
                                Message.Show(dataRow["Feedback"].ToString());
                            }
                            Clear();
                        }
                    }

                    else
                    {
                        Message.Show("You do not have sufficient balance to migrate to new package");
                    }
                }

                else
                {
                    Message.Show("Please accept disclaimer before submit");
                }
            }
            catch (Exception ex)
            {
                Message.Show(ex.Message);
            }

        }

        void InfoLoad()
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("CustomerID", (Session[SessionInfo.loginid].ToString()));
                ht.Add("ProcessID", "PACKAGE_CHANGE");
                DataTable dt = objDBUitility.GetDataByProc(ht, "sp_customerinfoforrequestSelfCare");


                foreach (DataRow dataRow in dt.Rows)
                {

                    if (dataRow["Feedback"].ToString() != "FOUND")
                    {
                        Message.Show(dataRow["Feedback"].ToString());
                        txtCustomerID.Text = string.Empty;
                        txtCustomerBalance.Text = string.Empty;
                        txtpackage.Text = string.Empty;
                        txtmrc.Text = string.Empty;
                        txtDiscount.Text = string.Empty;
                        txtnetmrc.Text = string.Empty;


                        txtCustomerType.Text = string.Empty;
                        txtCustomerName.Text = string.Empty;
                        txtPop.Text = string.Empty;
                        txtAddress.Text = string.Empty;
                        txtMobile.Text = string.Empty;
                        txtEmail.Text = string.Empty;
                        txtNationality.Text = string.Empty;
                        txtNIDPassport.Text = string.Empty;
                        txtOccupation.Text = string.Empty;
                        break;
                    }
                    else

                    {
                        txtCustomerID.Text = (Session[SessionInfo.loginid].ToString());
                        txtCustomerBalance.Text = dataRow["CustomerBalance"].ToString();
                        txtpackage.Text = dataRow["BandWidth"].ToString();
                        txtmrc.Text = dataRow["TotalMRC"].ToString();
                        txtDiscount.Text = (dataRow["Discount"].ToString());
                        txtnetmrc.Text = dataRow["NetMRC"].ToString();
                        txtCustomerType.Text = dataRow["CustomerTypeName"].ToString();
                        txtCustomerName.Text = dataRow["CustomerName"].ToString();
                        txtPop.Text = dataRow["POPName"].ToString();
                        txtAddress.Text = dataRow["Address"].ToString();
                        txtMobile.Text = dataRow["Mobile"].ToString();
                        txtEmail.Text = dataRow["Email"].ToString();
                        txtNationality.Text = dataRow["NationalityId"].ToString();
                        txtNIDPassport.Text = dataRow["NID"].ToString();
                        txtOccupation.Text = dataRow["OccupationName"].ToString();
                        if (radiType.Text.Trim() == "DOWNGRADATION")
                        {
                            Hashtable htDate = new Hashtable();
                            htDate.Add("StatusID", 5);
                            htDate.Add("CustomerID", txtCustomerID.Text.Trim());
                            DataTable dtDate = objDBUitility.GetDataByProc(htDate, "sp_getEffectiveDate");
                            foreach (DataRow dr in dtDate.Rows)
                            {
                                dpEffectiveDate.SelectedDate = DateTime.Parse(dr["EffectiveDate"].ToString());
                                lblEffectiveDate.Text = "Effective date must be in next billing cylcle start date.";
                            }
                        }
                        if (radiType.Text.Trim() == "UP-GRADATION")
                        {
                            Hashtable htDate = new Hashtable();
                            htDate.Add("StatusID", 4);
                            htDate.Add("CustomerID", txtCustomerID.Text.Trim());
                            DataTable dtDate = objDBUitility.GetDataByProc(htDate, "sp_getEffectiveDate");
                            foreach (DataRow dr in dtDate.Rows)
                            {
                                dpEffectiveDate.SelectedDate = DateTime.Parse(dr["EffectiveDate"].ToString());
                                lblEffectiveDate.Text = "Effective date must be in next billing cylcle start date.";
                            }
                        }
                    }

                }
            }

            catch (Exception ex)
            {
                Message.Show(ex.Message);
            }

        }

        void Clear()
        {
            txtCustomerID.Text = string.Empty;
            txtCustomerBalance.Text = string.Empty;
            txtpackage.Text = string.Empty;
            txtmrc.Text = string.Empty;
            txtDiscount.Text = string.Empty;
            txtnetmrc.Text = string.Empty;
            cmbPackage.Text = string.Empty;
            cmbPackage.ClearSelection();
            txtNewMRC.Text = string.Empty;
            dpEffectiveDate.Clear();
            txtnotes.Text = string.Empty;
            chkDisclaimer.Checked = false;

            txtCustomerType.Text = string.Empty;
            txtCustomerName.Text = string.Empty;
            txtPop.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtNationality.Text = string.Empty;
            txtNIDPassport.Text = string.Empty;
            txtOccupation.Text = string.Empty;
        }

        protected void radiType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radiType.Text.Trim() == "UP-GRADATION")
            {
                dpEffectiveDate.Enabled = true;
            }
            if (radiType.Text.Trim() == "DOWNGRADATION")
            {
                dpEffectiveDate.Enabled = false;
            }
            cmbPackage.Text = string.Empty;
            cmbPackage.ClearSelection();
            txtNewMRC.Text = string.Empty;
            //dpEffectiveDate.Clear();
            txtnotes.Text = string.Empty;
        }
    }
}