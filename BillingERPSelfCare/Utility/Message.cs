using System.Web;
using System.Web.UI;

namespace BillingERPSelfCare.Utility
{
    public class Message
    {
        public static void Show(string message)
        {
            Page page = HttpContext.Current.CurrentHandler as Page;

            if (page != null)
            {
                ScriptManager.RegisterStartupScript(page, page.GetType(), "Javascript", "javascript: Message('" + message + "');", true);
            }
        }
    }
}