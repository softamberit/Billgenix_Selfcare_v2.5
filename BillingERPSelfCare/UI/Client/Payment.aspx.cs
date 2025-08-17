using BillingERPConn;
using BillingERPSelfCare.Bkash;
using BillingERPSelfCare.Bkash.Models;
using BillingERPSelfCare.Session;
using BillingERPSelfCare.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;

namespace BillingERPSelfCare.UI.Client
{
    public partial class Payment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[SessionInfo.loginid] == null)
            {
                HttpContext.Current.Response.Redirect("~/SignIn.aspx");
            }

            //DueAmount();
        }


        private void DueAmount()
        {
            try
            {
                var objDB = new DBUtility();
                var ht = new Hashtable();

                var customerId = Session[SessionInfo.loginid];

                ht.Add("CustomerID", customerId);

                var dt = objDB.GetDataByProc(ht, "sp_getdashboardSelfCare");


                var balance = objDB.GetDataByProc(ht, "sp_getbalancebyid");

                var cust_balance = Conversion.TryCastDecimal(balance.Rows[0]["Balance"].ToString());
                txtInvoiceAmount.Text = (cust_balance * (-1) + Conversion.TryCastDecimal(dt.Rows[0]["tempMRC"].ToString())).ToString();

            }
            catch (Exception ex)
            {

                Message.Show(ex.Message);
            }

        }

        protected void btnYes_OnClick(object sender, EventArgs e)
        {

            if (bkashRadio.Checked)
            {
                payBkash();
            }
            else
            {
                paySSL();
            }
        }
        protected void paySSL()
        {
            try
            {
                //   txtInvoiceAmount.Text = string.Empty;
                var invoiceAmount = txtInvoiceAmount.Text;

                var val = Conversion.TryCastInteger(txtInvoiceAmount.Text.ToString());
                if (val < 0)
                {
                    Message.Show("Please Provide correct amount!");
                    return;
                }

                if (invoiceAmount != "")
                {
                    string ipaddress = Session[SessionInfo.PublicIP].ToString();
                    string customerid = Session[SessionInfo.loginid].ToString();
                    Session.Add(SessionInfo.InvoiceAmount, invoiceAmount);
                    Session.Add(SessionInfo.PaymentOption, "SSL");

                    WriteLogFile.WriteLog("SEND=> CustomerID=" + customerid + ":: Collection Amount= " + invoiceAmount + ":: IP=" + ipaddress + "  ");

                    Response.Redirect("~/UI/Client/PaymentGateway.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();

                }
                else
                {
                    Message.Show("Please Provide correct amount!");
                }
            }
            catch (Exception ex)
            {
                WriteLogFile.WriteLog("SEND=> CustomerID=" + Session[SessionInfo.loginid].ToString() + " " + ex.Message);

                Message.Show("Please Provide correct amount!");
            }
        }
        protected void AddBkash(object sender, EventArgs e)
        {
            Response.Redirect("~/Bkash/BKashAccountAdd.aspx", false);
        }
        protected void payBkash()
        {
            try
            {
                var invoiceAmount = txtInvoiceAmount.Text;
                var val = Conversion.TryCastInteger(txtInvoiceAmount.Text.ToString());
                if (val < 0)
                {
                    Message.Show("Please Provide correct amount!");
                    return;
                }
                if (invoiceAmount != "")
                {
                    string ipaddress = Session[SessionInfo.PublicIP].ToString();
                    string customerid = Session[SessionInfo.loginid].ToString();
                    Session.Add(SessionInfo.InvoiceAmount, invoiceAmount);
                    Session.Add(SessionInfo.PaymentOption, "Bkash");
                    if(Session[SessionInfo.PaymentOption].ToString() == "Bkash")
                    {
                        Response.Redirect("~/Bkash/BKashAccountAdd.aspx", false);
                    }
                    else
                    {
                        Response.Redirect("~/UI/Client/PaymentGateway.aspx", false);
                    }

                    //DBUtility db = new DBUtility();
                    //Hashtable ht = new Hashtable();
                    //ht.Add("loginId", customerid);
                    //DataTable res = db.GetDataByProc(ht, "sp_SelfCare__bKash_GetExistingAgreement");
                    //if (res.Rows.Count > 0)
                    //{
                    //    Session.Remove(SessionInfo.Bkash_paymentID);
                    //    Session.Remove(SessionInfo.Bkash_id_token);
                    //    // Session.Add(SessionInfo.agreementID, res.Rows[0]["agreementID"].ToString());
                    //    WriteLogFile.WriteLog("SEND=> CustomerID=" + customerid + ":: Collection Amount= " + invoiceAmount + ":: IP=" + ipaddress + "  ");
                    //    Response.Redirect("~/UI/Client/PaymentGateway.aspx", false);
                    //    Context.ApplicationInstance.CompleteRequest();
                    //}
                    //else
                    //{
                    //    Response.Redirect("~/Bkash/BKashAccountAdd.aspx", false);
                    //}

                }
                else
                {
                    Message.Show("Please Provide correct amount!");
                }
            }
            catch (Exception ex)
            {
                WriteLogFile.WriteLog("SEND=> CustomerID=" + Session[SessionInfo.loginid].ToString() + " " + ex.Message);

                Message.Show("Please Provide correct amount!");
            }
        }
    }
}