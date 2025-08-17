using BillingERPConn;
using BillingERPSelfCare.DataAccess;
using BillingERPSelfCare.Session;
using BillingERPSelfCare.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BillingERPSelfCare.Settings
{
    public partial class PasswordChange : System.Web.UI.Page
    {
        DBUtility objDBUtility = new DBUtility();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtLoginId.Text = Session[SessionInfo.loginid].ToString();
            }

            Literal pageLtl = ((Literal)PageUtility.FindControlIterative(this.Master, "ltlPageName"));
            if (pageLtl != null)
            {
                pageLtl.Text = "Change Password";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DAUser objDA = new DAUser();

                string loginId = Session[SessionInfo.loginid].ToString();
                string PinNo = Session[SessionInfo.PIN].ToString();
                string CurrentPasswd = txtCurrentPassword.Text.Trim();
                //string cmd = "SELECT [CustomerPassword]  FROM CustomerMaster WHERE CustomerID='" + loginId + "";
                //string RealCurrentPawwsd = objDBUtility.AggRetrive("SELECT [CustomerPassword]  FROM CustomerMaster WHERE CustomerID='" + loginId + "'").ToString();
                Hashtable ht = new Hashtable();
                ht.Add("LoginId", loginId);

                var data = objDBUtility.GetDataByProc(ht, "sp_SelfCare_checkCustomerAccessForReset");
                string RealCurrentPawwsd = "";
                if (data != null && data.Rows.Count > 0)
                {
                    RealCurrentPawwsd = data.Rows[0]["password"].ToString();
                   // userid = data.Rows[0]["loginid"].ToString();
                }

                string NewPasswd = txtNewPassword.Text.Trim();
                string ReTypeNewPasswd = txtReTypeNewPassword.Text.Trim();

                if (CurrentPasswd == RealCurrentPawwsd)
                {
                    if (NewPasswd == ReTypeNewPasswd)
                    {
                        if (NewPasswd.Length > 7 && NewPasswd.Length < 30)
                        {
                            if (PasswordValidation(NewPasswd))
                            {
                                Message.Show(objDA.PasswdChange(PinNo, loginId, NewPasswd, CurrentPasswd));


                                txtCurrentPassword.Text = String.Empty;
                                txtNewPassword.Text = String.Empty;
                                txtReTypeNewPassword.Text = String.Empty;
                            }
                            else
                            {
                                Message.Show("Please Read The Instructions");
                            }
                        }
                        else
                        {
                            Message.Show("Password length must be minimum 8 and maximun 30");
                        }

                    }
                    else
                    {
                        Message.Show("New Password does not match");
                    }
                }
                else
                {
                    Message.Show("Wrong Current Password");
                }

            }

            catch (Exception ex)
            {
                Message.Show(ex.Message);
            }
        }
        private bool PasswordValidation(string encrPass)
        {
            var regexItem = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");

            if (regexItem.IsMatch(encrPass))
            {
                return true;
            }
            else
            {
                return false;

            }
        }
    }
}