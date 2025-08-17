using ReportBilling;
using System;

namespace ReportBilling
{
    /// <summary>
    /// Summary description for InvoiceReport.
    /// </summary>
    public partial class REPORT_INVOICEVAT : Telerik.Reporting.Report
    {
        public REPORT_INVOICEVAT()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            sdc_BillingInfo.ConnectionString = ReportConnectionManager.GetConnectionString();
            sdc_InvoiceInfo.ConnectionString = ReportConnectionManager.GetConnectionString();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        public string RefNo
        {
            get
            {
                return (string)sdc_InvoiceInfo.Parameters[0].Value;
            }
            set
            {
                sdc_InvoiceInfo.Parameters[0].Value = value;
                sdc_BillingInfo.Parameters[0].Value = value;
            }

        }

        public static object EtoWDO(object value1)
        {
            double d1 = Convert.ToDouble(value1);

            BBSClass ns1 = new BBSClass();
            return ns1.changeNumericToWords(d1);
        }
    }
}