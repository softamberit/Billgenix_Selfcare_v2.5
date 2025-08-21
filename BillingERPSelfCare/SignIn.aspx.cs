using BillingERPSelfCare.BusinessEntity;
using BillingERPSelfCare.DataAccess;
using BillingERPSelfCare.Session;
using BillingERPSelfCare.Utilitys;
using BillingERPSelfCare.Utility;
using System;
using System.Data;
using BillingERPConn;
using System.Collections;

namespace BillingERPSelfCare
{
    public partial class SignIn : SignInUtility
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[SessionInfo.PIN] != null)
            {
                Response.Redirect("DashBoard.aspx", false);
            }


            divlogin.Visible = true;
            divotp.Visible = false;
            btnLogin.Visible = true;
        }

        protected void btnOtp_Click(object sender, EventArgs e)
        {
            if (rdoSMS.Checked)
            {
                SendOtp(1);

            }
            if (rdoVoice.Checked)
            {
                SendOtp(2);

            }
        }
        //protected void btnOtpByVoice_Click(object sender, EventArgs e)
        //{
        //    SendOtp(2);
        //}
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            login();
        }
        protected void SendOtp(int sms_media)
        {
            if (!rfv1.IsValid || !rfv2.IsValid || !rfv3.IsValid)
                return;

            try
            {
                DBUtility db = new DBUtility();

                BOUser obj = new BOUser();
                DAUser objDa = new DAUser();
                obj.LoginId = txtUserName.Text.Trim();
                obj.Password = txtPassword.Text.Trim();


                var requestIP = GetUserIP();
                int maxAttempts = 3;
                int lockoutMinutes = 5;

                if (IsIpBlockedInDb(requestIP, maxAttempts, lockoutMinutes, obj))
                {
                    LoginMsg.Text = "Too many failed attempts. Please try again after 5 minutes.";
                    return;
                }

                DataTable DTabLogin = objDa.UserAccess(obj);

                if (DTabLogin.Rows.Count == 1)
                {
                    string Pin = DTabLogin.Rows[0]["loginId"].ToString();
                    string mobileNumber = DTabLogin.Rows[0]["mobile"].ToString();
                    Session.Add(SessionInfo.user_name, DTabLogin.Rows[0]["UserName"].ToString());
                    Session.Add(SessionInfo.loginid, DTabLogin.Rows[0]["loginId"].ToString());
                    Session.Add(SessionInfo.PIN, Pin);
                    Session.Add(SessionInfo.CompanyName, DTabLogin.Rows[0]["CompanyName"].ToString());
                    Session.Add(SessionInfo.IsAllowedGraph, DTabLogin.Rows[0]["IsAllowedGraph"].ToString());

                    //IsAllowedGraph
                    string PublicIP = MachineDetector.GetUser_PublicIP();
                    Session.Add(SessionInfo.PublicIP, PublicIP);
                    if (GenerateAndSendOtp(Pin, mobileNumber, sms_media))
                    {
                        divlogin.Visible = false;
                        divotp.Visible = true;
                    }
                    else
                    {
                        RemoveSession();
                        LoginMsg.Text = "Internal server error";
                        return;
                    }
                }

                else
                {
                    divlogin.Visible = true;
                    divotp.Visible = false;
                    LoginMsg.Text = "Invalid Customer ID Or Password No";
                }

            }

            catch (Exception ex)
            {

                Alert.Show(ex.Message.ToString());
            }
        }

        private bool IsIpBlockedInDb(string requestIP, int maxAttempts, int lockoutMinutes, BOUser obj)
        {
            DBUtility db = new DBUtility();
            Hashtable ht = new Hashtable();
            ht.Add("RequestIP", requestIP);
            ht.Add("LoginId", obj.LoginId);
            ht.Add("Password", obj.Password);
            ht.Add("MaxAttempts", maxAttempts);
            ht.Add("LockoutMinutes", lockoutMinutes);


            var res = db.GetDataByProc(ht, "sp_SelfCare_InsertLoginAttempt");
            var status = res.Rows[0]["Status"].ToString();
            if (status == "allowed")
            {
                return false;// allowed
            }
            else
            {
                return true; // blocked
            }
        }

        private void login()
        {
            try
            {
                if (Session[SessionInfo.HashKey] == null)
                {
                    otpPageVisible();
                    OtpMsg.Text = "Your session is out";
                    return;
                }
                DBUtility db = new DBUtility();
                string codes = txtOtp.Text;
                string loginId = "";
                string verified_code = "";
                string HashKey = Session[SessionInfo.HashKey].ToString();
                if (codes != null && loginId != null)
                {
                    Hashtable ht_code = new Hashtable();
                    ht_code.Add("HashKey ", HashKey);
                    var data_code = db.GetDataByProc(ht_code, "sp_SelfCare_GetVerificationByHashKey");

                    if (data_code != null && data_code.Rows.Count > 0)
                    {
                        verified_code = data_code.Rows[0]["VerificationCode"].ToString();
                        DateTime sendTime = Conversion.TryCastDate(data_code.Rows[0]["SendDate"].ToString());
                        var Id = data_code.Rows[0]["ID"].ToString();
                        var attempt = data_code.Rows[0]["Attempt"].ToString();
                        string loginid = data_code.Rows[0]["LoginId"].ToString();
                        // string user_name = data_code.Rows[0]["username"].ToString();

                        var nowDate = DateTime.Now;
                        if (verified_code.ToString() != null && verified_code.ToString() == codes)
                        {
                            if ((nowDate - sendTime).TotalMinutes <= 5)
                            {
                                if (OtpUpdate(Id, 1, loginid))
                                {
                                    OtpMsg.Text = "Successfull";
                                    Session.Add(SessionInfo.loginid, loginid);
                                    Session.Add(SessionInfo.PIN, loginid);
                                    Session.Add(SessionInfo.PublicIP, GetUserIP());
                                    LoggedIn();
                                    Session.Remove(SessionInfo.HashKey);
                                }
                                else
                                {
                                    otpPageVisible();
                                    OtpMsg.Text = "Try Again";
                                }
                            }
                            else
                            {
                                otpPageVisible();
                                OtpUpdate(Id, 3, loginid);
                                OtpMsg.Text = "OTP is expired";

                            }
                        }
                        else
                        {
                            if (Conversion.TryCastInteger(attempt) < 2)
                            {
                                otpPageVisible();
                                OtpUpdate(Id, 4, loginid);// UPDATE WRONG ATTEMPT
                                OtpMsg.Text = $"The OTP is entered incorrectly {Conversion.TryCastInteger(attempt) + 1} time(s)";
                            }
                            else
                            {
                                otpPageVisible();
                                OtpUpdate(Id, 2, loginid);
                                OtpMsg.Text = "Your OTP is invalid or has expired. Please return to the login page.";
                                btnLogin.Visible = false;
                            }
                        }

                    }
                    else
                    {
                        otpPageVisible();
                        OtpMsg.Text = "OTP is invalid or expired";
                    }

                }
                else
                {
                    otpPageVisible();
                    OtpMsg.Text = "Enter your OTP";
                }

            }

            catch (Exception ex)
            {

                Message.Show(ex.Message + "No User Exists");
            }
        }
        private bool OtpUpdate(string Id, int status, string loginId)
        {
            try
            {
                DBUtility db = new DBUtility();
                Hashtable ht_vc = new Hashtable();
                ht_vc.Add("Id", Id);
                ht_vc.Add("VerifiedIP", GetUserIP());
                ht_vc.Add("IsVerified", status);
                ht_vc.Add("loginId", loginId);

                var res_vc = db.GetDataByProc(ht_vc, "sp_SelfCare_UpdateOTPVerification");
                ht_vc.Clear();
                return true;

            }
            catch (Exception)
            {


            }
            return false;
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            RemoveSession();
            Response.Redirect("SignIn.aspx", false);
            btnLogin.Visible = true;
        }

        protected void btnForgetPass_Click(object sender, EventArgs e)
        {
            Response.Redirect("ResetPassOtp.aspx", false);
        }

        private void otpPageVisible()
        {
            divlogin.Visible = false;
            divotp.Visible = true;
        }
    }
}