using System;
using BillingERPSelfCare.Session;

namespace BillingERPSelfCare
{
    public partial class LogOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           ///Clear All Session

            Session.Remove(SessionInfo.user_name);
            Session.Remove(SessionInfo.PIN);
            Session.Remove(SessionInfo.loginid);
            Session.Remove(SessionInfo.Bkash_paymentID);
            Session.Remove(SessionInfo.Bkash_id_token);
            Session.Remove(SessionInfo.agreementID);
            Session.RemoveAll();
            Session.Clear();
            Session.Abandon();
            Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddYears(-1);
            Response.Redirect("~/SignIn.aspx");
        }
            
        
    }
}