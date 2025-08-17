using BillingERPConn;
using BillingERPSelfCare.Session;
using BillingERPSelfCare.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BillingERPSelfCare
{
    public partial class ResetPassword : SignInUtility
    {
        DBUtility db = new DBUtility();
        string backUrl = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            backUrl = Request.QueryString["FromUrl"];
            if (Session[SessionInfo.HashKey] == null && Session[SessionInfo.loginid] == null)
            {
                Response.Redirect("~/SignIn.aspx");
            }

        }


        protected void btnResetPass_Click(object sender, EventArgs e)
        {


            if (Session[SessionInfo.HashKey] == null)
            {
                LoginMsg.Text = "Session is Expired";
                return;
            }
            string loginId = Session[SessionInfo.loginid].ToString();
            string mobile = Session[SessionInfo.phone].ToString();

            string newPass = txtNewPassword.Text.ToString();
            string confirmPass = txtConfirmPassword.Text.ToString();


            if (newPass != null && confirmPass != null)
            {
                if (newPass.Equals(confirmPass))
                {

                    if (PasswordValidation(confirmPass))
                    {

                        // var pindb = db.AggRetrive("Select PIN from AppsAuthentication where PIN='"+userPIN+"' ");

                        if (!string.IsNullOrEmpty(mobile))
                        {

                            Hashtable ht = new Hashtable();
                            ht.Add("Mobile", mobile);
                            ht.Add("CustomerId", loginId);
                            ht.Add("NewPassword", confirmPass);
                            var ss = db.InsertData(ht, "[sp_SelfCare_resetPasswordByCustomerId]");
                            if (ss > 0)
                            {
                                Session.RemoveAll();
                                LoginMsg.Text = "Password has changed Successfully";
                            }


                        }
                        else { LoginMsg.Text = "Invalid User"; }
                    }
                    else { LoginMsg.Text = "Please Read The Instructions"; }
                }
                else { LoginMsg.Text = "Passwords Not Matched"; }
            }
            else
            {

                LoginMsg.Text = "Please Enter Valid Password";
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
        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(backUrl))
            {
                Response.Redirect(backUrl);

            }
            else
            {
                Response.Redirect("SignIn.aspx");

            }
        }
    }
}