using System.Web;
using System.Text;
using System.Web.UI;


public static class Alert
{
    //design and coding by Rezaul


    public static void Show(string message)
    {

        Page page = HttpContext.Current.CurrentHandler as Page;

        if (page != null)
        {
            ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", string.Format("alert('{0}');", message), true);
        }
    }

    public static void ShowMessage(string message)
    {

        message = message.Replace("'", "@").Replace("/", "@").Replace("<", "@").Replace("\\", "@").Replace(">", "@");

        Page page = HttpContext.Current.CurrentHandler as Page;

        if (page != null)
        {
            ScriptManager.RegisterStartupScript(page, page.GetType(), "Javascript", "javascript: ValidationMessage('" + message + "');", true);
        }
    }



}