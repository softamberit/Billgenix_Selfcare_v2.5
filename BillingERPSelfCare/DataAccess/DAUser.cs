using BillingERPSelfCare.BusinessEntity;
using BillingERPConn;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BillingERPSelfCare.DataAccess
{
    public class DAUser
    {
        DBUtility objDBUtility = new DBUtility();
        public DAUser()
        { 
        }

        public DataTable UserAccess(BOUser objUser)
        {

            try
            {
            Hashtable ht = new Hashtable();
            ht.Add("loginId", objUser.LoginId);
            ht.Add("password", objUser.Password);
            return objDBUtility.GetDataByProc(ht, "sp_SelfCare_checkCustomerAccess");
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public string PasswdChange(string Pin, string LogInID, string Passwd, string oldPass)
        {
            Hashtable ht = new Hashtable();
            ht.Add("Pin", Pin);
            ht.Add("LogInID", LogInID);
            ht.Add("Passwd", Passwd);
            ht.Add("OldPass", oldPass);

            //return Convert.ToInt64(objDBUtility.InsertData(ht, "sp_UserPasswdChange"));

            DataTable dt = objDBUtility.GetDataByProc(ht, "sp_UserPasswdChangeSelfCare");

            string feedback = string.Empty;

            foreach (DataRow dataRow in dt.Rows)
            {
                feedback = dataRow["Feedback"].ToString();
            }
            return feedback;

        }

        public long InsertUserInfo(BOUser BOUserObj)
        {

            try
            {
                Hashtable ht = new Hashtable();

                ht.Add("ProcessID", BOUserObj.ProcessID);
                ht.Add("UserID", BOUserObj.UserID);
                ht.Add("PinNo", BOUserObj.PinNo);
                ht.Add("LogInID", BOUserObj.LoginId);
                ht.Add("Password", BOUserObj.Password);
                ht.Add("Mobile", BOUserObj.Mobile);
                ht.Add("UserName", BOUserObj.UserName);
                ht.Add("Email", BOUserObj.Email);
                ht.Add("IsERPUser", BOUserObj.IsERPUser);
                return Convert.ToInt64(objDBUtility.InsertData(ht, "sp_insertuserentry"));
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message.ToString());
                return 0;
            }
        }
        
    }
}