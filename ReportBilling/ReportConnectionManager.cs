using BillingERPConn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportBilling
{
    public class ReportConnectionManager
    {
        public static string GetConnectionString()
        {
            return DBUtility.GetConnectionStringForReport();
        }
    }
}
