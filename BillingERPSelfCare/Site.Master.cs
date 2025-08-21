
using BillingERPConn;
using BillingERPSelfCare.Session;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BillingERPSelfCare
{
    public partial class SiteMaster : MasterPage
    {
        //private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        //private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        //private string _antiXsrfTokenValue;

        //protected void Page_Init(object sender, EventArgs e)
        //{
        //    // The code below helps to protect against XSRF attacks
        //    var requestCookie = Request.Cookies[AntiXsrfTokenKey];
        //    Guid requestCookieGuidValue;
        //    if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
        //    {
        //        // Use the Anti-XSRF token from the cookie
        //        _antiXsrfTokenValue = requestCookie.Value;
        //        Page.ViewStateUserKey = _antiXsrfTokenValue;
        //    }
        //    else
        //    {
        //        // Generate a new Anti-XSRF token and save to the cookie
        //        _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
        //        Page.ViewStateUserKey = _antiXsrfTokenValue;

        //        var responseCookie = new HttpCookie(AntiXsrfTokenKey)
        //        {
        //            HttpOnly = true,
        //            Value = _antiXsrfTokenValue
        //        };
        //        if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
        //        {
        //            responseCookie.Secure = true;
        //        }
        //        Response.Cookies.Set(responseCookie);
        //    }

        //    Page.PreLoad += master_Page_PreLoad;
        //}

        //protected void master_Page_PreLoad(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        // Set Anti-XSRF token
        //        ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
        //        ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
        //    }
        //    else
        //    {
        //        // Validate the Anti-XSRF token
        //        if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
        //            || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
        //        {
        //            throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
        //        }
        //    }
        //}


        protected void Page_Load(object sender, EventArgs e)
        {
            // DBUtility objDBUitility = new DBUtility();

            if (Session["PIN"] != null)
            {

                string PinNo = Session[SessionInfo.PIN].ToString();
                string IsOnlineCust = Session[SessionInfo.IsOnlineCust]?.ToString();
                string IsAllowedGraph = Session[SessionInfo.IsAllowedGraph]?.ToString();

                if (IsAllowedGraph == "1")
                {
                    mntraffic.Visible = true;

                } else
                {
                    mntraffic.Visible = false;

                }


                if (IsOnlineCust == "True")
                {
                    lnkDocUploadMenu.Visible = true;
                    DivHyperLink.Visible = true;
                }
                else
                {
                    lnkDocUploadMenu.Visible = false;
                    DivHyperLink.Visible = true;
                }

                // Literal menuLiteral = ((Literal)PageUtility.FindControlIterative(this.Master, "ContentMenuLiteral"));

                //  string menu = DAMenu.GetContentPageMenu(PinNo);
                // ContentMenuLiteral.Text = menu;

                lblUser.Text = Session["user_name"].ToString();
            }

            else
            {
                Response.Redirect("~/LogOut.aspx");

            }
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut();
        }
    }

}