using BillingERPConn;
using BillingERPSelfCare.Session;
using System;
using System.Collections;

namespace BillingERPSelfCare.Utility
{
    public class SignInUtility : System.Web.UI.Page
    {
        public string GetUserIP()
        {
            string ipList = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipList))
            {
                return ipList.Split(',')[0];
            }

            return Request.ServerVariables["REMOTE_ADDR"];
        }

        public bool GenerateAndSendOtp(string loginId, string mobile, int sms_media)
        {
            bool rv = false;
            try
            {
                DBUtility db = new DBUtility();
                Random random = new Random();
                string code = random.Next(100000, 999999).ToString();
                string smsText = string.Format("Your OTP is : {0}. This OTP will be expired within 5 minutes. From MySwift, Amber IT", code);

                var hashKey = Guid.NewGuid().ToString();

                Hashtable ht = new Hashtable();
                ht.Add("Mobile", mobile);
                ht.Add("SMSType", 31);
                ht.Add("SMSText", smsText);
                ht.Add("IsSend", 0);
                ht.Add("InitializationTime", DateTime.Now);
                ht.Add("UserName", loginId);
                ht.Add("VerificationCode", code);
                ht.Add("HashKey", hashKey);
                ht.Add("UseSMSPortal", sms_media);

                long res = db.InsertData(ht, "sp_SelfCare_SendOtpFromLogin");
                if (res == 1)
                {
                    RemoveSession();
                    Session.Add(SessionInfo.HashKey, hashKey);
                    rv = true;
                }

            }
            catch (Exception ex)
            {


            }
            return rv;
        }
        public void RemoveSession()
        {
            Session.Remove(SessionInfo.loginid);
            Session.Remove(SessionInfo.PIN);
            Session.Remove(SessionInfo.CompanyName);
            Session.Remove(SessionInfo.PublicIP);
            Session.Remove(SessionInfo.HashKey);
        }
        public void LoggedIn()
        {

            if (Session[SessionInfo.PIN] != null)
            {
                InsertSessionHistory();
                Response.Redirect("DashBoard.aspx", false);
            }
        }
        private void InsertSessionHistory()
        {
            try
            {
                if (Session.SessionID != null)
                {
                    DBUtility db = new DBUtility();
                    Hashtable ht = new Hashtable();
                    ht.Add("SessionId ", Session.SessionID);
                    ht.Add("PIN ", Session[SessionInfo.PIN]);
                    ht.Add("SessionTime ", Session.Timeout);
                    ht.Add("SourceApp ", "SelfCare");
                    ht.Add("HashKey ", Session[SessionInfo.HashKey]);
                    ht.Add("LoggedInIP ", GetUserIP());
                    var data = db.GetDataByProc(ht, "sp_SelfCare_InsertUserSession");

                }
            }
            catch (Exception)
            {


            }

        }

        public void LogOut()
        {

            LogoutSessionHistory();

            //Session.Remove(SessionInfo.PIN);
            //Session.Remove(SessionInfo.user_name);
            //Session.Remove(SessionInfo.loginid);
            //Session.Remove(SessionInfo.HashID);
            //Session.Remove(SessionInfo.HashKey);


            Session.Remove(SessionInfo.user_name);
            Session.Remove(SessionInfo.PIN);
            Session.Remove(SessionInfo.loginid);
            Session.RemoveAll();
            Session.Clear();
            Session.Abandon();
            Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddYears(-1);
            Response.Redirect("~/SignIn.aspx");
        }

        private void LogoutSessionHistory()
        {
            try
            {
                if (Session.SessionID != null)
                {
                    DBUtility db = new DBUtility();
                    Hashtable ht = new Hashtable();
                    ht.Add("SessionId ", Session.SessionID);
                    var data = db.GetDataByProc(ht, "sp_SelfCare_LogoutUserSession");
                }

            }
            catch (Exception)
            {


            }
        }
    }
}