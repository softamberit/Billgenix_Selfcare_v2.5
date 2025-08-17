using BillingERPConn;
using BillingERPSelfCare.Session;
using BillingERPSelfCare.Utility;
using System;
using System.Collections;

namespace BillingERPSelfCare
{
    public partial class ResetPassOtp : SignInUtility
    {
        readonly DBUtility db = new DBUtility();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                divPhnMaskss.Visible = false;
                divSMSOption.Visible = false;
            }
        }

        protected void btnSearchUserName_Click(object sender, EventArgs e)
        {
            try
            {
                divPhnMaskss.Visible = true;
                string username = txtUserNames.Text;
                string MobileNo = "";
                string userid = "";
                string hashId = "";


                if (!string.IsNullOrEmpty(username))
                {

                    Hashtable ht = new Hashtable();
                    ht.Add("LoginId", username);

                    var data = db.GetDataByProc(ht, "sp_SelfCare_checkCustomerAccessForReset");

                    if (data != null && data.Rows.Count > 0)
                    {
                        MobileNo = data.Rows[0]["MobileNo"].ToString();
                        userid = data.Rows[0]["loginid"].ToString();
                    }

                    if (data != null && userid == username)
                    {
                        //txtPhnMaskss.Visible = true;
                        divSMSOption.Visible = true;
                        //btnSubmitNumbersCall.Visible = true;
                        txtConfirmMobiles.Visible = true;
                        txtPhnMaskss.Text = FormatPhoneNumber(MobileNo); // MobileNo.ToString().Remove(0, 2);
                        btnSearchUserNames.Visible = false;
                        txtUserNames.Enabled = false;
                        LoginMsg.Text = "Enter valid phone number";
                        //Session.Add(SessionInfo.HashID, hashId);
                        Session.Add(SessionInfo.phone, MobileNo);

                    }
                    else
                    {
                        LoginMsg.Text = "No User Found";
                        divPhnMaskss.Visible = false;

                    }


                }

                else
                {
                    LoginMsg.Text = "Please enter valid CID";
                    divPhnMaskss.Visible = false;
                }

            }
            catch (Exception ex)
            {

                Message.Show(ex.Message + " No User Exists");
            }

        }
        private string FormatPhoneNumber(string phoneNumber)
        {
            phoneNumber = phoneNumber.ToString().Remove(0, 2);
            if (phoneNumber.Length == 11)
            {
                return $"{phoneNumber.Substring(0, 3)}******{phoneNumber.Substring(9, 2)}";
            }
            return phoneNumber;
        }

        protected void btnSubmitNumber_Click(object sender, EventArgs e)
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
        //protected void btnSubmitNumberCall_Click(object sender, EventArgs e)
        //{

        //    login(2);


        //}
        protected void SendOtp(int sms_media)
        {
            string number = txtConfirmMobiles.Text.ToString();
            if (!string.IsNullOrEmpty(number) && (number.Length == 11))
            {
                txtConfirmMobiles.Visible = false;
                string MobileNo = Session[SessionInfo.phone].ToString();
                if (number.Length == 11)
                {
                    if (!number.StartsWith("88"))
                    {
                        number = "88" + number;
                    }
                }

                if (MobileNo == null)
                {
                    LoginMsg.Text = "Your session is out";

                }

                if (number != null)
                {

                    if (MobileNo == number)

                    {
                        Random random = new Random();
                        string code = random.Next(100000, 999999).ToString();
                        string smsText = string.Format("Use the OTP {0} to reset your password from My Swift. Amber IT", code);
                        var hashKey = Guid.NewGuid().ToString();
                        Session.Add(SessionInfo.HashKey, hashKey);
                        Hashtable ht = new Hashtable();
                        ht.Add("Mobile", MobileNo);
                        ht.Add("SMSText", smsText);
                        ht.Add("SMSType", 31);
                        ht.Add("IsSend", 0);
                        ht.Add("InitializationTime", DateTime.Now);
                        ht.Add("UserName", txtUserNames.Text.ToString());
                        ht.Add("VerificationCode", code);
                        ht.Add("HashKey", hashKey);
                        ht.Add("UseSMSPortal", sms_media);


                        long res = db.InsertData(ht, "sp_SelfCare_SendOtpFromLogin");
                        if (res == 1)
                        {
                            LoginMsg.Text = "OTP Sent Successfull, Please Check Your Mobile";
                            txtUserNames.Enabled = false;
                            txtVerifyCodes.Visible = true;
                            btnSubmitCode.Visible = true;
                            divSMSOption.Visible = false;
                            //btnSubmitNumbersCall.Visible = false;
                            Session.Remove(SessionInfo.phone);

                        }
                        else
                        {
                            LoginMsg.Text = "OTP can not send";
                            divPhnMaskss.Visible = true;
                            txtConfirmMobiles.Visible = true;
                            txtVerifyCodes.Visible = false;
                        }

                    }
                    else
                    {

                        LoginMsg.Text = "Number Not Matched";
                        divPhnMaskss.Visible = true;
                        txtConfirmMobiles.Visible = true;
                        txtVerifyCodes.Visible = false;
                    }
                }

                else
                {

                    LoginMsg.Text = "Please Enter Correct Mobile Number";
                    divPhnMaskss.Visible = true;
                    txtConfirmMobiles.Visible = true;
                    txtVerifyCodes.Visible = false;

                }
            }
            else
            {
                LoginMsg.Text = "Mobile Number length is not valid";
                divPhnMaskss.Visible = true;
                txtConfirmMobiles.Visible = true;
                txtVerifyCodes.Visible = false;
            }
        }
        protected void btnSubmitCode_Click(object sender, EventArgs e)
        {

            if (Session[SessionInfo.HashKey] == null)
            {
                LoginMsg.Text = "Your session is expired";
                return;
            }
            string hashKey = Session[SessionInfo.HashKey].ToString();
            string codes = txtVerifyCodes.Text.ToString();
            string loginId = txtUserNames.Text.ToString();

            if (codes != null && loginId != null)
            {
                Hashtable ht_code = new Hashtable();
                ht_code.Add("HashKey", hashKey);
                var data_code = db.GetDataByProc(ht_code, "sp_SelfCare_GetVerificationByHashKey");


                if (data_code != null && data_code.Rows.Count > 0)
                {
                    string verified_code = data_code.Rows[0]["VerificationCode"].ToString();
                    DateTime sendTime = Conversion.TryCastDate(data_code.Rows[0]["SendDate"].ToString());
                    var Id = data_code.Rows[0]["ID"].ToString();
                    var nowDate = DateTime.Now;
                    var attempt = data_code.Rows[0]["Attempt"].ToString();

                    if (verified_code.ToString() != null && verified_code.ToString() == codes)
                    {
                        if ((nowDate - sendTime).TotalMinutes <= 5)
                        {
                            if (OtpUpdate(Id, 1, loginId))
                            {
                                Session.Add(SessionInfo.loginid, data_code.Rows[0]["LoginID"].ToString());
                                Session.Add(SessionInfo.phone, data_code.Rows[0]["Mobile"].ToString());
                                //Session.Add(SessionInfo.loginid, loginId);

                                Response.Redirect("ResetPassword.aspx", false);
                            }
                            else
                            {
                                LoginMsg.Text = "Try Again";
                            }
                        }
                        else
                        {
                            OtpUpdate(Id, 3, loginId);
                            LoginMsg.Text = "OTP is expired";

                        }
                    }
                    else
                    {
                        if (Conversion.TryCastInteger(attempt) < 2)
                        {
                            OtpUpdate(Id, 4, loginId);// UPDATE WRONG ATTEMPT
                            LoginMsg.Text = $"You can try {2 - Conversion.TryCastInteger(attempt)} times";
                        }
                        else
                        {
                            OtpUpdate(Id, 2, loginId);
                            LoginMsg.Text = "Your OTP is invalid or has expired. Please return to the login page.";
                            btnSubmitCode.Visible = false;
                        }
                    }


                }
                else
                {
                    LoginMsg.Text = "OTP is Not Valid";


                }

            }

            else
            {
                LoginMsg.Text = "Login ID or Code is Empty";
            }


        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            divPhnMaskss.Visible = false;
            Session.RemoveAll();
            RemoveSession();
            Response.Redirect("SignIn.aspx");
        }

        private bool OtpUpdate(string Id, int status, string loginId)
        {
            try
            {


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
        protected void txtPhnMaskss_TextChanged(object sender, EventArgs e)
        {

        }
    }
}