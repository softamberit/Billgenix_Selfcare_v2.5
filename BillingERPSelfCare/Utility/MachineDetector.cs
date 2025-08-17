using System.Net;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.Security;


using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
namespace BillingERPSelfCare.Utilitys
{
    public static class MachineDetector
    {

        public static string GetIpAddress()  // Get IP Address
        {
            string ip = "";
            try
            {
                
                IPHostEntry ipEntry = Dns.GetHostEntry(GetCompCode());
                IPAddress[] addr = ipEntry.AddressList;
                ip = addr[2].ToString();
                
            }
            catch
            {
                ip = "";
            }
            return ip;
        }
        public static string GetCompCode()  // Get Computer Name
        {
            string strHostName = "";
            try
            {
                
                strHostName = Dns.GetHostName();
                return strHostName;
            }
            catch
            { 
                strHostName="";
            }
            return strHostName;
        }


        public static string GetUser_PublicIP()
        {

            try
            {
           
                string VisitorsIPAddr = string.Empty;
                if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                {
                    VisitorsIPAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                }
                else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
                {
                    VisitorsIPAddr = HttpContext.Current.Request.UserHostAddress;
                }

                return VisitorsIPAddr;
            }
            catch { return ""; }
        }

        public static string GetDevice(Page _page)
        {
            string Device = "";

            try
            {


                string strUserAgent = _page.Request.UserAgent.ToString().ToLower();
                if (strUserAgent != null)
                {
                    if (_page.Request.Browser.IsMobileDevice == true || strUserAgent.Contains("iphone") ||
                            strUserAgent.Contains("blackberry") || strUserAgent.Contains("mobile") ||
                                strUserAgent.Contains("windows ce") || strUserAgent.Contains("opera mini") ||
                                strUserAgent.Contains("palm"))
                    {
                        Device = "Mobile: ";
                    }
                    else
                    {
                        Device = "Computer: ";
                    }
                }
            }
            catch
            {
                Device = "";

            }

            return Device;

        }



        public static string GetAccessingDevice(Page _page)
        {
            string DeviceInformation = "";

            string comp_name = GetCompCode();
            string internamlIp = GetIpAddress();

            string publicIP = GetUser_PublicIP();

            string device = GetDevice(_page);



            DeviceInformation = "Device: " + device + ", Name: " + comp_name + ", InternalIP: " + internamlIp + ", PublicIP: " + publicIP;


            return DeviceInformation;
        }


    }
}