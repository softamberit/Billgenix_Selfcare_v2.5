using BillgenixTicketing.ApiIntigration;
using BillgenixTicketing.Models;
using BillingERPConn;
using BillingERPSelfCare.Session;
using BillingERPSelfCare.Utility;
using MkCommunication;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;

namespace BillingERPSelfCare.Sales
{
    public partial class CustomerDetail : BasePageClass
    {
        DBUtility objDBUitility = new DBUtility();
        //MkConnection objMKConnection = new MkConnection();

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            Literal pageLtl = ((Literal)PageUtility.FindControlIterative(this.Master, "ltlPageName"));
            if (!IsPostBack)
            {
                btnSearch_Click();
            }
            if (pageLtl != null)
            {
                pageLtl.Text = "Customer View";
            }
        }
        #endregion

        #region Search

        private void btnSearch_Click()
        {
            try
            {
                Clear();

                Hashtable ht = new Hashtable();
                string CustID = Session[SessionInfo.loginid].ToString();
                ht.Add("CustomerID", CustID);

                DataTable dt = objDBUitility.GetDataByProc(ht, "sp_getAllDetailForCustomer");

                foreach (DataRow dataRow in dt.Rows)
                {
                    if (dataRow["Feedback"].ToString() != "Found")
                    {
                        Message.Show(dataRow["Feedback"].ToString());
                        //txtCustomerID.Text = string.Empty;
                        break;
                    }

                    else
                    {

                        //txtCustomerID.Text      = Session[SessionInfo.loginid].ToString();
                        txtCustomerType.Text = dataRow["CustomerTypeName"].ToString();
                        txtCustomerName.Text = dataRow["CustomerName"].ToString();
                        txtContactName.Text = dataRow["Attention"].ToString();
                        txtAddress.Text = dataRow["Address"].ToString();
                        txtMobile.Text = dataRow["Mobile"].ToString();
                        txtEmail.Text = dataRow["Email"].ToString();
                        txtNationality.Text = dataRow["NationalityId"].ToString();
                        txtNIDPassport.Text = dataRow["NID"].ToString();
                        txtOccupation.Text = dataRow["OccupationName"].ToString();
                        txtPop.Text = dataRow["POPName"].ToString();
                        txtSalesDate.Text = dataRow["SalesDate"].ToString();
                        txtNotes.Text = dataRow["EntryNotes"].ToString();
                        txtPackage.Text = dataRow["Bandwidth"].ToString();

                        txtMRC.Text = dataRow["TotalMRC"].ToString();
                        txtOTC.Text = dataRow["TotalOTC"].ToString();
                        txtCollection.Text = dataRow["TranModeName"].ToString();
                        txtRefNo.Text = dataRow["MRNO"].ToString();
                        txtAmnt.Text = dataRow["MRAmount"].ToString();
                        txtDiscount.Text = dataRow["Discount"].ToString();
                        //txtCreditlimit.Text     = dataRow["CreditLimit"].ToString();
                        txtNetMRC.Text = dataRow["NetMRC"].ToString();
                        txtPackageMRC.Text = dataRow["PackageMRC"].ToString();
                        txtPackageOTC.Text = dataRow["PackageOTC"].ToString();
                        txtMRCVat.Text = dataRow["MRCVat"].ToString();
                        txtOTCVat.Text = dataRow["OTCVat"].ToString();

                        txtHandoverDate.Text = dataRow["HandoverDate"].ToString();

                        decimal cust_balance = Conversion.TryCastDecimal(dataRow["Balance"].ToString());

                        //txtBalance.Text = (-1 * (cust_balance * (-1) + Conversion.TryCastDecimal(dataRow["tempMRC"].ToString()))).ToString();
                        txtBalance.Text = cust_balance.ToString();

                        string Hostname = "", Username = "", Password = "", IPAddress = "", ProtocolID = "";
                        string RouterID = "", InsType = "", mkUser = ""; string mkVersion = "";

                        Username = dataRow["RouterUserName"].ToString();
                        Password = dataRow["Password"].ToString();
                        IPAddress = dataRow["IPAddress"].ToString();
                        Hostname = dataRow["Host"].ToString();
                        RouterID = dataRow["RouterID"].ToString();
                        ProtocolID = dataRow["ProtocolID"].ToString();

                        InsType = dataRow["InsType"].ToString();
                        mkUser = dataRow["MkUser"].ToString();
                        mkVersion = dataRow["mkVersion"].ToString();

                        MkStatus objMkStatus;

                        if (dataRow["IsLogicalInstallDone"].ToString() == "1")
                        {
                            //objMkStatus = objMKConnection.MikrotikStatus(Hostname, Username, Password, mkVersion, Conversion.TryCastInteger(ProtocolID), CustID, Conversion.TryCastInteger(InsType), mkUser);

                            BillgenixRadiusClient _client = new BillgenixRadiusClient();
                            objMkStatus = _client.GetMkStatus(CustID);

                            lbDiscontinueSource.Text = objMkStatus.RetMessage;
                            //if (InsType == "1")
                            //{
                            //    if (objMkStatus.DLength > 0)
                            //    {
                            //        if (objMkStatus.MikrotikStatus == 0)
                            //        {
                            //            if (dataRow["StatusID"].ToString() == "1")
                            //            {

                            //                lbDiscontinueSource.Text = "ACTIVE";
                            //            }
                            //            else if (dataRow["StatusID"].ToString() == "3")
                            //            {
                            //                lbDiscontinueSource.Text = dataRow["StatusName"].ToString() + ", " + dataRow["SecondaryStatus"].ToString();
                            //            }

                            //            else if (dataRow["StatusID"].ToString() == "2")
                            //            {
                            //                lbDiscontinueSource.Text = dataRow["StatusName"].ToString() + ", " + dataRow["SecondaryStatus"].ToString();
                            //            }

                            //            else if (dataRow["StatusID"].ToString() == "9")
                            //            {
                            //                lbDiscontinueSource.Text = dataRow["StatusName"].ToString() + ", " + dataRow["SecondaryStatus"].ToString();
                            //            }


                            //        }
                            //        else if (objMkStatus.MikrotikStatus == 1)
                            //        {
                            //            lbDiscontinueSource.Text = @"ACTIVE";
                            //        }
                            //    }

                            //    else if (objMkStatus.DLength == 0)
                            //    {
                            //        if (dataRow["StatusID"].ToString() == "1")
                            //        {
                            //            //string ss = "Active";

                            //            lbDiscontinueSource.Text = "ACTIVE";
                            //        }
                            //        else if (dataRow["StatusID"].ToString() == "3")
                            //        {
                            //            lbDiscontinueSource.Text = @"CANCEL";
                            //        }

                            //        else if (dataRow["StatusID"].ToString() == "2")
                            //        {
                            //            lbDiscontinueSource.Text = @"DISCONTINUE" + ", " + dataRow["SecondaryStatus"].ToString(); ;
                            //        }

                            //        else if (dataRow["StatusID"].ToString() == "9")
                            //        {
                            //            lbDiscontinueSource.Text = "INACTIVE";
                            //        }

                            //        else
                            //        {
                            //            lbDiscontinueSource.Text = "NO STATUS YET!";
                            //        }
                            //    }
                            //}

                            //else if (InsType == "2") // PPPOE 
                            //{
                            //    if (objMkStatus.MikrotikStatus == 1)
                            //    {
                            //        lbDiscontinueSource.ForeColor = Color.Red;
                            //        lbDiscontinueSource.Text = @"ACTIVE";

                            //    }
                            //    else if (objMkStatus.MikrotikStatus == 0)
                            //    {
                            //        lbDiscontinueSource.ForeColor = Color.Red;

                            //        if (dataRow["StatusID"].ToString() == "1")
                            //        {
                            //            //string ss = "Active";

                            //            lbDiscontinueSource.Text = "ACTIVE";
                            //        }
                            //        else if (dataRow["StatusID"].ToString() == "3")
                            //        {
                            //            lbDiscontinueSource.Text = @"CANCEL";
                            //        }

                            //        else if (dataRow["StatusID"].ToString() == "2")
                            //        {
                            //            lbDiscontinueSource.Text = @"DISCONTINUE" + ", " + dataRow["SecondaryStatus"].ToString(); ;
                            //        }

                            //        else if (dataRow["StatusID"].ToString() == "9")
                            //        {
                            //            lbDiscontinueSource.Text = dataRow["StatusName"].ToString() + ", " + dataRow["SecondaryStatus"].ToString();
                            //        }

                            //        else
                            //        {
                            //            lbDiscontinueSource.Text = "NO STATUS YET!";
                            //        }

                            //    }
                            //}
                        }
                        else
                        {
                            lbDiscontinueSource.Text = "NO STATUS YET!";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Message.Show(ex.Message.ToString());
            }
        }
        #endregion

        #region Clear
        private void Clear()
        {
            txtCustomerType.Text = string.Empty;
            txtCustomerName.Text = string.Empty;
            txtContactName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtNationality.Text = string.Empty;
            txtNIDPassport.Text = string.Empty;
            txtOccupation.Text = string.Empty;
            txtPop.Text = string.Empty;
            txtSalesDate.Text = string.Empty;
            txtNotes.Text = string.Empty;
            txtPackage.Text = string.Empty;
            //txtInternetMRC.Text = string.Empty;
            //txtInternetOTC.Text = string.Empty;
            //txtIpTvMRC.Text = string.Empty;
            //txtIpTvOTC.Text = string.Empty;
            txtMRC.Text = string.Empty;
            txtOTC.Text = string.Empty;
            txtCollection.Text = string.Empty;
            txtRefNo.Text = string.Empty;
            txtAmnt.Text = string.Empty;
            txtDiscount.Text = string.Empty;
            // txtCreditlimit.Text = string.Empty;
            txtNetMRC.Text = string.Empty;

            txtBalance.Text = string.Empty;
            txtHandoverDate.Text = string.Empty;
            //txtStatus.Text = string.Empty;
            //txtStatus.BackColor = Color.FromName("#e9ecef");
        }
        #endregion 
    }
}